using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using EncryptionClassLibrary;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;
using UnivOleDb;

namespace ClockWorkAPI
{
	// Token: 0x020000A4 RID: 164
	public class AccommodationList : List<Accommodation>
	{
		// Token: 0x06000820 RID: 2080 RVA: 0x00031534 File Offset: 0x00030534
		public AccommodationList(int personId, DateTime startDate, DateTime endDate, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, bool useAccommodationsApprovalSystem)
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

		// Token: 0x06000821 RID: 2081 RVA: 0x00031684 File Offset: 0x00030684
		public AccommodationList(int personId, int lucid, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, bool useAccommodationsApprovalSystem)
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

		// Token: 0x06000822 RID: 2082 RVA: 0x000317B4 File Offset: 0x000307B4
		public AccommodationList(int personId, int lucid, bool useAccommodationsApprovalSystem)
		{
			this.useAccommodationsApprovalSystem = useAccommodationsApprovalSystem;
			this.personId = personId;
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
			string commandText = "SELECT ad.*,a.longdescription\r\nFROM accommodationdataactive ad LEFT JOIN accommodations a ON a.controlid=ad.controlid\r\nWHERE ad.personid=@pid AND ad.courseid=dbo.AccommodationsCourseOrTemplate(@pid,@lucid)\r\nORDER BY ad.courseid";
			DataTable dataTable = new DataTable();
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@pid", personId);
			da.SelectCommand.Parameters.Add("@lucid", lucid);
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

		// Token: 0x06000823 RID: 2083 RVA: 0x000318FC File Offset: 0x000308FC
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

		// Token: 0x06000824 RID: 2084 RVA: 0x00031998 File Offset: 0x00030998
		private string GetAccommodationsSummaryHtmlForSection(ref StringBuilder sb, int lucourseId, List<Accommodation> accommodations, bool includePrivateNote)
		{
			bool flag;
			if (lucourseId == 0)
			{
				sb.Append("<h2>Template accommodations</h2>");
				flag = false;
			}
			else
			{
				Course courseById = Course.GetCourseById(this.courses, lucourseId);
				flag = (accommodations.Count > 0 && accommodations[0].Lucid == 0);
				sb.Append(string.Format("{0}{1}{2}{3}", new object[]
				{
					"<h2>",
					(courseById == null) ? "Unknown" : courseById.ToString(),
					flag ? " * Same as template * " : "",
					"</h2>"
				}));
			}
			if (!flag)
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

		// Token: 0x06000825 RID: 2085 RVA: 0x00031AF0 File Offset: 0x00030AF0
		public List<Accommodation> GetAccommodations(int lucid)
		{
			int num;
			if (lucid > 0)
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
				if (accommodation.Lucid == num)
				{
					list.Add(accommodation);
				}
			}
			return list;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00031B88 File Offset: 0x00030B88
		public bool HasAccommodationsSpecificallyForCourse(int lucid)
		{
			foreach (Accommodation accommodation in this)
			{
				if (lucid == accommodation.Lucid)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000422 RID: 1058
		private int personId;

		// Token: 0x04000423 RID: 1059
		private List<Course> courses;

		// Token: 0x04000424 RID: 1060
		private bool useAccommodationsApprovalSystem = false;
	}
}
