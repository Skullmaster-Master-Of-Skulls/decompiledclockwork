using System;
using System.Collections.Generic;
using System.Data;

namespace ClockWorkWebAPI.TestBooking
{
	// Token: 0x0200003C RID: 60
	[Serializable]
	public class DateRange
	{
		// Token: 0x0600031D RID: 797 RVA: 0x00017B94 File Offset: 0x00015D94
		public DateRange(DateTime startDate, DateTime endDate)
		{
			this.startDate = startDate;
			this.endDate = endDate;
			this.scope = 0;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00017BB3 File Offset: 0x00015DB3
		public DateRange(int scope, DateTime startDate, DateTime endDate)
		{
			this.startDate = startDate;
			this.endDate = endDate;
			this.scope = scope;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00017BD4 File Offset: 0x00015DD4
		public DateRange(DataRow dr)
		{
			this.startDate = (DateTime)dr["startdate"];
			this.endDate = (DateTime)dr["enddate"];
			bool flag = dr.Table.Columns.Contains("personid");
			if (flag)
			{
				this.scope = ((dr["personid"] == DBNull.Value) ? 0 : ((int)dr["personid"]));
			}
			else
			{
				this.scope = 0;
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00017C64 File Offset: 0x00015E64
		public bool Intersects(DateTime sdt, DateTime edt)
		{
			return !(edt <= this.startDate) && !(sdt >= this.endDate);
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000321 RID: 801 RVA: 0x00017C98 File Offset: 0x00015E98
		// (set) Token: 0x06000322 RID: 802 RVA: 0x00017CB0 File Offset: 0x00015EB0
		public DateTime StartDate
		{
			get
			{
				return this.startDate;
			}
			set
			{
				this.startDate = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00017CBC File Offset: 0x00015EBC
		// (set) Token: 0x06000324 RID: 804 RVA: 0x00017CD4 File Offset: 0x00015ED4
		public DateTime EndDate
		{
			get
			{
				return this.endDate;
			}
			set
			{
				this.endDate = value;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00017CE0 File Offset: 0x00015EE0
		// (set) Token: 0x06000326 RID: 806 RVA: 0x00017CF8 File Offset: 0x00015EF8
		public int Scope
		{
			get
			{
				return this.scope;
			}
			set
			{
				this.scope = value;
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00017D04 File Offset: 0x00015F04
		public int ShiftTimeToMatchStartTime(int hourMilitary, int minute)
		{
			DateTime d = new DateTime(this.startDate.Year, this.startDate.Month, this.startDate.Day, hourMilitary, minute, 0);
			int num = Convert.ToInt32((this.startDate - d).TotalMinutes);
			this.startDate = d;
			this.endDate = this.endDate.AddMinutes((double)(-(double)num));
			return num;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00017D78 File Offset: 0x00015F78
		public static List<DateRange> FromTable(DataTable t)
		{
			List<DateRange> list = new List<DateRange>();
			foreach (object obj in t.Rows)
			{
				DataRow dr = (DataRow)obj;
				list.Add(new DateRange(dr));
			}
			return list;
		}

		// Token: 0x04000191 RID: 401
		private int scope;

		// Token: 0x04000192 RID: 402
		private DateTime startDate;

		// Token: 0x04000193 RID: 403
		private DateTime endDate;
	}
}
