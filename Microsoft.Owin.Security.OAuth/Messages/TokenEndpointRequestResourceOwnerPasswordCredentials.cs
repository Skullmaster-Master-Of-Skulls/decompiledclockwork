using System;
using System.Collections.Generic;

namespace Microsoft.Owin.Security.OAuth.Messages
{
	// Token: 0x02000008 RID: 8
	public class TokenEndpointRequestResourceOwnerPasswordCredentials
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002616 File Offset: 0x00000816
		// (set) Token: 0x0600003A RID: 58 RVA: 0x0000261E File Offset: 0x0000081E
		public string UserName { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002627 File Offset: 0x00000827
		// (set) Token: 0x0600003C RID: 60 RVA: 0x0000262F File Offset: 0x0000082F
		public string Password { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002638 File Offset: 0x00000838
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002640 File Offset: 0x00000840
		public IList<string> Scope { get; set; }
	}
}
