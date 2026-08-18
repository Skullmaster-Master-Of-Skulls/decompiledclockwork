using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkWeb.ctrls;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.Entity;
using TechnoPro.Common.UI.Web.Entity.Web.EventArgs;

namespace TechnoPro.ClockWorkWeb.user.appt
{
	// Token: 0x020000FB RID: 251
	public class user_TutorSchedule_TutorMaster : MasterPage, IClockWorkMasterPage
	{
		// Token: 0x0600073A RID: 1850 RVA: 0x0003795C File Offset: 0x00035B5C
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.MODULES_ENABLED_AppointmentBooking);
			bool flag = !settingValue;
			if (flag)
			{
				base.Response.Redirect("~/user/misc/NotAllowed.aspx?code=module", true);
			}
			LicensingClientWebClientManager.CurrentInstance.CheckIsModuleLicensed(TechnoPro.Common.Public.Entities.Settings.Group.APPOINTMENTBOOKING);
			bool flag2 = !this.Page.IsPostBack;
			if (flag2)
			{
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x000379C4 File Offset: 0x00035BC4
		private user_TutorSchedule_TutorMaster.MenuItemsEnabledDisabled menuItemsEnabledDisabled
		{
			get
			{
				bool flag = this._menuItemsEnabledDisabled == null;
				if (flag)
				{
					this._menuItemsEnabledDisabled = this.FigureOutMenuItemsEnabledDisabled();
				}
				return this._menuItemsEnabledDisabled;
			}
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x000379F8 File Offset: 0x00035BF8
		private user_TutorSchedule_TutorMaster.MenuItemsEnabledDisabled FigureOutMenuItemsEnabledDisabled()
		{
			return new user_TutorSchedule_TutorMaster.MenuItemsEnabledDisabled
			{
				UseFAQ = !new WebSettingsClientManager().GetSettingValue<bool>(Setting.APPOINTMENTBOOKING_HideFaq)
			};
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00037A28 File Offset: 0x00035C28
		protected void ctrlMenu1_OnBeforeAddMenuItem(object sender, AddMenuItemEventArgs e)
		{
			user_TutorSchedule_TutorMaster.MenuItemsEnabledDisabled menuItemsEnabledDisabled = this.menuItemsEnabledDisabled;
			eClockWorkWebPage menuItem = e.MenuItem;
			if (menuItem == eClockWorkWebPage.AppointmentBooking_FAQ)
			{
				e.AbortAddingMenuItem = !menuItemsEnabledDisabled.UseFAQ;
			}
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00037A60 File Offset: 0x00035C60
		protected void mainMenu_MenuItemClick(object sender, MenuEventArgs e)
		{
			string value = e.Item.Value;
			if (!(value == "logout"))
			{
				if (!(value == "login"))
				{
					if (!(value == "upcomingapps"))
					{
					}
				}
				else
				{
					base.Response.Redirect("book.aspx");
				}
			}
			else
			{
				WebAuthenticationAuthorizationWebClientManager.CurrentInstance.Logout();
			}
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00037AC5 File Offset: 0x00035CC5
		public void SetCurrentPage(eClockWorkWebPage page)
		{
			this.ctrlMenu1.SetCurrentPage(page);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00037AD5 File Offset: 0x00035CD5
		public void SetCausesValidationForAllMenuItems(bool newCausesValidation)
		{
			this.ctrlMenu1.SetCausesValidationForAllMenuItems(newCausesValidation);
		}

		// Token: 0x04000586 RID: 1414
		private user_TutorSchedule_TutorMaster.MenuItemsEnabledDisabled _menuItemsEnabledDisabled;

		// Token: 0x04000587 RID: 1415
		protected Label lbl_msgroot;

		// Token: 0x04000588 RID: 1416
		protected Label lblHidden;

		// Token: 0x04000589 RID: 1417
		protected ScriptManager bbb;

		// Token: 0x0400058A RID: 1418
		protected Label lbl_mainMessage;

		// Token: 0x0400058B RID: 1419
		protected ctrls_CtrlMenu ctrlMenu1;

		// Token: 0x0400058C RID: 1420
		protected ContentPlaceHolder placeholder_content;

		// Token: 0x02000228 RID: 552
		internal class MenuItemsEnabledDisabled
		{
			// Token: 0x17000349 RID: 841
			// (get) Token: 0x06000E89 RID: 3721 RVA: 0x00050972 File Offset: 0x0004EB72
			// (set) Token: 0x06000E8A RID: 3722 RVA: 0x0005097A File Offset: 0x0004EB7A
			public bool UseFAQ { get; set; }
		}
	}
}
