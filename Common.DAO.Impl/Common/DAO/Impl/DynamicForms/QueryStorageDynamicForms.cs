using System;

namespace TechnoPro.Common.DAO.Impl.DynamicForms
{
	// Token: 0x020000E4 RID: 228
	public class QueryStorageDynamicForms
	{
		// Token: 0x040003CD RID: 973
		internal const string QS_CIDS_WITH_SCREENNUMS = "SELECT    DISTINCT x.orderid AS controlid,dsc.screennum,dsc.ordernum\r\nFROM        splitorderids(@cids,',') x LEFT JOIN dynamicscreencontrols dsc ON dsc.controlid=x.OrderID\r\nORDER BY dsc.ordernum";

		// Token: 0x040003CE RID: 974
		internal const string QS_CIDS_BY_SCREENNUM_NO_DATA_HOLDING_CONTROLS = "SELECT\tdsc.controlid \r\nFROM\tDynamicScreenControls dsc LEFT JOIN dynamiccontrols dc ON dc.ControlID=dsc.controlID \r\nWHERE\tdsc.screenNum=@screennum AND NOT dc.controlcode IN (SELECT controlcode FROM DynamicScreenNonDataControls)\r\nORDER BY dsc.orderNum";

		// Token: 0x040003CF RID: 975
		internal const string QS_CIDS_BY_SCREENNUM_WITH_DATA_HOLDING_CONTROLS = "SELECT dsc.controlid FROM DynamicScreenControls dsc WHERE dsc.screenNum=@screennum ORDER BY dsc.orderNum";

		// Token: 0x040003D0 RID: 976
		internal const string QS_FORMS_A_CONTROL_EXISTS_ON = "SELECT DISTINCT screennum FROM dynamicscreencontrols WHERE controlid=@cid";

		// Token: 0x040003D1 RID: 977
		internal const string QS_ALL_DYNAMIC_FORMS = "SELECT    s.screennum,s.typecode,s.description,s.shorttext,s.bottomless,s.columnwidth,\r\n            s.longdescription,s.showasbutton,s.iconindex,s.largeiconindex,s.isactive,s.screenuniqueid\r\nFROM        screens s \r\nORDER BY s.screennum";

		// Token: 0x040003D2 RID: 978
		internal const string QS_ACTIVE_DYNAMIC_FORMS_BY_FORM_TYPE = "SELECT    s.screennum,s.typecode,s.description,s.shorttext,s.bottomless,s.columnwidth,\r\n            s.longdescription,s.showasbutton,s.iconindex,s.largeiconindex,s.isactive,s.screenuniqueid\r\nFROM        screens s \r\nWHERE       s.isactive=1 AND s.typecode=@formtype\r\nORDER BY s.screennum";

		// Token: 0x040003D3 RID: 979
		internal const string QS_DYNAMICFORMS_BY_SEARCH_STRING_ON_TITLE = "SELECT    screennum,description,typecode,bottomless,verticalcontrolpad,columnwidth,columnpad,dateadded,\r\n            datemodified,isactive,iconindex,largeiconindex,shorttext,studentnamenumeditable,screenid,\r\n            showasbutton,fontname,fontsize,groupids,iswebscreen,longdescription,controlIdToActivate,\r\n            studentnumbercaption,studentnumberautogeneraterule,studentnamehidden,screenuniqueid\r\nFROM screens \r\nWHERE \r\n    (\r\n        (\r\n            @useprimary=1 AND description LIKE @searchstring\r\n        )\r\n        OR\r\n        (\r\n            @usesecondary=1 AND shorttext LIKE @searchstring\r\n        )\r\n    )\r\nORDER BY screennum";

		// Token: 0x040003D4 RID: 980
		internal const string QS_DYNAMICFORMS_WITH_EXTENDED_INFO_BY_SCREEN_NUMS = "SELECT    screennum,description,typecode,bottomless,verticalcontrolpad,columnwidth,columnpad,dateadded,\r\n            datemodified,isactive,iconindex,largeiconindex,shorttext,studentnamenumeditable,screenid,\r\n            showasbutton,fontname,fontsize,groupids,iswebscreen,longdescription,controlIdToActivate,\r\n            studentnumbercaption,studentnumberautogeneraterule,studentnamehidden,screenuniqueid\r\nFROM screens \r\nWHERE   (@screennums='' OR screennum IN (SELECT orderid AS screennum FROM splitorderids(@screennums,',')))\r\nORDER BY screennum";

		// Token: 0x040003D5 RID: 981
		internal const string QS_ACCOMMODATION_DATA_BY_FORM_AND_STUDENT_AND_COURSEORTEMPLATE = "SELECT    ps.personid,ps.controlid,ps.valtext,ps.valbytes,ps.valimage,ps.valint,ps.valdate,ps.valbytesisencrypted\r\n            ,dc.controlid,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.controlname,dc.controlgroup\r\n            ,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers\r\n            ,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,p.firstname,p.lastname,p.student_no,p.middlename,dc.uniqueid,dc.specialcontroltype\r\nFROM        accommodationdata ps LEFT JOIN people p ON p.personid=ps.personid\r\n            LEFT JOIN lucourses luc ON luc.lucourseid=ps.courseid\r\n            LEFT JOIN dynamiccontrols dc ON dc.controlid=ps.controlid\r\nWHERE       ps.personid=@pid AND ps.courseid=@courseid\r\n            AND ps.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum)";

		// Token: 0x040003D6 RID: 982
		internal const string QS_PS_DATA_BY_FORM_AND_STUDENT = "SELECT    ps.personid,ps.controlid,ps.valtext,ps.valbytes,ps.valimage,ps.valint,ps.valdate,ps.valbytesisencrypted\r\n            ,dc.controlid,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.controlname,dc.controlgroup\r\n            ,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers\r\n            ,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,p.firstname,p.lastname,p.student_no,p.middlename,dc.uniqueid,dc.specialcontroltype\r\nFROM        perstudentdata2 ps LEFT JOIN people p ON p.personid=ps.personid\r\n            LEFT JOIN dynamiccontrols dc ON dc.controlid=ps.controlid\r\nWHERE       ps.personid=@pid\r\n            AND ps.controlid IN (SELECT controlid FROM dynamicscreencontrols WHERE screennum=@screennum)";

		// Token: 0x040003D7 RID: 983
		internal const string QS_PS_DATA_BY_FIELDS_AND_STUDENT = "SELECT    ps.personid,ps.controlid,ps.valtext,ps.valbytes,ps.valimage,ps.valint,ps.valdate,ps.valbytesisencrypted\r\n            ,dc.controlid,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.controlname,dc.controlgroup\r\n            ,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers\r\n            ,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline\r\n            ,p.firstname,p.lastname,p.student_no,p.middlename,dc.uniqueid,dc.specialcontroltype\r\nFROM        perstudentdata2 ps LEFT JOIN people p ON p.personid=ps.personid\r\n            LEFT JOIN dynamiccontrols dc ON dc.controlid=ps.controlid\r\nWHERE       ps.personid=@pid\r\n            AND ps.controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','))";

		// Token: 0x040003D8 RID: 984
		internal const string QS_EMAIL_CID = "SELECT    sg.settingvalue \r\n            ,dc.controlid,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4\r\n            ,dc.setting4string,dc.defaultvalue,dc.defaultvaluestring,dc.controlname,dc.controlgroup\r\n            ,dc.helptext,dc.helptextdisplaymethod,dc.mask,dc.enforce,dc.actionhandlers\r\n            ,dc.enabled,dc.readonly,dc.hidecaption,dc.fontsize,dc.dontwraptonextline,dc.uniqueid,dc.specialcontroltype\r\nFROM        settingsgroups sg LEFT JOIN dynamiccontrols dc ON dc.controlid=sg.settingvalue\r\nWHERE       sg.groupid=-1 AND sg.settingcode=259 OR sg.settingcode=260";

		// Token: 0x040003D9 RID: 985
		internal const string QS_DYNAMIC_FORMS_BY_ID = "SELECT    s.screennum,s.typecode,s.description,s.shorttext,s.bottomless,s.columnwidth,s.screenuniqueid\r\nFROM        screens s \r\nWHERE       s.screennum IN (SELECT orderid AS screennum FROM splitorderids(@screennums,','))";

		// Token: 0x040003DA RID: 986
		internal const string QS_DYNAMIC_FORM_UNIQUEIDS_BY_SCREENNUMS = "SELECT screennum,screenuniqueid FROM screens WHERE screennum IN (SELECT orderid AS screennum FROM splitorderids(@screennums,',')) ORDER BY screennum";

		// Token: 0x040003DB RID: 987
		internal const string QS_DYNAMIC_FORM_BY_UNIQUEID = "SELECT    s.screennum,s.typecode,s.description,s.shorttext,s.bottomless,s.columnwidth,s.screenuniqueid\r\nFROM        screens s \r\nWHERE       s.screenuniqueid=@uniqueid";

		// Token: 0x040003DC RID: 988
		internal const string QS_LOAD_CONTROLS = "SELECT    dc.controlid,-1 AS screennum,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue,\r\n            dc.ControlName,dc.ControlGroup,dc.HelpText,dc.HelpTextDisplayMethod,dc.Mask,dc.Enforce,dc.ActionHandlers,dc.DefaultValueString,\r\n            dc.Setting4String,dc.enabled,dc.readonly,dc.hidecaption,dc.setting4,dc.fontsize,dc.dontwraptonextline,dc.uniqueid,dc.specialcontroltype\r\nFROM dynamiccontrols dc ORDER BY dc.controlcaption";

		// Token: 0x040003DD RID: 989
		internal const string QS_LOAD_DYNAMIC_SCREEN_CONTROLS_DATA = "SELECT dynamicscreencontrolid,screennum,controlid,ordernum,isactive FROM dynamicscreencontrols ORDER BY screennum,ordernum";

		// Token: 0x040003DE RID: 990
		internal const string QS_ALL_DYNAMIC_FORM_COLUMNS_BY_SCREENNUM = "SELECT * FROM screens WHERE screennum=@screennum";

		// Token: 0x040003DF RID: 991
		internal const string QS_ALL_LOOKUP_GROUP_INFO_BY_LOOKUPGROUPID = "SELECT * FROM lookupgroups WHERE lookupgroupid=@id";

		// Token: 0x040003E0 RID: 992
		internal const string QS_ALL_LOOKUP_ITEMS_INFO_BY_LOOKUPGROUPID = "SELECT * FROM lookuplists WHERE lookupgroupid=@id";

		// Token: 0x040003E1 RID: 993
		internal const string QS_ALL_LOOKUP_GROUP_INFO_BY_TITLE = "SELECT * FROM lookupgroups WHERE description=@description";

		// Token: 0x040003E2 RID: 994
		internal const string QI_ADD_LOOKUPGROUPITEM = "INSERT INTO lookupgroups (description,sortby) VALUES (@description,@sortby); SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int) AS lookupgroupid";

		// Token: 0x040003E3 RID: 995
		internal const string QI_ADD_LOOKUPITEM = "INSERT INTO lookuplists (lookupgroupid,lookuptext,ordernum,lookupvalue,visible) SELECT @lgi,@LT AS lookuptext,@ordernum AS ordernum,@lookupvalue AS lookupvalue,@visible AS visible WHERE NOT EXISTS(SELECT lookuplistid FROM lookuplists WHERE lookupgroupid=@lgi AND lookuptext=@lt)";

		// Token: 0x040003E4 RID: 996
		internal const string QI_ADD_DYNAMIC_CONTROL = "INSERT INTO dynamiccontrols \r\n    (controlcode,controlcaption,setting1,setting2,setting3,defaultvalue,ControlName,ControlGroup,HelpText,HelpTextDisplayMethod,Mask,\r\n     Enforce,ActionHandlers,DefaultValueString,Setting4String,enabled,readonly,hidecaption,setting4,fontsize,dontwraptonextline,uniqueid,specialcontroltype)\r\nVALUES \r\n    (@controlcode,@controlcaption,@setting1,@setting2,@setting3,@defaultvalue,@ControlName,@ControlGroup,@HelpText,@HelpTextDisplayMethod,@Mask,@Enforce,\r\n     @ActionHandlers,@DefaultValueString,@Setting4String,@enabled,@readonly,@hidecaption,@setting4,@fontsize,@dontwraptonextline,@uniqueid,@specialcontroltype);\r\nSELECT TOP 1 CAST(SCOPE_IDENTITY() AS int) AS controlid";

		// Token: 0x040003E5 RID: 997
		internal const string QI_ADD_DYNAMIC_SCREEN_CONTROL_ENTRY = "INSERT INTO dynamicscreencontrols (screennum,controlid,ordernum,isactive) VALUES (@screennum,@controlid,@ordernum,@isactive)";

		// Token: 0x040003E6 RID: 998
		internal const string QI_CREATE_DYNAMIC_FORM = "INSERT INTO screens (screennum,dateadded,datemodified,description,typecode,bottomless,verticalcontrolpad,columnwidth,columnpad,isactive,iconindex,largeiconindex,shorttext,\r\n    studentnamenumeditable,showasbutton,fontname,fontsize,groupids,controlidtoactivate,studentnumbercaption,studentnamehidden,studentnumberautogeneraterule,longdescription,screenuniqueid)\r\nVALUES (0,getdate(),getdate(),@description,@typecode,@bottomless,@verticalcontrolpad,@columnwidth,@columnpad,@isactive,@iconindex,@largeiconindex,@shorttext,\r\n    @studentnamenumeditable,@showasbutton,@fontname,@fontsize,@groupids,@controlidtoactivate,@studentnumbercaption,@studentnamehidden,@studentnumberautogeneraterule,@longdescription,@screenuniqueid);\r\nDECLARE @sn int\r\nSET @sn = (SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int) As screennum);\r\nUPDATE screens SET screennum=screenid WHERE screenid=@sn;\r\nSELECT @sn";

		// Token: 0x040003E7 RID: 999
		internal const string QD_DELETE_DYNAMIC_FORM = "DELETE FROM screens WHERE screennum=@screennum AND NOT screennum IN (SELECT screennum FROM dynamicscreencontrols WHERE screennum=@screennum);\r\nSELECT screennum FROM screens WHERE screennum=@screennum";

		// Token: 0x040003E8 RID: 1000
		internal const string QU_DYNAMIC_FORM = "UPDATE screens SET datemodified=getdate(),description=@description,typecode=@typecode,bottomless=@bottomless,verticalcontrolpad=@verticalcontrolpad,columnwidth=@columnwidth,\r\n    columnpad=@columnpad,isactive=@isactive,iconindex=@iconindex,largeiconindex=@largeiconindex,shorttext=@shorttext,studentnamenumeditable=@studentnamenumeditable,\r\n    showasbutton=@showasbutton,fontname=@fontname,fontsize=@fontsize,groupids=@groupids,controlidtoactivate=@controlidtoactivate,studentnumbercaption=@studentnumbercaption,\r\n    studentnamehidden=@studentnamehidden,studentnumberautogeneraterule=@studentnumberautogeneraterule,longdescription=@longdescription\r\nWHERE screennum=@screennum";
	}
}
