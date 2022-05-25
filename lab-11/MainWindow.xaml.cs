using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace lab_11
{
    record Rate
    {
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("ask")]
        public decimal Ask { get; set; }
        [JsonPropertyName("bid")]
        public decimal Bid { get; set; }
        public Rate(string Currency, string Code, decimal Ask, decimal Bid)
        {
            this.Currency = Currency;
            this.Code = Code;
            this.Ask = Ask;
            this.Bid = Bid;
        }

        public Rate()
        {

        }
    };
    class TableRates
    {
        [JsonPropertyName("table")]
        public string Table { get; set; }
        [JsonPropertyName("no")]
        public string Number { get; set; }
        [JsonPropertyName("tradingDate")]
        public DateTime TradingDate { get; set; }
        [JsonPropertyName("effectiveDate")]
        public DateTime EffectiveDate { get; set; }
        [JsonPropertyName("rates")]
        public List<Rate> Rates { get; set; }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        Dictionary<string, Rate> Rates = new Dictionary<string, Rate>();

        private void DownloadData()
        {
            CultureInfo culture = CultureInfo.InvariantCulture;
            WebClient client = new WebClient();
            client.Headers.Add("Accept", "application/xml");
            string xmlRate = client.DownloadString("http://api.nbp.pl/api/exchangerates/tables/C/");
            XDocument rateDoc = XDocument.Parse(xmlRate);
            var rates = rateDoc
                .Element("ArrayOfExchangeRatesTable")
                .Element("ExchangeRatesTable")
                .Element("Rates")
                .Elements("Rate")
                .Select(x => new Rate(
                    x.Element("Currency").Value,
                    x.Element("Code").Value,
                    decimal.Parse(x.Element("Ask").Value,culture),
                    decimal.Parse(x.Element("Bid").Value,culture)
                    ));

            foreach(Rate rate in rates)
            {
                Rates.Add(rate.Code, rate);
            }
            Rates.Add("PLN",new Rate("złoty","PLN",1,1));
        }
        public MainWindow()
        {
            InitializeComponent();
            DownloadJsonData();
            UpdateGui();
        }

        private void UpdateGui()
        {
            OutputCurrencyCode.Items.Clear();
            InputCurrencyCode.Items.Clear();
            foreach (var rate in Rates.Keys)
            {
                OutputCurrencyCode.Items.Add(rate);
                InputCurrencyCode.Items.Add(rate);
            }
            OutputCurrencyCode.SelectedIndex = 0;
            InputCurrencyCode.SelectedIndex = 1;
        }

        private void DownloadJsonData()
        {
            WebClient client = new WebClient();
            client.Headers.Add("Accept", "application/json");
            string json = client.DownloadString("http://api.nbp.pl/api/exchangerates/tables/C/");
            List<TableRates> tableRates = JsonSerializer.Deserialize<List<TableRates>>(json);
            TableRates table = tableRates[0];
            table.Rates.Add(new Rate() { Currency = "Złoty", Code = "PLN", Ask = 1, Bid = 1 });
            foreach(Rate rate in table.Rates)
            {
                Rates.Add(rate.Code, rate);
            }
        }

        private void CalcResult(object sender, RoutedEventArgs e)
        {
            Rate inputRate = Rates[InputCurrencyCode.Text];
            Rate outputRate = Rates[OutputCurrencyCode.Text];
            if(decimal.TryParse(InputAmount.Text, out decimal amount))
            {
                decimal result = amount * inputRate.Ask / outputRate.Ask;
                OutputAmount.Text = result.ToString("N2");
            }
        }

        private void LoadFromTxtFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Wybierz plik tekstowy z notowaniami";
            dialog.Filter = "Plik tekstowy (*.txt)|*.txt";
            if(dialog.ShowDialog() == true)
            {
                if(File.Exists(dialog.FileName)) {
                    string[] lines = File.ReadAllLines(dialog.FileName);
                    Rates.Clear();
                    foreach(string line in lines)
                    {
                        string[] tokens = line.Split(";");
                        string code = tokens[0];
                        string currency = tokens[1];
                        string askStr = tokens[2];
                        string bidStr = tokens[3];
                        if(decimal.TryParse(askStr, out decimal ask) && decimal.TryParse(bidStr, out decimal bid))
                        {
                            Rate rate = new Rate() { Code = code, Currency = currency, Ask = ask, Bid = bid };
                            Rates.Add(rate.Code, rate);
                        }
                    }
                    UpdateGui();
                }
            }
        }

        private void SaveToJsonFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Zapisz notowania do pliku JSON";
            dialog.Filter = "Plik JSON (*.json)|*.json";
            if(dialog.ShowDialog() == true)
            {
                if(File.Exists(dialog.FileName))
                {
                    string serializedRates = JsonSerializer.Serialize(Rates);
                    File.WriteAllText(dialog.FileName, serializedRates);
                }
            }

        }

        private void LoadFromJSONFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Odczytaj notowania z JSON";
            dialog.Filter = "Plik JSON (*.json)|*.json";
            if(dialog.ShowDialog() == true)
            {
                if(File.Exists(dialog.FileName))
                {
                    List<Rate> rates = JsonSerializer.Deserialize<List<Rate>>(File.ReadAllText(dialog.FileName));
                    Rates.Clear();
                    foreach(Rate rate in rates)
                    {
                        Rates.Add(rate.Code, rate);
                    }
                    UpdateGui();
                }
            }
        }

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            string oldText = InputAmount.Text;
            string deltaText = e.Text;
            e.Handled = !decimal.TryParse(oldText + deltaText, out decimal val);
        }
    }
}
