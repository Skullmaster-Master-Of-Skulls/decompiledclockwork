using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x020001EC RID: 492
	public class DsaSigner : IDsa
	{
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x0006E5F4 File Offset: 0x0006D5F4
		public string AlgorithmName
		{
			get
			{
				return "DSA";
			}
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x0006E5FC File Offset: 0x0006D5FC
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
				if (!(parameters is DsaPrivateKeyParameters))
				{
					throw new InvalidKeyException("DSA private key required for signing");
				}
				this.key = (DsaPrivateKeyParameters)parameters;
				return;
			}
			else
			{
				if (!(parameters is DsaPublicKeyParameters))
				{
					throw new InvalidKeyException("DSA public key required for verification");
				}
				this.key = (DsaPublicKeyParameters)parameters;
				return;
			}
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0006E67C File Offset: 0x0006D67C
		public BigInteger[] GenerateSignature(byte[] message)
		{
			DsaParameters parameters = this.key.Parameters;
			BigInteger q = parameters.Q;
			BigInteger bigInteger = this.calculateE(q, message);
			BigInteger bigInteger2;
			do
			{
				bigInteger2 = new BigInteger(q.BitLength, this.random);
			}
			while (bigInteger2.CompareTo(q) >= 0);
			BigInteger bigInteger3 = parameters.G.ModPow(bigInteger2, parameters.P).Mod(q);
			bigInteger2 = bigInteger2.ModInverse(q).Multiply(bigInteger.Add(((DsaPrivateKeyParameters)this.key).X.Multiply(bigInteger3)));
			BigInteger bigInteger4 = bigInteger2.Mod(q);
			return new BigInteger[]
			{
				bigInteger3,
				bigInteger4
			};
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x0006E724 File Offset: 0x0006D724
		public bool VerifySignature(byte[] message, BigInteger r, BigInteger s)
		{
			DsaParameters parameters = this.key.Parameters;
			BigInteger q = parameters.Q;
			BigInteger bigInteger = this.calculateE(q, message);
			if (r.SignValue <= 0 || q.CompareTo(r) <= 0)
			{
				return false;
			}
			if (s.SignValue <= 0 || q.CompareTo(s) <= 0)
			{
				return false;
			}
			BigInteger val = s.ModInverse(q);
			BigInteger bigInteger2 = bigInteger.Multiply(val).Mod(q);
			BigInteger bigInteger3 = r.Multiply(val).Mod(q);
			BigInteger p = parameters.P;
			bigInteger2 = parameters.G.ModPow(bigInteger2, p);
			bigInteger3 = ((DsaPublicKeyParameters)this.key).Y.ModPow(bigInteger3, p);
			BigInteger bigInteger4 = bigInteger2.Multiply(bigInteger3).Mod(p).Mod(q);
			return bigInteger4.Equals(r);
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x0006E7F4 File Offset: 0x0006D7F4
		private BigInteger calculateE(BigInteger n, byte[] message)
		{
			int length = Math.Min(message.Length, n.BitLength / 8);
			return new BigInteger(1, message, 0, length);
		}

		// Token: 0x04000D7B RID: 3451
		private DsaKeyParameters key;

		// Token: 0x04000D7C RID: 3452
		private SecureRandom random;
	}
}
