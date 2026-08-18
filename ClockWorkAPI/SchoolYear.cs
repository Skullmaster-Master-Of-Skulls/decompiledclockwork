using System;

namespace ClockWorkAPI
{
	// Token: 0x02000098 RID: 152
	public class SchoolYear
	{
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x0002CB0C File Offset: 0x0002BB0C
		public DateTime StartDate
		{
			get
			{
				return this.startDate;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x0002CB24 File Offset: 0x0002BB24
		public DateTime EndDate
		{
			get
			{
				return this.endDate;
			}
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0002CB3C File Offset: 0x0002BB3C
		public SchoolYear(DateTime startDate, DateTime endDate)
		{
			this.startDate = startDate;
			this.endDate = endDate;
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0002CB58 File Offset: 0x0002BB58
		public static SchoolYear GetSchoolYearStartEndDates(DateTime date)
		{
			DateTime dateTime = date;
			int month = dateTime.Month;
			DateTime dateTime2;
			DateTime dateTime3;
			if (month >= 9)
			{
				dateTime2 = new DateTime(dateTime.Year, 5, 1);
				dateTime3 = new DateTime(dateTime.Year + 1, 4, 30);
			}
			else if (month >= 5)
			{
				dateTime2 = new DateTime(dateTime.Year, 5, 1);
				dateTime3 = new DateTime(dateTime.Year + 1, 4, 30);
			}
			else
			{
				dateTime2 = new DateTime(dateTime.Year - 1, 5, 1);
				dateTime3 = new DateTime(dateTime.Year, 4, 30);
			}
			return new SchoolYear(dateTime2, dateTime3);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0002CC0C File Offset: 0x0002BC0C
		public static SchoolYear GetCourseTermStartEndDates(DateTime date)
		{
			DateTime dateTime = date;
			int month = dateTime.Month;
			DateTime dateTime2;
			DateTime dateTime3;
			if (month >= 9)
			{
				dateTime2 = new DateTime(dateTime.Year, 9, 1);
				dateTime3 = new DateTime(dateTime.Year + 1, 4, 30);
			}
			else if (month >= 5)
			{
				dateTime2 = new DateTime(dateTime.Year, 5, 1);
				dateTime3 = new DateTime(dateTime.Year, 8, 30);
			}
			else
			{
				dateTime2 = new DateTime(dateTime.Year, 1, 1);
				dateTime3 = new DateTime(dateTime.Year, 4, 30);
			}
			return new SchoolYear(dateTime2, dateTime3);
		}

		// Token: 0x040003E8 RID: 1000
		private DateTime startDate;

		// Token: 0x040003E9 RID: 1001
		private DateTime endDate;
	}
}
