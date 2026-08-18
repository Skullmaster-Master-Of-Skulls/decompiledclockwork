using System;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x020002EC RID: 748
	public class ECNRSigner : IDsa
	{
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001BAA RID: 7082 RVA: 0x000A5C4D File Offset: 0x000A4C4D
		public string AlgorithmName
		{
			get
			{
				return "ECNR";
			}
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x000A5C54 File Offset: 0x000A4C54
		public void Init(bool forSigning, ICipherParameters parameters)
		{
			this.forSigning = forSigning;
			if (forSigning)
			{
				if (parameters is ParametersWithRandom)
				{
					ParametersWithRandom parametersWithRandom = (ParametersWithRandom)parameters;
					this.random = parametersWithRandom.Random;
					parameters = parametersWithRandom.Parameters;
				}
				else
				{
					this.random = new SecureRandom();
				}
				if (!(parameters is ECPrivateKeyParameters))
				{
					throw new InvalidKeyException("EC private key required for signing");
				}
				this.key = (ECPrivateKeyParameters)parameters;
				return;
			}
			else
			{
				if (!(parameters is ECPublicKeyParameters))
				{
					throw new InvalidKeyException("EC public key required for verification");
				}
				this.key = (ECPublicKeyParameters)parameters;
				return;
			}
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x000A5CDC File Offset: 0x000A4CDC
		public BigInteger[] GenerateSignature(byte[] message)
		{
			if (!this.forSigning)
			{
				throw new InvalidOperationException("not initialised for signing");
			}
			BigInteger n = ((ECPrivateKeyParameters)this.key).Parameters.N;
			int bitLength = n.BitLength;
			BigInteger bigInteger = new BigInteger(1, message);
			int bitLength2 = bigInteger.BitLength;
			ECPrivateKeyParameters ecprivateKeyParameters = (ECPrivateKeyParameters)this.key;
			if (bitLength2 > bitLength)
			{
				throw new DataLengthException("input too large for ECNR key.");
			}
			AsymmetricCipherKeyPair asymmetricCipherKeyPair;
			BigInteger bigInteger3;
			do
			{
				ECKeyPairGenerator eckeyPairGenerator = new ECKeyPairGenerator();
				eckeyPairGenerator.Init(new ECKeyGenerationParameters(ecprivateKeyParameters.Parameters, this.random));
				asymmetricCipherKeyPair = eckeyPairGenerator.GenerateKeyPair();
				ECPublicKeyParameters ecpublicKeyParameters = (ECPublicKeyParameters)asymmetricCipherKeyPair.Public;
				BigInteger bigInteger2 = ecpublicKeyParameters.Q.X.ToBigInteger();
				bigInteger3 = bigInteger2.Add(bigInteger).Mod(n);
			}
			while (bigInteger3.SignValue == 0);
			BigInteger d = ecprivateKeyParameters.D;
			BigInteger d2 = ((ECPrivateKeyParameters)asymmetricCipherKeyPair.Private).D;
			BigInteger bigInteger4 = d2.Subtract(bigInteger3.Multiply(d)).Mod(n);
			return new BigInteger[]
			{
				bigInteger3,
				bigInteger4
			};
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x000A5DF8 File Offset: 0x000A4DF8
		public bool VerifySignature(byte[] message, BigInteger r, BigInteger s)
		{
			if (this.forSigning)
			{
				throw new InvalidOperationException("not initialised for verifying");
			}
			ECPublicKeyParameters ecpublicKeyParameters = (ECPublicKeyParameters)this.key;
			BigInteger n = ecpublicKeyParameters.Parameters.N;
			int bitLength = n.BitLength;
			BigInteger bigInteger = new BigInteger(1, message);
			int bitLength2 = bigInteger.BitLength;
			if (bitLength2 > bitLength)
			{
				throw new DataLengthException("input too large for ECNR key.");
			}
			if (r.CompareTo(BigInteger.One) < 0 || r.CompareTo(n) >= 0)
			{
				return false;
			}
			if (s.CompareTo(BigInteger.Zero) < 0 || s.CompareTo(n) >= 0)
			{
				return false;
			}
			ECPoint g = ecpublicKeyParameters.Parameters.G;
			ECPoint q = ecpublicKeyParameters.Q;
			ECPoint ecpoint = ECAlgorithms.SumOfTwoMultiplies(g, s, q, r);
			BigInteger n2 = ecpoint.X.ToBigInteger();
			BigInteger bigInteger2 = r.Subtract(n2).Mod(n);
			return bigInteger2.Equals(bigInteger);
		}

		// Token: 0x040012FD RID: 4861
		private bool forSigning;

		// Token: 0x040012FE RID: 4862
		private ECKeyParameters key;

		// Token: 0x040012FF RID: 4863
		private SecureRandom random;
	}
}
