using System;
using System.Threading.Tasks;
using TechnoPro.Common.Core.AppointmentLog;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.DAO.Impl.Appointments;
using TechnoPro.Common.ICore.AppointmentLog;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Core.Appointments
{
	// Token: 0x0200012A RID: 298
	public class AppointmentCancelInfoManager : IAppointmentCancelInfoManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000CA0 RID: 3232 RVA: 0x00058A99 File Offset: 0x00056C99
		public AppointmentCancelInfoManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new AppointmentCancelInfoDAO(opContext);
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x00058AB7 File Offset: 0x00056CB7
		// (set) Token: 0x06000CA2 RID: 3234 RVA: 0x00058ABF File Offset: 0x00056CBF
		public OperationContext OpContext { get; set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x00058AC8 File Offset: 0x00056CC8
		private IAppointmentLogDAO appLogDao
		{
			get
			{
				bool flag = this._appLogDao == null;
				if (flag)
				{
					this._appLogDao = new AppointmentLogDAO(this.OpContext);
				}
				return this._appLogDao;
			}
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00058B00 File Offset: 0x00056D00
		public AppCancelInfo LoadCancelInfoByAppointmentId(int AppointmentId)
		{
			return this.dao.LoadCancelInfoByAppointmentId(AppointmentId);
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00058B20 File Offset: 0x00056D20
		public void DeleteCancelInfo(bool runInTransaction, int AppointmentId)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(AppointmentId);
			}
			this.dao.DeleteCancelInfo(AppointmentId, null);
			bool flag2 = !runInTransaction;
			if (flag2)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(AppointmentId, eAppointmentModifiedItemType.Cancelled);
				});
			}
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00058B8C File Offset: 0x00056D8C
		public void InsertOrUpdateAppointmentCancelInfo(bool runInTransaction, int appId, AppCancelInfo appCancelInfo)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(appId);
			}
			this.dao.InsertOrUpdateAppointmentCancelInfo(appId, appCancelInfo, null);
			bool flag2 = !runInTransaction;
			if (flag2)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(appId, eAppointmentModifiedItemType.Cancelled);
				});
			}
		}

		// Token: 0x04000260 RID: 608
		private IAppointmentCancelInfoDAO dao;

		// Token: 0x04000262 RID: 610
		private IAppointmentLogDAO _appLogDao;
	}
}
