using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ClockWorkWebAPIWeb;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.ClientManager.ICore.Tutoring;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Tutoring;

namespace TechnoPro.ClockWorkWeb.ctrls.Tutoring.Tutee
{
	// Token: 0x02000134 RID: 308
	public class ctrls_Tutoring_Tutee_FrmTutorProfileViewForStudent : Page
	{
		// Token: 0x06000922 RID: 2338 RVA: 0x000417AC File Offset: 0x0003F9AC
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				string text = base.Request.QueryString["tid"];
				int num;
				bool flag2 = !string.IsNullOrEmpty(text) && int.TryParse(text, out num) && num > 0;
				if (flag2)
				{
					ITutorClientManager tutorClientManager = new TutorWebClientManager();
					TutorDTO tutorDTO = tutorClientManager.LoadTutorById(num);
					bool flag3 = tutorDTO != null;
					if (flag3)
					{
						this.lbl_fn.Text = (tutorDTO.FirstName ?? "");
						this.lbl_ln.Text = (tutorDTO.LastName ?? "");
					}
					IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
					int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.TUTORING_BioFormNum);
					DynamicControlLayoutHelper dynamicControlLayoutHelper = null;
					int settingValue2 = webSettingsClientManager.GetSettingValue<int>(Setting.TUTORING_TutorIsAuthorizedCid);
					List<int> list = new List<int>
					{
						settingValue2
					};
					string exemptCids = string.Join(",", list.ConvertAll<string>((int g) => g.ToString()).ToArray());
					DynamicScreenLayout.ControlsToScreen(ref dynamicControlLayoutHelper, base.Cache, settingValue, this.p_data, null, false, true, exemptCids);
					DynamicScreenLayout.FillScreenWithPerStudentData(this.p_data, settingValue, num, base.Cache, exemptCids);
				}
			}
		}

		// Token: 0x0400071B RID: 1819
		protected HtmlForm form1;

		// Token: 0x0400071C RID: 1820
		protected ScriptManager bbb;

		// Token: 0x0400071D RID: 1821
		protected Panel p_name;

		// Token: 0x0400071E RID: 1822
		protected Label lbl_fn;

		// Token: 0x0400071F RID: 1823
		protected Label lbl_ln;

		// Token: 0x04000720 RID: 1824
		protected Panel p_data;
	}
}
