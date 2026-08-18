using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using ClockWorkWebAPIWeb;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Core.Tutoring;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.TutoringTutors
{
	// Token: 0x02000041 RID: 65
	public class user_TutoringTutors_bio : Page
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0000B42C File Offset: 0x0000962C
		private int screenNum
		{
			get
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				return webSettingsClientManager.GetSettingValue<int>(Setting.TUTORING_BioFormNum);
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000B450 File Offset: 0x00009650
		protected void Page_Load(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
				tutoringClientWebClientManager.EnforceTutoringRedirects(pid, this.Page, eClockWorkWebPage.TutoringTutors_Calendar);
				bool flag2 = base.Master != null && base.Master is IClockWorkMasterPage;
				if (flag2)
				{
					((IClockWorkMasterPage)base.Master).SetCurrentPage(eClockWorkWebPage.TutoringTutors_Profile);
				}
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.TUTORING_TutorIsAuthorizedCid);
				List<int> list = new List<int>
				{
					settingValue
				};
				string exemptCids = string.Join(",", list.ConvertAll<string>((int g) => g.ToString()).ToArray());
				DynamicScreenLayout.FillScreenWithPerStudentData(this.p_data, this.screenNum, pid, base.Cache, exemptCids);
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000B538 File Offset: 0x00009738
		private void Page_Init(object sender, EventArgs e)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.TUTORING_TutorIsAuthorizedCid);
			List<int> list = new List<int>
			{
				settingValue
			};
			string exemptCids = string.Join(",", list.ConvertAll<string>((int g) => g.ToString()).ToArray());
			DynamicScreenLayout.ControlsToScreen(base.Cache, this.screenNum, this.p_data, null, false, false, exemptCids);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000B5B8 File Offset: 0x000097B8
		protected void btn_submit_Click(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.TUTORING_TutorIsAuthorizedCid);
			List<int> list = new List<int>
			{
				settingValue
			};
			string exemptCids = string.Join(",", list.ConvertAll<string>((int g) => g.ToString()).ToArray());
			DynamicScreenLayout.SaveDynamicData(ScreenType.ScreenType_PerStudent, pid, this.screenNum, base.Cache, this.p_data, exemptCids);
			base.Response.Redirect("TutorCalendar.aspx", true);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000A342 File Offset: 0x00008542
		protected void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("TutorCalendar.aspx", true);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000B654 File Offset: 0x00009854
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x0400013E RID: 318
		protected Panel p_data;

		// Token: 0x0400013F RID: 319
		protected Panel p_toolbar;

		// Token: 0x04000140 RID: 320
		protected Button btn_submit;

		// Token: 0x04000141 RID: 321
		protected Button btn_cancel;
	}
}
