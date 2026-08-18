using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.TutoringStudents
{
	// Token: 0x0200005F RID: 95
	public class user_TutoringStudents_TutoringStudents_nomenu : MasterPage, IClockWorkMasterPage
	{
		// Token: 0x0600024A RID: 586 RVA: 0x0000BD18 File Offset: 0x00009F18
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			LicensingClientWebClientManager.CurrentInstance.CheckIsModuleLicensed(Group.TUTORING);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000DC55 File Offset: 0x0000BE55
		public void SetCurrentPage(eClockWorkWebPage page)
		{
			this.ctrlMenu1.SetCurrentPage(page);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000DC65 File Offset: 0x0000BE65
		public void SetCausesValidationForAllMenuItems(bool newCausesValidation)
		{
			this.ctrlMenu1.SetCausesValidationForAllMenuItems(newCausesValidation);
		}

		// Token: 0x040001B4 RID: 436
		protected ScriptManager bbb;

		// Token: 0x040001B5 RID: 437
		protected ctrls_CtrlMenu ctrlMenu1;

		// Token: 0x040001B6 RID: 438
		protected ContentPlaceHolder placeholder_content;
	}
}
