using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.Entity;
using TechnoPro.Common.UI.Web.Entity.Web.EventArgs;

namespace TechnoPro.ClockWorkWeb.staff.schedule
{
	// Token: 0x02000108 RID: 264
	public class staff_schedule_ScheduleMaster : MasterPage, IClockWorkMasterPage
	{
		// Token: 0x060007C0 RID: 1984 RVA: 0x000246A7 File Offset: 0x000228A7
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x000397B8 File Offset: 0x000379B8
		protected void ctrlMenu1_OnBeforeAddMenuItem(object sender, AddMenuItemEventArgs e)
		{
			LicensingClientWebClientManager.CurrentInstance.CheckIsModuleLicensed(Group.STAFF);
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
			}
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x000397EA File Offset: 0x000379EA
		public void SetCurrentPage(eClockWorkWebPage page)
		{
			this.ctrlMenu1.SetCurrentPage(page);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x000397FA File Offset: 0x000379FA
		public void SetCausesValidationForAllMenuItems(bool newCausesValidation)
		{
			this.ctrlMenu1.SetCausesValidationForAllMenuItems(newCausesValidation);
		}

		// Token: 0x040005FA RID: 1530
		protected Label lbl_mainMessage;

		// Token: 0x040005FB RID: 1531
		protected ctrls_CtrlMenu ctrlMenu1;

		// Token: 0x040005FC RID: 1532
		protected ContentPlaceHolder placeholder_content;
	}
}
