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
            ImageSrc = @".\canon.png";
            SetupChart();
        }

        #region "Property"
        public int Id { get; set; }
        public string Name { get; set; }
        //public string IpAddress { get; set; }
        //public int Port { get; set; }
        public HostlinkKeyence MyHost { get; set; }
        public string ProductModel { get; set; }
        public PlcMemory Actual { get; set; }
        public PlcMemory Target { get; set; }
        public PlcMemory StopTime { get; set; }
        public PlcMemory RunningTime { get; set; }
        public PlcMemory Chokotei { get; set; }
        public PlcMemory AvailabilityRate { get; set; }
        public PlcMemory Performance { get; set; }
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
        #endregion

        #region "Charting"
        private void SetupChart()
        {
            ChartCollection = new SeriesCollection
            {   
                new LineSeries
                {
                    Title = "Performance",
                    Values = new ChartValues<double> { 2,3,4,5,4,3,2 },
                    PointGeometry = DefaultGeometries.Circle,
                },
                new LineSeries
                {
                    Title = "Avaibility rate",
                    Values = new ChartValues<double> { 1,2,3,4,5,6,7 },
                    PointGeometry = DefaultGeometries.Square,
                },
            };

            XAxis = new[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
            YFormatter = value => value.ToString("%");
        }
        public SeriesCollection ChartCollection { get; set; }

        public string[] XAxis { get; set; }

        public Func<double, string> YFormatter { get; set; }
        #endregion

    }
}
