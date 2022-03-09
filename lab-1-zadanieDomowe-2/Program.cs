using System;
using System.Collections.Generic;

namespace lab_1_zadanieDomowe_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Beam beam = new Beam(50, 200);
            Beam[] cutBeams = beam.cutToBeams(20);
            DumBeams(cutBeams);
            
        }

        static void DumBeams(Beam[] beams)
        {
            for(int i = 0; i < beams.Length; i++)
            {
                Console.WriteLine($"Beam {i + 1} L:{beams[i].length} W:{beams[i].width}");
            }
        }
    }

    class Beam
    {
        private float _width;
        private float _length;

        public float width
        {
            get { return _width; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Width cannot be less than 0");
                _width = value;
            }
        }

        public float length
        {
            get { return _length; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Length cannot be less than 0");
                _length = value;
            }
        }

        public Beam(float beamWidth, float beamLength)
        {
            length = beamLength;
            width = beamWidth;
        }

        public bool CutBeam(float cutLength)
        {
            if (cutLength > length)
                return false;
            length -= cutLength;
            return true;
        }

        public bool isEmpty()
        {
            return length == 0;
        }

        public float getBeamField()
        {
            return (float)length * width;
        }

        /// <summary>
        /// Cut Beam to one or more beams
        /// </summary>
        /// <param name="beamLength">Length of beams that will be cut</param>
        /// <returns>An array of beams</returns>

        public Beam[] cutToBeams(float beamLength)
        {
            List<Beam> beams = new List<Beam>();

            while(this.CutBeam(beamLength))
            {
                beams.Add(new Beam(width, beamLength));
            }

            return beams.ToArray();

        }
        
    }
}
