using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using Databases;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.Core.AppointmentsTestBooking;
using TechnoPro.Common.ClientManager.Core.LookupCourses;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.instructor
{
	// Token: 0x020000E1 RID: 225
	public class user_instructor_UploadedExams : Page
	{
		// Token: 0x060006B3 RID: 1715 RVA: 0x000331BC File Offset: 0x000313BC
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetInstructorId(this.Page);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x000331E0 File Offset: 0x000313E0
		private int GetAltContactId()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetAltContactId(this.Page);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00003E0A File Offset: 0x0000200A
		private void SetCurrentPageOnMenu(int index)
		{
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00033204 File Offset: 0x00031404
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
				bool flag2 = !this.Page.IsPostBack;
				if (flag2)
				{
					string text = base.Request.QueryString["reason"] ?? "";
					bool flag3 = text.Length > 0;
					if (flag3)
					{
						int num;
						bool flag4 = int.TryParse(text, out num) && Enum.IsDefined(typeof(eCantEditTestExamInfoReason), num);
						if (flag4)
						{
							switch (num)
							{
							case 2:
								this.ShowMessage("The test information you were trying to edit cannot be modified because the cutoff time has passed.  Please call or email our office.");
								break;
							case 3:
								this.ShowMessage("The test information you were trying to edit cannot be modified because you are not authorized to do this.  Please call or email our office.");
								break;
							case 4:
								this.ShowMessage("You cannot add a new test/exam for this course because the course has ended.  Please call or email our office.");
								break;
							}
						}
					}
					this.SetCurrentPageOnMenu(1);
					string text2 = base.Request.QueryString["cutoff"];
					bool flag5 = text2 != null && text2.Equals("1");
					if (flag5)
					{
						this.ShowMessage("Unfortunately the cutoff has passed for changing this online.  Please contact us via email or phone.");
					}
				}
				bool flag6 = !this.Page.IsPostBack;
				if (flag6)
				{
					bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.INSTRUCTOR_HideAddTestOption);
					bool flag7 = settingValue;
					if (flag7)
					{
						this.p_add.Visible = false;
					}
					string settingValue2 = new WebSettingsClientManager().GetSettingValue<string>(Setting.INSTRUCTOR_ExamListInstructionsText);
					bool flag8 = !string.IsNullOrEmpty(settingValue2);
					if (flag8)
					{
						this.lbl_intro.Text = settingValue2;
					}
					else
					{
						this.p_intro.Visible = false;
					}
					int lucid = this.GetLucid();
					ILookupCourseClientManager lookupCourseClientManager = new LookupCourseClientManager();
					LookupCourseDTO lookupCourseDTO = lookupCourseClientManager.LoadCourseByLuCourseId(lucid);
					bool flag9 = lookupCourseDTO != null;
					if (flag9)
					{
						this.lbl_course.Text = lookupCourseDTO.GetCourseDescription();
						this.lbl_courseDates.Style.Add(HtmlTextWriterStyle.FontSize, ".7em");
						this.lbl_courseDates.Style.Add(HtmlTextWriterStyle.FontStyle, "italic");
						this.lbl_courseDates.Text = string.Format(" ({0} to {1})", lookupCourseDTO.StartDate.ToString("yyyy MMM d"), lookupCourseDTO.EndDate.ToString("MMM d"));
					}
				}
			}
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00033478 File Offset: 0x00031678
		private int GetLucid()
		{
			return NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["lucid"] ?? "");
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x000334B4 File Offset: 0x000316B4
		protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
		{
			int pid = this.GetPid();
			int altContactId = this.GetAltContactId();
			int lucid = this.GetLucid();
			bool flag = altContactId > 0 && pid > 0;
			DataTable t;
			if (flag)
			{
				t = Course.LoadUploadedExams(lucid, pid);
				DataTable dataTable = this.LoadExamsAltContact(lucid, altContactId);
				List<DataRow> list = (from DataRow drx in dataTable.Rows
				let drs = t.Select("examid=" + drx["examid"].ToString())
				where drs.Length < 1
				select drx).ToList<DataRow>();
				foreach (DataRow row in list)
				{
					t.ImportRow(row);
				}
			}
			else
			{
				bool flag2 = pid > 0;
				if (flag2)
				{
					t = Course.LoadUploadedExams(lucid, pid);
				}
				else
				{
					bool flag3 = altContactId > 0;
					if (flag3)
					{
						t = this.LoadExamsAltContact(lucid, altContactId);
					}
					else
					{
						t = new DataTable();
						t.Columns.Add("examid", typeof(int));
						t.Columns.Add("dateentered", typeof(DateTime));
						t.Columns.Add("whoentered", typeof(int));
						t.Columns.Add("lucourseid", typeof(int));
						t.Columns.Add("description");
						t.Columns.Add("submitted", typeof(bool));
						t.Columns.Add("dateoftest", typeof(DateTime));
						t.Columns.Add("lastmodified", typeof(DateTime));
						t.Columns.Add("wholastmodified", typeof(int));
						t.Columns.Add("coursedescription");
						t.Columns.Add("enddate", typeof(DateTime));
						t.Columns.Add("testduration", typeof(int));
						t.Columns.Add("typecode");
						t.Columns.Add("testtype");
						t.Columns.Add("HasFile", typeof(bool));
					}
				}
			}
			this.RadGrid1.DataSource = t;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x000337D8 File Offset: 0x000319D8
		private DataTable LoadExamsAltContact(int lucid, int altContactId)
		{
			DataTable dataTable = Course.LoadUploadedExamsByAltContact(lucid, altContactId);
			List<DataRow> list = new List<DataRow>();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				bool flag = dataRow["altpermissionlevel"] == DBNull.Value;
				if (flag)
				{
					list.Add(dataRow);
				}
				else
				{
					int num = (int)dataRow["altpermissionlevel"];
					bool flag2 = (num & 2) != 2;
					if (flag2)
					{
						list.Add(dataRow);
					}
				}
			}
			foreach (DataRow row in list)
			{
				dataTable.Rows.Remove(row);
			}
			bool flag3 = dataTable.Columns.Contains("altpermissionlevel");
			if (flag3)
			{
				dataTable.Columns.Remove("altpermissionlevel");
			}
			return dataTable;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00033904 File Offset: 0x00031B04
		public string CombineExamIdExamFileId(string examidstr, string examfileidstr)
		{
			return examidstr + "," + ((examfileidstr.Length > 0) ? examfileidstr : "0");
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00033934 File Offset: 0x00031B34
		protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			object commandArgument = e.CommandArgument;
			int num = 0;
			bool flag = commandArgument != null;
			if (flag)
			{
				string text = commandArgument.ToString().Trim();
				bool flag2 = text.Length > 0;
				if (flag2)
				{
					bool flag3 = !int.TryParse(text, out num);
					if (flag3)
					{
						num = 0;
					}
				}
			}
			int lucid = this.GetLucid();
			string commandName = e.CommandName;
			if (!(commandName == "edit"))
			{
				if (!(commandName == "editfinal"))
				{
					if (commandName == "editfile")
					{
						string url = string.Format("ExamUpload.aspx?files=1&examid={0}&lucid={1}", NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(num), NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(lucid));
						base.Response.Redirect(url, true);
					}
				}
				else
				{
					string url = string.Format(this.EXAMBOOKING_FinalExamRequest_Enabled ? "FinalExamUpload.aspx?examid={0}&lucid={1}" : "ExamUpload.aspx?examid={0}&lucid={1}", NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(num), NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(lucid));
					base.Response.Redirect(url, true);
				}
			}
			else
			{
				string url = string.Format("ExamUpload.aspx?examid={0}&lucid={1}", NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(num), NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(lucid));
				base.Response.Redirect(url, true);
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void RadGrid1_ColumnCreating(object sender, GridColumnCreatingEventArgs e)
		{
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x00033A7C File Offset: 0x00031C7C
		private CutoffTime CutoffForUpdatingTests
		{
			get
			{
				bool flag = this.cutoffForUpdatingTests == null;
				if (flag)
				{
					string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.INSTRUCTOR_CutoffForUpdatingTests);
					this.cutoffForUpdatingTests = settingValue.CutoffTimeFromXml();
				}
				return this.cutoffForUpdatingTests;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x00033AC0 File Offset: 0x00031CC0
		private bool EXAMBOOKING_FinalExamRequest_Enabled
		{
			get
			{
				bool flag = this.finalExamRequest_Enabled == null;
				if (flag)
				{
					this.finalExamRequest_Enabled = new bool?(new WebSettingsClientManager().GetSettingValue<bool>(Setting.EXAMBOOKING_FinalExamRequest_Enabled));
					bool value = this.finalExamRequest_Enabled.Value;
					if (value)
					{
						bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.INSTRUCTOR_DisableExamRequestInterfaceForInstructors);
						bool flag2 = settingValue;
						if (flag2)
						{
							this.finalExamRequest_Enabled = new bool?(false);
						}
					}
				}
				return this.finalExamRequest_Enabled.Value;
			}
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00033B40 File Offset: 0x00031D40
		protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
		{
			bool flag = e.Item is GridDataItem;
			if (flag)
			{
				GridDataItem gridDataItem = e.Item as GridDataItem;
				TableCell tableCell = gridDataItem["col_datetime"];
				bool flag2 = tableCell != null;
				if (flag2)
				{
					tableCell.Attributes["scope"] = "row";
				}
			}
			CutoffTime cutoffTime = this.CutoffForUpdatingTests;
			bool flag3 = e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item;
			if (flag3)
			{
				GridDataItem gridDataItem2 = (GridDataItem)e.Item;
				bool flag4 = gridDataItem2.DataItem != null && gridDataItem2.DataItem is DataRowView;
				if (flag4)
				{
					DataRow row = ((DataRowView)gridDataItem2.DataItem).Row;
					DateTime contextDateTime = (DateTime)row["dateoftest"];
					bool? flag5 = cutoffTime.IsRightNowBeforeCutoffTime(contextDateTime);
					bool flag6 = flag5 == null;
					if (flag6)
					{
						bool flag7 = row["enddate"] is DBNull;
						if (flag7)
						{
							flag5 = new bool?(false);
						}
						else
						{
							DateTime dateTime = (DateTime)row["enddate"];
							int settingValue = new WebSettingsClientManager().GetSettingValue<int>(Setting.INSTRUCTOR_TestExamCourseEndDateAuthorizationExtensionInDays);
							flag5 = new bool?(dateTime.Date.AddDays((double)settingValue) >= DateTime.Now.Date);
						}
					}
					bool value = flag5.Value;
					bool flag8 = !value;
					if (flag8)
					{
						LinkButton linkButton = (LinkButton)gridDataItem2["col_who"].FindControl("btn_edit");
						bool flag9 = linkButton != null;
						if (flag9)
						{
							linkButton.Text = "Passed cutoff - please call or email";
							linkButton.Enabled = false;
							linkButton.ToolTip = "The cutoff date has passed for updating.  Please call or email.";
						}
					}
					else
					{
						bool flag10 = this.EXAMBOOKING_FinalExamRequest_Enabled && row["typecode"].ToString().ToLower().Equals("f");
						if (flag10)
						{
							LinkButton linkButton2 = (LinkButton)gridDataItem2["col_who"].FindControl("btn_edit");
							linkButton2.Visible = false;
							LinkButton linkButton3 = (LinkButton)gridDataItem2["col_who"].FindControl("btn_editFinal");
							linkButton3.Visible = true;
							string text = row["description"].ToString();
							bool flag11 = !string.IsNullOrEmpty(text);
							if (flag11)
							{
								Label label = (Label)gridDataItem2["col_datetime"].FindControl("lbl_choices");
								Label label2 = (Label)gridDataItem2["col_datetime"].FindControl("lbl_date");
								Label label3 = (Label)gridDataItem2["col_datetime"].FindControl("lbl_time");
								bool flag12 = label2 != null;
								if (flag12)
								{
									label2.Visible = false;
								}
								bool flag13 = label3 != null;
								if (flag13)
								{
									label3.Visible = false;
								}
								bool flag14 = label != null;
								if (flag14)
								{
									label.Visible = true;
									label.Text = text;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00033E68 File Offset: 0x00032068
		protected void btn_addTest_Click(object sender, EventArgs e)
		{
			int lucid = this.GetLucid();
			bool flag = lucid < 1;
			if (flag)
			{
				this.ShowMessage("Invalid course id.");
			}
			else
			{
				string text = (this.datepicker.Value ?? "").Trim();
				DateTime dt;
				bool flag2 = text.Length < 1 || !DateTime.TryParse(text, out dt) || dt == DateTime.MinValue;
				if (flag2)
				{
					this.ShowMessage("Please select a date first.");
				}
				else
				{
					IClassTestDefinitionClientManager classTestDefinitionClientManager = new ClassTestDefinitionClientManager();
					IList<ClassTestDTO> source = classTestDefinitionClientManager.LoadClassTestDefinitionsByCourse(lucid);
					ClassTestDTO classTestDTO = source.FirstOrDefault((ClassTestDTO g) => g.StartDateTime.Date == dt.Date);
					bool flag3 = classTestDTO != null;
					if (flag3)
					{
						this.ShowMessage("This class test already exists on this date; nothing was done.");
					}
					else
					{
						string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.INSTRUCTOR_CutoffNewClasTestCreateDate);
						CutoffTime cutoffTime = (settingValue ?? "").CutoffTimeFromXml();
						bool flag4 = cutoffTime != null && cutoffTime.Enabled;
						if (flag4)
						{
							DateTime? minimumDateForBeforeTypeCutoff = cutoffTime.GetMinimumDateForBeforeTypeCutoff();
							bool flag5 = dt < minimumDateForBeforeTypeCutoff;
							if (flag5)
							{
								this.ShowMessage("The date you have entered is too close to todays date.  Please contact us to tell us about this new test or exam.");
								return;
							}
						}
						string arg = NavigatorClientManager.CurrentInstance.ConvertIntParameterToUrlString(lucid);
						base.Response.Redirect(string.Format("ExamUpload.aspx?dt={0}&lucid={1}&newtest=1", dt, arg), true);
					}
				}
			}
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00033FEA File Offset: 0x000321EA
		private void ShowMessage(string msg)
		{
			this.p_msg.Visible = true;
			this.lbl_msg.Text = msg;
		}

		// Token: 0x04000514 RID: 1300
		private CutoffTime cutoffForUpdatingTests = null;

		// Token: 0x04000515 RID: 1301
		private bool? finalExamRequest_Enabled = null;

		// Token: 0x04000516 RID: 1302
		protected Panel p_title;

		// Token: 0x04000517 RID: 1303
		protected Label lblTitle;

		// Token: 0x04000518 RID: 1304
		protected Label lbl_course;

		// Token: 0x04000519 RID: 1305
		protected Label lbl_courseDates;

		// Token: 0x0400051A RID: 1306
		protected Panel p_msg;

		// Token: 0x0400051B RID: 1307
		protected Panel p_msg2;

		// Token: 0x0400051C RID: 1308
		protected Label lbl_msg;

		// Token: 0x0400051D RID: 1309
		protected Panel p_intro;

		// Token: 0x0400051E RID: 1310
		protected Label lbl_intro;

		// Token: 0x0400051F RID: 1311
		protected Panel p_add;

		// Token: 0x04000520 RID: 1312
		protected HtmlInputText datepicker;

		// Token: 0x04000521 RID: 1313
		protected LinkButton btn_addTest2;

		// Token: 0x04000522 RID: 1314
		protected Label lbl_gridtitle;

		// Token: 0x04000523 RID: 1315
		protected RadGrid RadGrid1;

		// Token: 0x04000524 RID: 1316
		protected Button btn_backToCourses;
	}
}
