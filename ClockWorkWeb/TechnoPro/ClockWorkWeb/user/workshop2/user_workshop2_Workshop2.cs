using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.Entity;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.workshop2
{
	// Token: 0x02000027 RID: 39
	public class user_workshop2_Workshop2 : MasterPage, IClockWorkMasterPage
	{
		// Token: 0x060000DF RID: 223 RVA: 0x00006836 File Offset: 0x00004A36
		public void SetCurrentPage(eClockWorkWebPage page)
		{
			this.ctrlMenu1.SetCurrentPage(page);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00006846 File Offset: 0x00004A46
		public void SetCausesValidationForAllMenuItems(bool newCausesValidation)
		{
			this.ctrlMenu1.SetCausesValidationForAllMenuItems(newCausesValidation);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00006858 File Offset: 0x00004A58
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			bool settingValue = SettingManager.GetInstance().GetSettingValue<bool>(Setting.MODULES_ENABLED_Workshops);
			bool flag = !settingValue;
			if (flag)
			{
				base.Response.Redirect("~/user/misc/NotAllowed.aspx?code=module", true);
			}
			LicensingClientWebClientManager.CurrentInstance.CheckIsModuleLicensed(TechnoPro.Common.Public.Entities.Settings.Group.WORKSHOPS);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000068AC File Offset: 0x00004AAC
		protected void RadMenu1_ItemClick(object sender, RadMenuEventArgs e)
		{
			bool flag = e.Item.Value.Equals("logout.aspx");
			if (flag)
			{
				WebAuthenticationAuthorizationWebClientManager.CurrentInstance.Logout();
			}
		}

		// Token: 0x040000A4 RID: 164
		protected HiddenField overridenocache;

		// Token: 0x040000A5 RID: 165
		protected ScriptManager bbb;

		// Token: 0x040000A6 RID: 166
		protected ctrls_CtrlMenu ctrlMenu1;

		// Token: 0x040000A7 RID: 167
		protected Label lbl_mainMessage;

		// Token: 0x040000A8 RID: 168
		protected ContentPlaceHolder placeholder_content;
	}
}
