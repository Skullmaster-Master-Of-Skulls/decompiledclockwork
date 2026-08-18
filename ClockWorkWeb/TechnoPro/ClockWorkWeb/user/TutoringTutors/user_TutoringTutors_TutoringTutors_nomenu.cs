using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.Entity;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.TutoringTutors
{
	// Token: 0x02000049 RID: 73
	public class user_TutoringTutors_TutoringTutors_nomenu : MasterPage, IClockWorkMasterPage
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x0000BD58 File Offset: 0x00009F58
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			LicensingClientWebClientManager.CurrentInstance.CheckIsModuleLicensed(Group.TUTORING);
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000BD95 File Offset: 0x00009F95
		public void SetCurrentPage(eClockWorkWebPage page)
		{
			this.ctrlMenu1.SetCurrentPage(page);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000BDA5 File Offset: 0x00009FA5
		public void SetCausesValidationForAllMenuItems(bool newCausesValidation)
		{
			this.ctrlMenu1.SetCausesValidationForAllMenuItems(newCausesValidation);
		}

		// Token: 0x04000165 RID: 357
		protected RadScriptManager RadScriptManager1;

		// Token: 0x04000166 RID: 358
		protected ctrls_CtrlMenu ctrlMenu1;

		// Token: 0x04000167 RID: 359
		protected ContentPlaceHolder placeholder_content;
	}
}
