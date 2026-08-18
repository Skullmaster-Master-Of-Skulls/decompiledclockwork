using System;

namespace TechnoPro.Common.DAO.Impl.AppointmentSync
{
	// Token: 0x0200013E RID: 318
	public class QueryStorageAppointmentMappings
	{
		// Token: 0x04000576 RID: 1398
		internal const string QS_DUPLICATE_MAPPINGS_ONE_EXTERNAL_MULTIPLE_CLOCKWORK = "select appm.UniqueId2, appm.ClockWorkAppointmentId AS appointmentid,appm.OutlookGlobalAppointmentId as UniqueId,appm.MasterRecurrenceAppointmentId,\r\nappm.OutlookLastModifiedDate as outlooklastupdateddate, appm.ClockWorkLastModifiedDate as clockworklastupdateddate,\r\nappidmap.OutlookGlobalAppointmentId as outlookglobalappointmentid\r\nfrom AppointmentMappingsClockWorkOutlook appm\r\nleft join appointments app on app.AppointmentID=appm.ClockWorkAppointmentId\r\nLEFT JOIN AppointmentMappingsClockWorkOutlookGlobalAppointmentId as appidmap on appm.OutlookGlobalAppointmentId COLLATE Latin1_General_CS_AS = appidmap.UniqueId\r\nwhere app.startDate >= @startdate AND app.startDate<=@enddate and (appm.UniqueId2 is not null and appm.UniqueId2 <> '')\r\nAND NOT appm.uniqueid2 IS NULL AND appm.uniqueid2 COLLATE Latin1_General_CS_AS IN \r\n    (select appm.uniqueid2\r\n    from AppointmentMappingsClockWorkOutlook appm\r\n    left join appointments app on app.appointmentid = appm.clockworkappointmentid\r\n    where uniqueid2 is not null and UniqueId2 <> '' and app.startDate >= GETDATE()\r\n    group by UniqueId2, app.startdate\r\n    having count(appm.ClockWorkAppointmentId) > 1)\r\norder by appm.UniqueId2, appm.ClockWorkAppointmentId";

		// Token: 0x04000577 RID: 1399
		internal const string QS_DUPLICATE_MAPPINGS_ONE_CLOCKWORK_MULTIPLE_EXTERNAL = "select appm.UniqueId2, appm.ClockWorkAppointmentId AS appointmentid,appm.OutlookGlobalAppointmentId as UniqueId,appm.MasterRecurrenceAppointmentId,\r\nappm.OutlookLastModifiedDate as outlooklastupdateddate, appm.ClockWorkLastModifiedDate as clockworklastupdateddate,\r\nappidmap.OutlookGlobalAppointmentId as outlookglobalappointmentid\r\nfrom AppointmentMappingsClockWorkOutlook appm\r\nleft join appointments app on app.AppointmentID=appm.ClockWorkAppointmentId\r\nLEFT JOIN AppointmentMappingsClockWorkOutlookGlobalAppointmentId as appidmap on appm.OutlookGlobalAppointmentId COLLATE Latin1_General_CS_AS = appidmap.UniqueId\r\nwhere app.startDate >= @startdate AND app.startDate<=@enddate and (appm.UniqueId2 is not null and appm.UniqueId2 <> '')\r\nAND appm.ClockWorkAppointmentId IN\r\n    (select appm.ClockWorkAppointmentId\r\n    from AppointmentMappingsClockWorkOutlook appm\r\n    left join appointments app on app.appointmentid = appm.clockworkappointmentid\r\n    where uniqueid2 is not null and app.startDate >= GETDATE()\r\n    group by ClockWorkAppointmentId, app.startdate\r\n    having count(uniqueid2) > 1)\r\norder by appm.ClockWorkAppointmentId,appm.UniqueId2";

		// Token: 0x04000578 RID: 1400
		internal const string QS_EXTERNAL_SYNC_MAPPING_WITH_NO_UNIQUEID2 = "select am.ClockWorkAppointmentId as appointmentid, am.OutlookGlobalAppointmentId as UniqueId, am.UniqueId2,am.MasterRecurrenceAppointmentId,\r\n\t   am.OutlookLastModifiedDate as outlooklastupdateddate, am.ClockWorkLastModifiedDate as clockworklastupdateddate,\r\n\t   appidmap.OutlookGlobalAppointmentId as outlookglobalappointmentid\r\nfrom AppointmentMappingsClockWorkOutlook am\r\ninner join Appointments a on a.AppointmentID = am.ClockWorkAppointmentId\r\nLEFT JOIN AppointmentMappingsClockWorkOutlookGlobalAppointmentId as appidmap\r\non am.OutlookGlobalAppointmentId COLLATE Latin1_General_CS_AS = appidmap.UniqueId\r\nwhere a.startDate >= DATEADD(DAY, -14, GETDATE()) and (am.UniqueId2 is NULL or am.UniqueId2 = '')";

		// Token: 0x04000579 RID: 1401
		internal const string QS_OUTLOOK_UNIQUE_ID_BY_GLOBAL_APPOINTMENT_ID = "SELECT uniqueid FROM AppointmentMappingsClockWorkOutlookGlobalAppointmentId WHERE OutlookGlobalAppointmentId COLLATE Latin1_General_CS_AS = @id";

		// Token: 0x0400057A RID: 1402
		internal const string QS_OUTLOOK_SYNC_MAPPING_BASE = "SELECT appmap.ClockWorkAppointmentId as appointmentid, appmap.OutlookGlobalAppointmentId as UniqueId, appmap.UniqueId2,appmap.MasterRecurrenceAppointmentId,\r\n\t   appmap.OutlookLastModifiedDate as outlooklastupdateddate, appmap.ClockWorkLastModifiedDate as clockworklastupdateddate,\r\n\t   appidmap.OutlookGlobalAppointmentId as outlookglobalappointmentid\r\nfrom AppointmentMappingsClockWorkOutlook as appmap\r\nLEFT JOIN AppointmentMappingsClockWorkOutlookGlobalAppointmentId as appidmap\r\non appmap.OutlookGlobalAppointmentId COLLATE Latin1_General_CS_AS = appidmap.UniqueId";

		// Token: 0x0400057B RID: 1403
		internal const string QS_OUTLOOK_SYNC_MAPPING_BY_CLOCKWORK_APPOINTMENT_ID = "SELECT appmap.ClockWorkAppointmentId as appointmentid, appmap.OutlookGlobalAppointmentId as UniqueId, appmap.UniqueId2,appmap.MasterRecurrenceAppointmentId,\r\n\t   appmap.OutlookLastModifiedDate as outlooklastupdateddate, appmap.ClockWorkLastModifiedDate as clockworklastupdateddate,\r\n\t   appidmap.OutlookGlobalAppointmentId as outlookglobalappointmentid\r\nfrom AppointmentMappingsClockWorkOutlook as appmap\r\nLEFT JOIN AppointmentMappingsClockWorkOutlookGlobalAppointmentId as appidmap\r\non appmap.OutlookGlobalAppointmentId COLLATE Latin1_General_CS_AS = appidmap.UniqueId WHERE appmap.ClockWorkAppointmentId = @clockworkappointmentid";

		// Token: 0x0400057C RID: 1404
		internal const string QS_EXTERNAL_SYNC_MAPPING_BY_UNIQUE_APPOINTMENT_ID = "SELECT appmap.ClockWorkAppointmentId as appointmentid, appmap.OutlookGlobalAppointmentId as UniqueId, appmap.UniqueId2,appmap.MasterRecurrenceAppointmentId,\r\n\t   appmap.OutlookLastModifiedDate as outlooklastupdateddate, appmap.ClockWorkLastModifiedDate as clockworklastupdateddate,\r\n\t   appidmap.OutlookGlobalAppointmentId as outlookglobalappointmentid\r\nfrom AppointmentMappingsClockWorkOutlook as appmap\r\nLEFT JOIN AppointmentMappingsClockWorkOutlookGlobalAppointmentId as appidmap\r\non appmap.OutlookGlobalAppointmentId COLLATE Latin1_General_CS_AS = appidmap.UniqueId WHERE appmap.OutlookGlobalAppointmentId COLLATE Latin1_General_CS_AS = @uniqueappointmentid";

		// Token: 0x0400057D RID: 1405
		internal const string QS_EXTERNAL_SYNC_MAPPING_BY_UNIQUE_APPOINTMENT_ID2 = "SELECT appmap.ClockWorkAppointmentId as appointmentid, appmap.OutlookGlobalAppointmentId as UniqueId, appmap.UniqueId2,appmap.MasterRecurrenceAppointmentId,\r\n\t   appmap.OutlookLastModifiedDate as outlooklastupdateddate, appmap.ClockWorkLastModifiedDate as clockworklastupdateddate,\r\n\t   appidmap.OutlookGlobalAppointmentId as outlookglobalappointmentid\r\nfrom AppointmentMappingsClockWorkOutlook as appmap\r\nLEFT JOIN AppointmentMappingsClockWorkOutlookGlobalAppointmentId as appidmap\r\non appmap.OutlookGlobalAppointmentId COLLATE Latin1_General_CS_AS = appidmap.UniqueId WHERE appmap.UniqueId2 COLLATE Latin1_General_CS_AS = @uniqueid2";

		// Token: 0x0400057E RID: 1406
		internal const string QS_EXTERNAL_SYNC_MAPPING_BY_MASTER_RECURRENCE_APPOINTMENT_ID = "SELECT appmap.ClockWorkAppointmentId as appointmentid, appmap.OutlookGlobalAppointmentId as UniqueId, appmap.UniqueId2,appmap.MasterRecurrenceAppointmentId,\r\n\t   appmap.OutlookLastModifiedDate as outlooklastupdateddate, appmap.ClockWorkLastModifiedDate as clockworklastupdateddate,\r\n\t   appidmap.OutlookGlobalAppointmentId as outlookglobalappointmentid\r\nfrom AppointmentMappingsClockWorkOutlook as appmap\r\nLEFT JOIN AppointmentMappingsClockWorkOutlookGlobalAppointmentId as appidmap\r\non appmap.OutlookGlobalAppointmentId COLLATE Latin1_General_CS_AS = appidmap.UniqueId WHERE appmap.MasterRecurrenceAppointmentId COLLATE Latin1_General_CS_AS = @masterrecurrenceappointmentid";

		// Token: 0x0400057F RID: 1407
		internal const string QS_OUTLOOK_SYNC_MAPPING_BY_OUTLOOK_GLOBAL_APPOINTMENT_ID = "SELECT appmap.ClockWorkAppointmentId as appointmentid, appmap.OutlookGlobalAppointmentId as UniqueId, appmap.UniqueId2,appmap.MasterRecurrenceAppointmentId,\r\n\t   appmap.OutlookLastModifiedDate as outlooklastupdateddate, appmap.ClockWorkLastModifiedDate as clockworklastupdateddate,\r\n\t   appidmap.OutlookGlobalAppointmentId as outlookglobalappointmentid\r\nfrom AppointmentMappingsClockWorkOutlook as appmap\r\nLEFT JOIN AppointmentMappingsClockWorkOutlookGlobalAppointmentId as appidmap\r\non appmap.OutlookGlobalAppointmentId COLLATE Latin1_General_CS_AS = appidmap.UniqueId  where appidmap.OutlookGlobalAppointmentId COLLATE Latin1_General_CS_AS = @outlookglobalappointmentid";

		// Token: 0x04000580 RID: 1408
		internal const string QI_OUTLOOK_SYNC_MAPPING = "IF NOT EXISTS(SELECT clockworkappointmentid FROM appointmentmappingsclockworkoutlook WHERE clockworkappointmentid=@clockworkappointmentid AND outlookglobalappointmentid COLLATE Latin1_General_CS_AS = @outlookglobalappointmentid)\r\nINSERT INTO AppointmentMappingsClockWorkOutlook (clockworkappointmentid,clockworklastmodifieddate,outlookglobalappointmentid,outlooklastmodifieddate,uniqueid2,MasterRecurrenceAppointmentId)\r\nVALUES (@clockworkappointmentid,@clockworklastmodifieddate,@outlookglobalappointmentid,@outlooklastmodifieddate,@uniqueid2,@masterrecurrenceappointmentid)";

		// Token: 0x04000581 RID: 1409
		internal const string QI_OUTLOOK_SYNC_MAPPING_LOOKUP = "INSERT INTO AppointmentMappingsClockWorkOutlookGlobalAppointmentId (uniqueid,OutlookGlobalAppointmentId) VALUES (@uniqueid,@globalappointmentid)";

		// Token: 0x04000582 RID: 1410
		internal const string QD_OUTLOOK_SYNC_MAPPING_LOOKUP = "DELETE FROM AppointmentMappingsClockWorkOutlookGlobalAppointmentId WHERE not uniqueid is NULL and @uniqueid <> '' and uniqueid COLLATE Latin1_General_CS_AS = @uniqueid AND not OutlookGlobalAppointmentId is NULL and @globalappointmentid <> '' and OutlookGlobalAppointmentId COLLATE Latin1_General_CS_AS = @globalappointmentid";

		// Token: 0x04000583 RID: 1411
		internal const string QD_OUTLOOK_SYNC_MAPPING = "DELETE FROM appointmentmappingsclockworkoutlook WHERE clockworkappointmentid=@clockworkappointmentid AND (( not uniqueid2 is null and @uniqueid2 <> '' and uniqueid2 COLLATE Latin1_General_CS_AS = @uniqueid2 ) or (not outlookglobalappointmentid is NULL and @outlookglobalappointmentid <> '' and  outlookglobalappointmentid COLLATE Latin1_General_CS_AS = @outlookglobalappointmentid))";

		// Token: 0x04000584 RID: 1412
		internal const string QU_UPDATE_MAPPING_LOOKUP_UNIQUE_ID = "UPDATE AppointmentMappingsClockWorkOutlookGlobalAppointmentId SET uniqueid=@newuniqueid WHERE uniqueid COLLATE Latin1_General_CS_AS = @olduniqueid";

		// Token: 0x04000585 RID: 1413
		internal const string QU_UPDATE_MAPPING_UNIQUE_ID = "UPDATE AppointmentMappingsClockWorkOutlook SET OutlookGlobalAppointmentId =@newuniqueid WHERE OutlookGlobalAppointmentId COLLATE Latin1_General_CS_AS = @olduniqueid";

		// Token: 0x04000586 RID: 1414
		internal const string QU_OUTLOOK_SYNC_MAPPING = "UPDATE appointmentmappingsclockworkoutlook SET clockworklastmodifieddate=@clockworklastmodifieddate,outlooklastmodifieddate=@outlooklastmodifieddate \r\nWHERE clockworkappointmentid=@clockworkappointmentid AND (( not uniqueid2 is null and @uniqueid2 <> '' and uniqueid2 COLLATE Latin1_General_CS_AS = @uniqueid2 ) or outlookglobalappointmentid COLLATE Latin1_General_CS_AS = @outlookglobalappointmentid)";

		// Token: 0x04000587 RID: 1415
		internal const string QU_OUTLOOK_SYNC_MAPPING_EXTERNAL_CHANGE = "if not exists (select uniqueid2 from appointmentmappingsclockworkoutlook where ClockWorkAppointmentId=@clockworkappointmentid and (UniqueId2 is not null or UniqueId2 <> ''))\r\nbegin\r\n UPDATE appointmentmappingsclockworkoutlook set UniqueId2=@uniqueid2 where ClockWorkAppointmentId=@clockworkappointmentid\r\nend\r\n\r\nUPDATE appointmentmappingsclockworkoutlook SET outlooklastmodifieddate=@outlooklastmodifieddate \r\nWHERE (( not uniqueid2 is null and @uniqueid2 <> '' and uniqueid2 COLLATE Latin1_General_CS_AS = @uniqueid2 ) or outlookglobalappointmentid COLLATE Latin1_General_CS_AS = @outlookglobalappointmentid)";

		// Token: 0x04000588 RID: 1416
		internal const string QU_OUTLOOK_SYNC_MAPPING_CLOCKWORK_CHANGE = "UPDATE appointmentmappingsclockworkoutlook SET clockworklastmodifieddate=@clockworklastmodifieddate\r\nWHERE clockworkappointmentid=@clockworkappointmentid";

		// Token: 0x04000589 RID: 1417
		internal const string QU_OUTLOOK_SYNC_MAPPING2 = "UPDATE appointmentmappingsclockworkoutlook SET UniqueId2=@uniqueid2 \r\nWHERE clockworkappointmentid=@clockworkappointmentid AND outlookglobalappointmentid COLLATE Latin1_General_CS_AS = @uniqueid";
	}
}
