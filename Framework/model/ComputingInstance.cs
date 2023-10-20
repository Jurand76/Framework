using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.model
{
    public class ComputingInstance
    {
        private readonly int numberOfInstances;
        private readonly string series;
        private readonly string machineType;
        private bool existsGPU;
        private int numberOfGPU;

        public ComputingInstance()
        {
            this.numberOfInstances = 4;
            this.series = "N1";
            this.machineType = "n1-standard-8";
            this.existsGPU = true;
            this.numberOfGPU = 1;
        }

        public string getNumberOfInstances()
        {
            return this.numberOfInstances.ToString();
        }

        public string getSeries()
        {
            return this.series;
        }

        public string getMachineType()
        {
            return this.machineType.ToLower();
        }

        public bool getGPUExistence()
        {
            return this.existsGPU;
        }


    }
}


