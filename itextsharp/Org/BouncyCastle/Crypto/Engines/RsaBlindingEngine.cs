using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200050A RID: 1290
	public class RsaBlindingEngine : IAsymmetricBlockCipher
	{
		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06002C16 RID: 11286 RVA: 0x0010C8F8 File Offset: 0x0010B8F8
		public string AlgorithmName
		{
			get
			{
				return "RSA";
			}
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x0010C900 File Offset: 0x0010B900
		public void Init(bool forEncryption, ICipherParameters param)
		{
			RsaBlindingParameters rsaBlindingParameters;
			if (param is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)param;
				rsaBlindingParameters = (RsaBlindingParameters)parametersWithRandom.Parameters;
			}
			else
			{
				rsaBlindingParameters = (RsaBlindingParameters)param;
			}
			this.core.Init(forEncryption, rsaBlindingParameters.PublicKey);
			this.forEncryption = forEncryption;
			this.key = rsaBlindingParameters.PublicKey;
			this.blindingFactor = rsaBlindingParameters.BlindingFactor;
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x0010C962 File Offset: 0x0010B962
		public int GetInputBlockSize()
		{
			return this.core.GetInputBlockSize();
		}

		// Token: 0x06002C19 RID: 11289 RVA: 0x0010C96F File Offset: 0x0010B96F
		public int GetOutputBlockSize()
		{
			return this.core.GetOutputBlockSize();
		}

		// Token: 0x06002C1A RID: 11290 RVA: 0x0010C97C File Offset: 0x0010B97C
		public byte[] ProcessBlock(byte[] inBuf, int inOff, int inLen)
		{
			BigInteger bigInteger = this.core.ConvertInput(inBuf, inOff, inLen);
			if (this.forEncryption)
			{
				bigInteger = this.BlindMessage(bigInteger);
			}
			else
			{
				bigInteger = this.UnblindMessage(bigInteger);
			}
			return this.core.ConvertOutput(bigInteger);
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x0010C9C0 File Offset: 0x0010B9C0
		private BigInteger BlindMessage(BigInteger msg)
		{
			BigInteger bigInteger = this.blindingFactor;
			bigInteger = msg.Multiply(bigInteger.ModPow(this.key.Exponent, this.key.Modulus));
			return bigInteger.Mod(this.key.Modulus);
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x0010CA0C File Offset: 0x0010BA0C
		private BigInteger UnblindMessage(BigInteger blindedMsg)
		{
			BigInteger modulus = this.key.Modulus;
			BigInteger val = this.blindingFactor.ModInverse(modulus);
			BigInteger bigInteger = blindedMsg.Multiply(val);
			return bigInteger.Mod(modulus);
		}

		// Token: 0x04001E62 RID: 7778
		private readonly RsaCoreEngine core = new RsaCoreEngine();

		// Token: 0x04001E63 RID: 7779
		private RsaKeyParameters key;

		// Token: 0x04001E64 RID: 7780
		private BigInteger blindingFactor;

		// Token: 0x04001E65 RID: 7781
		private bool forEncryption;
	}
}
