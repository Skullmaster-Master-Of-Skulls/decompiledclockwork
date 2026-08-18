using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.DAO.AppointmentsTestBooking
{
	// Token: 0x020000C0 RID: 192
	public interface ISittingDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600051F RID: 1311
		void DeleteSitting(int SittingId);

		// Token: 0x06000520 RID: 1312
		void SaveSitting(Sitting OldSitting, Sitting NewSitting);

		// Token: 0x06000521 RID: 1313
		List<Test> LoadSittingTests(int SittingId);

		// Token: 0x06000522 RID: 1314
		IList<Sitting> LoadSittings(DateTime StartDate, DateTime EndDate);

		// Token: 0x06000523 RID: 1315
		Sitting LoadSittingById(int SittingId);

		// Token: 0x06000524 RID: 1316
		void ClearSittingOnAppointment(int AppointmentId);

		// Token: 0x06000525 RID: 1317
		void SetSittingOnAppointment(int AppointmentId, int SittingId);
	}
}
