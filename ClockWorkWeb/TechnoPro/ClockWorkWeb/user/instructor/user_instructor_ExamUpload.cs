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
using ClockWorkWebAPI.ClockWorkAPIReplacement;
using ClockWorkWebAPIWeb;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkWeb.ctrls.Instructor;
using TechnoPro.Common.ClientManager.Core.AppointmentsTestBooking;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.LookupCourses;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.Templates;
using TechnoPro.Common.Public.Entities.TPMailMan;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Email;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.MailMerging;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.MailMerging;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;
using TechnoPro.Common.UI.Web.Entity.Modules;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.instructor
{
	// Token: 0x020000D1 RID: 209
	public class user_instructor_ExamUpload : Page
	{
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x0002C7C4 File Offset: 0x0002A9C4
		private bool NotAllowedToUploadTestsExams
		{
			get
			{
				bool flag = this.notAllowedToUploadTestsExams == null;
				if (flag)
				{
					IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
					this.notAllowedToUploadTestsExams = new bool?(webSettingsClientManager.GetSettingValue<bool>(Setting.INSTRUCTOR_DontAllowInstructorToUploadTestsExams));
				}
				return this.notAllowedToUploadTestsExams.Value;
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0002C814 File Offset: 0x0002AA14
		private string[] GetAllowedFileTypes()
		{
			return (from g in new WebSettingsClientManager().GetSettingValue<string>(Setting.INSTRUCTOR_allowedfiletypes).Split(new char[]
			{
				','
			})
			select g.Trim() into h
			where h.Length > 0
			select h).ToArray<string>();
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x0002C894 File Offset: 0x0002AA94
		private IList<string> AllowedFileTypes
		{
			get
			{
				return (from g in new WebSettingsClientManager().GetSettingValue<string>(Setting.INSTRUCTOR_allowedfiletypes).Split(new char[]
				{
					','
				})
				select g.Trim() into h
				where h.Length > 0
				select h).ToList<string>();
			}
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0002C914 File Offset: 0x0002AB14
		public string GetAllowedFileTypesForJavascript()
		{
			string[] value = (from g in this.GetAllowedFileTypes()
			select "'" + g.Substring(1) + "'").ToArray<string>();
			return "[" + string.Join(",", value) + "]";
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void rbtns_approve_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0002C970 File Offset: 0x0002AB70
		private void Page_Init(object sender, EventArgs e)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.INSTRUCTOR_InstructorConfirm_ShowStudentList);
			bool flag = !settingValue;
			if (flag)
			{
				this.step_students.Title = " ";
			}
			DynamicControlLayoutHelper dynamicControlLayoutHelper = null;
			int settingValue2 = webSettingsClientManager.GetSettingValue<int>(Setting.INSTRUCTOR_uploadscreennum);
			string settingValue3 = webSettingsClientManager.GetSettingValue<string>(Setting.INSTRUCTOR_uploadScreenExemptControlIds);
			DynamicScreenLayout.ControlsToScreen(ref dynamicControlLayoutHelper, base.Cache, settingValue2, this.p_data, null, false, false, settingValue3);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0002C9E0 File Offset: 0x0002ABE0
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetInstructorId(this.Page);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0002CA04 File Offset: 0x0002AC04
		private int GetAltContactId()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetAltContactId(this.Page);
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0002CA28 File Offset: 0x0002AC28
		private HttpPostedFile GetFileToUpload()
		{
			HttpFileCollection files = base.Request.Files;
			List<HttpPostedFile> list = new List<HttpPostedFile>();
			string[] allowedFileTypes = this.GetAllowedFileTypes();
			for (int i = 0; i < files.Count; i++)
			{
				HttpPostedFile httpPostedFile = files[i];
				bool flag = httpPostedFile.ContentLength > 0;
				if (flag)
				{
					string text = (httpPostedFile.FileName ?? "").Trim();
					int num = text.LastIndexOf(".");
					string text2 = (num > 0) ? text.Substring(num) : "";
					bool flag2 = text2.Length > 0 && allowedFileTypes.Contains(text2);
					if (flag2)
					{
						list.Add(httpPostedFile);
					}
				}
			}
			return list.FirstOrDefault<HttpPostedFile>();
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0002CAF8 File Offset: 0x0002ACF8
		private void UploadCurrentFileAndRememberItForLater()
		{
			HttpPostedFile fileToUpload = this.GetFileToUpload();
			bool flag = fileToUpload == null;
			if (!flag)
			{
				this.UploadFileAndRememberItForLater(fileToUpload);
			}
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0002CB20 File Offset: 0x0002AD20
		private void UploadFileAndRememberItForLater(HttpPostedFile uploadedFile)
		{
			bool flag = uploadedFile != null;
			if (flag)
			{
				object obj = this.Session["currentExamFileId"];
				int num = (obj is int) ? ((int)obj) : 0;
				byte[] array = new byte[uploadedFile.InputStream.Length];
				uploadedFile.InputStream.Read(array, 0, Convert.ToInt32(uploadedFile.InputStream.Length));
				string fileName = Path.GetFileName(uploadedFile.FileName);
				int pid = this.GetPid();
				string a = "";
				bool flag2 = num > 0;
				if (flag2)
				{
					DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork);
					DataTable dataTable = databaseLayer.ExecuteQuery("SELECT examfileid,filename FROM examfiles WHERE examfileid=@id AND visible=0", new DbParameter[]
					{
						databaseLayer.GetParameter("@id", DbType.Int32, num)
					});
					bool flag3 = dataTable.Rows.Count < 1;
					if (flag3)
					{
						num = 0;
					}
					else
					{
						a = dataTable.Rows[0][1].ToString();
					}
				}
				bool flag4 = num == 0;
				if (flag4)
				{
					num = ClockWorkWebAPI.Course.UploadTempExamFile(array, fileName, pid);
					this.Session["currentExamFileId"] = num;
				}
				else
				{
					bool flag5 = a != fileName;
					if (flag5)
					{
						ClockWorkWebAPI.Course.UploadTempExamFileReplace(num, array, fileName);
					}
				}
				this.lbl_upload_exam_alreadyUploaded_filename.Text = fileName;
				this.file1.Visible = false;
				this.p_upload_exam_alreadyUploaded.Visible = true;
				this.lbl_selectFile.Visible = false;
			}
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0002CC9C File Offset: 0x0002AE9C
		private int TryToUsePreviouslyUploadedTempFileFromSession(int examId)
		{
			try
			{
				object obj = this.Session["currentExamFileId"];
				int num = (obj != null) ? ((int)obj) : 0;
				bool flag = num < 1;
				if (flag)
				{
					return 0;
				}
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork);
				DataTable dataTable = databaseLayer.ExecuteQuery("SELECT examfileid FROM examfiles WHERE examfileid=@id AND visible=0", new DbParameter[]
				{
					databaseLayer.GetParameter("@id", DbType.Int32, num)
				});
				bool flag2 = dataTable.Rows.Count < 1;
				if (flag2)
				{
					return 0;
				}
				num = (int)dataTable.Rows[0][0];
				databaseLayer.ExecuteNonQuery("UPDATE examfiles SET visible=1,examid=@examid WHERE examfileid=@examfileid", new DbParameter[]
				{
					databaseLayer.GetParameter("@examid", DbType.Int32, examId),
					databaseLayer.GetParameter("@examfileid", DbType.Int32, num)
				});
				ClockWorkWebAPI.Course.MarkTestDeliveredOnline(examId);
				CWLogger.Logger.Info("Instructor:ExamUpload:UploadExamSuccess0:iid={0}", this.GetPid().ToString());
				return num;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("Instructor:ExamUpload:UploadExamFail0:iid={0}:error={1}", this.GetPid().ToString(), ex.ToString());
			}
			return 0;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0002CDEC File Offset: 0x0002AFEC
		protected void btn_uploadAgain_Click(object sender, EventArgs e)
		{
			object obj = this.Session["currentExamFileId"];
			int num = (obj is int) ? ((int)obj) : 0;
			bool flag = num > 0;
			if (flag)
			{
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork);
				databaseLayer.ExecuteNonQuery("DELETE FROM examfiles WHERE visible=0 AND examfileid=@id", new DbParameter[]
				{
					databaseLayer.GetParameter("@id", DbType.Int32, num)
				});
			}
			this.Session["currentExamFileId"] = 0;
			this.file1.Visible = true;
			this.p_upload_exam_alreadyUploaded.Visible = false;
			this.lbl_selectFile.Visible = true;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0002CE94 File Offset: 0x0002B094
		protected void Page_Load(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			int altContactId = this.GetAltContactId();
			bool flag = pid < 1 && altContactId < 1;
			if (flag)
			{
				base.Response.Redirect("Message.aspx?msgcode=notallowed", true);
			}
			else
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.INSTRUCTOR_TestConfirmationDateTimeMessage);
				bool flag2 = !string.IsNullOrEmpty(settingValue);
				if (flag2)
				{
					this.lblDateOfTestCustomTitle.Text = settingValue;
				}
				string settingValue2 = webSettingsClientManager.GetSettingValue<string>(Setting.INSTRUCTOR_SubmitTestPageIntro);
				bool flag3 = !string.IsNullOrEmpty(settingValue2);
				if (flag3)
				{
					this.lbl_intro.Text = settingValue2;
				}
				else
				{
					this.p_intro.Visible = false;
				}
				string settingValue3 = webSettingsClientManager.GetSettingValue<string>(Setting.INSTRUCTOR_ConfirmExamDetaislIntroMessage);
				this.lbl_submitinstructions.Text = settingValue3;
				string settingValue4 = webSettingsClientManager.GetSettingValue<string>(Setting.INSTRUCTOR_SubmitFileInstructions);
				bool flag4 = !string.IsNullOrEmpty(settingValue4);
				if (flag4)
				{
					this.lbl_fileinstructions.Text = settingValue4;
				}
				bool settingValue5 = webSettingsClientManager.GetSettingValue<bool>(Setting.INSTRUCTOR_InstructorConfirm_ConfirmEachStudent);
				bool settingValue6 = webSettingsClientManager.GetSettingValue<bool>(Setting.INSTRUCTOR_InstructorMustAcknowledgeReceiptOfExamRequests);
				bool flag5 = settingValue6;
				if (flag5)
				{
					bool flag6 = settingValue5;
					if (flag6)
					{
						this.p_instructorAcknowledgeReceiptOfTestRequests.Visible = false;
						this.gv_students.Columns[1].HeaderStyle.Width = 100;
						this.gv_students.Columns[4].Visible = true;
					}
					else
					{
						this.rbtns_instructorAcknowledgeReceiptOfTestRequests.Items[0].Text = webSettingsClientManager.GetSettingValue<string>(Setting.INSTRUCTOR_InstructorMustAcknowledgeReceiptOfExamRequests_AcknowledgeMessage);
						this.rbtns_instructorAcknowledgeReceiptOfTestRequests.Items[1].Text = webSettingsClientManager.GetSettingValue<string>(Setting.INSTRUCTOR_InstructorMustAcknowledgeReceiptOfExamRequests_QuestionsMessage);
						this.gv_students.Columns[4].Visible = false;
					}
				}
				else
				{
					this.p_instructorAcknowledgeReceiptOfTestRequests.Visible = false;
					this.gv_students.Columns[4].Visible = false;
				}
				string settingValue7 = webSettingsClientManager.GetSettingValue<string>(Setting.INSTRUCTOR_InvalidFileFormatUploadMessage);
				this.hidden_InvalidFileFormatUploadMessage.Value = settingValue7;
				string settingValue8 = webSettingsClientManager.GetSettingValue<string>(Setting.INSTRUCTOR_Tests_InstructionsForStudentsList);
				this.lbl_instructions.Text = settingValue8;
				string settingValue9 = webSettingsClientManager.GetSettingValue<string>(Setting.INSTRUCTOR_Tests_TestSubmitPageFinalNote);
				int num = settingValue9.IndexOf("{0}");
				bool flag7 = num > 0;
				if (flag7)
				{
					this.lbl_submitreminder.Text = settingValue9.Substring(0, num);
					this.lbl_submitreminder2.Text = settingValue9.Substring(num + 3);
				}
				else
				{
					this.lbl_submitreminder.Text = settingValue9;
					this.lbl_submitreminder2.Visible = false;
					this.link_submitChanges.Visible = false;
				}
				HttpPostedFile fileToUpload = this.GetFileToUpload();
				bool flag8 = fileToUpload != null;
				if (flag8)
				{
					this.UploadCurrentFileAndRememberItForLater();
				}
				bool flag9 = !this.Page.IsPostBack;
				if (flag9)
				{
					bool settingValue10 = webSettingsClientManager.GetSettingValue<bool>(Setting.INSTRUCTOR_InstructorConfirm_ShowStudentList);
					bool flag10 = settingValue10;
					if (flag10)
					{
						this.gv_students.Rebind();
					}
					int editingExamId = this.GetEditingExamId();
					bool flag11 = editingExamId < 1;
					if (flag11)
					{
						string text = base.Request.QueryString["newtest"];
						bool flag12 = text != null && text == "1";
						bool flag13 = !flag12;
						if (flag13)
						{
							base.Response.Redirect("courses.aspx", true);
							return;
						}
						this.SetupForNewClassTestDefinition(pid, altContactId);
					}
					else
					{
						this.SetupForExistingClassTestDefinition(editingExamId, pid, altContactId, settingValue6, settingValue5);
					}
					string text2 = base.Request.QueryString["dt"];
					bool flag14 = !string.IsNullOrEmpty(text2);
					if (flag14)
					{
						try
						{
							DateTime dateTime = DateTime.Parse(text2);
							this.datepicker.Value = dateTime.ToString("M/d/yyyy");
						}
						catch
						{
						}
					}
					string text3 = base.Request.QueryString["files"];
					bool flag15 = !string.IsNullOrEmpty(text3) && text3.Equals("1");
					if (flag15)
					{
						this.Wizard1.ActiveStepIndex = this.Wizard1.WizardSteps.IndexOf(this.step_submit);
					}
					bool flag16 = this.NotAllowedToUploadTestsExams;
					if (flag16)
					{
						this.p_submitfile.Visible = false;
					}
					bool settingValue11 = webSettingsClientManager.GetSettingValue<bool>(Setting.INSTRUCTOR_DontAskIfTestOrExam);
					bool flag17 = settingValue11;
					if (flag17)
					{
						this.chk_isFinalExam.Checked = false;
						this.chk_isFinalExam.Visible = false;
					}
				}
			}
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0002D318 File Offset: 0x0002B518
		protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
		{
			bool flag = this.Wizard1.WizardSteps[e.CurrentStepIndex].StepType == WizardStepType.Finish;
			if (flag)
			{
				e.Cancel = true;
			}
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0002D350 File Offset: 0x0002B550
		private void SetupForNewClassTestDefinition(int iid, int altContactId)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.INSTRUCTOR_HideAddTestOption);
			bool flag = settingValue;
			if (flag)
			{
				CWLogger.Logger.Warn("Instructor:ExamUpload:User attempted to navigate to this page with empty examid but new test is turned off:iid={0}:altContactid={1}", iid.ToString(), altContactId.ToString());
				base.Response.Redirect("courses.aspx", true);
			}
			else
			{
				this.p_existingCourse.Visible = false;
				int editingLucid = this.GetEditingLucid();
				bool flag2 = editingLucid > 0;
				if (flag2)
				{
					ILookupCourseClientManager lookupCourseClientManager = new LookupCourseClientManager();
					LookupCourseDTO lookupCourseDTO = lookupCourseClientManager.LoadCourseByLuCourseId(editingLucid);
					bool flag3 = !this.ctrlInstructorCourseChooser1.AddCourseManuallyAndSelect(lookupCourseDTO);
					if (flag3)
					{
						bool flag4 = editingLucid > 0;
						if (flag4)
						{
							CWLogger.Logger.Warn("Instructor:ExamUpload.aspx:passed lucourseid was > 0 but not found in loaded courses:lucid1={0}", editingLucid.ToString());
						}
						base.Response.Redirect("Message.aspx?msgcode=notallowedtoaddexam", true);
					}
					int settingValue2 = new WebSettingsClientManager().GetSettingValue<int>(Setting.INSTRUCTOR_TestExamCourseEndDateAuthorizationExtensionInDays);
					DateTime date = lookupCourseDTO.EndDate.AddDays((double)settingValue2).Date;
					bool flag5 = DateTime.Now.Date > date;
					if (flag5)
					{
						this.ReturnToCourseTestsList(eCantEditTestExamInfoReason.CantAddTestCourseHasEnded, editingLucid);
					}
				}
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0002D47F File Offset: 0x0002B67F
		protected void ctrlInstructorCourseChooser1_OnInstructorIdentityRequired(object sender, InstructorIdentityArgs e)
		{
			e.InstructorId = this.GetPid();
			e.AlternateContactId = this.GetAltContactId();
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0002D49C File Offset: 0x0002B69C
		private void ReturnToCourseTestsList(eCantEditTestExamInfoReason reason)
		{
			int editingLucid = this.GetEditingLucid();
			this.ReturnToCourseTestsList(reason, editingLucid);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0002D4BC File Offset: 0x0002B6BC
		private void ReturnToCourseTestsList(eCantEditTestExamInfoReason reason, int lucid)
		{
			string text;
			if (lucid <= 0)
			{
				text = "courses.aspx";
			}
			else
			{
				string str = "UploadedExams.aspx?lucid=";
				string str2 = NavigatorClientManager.CurrentInstance.ConvertIntParameterToUrlString(lucid);
				string str3 = "&reason=";
				int num = (int)reason;
				text = str + str2 + str3 + num.ToString();
			}
			string url = text;
			base.Response.Redirect(url, true);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0002D508 File Offset: 0x0002B708
		private void SetupForExistingClassTestDefinition(int examid, int iid, int altContactId, bool enableInstructorAcknowledgeReceiptOfTestRequests, bool confirmEachStudent)
		{
			this.p_newCourse.Visible = false;
			this.ctrlInstructorCourseChooser1.Visible = false;
			this.chk_isFinalExam.Visible = false;
			this.lbl_isFinalExam.Visible = true;
			IClassTestDefinitionClientManager classTestDefinitionClientManager = new ClassTestDefinitionClientManager();
			ClassTestDTO classTestDTO = classTestDefinitionClientManager.LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContact(examid, iid, altContactId);
			bool flag = classTestDTO == null;
			if (flag)
			{
				classTestDTO = classTestDefinitionClientManager.LoadClassTestById(examid);
				CWLogger.Logger.Warn("Instructor:ExamUpload:SetupForExistingClassTestDefinition:UnableToLoadClassTestDefinition:ExamId={0}:iid={1}:altid={2}:loadedexamid={3}", new object[]
				{
					examid.ToString(),
					iid.ToString(),
					altContactId.ToString(),
					(classTestDTO == null) ? "NULL" : classTestDTO.ExamId.ToString()
				});
				this.ReturnToCourseTestsList(eCantEditTestExamInfoReason.InstructorOrAltContactNotAllowed);
			}
			else
			{
				bool flag2 = enableInstructorAcknowledgeReceiptOfTestRequests && !confirmEachStudent && classTestDTO.InstructorAcknowledged != null;
				if (flag2)
				{
					char value = classTestDTO.InstructorAcknowledged.Value;
					bool flag3 = value == 'y' || value == 'Y';
					if (flag3)
					{
						this.rbtns_instructorAcknowledgeReceiptOfTestRequests.SelectedIndex = 0;
					}
					else
					{
						bool flag4 = value == 'n' || value == 'N';
						if (flag4)
						{
							this.rbtns_instructorAcknowledgeReceiptOfTestRequests.SelectedIndex = 1;
						}
						else
						{
							CWLogger.Logger.Warn("Instructor/ExamUpload:SetupForExistingClassTestDefinition:unable to display:classTest.InstructorAcknowledgedValue={0}", classTestDTO.InstructorAcknowledged.Value.ToString());
						}
					}
				}
				DateTime startDateTime = classTestDTO.StartDateTime;
				CutoffTime cutoffTime = new WebSettingsClientManager().GetSettingValue<string>(Setting.INSTRUCTOR_CutoffForUpdatingTests).CutoffTimeFromXml() ?? CutoffTime.None;
				bool? flag5 = cutoffTime.IsRightNowBeforeCutoffTime(startDateTime);
				bool flag6 = flag5 == null || flag5.Value;
				bool flag7 = !flag6;
				if (flag7)
				{
					this.ReturnToCourseTestsList(eCantEditTestExamInfoReason.CutoffTimeForEditingTestHasPassed);
				}
				else
				{
					IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
					CutoffTime cutoffTime2 = webSettingsClientManager.GetSettingValue<string>(Setting.INSTRUCTOR_CutoffForUpdatingTestDateTime).CutoffTimeFromXml() ?? CutoffTime.None;
					bool enabled = cutoffTime2.Enabled;
					if (enabled)
					{
						bool? flag8 = cutoffTime2.IsRightNowBeforeCutoffTime(startDateTime);
						flag6 = (flag8 != null && flag8.Value);
					}
					bool flag9 = !flag6;
					if (flag9)
					{
						this.isDatePickerDisabled.Value = "1";
						this.datepicker.Disabled = true;
						this.startTime.Disabled = true;
						this.endTime.Disabled = true;
					}
					int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.INSTRUCTOR_uploadscreennum);
					bool flag10 = settingValue > 0;
					if (flag10)
					{
						DynamicScreenLayout.FillScreenWithPerAppointmentData("InstructorPM", this.p_data, settingValue, 0, examid, base.Cache, "");
					}
					this.lbl_isFinalExam.Text = ((classTestDTO.ExamType == eClassTestType.FinalExam) ? "This is a final exam." : "This is a test, mid-term, or quiz.");
					this.datepicker.Value = startDateTime.ToString("M/d/yyyy");
					this.originalExamDate.Value = startDateTime.ToString("yyyy-MM-dd H:mm");
					this.startTime.Value = classTestDTO.StartDateTime.ToString("h:mm tt");
					int durationMinutes = classTestDTO.GetDurationMinutes();
					int num = (int)((double)durationMinutes / 60.0 / 60.0);
					int num2 = durationMinutes - num * 60 * 60;
					DateTime dateTime = classTestDTO.StartDateTime.AddHours((double)num).AddMinutes((double)num2);
					this.endTime.Value = dateTime.ToString("h:mm tt");
					this.originalExamDuration.Value = durationMinutes.ToString();
					int num3 = (classTestDTO.Course == null) ? 0 : classTestDTO.Course.LuCourseId;
					string text = this.ctrlInstructorCourseChooser1.SetSelectedItemForever(num3);
					bool flag11 = string.IsNullOrEmpty(text);
					if (flag11)
					{
						ILookupCourseClientManager lookupCourseClientManager = new LookupCourseClientManager();
						LookupCourseDTO lookupCourseDTO = lookupCourseClientManager.LoadCourseByLuCourseId(num3);
						bool flag12 = lookupCourseDTO == null;
						if (flag12)
						{
							CWLogger.Logger.Error("Instructor.ExamUpload.aspx.cs:Unable to allow instructor to edit exam because course not found:lucid={0}:iid={1}:altContactId={2}", num3.ToString(), iid.ToString(), altContactId.ToString());
							base.Response.Redirect("courses.aspx", true);
						}
						else
						{
							bool flag13 = lookupCourseDTO.Instructors != null && lookupCourseDTO.Instructors.FirstOrDefault((LookupInstructorDTO g) => g.InstructorId == iid && g.IsAllowed(ePermissionForCourseDTO.AccessTestInfoOnline)) != null;
							bool flag14 = !flag13;
							if (flag14)
							{
								flag13 = (lookupCourseDTO.AlternateContacts != null && lookupCourseDTO.AlternateContacts.FirstOrDefault((AlternateContactDTO g) => g.AlternateContactId == altContactId && g.IsAllowed(ePermissionForCourseDTO.AccessTestInfoOnline)) != null);
							}
							bool flag15 = !flag13;
							if (flag15)
							{
								CWLogger.Logger.Error("Instructor.ExamUpload.aspx.cs:Unable to allow instructor to edit exam because iid or altcontactid not assigned to course or not allowed to edit tests:lucid={0}:iid={1}:altContactId={2}", num3.ToString(), iid.ToString(), altContactId.ToString());
								this.ReturnToCourseTestsList(eCantEditTestExamInfoReason.InstructorOrAltContactNotAllowed);
							}
							else
							{
								this.ctrlInstructorCourseChooser1.AddCourseManuallyAndSelect(lookupCourseDTO);
								text = lookupCourseDTO.GetCourseDescriptionShort();
								this.lbl_readonlycourse.Text = (text ?? "");
							}
						}
					}
					else
					{
						this.lbl_readonlycourse.Text = (text ?? "");
					}
				}
			}
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0002DA3C File Offset: 0x0002BC3C
		private int GetSelectedCourse(out string courseDescription)
		{
			return this.ctrlInstructorCourseChooser1.GetSelectedCourse(out courseDescription);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0002DA5C File Offset: 0x0002BC5C
		private void SelectDropListItem(DropDownList cmb, string value)
		{
			foreach (object obj in cmb.Items)
			{
				ListItem listItem = (ListItem)obj;
				bool flag = listItem.Value.CompareTo(value) == 0;
				if (flag)
				{
					listItem.Selected = true;
					break;
				}
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0002DAD4 File Offset: 0x0002BCD4
		private int GetEditingExamId()
		{
			int intFromUrlParameter = NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["examid"] ?? "");
			bool flag = intFromUrlParameter > 0;
			int result;
			if (flag)
			{
				result = intFromUrlParameter;
			}
			else
			{
				IEncryption encryption = DatabaseLayerFactory.ClockWork.Encryption;
				result = ClockWorkWebCore.GetUrlVariableInt(base.Request, "examid", true, encryption);
			}
			return result;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0002DB38 File Offset: 0x0002BD38
		private int GetEditingLucid()
		{
			return NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["lucid"] ?? "");
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0002DB74 File Offset: 0x0002BD74
		private string ValidateFiles(int examid)
		{
			HttpPostedFile fileToUpload = this.GetFileToUpload();
			bool flag = fileToUpload == null;
			string result;
			if (flag)
			{
				bool flag2 = examid > 0;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = "No valid files are specified.";
				}
			}
			else
			{
				string[] allowedFileTypes = this.GetAllowedFileTypes();
				string text = Path.GetExtension(fileToUpload.FileName).ToLower().Trim();
				bool flag3 = !allowedFileTypes.Contains(text);
				if (flag3)
				{
					CWLogger.Logger.Debug("Instructor:ExamUpload:Submit:ValidateFiles:Failed - inavlid file type:ext={0}:allowedfiletypes={1}", text, allowedFileTypes);
					result = "Invalid file type - only [" + string.Join(",", allowedFileTypes ?? new string[0]) + "] file types are allowed.";
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0002DC19 File Offset: 0x0002BE19
		protected void btn_upload_Click(object sender, EventArgs e)
		{
			this.Submit();
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0002DC24 File Offset: 0x0002BE24
		private void Submit()
		{
			HttpPostedFile fileToUpload = this.GetFileToUpload();
			string[] allowedFileTypes = this.GetAllowedFileTypes();
			CWLogger.Logger.Debug("Instructor:ExamUpload:Submit:upload_examcount={0}:allowedfiletypes={1}", (fileToUpload == null) ? "0" : "1", string.Join(",", allowedFileTypes));
			int num = 0;
			bool flag = fileToUpload != null;
			if (flag)
			{
				CWLogger.Logger.Info("Instructor:ExamUpload:Submit:file#={0}:filelen={1};filename={2}", (num + 1).ToString(), fileToUpload.ContentLength.ToString(), fileToUpload.FileName);
			}
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork);
			int pid = this.GetPid();
			int altContactId = this.GetAltContactId();
			int num2 = this.GetEditingExamId();
			string text = base.Request.QueryString["newtest"];
			bool flag2 = text != null && text == "1";
			bool flag3 = fileToUpload != null;
			string text2;
			if (flag3)
			{
				text2 = this.ValidateFiles(num2);
			}
			else
			{
				text2 = null;
			}
			string value;
			int selectedCourse = this.GetSelectedCourse(out value);
			bool flag4 = text2 != null;
			if (flag4)
			{
				this.Session["msgcode"] = "fileproblem";
				this.Session["msgcodedesc"] = text2;
				CWLogger.Logger.Warn("Instructor:ExamUpload:FileProblem:iid={0}:fileproblem={1}", pid.ToString(), text2.ToString());
				this.ShowMessage();
			}
			else
			{
				bool flag5 = selectedCourse < 0;
				if (flag5)
				{
					this.Session["msgcode"] = "missingcourse";
					this.Session["msgcodedesc"] = "";
					CWLogger.Logger.Warn("Instructor:ExamUpload:MissingCourse:iid={0}", pid.ToString());
					this.ShowMessage();
				}
				else
				{
					DateTime minValue = DateTime.MinValue;
					DateTime minValue2 = DateTime.MinValue;
					string value2 = this.datepicker.Value;
					DateTime minValue3;
					bool flag6 = !DateTime.TryParse(value2, out minValue3);
					if (flag6)
					{
						minValue3 = DateTime.MinValue;
					}
					bool flag7 = minValue3 != DateTime.MinValue;
					if (flag7)
					{
						string str = this.startTime.Value.ToLower().Replace("am", " am").Replace("pm", " pm");
						string str2 = this.endTime.Value.ToLower().Replace("am", " am").Replace("pm", " pm");
						string str3 = minValue3.Date.ToString("yyyy-MM-dd");
						string text3 = str3 + " " + str;
						string text4 = str3 + " " + str2;
						bool flag8 = text3.Length < 1 || text4.Length < 1 || !DateTime.TryParse(text3, out minValue) || !DateTime.TryParse(text4, out minValue2);
						if (flag8)
						{
							minValue = DateTime.MinValue;
							minValue2 = DateTime.MinValue;
						}
					}
					bool flag9 = minValue == DateTime.MinValue || minValue2 == DateTime.MinValue;
					if (flag9)
					{
						this.Session["msgcode"] = "invalidtestdate";
						this.Session["msgcodedesc"] = "";
						CWLogger.Logger.Warn("Instructor:ExamUpload:InvalidTestDate:iid={0}:txt_dateoftest.selecteddate.value={1}", pid.ToString(), "st=" + this.startTime.Value + "; et=" + this.endTime.Value);
						this.ShowMessage();
					}
					else
					{
						int num3 = Convert.ToInt32((minValue2 - minValue).TotalMinutes);
						bool flag10 = num3 <= 0;
						if (flag10)
						{
							this.Session["msgcode"] = "invalidduration";
							this.Session["msgcodedesc"] = "";
							CWLogger.Logger.Warn("Instructor:ExamUpload:InvalidDuration:iid={0}:testduration={1}", pid.ToString(), num3.ToString());
							this.ShowMessage();
						}
						else
						{
							bool flag11 = minValue2 <= minValue;
							if (flag11)
							{
								this.Session["msgcode"] = "invalidtime";
								this.Session["msgcodedesc"] = "";
								CWLogger.Logger.Warn("Instructor:ExamUpload:InvalidTimeEndBeforeStart:iid={0}:testduration={1}:start={2}:end={3}", new object[]
								{
									pid.ToString(),
									num3.ToString(),
									minValue.ToString("yyyy-MM-dd h:mm tt"),
									minValue2.ToString("yyyy-MM-dd h:mm tt")
								});
								this.ShowMessage();
							}
							else
							{
								bool flag12 = pid < 1 && altContactId < 1;
								if (flag12)
								{
									this.Session["msgcode"] = "Session timed out.";
									this.Session["msgcodedesc"] = "Your session has timed out.  You will have to login and try again.";
									CWLogger.Logger.Warn("Instructor:ExamUpload:SessionTimedOut");
									this.ShowMessage();
								}
								else
								{
									bool flag13 = selectedCourse <= 0;
									if (flag13)
									{
										this.Session["msgcode"] = "missingcourse";
										this.Session["msgcodedesc"] = "";
										CWLogger.Logger.Warn("Instructor:ExamUpload:MissingCourse:iid={0}", pid.ToString());
										this.ShowMessage();
									}
									else
									{
										bool flag14 = false;
										string text5 = "";
										string text6 = "";
										WebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
										bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.INSTRUCTOR_InstructorMustAcknowledgeReceiptOfExamRequests);
										bool settingValue2 = webSettingsClientManager.GetSettingValue<bool>(Setting.INSTRUCTOR_InstructorConfirm_ConfirmEachStudent);
										char c = (settingValue && !settingValue2) ? this.GetSingleInstructorAcknowledgeCode() : ' ';
										bool flag15 = num2 > 0;
										int num4;
										Exception ex2;
										if (flag15)
										{
											num4 = num2;
											DbParameter[] parameters = new DbParameter[]
											{
												databaseLayer.GetParameter("@dateoftest", DbType.DateTime, minValue),
												databaseLayer.GetParameter("@iid", DbType.Int32, (pid > 0) ? pid : altContactId),
												databaseLayer.GetParameter("@testduration", DbType.Int32, num3),
												databaseLayer.GetParameter("@examid", DbType.Int32, num2),
												databaseLayer.GetParameter("@lucid", DbType.Int32, selectedCourse),
												databaseLayer.GetParameter("@instructoracknowledged", DbType.StringFixedLength, c)
											};
											try
											{
												databaseLayer.ExecuteNonQuery(ClockWorkWebAPI.QueryStorage.QS_Update_ExamInfo, parameters);
												CWLogger.Logger.Info("Instructor:ExamUpload:UpdateExamSuccess:iid={0}:dateoftest={1}:testduration={2}:examid={3}:lucid={4}", new object[]
												{
													pid.ToString(),
													minValue.ToString("yyyy-MM-dd h:mm tt"),
													num3.ToString(),
													num2.ToString(),
													selectedCourse.ToString()
												});
											}
											catch (Exception ex)
											{
												CWLogger.Logger.Error("Instructor:ExamUpload:UpdateExamFail:iid={0}:dateoftest={1}:testduration={2}:examid={3}:lucid={4}:error={5}", new object[]
												{
													pid.ToString(),
													minValue.ToString("yyyy-MM-dd h:mm tt"),
													num3.ToString(),
													num2.ToString(),
													selectedCourse.ToString(),
													ex.ToString()
												});
											}
											DateTime dateTime;
											int num5;
											bool flag16 = DateTime.TryParse(this.originalExamDate.Value, out dateTime) && int.TryParse(this.originalExamDuration.Value, out num5);
											if (flag16)
											{
												bool flag17 = dateTime.Date != minValue.Date || dateTime.Hour != minValue.Hour || dateTime.Minute != minValue.Minute;
												bool flag18 = num5 != num3;
												bool flag19 = flag17 || flag18;
												if (flag19)
												{
													flag14 = true;
													text5 = dateTime.ToString("MMM d, yyyy h:mm tt");
													text6 = num5.GetDurationDescriptionShort();
												}
											}
											ex2 = null;
										}
										else
										{
											bool flag20 = !flag2;
											if (flag20)
											{
												base.Response.Redirect("UploadedExams.aspx?lucid=" + NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(selectedCourse), true);
											}
											string typeCode = this.chk_isFinalExam.Checked ? "F" : "N";
											ex2 = ClockWorkWebAPI.Course.UploadExam(pid, selectedCourse, minValue, num3, typeCode, out num4);
											bool flag21 = num4 > 0;
											if (flag21)
											{
												num2 = num4;
											}
											bool flag22 = ex2 == null && num2 > 0;
											if (flag22)
											{
												CWLogger.Logger.Info("Instructor:ExamUpload:newExamSuccess:iid={0}:dateoftest={1}:testduration={2}:examid={3}:lucid={4}", new object[]
												{
													pid.ToString(),
													minValue.ToString("yyyy-MM-dd h:mm tt"),
													num3.ToString(),
													num2.ToString(),
													selectedCourse.ToString()
												});
											}
											else
											{
												CWLogger.Logger.Error("Instructor:ExamUpload:newExamFail:iid={0}:dateoftest={1}:testduration={2}:examid={3}:lucid={4}:error={5}", new object[]
												{
													pid.ToString(),
													minValue.ToString("yyyy-MM-dd h:mm tt"),
													num3.ToString(),
													num2.ToString(),
													selectedCourse.ToString(),
													(ex2 == null) ? "NULL" : ex2.ToString()
												});
											}
										}
										bool flag23 = ex2 == null && num4 > 0;
										if (flag23)
										{
											int num6 = this.TryToUsePreviouslyUploadedTempFileFromSession(num4);
											bool flag24 = num6 < 1;
											if (flag24)
											{
												bool flag25 = fileToUpload != null;
												if (flag25)
												{
													try
													{
														byte[] array = new byte[fileToUpload.InputStream.Length];
														fileToUpload.InputStream.Read(array, 0, Convert.ToInt32(fileToUpload.InputStream.Length));
														ex2 = ClockWorkWebAPI.Course.UploadExamFile(num4, array, Path.GetFileName(fileToUpload.FileName), pid);
														bool flag26 = ex2 == null;
														if (flag26)
														{
															CWLogger.Logger.Info("Instructor:ExamUpload:OldUploadExamSuccess0:iid={0}:filename={1}:size={2}", pid.ToString(), fileToUpload.FileName, fileToUpload.InputStream.Length.ToString());
														}
														else
														{
															CWLogger.Logger.Error("Instructor:ExamUpload:OldUploadExamFail0:iid={0}:filename={1}:size={2}:error={3}", new object[]
															{
																pid.ToString(),
																fileToUpload.FileName,
																fileToUpload.InputStream.Length.ToString(),
																ex2.ToString()
															});
														}
													}
													catch (Exception ex3)
													{
														CWLogger.Logger.Error("Instructor:ExamUpload:OldUploadExamFail1:iid={0}:filename={1}:size={2}:error={3}", new object[]
														{
															pid.ToString(),
															fileToUpload.FileName,
															fileToUpload.InputStream.Length.ToString(),
															ex3.ToString()
														});
													}
												}
											}
											int settingValue3 = new WebSettingsClientManager().GetSettingValue<int>(Setting.INSTRUCTOR_uploadscreennum);
											bool flag27 = settingValue3 > 0;
											if (flag27)
											{
												Exception ex4 = DynamicScreenLayout.SaveDynamicData(ScreenType.ScreenType_InstructorPerExam, 0, num4, settingValue3, base.Cache, this.p_data, "");
												bool flag28 = ex4 == null;
												if (flag28)
												{
													CWLogger.Logger.Debug("Instructor:ExamUpload:SaveDynamicDataSuccess:iid={0}:examid={1}", pid.ToString(), num2.ToString());
												}
												else
												{
													string text7;
													try
													{
														text7 = DynamicScreenLayout.GetSummaryPlainText(this.p_data, settingValue3, 0, base.Cache, null, "", false);
													}
													catch (Exception ex5)
													{
														text7 = ex5.ToString();
													}
													CWLogger.Logger.Error("Instructor:ExamUpload:SaveDynamicDataFail:iid={0}:examid={1}:formdata={2}:error={3}", new object[]
													{
														pid.ToString(),
														num2.ToString(),
														text7.ToString(),
														ex4.ToString()
													});
												}
											}
											bool flag29 = settingValue2;
											if (flag29)
											{
												foreach (object obj in this.gv_students.Items)
												{
													GridDataItem gridDataItem = (GridDataItem)obj;
													TableCell tableCell = gridDataItem["col_student"];
													TextBox textBox = ((tableCell != null) ? tableCell.FindControl("lbl_appid") : null) as TextBox;
													string s = ((textBox != null) ? textBox.Text : null) ?? "";
													int num7;
													bool flag30 = !int.TryParse(s, out num7);
													if (flag30)
													{
														num7 = 0;
													}
													bool flag31 = num7 > 0;
													if (flag31)
													{
														RadioButtonList radioButtonList = (RadioButtonList)gridDataItem["col_acknowledge"].FindControl("rb_confirm");
														string text8 = (radioButtonList != null) ? radioButtonList.SelectedValue : null;
														bool flag32 = string.IsNullOrEmpty(text8);
														if (!flag32)
														{
															int num8 = (text8 == "yes") ? 1 : 0;
															bool flag33 = num7 < 0;
															if (!flag33)
															{
																databaseLayer.ExecuteNonQuery("UPDATE appointmentcourses SET InstructorAcknowledgeValue=@val,InstructorAcknowledgeDate=getdate() WHERE appointmentid=@appid", new DbParameter[]
																{
																	databaseLayer.GetParameter("@val", DbType.Int32, num8),
																	databaseLayer.GetParameter("@appid", DbType.Int32, num7)
																});
																CWLogger.Logger.Debug("Instructor:ExamUpload:UpdateInstructorAcknowledgements:iid={0}:appid={1}", pid.ToString(), num7.ToString());
															}
														}
													}
												}
											}
											string value3 = "";
											string text9 = "";
											string value4 = "";
											bool flag34 = pid > 0;
											if (flag34)
											{
												ClockWorkController.Instructor instructor = ClockWorkController.Instructor.LoadInstructor(pid);
												bool flag35 = instructor != null;
												if (flag35)
												{
													value3 = instructor.InstructorName;
													text9 = instructor.InstructorEmail;
													value4 = instructor.InstructorPhone;
												}
											}
											else
											{
												bool flag36 = altContactId > 0;
												if (flag36)
												{
													CourseContactInformation courseContactInformation = ClockWorkController.Instructor.LoadAlternateContact(altContactId);
													bool flag37 = courseContactInformation != null;
													if (flag37)
													{
														value3 = courseContactInformation.Name;
														text9 = courseContactInformation.Email;
														value4 = courseContactInformation.Phone;
													}
												}
											}
											StringDictionary stringDictionary = new StringDictionary();
											string studentsListString = this.GetStudentsListString();
											string instructorAcknowledgeString = this.GetInstructorAcknowledgeString();
											stringDictionary.Add("coursedescription", value);
											stringDictionary.Add("instructorname", value3);
											stringDictionary.Add("instructoremail", text9);
											stringDictionary.Add("instructorphone", value4);
											stringDictionary.Add("students", studentsListString);
											stringDictionary.Add("date", DateTime.Now.ToString("MMMM d, yyyy"));
											stringDictionary.Add("time", DateTime.Now.ToString("h:mm tt"));
											stringDictionary.Add("instructoracknowledge", instructorAcknowledgeString);
											stringDictionary.Add("testdate", minValue.ToString("MMMM d, yyyy h:mm tt"));
											stringDictionary.Add("testduration", num3.GetDurationDescriptionShort());
											stringDictionary.Add("classdate", minValue.ToString("dddd MMMM d, yyyy"));
											stringDictionary.Add("classstarttime", minValue.ToString("h:mm tt"));
											stringDictionary.Add("classendtime", minValue.AddMinutes((double)num3).ToString("h:mm tt"));
											IMailMergeCodes mailMergeCodes = new MailMergeCodes();
											stringDictionary.Add("from", mailMergeCodes.GetDefaultFromAddress(eWebModule.InstructorTestsExams));
											stringDictionary.Add("signature", mailMergeCodes.GetDefaultSignature(eWebModule.InstructorTestsExams));
											IEmailClientManager emailClientManager = new EmailClientManager();
											MailMergeContextDTO mailMergeContext = new MailMergeContextDTO
											{
												InstructorId = pid,
												LuCourseId = selectedCourse,
												ExamId = num2
											};
											Setting setting = this.chk_isFinalExam.Checked ? Setting.INSTRUCTOR_Tests_EmailOnExamUpdate : Setting.INSTRUCTOR_Tests_EmailOnTestUpdate;
											bool flag38 = setting == Setting.INSTRUCTOR_Tests_EmailOnExamUpdate;
											if (flag38)
											{
												string settingValue4 = new WebSettingsClientManager().GetSettingValue<string>(setting);
												Template template = settingValue4.TemplateFromXml();
												int? num9;
												if (template == null)
												{
													num9 = null;
												}
												else
												{
													TPMailMessage emailTemplate = template.EmailTemplate;
													num9 = ((emailTemplate != null) ? new bool?(emailTemplate.IsActive) : null);
												}
												bool flag39 = (num9 ?? 0) == 0;
												if (flag39)
												{
													setting = Setting.INSTRUCTOR_Tests_EmailOnTestUpdate;
												}
											}
											emailClientManager.SendEmail(setting, mailMergeContext, stringDictionary.InsertBaseUserMailMergeValues(), "InstructorExamUpload");
											bool flag40 = flag14;
											if (flag40)
											{
												stringDictionary.Add("olddatetime", text5);
												stringDictionary.Add("oldduration", text6);
												bool flag41 = !stringDictionary.ContainsKey("from");
												if (flag41)
												{
													stringDictionary.Add("from", mailMergeCodes.GetDefaultFromAddress(eWebModule.InstructorTestsExams));
													stringDictionary.Add("signature", mailMergeCodes.GetDefaultSignature(eWebModule.InstructorTestsExams));
												}
												emailClientManager.SendEmail(Setting.INSTRUCTOR_Email_ChangedDateTimeOfTest, mailMergeContext, stringDictionary.InsertBaseUserMailMergeValues(), "InstructorExamUpload");
												CWLogger.Logger.Info("Instructor:ExamUpload:Submit:InstructorChangedDateTime:iid={0}:email={1}:olddatetime={2}:oldduration={3}:newdatetime={4}:newduration={5}", new object[]
												{
													pid.ToString(),
													text9.ToString(),
													text5,
													text6,
													minValue.ToString("yyyy-MM-dd h:mm tt"),
													num3.ToString()
												});
											}
											CWLogger.Logger.Info("Instructor:ExamUpload:SuccessfulSubmit:iid={0}", pid.ToString());
											base.Response.Redirect("ExamUploadComplete.aspx?lucid=" + NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(selectedCourse));
										}
										else
										{
											this.Session["msgcode"] = "uploaderror";
											this.Session["msgcodedesc"] = "There was an upload error.";
											CWLogger.Logger.Error("Instructor:ExamUpload:UploadError:iid={0}:error={1}", pid.ToString(), ex2.ToString());
											this.ShowMessage();
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0002DC19 File Offset: 0x0002BE19
		protected void link_submitChanges_Click(object sender, EventArgs e)
		{
			this.Submit();
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0002ED60 File Offset: 0x0002CF60
		private string GetInstructorAcknowledgeString()
		{
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.INSTRUCTOR_InstructorMustAcknowledgeReceiptOfExamRequests);
			bool flag = settingValue;
			string result;
			if (flag)
			{
				int selectedIndex = this.rbtns_instructorAcknowledgeReceiptOfTestRequests.SelectedIndex;
				if (selectedIndex != 0)
				{
					if (selectedIndex != 1)
					{
						result = "The instructor did not acknowledge or indicate they have questions.  This should not happen, please let your administrator know.";
					}
					else
					{
						result = this.rbtns_instructorAcknowledgeReceiptOfTestRequests.SelectedItem.Text;
					}
				}
				else
				{
					result = this.rbtns_instructorAcknowledgeReceiptOfTestRequests.SelectedItem.Text;
				}
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0002EDD4 File Offset: 0x0002CFD4
		private char GetSingleInstructorAcknowledgeCode()
		{
			int selectedIndex = this.rbtns_instructorAcknowledgeReceiptOfTestRequests.SelectedIndex;
			char result;
			if (selectedIndex != 0)
			{
				if (selectedIndex != 1)
				{
					result = '?';
				}
				else
				{
					result = 'N';
				}
			}
			else
			{
				result = 'Y';
			}
			return result;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0002EE0C File Offset: 0x0002D00C
		private IList<StudentInTestWrapper> LoadStudentsWritingThisTest()
		{
			int editingExamId = this.GetEditingExamId();
			ITestBookingClientManager testBookingClientManager = new TestBookingClientManager();
			IList<StudentWritingTestDTO> list = testBookingClientManager.LoadStudentsWritingExam(editingExamId);
			IList<StudentInTestWrapper> list2;
			if (list == null)
			{
				list2 = null;
			}
			else
			{
				list2 = (from g in list
				select new StudentInTestWrapper(g)).ToList<StudentInTestWrapper>();
			}
			this.lastStudents = (list2 ?? new List<StudentInTestWrapper>());
			return this.lastStudents;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0002EE78 File Offset: 0x0002D078
		private string GetStudentsListString()
		{
			IList<StudentInTestWrapper> list = this.lastStudents ?? this.LoadStudentsWritingThisTest();
			bool flag = list == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < list.Count; i++)
				{
					bool flag2 = i > 0;
					if (flag2)
					{
						stringBuilder.Append("\n");
					}
					StudentInTestWrapper studentInTestWrapper = list[i];
					stringBuilder.AppendFormat("{0} {1} ({2}) . {3} . {4}", new object[]
					{
						studentInTestWrapper.firstname,
						studentInTestWrapper.lastname,
						studentInTestWrapper.student_no,
						studentInTestWrapper.startdate.ToString("MMMM d, yyyy . h:mm tt"),
						studentInTestWrapper.enddate.ToString("h:mm tt")
					});
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0002EF60 File Offset: 0x0002D160
		protected void grid_previousuploads_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
		{
			int pid = this.GetPid();
			int editingExamId = this.GetEditingExamId();
			DateTime dateTime;
			DateTime dateTime2;
			DataTable dataTable = ClockWorkWebAPI.Course.LoadInstructorsCourses(pid, out dateTime, out dateTime2);
			DataTable dataTable2 = ClockWorkWebAPI.Course.LoadUploadedExams(editingExamId);
			foreach (object obj in dataTable2.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				dataRow["filename"] = HttpUtility.HtmlEncode(dataRow["filename"].ToString());
			}
			this.grid_previousuploads.DataSource = dataTable2;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0002F014 File Offset: 0x0002D214
		private void ShowMessage()
		{
			object obj = this.Session["msgcode"];
			bool flag = obj == null;
			if (!flag)
			{
				string text = (string)obj;
				object obj2 = this.Session["msgcodedesc"];
				string str = (obj2 == null) ? "" : ((string)obj2);
				string text2 = text;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text2);
				if (num <= 1625043138U)
				{
					if (num != 414134768U)
					{
						if (num != 673383625U)
						{
							if (num == 1625043138U)
							{
								if (text2 == "fileproblem")
								{
									this.lbl_topmsg.Text = "There was a problem with the file(s) you are trying to upload: " + str;
									this.p_topmsg.Visible = true;
									this.file1.Focus();
								}
							}
						}
						else if (text2 == "invalidtime")
						{
							this.lbl_topmsg.Text = "Please select a valid time in order to continue (end time is before start time).";
							this.p_topmsg.Visible = true;
							this.datepicker.Focus();
						}
					}
					else if (text2 == "invalidduration")
					{
						this.lbl_topmsg.Text = "Please enter a valid class test duration in order to continue.";
						this.p_topmsg.Visible = true;
						this.endTime.Focus();
					}
				}
				else if (num <= 3133876818U)
				{
					if (num != 2634644150U)
					{
						if (num == 3133876818U)
						{
							if (text2 == "missingcourse")
							{
								this.lbl_topmsg.Text = "Please select a valid course in order to continue.";
								this.p_topmsg.Visible = true;
								this.ctrlInstructorCourseChooser1.Focus();
							}
						}
					}
					else if (text2 == "invalidfiletype")
					{
						this.lbl_topmsg.Text = "The file that you have specified to submit is not an accepted type of file. " + str;
						this.p_topmsg.Visible = true;
						this.file1.Focus();
					}
				}
				else if (num != 3252609002U)
				{
					if (num == 3605639536U)
					{
						if (text2 == "invalidtestdate")
						{
							this.lbl_topmsg.Text = "Please select a valid test date / time in order to continue (ensure date is valid and end time is before start time).";
							this.p_topmsg.Visible = true;
							this.datepicker.Focus();
						}
					}
				}
				else if (text2 == "uploaderror")
				{
					this.lbl_topmsg.Text = "There was a database error and your test may not have been uploaded correctly. " + str;
					this.p_topmsg.Visible = true;
				}
				this.Session["msgcode"] = null;
				this.Session["msgcodedesc"] = null;
			}
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0002F2EC File Offset: 0x0002D4EC
		protected void Wizard1_OnCancelButtonClick(object sender, EventArgs e)
		{
			this.DoCancel();
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0002F2EC File Offset: 0x0002D4EC
		protected void btn_cancel_Click(object sender, EventArgs e)
		{
			this.DoCancel();
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0002F2F6 File Offset: 0x0002D4F6
		private void DoCancel()
		{
			this.ReturnToCourseTestsList(eCantEditTestExamInfoReason.UserCancelled);
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0002F304 File Offset: 0x0002D504
		protected void grid_previousuploads_ItemCommand(object source, GridCommandEventArgs e)
		{
			object commandArgument = e.CommandArgument;
			int num = 0;
			string text = (commandArgument != null) ? commandArgument.ToString() : null;
			bool flag = text != null && text.Length > 0;
			if (flag)
			{
				num = int.Parse(commandArgument.ToString());
			}
			bool flag2 = num <= 0;
			if (!flag2)
			{
				bool flag3 = e.CommandName.CompareTo("view") == 0;
				if (flag3)
				{
					try
					{
						string text2;
						byte[] bytes = ClockWorkWebAPI.Course.DownloadExam(num, null, out text2);
						FileWeb.DownloadFile(this.Page, base.Response, text2.Replace(' ', '_'), bytes, true);
					}
					catch (Exception ex)
					{
						CWLogger.Logger.Error("ExamUpload.aspx.cs:ViewFile:Error={0}", ex.ToString());
					}
				}
				else
				{
					bool flag4 = e.CommandName.CompareTo("remove") == 0;
					if (flag4)
					{
						int editingExamId = this.GetEditingExamId();
						ClockWorkWebAPI.Course.DeleteUploadedExam(editingExamId, num);
						this.Session["currentExamFileId"] = 0;
						this.grid_previousuploads.Rebind();
					}
				}
			}
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0002F424 File Offset: 0x0002D624
		protected void gv_students_ItemCreated(object sender, GridItemEventArgs e)
		{
			bool flag = e.Item is GridDataItem;
			if (flag)
			{
				GridDataItem gridDataItem = e.Item as GridDataItem;
				TableCell tableCell = gridDataItem["col_student"];
				bool flag2 = tableCell != null;
				if (flag2)
				{
					tableCell.Attributes["scope"] = "row";
				}
			}
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.INSTRUCTOR_InstructorMustAcknowledgeReceiptOfExamRequests);
			bool flag3 = !settingValue;
			if (!flag3)
			{
				bool settingValue2 = new WebSettingsClientManager().GetSettingValue<bool>(Setting.INSTRUCTOR_InstructorConfirm_ConfirmEachStudent);
				bool flag4 = !settingValue2;
				if (!flag4)
				{
					RadioButtonList radioButtonList = (RadioButtonList)e.Item.FindControl("rb_confirm");
					bool flag5 = radioButtonList == null;
					if (!flag5)
					{
						radioButtonList.Items[0].Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.INSTRUCTOR_InstructorMustAcknowledgeReceiptOfExamRequests_AcknowledgeMessage);
						radioButtonList.Items[1].Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.INSTRUCTOR_InstructorMustAcknowledgeReceiptOfExamRequests_QuestionsMessage);
					}
				}
			}
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void gv_students_ItemCommand(object source, GridCommandEventArgs e)
		{
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0002F524 File Offset: 0x0002D724
		protected void gv_students_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
		{
			IList<StudentInTestWrapper> dataSource = this.LoadStudentsWritingThisTest();
			this.gv_students.DataSource = dataSource;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0002F548 File Offset: 0x0002D748
		protected void OnSideBarButtonClick(object sender, WizardNavigationEventArgs e)
		{
			try
			{
				WizardStepBase wizardStepBase = (e.CurrentStepIndex >= 0) ? this.Wizard1.WizardSteps[e.CurrentStepIndex] : this.step_details;
				WizardStepBase wizardStepBase2 = (e.NextStepIndex >= 0) ? this.Wizard1.WizardSteps[e.NextStepIndex] : this.step_details;
				bool flag = wizardStepBase == this.step_info;
				if (flag)
				{
					bool flag2 = e.CurrentStepIndex < e.NextStepIndex;
					if (flag2)
					{
						this.ViewState["passedvalidation"] = true;
					}
				}
				else
				{
					bool flag3 = wizardStepBase2 == this.step_submit;
					if (flag3)
					{
						bool flag4 = wizardStepBase == this.step_details || wizardStepBase == this.step_students;
						if (flag4)
						{
							object obj = this.ViewState["passedvalidation"];
							bool flag5 = obj != null && obj is bool && (bool)obj;
							bool flag6 = !flag5;
							if (flag6)
							{
								this.Wizard1.ActiveStepIndex = this.Wizard1.WizardSteps.IndexOf(this.step_info);
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0002F690 File Offset: 0x0002D890
		protected void Wizard1_ActiveStepChanged(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			CWLogger.Logger.Debug("Instructor:ExamUpload:WizardStepChanged:iid={0}:stepindex={1}", pid.ToString(), this.Wizard1.ActiveStepIndex.ToString());
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.INSTRUCTOR_InstructorConfirm_ShowStudentList);
			WizardStepBase activeStep = this.Wizard1.ActiveStep;
			string text = (activeStep == null) ? "" : activeStep.Title;
			bool flag = !string.IsNullOrEmpty(text);
			if (flag)
			{
				this.Page.Title = "Submit Test Info - " + text;
			}
			bool flag2 = this.Wizard1.ActiveStep == this.step_students;
			if (flag2)
			{
				bool flag3 = !settingValue;
				if (flag3)
				{
					Wizard wizard = this.Wizard1;
					int activeStepIndex = wizard.ActiveStepIndex;
					wizard.ActiveStepIndex = activeStepIndex + 1;
				}
				else
				{
					string text2;
					this.GetSelectedCourse(out text2);
					this.lbl_page2.Text = "2. Students scheduled to-date" + (string.IsNullOrEmpty(text2) ? "" : (" for " + text2));
				}
			}
			bool flag4 = this.Wizard1.ActiveStep == this.step_info;
			if (flag4)
			{
				string text2;
				this.GetSelectedCourse(out text2);
				this.lbl_page3.Text = "3. Test Information" + (string.IsNullOrEmpty(text2) ? "" : (" for " + text2));
			}
			bool flag5 = this.Wizard1.ActiveStep == this.step_submit;
			if (flag5)
			{
				int editingExamId = this.GetEditingExamId();
				bool flag6 = editingExamId > 0;
				if (flag6)
				{
					this.grid_previousuploads.Rebind();
				}
				string text2;
				int selectedCourse = this.GetSelectedCourse(out text2);
				this.lbl_summary_course.Text = text2;
				this.lbl_page4.Text = "4. Confirm exam details" + (string.IsNullOrEmpty(text2) ? "" : (" for " + text2));
				string value = this.datepicker.Value;
				DateTime minValue;
				bool flag7 = value.Length < 1 || !DateTime.TryParse(value, out minValue);
				if (flag7)
				{
					minValue = DateTime.MinValue;
				}
				bool flag8 = minValue != DateTime.MinValue;
				if (flag8)
				{
					string text3 = minValue.ToString("ddd MMMM d, yyyy");
					string value2 = this.startTime.Value;
					string value3 = this.endTime.Value;
					text3 = string.Concat(new string[]
					{
						text3,
						" . ",
						value2,
						" - ",
						value3
					});
					this.lbl_summary_testDateAndTime.Text = text3;
					DynamicControlLayoutHelper helper = new DynamicControlLayoutHelper();
					IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
					int settingValue2 = webSettingsClientManager.GetSettingValue<int>(Setting.INSTRUCTOR_uploadscreennum);
					DynamicScreenLayout.AddSummaryToLabel(this.lbl_summary_testInfo, this.p_data, settingValue2, 0, base.Cache, helper, "", true);
					string text4 = this.lbl_summary_testInfo.Text;
					int num = text4.IndexOf("<table");
					bool flag9 = num >= 0;
					if (flag9)
					{
						num = text4.IndexOf(">", num);
						bool flag10 = num > 0;
						if (flag10)
						{
							int num2 = text4.IndexOf("</table>", num);
							bool flag11 = num2 > 0;
							if (flag11)
							{
								num++;
								text4 = ((num2 == num) ? "" : text4.Substring(num, num2 - num));
							}
						}
					}
					bool flag12 = text4.Trim().Length > 0;
					if (flag12)
					{
						bool flag13 = !this.p_testInformation.Visible;
						if (flag13)
						{
							this.p_testInformation.Visible = true;
						}
					}
					else
					{
						bool visible = this.p_testInformation.Visible;
						if (visible)
						{
							this.p_testInformation.Visible = false;
						}
					}
				}
			}
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0002DC19 File Offset: 0x0002BE19
		protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
		{
			this.Submit();
		}

		// Token: 0x04000460 RID: 1120
		private bool? notAllowedToUploadTestsExams;

		// Token: 0x04000461 RID: 1121
		private IList<StudentInTestWrapper> lastStudents;

		// Token: 0x04000462 RID: 1122
		protected ScriptManager bbb;

		// Token: 0x04000463 RID: 1123
		protected Panel p_topmsg;

		// Token: 0x04000464 RID: 1124
		protected Image img_topmsg;

		// Token: 0x04000465 RID: 1125
		protected Label lbl_topmsg;

		// Token: 0x04000466 RID: 1126
		protected HiddenField hidden_InvalidFileFormatUploadMessage;

		// Token: 0x04000467 RID: 1127
		protected Wizard Wizard1;

		// Token: 0x04000468 RID: 1128
		protected WizardStep step_details;

		// Token: 0x04000469 RID: 1129
		protected Panel p_page1Title;

		// Token: 0x0400046A RID: 1130
		protected Label lblTitle;

		// Token: 0x0400046B RID: 1131
		protected Panel p_intro;

		// Token: 0x0400046C RID: 1132
		protected Label lbl_intro;

		// Token: 0x0400046D RID: 1133
		protected Panel p_coursedetails;

		// Token: 0x0400046E RID: 1134
		protected Panel p_existingCourse;

		// Token: 0x0400046F RID: 1135
		protected Label lbl_readOnlyCourseLabel;

		// Token: 0x04000470 RID: 1136
		protected Label lbl_readonlycourse;

		// Token: 0x04000471 RID: 1137
		protected Panel p_newCourse;

		// Token: 0x04000472 RID: 1138
		protected Label lbl_course;

		// Token: 0x04000473 RID: 1139
		protected ctrls_Instructor_CtrlInstructorCourseChooser ctrlInstructorCourseChooser1;

		// Token: 0x04000474 RID: 1140
		protected CheckBox chk_isFinalExam;

		// Token: 0x04000475 RID: 1141
		protected Label lbl_isFinalExam;

		// Token: 0x04000476 RID: 1142
		protected Label lblDateOfTestCustomTitle;

		// Token: 0x04000477 RID: 1143
		protected Label lbl_dateoftest3;

		// Token: 0x04000478 RID: 1144
		protected HtmlInputText datepicker;

		// Token: 0x04000479 RID: 1145
		protected HiddenField isDatePickerDisabled;

		// Token: 0x0400047A RID: 1146
		protected HtmlInputText startTime;

		// Token: 0x0400047B RID: 1147
		protected HtmlInputText endTime;

		// Token: 0x0400047C RID: 1148
		protected HiddenField originalExamDate;

		// Token: 0x0400047D RID: 1149
		protected HiddenField originalExamDuration;

		// Token: 0x0400047E RID: 1150
		protected WizardStep step_students;

		// Token: 0x0400047F RID: 1151
		protected Panel Panel1;

		// Token: 0x04000480 RID: 1152
		protected Label lbl_page2;

		// Token: 0x04000481 RID: 1153
		protected Label lbl_course2;

		// Token: 0x04000482 RID: 1154
		protected Panel p_instructions;

		// Token: 0x04000483 RID: 1155
		protected Label lbl_instructions;

		// Token: 0x04000484 RID: 1156
		protected RadGrid gv_students;

		// Token: 0x04000485 RID: 1157
		protected Panel p_instructorAcknowledgeReceiptOfTestRequests;

		// Token: 0x04000486 RID: 1158
		protected Label lbl_instructorAcknowledgePlease;

		// Token: 0x04000487 RID: 1159
		protected RadioButtonList rbtns_instructorAcknowledgeReceiptOfTestRequests;

		// Token: 0x04000488 RID: 1160
		protected RequiredFieldValidator required_instructorAcknowledge;

		// Token: 0x04000489 RID: 1161
		protected WizardStep step_info;

		// Token: 0x0400048A RID: 1162
		protected Panel Panel2;

		// Token: 0x0400048B RID: 1163
		protected Label lbl_page3;

		// Token: 0x0400048C RID: 1164
		protected Label lbl_course3;

		// Token: 0x0400048D RID: 1165
		protected Panel p_data;

		// Token: 0x0400048E RID: 1166
		protected Label lbl_userpreviousinfo;

		// Token: 0x0400048F RID: 1167
		protected DropDownList dp;

		// Token: 0x04000490 RID: 1168
		protected Button btn_usePreviousInfo;

		// Token: 0x04000491 RID: 1169
		protected WizardStep step_submit;

		// Token: 0x04000492 RID: 1170
		protected Panel Panel3;

		// Token: 0x04000493 RID: 1171
		protected Label lbl_page4;

		// Token: 0x04000494 RID: 1172
		protected Label lbl_course4;

		// Token: 0x04000495 RID: 1173
		protected Panel p_info;

		// Token: 0x04000496 RID: 1174
		protected Label lbl_submitinstructions;

		// Token: 0x04000497 RID: 1175
		protected Label lbl_contactInfo;

		// Token: 0x04000498 RID: 1176
		protected Panel p_testDetails;

		// Token: 0x04000499 RID: 1177
		protected Panel pdet;

		// Token: 0x0400049A RID: 1178
		protected Label lbl_summary_course;

		// Token: 0x0400049B RID: 1179
		protected Label lbl_summary_testDateAndTime;

		// Token: 0x0400049C RID: 1180
		protected Panel p_testInformation;

		// Token: 0x0400049D RID: 1181
		protected Label lbl_summary_testInfo;

		// Token: 0x0400049E RID: 1182
		protected Panel p_submitfile;

		// Token: 0x0400049F RID: 1183
		protected Label lbl_fileinstructions;

		// Token: 0x040004A0 RID: 1184
		protected Label lbl_selectFile;

		// Token: 0x040004A1 RID: 1185
		protected HtmlInputFile file1;

		// Token: 0x040004A2 RID: 1186
		protected Panel p_upload_exam_alreadyUploaded;

		// Token: 0x040004A3 RID: 1187
		protected Label lbl_upload_exam_alreadyUploaded_filename;

		// Token: 0x040004A4 RID: 1188
		protected Button btn_uploadAgain;

		// Token: 0x040004A5 RID: 1189
		protected RadGrid grid_previousuploads;

		// Token: 0x040004A6 RID: 1190
		protected Label lbl_submitreminder;

		// Token: 0x040004A7 RID: 1191
		protected LinkButton link_submitChanges;

		// Token: 0x040004A8 RID: 1192
		protected Label lbl_submitreminder2;

		// Token: 0x040004A9 RID: 1193
		protected LinkButton btn_print3;
	}
}
