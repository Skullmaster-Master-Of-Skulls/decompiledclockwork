using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking
{
	// Token: 0x02000088 RID: 136
	public interface ISittingClientManager : IWebService
	{
		// Token: 0x06000404 RID: 1028
		int CreateSitting(SittingDTO Sitting);

		// Token: 0x06000405 RID: 1029
		void UpdateSitting(SittingDTO Sitting);

		// Token: 0x06000406 RID: 1030
		SittingDTO LoadSittingById(int SittingId);

		// Token: 0x06000407 RID: 1031
		IList<SittingDTO> LoadSittingsByDateRange(DateTime StartDate, DateTime EndDate);

		// Token: 0x06000408 RID: 1032
		void ClearSittingsOnAppointments(params int[] AppointmentIds);

		// Token: 0x06000409 RID: 1033
		void SetSittingOnAppointment(int AppointmentId, int SittingId);

		// Token: 0x0600040A RID: 1034
		void SetSittingsOnAppointments(IDictionary<int, int> AppointmentIdWithSittingIds);

		// Token: 0x0600040B RID: 1035
		IList<int> LoadBookingAppointmentIdsBySitting(int SittingId);

		// Token: 0x0600040C RID: 1036
		void DeleteSitting(int SittingId);

		// Token: 0x0600040D RID: 1037
		IList<BasicTestDTO> LoadBasicBookingsBySitting(int SittingId);
	}
}
