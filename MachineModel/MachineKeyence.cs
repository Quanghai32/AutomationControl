using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineModel
{
    public class MachineKeyence
    {
        public MachineKeyence()
        {
            IpAddress = "192.168.0.10";
            Port = 8501;
            Actual = new KeyenceMemory();
            Target = new KeyenceMemory();
            StopTime = new KeyenceMemory();
            RunningTime = new KeyenceMemory();
            Chokotei = new KeyenceMemory();
            AvailabilityRate = new KeyenceMemory();
            Performance = new KeyenceMemory();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public KeyenceMemory Actual { get; set; }
        public KeyenceMemory Target { get; set; }
        public KeyenceMemory StopTime { get; set; }
        public KeyenceMemory RunningTime { get; set; }
        public KeyenceMemory Chokotei { get; set; }
        public KeyenceMemory AvailabilityRate { get; set; }
        public KeyenceMemory Performance { get; set; }
        public string ProductModel { get; set; }
    }
}
