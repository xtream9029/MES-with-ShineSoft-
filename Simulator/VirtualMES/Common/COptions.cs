using System;
using System.Collections;
//using System.IO;
using System.Xml;

namespace VirtualMES.Common
{
    /// <summary>
    /// Handles the Properties, methods of Options
    /// </summary>
    public class COptions
    {
        private bool m_blSendAbortMsg;
        private long m_lSystemBytes;
        private long m_lSECS1LogLines;
        private long m_lSECS2LogLines;
        private bool m_blShowSECS2DataItems;
        private bool m_blUseSECSDispatcher;
        private bool m_blLogMode;
        private bool m_blSX2LogFilter;
        private bool m_blFilterRecvMsgOnly;
        private ArrayList m_arlSX2FilterMsgs;

        private string m_strDispatcherSMDFilePath;

        public COptions()
        {
            Initialize();
        }

        private void Initialize()
        {
            this.m_blSendAbortMsg = true;
            this.m_lSystemBytes = 1;
            this.m_lSECS1LogLines = this.m_lSECS2LogLines = 200;
            this.m_blShowSECS2DataItems = true;
            this.m_blUseSECSDispatcher = false;
            this.m_blLogMode = false;
            this.m_blSX2LogFilter = false;
            m_blFilterRecvMsgOnly = true;
            this.m_arlSX2FilterMsgs = new ArrayList();
            this.m_strDispatcherSMDFilePath = "";
        }

        public void Terminate()
        {
        }


        public bool SendAbortMsg
        {
            get
            {
                return this.m_blSendAbortMsg;
            }
            set
            {
                this.m_blSendAbortMsg = value;
            }
        }

        public long SystemBytes
        {
            get
            {
                return this.m_lSystemBytes;
            }
            set
            {
                this.m_lSystemBytes = value;
            }
        }

        public long SECS1LogLines
        {
            get
            {
                return this.m_lSECS1LogLines;
            }
            set
            {
                this.m_lSECS1LogLines = value;
            }
        }

        public long SECS2LogLines
        {
            get
            {
                return this.m_lSECS2LogLines;
            }
            set
            {
                this.m_lSECS2LogLines = value;
            }
        }

        public bool ShowSECS2DataItems
        {
            get
            {
                return this.m_blShowSECS2DataItems;
            }
            set
            {
                this.m_blShowSECS2DataItems = value;
            }
        }

        public bool UseSECSDispatcher
        {
            get
            {
                return this.m_blUseSECSDispatcher;
            }
            set
            {
                this.m_blUseSECSDispatcher = value;
            }
        }

        public bool LogModeOption
        {
            get
            {
                return this.m_blLogMode;
            }
            set
            {
                this.m_blLogMode = value;
            }
        }

        public bool SX2LogFilter
        {
            get
            {
                return this.m_blSX2LogFilter;
            }
            set
            {
                this.m_blSX2LogFilter = value;
            }
        }

        public bool FilterRecvMsgsOnly
        {
            get
            {
                return this.m_blFilterRecvMsgOnly;
            }
            set
            {
                this.m_blFilterRecvMsgOnly = value;
            }
        }

        public ArrayList SX2FilterMessages
        {
            get
            {
                return this.m_arlSX2FilterMsgs;
            }
            set
            {
                this.m_arlSX2FilterMsgs = value;
            }
        }

        public string DispatcherSMDFilePath
        {
            get
            {
                return this.m_strDispatcherSMDFilePath;
            }
            set
            {
                this.m_strDispatcherSMDFilePath = value;
            }
        }

    } // end of class COptions

    /// <summary>
    /// Handles properties and methods related to Stress Test
    /// </summary>
    public class CStressTest
    {
        private int m_iInterval;
        private bool m_blIsTesting;
        private XmlDocument m_SendPrimaryXml;

        public CStressTest()
        {
            Initialize();
        }

        private void Initialize()
        {
            this.m_iInterval = 1000;
            this.m_blIsTesting = false;
            m_SendPrimaryXml = null;
        }

        public int Interval
        {
            get
            {
                return this.m_iInterval;
            }
            set
            {
                this.m_iInterval = value;
            }
        }

        public bool IsTesting
        {
            get
            {
                return this.m_blIsTesting;
            }
            set
            {
                this.m_blIsTesting = value;
            }
        }

        public XmlDocument SendPrimaryXml
        {
            get
            {
                return this.m_SendPrimaryXml;
            }
            set
            {
                this.m_SendPrimaryXml = value;
            }
        }

    } // end of class CStressTest

    /// <summary>
    /// This class stores the message color options of SECS1 Log and SECS2 Log
    /// </summary>
    public class CLogColors
    {
        private System.Drawing.Color m_SECS1Send;
        private System.Drawing.Color m_SECS1Recv;
        private System.Drawing.Color m_SECS1Conn;
        private System.Drawing.Color m_SECS1Info;
        private System.Drawing.Color m_SECS1Other;
        private System.Drawing.Color m_SECS1Control;

        private System.Drawing.Color m_SECS2Send;
        private System.Drawing.Color m_SECS2Recv;
        private System.Drawing.Color m_SECS2Conn;
        private System.Drawing.Color m_SECS2Warn;
        private System.Drawing.Color m_SECS2Info;
        private System.Drawing.Color m_SECS2Other;

        public CLogColors()
        {//로그 메시지를 ui에 띄울때 색깔을 넣는 부분
            m_SECS1Send = System.Drawing.Color.Blue;
            m_SECS1Recv = System.Drawing.Color.Purple;
            m_SECS1Conn = System.Drawing.Color.Green;
            m_SECS1Info = System.Drawing.Color.Green;
            m_SECS1Control = System.Drawing.Color.Chocolate;
            m_SECS1Other = System.Drawing.Color.Brown;

            m_SECS2Send = System.Drawing.Color.Blue;
            m_SECS2Recv = System.Drawing.Color.Purple;
            m_SECS2Conn = System.Drawing.Color.Green;
            m_SECS2Warn = System.Drawing.Color.OrangeRed;
            m_SECS2Info = System.Drawing.Color.DarkSlateBlue;
            m_SECS2Other = System.Drawing.Color.Brown;

        }

        public System.Drawing.Color SECS1Send
        {
            get
            {
                return this.m_SECS1Send;
            }
            set
            {
                this.m_SECS1Send = value;
            }
        }

        public System.Drawing.Color SECS1Recv
        {
            get
            {
                return this.m_SECS1Recv;
            }
            set
            {
                this.m_SECS1Recv = value;
            }
        }

        public System.Drawing.Color SECS1Conn
        {
            get
            {
                return this.m_SECS1Conn;
            }
            set
            {
                this.m_SECS1Conn = value;
            }
        }

        public System.Drawing.Color SECS1Info
        {
            get
            {
                return this.m_SECS1Info;
            }
            set
            {
                this.m_SECS1Info = value;
            }
        }

        public System.Drawing.Color SECS1Control
        {
            get
            {
                return this.m_SECS1Control;
            }
            set
            {
                this.m_SECS1Control = value;
            }
        }

        public System.Drawing.Color SECS2Send
        {
            get
            {
                return this.m_SECS2Send;
            }
            set
            {
                this.m_SECS2Send = value;
            }
        }

        public System.Drawing.Color SECS2Recv
        {
            get
            {
                return this.m_SECS2Recv;
            }
            set
            {
                this.m_SECS2Recv = value;
            }
        }

        public System.Drawing.Color SECS2Conn
        {
            get
            {
                return this.m_SECS2Conn;
            }
            set
            {
                this.m_SECS2Conn = value;
            }
        }

        public System.Drawing.Color SECS2Warn
        {
            get
            {
                return this.m_SECS2Warn;
            }
            set
            {
                this.m_SECS2Warn = value;
            }
        }
        public System.Drawing.Color SECS1Other
        {
            get
            {
                return this.m_SECS1Other;
            }
            set
            {
                this.m_SECS1Other = value;
            }
        }
        public System.Drawing.Color SECS2Info
        {
            get
            {
                return this.m_SECS2Info;
            }
            set
            {
                this.m_SECS2Info = value;
            }
        }

        public System.Drawing.Color SECS2Other
        {
            get
            {
                return this.m_SECS2Other;
            }
            set
            {
                this.m_SECS2Other = value;
            }
        }

    } // end of class LogColros


    public class CMessageStats
    {
        private int m_nPriSent;
        private int m_nPriRecv;
        private int m_nSecSent;
        private int m_nSecRecv;
        private int m_nCtrlMsgSent;
        private int m_AutoReply;
        private int m_AbortMsg;
        private int m_Error;

        public CMessageStats()
        {
            m_nPriSent = 0;
            m_nPriRecv = 0;
            m_nSecSent = 0;
            m_nSecRecv = 0;
            m_nCtrlMsgSent = 0;
            m_AutoReply = 0;
            m_AbortMsg = 0;
            m_Error = 0;
        }

        public void Reset()
        {
            m_nPriSent = 0;
            m_nPriRecv = 0;
            m_nSecSent = 0;
            m_nSecRecv = 0;
            m_nCtrlMsgSent = 0;
            m_AutoReply = 0;
            m_AbortMsg = 0;
            m_Error = 0;
        }

        public int PrimaryMsgSent
        {
            get
            {
                return this.m_nPriSent;
            }
            set
            {
                this.m_nPriSent = value;
            }
        }

        public int PrimaryMsgReceived
        {
            get
            {
                return this.m_nPriRecv;
            }
            set
            {
                this.m_nPriRecv = value;
            }
        }

        public int SecondaryMsgSent
        {
            get
            {
                return this.m_nSecSent;
            }
            set
            {
                this.m_nSecSent = value;
            }
        }

        public int SecondaryMsgReceived
        {
            get
            {
                return this.m_nSecRecv;
            }
            set
            {
                this.m_nSecRecv = value;
            }
        }

        public int AutoReplySent
        {
            get
            {
                return this.m_AutoReply;
            }
            set
            {
                this.m_AutoReply = value;
            }
        }

        public int ControlMsgSent
        {
            get
            {
                return this.m_nCtrlMsgSent;
            }
            set
            {
                this.m_nCtrlMsgSent = value;
            }
        }

        public int AbortMsgSent
        {
            get
            {
                return this.m_AbortMsg;
            }
            set
            {
                this.m_AbortMsg = value;
            }
        }

        public int TransmissionError
        {
            get
            {
                return this.m_Error;
            }
            set
            {
                this.m_Error = value;
            }
        }

    } // end of class MessageStats



} // end of namespace SEComEnabler.SEComSimulator.Common

