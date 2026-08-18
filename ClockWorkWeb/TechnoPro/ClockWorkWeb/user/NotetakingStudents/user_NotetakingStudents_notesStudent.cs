using System;
using System.Data;
using System.Data.Common;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkLogger;
using Databases;
using TechnoPro.Common.ClientManager.Core.Notetaking;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Notetaking;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Notetaking;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.Notetaking;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.NotetakingStudents
{
	// Token: 0x02000098 RID: 152
	public class user_NotetakingStudents_notesStudent : Page
	{
		// Token: 0x060004ED RID: 1261 RVA: 0x00023CB0 File Offset: 0x00021EB0
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00023CD4 File Offset: 0x00021ED4
		protected void gv_courses_ItemDataBound(object sender, GridItemEventArgs e)
		{
			bool flag = e.Item is GridPagerItem;
			if (flag)
			{
				Label label = (Label)e.Item.FindControl("ChangePageSizeLabel");
				RadComboBox radComboBox = (RadComboBox)e.Item.FindControl("PageSizeComboBox");
				bool flag2 = label != null && radComboBox != null;
				if (flag2)
				{
					label.Visible = false;
					radComboBox.Label = "Page_size:";
				}
			}
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00023D48 File Offset: 0x00021F48
		protected void Page_Load(object sender, EventArgs e)
		{
			this.lbl_DownloadLectureNotesInfo.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_DownloadLectureNotesInfo);
			int pid = this.GetPid();
			bool flag = pid < 1;
			if (flag)
			{
				NavigatorClientManager.CurrentInstance.NotAllowed(Setting.GENERAL_ErrorMessage_NotAClockWorkStudent, this.Page);
			}
			else
			{
				bool flag2 = !this.Page.IsPostBack;
				if (flag2)
				{
					string stringFromUrlParameter = NavigatorClientManager.CurrentInstance.GetStringFromUrlParameter("cd");
					this.lblSampleNotesCourse.Text = stringFromUrlParameter;
				}
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00021599 File Offset: 0x0001F799
		protected void btn_backToNotetakee_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("courses.aspx");
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00023DCC File Offset: 0x00021FCC
		private int Lucourseid
		{
			get
			{
				return NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["lucid"] ?? "");
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00023E08 File Offset: 0x00022008
		protected void gv_courses_ItemCommand(object source, GridCommandEventArgs e)
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
						num = int.Parse(text);
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
			string commandName = e.CommandName;
			if (commandName == "download")
			{
				INotetakingWebClientManager notetakingWebClientManager = new NotetakingWebClientManager();
				bool flag3 = notetakingWebClientManager.DownloadLectureNoteToBrowser(num);
				bool flag4 = !flag3;
				if (flag4)
				{
					CWLogger.Logger.Warn("NotetakingStudents.notesStudent.aspx:Downloading note failed");
				}
				else
				{
					try
					{
						int pid = this.GetPid();
						INotetakingClientManager notetakingClientManager = new NotetakingClientManager();
						notetakingClientManager.RecordStudentDownloadedLectureNote(pid, num);
					}
					catch (Exception ex)
					{
						CWLogger.Logger.Error("NotetakingStudents.notesStudent.aspx:FailedToRecordStudentDownloadedLectureNote:Error={0}", ex.ToString());
					}
				}
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00023F04 File Offset: 0x00022104
		protected void link_history_Click(object sender, EventArgs e)
		{
			int lucourseid = this.Lucourseid;
			bool flag = lucourseid > 0;
			if (flag)
			{
				string text = this.lblSampleNotesCourse.Text;
				string url = "DownloadHistory.aspx?lucid=" + NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(lucourseid) + "&cd=" + NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(text);
				base.Response.Redirect(url);
			}
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00023F64 File Offset: 0x00022164
		protected void gv_courses_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
		{
			int pid = this.GetPid();
			int lucourseid = this.Lucourseid;
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.NOTETAKINGB_AllowStudentsToSeeNotetakerContactInfoAndName);
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@pid", DbType.Int32, pid),
				clockWork.GetParameter("@lucid", DbType.Int32, lucourseid),
				clockWork.GetParameter("@equiv", DbType.Int32, new WebSettingsClientManager().GetSettingValue<int>(Setting.NOTETAKINGB_EquivalentCourseStoredProcedureNumber)),
				clockWork.GetParameter("@allowothernotetakers", DbType.Boolean, new WebSettingsClientManager().GetSettingValue<bool>(Setting.NOTETAKINGB_AllowStudentsToAccessNotesFromOtherNotetakers)),
				clockWork.GetParameter("@allowunassignednotetakers", DbType.Boolean, new WebSettingsClientManager().GetSettingValue<bool>(Setting.NOTETAKINGB_AllowStudentsToAccessNotesFromOtherNotetakers_IncludeUnassignedNotetakersNotes)),
				clockWork.GetParameter("@showsamplenotes", DbType.Boolean, new WebSettingsClientManager().GetSettingValue<bool>(Setting.NOTETAKINGB_ShowSampleNotesInDownloadNotesList)),
				clockWork.GetParameter("@allowStudentToGetNotesEvenIfNoAssignedNotetaker", DbType.Boolean, new WebSettingsClientManager().GetSettingValue<bool>(Setting.NOTETAKINGB_AllowStudentsToAccessNotesEvenIfTheyDontHaveAnAssignedNotetaker))
			};
			DataTable dataTable = clockWork.ExecuteQuery("EXEC sp_Notetaking_StudentNotes @pid,@lucid,@equiv,@allowothernotetakers,@allowunassignednotetakers,@showsamplenotes,@allowStudentToGetNotesEvenIfNoAssignedNotetaker", parameters);
			bool flag = settingValue;
			if (flag)
			{
				dataTable = clockWork.Encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
				{
					"firstname",
					"lastname"
				});
			}
			DataTable dataTable2 = new DataTable();
			dataTable2.Columns.Add("fileid", typeof(int));
			dataTable2.Columns.Add("lucourseid", typeof(int));
			dataTable2.Columns.Add("Course");
			dataTable2.Columns.Add("description");
			dataTable2.Columns.Add("lecturedate", typeof(DateTime));
			dataTable2.Columns.Add("downloadeddate", typeof(DateTime));
			dataTable2.Columns.Add("nonotes", typeof(bool));
			dataTable2.Columns.Add("docname");
			dataTable2.Columns.Add("hasnotes", typeof(bool));
			dataTable2.Columns.Add("notetakertitle");
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				DataRow dataRow2 = dataTable2.NewRow();
				int num = (dataRow[0] is DBNull) ? 0 : ((int)dataRow[0]);
				dataRow2[0] = num;
				dataRow2[1] = dataRow["lucourseid"];
				dataRow2[2] = "";
				dataRow2[3] = dataRow["notes"].ToString();
				dataRow2[4] = ((dataRow["lecturedate"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dataRow["lecturedate"]));
				dataRow2[5] = DateTime.Now;
				string value = dataRow["docname"].ToString().Trim();
				dataRow2["docname"] = value;
				bool flag2 = dataRow["hasnotes"] != DBNull.Value && Convert.ToBoolean(dataRow["hasnotes"]);
				dataRow2["hasnotes"] = flag2;
				dataRow2["nonotes"] = !flag2;
				dataRow2["notetakertitle"] = (settingValue ? (dataRow["firstname"].ToString() + " " + dataRow["lastname"].ToString()) : ("Notetaker " + dataRow["notetakerid"].ToString()));
				dataTable2.Rows.Add(dataRow2);
			}
			this.gv_courses.DataSource = dataTable2;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x000243C8 File Offset: 0x000225C8
		protected void gv_courses_ItemCreated(object sender, GridItemEventArgs e)
		{
			bool flag = e.Item is GridDataItem;
			if (flag)
			{
				GridDataItem gridDataItem = e.Item as GridDataItem;
				TableCell tableCell = gridDataItem["col_lectureDate"];
				bool flag2 = tableCell != null;
				if (flag2)
				{
					tableCell.Attributes["scope"] = "row";
				}
			}
		}

		// Token: 0x040002CA RID: 714
		protected ScriptManager scripMang;

		// Token: 0x040002CB RID: 715
		protected Table tt;

		// Token: 0x040002CC RID: 716
		protected Panel p_Title;

		// Token: 0x040002CD RID: 717
		protected Label lblTitle;

		// Token: 0x040002CE RID: 718
		protected Label lblSampleNotesCourse;

		// Token: 0x040002CF RID: 719
		protected Button Button1;

		// Token: 0x040002D0 RID: 720
		protected Panel p_courseOptions;

		// Token: 0x040002D1 RID: 721
		protected LinkButton link_history;

		// Token: 0x040002D2 RID: 722
		protected Panel lbl_intro;

		// Token: 0x040002D3 RID: 723
		protected Label lbl_DownloadLectureNotesInfo;

		// Token: 0x040002D4 RID: 724
		protected Panel p_notes;

		// Token: 0x040002D5 RID: 725
		protected RadGrid gv_courses;

		// Token: 0x040002D6 RID: 726
		protected Panel p_b;

		// Token: 0x040002D7 RID: 727
		protected Button btn_backToNotetakee;
	}
}
