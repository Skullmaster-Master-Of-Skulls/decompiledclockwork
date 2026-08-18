using System;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x0200057B RID: 1403
	public class MediaContentPerFormatInfo : BusinessBase<int>
	{
		// Token: 0x170012EF RID: 4847
		// (get) Token: 0x06002D28 RID: 11560 RVA: 0x000320F8 File Offset: 0x000302F8
		// (set) Token: 0x06002D29 RID: 11561 RVA: 0x0000E258 File Offset: 0x0000C458
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

		// Token: 0x170012F0 RID: 4848
		// (get) Token: 0x06002D2A RID: 11562 RVA: 0x00032110 File Offset: 0x00030310
		// (set) Token: 0x06002D2B RID: 11563 RVA: 0x00032118 File Offset: 0x00030318
		public MediaContentFormat MediaContentFormat { get; set; }

		// Token: 0x170012F1 RID: 4849
		// (get) Token: 0x06002D2C RID: 11564 RVA: 0x00032121 File Offset: 0x00030321
		// (set) Token: 0x06002D2D RID: 11565 RVA: 0x00032129 File Offset: 0x00030329
		public Guid MediaContentId { get; set; }
	}
}
