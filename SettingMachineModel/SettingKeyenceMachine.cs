using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineModel
{
    public class SettingKeyenceMachine
    {
        public SettingKeyenceMachine()
        {
            IpAddress = "192.168.0.10";
            Port = 8501;
            Actual = new SettingKeyenceMemory();
            Target = new SettingKeyenceMemory();
            StopTimeHour = new SettingKeyenceMemory();
            StopTimeMin = new SettingKeyenceMemory();
            StopTimeSec = new SettingKeyenceMemory();
            RunningTimeHour = new SettingKeyenceMemory();
            RunningTimeMin = new SettingKeyenceMemory();
            RunningTimeSec = new SettingKeyenceMemory();
            Chokotei = new SettingKeyenceMemory();
            AvailabilityRate = new SettingKeyenceMemory();
            Performance = new SettingKeyenceMemory();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public SettingKeyenceMemory Actual { get; set; }
        public SettingKeyenceMemory Target { get; set; }
        
        public SettingKeyenceMemory RunningTimeHour { get; set; }
        public SettingKeyenceMemory RunningTimeMin { get; set; }
        public SettingKeyenceMemory RunningTimeSec { get; set; }

        public SettingKeyenceMemory StopTimeHour { get; set; }
        public SettingKeyenceMemory StopTimeMin { get; set; }
        public SettingKeyenceMemory StopTimeSec { get; set; }

        public SettingKeyenceMemory Chokotei { get; set; }

        public SettingKeyenceMemory AvailabilityRate { get; set; }
        public SettingKeyenceMemory Performance { get; set; }
        public string ProductModel { get; set; }
        public string ImageSrc { get; set; }
    }
}
