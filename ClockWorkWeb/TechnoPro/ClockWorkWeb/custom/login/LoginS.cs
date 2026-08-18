using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.Authentication;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;

namespace TechnoPro.ClockWorkWeb.custom.login
{
	// Token: 0x0200011E RID: 286
	public class LoginS : Page
	{
		// Token: 0x06000830 RID: 2096 RVA: 0x0003B3E8 File Offset: 0x000395E8
		private void DisplayDictionary(string title, IDictionary<string, string> args)
		{
			base.Response.Write("<h1>" + title + "</h1>");
			bool flag = args == null;
			if (flag)
			{
				base.Response.Write("args is null");
			}
			else
			{
				foreach (KeyValuePair<string, string> keyValuePair in args)
				{
					base.Response.Write(keyValuePair.Key + "=" + (keyValuePair.Value ?? "null"));
				}
			}
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0003B494 File Offset: 0x00039694
		private string GetDictionaryDisplayString(IDictionary<string, string> args)
		{
			bool flag = args == null;
			string result;
			if (flag)
			{
				result = "NULL";
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (KeyValuePair<string, string> keyValuePair in args)
				{
					stringBuilder.AppendLine(keyValuePair.Key + "=" + (keyValuePair.Value ?? "NULL"));
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0003B524 File Offset: 0x00039724
		protected void Page_Load(object sender, EventArgs e)
		{
			AuthenticationArgsDTO environmentVariables = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetEnvironmentVariables();
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			ClockWorkIdentity clockWorkIdentity = webAuthenticationAuthorizationWebClientManager.TryToLoginRightNowWithoutCredentials(environmentVariables);
			bool flag = clockWorkIdentity == null;
			if (flag)
			{
				CWLogger.Logger.Warn("/custom/login/LoginS.aspx:Page_Load:TryToLoginRightNowWithoutCredentialsFailed");
			}
			else
			{
				string defaultPage = "~/custom/misc/home.aspx";
				ClockWorkIdentity currentClockWorkIdentity = webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity(null);
				bool flag2 = currentClockWorkIdentity != null;
				if (flag2)
				{
					IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
					bool flag3 = currentClockWorkIdentity.InstructorId > 0 || currentClockWorkIdentity.AlternateContactId > 0;
					if (flag3)
					{
						bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.MODULES_ENABLED_Instructor);
						bool flag4 = settingValue;
						if (flag4)
						{
							defaultPage = "~/user/instructor/default.aspx";
						}
					}
					else
					{
						bool flag5 = currentClockWorkIdentity.NotetakerId > 0;
						if (flag5)
						{
							bool flag6 = webSettingsClientManager.GetSettingValue<bool>(Setting.MODULES_ENABLED_Notetakingb) || webSettingsClientManager.GetSettingValue<bool>(Setting.MODULES_ENABLED_Notetaking);
							bool flag7 = flag6;
							if (flag7)
							{
								defaultPage = "~/user/NotetakingNotetakers/default.aspx";
							}
						}
						else
						{
							bool settingValue2 = webSettingsClientManager.GetSettingValue<bool>(Setting.MODULES_ENABLED_Veterans);
							bool flag8 = settingValue2;
							if (flag8)
							{
								defaultPage = "~/user/vet/default.aspx";
							}
						}
					}
				}
				NavigatorClientManager.CurrentInstance.GotoLastReturnUrl(null, defaultPage);
			}
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x0400064E RID: 1614
		protected Panel p_info;

		// Token: 0x0400064F RID: 1615
		protected Label lbl_info;

		// Token: 0x04000650 RID: 1616
		protected TextBox txt;
	}
}
