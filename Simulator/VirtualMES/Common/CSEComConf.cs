using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Windows.Forms;

//using SEComEnabler;
using SEComEnabler.SEComUtility;
using SEComEnabler.SEComStructure;

namespace VirtualMES.Common
{
    /// <summary>
    /// Summary description for CSEComConf.
    /// </summary>
    public class CSEComConf
    {

        private string m_strCurSEComID;
        private string m_strConfigFilePath;
        private bool m_bFlagOpenConnection;

        public CSEComConf()
        {
            this.m_strCurSEComID = "";
            Initialize();
        }


        public CSEComConf(string aSEComID)
        {
            this.m_strCurSEComID = aSEComID;
            Initialize();
        }


        private void Initialize()
        {
            try
            {
                this.m_strConfigFilePath = Application.StartupPath + System.IO.Path.DirectorySeparatorChar + CConfigConst.SEComSimulatorConfig;
                this.m_bFlagOpenConnection = false;
                if (!System.IO.File.Exists(this.m_strConfigFilePath))
                {
                    CreateConfigFile(this.m_strConfigFilePath);
                }
            }
            catch
            {
                MessageBox.Show("Error in Initialization!!!", "SEComSimulator", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateConfigFile(string theFilePath)
        {
            try
            {
                XmlTextWriter xtw = new XmlTextWriter(theFilePath, System.Text.Encoding.UTF8);
                xtw.Formatting = Formatting.Indented;

                xtw.WriteStartDocument();
                xtw.WriteStartElement(CConfigConst.NODE_Top);

                xtw.WriteStartElement(CConfigConst.NODE_LastSelect);
                xtw.WriteElementString(CConfigConst.ELEMENT_LastSEComID, "");
                xtw.WriteEndElement();

                xtw.WriteStartElement(CConfigConst.NODE_SEComID);
                xtw.WriteEndElement();

                xtw.WriteEndDocument();
                xtw.Flush();
                xtw.Close();

            }
            catch { }

        }


        public string SEComID
        {
            get
            {
                if (this.m_strCurSEComID == null)
                    this.m_strCurSEComID = "";
                return this.m_strCurSEComID;
            }
            set
            {
                this.m_strCurSEComID = value;
            }
        }


        public string IniFilePath
        {
            get
            {
                return this.m_strConfigFilePath;
            }
        }


        public bool FlagOpenConnection
        {
            get
            {
                return this.m_bFlagOpenConnection;
            }
            set
            {
                this.m_bFlagOpenConnection = value;
            }
        }


        public ArrayList GetSectionList()
        {
            try
            {
                SEComError.SEComPlugIn aError = SEComError.SEComPlugIn.ERR_NONE;
                SEComEnabler.SEComUtility.XMLHandler xmlHandle = new XMLHandler();

                if (!xmlHandle.Initialize(this.m_strConfigFilePath, ref aError))
                {
                    return null;
                }
                System.Collections.ArrayList arlSectionList = xmlHandle.GetSectionList();
                return arlSectionList;
            }
            catch
            {
                return null;
            }
        }

        public string GetLastSEComID()

        {
            try
            {
                XmlDocument xmlConfig = new XmlDocument();
                xmlConfig.Load(this.m_strConfigFilePath);
                XmlNode xmlLastSEComID = xmlConfig.GetElementsByTagName(CConfigConst.ELEMENT_LastSEComID)[0];
                return xmlLastSEComID.InnerText;
            }
            catch
            {
                return "";
            }
        }

        public SXProFile ReadSectionData(string aSEComID)
        {
            try
            {
                SXProFile theInfo = new SXProFile();

                SEComError.SEComPlugIn aError = SEComError.SEComPlugIn.ERR_NONE;
                SEComEnabler.SEComUtility.XMLHandler xmlHandle = new XMLHandler();

                if (!xmlHandle.Initialize(this.m_strConfigFilePath, ref aError))
                {
                    System.Windows.Forms.MessageBox.Show("xmlHandle.Initialize Fail");
                    return null;
                }
                //				theInfo = xmlHandle.ReadSectionData(aSEComID.ToUpper(), ref aError);
                //				return theInfo;

                aError = xmlHandle.ReadSectionData(aSEComID.ToUpper(), ref theInfo);

                if (aError == SEComError.SEComPlugIn.ERR_NONE) return theInfo;
                return null;
            }
            catch
            {
                return null;
            }
        }

        public string ReadValue(string aSection, SXProFile.ProFileKey aEntry, string aDefaultValue)
        {
            try
            {
                SEComError.SEComPlugIn aError = SEComError.SEComPlugIn.ERR_NONE;
                SEComEnabler.SEComUtility.XMLHandler xmlHandle = new XMLHandler();

                if (!xmlHandle.Initialize(this.m_strConfigFilePath, ref aError))
                {
                    return null;
                }
                return xmlHandle.ReadValue(aSection.ToUpper(), aEntry, aDefaultValue);
            }
            catch
            {
                return null;
            }
        }

        public void RemoveSection(int theSection)
        {
            try
            {
                XmlDocument xmlConfig = new XmlDocument();
                xmlConfig.Load(this.m_strConfigFilePath);
                XmlNode SEComIDNode = xmlConfig.GetElementsByTagName(CConfigConst.NODE_SEComID)[0];

                SEComIDNode.RemoveChild(SEComIDNode.ChildNodes[theSection]);
                xmlConfig.Save(this.m_strConfigFilePath);
                xmlConfig = null;
            }
            catch { }
        }


        public bool AddSection(string theSection)
        {
            try
            {
                if (!System.IO.File.Exists(this.m_strConfigFilePath))
                    CreateConfigFile(this.m_strConfigFilePath);

                XmlDocument xmlConfig = new XmlDocument();
                xmlConfig.Load(this.m_strConfigFilePath);
                XmlNode xmlSEComID = xmlConfig.GetElementsByTagName(CConfigConst.NODE_SEComID)[0];

                string strEntry = "";
                XmlNode xmlSection, xmlCategory, xmlSubCat, xmlEntry;
                XmlAttribute xmlAttrib;

                // Create the Section
                xmlSection = xmlConfig.CreateNode(XmlNodeType.Element, theSection, "");
                xmlSEComID.AppendChild(xmlSection);

                // 01. DRIVERINFO
                xmlCategory = xmlConfig.CreateNode(XmlNodeType.Element, CConfigConst.SECTION_DrvInfo, "");
                xmlSection.AppendChild(xmlCategory);

                strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(SXProFile.ProFileKey.KEY_COMMON_DEVICEID);
                xmlEntry = xmlConfig.CreateElement(strEntry);
                xmlCategory.AppendChild(xmlEntry);  // DEVICEID

                strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(SXProFile.ProFileKey.KEY_COMMON_IDENTITY);
                xmlEntry = xmlConfig.CreateElement(strEntry);
                xmlCategory.AppendChild(xmlEntry);  // IDENTITY


                // 02. LOGINFO
                xmlCategory = xmlConfig.CreateNode(XmlNodeType.Element, CConfigConst.SECTION_LogInfo, "");
                xmlSection.AppendChild(xmlCategory);

                strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(SXProFile.ProFileKey.KEY_LOG_DIR);
                xmlEntry = xmlConfig.CreateElement(strEntry);
                xmlCategory.AppendChild(xmlEntry);  // DIR

                strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(SXProFile.ProFileKey.KEY_LOG_SECSIMODE);
                xmlEntry = xmlConfig.CreateElement(strEntry);
                xmlCategory.AppendChild(xmlEntry);  // SECS1MODE

                strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(SXProFile.ProFileKey.KEY_LOG_SECSIIMODE);
                xmlEntry = xmlConfig.CreateElement(strEntry);
                xmlCategory.AppendChild(xmlEntry);  // SECSIIMODE

                strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(SXProFile.ProFileKey.KEY_LOG_XMLMODE);
                xmlEntry = xmlConfig.CreateElement(strEntry);
                xmlCategory.AppendChild(xmlEntry);  // XMLMODE

                strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(SXProFile.ProFileKey.KEY_LOG_DRIVERMODE);
                xmlEntry = xmlConfig.CreateElement(strEntry);
                xmlCategory.AppendChild(xmlEntry);  // DRIVERMODE

                strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(SXProFile.ProFileKey.KEY_LOG_DRIVERLEVEL);
                xmlEntry = xmlConfig.CreateElement(strEntry);
                xmlCategory.AppendChild(xmlEntry);  // DRIVERLEVEL

                strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(SXProFile.ProFileKey.KEY_LOG_BACKUP);
                xmlEntry = xmlConfig.CreateElement(strEntry);
                xmlCategory.AppendChild(xmlEntry);  // BACKUP


                // 03. SECSMODE
                xmlCategory = xmlConfig.CreateNode(XmlNodeType.Element, CConfigConst.SECTION_SECSMode, "");
                xmlSection.AppendChild(xmlCategory);

                xmlAttrib = xmlConfig.CreateAttribute(CConfigConst.ATTRIBUTE_Mode);
                xmlCategory.Attributes.Append(xmlAttrib);   // MODE

                xmlSubCat = xmlConfig.CreateNode(XmlNodeType.Element, CConfigConst.SECTION_HSMS, "");
                xmlCategory.AppendChild(xmlSubCat); // HSMS

                strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(SXProFile.ProFileKey.KEY_HSMS_HSMSMODE);
                xmlEntry = xmlConfig.CreateElement(strEntry);
                xmlSubCat.AppendChild(xmlEntry);    // HSMSMODE

                strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(SXProFile.ProFileKey.KEY_HSMS_REMOTEIP);
                xmlEntry = xmlConfig.CreateElement(strEntry);
                xmlSubCat.AppendChild(xmlEntry);    // REMOTEIP

                strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(SXProFile.ProFileKey.KEY_HSMS_REMOTEPORT);
                xmlEntry = xmlConfig.CreateElement(strEntry);
                xmlSubCat.AppendChild(xmlEntry);    // REMOTEPORT

                strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(SXProFile.ProFileKey.KEY_HSMS_LOCALPORT);
                xmlEntry = xmlConfig.CreateElement(strEntry);
                xmlSubCat.AppendChild(xmlEntry);    // LOCALPORT


                xmlSubCat = xmlConfig.CreateNode(XmlNodeType.Element, CConfigConst.SECTION_SECS1, "");
                xmlCategory.AppendChild(xmlSubCat); //SECS1

                strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(SXProFile.ProFileKey.KEY_SECS1_MASTER);
                xmlEntry = xmlConfig.CreateElement(strEntry);
                xmlSubCat.AppendChild(xmlEntry);    // MASTER

                strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(SXProFile.ProFileKey.KEY_SECS1_COMPORT);
                xmlEntry = xmlConfig.CreateElement(strEntry);
                xmlSubCat.AppendChild(xmlEntry);    // COMPORT

                xmlEntry = xmlConfig.CreateElement(CConfigConst.ELEMENT_AutoBaud);
                xmlSubCat.AppendChild(xmlEntry);    // AUTOBAUD

                strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(SXProFile.ProFileKey.KEY_SECS1_BAUDRATE);
                xmlEntry = xmlConfig.CreateElement(strEntry);
                xmlSubCat.AppendChild(xmlEntry);    // BAUDRATE

                strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(SXProFile.ProFileKey.KEY_SECS1_INTERLEAVE);
                xmlEntry = xmlConfig.CreateElement(strEntry);
                xmlSubCat.AppendChild(xmlEntry);    // INTERLEAVE

                strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(SXProFile.ProFileKey.KEY_SECS1_RETRYCOUNT);
                xmlEntry = xmlConfig.CreateElement(strEntry);
                xmlSubCat.AppendChild(xmlEntry);    // RETRYCOUNT


                // 04. TIMEOUT
                xmlCategory = xmlConfig.CreateNode(XmlNodeType.Element, CConfigConst.SECTION_TimeOut, "");
                xmlSection.AppendChild(xmlCategory);    //TIMEOUT

                strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(SXProFile.ProFileKey.KEY_TIME_T1);
                xmlEntry = xmlConfig.CreateElement(strEntry);
                xmlCategory.AppendChild(xmlEntry);  // T1

                strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(SXProFile.ProFileKey.KEY_TIME_T2);
                xmlEntry = xmlConfig.CreateElement(strEntry);
                xmlCategory.AppendChild(xmlEntry);  // T2

                strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(SXProFile.ProFileKey.KEY_TIME_T3);
                xmlEntry = xmlConfig.CreateElement(strEntry);
                xmlCategory.AppendChild(xmlEntry);  // T3

                strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(SXProFile.ProFileKey.KEY_TIME_T4);
                xmlEntry = xmlConfig.CreateElement(strEntry);
                xmlCategory.AppendChild(xmlEntry);  // T4

                strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(SXProFile.ProFileKey.KEY_TIME_T5);
                xmlEntry = xmlConfig.CreateElement(strEntry);
                xmlCategory.AppendChild(xmlEntry);  // T5

                strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(SXProFile.ProFileKey.KEY_TIME_T6);
                xmlEntry = xmlConfig.CreateElement(strEntry);
                xmlCategory.AppendChild(xmlEntry);  // T6

                strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(SXProFile.ProFileKey.KEY_TIME_T7);
                xmlEntry = xmlConfig.CreateElement(strEntry);
                xmlCategory.AppendChild(xmlEntry);  // T7

                strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(SXProFile.ProFileKey.KEY_TIME_T8);
                xmlEntry = xmlConfig.CreateElement(strEntry);
                xmlCategory.AppendChild(xmlEntry);  // T8


                // 05. EXT
                xmlCategory = xmlConfig.CreateNode(XmlNodeType.Element, CConfigConst.ELEMENT_Ext, "");
                xmlSection.AppendChild(xmlCategory);    // EXT

                strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(SXProFile.ProFileKey.KEY_TIME_EXT_LINKTEST);
                xmlEntry = xmlConfig.CreateElement(strEntry);
                xmlCategory.AppendChild(xmlEntry);  // LINKTEST


                // Save the changes
                xmlConfig.Save(this.m_strConfigFilePath);

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("[ADD SECTION] " + ex.GetBaseException().ToString());
                return false;
            }
        }




        public void WriteValue(string theSection, SXProFile.ProFileKey theEntry, string theValue)
        {
            try
            {
                XmlDocument xmlConfig = new XmlDocument();
                xmlConfig.Load(this.m_strConfigFilePath);

                XmlNode xmlSEComID = xmlConfig.GetElementsByTagName(CConfigConst.NODE_SEComID)[0];

                XmlNode xmlSection = xmlSEComID.SelectSingleNode("//" + theSection);
                XmlNode xmlEntry = null;

                if (theEntry == SXProFile.ProFileKey.KEY_COMMON_SECSMODE)
                {
                    xmlEntry = xmlSection.SelectSingleNode("//" + theSection + "//" + CConfigConst.SECTION_SECSMode);
                    xmlEntry.Attributes[CConfigConst.ATTRIBUTE_Mode].Value = theValue.Trim().ToUpper();
                }
                else if (theEntry == SXProFile.ProFileKey.KEY_SECS1_BAUDRATE)
                {
                    if (theValue.ToUpper() == "AUTO")
                    {
                        xmlEntry = xmlSection.SelectSingleNode("//" + theSection + "//" + CConfigConst.ELEMENT_AutoBaud);
                        xmlEntry.InnerText = true.ToString();

                        string strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(theEntry);
                        xmlEntry = xmlSection.SelectSingleNode("//" + theSection + "//" + strEntry);
                        xmlEntry.InnerText = "9600";
                    }
                    else
                    {
                        xmlEntry = xmlSection.SelectSingleNode("//" + theSection + "//" + CConfigConst.ELEMENT_AutoBaud);
                        xmlEntry.InnerText = false.ToString();

                        string strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(theEntry);
                        xmlEntry = xmlSection.SelectSingleNode("//" + theSection + "//" + strEntry);
                        xmlEntry.InnerText = theValue.Trim();
                    }
                }
                else
                {
                    string strEntry = SEComEnabler.SEComUtility.CUtilities.SXProFileToString(theEntry);
                    xmlEntry = xmlSection.SelectSingleNode("//" + theSection + "//" + strEntry);
                    if (xmlEntry != null)
                    {
                        xmlEntry.InnerText = theValue.Trim();
                    }
                    else
                    {
                        XmlNode newNode = xmlConfig.CreateNode(XmlNodeType.Element, strEntry, "");
                        newNode.InnerText = theValue.Trim();
                        //						xmlSection.AppendChild(newNode);

                        CreateNewEntry(xmlSection, newNode, theEntry);
                    }
                }

                xmlConfig.Save(this.m_strConfigFilePath);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.GetBaseException().ToString());
            }
        }


        public void WriteLastSEComID(string theLastSEComID)
        {
            try
            {
                XmlDocument xmlConfig = new XmlDocument();
                xmlConfig.Load(this.m_strConfigFilePath);

                XmlNode xmlLastSEComID = xmlConfig.GetElementsByTagName(CConfigConst.ELEMENT_LastSEComID)[0];
                xmlLastSEComID.InnerText = theLastSEComID.ToUpper().Trim();

                xmlConfig.Save(this.m_strConfigFilePath);
                xmlConfig = null;
            }
            catch { }
        }

        private void CreateNewEntry(XmlNode theSection, XmlNode newNode, SXProFile.ProFileKey theEntry)
        {
            try
            {
                XmlNode xmlCategory;
                switch (theEntry)
                {
                    case SXProFile.ProFileKey.KEY_COMMON_IDENTITY:
                    case SXProFile.ProFileKey.KEY_COMMON_DEVICEID:
                        xmlCategory = theSection.SelectSingleNode("//" + CConfigConst.SECTION_DrvInfo);
                        xmlCategory.AppendChild(newNode);
                        break;

                    case SXProFile.ProFileKey.KEY_LOG_DIR:
                    case SXProFile.ProFileKey.KEY_LOG_BACKUP:
                    case SXProFile.ProFileKey.KEY_LOG_DRIVERLEVEL:
                    case SXProFile.ProFileKey.KEY_LOG_DRIVERMODE:
                    case SXProFile.ProFileKey.KEY_LOG_SECSIIMODE:
                    case SXProFile.ProFileKey.KEY_LOG_SECSIMODE:
                    case SXProFile.ProFileKey.KEY_LOG_XMLMODE:
                        xmlCategory = theSection.SelectSingleNode("//" + CConfigConst.SECTION_LogInfo);
                        xmlCategory.AppendChild(newNode);
                        break;

                    case SXProFile.ProFileKey.KEY_HSMS_HSMSMODE:
                    case SXProFile.ProFileKey.KEY_HSMS_REMOTEIP:
                    case SXProFile.ProFileKey.KEY_HSMS_REMOTEPORT:
                    case SXProFile.ProFileKey.KEY_HSMS_LOCALPORT:
                        xmlCategory = theSection.SelectSingleNode("//" + CConfigConst.SECTION_HSMS);
                        xmlCategory.AppendChild(newNode);
                        break;

                    case SXProFile.ProFileKey.KEY_SECS1_MASTER:
                    case SXProFile.ProFileKey.KEY_SECS1_COMPORT:
                    case SXProFile.ProFileKey.KEY_SECS1_BAUDRATE:
                    case SXProFile.ProFileKey.KEY_SECS1_INTERLEAVE:
                    case SXProFile.ProFileKey.KEY_SECS1_RETRYCOUNT:
                        xmlCategory = theSection.SelectSingleNode("//" + CConfigConst.SECTION_SECS1);
                        xmlCategory.AppendChild(newNode);
                        break;

                    case SXProFile.ProFileKey.KEY_TIME_T1:
                    case SXProFile.ProFileKey.KEY_TIME_T2:
                    case SXProFile.ProFileKey.KEY_TIME_T3:
                    case SXProFile.ProFileKey.KEY_TIME_T4:
                    case SXProFile.ProFileKey.KEY_TIME_T5:
                    case SXProFile.ProFileKey.KEY_TIME_T6:
                    case SXProFile.ProFileKey.KEY_TIME_T7:
                    case SXProFile.ProFileKey.KEY_TIME_T8:
                        xmlCategory = theSection.SelectSingleNode("//" + CConfigConst.SECTION_TimeOut);
                        xmlCategory.AppendChild(newNode);
                        break;

                    case SXProFile.ProFileKey.KEY_TIME_EXT_LINKTEST:
                        xmlCategory = theSection.SelectSingleNode("//" + CConfigConst.ELEMENT_Ext);
                        xmlCategory.AppendChild(newNode);
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.GetBaseException().ToString());
            }
        }

        //********************************************************************************
        //  for BC Project -> Options -> Is Use SECSDispatcher : SMD Filepath
        //	<SECSDISPATCHER>
        //		<ISUSEDISPATCHER>TRUE</ISUSEDISPATCHER>
        //		<SMDFILEPATH>C:\My Files\l6itc.smd</SMDFILEPATH>
        //	</SECSDISPATCHER>
        //********************************************************************************

        public bool WriteDispatcherValue(string aSEComID, string aNode, string aValue)
        {
            try
            {
                XmlDocument xmlConfig = new XmlDocument();
                xmlConfig.Load(this.m_strConfigFilePath);

                XmlNode xmlDriverList = xmlConfig.GetElementsByTagName(CConfigConst.NODE_SEComID)[0];
                if (xmlDriverList == null) return false;
                XmlNode xmlSEComID = xmlDriverList.SelectSingleNode("//" + aSEComID);
                if (xmlSEComID == null) return false;
                XmlNode xmlSection = xmlSEComID.SelectSingleNode("//" + CConfigConst.SECTION_Dispatcher);
                if (xmlSection == null)
                {
                    xmlSection = xmlConfig.CreateElement(CConfigConst.SECTION_Dispatcher);
                    xmlSEComID.AppendChild(xmlSection);
                }

                XmlNode xmlEntry = xmlSection.SelectSingleNode("//" + aNode);
                if (xmlEntry != null) xmlEntry.InnerText = aValue;
                else
                {
                    xmlEntry = xmlConfig.CreateElement(aNode);
                    xmlEntry.InnerText = aValue;
                    xmlSection.AppendChild(xmlEntry);
                }

                xmlConfig.Save(this.m_strConfigFilePath);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.GetBaseException().ToString());
                return false;
            }
        } // end of WriteDispatcherValue()

        public string ReadDispatcherValue(string aSEComID, string aNode)
        {
            try
            {
                XmlDocument xmlConfig = new XmlDocument();
                xmlConfig.Load(this.m_strConfigFilePath);

                XmlNode xmlDriverList = xmlConfig.GetElementsByTagName(CConfigConst.NODE_SEComID)[0];
                if (xmlDriverList == null)
                {
                    if (aNode.Equals(CConfigConst.ELEMENT_IsUseDispatcher)) return false.ToString();
                    else return null;
                }
                XmlNode xmlSEComID = xmlDriverList.SelectSingleNode("//" + aSEComID);
                if (xmlSEComID == null)
                {
                    if (aNode.Equals(CConfigConst.ELEMENT_IsUseDispatcher)) return false.ToString();
                    else return null;
                }
                XmlNode xmlSection = xmlSEComID.SelectSingleNode("//" + CConfigConst.SECTION_Dispatcher);
                if (xmlSection == null)
                {
                    if (aNode.Equals(CConfigConst.ELEMENT_IsUseDispatcher)) return false.ToString();
                    else return null;
                }

                XmlNode xmlEntry = xmlSEComID.SelectSingleNode("//" + aNode);
                if (xmlEntry == null)
                {
                    if (aNode.Equals(CConfigConst.ELEMENT_IsUseDispatcher)) return false.ToString();
                    else return null;
                }

                return xmlEntry.InnerText;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.GetBaseException().ToString());
                return null;
            }
        } // end of ReadDispatcherValue()


        // ***************************************************************************

    } // end of class CSEComConf
}
