using System;
using System.Collections;
using System.Data;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity
{
	// Token: 0x02000012 RID: 18
	public class CourseStartEndDateRuleCollection : CollectionBase
	{
		// Token: 0x0600015B RID: 347 RVA: 0x00023C15 File Offset: 0x00021E15
		public CourseStartEndDateRuleCollection()
		{
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00023C20 File Offset: 0x00021E20
		private string[] SplitStringIntoNEWLINE_delimitered_parts(string s, bool excludeEmptyStrings)
		{
			string[] array = s.Split(Environment.NewLine.ToCharArray());
			if (excludeEmptyStrings)
			{
				ArrayList arrayList = new ArrayList();
				foreach (string text in array)
				{
					bool flag = text.Trim().Length > 0;
					if (flag)
					{
						arrayList.Add(text.Trim());
					}
				}
				array = new string[arrayList.Count];
				for (int j = 0; j < arrayList.Count; j++)
				{
					array[j] = (string)arrayList[j];
				}
			}
			return array;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00023CD0 File Offset: 0x00021ED0
		public CourseStartEndDateRuleCollection(string defn)
		{
			string[] array = this.SplitStringIntoNEWLINE_delimitered_parts(defn, true);
			foreach (string defn2 in array)
			{
				CourseStartEndDateRule value = new CourseStartEndDateRule(defn2);
				base.List.Add(value);
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00023D1C File Offset: 0x00021F1C
		public int Add(CourseStartEndDateRule rule)
		{
			return base.List.Add(rule);
		}

		// Token: 0x17000026 RID: 38
		public CourseStartEndDateRule this[int index]
		{
			get
			{
				return (CourseStartEndDateRule)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00023D70 File Offset: 0x00021F70
		public void CalculateStartEndDates(DataRow dr, out DateTime sdate, out DateTime edate)
		{
			CourseStartEndDateRule courseStartEndDateRule = null;
			foreach (object obj in base.List)
			{
				CourseStartEndDateRule courseStartEndDateRule2 = (CourseStartEndDateRule)obj;
				bool isDefault = courseStartEndDateRule2.IsDefault;
				if (isDefault)
				{
					courseStartEndDateRule = courseStartEndDateRule2;
				}
				else
				{
					bool flag = courseStartEndDateRule2.Matches(dr);
					if (flag)
					{
						courseStartEndDateRule2.CalculateStartEndDates(dr, out sdate, out edate);
						return;
					}
				}
			}
			courseStartEndDateRule.CalculateStartEndDates(dr, out sdate, out edate);
		}
	}
}
