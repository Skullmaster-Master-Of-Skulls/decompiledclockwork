using System;

namespace TechnoPro.Common.Public.Entities.DataSync
{
	// Token: 0x020003D5 RID: 981
	public class DataSyncExternalCourseTimetableItem : IComparable
	{
		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x06001E47 RID: 7751 RVA: 0x00021D84 File Offset: 0x0001FF84
		// (set) Token: 0x06001E48 RID: 7752 RVA: 0x00021D8C File Offset: 0x0001FF8C
		public DayOfWeek DayOfWeek { get; set; }

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x06001E49 RID: 7753 RVA: 0x00021D95 File Offset: 0x0001FF95
		// (set) Token: 0x06001E4A RID: 7754 RVA: 0x00021D9D File Offset: 0x0001FF9D
		public TimeSpan StartTime { get; set; }

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06001E4B RID: 7755 RVA: 0x00021DA6 File Offset: 0x0001FFA6
		// (set) Token: 0x06001E4C RID: 7756 RVA: 0x00021DAE File Offset: 0x0001FFAE
		public TimeSpan EndTime { get; set; }

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06001E4D RID: 7757 RVA: 0x00021DB7 File Offset: 0x0001FFB7
		// (set) Token: 0x06001E4E RID: 7758 RVA: 0x00021DBF File Offset: 0x0001FFBF
		public string Room { get; set; }

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06001E4F RID: 7759 RVA: 0x00021DC8 File Offset: 0x0001FFC8
		// (set) Token: 0x06001E50 RID: 7760 RVA: 0x00021DD0 File Offset: 0x0001FFD0
		public DataSyncExternalCourseInstructor Instructor { get; set; }

		// Token: 0x06001E51 RID: 7761 RVA: 0x00021DDC File Offset: 0x0001FFDC
		public bool IsSameAs(DataSyncExternalCourseTimetableItem item)
		{
			return this.CompareTo(item) == 0;
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x00021DF8 File Offset: 0x0001FFF8
		public int CompareTo(object obj)
		{
			bool flag = obj == null || !(obj is DataSyncExternalCourseTimetableItem);
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				DataSyncExternalCourseTimetableItem dataSyncExternalCourseTimetableItem = (DataSyncExternalCourseTimetableItem)obj;
				int num = this.DayOfWeek.CompareTo(dataSyncExternalCourseTimetableItem.DayOfWeek);
				bool flag2 = num != 0;
				if (flag2)
				{
					result = num;
				}
				else
				{
					num = this.StartTime.CompareTo(dataSyncExternalCourseTimetableItem.StartTime);
					bool flag3 = num != 0;
					if (flag3)
					{
						result = num;
					}
					else
					{
						num = this.EndTime.CompareTo(dataSyncExternalCourseTimetableItem.EndTime);
						bool flag4 = num != 0;
						if (flag4)
						{
							result = num;
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
	}
}
