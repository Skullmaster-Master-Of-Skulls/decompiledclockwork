using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x0200042C RID: 1068
	public class DsaDigestSigner : ISigner
	{
		// Token: 0x0600245D RID: 9309 RVA: 0x000DDE6F File Offset: 0x000DCE6F
		public DsaDigestSigner(IDsa signer, IDigest digest)
		{
			this.digest = digest;
			this.dsaSigner = signer;
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x0600245E RID: 9310 RVA: 0x000DDE85 File Offset: 0x000DCE85
		public string AlgorithmName
		{
			get
			{
				return this.digest.AlgorithmName + "with" + this.dsaSigner.AlgorithmName;
			}
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x000DDEA8 File Offset: 0x000DCEA8
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

		// Token: 0x06002460 RID: 9312 RVA: 0x000DDF1D File Offset: 0x000DCF1D
		public void Update(byte input)
		{
			this.digest.Update(input);
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x000DDF2B File Offset: 0x000DCF2B
		public void BlockUpdate(byte[] input, int inOff, int length)
		{
			this.digest.BlockUpdate(input, inOff, length);
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x000DDF3C File Offset: 0x000DCF3C
		public byte[] GenerateSignature()
		{
			if (!this.forSigning)
			{
				throw new InvalidOperationException("DSADigestSigner not initialised for signature generation.");
			}
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array, 0);
			BigInteger[] array2 = this.dsaSigner.GenerateSignature(array);
			return this.DerEncode(array2[0], array2[1]);
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x000DDF94 File Offset: 0x000DCF94
		public bool VerifySignature(byte[] signature)
		{
			if (this.forSigning)
			{
				throw new InvalidOperationException("DSADigestSigner not initialised for verification");
			}
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array, 0);
			bool result;
			try
			{
				BigInteger[] array2 = this.DerDecode(signature);
				result = this.dsaSigner.VerifySignature(array, array2[0], array2[1]);
			}
			catch (IOException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x000DE008 File Offset: 0x000DD008
		public void Reset()
		{
			this.digest.Reset();
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x000DE018 File Offset: 0x000DD018
		private byte[] DerEncode(BigInteger r, BigInteger s)
		{
			return new DerSequence(new Asn1Encodable[]
			{
				new DerInteger(r),
				new DerInteger(s)
			}).GetDerEncoded();
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x000DE04C File Offset: 0x000DD04C
		private BigInteger[] DerDecode(byte[] encoding)
		{
			Asn1Sequence asn1Sequence = (Asn1Sequence)Asn1Object.FromByteArray(encoding);
			return new BigInteger[]
			{
				((DerInteger)asn1Sequence[0]).Value,
				((DerInteger)asn1Sequence[1]).Value
			};
		}

		// Token: 0x04001976 RID: 6518
		private readonly IDigest digest;

		// Token: 0x04001977 RID: 6519
		private readonly IDsa dsaSigner;

		// Token: 0x04001978 RID: 6520
		private bool forSigning;
	}
}
