using System;
using System.Security.Claims;

namespace Microsoft.AspNet.Identity.Owin
{
	// Token: 0x0200000F RID: 15
	public class ExternalLoginInfo
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003AEB File Offset: 0x00001CEB
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00003AF3 File Offset: 0x00001CF3
		public UserLoginInfo Login { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00003AFC File Offset: 0x00001CFC
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00003B04 File Offset: 0x00001D04
		public string DefaultUserName { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00003B0D File Offset: 0x00001D0D
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00003B15 File Offset: 0x00001D15
		public string Email { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003B1E File Offset: 0x00001D1E
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00003B26 File Offset: 0x00001D26
		public ClaimsIdentity ExternalIdentity { get; set; }
	}
}
