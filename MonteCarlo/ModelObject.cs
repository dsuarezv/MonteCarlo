using System;

namespace MonteCarlo
{
    public abstract class ModelObject
    {
        public string Name { get; set; }
        public string Alias { get; set; }


        public ModelObject()
        {
        }

        public abstract Distribution GetOutput();

        public abstract string[] GetInputNames();
    }
}

