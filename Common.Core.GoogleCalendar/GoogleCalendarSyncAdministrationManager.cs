using System;
using TechnoPro.Common.DAO.AppointmentSync;
using TechnoPro.Common.DAO.GoogleCalendar.Impl.V3;
using TechnoPro.Common.ICore.AppointmentSync;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentSync;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.GoogleCalendar
{
	// Token: 0x02000003 RID: 3
	public class GoogleCalendarSyncAdministrationManager : IApplicationSyncAdministrationManager, IBaseOperationContext<SyncOperationContext>
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002A4D File Offset: 0x00000C4D
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002A55 File Offset: 0x00000C55
		private IApplicationSyncAdministrationDAO ApplicationSyncAdministrationDAO { get; set; }

		// Token: 0x06000025 RID: 37 RVA: 0x00002A5E File Offset: 0x00000C5E
		public GoogleCalendarSyncAdministrationManager(SyncOperationContext opContext)
		{
			this.OpContext = opContext;
			this.ApplicationSyncAdministrationDAO = new GoogleCalendarSyncAdministrationV3DAO(this.OpContext);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002A82 File Offset: 0x00000C82
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002A8A File Offset: 0x00000C8A
		public SyncOperationContext OpContext { get; set; }

		// Token: 0x06000028 RID: 40 RVA: 0x00002A94 File Offset: 0x00000C94
		public DelegatePermissionLevel GetDelegatePermissionLevel(string userEmailAddress)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			object obj = cacheStorageManager[string.Format("{0}->{1}", this.OpContext.SyncSettings.SyncConnection.UserCredentials.Username, userEmailAddress)];
			bool flag = obj == null;
			DelegatePermissionLevel delegatePermissionLevel;
			if (flag)
			{
				delegatePermissionLevel = this.ApplicationSyncAdministrationDAO.GetDelegatePermissionLevel(userEmailAddress);
				cacheStorageManager.Insert(string.Format("{0}->{1}", this.OpContext.SyncSettings.SyncConnection.UserCredentials.Username, userEmailAddress), delegatePermissionLevel, new TimeSpan(24, 0, 0));
			}
			else
			{
				delegatePermissionLevel = (DelegatePermissionLevel)obj;
			}
			return delegatePermissionLevel;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002B38 File Offset: 0x00000D38
		public string GetPrimarySmtpAddress(string email)
		{
			return email;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002B4B File Offset: 0x00000D4B
		public void FillUniqueId2FieldInDatabase()
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002B50 File Offset: 0x00000D50
		public ProductLicenseState GetCalendarSyncLicenseStatus(out DateTime? expiryDate)
		{
			LicensingManager licensingManager = new LicensingManager();
			return licensingManager.GetProductState("Sync between ClockWork and Google Calendar.", out expiryDate);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002B74 File Offset: 0x00000D74
		public LicenseKeyInfo GetCalendarSyncProductKey()
		{
			LicensingManager licensingManager = new LicensingManager();
			return licensingManager.GetProductKey("Sync between ClockWork and Google Calendar.");
		}

		// Token: 0x04000007 RID: 7
		private const string GoogleCalendarSyncProductKeyInfo = "Sync between ClockWork and Google Calendar.";
	}
}
