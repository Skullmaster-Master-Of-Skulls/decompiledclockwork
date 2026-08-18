using System;
using System.Collections.Generic;
using System.Data;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x0200053C RID: 1340
	[Serializable]
	public class DateRange
	{
		// Token: 0x06002AB6 RID: 10934 RVA: 0x0002D9BC File Offset: 0x0002BBBC
		public DateRange(DateTime startDate, DateTime endDate)
		{
			this.startDate = startDate;
			this.endDate = endDate;
			this.scope = 0;
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x0002D9DB File Offset: 0x0002BBDB
		public DateRange(int scope, DateTime startDate, DateTime endDate)
		{
			this.startDate = startDate;
			this.endDate = endDate;
			this.scope = scope;
		}

		// Token: 0x06002AB8 RID: 10936 RVA: 0x0002D9FC File Offset: 0x0002BBFC
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

		// Token: 0x06002AB9 RID: 10937 RVA: 0x0002DA8C File Offset: 0x0002BC8C
		public bool Intersects(DateTime sdt, DateTime edt)
		{
			return !(edt <= this.startDate) && !(sdt >= this.endDate);
		}

		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x06002ABA RID: 10938 RVA: 0x0002DAC0 File Offset: 0x0002BCC0
		// (set) Token: 0x06002ABB RID: 10939 RVA: 0x0002DAD8 File Offset: 0x0002BCD8
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

		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x06002ABC RID: 10940 RVA: 0x0002DAE4 File Offset: 0x0002BCE4
		// (set) Token: 0x06002ABD RID: 10941 RVA: 0x0002DAFC File Offset: 0x0002BCFC
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

		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x06002ABE RID: 10942 RVA: 0x0002DB08 File Offset: 0x0002BD08
		// (set) Token: 0x06002ABF RID: 10943 RVA: 0x0002DB20 File Offset: 0x0002BD20
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

		// Token: 0x06002AC0 RID: 10944 RVA: 0x0002DB2C File Offset: 0x0002BD2C
		public int ShiftTimeToMatchStartTime(int hourMilitary, int minute)
		{
			DateTime d = new DateTime(this.startDate.Year, this.startDate.Month, this.startDate.Day, hourMilitary, minute, 0);
			int num = Convert.ToInt32((this.startDate - d).TotalMinutes);
			this.startDate = d;
			this.endDate = this.endDate.AddMinutes((double)(-(double)num));
			return num;
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x0002DBA0 File Offset: 0x0002BDA0
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

		// Token: 0x04001E6B RID: 7787
		private int scope;

		// Token: 0x04001E6C RID: 7788
		private DateTime startDate;

		// Token: 0x04001E6D RID: 7789
		private DateTime endDate;
	}
}
