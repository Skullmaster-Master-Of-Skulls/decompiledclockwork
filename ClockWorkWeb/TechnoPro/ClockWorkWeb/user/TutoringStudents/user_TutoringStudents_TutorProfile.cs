using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPIWeb;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.Core.People;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.TutoringStudents
{
	// Token: 0x02000060 RID: 96
	public class user_TutoringStudents_TutorProfile : Page
	{
		// Token: 0x0600024E RID: 590 RVA: 0x0000DC78 File Offset: 0x0000BE78
		protected void Page_Load(object sender, EventArgs e)
		{
			int num = this.LookupStudentPid();
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				bool flag2 = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
				if (flag2)
				{
					((IClockWorkMasterPage)this.Page.Master).SetCurrentPage(eClockWorkWebPage.TutoringStudents_MyTutors);
				}
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				int screenNum = webSettingsClientManager.GetSettingValue<int>(Setting.TUTORING_BioFormNum);
				int tutorPid = this.GetTutorPid();
				screenNum = 8;
				IPeopleClientManager peopleClientManager = new PeopleClientManager();
				PersonBaseDTO personBaseDTO = peopleClientManager.LoadPersonById(tutorPid);
				bool flag3 = personBaseDTO == null;
				if (flag3)
				{
					base.Response.Redirect("default.aspx");
				}
				this.lblTitle.Text = "Tutor profile: " + personBaseDTO.GetName();
				string exemptCids = "";
				DynamicControlLayoutHelper dynamicControlLayoutHelper = null;
				DynamicScreenLayout.ControlsToScreen(ref dynamicControlLayoutHelper, base.Cache, screenNum, this.p_data, null, false, false, exemptCids);
				DynamicScreenLayout.FillScreenWithPerStudentData(this.p_data, screenNum, tutorPid, base.Cache, null);
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000DD88 File Offset: 0x0000BF88
		private int GetTutorPid()
		{
			return NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["tpid"]);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000DDBC File Offset: 0x0000BFBC
		private int LookupStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000DDE0 File Offset: 0x0000BFE0
		protected void btn_calendar_Click(object sender, EventArgs e)
		{
			int tutorPid = this.GetTutorPid();
			base.Response.Redirect("TutorCalendar.aspx?id=" + ClockWorkWebCore.EncodeUrlVariable(tutorPid.ToString(), true), true);
		}

		// Token: 0x040001B7 RID: 439
		protected Label lblTitle;

		// Token: 0x040001B8 RID: 440
		protected Button btn_calendar;

		// Token: 0x040001B9 RID: 441
		protected Panel p_data;

		// Token: 0x040001BA RID: 442
		protected Label lblData;
	}
}
