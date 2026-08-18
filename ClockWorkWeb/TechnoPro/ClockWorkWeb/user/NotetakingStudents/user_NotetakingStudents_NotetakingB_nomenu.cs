using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.user.NotetakingStudents
{
	// Token: 0x0200009B RID: 155
	public class user_NotetakingStudents_NotetakingB_nomenu : MasterPage
	{
		// Token: 0x060004FF RID: 1279 RVA: 0x000246A7 File Offset: 0x000228A7
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
		}

		// Token: 0x040002E2 RID: 738
		protected ContentPlaceHolder placeholder_content;
	}
}
