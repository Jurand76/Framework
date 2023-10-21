using NUnit.Framework.Internal;
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
        private readonly bool existsGPU;
        private readonly int numberOfGPU;
        private readonly string typeOfGPU;
        private readonly string localSSD;
        private readonly string dataCenter;
        private readonly string committedUsage;

    
        public ComputingInstance()
        {
            this.numberOfInstances = 4;
            this.series = "N1";
            this.machineType = "n1-standard-8";
            this.existsGPU = true;
            this.numberOfGPU = 1;
            this.typeOfGPU = "NVIDIA Tesla V100";
            this.localSSD = "2x375 GB";
            this.dataCenter = "Frankfurt (europe-west3)";
            this.committedUsage = "1 Year";
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

        public int getNumberOfGPU()
        {
            return this.numberOfGPU;
        }

        public string getTypeOfGPU()
        {
            return this.typeOfGPU;
        }

        public string getLocalSSD()
        {
            return this.localSSD;
        }

        public string getDatacenter()
        {
            return this.dataCenter;
        }


    }
}


