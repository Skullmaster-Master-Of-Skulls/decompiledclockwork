using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using ClockWorkWebAPI.AuthenticationAuthorization;
using Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using Databases;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;
using TechnoPro.Common.UI.Web.Entity.Web;

namespace TechnoPro.ClockWorkWeb.user.NotetakingNotetakers
{
	// Token: 0x0200009F RID: 159
	public class user_NotetakingNotetakers_AdminTest : Page
	{
		// Token: 0x06000516 RID: 1302 RVA: 0x00025224 File Offset: 0x00023424
		protected void Page_Load(object sender, EventArgs e)
		{
			ClockWorkIdentity currentClockWorkIdentity_LoginIfNecessary = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetCurrentClockWorkIdentity_LoginIfNecessary(this.Page, ClockWorkWebAPI.AuthenticationAuthorization.GroupMembership.admin, false);
			bool flag = currentClockWorkIdentity_LoginIfNecessary == null;
			if (flag)
			{
				base.Response.Redirect("default.aspx");
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00025264 File Offset: 0x00023464
		protected void btn_testDataSync_Click(object sender, EventArgs e)
		{
			IAdminTestingWebClientManager adminTestingWebClientManager = new AdminTestingWebClientManager();
			int pid = this.GetPid();
			bool flag = pid <= 0;
			if (flag)
			{
				base.Response.Write("No logged in notetaker is present.  Test results were not recorded to the logs.");
			}
			else
			{
				IList<Course> list = this.LoadCoursesFromDataSync(pid);
				bool flag2 = list == null;
				string message;
				if (flag2)
				{
					message = "NULL";
				}
				else
				{
					message = string.Join(",", list.ToList<Course>().ConvertAll<string>((Course g) => g.ToString()).ToArray());
				}
				AdminTestMessageView message2 = new AdminTestMessageView
				{
					Context = "NotetakerTestDataSync",
					Message = message
				};
				adminTestingWebClientManager.ShowAdminMessage(this, message2);
				base.Response.Write("Test results have been recorded to 'info' logs");
			}
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00025330 File Offset: 0x00023530
		private IList<Course> LoadCoursesFromDataSync(int nid)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DateTime dateTime;
			DateTime dateTime2;
			this.GetSelectedTermDates(out dateTime, out dateTime2);
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@id", DbType.Int32, nid),
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
			return list;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00025728 File Offset: 0x00023928
		private void GetSelectedTermDates(out DateTime startDate, out DateTime endDate)
		{
			string s = DateTime.Now.ToString("yyyy-MM-dd");
			startDate = DateTime.Parse(s);
			endDate = startDate.AddMonths(4).AddDays(-1.0);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00025774 File Offset: 0x00023974
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetNotetakerId(this.Page);
		}

		// Token: 0x040002FA RID: 762
		protected ScriptManager bbb;

		// Token: 0x040002FB RID: 763
		protected Panel pintro;

		// Token: 0x040002FC RID: 764
		protected Label lblIntro;

		// Token: 0x040002FD RID: 765
		protected Panel p_buttons;

		// Token: 0x040002FE RID: 766
		protected Button btn_testDataSync;
	}
}
