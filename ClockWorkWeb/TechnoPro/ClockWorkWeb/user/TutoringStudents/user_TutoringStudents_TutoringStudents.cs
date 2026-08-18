using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.user.TutoringStudents
{
	// Token: 0x0200005E RID: 94
	public class user_TutoringStudents_TutoringStudents : MasterPage
	{
		// Token: 0x06000248 RID: 584 RVA: 0x0000BD18 File Offset: 0x00009F18
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			LicensingClientWebClientManager.CurrentInstance.CheckIsModuleLicensed(Group.TUTORING);
		}

		// Token: 0x040001B1 RID: 433
		protected ScriptManager bbb;

		// Token: 0x040001B2 RID: 434
		protected ctrls_CtrlMenu ctrlMenu1;

		// Token: 0x040001B3 RID: 435
		protected ContentPlaceHolder placeholder_content;
	}
}
