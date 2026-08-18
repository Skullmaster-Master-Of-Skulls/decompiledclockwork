using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.DropBox
{
	// Token: 0x020003C1 RID: 961
	public class DropBox_IM : BusinessBase<int>
	{
		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06001D4F RID: 7503 RVA: 0x000212B4 File Offset: 0x0001F4B4
		// (set) Token: 0x06001D50 RID: 7504 RVA: 0x000212BC File Offset: 0x0001F4BC
		public virtual string Message { get; set; }

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06001D51 RID: 7505 RVA: 0x000212C5 File Offset: 0x0001F4C5
		// (set) Token: 0x06001D52 RID: 7506 RVA: 0x000212CD File Offset: 0x0001F4CD
		public virtual bool RequiredResponse { get; set; }

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06001D53 RID: 7507 RVA: 0x000212D6 File Offset: 0x0001F4D6
		// (set) Token: 0x06001D54 RID: 7508 RVA: 0x000212DE File Offset: 0x0001F4DE
		public virtual bool RequiredReceivingConfirmation { get; set; }

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06001D55 RID: 7509 RVA: 0x000212E7 File Offset: 0x0001F4E7
		// (set) Token: 0x06001D56 RID: 7510 RVA: 0x000212EF File Offset: 0x0001F4EF
		public virtual DateTime IssuedOn { get; set; }

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06001D57 RID: 7511 RVA: 0x000212F8 File Offset: 0x0001F4F8
		// (set) Token: 0x06001D58 RID: 7512 RVA: 0x00021300 File Offset: 0x0001F500
		public virtual DropBox_User From { get; set; }

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06001D59 RID: 7513 RVA: 0x00021309 File Offset: 0x0001F509
		// (set) Token: 0x06001D5A RID: 7514 RVA: 0x00021311 File Offset: 0x0001F511
		public virtual string To { get; set; }

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06001D5B RID: 7515 RVA: 0x0002131A File Offset: 0x0001F51A
		// (set) Token: 0x06001D5C RID: 7516 RVA: 0x00021322 File Offset: 0x0001F522
		public virtual IDictionary<string, string> Parameters { get; set; }

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06001D5D RID: 7517 RVA: 0x0002132B File Offset: 0x0001F52B
		// (set) Token: 0x06001D5E RID: 7518 RVA: 0x00021333 File Offset: 0x0001F533
		public bool WasRead { get; set; }
	}
}
