using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.ICore.AppointmentsTestBooking
{
	// Token: 0x020000C8 RID: 200
	public interface ISittingManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600060E RID: 1550
		int CreateSitting(Sitting Sitting);

		// Token: 0x0600060F RID: 1551
		void DeleteSitting(int SittingId);

		// Token: 0x06000610 RID: 1552
		void UpdateSitting(Sitting Sitting);

		// Token: 0x06000611 RID: 1553
		Sitting LoadSittingById(int SittingId);

		// Token: 0x06000612 RID: 1554
		IList<Sitting> LoadSittingsByDate(DateTime Day);

		// Token: 0x06000613 RID: 1555
		IList<Sitting> LoadSittingsByDateRange(DateTime StartDate, DateTime EndDate);

		// Token: 0x06000614 RID: 1556
		void ClearSittingOnAppointment(params int[] AppointmentIds);

		// Token: 0x06000615 RID: 1557
		void SetSittingOnAppointment(IDictionary<int, int> AppointmentIdWithSittingIds);
	}
}
