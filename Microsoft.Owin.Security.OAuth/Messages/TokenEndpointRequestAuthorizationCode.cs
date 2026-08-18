using System;

namespace Microsoft.Owin.Security.OAuth.Messages
{
	// Token: 0x02000004 RID: 4
	public class TokenEndpointRequestAuthorizationCode
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002113 File Offset: 0x00000313
		// (set) Token: 0x0600000A RID: 10 RVA: 0x0000211B File Offset: 0x0000031B
		public string Code { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002124 File Offset: 0x00000324
		// (set) Token: 0x0600000C RID: 12 RVA: 0x0000212C File Offset: 0x0000032C
		public string RedirectUri { get; set; }
	}
}
