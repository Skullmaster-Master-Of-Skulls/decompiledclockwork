using System;
using System.Collections.Generic;
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
	// Token: 0x0200012D RID: 301
	public class AppointmentIconManager : IAppointmentIconManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000CB6 RID: 3254 RVA: 0x00058FBF File Offset: 0x000571BF
		public AppointmentIconManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new AppointmentIconDAO(opContext);
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x00058FDD File Offset: 0x000571DD
		// (set) Token: 0x06000CB8 RID: 3256 RVA: 0x00058FE5 File Offset: 0x000571E5
		public OperationContext OpContext { get; set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x00058FF0 File Offset: 0x000571F0
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

		// Token: 0x06000CBA RID: 3258 RVA: 0x00059028 File Offset: 0x00057228
		public IList<AppointmentIcon> LoadAppointmentIconsByAppointment(int AppointmentId)
		{
			return this.dao.LoadAppointmentIconsByAppointment(AppointmentId);
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00059048 File Offset: 0x00057248
		public AppointmentIcon LoadAppointmentIcon(int AppointmentId, int IconNum)
		{
			return this.dao.LoadAppointmentIcon(AppointmentId, IconNum);
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00059068 File Offset: 0x00057268
		public AppointmentIcon LoadAppointmentIconByIconNum(int IconNum)
		{
			return this.dao.LoadAppointmentIconByIconNum(IconNum);
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00059088 File Offset: 0x00057288
		public AppointmentIcon LoadAppointmentIcon(int IconInfoId)
		{
			return this.dao.LoadAppointmentIcon(IconInfoId);
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x000590A8 File Offset: 0x000572A8
		public void DeleteAppointmentIconsNotInList(bool runInTransaction, int AppointmentId, IList<int> IconNums)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(AppointmentId);
			}
			this.dao.DeleteAppointmentIconsNotInList(AppointmentId, IconNums, null);
			bool flag2 = !runInTransaction;
			if (flag2)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(AppointmentId, eAppointmentModifiedItemType.Icons);
				});
			}
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00059114 File Offset: 0x00057314
		public int InsertOrUpdateAppointmentIcon(bool runInTransaction, int AppointmentId, AppointmentIcon icon)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(AppointmentId);
			}
			int result = this.dao.InsertOrUpdateAppointmentIcon(AppointmentId, icon, null);
			bool flag2 = !runInTransaction;
			if (flag2)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(AppointmentId, eAppointmentModifiedItemType.Icons);
				});
			}
			return result;
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00059188 File Offset: 0x00057388
		public void DeleteAppointmentIcon(bool runInTransaction, int AppointmentId, int IconNum)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(AppointmentId);
			}
			this.dao.DeleteAppointmentIcon(AppointmentId, IconNum, null);
			bool flag2 = !runInTransaction;
			if (flag2)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(AppointmentId, eAppointmentModifiedItemType.Icons);
				});
			}
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x000591F4 File Offset: 0x000573F4
		public IList<IconInfo> LoadAllIconInfos()
		{
			IIconInfoDAO iconInfoDAO = new IconInfoDAO(this.OpContext);
			return iconInfoDAO.LoadAllIconInfos();
		}

		// Token: 0x04000266 RID: 614
		private IAppointmentIconDAO dao;

		// Token: 0x04000268 RID: 616
		private IAppointmentLogDAO _appLogDao;
	}
}
