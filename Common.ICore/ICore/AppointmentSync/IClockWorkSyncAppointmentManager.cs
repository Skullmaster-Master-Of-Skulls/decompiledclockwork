using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentSync;
using TechnoPro.Common.Public.Entities.AppointmentSync.FastSync;

namespace TechnoPro.Common.ICore.AppointmentSync
{
	// Token: 0x020000BD RID: 189
	public interface IClockWorkSyncAppointmentManager : IBaseOperationContext<SyncOperationContext>
	{
		// Token: 0x0600059C RID: 1436
		bool CreateClockWorkSyncAppointment(bool runInTransaction, ClockWorkSyncAppointment appointment, ExternalAppointment exapp);

		// Token: 0x0600059D RID: 1437
		void UpdateClockWorkSyncAppointment(bool runInTransaction, ClockWorkSyncAppointment appointment);

		// Token: 0x0600059E RID: 1438
		void DeleteClockWorkSyncAppointment(bool runInTransaction, int appointmentId);

		// Token: 0x0600059F RID: 1439
		void CancelClockWorkSyncAppointment(bool runInTransaction, int appointmentId);

		// Token: 0x060005A0 RID: 1440
		void UpdateClockWorkSyncAppointmentReadOnlyStatus(bool runInTransaction, int appointmentId, bool newReadOnlyStatus);

		// Token: 0x060005A1 RID: 1441
		List<ClockWorkSyncAppointment> LoadClockWorkAppointments(List<int> personIds, DateTime startDate, DateTime endDate, bool includeCancelled);

		// Token: 0x060005A2 RID: 1442
		ClockWorkSyncAppointment LoadClockWorkAppointmentById(int appointmentId);

		// Token: 0x060005A3 RID: 1443
		DateTime GetClockWorkAppointmentLastModifiedDateTime(int appointmentId);

		// Token: 0x060005A4 RID: 1444
		eClockWorkExternalApplicationAppointmentCompareResult CheckAppointmentDiff(ExternalAppointment externalAppointment, ClockWorkSyncAppointment clockWorkAppointment);

		// Token: 0x060005A5 RID: 1445
		ClockWorkSyncAppointmentChangeResponse LoadAppointmentChanges(ClockWorkSyncAppointmentChangeRequest request);
	}
}
