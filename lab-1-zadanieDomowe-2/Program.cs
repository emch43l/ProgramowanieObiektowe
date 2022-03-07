using System;

namespace lab_1_zadanieDomowe_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
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
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Width cannot be less than 0");
                _width = value;
            }
        }

        public float length
        {
            get { return _length; }
            set
            {
                if (value <= 0)
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
        
    }
}
