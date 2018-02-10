using HostLinkKeyenceBase;
using LiveCharts;
using LiveCharts.Wpf;
using MachineModel;
using MVVMBaseLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismTest
{
    public class KeyenceMachineModel: ViewModelBase
    {
        public KeyenceMachineModel()
        {
            ImageSrc = @".\Resources\canon.png";
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Avaibility rate",
                    Values = new ChartValues<double> { 1,2,3,4,5,6,7 },
                    PointGeometry = DefaultGeometries.Square,
                },
                new LineSeries
                {
                    Title = "Performance",
                    Values = new ChartValues<double> { 2,3,4,5,4,3,2 },
                    PointGeometry = DefaultGeometries.Circle,
                }
            };

            XAxis = new[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
            YFormatter = value => value.ToString("%");
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public string ProductModel { get; set; }
        public PlcMemory Actual { get; set; }
        public PlcMemory Target { get; set; }
        public PlcMemory StopTime { get; set; }
        public PlcMemory RunningTime { get; set; }
        public PlcMemory Chokotei { get; set; }
        public PlcMemory AvailabilityRate { get; set; }
        public PlcMemory Performance { get; set; }
        

        public SeriesCollection SeriesCollection { get; set; }

        public string[] XAxis { get; set; }

        public Func<double, string> YFormatter { get; set; }

        private string _ImageSrc;
        public string ImageSrc
        {
            get
            {
                return _ImageSrc;
            }
            set
            {
                _ImageSrc = value;
                RaisePropertyChanged("ImageScr");
            }
        }
    }
}
