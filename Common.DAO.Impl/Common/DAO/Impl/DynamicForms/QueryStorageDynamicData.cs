using System;

namespace TechnoPro.Common.DAO.Impl.DynamicForms
{
	// Token: 0x020000E1 RID: 225
	public class QueryStorageDynamicData
	{
		// Token: 0x0400031E RID: 798
		internal const string QS_DATETIME_PERSTUDENT_BY_PIDS = "SELECT personid,controlvalue FROM datetimeinfops WHERE controlid=@cid AND personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) ORDER BY personid";

		// Token: 0x0400031F RID: 799
		internal const string QS_NUMBER_OF_STUDENTS_ASSIGNED_TO_STAFF_IN_STAFF_DROPLIST = "SELECT COUNT(personid) AS ct FROM maininfops WHERE controlid=@cid AND controlvalue=@pid";

		// Token: 0x04000320 RID: 800
		internal const string QS_PERSONIDS_AND_CONTROLIDS_EXISTING_DATA_PERSTUDENT = "SELECT orderid AS controlid INTO #t1 FROM splitorderids(COALESCE(@cids,'0'),',')\r\n\r\nSELECT personid,controlid FROM perstudentdata2 WHERE @cids IS NULL OR controlid IN (SELECT controlid FROM #t1)\r\nORDER BY personid,controlid\r\n\r\nDROP TABLE #t1";

		// Token: 0x04000321 RID: 801
		internal const string QS_PERSONIDS_AND_CONTROLIDS_EXISTING_DATA_ACCOMMODATIONSTEMPLATEONLY = "SELECT orderid AS controlid INTO #t1 FROM splitorderids(COALESCE(@cids,'0'),',')\r\n\r\nSELECT personid,controlid FROM accommodationdata WHERE courseid=0 AND @cids IS NULL OR controlid IN (SELECT controlid FROM #t1)\r\nORDER BY personid,controlid\r\n\r\nDROP TABLE #t1";

		// Token: 0x04000322 RID: 802
		internal const string QS_APPIDS_CONTAINING_DATA_WITH_CID = "SELECT a1.appointmentid FROM \r\n    (SELECT DISTINCT appointmentid FROM maininfopa WHERE controlid=@cid AND personid=@personid \r\n     UNION \r\n     SELECT DISTINCT appointmentid FROM otherinfopa WHERE controlid=@cid AND personid=@personid \r\n     UNION \r\n     SELECT DISTINCT appointmentid FROM datetimeinfopa WHERE controlid=@cid AND personid=@personid\r\n     UNION \r\n     SELECT DISTINCT appointmentid FROM imageinfopa WHERE controlid=@cid AND personid=@personid) a1 \r\nWHERE NOT a1.appointmentid IN (SELECT appointmentid FROM appointmenticons WHERE screennum=@screennum)";

		// Token: 0x04000323 RID: 803
		internal const string QS_APPIDS_CONTAINING_DATA_WITHOUT_CID = "SELECT a1.appointmentid FROM \r\n    (SELECT DISTINCT appointmentid FROM maininfopa WHERE personid=@personid \r\n     UNION \r\n     SELECT DISTINCT appointmentid FROM otherinfopa WHERE personid=@personid \r\n     UNION \r\n     SELECT DISTINCT appointmentid FROM datetimeinfopa WHERE personid=@personid\r\n     UNION \r\n     SELECT DISTINCT appointmentid FROM imageinfopa WHERE personid=@personid) a1 \r\nWHERE NOT a1.appointmentid IN (SELECT appointmentid FROM appointmenticons WHERE screennum=@screennum)";

		// Token: 0x04000324 RID: 804
		internal const string QS_FILE_LISTS_PS_DATA = "SELECT dataid,personid,controlid,controlvalue FROM otherinfops WHERE (personid=@newpid OR personid=@oldpid) AND controlid IN (SELECT controlid FROM dynamiccontrols WHERE controlcode=20) ORDER BY personid,controlid";

		// Token: 0x04000325 RID: 805
		internal const string QS_File = "SELECT fileid,filename,filebytes,isencrypted,iscompressed,dateuploaded,whouploaded\r\nFROM files \r\nWHERE fileid=@fileid";

		// Token: 0x04000326 RID: 806
		internal const string QS_PER_DATE_ENTRIES = "SELECT    pm.appointmentid,pm.personid,pm.dateentered,\r\n            pm.whoentered AS whopersonid,pwho.firstname AS whofirstname,pwho.lastname AS wholastname,pwho.student_no AS whostudent_no,\r\n            p.firstname,p.middlename,p.lastname,p.student_no,\r\n            pm.[description],pm.screennum\r\nFROM        infopm pm LEFT JOIN people p ON p.personid=pm.personid\r\n            LEFT JOIN people pwho ON pwho.personid=pm.whoentered\r\nWHERE       pm.personid=@pid AND pm.screennum=@screennum\r\nORDER BY pm.dateentered desc";

		// Token: 0x04000327 RID: 807
		internal const string QS_PER_DATE_ENTRIES_WITH_CHILD_ENTRIES = "SELECT    pm.appointmentid,pm.personid,pm.dateentered,\r\n            pm.whoentered AS whopersonid,pwho.firstname AS whofirstname,pwho.lastname AS wholastname,pwho.student_no AS whostudent_no,\r\n            p.firstname,p.middlename,p.lastname,p.student_no,\r\n            pm.[description],pm.screennum\r\nFROM        infopm pm LEFT JOIN people p ON p.personid=pm.personid\r\n            LEFT JOIN people pwho ON pwho.personid=pm.whoentered\r\nWHERE       pm.personid=@pid AND pm.screennum=@screennum\r\nORDER BY pm.dateentered desc";

		// Token: 0x04000328 RID: 808
		internal const string QS_EXISTING_PER_DATE_ENTRY_BY_STUDENT_AND_SCREEN_AND_DATE = "SELECT    pm.appointmentid,pm.personid,pm.dateentered,\r\n            pm.whoentered AS whopersonid,pwho.firstname AS whofirstname,pwho.lastname AS wholastname,pwho.student_no AS whostudent_no,\r\n            p.firstname,p.middlename,p.lastname,p.student_no,\r\n            pm.[description],pm.screennum\r\nFROM        infopm pm LEFT JOIN people p ON p.personid=pm.personid\r\n            LEFT JOIN people pwho ON pwho.personid=pm.whoentered\r\nWHERE       pm.personid=@pid AND pm.screennum=@screennum\r\n            AND pm.dateentered BETWEEN @startdate AND @enddate\r\n            AND NOT CAST(pm.description AS varchar(max))='.DELETED.'\r\nORDER BY pm.dateentered desc";

		// Token: 0x04000329 RID: 809
		internal const string QS_UNIQUE_STUDENTS_WITH_PER_DATE_DATA_BY_FORM = "SELECT    DISTINCT ipm.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\nFROM        infopm ipm LEFT JOIN people p ON p.personid=ipm.personid\r\nWHERE       ipm.screennum=@screennum AND p.isactive=1";

		// Token: 0x0400032A RID: 810
		internal const string QS_UNIQUE_STUDENTS_WITH_PER_STUDENT_DATA_BY_FORM = "SELECT    DISTINCT pd.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\nFROM        perstudentdata2 pd LEFT JOIN people p ON p.personid=pd.personid\r\nWHERE       pd.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum)\r\n            AND p.isactive=1";

		// Token: 0x0400032B RID: 811
		internal const string QS_USER_BY_DATA_ITEM = "SELECT    ps2.personid,p.firstname,p.middlename,p.student_no,p.lastname\r\nFROM        perstudentdata2 ps2 LEFT JOIN people p ON p.personid=ps2.personid\r\nWHERE       ps2.controlid=@cid\r\n            AND p.isactive=1 --AND p.personid IN (SELECT personid FROM peoplegroups WHERE groupid=1 OR groupid=2 OR groupid=10)\r\n            AND \r\n            (\r\n                (NOT @valtext IS NULL AND ps2.valtext=@valtext)\r\n                OR\r\n                ((NOT @valbytes IS NULL AND ps2.valbytes=@valbytes) OR (NOT @valbytes2 IS NULL AND ps2.valbytes=@valbytes2) OR (NOT @valbytes3 IS NULL AND ps2.valbytes=@valbytes3))\r\n                OR\r\n                (NOT @valint IS NULL AND ps2.valint=@valint)\r\n                OR\r\n                (NOT @valdate IS NULL AND ps2.valdate=@valdate)\r\n            )";

		// Token: 0x0400032C RID: 812
		internal const string QS_Base_StudentOnly = "SELECT    ps.personid,ps.controlid,ps.valtext,ps.valbytes,ps.valimage,ps.valint,ps.valdate,ps.valbytesisencrypted\r\n            ,dc.controlid,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.controlname,dc.controlgroup\r\n            ,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers\r\n            ,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,p.firstname,p.lastname,p.student_no,p.middlename,ps.dataid,ps.uniqueid\r\nFROM        {0} ps LEFT JOIN people p ON p.personid=ps.personid\r\n            LEFT JOIN dynamiccontrols dc ON dc.controlid=ps.controlid\r\n            LEFT JOIN dynamicscreencontrols dsc ON @screennum>0 AND dsc.screennum=@screennum AND dsc.controlid=dc.controlid\r\nWHERE       ps.personid=@pid\r\n            AND\r\n            ( (@cid > 0 AND ps.controlid=@cid)\r\n             OR\r\n             (NOT @cids='' AND ps.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,',')))\r\n             OR\r\n             (@screennum>0 AND ps.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum))\r\n            )\r\nORDER BY dsc.ordernum";

		// Token: 0x0400032D RID: 813
		internal const string QS_Base_StudentAndAppointment = "SELECT    ps.personid,ps.controlid,ps.appointmentid,ps.valtext,ps.valbytes,ps.valimage,ps.valint,ps.valdate,ps.valbytesisencrypted\r\n            ,dc.controlid,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.controlname,dc.controlgroup\r\n            ,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers\r\n            ,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,p.firstname,p.lastname,p.student_no,p.middlename,ps.dataid,ps.uniqueid\r\nFROM        {0} ps LEFT JOIN people p ON p.personid=ps.personid\r\n            LEFT JOIN dynamiccontrols dc ON dc.controlid=ps.controlid\r\nWHERE       ps.personid=@pid\r\n            AND ps.appointmentid=@appid AND\r\n            ( (@cid > 0 AND ps.controlid=@cid)\r\n             OR\r\n             (NOT @cids='' AND ps.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,',')))\r\n             OR\r\n             (@screennum>0 AND ps.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum))\r\n            )";

		// Token: 0x0400032E RID: 814
		internal static string QS_OnlineForm = string.Format("SELECT    ps.personid,ps.controlid,ps.appointmentid,ps.valtext,ps.valbytes,ps.valimage,ps.valint,ps.valdate,ps.valbytesisencrypted\r\n            ,dc.controlid,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.controlname,dc.controlgroup\r\n            ,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers\r\n            ,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,p.firstname,p.lastname,p.student_no,p.middlename,ps.dataid,ps.uniqueid\r\nFROM        {0} ps LEFT JOIN people p ON p.personid=ps.personid\r\n            LEFT JOIN dynamiccontrols dc ON dc.controlid=ps.controlid\r\nWHERE       ps.personid=@pid\r\n            AND ps.appointmentid=@appid AND\r\n            ( (@cid > 0 AND ps.controlid=@cid)\r\n             OR\r\n             (NOT @cids='' AND ps.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,',')))\r\n             OR\r\n             (@screennum>0 AND ps.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum))\r\n            )", "peronlineformdata2").Replace("appointmentid", "people_onlineformId");

		// Token: 0x0400032F RID: 815
		internal static string QS_Survey = string.Format("SELECT    ps.personid,ps.controlid,ps.appointmentid,ps.valtext,ps.valbytes,ps.valimage,ps.valint,ps.valdate,ps.valbytesisencrypted\r\n            ,dc.controlid,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.controlname,dc.controlgroup\r\n            ,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers\r\n            ,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,p.firstname,p.lastname,p.student_no,p.middlename,ps.dataid,ps.uniqueid\r\nFROM        {0} ps LEFT JOIN people p ON p.personid=ps.personid\r\n            LEFT JOIN dynamiccontrols dc ON dc.controlid=ps.controlid\r\nWHERE       ps.personid=@pid\r\n            AND ps.appointmentid=@appid AND\r\n            ( (@cid > 0 AND ps.controlid=@cid)\r\n             OR\r\n             (NOT @cids='' AND ps.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,',')))\r\n             OR\r\n             (@screennum>0 AND ps.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum))\r\n            )", "persurveydata2").Replace("appointmentid", "people_surveyId");

		// Token: 0x04000330 RID: 816
		internal static string QS_PS = string.Format("SELECT    ps.personid,ps.controlid,ps.valtext,ps.valbytes,ps.valimage,ps.valint,ps.valdate,ps.valbytesisencrypted\r\n            ,dc.controlid,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.controlname,dc.controlgroup\r\n            ,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers\r\n            ,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,p.firstname,p.lastname,p.student_no,p.middlename,ps.dataid,ps.uniqueid\r\nFROM        {0} ps LEFT JOIN people p ON p.personid=ps.personid\r\n            LEFT JOIN dynamiccontrols dc ON dc.controlid=ps.controlid\r\n            LEFT JOIN dynamicscreencontrols dsc ON @screennum>0 AND dsc.screennum=@screennum AND dsc.controlid=dc.controlid\r\nWHERE       ps.personid=@pid\r\n            AND\r\n            ( (@cid > 0 AND ps.controlid=@cid)\r\n             OR\r\n             (NOT @cids='' AND ps.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,',')))\r\n             OR\r\n             (@screennum>0 AND ps.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum))\r\n            )\r\nORDER BY dsc.ordernum", "perstudentdata2");

		// Token: 0x04000331 RID: 817
		internal static string QS_PA = string.Format("SELECT    ps.personid,ps.controlid,ps.appointmentid,ps.valtext,ps.valbytes,ps.valimage,ps.valint,ps.valdate,ps.valbytesisencrypted\r\n            ,dc.controlid,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.controlname,dc.controlgroup\r\n            ,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers\r\n            ,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,p.firstname,p.lastname,p.student_no,p.middlename,ps.dataid,ps.uniqueid\r\nFROM        {0} ps LEFT JOIN people p ON p.personid=ps.personid\r\n            LEFT JOIN dynamiccontrols dc ON dc.controlid=ps.controlid\r\nWHERE       ps.personid=@pid\r\n            AND ps.appointmentid=@appid AND\r\n            ( (@cid > 0 AND ps.controlid=@cid)\r\n             OR\r\n             (NOT @cids='' AND ps.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,',')))\r\n             OR\r\n             (@screennum>0 AND ps.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum))\r\n            )", "perappdata2");

		// Token: 0x04000332 RID: 818
		internal static string QS_AN = "";

		// Token: 0x04000333 RID: 819
		internal static string QS_PI = string.Format("SELECT    ps.personid,ps.controlid,ps.valtext,ps.valbytes,ps.valimage,ps.valint,ps.valdate,ps.valbytesisencrypted\r\n            ,dc.controlid,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.controlname,dc.controlgroup\r\n            ,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers\r\n            ,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,p.firstname,p.lastname,p.student_no,p.middlename,ps.dataid,ps.uniqueid\r\nFROM        {0} ps LEFT JOIN people p ON p.personid=ps.personid\r\n            LEFT JOIN dynamiccontrols dc ON dc.controlid=ps.controlid\r\n            LEFT JOIN dynamicscreencontrols dsc ON @screennum>0 AND dsc.screennum=@screennum AND dsc.controlid=dc.controlid\r\nWHERE       ps.personid=@pid\r\n            AND\r\n            ( (@cid > 0 AND ps.controlid=@cid)\r\n             OR\r\n             (NOT @cids='' AND ps.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,',')))\r\n             OR\r\n             (@screennum>0 AND ps.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum))\r\n            )\r\nORDER BY dsc.ordernum", "perinstructordata2").Replace("ps.personid=@pid", "ps.appointmentid=@pid");

		// Token: 0x04000334 RID: 820
		internal static string QS_Inv = "";

		// Token: 0x04000335 RID: 821
		internal static string QS_AccommodationTemplateOnly = string.Format("SELECT    ps.personid,ps.controlid,ps.appointmentid,ps.valtext,ps.valbytes,ps.valimage,ps.valint,ps.valdate,ps.valbytesisencrypted\r\n            ,dc.controlid,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.controlname,dc.controlgroup\r\n            ,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers\r\n            ,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,p.firstname,p.lastname,p.student_no,p.middlename,ps.dataid,ps.uniqueid\r\nFROM        {0} ps LEFT JOIN people p ON p.personid=ps.personid\r\n            LEFT JOIN dynamiccontrols dc ON dc.controlid=ps.controlid\r\nWHERE       ps.personid=@pid\r\n            AND ps.appointmentid=@appid AND\r\n            ( (@cid > 0 AND ps.controlid=@cid)\r\n             OR\r\n             (NOT @cids='' AND ps.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,',')))\r\n             OR\r\n             (@screennum>0 AND ps.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum))\r\n            )".Replace("@appid", "0").Replace("appointmentid", "courseid"), "accommodationdata");

		// Token: 0x04000336 RID: 822
		internal static string QS_Accommodation = string.Format("SELECT    ps.personid,ps.controlid,ps.appointmentid,ps.valtext,ps.valbytes,ps.valimage,ps.valint,ps.valdate,ps.valbytesisencrypted\r\n            ,dc.controlid,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.controlname,dc.controlgroup\r\n            ,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers\r\n            ,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,p.firstname,p.lastname,p.student_no,p.middlename,ps.dataid,ps.uniqueid\r\nFROM        {0} ps LEFT JOIN people p ON p.personid=ps.personid\r\n            LEFT JOIN dynamiccontrols dc ON dc.controlid=ps.controlid\r\nWHERE       ps.personid=@pid\r\n            AND ps.appointmentid=@appid AND\r\n            ( (@cid > 0 AND ps.controlid=@cid)\r\n             OR\r\n             (NOT @cids='' AND ps.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,',')))\r\n             OR\r\n             (@screennum>0 AND ps.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum))\r\n            )".Replace("appointmentid", "courseid"), "accommodationdata");

		// Token: 0x04000337 RID: 823
		internal static string QS_PM = string.Format("SELECT    ps.personid,ps.controlid,ps.appointmentid,ps.valtext,ps.valbytes,ps.valimage,ps.valint,ps.valdate,ps.valbytesisencrypted\r\n            ,dc.controlid,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.controlname,dc.controlgroup\r\n            ,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers\r\n            ,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,p.firstname,p.lastname,p.student_no,p.middlename,ps.dataid,ps.uniqueid\r\nFROM        {0} ps LEFT JOIN people p ON p.personid=ps.personid\r\n            LEFT JOIN dynamiccontrols dc ON dc.controlid=ps.controlid\r\nWHERE       ps.personid=@pid\r\n            AND ps.appointmentid=@appid AND\r\n            ( (@cid > 0 AND ps.controlid=@cid)\r\n             OR\r\n             (NOT @cids='' AND ps.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,',')))\r\n             OR\r\n             (@screennum>0 AND ps.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum))\r\n            )", "pmdata2");

		// Token: 0x04000338 RID: 824
		internal static string QS_PC = string.Format("SELECT    ps.personid,ps.controlid,ps.appointmentid,ps.valtext,ps.valbytes,ps.valimage,ps.valint,ps.valdate,ps.valbytesisencrypted\r\n            ,dc.controlid,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.controlname,dc.controlgroup\r\n            ,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers\r\n            ,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,p.firstname,p.lastname,p.student_no,p.middlename,ps.dataid,ps.uniqueid\r\nFROM        {0} ps LEFT JOIN people p ON p.personid=ps.personid\r\n            LEFT JOIN dynamiccontrols dc ON dc.controlid=ps.controlid\r\nWHERE       ps.personid=@pid\r\n            AND ps.appointmentid=@appid AND\r\n            ( (@cid > 0 AND ps.controlid=@cid)\r\n             OR\r\n             (NOT @cids='' AND ps.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,',')))\r\n             OR\r\n             (@screennum>0 AND ps.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum))\r\n            )", "pcdata2");

		// Token: 0x04000339 RID: 825
		internal static string QS_Wl = "";

		// Token: 0x0400033A RID: 826
		internal const string QI_Other_Base_StudentOnly = "IF EXISTS(SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid)\r\nBEGIN\r\n    UPDATE otherinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid;\r\n    SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO otherinfo{0} (screennum,personid,controlid,controlvalue) VALUES (0,@pid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND";

		// Token: 0x0400033B RID: 827
		internal const string QI_Other_Base_StudentAndAppointment = "IF EXISTS(SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE otherinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO otherinfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND";

		// Token: 0x0400033C RID: 828
		internal const string QI_Other_Base_StudentAndAppointment_NoScreenNumInDataEntryTable = "IF EXISTS(SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE otherinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO otherinfo{0} (personid,appointmentid,controlid,controlvalue) VALUES (@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND";

		// Token: 0x0400033D RID: 829
		internal static string QI_SurveyOther = string.Format("IF EXISTS(SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE otherinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO otherinfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "Survey").Replace("appointmentid", "people_surveyId");

		// Token: 0x0400033E RID: 830
		internal static string QI_OnlineFormOther = string.Format("IF EXISTS(SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE otherinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO otherinfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "OnlineForm").Replace("appointmentid", "people_onlineformId");

		// Token: 0x0400033F RID: 831
		internal static string QI_PSOther = string.Format("IF EXISTS(SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid)\r\nBEGIN\r\n    UPDATE otherinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid;\r\n    SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO otherinfo{0} (screennum,personid,controlid,controlvalue) VALUES (0,@pid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "PS");

		// Token: 0x04000340 RID: 832
		internal static string QI_PAOther = string.Format("IF EXISTS(SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE otherinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO otherinfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "PA");

		// Token: 0x04000341 RID: 833
		internal static string QI_ANOther = string.Format("IF EXISTS(SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid)\r\nBEGIN\r\n    UPDATE otherinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid;\r\n    SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO otherinfo{0} (screennum,personid,controlid,controlvalue) VALUES (0,@pid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "AN");

		// Token: 0x04000342 RID: 834
		internal static string QI_PIOther = string.Format("IF EXISTS(SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid)\r\nBEGIN\r\n    UPDATE otherinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid;\r\n    SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO otherinfo{0} (screennum,personid,controlid,controlvalue) VALUES (0,@pid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "PI");

		// Token: 0x04000343 RID: 835
		internal static string QI_InvOther = string.Format("IF EXISTS(SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid)\r\nBEGIN\r\n    UPDATE otherinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid;\r\n    SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO otherinfo{0} (screennum,personid,controlid,controlvalue) VALUES (0,@pid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "Inv");

		// Token: 0x04000344 RID: 836
		internal static string QI_AccommodationTemplateOnlyOther = string.Format("IF EXISTS(SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE otherinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO otherinfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND".Replace("@appid", "0").Replace("appointmentid", "courseid"), "accommodationps");

		// Token: 0x04000345 RID: 837
		internal static string QI_AccommodationOther = string.Format("IF EXISTS(SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE otherinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO otherinfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND".Replace("appointmentid", "courseid"), "accommodationps");

		// Token: 0x04000346 RID: 838
		internal static string QI_PMOther = string.Format("IF EXISTS(SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE otherinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO otherinfo{0} (personid,appointmentid,controlid,controlvalue) VALUES (@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "PM");

		// Token: 0x04000347 RID: 839
		internal static string QI_PCOther = string.Format("IF EXISTS(SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE otherinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO otherinfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "PC");

		// Token: 0x04000348 RID: 840
		internal static string QI_WlOther = string.Format("IF EXISTS(SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE otherinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO otherinfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "WL");

		// Token: 0x04000349 RID: 841
		internal const string QI_Image_Base_StudentOnly = "IF EXISTS(SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid)\r\nBEGIN\r\n    UPDATE imageinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid;\r\n    SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO imageinfo{0} (screennum,personid,controlid,controlvalue) VALUES (0,@pid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND";

		// Token: 0x0400034A RID: 842
		internal const string QI_Image_Base_StudentAndAppointment = "IF EXISTS(SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE imageinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO imageinfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND";

		// Token: 0x0400034B RID: 843
		internal const string QI_Image_Base_StudentAndAppointment_NoScreenNumInDataEntryTable = "IF EXISTS(SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE imageinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO imageinfo{0} (personid,appointmentid,controlid,controlvalue) VALUES (@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND";

		// Token: 0x0400034C RID: 844
		internal static string QI_SurveyImage = string.Format("IF EXISTS(SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE imageinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO imageinfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "Survey").Replace("appointmentid", "people_surveyId");

		// Token: 0x0400034D RID: 845
		internal static string QI_OnlineFormImage = string.Format("IF EXISTS(SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE imageinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO imageinfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "OnlineForm").Replace("appointmentid", "people_onlineformId");

		// Token: 0x0400034E RID: 846
		internal static string QI_PSImage = string.Format("IF EXISTS(SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid)\r\nBEGIN\r\n    UPDATE imageinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid;\r\n    SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO imageinfo{0} (screennum,personid,controlid,controlvalue) VALUES (0,@pid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "PS");

		// Token: 0x0400034F RID: 847
		internal static string QI_PAImage = string.Format("IF EXISTS(SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE imageinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO imageinfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "PA");

		// Token: 0x04000350 RID: 848
		internal static string QI_ANImage = string.Format("IF EXISTS(SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid)\r\nBEGIN\r\n    UPDATE imageinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid;\r\n    SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO imageinfo{0} (screennum,personid,controlid,controlvalue) VALUES (0,@pid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "AN");

		// Token: 0x04000351 RID: 849
		internal static string QI_PIImage = string.Format("IF EXISTS(SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid)\r\nBEGIN\r\n    UPDATE imageinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid;\r\n    SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO imageinfo{0} (screennum,personid,controlid,controlvalue) VALUES (0,@pid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "PI");

		// Token: 0x04000352 RID: 850
		internal static string QI_InvImage = string.Format("IF EXISTS(SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid)\r\nBEGIN\r\n    UPDATE imageinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid;\r\n    SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO imageinfo{0} (screennum,personid,controlid,controlvalue) VALUES (0,@pid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "Inv");

		// Token: 0x04000353 RID: 851
		internal static string QI_AccommodationTemplateOnlyImage = string.Format("IF EXISTS(SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE imageinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO imageinfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND".Replace("@appid", "0").Replace("appointmentid", "courseid"), "accommodationps");

		// Token: 0x04000354 RID: 852
		internal static string QI_AccommodationImage = string.Format("IF EXISTS(SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE imageinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO imageinfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND".Replace("appointmentid", "courseid"), "accommodationps");

		// Token: 0x04000355 RID: 853
		internal static string QI_PMImage = string.Format("IF EXISTS(SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE imageinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO imageinfo{0} (personid,appointmentid,controlid,controlvalue) VALUES (@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "PM");

		// Token: 0x04000356 RID: 854
		internal static string QI_PCImage = string.Format("IF EXISTS(SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE imageinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO imageinfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "PC");

		// Token: 0x04000357 RID: 855
		internal static string QI_WlImage = string.Format("IF EXISTS(SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE imageinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO imageinfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "WL");

		// Token: 0x04000358 RID: 856
		internal const string QI_Main_Base_StudentOnly = "IF EXISTS(SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid)\r\nBEGIN\r\n    UPDATE maininfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid;\r\n    SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO maininfo{0} (screennum,personid,controlid,controlvalue) VALUES (0,@pid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND";

		// Token: 0x04000359 RID: 857
		internal const string QI_Main_Base_StudentAndAppointment = "IF EXISTS(SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE maininfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO maininfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND";

		// Token: 0x0400035A RID: 858
		internal const string QI_Main_Base_StudentAndAppointment_NoScreenNumInDataEntryTable = "IF EXISTS(SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE maininfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO maininfo{0} (personid,appointmentid,controlid,controlvalue) VALUES (@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND";

		// Token: 0x0400035B RID: 859
		internal static string QI_OnlineFormMain = string.Format("IF EXISTS(SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE maininfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO maininfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "OnlineForm").Replace("appointmentid", "people_onlineFormId");

		// Token: 0x0400035C RID: 860
		internal static string QI_SurveyMain = string.Format("IF EXISTS(SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE maininfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO maininfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "Survey").Replace("appointmentid", "people_surveyId");

		// Token: 0x0400035D RID: 861
		internal static string QI_PSMain = string.Format("IF EXISTS(SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid)\r\nBEGIN\r\n    UPDATE maininfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid;\r\n    SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO maininfo{0} (screennum,personid,controlid,controlvalue) VALUES (0,@pid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "PS");

		// Token: 0x0400035E RID: 862
		internal static string QI_PAMain = string.Format("IF EXISTS(SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE maininfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO maininfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "PA");

		// Token: 0x0400035F RID: 863
		internal static string QI_ANMain = string.Format("IF EXISTS(SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid)\r\nBEGIN\r\n    UPDATE maininfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid;\r\n    SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO maininfo{0} (screennum,personid,controlid,controlvalue) VALUES (0,@pid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "AN");

		// Token: 0x04000360 RID: 864
		internal static string QI_PIMain = string.Format("IF EXISTS(SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid)\r\nBEGIN\r\n    UPDATE maininfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid;\r\n    SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO maininfo{0} (screennum,personid,controlid,controlvalue) VALUES (0,@pid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "PI");

		// Token: 0x04000361 RID: 865
		internal static string QI_InvMain = string.Format("IF EXISTS(SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid)\r\nBEGIN\r\n    UPDATE maininfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid;\r\n    SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO maininfo{0} (screennum,personid,controlid,controlvalue) VALUES (0,@pid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "Inv");

		// Token: 0x04000362 RID: 866
		internal static string QI_AccommodationTemplateOnlyMain = string.Format("IF EXISTS(SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE maininfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO maininfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND".Replace("@appid", "0").Replace("appointmentid", "courseid"), "accommodationps");

		// Token: 0x04000363 RID: 867
		internal static string QI_AccommodationMain = string.Format("IF EXISTS(SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE maininfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO maininfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND".Replace("appointmentid", "courseid"), "accommodationps");

		// Token: 0x04000364 RID: 868
		internal static string QI_PMMain = string.Format("IF EXISTS(SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE maininfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO maininfo{0} (personid,appointmentid,controlid,controlvalue) VALUES (@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "PM");

		// Token: 0x04000365 RID: 869
		internal static string QI_PCMain = string.Format("IF EXISTS(SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE maininfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO maininfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "PC");

		// Token: 0x04000366 RID: 870
		internal static string QI_WlMain = string.Format("IF EXISTS(SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE maininfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM maininfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO maininfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "WL");

		// Token: 0x04000367 RID: 871
		internal const string QI_DateTime_Base_StudentOnly = "IF EXISTS(SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid)\r\nBEGIN\r\n    UPDATE datetimeinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid;\r\n    SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO datetimeinfo{0} (screennum,personid,controlid,controlvalue) VALUES (0,@pid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND";

		// Token: 0x04000368 RID: 872
		internal const string QI_DateTime_Base_StudentAndAppointment = "IF EXISTS(SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE datetimeinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO datetimeinfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND";

		// Token: 0x04000369 RID: 873
		internal const string QI_DateTime_Base_StudentAndAppointment_NoScreenNumInDataEntryTable = "IF EXISTS(SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE datetimeinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO datetimeinfo{0} (personid,appointmentid,controlid,controlvalue) VALUES (@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND";

		// Token: 0x0400036A RID: 874
		internal static string QI_SurveyDateTime = string.Format("IF EXISTS(SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE datetimeinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO datetimeinfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "Survey").Replace("appointmentid", "people_surveyId");

		// Token: 0x0400036B RID: 875
		internal static string QI_OnlineFormDateTime = string.Format("IF EXISTS(SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE datetimeinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO datetimeinfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "OnlineForm").Replace("appointmentid", "people_onlineFormId");

		// Token: 0x0400036C RID: 876
		internal static string QI_PSDateTime = string.Format("IF EXISTS(SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid)\r\nBEGIN\r\n    UPDATE datetimeinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid;\r\n    SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO datetimeinfo{0} (screennum,personid,controlid,controlvalue) VALUES (0,@pid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "PS");

		// Token: 0x0400036D RID: 877
		internal static string QI_PADateTime = string.Format("IF EXISTS(SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE datetimeinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO datetimeinfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "PA");

		// Token: 0x0400036E RID: 878
		internal static string QI_ANDateTime = string.Format("IF EXISTS(SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid)\r\nBEGIN\r\n    UPDATE datetimeinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid;\r\n    SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO datetimeinfo{0} (screennum,personid,controlid,controlvalue) VALUES (0,@pid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "AN");

		// Token: 0x0400036F RID: 879
		internal static string QI_PIDateTime = string.Format("IF EXISTS(SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid)\r\nBEGIN\r\n    UPDATE datetimeinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid;\r\n    SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO datetimeinfo{0} (screennum,personid,controlid,controlvalue) VALUES (0,@pid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "PI");

		// Token: 0x04000370 RID: 880
		internal static string QI_InvDateTime = string.Format("IF EXISTS(SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid)\r\nBEGIN\r\n    UPDATE datetimeinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid;\r\n    SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO datetimeinfo{0} (screennum,personid,controlid,controlvalue) VALUES (0,@pid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "Inv");

		// Token: 0x04000371 RID: 881
		internal static string QI_AccommodationTemplateOnlyDateTime = string.Format("IF EXISTS(SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE datetimeinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO datetimeinfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND".Replace("@appid", "0").Replace("appointmentid", "courseid"), "accommodationps");

		// Token: 0x04000372 RID: 882
		internal static string QI_AccommodationDateTime = string.Format("IF EXISTS(SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE datetimeinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO datetimeinfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND".Replace("appointmentid", "courseid"), "accommodationps");

		// Token: 0x04000373 RID: 883
		internal static string QI_PMDateTime = string.Format("IF EXISTS(SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE datetimeinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO datetimeinfo{0} (personid,appointmentid,controlid,controlvalue) VALUES (@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "PM");

		// Token: 0x04000374 RID: 884
		internal static string QI_PCDateTime = string.Format("IF EXISTS(SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE datetimeinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO datetimeinfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "PC");

		// Token: 0x04000375 RID: 885
		internal static string QI_WlDateTime = string.Format("IF EXISTS(SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)\r\nBEGIN\r\n    UPDATE datetimeinfo{0} SET controlvalue=@val WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid;\r\n    SELECT dataid FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO datetimeinfo{0} (screennum,personid,appointmentid,controlid,controlvalue) VALUES (0,@pid,@appid,@cid,@val);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS dataid\r\nEND", "WL");

		// Token: 0x04000376 RID: 886
		internal const string QU_MERGE_PS_DATA = "DELETE FROM maininfops WHERE maininfops.personid=@oldpid AND EXISTS(SELECT q.dataid FROM maininfops q WHERE q.personid=@newpid AND q.controlid=maininfops.controlid);\r\nUPDATE maininfops SET personid=@newpid WHERE personid=@oldpid AND NOT controlid IN (SELECT controlid FROM dynamiccontrols WHERE controlcode=20);\r\n\r\nDELETE FROM otherinfops WHERE otherinfops.personid=@oldpid AND EXISTS(SELECT q.dataid FROM otherinfops q WHERE q.personid=@newpid AND q.controlid=otherinfops.controlid) AND NOT otherinfops.controlid IN (SELECT controlid FROM dynamiccontrols WHERE controlcode=20);\r\nUPDATE otherinfops SET personid=@newpid WHERE personid=@oldpid AND NOT controlid IN (SELECT controlid FROM dynamiccontrols WHERE controlcode=20);\r\n\r\nDELETE FROM datetimeinfops WHERE datetimeinfops.personid=@oldpid AND EXISTS(SELECT q.dataid FROM datetimeinfops q WHERE q.personid=@newpid AND q.controlid=datetimeinfops.controlid);\r\nUPDATE datetimeinfops SET personid=@newpid WHERE personid=@oldpid AND NOT controlid IN (SELECT controlid FROM dynamiccontrols WHERE controlcode=20);\r\n\r\nDELETE FROM imageinfops WHERE imageinfops.personid=@oldpid AND EXISTS(SELECT q.dataid FROM imageinfops q WHERE q.personid=@newpid AND q.controlid=imageinfops.controlid) AND NOT imageinfops.controlid IN (SELECT controlid FROM dynamiccontrols WHERE controlcode=20);\r\nUPDATE imageinfops SET personid=@newpid WHERE personid=@oldpid AND NOT controlid IN (SELECT controlid FROM dynamiccontrols WHERE controlcode=20);";

		// Token: 0x04000377 RID: 887
		internal const string QU_MERGE_PA_DATA = "DELETE FROM maininfopa WHERE maininfopa.personid=@oldpid AND EXISTS(SELECT q.dataid FROM maininfopa q WHERE q.personid=@newpid AND q.appointmentid=maininfopa.appointmentid AND q.controlid=maininfopa.controlid);\r\nUPDATE maininfopa SET personid=@newpid WHERE personid=@oldpid;\r\n\r\nDELETE FROM otherinfopa WHERE otherinfopa.personid=@oldpid AND EXISTS(SELECT q.dataid FROM otherinfopa q WHERE q.personid=@newpid AND q.appointmentid=otherinfopa.appointmentid AND q.controlid=otherinfopa.controlid);\r\nUPDATE otherinfopa SET personid=@newpid WHERE personid=@oldpid;\r\n\r\nDELETE FROM datetimeinfopa WHERE datetimeinfopa.personid=@oldpid AND EXISTS(SELECT q.dataid FROM datetimeinfopa q WHERE q.personid=@newpid AND q.appointmentid=datetimeinfopa.appointmentid AND q.controlid=datetimeinfopa.controlid);\r\nUPDATE datetimeinfopa SET personid=@newpid WHERE personid=@oldpid;\r\n\r\nDELETE FROM imageinfopa WHERE imageinfopa.personid=@oldpid AND EXISTS(SELECT q.dataid FROM imageinfopa q WHERE q.personid=@newpid AND q.appointmentid=imageinfopa.appointmentid AND q.controlid=imageinfopa.controlid);\r\nUPDATE imageinfopa SET personid=@newpid WHERE personid=@oldpid;";

		// Token: 0x04000378 RID: 888
		internal const string QU_MERGE_PM_DATA = "DELETE FROM maininfopm WHERE maininfopm.personid=@oldpid AND EXISTS(SELECT q.dataid FROM maininfopm q WHERE q.personid=@newpid AND q.appointmentid=maininfopm.appointmentid AND q.controlid=maininfopm.controlid);\r\nUPDATE maininfopm SET personid=@newpid WHERE personid=@oldpid;\r\n\r\nDELETE FROM otherinfopm WHERE otherinfopm.personid=@oldpid AND EXISTS(SELECT q.dataid FROM otherinfopm q WHERE q.personid=@newpid AND q.appointmentid=otherinfopm.appointmentid AND q.controlid=otherinfopm.controlid);\r\nUPDATE otherinfopm SET personid=@newpid WHERE personid=@oldpid;\r\n\r\nDELETE FROM datetimeinfopm WHERE datetimeinfopm.personid=@oldpid AND EXISTS(SELECT q.dataid FROM datetimeinfopm q WHERE q.personid=@newpid AND q.appointmentid=datetimeinfopm.appointmentid AND q.controlid=datetimeinfopm.controlid);\r\nUPDATE datetimeinfopm SET personid=@newpid WHERE personid=@oldpid;\r\n\r\nDELETE FROM imageinfopm WHERE imageinfopm.personid=@oldpid AND EXISTS(SELECT q.dataid FROM imageinfopm q WHERE q.personid=@newpid AND q.appointmentid=imageinfopm.appointmentid AND q.controlid=imageinfopm.controlid);\r\nUPDATE imageinfopm SET personid=@newpid WHERE personid=@oldpid;\r\n\r\nUPDATE infopm SET personid=@newpid WHERE personid=@oldpid;";

		// Token: 0x04000379 RID: 889
		internal const string QU_MERGE_ACCOMM_DATA = "DELETE FROM maininfoaccommodationps WHERE maininfoaccommodationps.personid=@oldpid AND EXISTS(SELECT q.dataid FROM maininfoaccommodationps q WHERE q.personid=@newpid AND q.courseid=maininfoaccommodationps.courseid AND q.controlid=maininfoaccommodationps.controlid);\r\nUPDATE maininfoaccommodationps SET personid=@newpid WHERE personid=@oldpid;\r\n\r\nDELETE FROM otherinfoaccommodationps WHERE otherinfoaccommodationps.personid=@oldpid AND EXISTS(SELECT q.dataid FROM otherinfoaccommodationps q WHERE q.personid=@newpid AND q.courseid=otherinfoaccommodationps.courseid AND q.controlid=otherinfoaccommodationps.controlid);\r\nUPDATE otherinfoaccommodationps SET personid=@newpid WHERE personid=@oldpid;\r\n\r\nDELETE FROM datetimeinfoaccommodationps WHERE datetimeinfoaccommodationps.personid=@oldpid AND EXISTS(SELECT q.dataid FROM datetimeinfoaccommodationps q WHERE q.personid=@newpid AND q.courseid=datetimeinfoaccommodationps.courseid AND q.controlid=datetimeinfoaccommodationps.controlid);\r\nUPDATE datetimeinfoaccommodationps SET personid=@newpid WHERE personid=@oldpid;\r\n\r\nDELETE FROM imageinfoaccommodationps WHERE imageinfoaccommodationps.personid=@oldpid AND EXISTS(SELECT q.dataid FROM imageinfoaccommodationps q WHERE q.personid=@newpid AND q.courseid=imageinfoaccommodationps.courseid AND q.controlid=imageinfoaccommodationps.controlid);\r\nUPDATE imageinfoaccommodationps SET personid=@newpid WHERE personid=@oldpid;";

		// Token: 0x0400037A RID: 890
		internal const string QD_Main_Base_StudentOnly = "--INSERT INTO archive_MainInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@cid,controlvalue,getdate(),@whoami FROM maininfo{0} WHERE personid=@pid AND controlid=@cid;\r\nDELETE FROM maininfo{0} WHERE personid=@pid AND controlid=@cid";

		// Token: 0x0400037B RID: 891
		internal const string QD_Main_Base_StudentAndAppointment = "--INSERT INTO archive_MainInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM maininfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM maininfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid";

		// Token: 0x0400037C RID: 892
		internal static string QD_SurveyMain = string.Format("--INSERT INTO archive_MainInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM maininfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM maininfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid", "Survey").Replace("appointmentid", "people_surveyId");

		// Token: 0x0400037D RID: 893
		internal static string QD_OnlineFormMain = string.Format("--INSERT INTO archive_MainInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM maininfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM maininfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid", "OnlineForm").Replace("appointmentid", "people_onlineFormId");

		// Token: 0x0400037E RID: 894
		internal static string QD_PSMain = string.Format("--INSERT INTO archive_MainInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@cid,controlvalue,getdate(),@whoami FROM maininfo{0} WHERE personid=@pid AND controlid=@cid;\r\nDELETE FROM maininfo{0} WHERE personid=@pid AND controlid=@cid", "PS");

		// Token: 0x0400037F RID: 895
		internal static string QD_PAMain = string.Format("--INSERT INTO archive_MainInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM maininfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM maininfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid", "PA");

		// Token: 0x04000380 RID: 896
		internal static string QD_ANMain = string.Format("--INSERT INTO archive_MainInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@cid,controlvalue,getdate(),@whoami FROM maininfo{0} WHERE personid=@pid AND controlid=@cid;\r\nDELETE FROM maininfo{0} WHERE personid=@pid AND controlid=@cid", "AN");

		// Token: 0x04000381 RID: 897
		internal static string QD_PIMain = string.Format("--INSERT INTO archive_MainInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@cid,controlvalue,getdate(),@whoami FROM maininfo{0} WHERE personid=@pid AND controlid=@cid;\r\nDELETE FROM maininfo{0} WHERE personid=@pid AND controlid=@cid", "PI");

		// Token: 0x04000382 RID: 898
		internal static string QD_InvMain = string.Format("--INSERT INTO archive_MainInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@cid,controlvalue,getdate(),@whoami FROM maininfo{0} WHERE personid=@pid AND controlid=@cid;\r\nDELETE FROM maininfo{0} WHERE personid=@pid AND controlid=@cid", "Inv");

		// Token: 0x04000383 RID: 899
		internal static string QD_AccommodationTemplateOnlyMain = string.Format("--INSERT INTO archive_MainInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM maininfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM maininfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid".Replace("INSERT INTO archive_MainInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)", "").Replace("@appid", "0").Replace("appointmentid", "courseid"), "accommodationps");

		// Token: 0x04000384 RID: 900
		internal static string QD_AccommodationMain = string.Format("--INSERT INTO archive_MainInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM maininfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM maininfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid".Replace("INSERT INTO archive_MainInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)", "").Replace("appointmentid", "courseid"), "accommodationps");

		// Token: 0x04000385 RID: 901
		internal static string QD_PMMain = string.Format("--INSERT INTO archive_MainInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM maininfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM maininfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid", "PM");

		// Token: 0x04000386 RID: 902
		internal static string QD_PCMain = string.Format("--INSERT INTO archive_MainInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM maininfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM maininfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid", "PC");

		// Token: 0x04000387 RID: 903
		internal static string QD_WlMain = string.Format("--INSERT INTO archive_MainInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM maininfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM maininfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid", "WL");

		// Token: 0x04000388 RID: 904
		internal const string QD_Other_Base_StudentOnly = "--INSERT INTO archive_OtherInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@cid,controlvalue,getdate(),@whoami FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid;\r\nDELETE FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid";

		// Token: 0x04000389 RID: 905
		internal const string QD_Other_Base_StudentAndAppointment = "--INSERT INTO archive_OtherInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM otherinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM otherinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid";

		// Token: 0x0400038A RID: 906
		internal static string QD_SurveyOther = string.Format("--INSERT INTO archive_OtherInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM otherinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM otherinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid", "Survey").Replace("appointmentid", "people_surveyId");

		// Token: 0x0400038B RID: 907
		internal static string QD_OnlineFormOther = string.Format("--INSERT INTO archive_OtherInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM otherinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM otherinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid", "OnlineForm").Replace("appointmentid", "people_onlineformId");

		// Token: 0x0400038C RID: 908
		internal static string QD_PSOther = string.Format("--INSERT INTO archive_OtherInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@cid,controlvalue,getdate(),@whoami FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid;\r\nDELETE FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid", "PS");

		// Token: 0x0400038D RID: 909
		internal static string QD_PAOther = string.Format("--INSERT INTO archive_OtherInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM otherinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM otherinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid", "PA");

		// Token: 0x0400038E RID: 910
		internal static string QD_ANOther = string.Format("--INSERT INTO archive_OtherInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@cid,controlvalue,getdate(),@whoami FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid;\r\nDELETE FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid", "AN");

		// Token: 0x0400038F RID: 911
		internal static string QD_PIOther = string.Format("--INSERT INTO archive_OtherInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@cid,controlvalue,getdate(),@whoami FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid;\r\nDELETE FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid", "PI");

		// Token: 0x04000390 RID: 912
		internal static string QD_InvOther = string.Format("--INSERT INTO archive_OtherInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@cid,controlvalue,getdate(),@whoami FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid;\r\nDELETE FROM otherinfo{0} WHERE personid=@pid AND controlid=@cid", "Inv");

		// Token: 0x04000391 RID: 913
		internal static string QD_AccommodationTemplateOnlyOther = string.Format("--INSERT INTO archive_OtherInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM otherinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM otherinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid".Replace("INSERT INTO archive_OtherInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)", "").Replace("@appid", "0").Replace("appointmentid", "courseid"), "accommodationps");

		// Token: 0x04000392 RID: 914
		internal static string QD_AccommodationOther = string.Format("--INSERT INTO archive_OtherInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM otherinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM otherinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid".Replace("INSERT INTO archive_OtherInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)", "").Replace("appointmentid", "courseid"), "accommodationps");

		// Token: 0x04000393 RID: 915
		internal static string QD_PMOther = string.Format("--INSERT INTO archive_OtherInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM otherinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM otherinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid", "PM");

		// Token: 0x04000394 RID: 916
		internal static string QD_PCOther = string.Format("--INSERT INTO archive_OtherInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM otherinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM otherinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid", "PC");

		// Token: 0x04000395 RID: 917
		internal static string QD_WlOther = string.Format("--INSERT INTO archive_OtherInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM otherinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM otherinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid", "WL");

		// Token: 0x04000396 RID: 918
		internal const string QD_DateTime_Base_StudentOnly = "--INSERT INTO archive_DateTimeInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@cid,controlvalue,getdate(),@whoami FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid;\r\nDELETE FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid";

		// Token: 0x04000397 RID: 919
		internal const string QD_DateTime_Base_StudentAndAppointment = "--INSERT INTO archive_DateTimeInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM datetimeinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM datetimeinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid";

		// Token: 0x04000398 RID: 920
		internal static string QD_SurveyDateTime = string.Format("--INSERT INTO archive_DateTimeInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM datetimeinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM datetimeinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid", "Survey").Replace("appointmentid", "people_surveyId");

		// Token: 0x04000399 RID: 921
		internal static string QD_OnlineFormDateTime = string.Format("--INSERT INTO archive_DateTimeInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM datetimeinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM datetimeinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid", "OnlineForm").Replace("appointmentid", "people_onlineformId");

		// Token: 0x0400039A RID: 922
		internal static string QD_PSDateTime = string.Format("--INSERT INTO archive_DateTimeInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@cid,controlvalue,getdate(),@whoami FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid;\r\nDELETE FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid", "PS");

		// Token: 0x0400039B RID: 923
		internal static string QD_PADateTime = string.Format("--INSERT INTO archive_DateTimeInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM datetimeinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM datetimeinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid", "PA");

		// Token: 0x0400039C RID: 924
		internal static string QD_ANDateTime = string.Format("--INSERT INTO archive_DateTimeInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@cid,controlvalue,getdate(),@whoami FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid;\r\nDELETE FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid", "AN");

		// Token: 0x0400039D RID: 925
		internal static string QD_PIDateTime = string.Format("--INSERT INTO archive_DateTimeInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@cid,controlvalue,getdate(),@whoami FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid;\r\nDELETE FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid", "PI");

		// Token: 0x0400039E RID: 926
		internal static string QD_InvDateTime = string.Format("--INSERT INTO archive_DateTimeInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@cid,controlvalue,getdate(),@whoami FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid;\r\nDELETE FROM datetimeinfo{0} WHERE personid=@pid AND controlid=@cid", "Inv");

		// Token: 0x0400039F RID: 927
		internal static string QD_AccommodationTemplateOnlyDateTime = string.Format("--INSERT INTO archive_DateTimeInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM datetimeinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM datetimeinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid".Replace("INSERT INTO archive_DateTimeInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)", "").Replace("@appid", "0").Replace("appointmentid", "courseid"), "accommodationps");

		// Token: 0x040003A0 RID: 928
		internal static string QD_AccommodationDateTime = string.Format("--INSERT INTO archive_DateTimeInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM datetimeinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM datetimeinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid".Replace("INSERT INTO archive_DateTimeInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)", "").Replace("appointmentid", "courseid"), "accommodationps");

		// Token: 0x040003A1 RID: 929
		internal static string QD_PMDateTime = string.Format("--INSERT INTO archive_DateTimeInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM datetimeinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM datetimeinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid", "PM");

		// Token: 0x040003A2 RID: 930
		internal static string QD_PCDateTime = string.Format("--INSERT INTO archive_DateTimeInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM datetimeinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM datetimeinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid", "PC");

		// Token: 0x040003A3 RID: 931
		internal static string QD_WlDateTime = string.Format("--INSERT INTO archive_DateTimeInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM datetimeinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM datetimeinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid", "WL");

		// Token: 0x040003A4 RID: 932
		internal const string QD_Image_Base_StudentOnly = "--INSERT INTO archive_ImageInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@cid,controlvalue,getdate(),@whoami FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid;\r\nDELETE FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid";

		// Token: 0x040003A5 RID: 933
		internal const string QD_Image_Base_StudentAndAppointment = "--INSERT INTO archive_ImageInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM imageinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM imageinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid";

		// Token: 0x040003A6 RID: 934
		internal static string QD_SurveyImage = string.Format("--INSERT INTO archive_ImageInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@cid,controlvalue,getdate(),@whoami FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid;\r\nDELETE FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid", "Survey").Replace("appointmentid", "people_surveyId");

		// Token: 0x040003A7 RID: 935
		internal static string QD_OnlineFormImage = string.Format("--INSERT INTO archive_ImageInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@cid,controlvalue,getdate(),@whoami FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid;\r\nDELETE FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid", "OnlineForm").Replace("appointmentid", "people_onlineformId");

		// Token: 0x040003A8 RID: 936
		internal static string QD_PSImage = string.Format("--INSERT INTO archive_ImageInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@cid,controlvalue,getdate(),@whoami FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid;\r\nDELETE FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid", "PS");

		// Token: 0x040003A9 RID: 937
		internal static string QD_PAImage = string.Format("--INSERT INTO archive_ImageInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM imageinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM imageinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid", "PA");

		// Token: 0x040003AA RID: 938
		internal static string QD_ANImage = string.Format("--INSERT INTO archive_ImageInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@cid,controlvalue,getdate(),@whoami FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid;\r\nDELETE FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid", "AN");

		// Token: 0x040003AB RID: 939
		internal static string QD_PIImage = string.Format("--INSERT INTO archive_ImageInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@cid,controlvalue,getdate(),@whoami FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid;\r\nDELETE FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid", "PI");

		// Token: 0x040003AC RID: 940
		internal static string QD_InvImage = string.Format("--INSERT INTO archive_ImageInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@cid,controlvalue,getdate(),@whoami FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid;\r\nDELETE FROM imageinfo{0} WHERE personid=@pid AND controlid=@cid", "Inv");

		// Token: 0x040003AD RID: 941
		internal static string QD_AccommodationTemplateOnlyImage = string.Format("--INSERT INTO archive_ImageInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM imageinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM imageinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid".Replace("INSERT INTO archive_ImageInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)", "").Replace("@appid", "0").Replace("appointmentid", "courseid"), "accommodationps");

		// Token: 0x040003AE RID: 942
		internal static string QD_AccommodationImage = string.Format("--INSERT INTO archive_ImageInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM imageinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM imageinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid".Replace("INSERT INTO archive_ImageInfo{0} (personid,controlid,controlvalue,dateentered,whoentered)", "").Replace("appointmentid", "courseid"), "accommodationps");

		// Token: 0x040003AF RID: 943
		internal static string QD_PMImage = string.Format("--INSERT INTO archive_ImageInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM imageinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM imageinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid", "PM");

		// Token: 0x040003B0 RID: 944
		internal static string QD_PCImage = string.Format("--INSERT INTO archive_ImageInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM imageinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM imageinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid", "PC");

		// Token: 0x040003B1 RID: 945
		internal static string QD_WlImage = string.Format("--INSERT INTO archive_ImageInfo{0} (personid,appointmentid,controlid,controlvalue,dateentered,whoentered)\r\nSELECT @pid,@appid,@cid,controlvalue,getdate(),@whoami FROM imageinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid;\r\nDELETE FROM imageinfo{0} WHERE personid=@pid AND appointmentid=@appid AND controlid=@cid", "WL");

		// Token: 0x040003B2 RID: 946
		internal const string QD_APPOINTMENT_ICONS_NO_LONGER_NEEDED_AFTER_PA_DATA_CHANGE = "DELETE FROM appointmenticons \r\nWHERE   screennum=@screennum \r\n        AND appointmentid IN \r\n            (SELECT app.appointmentid \r\n             FROM attendees att LEFT JOIN appointments app ON app.appointmentid=att.appointmentid \r\n             WHERE att.personid=@personid)";

		// Token: 0x040003B3 RID: 947
		internal const string QI_APPOINTMENT_ICON_FOR_PA_DATA_CHANGE = "INSERT INTO appointmenticons (appointmentid,screennum,iconnum) VALUES (@appointmentid,@screennum,@iconnum)";

		// Token: 0x040003B4 RID: 948
		internal const string QI_File = "INSERT INTO files (filebytes,filename,filetypecode,isencrypted,iscompressed,dateuploaded,whouploaded)\r\nVALUES (@filebytes,@filename,@filetypecode,@isencrypted,@iscompressed,getdate(),@whouploaded);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS fileid";

		// Token: 0x040003B5 RID: 949
		internal const string QI_CREATE_PER_DATE_ENTRY = "INSERT INTO infopm (dateentered,whoentered,personid,description,screennum) VALUES (@dateentered,@whoentered,@personid,@description,@screennum);\r\nSELECT TOP 1 CAST(SCOPE_IDENTITY() AS int) AS appointmentid";

		// Token: 0x040003B6 RID: 950
		internal const string QI_COPY_PERSTUDENT_DATA_TO_PERDATE_FORM = "INSERT INTO maininfopm (appointmentid,personid,controlid,controlvalue) \r\n    SELECT @appid,@pid,controlid,controlvalue FROM maininfops WHERE personid=@pid AND controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum);\r\nINSERT INTO otherinfopm (appointmentid,personid,controlid,controlvalue) \r\n    SELECT @appid,@pid,controlid,controlvalue FROM otherinfops WHERE personid=@pid AND controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum);\r\nINSERT INTO imageinfopm (appointmentid,personid,controlid,controlvalue) \r\n    SELECT @appid,@pid,controlid,controlvalue FROM imageinfops WHERE personid=@pid AND controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum);\r\nINSERT INTO datetimeinfopm (appointmentid,personid,controlid,controlvalue) \r\n    SELECT @appid,@pid,controlid,controlvalue FROM datetimeinfops WHERE personid=@pid AND controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum);";
	}
}
