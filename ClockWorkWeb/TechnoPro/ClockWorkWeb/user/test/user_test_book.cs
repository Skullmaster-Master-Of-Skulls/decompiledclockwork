using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
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
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;
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
	// Token: 0x02000064 RID: 100
	public class user_test_book : Page
	{
		// Token: 0x06000274 RID: 628 RVA: 0x0000F0FC File Offset: 0x0000D2FC
		private T GetControl<T>(TemplatedWizardStep wizardStepPanel, string controlName) where T : Control
		{
			return (T)((object)wizardStepPanel.ContentTemplateContainer.FindControl(controlName));
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000F120 File Offset: 0x0000D320
		public string txt_dateClientID()
		{
			return this.GetControl<HtmlInputText>(this.step_classdatetime, "txt_date").ClientID;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000F148 File Offset: 0x0000D348
		public string lbl_dateClientID()
		{
			return this.GetControl<Label>(this.step_classdatetime, "lbl_date").ClientID;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000F170 File Offset: 0x0000D370
		public string radslideclientid()
		{
			return this.GetControl<RadSlider>(this.step_classdatetime, "RadSlider1").ClientID;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000F198 File Offset: 0x0000D398
		public string lbl_durationClientID()
		{
			return this.GetControl<Label>(this.step_classdatetime, "lbl_duration").ClientID;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000F1C0 File Offset: 0x0000D3C0
		public string txtClassStartTimeClientId()
		{
			return this.GetControl<TextBox>(this.step_classdatetime, "classStartTimePicker").ClientID;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000F1E8 File Offset: 0x0000D3E8
		public string lb_accommodationsClientId()
		{
			return this.GetControl<RadListBox>(this.step_confirmAndComplete, "lb_accommodations").ClientID;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000F210 File Offset: 0x0000D410
		public string accommodationsclientid()
		{
			return this.GetControl<CheckBoxList>(this.step_chooseAccommodations, "chk_accommodations").ClientID;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000F238 File Offset: 0x0000D438
		public string hf_maxdurationclientid()
		{
			return this.GetControl<HiddenField>(this.step_classdatetime, "hf_maxduration").ClientID;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000F260 File Offset: 0x0000D460
		public string potentialtimesclientid()
		{
			return this.GetControl<RadioButtonList>(this.step_selectTime, "rbtn_potentials").ClientID;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000F288 File Offset: 0x0000D488
		public string existingclassdatetimes()
		{
			return this.GetControl<RadioButtonList>(this.step_classdatetime, "rbtns_existingClassDateTimes").ClientID;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000F2B0 File Offset: 0x0000D4B0
		private void FixWizardStepOrdering(bool dontAskForInstructorConfirmation)
		{
			Label control = this.GetControl<Label>(this.step_selectTime, "lbl_selectTime");
			int num = 1;
			for (int i = 1; i < this.Wizard1.WizardSteps.Count; i++)
			{
				TestWizardPage wizardPage = this.GetWizardPage(this.Wizard1.WizardSteps[i]);
				bool flag = false;
				Label label = null;
				switch (wizardPage)
				{
				case TestWizardPage.SelectACourse:
					flag = true;
					label = this.GetControl<Label>(this.step_selectCourse, "lblTitle");
					break;
				case TestWizardPage.ClassDateTime:
					flag = true;
					label = this.GetControl<Label>(this.step_classdatetime, "Label5");
					break;
				case TestWizardPage.InstructorInfo:
				{
					bool flag2 = !dontAskForInstructorConfirmation;
					if (flag2)
					{
						flag = true;
						label = this.GetControl<Label>(this.step_confirmProfInfo, "Label1");
					}
					break;
				}
				case TestWizardPage.AdditionalRequirements:
				{
					flag = true;
					bool flag3 = this.Wizard1.WizardSteps[i].Controls.Count > 0 && this.Wizard1.WizardSteps[i].Controls[0] is Label;
					if (flag3)
					{
						label = (Label)this.Wizard1.WizardSteps[i].Controls[0];
					}
					break;
				}
				case TestWizardPage.ChooseAccommodations:
					flag = true;
					label = this.GetControl<Label>(this.step_chooseAccommodations, "lbl_chooseAccommodations");
					break;
				case TestWizardPage.SelectYourTestTime:
					flag = true;
					label = control;
					break;
				case TestWizardPage.ConfirmAndComplete:
					flag = true;
					label = this.GetControl<Label>(this.step_confirmAndComplete, "lbl_confirmAndCompleteTitle");
					break;
				}
				bool flag4 = flag;
				if (flag4)
				{
					WizardStepBase wizardStepBase = this.Wizard1.WizardSteps[i];
					string text = wizardStepBase.Title;
					bool flag5 = !string.IsNullOrEmpty(text.Trim());
					if (flag5)
					{
						wizardStepBase.Title = string.Format("{0}{1}", num.ToString(), text.Substring(1));
					}
					bool flag6 = label != null;
					if (flag6)
					{
						text = label.Text;
						label.Text = string.Format("{0}{1}", num.ToString(), text.Substring(1));
					}
					num++;
				}
			}
			control.Text = this.step_selectTime.Title;
			this.GetControl<Label>(this.step_confirmAndComplete, "lbl_confirmAndCompleteTitle").Text = this.step_confirmAndComplete.Title;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000F51C File Offset: 0x0000D71C
		private void SetupAdditionalInfoScreen(int screenNum, bool dontAskForInstructorConfirmation)
		{
			Label control = this.GetControl<Label>(this.step_selectTime, "lbl_selectTime");
			int num = dontAskForInstructorConfirmation ? 4 : 5;
			string arg = num.ToString();
			WizardStep wizardStep = new WizardStep();
			string text = this.GetSettingValue<bool>(Setting.TESTBOOKING_CustomWizardStepRewording_Enabled) ? this.GetSettingValue<string>(Setting.TESTBOOKING_CustomWizardStepRewording_StepAdditionalInfo) : "5. Additional Requirements";
			bool flag = string.IsNullOrEmpty(text);
			if (flag)
			{
				text = "5. Additional Requirements";
			}
			wizardStep.Title = string.Format("{0}. {1}", arg, (text.Length > 3) ? text.Substring(3) : text);
			wizardStep.ID = "step_additionalRequirements";
			Label child = new Label
			{
				Text = string.Format("<h1 class='PageTitle'>{0}</h1>", wizardStep.Title)
			};
			wizardStep.Controls.Add(child);
			this.step_additionalInfo = wizardStep;
			child = new Label
			{
				Text = "Please fill in the appropriate information below.",
				CssClass = "Intro4"
			};
			wizardStep.Controls.Add(child);
			Panel panel = new Panel
			{
				ID = "p_data",
				CssClass = "DynamicForm"
			};
			wizardStep.Controls.Add(panel);
			this.Wizard1.WizardSteps.Insert(num, wizardStep);
			this.step_selectTime.Title = string.Format("{0}{1}", (num + 1).ToString(), this.step_selectTime.Title.Substring(1));
			this.step_confirmAndComplete.Title = string.Format("{0}{1}", (num + 2).ToString(), this.step_confirmAndComplete.Title.Substring(1));
			control.Text = this.step_selectTime.Title;
			this.GetControl<Label>(this.step_confirmAndComplete, "lbl_confirmAndCompleteTitle").Text = this.step_confirmAndComplete.Title;
			DynamicControlLayoutHelper dynamicControlLayoutHelper = new DynamicControlLayoutHelper();
			DynamicScreenLayout.ControlsToScreen(ref dynamicControlLayoutHelper, base.Cache, screenNum, panel, null, false, false, "");
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000F71C File Offset: 0x0000D91C
		private int LookupStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000F740 File Offset: 0x0000D940
		private void Page_Init(object sender, EventArgs e)
		{
			int settingValue = this.GetSettingValue<int>(Setting.TESTBOOKING_WizardSetting_AdditionalInformationScreenNum);
			bool settingValue2 = this.GetSettingValue<bool>(Setting.TESTBOOKING_dontAskStudentToConfirmInstructorInformation);
			bool flag = false;
			Label control = this.GetControl<Label>(this.step_chooseAccommodations, "lbl_chooseAccommodations");
			bool flag2 = settingValue2;
			if (flag2)
			{
				this.step_confirmProfInfo.Title = " ";
				this.step_chooseAccommodations.Title = string.Format("{0}{1}", "3", this.step_chooseAccommodations.Title.Substring(1));
				control.Text = this.step_chooseAccommodations.Title;
				flag = true;
				this.GetControl<Panel>(this.step_confirmAndComplete, "p_instructorVal").Visible = false;
			}
			bool flag3 = settingValue > 0;
			if (flag3)
			{
				this.SetupAdditionalInfoScreen(settingValue, settingValue2);
			}
			bool flag4 = flag;
			if (flag4)
			{
				this.FixWizardStepOrdering(settingValue2);
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000F810 File Offset: 0x0000DA10
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

		// Token: 0x06000284 RID: 644 RVA: 0x0000F850 File Offset: 0x0000DA50
		private string CheckCustomRuleWhetherStudentCanBook(int pid)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			string settingValue = this.GetSettingValue<string>(Setting.TESTBOOKING_CustomAllowStudentToBookCheckSql);
			bool flag = !string.IsNullOrEmpty(settingValue);
			if (flag)
			{
				DataTable dataTable = new DataTable();
				try
				{
					DbParameter[] parameters = new DbParameter[]
					{
						clockWork.GetParameter("@pid", DbType.Int32, pid)
					};
					dataTable = clockWork.ExecuteQuery(settingValue, parameters);
					bool flag2 = dataTable.Rows.Count > 0;
					if (flag2)
					{
						return dataTable.Rows[0][0].ToString().Trim();
					}
				}
				catch (Exception ex)
				{
				}
			}
			return null;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000F904 File Offset: 0x0000DB04
		private T GetSettingValue<T>(Setting setting)
		{
			return new WebSettingsClientManager().GetSettingValue<T>(setting);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000F921 File Offset: 0x0000DB21
		private void NotAllowed(Setting setting)
		{
			NavigatorClientManager.CurrentInstance.NotAllowed(setting, this.Page);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000F938 File Offset: 0x0000DB38
		private void NotAllowed(eNotAllowedCode notAllowedCode, Dictionary<string, string> args = null)
		{
			bool flag = args == null;
			if (flag)
			{
				args = new Dictionary<string, string>();
			}
			NavigatorClientManager.CurrentInstance.NotAllowed(notAllowedCode, args, this.Page);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000F968 File Offset: 0x0000DB68
		private string SetupCustomWizardStepLabels()
		{
			this.ChangeWizardStepTitle(this.step_welcome, this.GetSettingValue<string>(Setting.TESTBOOKING_CustomWizardStepRewording_StepWelcome));
			this.ChangeWizardStepTitle(this.step_selectCourse, this.GetSettingValue<string>(Setting.TESTBOOKING_CustomWizardStepRewording_StepSelectCourse));
			this.ChangeWizardStepTitle(this.step_classdatetime, this.GetSettingValue<string>(Setting.TESTBOOKING_CustomWizardStepRewording_StepIndicateClassDateTime));
			bool settingValue = this.GetSettingValue<bool>(Setting.TESTBOOKING_dontAskStudentToConfirmInstructorInformation);
			bool flag = !settingValue;
			if (flag)
			{
				this.ChangeWizardStepTitle(this.step_confirmProfInfo, this.GetSettingValue<string>(Setting.TESTBOOKING_CustomWizardStepRewording_StepConfirmInstructorInfo));
			}
			this.ChangeWizardStepTitle(this.step_chooseAccommodations, this.GetSettingValue<string>(Setting.TESTBOOKING_CustomWizardStepRewording_StepChooseAccommodations));
			string settingValue2 = this.GetSettingValue<string>(Setting.TESTBOOKING_CustomWizardStepRewording_StepSelectScheduledTime);
			this.ChangeWizardStepTitle(this.step_selectTime, settingValue2);
			this.ChangeWizardStepTitle(this.step_confirmAndComplete, this.GetSettingValue<string>(Setting.TESTBOOKING_CustomWizardStepRewording_StepConfirmAndComplete));
			return settingValue2;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000FA3C File Offset: 0x0000DC3C
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			bool flag = !this.Page.IsPostBack;
			if (!flag)
			{
				bool flag2 = this.Wizard1.ActiveStep == this.step_classdatetime;
				if (flag2)
				{
					string value = this.GetControl<HiddenField>(this.step_classdatetime, "lbl_usingExistingClassDateTime").Value;
					bool flag3 = value == "1";
					if (flag3)
					{
						this.GetControl<HtmlInputText>(this.step_classdatetime, "txt_date").Focus();
					}
					else
					{
						this.GetControl<RadioButtonList>(this.step_classdatetime, "rbtns_existingClassDateTimes").Focus();
					}
				}
			}
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			bool flag4 = true;
			bool settingValue = this.GetSettingValue<bool>(Setting.TESTBOOKING_StudentsAllowedToBookTests);
			bool flag5 = !settingValue;
			if (flag5)
			{
				this.NotAllowed(Setting.TESTBOOKING_ErrorMessage_ModuleInactive);
				flag4 = false;
			}
			bool flag6 = flag4;
			int num;
			if (flag6)
			{
				num = this.LookupStudentPid();
				bool flag7 = num < 1;
				if (flag7)
				{
					this.NotAllowed(Setting.GENERAL_ErrorMessage_NotAClockWorkStudent);
					flag4 = false;
				}
			}
			else
			{
				num = 0;
			}
			bool flag8 = flag4;
			if (flag8)
			{
				IAccommodationsWebClientManager accommodationsWebClientManager = new AccommodationsWebClientManager();
				bool flag9 = accommodationsWebClientManager.AreAccommodationsCurrentlyExpired(num, true);
				bool flag10 = flag9;
				if (flag10)
				{
					this.NotAllowed(Setting.TESTBOOKING_ErrorMessage_AccommodationsExpired);
					flag4 = false;
				}
			}
			bool flag11 = flag4;
			if (flag11)
			{
				bool settingValue2 = this.GetSettingValue<bool>(Setting.TESTBOOKING_CustomAllowStudentToBookCheckSqlEnabled);
				bool flag12 = settingValue2;
				if (flag12)
				{
					string value2 = this.CheckCustomRuleWhetherStudentCanBook(num);
					bool flag13 = !string.IsNullOrEmpty(value2);
					if (flag13)
					{
						CacheStorageManager.Current.Insert("web_test_custom_check_emsg_" + num.ToString(), value2);
						this.NotAllowed(Setting.TESTBOOKING_CustomAllowStudentToBookCheckSql);
					}
				}
			}
			bool flag14 = flag4;
			if (flag14)
			{
				int[] settingValue3 = this.GetSettingValue<int[]>(Setting.TESTBOOKING_NoBookingIfNotAtLeastOneFieldFilledOut_cids);
				bool flag15 = settingValue3 != null && settingValue3.Length != 0;
				if (flag15)
				{
					string value3 = string.Join(",", new List<int>(settingValue3).ConvertAll<string>((int f) => f.ToString()).ToArray());
					string query = "SELECT dataid FROM perstudentdata2 WHERE personid=@pid AND controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','))";
					DbParameter[] parameters = new DbParameter[]
					{
						clockWork.GetParameter("@pid", DbType.Int32, num),
						clockWork.GetParameter("@cids", DbType.String, value3)
					};
					DataTable dataTable = clockWork.ExecuteQuery(query, parameters);
					bool flag16 = dataTable.Rows.Count < 1;
					if (flag16)
					{
						this.NotAllowed(Setting.TESTBOOKING_ErrorMessage_MissingPerStudentData);
						flag4 = false;
					}
				}
			}
			bool flag17 = flag4;
			if (flag17)
			{
				bool settingValue4 = this.GetSettingValue<bool>(Setting.TESTBOOKING_EnforceRegistrationDateRange);
				bool flag18 = settingValue4;
				if (flag18)
				{
					DateTime settingValue5 = this.GetSettingValue<DateTime>(Setting.TESTBOOKING_RegistrationStartDate);
					DateTime settingValue6 = this.GetSettingValue<DateTime>(Setting.TESTBOOKING_RegistrationEndDate);
					DateTime date = DateTime.Now.Date;
					bool flag19 = date < settingValue5 || date > settingValue6;
					if (flag19)
					{
						this.NotAllowed(Setting.TESTBOOKING_ErrorMessage_NotInRegistrationDateRange);
					}
				}
			}
			this.Page.Form.DefaultButton = null;
			bool flag20 = flag4 && !this.Page.IsPostBack;
			if (flag20)
			{
				this.SetupPageOnFirstLoad(num);
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000FD54 File Offset: 0x0000DF54
		private void AddCoursesToScreen(IList<CourseRegistrationDTO> courses, int bookedLucid)
		{
			string text = this.GetSettingValue<string>(Setting.TESTBOOKING_FilterCourseListByTimeOfDay).ToLower();
			string[] source = text.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			bool flag = !string.IsNullOrEmpty(text);
			DropDownList control = this.GetControl<DropDownList>(this.step_selectCourse, "cmb_course");
			foreach (CourseRegistrationDTO courseRegistrationDTO in courses)
			{
				bool flag2 = flag;
				bool flag3;
				if (flag2)
				{
					string tod = courseRegistrationDTO.Course.TimeOfDay ?? "";
					flag3 = source.All((string fx) => !tod.StartsWith(fx));
				}
				else
				{
					flag3 = true;
				}
				bool flag4 = flag3;
				if (flag4)
				{
					DateTime? dateTime = new DateTime?(courseRegistrationDTO.Course.StartDate);
					DateTime? dateTime2 = new DateTime?(courseRegistrationDTO.Course.EndDate);
					int luCourseId = courseRegistrationDTO.Course.LuCourseId;
					string text2 = ClockWorkWebAPI.Course.CourseToString(courseRegistrationDTO.Course);
					string value = string.Format("{0},{1},{2}", luCourseId.ToString(), (dateTime != null) ? dateTime.Value.ToString("yyyy-MM-dd") : "", (dateTime2 != null) ? dateTime2.Value.ToString("yyyy-MM-dd") : "");
					ListItem listItem = new ListItem(text2, value);
					control.Items.Add(listItem);
					bool flag5 = bookedLucid > 0 && bookedLucid == luCourseId;
					if (flag5)
					{
						listItem.Selected = true;
					}
				}
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000FF18 File Offset: 0x0000E118
		private void SetupPageOnFirstLoad(int pid)
		{
			bool flag = base.Master != null && base.Master is IClockWorkMasterPage;
			if (flag)
			{
				((IClockWorkMasterPage)base.Master).SetCurrentPage(eClockWorkWebPage.TestBooking_BookTest);
			}
			bool settingValue = this.GetSettingValue<bool>(Setting.TESTBOOKING_HideCheckAllCheckNone);
			bool flag2 = settingValue;
			if (flag2)
			{
				this.GetControl<Panel>(this.step_chooseAccommodations, "p_checkAllCheckNone").Visible = false;
			}
			string text = this.GetSettingValue<bool>(Setting.TESTBOOKING_CustomWizardStepRewording_Enabled) ? this.SetupCustomWizardStepLabels() : "";
			object obj = this.Session["lastbookedtest"];
			bool flag3 = obj != null && obj is BookedTest;
			BookedTest bookedTest;
			if (flag3)
			{
				bookedTest = (BookedTest)obj;
			}
			else
			{
				bookedTest = null;
			}
			this.Session.Remove("lastbookedtest");
			((Button)this.step_welcome.CustomNavigationTemplateContainer.FindControl("CancelButton")).OnClientClick = "if (!confirm('Are you sure you want to cancel?')) return;";
			((Button)this.step_selectTime.CustomNavigationTemplateContainer.FindControl("CancelButton")).OnClientClick = "if (!confirm('Are you sure you want to cancel?')) return;";
			((Button)this.step_selectCourse.CustomNavigationTemplateContainer.FindControl("CancelButton")).OnClientClick = "if (!confirm('Are you sure you want to cancel?')) return;";
			((Button)this.step_confirmProfInfo.CustomNavigationTemplateContainer.FindControl("CancelButton")).OnClientClick = "if (!confirm('Are you sure you want to cancel?')) return;";
			((Button)this.step_confirmAndComplete.CustomNavigationTemplateContainer.FindControl("CancelButton")).OnClientClick = "if (!confirm('Are you sure you want to cancel?')) return;";
			((Button)this.step_classdatetime.CustomNavigationTemplateContainer.FindControl("CancelButton")).OnClientClick = "if (!confirm('Are you sure you want to cancel?')) return;";
			((Button)this.step_chooseAccommodations.CustomNavigationTemplateContainer.FindControl("CancelButton")).OnClientClick = "if (!confirm('Are you sure you want to cancel?')) return;";
			IAutoTestBookingWebClientManager autoTestBookingWebClientManager = new AutoTestBookingWebClientManager();
			MinMaxDateRangeValue minMaxDateRangeValue = autoTestBookingWebClientManager.FigureOutMinMaxDateRangeStudentIsAllowedToBookForTest(pid);
			bool flag4 = minMaxDateRangeValue.Status != eMinMaxDateRangeInvalidReason.IsValid;
			if (flag4)
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
				DateTime start = minMaxDateRangeValue.DateRange.Start;
				this.GetControl<HiddenField>(this.step_confirmProfInfo, "cutoffDate").Value = start.ToString("yyyy-MM-dd H:mm");
				Button button = this.Wizard1.FindControl("StartNavigationTemplateContainerID").FindControl("CancelButton") as Button;
				bool flag5 = button != null;
				if (flag5)
				{
					button.OnClientClick = "return confirm('Are you sure you want to cancel?')";
				}
				HtmlInputText control = this.GetControl<HtmlInputText>(this.step_classdatetime, "txt_date");
				control.Attributes.Add("mindate", minMaxDateRangeValue.DateRange.Start.ToString("yyyy-MM-dd"));
				control.Attributes.Add("maxdate", minMaxDateRangeValue.DateRange.End.ToString("yyyy-MM-dd"));
				this.Wizard1.CancelDestinationPageUrl = this.GetSettingValue<string>(Setting.TESTBOOKING_TestBookingCancelUrl);
				Label wizardStepLabel = this.GetWizardStepLabel(this.step_welcome);
				bool flag6 = wizardStepLabel != null;
				if (flag6)
				{
					wizardStepLabel.Text = this.GetSettingValue<string>(Setting.TESTBOOKING_WizardSetting_WelcomeMsg);
				}
				this.GetControl<Label>(this.step_selectTime, "lbl_pleaseselectadate").Text = this.GetSettingValue<string>(Setting.TESTBOOKING_SelectADateTimeMessageToStudents);
				bool settingValue2 = this.GetSettingValue<bool>(Setting.TESTBOOKING_AskStudentForCourseAlternateContactInfo);
				TableRow control2 = this.GetControl<TableRow>(this.step_confirmProfInfo, "row_altContactName");
				TableRow control3 = this.GetControl<TableRow>(this.step_confirmProfInfo, "row_altContactPhone");
				TableRow control4 = this.GetControl<TableRow>(this.step_confirmProfInfo, "row_altContact");
				bool flag7 = !settingValue2;
				if (flag7)
				{
					control4.Visible = false;
					control2.Visible = false;
					control3.Visible = false;
				}
				string settingValue3 = this.GetSettingValue<string>(Setting.TESTBOOKING_WizardSetting_ConfirmBookingMsg);
				bool flag8 = settingValue3.Length > 0;
				if (flag8)
				{
					this.GetControl<Label>(this.step_confirmAndComplete, "lbl_finishMessage").Text = settingValue3;
				}
				string settingValue4 = this.GetSettingValue<string>(Setting.TESTBOOKING_WizardSetting_ConfirmationPage_IntroText);
				bool flag9 = settingValue4.Length > 0;
				if (flag9)
				{
					this.GetControl<Label>(this.step_confirmAndComplete, "lbl_confirmationIntroMsg").Text = settingValue4;
					this.GetControl<Panel>(this.step_confirmAndComplete, "p_confirmationIntroMsg").Visible = true;
				}
				string settingValue5 = this.GetSettingValue<string>(Setting.TESTBOOKING_WizardSetting_ConfirmationPage_IAgreeText);
				bool flag10 = settingValue5.Length > 0;
				if (flag10)
				{
					this.GetControl<CheckBox>(this.step_confirmAndComplete, "chk_iagree").Text = settingValue5;
				}
				string settingValue6 = this.GetSettingValue<string>(Setting.TESTBOOKING_RestrictCoursesToCampus);
				bool settingValue7 = this.GetSettingValue<bool>(Setting.TESTBOOKING_OnlyAllowBookingForCoursesThatHaveHadTheLOAGenerated);
				bool settingValue8 = this.GetSettingValue<bool>(Setting.TESTBOOKING_OnlyAllowBookingForCoursesThatInstructorHasConfirmedReceiptOfLOAOnline);
				bool settingValue9 = this.GetSettingValue<bool>(Setting.TESTBOOKING_OnlyAllowBookingForCoursesThatHaveApprovedAccommodationLetterRequest);
				ICourseRegistrationClientManager courseRegistrationClientManager = new CourseRegistrationClientManager();
				StudentCourseListDTO studentCourseListDTO = courseRegistrationClientManager.LoadCoursesStudentIsAllowedToBookTestsForNow(pid);
				IList<CourseRegistrationDTO> list = (studentCourseListDTO != null) ? studentCourseListDTO.Courses : null;
				bool flag11 = list != null && list.Count > 0;
				if (flag11)
				{
					user_test_book.<>c__DisplayClass26_0 CS$<>8__locals1 = new user_test_book.<>c__DisplayClass26_0();
					bool flag12 = settingValue8;
					if (flag12)
					{
						list = (from g in list
						where g.DateLetterReturned != null
						select g).ToList<CourseRegistrationDTO>();
					}
					user_test_book.<>c__DisplayClass26_0 CS$<>8__locals2 = CS$<>8__locals1;
					string[] onlyAllowTheseCampuses;
					if (!string.IsNullOrEmpty(settingValue6))
					{
						onlyAllowTheseCampuses = (from g in settingValue6.Split(new char[]
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
					bool flag13 = CS$<>8__locals1.onlyAllowTheseCampuses.Length != 0;
					if (flag13)
					{
						list = (from g in list
						where CS$<>8__locals1.onlyAllowTheseCampuses.Any(delegate(string h)
						{
							LookupCourseDTO course = g.Course;
							return h.Equals((((course != null) ? course.Campus : null) ?? "").Trim(), StringComparison.OrdinalIgnoreCase);
						})
						select g).ToList<CourseRegistrationDTO>();
					}
					bool flag14 = settingValue7;
					if (flag14)
					{
						list = (from g in list
						where g.DateLetterIssued != null
						select g).ToList<CourseRegistrationDTO>();
					}
					bool flag15 = settingValue9;
					if (flag15)
					{
						list = (from g in list
						where g.CourseAccommodationRequestBase != null && g.CourseAccommodationRequestBase.Status == eStudentCourseAccommodationRequestStatusDTO.Approved
						select g).ToList<CourseRegistrationDTO>();
					}
				}
				bool flag16 = list.Count < 1;
				if (flag16)
				{
					bool flag17 = studentCourseListDTO != null && studentCourseListDTO.AtLeastOneCourseRemovedBecauseOfSpecialAccommodationNotAllowedToBookRestriction;
					if (flag17)
					{
						this.NotAllowed(eNotAllowedCode.NoCoursesAvailableToBookBecauseSpecialAccBanForTestBooking, null);
					}
					else
					{
						this.NotAllowed(Setting.TESTBOOKING_ErrorMessage_NoCourses);
					}
				}
				else
				{
					this.AddCoursesToScreen(list, (bookedTest != null) ? bookedTest.Lucid : 0);
					int settingValue10 = this.GetSettingValue<int>(Setting.TESTBOOKING_WizardSetting_AdditionalInformationScreenNum);
					bool flag18 = settingValue10 < 1;
					if (flag18)
					{
						this.GetControl<Label>(this.step_confirmAndComplete, "lbl_additionalRequirements").Visible = false;
						this.GetControl<Label>(this.step_confirmAndComplete, "lbl_additionalRequirementsValue").Visible = false;
						this.GetControl<Panel>(this.step_confirmAndComplete, "p_conf_additionalRequirements").Visible = false;
					}
					bool settingValue11 = this.GetSettingValue<bool>(Setting.TESTBOOKING_StudentAllowedToSelectOwnDateTime);
					this.GetControl<LinkButton>(this.step_classdatetime, "btn_chooseAnotherDate").Visible = settingValue11;
					bool flag19 = !settingValue11;
					if (flag19)
					{
						this.ShowHideClassDateManualEntry(false);
					}
					bool settingValue12 = this.GetSettingValue<bool>(Setting.TESTBOOKING_StudentAllowedToSelectPreviousDateTimes);
					bool settingValue13 = this.GetSettingValue<bool>(Setting.TESTBOOKING_StudentAllowedToSelectPreviousClassTestDefinitions);
					bool settingValue14 = this.GetSettingValue<bool>(Setting.TESTBOOKING_StudentAllowedToSelectPreviousClassTestDefinitionsFromRegistrar);
					bool flag20 = settingValue12 || settingValue13 || settingValue14;
					this.GetControl<LinkButton>(this.step_classdatetime, "btn_chooseExistingClassDateTime").Visible = flag20;
					Label control5 = this.GetControl<Label>(this.step_classdatetime, "lbl_enterClassDateTimeDuration");
					bool flag21 = !flag20;
					if (flag21)
					{
						this.GetControl<Panel>(this.step_classdatetime, "p_existingExams").Visible = false;
						this.ShowHideClassDateManualEntry(settingValue11);
						string settingValue15 = this.GetSettingValue<string>(Setting.TESTBOOKING_SelectClassDateTimeInstruction);
						bool flag22 = !string.IsNullOrEmpty(settingValue15);
						if (flag22)
						{
							control5.Text = settingValue15;
						}
					}
					else
					{
						control5.Visible = false;
					}
					string settingValue16 = this.GetSettingValue<string>(Setting.TESTBOOKING_ClassDateTimeIntro);
					bool flag23 = !string.IsNullOrEmpty(settingValue16);
					if (flag23)
					{
						Label control6 = this.GetControl<Label>(this.step_classdatetime, "lbl_classDateTimeIntro");
						control6.Text = settingValue16;
						control6.Visible = true;
					}
					IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
					string settingValue17 = webSettingsClientManager.GetSettingValue<string>(Setting.TESTBOOKING_MessageWhenNoClassDatesAndTimesAreAvailableToChooseFrom);
					this.GetControl<Label>(this.step_classdatetime, "lbl_noExistingClassDateTimes").Text = settingValue17;
					this.GetControl<Label>(this.step_classdatetime, "lbl_enterClassDateTimeDuration_nodates").Text = settingValue17;
					bool flag24 = bookedTest != null;
					if (flag24)
					{
						control.Value = bookedTest.ClassStartDate.ToString("M/d/yyyy");
						this.GetControl<TextBox>(this.step_classdatetime, "classStartTimePicker").Text = bookedTest.ClassStartDate.ToString("h:mm tt").ToLower();
						TimeSpan timeSpan = bookedTest.ClassEndDate - bookedTest.ClassStartDate;
						HtmlInputGenericControl control7 = this.GetControl<HtmlInputGenericControl>(this.step_classdatetime, "txt_duration_hours");
						HtmlInputGenericControl control8 = this.GetControl<HtmlInputGenericControl>(this.step_classdatetime, "txt_duration_minutes");
						control7.Value = Convert.ToInt32(timeSpan.Hours).ToString();
						control8.Value = Convert.ToInt32(timeSpan.Minutes).ToString();
						foreach (object obj2 in this.GetControl<CheckBoxList>(this.step_chooseAccommodations, "chk_accommodations").Items)
						{
							ListItem listItem = (ListItem)obj2;
							listItem.Selected = true;
						}
					}
					string settingValue18 = this.GetSettingValue<string>(Setting.TESTBOOKING_SelectCourseInstructionMessage);
					bool flag25 = !string.IsNullOrEmpty(settingValue18);
					if (flag25)
					{
						this.GetControl<Panel>(this.step_selectCourse, "p_courseInstruction").Visible = true;
						this.GetControl<Label>(this.step_selectCourse, "lbl_courseInstruction").Text = settingValue18;
					}
					bool settingValue19 = this.GetSettingValue<bool>(Setting.TESTBOOKING_AskStudentForInstructorPhone);
					if (settingValue19)
					{
						this.GetControl<TableRow>(this.step_confirmProfInfo, "row_instructorPhone").Visible = true;
					}
					bool settingValue20 = this.GetSettingValue<bool>(Setting.TESTBOOKING_AskStudentForCourseAlternateContactInfo);
					bool flag26 = !settingValue20;
					if (flag26)
					{
						control4.Visible = false;
						control2.Visible = false;
						control3.Visible = false;
					}
					string settingValue21 = this.GetSettingValue<string>(Setting.TESTBOOKING_ChooseAccommodationsInstructions);
					string settingValue22 = this.GetSettingValue<string>(Setting.TESTBOOKING_ChooseAccommodationsNote);
					this.GetControl<Label>(this.step_chooseAccommodations, "lbl_chooseAccommodationsInstructions").Text = settingValue21;
					this.GetControl<Label>(this.step_chooseAccommodations, "lbl_accommodationsNote").Text = settingValue22;
					bool settingValue23 = this.GetSettingValue<bool>(Setting.TESTBOOKING_AllowStudentToSelectFromApprovedDateTimes);
					Label control9 = this.GetControl<Label>(this.step_selectTime, "lbl_availableDatesTimesImportantNote");
					Label control10 = this.GetControl<Label>(this.step_selectTime, "lbl_potential");
					bool flag27 = !settingValue23;
					if (flag27)
					{
						control9.Text = control9.Text.Replace("none of the dates/times below are possible", "the date/time below is not possible");
						this.GetControl<RadioButtonList>(this.step_selectTime, "rbtn_potentials").Visible = false;
						control10.Visible = true;
						string text2 = string.IsNullOrEmpty(text) ? "5. Search status" : text;
						Label control11 = this.GetControl<Label>(this.step_selectTime, "lbl_selectTime");
						control11.Text = control11.Text.Substring(0, 3) + text2.Substring(3);
						this.step_selectTime.Title = control11.Text;
						this.GetControl<Panel>(this.step_selectTime, "p_availableDatesTimes").GroupingText = "";
						control9.Visible = false;
						control10.Visible = false;
						this.GetControl<Label>(this.step_confirmAndComplete, "lbl_yourTestDateTime").Visible = false;
						this.GetControl<Label>(this.step_confirmAndComplete, "lbl_yourTestDateTimeVal").Visible = false;
					}
					string settingValue24 = this.GetSettingValue<string>(Setting.TESTBOOKING_AvailableTestDateTimesImportantNote);
					bool flag28 = !string.IsNullOrEmpty(settingValue24);
					if (flag28)
					{
						control9.Text = settingValue24;
					}
					string settingValue25 = this.GetSettingValue<string>(Setting.TESTBOOKING_NoRoomFoundMessage);
					string settingValue26 = this.GetSettingValue<string>(Setting.TESTBOOKING_RoomFoundMessage);
					this.GetControl<Label>(this.step_selectTime, "lbl_nodates").Text = settingValue25;
					this.GetControl<Label>(this.step_selectTime, "lbl_dateFound").Text = settingValue26;
					int maxDuration = this.GetMaxDuration();
					HiddenField control12 = this.GetControl<HiddenField>(this.step_classdatetime, "hf_maxduration");
					control12.Value = maxDuration.ToString();
				}
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00010B88 File Offset: 0x0000ED88
		private void SetAppropriateFocus()
		{
			WizardStepBase activeStep = this.Wizard1.ActiveStep;
			bool flag = activeStep == this.step_welcome;
			if (flag)
			{
				user_test_book.SetFocus2(this.GetControl<Label>(this.step_welcome, "lbl_welcome"));
			}
			else
			{
				bool flag2 = activeStep == this.step_selectCourse;
				if (flag2)
				{
					user_test_book.SetFocus2(this.GetControl<DropDownList>(this.step_selectCourse, "cmb_course"));
				}
				else
				{
					bool flag3 = activeStep == this.step_classdatetime;
					if (flag3)
					{
						user_test_book.SetFocus2ForClassDateTimeStep(this.step_classdatetime);
					}
					else
					{
						bool flag4 = activeStep == this.step_confirmProfInfo;
						if (flag4)
						{
							user_test_book.SetFocus2(this.GetControl<TextBox>(this.step_confirmProfInfo, "txt_instructorName"));
						}
						else
						{
							bool flag5 = this.Wizard1.ActiveStep == this.step_chooseAccommodations;
							if (flag5)
							{
								user_test_book.SetFocus2(this.GetControl<CheckBoxList>(this.step_chooseAccommodations, "chk_accommodations"));
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
										user_test_book.SetFocus2(control);
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
											user_test_book.SetFocus2(control2);
										}
										else
										{
											user_test_book.SetFocus2(this.GetControl<Label>(this.step_selectTime, "lbl_pleaseselectadate"));
										}
									}
									else
									{
										bool flag10 = activeStep == this.step_confirmAndComplete;
										if (flag10)
										{
											user_test_book.SetFocus2ForSummaryStep(this.GetControl<CheckBox>(this.step_confirmAndComplete, "chk_iagree"));
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00010DA8 File Offset: 0x0000EFA8
		private static void SetFocus2ForClassDateTimeStep(Control control)
		{
			string activeJavascript = "try { SelectPotentialTimesRadioButtonList2(); } catch ( ex0 ) { } \r\n" + "try { FocusClassDate(); } catch (ex) { } \r\n";
			user_test_book.SetFocus2(control, activeJavascript);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00010DD0 File Offset: 0x0000EFD0
		private static void SetFocus2ForSummaryStep(Control control)
		{
			string activeJavascript = "try { FocusTextBox('" + control.ClientID + "'); } catch ( ex0 ) { } \r\n" + "try { MakeSummaryAlertPop(); } catch (ex) { } \r\n";
			user_test_book.SetFocus2(control, activeJavascript);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00010E04 File Offset: 0x0000F004
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
				user_test_book.SetFocus2(control, activeJavascript);
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00010E80 File Offset: 0x0000F080
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

		// Token: 0x06000291 RID: 657 RVA: 0x00010F6C File Offset: 0x0000F16C
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

		// Token: 0x06000292 RID: 658 RVA: 0x000110D0 File Offset: 0x0000F2D0
		private TestWizardPage GetWizardPage(WizardStepBase step)
		{
			TestWizardPage result = TestWizardPage.Welcome;
			bool flag = step == this.step_welcome;
			if (flag)
			{
				result = TestWizardPage.Welcome;
			}
			else
			{
				bool flag2 = step == this.step_selectCourse;
				if (flag2)
				{
					result = TestWizardPage.SelectACourse;
				}
				else
				{
					bool flag3 = step == this.step_classdatetime;
					if (flag3)
					{
						result = TestWizardPage.ClassDateTime;
					}
				}
			}
			bool flag4 = step == this.step_confirmProfInfo;
			if (flag4)
			{
				result = TestWizardPage.InstructorInfo;
			}
			else
			{
				bool flag5 = step == this.step_chooseAccommodations;
				if (flag5)
				{
					result = TestWizardPage.ChooseAccommodations;
				}
				else
				{
					bool flag6 = step == this.step_selectTime;
					if (flag6)
					{
						result = TestWizardPage.SelectYourTestTime;
					}
					else
					{
						bool flag7 = step == this.step_confirmAndComplete;
						if (flag7)
						{
							result = TestWizardPage.ConfirmAndComplete;
						}
						else
						{
							bool flag8 = step.ID.Equals("step_additionalRequirements");
							if (flag8)
							{
								result = TestWizardPage.AdditionalRequirements;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0001117C File Offset: 0x0000F37C
		private void ShowCourseEMessage(string msg)
		{
			this.lbl_course_emsg2.Text = msg;
			this.p_course_emsg_2.Visible = true;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0001119C File Offset: 0x0000F39C
		protected void Wizard1_ActiveStepChanged(object sender, EventArgs e)
		{
			bool settingValue = this.GetSettingValue<bool>(Setting.TESTBOOKING_dontAskStudentToConfirmInstructorInformation);
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
			DateTime date = DateTime.Now.Date;
			bool flag2 = dateTime > date;
			if (flag2)
			{
				dateTime = date;
			}
			int pid = this.LookupStudentPid();
			DateTime? dateTime2;
			DateTime? dateTime3;
			int selectedLucid = this.GetSelectedLucid(out dateTime2, out dateTime3);
			int num = (selectedLucid > 0) ? this.GetLastSelectedLucid() : 0;
			int num2 = 0;
			bool flag3 = dateTime3 != null && dateTime3.Value > value;
			if (flag3)
			{
				value = dateTime3.Value;
			}
			bool flag4 = dateTime2 != null && dateTime2.Value < dateTime;
			if (flag4)
			{
				dateTime = dateTime2.Value;
			}
			TestWizardPage wizardPage = this.GetWizardPage(this.Wizard1.ActiveStep);
			string settingValue2 = this.GetSettingValue<string>(Setting.TESTBOOKING_WizardSetting_ConfirmBookingFinishButtonText);
			bool flag5 = !string.IsNullOrEmpty(settingValue2);
			if (flag5)
			{
				Button button = (Button)this.step_confirmAndComplete.CustomNavigationTemplateContainer.FindControl("StepNextButton");
				bool flag6 = button != null;
				if (flag6)
				{
					button.Text = ((wizardPage == TestWizardPage.ConfirmAndComplete) ? settingValue2 : "Next");
				}
			}
			HiddenField control = this.GetControl<HiddenField>(this.step_confirmProfInfo, "lastSelectedLucid");
			WizardStepBase activeStep = this.Wizard1.ActiveStep;
			string text = (activeStep == null) ? "" : activeStep.Title;
			bool flag7 = !string.IsNullOrEmpty(text);
			if (flag7)
			{
				this.Page.Title = "Schedule a Test - " + text;
			}
			IList<user_test_book.AccommodationItem> list = null;
			IList<user_test_book.AccommodationItem> list2 = null;
			bool flag8 = this.Wizard1.ActiveStep == this.step_selectTime || this.Wizard1.ActiveStep == this.step_additionalInfo || this.Wizard1.ActiveStep == this.step_confirmAndComplete;
			if (flag8)
			{
				bool flag9 = this.GetCheckedAccommodationItems(out list, out list2);
				int count = list.Count;
				bool flag10 = count < 1;
				if (flag10)
				{
					this.Wizard1.ActiveStepIndex = 4;
					return;
				}
			}
			try
			{
				bool flag11 = selectedLucid > 0 && num != selectedLucid;
				if (flag11)
				{
					user_test_book.eReasonNotAllowedToChooseCourse eReasonNotAllowedToChooseCourse = this.CourseChanged(selectedLucid, dateTime, value, pid);
					bool flag12 = eReasonNotAllowedToChooseCourse == user_test_book.eReasonNotAllowedToChooseCourse.NoTestExamAccommodations;
					if (flag12)
					{
						bool flag13 = this.Wizard1.ActiveStepIndex != 0;
						if (flag13)
						{
							this.Wizard1.ActiveStepIndex = this.Wizard1.WizardSteps.IndexOf(this.step_selectCourse);
						}
						this.ShowCourseEMessage("You don't have any test/exam related accommodations for this course.  Please contact your advisor for assistance with booking this test.");
						control.Value = "";
						return;
					}
					bool flag14 = eReasonNotAllowedToChooseCourse == user_test_book.eReasonNotAllowedToChooseCourse.LoaNotIssued;
					if (flag14)
					{
						bool flag15 = this.Wizard1.ActiveStepIndex != 0;
						if (flag15)
						{
							this.Wizard1.ActiveStepIndex = this.Wizard1.WizardSteps.IndexOf(this.step_selectCourse);
						}
						this.ShowCourseEMessage("Your accommodations for this course are not yet active.  Please contact your advisor to book this test.");
						control.Value = "";
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
				bool flag16 = this.Wizard1.ActiveStep == this.step_welcome;
				if (flag16)
				{
					num2 = 0;
					user_test_book.SetFocus2(this.GetControl<Label>(this.step_welcome, "lbl_welcome"));
				}
				else
				{
					bool flag17 = this.Wizard1.ActiveStep == this.step_selectCourse;
					if (flag17)
					{
						num2 = 1;
					}
					else
					{
						bool flag18 = this.Wizard1.ActiveStep == this.step_classdatetime;
						if (flag18)
						{
							num2 = 2;
							Panel control2 = this.GetControl<Panel>(this.step_classdatetime, "p_classDateandTime");
							Panel control3 = this.GetControl<Panel>(this.step_classdatetime, "p_existingExams");
							bool flag19 = control2 != null && control3 != null && control2.Visible && control3.Visible;
							if (flag19)
							{
								this.GetControl<HiddenField>(this.step_classdatetime, "lbl_usingExistingClassDateTime").Value = "";
								control3.Visible = false;
							}
						}
					}
				}
				bool flag20 = this.Wizard1.ActiveStep == this.step_confirmProfInfo;
				if (flag20)
				{
					num2 = 3;
				}
				else
				{
					bool flag21 = this.Wizard1.ActiveStep == this.step_chooseAccommodations;
					if (flag21)
					{
						num2 = 4;
					}
					else
					{
						bool flag22 = this.Wizard1.ActiveStep == this.step_selectTime;
						if (flag22)
						{
							num2 = 6;
							DateTime classDateTime = this.GetClassDateTime();
							int selectedDurationMinutes = this.GetSelectedDurationMinutes();
							DateTime dateTime4 = classDateTime.AddMinutes((double)selectedDurationMinutes);
							bool flag23 = classDateTime < DateTime.Now || selectedDurationMinutes < 1;
							if (flag23)
							{
								this.Wizard1.ActiveStepIndex = 1;
								return;
							}
							Panel pdata = this.GetPData();
							List<TryToBookAccommodationToUseDTO> selectedAccommodations = this.GetSelectedAccommodations();
							IList<TryToBookAccommodationToUseDTO> additionalAccommodationsToUse = this.GetAdditionalAccommodationsToUse(pdata, pid);
							Label control4 = this.GetControl<Label>(this.step_confirmAndComplete, "lbl_additionalRequirementsValue");
							bool visible2 = control4.Visible;
							if (visible2)
							{
								int settingValue3 = this.GetSettingValue<int>(Setting.TESTBOOKING_WizardSetting_AdditionalInformationScreenNum);
								DynamicScreenLayout.AddSummaryToLabel(control4, pdata, settingValue3, pid, base.Cache, new DynamicControlLayoutHelper(), "", true);
							}
							user_test_book.PotentialBookingsForStudent potentialBookingsForStudent = this.TryToFindBooking(pid, selectedLucid, classDateTime, selectedDurationMinutes, (from g in selectedAccommodations
							select g.ControlId).ToList<int>(), additionalAccommodationsToUse);
							RadioButtonList control5 = this.GetControl<RadioButtonList>(this.step_selectTime, "rbtn_potentials");
							control5.Items.Clear();
							Panel control6 = this.GetControl<Panel>(this.step_selectTime, "p_nodates");
							Label control7 = this.GetControl<Label>(this.step_selectTime, "lbl_dateFound");
							bool flag24 = potentialBookingsForStudent.Bookings == null || potentialBookingsForStudent.Bookings.Count < 1;
							if (flag24)
							{
								control6.Visible = true;
								control7.Visible = false;
								bool flag25 = !string.IsNullOrEmpty(potentialBookingsForStudent.NoBookingsAvailableMessage);
								if (flag25)
								{
									this.GetControl<Label>(this.step_selectTime, "lbl_nodates").Text = potentialBookingsForStudent.NoBookingsAvailableMessage;
								}
								string text2 = string.Join(", ", potentialBookingsForStudent.GeneralNotices.ToArray<string>());
								CWLogger.Logger.Info("TESTBOOK:NoDatesFound:pid={0}:lucid={1}:classdatetime={2} to {3}:privatenotes={4}", new object[]
								{
									pid.ToString(),
									selectedLucid.ToString(),
									classDateTime.ToString("yyyy-MM-dd H:mm"),
									dateTime4.ToString("H:mm"),
									text2
								});
							}
							else
							{
								string value2 = (control5.SelectedIndex > 0) ? control5.SelectedValue : null;
								control6.Visible = false;
								control7.Visible = true;
								int num3 = 0;
								List<DateTime> list3 = new List<DateTime>();
								foreach (user_test_book.PotentialBookingForStudent potentialBookingForStudent in potentialBookingsForStudent.Bookings)
								{
									potentialBookingForStudent.Id = num3++;
									DateTime startDateTime = potentialBookingForStudent.StartDateTime;
									bool flag26 = !list3.Contains(startDateTime);
									if (flag26)
									{
										list3.Add(startDateTime);
										string text3 = string.Concat(new string[]
										{
											potentialBookingForStudent.StartDateTime.ToString("dddd MMMM d"),
											" . ",
											potentialBookingForStudent.StartDateTime.ToString("h:mm tt"),
											" to ",
											potentialBookingForStudent.EndDateTime.ToString("h:mm tt")
										});
										ListItem listItem = new ListItem(text3, potentialBookingForStudent.Id.ToString());
										control5.Items.Add(listItem);
										bool flag27 = !string.IsNullOrEmpty(value2) && potentialBookingForStudent.Id.ToString().Equals(value2);
										if (flag27)
										{
											listItem.Selected = true;
										}
									}
								}
								bool flag28 = control5.Items.Count == 1;
								if (flag28)
								{
									control5.Items[0].Selected = true;
									this.GetControl<Label>(this.step_selectTime, "lbl_potential").Text = control5.Items[0].Text;
								}
								bool flag29 = control5.Items.Count == 1;
								if (flag29)
								{
									control5.Items[0].Selected = true;
								}
							}
						}
						else
						{
							bool flag30 = this.Wizard1.ActiveStep == this.step_confirmAndComplete;
							if (flag30)
							{
								num2 = 7;
								this.GetControl<Panel>(this.step_confirmAndComplete, "p_emsg").Visible = false;
								user_test_book.PotentialBookingForStudent selectedPotentialTest = this.GetSelectedPotentialTest();
								bool flag31 = selectedPotentialTest == null;
								if (flag31)
								{
									this.Wizard1.ActiveStepIndex = this.Wizard1.ActiveStepIndex - 1;
									return;
								}
								StringBuilder stringBuilder = new StringBuilder();
								stringBuilder.AppendFormat("{0} . {1} to {2}", selectedPotentialTest.StartDateTime.ToString("ddd MMM d, yyyy"), selectedPotentialTest.StartDateTime.ToString("h:mm tt"), selectedPotentialTest.EndDateTime.ToString("h:mm tt"));
								stringBuilder.Append(" (");
								stringBuilder.Append(((int)(selectedPotentialTest.EndDateTime - selectedPotentialTest.StartDateTime).TotalMinutes).GetDurationDescriptionShort());
								stringBuilder.Append(")");
								this.GetControl<Label>(this.step_confirmAndComplete, "lbl_yourTestDateTimeVal").Text = stringBuilder.ToString();
								DateTime classDateTime2 = this.GetClassDateTime();
								int selectedDurationMinutes2 = this.GetSelectedDurationMinutes();
								stringBuilder = new StringBuilder();
								stringBuilder.Append(classDateTime2.ToString("ddd MMM d, yyyy h:mm tt"));
								stringBuilder.Append(" (");
								stringBuilder.Append(selectedDurationMinutes2.GetDurationDescriptionShort());
								stringBuilder.Append(")");
								this.GetControl<Label>(this.step_confirmAndComplete, "lbl_classDateTimeVal").Text = stringBuilder.ToString();
								this.GetControl<Label>(this.step_confirmAndComplete, "lbl_courseVal").Text = this.GetControl<DropDownList>(this.step_selectCourse, "cmb_course").SelectedItem.Text;
								string arg = HttpUtility.HtmlEncode(this.GetControl<TextBox>(this.step_confirmProfInfo, "txt_instructorName").Text);
								string arg2 = HttpUtility.HtmlEncode(this.GetControl<TextBox>(this.step_confirmProfInfo, "txt_instructorEmail").Text);
								this.GetControl<Label>(this.step_confirmAndComplete, "lbl_instructorVal").Text = string.Format("{0} . {1}", arg, arg2);
								bool flag9 = !this.GetSettingValue<bool>(Setting.TESTBOOKING_ShowAccommodationsStudentOptedOutOfInConfirmation);
								bool flag32 = !flag9;
								if (flag32)
								{
									this.GetControl<Label>(this.step_confirmAndComplete, "lbl_accommodations").Text = "You opted out of the following accommodation(s):";
								}
								RadListBox control8 = this.GetControl<RadListBox>(this.step_confirmAndComplete, "lb_accommodations");
								control8.Items.Clear();
								foreach (object obj in this.GetControl<CheckBoxList>(this.step_chooseAccommodations, "chk_accommodations").Items)
								{
									ListItem listItem2 = (ListItem)obj;
									bool flag33 = listItem2.Selected == flag9;
									if (flag33)
									{
										RadListBoxItem item = new RadListBoxItem(listItem2.Text, listItem2.Value);
										control8.Items.Add(item);
									}
								}
								Label control9 = this.GetControl<Label>(this.step_confirmAndComplete, "lbl_noAccommodations");
								bool flag34 = control8.Items.Count < 1;
								if (flag34)
								{
									control8.Visible = false;
									control9.Text = "None";
								}
								else
								{
									bool flag35 = !control8.Visible;
									if (flag35)
									{
										control8.Visible = true;
										control9.Text = "";
									}
								}
								Label control10 = this.GetControl<Label>(this.step_confirmAndComplete, "lbl_additionalRequirementsValue");
								bool visible3 = control10.Visible;
								if (visible3)
								{
									int settingValue4 = this.GetSettingValue<int>(Setting.TESTBOOKING_WizardSetting_AdditionalInformationScreenNum);
									Panel pdata2 = this.GetPData();
									DynamicScreenLayout.AddSummaryToLabel(control10, pdata2, settingValue4, pid, base.Cache, new DynamicControlLayoutHelper(), "", true);
								}
							}
						}
					}
				}
				bool flag36 = this.Wizard1.ActiveStep != null;
				if (flag36)
				{
					num2 = this.Wizard1.WizardSteps.IndexOf(this.Wizard1.ActiveStep);
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("TESTBOOK:WizardActiveStepChanged:pid={0}:page={1}:lucid={2}:errmsg={3}", new object[]
				{
					pid.ToString(),
					this.Wizard1.ActiveStepIndex.ToString(),
					selectedLucid.ToString(),
					ex.ToString()
				});
			}
			finally
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				try
				{
					bool flag37 = num2 > 0;
					if (flag37)
					{
						int selectedDurationMinutes3 = this.GetSelectedDurationMinutes();
						stringBuilder2.AppendFormat("classdate={0};classduration={1}", this.GetClassDateTime().ToString("yyyy-MM-dd h:mm tt"), selectedDurationMinutes3);
					}
				}
				catch
				{
				}
				CWLogger.Logger.Info("TESTBOOK:WizardActiveStepChanged:pid={0}:page={1}:lucid={2}:pageinfo={3}", new object[]
				{
					pid.ToString(),
					this.Wizard1.ActiveStepIndex.ToString(),
					selectedLucid.ToString(),
					stringBuilder2.ToString()
				});
			}
			bool flag38 = wizardPage > TestWizardPage.SelectACourse;
			if (flag38)
			{
				bool flag39 = this.GetControl<DropDownList>(this.step_selectCourse, "cmb_course").SelectedIndex <= 0;
				if (flag39)
				{
					this.Wizard1.ActiveStepIndex = 1;
					return;
				}
			}
			bool flag40 = wizardPage > TestWizardPage.ClassDateTime && this.GetClassDateTime() == DateTime.MinValue;
			if (flag40)
			{
				this.Wizard1.ActiveStepIndex = 2;
			}
			else
			{
				this.SetAppropriateFocus();
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00011FFC File Offset: 0x000101FC
		private bool GetCheckedAccommodationItems(out IList<user_test_book.AccommodationItem> checkedAccommodationItems, out IList<user_test_book.AccommodationItem> unCheckedAccommodationItems)
		{
			bool result = !this.GetSettingValue<bool>(Setting.TESTBOOKING_ShowAccommodationsStudentOptedOutOfInConfirmation);
			checkedAccommodationItems = new List<user_test_book.AccommodationItem>();
			unCheckedAccommodationItems = new List<user_test_book.AccommodationItem>();
			CheckBoxList control = this.GetControl<CheckBoxList>(this.step_chooseAccommodations, "chk_accommodations");
			foreach (object obj in control.Items)
			{
				ListItem listItem = (ListItem)obj;
				bool selected = listItem.Selected;
				if (selected)
				{
					checkedAccommodationItems.Add(new user_test_book.AccommodationItem
					{
						Name = listItem.Text,
						Value = listItem.Value
					});
				}
				else
				{
					unCheckedAccommodationItems.Add(new user_test_book.AccommodationItem
					{
						Name = listItem.Text,
						Value = listItem.Value
					});
				}
			}
			return result;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000120E8 File Offset: 0x000102E8
		private user_test_book.PotentialBookingsForStudent TryToFindBooking(int pid, int lucid, DateTime classStartDateTime, int classDurationInMinutes, IList<int> accommodationCidsToUse, IList<TryToBookAccommodationToUseDTO> AdditionalAccommodationsToUse)
		{
			IAutoTestBookingClientManager autoTestBookingClientManager = new AutoTestBookingClientManager();
			TryToBookResultDTO resp = autoTestBookingClientManager.TryToFindBooking(eTestExamSettingType.Midterm, false, pid, lucid, classStartDateTime, classDurationInMinutes, accommodationCidsToUse, false, 0, AdditionalAccommodationsToUse, false, null);
			string noBookingsAvailableMessage = null;
			bool flag = resp.PotentialBookings.Count < 1;
			if (flag)
			{
				noBookingsAvailableMessage = (resp.StudentAlreadyHadAnotherTestBookedForSameDayAndCourse ? "You have already scheduled a test or exam with us for this course and day." : this.GetSettingValue<string>(Setting.TESTBOOKING_NoRoomFoundMessage));
			}
			RadioButtonList control = this.GetControl<RadioButtonList>(this.step_selectTime, "rbtn_potentials");
			string text = (control.SelectedIndex > 0) ? control.SelectedValue : null;
			user_test_book.PotentialBookingsForStudent potentialBookingsForStudent = new user_test_book.PotentialBookingsForStudent
			{
				Bookings = (from g in resp.PotentialBookings
				select new user_test_book.PotentialBookingForStudent
				{
					StartDateTime = g.StartDateTime,
					EndDateTime = g.EndDateTime,
					RoomPersonId = g.Room.PersonId,
					RoomTitle = g.Room.Title,
					AppliedBreakMinutes = resp.AppliedBreakMinutes,
					OkToDoubleBook = (g.Room.RoomType == eRoomType.VirtualRoom || g.Room.RoomType == eRoomType.SuperVirtualRoom)
				}).ToList<user_test_book.PotentialBookingForStudent>(),
				IconIdsToAdd = resp.IconIdsToBookWith,
				EmailAccommodationControlIds = resp.AccommodationCidsForEmail,
				NoBookingsAvailableMessage = noBookingsAvailableMessage,
				GeneralNotices = resp.NoticesForAllPotentialBookings
			};
			this.Session.Add("potentialtestbookings", potentialBookingsForStudent);
			return potentialBookingsForStudent;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00012208 File Offset: 0x00010408
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

		// Token: 0x06000298 RID: 664 RVA: 0x00012288 File Offset: 0x00010488
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

		// Token: 0x06000299 RID: 665 RVA: 0x00012330 File Offset: 0x00010530
		private DateTime GetClassDateTime()
		{
			bool flag = this.GetControl<HiddenField>(this.step_classdatetime, "lbl_usingExistingClassDateTime").Value.Equals("1");
			DateTime result;
			if (flag)
			{
				string selectedValue = this.GetControl<RadioButtonList>(this.step_classdatetime, "rbtns_existingClassDateTimes").SelectedValue;
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

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00012448 File Offset: 0x00010648
		private Panel p_course_emsg
		{
			get
			{
				return (Panel)this.step_selectCourse.ContentTemplateContainer.FindControl("p_course_emsg");
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00012474 File Offset: 0x00010674
		private Panel p_course_emsg_2
		{
			get
			{
				return (Panel)this.step_selectCourse.ContentTemplateContainer.FindControl("p_course_emsg_2");
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600029C RID: 668 RVA: 0x000124A0 File Offset: 0x000106A0
		private Label lbl_course_emsg2
		{
			get
			{
				return (Label)this.step_selectCourse.ContentTemplateContainer.FindControl("lbl_course_emsg2");
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x000124CC File Offset: 0x000106CC
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

		// Token: 0x0600029E RID: 670 RVA: 0x00012558 File Offset: 0x00010758
		private user_test_book.eReasonNotAllowedToChooseCourse CourseChanged(int newLucid, DateTime sdate, DateTime edate, int pid)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			this.GetControl<HiddenField>(this.step_confirmProfInfo, "lastSelectedLucid").Value = newLucid.ToString();
			DataTable dataTable = ClockWorkController.Course.LoadStudentsCourse(pid, newLucid, sdate, edate);
			TextBox control = this.GetControl<TextBox>(this.step_confirmProfInfo, "txt_altContactName");
			TextBox control2 = this.GetControl<TextBox>(this.step_confirmProfInfo, "txt_altContactEmail");
			TextBox control3 = this.GetControl<TextBox>(this.step_confirmProfInfo, "txt_altContactPhone");
			TextBox control4 = this.GetControl<TextBox>(this.step_confirmProfInfo, "txt_instructorPhone");
			Label control5 = this.GetControl<Label>(this.step_confirmProfInfo, "lbl_courseDescription");
			TextBox control6 = this.GetControl<TextBox>(this.step_confirmProfInfo, "txt_instructorEmail");
			TextBox control7 = this.GetControl<TextBox>(this.step_confirmProfInfo, "txt_instructorName");
			bool flag = dataTable.Rows.Count > 0;
			if (flag)
			{
				DataRow dataRow = dataTable.Rows[0];
				bool visible = this.p_course_emsg.Visible;
				if (visible)
				{
					this.p_course_emsg.Visible = false;
				}
				control7.Text = HttpUtility.HtmlDecode(dataRow["instructor"].ToString());
				control6.Text = HttpUtility.HtmlDecode(dataRow["instructoremail"].ToString());
				bool settingValue = this.GetSettingValue<bool>(Setting.TESTBOOKING_AskStudentForInstructorPhone);
				if (settingValue)
				{
					control4.Text = HttpUtility.HtmlDecode(dataRow["instructorphone"].ToString());
				}
				control5.Text = HttpUtility.HtmlDecode(ClockWorkWebAPI.Course.CourseToString(dataRow));
				bool settingValue2 = this.GetSettingValue<bool>(Setting.TESTBOOKING_AskStudentForCourseAlternateContactInfo);
				bool flag2 = settingValue2;
				if (flag2)
				{
					string query = "SELECT alternatecontactid,altname,altemail,altphone FROM lucoursealternatecontact WHERE alternatecontactid IN (SELECT alternatecontactid FROM lucourses WHERE lucourseid=@lucid)";
					DbParameter[] parameters = new DbParameter[]
					{
						clockWork.GetParameter("@lucid", DbType.Int32, newLucid)
					};
					DataTable dataTable2 = clockWork.ExecuteQuery(query, parameters);
					bool flag3 = dataTable2.Rows.Count > 0;
					if (flag3)
					{
						DataRow dataRow2 = dataTable2.Rows[0];
						control2.Text = HttpUtility.HtmlDecode(dataRow2["altemail"].ToString());
						control.Text = HttpUtility.HtmlDecode(dataRow2["altname"].ToString());
						control3.Text = HttpUtility.HtmlDecode(dataRow2["altphone"].ToString());
					}
					else
					{
						control2.Text = "";
						control.Text = "";
						control3.Text = "";
					}
				}
			}
			else
			{
				control6.Text = "";
				control7.Text = "";
				control5.Text = "unknown";
				control4.Text = "";
				control2.Text = "";
				control.Text = "";
				control3.Text = "";
			}
			ClockWorkWebAPI.AccommodationCollection accommodationCollection = ClockWorkController.Accommodation.LoadAccommodations(pid, newLucid, "");
			accommodationCollection.SortListByCaptionWithValue();
			string settingValue3 = this.GetSettingValue<string>(Setting.TESTBOOKING_NonNegotiableAccommodationCids);
			List<int> list = user_test_book.IntListFromString(settingValue3);
			bool settingValue4 = this.GetSettingValue<bool>(Setting.TESTBOOKING_WizardSetting_AccommodationsDefaultChecked);
			CheckBoxList control8 = this.GetControl<CheckBoxList>(this.step_chooseAccommodations, "chk_accommodations");
			control8.Items.Clear();
			foreach (object obj in accommodationCollection)
			{
				ClockWorkWebAPI.Accommodation accommodation = (ClockWorkWebAPI.Accommodation)obj;
				string value = HttpUtility.HtmlEncode(accommodation.ControlId.ToString() + "`" + accommodation.ControlCaption);
				string text = HttpUtility.HtmlEncode(accommodation.CaptionWithValue);
				ListItem listItem = new ListItem(text, value);
				control8.Items.Add(listItem);
				bool flag4 = settingValue4;
				if (flag4)
				{
					listItem.Selected = true;
				}
				bool flag5 = list.Contains(accommodation.ControlId);
				if (flag5)
				{
					listItem.Selected = true;
					listItem.Enabled = false;
				}
			}
			bool flag6 = accommodationCollection.Count < 1;
			user_test_book.eReasonNotAllowedToChooseCourse result;
			if (flag6)
			{
				control8.Items.Add("");
				control8.Enabled = false;
				result = user_test_book.eReasonNotAllowedToChooseCourse.NoTestExamAccommodations;
			}
			else
			{
				bool flag7 = !control8.Enabled;
				if (flag7)
				{
					control8.Enabled = true;
				}
				bool settingValue5 = this.GetSettingValue<bool>(Setting.TESTBOOKING_StudentAllowedToSelectPreviousDateTimes);
				bool settingValue6 = this.GetSettingValue<bool>(Setting.TESTBOOKING_StudentAllowedToSelectPreviousClassTestDefinitions);
				bool settingValue7 = this.GetSettingValue<bool>(Setting.TESTBOOKING_StudentAllowedToSelectPreviousClassTestDefinitionsFromRegistrar);
				bool flag8 = settingValue5 || settingValue6 || settingValue7;
				bool flag9 = flag8;
				if (flag9)
				{
					bool flag10 = settingValue5;
					DataTable dataTable3;
					if (flag10)
					{
						dataTable3 = ClockWorkController.Appointment.LoadPreviouslySubmittedTests(newLucid, 0);
						bool flag11 = settingValue6;
						if (flag11)
						{
							DataTable dataTable4 = ClockWorkController.Appointment.LoadPreviouslySubmittedClassTestDefinitionsByTypeCode(newLucid, 0, "", "F");
							bool flag12 = dataTable4 != null && dataTable4.Rows.Count > 0;
							if (flag12)
							{
								bool flag13 = dataTable3 == null || dataTable3.Columns.Count < 1;
								if (flag13)
								{
									dataTable3 = dataTable4;
								}
								else
								{
									foreach (object obj2 in dataTable4.Rows)
									{
										DataRow row = (DataRow)obj2;
										dataTable3.ImportRow(row);
									}
								}
							}
						}
					}
					else
					{
						bool flag14 = settingValue6;
						if (flag14)
						{
							dataTable3 = ClockWorkController.Appointment.LoadPreviouslySubmittedClassTestDefinitionsByTypeCode(newLucid, 0, "", "F");
						}
						else
						{
							bool flag15 = settingValue7;
							if (flag15)
							{
								dataTable3 = ClockWorkController.Appointment.LoadPreviouslySubmittedClassTestDefinitions(newLucid, 0, true);
							}
							else
							{
								dataTable3 = new DataTable();
							}
						}
					}
					IAutoTestBookingWebClientManager autoTestBookingWebClientManager = new AutoTestBookingWebClientManager();
					MinMaxDateRangeValue minMaxDateRangeValue = autoTestBookingWebClientManager.FigureOutMinMaxDateRangeStudentIsAllowedToBookForTest(pid);
					bool flag16 = minMaxDateRangeValue.Status != eMinMaxDateRangeInvalidReason.IsValid;
					if (flag16)
					{
						dataTable3.Rows.Clear();
					}
					DateTime date = minMaxDateRangeValue.DateRange.Start.Date;
					DateTime date2 = minMaxDateRangeValue.DateRange.End.Date;
					RadioButtonList control9 = this.GetControl<RadioButtonList>(this.step_classdatetime, "rbtns_existingClassDateTimes");
					control9.Items.Clear();
					List<string> list2 = new List<string>();
					foreach (object obj3 in dataTable3.Rows)
					{
						DataRow dataRow3 = (DataRow)obj3;
						DateTime t = (DateTime)dataRow3["startdate"];
						DateTime dateTime = (DateTime)dataRow3["enddate"];
						bool flag17 = t < date;
						if (!flag17)
						{
							bool flag18 = date2 != DateTime.MinValue && t.Date > date2;
							if (!flag18)
							{
								string text2 = t.ToString("dddd MMMM d . h:mm tt") + " to " + dateTime.ToString("h:mm tt");
								string text3 = t.ToString("yyyy-MM-dd HH:mm") + "," + dateTime.ToString("yyyy-MM-dd HH:mm");
								bool flag19 = !list2.Contains(text3);
								if (flag19)
								{
									ListItem item = new ListItem(text2, text3);
									control9.Items.Add(item);
									list2.Add(text3);
								}
							}
						}
					}
					bool flag20 = control9.Items.Count > 0;
					bool flag21 = control9.Items.Count == 1;
					if (flag21)
					{
						control9.Items[0].Selected = true;
					}
					this.GetControl<Label>(this.step_classdatetime, "lbl_noExistingClassDateTimes").Visible = !flag20;
					Panel control10 = this.GetControl<Panel>(this.step_classdatetime, "p_existingExams");
					control10.Visible = flag20;
					bool settingValue8 = this.GetSettingValue<bool>(Setting.TESTBOOKING_StudentAllowedToSelectOwnDateTime);
					this.ShowHideClassDateManualEntry(settingValue8 && !control10.Visible);
					this.GetControl<HiddenField>(this.step_classdatetime, "lbl_usingExistingClassDateTime").Value = (flag20 ? "1" : "0");
					bool visible2 = control10.Visible;
					if (visible2)
					{
						this.GetControl<Label>(this.step_classdatetime, "lbl_enterClassDateTimeDuration_nodates").Visible = (control9.Items.Count < 1);
					}
				}
				result = user_test_book.eReasonNotAllowedToChooseCourse.None;
			}
			return result;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00012DD4 File Offset: 0x00010FD4
		private void ShowHideClassDateManualEntry(bool show)
		{
			Label control = this.GetControl<Label>(this.step_classdatetime, "lbl_enterClassDateTimeDuration");
			Label control2 = this.GetControl<Label>(this.step_classdatetime, "lbl_enterClassDateTimeDuration_nodates");
			Panel control3 = this.GetControl<Panel>(this.step_classdatetime, "p_classDateandTime");
			bool flag = !show;
			if (flag)
			{
				control3.Visible = false;
				bool flag2 = !this.GetControl<LinkButton>(this.step_classdatetime, "btn_chooseAnotherDate").Visible;
				if (flag2)
				{
					control.Visible = false;
					control2.Visible = true;
				}
			}
			else
			{
				control3.Visible = true;
				control.Visible = true;
				control2.Visible = false;
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00012E78 File Offset: 0x00011078
		private int GetMaxDuration()
		{
			int settingValue = this.GetSettingValue<int>(Setting.TESTBOOKING_MaxDuration);
			bool flag = settingValue > 0;
			int result;
			if (flag)
			{
				bool settingValue2 = this.GetSettingValue<bool>(Setting.TESTBOOKING_MaxDurationUseTimetable);
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

		// Token: 0x060002A1 RID: 673 RVA: 0x00012EC8 File Offset: 0x000110C8
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
					MinMaxDateRangeValue minMaxDateRangeValue = autoTestBookingWebClientManager.FigureOutMinMaxDateRangeStudentIsAllowedToBookForTest(num);
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

		// Token: 0x060002A2 RID: 674 RVA: 0x00012FA0 File Offset: 0x000111A0
		private DateTime? GetSelectedDateOfClassTest()
		{
			HtmlInputText control = this.GetControl<HtmlInputText>(this.step_classdatetime, "txt_date");
			string text = control.Value.Trim();
			DateTime value;
			return (text.Length > 0 && DateTime.TryParse(text, out value)) ? new DateTime?(value) : null;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00012FF8 File Offset: 0x000111F8
		protected void ValidateTimeS(object source, ServerValidateEventArgs e)
		{
			TextBox control = this.GetControl<TextBox>(this.step_classdatetime, "classStartTimePicker");
			string text = control.Text.Trim();
			DateTime dateTime;
			e.IsValid = (text.Length > 0 && DateTime.TryParse(DateTime.Now.ToString("yyyy-MM-dd") + " " + text, out dateTime));
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0001305C File Offset: 0x0001125C
		protected void ServerValidationDuration2(object source, ServerValidateEventArgs e)
		{
			int selectedDurationInMinutes_DirectlyFromDurationControls = this.GetSelectedDurationInMinutes_DirectlyFromDurationControls();
			int maxDuration = this.GetMaxDuration();
			e.IsValid = (maxDuration <= 0 || selectedDurationInMinutes_DirectlyFromDurationControls <= maxDuration);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
		{
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00013090 File Offset: 0x00011290
		private int GetLastSelectedLucid()
		{
			string value = this.GetControl<HiddenField>(this.step_confirmProfInfo, "lastSelectedLucid").Value;
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

		// Token: 0x060002A7 RID: 679 RVA: 0x000130E8 File Offset: 0x000112E8
		private int GetSelectedLucid()
		{
			DateTime? dateTime;
			DateTime? dateTime2;
			return this.GetSelectedLucid(out dateTime, out dateTime2);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00013104 File Offset: 0x00011304
		private int GetSelectedLucid(out DateTime? sd, out DateTime? ed)
		{
			DropDownList control = this.GetControl<DropDownList>(this.step_selectCourse, "cmb_course");
			bool flag = control.SelectedItem != null;
			if (flag)
			{
				string value = control.SelectedItem.Value;
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

		// Token: 0x060002A9 RID: 681 RVA: 0x0001324F File Offset: 0x0001144F
		protected void btn_cancel_click(object sender, EventArgs e)
		{
			base.Response.Redirect(this.GetSettingValue<string>(Setting.TESTBOOKING_TestBookingCancelUrl), true);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0001326C File Offset: 0x0001146C
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

		// Token: 0x060002AB RID: 683 RVA: 0x0001342C File Offset: 0x0001162C
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

		// Token: 0x060002AC RID: 684 RVA: 0x000134BC File Offset: 0x000116BC
		private List<TryToBookAccommodationToUseDTO> GetSelectedAccommodations()
		{
			CheckBoxList control = this.GetControl<CheckBoxList>(this.step_chooseAccommodations, "chk_accommodations");
			return (from ListItem li in control.Items
			where li.Selected
			select this.GetAccommodationToUseFromListItem(li)).ToList<TryToBookAccommodationToUseDTO>();
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00013528 File Offset: 0x00011728
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
				int settingValue = this.GetSettingValue<int>(Setting.TESTBOOKING_WizardSetting_AdditionalInformationScreenNum);
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

		// Token: 0x060002AE RID: 686 RVA: 0x0001359C File Offset: 0x0001179C
		private user_test_book.PotentialBookingForStudent GetSelectedPotentialTest()
		{
			user_test_book.PotentialBookingsForStudent potentialBookingsForStudent;
			return this.GetSelectedPotentialTest(out potentialBookingsForStudent);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000135B8 File Offset: 0x000117B8
		private user_test_book.PotentialBookingForStudent GetSelectedPotentialTest(out user_test_book.PotentialBookingsForStudent potentialBookingsForStudent)
		{
			RadioButtonList control = this.GetControl<RadioButtonList>(this.step_selectTime, "rbtn_potentials");
			bool flag = control.SelectedItem != null;
			if (flag)
			{
				object obj = this.Session["potentialtestbookings"];
				bool flag2 = obj != null && obj is user_test_book.PotentialBookingsForStudent;
				if (flag2)
				{
					potentialBookingsForStudent = (user_test_book.PotentialBookingsForStudent)obj;
					string selectedValue = control.SelectedValue;
					int id;
					bool flag3 = int.TryParse(selectedValue, out id);
					if (flag3)
					{
						return potentialBookingsForStudent.Bookings.FirstOrDefault((user_test_book.PotentialBookingForStudent g) => g.Id == id);
					}
				}
			}
			potentialBookingsForStudent = null;
			return null;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00013660 File Offset: 0x00011860
		protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			int num = this.LookupStudentPid();
			user_test_book.PotentialBookingsForStudent potentialBookingsForStudent;
			user_test_book.PotentialBookingForStudent selectedPotentialTest = this.GetSelectedPotentialTest(out potentialBookingsForStudent);
			int selectedLucid = this.GetSelectedLucid();
			List<TryToBookAccommodationToUseDTO> selectedAccommodations = this.GetSelectedAccommodations();
			Panel pdata = this.GetPData();
			DateTime classDateTime = this.GetClassDateTime();
			int selectedDurationMinutes = this.GetSelectedDurationMinutes();
			DateTime dateTime = classDateTime.AddMinutes((double)selectedDurationMinutes);
			bool settingValue = this.GetSettingValue<bool>(Setting.TESTBOOKING_BookTestsAsTentative);
			int settingValue2 = this.GetSettingValue<int>(Setting.TESTBOOKING_AppointmentTypeToUseForBooking);
			int settingValue3 = this.GetSettingValue<int>(Setting.TESTBOOKING_WizardSetting_AdditionalInformationScreenNum);
			bool flag = selectedPotentialTest != null && selectedLucid > 0 && num > 0;
			ClockWorkWebAPI.Appointment.eCreateAppointmentFailedReason eCreateAppointmentFailedReason;
			if (flag)
			{
				bool makeSureRoomIsntAlreadyBooked = !selectedPotentialTest.OkToDoubleBook;
				object obj = this.Session["tb_privatenotes"];
				List<PrivateNoteDTO> list = (obj == null) ? new List<PrivateNoteDTO>() : ((List<PrivateNoteDTO>)obj);
				FindPotentialBookingsInfo findPotentialBookingsInfo = new FindPotentialBookingsInfo();
				findPotentialBookingsInfo.RestrictByCampus = this.GetSettingValue<bool>(Setting.TESTBOOKING_RestrictCoursesToCampus_EnableMatchCampusToRoom);
				findPotentialBookingsInfo.IgnoreStudentsSchedule = this.GetSettingValue<bool>(Setting.TESTBOOKING_IgnoreStudentSchedule);
				findPotentialBookingsInfo.IgnoreTwoTestsSameCourseSameDay = this.GetSettingValue<bool>(Setting.TESTBOOKING_IgnoreStudentTwoTestsSameCourseSameDay);
				List<ClockWorkWebAPI.TestBooking.Accommodation> list2 = new List<ClockWorkWebAPI.TestBooking.Accommodation>();
				foreach (TryToBookAccommodationToUseDTO tryToBookAccommodationToUseDTO in selectedAccommodations)
				{
					list2.Add(new ClockWorkWebAPI.TestBooking.Accommodation(tryToBookAccommodationToUseDTO.ControlId, tryToBookAccommodationToUseDTO.Caption, "", tryToBookAccommodationToUseDTO.Value, 0));
				}
				List<TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper.PrivateNote> list3 = new List<TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper.PrivateNote>();
				foreach (PrivateNoteDTO privateNoteDTO in list)
				{
					list3.Add(new TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper.PrivateNote(privateNoteDTO.Note));
				}
				string studentNote = string.Join("\r\n", list.ConvertAll<string>((PrivateNoteDTO g) => g.Note ?? "").ToArray());
				Exception ex;
				int num2 = ClockWorkController.Appointment.CreateTest(num, selectedPotentialTest.RoomPersonId, makeSureRoomIsntAlreadyBooked, selectedPotentialTest.StartDateTime, selectedPotentialTest.EndDateTime, classDateTime, dateTime, settingValue2, settingValue, selectedLucid, list2, out eCreateAppointmentFailedReason, out ex, selectedPotentialTest.AppliedBreakMinutes, studentNote, findPotentialBookingsInfo);
				bool flag2 = num2 > 0;
				if (flag2)
				{
					CWLogger.Logger.Info("TESTBOOK:book.aspx:SuccessfulBooking:pid={0}:lucid={1}:appid={2}", num.ToString(), selectedLucid.ToString(), num2.ToString());
					bool flag3 = pdata != null;
					if (flag3)
					{
						DynamicScreenLayout.SaveDynamicData(ScreenType.ScreenType_PerAppointment, num, num2, settingValue3, base.Cache, pdata, "");
					}
					ClockWorkWebAPI.Person studentInfo = ClockWorkWebAPI.Person.GetStudentInfo(num, this.Page);
					DropDownList control = this.GetControl<DropDownList>(this.step_selectCourse, "cmb_course");
					string s = (control.SelectedItem == null) ? "" : control.SelectedItem.Text;
					ClockWorkController.Instructor instructor = new ClockWorkController.Instructor(selectedLucid);
					string text = this.GetControl<TextBox>(this.step_confirmProfInfo, "txt_instructorName").Text.Trim();
					string text2 = this.GetControl<TextBox>(this.step_confirmProfInfo, "txt_instructorEmail").Text.Trim();
					bool settingValue4 = this.GetSettingValue<bool>(Setting.TESTBOOKING_AskStudentForInstructorPhone);
					string text3 = settingValue4 ? this.GetControl<TextBox>(this.step_confirmProfInfo, "txt_instructorPhone").Text.Trim() : "";
					StringDictionary stringDictionary = new StringDictionary();
					stringDictionary.Add("classstartdate", classDateTime.ToString("ddd MMMM d, yyyy"));
					stringDictionary.Add("classenddate", dateTime.ToString("ddd MMMM d, yyyy"));
					stringDictionary.Add("classstarttime", classDateTime.ToString("h:mm tt"));
					stringDictionary.Add("classendtime", dateTime.ToString("h:mm tt"));
					stringDictionary.Add("classduration", ClockWorkWebAPI.Core.MinutesToTimeDescription(Convert.ToInt32((dateTime - classDateTime).TotalMinutes)));
					stringDictionary.Add("startdate", selectedPotentialTest.StartDateTime.ToString("ddd MMMM d, yyyy"));
					stringDictionary.Add("starttime", selectedPotentialTest.StartDateTime.ToString("h:mm tt"));
					stringDictionary.Add("endtime", selectedPotentialTest.EndDateTime.ToString("h:mm tt"));
					stringDictionary.Add("enddate", selectedPotentialTest.EndDateTime.ToString("ddd MMMM d, yyyy"));
					string durationDescriptionShort = ((int)(selectedPotentialTest.EndDateTime - selectedPotentialTest.StartDateTime).TotalMinutes).GetDurationDescriptionShort();
					stringDictionary.Add("duration", durationDescriptionShort);
					stringDictionary.Add("room", selectedPotentialTest.RoomTitle);
					stringDictionary.Add("email", HttpUtility.HtmlEncode(studentInfo.Email));
					stringDictionary.Add("firstname", HttpUtility.HtmlEncode(studentInfo.FirstName));
					stringDictionary.Add("lastname", HttpUtility.HtmlEncode(studentInfo.LastName));
					stringDictionary.Add("student_no", HttpUtility.HtmlEncode(studentInfo.StudentNumber));
					stringDictionary.Add("name", HttpUtility.HtmlEncode(studentInfo.Name));
					stringDictionary.Add("accommodations", HttpUtility.HtmlEncode(this.GetAccommodationsString(selectedAccommodations)));
					stringDictionary.Add("course", HttpUtility.HtmlEncode(s));
					stringDictionary.Add("personid", num.ToString());
					stringDictionary.Add("appointmentid", num2.ToString());
					stringDictionary.Add("startdatetime", selectedPotentialTest.StartDateTime.ToString("MMMM d, yyyy . h:mm tt"));
					stringDictionary.Add("instructorname", HttpUtility.HtmlEncode(instructor.InstructorName));
					stringDictionary.Add("instructoremail", HttpUtility.HtmlEncode(instructor.InstructorEmail));
					stringDictionary.Add("instructorphone", HttpUtility.HtmlEncode(instructor.InstructorPhone));
					stringDictionary.Add("subjectemail", HttpUtility.HtmlEncode(instructor.SubjectEmail));
					stringDictionary.Add("newinstructorname", HttpUtility.HtmlEncode(text));
					stringDictionary.Add("newinstructoremail", HttpUtility.HtmlEncode(text2));
					stringDictionary.Add("newinstructorphone", HttpUtility.HtmlEncode(text3));
					stringDictionary.Add("appointment", string.Format("{0} {1} to {2} ({3})", new object[]
					{
						selectedPotentialTest.StartDateTime.ToString("dddd MMMM d, yyyy"),
						selectedPotentialTest.StartDateTime.ToString("h:mm tt"),
						selectedPotentialTest.EndDateTime.ToString("h:mm tt"),
						durationDescriptionShort
					}));
					IMailMergeCodes mailMergeCodes = new MailMergeCodes();
					stringDictionary.Add("from", mailMergeCodes.GetDefaultFromAddress(eWebModule.TestsExams));
					stringDictionary.Add("signature", mailMergeCodes.GetDefaultSignature(eWebModule.TestsExams));
					bool flag4 = settingValue3 > 0;
					if (flag4)
					{
						stringDictionary.Add("additionalinfo", DynamicScreenLayout.GetSummaryPlainText(pdata, settingValue3, num, base.Cache, new DynamicControlLayoutHelper(), "", true));
					}
					string adminEmail = ClockWorkController.Email.GetAdminEmail(Setting.TESTBOOKING_TestBookingCoordinatorEmail);
					stringDictionary.Add("adminemail", adminEmail);
					stringDictionary.Add("coordinatoremail", adminEmail);
					IEmailClientManager emailClientManager = new EmailClientManager();
					MailMergeContextDTO mailMergeContext = new MailMergeContextDTO
					{
						PersonId = num,
						LuCourseId = selectedLucid,
						AppointmentId = num2
					};
					string settingValue5 = this.GetSettingValue<string>(Setting.TESTBOOKING_Email_StudentBookingConfirmation_TemplateRules);
					IDictionary<string, int> campusesWithStudentEmailTemplateIdsFromXml = (settingValue5 ?? "").Trim().GetCampusesWithStudentEmailTemplateIdsFromXml();
					int num3 = 0;
					bool flag5 = campusesWithStudentEmailTemplateIdsFromXml != null && campusesWithStudentEmailTemplateIdsFromXml.Count > 0;
					if (flag5)
					{
						ILookupCourseClientManager lookupCourseClientManager = new LookupCourseClientManager();
						LookupCourseDTO lookupCourseDTO = lookupCourseClientManager.LoadCourseByLuCourseId(selectedLucid);
						string text4 = (((lookupCourseDTO != null) ? lookupCourseDTO.Campus : null) ?? "").Trim().ToLower();
						bool flag6 = text4.Length > 0 && campusesWithStudentEmailTemplateIdsFromXml.ContainsKey(text4);
						if (flag6)
						{
							num3 = campusesWithStudentEmailTemplateIdsFromXml[text4];
						}
					}
					bool flag7 = num3 < 1;
					if (flag7)
					{
						emailClientManager.SendEmail(Setting.TESTBOOKING_Email_StudentBookingConfirmation, mailMergeContext, stringDictionary.InsertBaseUserMailMergeValues(), "TestBooking");
					}
					else
					{
						emailClientManager.SendEmail(num3, mailMergeContext, stringDictionary.InsertBaseUserMailMergeValues(), "TestBooking");
					}
					string value = adminEmail;
					SendEmailsResp sendEmailsResp = emailClientManager.SendEmail(Setting.TESTBOOKING_Email_StudentBookingConfirmationForInstructor, mailMergeContext, stringDictionary.InsertBaseUserMailMergeValues(), "TestBooking");
					bool flag8 = sendEmailsResp != null && sendEmailsResp.SendEmailResult != null && sendEmailsResp.SendEmailResult.Status == eTPMailResultStatusDTO.NotSentBecauseTemplateIsDisabled && string.IsNullOrEmpty(instructor.InstructorEmail);
					if (flag8)
					{
						bool flag9 = !string.IsNullOrEmpty(value);
						if (flag9)
						{
							IEmailClientManager emailClientManager2 = new EmailClientManager();
							MailMergeContextDTO mailMergeContext2 = new MailMergeContextDTO
							{
								PersonId = num,
								LuCourseId = selectedLucid
							};
							emailClientManager2.SendEmail(Setting.TESTBOOKING_InstructorEmail_MissingEmailForInstructorEmailTemplate, mailMergeContext2, stringDictionary.InsertBaseUserMailMergeValues(), "TestBook22");
						}
					}
					bool settingValue6 = this.GetSettingValue<bool>(Setting.TESTBOOKING_AskStudentForCourseAlternateContactInfo);
					bool flag10 = settingValue6;
					if (flag10)
					{
						string text5 = this.GetControl<TextBox>(this.step_confirmProfInfo, "txt_altContactName").Text.Trim();
						string text6 = this.GetControl<TextBox>(this.step_confirmProfInfo, "txt_altContactEmail").Text.Trim();
						string text7 = this.GetControl<TextBox>(this.step_confirmProfInfo, "txt_altContactPhone").Text.Trim();
						bool flag11 = !string.IsNullOrEmpty(text5) || !string.IsNullOrEmpty(text6) || !string.IsNullOrEmpty(text7);
						if (flag11)
						{
							DbParameter[] parameters = new DbParameter[]
							{
								clockWork.GetParameter("@lucid", DbType.Int32, selectedLucid),
								clockWork.GetParameter("@name", DbType.String, HttpUtility.HtmlEncode(text5)),
								clockWork.GetParameter("@email", DbType.String, HttpUtility.HtmlEncode(text6)),
								clockWork.GetParameter("@phone", DbType.String, HttpUtility.HtmlEncode(text7))
							};
							clockWork.ExecuteNonQuery(ClockWorkWebAPI.QueryStorage.QS_InsertUpdate_CourseAlternateContact, parameters);
						}
					}
					bool flag12 = !text.ToLower().Equals(instructor.InstructorName.ToLower()) || !text2.ToLower().Equals(instructor.InstructorEmail.ToLower()) || (settingValue4 && !(text3 ?? "").ToLower().Equals((instructor.InstructorPhone ?? "").Trim().ToLower()));
					if (flag12)
					{
						emailClientManager.SendEmail(Setting.TESTBOOKING_StudentChangeProfInfoEmailTemplate, mailMergeContext, stringDictionary.InsertBaseUserMailMergeValues(), "TestBooking");
					}
					bool flag13 = potentialBookingsForStudent != null && potentialBookingsForStudent.EmailAccommodationControlIds != null && potentialBookingsForStudent.EmailAccommodationControlIds.Count > 0;
					if (flag13)
					{
						stringDictionary.Add("list", this.GetAccommodationsString((from g in selectedAccommodations
						where potentialBookingsForStudent.EmailAccommodationControlIds.Contains(g.ControlId)
						select g).ToList<TryToBookAccommodationToUseDTO>()));
						emailClientManager.SendEmail(Setting.TESTBOOKING_SpecialAccommodationsEmailTemplate, mailMergeContext, stringDictionary.InsertBaseUserMailMergeValues(), "ExamBooking");
					}
					this.GetControl<RadioButtonList>(this.step_classdatetime, "rbtns_existingClassDateTimes").SelectedIndex = -1;
					this.GetControl<RadioButtonList>(this.step_selectTime, "rbtn_potentials").SelectedIndex = -1;
					control.SelectedIndex = -1;
					this.Session.Remove("potentialtestbookings");
					this.Session.Remove("lastbookedtest");
					List<ClockWorkWebAPI.TestBooking.Accommodation> accommodationsToUse = this.ConvertAccommodationsToOld(selectedAccommodations);
					BookedTest bookedTest = new BookedTest(num, selectedPotentialTest.RoomPersonId, selectedPotentialTest.StartDateTime, selectedPotentialTest.EndDateTime, classDateTime, dateTime, settingValue2, selectedLucid, accommodationsToUse);
					bool flag14 = settingValue3 > 0;
					if (flag14)
					{
						Exception ex2;
						DataTable dynamicData = DynamicScreenLayout.SaveDynamicDataToDataTable(ScreenType.ScreenType_PerAppointment, num, num2, settingValue3, base.Cache, pdata, "", out ex2);
						bool flag15 = ex2 == null;
						if (flag15)
						{
							bookedTest.DynamicData = dynamicData;
						}
					}
					this.Session.Add("lastbookedtest", bookedTest);
					string key = "studentapps" + num.ToString();
					bool flag16 = base.Cache[key] != null;
					if (flag16)
					{
						base.Cache.Remove(key);
					}
					base.Response.Redirect("Thankyou.aspx");
					return;
				}
			}
			else
			{
				eCreateAppointmentFailedReason = ClockWorkWebAPI.Appointment.eCreateAppointmentFailedReason.MissingInformation;
			}
			string str = string.Format("If the problem persists please contact us to book your test{0}.", Misc.GetContactInformationHtml(Setting.TESTBOOKING_DepartmentContactInformation));
			string text8 = string.Format("{0} to {1}", selectedPotentialTest.StartDateTime.ToString("yyyy-MM-dd H:mm"), selectedPotentialTest.EndDateTime.ToString("H:mm"));
			switch (eCreateAppointmentFailedReason)
			{
			case ClockWorkWebAPI.Appointment.eCreateAppointmentFailedReason.RoomDoubleBooked:
				this.ShowEMessage("Unfortunately the location that was selected for you to write your test was scheduled by another student just before your attempt to complete your booking.  Please use the 'Previous' button below to go back and try to find another potential spot for your test.");
				CWLogger.Logger.Warn("TESTBOOK:Wizard:BookingFailed:pid={0}:lucid={1}:Room double booked[{2}] [{3}]", new object[]
				{
					num.ToString(),
					selectedLucid.ToString(),
					string.Format("{0}-{1}", selectedPotentialTest.RoomTitle, selectedPotentialTest.RoomPersonId.ToString()),
					text8
				});
				break;
			case ClockWorkWebAPI.Appointment.eCreateAppointmentFailedReason.MissingInformation:
				this.ShowEMessage("There was a problem - some information seems to be missing.  Please try using the 'Previous' button to verify all required information has been entered and try again.  " + str);
				CWLogger.Logger.Warn("TESTBOOK:Wizard:BookingFailed:pid={0}:lucid={1}:There was a problem - some information seems to be missing. ({2})", num.ToString(), selectedLucid.ToString(), eCreateAppointmentFailedReason.ToString());
				break;
			case ClockWorkWebAPI.Appointment.eCreateAppointmentFailedReason.StudentDoubleBooked:
				this.ShowEMessage("You already have another appointment or test scheduled at the same time as this test. Please contact us in order to book your test.");
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

		// Token: 0x060002B1 RID: 689 RVA: 0x00014424 File Offset: 0x00012624
		private void ShowEMessage(string emsg)
		{
			this.GetControl<Panel>(this.step_confirmAndComplete, "p_emsg").Visible = true;
			this.GetControl<Label>(this.step_confirmAndComplete, "lbl_emsg").Text = emsg;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00014458 File Offset: 0x00012658
		protected void btn_chooseAnotherDate_Click(object sender, EventArgs e)
		{
			this.GetControl<Panel>(this.step_classdatetime, "p_classDateandTime").Visible = true;
			this.GetControl<Panel>(this.step_classdatetime, "p_existingExams").Visible = false;
			this.GetControl<HiddenField>(this.step_classdatetime, "lbl_usingExistingClassDateTime").Value = "0";
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x000144B4 File Offset: 0x000126B4
		protected void btn_chooseExistingClassDateTime_Click(object sender, EventArgs e)
		{
			this.GetControl<Panel>(this.step_classdatetime, "p_classDateandTime").Visible = false;
			this.GetControl<Panel>(this.step_classdatetime, "p_existingExams").Visible = true;
			this.GetControl<HiddenField>(this.step_classdatetime, "lbl_usingExistingClassDateTime").Value = "1";
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00014510 File Offset: 0x00012710
		private string GetAccommodationsString(IEnumerable<TryToBookAccommodationToUseDTO> accommodations)
		{
			IEnumerable<string> source = from acc in accommodations
			select "• " + acc.Caption;
			return string.Join("\r\n", source.ToArray<string>());
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00014558 File Offset: 0x00012758
		private List<ClockWorkWebAPI.TestBooking.Accommodation> ConvertAccommodationsToOld(IEnumerable<TryToBookAccommodationToUseDTO> accs)
		{
			List<ClockWorkWebAPI.TestBooking.Accommodation> list = new List<ClockWorkWebAPI.TestBooking.Accommodation>();
			bool flag = accs == null;
			List<ClockWorkWebAPI.TestBooking.Accommodation> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				list.AddRange(from acc in accs
				select new ClockWorkWebAPI.TestBooking.Accommodation(acc.ControlId, acc.Caption, "", acc.Value, 0));
				result = list;
			}
			return result;
		}

		// Token: 0x040001D7 RID: 471
		private WizardStep step_additionalInfo = null;

		// Token: 0x040001D8 RID: 472
		protected ScriptManager bbb;

		// Token: 0x040001D9 RID: 473
		protected RadCodeBlock RadCodeBlock1;

		// Token: 0x040001DA RID: 474
		protected ValidationSummary ValidationSummary4;

		// Token: 0x040001DB RID: 475
		protected Wizard Wizard1;

		// Token: 0x040001DC RID: 476
		protected TemplatedWizardStep step_welcome;

		// Token: 0x040001DD RID: 477
		protected TemplatedWizardStep step_selectCourse;

		// Token: 0x040001DE RID: 478
		protected TemplatedWizardStep step_classdatetime;

		// Token: 0x040001DF RID: 479
		protected TemplatedWizardStep step_confirmProfInfo;

		// Token: 0x040001E0 RID: 480
		protected TemplatedWizardStep step_chooseAccommodations;

		// Token: 0x040001E1 RID: 481
		protected TemplatedWizardStep step_selectTime;

		// Token: 0x040001E2 RID: 482
		protected TemplatedWizardStep step_confirmAndComplete;

		// Token: 0x040001E3 RID: 483
		protected HiddenField hidden_bookingemailbody;

		// Token: 0x020001C2 RID: 450
		internal enum eMinMaxAllowedDatesToBookStatus
		{
			// Token: 0x0400096D RID: 2413
			Allowed,
			// Token: 0x0400096E RID: 2414
			NotAllowedBecauseAccommodationsExpiredBeforeToday,
			// Token: 0x0400096F RID: 2415
			NotAllowedBecauseAccommodationsExpiredBeforeCutoff
		}

		// Token: 0x020001C3 RID: 451
		internal class MinMaxAllowedDatesToBook
		{
			// Token: 0x170002C6 RID: 710
			// (get) Token: 0x06000C89 RID: 3209 RVA: 0x0004DF61 File Offset: 0x0004C161
			// (set) Token: 0x06000C8A RID: 3210 RVA: 0x0004DF69 File Offset: 0x0004C169
			public user_test_book.eMinMaxAllowedDatesToBookStatus Status { get; set; }

			// Token: 0x170002C7 RID: 711
			// (get) Token: 0x06000C8B RID: 3211 RVA: 0x0004DF72 File Offset: 0x0004C172
			// (set) Token: 0x06000C8C RID: 3212 RVA: 0x0004DF7A File Offset: 0x0004C17A
			public Range<DateTime> MinMaxRange { get; set; }
		}

		// Token: 0x020001C4 RID: 452
		internal class AccommodationItem
		{
			// Token: 0x170002C8 RID: 712
			// (get) Token: 0x06000C8E RID: 3214 RVA: 0x0004DF83 File Offset: 0x0004C183
			// (set) Token: 0x06000C8F RID: 3215 RVA: 0x0004DF8B File Offset: 0x0004C18B
			public string Name { get; set; }

			// Token: 0x170002C9 RID: 713
			// (get) Token: 0x06000C90 RID: 3216 RVA: 0x0004DF94 File Offset: 0x0004C194
			// (set) Token: 0x06000C91 RID: 3217 RVA: 0x0004DF9C File Offset: 0x0004C19C
			public string Value { get; set; }
		}

		// Token: 0x020001C5 RID: 453
		[Serializable]
		internal class PotentialBookingsForStudent
		{
			// Token: 0x170002CA RID: 714
			// (get) Token: 0x06000C93 RID: 3219 RVA: 0x0004DFA5 File Offset: 0x0004C1A5
			// (set) Token: 0x06000C94 RID: 3220 RVA: 0x0004DFAD File Offset: 0x0004C1AD
			public IList<user_test_book.PotentialBookingForStudent> Bookings { get; set; }

			// Token: 0x170002CB RID: 715
			// (get) Token: 0x06000C95 RID: 3221 RVA: 0x0004DFB6 File Offset: 0x0004C1B6
			// (set) Token: 0x06000C96 RID: 3222 RVA: 0x0004DFBE File Offset: 0x0004C1BE
			public string NoBookingsAvailableMessage { get; set; }

			// Token: 0x170002CC RID: 716
			// (get) Token: 0x06000C97 RID: 3223 RVA: 0x0004DFC7 File Offset: 0x0004C1C7
			// (set) Token: 0x06000C98 RID: 3224 RVA: 0x0004DFCF File Offset: 0x0004C1CF
			public IList<int> IconIdsToAdd { get; set; }

			// Token: 0x170002CD RID: 717
			// (get) Token: 0x06000C99 RID: 3225 RVA: 0x0004DFD8 File Offset: 0x0004C1D8
			// (set) Token: 0x06000C9A RID: 3226 RVA: 0x0004DFE0 File Offset: 0x0004C1E0
			public IList<int> EmailAccommodationControlIds { get; set; }

			// Token: 0x170002CE RID: 718
			// (get) Token: 0x06000C9B RID: 3227 RVA: 0x0004DFE9 File Offset: 0x0004C1E9
			// (set) Token: 0x06000C9C RID: 3228 RVA: 0x0004DFF1 File Offset: 0x0004C1F1
			public IList<string> GeneralNotices { get; set; }
		}

		// Token: 0x020001C6 RID: 454
		[Serializable]
		internal class PotentialBookingForStudent
		{
			// Token: 0x170002CF RID: 719
			// (get) Token: 0x06000C9E RID: 3230 RVA: 0x0004DFFA File Offset: 0x0004C1FA
			// (set) Token: 0x06000C9F RID: 3231 RVA: 0x0004E002 File Offset: 0x0004C202
			public int Id { get; set; }

			// Token: 0x170002D0 RID: 720
			// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x0004E00B File Offset: 0x0004C20B
			// (set) Token: 0x06000CA1 RID: 3233 RVA: 0x0004E013 File Offset: 0x0004C213
			public DateTime StartDateTime { get; set; }

			// Token: 0x170002D1 RID: 721
			// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x0004E01C File Offset: 0x0004C21C
			// (set) Token: 0x06000CA3 RID: 3235 RVA: 0x0004E024 File Offset: 0x0004C224
			public DateTime EndDateTime { get; set; }

			// Token: 0x170002D2 RID: 722
			// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x0004E02D File Offset: 0x0004C22D
			// (set) Token: 0x06000CA5 RID: 3237 RVA: 0x0004E035 File Offset: 0x0004C235
			public int RoomPersonId { get; set; }

			// Token: 0x170002D3 RID: 723
			// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x0004E03E File Offset: 0x0004C23E
			// (set) Token: 0x06000CA7 RID: 3239 RVA: 0x0004E046 File Offset: 0x0004C246
			public string RoomTitle { get; set; }

			// Token: 0x170002D4 RID: 724
			// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x0004E04F File Offset: 0x0004C24F
			// (set) Token: 0x06000CA9 RID: 3241 RVA: 0x0004E057 File Offset: 0x0004C257
			public bool OkToDoubleBook { get; set; }

			// Token: 0x170002D5 RID: 725
			// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0004E060 File Offset: 0x0004C260
			// (set) Token: 0x06000CAB RID: 3243 RVA: 0x0004E068 File Offset: 0x0004C268
			public int AppliedBreakMinutes { get; set; }
		}

		// Token: 0x020001C7 RID: 455
		internal enum eReasonNotAllowedToChooseCourse
		{
			// Token: 0x04000981 RID: 2433
			None,
			// Token: 0x04000982 RID: 2434
			LoaNotIssued,
			// Token: 0x04000983 RID: 2435
			NoTestExamAccommodations
		}
	}
}
