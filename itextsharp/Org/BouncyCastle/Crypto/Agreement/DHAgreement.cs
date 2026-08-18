using System;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Agreement
{
	// Token: 0x02000195 RID: 405
	public class DHAgreement
	{
		// Token: 0x06000FC9 RID: 4041 RVA: 0x0005BA70 File Offset: 0x0005AA70
		public void Init(ICipherParameters parameters)
		{
			AsymmetricKeyParameter asymmetricKeyParameter;
			if (parameters is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)parameters;
				this.random = parametersWithRandom.Random;
				asymmetricKeyParameter = (AsymmetricKeyParameter)parametersWithRandom.Parameters;
			}
			else
			{
				this.random = new SecureRandom();
				asymmetricKeyParameter = (AsymmetricKeyParameter)parameters;
			}
			if (!(asymmetricKeyParameter is DHPrivateKeyParameters))
			{
				throw new ArgumentException("DHEngine expects DHPrivateKeyParameters");
			}
			this.key = (DHPrivateKeyParameters)asymmetricKeyParameter;
			this.dhParams = this.key.Parameters;
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0005BAE8 File Offset: 0x0005AAE8
		public BigInteger CalculateMessage()
		{
			DHKeyPairGenerator dhkeyPairGenerator = new DHKeyPairGenerator();
			dhkeyPairGenerator.Init(new DHKeyGenerationParameters(this.random, this.dhParams));
			AsymmetricCipherKeyPair asymmetricCipherKeyPair = dhkeyPairGenerator.GenerateKeyPair();
			this.privateValue = ((DHPrivateKeyParameters)asymmetricCipherKeyPair.Private).X;
			return ((DHPublicKeyParameters)asymmetricCipherKeyPair.Public).Y;
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0005BB40 File Offset: 0x0005AB40
		public BigInteger CalculateAgreement(DHPublicKeyParameters pub, BigInteger message)
		{
			if (pub == null)
			{
				throw new ArgumentNullException("pub");
			}
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			if (!pub.Parameters.Equals(this.dhParams))
			{
				throw new ArgumentException("Diffie-Hellman public key has wrong parameters.");
			}
			BigInteger p = this.dhParams.P;
			return message.ModPow(this.key.X, p).Multiply(pub.Y.ModPow(this.privateValue, p)).Mod(p);
		}

		// Token: 0x04000B62 RID: 2914
		private DHPrivateKeyParameters key;

		// Token: 0x04000B63 RID: 2915
		private DHParameters dhParams;

		// Token: 0x04000B64 RID: 2916
		private BigInteger privateValue;

		// Token: 0x04000B65 RID: 2917
		private SecureRandom random;
	}
}
