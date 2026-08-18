using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x02000359 RID: 857
	public class SyncDocumentAction
	{
		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06001A9A RID: 6810 RVA: 0x0001EA1A File Offset: 0x0001CC1A
		// (set) Token: 0x06001A9B RID: 6811 RVA: 0x0001EA22 File Offset: 0x0001CC22
		public int PersonId { get; set; }

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06001A9C RID: 6812 RVA: 0x0001EA2B File Offset: 0x0001CC2B
		// (set) Token: 0x06001A9D RID: 6813 RVA: 0x0001EA33 File Offset: 0x0001CC33
		public string ExternalFileName { get; set; }

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06001A9E RID: 6814 RVA: 0x0001EA3C File Offset: 0x0001CC3C
		// (set) Token: 0x06001A9F RID: 6815 RVA: 0x0001EA44 File Offset: 0x0001CC44
		public int ClockWorkFileId { get; set; }
	}
}
