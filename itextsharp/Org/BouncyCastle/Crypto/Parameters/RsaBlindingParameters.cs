using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000614 RID: 1556
	public class RsaBlindingParameters : ICipherParameters
	{
		// Token: 0x060034FC RID: 13564 RVA: 0x00148BD4 File Offset: 0x00147BD4
		public RsaBlindingParameters(RsaKeyParameters publicKey, BigInteger blindingFactor)
		{
			if (publicKey.IsPrivate)
			{
				throw new ArgumentException("RSA parameters should be for a public key");
			}
			this.publicKey = publicKey;
			this.blindingFactor = blindingFactor;
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x060034FD RID: 13565 RVA: 0x00148BFD File Offset: 0x00147BFD
		public RsaKeyParameters PublicKey
		{
			get
			{
				return this.publicKey;
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x060034FE RID: 13566 RVA: 0x00148C05 File Offset: 0x00147C05
		public BigInteger BlindingFactor
		{
			get
			{
				return this.blindingFactor;
			}
		}

		// Token: 0x04002377 RID: 9079
		private readonly RsaKeyParameters publicKey;

		// Token: 0x04002378 RID: 9080
		private readonly BigInteger blindingFactor;
	}
}
