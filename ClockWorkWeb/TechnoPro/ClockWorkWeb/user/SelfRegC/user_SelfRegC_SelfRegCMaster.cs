using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.SelfRegC
{
	// Token: 0x0200008B RID: 139
	public class user_SelfRegC_SelfRegCMaster : MasterPage, IClockWorkMasterPage
	{
		// Token: 0x060004A5 RID: 1189 RVA: 0x00021302 File Offset: 0x0001F502
		public void SetCurrentPage(eClockWorkWebPage page)
		{
			this.ctrlMenu1.SetCurrentPage(page);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00021312 File Offset: 0x0001F512
		public void SetCausesValidationForAllMenuItems(bool newCausesValidation)
		{
			this.ctrlMenu1.SetCausesValidationForAllMenuItems(newCausesValidation);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00021324 File Offset: 0x0001F524
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.MODULES_ENABLED_SelfReg);
			bool flag = !settingValue;
			if (flag)
			{
				base.Response.Redirect("~/user/misc/NotAllowed.aspx?code=module", true);
			}
			LicensingClientWebClientManager.CurrentInstance.CheckIsModuleLicensed(Group.SELFREGC);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void Page_PreInit(object sender, EventArgs e)
		{
		}

		// Token: 0x0400027F RID: 639
		protected HiddenField overridenocache;

		// Token: 0x04000280 RID: 640
		protected ScriptManager bbb;

		// Token: 0x04000281 RID: 641
		protected ctrls_CtrlMenu ctrlMenu1;

		// Token: 0x04000282 RID: 642
		protected Label lbl_mainMessage;

		// Token: 0x04000283 RID: 643
		protected ContentPlaceHolder placeholder_content;
	}
}
