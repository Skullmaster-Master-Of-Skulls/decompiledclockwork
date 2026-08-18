using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.Web.Entity.Adapters;
using TechnoPro.Common.UI.Web.Entity.Survey;

namespace TechnoPro.ClockWorkWeb.user.survey
{
	// Token: 0x02000078 RID: 120
	public class SurveyNotAvailable : Page
	{
		// Token: 0x06000462 RID: 1122 RVA: 0x000201BC File Offset: 0x0001E3BC
		protected void Page_Load(object sender, EventArgs e)
		{
			int integerFromQueryString = this.Page.GetIntegerFromQueryString("msgcode");
			switch (Enum.IsDefined(typeof(eSurveyNotAvailableReason), integerFromQueryString) ? integerFromQueryString : 0)
			{
			case 1:
			case 2:
			case 3:
			case 6:
			case 7:
				this.lbl_msg.Text = "The requested survey could not be found.  This could indicate a problem with the link.";
				break;
			case 4:
				this.lbl_msg.Text = "The requested survey hasn't started yet.  Please check back again later.";
				break;
			case 5:
				this.lbl_msg.Text = "The requested survey has been closed.";
				break;
			case 8:
				this.lbl_msg.Text = "You have already filled out this survey.";
				break;
			default:
				this.lbl_msg.Text = "The requested survey is not available.";
				break;
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x0400023C RID: 572
		protected Label lbl_msg;

		// Token: 0x0400023D RID: 573
		protected Button btn_home;
	}
}
