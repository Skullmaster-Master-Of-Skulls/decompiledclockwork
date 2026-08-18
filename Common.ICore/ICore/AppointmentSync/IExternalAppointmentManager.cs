using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentSync;
using TechnoPro.Common.Public.Entities.AppointmentSync.FastSync;

namespace TechnoPro.Common.ICore.AppointmentSync
{
	// Token: 0x020000BE RID: 190
	public interface IExternalAppointmentManager : IBaseOperationContext<SyncOperationContext>
	{
		// Token: 0x060005A6 RID: 1446
		ExternalAppointment LoadAppointment(ExternalAppointmentId appId);

		// Token: 0x060005A7 RID: 1447
		ExternalAppointment LoadAppointment(ExternalAppointmentId appId, string calendarId);

		// Token: 0x060005A8 RID: 1448
		ExternalAppointment LoadOcurrenceOfRecurringSerieByAnyOcurrenceId(string uniqueId, int ocurrenceIndex);

		// Token: 0x060005A9 RID: 1449
		ExternalAppointment LoadOccurrenceOfRecurringSerieByMasterId(string masterAppUid, int occurenceIndex);

		// Token: 0x060005AA RID: 1450
		void DeleteAppointment(ExternalAppointmentId appId);

		// Token: 0x060005AB RID: 1451
		void DeleteAppointment(ExternalAppointmentId appId, string calendarId);

		// Token: 0x060005AC RID: 1452
		IList<ExternalAppointment> LoadAppointments(ExternalAttendee user, DateTime startdate, DateTime endDate);

		// Token: 0x060005AD RID: 1453
		IList<ExternalAppointment> TryToLoadAppointments(IList<ExternalAppointmentId> appUidList);

		// Token: 0x060005AE RID: 1454
		IList<ExternalAppointment> LoadModifiedAppointments(ExternalAttendee user, DateTime startdate, DateTime thresholdTime, bool sortedByDate = true);

		// Token: 0x060005AF RID: 1455
		IList<ExternalAppointment> LoadOccurrenceAppointmentsOfRecurrenceSerie(string masterAppUid, DateTime? startDatetime = null, int count = 100, bool loadMapping = false);

		// Token: 0x060005B0 RID: 1456
		void CreateAppointment(ExternalAppointment appointment);

		// Token: 0x060005B1 RID: 1457
		void UpdateAppointment(ExternalAppointment appointment);

		// Token: 0x060005B2 RID: 1458
		bool IsAppointmentEditable(ExternalAppointmentId extAppId);

		// Token: 0x060005B3 RID: 1459
		bool IsAppointmentEditable(ExternalAppointmentId extAppId, string calendarId);

		// Token: 0x060005B4 RID: 1460
		void ReloadExternalCalendarLastDateTimeModified(ExternalAppointment appointment);

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060005B5 RID: 1461
		// (set) Token: 0x060005B6 RID: 1462
		IApplicationSyncAdministrationManager ApplicationSyncAdministrationMng { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060005B7 RID: 1463
		IList<eSyncAppointmentComparisonIdType> AppointmentCompareWorkflow { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060005B8 RID: 1464
		IList<eSyncAppointmentComparisonIdType> RecurrenceAppointmentCompareWorkflow { get; }

		// Token: 0x060005B9 RID: 1465
		bool AppointmentsAreEqual(ClockWorkSyncAppointment cwapp, ExternalAppointment exapp);

		// Token: 0x060005BA RID: 1466
		bool AppointmentsAreEqual(ExternalAppointment exapp1, ExternalAppointment exapp2);

		// Token: 0x060005BB RID: 1467
		bool ExternalAppointmentIdAreEquals(ExternalAppointmentId exappId1, ExternalAppointmentId exappId2);

		// Token: 0x060005BC RID: 1468
		bool SupportsFastSync();

		// Token: 0x060005BD RID: 1469
		ExternalSyncAppointmentChangesResponse LoadAppointmentChanges(ExternalSyncAppointmentChangesRequest request);

		// Token: 0x060005BE RID: 1470
		string LoadNativeAppointmentInfo(string appId);
	}
}
