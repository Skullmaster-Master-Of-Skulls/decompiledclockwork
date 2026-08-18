using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.NotetakingNotetakers
{
	// Token: 0x020000AF RID: 175
	public class user_NotetakingNotetakers_Notetakingb : MasterPage, IClockWorkMasterPage
	{
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00028670 File Offset: 0x00026870
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x00028688 File Offset: 0x00026888
		public bool DisableNoCache
		{
			get
			{
				return this.disableNoCache;
			}
			set
			{
				this.disableNoCache = value;
			}
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00028692 File Offset: 0x00026892
		public void SetCurrentPage(eClockWorkWebPage page)
		{
			this.ctrlMenu1.SetCurrentPage(page);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x000286A2 File Offset: 0x000268A2
		public void SetCausesValidationForAllMenuItems(bool newCausesValidation)
		{
			this.ctrlMenu1.SetCausesValidationForAllMenuItems(newCausesValidation);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x000286B4 File Offset: 0x000268B4
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.DisableNoCache;
			if (flag)
			{
				NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			}
			else
			{
				this.DisableNoCache = false;
			}
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.MODULES_ENABLED_Notetakingb);
			bool flag2 = !settingValue;
			if (flag2)
			{
				base.Response.Redirect("~/user/misc/NotAllowed.aspx?code=module", true);
			}
			LicensingClientWebClientManager.CurrentInstance.CheckIsModuleLicensed(TechnoPro.Common.Public.Entities.Settings.Group.NOTETAKING);
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			bool flag3 = !this.Page.IsPostBack;
			if (flag3)
			{
				string authenticatedUsername = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetAuthenticatedUsername(this.Page);
				string[] settingValue2 = new WebSettingsClientManager().GetSettingValue<string[]>(Setting.NOTETAKINGB_RestrictLoginTo_Usernames);
				bool flag4 = settingValue2 != null && settingValue2.Length != 0;
				if (flag4)
				{
					bool flag5 = false;
					foreach (string text in settingValue2)
					{
						bool flag6 = text.Equals(authenticatedUsername, StringComparison.OrdinalIgnoreCase);
						if (flag6)
						{
							flag5 = true;
							break;
						}
					}
					bool flag7 = !flag5;
					if (flag7)
					{
						NavigatorClientManager.CurrentInstance.NotAllowed(Setting.INSTRUCTOR_ErrorMessage_Pilot, this.Page);
					}
				}
			}
		}

		// Token: 0x04000398 RID: 920
		private bool disableNoCache = false;

		// Token: 0x04000399 RID: 921
		protected HiddenField overridenocache;

		// Token: 0x0400039A RID: 922
		protected ctrls_CtrlMenu ctrlMenu1;

		// Token: 0x0400039B RID: 923
		protected Label lbl_mainMessage;

		// Token: 0x0400039C RID: 924
		protected ContentPlaceHolder placeholder_content;
	}
}
