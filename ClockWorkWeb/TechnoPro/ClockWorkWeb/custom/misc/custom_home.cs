using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.custom.misc
{
	// Token: 0x0200011B RID: 283
	public class custom_home : Page
	{
		// Token: 0x06000826 RID: 2086 RVA: 0x0003B27C File Offset: 0x0003947C
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.LOGIN_ForceAuthenticationRequiredForAllPages);
			bool flag = settingValue;
			if (flag)
			{
				int studentPid = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
			}
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x0400062A RID: 1578
		protected Panel p_title;

		// Token: 0x0400062B RID: 1579
		protected Label lbl_title;

		// Token: 0x0400062C RID: 1580
		protected Panel p_intro;

		// Token: 0x0400062D RID: 1581
		protected Label lbl_intro;
	}
}
