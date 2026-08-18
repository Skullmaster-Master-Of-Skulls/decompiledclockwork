using System;
using System.Collections.Generic;
using System.Data;
using Databases;

namespace ClockWorkController
{
	// Token: 0x02000007 RID: 7
	public class Semester
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00003B58 File Offset: 0x00001D58
		public eSemester ESemester
		{
			get
			{
				return this.eSemester;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00003B70 File Offset: 0x00001D70
		public string Title
		{
			get
			{
				return this.title;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003B88 File Offset: 0x00001D88
		public Semester()
		{
			this.eSemester = eSemester.Unknown;
			this.title = "?";
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003BA4 File Offset: 0x00001DA4
		public Semester(eSemester eSemester, string title)
		{
			this.eSemester = eSemester;
			this.title = title;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003BBC File Offset: 0x00001DBC
		public Semester(DataRow dr)
		{
			int num = (dr["lucoursesessiondateid"] == DBNull.Value) ? 0 : ((int)dr["lucoursesessiondateid"]);
			this.title = dr["description"].ToString().Replace("Session", "").Trim();
			switch (num)
			{
			case 1:
				this.eSemester = eSemester.Fall;
				return;
			case 2:
				this.eSemester = eSemester.Summer;
				return;
			case 3:
				this.eSemester = eSemester.Winter;
				return;
			case 5:
				this.eSemester = eSemester.Spring;
				return;
			}
			this.eSemester = eSemester.Unknown;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003C70 File Offset: 0x00001E70
		public static List<Semester> LoadSemesters()
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			string query = "SELECT lucoursesessiondateid,description FROM lucoursesessiondate";
			DataTable dataTable = clockWork.ExecuteQuery(query);
			List<Semester> list = new List<Semester>();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dr = (DataRow)obj;
				Semester item = new Semester(dr);
				list.Add(item);
			}
			Semester semester = list.Find((Semester e) => e.ESemester == eSemester.Spring);
			Semester semester2 = list.Find((Semester f) => f.ESemester == eSemester.Summer);
			bool flag = semester != null && semester2 != null;
			if (flag)
			{
				list.Add(new Semester(eSemester.SpringSummer, string.Format("{0} / {1}", semester.Title, semester2.Title)));
			}
			return list;
		}

		// Token: 0x04000009 RID: 9
		private eSemester eSemester;

		// Token: 0x0400000A RID: 10
		private string title;
	}
}
