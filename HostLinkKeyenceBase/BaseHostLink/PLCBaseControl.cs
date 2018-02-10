using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostLinkKeyenceBase
{
    public class PLCBaseControl:INotifyPropertyChanged
    {
        public string preValue = "";
        public general.PLCLink link;
        #region Properties
        private int _PLCAdress = 100;
        [Category("PLC connection"), DefaultValue(100)]
        public int PLCAddress
        {
            get
            {
                return _PLCAdress;
            }
            set
            {
                _PLCAdress = value;
                if (ReferenceEquals(link, null))
                {
                    link = new general.PLCLink();
                }
                link.add.MemAdd = _PLCAdress;
                CalcCommand();
            }
        }
        private HostlinkKeyence _PLCLink;
        [Category("PLC connection")]
        public HostlinkKeyence PLCLink
        {
            get
            {
                return _PLCLink;
            }
            set
            {
                //If IsNothing(link) Then Return
                _PLCLink = value;
                if (ReferenceEquals(value, null))
                {
                    return;
                }
                if (ReferenceEquals(link, null))
                {
                    link = new general.PLCLink();
                }
                link.usingmethod = (general.IPLCControl)this;
                link.enable = true;
                CalcCommand();
                _PLCLink.AddLink(link);
            }
        }
        private string _dataFormat = ".U";
        [Category("PLC connection"), DefaultValue(".U")]
        public string DataFormat
        {
            get
            {
                return _dataFormat;
            }
            set
            {
                _dataFormat = value;
                if (ReferenceEquals(link, null))
                {
                    link = new general.PLCLink();
                }
                link.add.format = value;
                CalcCommand();
            }
        }
        private string _MemType = "DM";


        [Category("PLC connection"), DefaultValue("DM")]
        public string MemoryType
        {
            get
            {
                return _MemType;
            }
            set
            {
                _MemType = value;
                if (ReferenceEquals(link, null))
                {
                    link = new general.PLCLink();
                }
                link.add.MemType = _MemType;
                CalcCommand();
            }
        }
        #endregion
        public virtual void CalcCommand(bool Write = false, string value = "")
        {
            if (ReferenceEquals(link, null))
            {
                link = new general.PLCLink();
            }
            link.add.MemType = MemoryType;
            link.add.MemAdd = PLCAddress;
            link.add.format = DataFormat;
            if (Write)
            {
                link.cmd = "WR " + link.add.MemType + System.Convert.ToString(link.add.MemAdd) + link.add.format + " " + value;
            }
            else
            {
                link.cmd = "RD " + link.add.MemType + System.Convert.ToString(link.add.MemAdd) + link.add.format;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event Action<string> EventUpdateValue;
        public void UpdatePlcValue(string value)
        {
            if (EventUpdateValue != null)
            {
                EventUpdateValue(value);
            }
        }
    }
}
