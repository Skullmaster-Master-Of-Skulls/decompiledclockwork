using System;

namespace NewBooker.Entities.AutoTestBooking.Booker2
{
	// Token: 0x020000B4 RID: 180
	public class TryToBookTimeToInvestigate : ICloneable
	{
		// Token: 0x0600047B RID: 1147 RVA: 0x0000D55A File Offset: 0x0000B75A
		public TryToBookTimeToInvestigate()
		{
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000D564 File Offset: 0x0000B764
		public TryToBookTimeToInvestigate(TryToBookTimeToInvestigate timeToInvestigate)
		{
			this.StartDateTime = timeToInvestigate.StartDateTime;
			this.EndDateTime = timeToInvestigate.EndDateTime;
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000D588 File Offset: 0x0000B788
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x0000D590 File Offset: 0x0000B790
		public DateTime StartDateTime { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0000D599 File Offset: 0x0000B799
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x0000D5A1 File Offset: 0x0000B7A1
		public DateTime EndDateTime { get; set; }

		// Token: 0x06000481 RID: 1153 RVA: 0x0000D5AC File Offset: 0x0000B7AC
		public TryToBookTimeToInvestigate Clone()
		{
			return new TryToBookTimeToInvestigate(this);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000D5C4 File Offset: 0x0000B7C4
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
