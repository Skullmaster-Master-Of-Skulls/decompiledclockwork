using System;
using TechnoPro.Common.Public.Entities.Authentication.Authorization;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005B7 RID: 1463
	public class AuthorizationContextItemStandalone
	{
		// Token: 0x170013D1 RID: 5073
		// (get) Token: 0x06002F52 RID: 12114 RVA: 0x00034AE8 File Offset: 0x00032CE8
		// (set) Token: 0x06002F53 RID: 12115 RVA: 0x00034AF0 File Offset: 0x00032CF0
		public AuthorizationContextItem ContextItem { get; set; }

		// Token: 0x170013D2 RID: 5074
		// (get) Token: 0x06002F54 RID: 12116 RVA: 0x00034AF9 File Offset: 0x00032CF9
		// (set) Token: 0x06002F55 RID: 12117 RVA: 0x00034B01 File Offset: 0x00032D01
		public string Type { get; set; }
	}
}
