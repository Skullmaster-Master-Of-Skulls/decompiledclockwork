using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;

namespace Org.BouncyCastle.Crypto.Agreement
{
	// Token: 0x020002FD RID: 765
	public class ECDHBasicAgreement : IBasicAgreement
	{
		// Token: 0x06001C16 RID: 7190 RVA: 0x000A8663 File Offset: 0x000A7663
		public void Init(ICipherParameters parameters)
		{
			if (parameters is ParametersWithRandom)
			{
				parameters = ((ParametersWithRandom)parameters).Parameters;
			}
			this.privKey = (ECPrivateKeyParameters)parameters;
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x000A8688 File Offset: 0x000A7688
		public virtual BigInteger CalculateAgreement(ICipherParameters pubKey)
		{
			ECPublicKeyParameters ecpublicKeyParameters = (ECPublicKeyParameters)pubKey;
			ECPoint ecpoint = ecpublicKeyParameters.Q.Multiply(this.privKey.D);
			return ecpoint.X.ToBigInteger();
		}

		// Token: 0x04001350 RID: 4944
		protected internal ECPrivateKeyParameters privKey;
	}
}
