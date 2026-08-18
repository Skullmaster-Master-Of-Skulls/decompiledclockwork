using System;
using System.Data;
using System.Data.Common;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using Databases;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Notetaking;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.Notetaking;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.NotetakingStudents
{
	// Token: 0x0200009C RID: 156
	public class user_NotetakingStudents_SampleNotesStudent : Page
	{
		// Token: 0x06000501 RID: 1281 RVA: 0x000246B8 File Offset: 0x000228B8
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				this.lbl_introNotetakee.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_SampleNotesDownloadInfo);
				string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_SampleNotesAdditionalInfoNotetakee);
				bool flag2 = settingValue.Length > 0;
				if (flag2)
				{
					this.lbl_additionalInfo.Text = settingValue;
				}
				else
				{
					this.p_additionalInfo.Visible = false;
				}
				string str = base.Request.QueryString["cd"] ?? "";
				this.lblSampleNotesCourse.Text = " for " + str;
				this.lblSampleNotesNotetaker.Text = this.GetNotetakerNameFromUrl();
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0002477C File Offset: 0x0002297C
		private string GetNotetakerNameFromUrl()
		{
			return NavigatorClientManager.CurrentInstance.GetStringFromUrlParameter("nn");
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x000247A0 File Offset: 0x000229A0
		protected void gv_course_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
		{
			int intFromUrlParameter = NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["lucid"] ?? "");
			int intFromUrlParameter2 = NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["lucid2"] ?? "");
			int intFromUrlParameter3 = NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["spid"] ?? "");
			int num = new WebSettingsClientManager().GetSettingValue<int>(Setting.NOTETAKINGB_NotetakersMaxSampleNotesUploadCount);
			bool flag = num < 1;
			if (flag)
			{
				num = 3;
			}
			DateTime dateTime;
			DateTime dateTime2;
			Core.GetTermStartEndDates(out dateTime, out dateTime2);
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@lucidsp", DbType.Int32, intFromUrlParameter2),
				clockWork.GetParameter("@spid", DbType.Int32, intFromUrlParameter3),
				clockWork.GetParameter("@sdate", DbType.DateTime, dateTime),
				clockWork.GetParameter("@edate", DbType.DateTime, dateTime2)
			};
			string text = QueryStorage.QS_Select_StudentNotes2;
			text = text.Replace("@numsamplenotes", num.ToString());
			DataTable dataTable = clockWork.ExecuteQuery(text, parameters);
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				dataRow["description"] = Notetakingb.GetNotesFilename(dataRow);
			}
			this.gv_courses.DataSource = dataTable;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00024958 File Offset: 0x00022B58
		protected void btn_return_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("NotetakerApp.aspx");
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0002496C File Offset: 0x00022B6C
		protected void gv_courses_ItemCommand(object source, GridCommandEventArgs e)
		{
			int intFromUrlParameter = NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["lucid"]);
			int intFromUrlParameter2 = NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["spid"]);
			object commandArgument = e.CommandArgument;
			bool flag = commandArgument != null;
			int docID;
			if (flag)
			{
				string text = commandArgument.ToString().Trim();
				bool flag2 = text.Length > 0;
				if (flag2)
				{
					try
					{
						docID = int.Parse(text);
					}
					catch
					{
						docID = 0;
					}
				}
				else
				{
					docID = 0;
				}
			}
			else
			{
				docID = 0;
			}
			INotetakingWebClientManager notetakingWebClientManager = new NotetakingWebClientManager();
			bool flag3 = e.CommandName.CompareTo("download") == 0;
			if (flag3)
			{
				bool flag4 = notetakingWebClientManager.DownloadLectureNoteToBrowser(docID);
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00024A44 File Offset: 0x00022C44
		protected void btn_backToChooseNotetaker_Click(object sender, EventArgs e)
		{
			int intFromUrlParameter = NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["lucid"]);
			base.Response.Redirect("ChooseNotetaker.aspx?lucid=" + NavigatorClientManager.CurrentInstance.GetUrlParameterFromString(intFromUrlParameter), true);
		}

		// Token: 0x040002E3 RID: 739
		protected ScriptManager bbb;

		// Token: 0x040002E4 RID: 740
		protected Panel p_title;

		// Token: 0x040002E5 RID: 741
		protected Label lblTitle;

		// Token: 0x040002E6 RID: 742
		protected Label lblSampleNotesNotetaker;

		// Token: 0x040002E7 RID: 743
		protected Label lblSampleNotesCourse;

		// Token: 0x040002E8 RID: 744
		protected Label lbl_introNotetakee;

		// Token: 0x040002E9 RID: 745
		protected RadGrid gv_courses;

		// Token: 0x040002EA RID: 746
		protected Button btn_backToChooseNotetaker;

		// Token: 0x040002EB RID: 747
		protected Panel p_additionalInfo;

		// Token: 0x040002EC RID: 748
		protected Label lbl_additionalInfo;
	}
}
