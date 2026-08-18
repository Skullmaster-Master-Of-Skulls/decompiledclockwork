using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.user.SelfRegC
{
	// Token: 0x0200008C RID: 140
	public class user_SelfRegC_SelfRegCMaster_nomenu : MasterPage
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x00021377 File Offset: 0x0001F577
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			LicensingClientWebClientManager.CurrentInstance.CheckIsModuleLicensed(Group.SELFREGC);
		}

		// Token: 0x04000284 RID: 644
		protected ScriptManager bbb;

		// Token: 0x04000285 RID: 645
		protected ContentPlaceHolder placeholder_content;
	}
}
