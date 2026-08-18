using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnoPro.Common.Core.AppointmentLog;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.AppointmentsCalendar;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.DAO.AppointmentsWorkshops;
using TechnoPro.Common.DAO.Impl.Appointments;
using TechnoPro.Common.ICore.AppointmentLog;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.AppointmentsWorkshops;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsWorkshops;

namespace TechnoPro.Common.Core.AppointmentsWorkshops
{
	// Token: 0x02000137 RID: 311
	public class WorkshopAppointmentManager : IWorkshopAppointmentManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x00061EEB File Offset: 0x000600EB
		// (set) Token: 0x06000D7A RID: 3450 RVA: 0x00061EF3 File Offset: 0x000600F3
		public IWorkshopAppointmentDAO dao { get; set; }

		// Token: 0x06000D7B RID: 3451 RVA: 0x00061EFC File Offset: 0x000600FC
		public WorkshopAppointmentManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new WorkshopAppointmentDAO(opContext);
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x00061F1C File Offset: 0x0006011C
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

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x00061F52 File Offset: 0x00060152
		// (set) Token: 0x06000D7E RID: 3454 RVA: 0x00061F5A File Offset: 0x0006015A
		public OperationContext OpContext { get; set; }

		// Token: 0x06000D7F RID: 3455 RVA: 0x00061F64 File Offset: 0x00060164
		public void UncancelWorkshopAppointment(bool runInTransaction, int AppointmentId)
		{
			AppointmentManager appointmentManager = new AppointmentManager(this.OpContext);
			appointmentManager.UnCancelAppointment(runInTransaction, AppointmentId);
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00061F88 File Offset: 0x00060188
		public void CancelWorkshopAppointment(bool runInTransaction, int AppointmentId, AppCancelInfo CancelInfo)
		{
			AppointmentManager appointmentManager = new AppointmentManager(this.OpContext);
			appointmentManager.CancelAppointment(runInTransaction, AppointmentId, CancelInfo);
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00061FAC File Offset: 0x000601AC
		public IList<WorkshopAppointment> LoadWorkshopAppointmentsByWorkshopId(DateTime StartDate, DateTime EndDate, int WorkshopId)
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(this.OpContext);
			List<int> allowedAppTypeIds = appointmentTypeManager.GetAllowedAppTypeIds(this.OpContext.WhoAmI).ToList<int>();
			return this.dao.LoadWorkshopAppointmentsByWorkshopId(StartDate, EndDate, WorkshopId, allowedAppTypeIds);
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00061FF0 File Offset: 0x000601F0
		public int CreateWorkshopAppointment(bool runInTransaction, WorkshopAppointment WorkshopApp)
		{
			return this.dao.CreateWorkshopAppointment(WorkshopApp);
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00062010 File Offset: 0x00060210
		public void DeleteWorkshopAppointment(bool runInTransaction, int AppointmentId)
		{
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(this.OpContext);
			baseAppointmentManager.DeleteAppointment(runInTransaction, AppointmentId);
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00062034 File Offset: 0x00060234
		public void UpdateWorkshopAppointment(bool runInTransaction, WorkshopAppointment WorkshopApp)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(WorkshopApp.AppointmentId);
			}
			this.dao.UpdateWorkshopAppointment(WorkshopApp);
			bool flag2 = !runInTransaction;
			if (flag2)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(WorkshopApp.AppointmentId, eAppointmentModifiedItemType.WorkshopInfo);
				});
			}
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x000620A3 File Offset: 0x000602A3
		public void UpdateWorkshopAppointmentMaxAttendees(int appointmentId, int newMaxAttendees)
		{
			this.dao.UpdateWorkshopAppointmentMaxAttendees(appointmentId, newMaxAttendees);
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x000620B4 File Offset: 0x000602B4
		public WorkshopAppointment LoadWorkshopAppointmentById(int workshopAppId)
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(this.OpContext);
			List<int> allowedAppTypeIds = appointmentTypeManager.GetAllowedAppTypeIds(this.OpContext.WhoAmI).ToList<int>();
			return this.dao.LoadWorkshopAppointmentById(workshopAppId, allowedAppTypeIds);
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x000620F8 File Offset: 0x000602F8
		public IList<AppType> GetWorkshopGroups()
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(this.OpContext);
			return (from appType in appointmentTypeManager.LoadAllAppTypes()
			where appType.IsWorkshop
			select appType).ToList<AppType>();
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00062148 File Offset: 0x00060348
		public IList<WorkshopAppointment> LoadWorkshopAppointmentsWithNoWorkshopId(DateTime StartDate, DateTime EndDate, int appTypeId)
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(this.OpContext);
			List<int> list = appointmentTypeManager.GetAllowedAppTypeIds(this.OpContext.WhoAmI).ToList<int>();
			bool flag = !list.Contains(appTypeId);
			IList<WorkshopAppointment> result;
			if (flag)
			{
				result = new List<WorkshopAppointment>();
			}
			else
			{
				IList<WorkshopAppointment> list2 = this.dao.LoadWorkshopAppointmentsWithNoWorkshopId(StartDate, EndDate, appTypeId);
				BaseAppointmentManager.HideAppointmentInfoBasedOnPermissions<WorkshopAppointment>(this.OpContext.WhoAmI, null, ref list2);
				result = list2;
			}
			return result;
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x000621BC File Offset: 0x000603BC
		public void UpdateAppointmentWorkshopId(bool runInTransaction, int AppointmentId, int NewWorkshopId)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(AppointmentId);
			}
			this.dao.UpdateAppointmentWorkshopId(AppointmentId, NewWorkshopId);
			bool flag2 = !runInTransaction;
			if (flag2)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(AppointmentId, eAppointmentModifiedItemType.WorkshopInfo);
				});
			}
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00062228 File Offset: 0x00060428
		public bool IsAppointmentAWorkshop(int appointmentId)
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(this.OpContext);
			AppType appType = appointmentTypeManager.LoadAppTypeByAppointmentId(appointmentId);
			return appType != null && appType.IsWorkshop;
		}

		// Token: 0x04000283 RID: 643
		private IAppointmentLogDAO _appLogDao;
	}
}
