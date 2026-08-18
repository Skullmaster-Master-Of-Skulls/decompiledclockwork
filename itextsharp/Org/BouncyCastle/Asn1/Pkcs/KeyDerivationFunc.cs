using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x02000386 RID: 902
	public class KeyDerivationFunc : AlgorithmIdentifier
	{
		// Token: 0x06001F7B RID: 8059 RVA: 0x000BC168 File Offset: 0x000BB168
		internal KeyDerivationFunc(Asn1Sequence seq) : base(seq)
		{
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x000BC171 File Offset: 0x000BB171
		internal KeyDerivationFunc(DerObjectIdentifier id, Asn1Encodable parameters) : base(id, parameters)
		{
		}
	}
}
