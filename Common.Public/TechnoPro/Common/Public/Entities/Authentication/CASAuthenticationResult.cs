using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Authentication
{
	// Token: 0x0200048B RID: 1163
	public class CASAuthenticationResult
	{
		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06002308 RID: 8968 RVA: 0x00026B72 File Offset: 0x00024D72
		// (set) Token: 0x06002309 RID: 8969 RVA: 0x00026B7A File Offset: 0x00024D7A
		public bool IsAuthenticated { get; set; }

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x0600230A RID: 8970 RVA: 0x00026B83 File Offset: 0x00024D83
		// (set) Token: 0x0600230B RID: 8971 RVA: 0x00026B8B File Offset: 0x00024D8B
		public string UserName { get; set; }

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x0600230C RID: 8972 RVA: 0x00026B94 File Offset: 0x00024D94
		// (set) Token: 0x0600230D RID: 8973 RVA: 0x00026B9C File Offset: 0x00024D9C
		public Dictionary<string, string> ReturnAttributes { get; set; }
	}
}
