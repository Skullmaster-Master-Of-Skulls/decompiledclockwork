using System;

namespace System.Web.Security.Cryptography
{
	// Token: 0x02000608 RID: 1544
	internal interface ICryptoService
	{
		// Token: 0x06004DB0 RID: 19888
		byte[] Protect(byte[] clearData);

		// Token: 0x06004DB1 RID: 19889
		byte[] Unprotect(byte[] protectedData);
	}
}
