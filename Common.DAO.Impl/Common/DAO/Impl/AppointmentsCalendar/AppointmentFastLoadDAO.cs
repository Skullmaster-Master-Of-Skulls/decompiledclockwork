using System;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.AppointmentsCalendar;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Impl.AppointmentsCalendar
{
	// Token: 0x0200015F RID: 351
	public class AppointmentFastLoadDAO : IAppointmentFastLoadDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000A45 RID: 2629 RVA: 0x0000ED1A File Offset: 0x0000CF1A
		public AppointmentFastLoadDAO()
		{
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0006C0AC File Offset: 0x0006A2AC
		public AppointmentFastLoadDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x0006C0BE File Offset: 0x0006A2BE
		// (set) Token: 0x06000A48 RID: 2632 RVA: 0x0006C0C6 File Offset: 0x0006A2C6
		public OperationContext OpContext { get; set; }

		// Token: 0x06000A49 RID: 2633 RVA: 0x0006C0D0 File Offset: 0x0006A2D0
		public DateTime? GetCurrentAppointmentFastLoadDate()
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			object obj = databaseLayer.ExecuteScalar("SELECT safevalue FROM miscsafedate WHERE safekey='AppFastLoadCutoffDate'");
			bool flag = obj == null || obj is DBNull || !(obj is DateTime);
			DateTime? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new DateTime?((DateTime)obj);
			}
			return result;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0006C138 File Offset: 0x0006A338
		public void RefreshAppointmentFastLoadTables(DateTime dt)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@dt", DbType.DateTime, dt)
			};
			databaseLayer.ExecuteStoredProcedure("sp_APPFASTLOAD_ChangeAppFastLoadCutoffDate", parameters);
		}
	}
}
