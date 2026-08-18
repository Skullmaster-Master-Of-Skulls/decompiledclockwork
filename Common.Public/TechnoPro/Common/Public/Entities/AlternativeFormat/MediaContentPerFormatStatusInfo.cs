using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x0200057C RID: 1404
	public class MediaContentPerFormatStatusInfo : BusinessBase<int>
	{
		// Token: 0x170012F2 RID: 4850
		// (get) Token: 0x06002D2F RID: 11567 RVA: 0x00032134 File Offset: 0x00030334
		// (set) Token: 0x06002D30 RID: 11568 RVA: 0x0000E258 File Offset: 0x0000C458
		public int MediaContentPerFormatId
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

		// Token: 0x170012F3 RID: 4851
		// (get) Token: 0x06002D31 RID: 11569 RVA: 0x0003214C File Offset: 0x0003034C
		// (set) Token: 0x06002D32 RID: 11570 RVA: 0x00032154 File Offset: 0x00030354
		public MediaContentFormat MediaContentFormat { get; set; }

		// Token: 0x170012F4 RID: 4852
		// (get) Token: 0x06002D33 RID: 11571 RVA: 0x0003215D File Offset: 0x0003035D
		// (set) Token: 0x06002D34 RID: 11572 RVA: 0x00032165 File Offset: 0x00030365
		public eMediaContentPerFormatStatus Status { get; set; }

		// Token: 0x170012F5 RID: 4853
		// (get) Token: 0x06002D35 RID: 11573 RVA: 0x0003216E File Offset: 0x0003036E
		// (set) Token: 0x06002D36 RID: 11574 RVA: 0x00032176 File Offset: 0x00030376
		public IList<int> CompletedJobIds { get; set; }

		// Token: 0x170012F6 RID: 4854
		// (get) Token: 0x06002D37 RID: 11575 RVA: 0x0003217F File Offset: 0x0003037F
		// (set) Token: 0x06002D38 RID: 11576 RVA: 0x00032187 File Offset: 0x00030387
		public IList<int> InProgressJobIds { get; set; }
	}
}
