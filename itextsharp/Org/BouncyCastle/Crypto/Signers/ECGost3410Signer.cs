using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x02000475 RID: 1141
	public class ECGost3410Signer : IDsa
	{
		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060026D7 RID: 9943 RVA: 0x000EAD73 File Offset: 0x000E9D73
		public string AlgorithmName
		{
			get
			{
				return "ECGOST3410";
			}
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x000EAD7C File Offset: 0x000E9D7C
		public void Init(bool forSigning, ICipherParameters parameters)
		{
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

		// Token: 0x060026D9 RID: 9945 RVA: 0x000EADFC File Offset: 0x000E9DFC
		public BigInteger[] GenerateSignature(byte[] message)
		{
			byte[] array = new byte[message.Length];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = message[array.Length - 1 - num];
			}
			BigInteger val = new BigInteger(1, array);
			BigInteger n = this.key.Parameters.N;
			BigInteger bigInteger3;
			BigInteger bigInteger4;
			do
			{
				BigInteger bigInteger;
				for (;;)
				{
					bigInteger = new BigInteger(n.BitLength, this.random);
					if (bigInteger.SignValue != 0)
					{
						ECPoint ecpoint = this.key.Parameters.G.Multiply(bigInteger);
						BigInteger bigInteger2 = ecpoint.X.ToBigInteger();
						bigInteger3 = bigInteger2.Mod(n);
						if (bigInteger3.SignValue != 0)
						{
							break;
						}
					}
				}
				BigInteger d = ((ECPrivateKeyParameters)this.key).D;
				bigInteger4 = bigInteger.Multiply(val).Add(d.Multiply(bigInteger3)).Mod(n);
			}
			while (bigInteger4.SignValue == 0);
			return new BigInteger[]
			{
				bigInteger3,
				bigInteger4
			};
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x000EAEF8 File Offset: 0x000E9EF8
		public bool VerifySignature(byte[] message, BigInteger r, BigInteger s)
		{
			byte[] array = new byte[message.Length];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = message[array.Length - 1 - num];
			}
			BigInteger bigInteger = new BigInteger(1, array);
			BigInteger n = this.key.Parameters.N;
			if (r.CompareTo(BigInteger.One) < 0 || r.CompareTo(n) >= 0)
			{
				return false;
			}
			if (s.CompareTo(BigInteger.One) < 0 || s.CompareTo(n) >= 0)
			{
				return false;
			}
			BigInteger val = bigInteger.ModInverse(n);
			BigInteger a = s.Multiply(val).Mod(n);
			BigInteger b = n.Subtract(r).Multiply(val).Mod(n);
			ECPoint g = this.key.Parameters.G;
			ECPoint q = ((ECPublicKeyParameters)this.key).Q;
			ECPoint ecpoint = ECAlgorithms.SumOfTwoMultiplies(g, a, q, b);
			BigInteger bigInteger2 = ecpoint.X.ToBigInteger().Mod(n);
			return bigInteger2.Equals(r);
		}

		// Token: 0x04001ABA RID: 6842
		private ECKeyParameters key;

		// Token: 0x04001ABB RID: 6843
		private SecureRandom random;
	}
}
