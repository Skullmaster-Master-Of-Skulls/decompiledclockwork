using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x020002EB RID: 747
	public class GenericSigner : ISigner
	{
		// Token: 0x06001BA2 RID: 7074 RVA: 0x000A5A83 File Offset: 0x000A4A83
		public GenericSigner(IAsymmetricBlockCipher engine, IDigest digest)
		{
			this.engine = engine;
			this.digest = digest;
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001BA3 RID: 7075 RVA: 0x000A5A9C File Offset: 0x000A4A9C
		public string AlgorithmName
		{
			get
			{
				return string.Concat(new string[]
				{
					"Generic(",
					this.engine.AlgorithmName,
					"/",
					this.digest.AlgorithmName,
					")"
				});
			}
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x000A5AEC File Offset: 0x000A4AEC
		public void Init(bool forSigning, ICipherParameters parameters)
		{
			this.forSigning = forSigning;
			AsymmetricKeyParameter asymmetricKeyParameter;
			if (parameters is ParametersWithRandom)
			{
				asymmetricKeyParameter = (AsymmetricKeyParameter)((ParametersWithRandom)parameters).Parameters;
			}
			else
			{
				asymmetricKeyParameter = (AsymmetricKeyParameter)parameters;
			}
			if (forSigning && !asymmetricKeyParameter.IsPrivate)
			{
				throw new InvalidKeyException("Signing requires private key.");
			}
			if (!forSigning && asymmetricKeyParameter.IsPrivate)
			{
				throw new InvalidKeyException("Verification requires public key.");
			}
			this.Reset();
			this.engine.Init(forSigning, parameters);
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x000A5B61 File Offset: 0x000A4B61
		public void Update(byte input)
		{
			this.digest.Update(input);
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x000A5B6F File Offset: 0x000A4B6F
		public void BlockUpdate(byte[] input, int inOff, int length)
		{
			this.digest.BlockUpdate(input, inOff, length);
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x000A5B80 File Offset: 0x000A4B80
		public byte[] GenerateSignature()
		{
			if (!this.forSigning)
			{
				throw new InvalidOperationException("GenericSigner not initialised for signature generation.");
			}
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array, 0);
			return this.engine.ProcessBlock(array, 0, array.Length);
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x000A5BD0 File Offset: 0x000A4BD0
		public bool VerifySignature(byte[] signature)
		{
			if (this.forSigning)
			{
				throw new InvalidOperationException("GenericSigner not initialised for verification");
			}
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array, 0);
			bool result;
			try
			{
				byte[] a = this.engine.ProcessBlock(signature, 0, signature.Length);
				result = Arrays.ConstantTimeAreEqual(a, array);
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x000A5C40 File Offset: 0x000A4C40
		public void Reset()
		{
			this.digest.Reset();
		}

		// Token: 0x040012FA RID: 4858
		private readonly IAsymmetricBlockCipher engine;

		// Token: 0x040012FB RID: 4859
		private readonly IDigest digest;

		// Token: 0x040012FC RID: 4860
		private bool forSigning;
	}
}
