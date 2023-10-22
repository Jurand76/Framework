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
        readonly ConfigModel configModel = ConfigModel.GetConfiguration();

        public ComputingInstance()
        {
            this.numberOfInstances = configModel.Number_of_instances;
            this.series = configModel.Series;
            this.machineType = configModel.Machine_type;
            this.existsGPU = configModel.Exists_GPU;
            this.numberOfGPU = configModel.Number_of_GPU;
            this.typeOfGPU = configModel.Type_of_GPU;
            this.localSSD = configModel.Local_SSD;
            this.dataCenter = configModel.Datacenter;
            this.committedUsage = configModel.Committed_usage;
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

        public string getCommittedUsage()
        {
            return this.committedUsage;
        }
    }
}


