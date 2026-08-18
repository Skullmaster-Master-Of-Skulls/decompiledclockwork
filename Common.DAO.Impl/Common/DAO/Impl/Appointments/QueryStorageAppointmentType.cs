using System;

namespace TechnoPro.Common.DAO.Impl.Appointments
{
	// Token: 0x02000130 RID: 304
	public class QueryStorageAppointmentType
	{
		// Token: 0x04000502 RID: 1282
		internal const string QS_PERAPPSCREENNUMS_BY_APPTYPEID = "SELECT perappscreennumsfortabs FROM appointmenttypes WHERE apptypeid=@apptypeid";

		// Token: 0x04000503 RID: 1283
		internal const string QS_ORPHAN_APP_TYPES = "SELECT at.apptypeid,at.description AS apptypedescription,at.defaultcolour,at.isworkshop,at.iscourse\r\n        ,-1 AS appointmenttypegroupid,CAST(NULL AS varchar(max)) AS apptypegrouptitle,CAST(NULL AS varchar(max)) AS gidstr,at.isactive AS apptypeisactive\r\nFROM    appointmenttypes at \r\nWHERE   at.appointmentTypeGroupID IS NULL OR at.appointmentTypeGroupID<1\r\nORDER BY at.description";

		// Token: 0x04000504 RID: 1284
		internal const string QS_ALL_APP_TYPES = "SELECT at.apptypeid,at.description AS apptypedescription,at.defaultcolour,at.isworkshop,at.iscourse\r\n        ,at.appointmenttypegroupid,atg.title AS apptypegrouptitle,atg.[description] AS gidstr\r\nFROM    appointmenttypes at LEFT JOIN appointmenttypegroups atg ON atg.appointmenttypegroupid=at.appointmenttypegroupid\r\nWHERE at.isactive=1\r\nORDER BY atg.title,at.description";

		// Token: 0x04000505 RID: 1285
		internal const string QS_ALL_INACTIVE_APP_TYPES = "SELECT at.apptypeid,at.description AS apptypedescription,at.defaultcolour,at.isworkshop,at.iscourse\r\n        ,at.appointmenttypegroupid,atg.title AS apptypegrouptitle,atg.[description] AS gidstr\r\nFROM    appointmenttypes at LEFT JOIN appointmenttypegroups atg ON atg.appointmenttypegroupid=at.appointmenttypegroupid\r\nWHERE at.isactive=0\r\nORDER BY atg.title,at.description";

		// Token: 0x04000506 RID: 1286
		internal const string QS_APP_TYPE_BY_ID = "SELECT at.apptypeid,at.description AS apptypedescription,at.defaultcolour,at.isworkshop,at.iscourse\r\n        ,at.appointmenttypegroupid,atg.title AS apptypegrouptitle,atg.[description] AS gidstr\r\nFROM    appointmenttypes at LEFT JOIN appointmenttypegroups atg ON atg.appointmenttypegroupid=at.appointmenttypegroupid\r\nWHERE at.isactive=1 AND at.apptypeid=@apptypeid";

		// Token: 0x04000507 RID: 1287
		internal const string QS_APP_TYPE_BY_APPOINTMENT_ID = "SELECT at.apptypeid,at.description AS apptypedescription,at.defaultcolour,at.isworkshop,at.iscourse\r\n        ,at.appointmenttypegroupid,atg.title AS apptypegrouptitle,atg.[description] AS gidstr\r\nFROM    appointments a LEFT JOIN appointmenttypes at ON at.apptypeid=a.apptypeid LEFT JOIN appointmenttypegroups atg ON atg.appointmenttypegroupid=at.appointmenttypegroupid\r\nWHERE a.appointmentid=@appid AND at.isactive=1";

		// Token: 0x04000508 RID: 1288
		internal const string QS_APP_TYPE_EXTENDED_BY_ID = "SELECT\tat.apptypeid,at.[description] AS apptypedescription,at.defaultcolour,at.isworkshop,at.iscourse,\r\n\t\tat.appointmenttypegroupid,atg.title AS apptypegrouptitle,atg.[description] AS gidstr,\r\n\t\tat.isbackground,at.defaultOverrideColour,at.defaultIcon,at.ShowInHighlights,\r\n\t\tat.perAppScreenNumsForTabs,at.perJustAppScreenNum,at.iconindex,at.longdescription AS clientGroupIds,at.isactive AS apptypeisactive,\r\n        at.requiresroom\r\nFROM    appointmenttypes at LEFT JOIN appointmenttypegroups atg ON atg.appointmenttypegroupid=at.appointmenttypegroupid\r\nWHERE\tat.apptypeid=@apptypeid";

		// Token: 0x04000509 RID: 1289
		internal const string QS_APPOINTMENT_TYPE_GROUPS_ALL_WITH_APP_TYPES = "SELECT at.apptypeid,at.[description] AS apptypedescription,at.defaultcolour,at.isworkshop,at.iscourse\r\n        ,atg.appointmenttypegroupid,atg.title AS apptypegrouptitle,at.isactive AS apptypeisactive,atg.[description] AS gidstr\r\nFROM    appointmenttypegroups atg LEFT JOIN appointmenttypes at ON at.appointmenttypegroupid=atg.appointmenttypegroupid\r\nWHERE at.isactive IS NULL OR at.isactive=1\r\nORDER BY atg.title,at.description";

		// Token: 0x0400050A RID: 1290
		internal const string QS_APPOINTMENT_TYPE_GROUPS_ALL_WITH_APP_TYPES_ACTIVE_OR_INACTIVE = "SELECT at.apptypeid,at.[description] AS apptypedescription,at.defaultcolour,at.isworkshop,at.iscourse\r\n        ,atg.appointmenttypegroupid,atg.title AS apptypegrouptitle,at.isactive AS apptypeisactive,atg.[description] AS gidstr\r\nFROM    appointmenttypegroups atg LEFT JOIN appointmenttypes at ON at.appointmenttypegroupid=atg.appointmenttypegroupid\r\nORDER BY atg.title,at.description";

		// Token: 0x0400050B RID: 1291
		internal const string QS_APPOINTMENT_TYPE_GROUP_BY_ID_ALL_WITH_APP_TYPES = "SELECT at.apptypeid,at.[description] AS apptypedescription,at.defaultcolour,at.isworkshop,at.iscourse\r\n        ,atg.appointmenttypegroupid,atg.title AS apptypegrouptitle,at.isactive AS apptypeisactive,atg.[description] AS gidstr\r\nFROM    appointmenttypegroups atg LEFT JOIN appointmenttypes at ON at.appointmenttypegroupid=atg.appointmenttypegroupid\r\nWHERE   (@includeinactiveapptypes=1 OR (at.isactive IS NULL OR at.isactive=1))\r\n        AND atg.appointmenttypegroupid=@appointmenttypegroupid\r\nORDER BY atg.title,at.description";

		// Token: 0x0400050C RID: 1292
		internal const string QS_APPOINTMENT_TYPE_GROUP_BY_ID = "SELECT atg.appointmenttypegroupid,atg.title AS apptypegrouptitle,atg.[description] AS gidstr\r\nFROM    appointmenttypegroups atg\r\nWHERE atg.appointmenttypegroupid=@appointmenttypegroupid";

		// Token: 0x0400050D RID: 1293
		internal const string QD_APPOINTMENT_TYPE_GROUP = "DELETE FROM appointmenttypegroups WHERE NOT appointmenttypegroupid IN (SELECT appointmenttypegroupid FROM appointmenttypes) AND appointmenttypegroupid=@appointmenttypegroupid";

		// Token: 0x0400050E RID: 1294
		internal const string QD_DISABLE_APP_TYPE = "UPDATE appointmenttypes SET isactive=0 WHERE apptypeid=@apptypeid";

		// Token: 0x0400050F RID: 1295
		internal const string QD_APP_TYPE = "IF @apptypeidtoreplace > 0 \r\n    UPDATE appointments SET apptypeid=@apptypeidtokeep WHERE apptypeid=@apptypeidtoreplace\r\nDELETE FROM appointmenttypes WHERE apptypeid=@apptypeidtoreplace AND NOT apptypeid IN (SELECT apptypeid FROM appointments)";

		// Token: 0x04000510 RID: 1296
		internal const string QI_APP_TYPE_GROUP = "INSERT INTO appointmenttypegroups (title,description) VALUES (@title,''); SET @appointmenttypegroupid=SCOPE_IDENTITY();";

		// Token: 0x04000511 RID: 1297
		internal const string QI_APP_TYPE = "INSERT INTO appointmenttypes \r\n    (description,defaultcolour,isworkshop,iscourse,appointmenttypegroupid,defaulticon)\r\nVALUES (@description,@defaultcolour,@isworkshop,@iscourse,@appointmenttypegroupid,NULL);\r\nSET @apptypeid=SCOPE_IDENTITY()";

		// Token: 0x04000512 RID: 1298
		internal const string QI_APP_TYPE_EXTENDED = "INSERT INTO appointmenttypes \r\n    ([description],defaultcolour,isworkshop,iscourse,appointmenttypegroupid,isbackground,defaultoverridecolour,defaulticon,\r\n\tshowinhighlights,perappscreennumsfortabs,perjustappscreennum,iconindex,longdescription,isactive,requiresroom)\r\nVALUES (@description,@defaultcolour,@isworkshop,@iscourse,@appointmenttypegroupid,@isbackground,@defaultoverridecolour,@defaulticon,\r\n\t@showinhighlights,@perappscreennumsfortabs,@perjustappscreennum,@iconindex,@longdescription,@isactive,@requiresroom);\r\nSET @apptypeid=SCOPE_IDENTITY()";

		// Token: 0x04000513 RID: 1299
		internal const string QU_APP_TYPE_GROUP = "UPDATE appointmenttypegroups SET title=@title,description=@gidstr WHERE appointmenttypegroupid=@appointmenttypegroupid";

		// Token: 0x04000514 RID: 1300
		internal const string QU_APP_TYPE = "UPDATE appointmenttypes SET \r\n    description=@description,defaultcolour=@defaultcolour,isworkshop=@isworkshop,\r\n    iscourse=@iscourse,appointmenttypegroupid=@appointmenttypegroupid\r\nWHERE apptypeid=@apptypeid";

		// Token: 0x04000515 RID: 1301
		internal const string QU_APP_TYPE_EXTENDED = "UPDATE appointmenttypes SET \r\n    [description]=@description,defaultcolour=@defaultcolour,isworkshop=@isworkshop,\r\n    iscourse=@iscourse,appointmenttypegroupid=@appointmenttypegroupid,\r\n    isbackground=@isbackground,defaultoverridecolour=@defaultoverridecolour,defaulticon=@defaulticon,\r\n    showinhighlights=@showinhighlights,perappscreennumsfortabs=@perappscreennumsfortabs,\r\n    perjustappscreennum=@perjustappscreennum,iconindex=@iconindex,longdescription=@longdescription,\r\n    isactive=@isactive,requiresroom=@requiresroom\r\nWHERE apptypeid=@apptypeid";
	}
}
