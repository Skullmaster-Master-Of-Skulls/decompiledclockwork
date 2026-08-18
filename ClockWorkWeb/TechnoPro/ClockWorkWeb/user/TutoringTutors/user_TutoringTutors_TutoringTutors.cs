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
	// Token: 0x02000048 RID: 72
	public class user_TutoringTutors_TutoringTutors : MasterPage, IClockWorkMasterPage
	{
		// Token: 0x060001BD RID: 445 RVA: 0x0000BD18 File Offset: 0x00009F18
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			LicensingClientWebClientManager.CurrentInstance.CheckIsModuleLicensed(Group.TUTORING);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000BD36 File Offset: 0x00009F36
		public void SetCurrentPage(eClockWorkWebPage page)
		{
			this.ctrlMenu1.SetCurrentPage(page);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000BD46 File Offset: 0x00009F46
		public void SetCausesValidationForAllMenuItems(bool newCausesValidation)
		{
			this.ctrlMenu1.SetCausesValidationForAllMenuItems(newCausesValidation);
		}

		// Token: 0x04000162 RID: 354
		protected ctrls_CtrlMenu ctrlMenu1;

		// Token: 0x04000163 RID: 355
		protected ContentPlaceHolder placeholder_content;

		// Token: 0x04000164 RID: 356
		protected RadScriptManager RadScriptManager1;
	}
}
