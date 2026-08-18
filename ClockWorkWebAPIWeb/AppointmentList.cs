using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.SessionState;
using ClockWorkWebAPI;
using ClockWorkWebAPI.AuthenticationAuthorization;
using TechnoPro.Common.Configuration;
using Telerik.Web.UI;

namespace ClockWorkWebAPIWeb
{
	// Token: 0x02000003 RID: 3
	public class AppointmentList
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000022EC File Offset: 0x000004EC
		public void Add(AppointmentInfo ai)
		{
			List<AppointmentInfo> list = AppointmentList.AllData();
			list.Add(ai);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002308 File Offset: 0x00000508
		public static void SetAllData(List<AppointmentInfo> apps)
		{
			HttpContext.Current.Session.Add("Scheduler.GettingStarted_Apts", apps);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002324 File Offset: 0x00000524
		public static List<AppointmentInfo> AllData()
		{
			List<AppointmentInfo> list = HttpContext.Current.Session["Scheduler.GettingStarted_Apts"] as List<AppointmentInfo>;
			bool flag = list == null;
			if (flag)
			{
				list = new List<AppointmentInfo>();
				HttpContext.Current.Session["Scheduler.GettingStarted_Apts"] = list;
			}
			return list;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002378 File Offset: 0x00000578
		public static void InsertAppointment(string Subject, DateTime Start, DateTime End, string RecurrenceRule, object RecurrenceParentID, RecurrenceState RecurrenceState, object TutorId)
		{
			HttpSessionState session = HttpContext.Current.Session;
			UserInfo userInfo = ClockWorkWebCore.GetUserInfo(session);
			bool flag = userInfo != null && userInfo.ClockworkPid > 0;
			if (flag)
			{
				db db = new db(ClockWorkConfigurationManager.GetConnectionStringByNameUsingProtection("clockwork"));
				db.Da.SelectCommand.CommandText = "SELECT personid FROM peoplegroups WHERE (groupid=10 OR groupid=2) AND personid=" + userInfo.ClockworkPid.ToString();
				DataTable dataTable = new DataTable();
				db.Da.Fill(dataTable);
				bool flag2 = dataTable.Rows.Count > 0;
				if (flag2)
				{
					db.Da.SelectCommand.CommandText = "INSERT INTO appointments (startdate,enddate,apptypeid,cancelled,dateadded,personid,ishidden,islocked,appcode,groupcode,subject) VALUES (@startdate,@enddate,@apptypeid,0,getdate(),@pid,0,0,0,-1,@subject)";
					db.Da.SelectCommand.Parameters.Clear();
					db.Da.SelectCommand.Parameters.Add("@startdate", Start);
					db.Da.SelectCommand.Parameters.Add("@enddate", End);
					db.Da.SelectCommand.Parameters.Add("@apptypeid", -1);
					db.Da.SelectCommand.Parameters.Add("@pid", userInfo.ClockworkPid);
					db.Da.SelectCommand.Parameters.Add("@subject", db.TripleDES.Encrypt(Subject));
					SqlCommand selectCommand = db.Da.SelectCommand;
					selectCommand.CommandText += "; SELECT appointmentid FROM appointments WHERE appointmentid=@@identity";
					dataTable = new DataTable();
					db.Da.Fill(dataTable);
					bool flag3 = dataTable.Rows.Count > 0;
					if (flag3)
					{
						int num = (int)dataTable.Rows[0][0];
						db.Da.SelectCommand.CommandText = "INSERT INTO attendees (appointmentid,personid,noshow,misccode) VALUES (@appid,@pid,0,-1)";
						db.Da.SelectCommand.Parameters.Clear();
						db.Da.SelectCommand.Parameters.Add("@appid", num);
						db.Da.SelectCommand.Parameters.Add("@pid", userInfo.ClockworkPid);
						db.Da.Fill(new DataTable());
						List<AppointmentInfo> list = AppointmentList.AllData();
						AppointmentInfo item = new AppointmentInfo(num.ToString(), userInfo.ClockworkPid.ToString(), Subject, Start, End, -1);
						list.Add(item);
					}
				}
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002616 File Offset: 0x00000816
		public static void DeleteAppointment(string ID)
		{
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000261C File Offset: 0x0000081C
		public static void UpdateAppointment(string ID, string Subject, DateTime Start, DateTime End, string RecurrenceRule, object RecurrenceParentID, RecurrenceState RecurrenceState, object tutorID)
		{
			List<AppointmentInfo> sessApts = AppointmentList.AllData();
			AppointmentInfo appointmentInfo = AppointmentList.FindById(ID, sessApts);
			appointmentInfo.Subject = Subject;
			appointmentInfo.Start = Start;
			appointmentInfo.End = End;
			try
			{
				int num = int.Parse(ID);
				HttpSessionState session = HttpContext.Current.Session;
				UserInfo userInfo = ClockWorkWebCore.GetUserInfo(session);
				bool flag = userInfo != null && userInfo.ClockworkPid > 0;
				if (flag)
				{
					db db = new db(ClockWorkConfigurationManager.GetConnectionStringByNameUsingProtection("clockwork"));
					db.Da.SelectCommand.CommandText = "SELECT personid FROM peoplegroups WHERE (groupid=10 OR groupid=2) AND personid=" + userInfo.ClockworkPid.ToString();
					DataTable dataTable = new DataTable();
					db.Da.Fill(dataTable);
					bool flag2 = dataTable.Rows.Count > 0;
					if (flag2)
					{
						db.Da.SelectCommand.CommandText = "UPDATE appointments SET startdate=@startdate,enddate=@enddate WHERE appointmentid=@appid";
						db.Da.SelectCommand.Parameters.Clear();
						db.Da.SelectCommand.Parameters.Add("@startdate", Start);
						db.Da.SelectCommand.Parameters.Add("@enddate", End);
						db.Da.SelectCommand.Parameters.Add("@appid", num);
						dataTable = new DataTable();
						db.Da.Fill(dataTable);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000027C4 File Offset: 0x000009C4
		public static AppointmentInfo FindById(string ID, List<AppointmentInfo> sessApts)
		{
			foreach (AppointmentInfo appointmentInfo in sessApts)
			{
				bool flag = appointmentInfo.ID == ID;
				if (flag)
				{
					return appointmentInfo;
				}
			}
			return null;
		}

		// Token: 0x0400000D RID: 13
		private const string AppointmentsKey = "Scheduler.GettingStarted_Apts";
	}
}
