using System;
using System.Collections.Generic;

namespace MonteCarlo
{
    public abstract class ModelObject
    {
        private Distribution mOutput;

        public string Name { get; set; }
        public string Alias { get; set; }

        public Distribution Output
        {
            get { return mOutput; }
            protected set { mOutput = value; }
        }

        public ModelObject()
        {
        }

        public abstract void Solve(Model model);

        public abstract ICollection<string> GetNeededInputs();
    }
}

