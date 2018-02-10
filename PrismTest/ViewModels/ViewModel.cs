using HostLinkKeyenceBase;
using MachineModel;
using MVVMBaseLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrismTest
{
    //public class ViewModel
    //{
    //    public ViewModel()
    //    {
    //        ListMachine = new ObservableCollection<ucMachine>();
    //        for (int i = 0; i < 5; i++)
    //        {
    //            ucMachine u = new ucMachine();
    //            u.Id = i;
    //            u.IsExpand = true;   //set false if you want to show small icon default
    //            //u.SetSize(50, 100);
    //            u.EventUpdateExpander += U_MyPersonalizedUCEvent;
    //            ListMachine.Add(u);
    //        }
    //    }

    //    private void U_MyPersonalizedUCEvent(ucMachine obj)
    //    {
    //        return;     //comment if you want to show small icon default
    //        if (obj.IsExpand == false)
    //        {
    //            obj.IsExpand = true;
    //        }
    //        else
    //        {
    //            obj.IsExpand = false;
    //        }
    //        foreach (ucMachine u in ListMachine)
    //        {
    //            if (u.Id != obj.Id)
    //            {
    //                u.IsExpand = false;
    //            }
    //        }

    //    }

    //    ObservableCollection<ucMachine> listMachine;
    //    public ObservableCollection<ucMachine> ListMachine
    //    {
    //        get;
    //        set;
    //    }
    //}

    public class MainViewModel
    {
        public MainViewModel()
        {
            InitCommand();
            LoadSettingFile();
        }

        private void LoadSettingFile()
        {
            ListMachine = new ObservableCollection<KeyenceMachineModel>();
            List<MachineKeyence> tempList = JsonDB.Instance.Read<List<MachineKeyence>>(@"./json.txt");
            foreach (MachineKeyence m in tempList)
            {
                HostlinkKeyence host = new HostlinkKeyence()
                {
                    IP = m.IpAddress,
                    port = m.Port,
                };
                ListMachine.Add(new KeyenceMachineModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                    ProductModel = m.ProductModel,

                    Actual = new PlcMemory() { MemoryType = m.Actual.TypeSymbol, PLCAddress = m.Actual.MemoryAddress, DataFormat = m.Actual.FormatSymbol, PLCLink = host },
                    Target = new PlcMemory() { MemoryType = m.Target.TypeSymbol, PLCAddress = m.Target.MemoryAddress, DataFormat = m.Target.FormatSymbol, PLCLink = host },
                    StopTime = new PlcMemory() { MemoryType = m.StopTime.TypeSymbol, PLCAddress = m.StopTime.MemoryAddress, DataFormat = m.StopTime.FormatSymbol, PLCLink = host },
                    RunningTime = new PlcMemory() { MemoryType = m.RunningTime.TypeSymbol, PLCAddress = m.RunningTime.MemoryAddress, DataFormat = m.RunningTime.FormatSymbol, PLCLink = host },
                    Chokotei = new PlcMemory() { MemoryType = m.Chokotei.TypeSymbol, PLCAddress = m.Chokotei.MemoryAddress, DataFormat = m.Chokotei.FormatSymbol, PLCLink = host },
                    AvailabilityRate = new PlcMemory() { MemoryType = m.AvailabilityRate.TypeSymbol, PLCAddress = m.AvailabilityRate.MemoryAddress, DataFormat = m.AvailabilityRate.FormatSymbol, PLCLink = host },
                    Performance = new PlcMemory() { MemoryType = m.Performance.TypeSymbol, PLCAddress = m.Performance.MemoryAddress, DataFormat = m.Performance.FormatSymbol, PLCLink = host },
                });
            }
        }

        public ObservableCollection<KeyenceMachineModel> ListMachine
        {
            get;
            set;
        }
        #region "Command"
        public ICommand AddCommand { get; set; }
        public ICommand OpenSettingCommand { get; set; }

        private void InitCommand()
        {
            //AddCommand = new RelayCommand<UIElementCollection>(
            //    (s) => true,
            //    (s) => { OpenSetting(s); });

            OpenSettingCommand = new RelayCommand(() => { OpenSetting_Execute(); });
        }

        private void OpenSetting_Execute()
        {
            Process.Start("SettingMachine.exe");
        }
        #endregion
        #region "Feild"
        #endregion
        #region "Property"

        #endregion
        #region "Method"
        #endregion

    }
}
