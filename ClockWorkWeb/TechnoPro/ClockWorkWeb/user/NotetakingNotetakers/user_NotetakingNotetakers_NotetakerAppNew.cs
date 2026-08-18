using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using ClockWorkLogger;
using skmValidators;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync.Notetaking;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.Notetaking;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.Notetaking;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Email;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.MailMerging;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Notetaking;
using TechnoPro.Common.UI.ClientManager.Web.Core.MailMerging;
using TechnoPro.Common.UI.ClientManager.Web.Core.Notetaking;
using TechnoPro.Common.UI.Web.Entity;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;
using TechnoPro.Common.UI.Web.Entity.Modules;
using TechnoPro.Common.UI.Web.Entity.Notetaking;
using TechnoPro.Common.UI.Web.Entity.Web;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.NotetakingNotetakers
{
	// Token: 0x020000AC RID: 172
	public class user_NotetakingNotetakers_NotetakerAppNew : Page
	{
		// Token: 0x06000568 RID: 1384 RVA: 0x000277E8 File Offset: 0x000259E8
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetNotetakerId(this.Page);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0002780C File Offset: 0x00025A0C
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
			if (flag)
			{
				((IClockWorkMasterPage)this.Page.Master).SetCurrentPage(eClockWorkWebPage.NotetakingNotetakers_Courses);
			}
			this.p_topmsg.Visible = false;
			int pid = this.GetPid();
			bool flag2 = pid > 0;
			if (flag2)
			{
				base.Response.Redirect("notetakerapp.aspx");
			}
			bool flag3 = !this.Page.IsPostBack;
			if (flag3)
			{
				string arg = ((string)this.Session["student_no"]) ?? "";
				CWLogger.Logger.Info("Notetaking:NotetakerAppNew.aspx:msg=New student entered notetaker application form with username={0}, notetakerid={1}, snumfromsession={2}", "", pid.ToString(), arg);
				bool flag4 = pid < 1;
				if (flag4)
				{
					INotetakingClientDataSyncWebClientManager notetakingClientDataSyncWebClientManager = new NotetakingClientDataSyncWebClientManager();
					GetNotetakerInfoAndCoursesInfo getNotetakerInfoAndCoursesInfo;
					NotetakerWithExternalCoursesDTO notetakerAndCourseInfo = notetakingClientDataSyncWebClientManager.GetNotetakerAndCourseInfo(false, this.Page, out getNotetakerInfoAndCoursesInfo);
					bool flag5 = notetakerAndCourseInfo == null;
					if (flag5)
					{
						CWLogger.Logger.Error("NotetakerAppNew:NotAbleToLoadNotetakerWithExternalCourses:altId={0}:Aborting and sending student to err.aspx", getNotetakerInfoAndCoursesInfo.Username ?? "");
						base.Response.Redirect("err.aspx?code=" + UserErrorCode.NotetakerAppNew.ToString(), true);
						return;
					}
					this.NotetakerToScreen(notetakerAndCourseInfo);
				}
				this.gv_courses.Visible = false;
				bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.NOTETAKINGB_NotetakerMandatoryEmail1);
				bool settingValue2 = new WebSettingsClientManager().GetSettingValue<bool>(Setting.NOTETAKINGB_NotetakerMandatoryEmail2);
				bool settingValue3 = new WebSettingsClientManager().GetSettingValue<bool>(Setting.NOTETAKINGB_NotetakerMandatoryPhone1);
				bool settingValue4 = new WebSettingsClientManager().GetSettingValue<bool>(Setting.NOTETAKINGB_NotetakerMandatoryPhone2);
				bool settingValue5 = new WebSettingsClientManager().GetSettingValue<bool>(Setting.NOTETAKINGB_NotetakerMandatoryAddress1);
				bool settingValue6 = new WebSettingsClientManager().GetSettingValue<bool>(Setting.NOTETAKINGB_NotetakerMandatoryAddress2);
				foreach (var <>f__AnonymousType in from g in new <>f__AnonymousType14<bool, RequiredFieldValidator>[]
				{
					new
					{
						IsRequired = settingValue,
						val1 = this.val_email1
					},
					new
					{
						IsRequired = settingValue2,
						val1 = this.val_email2
					},
					new
					{
						IsRequired = settingValue3,
						val1 = this.val_phone1
					},
					new
					{
						IsRequired = settingValue4,
						val1 = this.val_phone2
					},
					new
					{
						IsRequired = settingValue5,
						val1 = this.val_address1
					},
					new
					{
						IsRequired = settingValue6,
						val1 = this.val_address2
					}
				}
				where g.IsRequired
				select g)
				{
					<>f__AnonymousType.val1.Enabled = true;
				}
				this.lbl_mailingAddressIntro.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_NotetakerApplicationAddressIntro);
				this.lbl_emailIntro.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_EmailIntro);
				this.lbl_notesLegend.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_sampleNotesUploadInstructions);
				this.lbl_confidentiality.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_ConfidentialityAgreement);
				string settingValue7 = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_notetakerConfidentialityIAgreeWording);
				bool flag6 = !string.IsNullOrEmpty(settingValue7);
				if (flag6)
				{
					this.chk_iagree.Text = settingValue7;
				}
				this.lbl_additionalInfo.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_AdditionalInfoNotetaker);
				this.lblIntro.Text = "Please fill in your registration information in order to create your account:";
				this.p_additionalInfo.Visible = false;
				bool settingValue8 = new WebSettingsClientManager().GetSettingValue<bool>(Setting.NOTETAKINGB_showNotetakerConfidentiality);
				bool flag7 = settingValue8;
				if (flag7)
				{
					string settingValue9 = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_notetakerConfidentialityIAgreeWording);
					this.chk_iagree.Text = settingValue9;
				}
				bool settingValue10 = new WebSettingsClientManager().GetSettingValue<bool>(Setting.NOTETAKINGB_AllowNotetakersToUploadSampleNotes);
				bool flag8 = !settingValue10;
				if (flag8)
				{
					this.step_upload.Title = "Registration complete";
					this.lbl_t.Text = "Registration completed.";
					this.Button1.Text = "Continue ...";
					this.btn_noSampleNotes.Visible = false;
				}
				string settingValue11 = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_NotetakerIntakeChooseCoursesMessage);
				bool flag9 = !string.IsNullOrEmpty(settingValue11);
				if (flag9)
				{
					this.lbl_chooseCourses.Text = settingValue11;
				}
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00027C48 File Offset: 0x00025E48
		private void NotetakerToScreen(NotetakerWithExternalCoursesDTO notetakerWithExternalCourses)
		{
			SPProviderDTO notetaker = notetakerWithExternalCourses.Notetaker;
			DateTime now = DateTime.Now.Date;
			List<DataSyncExternalCourseDTO> list = (from g in notetakerWithExternalCourses.ExternalCourses
			where g.EndDate >= now
			select g).ToList<DataSyncExternalCourseDTO>();
			bool flag = notetaker != null;
			if (flag)
			{
				this.txt_phoneHome.Text = notetaker.Phone1;
				this.txt_phoneCell.Text = notetaker.Phone2;
				this.txt_firstName.Text = notetaker.Person.FirstName;
				this.txt_lastname.Text = notetaker.Person.LastName;
				this.txt_student_no.Text = notetaker.Person.Student_no;
				bool flag2 = !string.IsNullOrEmpty(notetaker.UserName);
				if (flag2)
				{
					this.txt_altid.Value = notetaker.UserName;
				}
				bool flag3 = !string.IsNullOrEmpty(notetaker.Email);
				if (flag3)
				{
					this.txt_email.Text = notetaker.Email;
				}
				this.txt_address.Text = notetaker.Address1;
				this.txt_perm.Text = notetaker.Address2;
				bool flag4 = string.IsNullOrEmpty(notetaker.Address1) && !string.IsNullOrEmpty(notetaker.Address2);
				if (flag4)
				{
					this.chk_mailing.Checked = false;
					this.chk_perm.Checked = true;
				}
				TextBox[] array = new TextBox[]
				{
					this.txt_firstName,
					this.txt_lastname,
					this.txt_student_no,
					this.txt_email
				};
				foreach (TextBox textBox in array)
				{
					bool flag5 = !string.IsNullOrEmpty(textBox.Text);
					if (flag5)
					{
						textBox.ReadOnly = true;
					}
				}
			}
			this.chks_courses.Items.Clear();
			List<string> list2 = new List<string>();
			foreach (DataSyncExternalCourseDTO course in list)
			{
				string externalCourseUniqueId = this.GetExternalCourseUniqueId(course);
				bool flag6 = !string.IsNullOrEmpty(externalCourseUniqueId) && !list2.Contains(externalCourseUniqueId);
				if (flag6)
				{
					this.chks_courses.Items.Add(new ListItem(externalCourseUniqueId, externalCourseUniqueId));
					list2.Add(externalCourseUniqueId);
				}
			}
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00027ECC File Offset: 0x000260CC
		private string GetExternalCourseUniqueId(DataSyncExternalCourseDTO course)
		{
			return (course == null) ? "?" : string.Concat(new string[]
			{
				course.Subject,
				" ",
				course.Course,
				" ",
				course.Section,
				" ",
				course.TimeOfDay
			});
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00027F30 File Offset: 0x00026130
		private List<string> GetSelectedCourseVals()
		{
			bool flag = !this.chks_courses.Visible;
			List<string> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<string> list = new List<string>();
				foreach (object obj in this.chks_courses.Items)
				{
					ListItem listItem = (ListItem)obj;
					bool selected = listItem.Selected;
					if (selected)
					{
						list.Add(listItem.Value);
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00027FCC File Offset: 0x000261CC
		protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
		{
			List<string> selectedCourseVals = this.GetSelectedCourseVals();
			bool flag = selectedCourseVals != null && selectedCourseVals.Count < 1;
			if (flag)
			{
				this.Wizard1.ActiveStepIndex = 2;
				e.Cancel = true;
			}
			else
			{
				string text = this.txt_altid.Value.Trim();
				bool flag2 = string.IsNullOrEmpty(text);
				if (flag2)
				{
					string text2 = (WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetAuthenticatedUsername(this.Page) ?? "").Trim();
					bool flag3 = text2.Length > 0;
					if (flag3)
					{
						text = text2;
					}
				}
				INotetakingClientDataSyncWebClientManager notetakingClientDataSyncWebClientManager = new NotetakingClientDataSyncWebClientManager();
				GetNotetakerInfoAndCoursesInfo getNotetakerInfoAndCoursesInfo;
				NotetakerWithExternalCoursesDTO notetakerAndCourseInfo = notetakingClientDataSyncWebClientManager.GetNotetakerAndCourseInfo(false, this.Page, out getNotetakerInfoAndCoursesInfo);
				bool flag4 = notetakerAndCourseInfo != null && notetakerAndCourseInfo.Notetaker != null && string.IsNullOrEmpty(notetakerAndCourseInfo.Notetaker.UserName);
				if (flag4)
				{
					notetakerAndCourseInfo.Notetaker.UserName = text;
				}
				bool flag5 = text.Trim().Length < 1 || notetakerAndCourseInfo == null || notetakerAndCourseInfo.Notetaker == null || notetakerAndCourseInfo.Notetaker.UserName == null || !notetakerAndCourseInfo.Notetaker.UserName.Equals(text, StringComparison.OrdinalIgnoreCase);
				if (flag5)
				{
					CWLogger.Logger.Error("NotetakerAppNew.aspx:Wizard1_FinishButtonClick:FailedToRetrieveNotetakerWithExternalCoursesFromSession:AltId={0}", text);
					base.Response.Redirect("err.aspx?code=" + UserErrorCode.NotetakerAppNew.ToString(), true);
				}
				else
				{
					string text3 = this.txt_email2.Text.Trim();
					bool flag6 = text3.Length > 0;
					if (flag6)
					{
						notetakerAndCourseInfo.Notetaker.AlternateEmail = text3;
					}
					string text4 = this.txt_phoneCell.Text.Trim();
					bool flag7 = text4.Length > 0;
					if (flag7)
					{
						notetakerAndCourseInfo.Notetaker.Phone2 = text4;
					}
					string text5 = this.txt_address.Text.Trim();
					string text6 = this.txt_perm.Text.Trim();
					bool flag8 = text5.Length > 0;
					if (flag8)
					{
						notetakerAndCourseInfo.Notetaker.Address1 = text5;
					}
					bool flag9 = text6.Length > 0;
					if (flag9)
					{
						notetakerAndCourseInfo.Notetaker.Address2 = text6;
					}
					bool flag10 = this.chk_mailing.Checked || !this.chk_perm.Checked;
					if (flag10)
					{
						notetakerAndCourseInfo.Notetaker.Address1IsPrimary = true;
					}
					bool flag11 = (notetakerAndCourseInfo.Notetaker.Person.Student_no ?? "").Trim().Length < 1;
					if (flag11)
					{
						CWLogger.Logger.Trace("NotetakerAppNew.aspx:Can'tCreateNotetakerWithMissingStudentNumber:altid={0}", text.ToString());
						base.Response.Redirect("err.aspx?code=" + UserErrorCode.NotetakerAppNew.ToString(), true);
					}
					else
					{
						INotetakingClientManager notetakingClientManager = new NotetakingClientManager();
						int num = notetakingClientManager.CreateNotetakerAccount(notetakerAndCourseInfo.Notetaker);
						bool flag12 = num < 1;
						if (flag12)
						{
							CWLogger.Logger.Error("NotetakerAppNew.aspx:Wizard1_FinishButtonClick:FailedToCreateNotetakerAccount:altid={0}", text);
							base.Response.Redirect("err.aspx?code=" + UserErrorCode.NotetakerAppNew.ToString(), true);
						}
						else
						{
							CWLogger.Logger.Info("NotetakerAppNew.aspx:Wizard1_FinishButtonClick:CreatedNewNotetakerAccount:altid={0}:nid={1}", text ?? "NULL", num.ToString());
							ClockWorkIdentity currentClockWorkIdentity = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetCurrentClockWorkIdentity(this.Page);
							bool flag13 = currentClockWorkIdentity != null;
							if (flag13)
							{
								currentClockWorkIdentity.NotetakerId = num;
								WebAuthenticationAuthorizationWebClientManager.CurrentInstance.SetCurrentClockWorkIdentity(currentClockWorkIdentity);
							}
							bool flag14 = notetakerAndCourseInfo.ExternalCourses != null && notetakerAndCourseInfo.ExternalCourses.Count > 0;
							if (flag14)
							{
								bool flag15 = selectedCourseVals == null;
								List<DataSyncExternalCourseDTO> list;
								if (flag15)
								{
									list = null;
								}
								else
								{
									list = new List<DataSyncExternalCourseDTO>();
									foreach (DataSyncExternalCourseDTO dataSyncExternalCourseDTO in notetakerAndCourseInfo.ExternalCourses)
									{
										string externalCourseUniqueId = this.GetExternalCourseUniqueId(dataSyncExternalCourseDTO);
										bool flag16 = selectedCourseVals.Contains(externalCourseUniqueId);
										if (flag16)
										{
											list.Add(dataSyncExternalCourseDTO);
										}
									}
								}
								notetakingClientManager.AddPotentialCoursesForNotetaker(num, list);
							}
							this.Session.Remove("notetakerwithextcourses");
							this.SendEmail();
						}
					}
				}
			}
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00028418 File Offset: 0x00026618
		protected void Wizard1_ActiveStepChanged(object sender, EventArgs e)
		{
			bool flag = this.Wizard1.ActiveStepIndex > 1;
			if (flag)
			{
				bool flag2 = !this.chk_iagree.Checked;
				if (flag2)
				{
					this.Wizard1.ActiveStepIndex = 1;
				}
			}
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00028459 File Offset: 0x00026659
		protected void Button1_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("notetakerapp.aspx", true);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00028470 File Offset: 0x00026670
		private void SendEmail()
		{
			StringDictionary stringDictionary = new StringDictionary
			{
				{
					"email",
					this.txt_email.Text
				},
				{
					"firstname",
					this.txt_firstName.Text
				},
				{
					"lastname",
					this.txt_lastname.Text
				},
				{
					"student_no",
					this.txt_student_no.Text
				}
			};
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in this.chks_courses.Items)
			{
				ListItem listItem = (ListItem)obj;
				bool flag = !listItem.Selected;
				if (!flag)
				{
					bool flag2 = stringBuilder.Length > 0;
					if (flag2)
					{
						stringBuilder.Append(Environment.NewLine);
					}
					stringBuilder.Append(listItem.Text);
				}
			}
			stringDictionary.Add("courses", stringBuilder.ToString());
			IMailMergeCodes mailMergeCodes = new MailMergeCodes();
			stringDictionary.Add("from", mailMergeCodes.GetDefaultFromAddress(eWebModule.Notetaking));
			stringDictionary.Add("signature", mailMergeCodes.GetDefaultSignature(eWebModule.Notetaking));
			IEmailClientManager emailClientManager = new EmailClientManager();
			MailMergeContextDTO mailMergeContext = new MailMergeContextDTO();
			emailClientManager.SendEmail(Setting.NOTETAKINGB_Email_RequestSampleNotes, mailMergeContext, stringDictionary.InsertBaseUserMailMergeValues(), "NotetakingNotetakers_NotetakerAppNew");
			emailClientManager.SendEmail(Setting.NOTETAKINGB_Email_NewNotetakerSignup, mailMergeContext, stringDictionary.InsertBaseUserMailMergeValues(), "NotetakingNotetakers_NotetakerAppNew2");
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x000285F4 File Offset: 0x000267F4
		protected void btn_noSampleNotes_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("info1.aspx", true);
		}

		// Token: 0x04000351 RID: 849
		protected ScriptManager bbb;

		// Token: 0x04000352 RID: 850
		protected Label lblIntro;

		// Token: 0x04000353 RID: 851
		protected Panel p_topmsg;

		// Token: 0x04000354 RID: 852
		protected Image img_topmsg;

		// Token: 0x04000355 RID: 853
		protected Label lbl_topmsg;

		// Token: 0x04000356 RID: 854
		protected Wizard Wizard1;

		// Token: 0x04000357 RID: 855
		protected WizardStep step_welcome;

		// Token: 0x04000358 RID: 856
		protected Panel pProfile;

		// Token: 0x04000359 RID: 857
		protected Label lbl_fn;

		// Token: 0x0400035A RID: 858
		protected TextBox txt_firstName;

		// Token: 0x0400035B RID: 859
		protected RequiredFieldValidator val_firstName;

		// Token: 0x0400035C RID: 860
		protected HiddenField txt_altid;

		// Token: 0x0400035D RID: 861
		protected Label Label1;

		// Token: 0x0400035E RID: 862
		protected TextBox txt_lastname;

		// Token: 0x0400035F RID: 863
		protected RequiredFieldValidator val_lastname;

		// Token: 0x04000360 RID: 864
		protected Label Label2;

		// Token: 0x04000361 RID: 865
		protected TextBox txt_student_no;

		// Token: 0x04000362 RID: 866
		protected RequiredFieldValidator val_student_no;

		// Token: 0x04000363 RID: 867
		protected Label Label3;

		// Token: 0x04000364 RID: 868
		protected TextBox txt_email;

		// Token: 0x04000365 RID: 869
		protected RequiredFieldValidator val_email1;

		// Token: 0x04000366 RID: 870
		protected Label Label4;

		// Token: 0x04000367 RID: 871
		protected TextBox txt_email2;

		// Token: 0x04000368 RID: 872
		protected RequiredFieldValidator val_email2;

		// Token: 0x04000369 RID: 873
		protected Label lbl_emailIntro;

		// Token: 0x0400036A RID: 874
		protected CheckBox chk_mailing;

		// Token: 0x0400036B RID: 875
		protected Label Label7;

		// Token: 0x0400036C RID: 876
		protected TextBox txt_address;

		// Token: 0x0400036D RID: 877
		protected RequiredFieldValidator val_address1;

		// Token: 0x0400036E RID: 878
		protected CheckBox chk_perm;

		// Token: 0x0400036F RID: 879
		protected Label Label8;

		// Token: 0x04000370 RID: 880
		protected TextBox txt_perm;

		// Token: 0x04000371 RID: 881
		protected RequiredFieldValidator val_address2;

		// Token: 0x04000372 RID: 882
		protected Label lbl_mailingAddressIntro;

		// Token: 0x04000373 RID: 883
		protected Label Label5;

		// Token: 0x04000374 RID: 884
		protected TextBox txt_phoneHome;

		// Token: 0x04000375 RID: 885
		protected RequiredFieldValidator val_phone1;

		// Token: 0x04000376 RID: 886
		protected Label Label6;

		// Token: 0x04000377 RID: 887
		protected TextBox txt_phoneCell;

		// Token: 0x04000378 RID: 888
		protected RequiredFieldValidator val_phone2;

		// Token: 0x04000379 RID: 889
		protected WizardStep WizardStep1;

		// Token: 0x0400037A RID: 890
		protected Label lbl_confidentiality;

		// Token: 0x0400037B RID: 891
		protected CheckBox chk_iagree;

		// Token: 0x0400037C RID: 892
		protected CheckBoxValidator chk_iagree_validator;

		// Token: 0x0400037D RID: 893
		protected WizardStep step_courses;

		// Token: 0x0400037E RID: 894
		protected Panel p_addCourse;

		// Token: 0x0400037F RID: 895
		protected Label lbl_subject;

		// Token: 0x04000380 RID: 896
		protected RadComboBox cmb_subject;

		// Token: 0x04000381 RID: 897
		protected TextBox txt_subject;

		// Token: 0x04000382 RID: 898
		protected AutoCompleteExtender asldkjf;

		// Token: 0x04000383 RID: 899
		protected Label lbl_chooseCourses;

		// Token: 0x04000384 RID: 900
		protected CheckBoxList chks_courses;

		// Token: 0x04000385 RID: 901
		protected WizardStep step_upload;

		// Token: 0x04000386 RID: 902
		protected Label lbl_t;

		// Token: 0x04000387 RID: 903
		protected Label lbl_notesLegend;

		// Token: 0x04000388 RID: 904
		protected Button Button1;

		// Token: 0x04000389 RID: 905
		protected Button btn_noSampleNotes;

		// Token: 0x0400038A RID: 906
		protected Panel p_step3;

		// Token: 0x0400038B RID: 907
		protected Panel p_courseList;

		// Token: 0x0400038C RID: 908
		protected RadGrid gv_courses;

		// Token: 0x0400038D RID: 909
		protected Panel p_additionalInfo;

		// Token: 0x0400038E RID: 910
		protected Label lbl_additionalInfo;
	}
}
