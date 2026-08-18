using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPIWeb;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.Entity.AppointmentsTestBooking.AutoTestBooking;
using TechnoPro.Common.UI.Web.Entity.Web;

namespace TechnoPro.ClockWorkWeb.user.misc
{
	// Token: 0x020000C2 RID: 194
	public class NotAllowed : Page
	{
		// Token: 0x060005BE RID: 1470 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0002A3A0 File Offset: 0x000285A0
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				string text = base.Request.QueryString["code"] ?? "";
				string text2 = base.Request.QueryString["notAllowedCode"] ?? "";
				int num = 0;
				bool flag2 = text2.Length > 0;
				if (flag2)
				{
					int.TryParse(text2, out num);
				}
				eNotAllowedCode eNotAllowedCode = (eNotAllowedCode)((num > 0 && Enum.IsDefined(typeof(eNotAllowedCode), num)) ? num : 0);
				bool flag3 = text == "module";
				if (flag3)
				{
					this.lbl_msg.Text = "The module is not currently active.";
				}
				else
				{
					bool flag4 = eNotAllowedCode > eNotAllowedCode.Unknown;
					if (flag4)
					{
						this.ShowNotAllowedCode(eNotAllowedCode);
					}
					else
					{
						int num2;
						bool flag5 = !int.TryParse(text, out num2);
						if (flag5)
						{
							bool flag6 = Enum.IsDefined(typeof(Setting), text);
							if (flag6)
							{
								num2 = (int)((Setting)Enum.Parse(typeof(Setting), text));
							}
						}
						bool flag7 = num2 <= 0;
						if (!flag7)
						{
							bool flag8 = !Enum.IsDefined(typeof(Setting), num2);
							if (!flag8)
							{
								Setting setting = (Setting)num2;
								Setting setting2 = setting;
								if (setting2 <= Setting.EXAMBOOKING_CustomAllowStudentToBookCheckSql)
								{
									if (setting2 == Setting.TESTBOOKING_CustomAllowStudentToBookCheckSql)
									{
										string key = "web_test_custom_check_emsg_" + this.LookupStudentPid().ToString();
										object obj = CacheStorageManager.Current[key];
										string text3 = "";
										bool flag9 = obj != null;
										if (flag9)
										{
											text3 = obj.ToString();
										}
										bool flag10 = text3.Length < 1;
										if (flag10)
										{
											text3 = "Unfortunately the online booking system is currently unavailable for you to book your test.  Please contact us for more information.";
										}
										this.lbl_msg.Text = text3;
										goto IL_2D2;
									}
									if (setting2 == Setting.EXAMBOOKING_CustomAllowStudentToBookCheckSql)
									{
										string key = "web_exam_custom_check_emsg_" + this.LookupStudentPid().ToString();
										object obj = CacheStorageManager.Current[key];
										string text3 = "";
										bool flag11 = obj != null;
										if (flag11)
										{
											text3 = obj.ToString();
										}
										bool flag12 = text3.Length < 1;
										if (flag12)
										{
											text3 = "Unfortunately the online booking system is currently unavailable for you to book your final exam.  Please contact us for more information.";
										}
										this.lbl_msg.Text = text3;
										goto IL_2D2;
									}
								}
								else
								{
									if (setting2 == Setting.SELFREGC_ControlIdToAuthorizeStudentForAccommodationsRequestSystemMessageOnFail)
									{
										string text3 = new WebSettingsClientManager().GetSettingValue<string>(setting).Trim();
										this.lbl_msg.Text = ((text3.Length > 0) ? text3 : "Please contact us in order to request your accommodations.");
										goto IL_2D2;
									}
									if (setting2 == Setting.TUTORING_StudentIsAuthorizedCid)
									{
										this.lbl_msg.Text = "You are currently not authorized to use the tutoring functionality online.  Please contact us for more information.";
										goto IL_2D2;
									}
								}
								string settingValue = new WebSettingsClientManager().GetSettingValue<string>(setting);
								this.lbl_msg.Text = settingValue;
								IL_2D2:;
							}
						}
					}
				}
			}
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00004233 File Offset: 0x00002433
		protected void btn_logout_Click(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.Logout();
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0002A681 File Offset: 0x00028881
		protected void btn_home_Click(object sender, EventArgs e)
		{
			ClockWorkWebCore.GoHome(this.Session, base.Request, base.Response);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0002A69C File Offset: 0x0002889C
		protected void btn_tryAgain_Click(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.GotoLastReturnUrl("/user/misc/", "default.aspx");
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0002A6B4 File Offset: 0x000288B4
		private int LookupStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0002A6D8 File Offset: 0x000288D8
		private void ShowNotAllowedCode(eNotAllowedCode notAllowedCode)
		{
			Dictionary<string, string> dictionary = base.Request.QueryString.AllKeys.ToDictionary((string g) => g.ToLower(), (string g) => base.Request.QueryString[g]);
			switch (notAllowedCode)
			{
			case eNotAllowedCode.InvalidMinMaxDatesForTestBooking:
			case eNotAllowedCode.InvalidMinMaxDatesForExamBooking:
			{
				string text = dictionary.ContainsKey("status") ? dictionary["status"] : "";
				int num = 0;
				bool flag = text.Length > 0;
				if (flag)
				{
					int.TryParse(text, out num);
				}
				switch ((num > 0 && Enum.IsDefined(typeof(eMinMaxDateRangeInvalidReason), num)) ? num : 0)
				{
				case 2:
					this.lbl_msg.Text = "The final exam period has passed.";
					break;
				case 3:
					this.lbl_msg.Text = "Your accommodations expiry date has not been set; please contact us.";
					break;
				case 4:
					this.lbl_msg.Text = "Your accommodations will be expiring before the first day you are allowed to book a test or exam.";
					break;
				case 5:
					this.lbl_msg.Text = "The final exam period has passed";
					break;
				}
				break;
			}
			case eNotAllowedCode.NoCoursesAvailableToBookBecauseSpecialAccBanForTestBooking:
			case eNotAllowedCode.NoCoursesAvailableToBookBecauseSpecialAccBanForExamBooking:
			{
				WebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				string xml = (notAllowedCode == eNotAllowedCode.NoCoursesAvailableToBookBecauseSpecialAccBanForExamBooking) ? webSettingsClientManager.GetSettingValue<string>(Setting.EXAMBOOKING_SpecialAccommodations) : webSettingsClientManager.GetSettingValue<string>(Setting.TESTBOOKING_SpecialAccommodations);
				List<SpecialAccommodation> source = SpecialAccommodation.LoadSpecialAccommodations(xml, "");
				SpecialAccommodation specialAccommodation = source.FirstOrDefault((SpecialAccommodation g) => g.IsActive && g.SpecialAccommodationType == SpecialAccommodationType.CantBookOnline);
				string text2 = (((specialAccommodation != null) ? specialAccommodation.GetArg("msgtostudent", "") : null) ?? "").Trim();
				bool flag2 = text2.Length < 1;
				if (flag2)
				{
					text2 = (webSettingsClientManager.GetSettingValue<string>(Setting.TESTBOOKING_ErrorMessage_NoCourses) ?? "").Trim();
					bool flag3 = text2.Length < 1;
					if (flag3)
					{
						text2 = "You have no courses available to book a test/exam for.  Please contact us for assistance.";
					}
				}
				this.lbl_msg.Text = text2;
				break;
			}
			}
		}

		// Token: 0x04000412 RID: 1042
		protected Panel p_main;

		// Token: 0x04000413 RID: 1043
		protected Label lbl_msg;

		// Token: 0x04000414 RID: 1044
		protected Panel p_contactinfo;

		// Token: 0x04000415 RID: 1045
		protected Label lbl_contact;

		// Token: 0x04000416 RID: 1046
		protected Button btn_logout;

		// Token: 0x04000417 RID: 1047
		protected Button btn_home;

		// Token: 0x04000418 RID: 1048
		protected Button btn_tryAgain;
	}
}
