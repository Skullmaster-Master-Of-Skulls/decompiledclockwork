using System;

namespace TechnoPro.Common.Public.Entities.DataSync.DataSyncCourses
{
	// Token: 0x020003F9 RID: 1017
	public class DataSyncExternalCourseFinalExamInfo : IComparable
	{
		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06001F1C RID: 7964 RVA: 0x00022D03 File Offset: 0x00020F03
		// (set) Token: 0x06001F1D RID: 7965 RVA: 0x00022D0B File Offset: 0x00020F0B
		public string ExternalId { get; set; }

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06001F1E RID: 7966 RVA: 0x00022D14 File Offset: 0x00020F14
		// (set) Token: 0x06001F1F RID: 7967 RVA: 0x00022D1C File Offset: 0x00020F1C
		public DateTime? StartDateTime { get; set; }

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06001F20 RID: 7968 RVA: 0x00022D25 File Offset: 0x00020F25
		// (set) Token: 0x06001F21 RID: 7969 RVA: 0x00022D2D File Offset: 0x00020F2D
		public DateTime? EndDateTime { get; set; }

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06001F22 RID: 7970 RVA: 0x00022D36 File Offset: 0x00020F36
		// (set) Token: 0x06001F23 RID: 7971 RVA: 0x00022D3E File Offset: 0x00020F3E
		public string Location { get; set; }

		// Token: 0x06001F24 RID: 7972 RVA: 0x00022D48 File Offset: 0x00020F48
		public bool IsSameAs(DataSyncExternalCourseFinalExamInfo item)
		{
			bool flag = item == null;
			return !flag && this.CompareTo(item) == 0;
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x00022D70 File Offset: 0x00020F70
		public int CompareTo(object obj)
		{
			bool flag = obj == null || !(obj is DataSyncExternalCourseFinalExamInfo);
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				DataSyncExternalCourseFinalExamInfo dataSyncExternalCourseFinalExamInfo = (DataSyncExternalCourseFinalExamInfo)obj;
				int num = this.CompareDateTime(this.StartDateTime, dataSyncExternalCourseFinalExamInfo.StartDateTime);
				bool flag2 = num != 0;
				if (flag2)
				{
					result = num;
				}
				else
				{
					num = this.CompareDateTime(this.EndDateTime, dataSyncExternalCourseFinalExamInfo.EndDateTime);
					bool flag3 = num != 0;
					if (flag3)
					{
						result = num;
					}
					else
					{
						string text = this.Location ?? "";
						string text2 = dataSyncExternalCourseFinalExamInfo.Location ?? "";
						bool flag4 = !text.Trim().Equals(text2, StringComparison.OrdinalIgnoreCase);
						if (flag4)
						{
							result = text.CompareTo(text2);
						}
						else
						{
							result = 0;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x00022E34 File Offset: 0x00021034
		private int CompareDateTime(DateTime? d1, DateTime? d2)
		{
			bool flag = d1 == null && d2 == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = d1 == null;
				if (flag2)
				{
					result = -1;
				}
				else
				{
					bool flag3 = d2 == null;
					if (flag3)
					{
						result = 1;
					}
					else
					{
						DateTime dateTime = d1.Value.Date.AddMinutes(d1.Value.TimeOfDay.TotalMinutes);
						DateTime value = d2.Value.Date.AddMinutes(d2.Value.TimeOfDay.TotalMinutes);
						result = dateTime.CompareTo(value);
					}
				}
			}
			return result;
		}
	}
}
