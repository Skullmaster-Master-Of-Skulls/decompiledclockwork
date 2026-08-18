using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020001F4 RID: 500
	public class RsaKeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x06001376 RID: 4982 RVA: 0x0006F149 File Offset: 0x0006E149
		public void Init(KeyGenerationParameters parameters)
		{
			if (parameters is RsaKeyGenerationParameters)
			{
				this.param = (RsaKeyGenerationParameters)parameters;
				return;
			}
			this.param = new RsaKeyGenerationParameters(RsaKeyPairGenerator.DefaultPublicExponent, parameters.Random, parameters.Strength, 12);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0006F180 File Offset: 0x0006E180
		public AsymmetricCipherKeyPair GenerateKeyPair()
		{
			int strength = this.param.Strength;
			int num = (strength + 1) / 2;
			int bitLength = strength - num;
			int num2 = strength / 3;
			BigInteger publicExponent = this.param.PublicExponent;
			BigInteger bigInteger;
			do
			{
				bigInteger = new BigInteger(num, 1, this.param.Random);
			}
			while (bigInteger.Mod(publicExponent).Equals(BigInteger.One) || !bigInteger.IsProbablePrime(this.param.Certainty) || !publicExponent.Gcd(bigInteger.Subtract(BigInteger.One)).Equals(BigInteger.One));
			BigInteger bigInteger2;
			BigInteger bigInteger3;
			for (;;)
			{
				bigInteger2 = new BigInteger(bitLength, 1, this.param.Random);
				if (bigInteger2.Subtract(bigInteger).Abs().BitLength >= num2 && !bigInteger2.Mod(publicExponent).Equals(BigInteger.One) && bigInteger2.IsProbablePrime(this.param.Certainty) && publicExponent.Gcd(bigInteger2.Subtract(BigInteger.One)).Equals(BigInteger.One))
				{
					bigInteger3 = bigInteger.Multiply(bigInteger2);
					if (bigInteger3.BitLength == this.param.Strength)
					{
						break;
					}
					bigInteger = bigInteger.Max(bigInteger2);
				}
			}
			BigInteger bigInteger4;
			if (bigInteger.CompareTo(bigInteger2) < 0)
			{
				bigInteger4 = bigInteger;
				bigInteger = bigInteger2;
				bigInteger2 = bigInteger4;
			}
			BigInteger bigInteger5 = bigInteger.Subtract(BigInteger.One);
			BigInteger bigInteger6 = bigInteger2.Subtract(BigInteger.One);
			bigInteger4 = bigInteger5.Multiply(bigInteger6);
			BigInteger bigInteger7 = publicExponent.ModInverse(bigInteger4);
			BigInteger dP = bigInteger7.Remainder(bigInteger5);
			BigInteger dQ = bigInteger7.Remainder(bigInteger6);
			BigInteger qInv = bigInteger2.ModInverse(bigInteger);
			return new AsymmetricCipherKeyPair(new RsaKeyParameters(false, bigInteger3, publicExponent), new RsaPrivateCrtKeyParameters(bigInteger3, publicExponent, bigInteger7, bigInteger, bigInteger2, dP, dQ, qInv));
		}

		// Token: 0x04000D93 RID: 3475
		private const int DefaultTests = 12;

		// Token: 0x04000D94 RID: 3476
		private static readonly BigInteger DefaultPublicExponent = BigInteger.ValueOf(65537L);

		// Token: 0x04000D95 RID: 3477
		private RsaKeyGenerationParameters param;
	}
}
