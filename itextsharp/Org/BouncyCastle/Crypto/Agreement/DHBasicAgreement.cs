using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Agreement
{
	// Token: 0x020003DA RID: 986
	public class DHBasicAgreement : IBasicAgreement
	{
		// Token: 0x06002266 RID: 8806 RVA: 0x000D5E74 File Offset: 0x000D4E74
		public void Init(ICipherParameters parameters)
		{
			if (parameters is ParametersWithRandom)
			{
				parameters = ((ParametersWithRandom)parameters).Parameters;
			}
			if (!(parameters is DHPrivateKeyParameters))
			{
				throw new ArgumentException("DHEngine expects DHPrivateKeyParameters");
			}
			this.key = (DHPrivateKeyParameters)parameters;
			this.dhParams = this.key.Parameters;
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x000D5EC8 File Offset: 0x000D4EC8
		public BigInteger CalculateAgreement(ICipherParameters pubKey)
		{
			if (this.key == null)
			{
				throw new InvalidOperationException("Agreement algorithm not initialised");
			}
			DHPublicKeyParameters dhpublicKeyParameters = (DHPublicKeyParameters)pubKey;
			if (!dhpublicKeyParameters.Parameters.Equals(this.dhParams))
			{
				throw new ArgumentException("Diffie-Hellman public key has wrong parameters.");
			}
			return dhpublicKeyParameters.Y.ModPow(this.key.X, this.dhParams.P);
		}

		// Token: 0x0400179F RID: 6047
		private DHPrivateKeyParameters key;

		// Token: 0x040017A0 RID: 6048
		private DHParameters dhParams;
	}
}
