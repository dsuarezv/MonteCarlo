using System;

namespace MonteCarlo
{
    public class ModelInput: ModelObject
    {
        public Distribution Distribution { get; set; } 


        public ModelInput()
        {
     
        }


        public override Distribution GetOutput()
        {
            return Distribution;
        }


        public override string[] GetInputNames()
        {
            return null;
        }
    }
}

