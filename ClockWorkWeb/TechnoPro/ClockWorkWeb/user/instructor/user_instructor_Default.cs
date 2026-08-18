using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;
using TechnoPro.Common.UI.Web.Entity;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.instructor
{
	// Token: 0x020000CF RID: 207
	public class user_instructor_Default : Page
	{
		// Token: 0x060005FF RID: 1535 RVA: 0x0002C52C File Offset: 0x0002A72C
		private void Page_Init(object sender, EventArgs e)
		{
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.INSTRUCTOR_ForceLoginOnInstructionsDefaultPage);
			bool flag = !settingValue;
			if (flag)
			{
				WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0002C564 File Offset: 0x0002A764
		protected void Page_Load(object sender, EventArgs e)
		{
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			ClockWorkIdentity currentClockWorkIdentity = webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity(this.Page);
			bool flag = currentClockWorkIdentity != null;
			if (flag)
			{
				int instructorId = currentClockWorkIdentity.InstructorId;
				int alternateContactId = currentClockWorkIdentity.AlternateContactId;
				bool flag2 = instructorId < 1 && alternateContactId < 1;
				if (flag2)
				{
					int personId = currentClockWorkIdentity.PersonId;
					bool flag3 = personId > 0;
					if (flag3)
					{
						base.Response.Redirect("../../custom/misc/home.aspx", true);
					}
				}
			}
			bool flag4 = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
			if (flag4)
			{
				((IClockWorkMasterPage)this.Page.Master).SetCurrentPage(eClockWorkWebPage.Instructor_Help);
			}
			bool flag5 = !this.Page.IsPostBack;
			if (flag5)
			{
				RadMenu radMenu = (RadMenu)base.Master.FindControl("RadMenu1");
				RadMenuItem radMenuItem = (radMenu != null) ? radMenu.Items.FindItemByValue("info") : null;
				bool flag6 = radMenuItem != null;
				if (flag6)
				{
					radMenuItem.CssClass = "Alert";
				}
				this.lbl_intro.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.INSTRUCTOR_WelcomeMessage);
			}
		}

		// Token: 0x0400045E RID: 1118
		protected Label lbl_intro;
	}
}
