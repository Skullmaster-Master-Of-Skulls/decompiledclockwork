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
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.test
{
	// Token: 0x0200006D RID: 109
	public class user_test_TestMaster : MasterPage, IClockWorkMasterPage
	{
		// Token: 0x06000432 RID: 1074 RVA: 0x0001F1E4 File Offset: 0x0001D3E4
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			LicensingClientWebClientManager.CurrentInstance.CheckIsModuleLicensed(TechnoPro.Common.Public.Entities.Settings.Group.TESTBOOKING);
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				int[] settingValue = new WebSettingsClientManager().GetSettingValue<int[]>(Setting.TESTBOOKING_RestrictLoginTo);
				bool flag2 = settingValue != null && settingValue.Length != 0;
				if (flag2)
				{
					int pidIfExists = this.GetPidIfExists();
					bool flag3 = pidIfExists > 0;
					if (flag3)
					{
						bool flag4 = Array.IndexOf<int>(settingValue, pidIfExists) < 0;
						if (flag4)
						{
							NavigatorClientManager.CurrentInstance.NotAllowed(Setting.TESTBOOKING_ErrorMessage_Pilot, this.Page);
						}
					}
				}
				string settingValue2 = new WebSettingsClientManager().GetSettingValue<string>(Setting.MODULES_MessageInstructor);
				bool flag5 = !string.IsNullOrEmpty(settingValue2);
				if (flag5)
				{
					this.lbl_mainMessage.Text = settingValue2;
				}
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0001F2AC File Offset: 0x0001D4AC
		protected void ctrlMenu1_OnBeforeAddMenuItem(object sender, AddMenuItemEventArgs e)
		{
			user_test_TestMaster.MenuItemsEnabledDisabled menuItemsEnabledDisabled = this.menuItemsEnabledDisabled;
			switch (e.MenuItem)
			{
			case eClockWorkWebPage.TestBooking_BookTest:
			{
				bool flag = !menuItemsEnabledDisabled.BookTestEnabled;
				if (flag)
				{
					e.AbortAddingMenuItem = true;
				}
				break;
			}
			case eClockWorkWebPage.TestBooking_BookExam:
			{
				bool flag2 = !menuItemsEnabledDisabled.BookExamEnabled;
				if (flag2)
				{
					e.AbortAddingMenuItem = true;
				}
				else
				{
					bool useBookExamsNotBookExam = menuItemsEnabledDisabled.UseBookExamsNotBookExam;
					if (useBookExamsNotBookExam)
					{
						e.NavigatePage = "bookexams.aspx";
					}
					else
					{
						bool useBookExams = menuItemsEnabledDisabled.UseBookExams2;
						if (useBookExams)
						{
							e.NavigatePage = "bookexam2.aspx";
							e.MenuItemTitle = "Request a final exam";
						}
					}
				}
				break;
			}
			case eClockWorkWebPage.TestBooking_Accommodations:
			{
				bool flag3 = !menuItemsEnabledDisabled.AccommodationsEnabled;
				if (flag3)
				{
					e.AbortAddingMenuItem = true;
				}
				else
				{
					bool alwaysUseTemplateLetter = menuItemsEnabledDisabled.AlwaysUseTemplateLetter;
					if (alwaysUseTemplateLetter)
					{
						e.NavigatePage = "accommodationsletter.aspx";
					}
				}
				break;
			}
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0001F38C File Offset: 0x0001D58C
		private user_test_TestMaster.MenuItemsEnabledDisabled menuItemsEnabledDisabled
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

		// Token: 0x06000435 RID: 1077 RVA: 0x0001F3C0 File Offset: 0x0001D5C0
		private user_test_TestMaster.MenuItemsEnabledDisabled FigureOutMenuItemsEnabledDisabled()
		{
			return new user_test_TestMaster.MenuItemsEnabledDisabled
			{
				UseBookExamsNotBookExam = new WebSettingsClientManager().GetSettingValue<bool>(Setting.EXAMBOOKING_AllowStudentsToBookMultipleExams),
				UseBookExams2 = new WebSettingsClientManager().GetSettingValue<bool>(Setting.EXAMBOOKING_FinalExamRequest_Enabled),
				BookExamEnabled = new WebSettingsClientManager().GetSettingValue<bool>(Setting.EXAMBOOKING_StudentsAllowedToBookExams),
				BookTestEnabled = new WebSettingsClientManager().GetSettingValue<bool>(Setting.TESTBOOKING_StudentsAllowedToBookTests),
				AccommodationsEnabled = new WebSettingsClientManager().GetSettingValue<bool>(Setting.ACCOMMODATIONS_StudentsAllowedToAccessAccommodationLettersOnline),
				AlwaysUseTemplateLetter = new WebSettingsClientManager().GetSettingValue<bool>(Setting.ACCOMMODATIONS_TemplateAccommodationLetterOnly)
			};
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0001F45C File Offset: 0x0001D65C
		protected void RadMenu1_ItemClick(object sender, RadMenuEventArgs e)
		{
			bool flag = e.Item.Value.ToLower().Contains("logout");
			if (flag)
			{
				WebAuthenticationAuthorizationWebClientManager.CurrentInstance.Logout();
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001F493 File Offset: 0x0001D693
		public void SetCurrentPage(eClockWorkWebPage page)
		{
			this.ctrlMenu1.SetCurrentPage(page);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0001F4A3 File Offset: 0x0001D6A3
		public void SetCausesValidationForAllMenuItems(bool newCausesValidation)
		{
			this.ctrlMenu1.SetCausesValidationForAllMenuItems(newCausesValidation);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0001F4B4 File Offset: 0x0001D6B4
		private int GetPidIfExists()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid_DontTryToAuthenticate(this.Page);
		}

		// Token: 0x04000211 RID: 529
		private user_test_TestMaster.MenuItemsEnabledDisabled _menuItemsEnabledDisabled;

		// Token: 0x04000212 RID: 530
		protected Label lbl_mainMessage;

		// Token: 0x04000213 RID: 531
		protected ctrls_CtrlMenu ctrlMenu1;

		// Token: 0x04000214 RID: 532
		protected ContentPlaceHolder placeholder_content;

		// Token: 0x04000215 RID: 533
		protected RadScriptManager RadScriptManager1;

		// Token: 0x020001E3 RID: 483
		internal class MenuItemsEnabledDisabled
		{
			// Token: 0x170002F2 RID: 754
			// (get) Token: 0x06000D2F RID: 3375 RVA: 0x0004E826 File Offset: 0x0004CA26
			// (set) Token: 0x06000D30 RID: 3376 RVA: 0x0004E82E File Offset: 0x0004CA2E
			public bool UseBookExamsNotBookExam { get; set; }

			// Token: 0x170002F3 RID: 755
			// (get) Token: 0x06000D31 RID: 3377 RVA: 0x0004E837 File Offset: 0x0004CA37
			// (set) Token: 0x06000D32 RID: 3378 RVA: 0x0004E83F File Offset: 0x0004CA3F
			public bool UseBookExams2 { get; set; }

			// Token: 0x170002F4 RID: 756
			// (get) Token: 0x06000D33 RID: 3379 RVA: 0x0004E848 File Offset: 0x0004CA48
			// (set) Token: 0x06000D34 RID: 3380 RVA: 0x0004E850 File Offset: 0x0004CA50
			public bool BookTestEnabled { get; set; }

			// Token: 0x170002F5 RID: 757
			// (get) Token: 0x06000D35 RID: 3381 RVA: 0x0004E859 File Offset: 0x0004CA59
			// (set) Token: 0x06000D36 RID: 3382 RVA: 0x0004E861 File Offset: 0x0004CA61
			public bool BookExamEnabled { get; set; }

			// Token: 0x170002F6 RID: 758
			// (get) Token: 0x06000D37 RID: 3383 RVA: 0x0004E86A File Offset: 0x0004CA6A
			// (set) Token: 0x06000D38 RID: 3384 RVA: 0x0004E872 File Offset: 0x0004CA72
			public bool AccommodationsEnabled { get; set; }

			// Token: 0x170002F7 RID: 759
			// (get) Token: 0x06000D39 RID: 3385 RVA: 0x0004E87B File Offset: 0x0004CA7B
			// (set) Token: 0x06000D3A RID: 3386 RVA: 0x0004E883 File Offset: 0x0004CA83
			public bool AlwaysUseTemplateLetter { get; set; }
		}
	}
}
