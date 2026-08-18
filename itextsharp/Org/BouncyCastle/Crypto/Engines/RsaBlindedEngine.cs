using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x020005AA RID: 1450
	public class RsaBlindedEngine : IAsymmetricBlockCipher
	{
		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06003206 RID: 12806 RVA: 0x0013786E File Offset: 0x0013686E
		public string AlgorithmName
		{
			get
			{
				return "RSA";
			}
		}

		// Token: 0x06003207 RID: 12807 RVA: 0x00137878 File Offset: 0x00136878
		public void Init(bool forEncryption, ICipherParameters param)
		{
			this.core.Init(forEncryption, param);
			if (param is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)param;
				this.key = (RsaKeyParameters)parametersWithRandom.Parameters;
				this.random = parametersWithRandom.Random;
				return;
			}
			this.key = (RsaKeyParameters)param;
			this.random = new SecureRandom();
		}

		// Token: 0x06003208 RID: 12808 RVA: 0x001378D6 File Offset: 0x001368D6
		public int GetInputBlockSize()
		{
			return this.core.GetInputBlockSize();
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x001378E3 File Offset: 0x001368E3
		public int GetOutputBlockSize()
		{
			return this.core.GetOutputBlockSize();
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x001378F0 File Offset: 0x001368F0
		public byte[] ProcessBlock(byte[] inBuf, int inOff, int inLen)
		{
			if (this.key == null)
			{
				throw new InvalidOperationException("RSA engine not initialised");
			}
			BigInteger bigInteger = this.core.ConvertInput(inBuf, inOff, inLen);
			BigInteger result;
			if (this.key is RsaPrivateCrtKeyParameters)
			{
				RsaPrivateCrtKeyParameters rsaPrivateCrtKeyParameters = (RsaPrivateCrtKeyParameters)this.key;
				BigInteger publicExponent = rsaPrivateCrtKeyParameters.PublicExponent;
				if (publicExponent != null)
				{
					BigInteger modulus = rsaPrivateCrtKeyParameters.Modulus;
					BigInteger bigInteger2 = BigIntegers.CreateRandomInRange(BigInteger.One, modulus.Subtract(BigInteger.One), this.random);
					BigInteger input = bigInteger2.ModPow(publicExponent, modulus).Multiply(bigInteger).Mod(modulus);
					BigInteger bigInteger3 = this.core.ProcessBlock(input);
					BigInteger val = bigInteger2.ModInverse(modulus);
					result = bigInteger3.Multiply(val).Mod(modulus);
				}
				else
				{
					result = this.core.ProcessBlock(bigInteger);
				}
			}
			else
			{
				result = this.core.ProcessBlock(bigInteger);
			}
			return this.core.ConvertOutput(result);
		}

		// Token: 0x04002258 RID: 8792
		private readonly RsaCoreEngine core = new RsaCoreEngine();

		// Token: 0x04002259 RID: 8793
		private RsaKeyParameters key;

		// Token: 0x0400225A RID: 8794
		private SecureRandom random;
	}
}
