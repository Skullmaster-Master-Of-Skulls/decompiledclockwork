using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x020001EB RID: 491
	public class ECDsaSigner : IDsa
	{
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001334 RID: 4916 RVA: 0x0006E394 File Offset: 0x0006D394
		public string AlgorithmName
		{
			get
			{
				return "ECDSA";
			}
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0006E39C File Offset: 0x0006D39C
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

		// Token: 0x06001336 RID: 4918 RVA: 0x0006E41C File Offset: 0x0006D41C
		public BigInteger[] GenerateSignature(byte[] message)
		{
			BigInteger n = this.key.Parameters.N;
			BigInteger bigInteger = this.calculateE(n, message);
			BigInteger bigInteger4;
			BigInteger bigInteger5;
			do
			{
				BigInteger bigInteger2;
				for (;;)
				{
					bigInteger2 = new BigInteger(n.BitLength, this.random);
					if (bigInteger2.SignValue != 0)
					{
						ECPoint ecpoint = this.key.Parameters.G.Multiply(bigInteger2);
						BigInteger bigInteger3 = ecpoint.X.ToBigInteger();
						bigInteger4 = bigInteger3.Mod(n);
						if (bigInteger4.SignValue != 0)
						{
							break;
						}
					}
				}
				BigInteger d = ((ECPrivateKeyParameters)this.key).D;
				bigInteger5 = bigInteger2.ModInverse(n).Multiply(bigInteger.Add(d.Multiply(bigInteger4))).Mod(n);
			}
			while (bigInteger5.SignValue == 0);
			return new BigInteger[]
			{
				bigInteger4,
				bigInteger5
			};
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0006E4F4 File Offset: 0x0006D4F4
		public bool VerifySignature(byte[] message, BigInteger r, BigInteger s)
		{
			BigInteger n = this.key.Parameters.N;
			if (r.SignValue < 1 || s.SignValue < 1 || r.CompareTo(n) >= 0 || s.CompareTo(n) >= 0)
			{
				return false;
			}
			BigInteger bigInteger = this.calculateE(n, message);
			BigInteger val = s.ModInverse(n);
			BigInteger a = bigInteger.Multiply(val).Mod(n);
			BigInteger b = r.Multiply(val).Mod(n);
			ECPoint g = this.key.Parameters.G;
			ECPoint q = ((ECPublicKeyParameters)this.key).Q;
			ECPoint ecpoint = ECAlgorithms.SumOfTwoMultiplies(g, a, q, b);
			BigInteger bigInteger2 = ecpoint.X.ToBigInteger().Mod(n);
			return bigInteger2.Equals(r);
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x0006E5B8 File Offset: 0x0006D5B8
		private BigInteger calculateE(BigInteger n, byte[] message)
		{
			int num = message.Length * 8;
			BigInteger bigInteger = new BigInteger(1, message);
			if (n.BitLength < num)
			{
				bigInteger = bigInteger.ShiftRight(num - n.BitLength);
			}
			return bigInteger;
		}

		// Token: 0x04000D79 RID: 3449
		private ECKeyParameters key;

		// Token: 0x04000D7A RID: 3450
		private SecureRandom random;
	}
}
