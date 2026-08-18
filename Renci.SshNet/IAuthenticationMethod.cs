using System;
using System.Collections.Generic;

namespace Renci.SshNet
{
	// Token: 0x0200000E RID: 14
	internal interface IAuthenticationMethod
	{
		// Token: 0x060000B7 RID: 183
		AuthenticationResult Authenticate(ISession session);

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000B8 RID: 184
		IList<string> AllowedAuthentications { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000B9 RID: 185
		string Name { get; }
	}
}
