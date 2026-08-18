using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Authentication.ADFS
{
	// Token: 0x020004A4 RID: 1188
	public class AdfsAuthenticationResult
	{
		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x060023D3 RID: 9171 RVA: 0x00027306 File Offset: 0x00025506
		// (set) Token: 0x060023D4 RID: 9172 RVA: 0x0002730E File Offset: 0x0002550E
		public bool IsAuthenticated { get; set; }

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x060023D5 RID: 9173 RVA: 0x00027317 File Offset: 0x00025517
		// (set) Token: 0x060023D6 RID: 9174 RVA: 0x0002731F File Offset: 0x0002551F
		public string UserName { get; set; }

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x060023D7 RID: 9175 RVA: 0x00027328 File Offset: 0x00025528
		// (set) Token: 0x060023D8 RID: 9176 RVA: 0x00027330 File Offset: 0x00025530
		public Dictionary<string, string> ClaimEntries { get; set; }
	}
}
