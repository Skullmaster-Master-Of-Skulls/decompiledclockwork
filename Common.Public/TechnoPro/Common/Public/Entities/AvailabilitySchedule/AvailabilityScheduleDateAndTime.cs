using System;

namespace TechnoPro.Common.Public.Entities.AvailabilitySchedule
{
	// Token: 0x0200047B RID: 1147
	public class AvailabilityScheduleDateAndTime : ICloneable<AvailabilityScheduleDateAndTime>, ICloneable
	{
		// Token: 0x060022A1 RID: 8865 RVA: 0x0000D55A File Offset: 0x0000B75A
		public AvailabilityScheduleDateAndTime()
		{
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x0002674C File Offset: 0x0002494C
		public AvailabilityScheduleDateAndTime(AvailabilityScheduleDateAndTime item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.Date = item.Date;
				AvailabilityScheduleTime time = item.Time;
				this.Time = ((time != null) ? time.Clone() : null);
			}
		}

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x060022A3 RID: 8867 RVA: 0x00026791 File Offset: 0x00024991
		// (set) Token: 0x060022A4 RID: 8868 RVA: 0x00026799 File Offset: 0x00024999
		public DateTime Date { get; set; }

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x060022A5 RID: 8869 RVA: 0x000267A2 File Offset: 0x000249A2
		// (set) Token: 0x060022A6 RID: 8870 RVA: 0x000267AA File Offset: 0x000249AA
		public AvailabilityScheduleTime Time { get; set; }

		// Token: 0x060022A7 RID: 8871 RVA: 0x000267B4 File Offset: 0x000249B4
		public AvailabilityScheduleDateAndTime Clone()
		{
			return new AvailabilityScheduleDateAndTime(this);
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x000267CC File Offset: 0x000249CC
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
