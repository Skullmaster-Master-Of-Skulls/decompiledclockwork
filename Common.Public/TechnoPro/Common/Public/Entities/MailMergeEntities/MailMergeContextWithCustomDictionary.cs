using System;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities
{
	// Token: 0x020002BB RID: 699
	public class MailMergeContextWithCustomDictionary
	{
		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06001501 RID: 5377 RVA: 0x0001A492 File Offset: 0x00018692
		// (set) Token: 0x06001502 RID: 5378 RVA: 0x0001A49A File Offset: 0x0001869A
		public MailMergeContext Context { get; set; }

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x0001A4A3 File Offset: 0x000186A3
		// (set) Token: 0x06001504 RID: 5380 RVA: 0x0001A4AB File Offset: 0x000186AB
		public MailMergeCustomDictionary CustomDictionary { get; set; }
	}
}
