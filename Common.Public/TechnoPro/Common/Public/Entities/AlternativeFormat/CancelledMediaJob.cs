using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x02000591 RID: 1425
	public class CancelledMediaJob : MediaJob
	{
		// Token: 0x17001373 RID: 4979
		// (get) Token: 0x06002E49 RID: 11849 RVA: 0x00032FB8 File Offset: 0x000311B8
		// (set) Token: 0x06002E4A RID: 11850 RVA: 0x00032FC0 File Offset: 0x000311C0
		public DateTime CancelledOn { get; set; }

		// Token: 0x17001374 RID: 4980
		// (get) Token: 0x06002E4B RID: 11851 RVA: 0x00032FCC File Offset: 0x000311CC
		// (set) Token: 0x06002E4C RID: 11852 RVA: 0x00032FDF File Offset: 0x000311DF
		public override bool IsCancelled
		{
			get
			{
				return true;
			}
			set
			{
				base.IsCancelled = value;
			}
		}

		// Token: 0x17001375 RID: 4981
		// (get) Token: 0x06002E4D RID: 11853 RVA: 0x00032FEC File Offset: 0x000311EC
		// (set) Token: 0x06002E4E RID: 11854 RVA: 0x00032FFF File Offset: 0x000311FF
		public override bool IsCompleted
		{
			get
			{
				return false;
			}
			set
			{
				base.IsCompleted = value;
			}
		}

		// Token: 0x17001376 RID: 4982
		// (get) Token: 0x06002E4F RID: 11855 RVA: 0x0003300A File Offset: 0x0003120A
		// (set) Token: 0x06002E50 RID: 11856 RVA: 0x00033012 File Offset: 0x00031212
		public PersonBase CancelledBy { get; set; }

		// Token: 0x17001377 RID: 4983
		// (get) Token: 0x06002E51 RID: 11857 RVA: 0x0003301B File Offset: 0x0003121B
		// (set) Token: 0x06002E52 RID: 11858 RVA: 0x00033023 File Offset: 0x00031223
		public string CancellationReason { get; set; }
	}
}
