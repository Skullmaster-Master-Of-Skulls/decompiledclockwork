using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ClockWorkLogger;
using ClockWorkWebAPI;
using ClockWorkWebAPI.AuthenticationAuthorization;
using ClockWorkWebAPIWeb;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync.Student;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.Intake;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkWeb.ctrls.Common.Captcha;
using TechnoPro.Common.ClientManager.Core.DataSync;
using TechnoPro.Common.ClientManager.Core.DynamicForms;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.Intake;
using TechnoPro.Common.ClientManager.Core.People;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.DataSync;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.Intake;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Email;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.MailMerging;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.MailMerging;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;
using TechnoPro.Common.UI.Web.Entity.Modules;

namespace TechnoPro.ClockWorkWeb.user.Intake
{
	// Token: 0x020000CB RID: 203
	public class user_Intake_register : Page
	{
		// Token: 0x060005E0 RID: 1504 RVA: 0x0002B154 File Offset: 0x00029354
		private ClockWorkIdentity GetClockWorkIdentity(bool loginIfNotAlreadyLoggedIn)
		{
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			return webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity_LoginIfNecessary(this.Page, ClockWorkWebAPI.AuthenticationAuthorization.GroupMembership.student, loginIfNotAlreadyLoggedIn);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0002B180 File Offset: 0x00029380
		private ClockWorkIdentity GetClockWorkIdentity()
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.INTAKE_RequireStudentsToLoginFirst);
			return this.GetClockWorkIdentity(settingValue);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0002B1AC File Offset: 0x000293AC
		private int LookupStudentPidWithoutLoginAttempt()
		{
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			ClockWorkIdentity currentClockWorkIdentity_LoginIfNecessary = webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity_LoginIfNecessary(this.Page, ClockWorkWebAPI.AuthenticationAuthorization.GroupMembership.student, false);
			return (currentClockWorkIdentity_LoginIfNecessary == null) ? 0 : currentClockWorkIdentity_LoginIfNecessary.PersonId;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0002B1E4 File Offset: 0x000293E4
		protected void Page_Load(object sender, EventArgs e)
		{
			CWLogger.Logger.Debug("PAGE LOAD");
			CWLogger.Logger.Debug("Session: " + this.Session.SessionID);
			ClockWorkIdentity clockWorkIdentity = this.GetClockWorkIdentity();
			CWLogger.Logger.Debug(("PAGE LOAD:identity=" + clockWorkIdentity == null) ? "NULL" : "not null");
			int num = this.LookupStudentPidWithoutLoginAttempt();
			CWLogger.Logger.Debug("PAGE LOAD:pid=" + num.ToString());
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.INTAKE_SendClockWorkStudentsWithPidToFirstAvailableForm);
			CWLogger.Logger.Debug("PAGE LOAD:sendClockWorkStudentsWithPidToForms=" + settingValue.ToString());
			bool flag = settingValue;
			if (flag)
			{
				bool flag2 = num > 0;
				if (flag2)
				{
					bool flag3 = this.IsStudentAllowedToFillInForm(num, Setting.SURVEYS_Form_A_ScreenNum, Setting.SURVEYS_Form_A_CheckboxControlIndicatingOkToFillInNewForm);
					if (flag3)
					{
						base.Response.Redirect("~/user/Forms/FormA.aspx", true);
					}
					else
					{
						bool flag4 = this.IsStudentAllowedToFillInForm(num, Setting.SURVEYS_Form_B_ScreenNum, Setting.SURVEYS_Form_B_CheckboxControlIndicatingOkToFillInNewForm);
						if (flag4)
						{
							base.Response.Redirect("~/user/Forms/FormB.aspx", true);
						}
						else
						{
							bool flag5 = this.IsStudentAllowedToFillInForm(num, Setting.SURVEYS_Form_C_ScreenNum, Setting.SURVEYS_Form_C_CheckboxControlIndicatingOkToFillInNewForm);
							if (flag5)
							{
								base.Response.Redirect("~/user/Forms/FormC.aspx", true);
							}
						}
					}
				}
			}
			bool flag6 = true;
			bool flag7 = num > 0;
			if (flag7)
			{
				bool settingValue2 = new WebSettingsClientManager().GetSettingValue<bool>(Setting.INTAKE_AllowStudentToFillOutIntakeFormIfTheirStudentNumberIsAlreadyInClockWork);
				bool flag8 = !settingValue2;
				if (flag8)
				{
					flag6 = false;
					CWLogger.Logger.Warn("/user/intake/register.aspx.cs:Page_Load:User already has pid:pid={0}", num.ToString());
					base.Response.Redirect("NotAllowed.aspx?code=incw");
				}
			}
			bool flag9 = flag6;
			if (flag9)
			{
				bool flag10 = !this.Page.IsPostBack;
				if (flag10)
				{
					CWLogger.Logger.Debug("PAGE LOAD:!Page.IsPostback");
					WebSettingsClientManager webSettingsClientManager2 = new WebSettingsClientManager();
					bool settingValue3 = webSettingsClientManager2.GetSettingValue<bool>(Setting.INTAKE_HideCaptcha);
					bool flag11 = settingValue3;
					if (flag11)
					{
						this.p_captcha.Visible = false;
					}
					string settingValue4 = webSettingsClientManager2.GetSettingValue<string>(Setting.INTAKE_RegistrationInstructions);
					this.lbl_info.Text = settingValue4;
					bool flag12 = !this.Page.IsPostBack;
					if (flag12)
					{
						CWLogger.Logger.Info("Intake:Register:Entry:ip={0}", WebClientUtilityWebClientManager.CurrentInstance.GetUsersIpAddress());
						bool flag13 = clockWorkIdentity != null && (!string.IsNullOrEmpty(clockWorkIdentity.UserName) || !string.IsNullOrEmpty(clockWorkIdentity.StudentNumber));
						if (flag13)
						{
							IDataSyncClientManager dataSyncClientManager = new DataSyncClientManager();
							StudentDataSyncPreviewDataDTO studentPreviewDataByStudentNumberOrUsername = dataSyncClientManager.GetStudentPreviewDataByStudentNumberOrUsername(clockWorkIdentity.UserName, clockWorkIdentity.StudentNumber);
							bool flag14 = studentPreviewDataByStudentNumberOrUsername != null;
							if (flag14)
							{
								this.txt_fn.Text = (studentPreviewDataByStudentNumberOrUsername.FirstName ?? "");
								this.txt_ln.Text = (studentPreviewDataByStudentNumberOrUsername.LastName ?? "");
								this.txt_student_no.Text = (studentPreviewDataByStudentNumberOrUsername.StudentNumber ?? "");
								int settingValue5 = new WebSettingsClientManager().GetSettingValue<int>(Setting.INTAKE_EmailCid);
								Control control = (settingValue5 > 0) ? DynamicScreenLayout.FindControl(base.Cache, settingValue5, this.p_data) : null;
								TextBox textBox = this.txt_email;
								bool flag15 = control != null && control is TextBox;
								if (flag15)
								{
									textBox = (TextBox)control;
									this.EmailRow.Visible = false;
								}
								textBox.Text = (studentPreviewDataByStudentNumberOrUsername.Email ?? "").Trim();
								bool flag16 = this.txt_fn.Text.Length > 0;
								if (flag16)
								{
									this.txt_fn.ReadOnly = true;
								}
								bool flag17 = this.txt_ln.Text.Length > 0;
								if (flag17)
								{
									this.txt_ln.ReadOnly = true;
								}
								bool flag18 = this.txt_student_no.Text.Length > 0;
								if (flag18)
								{
									this.txt_student_no.ReadOnly = true;
								}
								bool flag19 = textBox.Text.Length > 0;
								if (flag19)
								{
									textBox.ReadOnly = true;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0002B600 File Offset: 0x00029800
		private bool IsStudentAllowedToFillInForm(int pid, Setting screenNumSetting, Setting chkCidSetting)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			int settingValue = webSettingsClientManager.GetSettingValue<int>(screenNumSetting);
			bool flag = settingValue < 1;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				int settingValue2 = webSettingsClientManager.GetSettingValue<int>(chkCidSetting);
				bool flag2 = settingValue2 > 0;
				if (flag2)
				{
					IDynamicDataClientManager dynamicDataClientManager = new DynamicDataClientManager();
					DynamicDataContextDTO context = new DynamicDataContextDTO
					{
						PrimaryId = pid
					};
					IList<DynamicDataDTO> list = dynamicDataClientManager.LoadDataByFields(context, new List<int>
					{
						settingValue2
					}, eDynamicFormTypeDTO.PerStudent);
					result = (list != null && list.Count > 0);
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0002B688 File Offset: 0x00029888
		private void Page_Init(object sender, EventArgs e)
		{
			int screenNum = this.ScreenNum;
			DynamicControlLayoutHelper dynamicControlLayoutHelper = null;
			List<string> exemptControlNames = new List<string>
			{
				"DATEADDED"
			};
			DynamicScreenLayout.ControlsToScreen(ref dynamicControlLayoutHelper, base.Cache, screenNum, this.p_data, null, false, false, exemptControlNames);
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0002B6CC File Offset: 0x000298CC
		private int ScreenNum
		{
			get
			{
				int num = new WebSettingsClientManager().GetSettingValue<int>(Setting.INTAKE_FormNum);
				bool flag = num < 1;
				if (flag)
				{
					num = 65;
				}
				return num;
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0002B6FC File Offset: 0x000298FC
		protected void btn_submit_click(object sender, EventArgs e)
		{
			CWLogger.Logger.Debug("btn_submit_click START");
			CWLogger.Logger.Debug("Session: " + this.Session.SessionID);
			bool flag = !this.Page.IsValid;
			if (flag)
			{
				CWLogger.Logger.Debug("!Page.IsValid=true; aborting");
				this.lbl_emsg.Text = "The form cannot be submitted.  Please check that all required fields have been filled in.";
				this.p_emsg.Visible = true;
			}
			else
			{
				WebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.INTAKE_HideCaptcha);
				int screenNum = this.ScreenNum;
				bool flag2 = !settingValue && !this.CaptchaControl1.ValidateCaptcha();
				if (flag2)
				{
					CWLogger.Logger.Debug("failed captcha; aborting");
				}
				else
				{
					int settingValue2 = new WebSettingsClientManager().GetSettingValue<int>(Setting.INTAKE_EmailCid);
					Control control = DynamicScreenLayout.FindControl(base.Cache, settingValue2, this.p_data);
					bool flag3 = control != null && control is TextBox;
					string text;
					if (flag3)
					{
						text = ((TextBox)control).Text.Trim();
					}
					else
					{
						text = this.txt_email.Text.Trim();
					}
					bool flag4 = string.IsNullOrEmpty(text);
					if (flag4)
					{
						CWLogger.Logger.Debug("missing email; aborting");
						base.Response.Write("Missing email.");
					}
					else
					{
						string text2 = this.txt_fn.Text;
						string text3 = this.txt_ln.Text;
						string text4 = this.txt_student_no.Text;
						bool flag5 = text4.Length < 1;
						if (flag5)
						{
							CWLogger.Logger.Debug("missing student number; aborting");
							base.Response.Write("Missing student number.");
						}
						else
						{
							bool flag6 = true;
							bool settingValue3 = webSettingsClientManager.GetSettingValue<bool>(Setting.INTAKE_AllowStudentToFillOutIntakeFormIfTheirStudentNumberIsAlreadyInClockWork);
							bool flag7 = !settingValue3;
							if (flag7)
							{
								IPeopleClientManager peopleClientManager = new PeopleClientManager();
								PersonBaseDTO personBaseDTO = peopleClientManager.LoadPersonByStudentNumber(text4, false);
								bool flag8 = personBaseDTO != null;
								if (flag8)
								{
									flag6 = false;
									CWLogger.Logger.Warn("User:Intake:Register:Cannot allow student to submit intake form because their student number already exists in ClockWork:snum={0}", text4);
									base.Response.Redirect("NotAllowed.aspx?code=incw", true);
								}
							}
							bool flag9 = flag6;
							if (flag9)
							{
								ClockWorkIdentity clockWorkIdentity = this.GetClockWorkIdentity();
								string text5 = (clockWorkIdentity == null) ? "" : (clockWorkIdentity.UserName ?? "");
								bool flag10 = text5.Length > 0;
								if (flag10)
								{
									IDataSyncClientManager dataSyncClientManager = new DataSyncClientManager();
									string studentNumber = (clockWorkIdentity == null) ? "" : (clockWorkIdentity.StudentNumber ?? "");
									StudentDataSyncPreviewDataDTO studentPreviewDataByStudentNumberOrUsername = dataSyncClientManager.GetStudentPreviewDataByStudentNumberOrUsername(text5, studentNumber);
									bool flag11 = studentPreviewDataByStudentNumberOrUsername != null;
									if (flag11)
									{
										bool flag12 = !string.IsNullOrEmpty(studentPreviewDataByStudentNumberOrUsername.FirstName);
										if (flag12)
										{
											text2 = studentPreviewDataByStudentNumberOrUsername.FirstName;
										}
										bool flag13 = !string.IsNullOrEmpty(studentPreviewDataByStudentNumberOrUsername.LastName);
										if (flag13)
										{
											text3 = studentPreviewDataByStudentNumberOrUsername.LastName;
										}
										bool flag14 = !string.IsNullOrEmpty(studentPreviewDataByStudentNumberOrUsername.StudentNumber);
										if (flag14)
										{
											text4 = studentPreviewDataByStudentNumberOrUsername.StudentNumber;
										}
									}
								}
								CWLogger.Logger.Debug(string.Format("About to create new intake account:snum={0}", text4));
								IIntakeAccountClientManager intakeAccountClientManager = new IntakeAccountClientManager();
								int num = intakeAccountClientManager.CreateNewIntakeAccount(new IntakeUserAccountDTO
								{
									FirstName = text2,
									MiddleName = "",
									LastName = text3,
									StudentNumber = text4.Trim().ToUpper(),
									Email = text,
									IpAddress = ""
								});
								CWLogger.Logger.Debug(string.Format("Created new intake account; pid={0}", num));
								bool flag15 = num > 0;
								if (flag15)
								{
									CWLogger.Logger.Debug("about to save data...");
									Exception ex = DynamicScreenLayout.SaveDynamicData(ScreenType.ScreenType_Intake, num, screenNum, base.Cache, this.p_data, "");
									bool flag16 = ex == null;
									if (flag16)
									{
										CWLogger.Logger.Info("Intake:Register:SaveDataSuccess:ip={0}:name={1} {2} . {3}", new object[]
										{
											WebClientUtilityWebClientManager.CurrentInstance.GetUsersIpAddress(),
											text2,
											text3,
											text4
										});
									}
									else
									{
										CWLogger.Logger.Error("Intake:Register:SaveDataFail:ip={0}:name={1} {2} . {3}:error={4}", new object[]
										{
											WebClientUtilityWebClientManager.CurrentInstance.GetUsersIpAddress(),
											text2,
											text3,
											text4,
											ex.ToString()
										});
									}
									IMailMergeCodes mailMergeCodes = new MailMergeCodes();
									Dictionary<string, string> args = new Dictionary<string, string>
									{
										{
											"email",
											text
										},
										{
											"firstname",
											text2
										},
										{
											"lastname",
											text3
										},
										{
											"student_no",
											text4
										},
										{
											"from",
											mailMergeCodes.GetDefaultFromAddress(eWebModule.Intake)
										},
										{
											"signature",
											mailMergeCodes.GetDefaultSignature(eWebModule.Intake)
										}
									}.InsertBaseUserMailMergeValues();
									IEmailClientManager emailClientManager = new EmailClientManager();
									MailMergeContextDTO context = new MailMergeContextDTO();
									emailClientManager.SendEmail(Setting.INTAKE_StudentConfirmation, new MailMergeContextWithCustomDictionaryDTO
									{
										Context = context,
										CustomDictionary = new MailMergeCustomDictionaryDTO
										{
											Args = args
										}
									}, "IntakeRegister");
								}
								base.Response.Redirect("thankyou.aspx", true);
							}
						}
					}
				}
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00008694 File Offset: 0x00006894
		protected void btn_cancel_click(object sender, EventArgs e)
		{
			base.Response.Redirect("default.aspx", true);
		}

		// Token: 0x04000428 RID: 1064
		protected ScriptManager bbb;

		// Token: 0x04000429 RID: 1065
		protected Panel p_title;

		// Token: 0x0400042A RID: 1066
		protected Label lbl_title;

		// Token: 0x0400042B RID: 1067
		protected Panel p_info;

		// Token: 0x0400042C RID: 1068
		protected Label lbl_info;

		// Token: 0x0400042D RID: 1069
		protected Panel p_emsg;

		// Token: 0x0400042E RID: 1070
		protected Label lbl_emsg;

		// Token: 0x0400042F RID: 1071
		protected Panel Panel4;

		// Token: 0x04000430 RID: 1072
		protected Label Label7;

		// Token: 0x04000431 RID: 1073
		protected RequiredFieldValidator RequiredFieldValidator1;

		// Token: 0x04000432 RID: 1074
		protected TextBox txt_fn;

		// Token: 0x04000433 RID: 1075
		protected Label Label8;

		// Token: 0x04000434 RID: 1076
		protected RequiredFieldValidator RequiredFieldValidator2;

		// Token: 0x04000435 RID: 1077
		protected TextBox txt_ln;

		// Token: 0x04000436 RID: 1078
		protected Label Label10;

		// Token: 0x04000437 RID: 1079
		protected RequiredFieldValidator RequiredFieldValidator6;

		// Token: 0x04000438 RID: 1080
		protected TextBox txt_student_no;

		// Token: 0x04000439 RID: 1081
		protected HtmlGenericControl EmailRow;

		// Token: 0x0400043A RID: 1082
		protected Label Label1;

		// Token: 0x0400043B RID: 1083
		protected RequiredFieldValidator RequiredFieldValidator3;

		// Token: 0x0400043C RID: 1084
		protected TextBox txt_email;

		// Token: 0x0400043D RID: 1085
		protected Panel p_data;

		// Token: 0x0400043E RID: 1086
		protected Panel p_captcha;

		// Token: 0x0400043F RID: 1087
		protected ctrls_Common_Captcha_CaptchaText CaptchaControl1;

		// Token: 0x04000440 RID: 1088
		protected Panel p_control;

		// Token: 0x04000441 RID: 1089
		protected Button btn_submit;

		// Token: 0x04000442 RID: 1090
		protected Button btn_cancel;
	}
}
