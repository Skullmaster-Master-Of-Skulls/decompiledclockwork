using System;
using System.Data;

namespace ClockWorkAPI
{
	// Token: 0x0200001F RID: 31
	public class ActionPlanItem
	{
		// Token: 0x0600012A RID: 298 RVA: 0x00008030 File Offset: 0x00007030
		public ActionPlanItem()
		{
			this.personid = 0;
			this.advisorid = 0;
			this.dateCreated = DateTime.Now;
			this.dueDate = DateTime.MinValue;
			this.titleId = 0;
			this.progress = 0;
			this.advisorNotes = "";
			this.studentNotes = "";
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00008090 File Offset: 0x00007090
		public ActionPlanItem(DataRow dr)
		{
			int num = (dr["personid"] != DBNull.Value) ? ((int)dr["personid"]) : 0;
			int num2 = (dr["advisorpersonid"] != DBNull.Value) ? ((int)dr["advisorpersonid"]) : 0;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000080F2 File Offset: 0x000070F2
		public void Update(DateTime dueDate, int titleId, string title, int progress, string advisorNotes, string studentNotes)
		{
			this.dueDate = dueDate;
			this.titleId = titleId;
			this.title = title;
			this.progress = progress;
			this.advisorNotes = advisorNotes;
			this.studentNotes = studentNotes;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00008124 File Offset: 0x00007124
		public int TitleId
		{
			get
			{
				return this.titleId;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600012E RID: 302 RVA: 0x0000813C File Offset: 0x0000713C
		public string Title
		{
			get
			{
				return this.title;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00008154 File Offset: 0x00007154
		public int Progress
		{
			get
			{
				return this.progress;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000816C File Offset: 0x0000716C
		public string AdvisorNotes
		{
			get
			{
				return this.advisorNotes;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00008184 File Offset: 0x00007184
		public DateTime DueDate
		{
			get
			{
				return this.dueDate;
			}
		}

		// Token: 0x040000A9 RID: 169
		private int personid;

		// Token: 0x040000AA RID: 170
		private int advisorid;

		// Token: 0x040000AB RID: 171
		private DateTime dateCreated;

		// Token: 0x040000AC RID: 172
		private DateTime dueDate;

		// Token: 0x040000AD RID: 173
		private int titleId;

		// Token: 0x040000AE RID: 174
		private string title;

		// Token: 0x040000AF RID: 175
		private int progress;

		// Token: 0x040000B0 RID: 176
		private string advisorNotes;

		// Token: 0x040000B1 RID: 177
		private string studentNotes;
	}
}
