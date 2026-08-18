using System;

namespace TechnoPro.Common.Public.Entities.DropBox
{
	// Token: 0x020003C3 RID: 963
	public class DropBox_AttachmentInfo : BusinessBase<int>
	{
		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06001D67 RID: 7527 RVA: 0x0002138D File Offset: 0x0001F58D
		// (set) Token: 0x06001D68 RID: 7528 RVA: 0x00021395 File Offset: 0x0001F595
		public virtual string Extension { get; set; }

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x06001D69 RID: 7529 RVA: 0x0002139E File Offset: 0x0001F59E
		// (set) Token: 0x06001D6A RID: 7530 RVA: 0x000213A6 File Offset: 0x0001F5A6
		public virtual string Filename { get; set; }

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x06001D6B RID: 7531 RVA: 0x000213AF File Offset: 0x0001F5AF
		// (set) Token: 0x06001D6C RID: 7532 RVA: 0x000213B7 File Offset: 0x0001F5B7
		public virtual string Description { get; set; }

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x06001D6D RID: 7533 RVA: 0x000213C0 File Offset: 0x0001F5C0
		// (set) Token: 0x06001D6E RID: 7534 RVA: 0x000213C8 File Offset: 0x0001F5C8
		public virtual bool RequiredReceivingConfirmation { get; set; }

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x06001D6F RID: 7535 RVA: 0x000213D1 File Offset: 0x0001F5D1
		// (set) Token: 0x06001D70 RID: 7536 RVA: 0x000213D9 File Offset: 0x0001F5D9
		public virtual DateTime IssuedOn { get; set; }

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06001D71 RID: 7537 RVA: 0x000213E2 File Offset: 0x0001F5E2
		// (set) Token: 0x06001D72 RID: 7538 RVA: 0x000213EA File Offset: 0x0001F5EA
		public virtual DropBox_User From { get; set; }

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06001D73 RID: 7539 RVA: 0x000213F3 File Offset: 0x0001F5F3
		// (set) Token: 0x06001D74 RID: 7540 RVA: 0x000213FB File Offset: 0x0001F5FB
		public virtual string To { get; set; }

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06001D75 RID: 7541 RVA: 0x00021404 File Offset: 0x0001F604
		// (set) Token: 0x06001D76 RID: 7542 RVA: 0x0002140C File Offset: 0x0001F60C
		public bool WasRead { get; set; }

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06001D77 RID: 7543 RVA: 0x00021415 File Offset: 0x0001F615
		// (set) Token: 0x06001D78 RID: 7544 RVA: 0x0002141D File Offset: 0x0001F61D
		public int SizeInBytes { get; set; }
	}
}
