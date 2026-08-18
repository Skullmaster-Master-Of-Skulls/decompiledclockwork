using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.AppointmentsReminder;
using TechnoPro.Common.DAO.Impl.AppointmentsReminder;
using TechnoPro.Common.ICore;
using TechnoPro.Common.ICore.AppointmentsReminder;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsReminder;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.AppointmentsReminder
{
	// Token: 0x02000146 RID: 326
	public class AppointmentsReminderManager : IAppointmentsReminderManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x0006C59A File Offset: 0x0006A79A
		// (set) Token: 0x06000E87 RID: 3719 RVA: 0x0006C5A2 File Offset: 0x0006A7A2
		private IAppointmentsReminderDAO AppReminderDAO { get; set; }

		// Token: 0x06000E88 RID: 3720 RVA: 0x0006C5AB File Offset: 0x0006A7AB
		public AppointmentsReminderManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.AppReminderDAO = new AppointmentsReminderDAO(this.OpContext);
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0006C5D0 File Offset: 0x0006A7D0
		public IList<AppointmentReminder> LoadAppointmentsReminder()
		{
			return this.AppReminderDAO.LoadAppointmentsReminder();
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0006C5ED File Offset: 0x0006A7ED
		public void ChangeAppointmentsReminderNotificationStatus(IList<int> appReminderIdList, bool alreadyNotified)
		{
			this.AppReminderDAO.ChangeAppointmentsReminderNotificationStatus(appReminderIdList, alreadyNotified);
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0006C600 File Offset: 0x0006A800
		public void AddPeopleToExclusionList(int personId)
		{
			bool flag = this.AppReminderDAO.AddPeopleToExclusionList(personId);
			bool flag2 = flag;
			if (flag2)
			{
				ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
				cacheStorageManager.Remove("AppointmentReminderPeopleExclusionList");
			}
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0006C634 File Offset: 0x0006A834
		public void RemovePeopleFromExclusionList(int personId)
		{
			bool flag = this.AppReminderDAO.RemovePeopleFromExclusionList(personId);
			bool flag2 = flag;
			if (flag2)
			{
				ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
				cacheStorageManager.Remove("AppointmentReminderPeopleExclusionList");
			}
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0006C668 File Offset: 0x0006A868
		public IList<int> LoadPeopleExclusionList()
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			object obj = cacheStorageManager["AppointmentReminderPeopleExclusionList"];
			bool flag = obj != null;
			IList<int> result;
			if (flag)
			{
				result = (IList<int>)obj;
			}
			else
			{
				result = this.AppReminderDAO.LoadPeopleExclusionList();
			}
			return result;
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0006C6A8 File Offset: 0x0006A8A8
		public IList<int> LoadGroupInclusionList()
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			object obj = cacheStorageManager["AppointmentReminderGroupInclusionList"];
			bool flag = obj != null;
			IList<int> result;
			if (flag)
			{
				result = (IList<int>)obj;
			}
			else
			{
				result = this.AppReminderDAO.LoadGroupInclusionList();
			}
			return result;
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0006C6E8 File Offset: 0x0006A8E8
		public bool IsAppointmentsReminderEnable()
		{
			IMiscSafeManager miscSafeManager = new MiscSafeManager();
			string value = miscSafeManager.GetValue("AppointmentsReminder.Enable");
			bool flag2;
			bool flag = string.IsNullOrEmpty(value) || (bool.TryParse(value, out flag2) && flag2);
			return flag && !this.AppReminderDAO.IsPersonInExclusionList(this.OpContext.WhoAmI);
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0006C744 File Offset: 0x0006A944
		public int AddAppointmentReminder(AppointmentReminder appReminder)
		{
			return this.AppReminderDAO.AddAppointmentReminder(appReminder);
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0006C762 File Offset: 0x0006A962
		public void UpdateAppointmentReminder(AppointmentReminder appReminder)
		{
			this.AppReminderDAO.UpdateAppointmentReminder(appReminder);
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0006C772 File Offset: 0x0006A972
		public void DeleteAppointmentReminder(int appointmentID, int attPersonID)
		{
			this.AppReminderDAO.DeleteAppointmentReminder(appointmentID, attPersonID);
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x0006C783 File Offset: 0x0006A983
		// (set) Token: 0x06000E94 RID: 3732 RVA: 0x0006C78B File Offset: 0x0006A98B
		public OperationContext OpContext { get; set; }
	}
}
