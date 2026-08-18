using System;
using System.Web.UI;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.staff.schedule
{
	// Token: 0x02000105 RID: 261
	public class staff_schedule_login : Page
	{
		// Token: 0x060007B2 RID: 1970 RVA: 0x00005AEE File Offset: 0x00003CEE
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, true);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x000391C0 File Offset: 0x000373C0
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			this.ClockWorkLoginControl1.OverrideExternalCollectCredentialsUrl = true;
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				this.HideMenu();
			}
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00039204 File Offset: 0x00037404
		private void HideMenu()
		{
			bool flag = base.Master == null;
			if (!flag)
			{
				ctrls_CtrlMenu ctrls_CtrlMenu = this.FindFirstCtrlMenu(base.Master.Controls);
				bool flag2 = ctrls_CtrlMenu != null;
				if (flag2)
				{
					ctrls_CtrlMenu.Visible = false;
				}
			}
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00039244 File Offset: 0x00037444
		private ctrls_CtrlMenu FindFirstCtrlMenu(ControlCollection controls)
		{
			foreach (object obj in controls)
			{
				Control control = (Control)obj;
				bool flag = control is ctrls_CtrlMenu;
				if (flag)
				{
					return (ctrls_CtrlMenu)control;
				}
				bool flag2 = control.Controls.Count < 1;
				if (!flag2)
				{
					ctrls_CtrlMenu ctrls_CtrlMenu = this.FindFirstCtrlMenu(control.Controls);
					bool flag3 = ctrls_CtrlMenu != null;
					if (flag3)
					{
						return ctrls_CtrlMenu;
					}
				}
			}
			return null;
		}

		// Token: 0x040005EA RID: 1514
		protected ScriptManager bbb;

		// Token: 0x040005EB RID: 1515
		protected ClockWorkLoginControl ClockWorkLoginControl1;
	}
}
