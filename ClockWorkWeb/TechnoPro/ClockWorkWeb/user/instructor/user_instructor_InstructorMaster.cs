using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI.AuthenticationAuthorization;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.Entity;
using TechnoPro.Common.UI.Web.Entity.Web.EventArgs;

namespace TechnoPro.ClockWorkWeb.user.instructor
{
	// Token: 0x020000DA RID: 218
	public class user_instructor_InstructorMaster : MasterPage, IClockWorkMasterPage
	{
		// Token: 0x06000690 RID: 1680 RVA: 0x00032690 File Offset: 0x00030890
		public void SetCurrentPage(eClockWorkWebPage page)
		{
			this.ctrlMenu1.SetCurrentPage(page);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x000326A0 File Offset: 0x000308A0
		public void SetCausesValidationForAllMenuItems(bool newCausesValidation)
		{
			this.ctrlMenu1.SetCausesValidationForAllMenuItems(newCausesValidation);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x000326B0 File Offset: 0x000308B0
		private int GetIidIfExists()
		{
			object obj = base.Session["userinfo"];
			bool flag = obj != null;
			int result;
			if (flag)
			{
				UserInfo userInfo = (UserInfo)obj;
				result = userInfo.ClockworkIid;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x000326F0 File Offset: 0x000308F0
		protected void ctrlMenu1_OnBeforeAddMenuItem(object sender, AddMenuItemEventArgs e)
		{
			eClockWorkWebPage menuItem = e.MenuItem;
			if (menuItem == eClockWorkWebPage.Instructor_AccommodationLetters)
			{
				bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.INSTRUCTOR_LettersEnabled);
				e.AbortAddingMenuItem = !settingValue;
			}
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0003272C File Offset: 0x0003092C
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			bool settingValue = SettingManager.GetInstance().GetSettingValue<bool>(Setting.MODULES_ENABLED_Instructor);
			bool flag = !settingValue;
			if (flag)
			{
				base.Response.Redirect("~/user/misc/NotAllowed.aspx?code=module", true);
			}
			LicensingClientWebClientManager.CurrentInstance.CheckIsModuleLicensed(TechnoPro.Common.Public.Entities.Settings.Group.INSTRUCTOR);
			string settingValue2 = SettingManager.GetInstance().GetSettingValue<string>(Setting.MODULES_MessageInstructor);
			bool flag2 = !string.IsNullOrEmpty(settingValue2);
			if (flag2)
			{
				this.lbl_mainMessage.Text = settingValue2;
			}
			bool flag3 = !this.Page.IsPostBack;
			if (flag3)
			{
				int[] settingValue3 = new WebSettingsClientManager().GetSettingValue<int[]>(Setting.INSTRUCTOR_RestrictLoginTo);
				bool flag4 = settingValue3 != null && settingValue3.Length != 0;
				if (flag4)
				{
					int iidIfExists = this.GetIidIfExists();
					bool flag5 = iidIfExists > 0;
					if (flag5)
					{
						bool flag6 = Array.IndexOf<int>(settingValue3, iidIfExists) < 0;
						if (flag6)
						{
							NavigatorClientManager.CurrentInstance.NotAllowed(Setting.INSTRUCTOR_ErrorMessage_Pilot, this.Page);
						}
					}
				}
			}
		}

		// Token: 0x040004FD RID: 1277
		protected HiddenField overridenocache;

		// Token: 0x040004FE RID: 1278
		protected ScriptManager bbb;

		// Token: 0x040004FF RID: 1279
		protected ctrls_CtrlMenu ctrlMenu1;

		// Token: 0x04000500 RID: 1280
		protected Label lbl_mainMessage;

		// Token: 0x04000501 RID: 1281
		protected ContentPlaceHolder placeholder_content;
	}
}
