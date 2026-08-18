using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ClockWorkWebAPI
{
	// Token: 0x02000020 RID: 32
	public class Notetaking
	{
		// Token: 0x060001E2 RID: 482 RVA: 0x0000DD04 File Offset: 0x0000BF04
		public static DataTable LoadNotetakeeCourses(db conn, int pid, DateTime startDate, DateTime endDate)
		{
			SqlDataAdapter da = conn.Da;
			da.SelectCommand.CommandText = "SELECT c.personid,c.coursesid,c.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,luc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,lucd.altlookupstring AS subject,lucd2.altlookupstring AS instructor,lucd2.email AS instructoremail,lucd2.phone AS instructorphone,'' AS session, luc.term + ' ' + lucd.altlookupstring + ' ' + luc.course + luc.timeofday + ' ' + luc.section AS coursedescription FROM\tcourses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid WHERE\tc.personid=@personid AND luc.enddate >= @startdate ORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.AddWithValue("@personid", pid);
			da.SelectCommand.Parameters.AddWithValue("@startdate", startDate);
			da.SelectCommand.Parameters.AddWithValue("@enddate", endDate);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			return dataTable;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000DDA4 File Offset: 0x0000BFA4
		public static DataTable GetLectureNotes(DataRow courseRow, string path, string template, string lectureDateFormat)
		{
			string text = template.Replace("#<subject>#", courseRow["subject"].ToString());
			text = text.Replace("#<course>#", courseRow["course"].ToString());
			text = text.Replace("#<section>#", courseRow["section"].ToString());
			text = text.Replace("#<classdate>#", "");
			text = text.Replace("#<classdate2>#", "");
			text += "*.*";
			string[] files = Directory.GetFiles(path, text);
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("lecturedate");
			dataTable.Columns.Add("coursedescription");
			dataTable.Columns.Add("filename");
			dataTable.Columns.Add("lecturedate2", typeof(DateTime));
			string value = courseRow["coursedescription"].ToString();
			foreach (string text2 in files)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["filename"] = text2;
				dataRow["coursedescription"] = value;
				string text3 = Path.GetFileNameWithoutExtension(text2);
				int num = text3.IndexOf("___");
				bool flag = num > 0;
				if (flag)
				{
					text3 = text3.Substring(0, num);
				}
				int num2 = text3.LastIndexOf('_');
				bool flag2 = num2 > 0;
				if (flag2)
				{
					string s = text3.Substring(num2 + 1).Replace('.', '-');
					DateTime dateTime;
					try
					{
						dateTime = DateTime.Parse(s);
					}
					catch
					{
						dateTime = DateTime.MinValue;
					}
					bool flag3 = dateTime != DateTime.MinValue;
					if (flag3)
					{
						dataRow["lecturedate2"] = dateTime;
						dataRow["lecturedate"] = dateTime.ToString(lectureDateFormat);
					}
					else
					{
						dataRow["lecturedate"] = text3.Substring(num2 + 1);
						dataRow["lecturedate2"] = DBNull.Value;
					}
				}
				else
				{
					dataRow["lecturedate"] = text3;
				}
				dataTable.Rows.Add(dataRow);
			}
			return dataTable;
		}
	}
}
