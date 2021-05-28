using System;

using SEComEnabler.SEComStructure;

namespace VirtualMES.Common
{
    /// <summary>
    /// This class contains COptions object and CStressTestObject
    /// </summary>

    public enum Cal_SystemBytes_State
    {
        INCREASE,
        REDUCE,
        STAY
    }

    public class CCommunicationInfo
    {
        public const long MAX_SYSTEMBYTES = 4294967295L;

        private SXProFile m_SXProFile;
        private COptions m_Options;
        private CStressTest m_StressTest;

        private bool m_bIsSECSConnected;
        private long m_lSendSystemBytes;
        private long m_lRcvedSystemBytes;


        public CCommunicationInfo()
        {
            this.m_SXProFile = null;
            this.m_Options = new COptions();
            this.m_StressTest = new CStressTest();

            this.m_bIsSECSConnected = false;
            m_lSendSystemBytes = 1;
            m_lRcvedSystemBytes = 1;
        }

        public void Initialize(SXProFile aProfile)
        {
            this.m_SXProFile = aProfile;
            this.m_bIsSECSConnected = false;
            ResetSystemBytes();
        }


        public void ResetSystemBytes()
        {
            this.m_lSendSystemBytes = this.m_Options.SystemBytes - 1;
            this.m_lRcvedSystemBytes = 1;
        }

        public bool IsSECSConnected
        {
            get
            {
                return this.m_bIsSECSConnected;
            }
            set
            {
                if (value == false && StressTest.IsTesting == true)
                {
                    StressTest.IsTesting = false;
                }
                this.m_bIsSECSConnected = value;
            }
        }

        public SXProFile ProFile
        {
            get
            {
                return this.m_SXProFile;
            }
        }

        public string GetSEComID()
        {
            try
            {
                return this.m_SXProFile.Read(SXProFile.ProFileKey.KEY_COMMON_EQUIPMENTID);
            }
            catch
            {
                return "";
            }
        }

        public SX.SECSDirection GetDirection()
        {
            try
            {
                string strValue = m_SXProFile.Read(SXProFile.ProFileKey.KEY_COMMON_IDENTITY);
                if (strValue.ToUpper() == SX.SECSInfo.HOST.ToString().ToUpper())
                {
                    return SX.SECSDirection.FromHost;
                }
                else
                {
                    return SX.SECSDirection.FromEquipment;
                }
            }
            catch
            {
                return SX.SECSDirection.Both;
            }
        }

        public COptions Options
        {
            get
            {
                return this.m_Options;
            }
            set
            {
                this.m_Options = value;
            }
        }

        public CStressTest StressTest
        {
            get
            {
                return this.m_StressTest;
            }
            set
            {
                this.m_StressTest = value;
            }
        }

        public long ReceivedSystemByte
        {
            get
            {
                return this.m_lRcvedSystemBytes;
            }
            set
            {
                this.m_lRcvedSystemBytes = value;
            }
        }

        public long calcSystemBytes(Cal_SystemBytes_State aState)
        {
            switch (aState)
            {
                case Cal_SystemBytes_State.INCREASE:
                    this.m_lSendSystemBytes++;
                    if (this.m_lSendSystemBytes >= MAX_SYSTEMBYTES)
                        this.m_lSendSystemBytes = 1;
                    break;
                case Cal_SystemBytes_State.REDUCE:
                    this.m_lSendSystemBytes--;
                    if (this.m_lSendSystemBytes <= 0)
                        this.m_lSendSystemBytes = 1;
                    break;
                default:
                    break;
            }

            return this.m_lSendSystemBytes;
        }

    }
}
