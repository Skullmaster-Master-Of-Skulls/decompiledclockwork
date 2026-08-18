using System;

namespace TechnoPro.Common.Public.Entities.Intake
{
	// Token: 0x02000328 RID: 808
	public class IntakeStatus : BusinessBase<Guid>
	{
		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x0600192E RID: 6446 RVA: 0x0001DC6C File Offset: 0x0001BE6C
		// (set) Token: 0x0600192F RID: 6447 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		public virtual Guid IntakeStatusId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06001930 RID: 6448 RVA: 0x0001DC84 File Offset: 0x0001BE84
		// (set) Token: 0x06001931 RID: 6449 RVA: 0x0001DC8C File Offset: 0x0001BE8C
		public string Title { get; set; }

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06001932 RID: 6450 RVA: 0x0001DC95 File Offset: 0x0001BE95
		// (set) Token: 0x06001933 RID: 6451 RVA: 0x0001DC9D File Offset: 0x0001BE9D
		public string Description { get; set; }

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06001934 RID: 6452 RVA: 0x0001DCA6 File Offset: 0x0001BEA6
		// (set) Token: 0x06001935 RID: 6453 RVA: 0x0001DCAE File Offset: 0x0001BEAE
		public int BackgroundColor { get; set; }

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06001936 RID: 6454 RVA: 0x0001DCB7 File Offset: 0x0001BEB7
		// (set) Token: 0x06001937 RID: 6455 RVA: 0x0001DCBF File Offset: 0x0001BEBF
		public bool IsInactive { get; set; }

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06001938 RID: 6456 RVA: 0x0001DCC8 File Offset: 0x0001BEC8
		// (set) Token: 0x06001939 RID: 6457 RVA: 0x0001DCD0 File Offset: 0x0001BED0
		public int OrderNum { get; set; }
	}
}
