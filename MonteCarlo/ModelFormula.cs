using System;

namespace MonteCarlo
{
    public class ModelFormula: ModelObject
    {
        public string Formula { get; set; }

        public ModelFormula()
        {
        }



        public override Distribution GetOutput()
        {
            throw new NotImplementedException();
        }

        public override string[] GetInputNames()
        {
            throw new NotImplementedException();
        }
    }
}

