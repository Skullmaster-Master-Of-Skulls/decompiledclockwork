using System;
using System.Collections;
using System.Data;

namespace AutoComboBox
{
	// Token: 0x020000B8 RID: 184
	public class DateScopes
	{
		// Token: 0x060006EE RID: 1774 RVA: 0x000383F2 File Offset: 0x000373F2
		public DateScopes(DataTable dateRangesTable)
		{
			this.Init(dateRangesTable, DateTime.Now);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0003840A File Offset: 0x0003740A
		public DateScopes(DataTable dateRangesTable, DateTime today)
		{
			this.Init(dateRangesTable, today);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00038420 File Offset: 0x00037420
		private void Init(DataTable dateRangesTable, DateTime today)
		{
			if (dateRangesTable != null && dateRangesTable.Rows.Count > 0)
			{
				this.dateScopes = new DateScope[dateRangesTable.Rows.Count];
				for (int i = 0; i < dateRangesTable.Rows.Count; i++)
				{
					DataRow dataRow = dateRangesTable.Rows[i];
					string description = dataRow[1].ToString();
					int startMonth = (int)dataRow[2];
					int endMonth = (int)dataRow[3];
					int numYearsBetween = (int)dataRow[4];
					int num = (int)dataRow[5];
					int startDay = (int)dataRow[6];
					int endDay = (int)dataRow[7];
					DateScope dateScope = new DateScope(startMonth, startDay, endMonth, endDay, numYearsBetween, description);
					this.dateScopes[i] = dateScope;
				}
			}
			else
			{
				this.dateScopes = null;
			}
			this.SetScope(today);
			int num2 = 0;
			while (this.dateScope == null && today.Day > 28 && num2++ < 10)
			{
				this.Init(dateRangesTable, today.AddDays(-1.0));
			}
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0003856C File Offset: 0x0003756C
		public void SetScope(DateTime date)
		{
			if (this.dateScopes != null)
			{
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = new ArrayList();
				foreach (DateScope dateScope in this.dateScopes)
				{
					DateTime dateTime = dateScope.GetStartDate(date);
					if (dateTime != DateTime.MinValue)
					{
						arrayList.Add(dateTime);
						arrayList2.Add(dateScope);
					}
				}
				DateTime t = DateTime.MaxValue;
				DateScope dateScope2 = null;
				for (int j = 0; j < arrayList.Count; j++)
				{
					DateTime dateTime = (DateTime)arrayList[j];
					if (dateTime < t)
					{
						t = dateTime;
						dateScope2 = (DateScope)arrayList2[j];
					}
				}
				if (dateScope2 == null)
				{
					this.startDate = DateTime.MinValue;
					this.endDate = DateTime.MaxValue;
					this.dateScope = null;
				}
				else
				{
					this.startDate = t;
					this.endDate = new DateTime(this.startDate.Year + dateScope2.numYearsBetween, dateScope2.endMonth, dateScope2.endDay);
					this.dateScope = dateScope2;
				}
			}
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x000386C0 File Offset: 0x000376C0
		public int Count()
		{
			int result;
			if (this.dateScopes == null)
			{
				result = 0;
			}
			else
			{
				result = this.dateScopes.Length;
			}
			return result;
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x000386F0 File Offset: 0x000376F0
		public void MoveScope(int numYears)
		{
			DateTime scope = this.startDate.AddYears(numYears);
			this.SetScope(scope);
			this.startDate = this.startDate;
			this.endDate = this.endDate;
		}

		// Token: 0x04000565 RID: 1381
		public DateScope[] dateScopes;

		// Token: 0x04000566 RID: 1382
		public DateTime startDate;

		// Token: 0x04000567 RID: 1383
		public DateTime endDate;

		// Token: 0x04000568 RID: 1384
		public DateScope dateScope;
	}
}
