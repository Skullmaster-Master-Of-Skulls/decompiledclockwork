using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.LookupCourses;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.LookupCourses;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;
using TechnoPro.Common.UI.Web.Mappers.LookupCourses;
using TechnoPro.Common.UI.Web.Veterans.Controls;

namespace TechnoPro.ClockWorkWeb.user.vet
{
	// Token: 0x02000030 RID: 48
	public class user_vet_Default : Page
	{
		// Token: 0x0600012E RID: 302 RVA: 0x0000925C File Offset: 0x0000745C
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = this.CheckIsAuthenticated();
			bool flag2 = !flag;
			if (flag2)
			{
				NavigatorClientManager.CurrentInstance.SetReturnUrlSpecific("/user/vet/default.aspx");
				base.Response.Redirect("login.aspx", true);
			}
			bool flag3 = !this.Page.IsPostBack;
			if (flag3)
			{
				string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.VETERANS_ApplicationStatusTitle);
				string settingValue2 = new WebSettingsClientManager().GetSettingValue<string>(Setting.VETERANS_ApplicationStatusIntro);
				this.lbl_title.Text = settingValue;
				this.lbl_info.Text = settingValue2;
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000092F0 File Offset: 0x000074F0
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
			object obj = this.Session["VetSelectedSession"];
			bool flag = obj == null;
			if (flag)
			{
				ISessionClientManager sessionClientManager = new SessionClientManager();
				SessionView currentSession = sessionClientManager.GetCurrentSession();
				this.Session.Add("VetSelectedSession", currentSession);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000934C File Offset: 0x0000754C
		private int Pid
		{
			get
			{
				return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00009370 File Offset: 0x00007570
		private bool CheckIsAuthenticated()
		{
			int studentPid = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
			return true;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00009394 File Offset: 0x00007594
		protected void OnStudentPidRequested_Click(object sender, StudentPidRequestEventArgs e)
		{
			e.Pid = this.Pid;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000093A4 File Offset: 0x000075A4
		private SessionDTO session
		{
			get
			{
				SessionView selectedSession = this.taskList.GetSelectedSession();
				return selectedSession.ToDTO();
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000093C8 File Offset: 0x000075C8
		protected void OnSessionRequested_Click(object sender, SessionRequestEventArgs e)
		{
			SessionDTO session = this.session;
			e.Session = session;
			bool flag = session.EndDate < DateTime.Now.Date;
			if (flag)
			{
			}
		}

		// Token: 0x040000E6 RID: 230
		private SessionDTO _session;

		// Token: 0x040000E7 RID: 231
		protected Label lbl_title;

		// Token: 0x040000E8 RID: 232
		protected Label lbl_info;

		// Token: 0x040000E9 RID: 233
		protected Panel p_noChangesMessage;

		// Token: 0x040000EA RID: 234
		protected Label lbl_noChangesMessage;

		// Token: 0x040000EB RID: 235
		protected CtrlTaskCheckList taskList;
	}
}
