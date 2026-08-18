using System;
using System.Security.Cryptography;

namespace System.Web.Security.Cryptography
{
	// Token: 0x02000607 RID: 1543
	internal interface ICryptoAlgorithmFactory
	{
		// Token: 0x06004DAE RID: 19886
		SymmetricAlgorithm GetEncryptionAlgorithm();

		// Token: 0x06004DAF RID: 19887
		KeyedHashAlgorithm GetValidationAlgorithm();
	}
}
