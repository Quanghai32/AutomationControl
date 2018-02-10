using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostLinkKeyenceBase
{
    public class PLCRunningStatus : PLCBaseControl, general.IPLCControl
    {
        private bool _isRunning;
        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                _isRunning = value;
                if (_isRunning)
                {
                    CalcCommand(true, "1");
                }
                else
                {
                    CalcCommand(true, "0");
                }
            }
        }

        public void useResult(string result)
        {
            result = result.Replace("\r\n", "");
            if (preValue != result)
            {
                if (string.Compare(result, "OK") == 0)
                {
                    CalcCommand();
                }
                else
                {
                    bool txt;
                    txt = Convert.ToBoolean(Convert.ToInt16(result));
                    if (txt == true)
                    {
                        _isRunning = true;
                    }
                    else
                    {
                        _isRunning = false;
                    }
                    preValue = result;
                }
                RaisePropertyChanged("IsRunning");
                UpdatePlcValue(IsRunning.ToString());
            }
        }

        public override void CalcCommand(bool Write = false, string value = "")
        {
            if (ReferenceEquals(link, null))
            {
                link = new general.PLCLink();
            }
            if (Write)
            {
                link.cmd = "M" + value;
            }
            else
            {
                link.cmd = "?M";
            }
        }
    }
}
