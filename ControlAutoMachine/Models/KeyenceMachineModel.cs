using HostLinkKeyenceBase;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using MachineModel;
using MVVMBaseLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using LiveCharts.Defaults;
using System.IO;

namespace PrismTest
{
    public class KeyenceMachineModel : ViewModelBase
    {
        public KeyenceMachineModel()
        {
            SetupChart();
            Actual = new PlcMemory();
            Target = new PlcMemory();
            StopTimeHour = new PlcMemory();
            StopTimeMin = new PlcMemory();
            StopTimeSec = new PlcMemory();
            RunningTimeHour = new PlcMemory();
            RunningTimeMin = new PlcMemory();
            RunningTimeSec = new PlcMemory();
            Chokotei = new PlcMemory();
            AvailabilityRate = new PlcMemory();
            Performance = new PlcMemory();

            AvailabilityRate.PropertyChanged += Performance_PropertyChanged;
            Performance.PropertyChanged += Performance_PropertyChanged;
        }

        private void Performance_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Application.Current == null) return;
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                UpdateChart();
            });
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

        public PlcMemory StopTimeHour { get; set; }
        public PlcMemory StopTimeMin { get; set; }
        public PlcMemory StopTimeSec { get; set; }

        public PlcMemory RunningTimeHour { get; set; }
        public PlcMemory RunningTimeMin { get; set; }
        public PlcMemory RunningTimeSec { get; set; }

        public PlcMemory Chokotei { get; set; }
        public PlcMemory AvailabilityRate { get; set; }
        public PlcMemory Performance { get; set; }
        public string ImageSrc { get; set; }
        public List<OldData> OldData { get; set; }
        public string Shift { get; set; } = "AllDay";
        public string Date { get; set; } = "";

        #endregion

        #region "Charting"
        private void SetupChart()
        {
            PerformanceValue = new ChartValues<ObservableValue>
            {
                new ObservableValue(100),
                new ObservableValue(100),
                new ObservableValue(100),
                new ObservableValue(100)
            };

            AvailabilityValue = new ChartValues<ObservableValue>
            {
                new ObservableValue(100),
                new ObservableValue(100),
                new ObservableValue(100),
                new ObservableValue(100)
            };

            var Mapper = Mappers.Xy<ObservableValue>()
                .X((item, index) => index)
                .Y(item => item.Value);

            var performanceLine = new LineSeries
            {
                Values = PerformanceValue,
                StrokeThickness = 4,
                Fill = Brushes.Transparent,
                PointGeometrySize = 0,
                DataLabels = true,
                Configuration = Mapper,
            };

            var availabilityLine = new LineSeries
            {
                Values = AvailabilityValue,
                StrokeThickness = 4,
                Fill = Brushes.Transparent,
                PointGeometrySize = 0,
                DataLabels = true,
                Configuration = Mapper
            };

            PerformanceChart = new SeriesCollection { performanceLine, availabilityLine };
            XAxis = new[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
            //YFormatter = value => value.ToString("%");
        }

        public SeriesCollection PerformanceChart { get; set; }

        public ChartValues<ObservableValue> PerformanceValue { get; set; }
        public ChartValues<ObservableValue> AvailabilityValue { get; set; }
        public string[] XAxis { get; set; }

        public Func<double, string> YFormatter { get; set; }

        private void UpdateChart()
        {
            double per = 0;
            Double.TryParse(Performance.MemoryValue, out per);
            PerformanceValue[PerformanceValue.Count - 1].Value = per;

            double ava = 0;
            Double.TryParse(AvailabilityRate.MemoryValue, out ava);
            PerformanceValue[PerformanceValue.Count - 1].Value = ava;
        }
        #endregion

        #region "Method"
        public void LoadHistory()
        {
            List<string> arrayString = new List<string>();
            FileInfo[] fileInfor;
            DirectoryInfo diDirectoryInfo = new DirectoryInfo(@"./LOG/");
            fileInfor = diDirectoryInfo.GetFiles(Name + "*");

            for (int i = 1; i < 20; i++)
            {
                string day = DateTime.Today.AddDays(-i).ToString("yyyyMMdd");







                if (File.Exists(@"./LOG/" + day + ".txt"))
                {
                    arrayString.Add(day);
                }
                if (arrayString.Count >= 5) break;
            }



        }
        #endregion
    }



    public class OldData
    {
        public string Value { get; set; }
        public DateTime Date { get; set; }
    }
}
