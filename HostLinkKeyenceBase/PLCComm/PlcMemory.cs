using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostLinkKeyenceBase
{
    public class PlcMemory : PLCBaseControl, general.IPLCControl
    {
        public PlcMemory()
        {
            IsReadOnly = false;
        }

        private bool _IsReadOnly;
        public bool IsReadOnly
        {
            get
            {
                return _IsReadOnly;
            }
            set
            {
                _IsReadOnly = value;
                RaisePropertyChanged("IsReadOnly");
            }
        }

        private string _MemoryValue;
        public string MemoryValue
        {
            get
            {
                return _MemoryValue;
            }
            set
            {
                _MemoryValue = value;
                RaisePropertyChanged("MemoryValue");
                UpdatePlcValue(_MemoryValue);
            }
        }

        public void useResult(string result)
        {
            if (result == "") return;
            if (preValue != result)
            {
                if (string.Compare(result, "OK") == 0)
                {
                    CalcCommand();
                }
                else
                {
                    string txt = "";
                    switch (DataFormat)
                    {
                        case ".U":
                            txt = System.Convert.ToString(Convert.ToUInt16(result));
                            break;
                        case ".S":
                            txt = System.Convert.ToString(Convert.ToInt16(result));
                            break;
                        case ".D":
                            txt = System.Convert.ToString(Convert.ToUInt32(result));
                            break;
                        case ".L":
                            txt = System.Convert.ToString(Convert.ToInt32(result));
                            break;
                        case ".H":
                            txt = result;
                            break;
                        default:
                            txt = System.Convert.ToString(Convert.ToUInt16(result));
                            break;
                    }
                    MemoryValue = result.Replace("\0", String.Empty);
                    preValue = MemoryValue;
                }
            }
        }
    }
}
