using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using ClockWorkWebAPI.AuthenticationAuthorization;
using ClockWorkWebAPIWeb;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync.Student;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.Core.DataSync;
using TechnoPro.Common.ClientManager.Core.DynamicForms;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.People;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.DataSync;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Email;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.appt
{
	// Token: 0x020000F9 RID: 249
	public class user_TutorSchedule_NewUser : Page
	{
		// Token: 0x0600072C RID: 1836 RVA: 0x00037090 File Offset: 0x00035290
		private void Page_Init(object sender, EventArgs e)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.APPOINTMENTBOOKING_registrationScreenNum);
			this.AddWizardControls(settingValue);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x000370B8 File Offset: 0x000352B8
		private ClockWorkIdentity GetClockWorkIdentity()
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.INTAKE_RequireStudentsToLoginFirst);
			return this.GetClockWorkIdentity(settingValue);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x000370E4 File Offset: 0x000352E4
		private ClockWorkIdentity GetClockWorkIdentity(bool loginIfNotAlreadyLoggedIn)
		{
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			return webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity_LoginIfNecessary(this.Page, ClockWorkWebAPI.AuthenticationAuthorization.GroupMembership.student, loginIfNotAlreadyLoggedIn);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00037110 File Offset: 0x00035310
		private int LookupStudentPidWithoutLoginAttempt()
		{
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			ClockWorkIdentity currentClockWorkIdentity_LoginIfNecessary = webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity_LoginIfNecessary(this.Page, ClockWorkWebAPI.AuthenticationAuthorization.GroupMembership.student, false);
			return (currentClockWorkIdentity_LoginIfNecessary == null) ? 0 : currentClockWorkIdentity_LoginIfNecessary.PersonId;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00037148 File Offset: 0x00035348
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = true;
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.APPOINTMENTBOOKING_allowNonClockWorkStudentsToRegister);
			bool flag2 = !settingValue;
			if (flag2)
			{
				flag = false;
				base.Response.Redirect("Message.aspx?msgcode=notallowed");
			}
			ClockWorkIdentity clockWorkIdentity = null;
			bool flag3 = flag;
			if (flag3)
			{
				clockWorkIdentity = this.GetClockWorkIdentity();
				int num = this.LookupStudentPidWithoutLoginAttempt();
				bool flag4 = num > 0;
				if (flag4)
				{
					flag = false;
					base.Response.Redirect("book.aspx", true);
				}
			}
			bool flag5 = flag;
			if (flag5)
			{
				bool flag6 = clockWorkIdentity == null || (string.IsNullOrEmpty(clockWorkIdentity.UserName) && string.IsNullOrEmpty(clockWorkIdentity.StudentNumber));
				if (flag6)
				{
					flag = false;
					base.Response.Redirect("Message.aspx?msgcode=invalidlogin");
				}
				bool flag7 = flag && !this.Page.IsPostBack;
				if (flag7)
				{
					this.lbl_confidentiality.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.APPOINTMENTBOOKING_confidentialityAgreement);
					IDataSyncClientManager dataSyncClientManager = new DataSyncClientManager();
					StudentDataSyncPreviewDataDTO studentPreviewDataByStudentNumberOrUsername = dataSyncClientManager.GetStudentPreviewDataByStudentNumberOrUsername(clockWorkIdentity.UserName, clockWorkIdentity.StudentNumber);
					bool flag8 = studentPreviewDataByStudentNumberOrUsername == null || string.IsNullOrEmpty(studentPreviewDataByStudentNumberOrUsername.StudentNumber) || (string.IsNullOrEmpty(studentPreviewDataByStudentNumberOrUsername.FirstName) && string.IsNullOrEmpty(studentPreviewDataByStudentNumberOrUsername.LastName));
					if (flag8)
					{
						flag = false;
						base.Response.Redirect("Message.aspx?msgcode=invaliddatasync");
					}
					bool flag9 = flag;
					if (flag9)
					{
						this.txt_fn.Text = (studentPreviewDataByStudentNumberOrUsername.FirstName ?? "");
						this.txt_ln.Text = (studentPreviewDataByStudentNumberOrUsername.LastName ?? "");
						this.txt_student_no.Text = (studentPreviewDataByStudentNumberOrUsername.StudentNumber ?? "");
						this.txt_email.Text = (studentPreviewDataByStudentNumberOrUsername.Email ?? "").Trim();
						bool flag10 = this.txt_email.Text.Length > 0;
						if (flag10)
						{
							this.txt_email.ReadOnly = true;
						}
						bool flag11 = this.txt_fn.Text.Length > 0;
						if (flag11)
						{
							this.txt_fn.ReadOnly = true;
						}
						bool flag12 = this.txt_ln.Text.Length > 0;
						if (flag12)
						{
							this.txt_ln.ReadOnly = true;
						}
						bool flag13 = this.txt_student_no.Text.Length > 0;
						if (flag13)
						{
							this.txt_student_no.ReadOnly = true;
						}
					}
				}
			}
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x000373C8 File Offset: 0x000355C8
		private void AddWizardControls(int screenNum)
		{
			bool flag = screenNum > 0;
			if (flag)
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_EmailCid);
				DynamicControlLayoutHelper dynamicControlLayoutHelper = new DynamicControlLayoutHelper();
				DynamicScreenLayout.ControlsToScreen(ref dynamicControlLayoutHelper, base.Cache, screenNum, this.p_data, null, false, false, settingValue);
			}
			else
			{
				this.p_data.Visible = false;
			}
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00037420 File Offset: 0x00035620
		public void btn_submit_click(object sender, EventArgs e)
		{
			bool flag = !this.chk_iagree.Checked;
			if (flag)
			{
				this.lbl_sub.Visible = false;
				this.lbl_iacceptrequired.Visible = true;
				this.lbl_iacceptrequired.Focus();
			}
			else
			{
				IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
				ClockWorkIdentity currentClockWorkIdentity = webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity(this.Page);
				string text = (currentClockWorkIdentity == null) ? null : currentClockWorkIdentity.UserName;
				bool flag2 = string.IsNullOrEmpty(text);
				if (flag2)
				{
					NavigatorClientManager.CurrentInstance.SetReturnUrl();
					string loginUrl = ClockWorkWebAPI.Core.GetLoginUrl();
					base.Response.Redirect(loginUrl, true);
				}
				user_TutorSchedule_NewUser.NewUserView newUserView = new user_TutorSchedule_NewUser.NewUserView
				{
					StudentNumber = this.txt_student_no.Text.Trim().ToUpper(),
					FirstName = this.txt_fn.Text.Trim(),
					MiddleName = string.Empty,
					LastName = this.txt_ln.Text.Trim(),
					Username = text
				};
				bool flag3 = newUserView.StudentNumber.Trim().Length < 1;
				if (flag3)
				{
					this.ShowValidationFailed("student number");
				}
				else
				{
					bool flag4 = newUserView.FirstName.Trim().Length < 1;
					if (flag4)
					{
						this.ShowValidationFailed("first name");
					}
					else
					{
						bool flag5 = newUserView.LastName.Trim().Length < 1;
						if (flag5)
						{
							this.ShowValidationFailed("last name");
						}
						else
						{
							List<int> list = new List<int>
							{
								1
							};
							int settingValue = new WebSettingsClientManager().GetSettingValue<int>(Setting.APPOINTMENTBOOKING_clientGid);
							bool flag6 = settingValue > 0 && !list.Contains(settingValue);
							if (flag6)
							{
								list.Add(settingValue);
							}
							IPeopleClientManager peopleClientManager = new PeopleClientManager();
							int num = peopleClientManager.CreateUser(new PersonBaseDTO
							{
								Student_no = newUserView.StudentNumber,
								FirstName = newUserView.FirstName,
								MiddleName = newUserView.MiddleName,
								LastName = newUserView.LastName
							}, list);
							bool flag7 = num < 1;
							if (flag7)
							{
								this.ShowMessage("Something went wrong and your account could not be created.  Please contact us for assistance.");
							}
							else
							{
								IDynamicFieldClientManager dynamicFieldClientManager = new DynamicFieldClientManager();
								IDynamicDataClientManager dynamicDataClientManager = new DynamicDataClientManager();
								int settingValue2 = new WebSettingsClientManager().GetSettingValue<int>(Setting.APPOINTMENTBOOKING_OptionalUsernameCid);
								bool flag8 = settingValue2 > 0;
								if (flag8)
								{
									IList<DynamicFieldDTO> list2 = dynamicFieldClientManager.LoadFieldsByControlIds(new List<int>
									{
										settingValue2
									});
									DynamicFieldDTO dynamicFieldDTO = (list2 != null && list2.Count > 0) ? list2[0] : null;
									bool flag9 = dynamicFieldDTO != null;
									if (flag9)
									{
										dynamicDataClientManager.SaveData(new DynamicDataContextDTO
										{
											PrimaryId = num
										}, new List<DynamicDataDTO>
										{
											new DynamicDataDTO
											{
												Field = dynamicFieldDTO,
												Value = text
											}
										}, eDynamicFormTypeDTO.PerStudent);
									}
								}
								string text2 = newUserView.Email ?? "";
								bool flag10 = text2.Length > 0;
								if (flag10)
								{
									DynamicFieldDTO emailField = dynamicFieldClientManager.GetEmailField();
									bool flag11 = emailField != null;
									if (flag11)
									{
										dynamicDataClientManager.SaveData(new DynamicDataContextDTO
										{
											PrimaryId = num
										}, new List<DynamicDataDTO>
										{
											new DynamicDataDTO
											{
												Field = emailField,
												Value = text2
											}
										}, eDynamicFormTypeDTO.PerStudent);
									}
								}
								bool flag12 = currentClockWorkIdentity != null;
								if (flag12)
								{
									currentClockWorkIdentity.PersonId = num;
									webAuthenticationAuthorizationWebClientManager.SetCurrentClockWorkIdentity(currentClockWorkIdentity);
								}
								IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
								int settingValue3 = webSettingsClientManager.GetSettingValue<int>(Setting.APPOINTMENTBOOKING_registrationScreenNum);
								DynamicScreenLayout.SaveDynamicData(ScreenType.ScreenType_PerStudent, num, settingValue3, base.Cache, this.p_data, "");
								IDataSyncClientManager dataSyncClientManager = new DataSyncClientManager();
								dataSyncClientManager.RunFullDataSyncForExistingStudent(newUserView.StudentNumber, false, false);
								IEmailClientManager emailClientManager = new EmailClientManager();
								Dictionary<string, string> dictionary = new Dictionary<string, string>().InsertBaseUserMailMergeValues();
								bool flag13 = text2.Length > 0;
								if (flag13)
								{
									dictionary.Add("email", text2);
								}
								emailClientManager.SendEmail(num, Setting.APPOINTMENTBOOKING_email_registration, TechnoPro.Common.Public.Entities.Settings.Group.APPOINTMENTBOOKING, dictionary);
								INavigatorClientManager navigatorClientManager = new NavigatorClientManager();
								navigatorClientManager.GotoLastReturnUrl("~/user/appt", "book.aspx");
							}
						}
					}
				}
			}
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0003782B File Offset: 0x00035A2B
		private void ShowValidationFailed(string itemName)
		{
			this.ShowMessage(string.Format("Please enter a valid {0} first.", itemName));
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00037840 File Offset: 0x00035A40
		private void ShowMessage(string msg)
		{
			msg = (msg ?? "");
			this.lbl_msg.Text = msg;
			this.p_msg.Visible = (msg.Length > 0);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00037871 File Offset: 0x00035A71
		public void btn_cancel_click(object sender, EventArgs e)
		{
			base.Response.Redirect("Default.aspx", true);
		}

		// Token: 0x0400056C RID: 1388
		protected RadCodeBlock RadCodeBlock2;

		// Token: 0x0400056D RID: 1389
		protected Label lbl_title;

		// Token: 0x0400056E RID: 1390
		protected Label lbl_sub;

		// Token: 0x0400056F RID: 1391
		protected Label lbl_iacceptrequired;

		// Token: 0x04000570 RID: 1392
		protected Panel p_msg;

		// Token: 0x04000571 RID: 1393
		protected Label lbl_msg;

		// Token: 0x04000572 RID: 1394
		protected Panel p_name;

		// Token: 0x04000573 RID: 1395
		protected Label lbl_student_no;

		// Token: 0x04000574 RID: 1396
		protected TextBox txt_student_no;

		// Token: 0x04000575 RID: 1397
		protected RequiredFieldValidator val_sn;

		// Token: 0x04000576 RID: 1398
		protected Label Label1;

		// Token: 0x04000577 RID: 1399
		protected TextBox txt_fn;

		// Token: 0x04000578 RID: 1400
		protected RequiredFieldValidator RequiredFieldValidator1;

		// Token: 0x04000579 RID: 1401
		protected Label Label2;

		// Token: 0x0400057A RID: 1402
		protected TextBox txt_ln;

		// Token: 0x0400057B RID: 1403
		protected RequiredFieldValidator RequiredFieldValidator2;

		// Token: 0x0400057C RID: 1404
		protected Label lbl_email;

		// Token: 0x0400057D RID: 1405
		protected TextBox txt_email;

		// Token: 0x0400057E RID: 1406
		protected RequiredFieldValidator RequiredFieldValidator3;

		// Token: 0x0400057F RID: 1407
		protected Panel p_data;

		// Token: 0x04000580 RID: 1408
		protected Button btn_cancel2;

		// Token: 0x04000581 RID: 1409
		protected Label lbl_confidentiality;

		// Token: 0x04000582 RID: 1410
		protected CheckBox chk_iagree;

		// Token: 0x04000583 RID: 1411
		protected Button btn_submit;

		// Token: 0x04000584 RID: 1412
		protected Button btn_cancel;

		// Token: 0x02000227 RID: 551
		internal class NewUserView
		{
			// Token: 0x17000343 RID: 835
			// (get) Token: 0x06000E7C RID: 3708 RVA: 0x0005090C File Offset: 0x0004EB0C
			// (set) Token: 0x06000E7D RID: 3709 RVA: 0x00050914 File Offset: 0x0004EB14
			public string Username { get; set; }

			// Token: 0x17000344 RID: 836
			// (get) Token: 0x06000E7E RID: 3710 RVA: 0x0005091D File Offset: 0x0004EB1D
			// (set) Token: 0x06000E7F RID: 3711 RVA: 0x00050925 File Offset: 0x0004EB25
			public string StudentNumber { get; set; }

			// Token: 0x17000345 RID: 837
			// (get) Token: 0x06000E80 RID: 3712 RVA: 0x0005092E File Offset: 0x0004EB2E
			// (set) Token: 0x06000E81 RID: 3713 RVA: 0x00050936 File Offset: 0x0004EB36
			public string FirstName { get; set; }

			// Token: 0x17000346 RID: 838
			// (get) Token: 0x06000E82 RID: 3714 RVA: 0x0005093F File Offset: 0x0004EB3F
			// (set) Token: 0x06000E83 RID: 3715 RVA: 0x00050947 File Offset: 0x0004EB47
			public string MiddleName { get; set; }

			// Token: 0x17000347 RID: 839
			// (get) Token: 0x06000E84 RID: 3716 RVA: 0x00050950 File Offset: 0x0004EB50
			// (set) Token: 0x06000E85 RID: 3717 RVA: 0x00050958 File Offset: 0x0004EB58
			public string LastName { get; set; }

			// Token: 0x17000348 RID: 840
			// (get) Token: 0x06000E86 RID: 3718 RVA: 0x00050961 File Offset: 0x0004EB61
			// (set) Token: 0x06000E87 RID: 3719 RVA: 0x00050969 File Offset: 0x0004EB69
			public string Email { get; set; }
		}
	}
}
