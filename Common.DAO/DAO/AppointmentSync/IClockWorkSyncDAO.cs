using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentSync;
using TechnoPro.Common.Public.Entities.AppointmentSync.FastSync;

namespace TechnoPro.Common.DAO.AppointmentSync
{
	// Token: 0x020000B0 RID: 176
	public interface IClockWorkSyncDAO : IBaseOperationContext<SyncOperationContext>
	{
		// Token: 0x060004B5 RID: 1205
		void CreateClockWorkSyncAppointment(ClockWorkSyncAppointment appointment);

		// Token: 0x060004B6 RID: 1206
		void UpdateClockWorkSyncAppointment(ClockWorkSyncAppointment appointment);

		// Token: 0x060004B7 RID: 1207
		void UpdateClockWorkSyncAppointmentReadOnlyStatus(int appointmentId, bool newReadOnlyStatus);

		// Token: 0x060004B8 RID: 1208
		List<ClockWorkSyncAppointment> LoadClockWorkAppointments(List<int> personIds, DateTime startDate, DateTime endDate, bool includeCancelled);

		// Token: 0x060004B9 RID: 1209
		ClockWorkSyncAppointment LoadClockWorkAppointmentById(int appointmentId);

		// Token: 0x060004BA RID: 1210
		DateTime GetClockWorkAppointmentLastModifiedDateTime(int appointmentId);

		// Token: 0x060004BB RID: 1211
		ClockWorkSyncAppointmentChangeResponse LoadAppointmentChanges(ClockWorkSyncAppointmentChangeRequest request);

		// Token: 0x060004BC RID: 1212
		DateTime ResetSyncState(int clockworkPersonId);
	}
}
