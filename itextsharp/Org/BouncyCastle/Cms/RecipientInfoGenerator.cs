using System;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000134 RID: 308
	internal interface RecipientInfoGenerator
	{
		// Token: 0x06000B4F RID: 2895
		RecipientInfo Generate(KeyParameter contentEncryptionKey, SecureRandom random);
	}
}
