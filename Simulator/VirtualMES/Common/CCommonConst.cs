using System;

//using SEComEnabler;
using SEComEnabler.SEComStructure;
using SEComEnabler.SEComUtility;


namespace VirtualMES.Common
{
    /// <summary>
    /// CCommonConst에 대한 요약 설명입니다.
    /// </summary>

    public class CCommonConst
    {
        private CCommonConst()
        { }

        // these consts indicate the node level
        public const int SECSLibrary_Level = 0;
        public const int SECSPairMsg_Level = 1;
        public const int SECSMessage_Level = 2;
        public const int SECSDataItem_Level = 3;

        // This string represents the default text for the new node to be added
        public const string NewChildNodeText = "U2 1 New1 '0'";

        public const string DefaultLibraryFile = "DEFAULT.SMD";
        public const string UserDefaultLibraryFile = "USERLIB.SMD";
        public const string TempSMDFile = "SIMULATOR_DISPATCH.SMD";

        public const string MSG_ENDTAG = ".";

        public const string EXT_LOGFILE = ".LOG";
        public const string EXT_FABLOGFILE = ".TXT";

        public const string Identity_Host = "HOST";
        public const string Identity_Eqp = "EQP";

        // Log Messages related constants
        public const string LogMsg_Control = "FF FF";
        public const string LogMsg_Send = "SEND";
        public const string LogMsg_Recv = "RECV";
        public const string LogMsg_Info = "INFO";
        public const string LogMsg_HSMS = "HSMS";
        public const string LogMsg_SECS1 = "SECS1";
        public const string LogMsg_Warn = "WARN";
        public const string SX1Msg_Length = "Length";
        public const string SX1_SystemBytes = "SB";
        public const string SX2_SystemBytes = "SystemBytes";
        public const string LogMsg_TimeStartTag = "[";
        public const string LogMsg_TimeEndTag = "]";

        // FABuilder Log mode constants
        public const string FABLogMsg_Recd = "RECD";
        public const string FABLogMsg_Sent = "SENT";
        public const string FABLogMsg_Connect = "CONNECT";
        public const string FABLogMsg_Disconnect = "DISCONNECT";

        public const string FABLogMsg_SX1Length = "Length";

        // MODELING CONSTANTS	/***/
        public const string FILTERDATA_ITEMNAMEDICTIONARY = "ITEMNAME DICTIONARY";
        public const string FILTERDATA_ITEMVALUEDICTIONARY = "ITEMVALUE DICTIONARY";
        public const string FILTERDATA_SXFX = "SxFx";
        public const string FILTERDATA_ITEMNAME = "ITEMNAME";
        public const string FILTERDATA_ITEMVALUE = "ITEMVALUE";
        public const string FILTERDATA_ALIASVALUE = "ALIAS ITEMVALUE";
        public const string FILTERDATA_ALIASNAME = "ALIAS ITEMNAME";

        public const string FILTERDATA_FILENAME = "FilterData.dat";
        public const string FILTERDATA_ITEMNAMEDICT_FILENAME = "ItemNameDict.xml";

    }
    /// <summary>
    /// Configuration related constants
    /// </summary>
    internal class CConfigConst
    {
        CConfigConst()
        { }

        public const string SEComSimulatorConfig = "SEComSimulator.Conf";

        // Nodes
        public const string NODE_Top = "SEComSimulatorConfiguration";
        public const string NODE_LastSelect = "LASTSELECT";
        public const string NODE_SEComID = "SEComID";

        //Attributes
        public const string ATTRIBUTE_Mode = "MODE";

        // Sections
        public const string SECTION_DrvInfo = "DRVINFO";
        public const string SECTION_LogInfo = "LOGINFO";
        public const string SECTION_SECSMode = "SECSMODE";
        public const string SECTION_HSMS = "HSMS";
        public const string SECTION_SECS1 = "SECS1";
        public const string SECTION_TimeOut = "TIMEOUT";
        public const string SECTION_Dispatcher = "SECSDISPATCHER";

        // Elements
        public const string ELEMENT_LastSEComID = "LASTSEComID";
        public const string ELEMENT_Ext = "EXT";
        public const string ELEMENT_AutoBaud = "AUTOBAUD";
        public const string ELEMENT_DISPATCHERSMDFilePath = "SMDFILEPATH";
        public const string ELEMENT_IsUseDispatcher = "ISUSEDISPATCHER";

    } // END OF CConfigConst

    internal class CScenariosConst
    {
        CScenariosConst()
        { }

        public const string SEComSimulatorScenarios = "SCENARIOS.XML";

        public const string NODE_TOP = "SECOMSIMULATOR_SCENARIOS";
        public const string LIST_TRANSACTIONS = "TRANSACTIONS_LIST";
        public const string LIST_SCENARIOS = "SCENARIOS_LIST";
        public const string NODE_TRX = "TRX";
        public const string NODE_SCENARIO = "SCENARIO";

        // Transactions related nodes
        public const string NODE_TRX_NAME = "NAME";
        public const string NODE_TRX_DESC = "DESCRIPTION";

        // Scenarios related nodes
        public const string NODE_SCENARIO_TRXNAME = "TRXNAME";
        public const string NODE_SCENARIO_OPERATION = "OPERATION";
        public const string NODE_SCENARIO_PARAMS = "PARAMS";
        public const string NODE_SCENARIO_CONDITION = "CONDITION";
        public const string NODE_SCENARIO_VALUE = "VALUE";
        public const string NODE_SCENARIO_GOTOTRX = "GOTOTRX";
        public const string ATTRIBUTE_SCENARIO_NAME = "NAME";



    } // end of CScenariosConst
}
