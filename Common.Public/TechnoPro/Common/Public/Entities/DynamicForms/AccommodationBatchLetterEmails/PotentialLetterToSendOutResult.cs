using System;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.Public.Entities.DynamicForms.AccommodationBatchLetterEmails
{
	// Token: 0x020003BF RID: 959
	public class PotentialLetterToSendOutResult : PotentialLetterToSendOut
	{
		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06001D3F RID: 7487 RVA: 0x00021234 File Offset: 0x0001F434
		// (set) Token: 0x06001D40 RID: 7488 RVA: 0x0002123C File Offset: 0x0001F43C
		public bool ShouldSend { get; set; }

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06001D41 RID: 7489 RVA: 0x00021245 File Offset: 0x0001F445
		// (set) Token: 0x06001D42 RID: 7490 RVA: 0x0002124D File Offset: 0x0001F44D
		public string Note { get; set; }

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06001D43 RID: 7491 RVA: 0x00021256 File Offset: 0x0001F456
		// (set) Token: 0x06001D44 RID: 7492 RVA: 0x0002125E File Offset: 0x0001F45E
		public bool SentSuccessfully { get; set; }

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06001D45 RID: 7493 RVA: 0x00021267 File Offset: 0x0001F467
		// (set) Token: 0x06001D46 RID: 7494 RVA: 0x0002126F File Offset: 0x0001F46F
		public TPMailMessage Email { get; set; }

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06001D47 RID: 7495 RVA: 0x00021278 File Offset: 0x0001F478
		// (set) Token: 0x06001D48 RID: 7496 RVA: 0x00021280 File Offset: 0x0001F480
		public TPMailAttachment Attachment { get; set; }
	}
}
