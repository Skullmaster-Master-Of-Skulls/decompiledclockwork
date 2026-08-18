using System;
using System.Web.UI;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.SelfRegC
{
	// Token: 0x0200008D RID: 141
	public class user_SelfRegC_SubmitComment : Page
	{
		// Token: 0x060004AC RID: 1196 RVA: 0x00021398 File Offset: 0x0001F598
		protected void Page_Load(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			bool flag = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
			if (flag)
			{
				IClockWorkMasterPage clockWorkMasterPage = (IClockWorkMasterPage)this.Page.Master;
				clockWorkMasterPage.SetCurrentPage(eClockWorkWebPage.SelfRegistration_SubmitComment);
				clockWorkMasterPage.SetCausesValidationForAllMenuItems(false);
			}
			string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.GENERAL_DefaultFrom_SelfRegistration);
			this.cwSubmitComment1.Init(settingValue);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00021418 File Offset: 0x0001F618
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x04000286 RID: 646
		protected user_SubmitComment cwSubmitComment1;
	}
}
