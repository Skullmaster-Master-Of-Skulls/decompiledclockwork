using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Authentication
{
	// Token: 0x02000491 RID: 1169
	public class LdapAuthenticationResult
	{
		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x0600232F RID: 9007 RVA: 0x00026C93 File Offset: 0x00024E93
		// (set) Token: 0x06002330 RID: 9008 RVA: 0x00026C9B File Offset: 0x00024E9B
		public bool IsAuthenticated { get; set; }

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06002331 RID: 9009 RVA: 0x00026CA4 File Offset: 0x00024EA4
		// (set) Token: 0x06002332 RID: 9010 RVA: 0x00026CAC File Offset: 0x00024EAC
		public Dictionary<string, string> ReturnAttributes { get; set; }

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06002333 RID: 9011 RVA: 0x00026CB5 File Offset: 0x00024EB5
		// (set) Token: 0x06002334 RID: 9012 RVA: 0x00026CBD File Offset: 0x00024EBD
		public string ErrorMessage { get; set; }
	}
}
