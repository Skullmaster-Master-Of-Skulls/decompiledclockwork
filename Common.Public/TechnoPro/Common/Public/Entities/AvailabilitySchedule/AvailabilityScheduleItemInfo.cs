using System;

namespace TechnoPro.Common.Public.Entities.AvailabilitySchedule
{
	// Token: 0x0200047D RID: 1149
	public class AvailabilityScheduleItemInfo : ICloneable<AvailabilityScheduleItemInfo>, ICloneable
	{
		// Token: 0x060022B0 RID: 8880 RVA: 0x0000D55A File Offset: 0x0000B75A
		public AvailabilityScheduleItemInfo()
		{
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x00026818 File Offset: 0x00024A18
		public AvailabilityScheduleItemInfo(AvailabilityScheduleItemInfo item)
		{
			bool flag = item == null;
			if (!flag)
			{
				AvailabilityScheduleDateAndTime dayAndTime = item.DayAndTime;
				this.DayAndTime = ((dayAndTime != null) ? dayAndTime.Clone() : null);
			}
		}

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x060022B2 RID: 8882 RVA: 0x00026850 File Offset: 0x00024A50
		// (set) Token: 0x060022B3 RID: 8883 RVA: 0x00026858 File Offset: 0x00024A58
		public AvailabilityScheduleDateAndTime DayAndTime { get; set; }

		// Token: 0x060022B4 RID: 8884 RVA: 0x00026864 File Offset: 0x00024A64
		public AvailabilityScheduleItemInfo Clone()
		{
			return new AvailabilityScheduleItemInfo(this);
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x0002687C File Offset: 0x00024A7C
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
