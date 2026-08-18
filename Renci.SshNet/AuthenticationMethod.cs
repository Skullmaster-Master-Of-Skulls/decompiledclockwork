using System;
using System.Collections.Generic;

namespace Renci.SshNet
{
	// Token: 0x02000004 RID: 4
	public abstract class AuthenticationMethod : IAuthenticationMethod
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000D RID: 13
		public abstract string Name { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000022C0 File Offset: 0x000004C0
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000022C8 File Offset: 0x000004C8
		public string Username { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000022D1 File Offset: 0x000004D1
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000022D9 File Offset: 0x000004D9
		public IList<string> AllowedAuthentications { get; protected set; }

		// Token: 0x06000012 RID: 18 RVA: 0x000022E2 File Offset: 0x000004E2
		protected AuthenticationMethod(string username)
		{
			if (username.IsNullOrWhiteSpace())
			{
				throw new ArgumentException("username");
			}
			this.Username = username;
		}

		// Token: 0x06000013 RID: 19
		public abstract AuthenticationResult Authenticate(Session session);

		// Token: 0x06000014 RID: 20 RVA: 0x00002304 File Offset: 0x00000504
		AuthenticationResult IAuthenticationMethod.Authenticate(ISession session)
		{
			return this.Authenticate((Session)session);
		}
	}
}
