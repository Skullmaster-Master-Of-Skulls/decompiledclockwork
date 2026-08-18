using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkLogger;
using ClockWorkWebAPI;
using ClockWorkWebAPIWeb;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;
using TechnoPro.Common.ClientManager.Core.ServiceProviderOriginal;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.ServiceProviderOriginal;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.ClockWork.Controls;
using TechnoPro.Common.UI.Web.Entity;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.NotetakingNotetakers
{
	// Token: 0x020000AB RID: 171
	public class user_NotetakingNotetakers_NotetakerApp : Page
	{
		// Token: 0x06000557 RID: 1367 RVA: 0x00026B4C File Offset: 0x00024D4C
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetNotetakerId(this.Page);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00026B70 File Offset: 0x00024D70
		protected void CtrlTermChooser1_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			SessionView selectedSession = this.CtrlTermChooser1.SelectedSession;
			bool flag = selectedSession != null;
			if (flag)
			{
				this.gv_courses.Rebind();
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00026BA0 File Offset: 0x00024DA0
		protected void ctrlTermChooser1_OnUserInfoRequested(object sender, UserInfoForCourseArgs e)
		{
			e.Info.NotetakerId = this.GetPid();
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00026BB8 File Offset: 0x00024DB8
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
			if (flag)
			{
				((IClockWorkMasterPage)this.Page.Master).SetCurrentPage(eClockWorkWebPage.NotetakingNotetakers_Courses);
			}
			this.p_topmsg.Visible = false;
			int pid = this.GetPid();
			bool flag2 = pid <= 0;
			if (flag2)
			{
				CWLogger.Logger.Info("Notetaking:NotetakerApp.aspx:msg=Student is logged in as '{0}', but does not have a notetaker id.  Sending them to the new notetaker application page...", "");
				base.Response.Redirect("NotetakerAppNew.aspx", true);
			}
			bool flag3 = !this.Page.IsPostBack;
			if (flag3)
			{
				bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.NOTETAKINGB_NotetakerOnlyAllowAccessIfRegistrationIsComplete);
				bool flag4 = settingValue;
				if (flag4)
				{
					IServiceProviderOriginalProviderClientManager serviceProviderOriginalProviderClientManager = new ServiceProviderOriginalProviderClientManager();
					ServiceProviderBaseDTO serviceProviderBaseDTO = serviceProviderOriginalProviderClientManager.LoadProviderBaseById(pid);
					bool flag5 = serviceProviderBaseDTO == null || !serviceProviderBaseDTO.RegistrationIsComplete;
					if (flag5)
					{
						base.Response.Redirect("Message.aspx?msgcode=registrationIncomplete", true);
						return;
					}
				}
				this.ShowMessage();
				this.lbl_confidentialityAgreementReminder.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_ConfidentialityAgreement);
				this.lbl_additionalInfo.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_AdditionalInfoNotetaker);
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00026D04 File Offset: 0x00024F04
		private string sampleNotesWording
		{
			get
			{
				string result;
				if ((result = this._sampleNotesWording) == null)
				{
					result = (this._sampleNotesWording = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_SampleNotesWording));
				}
				return result;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x00026D38 File Offset: 0x00024F38
		public string UploadSampleNotesWording
		{
			get
			{
				return "Upload " + this.sampleNotesWording;
			}
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00026D5C File Offset: 0x00024F5C
		private void GetSelectedTermDates(out DateTime startDate, out DateTime endDate)
		{
			SessionView sessionView;
			if ((sessionView = this.CtrlTermChooser1.SelectedSession) == null)
			{
				SessionView sessionView2 = new SessionView();
				sessionView2.StartDate = DateTime.Now;
				sessionView = sessionView2;
				sessionView2.EndDate = DateTime.Now;
			}
			SessionView sessionView3 = sessionView;
			startDate = sessionView3.StartDate;
			endDate = sessionView3.EndDate;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00026DB0 File Offset: 0x00024FB0
		private DataTable LoadCourses(int notetakerId)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DateTime dateTime;
			DateTime dateTime2;
			this.GetSelectedTermDates(out dateTime, out dateTime2);
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@id", DbType.Int32, notetakerId),
				clockWork.GetParameter("@sdate", DbType.DateTime, dateTime),
				clockWork.GetParameter("@edate", DbType.DateTime, dateTime2)
			};
			DataTable dataTable = clockWork.ExecuteQuery(ClockWorkWebAPI.QueryStorage.QS_Select_NotetakerCourses, parameters);
			DataTable dataTable2 = new DataTable();
			dataTable2.Columns.Add("lucourseid", typeof(int));
			dataTable2.Columns.Add("CourseDescription");
			dataTable2.Columns.Add("notetakerassigned", typeof(bool));
			dataTable2.Columns.Add("notetakerapplied", typeof(bool));
			dataTable2.Columns.Add("samplenotescount", typeof(int));
			dataTable2.Columns.Add("numstudents", typeof(int));
			dataTable2.Columns.Add("subject");
			dataTable2.Columns.Add("course");
			dataTable2.Columns.Add("section");
			dataTable2.Columns.Add("term");
			dataTable2.Columns.Add("duration");
			dataTable2.Columns.Add("timeofday");
			dataTable2.Columns.Add("startdate", typeof(DateTime));
			dataTable2.Columns.Add("enddate", typeof(DateTime));
			int i = 0;
			List<Course> list = new List<Course>();
			while (i < dataTable.Rows.Count)
			{
				DataRow dataRow = dataTable.Rows[i];
				int num = (int)dataRow["lucourseid"];
				int j;
				for (j = i + 1; j < dataTable.Rows.Count; j++)
				{
					int num2 = (int)dataTable.Rows[j]["lucourseid"];
					bool flag = num2 != num;
					if (flag)
					{
						break;
					}
				}
				DataRow dataRow2 = dataTable2.NewRow();
				dataRow2["lucourseid"] = dataRow["lucourseid"];
				dataRow2["CourseDescription"] = dataRow["CourseDescription"];
				dataRow2["notetakerassigned"] = (dataRow["serviceproviderrequestid"] != DBNull.Value);
				dataRow2["notetakerapplied"] = true;
				dataRow2["samplenotescount"] = 0;
				dataRow2["numstudents"] = j - i;
				dataRow2["subject"] = dataRow["subject"];
				dataRow2["course"] = dataRow["course"];
				dataRow2["section"] = dataRow["section"];
				dataRow2["term"] = dataRow["term"];
				dataRow2["duration"] = dataRow["duration"];
				dataRow2["timeofday"] = dataRow["timeofday"];
				dataRow2["startdate"] = dataRow["startdate"];
				dataRow2["enddate"] = dataRow["enddate"];
				dataTable2.Rows.Add(dataRow2);
				Course item = new Course(dataRow);
				list.Add(item);
				i = j;
			}
			this.cmb_existingcourses.Items.Clear();
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.NOTETAKINGB_AllowNotetakersToCancelThemselves);
			bool flag2 = settingValue && dataTable2.Rows.Count > 0;
			if (flag2)
			{
				bool flag3 = !this.p_existingcourses.Visible;
				if (flag3)
				{
					this.p_existingcourses.Visible = true;
				}
				this.cmb_existingcourses.Items.Add(new ListItem("", "0"));
				foreach (object obj in dataTable2.Rows)
				{
					DataRow dataRow3 = (DataRow)obj;
					this.cmb_existingcourses.Items.Add(new ListItem(dataRow3["coursedescription"].ToString(), dataRow3["lucourseid"].ToString()));
				}
			}
			else
			{
				this.p_existingcourses.Visible = false;
			}
			return dataTable2;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x000272C4 File Offset: 0x000254C4
		private void ShowMessage()
		{
			object obj = this.Session["msgcode"];
			bool flag = obj != null;
			if (flag)
			{
				string msgCode = (string)obj;
				object obj2 = this.Session["msgcodedesc"];
				string msgCodeDescription = (obj2 == null) ? "" : ((string)obj2);
				this.ShowMessage(msgCode, msgCodeDescription);
				this.Session["msgcode"] = null;
				this.Session["msgcodedesc"] = null;
			}
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00027348 File Offset: 0x00025548
		private void ShowMessage(string msgCode, string msgCodeDescription)
		{
			if (!(msgCode == "becomeavailable"))
			{
				if (!(msgCode == "becomeunavailable"))
				{
					if (!(msgCode == "cantcreateaccount"))
					{
						if (!(msgCode == "accountcreated"))
						{
							if (!(msgCode == "accountupdated"))
							{
								if (msgCode == "registrationIncomplete")
								{
									this.lbl_topmsg.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_Message_RegistrationIncomplete);
									this.p_topmsg.Visible = true;
								}
							}
							else
							{
								this.lbl_topmsg.Text = "Your profile was successfully updated.";
								this.p_topmsg.Visible = true;
							}
						}
						else
						{
							string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_newNotetakerWelcomeMessage);
							this.lbl_topmsg.Text = settingValue;
							this.p_topmsg.Visible = true;
						}
					}
					else
					{
						this.lbl_topmsg.Text = "Unable to create an account for you.";
						this.p_topmsg.Visible = true;
					}
				}
				else
				{
					this.lbl_topmsg.Text = "Successfuly marked you as NO LONGER AVAILABLE as a notetaker for " + this.GetCourseDescription(msgCodeDescription);
					this.p_topmsg.Visible = true;
				}
			}
			else
			{
				this.lbl_topmsg.Text = "Successfully marked you as available to become a notetaker for " + this.GetCourseDescription(msgCodeDescription) + ". You will receive a confirmation email shortly.";
				this.p_topmsg.Visible = true;
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x000274B0 File Offset: 0x000256B0
		private string GetCourseDescription(string luCourseId)
		{
			bool flag = luCourseId.Trim().Length <= 0;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				int num;
				int.TryParse(luCourseId, out num);
				result = ((num > 0) ? this.GetCourseDescription(num) : "");
			}
			return result;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000274FC File Offset: 0x000256FC
		private string GetCourseDescription(int luCourseId)
		{
			bool flag = this.gv_courses.DataSource != null;
			if (flag)
			{
				DataTable dataTable = (DataTable)this.gv_courses.DataSource;
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					int num = (int)dataRow["lucourseid"];
					bool flag2 = num == luCourseId;
					if (flag2)
					{
						return dataRow["course"].ToString();
					}
				}
			}
			return "";
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x000275BC File Offset: 0x000257BC
		protected void gv_course_ItemCommand(object sender, GridCommandEventArgs e)
		{
			object commandArgument = e.CommandArgument;
			bool flag = commandArgument != null;
			int num;
			if (flag)
			{
				string text = commandArgument.ToString().Trim();
				bool flag2 = text.Length > 0;
				if (flag2)
				{
					try
					{
						string[] array = text.Split(new char[]
						{
							','
						});
						num = int.Parse(array[0]);
					}
					catch
					{
						num = 0;
					}
				}
				else
				{
					num = 0;
				}
			}
			else
			{
				num = 0;
			}
			bool flag3 = string.Compare(e.CommandName, "lecturenotes", StringComparison.Ordinal) == 0;
			if (flag3)
			{
				base.Response.Redirect("notesnotetaker.aspx?lucid=" + NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(num));
			}
			else
			{
				bool flag4 = e.CommandName.Equals("uploadsample");
				if (flag4)
				{
					base.Response.Redirect("SampleNotesNotetaker.aspx?lucid=" + NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(num));
				}
			}
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x000276B4 File Offset: 0x000258B4
		protected void gv_course_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
		{
			int pid = this.GetPid();
			DataTable dataSource = (pid > 0) ? this.LoadCourses(pid) : null;
			this.gv_courses.DataSource = dataSource;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000276E8 File Offset: 0x000258E8
		protected void btn_remove_Click(object sender, EventArgs e)
		{
			IEncryption encryption = DatabaseLayerFactory.ClockWork.Encryption;
			string selectedValue = this.cmb_existingcourses.SelectedValue;
			string text = this.cmb_existingcourses.SelectedItem.Text;
			bool flag = !string.IsNullOrEmpty(selectedValue);
			int num;
			if (flag)
			{
				try
				{
					num = int.Parse(selectedValue);
				}
				catch
				{
					num = 0;
				}
			}
			else
			{
				num = 0;
			}
			bool flag2 = num > 0;
			if (flag2)
			{
				base.Response.Redirect("DontRequireNotetaker.aspx?lucid=" + NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(num) + "&cd=" + ClockWorkWebCore.EncodeUrlVariable(text, true, encryption));
			}
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00027790 File Offset: 0x00025990
		public bool AllowNotetakersToUploadSampleNotes()
		{
			bool flag = this.allowNotetakersToUploadSampleNotes == 0;
			if (flag)
			{
				this.allowNotetakersToUploadSampleNotes = (new WebSettingsClientManager().GetSettingValue<bool>(Setting.NOTETAKINGB_AllowNotetakersToUploadSampleNotes) ? 1 : -1);
			}
			return this.allowNotetakersToUploadSampleNotes == 1;
		}

		// Token: 0x0400033B RID: 827
		private string _sampleNotesWording;

		// Token: 0x0400033C RID: 828
		private int allowNotetakersToUploadSampleNotes = 0;

		// Token: 0x0400033D RID: 829
		protected ScriptManager bbb;

		// Token: 0x0400033E RID: 830
		protected Label lblTitle;

		// Token: 0x0400033F RID: 831
		protected Panel p_topmsg;

		// Token: 0x04000340 RID: 832
		protected Image img_topmsg;

		// Token: 0x04000341 RID: 833
		protected Label lbl_topmsg;

		// Token: 0x04000342 RID: 834
		protected Table Table1;

		// Token: 0x04000343 RID: 835
		protected Label Label1;

		// Token: 0x04000344 RID: 836
		protected CtrlTermChooser CtrlTermChooser1;

		// Token: 0x04000345 RID: 837
		protected Panel p_courseList;

		// Token: 0x04000346 RID: 838
		protected RadGrid gv_courses;

		// Token: 0x04000347 RID: 839
		protected Panel p_additionalcourses;

		// Token: 0x04000348 RID: 840
		protected Button btn_addPotentialCourse;

		// Token: 0x04000349 RID: 841
		protected Panel p_existingcourses;

		// Token: 0x0400034A RID: 842
		protected Label lbl_existingcourses;

		// Token: 0x0400034B RID: 843
		protected DropDownList cmb_existingcourses;

		// Token: 0x0400034C RID: 844
		protected Button btn_remove;

		// Token: 0x0400034D RID: 845
		protected Panel p_additionalInfo;

		// Token: 0x0400034E RID: 846
		protected Label lbl_additionalInfo;

		// Token: 0x0400034F RID: 847
		protected Panel p_confidentialityAgreementReminder;

		// Token: 0x04000350 RID: 848
		protected Label lbl_confidentialityAgreementReminder;
	}
}
