using System;
using System.Collections.Generic;
using TechnoPro.Common.Core.AppointmentSync;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.AppointmentSync;
using TechnoPro.Common.DAO.Exchange.Impl;
using TechnoPro.Common.ICore.AppointmentSync;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentSync;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.Exchange
{
	// Token: 0x02000003 RID: 3
	public class ExchangeSyncAdministrationManager : IApplicationSyncAdministrationManager, IBaseOperationContext<SyncOperationContext>
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00003215 File Offset: 0x00001415
		// (set) Token: 0x06000027 RID: 39 RVA: 0x0000321D File Offset: 0x0000141D
		public IApplicationSyncAdministrationDAO ApplicationSyncAdministrationDAO { get; set; }

		// Token: 0x06000028 RID: 40 RVA: 0x00003226 File Offset: 0x00001426
		public ExchangeSyncAdministrationManager(SyncOperationContext opContext)
		{
			this.OpContext = opContext;
			this.ApplicationSyncAdministrationDAO = new ExchangeSyncAdministrationDAO(this.OpContext);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000324C File Offset: 0x0000144C
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

		// Token: 0x0600002A RID: 42 RVA: 0x000032F0 File Offset: 0x000014F0
		public string GetPrimarySmtpAddress(string email)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			object obj = cacheStorageManager["primarysmtpaddress:" + email];
			bool flag = obj != null;
			string result;
			if (flag)
			{
				result = (string)obj;
			}
			else
			{
				ISyncContactsDAO syncContactsDAO = new ExchangeContactsDAO(this.OpContext);
				result = (string)(cacheStorageManager["primarysmtpaddress:" + email] = syncContactsDAO.GetPrimarySmtpAddress(email));
			}
			return result;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003360 File Offset: 0x00001560
		public void FillUniqueId2FieldInDatabase()
		{
			IMiscCodeManager miscCodeManager = new MiscCodeManager(this.OpContext);
			string text = miscCodeManager.LoadMiscCodeValue(eMiscCode.IsAppointmentSyncUniqueId2MappingFilled);
			bool flag = !string.IsNullOrEmpty(text) && (text.Equals("1") || text.Equals("y", StringComparison.OrdinalIgnoreCase) || text.Equals("yes", StringComparison.OrdinalIgnoreCase) || text.Equals("true", StringComparison.OrdinalIgnoreCase));
			bool flag2 = !flag;
			if (flag2)
			{
				IAppointmentSyncMappingManager appointmentSyncMappingManager = new AppointmentSyncMappingManager(this.OpContext);
				IList<ClockWorkExternalAppMapping> list = appointmentSyncMappingManager.LoadAllMappingsWithNoUniqueId2();
				IExternalAppointmentManager externalAppointmentManager = new ExchangeAppointmentManager(this.OpContext);
				foreach (ClockWorkExternalAppMapping clockWorkExternalAppMapping in list)
				{
					try
					{
						ExternalAppointmentId appId = new ExternalAppointmentId
						{
							ClockWorkAppId = clockWorkExternalAppMapping.ClockWorkAppointmentId,
							UniqueId = clockWorkExternalAppMapping.ExternalApplicationUniqueAppointmentId,
							UniqueId2 = clockWorkExternalAppMapping.ExternalApplicationUniqueAppointmentId2,
							GlobalAppId = clockWorkExternalAppMapping.ExternalApplicationGlobalAppointmentId
						};
						ExternalAppointment externalAppointment = externalAppointmentManager.LoadAppointment(appId);
						bool flag3 = externalAppointment != null;
						if (flag3)
						{
							clockWorkExternalAppMapping.ExternalApplicationUniqueAppointmentId2 = externalAppointment.UniqueId2;
							appointmentSyncMappingManager.UpdateMappingsTable(clockWorkExternalAppMapping.ClockWorkAppointmentId, clockWorkExternalAppMapping.ExternalApplicationUniqueAppointmentId, externalAppointment.UniqueId2);
						}
					}
					catch (Exception ex)
					{
					}
				}
				miscCodeManager.SaveMiscCodeValue(eMiscCode.IsAppointmentSyncUniqueId2MappingFilled, "1");
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000034EC File Offset: 0x000016EC
		public ProductLicenseState GetCalendarSyncLicenseStatus(out DateTime? expiryDate)
		{
			LicensingManager licensingManager = new LicensingManager();
			return licensingManager.GetProductState("Outlook Calendar Sync", out expiryDate);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003510 File Offset: 0x00001710
		public LicenseKeyInfo GetCalendarSyncProductKey()
		{
			LicensingManager licensingManager = new LicensingManager();
			return licensingManager.GetProductKey("Outlook Calendar Sync");
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00003533 File Offset: 0x00001733
		// (set) Token: 0x0600002F RID: 47 RVA: 0x0000353B File Offset: 0x0000173B
		public SyncOperationContext OpContext { get; set; }

		// Token: 0x04000008 RID: 8
		private const string OutlookSyncProductKeyInfo = "Outlook Calendar Sync";
	}
}
