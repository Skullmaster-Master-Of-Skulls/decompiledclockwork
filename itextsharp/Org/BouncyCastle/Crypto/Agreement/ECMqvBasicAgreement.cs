using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;

namespace Org.BouncyCastle.Crypto.Agreement
{
	// Token: 0x02000354 RID: 852
	public class ECMqvBasicAgreement : IBasicAgreement
	{
		// Token: 0x06001EBA RID: 7866 RVA: 0x000B95EB File Offset: 0x000B85EB
		public void Init(ICipherParameters parameters)
		{
			if (parameters is ParametersWithRandom)
			{
				parameters = ((ParametersWithRandom)parameters).Parameters;
			}
			this.privParams = (MqvPrivateParameters)parameters;
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x000B9610 File Offset: 0x000B8610
		public virtual BigInteger CalculateAgreement(ICipherParameters pubKey)
		{
			MqvPublicParameters mqvPublicParameters = (MqvPublicParameters)pubKey;
			ECPrivateKeyParameters staticPrivateKey = this.privParams.StaticPrivateKey;
			ECPoint ecpoint = ECMqvBasicAgreement.calculateMqvAgreement(staticPrivateKey.Parameters, staticPrivateKey, this.privParams.EphemeralPrivateKey, this.privParams.EphemeralPublicKey, mqvPublicParameters.StaticPublicKey, mqvPublicParameters.EphemeralPublicKey);
			return ecpoint.X.ToBigInteger();
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x000B966C File Offset: 0x000B866C
		private static ECPoint calculateMqvAgreement(ECDomainParameters parameters, ECPrivateKeyParameters d1U, ECPrivateKeyParameters d2U, ECPublicKeyParameters Q2U, ECPublicKeyParameters Q1V, ECPublicKeyParameters Q2V)
		{
			BigInteger n = parameters.N;
			int num = (n.BitLength + 1) / 2;
			BigInteger m = BigInteger.One.ShiftLeft(num);
			ECPoint ecpoint;
			if (Q2U == null)
			{
				ecpoint = parameters.G.Multiply(d2U.D);
			}
			else
			{
				ecpoint = Q2U.Q;
			}
			BigInteger bigInteger = ecpoint.X.ToBigInteger();
			BigInteger bigInteger2 = bigInteger.Mod(m);
			BigInteger val = bigInteger2.SetBit(num);
			BigInteger val2 = d1U.D.Multiply(val).Mod(n).Add(d2U.D).Mod(n);
			BigInteger bigInteger3 = Q2V.Q.X.ToBigInteger();
			BigInteger bigInteger4 = bigInteger3.Mod(m);
			BigInteger bigInteger5 = bigInteger4.SetBit(num);
			BigInteger bigInteger6 = parameters.H.Multiply(val2).Mod(n);
			ECPoint ecpoint2 = ECAlgorithms.SumOfTwoMultiplies(Q1V.Q, bigInteger5.Multiply(bigInteger6).Mod(n), Q2V.Q, bigInteger6);
			if (ecpoint2.IsInfinity)
			{
				throw new InvalidOperationException("Infinity is not a valid agreement value for MQV");
			}
			return ecpoint2;
		}

		// Token: 0x04001544 RID: 5444
		protected internal MqvPrivateParameters privParams;
	}
}
