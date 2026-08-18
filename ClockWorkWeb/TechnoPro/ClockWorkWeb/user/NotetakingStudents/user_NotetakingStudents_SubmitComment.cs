using System;
using System.Web.UI;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.NotetakingStudents
{
	// Token: 0x0200009D RID: 157
	public class user_NotetakingStudents_SubmitComment : Page
	{
		// Token: 0x06000508 RID: 1288 RVA: 0x00024A94 File Offset: 0x00022C94
		protected void Page_Load(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			bool flag = pid < 1;
			if (flag)
			{
				NavigatorClientManager.CurrentInstance.NotAllowed(Setting.GENERAL_ErrorMessage_NotAClockWorkStudent, this.Page);
			}
			else
			{
				bool flag2 = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
				if (flag2)
				{
					IClockWorkMasterPage clockWorkMasterPage = (IClockWorkMasterPage)this.Page.Master;
					clockWorkMasterPage.SetCurrentPage(eClockWorkWebPage.NotetakingStudents_SubmitComment);
					clockWorkMasterPage.SetCausesValidationForAllMenuItems(false);
				}
				string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_AdminEmail);
				bool flag3 = string.IsNullOrEmpty(settingValue);
				if (flag3)
				{
					settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.GENERAL_DefaultFrom_Notetaking);
				}
				this.cwSubmitComment1.Init(settingValue);
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00024B54 File Offset: 0x00022D54
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x040002ED RID: 749
		protected ScriptManager bbb;

		// Token: 0x040002EE RID: 750
		protected user_SubmitComment cwSubmitComment1;
	}
}
