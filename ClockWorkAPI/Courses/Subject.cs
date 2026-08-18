using System;
using System.Data;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;
using UnivOleDb;

namespace ClockWorkAPI.Courses
{
	// Token: 0x02000008 RID: 8
	public class Subject
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002674 File Offset: 0x00001674
		// (set) Token: 0x06000015 RID: 21 RVA: 0x0000268C File Offset: 0x0000168C
		public int SubjectId
		{
			get
			{
				return this.subjectId;
			}
			set
			{
				this.subjectId = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002698 File Offset: 0x00001698
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000026B0 File Offset: 0x000016B0
		public string SubjectDescription
		{
			get
			{
				return this.subjectDescription;
			}
			set
			{
				this.subjectDescription = value;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000026BA File Offset: 0x000016BA
		public Subject()
		{
			this.subjectDescription = "";
			this.subjectId = 0;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000026D7 File Offset: 0x000016D7
		public Subject(int subjectId, string description)
		{
			this.subjectId = subjectId;
			this.subjectDescription = description;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000026F0 File Offset: 0x000016F0
		public Subject(string description)
		{
			this.subjectId = 0;
			this.subjectDescription = description;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000270C File Offset: 0x0000170C
		public static void LookupSubjectId(ref Subject subject, bool createSubjectIfItDoesntExist)
		{
			if (!string.IsNullOrEmpty(subject.SubjectDescription))
			{
				UnivDataAdapter da = ClientCache.CurrentInstance.da;
				string commandText;
				if (createSubjectIfItDoesntExist)
				{
					commandText = "IF EXISTS(SELECT lucoursedataid FROM lucourses WHERE lookuplisttype=0 AND altlookupstring=@desc)\r\nBEGIN\r\n    SELECT lucoursedataid FROM lucourses WHERE lookuplisttype=0 AND altlookupstring=@desc\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO lucoursedata (lookuplisttype,lookupstring,altlookupstring) VALUES (0,@desc,@desc);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS lucourseid\r\nEND";
				}
				else
				{
					commandText = "SELECT lucoursedataid FROM lucourses WHERE lookuplisttype=0 AND altlookupstring=@desc";
				}
				DataTable dataTable = new DataTable();
				da.SelectCommand.CommandText = commandText;
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@desc", subject.SubjectDescription.Trim());
				da.Fill(dataTable);
				if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
				{
					subject.SubjectId = (int)dataTable.Rows[0][0];
				}
			}
		}

		// Token: 0x04000004 RID: 4
		private int subjectId;

		// Token: 0x04000005 RID: 5
		private string subjectDescription;
	}
}
