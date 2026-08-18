using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Authentication.Authentication
{
	// Token: 0x02000498 RID: 1176
	public class AuthenticationArgs
	{
		// Token: 0x06002378 RID: 9080 RVA: 0x00026EDE File Offset: 0x000250DE
		public AuthenticationArgs()
		{
			this.SecureArgs = new Dictionary<string, string>();
			this.InsecureArgs = new Dictionary<string, string>();
		}

		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x06002379 RID: 9081 RVA: 0x00026F00 File Offset: 0x00025100
		// (set) Token: 0x0600237A RID: 9082 RVA: 0x00026F08 File Offset: 0x00025108
		public IDictionary<string, string> SecureArgs { get; set; }

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x0600237B RID: 9083 RVA: 0x00026F11 File Offset: 0x00025111
		// (set) Token: 0x0600237C RID: 9084 RVA: 0x00026F19 File Offset: 0x00025119
		public IDictionary<string, string> InsecureArgs { get; set; }
	}
}
