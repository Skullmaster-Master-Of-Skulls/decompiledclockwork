using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Cases
{
	// Token: 0x02000467 RID: 1127
	public class Case : CaseBase
	{
		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x0600224F RID: 8783 RVA: 0x00026463 File Offset: 0x00024663
		// (set) Token: 0x06002250 RID: 8784 RVA: 0x0002646B File Offset: 0x0002466B
		public DateTime DateEntered { get; set; }

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x06002251 RID: 8785 RVA: 0x00026474 File Offset: 0x00024674
		// (set) Token: 0x06002252 RID: 8786 RVA: 0x0002647C File Offset: 0x0002467C
		public PersonBase WhoEntered { get; set; }

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x06002253 RID: 8787 RVA: 0x00026485 File Offset: 0x00024685
		// (set) Token: 0x06002254 RID: 8788 RVA: 0x0002648D File Offset: 0x0002468D
		public string Status { get; set; }

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x00026496 File Offset: 0x00024696
		// (set) Token: 0x06002256 RID: 8790 RVA: 0x0002649E File Offset: 0x0002469E
		public IList<CaseClient> Clients { get; set; }
	}
}
