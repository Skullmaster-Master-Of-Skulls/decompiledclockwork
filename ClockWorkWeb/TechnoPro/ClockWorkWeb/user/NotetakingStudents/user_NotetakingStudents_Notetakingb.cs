using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.NotetakingStudents
{
	// Token: 0x0200009A RID: 154
	public class user_NotetakingStudents_Notetakingb : MasterPage, IClockWorkMasterPage
	{
		// Token: 0x060004FB RID: 1275 RVA: 0x0002462E File Offset: 0x0002282E
		public void SetCurrentPage(eClockWorkWebPage page)
		{
			this.ctrlMenu1.SetCurrentPage(page);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0002463E File Offset: 0x0002283E
		public void SetCausesValidationForAllMenuItems(bool newCausesValidation)
		{
			this.ctrlMenu1.SetCausesValidationForAllMenuItems(newCausesValidation);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00024650 File Offset: 0x00022850
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.MODULES_ENABLED_Notetakingb);
			bool flag = !settingValue;
			if (flag)
			{
				base.Response.Redirect("~/user/misc/NotAllowed.aspx?code=module", true);
			}
			bool flag2 = !this.Page.IsPostBack;
			if (flag2)
			{
			}
		}

		// Token: 0x040002DE RID: 734
		protected HiddenField overridenocache;

		// Token: 0x040002DF RID: 735
		protected ctrls_CtrlMenu ctrlMenu1;

		// Token: 0x040002E0 RID: 736
		protected Label lbl_mainMessage;

		// Token: 0x040002E1 RID: 737
		protected ContentPlaceHolder placeholder_content;
	}
}
