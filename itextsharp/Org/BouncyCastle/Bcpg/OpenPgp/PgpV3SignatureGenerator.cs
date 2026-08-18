using System;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Date;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000299 RID: 665
	public class PgpV3SignatureGenerator
	{
		// Token: 0x06001906 RID: 6406 RVA: 0x00092FC7 File Offset: 0x00091FC7
		public PgpV3SignatureGenerator(PublicKeyAlgorithmTag keyAlgorithm, HashAlgorithmTag hashAlgorithm)
		{
			this.keyAlgorithm = keyAlgorithm;
			this.hashAlgorithm = hashAlgorithm;
			this.dig = DigestUtilities.GetDigest(PgpUtilities.GetDigestName(hashAlgorithm));
			this.sig = SignerUtilities.GetSigner(PgpUtilities.GetSignatureName(keyAlgorithm, hashAlgorithm));
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x00093000 File Offset: 0x00092000
		public void InitSign(int sigType, PgpPrivateKey key)
		{
			this.InitSign(sigType, key, null);
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x0009300C File Offset: 0x0009200C
		public void InitSign(int sigType, PgpPrivateKey key, SecureRandom random)
		{
			this.privKey = key;
			this.signatureType = sigType;
			try
			{
				ICipherParameters parameters = key.Key;
				if (random != null)
				{
					parameters = new ParametersWithRandom(key.Key, random);
				}
				this.sig.Init(true, parameters);
			}
			catch (InvalidKeyException exception)
			{
				throw new PgpException("invalid key.", exception);
			}
			this.dig.Reset();
			this.lastb = 0;
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x0009307C File Offset: 0x0009207C
		public void Update(byte b)
		{
			if (this.signatureType == 1)
			{
				this.doCanonicalUpdateByte(b);
				return;
			}
			this.doUpdateByte(b);
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x00093096 File Offset: 0x00092096
		private void doCanonicalUpdateByte(byte b)
		{
			if (b == 13)
			{
				this.doUpdateCRLF();
			}
			else if (b == 10)
			{
				if (this.lastb != 13)
				{
					this.doUpdateCRLF();
				}
			}
			else
			{
				this.doUpdateByte(b);
			}
			this.lastb = b;
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x000930CA File Offset: 0x000920CA
		private void doUpdateCRLF()
		{
			this.doUpdateByte(13);
			this.doUpdateByte(10);
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x000930DC File Offset: 0x000920DC
		private void doUpdateByte(byte b)
		{
			this.sig.Update(b);
			this.dig.Update(b);
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x000930F8 File Offset: 0x000920F8
		public void Update(byte[] b)
		{
			if (this.signatureType == 1)
			{
				for (int num = 0; num != b.Length; num++)
				{
					this.doCanonicalUpdateByte(b[num]);
				}
				return;
			}
			this.sig.BlockUpdate(b, 0, b.Length);
			this.dig.BlockUpdate(b, 0, b.Length);
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x00093148 File Offset: 0x00092148
		public void Update(byte[] b, int off, int len)
		{
			if (this.signatureType == 1)
			{
				int num = off + len;
				for (int num2 = off; num2 != num; num2++)
				{
					this.doCanonicalUpdateByte(b[num2]);
				}
				return;
			}
			this.sig.BlockUpdate(b, off, len);
			this.dig.BlockUpdate(b, off, len);
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x00093194 File Offset: 0x00092194
		public PgpOnePassSignature GenerateOnePassVersion(bool isNested)
		{
			return new PgpOnePassSignature(new OnePassSignaturePacket(this.signatureType, this.hashAlgorithm, this.keyAlgorithm, this.privKey.KeyId, isNested));
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x000931C0 File Offset: 0x000921C0
		public PgpSignature Generate()
		{
			long num = DateTimeUtilities.CurrentUnixMs() / 1000L;
			byte[] array = new byte[]
			{
				(byte)this.signatureType,
				(byte)(num >> 24),
				(byte)(num >> 16),
				(byte)(num >> 8),
				(byte)num
			};
			this.sig.BlockUpdate(array, 0, array.Length);
			this.dig.BlockUpdate(array, 0, array.Length);
			byte[] encoding = this.sig.GenerateSignature();
			byte[] array2 = DigestUtilities.DoFinal(this.dig);
			byte[] fingerprint = new byte[]
			{
				array2[0],
				array2[1]
			};
			MPInteger[] signature = (this.keyAlgorithm == PublicKeyAlgorithmTag.RsaSign || this.keyAlgorithm == PublicKeyAlgorithmTag.RsaGeneral) ? PgpUtilities.RsaSigToMpi(encoding) : PgpUtilities.DsaSigToMpi(encoding);
			return new PgpSignature(new SignaturePacket(3, this.signatureType, this.privKey.KeyId, this.keyAlgorithm, this.hashAlgorithm, num * 1000L, fingerprint, signature));
		}

		// Token: 0x040010E6 RID: 4326
		private PublicKeyAlgorithmTag keyAlgorithm;

		// Token: 0x040010E7 RID: 4327
		private HashAlgorithmTag hashAlgorithm;

		// Token: 0x040010E8 RID: 4328
		private PgpPrivateKey privKey;

		// Token: 0x040010E9 RID: 4329
		private ISigner sig;

		// Token: 0x040010EA RID: 4330
		private IDigest dig;

		// Token: 0x040010EB RID: 4331
		private int signatureType;

		// Token: 0x040010EC RID: 4332
		private byte lastb;
	}
}
