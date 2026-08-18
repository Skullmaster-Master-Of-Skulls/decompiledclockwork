using System;

namespace TechnoPro.Common.Public.Entities.AvailabilitySchedule
{
	// Token: 0x0200047F RID: 1151
	public class AvailabilityScheduleTime : ICloneable<AvailabilityScheduleTime>, ICloneable
	{
		// Token: 0x060022BB RID: 8891 RVA: 0x0000D55A File Offset: 0x0000B75A
		public AvailabilityScheduleTime()
		{
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x000268B8 File Offset: 0x00024AB8
		public AvailabilityScheduleTime(AvailabilityScheduleTime item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.StartTime = item.StartTime;
				this.EndTime = item.EndTime;
			}
		}

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x060022BD RID: 8893 RVA: 0x000268F1 File Offset: 0x00024AF1
		// (set) Token: 0x060022BE RID: 8894 RVA: 0x000268F9 File Offset: 0x00024AF9
		public TimeSpan StartTime { get; set; }

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x060022BF RID: 8895 RVA: 0x00026902 File Offset: 0x00024B02
		// (set) Token: 0x060022C0 RID: 8896 RVA: 0x0002690A File Offset: 0x00024B0A
		public TimeSpan EndTime { get; set; }

		// Token: 0x060022C1 RID: 8897 RVA: 0x00026914 File Offset: 0x00024B14
		public AvailabilityScheduleTime Clone()
		{
			return new AvailabilityScheduleTime(this);
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x0002692C File Offset: 0x00024B2C
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
