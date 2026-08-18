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
	// Token: 0x0200010B RID: 267
	public class StaffMasterFullWidth : MasterPage, IClockWorkMasterPage
	{
		// Token: 0x060007E3 RID: 2019 RVA: 0x000246A7 File Offset: 0x000228A7
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0003A470 File Offset: 0x00038670
		protected void ctrlMenu1_OnBeforeAddMenuItem(object sender, AddMenuItemEventArgs e)
		{
			LicensingClientWebClientManager.CurrentInstance.CheckIsModuleLicensed(Group.STAFF);
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
			}
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0003A4A2 File Offset: 0x000386A2
		public void SetCurrentPage(eClockWorkWebPage page)
		{
			this.ctrlMenu1.SetCurrentPage(page);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0003A4B2 File Offset: 0x000386B2
		public void SetCausesValidationForAllMenuItems(bool newCausesValidation)
		{
			this.ctrlMenu1.SetCausesValidationForAllMenuItems(newCausesValidation);
		}

		// Token: 0x04000616 RID: 1558
		protected Label lbl_mainMessage;

		// Token: 0x04000617 RID: 1559
		protected ctrls_CtrlMenu ctrlMenu1;

		// Token: 0x04000618 RID: 1560
		protected ContentPlaceHolder placeholder_content;
	}
}
