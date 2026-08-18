using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentSync;
using TechnoPro.Common.Public.Entities.AppointmentSync.FastSync;

namespace TechnoPro.Common.DAO.AppointmentSync
{
	// Token: 0x020000B1 RID: 177
	public interface IExternalAppointmentDAO : IBaseOperationContext<SyncOperationContext>
	{
		// Token: 0x060004BD RID: 1213
		IList<ExternalAppointment> LoadAppointments(ExternalAttendee user, DateTime startdate, DateTime endDate);

		// Token: 0x060004BE RID: 1214
		IList<ExternalAppointment> LoadAppointments(ExternalAttendee user, DateTime startdate, DateTime endDate, bool sortedByDate);

		// Token: 0x060004BF RID: 1215
		IList<ExternalAppointment> LoadModifiedAppointments(ExternalAttendee user, DateTime startdate, DateTime thresholdTime, bool sortedByDate = true);

		// Token: 0x060004C0 RID: 1216
		IList<ExternalAppointment> LoadOccurrenceAppointmentsOfRecurrenceSerie(string masterAppUid, DateTime? startDatetime = null, int count = 100, bool loadMapping = false);

		// Token: 0x060004C1 RID: 1217
		ExternalAppointment LoadAppointment(string appUid);

		// Token: 0x060004C2 RID: 1218
		IList<ExternalAppointment> LoadAppointments(IList<string> appUidList);

		// Token: 0x060004C3 RID: 1219
		ExternalAppointment LoadOccurrenceOfRecurringSerieByMasterId(string masterAppUid, int occurenceIndex);

		// Token: 0x060004C4 RID: 1220
		ExternalAppointment LoadOcurrenceOfRecurringSerieByAnyOcurrenceId(string uniqueId, int ocurrenceIndex);

		// Token: 0x060004C5 RID: 1221
		ExternalAppointment LoadAppointmentByClockWorkAppointmentId(int cwappid);

		// Token: 0x060004C6 RID: 1222
		ExternalAppointment CreateAppointment(ExternalAppointment appointment);

		// Token: 0x060004C7 RID: 1223
		void UpdateAppointment(ExternalAppointment appointment);

		// Token: 0x060004C8 RID: 1224
		void DeleteAppointment(ExternalAppointment exApp);

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x060004C9 RID: 1225
		// (set) Token: 0x060004CA RID: 1226
		int PagingSize { get; set; }

		// Token: 0x060004CB RID: 1227
		void UpdateClockWorkAppId(string uniqueId, int cwappid);

		// Token: 0x060004CC RID: 1228
		string ResetSyncState(string username);

		// Token: 0x060004CD RID: 1229
		ExternalSyncAppointmentChangesResponse LoadAppointmentChanges(ExternalSyncAppointmentChangesRequest request);

		// Token: 0x060004CE RID: 1230
		string LoadNativeAppointmentInfo(string appId);
	}
}
