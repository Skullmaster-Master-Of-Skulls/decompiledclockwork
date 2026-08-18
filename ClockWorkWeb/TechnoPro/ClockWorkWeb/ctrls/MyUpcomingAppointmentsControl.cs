using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkController;
using ClockWorkWebAPI;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.Core.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.AppointmentsCalendar;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.AppointmentsCalendar;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Email;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.MailMerging;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.MailMerging;
using TechnoPro.Common.UI.Web.Entity.Modules;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.ctrls
{
	// Token: 0x02000124 RID: 292
	public class MyUpcomingAppointmentsControl : UserControl
	{
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000884 RID: 2180 RVA: 0x0003C9F8 File Offset: 0x0003ABF8
		// (remove) Token: 0x06000885 RID: 2181 RVA: 0x0003CA30 File Offset: 0x0003AC30
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event MyUpcomingAppointmentsControl.AppointmentEventhandler AppointmentCancelled;

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x0003CA68 File Offset: 0x0003AC68
		// (set) Token: 0x06000887 RID: 2183 RVA: 0x0003CA80 File Offset: 0x0003AC80
		public bool IsFacilitator
		{
			get
			{
				return this.isFacilitator;
			}
			set
			{
				this.isFacilitator = value;
			}
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0003CA8C File Offset: 0x0003AC8C
		private void FireOnCancelled(int appId)
		{
			bool flag = this.AppointmentCancelled != null;
			if (flag)
			{
				this.AppointmentCancelled(this, appId, new EventArgs());
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x0003CABC File Offset: 0x0003ACBC
		// (set) Token: 0x0600088A RID: 2186 RVA: 0x0003CAD4 File Offset: 0x0003ACD4
		public bool IsDisabled
		{
			get
			{
				return this._isDisabled;
			}
			set
			{
				this._isDisabled = value;
			}
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0003CADE File Offset: 0x0003ACDE
		protected void Page_Load(object sender, EventArgs e)
		{
			this.MyInit();
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x0003CAE8 File Offset: 0x0003ACE8
		private bool showLocation
		{
			get
			{
				bool flag = this._showLocation == null;
				if (flag)
				{
					this._showLocation = new bool?(new WebSettingsClientManager().GetSettingValue<bool>(Setting.APPOINTMENTBOOKING_MyUpcomingAppointments_ShowLocation));
				}
				return this._showLocation.Value;
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0003CB34 File Offset: 0x0003AD34
		public void MyInit()
		{
			int num = this.LookupStudentPid();
			bool flag = num <= 0;
			if (flag)
			{
				bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.APPOINTMENTBOOKING_allowNonClockWorkStudentsToRegister);
				base.Response.Redirect(settingValue ? "NewUser.aspx" : "Message.aspx?msgcode=notallowed", true);
			}
			else
			{
				bool flag2 = !this.Page.IsPostBack;
				if (flag2)
				{
					bool flag3 = !this.showLocation && !this.ShowTestLocationOnMyUpcomingEvents_StartShowingDate.Enabled;
					if (flag3)
					{
						this.RadGrid1.Columns[2].Visible = false;
					}
					string text = base.Request.QueryString["refresh"];
					bool flag4 = text != null && text.Equals("1");
					if (flag4)
					{
						this.ClearAppsCache();
					}
					string settingValue2 = new WebSettingsClientManager().GetSettingValue<string>(Setting.GENERAL_MyUpcomingAppointments_Info);
					bool flag5 = settingValue2.Length > 0;
					if (flag5)
					{
						this.lbl_info.Text = settingValue2;
						this.p_info.Visible = true;
					}
					bool enabled = this.ShowOnlyClassTimeCutoff.Enabled;
					if (enabled)
					{
						this.p_onlyShowingClassDateTime.Visible = true;
					}
					this.CheckIfOptionsColumnCanBeHidden();
				}
			}
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0003CC6E File Offset: 0x0003AE6E
		private void ShowMessage(string msg)
		{
			this.lbl_msg.Text = msg;
			this.p_msg.Visible = true;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0003CC8B File Offset: 0x0003AE8B
		private void HideMessage()
		{
			this.p_msg.Visible = false;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0003CC9B File Offset: 0x0003AE9B
		protected void btn_export_Click(object sender, EventArgs e)
		{
			this.RadGrid1.Columns[this.RadGrid1.Columns.Count - 1].Visible = false;
			this.RadGrid1.MasterTableView.ExportToPdf();
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0003CCD8 File Offset: 0x0003AED8
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0003CCFC File Offset: 0x0003AEFC
		protected void grid_waitingList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = DatabaseLayerFactory.ClockWork.Encryption;
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.APPOINTMENTBOOKING_useWaitingList);
			bool flag = settingValue;
			if (flag)
			{
				int pid = this.GetPid();
				string key = "studentwaitinglist" + pid.ToString();
				object obj = base.Cache[key];
				bool flag2 = obj == null;
				DataTable dataTable;
				if (flag2)
				{
					dataTable = new DataTable();
					string query = "SELECT wl.waitinglistid,wl.appointmentid,app.startdate,app.enddate,coalesce(at2.description,at.description) AS description\r\n            ,app.subject AS subtitle,app.location \r\nFROM waitinglist wl LEFT JOIN appointments app ON app.appointmentid=wl.appointmentid \r\n            LEFT JOIN appointmenttypes at ON at.apptypeid=app.apptypeid \r\n            LEFT JOIN appointmenttypes at2 ON at2.apptypeid=wl.apptypeid \r\nWHERE wl.personid=@pid AND NOT app.appointmentid IS NULL ORDER BY app.startdate";
					DbParameter[] parameters = new DbParameter[]
					{
						clockWork.GetParameter("@pid", DbType.Int32, pid)
					};
					dataTable = clockWork.ExecuteQuery(query, parameters);
					dataTable = encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
					{
						"subtitle",
						"location"
					});
					int settingValue2 = new WebSettingsClientManager().GetSettingValue<int>(Setting.GENERAL_Caching_MinutesToCacheUserData);
					bool flag3 = settingValue2 > 0;
					if (flag3)
					{
						base.Cache.Insert(key, dataTable, null, DateTime.Now.AddMinutes((double)settingValue2), TimeSpan.Zero);
					}
				}
				else
				{
					dataTable = (DataTable)obj;
				}
				bool flag4 = dataTable.Rows.Count > 0;
				if (flag4)
				{
					bool flag5 = !this.p_waitingList.Visible;
					if (flag5)
					{
						this.p_waitingList.Visible = true;
					}
					this.grid_waitingList.DataSource = dataTable;
				}
				else
				{
					this.p_waitingList.Visible = false;
				}
			}
			else
			{
				this.p_waitingList.Visible = false;
			}
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0003CE7C File Offset: 0x0003B07C
		protected void grid_waitingList_ItemCommand(object source, GridCommandEventArgs e)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
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
			int num2 = this.LookupStudentPid();
			bool flag3 = num2 > 0;
			if (flag3)
			{
				bool flag4 = e.CommandName.Equals("remove");
				if (flag4)
				{
					clockWork.ExecuteNonQuery("DELETE FROM waitinglist WHERE waitinglistid=@id", new DbParameter[]
					{
						clockWork.GetParameter("@id", DbType.Int32, num)
					});
					this.ClearAppsCache();
					base.Response.Redirect(base.Request.Url.ToString());
				}
			}
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0003CF64 File Offset: 0x0003B164
		protected void btn_refresh_Click(object sender, EventArgs e)
		{
			this.ClearAppsCache();
			base.Response.Redirect(base.Request.Url.ToString());
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0003CF8C File Offset: 0x0003B18C
		private void ClearAppsCache()
		{
			int num = this.LookupStudentPid();
			string key = "studentapps" + num.ToString();
			bool flag = base.Cache[key] != null;
			if (flag)
			{
				base.Cache.Remove(key);
			}
			key = "studentwaitinglist" + num.ToString();
			bool flag2 = base.Cache[key] != null;
			if (flag2)
			{
				base.Cache.Remove(key);
			}
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0003D008 File Offset: 0x0003B208
		protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
		{
			bool isDisabled = this._isDisabled;
			if (!isDisabled)
			{
				DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
				IEncryption encryption = DatabaseLayerFactory.ClockWork.Encryption;
				int num6 = this.LookupStudentPid();
				string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.APPOINTMENTBOOKING_TutorGids);
				bool flag = this.isFacilitator;
				if (flag)
				{
					this.lbl_title.BackColor = Color.LightSkyBlue;
					this.lbl_title.BorderStyle = BorderStyle.Dotted;
				}
				bool settingValue2 = new WebSettingsClientManager().GetSettingValue<bool>(Setting.APPOINTMENTBOOKING_CanUserCancelAppointments);
				int appId = this.GetAppId();
				string key = "studentapps" + num6.ToString();
				object obj = base.Cache[key];
				bool flag2 = obj == null;
				DataTable dataTable;
				if (flag2)
				{
					DateTime now = DateTime.Now;
					DateTime dateTime = new DateTime(now.Year, now.Month, now.Day);
					DateTime dateTime2 = dateTime.AddYears(1);
					string value = num6.ToString();
					dataTable = new DataTable();
					bool flag3 = this.isFacilitator;
					string query;
					if (flag3)
					{
						query = ClockWorkWebAPI.QueryStorage.QS_Select_LoadStudentCalendarForFacilitator;
						dateTime = dateTime.AddMonths(-4);
					}
					else
					{
						query = "SELECT app.appointmentid,app.startdate,app.enddate,app.cancelled,app.apptypeid\r\n        ,at.[description] + COALESCE(': ' + w.workshoptitle,'') AS [description]\r\n        --,at.iscourse\r\n         ,CASE WHEN ac.appointmentid IS NULL THEN CAST(0 AS bit) \r\nELSE CAST(1 as bit) \r\nEND AS iscourse,\r\n        app.appcode,att.personid,att2.noshow,\r\n        att2.personid AS personid2,p.firstname,p.lastname,ac.lucourseid,\r\n        lucd.altlookupstring AS subject,luc.course,luc.section,\r\n        app.subject AS subtitle,app.location,\r\n        ac.originalstartdatetime,ac.originalenddatetime,\r\n        pr.firstname AS room\r\nFROM appointments app LEFT JOIN appointmenttypes at ON at.apptypeid=app.apptypeid \r\n        LEFT JOIN attendees att ON att.appointmentid=app.appointmentid \r\n        LEFT JOIN attendees att2 ON att2.appointmentid=app.appointmentid AND att2.personid IN (SELECT personid FROM peoplegroups WHERE groupid=2 OR personid IN (SELECT personid FROM peoplegroups WHERE groupid IN ( SELECT orderid AS groupid FROM splitorderids(@gids,','))) ) \r\n        LEFT JOIN people p ON p.personid=att2.personid \r\n        LEFT JOIN appointmentcourses ac ON ac.appointmentid=app.appointmentid \r\n        LEFT JOIN lucourses luc ON luc.lucourseid=ac.lucourseid \r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\n        LEFT JOIN attendees attroom ON attroom.appointmentid=app.appointmentid AND attroom.personid IN (SELECT personid FROM peoplegroups WHERE groupid=3)\r\n        LEFT JOIN people pr ON pr.personid=attroom.personid\r\n        LEFT JOIN examstatuslookup esl ON esl.ExamStatusLookupId=ac.ExamStatusLookupId \r\n        LEFT JOIN appointmentworkshops aw ON aw.appointmentid=app.appointmentid\r\n        LEFT JOIN workshops w ON w.workshopid=aw.workshopid\r\nWHERE att.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) \r\n        AND app.startdate>=@sdate AND app.enddate<=@edate AND app.cancelled=0 \r\n        AND (@apptypeids='' OR app.apptypeid IN (SELECT orderid AS apptypeid FROM splitorderids(@apptypeids,',')))\r\n        AND NOT (datepart(hour,app.startdate)=0 AND datepart(minute,app.startdate)=0 AND datepart(hour,app.enddate)=1 AND datepart(minute,app.enddate)=0)\r\n\t\tAND (esl.HideFromStudent IS NULL OR esl.HideFromStudent=0)\r\nORDER BY app.startdate,app.appointmentid";
					}
					int[] settingValue3 = new WebSettingsClientManager().GetSettingValue<int[]>(Setting.APPOINTMENTBOOKING_AppointmentTypesToAllowInMyUpcomingEventsList);
					bool flag4 = settingValue3 != null;
					string value2;
					if (flag4)
					{
						List<int> list = new List<int>(settingValue3);
						value2 = string.Join(",", list.ConvertAll<string>((int num) => num.ToString()).ToArray());
					}
					else
					{
						value2 = "";
					}
					DbParameter[] parameters = new DbParameter[]
					{
						clockWork.GetParameter("@pids", DbType.String, value),
						clockWork.GetParameter("@sdate", DbType.DateTime, dateTime),
						clockWork.GetParameter("@edate", DbType.DateTime, dateTime2),
						clockWork.GetParameter("@gids", DbType.String, settingValue),
						clockWork.GetParameter("@apptypeids", DbType.String, value2)
					};
					dataTable = clockWork.ExecuteQuery(query, parameters);
					dataTable = encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
					{
						"room",
						"firstname",
						"lastname",
						"subtitle",
						"location"
					});
					dataTable.Columns.Add("showingclasstime", typeof(bool));
					MyUpcomingAppointmentsControl.UpdateShowClassTimeOrScheduledTime(ref dataTable, this.ShowOnlyClassTimeCutoff);
					int num2 = 0;
					List<DataRow> list2 = new List<DataRow>();
					foreach (object obj2 in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj2;
						int num3 = (int)dataRow["appointmentid"];
						bool flag5 = num2 != num3;
						if (flag5)
						{
							num2 = num3;
						}
						else
						{
							list2.Add(dataRow);
						}
					}
					foreach (DataRow row in list2)
					{
						dataTable.Rows.Remove(row);
					}
					list2.Clear();
					dataTable.Columns.Add("Status");
					dataTable.Columns.Add("CanCancel", typeof(bool));
					dataTable.Columns.Add("CanMarkNoshow", typeof(bool));
					dataTable.Columns.Add("Tentative", typeof(bool));
					dataTable.Columns.Add("Highlighted", typeof(bool));
					dataTable.Columns.Add("studentformnum", typeof(int));
					dataTable.Columns["description"].ColumnName = "desc";
					dataTable.Columns.Add("description");
					foreach (object obj3 in dataTable.Rows)
					{
						DataRow dataRow2 = (DataRow)obj3;
						dataRow2["description"] = dataRow2["desc"].ToString().Trim();
					}
					foreach (object obj4 in dataTable.Rows)
					{
						DataRow dataRow3 = (DataRow)obj4;
						int num4 = (int)dataRow3["appointmentid"];
						bool flag6 = dataRow3["cancelled"] != DBNull.Value && (bool)dataRow3["cancelled"];
						bool flag7 = dataRow3["noshow"] != DBNull.Value && Convert.ToBoolean(dataRow3["noshow"]);
						bool flag8 = dataRow3["appcode"] != DBNull.Value && (int)dataRow3["appcode"] == -1;
						int num5 = (int)dataRow3["apptypeid"];
						dataRow3["Tentative"] = flag8;
						dataRow3["CanCancel"] = (settingValue2 && !flag6 && !flag7 && !this.isFacilitator);
						dataRow3["CanMarkNoShow"] = (this.isFacilitator && !flag7);
						dataRow3["Highlighted"] = (num4 == appId);
						StringBuilder stringBuilder = new StringBuilder();
						bool flag9 = flag6;
						if (flag9)
						{
							stringBuilder.Append("Cancelled");
						}
						bool flag10 = flag7;
						if (flag10)
						{
							bool flag11 = flag6;
							if (flag11)
							{
								stringBuilder.Append(", ");
							}
							stringBuilder.Append("No-Show");
						}
						bool flag12 = flag8;
						if (flag12)
						{
							bool flag13 = flag6 || flag7;
							if (flag13)
							{
								stringBuilder.Append(", ");
							}
							stringBuilder.Append("Tentative");
						}
						dataRow3["Status"] = ((stringBuilder.Length > 0) ? stringBuilder.ToString() : "<span style='color: #999'>Booked</span>");
						string str = dataRow3["description"].ToString();
						bool flag14 = dataRow3["subtitle"] != DBNull.Value;
						if (flag14)
						{
							string text = dataRow3["subtitle"].ToString();
							bool flag15 = text.Length > 0;
							if (flag15)
							{
								dataRow3["description"] = str + ": " + text;
							}
						}
						bool flag16 = dataRow3["lucourseid"] != DBNull.Value;
						if (flag16)
						{
							dataRow3["firstname"] = string.Format("{0} {1} {2}", dataRow3["subject"].ToString(), dataRow3["course"].ToString(), dataRow3["section"].ToString());
						}
						bool flag17 = this.isFacilitator;
						if (flag17)
						{
							DataRow dataRow4 = dataRow3;
							dataRow4["firstname"] = dataRow4["firstname"] + " " + dataRow3["lastname"].ToString();
						}
					}
					int settingValue4 = new WebSettingsClientManager().GetSettingValue<int>(Setting.GENERAL_Caching_MinutesToCacheUserData);
					bool flag18 = settingValue4 > 0;
					if (flag18)
					{
						base.Cache.Insert(key, dataTable, null, DateTime.Now.AddMinutes((double)settingValue4), TimeSpan.Zero);
					}
				}
				else
				{
					dataTable = (DataTable)obj;
				}
				this.RadGrid1.DataSource = dataTable;
			}
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0003D838 File Offset: 0x0003BA38
		private static bool IsTestRowBeforeCutoff(DataRow dr, CutoffTime showOnlyClassTimeCutoff)
		{
			DateTime? dateTime = (dr["startdate"] is DBNull) ? null : new DateTime?((DateTime)dr["startdate"]);
			bool flag = dateTime == null || dateTime.Value == DateTime.MinValue;
			return !flag && (showOnlyClassTimeCutoff.IsRightNowBeforeCutoffTime(dateTime.Value) ?? false);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0003D8C4 File Offset: 0x0003BAC4
		private static void UpdateShowClassTimeOrScheduledTime(ref DataTable t, CutoffTime showOnlyClassTimeCutoff)
		{
			bool flag = showOnlyClassTimeCutoff == null || !showOnlyClassTimeCutoff.Enabled;
			if (!flag)
			{
				List<DataRow> source = (from DataRow dr in t.Rows
				where !(dr["iscourse"] is DBNull) && Convert.ToBoolean(dr["iscourse"])
				select dr).ToList<DataRow>();
				List<DataRow> list = (from g in source
				where MyUpcomingAppointmentsControl.IsTestRowBeforeCutoff(g, showOnlyClassTimeCutoff)
				select g).ToList<DataRow>();
				foreach (DataRow dataRow in list)
				{
					dataRow["showingclasstime"] = true;
				}
			}
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0003D9A4 File Offset: 0x0003BBA4
		private void CheckIfOptionsColumnCanBeHidden()
		{
			bool flag = this.StudentsCanCancelApps;
			bool flag2 = flag;
			if (!flag2)
			{
				bool flag3 = this.MakeStudentsConfirmTests;
				bool flag4 = flag3;
				if (!flag4)
				{
					bool flag5 = this.MakeStudentsConfirmApps;
					bool flag6 = flag5;
					if (!flag6)
					{
						GridColumn gridColumn = this.RadGrid1.Columns.FindByUniqueName("col_option");
						bool flag7 = gridColumn != null;
						if (flag7)
						{
							gridColumn.Visible = false;
						}
					}
				}
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x0003DA0C File Offset: 0x0003BC0C
		public bool MakeStudentsConfirmApps
		{
			get
			{
				bool flag = this.makeStudentsConfirmApps == null;
				if (flag)
				{
					this.makeStudentsConfirmApps = new bool?(new WebSettingsClientManager().GetSettingValue<bool>(Setting.APPOINTMENTBOOKING_MakeStudentsConfirmTentativeApps));
				}
				return this.makeStudentsConfirmApps.Value;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x0003DA58 File Offset: 0x0003BC58
		public bool MakeStudentsConfirmTests
		{
			get
			{
				bool flag = this.makeStudentsConfirmTests == null;
				if (flag)
				{
					this.makeStudentsConfirmTests = new bool?(new WebSettingsClientManager().GetSettingValue<bool>(Setting.TESTBOOKING_MakeStudentsConfirmTentativeTests));
				}
				return this.makeStudentsConfirmTests.Value;
			}
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0003DAA4 File Offset: 0x0003BCA4
		private int GetAppId()
		{
			return NavigatorClientManager.CurrentInstance.ConvertUrlStringToIntParameter(base.Request.QueryString["appid"] ?? "");
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0003DAE0 File Offset: 0x0003BCE0
		protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
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
			string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.APPOINTMENTBOOKING_TutorGids);
			string query = "SELECT att.personid FROM attendees att \r\n    WHERE att.appointmentid=@appid \r\n        AND att.personid IN (SELECT personid FROM peoplegroups WHERE groupid IN (SELECT orderid AS groupid FROM splitorderids(@gids,',')))";
			DataTable dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
			{
				clockWork.GetParameter("@appid", DbType.Int32, num),
				clockWork.GetParameter("@gids", DbType.String, settingValue)
			});
			int num2 = 0;
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				num2 = (int)dataRow[0];
			}
			bool flag3 = num2 <= 0;
			if (flag3)
			{
				num2 = this.GetPid();
			}
			bool flag4 = e.CommandName.CompareTo("cancel") == 0;
			if (flag4)
			{
				bool flag5 = !this.StudentsCanCancelApps;
				if (flag5)
				{
					this.ShowMessage("Please contact us to cancel your appointment.");
				}
				else
				{
					IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
					appointmentClientManager.CancelAttendeeAppointment(num, num2, null);
					this.FireOnCancelled(num);
					this.ResetAppointmentCache();
					StringDictionary stringDictionary = new StringDictionary();
					DateTime now = DateTime.Now;
					bool flag6 = now.Month < 5;
					string value;
					if (flag6)
					{
						value = (now.Year - 1).ToString().Substring(2) + "." + now.Year.ToString();
					}
					else
					{
						value = now.Year.ToString().Substring(2) + "." + (now.Year + 1).ToString();
					}
					stringDictionary.Add("#<schoolyear>#", value);
					int settingValue2 = new WebSettingsClientManager().GetSettingValue<int>(Setting.GENERAL_EmailCid);
					bool settingValue3 = new WebSettingsClientManager().GetSettingValue<bool>(Setting.GENERAL_EmailEncrypted);
					PersonBaseDTO personBaseDTO;
					string value2 = ClockWorkController.Student.LookupEmail(num2, settingValue2, settingValue3, out personBaseDTO);
					stringDictionary.Add("#<email>#", value2);
					bool flag7 = personBaseDTO != null;
					if (flag7)
					{
						stringDictionary.Add("#<firstname>#", personBaseDTO.FirstName);
						stringDictionary.Add("#<lastname>#", personBaseDTO.LastName);
					}
					AppointmentDTO appointmentDTO = ClockWorkController.Appointment.LoadAppointment(num);
					bool flag8 = appointmentDTO != null;
					if (flag8)
					{
						stringDictionary.Add("#<title>#", appointmentDTO.AppType.Description);
						stringDictionary.Add("#<appdate>#", appointmentDTO.StartDateTime.ToString("MMMM d, yyyy"));
						stringDictionary.Add("#<starttime>#", appointmentDTO.StartDateTime.ToString("h:mm tt"));
						stringDictionary.Add("#<endtime>#", appointmentDTO.EndDateTime.ToString("h:mm tt"));
						stringDictionary.Add("#<duration>#", appointmentDTO.GetDurationDescription());
					}
					IMailMergeCodes mailMergeCodes = new MailMergeCodes();
					stringDictionary.Add("from", mailMergeCodes.GetDefaultFromAddress(eWebModule.AppointmentBooking));
					stringDictionary.Add("signature", mailMergeCodes.GetDefaultSignature(eWebModule.AppointmentBooking));
					IEmailClientManager emailClientManager = new EmailClientManager();
					MailMergeContextDTO mailMergeContext = new MailMergeContextDTO
					{
						PersonId = num2,
						AppointmentId = num
					};
					emailClientManager.SendEmail(Setting.APPOINTMENTBOOKING_email_cancel, mailMergeContext, stringDictionary.InsertBaseUserMailMergeValues(), "MyUpcomingAppointments");
					this.ShowMessage("Successfully cancelled the appointment.");
					this.RadGrid1.Rebind();
				}
			}
			else
			{
				bool flag9 = e.CommandName.CompareTo("noshow") == 0;
				if (flag9)
				{
					Appointments.NoshowUnnoshowAppointment2(num, settingValue, true, num2);
					int noShowConsecutiveCount = ClockWorkController.Appointment.GetNoShowConsecutiveCount(num2, DateTime.Now);
					int settingValue4 = new WebSettingsClientManager().GetSettingValue<int>(Setting.APPOINTMENTBOOKING_bannedNumNoshows);
					bool flag10 = settingValue4 > 0 && noShowConsecutiveCount >= settingValue4;
					if (flag10)
					{
						int settingValue5 = new WebSettingsClientManager().GetSettingValue<int>(Setting.APPOINTMENTBOOKING_bannedExpiryDateCid);
						IAppointmentBookingStudentWebClientManager appointmentBookingStudentWebClientManager = new AppointmentBookingStudentWebClientManager();
						DateTime? dateTime = appointmentBookingStudentWebClientManager.MarkStudentBannedFromOnlineAppointmentBooking(num2);
						DateTime? dateTime2 = dateTime;
						DateTime minValue = DateTime.MinValue;
						bool flag11 = dateTime2 == null || (dateTime2 != null && dateTime2.GetValueOrDefault() != minValue);
						if (flag11)
						{
						}
						StringDictionary stringDictionary2 = new StringDictionary();
						DateTime now2 = DateTime.Now;
						bool flag12 = now2.Month < 5;
						string value3;
						if (flag12)
						{
							value3 = (now2.Year - 1).ToString().Substring(2) + "." + now2.Year.ToString();
						}
						else
						{
							value3 = now2.Year.ToString().Substring(2) + "." + (now2.Year + 1).ToString();
						}
						stringDictionary2.Add("#<schoolyear>#", value3);
						int settingValue6 = new WebSettingsClientManager().GetSettingValue<int>(Setting.GENERAL_EmailCid);
						bool settingValue7 = new WebSettingsClientManager().GetSettingValue<bool>(Setting.GENERAL_EmailEncrypted);
						PersonBaseDTO personBaseDTO2;
						string value4 = ClockWorkController.Student.LookupEmail(num2, settingValue6, settingValue7, out personBaseDTO2);
						stringDictionary2.Add("#<email>#", value4);
						bool flag13 = personBaseDTO2 != null;
						if (flag13)
						{
							stringDictionary2.Add("#<firstname>#", personBaseDTO2.FirstName);
							stringDictionary2.Add("#<lastname>#", personBaseDTO2.LastName);
						}
						stringDictionary2.Add("#<banneduntil>#", (dateTime != null) ? dateTime.Value.ToString("MMMM d, yyyy") : "");
						IMailMergeCodes mailMergeCodes2 = new MailMergeCodes();
						stringDictionary2.Add("from", mailMergeCodes2.GetDefaultFromAddress(eWebModule.AppointmentBooking));
						stringDictionary2.Add("signature", mailMergeCodes2.GetDefaultSignature(eWebModule.AppointmentBooking));
						IEmailClientManager emailClientManager2 = new EmailClientManager();
						MailMergeContextDTO mailMergeContext2 = new MailMergeContextDTO
						{
							PersonId = num2,
							AppointmentId = num
						};
						emailClientManager2.SendEmail(Setting.APPOINTMENTBOOKING_email_banned, mailMergeContext2, stringDictionary2.InsertBaseUserMailMergeValues(), "MyUpcomingAppointments");
					}
					this.ResetAppointmentCache();
					this.ShowMessage("Successfully no-showed the student.");
					this.RadGrid1.Rebind();
				}
				else
				{
					bool flag14 = e.CommandName.CompareTo("notes") == 0;
					if (flag14)
					{
						string str = NavigatorClientManager.CurrentInstance.ConvertIntParameterToUrlString(num);
						string str2 = NavigatorClientManager.CurrentInstance.ConvertIntParameterToUrlString(num2);
						base.Response.Redirect("Notes.aspx?appid=" + str + "&pid=" + str2);
					}
					else
					{
						bool flag15 = e.CommandName.Equals("confirm");
						if (flag15)
						{
							DbParameter[] parameters = new DbParameter[]
							{
								clockWork.GetParameter("@appid", DbType.Int32, num),
								clockWork.GetParameter("@pid", DbType.Int32, num2)
							};
							query = "UPDATE appointments SET appcode=0 WHERE appointmentid=@appid";
							clockWork.ExecuteNonQuery(query, parameters);
							query = "INSERT INTO appointmentsmodifieddates \r\n    (appointmentid,datemodified,personid,howmodifiedcode,changed_datetime,changed_description,changed_room,changed_memo,changed_attendees,changed_cancelled,changed_noshow,changed_course,changed_other1,changed_other2,changed_icons)\r\nVALUES (@appid,getdate(),@pid,1,0,0,0,0,0,0,0,0,1,0,0)";
							clockWork.ExecuteNonQuery(query, new DbParameter[]
							{
								clockWork.GetParameter("@appid", DbType.Int32, num),
								clockWork.GetParameter("@pid", DbType.Int32, num2)
							});
							this.ResetAppointmentCache();
							this.RadGrid1.Rebind();
							this.ShowMessage("Successfully confirmed the test.");
						}
					}
				}
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0003E26C File Offset: 0x0003C46C
		private void ResetAppointmentCache()
		{
			this.HideMessage();
			string key = "studentapps" + this.GetPid().ToString();
			bool flag = base.Cache[key] != null;
			if (flag)
			{
				base.Cache.Remove(key);
			}
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0003E2BA File Offset: 0x0003C4BA
		protected void RadGrid1_PageIndexChanged(object source, GridPageChangedEventArgs e)
		{
			this.HideMessage();
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0003E2C4 File Offset: 0x0003C4C4
		private int LookupStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x0003E2E8 File Offset: 0x0003C4E8
		protected bool StudentsCanCancelApps
		{
			get
			{
				bool flag = this.studentsCanCancelApps == null;
				if (flag)
				{
					this.studentsCanCancelApps = new bool?(new WebSettingsClientManager().GetSettingValue<bool>(Setting.APPOINTMENTBOOKING_CanUserCancelAppointments));
				}
				return this.studentsCanCancelApps.Value;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x0003E334 File Offset: 0x0003C534
		private CutoffTime AppointmentCancelCutoff
		{
			get
			{
				bool flag = this.appointmentCancelCutoff == null;
				if (flag)
				{
					string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.APPOINTMENTBOOKING_CutoffForCancelling);
					this.appointmentCancelCutoff = settingValue.CutoffTimeFromXml();
				}
				CutoffTime result;
				if (this.appointmentCancelCutoff != null)
				{
					result = this.appointmentCancelCutoff;
				}
				else
				{
					(result = new CutoffTime()).Enabled = false;
				}
				return result;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x0003E390 File Offset: 0x0003C590
		private CutoffTime ShowOnlyClassTimeCutoff
		{
			get
			{
				bool flag = this.showOnlyClassTimeCutoff != null;
				CutoffTime result;
				if (flag)
				{
					CutoffTime cutoffTime;
					if ((cutoffTime = this.showOnlyClassTimeCutoff) == null)
					{
						(cutoffTime = new CutoffTime()).Enabled = false;
					}
					result = cutoffTime;
				}
				else
				{
					string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.TESTBOOKING_ShowClassDateTimeInsteadOfScheduledDateTimeInMyUpcomingApptsCutoff);
					this.showOnlyClassTimeCutoff = settingValue.CutoffTimeFromXml();
					CutoffTime cutoffTime2;
					if ((cutoffTime2 = this.showOnlyClassTimeCutoff) == null)
					{
						(cutoffTime2 = new CutoffTime()).Enabled = false;
					}
					result = cutoffTime2;
				}
				return result;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x0003E3FC File Offset: 0x0003C5FC
		private CutoffTime StudentsCanCancelTestsCutoffTime
		{
			get
			{
				bool flag = this.studentsCanCancelTestsCutoffTime != null;
				CutoffTime result;
				if (flag)
				{
					result = this.studentsCanCancelTestsCutoffTime;
				}
				else
				{
					string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.TESTBOOKING_CutoffTimeForStudentsToCancelTheirTestBookings);
					this.studentsCanCancelTestsCutoffTime = settingValue.CutoffTimeFromXml();
					result = this.studentsCanCancelTestsCutoffTime;
				}
				return result;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x0003E448 File Offset: 0x0003C648
		public CutoffTime ConfirmCutoffStart
		{
			get
			{
				string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.TESTBOOKING_ConfirmTestsStart);
				CutoffTime result;
				if ((result = this.confirmCutoffStart) == null)
				{
					result = (this.confirmCutoffStart = settingValue.CutoffTimeFromXml());
				}
				return result;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x0003E484 File Offset: 0x0003C684
		public CutoffTime ConfirmCutoffEnd
		{
			get
			{
				string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.TESTBOOKING_ConfirmTestsEnd);
				CutoffTime result;
				if ((result = this.confirmCutoffEnd) == null)
				{
					result = (this.confirmCutoffEnd = settingValue.CutoffTimeFromXml());
				}
				return result;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x0003E4C0 File Offset: 0x0003C6C0
		private CutoffTime ShowTestLocationOnMyUpcomingEvents_StartShowingDate
		{
			get
			{
				bool flag = this.showTestLocationOnMyUpcomingEvents_StartShowingDate != null;
				CutoffTime result;
				if (flag)
				{
					result = this.showTestLocationOnMyUpcomingEvents_StartShowingDate;
				}
				else
				{
					string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.TESTBOOKING_ShowTestLocationOnMyUpcomingEvents_StartShowingDate);
					this.showTestLocationOnMyUpcomingEvents_StartShowingDate = settingValue.CutoffTimeFromXml();
					result = this.showTestLocationOnMyUpcomingEvents_StartShowingDate;
				}
				return result;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x0003E50C File Offset: 0x0003C70C
		private int[] ShowTestLocationFormat
		{
			get
			{
				bool flag = this.showTestLocationFormat == null;
				if (flag)
				{
					this.showTestLocationFormat = new WebSettingsClientManager().GetSettingValue<int[]>(Setting.TESTBOOKING_ShowTestLocationFormat);
				}
				return this.showTestLocationFormat;
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0003E548 File Offset: 0x0003C748
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
			bool flag3 = this.StudentsCanCancelApps;
			CutoffTime cutoffTime = this.StudentsCanCancelTestsCutoffTime;
			bool flag4 = this.MakeStudentsConfirmTests;
			bool flag5 = this.MakeStudentsConfirmApps;
			CutoffTime cutoffTime2 = this.ShowTestLocationOnMyUpcomingEvents_StartShowingDate;
			bool flag6 = e.Item.ItemType != GridItemType.AlternatingItem && e.Item.ItemType != GridItemType.Item;
			if (!flag6)
			{
				GridDataItem gridDataItem2 = (GridDataItem)e.Item;
				bool flag7 = !(gridDataItem2.DataItem is DataRowView);
				if (!flag7)
				{
					DataRow row = ((DataRowView)gridDataItem2.DataItem).Row;
					bool flag8 = row["iscourse"] != DBNull.Value && Convert.ToBoolean(row["iscourse"]);
					DateTime contextDateTime = (DateTime)row["startdate"];
					bool flag9 = row["Tentative"] != DBNull.Value && Convert.ToBoolean(row["Tentative"]);
					Panel panel = (Panel)gridDataItem2["col_option"].FindControl("p_cancel2");
					Panel panel2 = (Panel)gridDataItem2["col_option"].FindControl("p_confirm");
					Panel panel3 = (Panel)gridDataItem2["col_status"].FindControl("p_optionsMessage");
					LinkButton linkButton = (panel == null) ? null : ((LinkButton)gridDataItem2["col_option"].FindControl("link_cancel2"));
					bool flag10 = panel != null;
					if (flag10)
					{
						bool flag11 = linkButton != null;
						if (flag11)
						{
							bool flag12 = flag8;
							if (flag12)
							{
								linkButton.Visible = (cutoffTime.Enabled ? (cutoffTime.IsRightNowBeforeCutoffTime(contextDateTime) ?? flag3) : flag3);
								bool flag13 = panel2 != null;
								if (flag13)
								{
									bool flag14 = flag9 && flag4;
									if (flag14)
									{
										panel2.Visible = true;
										gridDataItem2.BackColor = Color.LightGoldenrodYellow;
									}
									else
									{
										panel2.Visible = false;
										bool flag15 = flag4 && !flag9 && flag8 && panel3 != null;
										if (flag15)
										{
											panel3.Visible = true;
											Label label = (Label)panel3.FindControl("lbl_optionsMessage");
											bool flag16 = label != null;
											if (flag16)
											{
												label.Text = "Confirmed.";
											}
										}
									}
								}
							}
							else
							{
								bool enabled = this.AppointmentCancelCutoff.Enabled;
								if (enabled)
								{
									bool? flag17 = this.AppointmentCancelCutoff.IsRightNowBeforeCutoffTime(contextDateTime);
									linkButton.Visible = (flag17 ?? flag3);
								}
								else
								{
									linkButton.Visible = flag3;
								}
								bool flag18 = panel2 != null;
								if (flag18)
								{
									panel2.Visible = (flag9 && flag5);
								}
							}
						}
						bool flag19 = panel2 != null && panel2.Visible;
						if (flag19)
						{
							bool? flag20 = !this.ConfirmCutoffStart.IsRightNowBeforeCutoffTime(contextDateTime);
							bool? flag21 = this.ConfirmCutoffEnd.IsRightNowBeforeCutoffTime(contextDateTime);
							bool flag22 = flag20 != null && flag20.Value;
							if (flag22)
							{
								bool flag23 = panel2 != null;
								if (flag23)
								{
									panel2.Visible = false;
								}
							}
							else
							{
								bool flag24 = flag21 != null && flag21.Value;
								if (flag24)
								{
									bool flag25 = panel2 != null;
									if (flag25)
									{
										panel2.Visible = false;
									}
								}
							}
						}
					}
					Label label2 = (Label)gridDataItem2["col_location"].FindControl("lbl_date25");
					bool flag26 = label2 != null;
					if (flag26)
					{
						bool flag27 = flag8;
						if (flag27)
						{
							bool enabled2 = this.ShowTestLocationOnMyUpcomingEvents_StartShowingDate.Enabled;
							if (enabled2)
							{
								bool? flag28 = this.ShowTestLocationOnMyUpcomingEvents_StartShowingDate.IsRightNowBeforeCutoffTime(contextDateTime);
								bool flag29 = flag28 != null && flag28.Value;
								bool flag30 = !flag29;
								if (flag30)
								{
									int[] array = this.ShowTestLocationFormat;
									bool flag31 = array == null || array.Length < 1;
									if (flag31)
									{
										array = new int[]
										{
											1
										};
									}
									StringBuilder stringBuilder = new StringBuilder();
									string text = row["room"].ToString().Trim();
									bool flag32 = Array.IndexOf<int>(array, 1) >= 0;
									if (flag32)
									{
										stringBuilder.AppendFormat("{0} ", text);
									}
									bool flag33 = Array.IndexOf<int>(array, 2) >= 0;
									if (flag33)
									{
										stringBuilder.AppendFormat("{0} ", row["location"].ToString().Trim());
									}
									bool flag34 = Array.IndexOf<int>(array, 4) >= 0;
									if (flag34)
									{
										int num = text.IndexOf(' ');
										stringBuilder.AppendFormat("{0} ", (num > 0) ? text.Substring(num + 1) : text);
									}
									label2.Text = stringBuilder.ToString();
								}
								else
								{
									label2.Text = "to be determined";
								}
							}
							else
							{
								label2.Text = "";
							}
						}
						else
						{
							bool showLocation = this.showLocation;
							if (showLocation)
							{
								label2.Text = row["room"].ToString().Trim();
							}
						}
					}
					bool enabled3 = this.ShowOnlyClassTimeCutoff.Enabled;
					if (enabled3)
					{
						bool flag35 = row["showingclasstime"] != DBNull.Value && Convert.ToBoolean(row["showingclasstime"]);
						Panel c = (Panel)gridDataItem2["col_datetime"].FindControl("p_scheduledDateAndTime");
						Panel c2 = (Panel)gridDataItem2["col_datetime"].FindControl("p_classDateAndTime");
						Panel c3 = (Panel)gridDataItem2["col_datetime"].FindControl("p_scheduleddatetimenote");
						this.ShowOrHideControl(c3, true);
						this.ShowOrHideControl(c, !flag35);
						this.ShowOrHideControl(c2, flag35);
					}
				}
			}
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0003EBAC File Offset: 0x0003CDAC
		private void ShowOrHideControl(Control c, bool showIt)
		{
			bool flag = c != null;
			if (flag)
			{
				c.Visible = showIt;
			}
		}

		// Token: 0x04000677 RID: 1655
		protected Label lbl_title;

		// Token: 0x04000678 RID: 1656
		protected Panel p_info;

		// Token: 0x04000679 RID: 1657
		protected Label lbl_info;

		// Token: 0x0400067A RID: 1658
		protected Panel p_msg;

		// Token: 0x0400067B RID: 1659
		protected Label lbl_msg;

		// Token: 0x0400067C RID: 1660
		protected Panel p_refreshtop;

		// Token: 0x0400067D RID: 1661
		protected Label lbl_gridTitle;

		// Token: 0x0400067E RID: 1662
		protected Panel p_onlyShowingClassDateTime;

		// Token: 0x0400067F RID: 1663
		protected Label lbl_onlyShowingClassDateTime;

		// Token: 0x04000680 RID: 1664
		protected Button btn_refresh0;

		// Token: 0x04000681 RID: 1665
		protected RadGrid RadGrid1;

		// Token: 0x04000682 RID: 1666
		protected Panel p_refreshMain;

		// Token: 0x04000683 RID: 1667
		protected LinkButton btn_export;

		// Token: 0x04000684 RID: 1668
		protected System.Web.UI.WebControls.Image img_export;

		// Token: 0x04000685 RID: 1669
		protected Button btn_export2;

		// Token: 0x04000686 RID: 1670
		protected Button btn_refresh;

		// Token: 0x04000687 RID: 1671
		protected Panel p_waitingList;

		// Token: 0x04000688 RID: 1672
		protected Panel p_waitingListTitle;

		// Token: 0x04000689 RID: 1673
		protected Label lbl_waitListTitle;

		// Token: 0x0400068A RID: 1674
		protected RadGrid grid_waitingList;

		// Token: 0x0400068C RID: 1676
		private bool isFacilitator = false;

		// Token: 0x0400068D RID: 1677
		private bool _isDisabled = false;

		// Token: 0x0400068E RID: 1678
		private bool? _showLocation;

		// Token: 0x0400068F RID: 1679
		private bool? makeStudentsConfirmApps = null;

		// Token: 0x04000690 RID: 1680
		private bool? makeStudentsConfirmTests = null;

		// Token: 0x04000691 RID: 1681
		private bool? studentsCanCancelApps = null;

		// Token: 0x04000692 RID: 1682
		private CutoffTime studentsCanCancelTestsCutoffTime = null;

		// Token: 0x04000693 RID: 1683
		private CutoffTime appointmentCancelCutoff = null;

		// Token: 0x04000694 RID: 1684
		private CutoffTime showOnlyClassTimeCutoff = null;

		// Token: 0x04000695 RID: 1685
		private CutoffTime confirmCutoffStart = null;

		// Token: 0x04000696 RID: 1686
		private CutoffTime confirmCutoffEnd = null;

		// Token: 0x04000697 RID: 1687
		private CutoffTime showTestLocationOnMyUpcomingEvents_StartShowingDate = null;

		// Token: 0x04000698 RID: 1688
		private int[] showTestLocationFormat = null;

		// Token: 0x0200023C RID: 572
		// (Invoke) Token: 0x06000ECC RID: 3788
		public delegate void AppointmentEventhandler(object sender, int appId, EventArgs e);
	}
}
