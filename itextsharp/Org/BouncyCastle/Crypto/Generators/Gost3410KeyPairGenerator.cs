using System;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x0200034E RID: 846
	public class Gost3410KeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x06001E7F RID: 7807 RVA: 0x000B65BC File Offset: 0x000B55BC
		public void Init(KeyGenerationParameters parameters)
		{
			if (parameters is Gost3410KeyGenerationParameters)
			{
				this.param = (Gost3410KeyGenerationParameters)parameters;
				return;
			}
			Gost3410KeyGenerationParameters gost3410KeyGenerationParameters = new Gost3410KeyGenerationParameters(parameters.Random, CryptoProObjectIdentifiers.GostR3410x94CryptoProA);
			int strength = parameters.Strength;
			int num = gost3410KeyGenerationParameters.Parameters.P.BitLength - 1;
			this.param = gost3410KeyGenerationParameters;
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x000B6610 File Offset: 0x000B5610
		public AsymmetricCipherKeyPair GenerateKeyPair()
		{
			SecureRandom random = this.param.Random;
			Gost3410Parameters parameters = this.param.Parameters;
			BigInteger q = parameters.Q;
			BigInteger bigInteger;
			do
			{
				bigInteger = new BigInteger(256, random);
			}
			while (bigInteger.SignValue < 1 || bigInteger.CompareTo(q) >= 0);
			BigInteger p = parameters.P;
			BigInteger a = parameters.A;
			BigInteger y = a.ModPow(bigInteger, p);
			if (this.param.PublicKeyParamSet != null)
			{
				return new AsymmetricCipherKeyPair(new Gost3410PublicKeyParameters(y, this.param.PublicKeyParamSet), new Gost3410PrivateKeyParameters(bigInteger, this.param.PublicKeyParamSet));
			}
			return new AsymmetricCipherKeyPair(new Gost3410PublicKeyParameters(y, parameters), new Gost3410PrivateKeyParameters(bigInteger, parameters));
		}

		// Token: 0x0400151E RID: 5406
		private Gost3410KeyGenerationParameters param;
	}
}
