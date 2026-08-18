using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Databases;
using EncryptionClassLibrary;
using UnivOleDb;

namespace ClockWorkWebAPI.ClockWorkAPIReplacement
{
	// Token: 0x0200004E RID: 78
	public class AccommodationList : List<Accommodation>
	{
		// Token: 0x060003BD RID: 957 RVA: 0x0001B21C File Offset: 0x0001941C
		public AccommodationList(int personId, DateTime startDate, DateTime endDate, UnivDataAdapter da, IEncryption tripleDES, bool useAccommodationsApprovalSystem)
		{
			this.useAccommodationsApprovalSystem = useAccommodationsApprovalSystem;
			this.personId = personId;
			this.courses = Course.LoadStudentsCourses(da, tripleDES, personId, startDate, endDate, false);
			string commandText = "SELECT ad.*,a.longdescription\r\nFROM accommodationdataactive ad LEFT JOIN accommodations a ON a.controlid=ad.controlid\r\nWHERE ad.personid=@pid AND (ad.courseid=0 OR ad.courseid IN (SELECT orderid AS courseid FROM splitorderids(@lucids,',')))\r\nORDER BY ad.courseid";
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@pid", personId);
			da.SelectCommand.Parameters.Add("@lucids", Course.CourseLucidsToStringCommaSeparated(this.courses));
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			dataTable = tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
			{
				"altlongdescription",
				"note",
				"rationale"
			});
			foreach (object obj in dataTable.Rows)
			{
				DataRow dr = (DataRow)obj;
				Accommodation item = new Accommodation(dr);
				base.Add(item);
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0001B354 File Offset: 0x00019554
		public AccommodationList(int personId, int lucid, UnivDataAdapter da, IEncryption tripleDES, bool useAccommodationsApprovalSystem)
		{
			this.useAccommodationsApprovalSystem = useAccommodationsApprovalSystem;
			this.personId = personId;
			string commandText = "SELECT ad.*,a.longdescription\r\nFROM accommodationdataactive ad LEFT JOIN accommodations a ON a.controlid=ad.controlid\r\nWHERE ad.personid=@pid AND ad.courseid=dbo.AccommodationsCourseOrTemplate(@pid,@lucid)\r\nORDER BY ad.courseid";
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@pid", personId);
			da.SelectCommand.Parameters.Add("@lucid", lucid);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			dataTable = tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
			{
				"altlongdescription",
				"note",
				"rationale"
			});
			foreach (object obj in dataTable.Rows)
			{
				DataRow dr = (DataRow)obj;
				Accommodation item = new Accommodation(dr);
				base.Add(item);
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0001B470 File Offset: 0x00019670
		public AccommodationList(int personId, int lucid, bool useAccommodationsApprovalSystem)
		{
			this.useAccommodationsApprovalSystem = useAccommodationsApprovalSystem;
			this.personId = personId;
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			string query = "SELECT ad.*,a.longdescription\r\nFROM accommodationdataactive ad LEFT JOIN accommodations a ON a.controlid=ad.controlid\r\nWHERE ad.personid=@pid AND ad.courseid=dbo.AccommodationsCourseOrTemplate(@pid,@lucid)\r\nORDER BY ad.courseid";
			DataTable dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
			{
				clockWork.GetParameter("@pid", DbType.Int32, personId),
				clockWork.GetParameter("@lucid", DbType.Int32, lucid)
			});
			dataTable = encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
			{
				"altlongdescription",
				"note",
				"rationale"
			});
			foreach (object obj in dataTable.Rows)
			{
				DataRow dr = (DataRow)obj;
				Accommodation item = new Accommodation(dr);
				base.Add(item);
			}
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0001B578 File Offset: 0x00019778
		public string GetAccommodationsSummaryHtml(bool includePrivateNote)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<Accommodation> accommodations = this.GetAccommodations(0);
			this.GetAccommodationsSummaryHtmlForSection(ref stringBuilder, 0, accommodations, includePrivateNote);
			foreach (Course course in this.courses)
			{
				List<Accommodation> accommodations2 = this.GetAccommodations(course.LuCourseId);
				this.GetAccommodationsSummaryHtmlForSection(ref stringBuilder, course.LuCourseId, accommodations2, includePrivateNote);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0001B610 File Offset: 0x00019810
		private string GetAccommodationsSummaryHtmlForSection(ref StringBuilder sb, int lucourseId, List<Accommodation> accommodations, bool includePrivateNote)
		{
			bool flag = lucourseId == 0;
			bool flag2;
			if (flag)
			{
				sb.Append("<h2>Template accommodations</h2>");
				flag2 = false;
			}
			else
			{
				Course courseById = Course.GetCourseById(this.courses, lucourseId);
				flag2 = (accommodations.Count > 0 && accommodations[0].Lucid == 0);
				sb.Append(string.Format("{0}{1}{2}{3}", new object[]
				{
					"<h2>",
					(courseById == null) ? "Unknown" : courseById.ToString(),
					flag2 ? " * Same as template * " : "",
					"</h2>"
				}));
			}
			bool flag3 = !flag2;
			if (flag3)
			{
				sb.Append("<ul>");
				foreach (Accommodation accommodation in accommodations)
				{
					sb.Append("<li>");
					sb.Append(accommodation.ToStringHtml(this.useAccommodationsApprovalSystem, includePrivateNote));
					sb.Append("</li>");
				}
				sb.Append("</ul>");
			}
			return sb.ToString();
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0001B754 File Offset: 0x00019954
		public List<Accommodation> GetAccommodations(int lucid)
		{
			bool flag = lucid > 0;
			int num;
			if (flag)
			{
				num = (this.HasAccommodationsSpecificallyForCourse(lucid) ? lucid : 0);
			}
			else
			{
				num = lucid;
			}
			List<Accommodation> list = new List<Accommodation>();
			foreach (Accommodation accommodation in this)
			{
				bool flag2 = accommodation.Lucid == num;
				if (flag2)
				{
					list.Add(accommodation);
				}
			}
			return list;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0001B7E4 File Offset: 0x000199E4
		public bool HasAccommodationsSpecificallyForCourse(int lucid)
		{
			foreach (Accommodation accommodation in this)
			{
				bool flag = lucid == accommodation.Lucid;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040001EB RID: 491
		private int personId;

		// Token: 0x040001EC RID: 492
		private List<Course> courses;

		// Token: 0x040001ED RID: 493
		private bool useAccommodationsApprovalSystem = false;
	}
}
