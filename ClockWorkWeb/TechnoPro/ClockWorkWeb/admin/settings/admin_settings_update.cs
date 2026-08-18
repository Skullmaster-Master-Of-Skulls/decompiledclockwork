using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.Authentication;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.ClientManager.Core.Authentication;
using TechnoPro.Common.ClientManager.Core.Caching;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Authentication;
using TechnoPro.Common.ClientManager.ICore.Caching;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Configuration;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Authentication;
using TechnoPro.Common.Public.Entities.Caching;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.ClockWorkWeb.admin.settings
{
	// Token: 0x02000192 RID: 402
	public class admin_settings_update : Page
	{
		// Token: 0x06000BD1 RID: 3025 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void btn_temp_set_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0004CF5C File Offset: 0x0004B15C
		protected void btn_a_Click(object sender, EventArgs e)
		{
			string text = this.txt_u.Text.Trim();
			string text2 = this.txt_p.Text.Trim();
			bool flag = text.Length > 0 && text2.Length > 0;
			if (flag)
			{
				IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
				ClockWorkIdentity currentClockWorkIdentity = webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity(this.Page);
				AuthenticationAndAuthorizationResultDTO authenticationAndAuthorizationResultDTO = webAuthenticationAuthorizationWebClientManager.TryToAuthenticateStaff(text, text2, new AuthenticationArgsDTO(), true);
				int num = (authenticationAndAuthorizationResultDTO != null && authenticationAndAuthorizationResultDTO.PassedAuthentication) ? authenticationAndAuthorizationResultDTO.ClockWorkUser.ClockWorkPid : 0;
				bool flag2 = currentClockWorkIdentity != null;
				if (flag2)
				{
					webAuthenticationAuthorizationWebClientManager.SetCurrentClockWorkIdentity(currentClockWorkIdentity);
				}
				else
				{
					WebAuthenticationAuthorizationWebClientManager.CurrentInstance.Logout(false);
				}
				bool flag3 = false;
				bool flag4 = num > 0;
				if (flag4)
				{
					IClockWorkAuthenticationClientManager clockWorkAuthenticationClientManager = new ClockWorkAuthenticationClientManager();
					flag3 = clockWorkAuthenticationClientManager.IsUserAdminOrInSettingsListOfStaffPidsAllowedToLoginAsAnother(num);
				}
				bool flag5 = flag3;
				string text3;
				if (flag5)
				{
					foreach (object obj in HttpContext.Current.Cache)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						HttpContext.Current.Cache.Remove((string)dictionaryEntry.Key);
					}
					IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
					webSettingsClientManager.ClearSettingsCache();
					IServerCacheClientManager serverCacheClientManager = new ServerCacheClientManager();
					try
					{
						IServerCacheClientManager serverCacheClientManager2 = serverCacheClientManager;
						eServerCacheItemType[] array = new eServerCacheItemType[4];
						RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.9058D9D22DFE1CB7DD5CF8E09DB4680ACFA2F6F7).FieldHandle);
						serverCacheClientManager2.ClearServerCacheAllSubItems(array);
						serverCacheClientManager.ClearServerCacheItems(new string[]
						{
							"TestBookingRooms",
							"TestBookingAssets",
							"TestBookingRules",
							"TestBookingRooms",
							"TestBookingSpecialAccommodations"
						});
					}
					catch (Exception ex)
					{
						CWLogger.Logger.Error("/admin/settings/update.aspx.cs:btn_a_Click:serverCacheClientManager.ClearServerCacheAllSubItems:{0}", ex.ToString());
					}
					try
					{
						serverCacheClientManager.ClearServerCacheItems(new string[]
						{
							"TestBookingAssets",
							"TestBookingRules",
							"TestBookingRooms",
							"TestBookingSpecialAccommodations"
						});
					}
					catch (Exception ex2)
					{
						CWLogger.Logger.Error("/admin/settings/update.aspx.cs2:btn_a_Click:serverCacheClientManager.ClearServerCacheAllSubItems:{0}", ex2.ToString());
					}
					try
					{
						ICacheStorageManager cacheStorageManager = CacheStorageManager.Current;
						cacheStorageManager.ClearCache();
					}
					catch
					{
					}
					ObjectFactory.Resolve<ClientCache>().ClearCache();
					ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
					clientCache.AuthenticationMode = eAuthenticationMode.PerSession;
					clientCache.ApplicationContext = new ApplicationContext
					{
						ExecutingPath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "bin")
					};
					string appSettingsByNameUsingProtection = ClockWorkConfigurationManager.GetAppSettingsByNameUsingProtection("instancename");
					clientCache.InstanceName = (string.IsNullOrEmpty(appSettingsByNameUsingProtection) ? "ClockWork" : appSettingsByNameUsingProtection);
					text3 = "Done.";
					CWLogger.Logger.Info("Cache was reset by: {0}", this.txt_u.Text);
					CWLogger.Logger.Info("Cache was reset by: {0}", this.txt_u.Text);
				}
				else
				{
					text3 = "Failed.";
				}
				this.btn_a.Text = text3;
			}
		}

		// Token: 0x040008DD RID: 2269
		protected HtmlForm form1;

		// Token: 0x040008DE RID: 2270
		protected Label lbl_username;

		// Token: 0x040008DF RID: 2271
		protected TextBox txt_u;

		// Token: 0x040008E0 RID: 2272
		protected Label lbl_pass;

		// Token: 0x040008E1 RID: 2273
		protected TextBox txt_p;

		// Token: 0x040008E2 RID: 2274
		protected Button btn_a;
	}
}
