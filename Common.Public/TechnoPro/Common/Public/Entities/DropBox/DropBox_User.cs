using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.DropBox
{
	// Token: 0x020003C4 RID: 964
	public class DropBox_User : BusinessBase<string>
	{
		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06001D7A RID: 7546 RVA: 0x00021428 File Offset: 0x0001F628
		// (set) Token: 0x06001D7B RID: 7547 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public string Username
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

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06001D7C RID: 7548 RVA: 0x00021440 File Offset: 0x0001F640
		// (set) Token: 0x06001D7D RID: 7549 RVA: 0x00021448 File Offset: 0x0001F648
		public string FullName { get; set; }

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06001D7E RID: 7550 RVA: 0x00021451 File Offset: 0x0001F651
		// (set) Token: 0x06001D7F RID: 7551 RVA: 0x00021459 File Offset: 0x0001F659
		public string Email { get; set; }

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06001D80 RID: 7552 RVA: 0x00021462 File Offset: 0x0001F662
		// (set) Token: 0x06001D81 RID: 7553 RVA: 0x0002146A File Offset: 0x0001F66A
		public string Phone { get; set; }

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06001D82 RID: 7554 RVA: 0x00021473 File Offset: 0x0001F673
		// (set) Token: 0x06001D83 RID: 7555 RVA: 0x0002147B File Offset: 0x0001F67B
		public List<string> Roles { get; set; }
	}
}
