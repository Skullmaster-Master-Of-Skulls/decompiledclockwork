using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ClockWorkController;
using ClockWorkLogger;
using ClockWorkWebAPI;
using ClockWorkWebAPI.TestBooking;
using ClockWorkWebAPIWeb;
using Databases;
using NewBooker.Entities.AutoTestBooking.Booker2;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Booker2;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.ClientManager.Core.AppointmentsTestBooking;
using TechnoPro.Common.ClientManager.Core.CourseRegistrations;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.LookupCourses;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.ClientManager.ICore.CourseRegistrations;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.Settings.Adapters;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.AppointmentsTestBooking;
using TechnoPro.Common.UI.ClientManager.Web.Core.DynamicForms;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.DynamicForms;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.AppointmentsTestBooking;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Email;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.MailMerging;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.MailMerging;
using TechnoPro.Common.UI.Web.Entity;
using TechnoPro.Common.UI.Web.Entity.AppointmentsTestBooking.AutoTestBooking;
using TechnoPro.Common.UI.Web.Entity.Modules;
using TechnoPro.Common.UI.Web.Entity.Web;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.test
{
	// Token: 0x02000066 RID: 102
	public class user_test_bookexam : Page
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x000145C4 File Offset: 0x000127C4
		private T GetSettingValue<T>(Setting setting)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			return webSettingsClientManager.GetSettingValue<T>(setting);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x000145E4 File Offset: 0x000127E4
		private Label GetWizardStepLabel(TemplatedWizardStep step)
		{
			bool flag = step.ID == this.step_welcome.ID;
			string text;
			if (flag)
			{
				text = "lbl_welcome";
			}
			else
			{
				bool flag2 = step.ID == this.step_selectCourse.ID;
				if (flag2)
				{
					text = "lblTitle";
				}
				else
				{
					bool flag3 = step.ID == this.step_classdatetime.ID;
					if (flag3)
					{
						text = "Label5";
					}
					else
					{
						bool flag4 = step.ID == this.step_confirmProfInfo.ID;
						if (flag4)
						{
							text = "Label1";
						}
						else
						{
							bool flag5 = step.ID == this.step_chooseAccommodations.ID;
							if (flag5)
							{
								text = "lbl_chooseAccommodations";
							}
							else
							{
								bool flag6 = step.ID == this.step_selectTime.ID;
								if (flag6)
								{
									text = "lbl_selectTime";
								}
								else
								{
									bool flag7 = step.ID == this.step_confirmAndComplete.ID;
									if (flag7)
									{
										text = "lbl_confirmAndCompleteTitle";
									}
									else
									{
										bool flag8 = step.ID == this.step_additionalInfo.ID;
										if (flag8)
										{
											text = "lbl_title_additionalRequirements";
										}
										else
										{
											text = null;
										}
									}
								}
							}
						}
					}
				}
			}
			bool flag9 = !string.IsNullOrEmpty(text);
			Label result;
			if (flag9)
			{
				result = (Label)step.ContentTemplateContainer.FindControl(text);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00014748 File Offset: 0x00012948
		private void ChangeWizardStepTitle(TemplatedWizardStep step, string newTitle)
		{
			bool flag = !string.IsNullOrEmpty(newTitle);
			if (flag)
			{
				step.Title = newTitle;
				Label wizardStepLabel = this.GetWizardStepLabel(step);
				bool flag2 = wizardStepLabel != null;
				if (flag2)
				{
					wizardStepLabel.Text = newTitle;
				}
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00014788 File Offset: 0x00012988
		private void Page_Init(object sender, EventArgs e)
		{
			int settingValue = this.GetSettingValue<int>(Setting.EXAMBOOKING_WizardSetting_AdditionalInformationScreenNum);
			bool settingValue2 = this.GetSettingValue<bool>(Setting.EXAMBOOKING_dontAskStudentToConfirmInstructorInformation);
			bool flag = false;
			bool flag2 = settingValue2;
			if (flag2)
			{
				this.step_confirmProfInfo.Title = " ";
				this.step_chooseAccommodations.Title = string.Format("{0}{1}", "3", this.step_chooseAccommodations.Title.Substring(1));
				this.lbl_chooseAccommodations.Text = this.step_chooseAccommodations.Title;
				flag = true;
				this.p_instructorVal.Visible = false;
			}
			bool flag3 = settingValue > 0;
			if (flag3)
			{
				int num = settingValue2 ? 4 : 5;
				string arg = num.ToString();
				WizardStep wizardStep = new WizardStep();
				string text = this.GetSettingValue<bool>(Setting.EXAMBOOKING_CustomWizardStepRewording_Enabled) ? this.GetSettingValue<string>(Setting.EXAMBOOKING_CustomWizardStepRewording_StepAdditionalInfo) : "5. Additional Requirements";
				bool flag4 = string.IsNullOrEmpty(text);
				if (flag4)
				{
					text = "5. Additional Requirements";
				}
				wizardStep.Title = string.Format("{0}. {1}", arg, (text.Length > 3) ? text.Substring(3) : text);
				wizardStep.ID = "step_additionalRequirements";
				Label label = new Label
				{
					Text = string.Format("<h1 class='PageTitle'>{0}</h1>", wizardStep.Title)
				};
				wizardStep.Controls.Add(label);
				this.step_additionalInfo = wizardStep;
				label = new Label();
				label.Text = "Please fill in the appropriate information below.";
				label.CssClass = "Intro4";
				wizardStep.Controls.Add(label);
				Panel panel = new Panel
				{
					ID = "p_data",
					CssClass = "DynamicForm"
				};
				wizardStep.Controls.Add(panel);
				this.Wizard1.WizardSteps.Insert(num, wizardStep);
				this.step_selectTime.Title = string.Format("{0}{1}", (num + 1).ToString(), this.step_selectTime.Title.Substring(1));
				this.step_confirmAndComplete.Title = string.Format("{0}{1}", (num + 2).ToString(), this.step_confirmAndComplete.Title.Substring(1));
				this.lbl_selectTime.Text = this.step_selectTime.Title;
				this.lbl_confirmAndCompleteTitle.Text = this.step_confirmAndComplete.Title;
				this.AddWizardControls(settingValue, panel);
			}
			bool flag5 = flag;
			if (flag5)
			{
				int num2 = 1;
				for (int i = 1; i < this.Wizard1.WizardSteps.Count; i++)
				{
					WizardStepBase wizardStepBase = this.Wizard1.WizardSteps[i];
					string title = wizardStepBase.Title;
					bool flag6 = !string.IsNullOrEmpty(title.Trim());
					if (flag6)
					{
						string format = "{0}{1}";
						int num3 = num2;
						num2 = num3 + 1;
						string text2 = string.Format(format, num3.ToString(), title.Substring(1));
						Label label2 = (wizardStepBase.Controls.Count > 0 && wizardStepBase.Controls[0] is Label) ? ((Label)wizardStepBase.Controls[0]) : null;
						bool flag7 = label2 != null && label2.Text == wizardStepBase.Title;
						if (flag7)
						{
							label2.Text = text2;
						}
						wizardStepBase.Title = text2;
					}
				}
				this.lbl_selectTime.Text = this.step_selectTime.Title;
				this.lbl_confirmAndCompleteTitle.Text = this.step_confirmAndComplete.Title;
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00014B30 File Offset: 0x00012D30
		private void AddWizardControls(int screenNum, Panel p_data)
		{
			DynamicControlLayoutHelper dynamicControlLayoutHelper = new DynamicControlLayoutHelper();
			DynamicScreenLayout.ControlsToScreen(ref dynamicControlLayoutHelper, base.Cache, screenNum, p_data, null, false, false, "");
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00014B5C File Offset: 0x00012D5C
		private int LookupStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00014B80 File Offset: 0x00012D80
		private string finishButtonText
		{
			get
			{
				return this.GetSettingValue<string>(Setting.EXAMBOOKING_WizardSetting_ConfirmBookingFinishButtonText);
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00014BA0 File Offset: 0x00012DA0
		public string hf_maxdurationclientid()
		{
			return this.GetControl<HiddenField>(this.step_classdatetime, "hf_maxduration").ClientID;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00014BC8 File Offset: 0x00012DC8
		private DateTime GetCutoffDate()
		{
			string settingValue = this.GetSettingValue<string>(Setting.EXAMBOOKING_CutoffBookingDate);
			bool flag = !string.IsNullOrEmpty(settingValue);
			if (flag)
			{
				CutoffTime cutoffTime = settingValue.CutoffTimeFromXml() ?? CutoffTime.None;
				bool enabled = cutoffTime.Enabled;
				if (enabled)
				{
					DateTime? minimumDateForBeforeTypeCutoff = cutoffTime.GetMinimumDateForBeforeTypeCutoff();
					bool flag2 = minimumDateForBeforeTypeCutoff != null;
					if (flag2)
					{
						return minimumDateForBeforeTypeCutoff.Value;
					}
				}
			}
			return DateTime.Now.Date.AddDays((double)this.GetSettingValue<int>(Setting.EXAMBOOKING_WizardSetting_MinDaysAheadToBook));
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00014C58 File Offset: 0x00012E58
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			bool settingValue = this.GetSettingValue<bool>(Setting.EXAMBOOKING_StudentsAllowedToBookExams);
			bool flag = !settingValue;
			if (flag)
			{
				NavigatorClientManager.CurrentInstance.NotAllowed(Setting.EXAMBOOKING_ErrorMessage_ModuleInactive, this.Page);
			}
			else
			{
				Control control = this.Wizard1.FindControl("SideBarContainer");
				DataList dataList = (DataList)control.FindControl("SideBarList");
				dataList.ItemCreated += this.lst_ItemCreated;
				dataList.ItemDataBound += this.lst_ItemDataBound;
				bool settingValue2 = this.GetSettingValue<bool>(Setting.EXAMBOOKING_AllowStudentsToBookMultipleExams);
				bool flag2 = settingValue2;
				if (flag2)
				{
					base.Response.Redirect("bookexams.aspx", true);
				}
				else
				{
					bool settingValue3 = this.GetSettingValue<bool>(Setting.EXAMBOOKING_FinalExamRequest_Enabled);
					bool flag3 = settingValue3;
					if (flag3)
					{
						base.Response.Redirect("bookexam2.aspx", true);
					}
					else
					{
						int num = this.LookupStudentPid();
						bool flag4 = num < 1;
						if (flag4)
						{
							NavigatorClientManager.CurrentInstance.NotAllowed(Setting.GENERAL_ErrorMessage_NotAClockWorkStudent, this.Page);
						}
						else
						{
							DateTime cutoffDate = this.GetCutoffDate();
							IAccommodationsWebClientManager accommodationsWebClientManager = new AccommodationsWebClientManager();
							bool flag5 = accommodationsWebClientManager.AreAccommodationsCurrentlyExpired(num, cutoffDate, true);
							bool flag6 = flag5;
							if (flag6)
							{
								NavigatorClientManager.CurrentInstance.NotAllowed(Setting.EXAMBOOKING_ErrorMessage_AccommodationsExpired, this.Page);
							}
							else
							{
								bool settingValue4 = this.GetSettingValue<bool>(Setting.EXAMBOOKING_CustomAllowStudentToBookCheckSqlEnabled);
								bool flag7 = settingValue4;
								if (flag7)
								{
									string settingValue5 = this.GetSettingValue<string>(Setting.EXAMBOOKING_CustomAllowStudentToBookCheckSql);
									bool flag8 = !string.IsNullOrEmpty(settingValue5);
									if (flag8)
									{
										DataTable dataTable = new DataTable();
										try
										{
											DbParameter[] parameters = new DbParameter[]
											{
												clockWork.GetParameter("@pid", DbType.Int32, num)
											};
											dataTable = clockWork.ExecuteQuery(settingValue5, parameters);
											bool flag9 = dataTable.Rows.Count > 0;
											if (flag9)
											{
												string value = dataTable.Rows[0][0].ToString().Trim();
												bool flag10 = !string.IsNullOrEmpty(value);
												if (flag10)
												{
													CacheStorageManager.Current.Insert("web_exam_custom_check_emsg_" + num.ToString(), value);
													NavigatorClientManager.CurrentInstance.NotAllowed(Setting.EXAMBOOKING_CustomAllowStudentToBookCheckSql, this.Page);
													return;
												}
											}
										}
										catch
										{
										}
									}
								}
								bool settingValue6 = this.GetSettingValue<bool>(Setting.EXAMBOOKING_EnforceRegistrationDateRange);
								bool flag11 = settingValue6;
								if (flag11)
								{
									DateTime settingValue7 = this.GetSettingValue<DateTime>(Setting.EXAMBOOKING_RegistrationStartDate);
									DateTime settingValue8 = this.GetSettingValue<DateTime>(Setting.EXAMBOOKING_RegistrationEndDate);
									DateTime date = DateTime.Now.Date;
									bool flag12 = date < settingValue7 || date > settingValue8;
									if (flag12)
									{
										NavigatorClientManager.CurrentInstance.NotAllowed(Setting.EXAMBOOKING_ErrorMessage_NotInRegistrationDateRange, this.Page);
									}
								}
								bool flag13 = !this.Page.IsPostBack;
								if (flag13)
								{
									bool flag14 = base.Master != null && base.Master is IClockWorkMasterPage;
									if (flag14)
									{
										((IClockWorkMasterPage)base.Master).SetCurrentPage(eClockWorkWebPage.TestBooking_BookExam);
									}
									IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
									bool settingValue9 = webSettingsClientManager.GetSettingValue<bool>(Setting.EXAMBOOKING_HideCheckAllCheckNone);
									bool flag15 = settingValue9;
									if (flag15)
									{
										this.p_checkAll.Visible = false;
									}
									bool settingValue10 = this.GetSettingValue<bool>(Setting.EXAMBOOKING_CustomWizardStepRewording_Enabled);
									string text = "";
									bool flag16 = settingValue10;
									if (flag16)
									{
										this.ChangeWizardStepTitle(this.step_welcome, this.GetSettingValue<string>(Setting.EXAMBOOKING_CustomWizardStepRewording_StepWelcome));
										this.ChangeWizardStepTitle(this.step_selectCourse, this.GetSettingValue<string>(Setting.EXAMBOOKING_CustomWizardStepRewording_StepSelectCourse));
										this.ChangeWizardStepTitle(this.step_classdatetime, this.GetSettingValue<string>(Setting.EXAMBOOKING_CustomWizardStepRewording_StepIndicateClassDateTime));
										bool settingValue11 = this.GetSettingValue<bool>(Setting.EXAMBOOKING_dontAskStudentToConfirmInstructorInformation);
										bool flag17 = !settingValue11;
										if (flag17)
										{
											this.ChangeWizardStepTitle(this.step_confirmProfInfo, this.GetSettingValue<string>(Setting.EXAMBOOKING_CustomWizardStepRewording_StepConfirmInstructorInfo));
										}
										this.ChangeWizardStepTitle(this.step_chooseAccommodations, this.GetSettingValue<string>(Setting.EXAMBOOKING_CustomWizardStepRewording_StepChooseAccommodations));
										text = this.GetSettingValue<string>(Setting.EXAMBOOKING_CustomWizardStepRewording_StepSelectScheduledTime);
										this.ChangeWizardStepTitle(this.step_selectTime, text);
										this.ChangeWizardStepTitle(this.step_confirmAndComplete, this.GetSettingValue<string>(Setting.EXAMBOOKING_CustomWizardStepRewording_StepConfirmAndComplete));
									}
									string onClientClick = "if (!confirm('Are you sure you want to cancel?')) return;";
									((Button)this.step_welcome.CustomNavigationTemplateContainer.FindControl("CancelButton")).OnClientClick = onClientClick;
									((Button)this.step_selectTime.CustomNavigationTemplateContainer.FindControl("CancelButton")).OnClientClick = onClientClick;
									((Button)this.step_selectCourse.CustomNavigationTemplateContainer.FindControl("CancelButton")).OnClientClick = onClientClick;
									((Button)this.step_confirmProfInfo.CustomNavigationTemplateContainer.FindControl("CancelButton")).OnClientClick = onClientClick;
									((Button)this.step_confirmAndComplete.CustomNavigationTemplateContainer.FindControl("CancelButton")).OnClientClick = onClientClick;
									((Button)this.step_classdatetime.CustomNavigationTemplateContainer.FindControl("CancelButton")).OnClientClick = onClientClick;
									((Button)this.step_chooseAccommodations.CustomNavigationTemplateContainer.FindControl("CancelButton")).OnClientClick = onClientClick;
									DateTime cutoffDate2 = this.GetCutoffDate();
									this.GetControl<HiddenField>(this.step_confirmProfInfo, "cutoffDate").Value = cutoffDate2.ToString("yyyy-MM-dd H:mm");
									Button button = this.Wizard1.FindControl("StartNavigationTemplateContainerID").FindControl("CancelButton") as Button;
									bool flag18 = button != null;
									if (flag18)
									{
										button.OnClientClick = "return confirm('Are you sure you want to cancel?')";
									}
									IAutoTestBookingWebClientManager autoTestBookingWebClientManager = new AutoTestBookingWebClientManager();
									MinMaxDateRangeValue minMaxDateRangeValue = autoTestBookingWebClientManager.FigureOutMinMaxDateRangeStudentIsAllowedToBookForExam(num);
									bool flag19 = minMaxDateRangeValue.Status != eMinMaxDateRangeInvalidReason.IsValid;
									if (flag19)
									{
										NavigatorClientManager.CurrentInstance.NotAllowed(eNotAllowedCode.InvalidMinMaxDatesForTestBooking, new Dictionary<string, string>
										{
											{
												"status",
												((int)minMaxDateRangeValue.Status).ToString()
											}
										}, this.Page);
									}
									else
									{
										this.txt_date.Attributes.Add("mindate", minMaxDateRangeValue.DateRange.Start.ToString("yyyy-MM-dd"));
										this.txt_date.Attributes.Add("maxdate", minMaxDateRangeValue.DateRange.End.ToString("yyyy-MM-dd"));
										this.Wizard1.CancelDestinationPageUrl = this.GetSettingValue<string>(Setting.TESTBOOKING_TestBookingCancelUrl);
										string text2 = this.GetSettingValue<string>(Setting.EXAMBOOKING_WizardSetting_WelcomeMsg);
										DateTime settingValue12 = this.GetSettingValue<DateTime>(Setting.EXAMBOOKING_FinalExamRequest_FinalsStartDate);
										DateTime settingValue13 = this.GetSettingValue<DateTime>(Setting.EXAMBOOKING_FinalExamRequest_FinalsEndDate);
										text2 = text2.Replace("#~startdate~#", settingValue12.ToString("MMMM d"));
										text2 = text2.Replace("#~enddate~#", settingValue13.ToString("MMMM d"));
										((Label)this.step_welcome.ContentTemplateContainer.FindControl("lbl_welcome")).Text = text2;
										this.lbl_pleaseselectadate.Text = this.GetSettingValue<string>(Setting.EXAMBOOKING_SelectADateTimeMessageToStudents);
										string settingValue14 = this.GetSettingValue<string>(Setting.EXAMBOOKING_WizardSetting_ConfirmBookingMsg);
										bool flag20 = settingValue14.Length > 0;
										if (flag20)
										{
											this.lbl_finishMessage.Text = settingValue14;
										}
										string settingValue15 = this.GetSettingValue<string>(Setting.EXAMBOOKING_WizardSetting_ConfirmationPage_IntroText);
										bool flag21 = settingValue15.Length > 0;
										if (flag21)
										{
											this.lbl_confirmationIntroMsg.Text = settingValue15;
											this.p_confirmationIntroMsg.Visible = true;
										}
										string settingValue16 = this.GetSettingValue<string>(Setting.EXAMBOOKING_WizardSetting_ConfirmationPage_IAgreeText);
										bool flag22 = settingValue16.Length > 0;
										if (flag22)
										{
											this.chk_iagree.Text = settingValue16;
										}
										DateTime t;
										DateTime dateTime;
										ClockWorkWebAPI.Core.GetTermStartEndDates(out t, out dateTime);
										DateTime date2 = DateTime.Now.Date;
										bool flag23 = t > date2;
										if (flag23)
										{
											t = date2;
										}
										string settingValue17 = this.GetSettingValue<string>(Setting.EXAMBOOKING_RestrictCoursesToCampus);
										bool settingValue18 = this.GetSettingValue<bool>(Setting.EXAMBOOKING_OnlyAllowBookingForCoursesThatHaveHadTheLOAGenerated);
										bool settingValue19 = this.GetSettingValue<bool>(Setting.EXAMBOOKING_OnlyAllowBookingForCoursesThatInstructorHasConfirmedReceiptOfLOAOnline);
										bool settingValue20 = this.GetSettingValue<bool>(Setting.EXAMBOOKING_OnlyAllowBookingForCoursesThatHaveApprovedAccommodationLetterRequest);
										ICourseRegistrationClientManager courseRegistrationClientManager = new CourseRegistrationClientManager();
										StudentCourseListDTO studentCourseListDTO = courseRegistrationClientManager.LoadCoursesStudentIsAllowedToBookTestsForNow(num);
										IList<CourseRegistrationDTO> list = (studentCourseListDTO != null) ? studentCourseListDTO.Courses : null;
										bool flag24 = list != null;
										if (flag24)
										{
											user_test_bookexam.<>c__DisplayClass11_0 CS$<>8__locals1 = new user_test_bookexam.<>c__DisplayClass11_0();
											bool flag25 = settingValue19;
											if (flag25)
											{
												list = (from g in list
												where g.DateLetterReturned != null
												select g).ToList<CourseRegistrationDTO>();
											}
											user_test_bookexam.<>c__DisplayClass11_0 CS$<>8__locals2 = CS$<>8__locals1;
											string[] onlyAllowTheseCampuses;
											if (!string.IsNullOrEmpty(settingValue17))
											{
												onlyAllowTheseCampuses = (from g in settingValue17.Split(new char[]
												{
													','
												})
												select g.Trim()).ToArray<string>();
											}
											else
											{
												onlyAllowTheseCampuses = new string[0];
											}
											CS$<>8__locals2.onlyAllowTheseCampuses = onlyAllowTheseCampuses;
											bool flag26 = CS$<>8__locals1.onlyAllowTheseCampuses.Length != 0;
											if (flag26)
											{
												list = (from g in list
												where CS$<>8__locals1.onlyAllowTheseCampuses.Any(delegate(string h)
												{
													LookupCourseDTO course = g.Course;
													return h.Equals((((course != null) ? course.Campus : null) ?? "").Trim(), StringComparison.OrdinalIgnoreCase);
												})
												select g).ToList<CourseRegistrationDTO>();
											}
											bool flag27 = settingValue18;
											if (flag27)
											{
												list = (from g in list
												where g.DateLetterIssued != null
												select g).ToList<CourseRegistrationDTO>();
											}
											bool flag28 = settingValue20;
											if (flag28)
											{
												list = (from g in list
												where g.CourseAccommodationRequestBase != null && g.CourseAccommodationRequestBase.Status == eStudentCourseAccommodationRequestStatusDTO.Approved
												select g).ToList<CourseRegistrationDTO>();
											}
										}
										bool flag29 = list.Count < 1;
										if (flag29)
										{
											bool flag30 = studentCourseListDTO != null && studentCourseListDTO.AtLeastOneCourseRemovedBecauseOfSpecialAccommodationNotAllowedToBookRestriction;
											if (flag30)
											{
												NavigatorClientManager.CurrentInstance.NotAllowed(eNotAllowedCode.NoCoursesAvailableToBookBecauseSpecialAccBanForExamBooking, new Dictionary<string, string>(), this.Page);
											}
											else
											{
												NavigatorClientManager.CurrentInstance.NotAllowed(Setting.EXAMBOOKING_ErrorMessage_NoCourses, this.Page);
											}
										}
										else
										{
											string text3 = this.GetSettingValue<string>(Setting.EXAMBOOKING_FilterCourseListByTimeOfDay).ToLower();
											string[] array = text3.Split(new char[]
											{
												','
											}, StringSplitOptions.RemoveEmptyEntries);
											bool flag31 = !string.IsNullOrEmpty(text3);
											for (int i = 0; i < list.Count; i++)
											{
												CourseRegistrationDTO courseRegistrationDTO = list[i];
												bool flag32 = flag31;
												bool flag33;
												if (flag32)
												{
													string text4 = courseRegistrationDTO.Course.TimeOfDay ?? "";
													flag33 = true;
													foreach (string value2 in array)
													{
														bool flag34 = text4.StartsWith(value2);
														if (flag34)
														{
															flag33 = false;
															break;
														}
													}
												}
												else
												{
													flag33 = true;
												}
												bool flag35 = flag33;
												if (flag35)
												{
													string text5 = ClockWorkWebAPI.Course.CourseToString(courseRegistrationDTO.Course);
													DateTime? dateTime2 = new DateTime?(courseRegistrationDTO.Course.StartDate);
													DateTime? dateTime3 = new DateTime?(courseRegistrationDTO.Course.EndDate);
													string value3 = string.Format("{0},{1},{2}", courseRegistrationDTO.Course.LuCourseId, (dateTime2 != null) ? dateTime2.Value.ToString("yyyy-MM-dd") : "", (dateTime3 != null) ? dateTime3.Value.ToString("yyyy-MM-dd") : "");
													ListItem item = new ListItem(text5, value3);
													this.cmb_course.Items.Add(item);
												}
											}
											int settingValue21 = this.GetSettingValue<int>(Setting.EXAMBOOKING_WizardSetting_AdditionalInformationScreenNum);
											bool flag36 = settingValue21 < 1;
											if (flag36)
											{
												this.lbl_additionalRequirements.Visible = false;
												this.lbl_additionalRequirementsValue.Visible = false;
											}
											bool settingValue22 = this.GetSettingValue<bool>(Setting.EXAMBOOKING_StudentAllowedToSelectOwnDateTime);
											this.btn_chooseAnotherDate.Visible = settingValue22;
											bool settingValue23 = this.GetSettingValue<bool>(Setting.EXAMBOOKING_StudentAllowedToSelectPreviousDateTimes);
											bool settingValue24 = this.GetSettingValue<bool>(Setting.EXAMBOOKING_StudentAllowedToSelectPreviousClassTestDefinitions);
											bool settingValue25 = this.GetSettingValue<bool>(Setting.EXAMBOOKING_StudentAllowedToSelectPreviousClassTestDefinitionsFromRegistrar);
											bool flag37 = settingValue23 || settingValue24 || settingValue25;
											this.btn_chooseExistingClassDateTime.Visible = flag37;
											bool flag38 = !flag37;
											if (flag38)
											{
												this.p_existingExams.Visible = false;
												this.p_classDateandTime.Visible = true;
												string settingValue26 = this.GetSettingValue<string>(Setting.EXAMBOOKING_SelectClassDateTimeInstruction);
												bool flag39 = !string.IsNullOrEmpty(settingValue26);
												if (flag39)
												{
													this.lbl_enterClassDateTimeDuration.Text = settingValue26;
												}
											}
											else
											{
												this.lbl_enterClassDateTimeDuration.Visible = false;
											}
											bool settingValue27 = this.GetSettingValue<bool>(Setting.EXAMBOOKING_AskStudentForInstructorPhone);
											bool flag40 = settingValue27;
											if (flag40)
											{
												this.row_instructorPhone.Visible = true;
											}
											bool settingValue28 = this.GetSettingValue<bool>(Setting.EXAMBOOKING_AskStudentForCourseAlternateContactInfo);
											bool flag41 = !settingValue28;
											if (flag41)
											{
												this.row_altContact.Visible = false;
											}
											string settingValue29 = this.GetSettingValue<string>(Setting.EXAMBOOKING_ChooseAccommodationsInstructions);
											string settingValue30 = this.GetSettingValue<string>(Setting.EXAMBOOKING_ChooseAccommodationsNote);
											this.lbl_chooseAccommodationsInstructions.Text = settingValue29;
											this.lbl_accommodationsNote.Text = settingValue30;
											bool settingValue31 = this.GetSettingValue<bool>(Setting.EXAMBOOKING_AllowStudentToSelectFromApprovedDateTimes);
											bool flag42 = !settingValue31;
											if (flag42)
											{
												this.lbl_availableDatesTimesImportantNote.Text = this.lbl_availableDatesTimesImportantNote.Text.Replace("none of the dates/times below are possible", "the date/time below is not possible");
												this.rbtn_potentials.Visible = false;
												this.lbl_potential.Visible = true;
												string text6 = string.IsNullOrEmpty(text) ? "5. Search status" : text;
												this.lbl_selectTime.Text = this.lbl_selectTime.Text.Substring(0, 3) + text6.Substring(3);
												this.step_selectTime.Title = this.lbl_selectTime.Text;
												this.p_availableDatesTimes.GroupingText = "";
												this.lbl_availableDatesTimesImportantNote.Visible = false;
												this.lbl_potential.Visible = false;
												this.lbl_yourTestDateTime.Visible = false;
												this.lbl_yourTestDateTimeVal.Visible = false;
												this.lbl_yourTestDateTimeGap.Visible = false;
												this.lbl_yourTestDateTimeGap0.Visible = false;
											}
											string settingValue32 = this.GetSettingValue<string>(Setting.EXAMBOOKING_AvailableTestDateTimesImportantNote);
											bool flag43 = !string.IsNullOrEmpty(settingValue32);
											if (flag43)
											{
												this.lbl_availableDatesTimesImportantNote.Text = settingValue32;
											}
											string settingValue33 = this.GetSettingValue<string>(Setting.EXAMBOOKING_NoRoomFoundMessage);
											string settingValue34 = this.GetSettingValue<string>(Setting.EXAMBOOKING_RoomFoundMessage);
											this.lbl_nodates.Text = settingValue33;
											this.lbl_dateFound.Text = settingValue34;
											string settingValue35 = this.GetSettingValue<string>(Setting.EXAMBOOKING_ClassDateTimeIntro);
											bool flag44 = !string.IsNullOrEmpty(settingValue35);
											if (flag44)
											{
												this.lbl_classDateTimeIntro.Text = settingValue35;
												this.lbl_classDateTimeIntro.Visible = true;
											}
											string settingValue36 = webSettingsClientManager.GetSettingValue<string>(Setting.EXAMBOOKING_MessageWhenNoClassDatesAndTimesAreAvailableToChooseFrom);
											this.lbl_noExistingClassDateTimes.Text = settingValue36;
											int maxDuration = this.GetMaxDuration();
											HiddenField control2 = this.GetControl<HiddenField>(this.step_classdatetime, "hf_maxduration");
											control2.Value = maxDuration.ToString();
											string settingValue37 = webSettingsClientManager.GetSettingValue<string>(Setting.EXAMBOOKING_SelectCourseInstructionMessage);
											bool flag45 = !string.IsNullOrEmpty(settingValue37);
											if (flag45)
											{
												this.GetControl<Panel>(this.step_selectCourse, "p_courseInstruction").Visible = true;
												this.GetControl<Label>(this.step_selectCourse, "lbl_courseInstruction").Text = settingValue37;
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00015B2C File Offset: 0x00013D2C
		public string txtClassStartTimeClientId()
		{
			return this.GetControl<TextBox>(this.step_classdatetime, "classStartTimePicker").ClientID;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00015B54 File Offset: 0x00013D54
		private void lst_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			DataListItem item = e.Item;
			Control control = item.FindControl("SideBarButton");
			bool flag = control != null && control is LinkButton;
			if (flag)
			{
				LinkButton linkButton = (LinkButton)control;
				bool flag2 = linkButton.Text.Equals(" ");
				if (flag2)
				{
					linkButton.Style.Add(HtmlTextWriterStyle.Display, "none");
					linkButton.Attributes.Add("alt", "separator");
				}
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00003E0A File Offset: 0x0000200A
		private void lst_ItemCreated(object sender, DataListItemEventArgs e)
		{
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00015BD4 File Offset: 0x00013DD4
		public string potentialtimesclientid()
		{
			return this.GetControl<RadioButtonList>(this.step_selectTime, "rbtn_potentials").ClientID;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00015BFC File Offset: 0x00013DFC
		public string existingclassdatetimes()
		{
			return this.GetControl<RadioButtonList>(this.step_classdatetime, "rbtns_existingClassDateTimes").ClientID;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00015C24 File Offset: 0x00013E24
		private Panel p_checkAll
		{
			get
			{
				return (Panel)this.step_chooseAccommodations.ContentTemplateContainer.FindControl("p_checkAllCheckNone");
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00015C50 File Offset: 0x00013E50
		private Label lbl_enterClassDateTimeDuration
		{
			get
			{
				return (Label)this.step_classdatetime.ContentTemplateContainer.FindControl("lbl_enterClassDateTimeDuration");
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00015C7C File Offset: 0x00013E7C
		private Label lbl_availableDatesTimesImportantNote
		{
			get
			{
				return (Label)this.step_selectTime.ContentTemplateContainer.FindControl("lbl_availableDatesTimesImportantNote");
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00015CA8 File Offset: 0x00013EA8
		private Label lbl_potential
		{
			get
			{
				return (Label)this.step_selectTime.ContentTemplateContainer.FindControl("lbl_potential");
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00015CD4 File Offset: 0x00013ED4
		private Label lbl_accommodations
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_accommodations");
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00015D00 File Offset: 0x00013F00
		private Label lbl_chooseAccommodationsInstructions
		{
			get
			{
				return (Label)this.step_chooseAccommodations.ContentTemplateContainer.FindControl("lbl_chooseAccommodationsInstructions");
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00015D2C File Offset: 0x00013F2C
		private Label lbl_accommodationsNote
		{
			get
			{
				return (Label)this.step_chooseAccommodations.ContentTemplateContainer.FindControl("lbl_accommodationsNote");
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00015D58 File Offset: 0x00013F58
		private TextBox txt_instructorPhone
		{
			get
			{
				return (TextBox)this.step_confirmProfInfo.ContentTemplateContainer.FindControl("txt_instructorPhone");
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00015D84 File Offset: 0x00013F84
		private TableRow row_instructorPhone
		{
			get
			{
				return (TableRow)this.step_confirmProfInfo.ContentTemplateContainer.FindControl("row_instructorPhone");
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00015DB0 File Offset: 0x00013FB0
		private Panel p_availableDatesTimes
		{
			get
			{
				return (Panel)this.step_selectTime.ContentTemplateContainer.FindControl("p_availableDatesTimes");
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00015DDC File Offset: 0x00013FDC
		private Panel p_available
		{
			get
			{
				return (Panel)this.step_chooseAccommodations.ContentTemplateContainer.FindControl("p_available");
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00015E08 File Offset: 0x00014008
		private Panel p_instructorInfo
		{
			get
			{
				return (Panel)this.step_confirmProfInfo.ContentTemplateContainer.FindControl("p_instructorInfo");
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00015E34 File Offset: 0x00014034
		private Panel p_welcome
		{
			get
			{
				return (Panel)this.step_welcome.ContentTemplateContainer.FindControl("p_welcome");
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00015E60 File Offset: 0x00014060
		private Label lbl_emsg
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_emsg");
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00015E8C File Offset: 0x0001408C
		private Label lbl_classDateTimeIntro
		{
			get
			{
				return (Label)this.step_classdatetime.ContentTemplateContainer.FindControl("lbl_classDateTimeIntro");
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00015EB8 File Offset: 0x000140B8
		private Label lbl_chooseAccommodations
		{
			get
			{
				return (Label)this.step_chooseAccommodations.ContentTemplateContainer.FindControl("lbl_chooseAccommodations");
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00015EE4 File Offset: 0x000140E4
		private Panel p_emsg
		{
			get
			{
				return (Panel)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("p_emsg");
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x00015F10 File Offset: 0x00014110
		private TextBox txt_altProfEmail
		{
			get
			{
				return (TextBox)this.step_confirmProfInfo.ContentTemplateContainer.FindControl("txt_altProfEmail");
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x00015F3C File Offset: 0x0001413C
		private Label lbl_noExistingClassDateTimes
		{
			get
			{
				return (Label)this.step_classdatetime.ContentTemplateContainer.FindControl("lbl_noExistingClassDateTimes");
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002DA RID: 730 RVA: 0x00015F68 File Offset: 0x00014168
		private Label lbl_courseDescription
		{
			get
			{
				return (Label)this.step_confirmProfInfo.ContentTemplateContainer.FindControl("lbl_courseDescription");
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00015F94 File Offset: 0x00014194
		private RadTimePicker txt_classTestStartTime
		{
			get
			{
				return (RadTimePicker)this.step_classdatetime.ContentTemplateContainer.FindControl("txt_classTestStartTime");
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002DC RID: 732 RVA: 0x00015FC0 File Offset: 0x000141C0
		private HiddenField lastSelectedLucid
		{
			get
			{
				return (HiddenField)this.step_confirmProfInfo.ContentTemplateContainer.FindControl("lastSelectedLucid");
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00015FEC File Offset: 0x000141EC
		private RadioButtonList rbtns_existingClassDateTimes
		{
			get
			{
				return (RadioButtonList)this.step_classdatetime.ContentTemplateContainer.FindControl("rbtns_existingClassDateTimes");
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00016018 File Offset: 0x00014218
		private HiddenField lbl_usingExistingClassDateTime
		{
			get
			{
				return (HiddenField)this.step_classdatetime.ContentTemplateContainer.FindControl("lbl_usingExistingClassDateTime");
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00016044 File Offset: 0x00014244
		private RadListBox lb_accommodations
		{
			get
			{
				return (RadListBox)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lb_accommodations");
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00016070 File Offset: 0x00014270
		private TextBox txt_instructorEmail
		{
			get
			{
				return (TextBox)this.step_confirmProfInfo.ContentTemplateContainer.FindControl("txt_instructorEmail");
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0001609C File Offset: 0x0001429C
		private Label lbl_instructorVal
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_instructorVal");
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x000160C8 File Offset: 0x000142C8
		private Panel p_instructorVal
		{
			get
			{
				return (Panel)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("p_instructorVal");
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x000160F4 File Offset: 0x000142F4
		private Label lbl_courseVal
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_courseVal");
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00016120 File Offset: 0x00014320
		private Label lbl_classDateTimeVal
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_classDateTimeVal");
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0001614C File Offset: 0x0001434C
		private Label lbl_yourTestDateTimeVal
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_yourTestDateTimeVal");
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00016178 File Offset: 0x00014378
		private Label lbl_yourTestDateTime
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_yourTestDateTime");
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x000161A4 File Offset: 0x000143A4
		private Label lbl_yourTestDateTimeGap
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_yourTestDateTimeGap");
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x000161D0 File Offset: 0x000143D0
		private Label lbl_yourTestDateTimeGap0
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_yourTestDateTimeGap0");
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x000161FC File Offset: 0x000143FC
		private Label lbl_nodates
		{
			get
			{
				return (Label)this.step_selectTime.ContentTemplateContainer.FindControl("lbl_nodates");
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00016228 File Offset: 0x00014428
		private Label lbl_dateFound
		{
			get
			{
				return (Label)this.step_selectTime.ContentTemplateContainer.FindControl("lbl_dateFound");
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00016254 File Offset: 0x00014454
		private RadioButtonList rbtn_potentials
		{
			get
			{
				return (RadioButtonList)this.step_selectTime.ContentTemplateContainer.FindControl("rbtn_potentials");
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00016280 File Offset: 0x00014480
		private CheckBoxList chk_accommodations
		{
			get
			{
				return (CheckBoxList)this.step_chooseAccommodations.ContentTemplateContainer.FindControl("chk_accommodations");
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002ED RID: 749 RVA: 0x000162AC File Offset: 0x000144AC
		private TextBox txt_instructorName
		{
			get
			{
				return (TextBox)this.step_confirmProfInfo.ContentTemplateContainer.FindControl("txt_instructorName");
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002EE RID: 750 RVA: 0x000162D8 File Offset: 0x000144D8
		private Panel p_classDateandTime
		{
			get
			{
				return (Panel)this.step_classdatetime.ContentTemplateContainer.FindControl("p_classDateandTime");
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00016304 File Offset: 0x00014504
		private Panel p_existingExams
		{
			get
			{
				return (Panel)this.step_classdatetime.ContentTemplateContainer.FindControl("p_existingExams");
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00016330 File Offset: 0x00014530
		private LinkButton btn_chooseExistingClassDateTime
		{
			get
			{
				return (LinkButton)this.step_classdatetime.ContentTemplateContainer.FindControl("btn_chooseExistingClassDateTime");
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0001635C File Offset: 0x0001455C
		private LinkButton btn_chooseAnotherDate
		{
			get
			{
				return (LinkButton)this.step_classdatetime.ContentTemplateContainer.FindControl("btn_chooseAnotherDate");
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00016388 File Offset: 0x00014588
		private Label lbl_additionalRequirementsValue
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_additionalRequirementsValue");
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x000163B4 File Offset: 0x000145B4
		private CheckBox chk_iagree
		{
			get
			{
				return (CheckBox)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("chk_iagree");
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x000163E0 File Offset: 0x000145E0
		private Label lbl_additionalRequirements
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_additionalRequirements");
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0001640C File Offset: 0x0001460C
		private Panel p_confirmationIntroMsg
		{
			get
			{
				return (Panel)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("p_confirmationIntroMsg");
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00016438 File Offset: 0x00014638
		private Label lbl_finishMessage
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_finishMessage");
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00016464 File Offset: 0x00014664
		private Label lbl_confirmationIntroMsg
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_confirmationIntroMsg");
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00016490 File Offset: 0x00014690
		private TableRow row_altContact
		{
			get
			{
				return (TableRow)this.step_confirmProfInfo.ContentTemplateContainer.FindControl("row_altContact");
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x000164BC File Offset: 0x000146BC
		private Label lbl_pleaseselectadate
		{
			get
			{
				return (Label)this.step_selectTime.ContentTemplateContainer.FindControl("lbl_pleaseselectadate");
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002FA RID: 762 RVA: 0x000164E8 File Offset: 0x000146E8
		private HtmlInputText txt_date
		{
			get
			{
				return (HtmlInputText)this.step_classdatetime.ContentTemplateContainer.FindControl("txt_date");
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00016504 File Offset: 0x00014704
		private HiddenField cutoffDate
		{
			get
			{
				return (HiddenField)this.step_confirmProfInfo.ContentTemplateContainer.FindControl("cutoffDate");
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002FC RID: 764 RVA: 0x00016530 File Offset: 0x00014730
		private Label lbl_confirmAndCompleteTitle
		{
			get
			{
				return (Label)this.step_confirmAndComplete.ContentTemplateContainer.FindControl("lbl_confirmAndCompleteTitle");
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0001655C File Offset: 0x0001475C
		private Label lbl_selectTime
		{
			get
			{
				return (Label)this.step_selectTime.ContentTemplateContainer.FindControl("lbl_selectTime");
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00016588 File Offset: 0x00014788
		private DropDownList cmb_course
		{
			get
			{
				return (DropDownList)this.step_selectCourse.ContentTemplateContainer.FindControl("cmb_course");
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002FF RID: 767 RVA: 0x000165B4 File Offset: 0x000147B4
		private Panel p_courseInfo
		{
			get
			{
				return (Panel)this.step_selectCourse.ContentTemplateContainer.FindControl("p_courseInfo");
			}
		}

		// Token: 0x06000300 RID: 768 RVA: 0x000165E0 File Offset: 0x000147E0
		private IList<TryToBookAccommodationToUseDTO> GetAdditionalAccommodationsToUse(Panel pData, int pid)
		{
			bool flag = pData == null;
			IList<TryToBookAccommodationToUseDTO> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int settingValue = this.GetSettingValue<int>(Setting.EXAMBOOKING_WizardSetting_AdditionalInformationScreenNum);
				DynamicControlLayoutHelper helper = new DynamicControlLayoutHelper();
				List<ClockWorkWebAPI.TestBooking.Accommodation> accommodationsFromDynamicForm = DynamicScreenLayout.GetAccommodationsFromDynamicForm(pData, settingValue, pid, base.Cache, helper, "", true);
				result = (from g in accommodationsFromDynamicForm
				select new TryToBookAccommodationToUseDTO
				{
					ControlId = g.Controlid,
					Caption = g.Title,
					Value = (g.LookupText ?? "") + (g.SubText ?? "")
				}).ToList<TryToBookAccommodationToUseDTO>();
			}
			return result;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00016653 File Offset: 0x00014853
		private void ShowCourseEMessage(string msg)
		{
			this.lbl_course_emsg2.Text = msg;
			this.p_course_emsg_2.Visible = true;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00016670 File Offset: 0x00014870
		protected void Wizard1_ActiveStepChanged(object sender, EventArgs e)
		{
			bool settingValue = this.GetSettingValue<bool>(Setting.EXAMBOOKING_dontAskStudentToConfirmInstructorInformation);
			bool flag = settingValue && this.Wizard1.ActiveStep == this.step_confirmProfInfo;
			if (flag)
			{
				Wizard wizard = this.Wizard1;
				int activeStepIndex = wizard.ActiveStepIndex;
				wizard.ActiveStepIndex = activeStepIndex + 1;
			}
			DateTime dateTime;
			DateTime value;
			ClockWorkWebAPI.Core.GetTermStartEndDates(out dateTime, out value);
			bool flag2 = dateTime > DateTime.Now.Date;
			if (flag2)
			{
				dateTime = DateTime.Now.Date;
			}
			int pid = this.LookupStudentPid();
			DateTime? dateTime2;
			DateTime? dateTime3;
			int selectedLucid = this.GetSelectedLucid(out dateTime2, out dateTime3);
			int num = (selectedLucid > 0) ? this.GetLastSelectedLucid() : 0;
			string finishButtonText = this.finishButtonText;
			bool flag3 = !string.IsNullOrEmpty(finishButtonText);
			if (flag3)
			{
				Button button = (Button)this.step_confirmAndComplete.CustomNavigationTemplateContainer.FindControl("StepNextButton");
				bool flag4 = button != null;
				if (flag4)
				{
					button.Text = ((this.Wizard1.ActiveStep == this.step_confirmAndComplete) ? finishButtonText : "Next");
				}
			}
			bool flag5 = dateTime3 != null && dateTime3.Value > value;
			if (flag5)
			{
				value = dateTime3.Value;
			}
			bool flag6 = dateTime2 != null && dateTime2.Value < dateTime;
			if (flag6)
			{
				dateTime = dateTime2.Value;
			}
			bool flag7 = selectedLucid > 0 && num != selectedLucid;
			if (flag7)
			{
				user_test_bookexam.eReasonNotAllowedToChooseCourse eReasonNotAllowedToChooseCourse = this.CourseChanged(selectedLucid, dateTime, value, pid);
				bool flag8 = eReasonNotAllowedToChooseCourse == user_test_bookexam.eReasonNotAllowedToChooseCourse.NoTestExamAccommodations;
				if (flag8)
				{
					bool flag9 = this.Wizard1.ActiveStepIndex != 0;
					if (flag9)
					{
						this.Wizard1.ActiveStepIndex = this.Wizard1.WizardSteps.IndexOf(this.step_selectCourse);
					}
					this.ShowCourseEMessage("You don't have any test/exam related accommodations for this course.  Please contact your advisor for assistance with booking this test.");
					this.lastSelectedLucid.Value = "";
					return;
				}
				bool flag10 = eReasonNotAllowedToChooseCourse == user_test_bookexam.eReasonNotAllowedToChooseCourse.LoaNotIssued;
				if (flag10)
				{
					bool flag11 = this.Wizard1.ActiveStepIndex != 0;
					if (flag11)
					{
						this.Wizard1.ActiveStepIndex = this.Wizard1.WizardSteps.IndexOf(this.step_selectCourse);
					}
					this.ShowCourseEMessage("Your accommodations for this course are not yet active.  Please contact your advisor to book this test.");
					this.lastSelectedLucid.Value = "";
					return;
				}
			}
			else
			{
				bool visible = this.p_course_emsg_2.Visible;
				if (visible)
				{
					this.p_course_emsg_2.Visible = false;
				}
			}
			WizardStepBase activeStep = this.Wizard1.ActiveStep;
			string text = (activeStep == null) ? "" : activeStep.Title;
			bool flag12 = !string.IsNullOrEmpty(text);
			if (flag12)
			{
				this.Page.Title = "Schedule a Final Exam - " + text;
			}
			bool flag13 = this.Wizard1.ActiveStep == this.step_welcome;
			if (!flag13)
			{
				bool flag14 = this.Wizard1.ActiveStep == this.step_selectCourse;
				if (flag14)
				{
					ClockWorkWebCore.SetFocus(this.cmb_course);
				}
				else
				{
					bool flag15 = this.Wizard1.ActiveStep == this.step_classdatetime;
					if (flag15)
					{
						bool flag16 = selectedLucid <= 0;
						if (flag16)
						{
							this.Wizard1.ActiveStepIndex = 1;
							return;
						}
						ClockWorkWebCore.SetFocus(this.txt_date);
					}
				}
			}
			bool flag17 = this.Wizard1.ActiveStep == this.step_confirmProfInfo || this.Wizard1.ActiveStep == this.step_chooseAccommodations || this.Wizard1.ActiveStep == this.step_selectTime || this.Wizard1.ActiveStep == this.step_additionalInfo || this.Wizard1.ActiveStep == this.step_confirmAndComplete;
			if (flag17)
			{
				DateTime classDateTime = this.GetClassDateTime();
				int selectedDurationMinutes = this.GetSelectedDurationMinutes();
				bool flag18 = classDateTime == DateTime.MinValue || selectedDurationMinutes < 1;
				if (flag18)
				{
					this.Wizard1.ActiveStepIndex = 2;
					return;
				}
			}
			IList<user_test_bookexam.AccommodationItem> list = null;
			IList<user_test_bookexam.AccommodationItem> list2 = null;
			bool flag19 = this.Wizard1.ActiveStep == this.step_selectTime || this.Wizard1.ActiveStep == this.step_additionalInfo || this.Wizard1.ActiveStep == this.step_confirmAndComplete;
			if (flag19)
			{
				bool flag20 = this.GetCheckedAccommodationItems(out list, out list2);
				int count = list.Count;
				bool flag21 = count < 1;
				if (flag21)
				{
					this.Wizard1.ActiveStepIndex = 4;
					return;
				}
			}
			bool flag22 = this.Wizard1.ActiveStep == this.step_confirmProfInfo;
			if (flag22)
			{
				ClockWorkWebCore.SetFocus(this.txt_instructorName);
			}
			else
			{
				bool flag23 = this.Wizard1.ActiveStep == this.step_chooseAccommodations;
				if (flag23)
				{
					bool flag24 = selectedLucid > 0 && num != selectedLucid;
					if (flag24)
					{
						this.CourseChanged(selectedLucid, dateTime, value, pid);
					}
					ClockWorkWebCore.SetFocus(this.chk_accommodations);
				}
				else
				{
					bool flag25 = this.Wizard1.ActiveStep == this.step_selectTime;
					if (flag25)
					{
						DateTime classDateTime2 = this.GetClassDateTime();
						int selectedDurationMinutes2 = this.GetSelectedDurationMinutes();
						DateTime dateTime4 = classDateTime2.AddMinutes((double)selectedDurationMinutes2);
						bool flag26 = classDateTime2 < DateTime.Now || selectedDurationMinutes2 < 1;
						if (flag26)
						{
							this.Wizard1.ActiveStepIndex = 1;
							return;
						}
						List<TryToBookAccommodationToUseDTO> selectedAccommodations = this.GetSelectedAccommodations();
						string text2 = Path.Combine(base.Request.PhysicalApplicationPath, "bin");
						Panel pdata = this.GetPData();
						bool visible2 = this.lbl_additionalRequirementsValue.Visible;
						if (visible2)
						{
							int settingValue2 = this.GetSettingValue<int>(Setting.EXAMBOOKING_WizardSetting_AdditionalInformationScreenNum);
							DynamicScreenLayout.AddSummaryToLabel(this.lbl_additionalRequirementsValue, pdata, settingValue2, pid, base.Cache, new DynamicControlLayoutHelper(), "", true);
						}
						IList<TryToBookAccommodationToUseDTO> additionalAccommodationsToUse = this.GetAdditionalAccommodationsToUse(pdata, pid);
						user_test_bookexam.PotentialBookingsForStudent potentialBookingsForStudent = this.TryToFindBooking(pid, selectedLucid, classDateTime2, selectedDurationMinutes2, (from g in selectedAccommodations
						select g.ControlId).ToList<int>(), additionalAccommodationsToUse);
						RadioButtonList control = this.GetControl<RadioButtonList>(this.step_selectTime, "rbtn_potentials");
						control.Items.Clear();
						Panel control2 = this.GetControl<Panel>(this.step_selectTime, "p_nodates");
						Label control3 = this.GetControl<Label>(this.step_selectTime, "lbl_dateFound");
						bool flag27 = potentialBookingsForStudent.Bookings == null || potentialBookingsForStudent.Bookings.Count < 1;
						if (flag27)
						{
							control2.Visible = true;
							control3.Visible = false;
							bool flag28 = !string.IsNullOrEmpty(potentialBookingsForStudent.NoBookingsAvailableMessage);
							if (flag28)
							{
								this.GetControl<Label>(this.step_selectTime, "lbl_nodates").Text = potentialBookingsForStudent.NoBookingsAvailableMessage;
							}
							string text3 = string.Join(", ", potentialBookingsForStudent.GeneralNotices.ToArray<string>());
							CWLogger.Logger.Info("EXAMBOOK:NoDatesFound:pid={0}:lucid={1}:classdatetime={2} to {3}:privatenotes={4}", new object[]
							{
								pid.ToString(),
								selectedLucid.ToString(),
								classDateTime2.ToString("yyyy-MM-dd H:mm"),
								dateTime4.ToString("H:mm"),
								text3
							});
						}
						else
						{
							string value2 = (control.SelectedIndex > 0) ? control.SelectedValue : null;
							control2.Visible = false;
							control3.Visible = true;
							int num2 = 0;
							List<DateTime> list3 = new List<DateTime>();
							foreach (user_test_bookexam.PotentialBookingForStudent potentialBookingForStudent in potentialBookingsForStudent.Bookings)
							{
								potentialBookingForStudent.Id = num2++;
								DateTime startDateTime = potentialBookingForStudent.StartDateTime;
								bool flag29 = !list3.Contains(startDateTime);
								if (flag29)
								{
									list3.Add(startDateTime);
									string text4 = string.Concat(new string[]
									{
										potentialBookingForStudent.StartDateTime.ToString("dddd MMMM d"),
										" . ",
										potentialBookingForStudent.StartDateTime.ToString("h:mm tt"),
										" to ",
										potentialBookingForStudent.EndDateTime.ToString("h:mm tt")
									});
									ListItem listItem = new ListItem(text4, potentialBookingForStudent.Id.ToString());
									control.Items.Add(listItem);
									bool flag30 = !string.IsNullOrEmpty(value2) && potentialBookingForStudent.Id.ToString().Equals(value2);
									if (flag30)
									{
										listItem.Selected = true;
									}
								}
							}
							bool flag31 = control.Items.Count == 1;
							if (flag31)
							{
								control.Items[0].Selected = true;
								this.GetControl<Label>(this.step_selectTime, "lbl_potential").Text = control.Items[0].Text;
							}
							bool flag32 = control.Items.Count == 1;
							if (flag32)
							{
								control.Items[0].Selected = true;
							}
						}
					}
					else
					{
						bool flag33 = this.Wizard1.ActiveStep == this.step_confirmAndComplete;
						if (flag33)
						{
							this.p_emsg.Visible = false;
							user_test_bookexam.PotentialBookingForStudent selectedPotentialTest = this.GetSelectedPotentialTest();
							bool flag34 = selectedPotentialTest == null;
							if (flag34)
							{
								this.Wizard1.ActiveStepIndex = this.Wizard1.ActiveStepIndex - 1;
								return;
							}
							StringBuilder stringBuilder = new StringBuilder();
							stringBuilder.Append(selectedPotentialTest.StartDateTime.ToString("ddd MMM d, yyyy h:mm tt"));
							stringBuilder.Append(" (");
							stringBuilder.Append(((int)(selectedPotentialTest.EndDateTime - selectedPotentialTest.StartDateTime).TotalMinutes).GetDurationDescriptionShort());
							stringBuilder.Append(")");
							this.lbl_yourTestDateTimeVal.Text = stringBuilder.ToString();
							DateTime classDateTime3 = this.GetClassDateTime();
							int selectedDurationMinutes3 = this.GetSelectedDurationMinutes();
							DateTime dateTime5 = classDateTime3.AddMinutes((double)selectedDurationMinutes3);
							stringBuilder = new StringBuilder();
							stringBuilder.Append(classDateTime3.ToString("ddd MMM d, yyyy h:mm tt"));
							stringBuilder.Append(" (");
							stringBuilder.Append(selectedDurationMinutes3.GetDurationDescriptionShort());
							stringBuilder.Append(")");
							this.lbl_classDateTimeVal.Text = stringBuilder.ToString();
							this.lbl_courseVal.Text = this.cmb_course.SelectedItem.Text;
							this.lbl_instructorVal.Text = this.txt_instructorName.Text + " . " + this.txt_instructorEmail.Text;
							bool flag20 = !this.GetSettingValue<bool>(Setting.EXAMBOOKING_ShowAccommodationsStudentOptedOutOfInConfirmation);
							bool flag35 = !flag20;
							if (flag35)
							{
								this.GetControl<Label>(this.step_confirmAndComplete, "lbl_accommodations").Text = "You opted out of the following accommodation(s):";
							}
							RadListBox control4 = this.GetControl<RadListBox>(this.step_confirmAndComplete, "lb_accommodations");
							control4.Items.Clear();
							foreach (object obj in this.GetControl<CheckBoxList>(this.step_chooseAccommodations, "chk_accommodations").Items)
							{
								ListItem listItem2 = (ListItem)obj;
								bool flag36 = listItem2.Selected == flag20;
								if (flag36)
								{
									RadListBoxItem item = new RadListBoxItem(listItem2.Text, listItem2.Value);
									control4.Items.Add(item);
								}
							}
							Label control5 = this.GetControl<Label>(this.step_confirmAndComplete, "lbl_noAccommodations");
							bool flag37 = control4.Items.Count < 1;
							if (flag37)
							{
								control4.Visible = false;
								control5.Text = "None";
							}
							else
							{
								bool flag38 = !control4.Visible;
								if (flag38)
								{
									control4.Visible = true;
									control5.Text = "";
								}
							}
							bool visible3 = this.lbl_additionalRequirementsValue.Visible;
							if (visible3)
							{
								int settingValue3 = this.GetSettingValue<int>(Setting.EXAMBOOKING_WizardSetting_AdditionalInformationScreenNum);
								Panel pdata2 = this.GetPData();
								DynamicScreenLayout.AddSummaryToLabel(this.lbl_additionalRequirementsValue, pdata2, settingValue3, pid, base.Cache, new DynamicControlLayoutHelper(), "", true);
							}
						}
					}
				}
			}
			this.SetAppropriateFocus();
		}

		// Token: 0x06000303 RID: 771 RVA: 0x000172D8 File Offset: 0x000154D8
		private void SetAppropriateFocus()
		{
			WizardStepBase activeStep = this.Wizard1.ActiveStep;
			bool flag = activeStep == this.step_welcome;
			if (flag)
			{
				user_test_bookexam.SetFocus2(this.GetControl<Label>(this.step_welcome, "lbl_welcome"));
			}
			else
			{
				bool flag2 = activeStep == this.step_selectCourse;
				if (flag2)
				{
					user_test_bookexam.SetFocus2(this.GetControl<DropDownList>(this.step_selectCourse, "cmb_course"));
				}
				else
				{
					bool flag3 = activeStep == this.step_classdatetime;
					if (flag3)
					{
						user_test_bookexam.SetFocus2ForClassDateTimeStep(this.step_classdatetime);
					}
					else
					{
						bool flag4 = activeStep == this.step_confirmProfInfo;
						if (flag4)
						{
							user_test_bookexam.SetFocus2(this.GetControl<TextBox>(this.step_confirmProfInfo, "txt_instructorName"));
						}
						else
						{
							bool flag5 = this.Wizard1.ActiveStep == this.step_chooseAccommodations;
							if (flag5)
							{
								user_test_bookexam.SetFocus2(this.GetControl<CheckBoxList>(this.step_chooseAccommodations, "chk_accommodations"));
							}
							else
							{
								bool flag6 = this.Wizard1.ActiveStep == this.step_additionalInfo;
								if (flag6)
								{
									Panel panel = this.step_additionalInfo.FindControl("p_data") as Panel;
									Control control = (panel == null || panel.Controls.Count < 1) ? null : panel.Controls[0];
									while (control != null && control is Panel && control.Controls.Count > 0)
									{
										control = control.Controls[0];
									}
									bool flag7 = control != null;
									if (flag7)
									{
										user_test_bookexam.SetFocus2(control);
									}
								}
								else
								{
									bool flag8 = this.Wizard1.ActiveStep == this.step_selectTime;
									if (flag8)
									{
										RadioButtonList control2 = this.GetControl<RadioButtonList>(this.step_selectTime, "rbtn_potentials");
										bool flag9 = control2.Items.Count > 0;
										if (flag9)
										{
											user_test_bookexam.SetFocus2(control2);
										}
										else
										{
											user_test_bookexam.SetFocus2(this.GetControl<Label>(this.step_selectTime, "lbl_pleaseselectadate"));
										}
									}
									else
									{
										bool flag10 = activeStep == this.step_confirmAndComplete;
										if (flag10)
										{
											user_test_bookexam.SetFocus2ForSummaryStep(this.GetControl<CheckBox>(this.step_confirmAndComplete, "chk_iagree"));
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000174F8 File Offset: 0x000156F8
		private static void SetFocus2ForClassDateTimeStep(Control control)
		{
			string activeJavascript = "try { SelectPotentialTimesRadioButtonList2(); } catch ( ex0 ) { } \r\n" + "try { FocusClassDate(); } catch (ex) { } \r\n";
			user_test_bookexam.SetFocus2(control, activeJavascript);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00017520 File Offset: 0x00015720
		private static void SetFocus2ForSummaryStep(Control control)
		{
			string activeJavascript = "try { FocusTextBox('" + control.ClientID + "'); } catch ( ex0 ) { } \r\n" + "try { MakeSummaryAlertPop(); } catch (ex) { } \r\n";
			user_test_bookexam.SetFocus2(control, activeJavascript);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00017554 File Offset: 0x00015754
		private static void SetFocus2(Control control)
		{
			bool flag = control == null;
			if (!flag)
			{
				bool flag2 = control is CheckBoxList;
				string activeJavascript;
				if (flag2)
				{
					activeJavascript = "SelectAccommodationsCheckBoxList();\r\n";
				}
				else
				{
					bool flag3 = control is RadioButtonList;
					if (flag3)
					{
						activeJavascript = (control.ID.Equals("rbtns_existingClassDateTimes") ? "SelectPotentialTimesRadioButtonList2();\r\n" : "SelectPotentialTimesRadioButtonList();\r\n");
					}
					else
					{
						activeJavascript = "FocusTextBox('" + control.ClientID + "');\r\n";
					}
				}
				user_test_bookexam.SetFocus2(control, activeJavascript);
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x000175D0 File Offset: 0x000157D0
		private static void SetFocus2(Control control, string activeJavascript)
		{
			bool flag = control == null;
			if (!flag)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("\r\n<script language='JavaScript'>\r\n");
				stringBuilder.Append("<!--\r\n");
				stringBuilder.Append("function SetFocus()\r\n");
				stringBuilder.Append("{\r\n");
				stringBuilder.Append("try {");
				stringBuilder.Append(activeJavascript);
				stringBuilder.Append("window.location='#MainContent';\r\n");
				stringBuilder.Append("}\r\n");
				stringBuilder.Append("catch ( e ) {\r\n");
				stringBuilder.Append("}\r\n");
				stringBuilder.Append("}\r\n");
				stringBuilder.Append("window.onload = SetFocus;\r\n");
				stringBuilder.Append("// -->\r\n");
				stringBuilder.Append("</script>");
				control.Page.ClientScript.RegisterClientScriptBlock(control.Page.GetType(), "SetFocus", stringBuilder.ToString());
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x000176BC File Offset: 0x000158BC
		private user_test_bookexam.PotentialBookingsForStudent TryToFindBooking(int pid, int lucid, DateTime classStartDateTime, int classDurationInMinutes, IList<int> accommodationCidsToUse, IList<TryToBookAccommodationToUseDTO> AdditionalAccommodationsToUse)
		{
			IAutoTestBookingClientManager autoTestBookingClientManager = new AutoTestBookingClientManager();
			TryToBookResultDTO resp = autoTestBookingClientManager.TryToFindBooking(eTestExamSettingType.Final, false, pid, lucid, classStartDateTime, classDurationInMinutes, accommodationCidsToUse, false, 0, AdditionalAccommodationsToUse, false, null);
			string noBookingsAvailableMessage = null;
			bool flag = resp.PotentialBookings.Count < 1;
			if (flag)
			{
				noBookingsAvailableMessage = (resp.StudentAlreadyHadAnotherTestBookedForSameDayAndCourse ? "You have already scheduled a test or exam with us for this course and day." : this.GetSettingValue<string>(Setting.EXAMBOOKING_NoRoomFoundMessage));
			}
			RadioButtonList control = this.GetControl<RadioButtonList>(this.step_selectTime, "rbtn_potentials");
			string text = (control.SelectedIndex > 0) ? control.SelectedValue : null;
			user_test_bookexam.PotentialBookingsForStudent potentialBookingsForStudent = new user_test_bookexam.PotentialBookingsForStudent
			{
				Bookings = (from g in resp.PotentialBookings
				select new user_test_bookexam.PotentialBookingForStudent
				{
					StartDateTime = g.StartDateTime,
					EndDateTime = g.EndDateTime,
					RoomPersonId = g.Room.PersonId,
					RoomTitle = g.Room.Title,
					AppliedBreakMinutes = resp.AppliedBreakMinutes,
					OkToDoubleBook = (g.Room.RoomType == eRoomType.VirtualRoom || g.Room.RoomType == eRoomType.SuperVirtualRoom)
				}).ToList<user_test_bookexam.PotentialBookingForStudent>(),
				IconIdsToAdd = resp.IconIdsToBookWith,
				EmailAccommodationControlIds = resp.AccommodationCidsForEmail,
				NoBookingsAvailableMessage = noBookingsAvailableMessage,
				GeneralNotices = resp.NoticesForAllPotentialBookings
			};
			this.Session.Add("potentialtestbookings", potentialBookingsForStudent);
			return potentialBookingsForStudent;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x000177DC File Offset: 0x000159DC
		private bool GetCheckedAccommodationItems(out IList<user_test_bookexam.AccommodationItem> checkedAccommodationItems, out IList<user_test_bookexam.AccommodationItem> unCheckedAccommodationItems)
		{
			bool result = !this.GetSettingValue<bool>(Setting.EXAMBOOKING_ShowAccommodationsStudentOptedOutOfInConfirmation);
			checkedAccommodationItems = new List<user_test_bookexam.AccommodationItem>();
			unCheckedAccommodationItems = new List<user_test_bookexam.AccommodationItem>();
			foreach (object obj in this.chk_accommodations.Items)
			{
				ListItem listItem = (ListItem)obj;
				bool selected = listItem.Selected;
				if (selected)
				{
					checkedAccommodationItems.Add(new user_test_bookexam.AccommodationItem
					{
						Name = listItem.Text,
						Value = listItem.Value
					});
				}
				else
				{
					unCheckedAccommodationItems.Add(new user_test_bookexam.AccommodationItem
					{
						Name = listItem.Text,
						Value = listItem.Value
					});
				}
			}
			return result;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000178BC File Offset: 0x00015ABC
		private T GetControl<T>(TemplatedWizardStep wizardStepPanel, string controlName) where T : Control
		{
			return (T)((object)wizardStepPanel.ContentTemplateContainer.FindControl(controlName));
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000178E0 File Offset: 0x00015AE0
		private TryToBookAccommodationToUseDTO GetAccommodationToUseFromListItem(ListItem li)
		{
			string value = li.Value;
			string value2 = "";
			int num = value.IndexOf('`');
			bool flag = num > 0;
			string s;
			if (flag)
			{
				s = value.Substring(0, num);
				bool flag2 = num < value.Length - 1;
				if (flag2)
				{
					value2 = value.Substring(num + 1);
				}
			}
			else
			{
				s = value;
			}
			int controlId;
			int.TryParse(s, out controlId);
			return new TryToBookAccommodationToUseDTO
			{
				ControlId = controlId,
				Caption = li.Text,
				Value = value2
			};
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00017970 File Offset: 0x00015B70
		private List<TryToBookAccommodationToUseDTO> GetSelectedAccommodations()
		{
			CheckBoxList control = this.GetControl<CheckBoxList>(this.step_chooseAccommodations, "chk_accommodations");
			return (from ListItem li in control.Items
			where li.Selected
			select this.GetAccommodationToUseFromListItem(li)).ToList<TryToBookAccommodationToUseDTO>();
		}

		// Token: 0x0600030D RID: 781 RVA: 0x000179DC File Offset: 0x00015BDC
		private user_test_bookexam.PotentialBookingForStudent GetSelectedPotentialTest()
		{
			user_test_bookexam.PotentialBookingsForStudent potentialBookingsForStudent;
			return this.GetSelectedPotentialTest(out potentialBookingsForStudent);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x000179F8 File Offset: 0x00015BF8
		private user_test_bookexam.PotentialBookingForStudent GetSelectedPotentialTest(out user_test_bookexam.PotentialBookingsForStudent potentialBookingsForStudent)
		{
			RadioButtonList control = this.GetControl<RadioButtonList>(this.step_selectTime, "rbtn_potentials");
			bool flag = control.SelectedItem != null;
			if (flag)
			{
				object obj = this.Session["potentialtestbookings"];
				bool flag2 = obj != null && obj is user_test_bookexam.PotentialBookingsForStudent;
				if (flag2)
				{
					potentialBookingsForStudent = (user_test_bookexam.PotentialBookingsForStudent)obj;
					string selectedValue = control.SelectedValue;
					int id;
					bool flag3 = int.TryParse(selectedValue, out id);
					if (flag3)
					{
						return potentialBookingsForStudent.Bookings.FirstOrDefault((user_test_bookexam.PotentialBookingForStudent g) => g.Id == id);
					}
				}
			}
			potentialBookingsForStudent = null;
			return null;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00017AA0 File Offset: 0x00015CA0
		private int GetSelectedDurationInMinutes_DirectlyFromDurationControls()
		{
			HtmlInputGenericControl control = this.GetControl<HtmlInputGenericControl>(this.step_classdatetime, "txt_duration_hours");
			HtmlInputGenericControl control2 = this.GetControl<HtmlInputGenericControl>(this.step_classdatetime, "txt_duration_minutes");
			int num;
			bool flag = !int.TryParse(control.Value.Trim(), out num);
			if (flag)
			{
				num = 0;
			}
			int num2;
			bool flag2 = !int.TryParse(control2.Value.Trim(), out num2);
			if (flag2)
			{
				num2 = 0;
			}
			return Convert.ToInt32(num * 60 + num2);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00017B20 File Offset: 0x00015D20
		private int GetSelectedDurationMinutes()
		{
			bool flag = !this.GetControl<HiddenField>(this.step_classdatetime, "lbl_usingExistingClassDateTime").Value.Equals("1");
			int result;
			if (flag)
			{
				result = this.GetSelectedDurationInMinutes_DirectlyFromDurationControls();
			}
			else
			{
				string selectedValue = this.GetControl<RadioButtonList>(this.step_classdatetime, "rbtns_existingClassDateTimes").SelectedValue;
				bool flag2 = string.IsNullOrEmpty(selectedValue);
				if (flag2)
				{
					result = 0;
				}
				else
				{
					string[] array = selectedValue.Split(new char[]
					{
						','
					});
					DateTime d = DateTime.Parse(array[0]);
					DateTime d2 = DateTime.Parse(array[1]);
					result = Convert.ToInt32((d2 - d).TotalMinutes);
				}
			}
			return result;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00017BC8 File Offset: 0x00015DC8
		private DateTime GetClassDateTime()
		{
			bool flag = this.lbl_usingExistingClassDateTime.Value.Equals("1");
			DateTime result;
			if (flag)
			{
				string selectedValue = this.rbtns_existingClassDateTimes.SelectedValue;
				bool flag2 = !string.IsNullOrEmpty(selectedValue);
				if (flag2)
				{
					string[] array = selectedValue.Split(new char[]
					{
						','
					});
					result = DateTime.Parse(array[0]);
				}
				else
				{
					result = DateTime.MinValue;
				}
			}
			else
			{
				DateTime? selectedDateOfClassTest = this.GetSelectedDateOfClassTest();
				DateTime dateTime = (selectedDateOfClassTest != null) ? selectedDateOfClassTest.Value : DateTime.Today;
				string str = this.GetControl<TextBox>(this.step_classdatetime, "classStartTimePicker").Text.Trim();
				DateTime dateTime2 = DateTime.Parse(dateTime.ToString("yyyy-MM-dd") + " " + str);
				result = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime2.Hour, dateTime2.Minute, 0);
			}
			return result;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00017CC8 File Offset: 0x00015EC8
		private DateTime? GetSelectedDateOfClassTest()
		{
			HtmlInputText control = this.GetControl<HtmlInputText>(this.step_classdatetime, "txt_date");
			string text = control.Value.Trim();
			DateTime value;
			return (text.Length > 0 && DateTime.TryParse(text, out value)) ? new DateTime?(value) : null;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00017D1E File Offset: 0x00015F1E
		private Panel p_course_emsg
		{
			get
			{
				return (Panel)this.step_selectCourse.ContentTemplateContainer.FindControl("p_course_emsg");
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000314 RID: 788 RVA: 0x00017D3A File Offset: 0x00015F3A
		private Panel p_course_emsg_2
		{
			get
			{
				return (Panel)this.step_selectCourse.ContentTemplateContainer.FindControl("p_course_emsg_2");
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00017D56 File Offset: 0x00015F56
		private Label lbl_course_emsg2
		{
			get
			{
				return (Label)this.step_selectCourse.ContentTemplateContainer.FindControl("lbl_course_emsg2");
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00017D74 File Offset: 0x00015F74
		private static List<int> IntListFromString(string commaSeparatedNumbers)
		{
			List<int> list = new List<int>();
			bool flag = commaSeparatedNumbers == null;
			List<int> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				string[] array = commaSeparatedNumbers.Split(new char[]
				{
					','
				});
				foreach (string text in array)
				{
					string text2 = text.Trim();
					bool flag2 = !string.IsNullOrEmpty(text2);
					if (flag2)
					{
						int item;
						bool flag3 = int.TryParse(text2, out item);
						if (flag3)
						{
							list.Add(item);
						}
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00017E00 File Offset: 0x00016000
		private user_test_bookexam.eReasonNotAllowedToChooseCourse CourseChanged(int newLucid, DateTime sdate, DateTime edate, int pid)
		{
			this.lastSelectedLucid.Value = newLucid.ToString();
			DataTable dataTable = ClockWorkController.Course.LoadStudentsCourse(pid, newLucid, sdate, edate);
			bool flag = dataTable.Rows.Count > 0;
			if (flag)
			{
				DataRow dataRow = dataTable.Rows[0];
				this.txt_instructorName.Text = dataRow["instructor"].ToString();
				this.txt_instructorEmail.Text = dataRow["instructoremail"].ToString();
				bool visible = this.txt_instructorPhone.Visible;
				if (visible)
				{
					this.txt_instructorPhone.Text = dataRow["instructorphone"].ToString();
				}
				this.lbl_courseDescription.Text = ClockWorkWebAPI.Course.CourseToString(dataRow);
			}
			else
			{
				this.txt_instructorEmail.Text = "";
				this.txt_instructorName.Text = "";
				this.lbl_courseDescription.Text = "unknown";
			}
			ClockWorkWebAPI.AccommodationCollection accommodationCollection = ClockWorkController.Accommodation.LoadAccommodations(pid, newLucid, "");
			accommodationCollection.SortListByCaptionWithValue();
			string settingValue = this.GetSettingValue<string>(Setting.EXAMBOOKING_NonNegotiableAccommodationCids);
			List<int> list = user_test_bookexam.IntListFromString(settingValue);
			bool settingValue2 = this.GetSettingValue<bool>(Setting.EXAMBOOKING_WizardSetting_AccommodationsDefaultChecked);
			this.chk_accommodations.Items.Clear();
			foreach (object obj in accommodationCollection)
			{
				ClockWorkWebAPI.Accommodation accommodation = (ClockWorkWebAPI.Accommodation)obj;
				string value = HttpUtility.HtmlEncode(accommodation.ControlId.ToString() + "`" + accommodation.ControlCaption);
				string text = HttpUtility.HtmlEncode(accommodation.CaptionWithValue);
				ListItem listItem = new ListItem(text, value);
				this.chk_accommodations.Items.Add(listItem);
				bool flag2 = settingValue2;
				if (flag2)
				{
					listItem.Selected = true;
				}
				bool flag3 = list.Contains(accommodation.ControlId);
				if (flag3)
				{
					listItem.Selected = true;
					listItem.Enabled = false;
				}
			}
			bool flag4 = this.chk_accommodations.Items.Count < 1;
			user_test_bookexam.eReasonNotAllowedToChooseCourse result;
			if (flag4)
			{
				this.chk_accommodations.Items.Add("");
				this.chk_accommodations.Enabled = false;
				result = user_test_bookexam.eReasonNotAllowedToChooseCourse.NoTestExamAccommodations;
			}
			else
			{
				bool settingValue3 = this.GetSettingValue<bool>(Setting.EXAMBOOKING_StudentAllowedToSelectOwnDateTime);
				bool settingValue4 = this.GetSettingValue<bool>(Setting.EXAMBOOKING_StudentAllowedToSelectPreviousDateTimes);
				bool settingValue5 = this.GetSettingValue<bool>(Setting.EXAMBOOKING_StudentAllowedToSelectPreviousClassTestDefinitions);
				bool settingValue6 = this.GetSettingValue<bool>(Setting.EXAMBOOKING_StudentAllowedToSelectPreviousClassTestDefinitionsFromRegistrar);
				bool flag5 = settingValue4 || settingValue5 || settingValue6;
				bool flag6 = flag5;
				if (flag6)
				{
					bool flag7 = settingValue4;
					DataTable dataTable2;
					if (flag7)
					{
						dataTable2 = ClockWorkController.Appointment.LoadPreviouslySubmittedTests(newLucid, 0);
					}
					else
					{
						bool flag8 = settingValue5;
						if (flag8)
						{
							dataTable2 = ClockWorkController.Appointment.LoadPreviouslySubmittedClassTestDefinitions(newLucid, 0, true);
						}
						else
						{
							bool flag9 = settingValue6;
							if (flag9)
							{
								dataTable2 = ClockWorkController.Appointment.LoadPreviouslySubmittedClassTestDefinitions(newLucid, 0, true);
							}
							else
							{
								dataTable2 = new DataTable();
							}
						}
					}
					IAutoTestBookingWebClientManager autoTestBookingWebClientManager = new AutoTestBookingWebClientManager();
					MinMaxDateRangeValue minMaxDateRangeValue = autoTestBookingWebClientManager.FigureOutMinMaxDateRangeStudentIsAllowedToBookForExam(pid);
					bool flag10 = minMaxDateRangeValue.Status != eMinMaxDateRangeInvalidReason.IsValid;
					if (flag10)
					{
						dataTable2.Rows.Clear();
					}
					DateTime date = minMaxDateRangeValue.DateRange.Start.Date;
					DateTime date2 = minMaxDateRangeValue.DateRange.End.Date;
					this.rbtns_existingClassDateTimes.Items.Clear();
					foreach (object obj2 in dataTable2.Rows)
					{
						DataRow dataRow2 = (DataRow)obj2;
						DateTime t = (DateTime)dataRow2["startdate"];
						DateTime dateTime = (DateTime)dataRow2["enddate"];
						bool flag11 = t < date;
						if (!flag11)
						{
							bool flag12 = date2 != DateTime.MinValue && t.Date > date2;
							if (!flag12)
							{
								string text2 = t.ToString("dddd MMMM d . h:mm tt") + " to " + dateTime.ToString("h:mm tt");
								string value2 = t.ToString("yyyy-MM-dd HH:mm") + "," + dateTime.ToString("yyyy-MM-dd HH:mm");
								ListItem item = new ListItem(text2, value2);
								this.rbtns_existingClassDateTimes.Items.Add(item);
							}
						}
					}
					bool flag13 = this.rbtns_existingClassDateTimes.Items.Count > 0;
					bool flag14 = this.rbtns_existingClassDateTimes.Items.Count == 1;
					if (flag14)
					{
						this.rbtns_existingClassDateTimes.Items[0].Selected = true;
					}
					this.lbl_noExistingClassDateTimes.Visible = !flag13;
					this.rbtns_existingClassDateTimes.Visible = flag13;
					this.lbl_usingExistingClassDateTime.Value = (flag13 ? "1" : "0");
				}
				bool flag15 = this.p_classDateandTime.Visible && this.p_existingExams.Visible;
				if (flag15)
				{
					this.p_classDateandTime.Visible = false;
				}
				result = user_test_bookexam.eReasonNotAllowedToChooseCourse.None;
			}
			return result;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00018340 File Offset: 0x00016540
		public string txt_classTestStartTimeClientID()
		{
			return this.txt_classTestStartTime.ClientID;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00018360 File Offset: 0x00016560
		public string txt_dateClientID()
		{
			return this.txt_date.ClientID;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00018380 File Offset: 0x00016580
		public string radslideclientid()
		{
			return this.RadSlider1.ClientID;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x000183A0 File Offset: 0x000165A0
		public string lbl_durationClientID()
		{
			return this.lbl_duration.ClientID;
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600031C RID: 796 RVA: 0x000183C0 File Offset: 0x000165C0
		public RadSlider RadSlider1
		{
			get
			{
				return (RadSlider)this.step_classdatetime.ContentTemplateContainer.FindControl("RadSlider1");
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600031D RID: 797 RVA: 0x000183EC File Offset: 0x000165EC
		public Label lbl_duration
		{
			get
			{
				return (Label)this.step_classdatetime.ContentTemplateContainer.FindControl("lbl_duration");
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00018418 File Offset: 0x00016618
		public string accommodationsclientid()
		{
			return this.chk_accommodations.ClientID;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00018438 File Offset: 0x00016638
		private int GetMaxDuration()
		{
			int settingValue = this.GetSettingValue<int>(Setting.EXAMBOOKING_MaxDuration);
			bool flag = settingValue > 0;
			int result;
			if (flag)
			{
				bool settingValue2 = this.GetSettingValue<bool>(Setting.EXAMBOOKING_MaxDurationUseTimetable);
				bool flag2 = settingValue2;
				if (flag2)
				{
					int lastSelectedLucid = this.GetLastSelectedLucid();
					result = settingValue;
				}
				else
				{
					result = settingValue;
				}
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00018488 File Offset: 0x00016688
		protected void ServerValidationDuration(object source, ServerValidateEventArgs e)
		{
			int num;
			bool flag = int.TryParse(e.Value, out num) && num > 0;
			if (flag)
			{
				int maxDuration = this.GetMaxDuration();
				bool flag2 = maxDuration <= 0 || num <= maxDuration;
				if (flag2)
				{
					e.IsValid = true;
				}
				else
				{
					e.IsValid = false;
				}
			}
			else
			{
				e.IsValid = false;
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
		{
		}

		// Token: 0x06000322 RID: 802 RVA: 0x000184E8 File Offset: 0x000166E8
		private int GetLastSelectedLucid()
		{
			string value = this.lastSelectedLucid.Value;
			bool flag = value.Length > 0;
			if (flag)
			{
				try
				{
					return int.Parse(value);
				}
				catch
				{
					return 0;
				}
			}
			return 0;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00018538 File Offset: 0x00016738
		private int GetSelectedLucid()
		{
			DateTime? dateTime;
			DateTime? dateTime2;
			return this.GetSelectedLucid(out dateTime, out dateTime2);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00018554 File Offset: 0x00016754
		private int GetSelectedLucid(out DateTime? sd, out DateTime? ed)
		{
			bool flag = this.cmb_course.SelectedItem != null;
			if (flag)
			{
				string value = this.cmb_course.SelectedItem.Value;
				bool flag2 = value.Length > 0;
				if (flag2)
				{
					int num = value.IndexOf(",");
					string s = (num > 0) ? value.Substring(0, num) : value;
					int num2;
					bool flag3 = !int.TryParse(s, out num2);
					if (flag3)
					{
						num2 = 0;
					}
					bool flag4 = num2 > 0;
					if (flag4)
					{
						bool flag5 = num > 0;
						if (flag5)
						{
							string text = value.Substring(num + 1);
							int num3 = text.IndexOf(",");
							bool flag6 = num3 > 0;
							if (flag6)
							{
								string s2 = text.Substring(0, num3);
								string s3 = text.Substring(num3 + 1);
								DateTime value2;
								DateTime value3;
								bool flag7 = DateTime.TryParse(s2, out value2) && DateTime.TryParse(s3, out value3);
								if (flag7)
								{
									sd = new DateTime?(value2);
									ed = new DateTime?(value3);
									return num2;
								}
							}
						}
						sd = null;
						ed = null;
						return num2;
					}
				}
			}
			sd = null;
			ed = null;
			return 0;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0001868F File Offset: 0x0001688F
		protected void btn_cancel_click(object sender, EventArgs e)
		{
			base.Response.Redirect(this.GetSettingValue<string>(Setting.TESTBOOKING_TestBookingCancelUrl), true);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x000186AC File Offset: 0x000168AC
		private Panel GetPData()
		{
			for (int i = 0; i < this.Wizard1.WizardSteps.Count; i++)
			{
				bool flag = this.Wizard1.WizardSteps[i] is WizardStep;
				if (flag)
				{
					WizardStep wizardStep = (WizardStep)this.Wizard1.WizardSteps[i];
					bool flag2 = wizardStep.ID.Equals("step_additionalRequirements");
					if (flag2)
					{
						foreach (object obj in wizardStep.Controls)
						{
							Control control = (Control)obj;
							bool flag3 = control is Panel;
							if (flag3)
							{
								return (Panel)control;
							}
						}
					}
				}
				else
				{
					bool flag4 = this.Wizard1.WizardSteps[i] is TemplatedWizardStep;
					if (flag4)
					{
						TemplatedWizardStep templatedWizardStep = (TemplatedWizardStep)this.Wizard1.WizardSteps[i];
						bool flag5 = templatedWizardStep.ID.Equals("step_additionalRequirements");
						if (flag5)
						{
							foreach (object obj2 in templatedWizardStep.Controls)
							{
								Control control2 = (Control)obj2;
								bool flag6 = control2 is Panel;
								if (flag6)
								{
									return (Panel)control2;
								}
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0001886C File Offset: 0x00016A6C
		private string GetAccommodationsString(List<TryToBookAccommodationToUseDTO> accommodations)
		{
			IEnumerable<string> source = from acc in accommodations
			select "• " + acc.Caption;
			return string.Join("\r\n", source.ToArray<string>());
		}

		// Token: 0x06000328 RID: 808 RVA: 0x000188B4 File Offset: 0x00016AB4
		protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			int num = this.LookupStudentPid();
			user_test_bookexam.PotentialBookingsForStudent potentialBookingsForStudent;
			user_test_bookexam.PotentialBookingForStudent selectedPotentialTest = this.GetSelectedPotentialTest(out potentialBookingsForStudent);
			bool flag = selectedPotentialTest == null;
			if (flag)
			{
				this.Wizard1.ActiveStepIndex = this.Wizard1.ActiveStepIndex - 1;
			}
			else
			{
				DateTime? dateTime;
				DateTime? dateTime2;
				int selectedLucid = this.GetSelectedLucid(out dateTime, out dateTime2);
				List<TryToBookAccommodationToUseDTO> selectedAccommodations = this.GetSelectedAccommodations();
				Panel pdata = this.GetPData();
				DateTime classDateTime = this.GetClassDateTime();
				int selectedDurationMinutes = this.GetSelectedDurationMinutes();
				DateTime dateTime3 = classDateTime.AddMinutes((double)selectedDurationMinutes);
				bool settingValue = this.GetSettingValue<bool>(Setting.EXAMBOOKING_BookTestsAsTentative);
				int settingValue2 = this.GetSettingValue<int>(Setting.EXAMBOOKING_AppointmentTypeToUseForBooking);
				int settingValue3 = this.GetSettingValue<int>(Setting.EXAMBOOKING_WizardSetting_AdditionalInformationScreenNum);
				bool flag2 = selectedPotentialTest != null && selectedLucid > 0 && num > 0;
				ClockWorkWebAPI.Appointment.eCreateAppointmentFailedReason eCreateAppointmentFailedReason;
				if (flag2)
				{
					bool makeSureRoomIsntAlreadyBooked = !selectedPotentialTest.OkToDoubleBook;
					object obj = this.Session["tb_privatenotes"];
					List<PrivateNoteDTO> source = (obj == null) ? new List<PrivateNoteDTO>() : ((List<PrivateNoteDTO>)obj);
					FindPotentialBookingsInfo findPotentialBookingsInfo = new FindPotentialBookingsInfo();
					findPotentialBookingsInfo.RestrictByCampus = this.GetSettingValue<bool>(Setting.EXAMBOOKING_RestrictCoursesToCampus);
					findPotentialBookingsInfo.IgnoreStudentsSchedule = this.GetSettingValue<bool>(Setting.EXAMBOOKING_IgnoreStudentSchedule);
					findPotentialBookingsInfo.IgnoreTwoTestsSameCourseSameDay = this.GetSettingValue<bool>(Setting.EXAMBOOKING_IgnoreStudentTwoTestsSameCourseSameDay);
					List<PrivateNote> privateNotes = (from g in source
					select new PrivateNote(g.Note)).ToList<PrivateNote>();
					List<ClockWorkWebAPI.TestBooking.Accommodation> accommodationsToUse = (from atu in selectedAccommodations
					select new ClockWorkWebAPI.TestBooking.Accommodation(atu.ControlId, atu.Caption, "", atu.Value, 0)).ToList<ClockWorkWebAPI.TestBooking.Accommodation>();
					Exception ex;
					int num2 = ClockWorkController.Appointment.CreateExam(num, selectedPotentialTest.RoomPersonId, makeSureRoomIsntAlreadyBooked, selectedPotentialTest.StartDateTime, selectedPotentialTest.EndDateTime, classDateTime, dateTime3, settingValue2, settingValue, selectedLucid, accommodationsToUse, out eCreateAppointmentFailedReason, out ex, selectedPotentialTest.AppliedBreakMinutes, privateNotes, findPotentialBookingsInfo);
					bool flag3 = num2 > 0;
					if (flag3)
					{
						bool flag4 = pdata != null;
						if (flag4)
						{
							DynamicScreenLayout.SaveDynamicData(ScreenType.ScreenType_PerAppointment, num, num2, settingValue3, base.Cache, pdata, "");
						}
						ClockWorkWebAPI.Person studentInfo = ClockWorkWebAPI.Person.GetStudentInfo(num, this.Page);
						string value = (this.cmb_course.SelectedItem == null) ? "" : this.cmb_course.SelectedItem.Text;
						ClockWorkController.Instructor instructor = new ClockWorkController.Instructor(selectedLucid);
						string text = this.txt_instructorName.Text.Trim();
						string text2 = this.txt_instructorEmail.Text.Trim();
						string text3 = this.txt_instructorPhone.Visible ? this.txt_instructorPhone.Text.Trim() : "";
						StringDictionary stringDictionary = new StringDictionary();
						stringDictionary.Add("classstartdate", classDateTime.ToString("ddd MMMM d, yyyy"));
						stringDictionary.Add("classenddate", dateTime3.ToString("ddd MMMM d, yyyy"));
						stringDictionary.Add("classstarttime", classDateTime.ToString("h:mm tt"));
						stringDictionary.Add("classendtime", dateTime3.ToString("h:mm tt"));
						stringDictionary.Add("classduration", ClockWorkWebAPI.Core.MinutesToTimeDescription(Convert.ToInt32((dateTime3 - classDateTime).TotalMinutes)));
						stringDictionary.Add("startdate", selectedPotentialTest.StartDateTime.ToString("ddd MMMM d, yyyy"));
						stringDictionary.Add("starttime", selectedPotentialTest.StartDateTime.ToString("h:mm tt"));
						stringDictionary.Add("endtime", selectedPotentialTest.EndDateTime.ToString("h:mm tt"));
						stringDictionary.Add("enddate", selectedPotentialTest.EndDateTime.ToString("ddd MMMM d, yyyy"));
						string durationDescription = ((int)(selectedPotentialTest.EndDateTime - selectedPotentialTest.StartDateTime).TotalMinutes).GetDurationDescription();
						stringDictionary.Add("duration", durationDescription);
						stringDictionary.Add("room", selectedPotentialTest.RoomTitle);
						stringDictionary.Add("email", studentInfo.Email);
						stringDictionary.Add("firstname", studentInfo.FirstName);
						stringDictionary.Add("lastname", studentInfo.LastName);
						stringDictionary.Add("student_no", studentInfo.StudentNumber);
						stringDictionary.Add("name", studentInfo.Name);
						stringDictionary.Add("accommodations", this.GetAccommodationsString(selectedAccommodations));
						stringDictionary.Add("course", value);
						stringDictionary.Add("personid", num.ToString());
						stringDictionary.Add("appointmentid", num2.ToString());
						stringDictionary.Add("startdatetime", selectedPotentialTest.StartDateTime.ToString("MMMM d, yyyy . h:mm tt"));
						stringDictionary.Add("instructorname", instructor.InstructorName);
						stringDictionary.Add("instructoremail", instructor.InstructorEmail);
						stringDictionary.Add("instructorphone", instructor.InstructorPhone);
						stringDictionary.Add("newinstructorname", text);
						stringDictionary.Add("newinstructoremail", text2);
						stringDictionary.Add("newinstructorphone", text3);
						stringDictionary.Add("appointment", string.Format("{0} {1} to {2} ({3})", new object[]
						{
							selectedPotentialTest.StartDateTime.ToString("dddd MMMM d, yyyy"),
							selectedPotentialTest.StartDateTime.ToString("h:mm tt"),
							selectedPotentialTest.EndDateTime.ToString("h:mm tt"),
							durationDescription
						}));
						IMailMergeCodes mailMergeCodes = new MailMergeCodes();
						stringDictionary.Add("from", mailMergeCodes.GetDefaultFromAddress(eWebModule.TestsExams));
						stringDictionary.Add("signature", mailMergeCodes.GetDefaultSignature(eWebModule.TestsExams));
						bool flag5 = settingValue3 > 0;
						if (flag5)
						{
							stringDictionary.Add("additionalinfo", DynamicScreenLayout.GetSummaryPlainText(pdata, settingValue3, num, base.Cache, new DynamicControlLayoutHelper(), "", true));
						}
						string adminEmail = ClockWorkController.Email.GetAdminEmail(Setting.EXAMBOOKING_TestBookingCoordinatorEmail);
						stringDictionary.Add("adminemail", adminEmail);
						stringDictionary.Add("coordinatoremail", adminEmail);
						IEmailClientManager emailClientManager = new EmailClientManager();
						MailMergeContextDTO mailMergeContext = new MailMergeContextDTO
						{
							PersonId = num,
							LuCourseId = selectedLucid,
							AppointmentId = num2
						};
						string settingValue4 = this.GetSettingValue<string>(Setting.EXAMBOOKING_Email_StudentBookingConfirmation_TemplateRules);
						IDictionary<string, int> campusesWithStudentEmailTemplateIdsFromXml = (settingValue4 ?? "").Trim().GetCampusesWithStudentEmailTemplateIdsFromXml();
						int num3 = 0;
						bool flag6 = campusesWithStudentEmailTemplateIdsFromXml != null && campusesWithStudentEmailTemplateIdsFromXml.Count > 0;
						if (flag6)
						{
							ILookupCourseClientManager lookupCourseClientManager = new LookupCourseClientManager();
							LookupCourseDTO lookupCourseDTO = lookupCourseClientManager.LoadCourseByLuCourseId(selectedLucid);
							string text4 = (((lookupCourseDTO != null) ? lookupCourseDTO.Campus : null) ?? "").Trim().ToLower();
							bool flag7 = text4.Length > 0 && campusesWithStudentEmailTemplateIdsFromXml.ContainsKey(text4);
							if (flag7)
							{
								num3 = campusesWithStudentEmailTemplateIdsFromXml[text4];
							}
						}
						bool flag8 = num3 < 1;
						if (flag8)
						{
							emailClientManager.SendEmail(Setting.EXAMBOOKING_Email_StudentBookingConfirmation, mailMergeContext, stringDictionary.InsertBaseUserMailMergeValues(), "ExamBooking");
						}
						else
						{
							emailClientManager.SendEmail(num3, mailMergeContext, stringDictionary.InsertBaseUserMailMergeValues(), "ExamBooking");
						}
						string value2 = adminEmail;
						SendEmailsResp sendEmailsResp = emailClientManager.SendEmail(Setting.EXAMBOOKING_Email_StudentBookingConfirmationForInstructor, mailMergeContext, stringDictionary, "ExamBooking");
						bool flag9 = sendEmailsResp != null && sendEmailsResp.SendEmailResult != null && sendEmailsResp.SendEmailResult.Status == eTPMailResultStatusDTO.NotSentBecauseTemplateIsDisabled && string.IsNullOrEmpty(instructor.InstructorEmail);
						if (flag9)
						{
							bool flag10 = !string.IsNullOrEmpty(value2);
							if (flag10)
							{
							}
						}
						bool settingValue5 = this.GetSettingValue<bool>(Setting.EXAMBOOKING_AskStudentForInstructorPhone);
						bool flag11 = !text.ToLower().Equals(instructor.InstructorName.ToLower()) || !text2.ToLower().Equals(instructor.InstructorEmail.ToLower()) || (settingValue5 && !(text3 ?? "").ToLower().Equals((instructor.InstructorPhone ?? "").Trim().ToLower()));
						if (flag11)
						{
							emailClientManager.SendEmail(Setting.EXAMBOOKING_StudentChangeProfInfoEmailTemplate, mailMergeContext, stringDictionary.InsertBaseUserMailMergeValues(), "ExamBooking");
						}
						bool flag12 = potentialBookingsForStudent != null && potentialBookingsForStudent.EmailAccommodationControlIds != null && potentialBookingsForStudent.EmailAccommodationControlIds.Count > 0;
						if (flag12)
						{
							stringDictionary.Add("list", this.GetAccommodationsString((from g in selectedAccommodations
							where potentialBookingsForStudent.EmailAccommodationControlIds.Contains(g.ControlId)
							select g).ToList<TryToBookAccommodationToUseDTO>()));
							emailClientManager.SendEmail(Setting.EXAMBOOKING_SpecialAccommodationsEmailTemplate, mailMergeContext, stringDictionary.InsertBaseUserMailMergeValues(), "ExamBooking");
						}
						this.rbtns_existingClassDateTimes.SelectedIndex = -1;
						this.rbtn_potentials.SelectedIndex = -1;
						this.cmb_course.SelectedIndex = -1;
						string key = "studentapps" + num.ToString();
						bool flag13 = base.Cache[key] != null;
						if (flag13)
						{
							base.Cache.Remove(key);
						}
						base.Response.Redirect("ThankyouExam.aspx");
						return;
					}
				}
				else
				{
					eCreateAppointmentFailedReason = ClockWorkWebAPI.Appointment.eCreateAppointmentFailedReason.MissingInformation;
				}
				string str = string.Format("If the problem persists please contact us to book your test{0}.", Misc.GetContactInformationHtml(Setting.EXAMBOOKING_DepartmentContactInformation));
				switch (eCreateAppointmentFailedReason)
				{
				case ClockWorkWebAPI.Appointment.eCreateAppointmentFailedReason.RoomDoubleBooked:
					this.ShowEMessage("Unfortunately the location that was selected for you to write your test was scheduled by another student just before your attempt to complete your booking.  Please use the 'Previous' button below to go back and try to find another potential spot for your test.");
					CWLogger.Logger.Warn("TESTBOOK:Wizard:BookingFailed:pid={0}:lucid={1}:Room double booked", num.ToString(), selectedLucid.ToString());
					break;
				case ClockWorkWebAPI.Appointment.eCreateAppointmentFailedReason.MissingInformation:
					this.ShowEMessage("There was a problem - some information seems to be missing.  Please try using the 'Previous' button to verify all required information has been entered and try again.  " + str);
					CWLogger.Logger.Warn("TESTBOOK:Wizard:BookingFailed:pid={0}:lucid={1}:There was a problem - some information seems to be missing. ({2})", num.ToString(), selectedLucid.ToString(), eCreateAppointmentFailedReason.ToString());
					break;
				case ClockWorkWebAPI.Appointment.eCreateAppointmentFailedReason.StudentDoubleBooked:
					this.ShowEMessage("You already have another appointment or test scheduled at the same time of this test. Please contact us in order to book your test.");
					CWLogger.Logger.Warn("TESTBOOK:Wizard:BookingFailed:pid={0}:lucid={1}:StudentDoubleBooked", num.ToString(), selectedLucid.ToString());
					break;
				case ClockWorkWebAPI.Appointment.eCreateAppointmentFailedReason.StudentAlreadyBookedSameCourseSameDay:
					this.ShowEMessage("You have already scheduled a test for this course on the same day.");
					CWLogger.Logger.Warn("TESTBOOK:Wizard:BookingFailed:pid={0}:lucid={1}:StudentAlreadyBookedSameCourseSameDay", num.ToString(), selectedLucid.ToString());
					break;
				default:
					this.ShowEMessage("There was an unknown error. You can try scheduling this test again by clicking the 'Submit' button at the bottom.  " + str);
					CWLogger.Logger.Warn("TESTBOOK:Wizard:BookingFailed:pid={0}:lucid={1}:There was an unknown error ({2})", num.ToString(), selectedLucid.ToString(), eCreateAppointmentFailedReason.ToString());
					break;
				}
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00019300 File Offset: 0x00017500
		private void ShowEMessage(string emsg)
		{
			this.p_emsg.Visible = true;
			this.lbl_emsg.Text = emsg;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00019320 File Offset: 0x00017520
		protected void ValidateDateS(object source, ServerValidateEventArgs e)
		{
			DateTime? selectedDateOfClassTest = this.GetSelectedDateOfClassTest();
			bool flag = selectedDateOfClassTest == null;
			if (flag)
			{
				e.IsValid = false;
			}
			else
			{
				IAutoTestBookingWebClientManager autoTestBookingWebClientManager = new AutoTestBookingWebClientManager();
				int num = this.LookupStudentPid();
				bool flag2 = num < 1;
				if (flag2)
				{
					e.IsValid = false;
				}
				else
				{
					MinMaxDateRangeValue minMaxDateRangeValue = autoTestBookingWebClientManager.FigureOutMinMaxDateRangeStudentIsAllowedToBookForExam(num);
					bool flag3 = minMaxDateRangeValue.Status != eMinMaxDateRangeInvalidReason.IsValid;
					if (flag3)
					{
						e.IsValid = false;
					}
					else
					{
						DateTime date = selectedDateOfClassTest.Value.Date;
						e.IsValid = (date >= minMaxDateRangeValue.DateRange.Start.Date && date <= minMaxDateRangeValue.DateRange.End.Date);
					}
				}
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x000193F8 File Offset: 0x000175F8
		protected void ValidateTimeS(object source, ServerValidateEventArgs e)
		{
			TextBox control = this.GetControl<TextBox>(this.step_classdatetime, "classStartTimePicker");
			string text = control.Text.Trim();
			DateTime dateTime;
			e.IsValid = (text.Length > 0 && DateTime.TryParse(DateTime.Now.ToString("yyyy-MM-dd") + " " + text, out dateTime));
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0001945C File Offset: 0x0001765C
		protected void ServerValidationDuration2(object source, ServerValidateEventArgs e)
		{
			int selectedDurationInMinutes_DirectlyFromDurationControls = this.GetSelectedDurationInMinutes_DirectlyFromDurationControls();
			int maxDuration = this.GetMaxDuration();
			e.IsValid = (maxDuration <= 0 || selectedDurationInMinutes_DirectlyFromDurationControls <= maxDuration);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0001948D File Offset: 0x0001768D
		protected void btn_chooseAnotherDate_Click(object sender, EventArgs e)
		{
			this.p_classDateandTime.Visible = true;
			this.p_existingExams.Visible = false;
			this.lbl_usingExistingClassDateTime.Value = "0";
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000194BB File Offset: 0x000176BB
		protected void btn_chooseExistingClassDateTime_Click(object sender, EventArgs e)
		{
			this.p_classDateandTime.Visible = false;
			this.p_existingExams.Visible = true;
			this.lbl_usingExistingClassDateTime.Value = "1";
		}

		// Token: 0x040001ED RID: 493
		private WizardStep step_additionalInfo = null;

		// Token: 0x040001EE RID: 494
		protected ScriptManager bbb;

		// Token: 0x040001EF RID: 495
		protected ValidationSummary ValidationSummary4;

		// Token: 0x040001F0 RID: 496
		protected Wizard Wizard1;

		// Token: 0x040001F1 RID: 497
		protected TemplatedWizardStep step_welcome;

		// Token: 0x040001F2 RID: 498
		protected TemplatedWizardStep step_selectCourse;

		// Token: 0x040001F3 RID: 499
		protected TemplatedWizardStep step_classdatetime;

		// Token: 0x040001F4 RID: 500
		protected TemplatedWizardStep step_confirmProfInfo;

		// Token: 0x040001F5 RID: 501
		protected TemplatedWizardStep step_chooseAccommodations;

		// Token: 0x040001F6 RID: 502
		protected TemplatedWizardStep step_selectTime;

		// Token: 0x040001F7 RID: 503
		protected TemplatedWizardStep step_confirmAndComplete;

		// Token: 0x040001F8 RID: 504
		protected HiddenField hidden_bookingemailbody;

		// Token: 0x020001CF RID: 463
		internal enum eReasonNotAllowedToChooseCourse
		{
			// Token: 0x04000997 RID: 2455
			None,
			// Token: 0x04000998 RID: 2456
			LoaNotIssued,
			// Token: 0x04000999 RID: 2457
			NoTestExamAccommodations
		}

		// Token: 0x020001D0 RID: 464
		[Serializable]
		internal class PotentialBookingsForStudent
		{
			// Token: 0x170002D6 RID: 726
			// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x0004E2A8 File Offset: 0x0004C4A8
			// (set) Token: 0x06000CC7 RID: 3271 RVA: 0x0004E2B0 File Offset: 0x0004C4B0
			public IList<user_test_bookexam.PotentialBookingForStudent> Bookings { get; set; }

			// Token: 0x170002D7 RID: 727
			// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x0004E2B9 File Offset: 0x0004C4B9
			// (set) Token: 0x06000CC9 RID: 3273 RVA: 0x0004E2C1 File Offset: 0x0004C4C1
			public string NoBookingsAvailableMessage { get; set; }

			// Token: 0x170002D8 RID: 728
			// (get) Token: 0x06000CCA RID: 3274 RVA: 0x0004E2CA File Offset: 0x0004C4CA
			// (set) Token: 0x06000CCB RID: 3275 RVA: 0x0004E2D2 File Offset: 0x0004C4D2
			public IList<int> IconIdsToAdd { get; set; }

			// Token: 0x170002D9 RID: 729
			// (get) Token: 0x06000CCC RID: 3276 RVA: 0x0004E2DB File Offset: 0x0004C4DB
			// (set) Token: 0x06000CCD RID: 3277 RVA: 0x0004E2E3 File Offset: 0x0004C4E3
			public IList<int> EmailAccommodationControlIds { get; set; }

			// Token: 0x170002DA RID: 730
			// (get) Token: 0x06000CCE RID: 3278 RVA: 0x0004E2EC File Offset: 0x0004C4EC
			// (set) Token: 0x06000CCF RID: 3279 RVA: 0x0004E2F4 File Offset: 0x0004C4F4
			public IList<string> GeneralNotices { get; set; }
		}

		// Token: 0x020001D1 RID: 465
		[Serializable]
		internal class PotentialBookingForStudent
		{
			// Token: 0x170002DB RID: 731
			// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x0004E2FD File Offset: 0x0004C4FD
			// (set) Token: 0x06000CD2 RID: 3282 RVA: 0x0004E305 File Offset: 0x0004C505
			public int Id { get; set; }

			// Token: 0x170002DC RID: 732
			// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x0004E30E File Offset: 0x0004C50E
			// (set) Token: 0x06000CD4 RID: 3284 RVA: 0x0004E316 File Offset: 0x0004C516
			public DateTime StartDateTime { get; set; }

			// Token: 0x170002DD RID: 733
			// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0004E31F File Offset: 0x0004C51F
			// (set) Token: 0x06000CD6 RID: 3286 RVA: 0x0004E327 File Offset: 0x0004C527
			public DateTime EndDateTime { get; set; }

			// Token: 0x170002DE RID: 734
			// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x0004E330 File Offset: 0x0004C530
			// (set) Token: 0x06000CD8 RID: 3288 RVA: 0x0004E338 File Offset: 0x0004C538
			public int RoomPersonId { get; set; }

			// Token: 0x170002DF RID: 735
			// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x0004E341 File Offset: 0x0004C541
			// (set) Token: 0x06000CDA RID: 3290 RVA: 0x0004E349 File Offset: 0x0004C549
			public string RoomTitle { get; set; }

			// Token: 0x170002E0 RID: 736
			// (get) Token: 0x06000CDB RID: 3291 RVA: 0x0004E352 File Offset: 0x0004C552
			// (set) Token: 0x06000CDC RID: 3292 RVA: 0x0004E35A File Offset: 0x0004C55A
			public bool OkToDoubleBook { get; set; }

			// Token: 0x170002E1 RID: 737
			// (get) Token: 0x06000CDD RID: 3293 RVA: 0x0004E363 File Offset: 0x0004C563
			// (set) Token: 0x06000CDE RID: 3294 RVA: 0x0004E36B File Offset: 0x0004C56B
			public int AppliedBreakMinutes { get; set; }
		}

		// Token: 0x020001D2 RID: 466
		internal class AccommodationItem
		{
			// Token: 0x170002E2 RID: 738
			// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x0004E374 File Offset: 0x0004C574
			// (set) Token: 0x06000CE1 RID: 3297 RVA: 0x0004E37C File Offset: 0x0004C57C
			public string Name { get; set; }

			// Token: 0x170002E3 RID: 739
			// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x0004E385 File Offset: 0x0004C585
			// (set) Token: 0x06000CE3 RID: 3299 RVA: 0x0004E38D File Offset: 0x0004C58D
			public string Value { get; set; }
		}
	}
}
