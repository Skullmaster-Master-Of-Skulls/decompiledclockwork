using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x02000611 RID: 1553
	public class Gost3410DigestSigner : ISigner
	{
		// Token: 0x060034EF RID: 13551 RVA: 0x001488E2 File Offset: 0x001478E2
		public Gost3410DigestSigner(IDsa signer, IDigest digest)
		{
			this.dsaSigner = signer;
			this.digest = digest;
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x060034F0 RID: 13552 RVA: 0x001488F8 File Offset: 0x001478F8
		public string AlgorithmName
		{
			get
			{
				return this.digest.AlgorithmName + "with" + this.dsaSigner.AlgorithmName;
			}
		}

		// Token: 0x060034F1 RID: 13553 RVA: 0x0014891C File Offset: 0x0014791C
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
				throw new InvalidKeyException("Signing Requires Private Key.");
			}
			if (!forSigning && asymmetricKeyParameter.IsPrivate)
			{
				throw new InvalidKeyException("Verification Requires Public Key.");
			}
			this.Reset();
			this.dsaSigner.Init(forSigning, parameters);
		}

		// Token: 0x060034F2 RID: 13554 RVA: 0x00148991 File Offset: 0x00147991
		public void Update(byte input)
		{
			this.digest.Update(input);
		}

		// Token: 0x060034F3 RID: 13555 RVA: 0x0014899F File Offset: 0x0014799F
		public void BlockUpdate(byte[] input, int inOff, int length)
		{
			this.digest.BlockUpdate(input, inOff, length);
		}

		// Token: 0x060034F4 RID: 13556 RVA: 0x001489B0 File Offset: 0x001479B0
		public byte[] GenerateSignature()
		{
			if (!this.forSigning)
			{
				throw new InvalidOperationException("GOST3410DigestSigner not initialised for signature generation.");
			}
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array, 0);
			byte[] result;
			try
			{
				BigInteger[] array2 = this.dsaSigner.GenerateSignature(array);
				byte[] array3 = new byte[64];
				byte[] array4 = array2[0].ToByteArrayUnsigned();
				byte[] array5 = array2[1].ToByteArrayUnsigned();
				array5.CopyTo(array3, 32 - array5.Length);
				array4.CopyTo(array3, 64 - array4.Length);
				result = array3;
			}
			catch (Exception ex)
			{
				throw new SignatureException(ex.Message, ex);
			}
			return result;
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x00148A5C File Offset: 0x00147A5C
		public bool VerifySignature(byte[] signature)
		{
			if (this.forSigning)
			{
				throw new InvalidOperationException("DSADigestSigner not initialised for verification");
			}
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array, 0);
			BigInteger r;
			BigInteger s;
			try
			{
				r = new BigInteger(1, signature, 32, 32);
				s = new BigInteger(1, signature, 0, 32);
			}
			catch (Exception exception)
			{
				throw new SignatureException("error decoding signature bytes.", exception);
			}
			return this.dsaSigner.VerifySignature(array, r, s);
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x00148AE0 File Offset: 0x00147AE0
		public void Reset()
		{
			this.digest.Reset();
		}

		// Token: 0x04002372 RID: 9074
		private readonly IDigest digest;

		// Token: 0x04002373 RID: 9075
		private readonly IDsa dsaSigner;

		// Token: 0x04002374 RID: 9076
		private bool forSigning;
	}
}
