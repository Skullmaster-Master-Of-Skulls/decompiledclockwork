using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkController;
using ClockWorkLogger;
using ClockWorkWebAPI;
using ClockWorkWebAPI.ClockWorkAPIReplacement;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkWeb.ctrls.Instructor;
using TechnoPro.ClockWorkWeb.ctrls.Instructor.FinalExamUploadRequestsMode;
using TechnoPro.Common.ClientManager.Core.AppointmentsTestBooking;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.LookupCourses;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.MailMerging;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.MailMerging;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;
using TechnoPro.Common.UI.Web.Entity.Common.EventArgs;
using TechnoPro.Common.UI.Web.Entity.Common.FileUpload;
using TechnoPro.Common.UI.Web.Entity.Instructor.FinalExamRequest;
using TechnoPro.Common.UI.Web.Entity.Modules;

namespace TechnoPro.ClockWorkWeb.user.instructor
{
	// Token: 0x020000D5 RID: 213
	public class user_instructor_FinalExamUpload : Page
	{
		// Token: 0x06000648 RID: 1608 RVA: 0x0002FCF0 File Offset: 0x0002DEF0
		private static user_instructor_FinalExamUpload.ExistingExam LoadExamByLucid(int lucid, int preferredExamId)
		{
			bool flag = lucid < 1;
			user_instructor_FinalExamUpload.ExistingExam result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IExamRequestClientManager examRequestClientManager = new ExamRequestClientManager();
				IList<int> list2;
				IList<PersonBaseDTO> list = examRequestClientManager.LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequests(lucid, out list2);
				ISessionClientManager sessionClientManager = new SessionClientManager();
				SessionDTO currentSession = sessionClientManager.GetCurrentSession();
				DateTime startDate = currentSession.StartDate;
				DateTime endDate = currentSession.EndDate;
				IClassTestDefinitionClientManager classTestDefinitionClientManager = new ClassTestDefinitionClientManager();
				IList<ClassTestForExamRequestDTO> list3 = classTestDefinitionClientManager.LoadClassTestsForExamRequestByDateRange(lucid, startDate, endDate, eClassTestType.FinalExam);
				bool flag2 = list3.Count < 1;
				if (flag2)
				{
					result = null;
				}
				else
				{
					bool flag3 = list3.Count == 1;
					if (flag3)
					{
						result = new user_instructor_FinalExamUpload.ExistingExam(list3[0]);
					}
					else
					{
						ClassTestForExamRequestDTO classTestForExamRequestDTO = (preferredExamId > 0) ? list3.FirstOrDefault((ClassTestForExamRequestDTO g) => g.ExamId == preferredExamId) : null;
						bool flag4 = classTestForExamRequestDTO != null;
						if (flag4)
						{
							result = new user_instructor_FinalExamUpload.ExistingExam(classTestForExamRequestDTO);
						}
						else
						{
							CutoffTime cutoffForUpdatingTests = new WebSettingsClientManager().GetSettingValue<string>(Setting.INSTRUCTOR_CutoffForUpdatingTests).CutoffTimeFromXml() ?? CutoffTime.None;
							List<ClassTestForExamRequestDTO> list4 = list3.Where(delegate(ClassTestForExamRequestDTO g)
							{
								DateTime startDateTime = g.StartDateTime;
								bool? flag6 = cutoffForUpdatingTests.IsRightNowBeforeCutoffTime(startDateTime);
								return flag6 == null || flag6.Value;
							}).ToList<ClassTestForExamRequestDTO>();
							bool flag5 = list4.Count < 1;
							if (flag5)
							{
								result = null;
							}
							else
							{
								ClassTestForExamRequestDTO classTestForExamRequestDTO2 = list4.FirstOrDefault((ClassTestForExamRequestDTO g) => g.ExamRequestInstructorChoices.Trim().Length > 0) ?? list3[0];
								result = ((classTestForExamRequestDTO2 == null) ? null : new user_instructor_FinalExamUpload.ExistingExam(classTestForExamRequestDTO2));
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0002FE78 File Offset: 0x0002E078
		private void Page_Init(object sender, EventArgs e)
		{
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.INSTRUCTOR_InstructorConfirm_ShowStudentList);
			bool flag = !settingValue;
			if (flag)
			{
				this.step_students.Title = " ";
			}
			this.ctrlFinalExamGrid1.Init(0, 0);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0002FEC0 File Offset: 0x0002E0C0
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetInstructorId(this.Page);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0002FEE4 File Offset: 0x0002E0E4
		private int GetAltContactId()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetAltContactId(this.Page);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0002FF06 File Offset: 0x0002E106
		protected void ctrlInstructorExamUpload1_OnExamIdRequired(object sender, NumberEventArgs e)
		{
			e.Number = this.GetEditingExamId();
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0002FF18 File Offset: 0x0002E118
		protected void Page_Load(object sender, EventArgs e)
		{
			NavigatorClientManager.CurrentInstance.EnsurePageNotCached();
			WebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			bool flag = webSettingsClientManager.GetSettingValue<bool>(Setting.EXAMBOOKING_FinalExamRequest_Enabled);
			bool flag2 = flag;
			if (flag2)
			{
				bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.INSTRUCTOR_DisableExamRequestInterfaceForInstructors);
				bool flag3 = settingValue;
				if (flag3)
				{
					flag = false;
				}
			}
			bool flag4 = !flag;
			if (flag4)
			{
				base.Response.Redirect("courses.aspx", true);
			}
			else
			{
				int pid = this.GetPid();
				int altContactId = this.GetAltContactId();
				bool flag5 = pid < 1 && altContactId < 1;
				if (flag5)
				{
					NavigatorClientManager.CurrentInstance.NotAllowed(Setting.INSTRUCTOR_ErrorMessage_NotRegistered, this.Page);
				}
				else
				{
					int lucid = this.GetLucidFromUrl();
					int num = this.GetEditingExamId();
					bool flag6 = lucid < 1 && num < 1;
					if (flag6)
					{
						string url = string.Format("FinalExamUploadChooseCourse.aspx?lucid={0}&examid={1}", NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(lucid), NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(num));
						base.Response.Redirect(url, true);
					}
					else
					{
						bool flag7 = !this.Page.IsPostBack;
						if (flag7)
						{
							user_instructor_FinalExamUpload.ExistingExam existingExam = user_instructor_FinalExamUpload.LoadExamByLucid(lucid, num);
							bool flag8 = existingExam != null;
							if (flag8)
							{
								num = existingExam.ExamId;
							}
							WebSettingsClientManager webSettingsClientManager2 = new WebSettingsClientManager();
							ISessionClientManager sessionClientManager = new SessionClientManager();
							SessionDTO currentSession = sessionClientManager.GetCurrentSession();
							ILookupInstructorClientManager lookupInstructorClientManager = new LookupInstructorClientManager();
							IList<LookupCourseDTO> source = lookupInstructorClientManager.LoadCoursesByInstructor(pid, altContactId, currentSession.StartDate, currentSession.StartDate.AddYears(1), 2);
							LookupCourseDTO lookupCourseDTO = source.FirstOrDefault((LookupCourseDTO g) => g.LuCourseId == lucid);
							bool flag9 = lookupCourseDTO == null;
							if (flag9)
							{
								base.Response.Redirect("FinalExamUploadChooseCourse.aspx", true);
								return;
							}
							this.lbl_readonlycourse.Text = lookupCourseDTO.GetCourseDescription();
							this.ShowMessage();
							string settingValue2 = webSettingsClientManager2.GetSettingValue<string>(Setting.INSTRUCTOR_SubmitExamPageIntro);
							bool flag10 = !string.IsNullOrEmpty(settingValue2);
							if (flag10)
							{
								this.lbl_intro.Text = settingValue2;
							}
							else
							{
								this.p_intro.Visible = false;
							}
							bool settingValue3 = webSettingsClientManager2.GetSettingValue<bool>(Setting.INSTRUCTOR_HideAddTestOption);
							DateTime dateTime = DateTime.MinValue;
							bool flag11 = num > 0;
							if (flag11)
							{
								IClassTestDefinitionClientManager classTestDefinitionClientManager = new ClassTestDefinitionClientManager();
								ClassTestForExamRequestDTO classTestForExamRequestDTO = classTestDefinitionClientManager.LoadClassTestForExamRequestById(num);
								bool flag12 = classTestForExamRequestDTO != null;
								if (flag12)
								{
									dateTime = classTestForExamRequestDTO.StartDateTime.Date;
									double totalMinutes = (classTestForExamRequestDTO.EndDateTime - classTestForExamRequestDTO.StartDateTime).TotalMinutes;
									int num2 = (int)(totalMinutes / 60.0);
									double num3 = totalMinutes - (double)(num2 * 60);
									this.cmb_dur_hours.Text = num2.ToString();
									this.txt_dur_minutes.Text = num3.ToString();
									string text = classTestForExamRequestDTO.ExamRequestInstructorChoices ?? "";
									text = text.Replace("<br /><br />", "`");
									text = text.Replace("<br />", "");
									string text2 = HttpUtility.HtmlEncode(text);
									string[] array = text2.Split(new char[]
									{
										'`'
									});
									string dateString = (array.Length != 0) ? array[0] : "";
									string dateString2 = (array.Length > 1) ? array[1] : "";
									string dateString3 = (array.Length > 2) ? array[2] : "";
									this.ctrlFinalExamGrid1.SelectDate(dateString, 1);
									this.ctrlFinalExamGrid1.SelectDate(dateString2, 2);
									this.ctrlFinalExamGrid1.SelectDate(dateString3, 3);
									this.cmb_dur_hours.Enabled = false;
									this.txt_dur_minutes.Enabled = false;
									this.lbl_dur.Text = "Your previously selected times are listed below.  If you require a change please contact us.";
								}
							}
							string settingValue4 = webSettingsClientManager2.GetSettingValue<string>(Setting.INSTRUCTOR_Tests_InstructionsForStudentsList);
							this.lbl_instructions.Text = settingValue4;
							this.ctrlInstructorExamConfirmExamDetails1.SetSubmitReminderText(webSettingsClientManager2.GetSettingValue<string>(Setting.INSTRUCTOR_Tests_FinalExamSubmitPageFinalNote));
							bool flag13 = dateTime != DateTime.MinValue;
							if (flag13)
							{
								CutoffTime cutoffTime = webSettingsClientManager2.GetSettingValue<string>(Setting.INSTRUCTOR_CutoffForUpdatingTests).CutoffTimeFromXml() ?? CutoffTime.None;
								bool? flag14 = cutoffTime.IsRightNowBeforeCutoffTime(dateTime);
								bool flag15 = flag14 == null || flag14.Value;
								bool flag16 = !flag15;
								if (flag16)
								{
									base.Response.Redirect(string.Format("UploadedExams.aspx?lucid={0}&reason=2", NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(lucid)), true);
								}
								else
								{
									string settingValue5 = webSettingsClientManager2.GetSettingValue<string>(Setting.INSTRUCTOR_CutoffForUpdatingTestDateTime);
									CutoffTime cutoffTime2 = settingValue5.CutoffTimeFromXml() ?? CutoffTime.None;
								}
							}
						}
						this.Page.Form.Attributes.Add("enctype", "multipart/form-data");
					}
				}
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x000303DC File Offset: 0x0002E5DC
		private int GetLucidFromUrl()
		{
			return NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["lucid"] ?? "");
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00030416 File Offset: 0x0002E616
		private void ShowMessage(string msg)
		{
			this.lbl_topmsg.Text = msg;
			this.p_topmsg.Visible = true;
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00030434 File Offset: 0x0002E634
		private void ShowMessage()
		{
			object obj = this.Session["msgcode"];
			bool flag = obj == null;
			if (!flag)
			{
				string text = (string)obj;
				object obj2 = this.Session["msgcodedesc"];
				string str = (obj2 == null) ? "" : ((string)obj2);
				string a = text;
				if (!(a == "fileproblem"))
				{
					if (!(a == "uploaderror"))
					{
						if (!(a == "invalidfiletype"))
						{
							if (a == "invalidduration")
							{
								this.lbl_topmsg.Text = "Please enter a valid class test duration in order to continue.";
								this.p_topmsg.Visible = true;
							}
						}
						else
						{
							this.lbl_topmsg.Text = "The file that you have specified to submit is not an accepted type of file. " + str;
							this.p_topmsg.Visible = true;
							this.ctrlInstructorExamUpload1.Focus();
						}
					}
					else
					{
						this.lbl_topmsg.Text = "There was a database error and your test may not have been uploaded correctly. " + str;
						this.p_topmsg.Visible = true;
					}
				}
				else
				{
					this.lbl_topmsg.Text = "There was a problem with the file(s) you are trying to upload: " + str;
					this.p_topmsg.Visible = true;
					this.ctrlInstructorExamUpload1.Focus();
				}
				this.Session["msgcode"] = null;
				this.Session["msgcodedesc"] = null;
			}
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00003E0A File Offset: 0x0000200A
		[WebMethod]
		public static void RemoveFile(int fileForUploadId, string guid)
		{
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0003059C File Offset: 0x0002E79C
		[WebMethod]
		public static FileForUploadSet GetPendingFileInfosForUpload(string guid)
		{
			return null;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x000305B0 File Offset: 0x0002E7B0
		private DataTable LoadStudentsRequestedThisTest()
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			int selectedLucid = this.GetSelectedLucid();
			bool flag = selectedLucid > 0;
			DataTable dataTable;
			if (flag)
			{
				DateTime dateTime;
				DateTime dateTime2;
				ClockWorkWebAPI.Core.GetTermStartEndDates(out dateTime, out dateTime2);
				DbParameter[] parameters = new DbParameter[]
				{
					clockWork.GetParameter("@lucid", DbType.Int32, selectedLucid),
					clockWork.GetParameter("@sdate", DbType.DateTime, dateTime),
					clockWork.GetParameter("@edate", DbType.DateTime, dateTime2)
				};
				dataTable = clockWork.ExecuteQuery("SELECT DISTINCT er.personid,p.firstname,p.middlename,p.lastname,p.student_no,er.dateentered\r\nFROM examrequest er LEFT JOIN people p ON p.personid=er.personid\r\nWHERE   er.lucourseid=@lucid AND er.dateentered >= @sdate AND er.dateentered<=@edate AND p.isactive=1", parameters);
				dataTable = encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
				{
					"firstname",
					"middlename",
					"lastname",
					"student_no"
				});
				DataTable dataTable2 = dataTable.Clone();
				List<int> list = new List<int>();
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					int item = (int)dataRow["personid"];
					bool flag2 = list.Contains(item);
					if (!flag2)
					{
						list.Add(item);
						dataTable2.ImportRow(dataRow);
					}
				}
				dataTable = dataTable2;
			}
			else
			{
				dataTable = new DataTable();
				dataTable.Columns.Add("firstname");
				dataTable.Columns.Add("middlename");
				dataTable.Columns.Add("lastname");
				dataTable.Columns.Add("student_no");
				dataTable.Columns.Add("dateentered", typeof(DateTime));
			}
			return dataTable;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00030780 File Offset: 0x0002E980
		private void CourseChanged(int newLucid)
		{
			this.lastSelectedLucid.Value = newLucid.ToString();
			this.ctrlInstructorExamStudentList1.ReloadStudentList(newLucid);
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x000307A4 File Offset: 0x0002E9A4
		private int GetLastSelectedLucid()
		{
			string value = this.lastSelectedLucid.Value;
			int num;
			bool flag = value.Length <= 0 || !int.TryParse(value, out num);
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = num;
			}
			return result;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x000307E4 File Offset: 0x0002E9E4
		private int GetSelectedLucid()
		{
			return this.GetLucidFromUrl();
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x000307FC File Offset: 0x0002E9FC
		protected void Wizard1_ActiveStepChanged(object sender, EventArgs e)
		{
			int selectedLucid = this.GetSelectedLucid();
			int num = (selectedLucid > 0) ? this.GetLastSelectedLucid() : 0;
			bool flag = selectedLucid > 0 && num != selectedLucid;
			if (flag)
			{
				this.CourseChanged(selectedLucid);
			}
			bool flag2 = this.Wizard1.ActiveStep == this.step_submit;
			if (flag2)
			{
				List<FinalExamDay> selectedExamDates = this.GetSelectedExamDates();
				string myGuid = this.ctrlInstructorExamUpload1.GetMyGuid();
				IFileUploadWebClientManager fileUploadWebClientManager = new FileUploadWebClientManager();
				FileForUploadSet fileForUploadInfoFromSession = fileUploadWebClientManager.GetFileForUploadInfoFromSession(myGuid);
				int editingExamId = this.GetEditingExamId();
				int pid = this.GetPid();
				int altContactId = this.GetAltContactId();
				IEnumerable<ExamFileDTO> previousFiles = this.ctrlInstructorExamUpload1.LoadPreviousUploads(pid, altContactId, editingExamId);
				this.ctrlInstructorExamConfirmExamDetails1.UpdateDisplay(this.lbl_readonlycourse.Text, selectedExamDates, this.GetTestDurationMinutes(), this.ctrlInstructorTestExamDynamicFormData1.GetDataSummaryForDisplay(), fileForUploadInfoFromSession, previousFiles);
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x000308D8 File Offset: 0x0002EAD8
		protected void cusCustom_ServerValidate(object sender, ServerValidateEventArgs e)
		{
			List<FinalExamDay> selectedExamDates = this.GetSelectedExamDates();
			e.IsValid = (selectedExamDates.Count > 0);
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x00030900 File Offset: 0x0002EB00
		private DateTime DateOfTest
		{
			get
			{
				List<FinalExamDay> selectedExamDates = this.GetSelectedExamDates();
				bool flag = selectedExamDates.Count <= 0;
				DateTime result;
				if (flag)
				{
					result = DateTime.MinValue;
				}
				else
				{
					FinalExamDay finalExamDay = selectedExamDates[0];
					result = finalExamDay.Date;
				}
				return result;
			}
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00030940 File Offset: 0x0002EB40
		private int GetEditingExamId()
		{
			return NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["examid"]);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00030974 File Offset: 0x0002EB74
		private static Exception UpdateExistingExam(DatabaseLayer db, int lucid, int examid, int iid, int altContactId, DateTime dateOfTest, int testDuration, string description)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				db.GetParameter("@description", DbType.String, description),
				db.GetParameter("@dateoftest", DbType.DateTime, dateOfTest),
				db.GetParameter("@iid", DbType.Int32, (iid > 0) ? iid : altContactId),
				db.GetParameter("@testduration", DbType.Int32, testDuration),
				db.GetParameter("@examid", DbType.Int32, examid),
				db.GetParameter("@lucid", DbType.Int32, lucid),
				db.GetParameter("@instructoracknowledged", DbType.StringFixedLength, ' ')
			};
			try
			{
				db.ExecuteNonQuery("UPDATE exams SET \r\nlucourseid=@lucid,lastmodified=getdate(),description=@description,dateoftest=@dateoftest,wholastmodified=@iid,testduration=@testduration,instructoracknowledged=@instructoracknowledged\r\nWHERE examid=@examid", parameters);
				CWLogger.Logger.Info("Instructor:ExamUpload:UpdateExamSuccess:iid={0}:description={1}:dateoftest={2}:testduration={3}:examid={4}:lucid={5}", new object[]
				{
					iid.ToString(),
					description ?? "NULL",
					dateOfTest.ToString("yyyy-MM-dd"),
					testDuration.ToString(),
					examid.ToString(),
					lucid.ToString()
				});
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("Instructor:ExamUpload:UpdateExamSuccess:iid={0}:description={1}:dateoftest={2}:testduration={3}:examid={4}:lucid={5}:error={6}", new object[]
				{
					iid.ToString(),
					description ?? "NULL",
					dateOfTest.ToString("yyyy-MM-dd"),
					testDuration.ToString(),
					examid.ToString(),
					lucid.ToString(),
					ex.ToString()
				});
				return ex;
			}
			return null;
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00030B14 File Offset: 0x0002ED14
		private int GetTestDurationMinutes()
		{
			int num;
			bool flag = !int.TryParse(this.txt_dur_minutes.Text, out num);
			if (flag)
			{
				num = 0;
			}
			int num2;
			bool flag2 = int.TryParse(this.cmb_dur_hours.SelectedValue, out num2);
			if (flag2)
			{
				num += num2 * 60;
			}
			return num;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00030B64 File Offset: 0x0002ED64
		protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
		{
			CWLogger.Logger.Debug("Instructor:ExamUpload:Submit:upload_examcount={0}:allowedfiletypes={1}", this.ctrlInstructorExamUpload1.UploadedFilesCount.ToString(), string.Join(",", this.ctrlInstructorExamUpload1.AllowedFileTypes.ToArray()));
			DatabaseLayer instance = DatabaseLayer.GetInstance();
			int pid = this.GetPid();
			int altContactId = this.GetAltContactId();
			int num = this.GetEditingExamId();
			string text = null;
			bool flag = text != null;
			if (flag)
			{
				this.Session["msgcode"] = "fileproblem";
				this.Session["msgcodedesc"] = text;
				CWLogger.Logger.Warn("Instructor:ExamUpload:FileProblem:iid={0}:fileproblem={1}", pid.ToString(), text.ToString());
				this.ShowMessage();
			}
			else
			{
				DateTime dateOfTest = this.DateOfTest;
				bool flag2 = dateOfTest == DateTime.MinValue;
				if (flag2)
				{
					this.Session["msgcode"] = "invalid test date";
					this.Session["msgcodedesc"] = "";
					CWLogger.Logger.Warn("Instructor:ExamUpload:InvalidTestDate:iid={0}:txt_dateoftest.selecteddate.value={1}", pid.ToString(), dateOfTest.ToString());
					this.ShowMessage();
				}
				else
				{
					int testDurationMinutes = this.GetTestDurationMinutes();
					bool flag3 = testDurationMinutes <= 0;
					if (flag3)
					{
						this.Session["msgcode"] = "invalidduration";
						this.Session["msgcodedesc"] = "";
						CWLogger.Logger.Warn("Instructor:ExamUpload:InvalidDuration:iid={0}:testduration={1}", pid.ToString(), testDurationMinutes.ToString());
						this.ShowMessage();
					}
					else
					{
						bool flag4 = pid < 1 && altContactId < 1;
						if (flag4)
						{
							this.Session["msgcode"] = "Session timed out.";
							this.Session["msgcodedesc"] = "Your session has timed out.  You will have to login and try again.";
							CWLogger.Logger.Warn("Instructor:ExamUpload:SessionTimedOut");
							this.ShowMessage();
						}
						else
						{
							int selectedLucid = this.GetSelectedLucid();
							bool flag5 = selectedLucid <= 0;
							if (flag5)
							{
								this.Session["msgcode"] = "missingcourse";
								this.Session["msgcodedesc"] = "";
								CWLogger.Logger.Warn("Instructor:ExamUpload:MissingCourse:iid={0}", pid.ToString());
								this.ShowMessage();
							}
							else
							{
								List<FinalExamDay> selectedExamDates = this.GetSelectedExamDates();
								string description = string.Join("<br />", selectedExamDates.ConvertAll<string>((FinalExamDay fed) => string.Format("{0}. {1} {2} . {3}<br />", new object[]
								{
									fed.Level.ToString(),
									fed.Date.ToString("ddd").ToUpper(),
									fed.Date.ToString("MMM d"),
									fed.Date.ToString("h:mm tt").ToUpper()
								})).ToArray());
								bool flag6 = num > 0;
								int num2;
								Exception ex;
								if (flag6)
								{
									num2 = num;
									ex = user_instructor_FinalExamUpload.UpdateExistingExam(instance, selectedLucid, num, pid, altContactId, dateOfTest, testDurationMinutes, description);
								}
								else
								{
									ex = ClockWorkWebAPI.Course.UploadExam(pid, selectedLucid, description, dateOfTest, testDurationMinutes, "F", out num2);
									num = num2;
									bool flag7 = ex == null && num > 0;
									if (!flag7)
									{
										CWLogger.Logger.Error("Instructor:ExamUpload:newExamFail:iid={0}:description={1}:dateoftest={2}:testduration={3}:examid={4}:lucid={5}:error={6}", new object[]
										{
											pid.ToString(),
											"",
											dateOfTest.ToString("yyyy-MM-dd"),
											testDurationMinutes.ToString(),
											num.ToString(),
											selectedLucid.ToString(),
											((ex != null) ? ex.ToString() : null) ?? "NULL"
										});
										this.ShowMessage("An unknown problem has occurred.  We are sorry for the incovenience.  Please contact us for assistance.");
										return;
									}
									CWLogger.Logger.Info("Instructor:ExamUpload:newExamSuccess:iid={0}:description={1}:dateoftest={2}:testduration={3}:examid={4}:lucid={5}", new object[]
									{
										pid.ToString(),
										"",
										dateOfTest.ToString("yyyy-MM-dd"),
										testDurationMinutes.ToString(),
										num.ToString(),
										selectedLucid.ToString()
									});
								}
								bool flag8 = ex != null || num2 <= 0;
								if (flag8)
								{
									this.Session["msgcode"] = "uploaderror";
									this.Session["msgcodedesc"] = "There was an upload error.";
									CWLogger.Logger.Error("Instructor:ExamUpload:UploadError:iid={0}:error={1}", pid.ToString(), ex.ToString());
									this.ShowMessage();
								}
								else
								{
									this.ctrlInstructorExamUpload1.UploadFiles(num2, pid);
									this.ctrlInstructorTestExamDynamicFormData1.SaveDynamicData(pid, num);
									string studentsListString = this.GetStudentsListString();
									string courseDescription = "";
									user_instructor_FinalExamUpload.SendEmail(selectedLucid, pid, altContactId, courseDescription, description, studentsListString);
									CWLogger.Logger.Info("Instructor:ExamUpload:SuccessfulSubmit:iid={0}", pid.ToString());
									base.Response.Redirect(string.Format("ExamUploadComplete2.aspx?lucid={0}", NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(selectedLucid)));
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0003101C File Offset: 0x0002F21C
		private static void SendEmail(int lucid, int iid, int altContactId, string courseDescription, string description, string studentsString)
		{
			string value = "";
			string value2 = "";
			string value3 = "";
			bool flag = iid > 0;
			if (flag)
			{
				ClockWorkController.Instructor instructor = ClockWorkController.Instructor.LoadInstructor(iid);
				bool flag2 = instructor != null;
				if (flag2)
				{
					value = instructor.InstructorName;
					value2 = instructor.InstructorEmail;
					value3 = instructor.InstructorPhone;
				}
			}
			else
			{
				bool flag3 = altContactId > 0;
				if (flag3)
				{
					CourseContactInformation courseContactInformation = ClockWorkController.Instructor.LoadAlternateContact(altContactId);
					bool flag4 = courseContactInformation != null;
					if (flag4)
					{
						value = courseContactInformation.Name;
						value2 = courseContactInformation.Email;
						value3 = courseContactInformation.Phone;
					}
				}
			}
			StringDictionary stringDictionary = new StringDictionary
			{
				{
					"coursedescription",
					courseDescription ?? ""
				},
				{
					"instructorname",
					value
				},
				{
					"instructoremail",
					value2
				},
				{
					"instructorphone",
					value3
				},
				{
					"students",
					studentsString
				},
				{
					"date",
					DateTime.Now.ToString("MMMM d, yyyy")
				},
				{
					"time",
					DateTime.Now.ToString("h:mm tt")
				},
				{
					"description",
					description ?? ""
				}
			};
			IMailMergeCodes mailMergeCodes = new MailMergeCodes();
			stringDictionary.Add("from", mailMergeCodes.GetDefaultFromAddress(eWebModule.InstructorTestsExams));
			stringDictionary.Add("signature", mailMergeCodes.GetDefaultSignature(eWebModule.InstructorTestsExams));
			IEmailClientManager emailClientManager = new EmailClientManager();
			MailMergeContextDTO mailMergeContext = new MailMergeContextDTO
			{
				LuCourseId = lucid,
				InstructorId = iid
			};
			emailClientManager.SendEmail(Setting.INSTRUCTOR_Tests_EmailOnExamUpdate, mailMergeContext, stringDictionary, "InstructorFinalExamUpload");
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x000311B8 File Offset: 0x0002F3B8
		private string GetStudentsListString()
		{
			DataTable dataTable = this.LoadStudentsRequestedThisTest();
			bool flag = dataTable == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					bool flag2 = i > 0;
					if (flag2)
					{
						stringBuilder.Append("\n");
					}
					DataRow dataRow = dataTable.Rows[i];
					stringBuilder.AppendFormat("{0} {1} ({2})", dataRow["firstname"].ToString(), dataRow["lastname"].ToString(), dataRow["student_no"].ToString());
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0003127C File Offset: 0x0002F47C
		private List<FinalExamDay> GetSelectedExamDates()
		{
			return this.ctrlFinalExamGrid1.GetSelectedExamDates();
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00031299 File Offset: 0x0002F499
		protected void ctrlInstructorExamStudentList1_OnOnLuCourseIdRequired(object sender, NumberEventArgs e)
		{
			e.Number = this.GetLucidFromUrl();
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00031299 File Offset: 0x0002F499
		protected void ctrlInstructorExamConfirmExamDetails1_OnOnLuCourseIdRequired(object sender, NumberEventArgs e)
		{
			e.Number = this.GetLucidFromUrl();
		}

		// Token: 0x040004B5 RID: 1205
		protected ScriptManager bbb;

		// Token: 0x040004B6 RID: 1206
		protected Panel p_topmsg;

		// Token: 0x040004B7 RID: 1207
		protected Image img_topmsg;

		// Token: 0x040004B8 RID: 1208
		protected Label lbl_topmsg;

		// Token: 0x040004B9 RID: 1209
		protected Wizard Wizard1;

		// Token: 0x040004BA RID: 1210
		protected WizardStep step_details;

		// Token: 0x040004BB RID: 1211
		protected Label lbl_Title;

		// Token: 0x040004BC RID: 1212
		protected Panel p_intro;

		// Token: 0x040004BD RID: 1213
		protected Label lbl_intro;

		// Token: 0x040004BE RID: 1214
		protected Panel p_coursedetails;

		// Token: 0x040004BF RID: 1215
		protected Label lbl_course;

		// Token: 0x040004C0 RID: 1216
		protected Label lbl_readonlycourse;

		// Token: 0x040004C1 RID: 1217
		protected HiddenField lastSelectedLucid;

		// Token: 0x040004C2 RID: 1218
		protected LinkButton btn_chooseADifferentCourse;

		// Token: 0x040004C3 RID: 1219
		protected CtrlInstructorFinalExamDateTimeGrid ctrlFinalExamGrid1;

		// Token: 0x040004C4 RID: 1220
		protected Panel p_dur;

		// Token: 0x040004C5 RID: 1221
		protected Label lbl_dur;

		// Token: 0x040004C6 RID: 1222
		protected DropDownList cmb_dur_hours;

		// Token: 0x040004C7 RID: 1223
		protected TextBox txt_dur_minutes;

		// Token: 0x040004C8 RID: 1224
		protected CustomValidator cusCustom;

		// Token: 0x040004C9 RID: 1225
		protected RequiredFieldValidator RequiredFieldValidator1;

		// Token: 0x040004CA RID: 1226
		protected RequiredFieldValidator RequiredFieldValidator2;

		// Token: 0x040004CB RID: 1227
		protected WizardStep step_students;

		// Token: 0x040004CC RID: 1228
		protected Panel p_instructions;

		// Token: 0x040004CD RID: 1229
		protected Label lbl_instructions;

		// Token: 0x040004CE RID: 1230
		protected CtrlInstructorExamStudentList ctrlInstructorExamStudentList1;

		// Token: 0x040004CF RID: 1231
		protected WizardStep step_info;

		// Token: 0x040004D0 RID: 1232
		protected CtrlInstructorTestExamDynamicFormData ctrlInstructorTestExamDynamicFormData1;

		// Token: 0x040004D1 RID: 1233
		protected WizardStep step_upload;

		// Token: 0x040004D2 RID: 1234
		protected Label Label1;

		// Token: 0x040004D3 RID: 1235
		protected CtrlInstructorExamUpload ctrlInstructorExamUpload1;

		// Token: 0x040004D4 RID: 1236
		protected WizardStep step_submit;

		// Token: 0x040004D5 RID: 1237
		protected Label Label5;

		// Token: 0x040004D6 RID: 1238
		protected CtrlInstructorExamConfirmExamDetails ctrlInstructorExamConfirmExamDetails1;

		// Token: 0x02000204 RID: 516
		public class ExistingExam
		{
			// Token: 0x06000DC2 RID: 3522 RVA: 0x0000AF9E File Offset: 0x0000919E
			public ExistingExam()
			{
			}

			// Token: 0x06000DC3 RID: 3523 RVA: 0x0004F5DC File Offset: 0x0004D7DC
			public ExistingExam(ClassTestForExamRequestDTO classTestForExamRequest)
			{
				bool flag = classTestForExamRequest == null;
				if (!flag)
				{
					this.ExamRequestInstructorChoices = classTestForExamRequest.ExamRequestInstructorChoices;
					this.LuCourseId = ((classTestForExamRequest.Course == null) ? 0 : classTestForExamRequest.Course.LuCourseId);
					this.ExamId = classTestForExamRequest.ExamId;
					this.DateOfTest = classTestForExamRequest.StartDateTime.Date;
					this.Title = string.Join(": ", (from g in new string[]
					{
						this.DateOfTest.ToString("MMM d, yyyy"),
						(this.ExamRequestInstructorChoices ?? "").Trim()
					}
					where g.Length > 0
					select g).ToArray<string>());
				}
			}

			// Token: 0x1700030B RID: 779
			// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x0004F6B7 File Offset: 0x0004D8B7
			// (set) Token: 0x06000DC5 RID: 3525 RVA: 0x0004F6BF File Offset: 0x0004D8BF
			public int ExamId { get; set; }

			// Token: 0x1700030C RID: 780
			// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x0004F6C8 File Offset: 0x0004D8C8
			// (set) Token: 0x06000DC7 RID: 3527 RVA: 0x0004F6D0 File Offset: 0x0004D8D0
			public int LuCourseId { get; set; }

			// Token: 0x1700030D RID: 781
			// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x0004F6D9 File Offset: 0x0004D8D9
			// (set) Token: 0x06000DC9 RID: 3529 RVA: 0x0004F6E1 File Offset: 0x0004D8E1
			public DateTime DateOfTest { get; set; }

			// Token: 0x1700030E RID: 782
			// (get) Token: 0x06000DCA RID: 3530 RVA: 0x0004F6EA File Offset: 0x0004D8EA
			// (set) Token: 0x06000DCB RID: 3531 RVA: 0x0004F6F2 File Offset: 0x0004D8F2
			public string ExamRequestInstructorChoices { get; set; }

			// Token: 0x1700030F RID: 783
			// (get) Token: 0x06000DCC RID: 3532 RVA: 0x0004F6FB File Offset: 0x0004D8FB
			// (set) Token: 0x06000DCD RID: 3533 RVA: 0x0004F703 File Offset: 0x0004D903
			public string Title { get; set; }
		}
	}
}
