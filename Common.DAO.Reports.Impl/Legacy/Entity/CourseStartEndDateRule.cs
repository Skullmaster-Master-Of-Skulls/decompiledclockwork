using System;
using System.Data;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity
{
	// Token: 0x02000013 RID: 19
	public class CourseStartEndDateRule
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00023E00 File Offset: 0x00022000
		public bool IsDefault
		{
			get
			{
				return this.isDefault;
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00023E18 File Offset: 0x00022018
		public CourseStartEndDateRule(string defn)
		{
			string[] array = defn.Split(new char[]
			{
				':'
			});
			bool flag = array.Length == 4;
			string text;
			if (flag)
			{
				this.isDefault = false;
				this.colName = array[0];
				this.matchStr = array[1].ToLower();
				this.startDateExistingCol = array[2];
				text = array[3];
			}
			else
			{
				this.isDefault = true;
				this.colName = "";
				this.matchStr = "";
				this.startDateExistingCol = array[1];
				text = array[2];
			}
			int num = text.IndexOf('-');
			bool flag2 = num > 0;
			if (flag2)
			{
				this.startMonth = int.Parse(text.Substring(0, 2));
				this.startDay = int.Parse(text.Substring(3, 2));
				this.endMonth = int.Parse(text.Substring(6, 2));
				this.endDay = int.Parse(text.Substring(9, 2));
				this.courseDurationInMonths = 0;
			}
			else
			{
				this.startMonth = 0;
				this.startDay = 0;
				num = text.IndexOf('/');
				bool flag3 = num > 0;
				if (flag3)
				{
					this.endMonth = int.Parse(text.Substring(0, 2));
					this.endDay = int.Parse(text.Substring(3, 2));
					this.courseDurationInMonths = 0;
				}
				else
				{
					this.endMonth = 0;
					this.endDay = 0;
					this.courseDurationInMonths = int.Parse(text);
				}
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00023F80 File Offset: 0x00022180
		public bool Matches(DataRow dr)
		{
			bool flag = !this.isDefault;
			bool result;
			if (flag)
			{
				string text = dr[this.colName].ToString().ToLower().Trim();
				result = (text.CompareTo(this.matchStr) == 0);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00023FD0 File Offset: 0x000221D0
		private static DateTime ParseDateTime(string s)
		{
			bool flag = s.Trim().Length > 0;
			if (flag)
			{
				try
				{
					return DateTime.Parse(s);
				}
				catch
				{
					return DateTime.MinValue;
				}
			}
			return DateTime.MinValue;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00024020 File Offset: 0x00022220
		public void CalculateStartEndDates(DataRow dr, out DateTime sdate, out DateTime edate)
		{
			DateTime dateTime = CourseStartEndDateRule.ParseDateTime(dr[this.startDateExistingCol].ToString());
			bool flag = dateTime == DateTime.MinValue;
			if (!flag)
			{
				bool flag2 = this.courseDurationInMonths > 0 && this.startMonth == 0;
				if (flag2)
				{
					sdate = dateTime;
					edate = dateTime.AddMonths(this.courseDurationInMonths);
					return;
				}
				bool flag3 = this.courseDurationInMonths == 0 && this.startMonth == 0 && this.endMonth > 0;
				if (flag3)
				{
					sdate = dateTime;
					edate = new DateTime(sdate.Year, this.endMonth, this.endDay);
					bool flag4 = edate < sdate;
					if (flag4)
					{
						edate = new DateTime(sdate.Year + 1, this.endMonth, this.endDay);
					}
					return;
				}
			}
			sdate = dateTime;
			edate = dateTime.AddMonths(4);
		}

		// Token: 0x0400004C RID: 76
		private string colName;

		// Token: 0x0400004D RID: 77
		private string matchStr;

		// Token: 0x0400004E RID: 78
		private int startMonth;

		// Token: 0x0400004F RID: 79
		private int startDay;

		// Token: 0x04000050 RID: 80
		private int endMonth;

		// Token: 0x04000051 RID: 81
		private int endDay;

		// Token: 0x04000052 RID: 82
		private string startDateExistingCol;

		// Token: 0x04000053 RID: 83
		private int courseDurationInMonths;

		// Token: 0x04000054 RID: 84
		private bool isDefault;
	}
}
