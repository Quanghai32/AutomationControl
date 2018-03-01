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
using System.Windows.Input;

namespace PrismTest
{
    public class KeyenceMachineViewModel : ViewModelBase
    {
        public KeyenceMachineViewModel()
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

            InitCommand();
        }

        private void Performance_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Application.Current == null) return;
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                SaveHistory();
                //if(IsShowingDetail)
                {
                    //UpdateChart();
                }
            });
        }

        #region "Property"
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsShowingDetail { get; set; }
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
        public List<History> OldData { get; set; }
        public string Shift { get; set; }
        public string ShiftToViewHistory { get; set; }
        public DateTime Date { get; set; }
        public DateTime FromDate { get; set; } = DateTime.Today.AddDays(-5);
        public DateTime ToDate { get; set; } = DateTime.Today;



        #endregion

        #region "Feild"
        DetailView DetailForm;
        TimeSpan StartAShift = new TimeSpan(8, 5, 0);
        TimeSpan StopAShift = new TimeSpan(20, 5, 0);
        TimeSpan StartBShift = new TimeSpan(20, 5, 0);
        TimeSpan StopBShift = new TimeSpan(8, 5, 0);
        #endregion

        #region Command
        public ICommand ClickBorderCommand { get; set; }
        public ICommand GetHistoryCommand { get; set; }

        private void InitCommand()
        {
            ClickBorderCommand = new RelayCommand(() => ClickBorderCommand_Execute());
            GetHistoryCommand = new RelayCommand(() => GetHistoryCommand_Execute());
        }

        private void ClickBorderCommand_Execute()
        {
            if (DetailForm == null)
            {
                DetailForm = new DetailView();
                DetailForm.DataContext = this;
            }
            DetailForm.Show();
            IsShowingDetail = true;
        }

        private void GetHistoryCommand_Execute()
        {
            LiteDbHandle.Instance.LoadHistory(FromDate, ToDate, ShiftToViewHistory, ref performanceValue, ref availabilityValue, ref xAxis);
        }
        #endregion

        #region "Charting"
        public void SetupChart()
        {
            PerformanceValue = new ChartValues<ObservableValue>();

            AvailabilityValue = new ChartValues<ObservableValue>();


            //var Mapper = Mappers.Xy<ObservableValue>()
            //    .X((item, index) => index)
            //    .Y(item => item.Value);

            var performanceLine = new LineSeries
            {
                Title = "Performance",
                Values = PerformanceValue,
                StrokeThickness = 4,
                Fill = Brushes.Transparent,
                PointGeometrySize = 0,
                DataLabels = true,
                //Configuration = Mapper,
            };

            var availabilityLine = new LineSeries
            {
                Title = "Availibility rate",
                Values = AvailabilityValue,
                StrokeThickness = 4,
                Fill = Brushes.Transparent,
                PointGeometrySize = 0,
                DataLabels = true,
                //Configuration = Mapper
            };

            //PerformanceChart = new SeriesCollection { performanceLine, availabilityLine };
            DetailChart = new SeriesCollection { performanceLine, availabilityLine };

            XAxis = new List<string>();
            YFormatter = value => value + " " + value.ToString("%");
        }

        public SeriesCollection PerformanceChart { get; set; }
        public SeriesCollection DetailChart { get; set; }

        private ChartValues<ObservableValue> performanceValue;
        public ChartValues<ObservableValue> PerformanceValue
        {
            get
            {
                return performanceValue;
            }
            set
            {
                performanceValue = value;
            }
        }

        private ChartValues<ObservableValue> availabilityValue;
        public ChartValues<ObservableValue> AvailabilityValue
        {
            get
            {
                return availabilityValue;
            }
            set
            {
                availabilityValue = value;
            }
        }

        private List<string> xAxis;
        public List<string> XAxis
        {
            get
            {
                return xAxis;
            }
            set
            {
                xAxis = value;
            }
        }

        public Func<double, string> YFormatter { get; set; }

        private void UpdateChart()
        {
            if (PerformanceValue.Count == 0 || AvailabilityValue.Count == 0) return;
            double per = 0;
            Double.TryParse(Performance.MemoryValue, out per);
            PerformanceValue[PerformanceValue.Count - 1].Value = per;

            double ava = 0;
            Double.TryParse(AvailabilityRate.MemoryValue, out ava);
            AvailabilityValue[AvailabilityValue.Count - 1].Value = ava;
        }
        #endregion

        #region "Method"
        public void LoadHistory()
        {

        }

        public void SaveHistory()
        {
            CheckDateAndShift();
            LiteDbHandle.Instance.SaveHistory(this);
        }

        private void CheckDateAndShift()
        {
            DateTime checkTime = DateTime.Now;
            TimeSpan BeforeZeroHour = new TimeSpan(23, 59, 59);
            TimeSpan ZeroHour = new TimeSpan(0, 0, 0);

            if (checkTime.TimeOfDay >= StartAShift && checkTime.TimeOfDay <= StopAShift)
            {
                Shift = "A";
                Date = DateTime.Now;
                return;
            }
            else if (checkTime.TimeOfDay >= StartBShift && checkTime.TimeOfDay <= BeforeZeroHour)
            {
                Shift = "B";
                Date = DateTime.Now;
                return;
            }
            else if (checkTime.TimeOfDay >= ZeroHour && checkTime.TimeOfDay <= StopBShift)
            {
                Shift = "B";
                Date = checkTime.AddDays(-1);
                return;
            }
        }
        #endregion
    }



    //public class OldData
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public string Date { get; set; }
    //    public string Shift { get; set; }
    //    public double Performance { get; set; }
    //    public double AvailabilityRate { get; set; }
    //}
}
