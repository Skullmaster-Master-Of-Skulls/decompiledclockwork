using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Cache;
using TechnoPro.Common.UI.Web.AccommodationsRequest.Controls;

namespace TechnoPro.ClockWorkWeb.user.SelfRegC
{
	// Token: 0x02000089 RID: 137
	public class user_SelfRegC_request : Page
	{
		// Token: 0x0600049D RID: 1181 RVA: 0x0002124C File Offset: 0x0001F44C
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00003E0A File Offset: 0x0000200A
		private void Page_Init(object sender, EventArgs e)
		{
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00021270 File Offset: 0x0001F470
		protected void Page_Load(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			bool flag = pid < 1;
			if (flag)
			{
			}
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00021290 File Offset: 0x0001F490
		protected void OnButtonCancelClicked(object sender, AbortSelfRegHandler e)
		{
			bool flag = !string.IsNullOrEmpty(e.SelfRegCMsgCode);
			if (flag)
			{
				SessionCaching.CurrentInstance.Insert("selfregc_msgcode", e.SelfRegCMsgCode);
			}
			base.Response.Redirect("courses.aspx", true);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000212D8 File Offset: 0x0001F4D8
		protected void OnButtonSubmitClicked(object sender, EventArgs e)
		{
			SessionCaching.CurrentInstance.Insert("selfregc_msgcode", "0");
			base.Response.Redirect("courses.aspx", true);
		}

		// Token: 0x0400027C RID: 636
		protected Panel p_data;

		// Token: 0x0400027D RID: 637
		protected CtrlAccommodationRequestGroup ctrlGroup;
	}
}
