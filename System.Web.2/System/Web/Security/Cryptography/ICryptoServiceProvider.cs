using System;

namespace System.Web.Security.Cryptography
{
	// Token: 0x02000603 RID: 1539
	internal interface ICryptoServiceProvider
	{
		// Token: 0x06004DA5 RID: 19877
		ICryptoService GetCryptoService(Purpose purpose, CryptoServiceOptions options = CryptoServiceOptions.None);
	}
}
