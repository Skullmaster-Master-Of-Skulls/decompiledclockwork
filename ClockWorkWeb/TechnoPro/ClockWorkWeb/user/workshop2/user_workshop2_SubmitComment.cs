using System;
using System.Web.UI;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.workshop2
{
	// Token: 0x02000025 RID: 37
	public class user_workshop2_SubmitComment : Page
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00006794 File Offset: 0x00004994
		protected void Page_Load(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			bool flag = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
			if (flag)
			{
				IClockWorkMasterPage clockWorkMasterPage = (IClockWorkMasterPage)this.Page.Master;
				clockWorkMasterPage.SetCurrentPage(eClockWorkWebPage.NotetakingStudents_SubmitComment);
				clockWorkMasterPage.SetCausesValidationForAllMenuItems(false);
			}
			string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.GENERAL_DefaultFrom_Workshops);
			this.cwSubmitComment1.Init(settingValue);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00006814 File Offset: 0x00004A14
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x040000A3 RID: 163
		protected user_SubmitComment cwSubmitComment1;
	}
}
