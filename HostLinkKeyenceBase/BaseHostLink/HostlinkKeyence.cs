﻿using MVVMBaseLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace HostLinkKeyenceBase
{
    public class HostlinkKeyence   :ViewModelBase
    {
        public HostlinkKeyence()
        {
            time = 100;
        }
        public List<general.PLCLink> ConnList;
        private Timer periodTimer;
        private TcpClient connection;

        #region Properties
        private string _IP = "192.168.0.10";
        private int _port = 8501;
        [DefaultValue("192.168.0.10")]
        public string IP
        {
            get
            {
                return _IP;
            }
            set
            {
                _IP = value;
            }
        }
        [DefaultValue(8501)]
        public int port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
            }
        }
        private double _time = 100;
        [DefaultValue(100)]
        public double time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
                if (ReferenceEquals(periodTimer, null))
                {
                    periodTimer = new Timer();
                    periodTimer.Elapsed += SyncCircle;
                }
                periodTimer.Interval = value;
                periodTimer.Start();
            }
        }
        private string m_Name;
        [BrowsableAttribute(false)]
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        private bool isPlcConnected;

        public bool IsPlcConnect
        {
            get { return isPlcConnected; }
            set
            {
                isPlcConnected = value;
                RaisePropertyChanged("IsPlcConnected");
            }
        }

        #endregion

        public void SendRequest(ref general.PLCLink link)
        {
            if (link.enable == false)
            {
                return;
            }
            string returnStr = "";
            if (ReferenceEquals(connection, null))
            {
                Debug.Print("Creat connection");
                connection = new TcpClient();
            }
            if (!connection.Connected)
            {
                try
                {
                    //connection.ConnectAsync(_IP, _port)
                    connection.Connect(_IP, _port);
                    Debug.Print("Start connecting ...");
                    int timeout = Environment.TickCount;
                    while (((timeout + 5000) > Environment.TickCount) && (connection.Connected == false))
                    {
                    }
                    if (connection.Connected == true)
                    {
                        Debug.Print("Connected!");
                    }
                }
                catch (Exception)
                {

                }
            }

            if (connection.Connected)
            {
                returnStr = ReadCommand(link);
                link.usingmethod.useResult(returnStr);
            }
            else
            {

            }
        }
        public string ReadCommand(general.PLCLink link)
        {
            string str = link.cmd;
            byte[] dataByte = System.Text.Encoding.ASCII.GetBytes(str);
            byte[] dataByte2 = new byte[1] { 0xD };
            NetworkStream stream = connection.GetStream();
            Debug.Print("Start sending command ...");
            stream.Write(dataByte, 0, dataByte.Length);
            stream.Write(dataByte2, 0, dataByte2.Length);
            Debug.Print("Command was send!");
            int timeout = Environment.TickCount;
            while (connection.Available == 0 && (timeout + 5000) > Environment.TickCount)

            {

            }
            if (connection.Available != 0)
            {
                Debug.Print("Reading data ...");
                byte[] receiveByte = new byte[connection.ReceiveBufferSize + 1];
                stream.Read(receiveByte, 0, receiveByte.Length);
                Debug.Print("Data was read!");
                string retStr = System.Text.Encoding.ASCII.GetString(receiveByte, 0, receiveByte.Length);
                return retStr;
            }
            Debug.Print("Time out!");
            return "";
        }

        public void AddLink(general.PLCLink link)
        {
            if (ReferenceEquals(ConnList, null))
            {
                ConnList = new List<general.PLCLink>();
            }
            ConnList.Add(link);
        }

        public void SyncCircle(object source, ElapsedEventArgs e)
        {
            if(connection!=null)
            {
                IsPlcConnect = connection.Connected;
            }
            periodTimer.Enabled = false;
            if (ReferenceEquals(ConnList, null))
            {
                periodTimer.Start();
                return;
            }
            if (ConnList.Count == 0)
            {
                periodTimer.Start();
                return;
            }
            for (int i = 0; i <= ConnList.Count - 1; i++)
            {
                general.PLCLink temp_ConnListi = ConnList[i];
                SendRequest(ref temp_ConnListi);
                ConnList[i] = temp_ConnListi;
            }
            periodTimer.Start();
        }

        public override string ToString()
        {
            return "";
        }
    

    }
}
