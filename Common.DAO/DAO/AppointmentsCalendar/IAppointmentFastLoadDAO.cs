using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.AppointmentsCalendar
{
	// Token: 0x020000C5 RID: 197
	public interface IAppointmentFastLoadDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600055E RID: 1374
		DateTime? GetCurrentAppointmentFastLoadDate();

		// Token: 0x0600055F RID: 1375
		void RefreshAppointmentFastLoadTables(DateTime dt);
	}
}
