using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.user.NotetakingNotetakers
{
	// Token: 0x020000B0 RID: 176
	public class user_NotetakingNotetakers_NotetakingB_nomenu : MasterPage
	{
		// Token: 0x0600057F RID: 1407 RVA: 0x000287E4 File Offset: 0x000269E4
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			LicensingClientWebClientManager.CurrentInstance.CheckIsModuleLicensed(TechnoPro.Common.Public.Entities.Settings.Group.NOTETAKING);
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				string authenticatedUsername = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetAuthenticatedUsername(this.Page);
				string[] settingValue = new WebSettingsClientManager().GetSettingValue<string[]>(Setting.NOTETAKINGB_RestrictLoginTo_Usernames);
				bool flag2 = settingValue != null && settingValue.Length != 0;
				if (flag2)
				{
					bool flag3 = false;
					foreach (string text in settingValue)
					{
						bool flag4 = text.Equals(authenticatedUsername, StringComparison.OrdinalIgnoreCase);
						if (flag4)
						{
							flag3 = true;
							break;
						}
					}
					bool flag5 = !flag3;
					if (flag5)
					{
						NavigatorClientManager.CurrentInstance.NotAllowed(Setting.INSTRUCTOR_ErrorMessage_Pilot, this.Page);
					}
				}
			}
		}

		// Token: 0x0400039D RID: 925
		protected ContentPlaceHolder placeholder_content;
	}
}
