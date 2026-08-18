using System;
using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200046E RID: 1134
	public class PgpOnePassSignature
	{
		// Token: 0x060026AC RID: 9900 RVA: 0x000EA794 File Offset: 0x000E9794
		internal PgpOnePassSignature(BcpgInputStream bcpgInput) : this((OnePassSignaturePacket)bcpgInput.ReadPacket())
		{
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x000EA7A7 File Offset: 0x000E97A7
		internal PgpOnePassSignature(OnePassSignaturePacket sigPack)
		{
			this.sigPack = sigPack;
			this.signatureType = sigPack.SignatureType;
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x000EA7C4 File Offset: 0x000E97C4
		public void InitVerify(PgpPublicKey pubKey)
		{
			this.lastb = 0;
			try
			{
				this.sig = SignerUtilities.GetSigner(PgpUtilities.GetSignatureName(this.sigPack.KeyAlgorithm, this.sigPack.HashAlgorithm));
			}
			catch (Exception exception)
			{
				throw new PgpException("can't set up signature object.", exception);
			}
			try
			{
				this.sig.Init(false, pubKey.GetKey());
			}
			catch (InvalidKeyException exception2)
			{
				throw new PgpException("invalid key.", exception2);
			}
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x000EA84C File Offset: 0x000E984C
		public void Update(byte b)
		{
			if (this.signatureType == 1)
			{
				this.doCanonicalUpdateByte(b);
				return;
			}
			this.sig.Update(b);
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x000EA86B File Offset: 0x000E986B
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
				this.sig.Update(b);
			}
			this.lastb = b;
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x000EA8A4 File Offset: 0x000E98A4
		private void doUpdateCRLF()
		{
			this.sig.Update(13);
			this.sig.Update(10);
		}

		// Token: 0x060026B2 RID: 9906 RVA: 0x000EA8C0 File Offset: 0x000E98C0
		public void Update(byte[] bytes)
		{
			if (this.signatureType == 1)
			{
				for (int num = 0; num != bytes.Length; num++)
				{
					this.doCanonicalUpdateByte(bytes[num]);
				}
				return;
			}
			this.sig.BlockUpdate(bytes, 0, bytes.Length);
		}

		// Token: 0x060026B3 RID: 9907 RVA: 0x000EA900 File Offset: 0x000E9900
		public void Update(byte[] bytes, int off, int length)
		{
			if (this.signatureType == 1)
			{
				int num = off + length;
				for (int num2 = off; num2 != num; num2++)
				{
					this.doCanonicalUpdateByte(bytes[num2]);
				}
				return;
			}
			this.sig.BlockUpdate(bytes, off, length);
		}

		// Token: 0x060026B4 RID: 9908 RVA: 0x000EA940 File Offset: 0x000E9940
		public bool Verify(PgpSignature pgpSig)
		{
			byte[] signatureTrailer = pgpSig.GetSignatureTrailer();
			this.sig.BlockUpdate(signatureTrailer, 0, signatureTrailer.Length);
			return this.sig.VerifySignature(pgpSig.GetSignature());
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x060026B5 RID: 9909 RVA: 0x000EA975 File Offset: 0x000E9975
		public long KeyId
		{
			get
			{
				return this.sigPack.KeyId;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x060026B6 RID: 9910 RVA: 0x000EA982 File Offset: 0x000E9982
		public int SignatureType
		{
			get
			{
				return this.sigPack.SignatureType;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x060026B7 RID: 9911 RVA: 0x000EA98F File Offset: 0x000E998F
		public HashAlgorithmTag HashAlgorithm
		{
			get
			{
				return this.sigPack.HashAlgorithm;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x060026B8 RID: 9912 RVA: 0x000EA99C File Offset: 0x000E999C
		public PublicKeyAlgorithmTag KeyAlgorithm
		{
			get
			{
				return this.sigPack.KeyAlgorithm;
			}
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x000EA9AC File Offset: 0x000E99AC
		public byte[] GetEncoded()
		{
			MemoryStream memoryStream = new MemoryStream();
			this.Encode(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x060026BA RID: 9914 RVA: 0x000EA9CC File Offset: 0x000E99CC
		public void Encode(Stream outStr)
		{
			BcpgOutputStream.Wrap(outStr).WritePacket(this.sigPack);
		}

		// Token: 0x04001AB2 RID: 6834
		private OnePassSignaturePacket sigPack;

		// Token: 0x04001AB3 RID: 6835
		private int signatureType;

		// Token: 0x04001AB4 RID: 6836
		private ISigner sig;

		// Token: 0x04001AB5 RID: 6837
		private byte lastb;
	}
}
