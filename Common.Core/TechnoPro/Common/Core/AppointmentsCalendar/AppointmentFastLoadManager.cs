using System;
using ClockWorkLogger;
using TechnoPro.Common.DAO.AppointmentsCalendar;
using TechnoPro.Common.DAO.Impl.AppointmentsCalendar;
using TechnoPro.Common.ICore.AppointmentsCalendar;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core.AppointmentsCalendar
{
	// Token: 0x02000149 RID: 329
	public class AppointmentFastLoadManager : IAppointmentFastLoadManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000ECB RID: 3787 RVA: 0x0000672B File Offset: 0x0000492B
		public AppointmentFastLoadManager()
		{
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x0006F7E2 File Offset: 0x0006D9E2
		public AppointmentFastLoadManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x0006F7F4 File Offset: 0x0006D9F4
		// (set) Token: 0x06000ECE RID: 3790 RVA: 0x0006F7FC File Offset: 0x0006D9FC
		public OperationContext OpContext { get; set; }

		// Token: 0x06000ECF RID: 3791 RVA: 0x0006F808 File Offset: 0x0006DA08
		public void RefreshAppointmentFastLoadTables()
		{
			IAppointmentFastLoadDAO appointmentFastLoadDAO = new AppointmentFastLoadDAO(this.OpContext);
			DateTime? currentAppointmentFastLoadDate = appointmentFastLoadDAO.GetCurrentAppointmentFastLoadDate();
			bool flag = currentAppointmentFastLoadDate == null;
			if (!flag)
			{
				DateTime date = DateTime.Now.Date;
				int month = date.Month;
				int year = date.Year;
				bool flag2 = month < 5;
				DateTime dateTime;
				if (flag2)
				{
					dateTime = new DateTime(year - 1, 9, 1);
				}
				else
				{
					bool flag3 = month < 9;
					if (flag3)
					{
						dateTime = new DateTime(year, 1, 1);
					}
					else
					{
						dateTime = new DateTime(year, 5, 1);
					}
				}
				bool flag4 = currentAppointmentFastLoadDate.Value.Date >= dateTime;
				if (!flag4)
				{
					appointmentFastLoadDAO.RefreshAppointmentFastLoadTables(dateTime);
					CWLogger.Logger.Info("AppointmentFastLoadManager:RefreshAppointmentFastLoadTables:Fast load date updated from {0} to {1}", currentAppointmentFastLoadDate.Value.ToString("yyyy-MM-dd"), dateTime.ToString("yyyy-MM-dd"));
				}
			}
		}
	}
}
