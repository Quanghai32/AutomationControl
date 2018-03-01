using HostLinkKeyenceBase;
using LiteDB;
using MachineModel;
using MVVMBaseLib;
using PrismTest.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrismTest
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            InitCommand();
            LoadSettingFile();
        }

        ~MainViewModel()
        {
            SaveHistory();
        }




        #region "Command"
        public ICommand AddCommand { get; set; }
        public ICommand OpenSettingCommand { get; set; }
        public ICommand ClearHistoryCommand { get; set; }

        private void InitCommand()
        {
            //AddCommand = new RelayCommand<UIElementCollection>(
            //    (s) => true,
            //    (s) => { OpenSetting(s); });

            OpenSettingCommand = new RelayCommand(() => { OpenSetting_Execute(); });
            ClearHistoryCommand = new RelayCommand(() => { ClearHistory_Execute(); });
        }

        private void ClearHistory_Execute()
        {
            
        }

        private void OpenSetting_Execute()
        {
            Process.Start("SettingMachine.exe");
        }
        #endregion

        #region "Feild"

        #endregion

        #region "Property"
        public ObservableCollection<KeyenceMachineViewModel> ListMachine
        {
            get;
            set;
        }
        #endregion

        #region "Method"
        private void SaveHistory()
        {
           
        }




        private void LoadSettingFile()
        {
            ListMachine = new ObservableCollection<KeyenceMachineViewModel>();
            List<SettingKeyenceMachine> tempList = JsonDB.Instance.Read<List<SettingKeyenceMachine>>(@"./SETUP/Setting.txt");

            if (tempList == null)
            {
                MessageBox.Show("Don't have data. Please check setting file!!!");
                return;
            }
            foreach (SettingKeyenceMachine m in tempList)
            {
                HostlinkKeyence host = new HostlinkKeyence()
                {
                    IP = m.IpAddress,
                    port = m.Port,
                };
                ListMachine.Add(new KeyenceMachineViewModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                    ProductModel = m.ProductModel,
                    MyHost = host,
                    ImageSrc = m.ImageSrc,
                    Actual = { MemoryType = m.Actual.TypeSymbol, PLCAddress = m.Actual.MemoryAddress, DataFormat = m.Actual.FormatSymbol, PLCLink = host },
                    Target = { MemoryType = m.Target.TypeSymbol, PLCAddress = m.Target.MemoryAddress, DataFormat = m.Target.FormatSymbol, PLCLink = host },
                    StopTimeHour = { MemoryType = m.StopTimeHour.TypeSymbol, PLCAddress = m.StopTimeHour.MemoryAddress, DataFormat = m.StopTimeHour.FormatSymbol, PLCLink = host },
                    StopTimeMin = { MemoryType = m.StopTimeMin.TypeSymbol, PLCAddress = m.StopTimeMin.MemoryAddress, DataFormat = m.StopTimeMin.FormatSymbol, PLCLink = host },
                    StopTimeSec = { MemoryType = m.StopTimeSec.TypeSymbol, PLCAddress = m.StopTimeSec.MemoryAddress, DataFormat = m.StopTimeSec.FormatSymbol, PLCLink = host },

                    RunningTimeHour = { MemoryType = m.RunningTimeHour.TypeSymbol, PLCAddress = m.RunningTimeHour.MemoryAddress, DataFormat = m.RunningTimeHour.FormatSymbol, PLCLink = host },
                    RunningTimeMin = { MemoryType = m.RunningTimeMin.TypeSymbol, PLCAddress = m.RunningTimeMin.MemoryAddress, DataFormat = m.RunningTimeMin.FormatSymbol, PLCLink = host },
                    RunningTimeSec = { MemoryType = m.RunningTimeSec.TypeSymbol, PLCAddress = m.RunningTimeSec.MemoryAddress, DataFormat = m.RunningTimeSec.FormatSymbol, PLCLink = host },

                    Chokotei = { MemoryType = m.Chokotei.TypeSymbol, PLCAddress = m.Chokotei.MemoryAddress, DataFormat = m.Chokotei.FormatSymbol, PLCLink = host },
                    AvailabilityRate = { MemoryType = m.AvailabilityRate.TypeSymbol, PLCAddress = m.AvailabilityRate.MemoryAddress, DataFormat = m.AvailabilityRate.FormatSymbol, PLCLink = host },
                    Performance = { MemoryType = m.Performance.TypeSymbol, PLCAddress = m.Performance.MemoryAddress, DataFormat = m.Performance.FormatSymbol, PLCLink = host },
                });
            }
        }
        #endregion

    }


}
