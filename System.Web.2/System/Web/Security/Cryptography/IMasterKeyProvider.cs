using System;

namespace System.Web.Security.Cryptography
{
	// Token: 0x02000604 RID: 1540
	internal interface IMasterKeyProvider
	{
		// Token: 0x06004DA6 RID: 19878
		CryptographicKey GetEncryptionKey();

		// Token: 0x06004DA7 RID: 19879
		CryptographicKey GetValidationKey();
	}
}
