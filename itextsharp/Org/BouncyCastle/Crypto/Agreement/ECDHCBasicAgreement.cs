using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;

namespace Org.BouncyCastle.Crypto.Agreement
{
	// Token: 0x02000355 RID: 853
	public class ECDHCBasicAgreement : IBasicAgreement
	{
		// Token: 0x06001EBE RID: 7870 RVA: 0x000B977C File Offset: 0x000B877C
		public void Init(ICipherParameters parameters)
		{
			if (parameters is ParametersWithRandom)
			{
				parameters = ((ParametersWithRandom)parameters).Parameters;
			}
			this.key = (ECPrivateKeyParameters)parameters;
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x000B97A0 File Offset: 0x000B87A0
		public BigInteger CalculateAgreement(ICipherParameters pubKey)
		{
			ECPublicKeyParameters ecpublicKeyParameters = (ECPublicKeyParameters)pubKey;
			ECDomainParameters parameters = ecpublicKeyParameters.Parameters;
			ECPoint ecpoint = ecpublicKeyParameters.Q.Multiply(parameters.H.Multiply(this.key.D));
			return ecpoint.X.ToBigInteger();
		}

		// Token: 0x04001545 RID: 5445
		private ECPrivateKeyParameters key;
	}
}
