using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI.AuthenticationAuthorization;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;

namespace TechnoPro.ClockWorkWeb.user.misc
{
	// Token: 0x020000B7 RID: 183
	public class user_misc_home : Page
	{
		// Token: 0x0600059F RID: 1439 RVA: 0x00029FA4 File Offset: 0x000281A4
		protected void Page_Load(object sender, EventArgs e)
		{
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			ClockWorkIdentity currentClockWorkIdentity_LoginIfNecessary = webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity_LoginIfNecessary(this.Page, ClockWorkWebAPI.AuthenticationAuthorization.GroupMembership.instructors | ClockWorkWebAPI.AuthenticationAuthorization.GroupMembership.altcontact, false);
			int num = (currentClockWorkIdentity_LoginIfNecessary == null) ? 0 : currentClockWorkIdentity_LoginIfNecessary.InstructorId;
			int num2 = (currentClockWorkIdentity_LoginIfNecessary == null) ? 0 : currentClockWorkIdentity_LoginIfNecessary.AlternateContactId;
			bool flag = num > 0 && num2 > 0;
			if (flag)
			{
				int num3 = (currentClockWorkIdentity_LoginIfNecessary == null) ? 0 : currentClockWorkIdentity_LoginIfNecessary.PersonId;
				bool flag2 = num3 < 1;
				if (flag2)
				{
					base.Response.Redirect("~/user/instructor/courses.aspx", true);
				}
			}
		}

		// Token: 0x04000402 RID: 1026
		protected Label lbl_homeWelcome;

		// Token: 0x04000403 RID: 1027
		protected HyperLink link1;

		// Token: 0x04000404 RID: 1028
		protected HyperLink link2;

		// Token: 0x04000405 RID: 1029
		protected HyperLink link3;

		// Token: 0x04000406 RID: 1030
		protected HyperLink link4;
	}
}
