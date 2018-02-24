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
            MemoryValue = "0";
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
            //result = result.Replace("\0", String.Empty);
            if (result == "") return;
            if (preValue != result)
            {
                if (string.Compare(result, "OK") == 0)
                {
                    CalcCommand();
                }
                else
                {
                    result = result.Replace("\0", String.Empty);
                    string txt = "";
                    switch (DataFormat)
                    {
                        case ".U":
                            //txt = System.Convert.ToString(Convert.ToUInt16(result));
                            UInt16 Uoutput;
                            UInt16.TryParse(result, out Uoutput);
                            txt = Uoutput.ToString();
                            break;
                        case ".S":
                            //txt = System.Convert.ToString(Convert.ToInt16(result));
                            UInt16 Soutput;
                            UInt16.TryParse(result, out Soutput);
                            txt = Soutput.ToString();
                            break;
                        case ".D":
                            //txt = System.Convert.ToString(Convert.ToUInt32(result));
                            UInt32 Doutput;
                            UInt32.TryParse(result, out Doutput);
                            txt = Doutput.ToString();
                            break;
                        case ".L":
                            //txt = System.Convert.ToString(Convert.ToInt32(result));
                            UInt32 Loutput;
                            UInt32.TryParse(result, out Loutput);
                            txt = Loutput.ToString();
                            break;
                        case ".H":
                            txt = result;
                            break;
                        default:
                            //txt = System.Convert.ToString(Convert.ToUInt16(result));
                            UInt16 Defaultoutput;
                            UInt16.TryParse(result, out Defaultoutput);
                            txt = Defaultoutput.ToString();
                            break;
                    }
                    MemoryValue = result.Replace("\0", String.Empty);
                    preValue = MemoryValue;
                }
            }
        }
    }
}
