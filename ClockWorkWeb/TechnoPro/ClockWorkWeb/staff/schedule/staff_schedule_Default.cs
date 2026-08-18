using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI.AuthenticationAuthorization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.Core.Authentication;
using TechnoPro.Common.ClientManager.Core.People;
using TechnoPro.Common.ClientManager.ICore.Authentication;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;
using TechnoPro.Common.UI.Web.Entity;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;

namespace TechnoPro.ClockWorkWeb.staff.schedule
{
	// Token: 0x02000104 RID: 260
	public class staff_schedule_Default : Page
	{
		// Token: 0x060007AF RID: 1967 RVA: 0x00039080 File Offset: 0x00037280
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = base.Master != null && base.Master is IClockWorkMasterPage;
			if (flag)
			{
				((IClockWorkMasterPage)base.Master).SetCurrentPage(eClockWorkWebPage.Staff_Help);
			}
			this.CheckLoggedIn();
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x000390C8 File Offset: 0x000372C8
		private void CheckLoggedIn()
		{
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			ClockWorkIdentity currentClockWorkIdentity_LoginIfNecessary = webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity_LoginIfNecessary(this.Page, ClockWorkWebAPI.AuthenticationAuthorization.GroupMembership.staff, true, false);
			bool flag = currentClockWorkIdentity_LoginIfNecessary == null || currentClockWorkIdentity_LoginIfNecessary.PersonId < 1;
			if (flag)
			{
				base.Response.Redirect("~/custom/misc/home.aspx", true);
			}
			else
			{
				IPersonBaseClientManager personBaseClientManager = new PersonBaseClientManager();
				PersonBaseDTO personBaseDTO = personBaseClientManager.LoadPerson(currentClockWorkIdentity_LoginIfNecessary.PersonId);
				bool flag2;
				if (personBaseDTO != null)
				{
					if (personBaseDTO.CoreGroup != eCoreGroupDTO.Admin)
					{
						if (personBaseDTO.Groups != null)
						{
							flag2 = (personBaseDTO.Groups.FirstOrDefault((GroupDTO g) => g.GroupId == 10) != null);
						}
						else
						{
							flag2 = false;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag2 = false;
				}
				bool flag3 = flag2;
				bool flag4 = flag3;
				if (!flag4)
				{
					IClockWorkAuthenticationClientManager clockWorkAuthenticationClientManager = new ClockWorkAuthenticationClientManager();
					flag3 = clockWorkAuthenticationClientManager.IsUserAdminOrInSettingsListOfStaffPidsAllowedToLoginAsAnother(currentClockWorkIdentity_LoginIfNecessary.PersonId);
					bool flag5 = !flag3;
					if (flag5)
					{
						base.Response.Redirect("~/custom/misc/home.aspx", true);
					}
				}
			}
		}

		// Token: 0x040005E3 RID: 1507
		protected ScriptManager bbb;

		// Token: 0x040005E4 RID: 1508
		protected HyperLink lnk_home;

		// Token: 0x040005E5 RID: 1509
		protected Image img1;

		// Token: 0x040005E6 RID: 1510
		protected Label lbl1;

		// Token: 0x040005E7 RID: 1511
		protected HyperLink link_loginOptions;

		// Token: 0x040005E8 RID: 1512
		protected Image img2;

		// Token: 0x040005E9 RID: 1513
		protected Label lbl2;
	}
}
