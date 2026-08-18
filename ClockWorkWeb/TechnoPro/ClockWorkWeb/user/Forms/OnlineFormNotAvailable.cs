using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.Web.Entity.Adapters;
using TechnoPro.Common.UI.Web.Entity.OnlineForms;

namespace TechnoPro.ClockWorkWeb.user.Forms
{
	// Token: 0x020000ED RID: 237
	public class OnlineFormNotAvailable : Page
	{
		// Token: 0x060006F7 RID: 1783 RVA: 0x00035398 File Offset: 0x00033598
		protected void Page_Load(object sender, EventArgs e)
		{
			int integerFromQueryString = this.Page.GetIntegerFromQueryString("msgcode");
			switch (Enum.IsDefined(typeof(eOnlineFormNotAvailableReason), integerFromQueryString) ? integerFromQueryString : 0)
			{
			case 1:
			case 2:
			case 3:
			case 6:
			case 7:
				this.lbl_msg.Text = "The requested form could not be found.  This could indicate a problem with the link.";
				break;
			case 4:
				this.lbl_msg.Text = "The requested form isn't available yet.  Please check back again later.";
				break;
			case 5:
				this.lbl_msg.Text = "The requested form has been closed.";
				break;
			case 8:
				this.lbl_msg.Text = "You have already filled out this form.";
				break;
			default:
				this.lbl_msg.Text = "The requested form is not available.";
				break;
			}
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x04000545 RID: 1349
		protected Label lbl_msg;

		// Token: 0x04000546 RID: 1350
		protected Button btn_home;

		// Token: 0x04000547 RID: 1351
		protected Button btn_forms;
	}
}
