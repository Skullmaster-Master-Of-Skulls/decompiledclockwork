using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ClockWorkWebAPI.Settings;
using Databases;

namespace ClockWorkWebAPI
{
	// Token: 0x0200000B RID: 11
	[Serializable]
	public class Appointments
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00004EA8 File Offset: 0x000030A8
		public static DateTime FixDate(DateTime dateOnly)
		{
			return new DateTime(dateOnly.Year, dateOnly.Month, dateOnly.Day);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004ED4 File Offset: 0x000030D4
		private static string IntArrayToStringList(int[] intArray)
		{
			return Appointments.IntArrayToStringList(intArray, ",");
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004EF4 File Offset: 0x000030F4
		private static string IntArrayToStringList(int[] intArray, string separator)
		{
			bool flag = intArray == null || intArray.Length < 1;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				string text = "";
				for (int i = 0; i < intArray.Length; i++)
				{
					text = text + ((i > 0) ? separator : "") + intArray[i].ToString();
				}
				result = text;
			}
			return result;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004F5C File Offset: 0x0000315C
		public static DataTable GetAppointments(db conn, DateTime startDate, DateTime endDate, int[] personids, int[] apptypeids, bool includeCancelledApps)
		{
			string appTypeIdsStr = Appointments.IntArrayToStringList(apptypeids);
			string pidsStr = Appointments.IntArrayToStringList(personids);
			return Appointments.GetAppointments(conn, startDate, endDate, pidsStr, appTypeIdsStr, includeCancelledApps);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004F8C File Offset: 0x0000318C
		public static DataTable GetAppointments(db conn, DateTime startDate, DateTime endDate, List<int> personids, List<int> apptypeids, bool includeCancelledApps)
		{
			string appTypeIdsStr = AppSettingsV2.IntListToString(apptypeids);
			string pidsStr = AppSettingsV2.IntListToString(personids);
			return Appointments.GetAppointments(conn, startDate, endDate, pidsStr, appTypeIdsStr, includeCancelledApps);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004FBC File Offset: 0x000031BC
		public static DataTable GetAppointments(db conn, DateTime startDate, DateTime endDate, string pidsStr, string appTypeIdsStr, bool includeCancelledApps)
		{
			startDate = Appointments.FixDate(startDate);
			endDate = Appointments.FixDate(endDate);
			bool flag = startDate == endDate;
			if (flag)
			{
				endDate = startDate.AddMinutes(1439.0);
			}
			else
			{
				endDate = endDate.AddMinutes(1.0);
			}
			conn.Da.SelectCommand.CommandText = "SELECT\ta1.appointmentid,a1.apptypeid,at.description,at.defaultcolour,atg.title AS apptypegrouptitle,\r\n\t\t\ta1.startdate,a1.enddate,a1.cancelled,\r\n\t\t\tatt.personid,att.noshow,att.misccode,am.memotext,ai.screennum,ai.iconnum,\r\n\t\t\ta1.dateadded,a1.whoadded,am.isencrypted,a1.ishidden,a1.islocked,a1.overridecolour,\r\n\t\t\tp.firstname,p.lastname,p.student_no,pg.groupid,aw.workshopid,ac.lucourseid,w.workshoptitle,\r\n\t\t\tw.maxattendees,lucd.altlookupstring AS subject,lc.course,a1.extraattendeescount,\r\n\t\t\ta1.appcode,a1.groupcode,ac.originalstartdatetime,ac.originalenddatetime,ac.testnote,\r\n\t\t\tlucd2.altlookupstring,lucd2.email,lucd2.phone,lc.section,ac.studentnote\r\n\tFROM\t(\tSELECT\tDISTINCT app.appointmentid,app.apptypeid,app.startdate,app.enddate,app.cancelled,\r\n\t\t\t\t\t\tapp.dateadded,app.personid AS whoadded,app.ishidden,app.islocked,app.overridecolour,\r\n\t\t\t\t\t\tapp.extraattendeescount,app.appcode,app.groupcode\r\n\t\t\t\tFROM\tattendees att LEFT JOIN appointments app ON app.appointmentid=att.appointmentid\r\n\t\t\t\tWHERE\t(@personids = '' OR att.personid IN (SELECT orderid AS personid FROM SplitOrderIDs( @personids,',' )))\r\n\t\t\t\t\t\tAND ((app.startdate>=@startdate and app.startdate <@enddate) or (app.enddate>=@startdate AND app.enddate<@enddate))\r\n\t\t\t\t\t\tAND (@includeCancelledApps=-1 OR (@includeCancelledApps=0 AND app.cancelled=0) OR (@includeCancelledApps=1 AND app.cancelled=1))\r\n\t\t\t\t\t\tAND (@apptypeids = '' OR app.apptypeid IN (SELECT orderid AS apptypeid FROM SplitOrderIDs( @appTypeIds,',' ) ) )\r\n\t\t\t) a1 LEFT JOIN appointmentmemos am ON am.appointmentid=a1.appointmentid\r\n\t\t\tLEFT JOIN appointmenticons ai ON ai.appointmentid=a1.appointmentid\r\n\t\t\tLEFT JOIN attendees att ON att.appointmentid=a1.appointmentid\r\n\t\t\tLEFT JOIN people p ON p.personid=att.personid\r\n\t\t\tLEFT JOIN peoplegroups pg ON pg.personid=att.personid AND pg.isprimarygroup=1\r\n\t\t\tLEFT JOIN appointmentworkshops aw ON aw.appointmentid=a1.appointmentid\r\n\t\t\tLEFT JOIN appointmentcourses ac ON ac.appointmentid=a1.appointmentid\r\n\t\t\tLEFT JOIN workshops w ON w.workshopid=aw.workshopid\r\n\t\t\tLEFT JOIN lucourses lc ON lc.lucourseid=ac.lucourseid\r\n\t\t\tLEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=lc.subjectid\r\n\t\t\tLEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=lc.instructorid\r\n\t\t\tLEFT JOIN appointmenttypes at ON at.apptypeid=a1.apptypeid\r\n\t\t\tLEFT JOIN appointmenttypegroups atg ON atg.appointmenttypegroupid=at.appointmenttypegroupid\r\n\tORDER BY a1.startdate,a1.appointmentid,pg.groupid,att.personid,ai.screennum,ai.iconnum";
			conn.Da.SelectCommand.Parameters.Clear();
			conn.Da.SelectCommand.Parameters.AddWithValue("@startdate", startDate);
			conn.Da.SelectCommand.Parameters.AddWithValue("@enddate", endDate);
			conn.Da.SelectCommand.Parameters.AddWithValue("@personids", pidsStr);
			conn.Da.SelectCommand.Parameters.AddWithValue("@apptypeids", appTypeIdsStr);
			conn.Da.SelectCommand.Parameters.AddWithValue("@includeCancelledApps", includeCancelledApps);
			DataTable dataTable = new DataTable();
			conn.Da.Fill(dataTable);
			Appointments.RemoveDuplicateRows(ref dataTable, "appointmentid");
			return dataTable;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00005100 File Offset: 0x00003300
		private static void RemoveDuplicateRows(ref DataTable t, string colNameWithUniqueId)
		{
			ArrayList arrayList = new ArrayList();
			int num = -2;
			foreach (object obj in t.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num2 = (int)dataRow[colNameWithUniqueId];
				bool flag = num2 != num;
				if (flag)
				{
					num = num2;
				}
				else
				{
					arrayList.Add(dataRow);
				}
			}
			foreach (object obj2 in arrayList)
			{
				DataRow row = (DataRow)obj2;
				t.Rows.Remove(row);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000051E4 File Offset: 0x000033E4
		[Obsolete]
		public static void NoshowUnnoshowAppointment(int appid, int personid, db conn, bool newNoshow)
		{
			conn.Da.SelectCommand.CommandText = "UPDATE attendees SET noshow=@noshow WHERE appointmentid=@appid AND personid=@pid";
			conn.Da.SelectCommand.Parameters.Clear();
			conn.Da.SelectCommand.Parameters.Add("@appid", appid);
			conn.Da.SelectCommand.Parameters.Add("@pid", personid);
			conn.Da.SelectCommand.Parameters.Add("@noshow", newNoshow);
			conn.Da.Fill(new DataTable());
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005294 File Offset: 0x00003494
		[Obsolete]
		public static void NoshowUnnoshowAppointment2(int appid, string noshowAnyoneNotInTheseGroups, bool newNoshow, int facilitatorPid)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			string query = "UPDATE attendees SET noshow=@noshow WHERE appointmentid=@appid AND NOT personid=@pid AND NOT personid IN (SELECT personid FROM peoplegroups WHERE groupid IN (SELECT orderid AS groupid FROM splitorderids(@gids,',')))";
			clockWork.ExecuteNonQuery(query, new DbParameter[]
			{
				clockWork.GetParameter("@appid", DbType.Int32, appid),
				clockWork.GetParameter("@gids", DbType.String, noshowAnyoneNotInTheseGroups),
				clockWork.GetParameter("@noshow", DbType.Boolean, newNoshow),
				clockWork.GetParameter("@pid", DbType.Int32, facilitatorPid)
			});
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00005310 File Offset: 0x00003510
		public static int GetNoShowCount(db conn, int pid, DateTime sinceDate)
		{
			conn.Da.SelectCommand.CommandText = "SELECT DISTINCT a.appointmentid FROM apps a WHERE a.personid=@pid AND a.startdate>=@sdate AND a.startdate<=getdate() AND a.noshow=1";
			conn.Da.SelectCommand.Parameters.Clear();
			conn.Da.SelectCommand.Parameters.Add("@pid", pid);
			conn.Da.SelectCommand.Parameters.Add("@sdate", sinceDate);
			DataTable dataTable = new DataTable();
			conn.Da.Fill(dataTable);
			return dataTable.Rows.Count;
		}
	}
}
