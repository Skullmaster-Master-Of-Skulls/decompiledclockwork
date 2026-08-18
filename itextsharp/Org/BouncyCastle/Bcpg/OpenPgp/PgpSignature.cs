using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Date;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x020005A2 RID: 1442
	public class PgpSignature
	{
		// Token: 0x06003194 RID: 12692 RVA: 0x0013560A File Offset: 0x0013460A
		internal PgpSignature(BcpgInputStream bcpgInput) : this((SignaturePacket)bcpgInput.ReadPacket())
		{
		}

		// Token: 0x06003195 RID: 12693 RVA: 0x0013561D File Offset: 0x0013461D
		internal PgpSignature(SignaturePacket sigPacket) : this(sigPacket, null)
		{
		}

		// Token: 0x06003196 RID: 12694 RVA: 0x00135627 File Offset: 0x00134627
		internal PgpSignature(SignaturePacket sigPacket, TrustPacket trustPacket)
		{
			if (sigPacket == null)
			{
				throw new ArgumentNullException("sigPacket");
			}
			this.sigPck = sigPacket;
			this.signatureType = this.sigPck.SignatureType;
			this.trustPck = trustPacket;
		}

		// Token: 0x06003197 RID: 12695 RVA: 0x0013565C File Offset: 0x0013465C
		private void GetSig()
		{
			this.sig = SignerUtilities.GetSigner(PgpUtilities.GetSignatureName(this.sigPck.KeyAlgorithm, this.sigPck.HashAlgorithm));
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06003198 RID: 12696 RVA: 0x00135684 File Offset: 0x00134684
		public int Version
		{
			get
			{
				return this.sigPck.Version;
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06003199 RID: 12697 RVA: 0x00135691 File Offset: 0x00134691
		public PublicKeyAlgorithmTag KeyAlgorithm
		{
			get
			{
				return this.sigPck.KeyAlgorithm;
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x0600319A RID: 12698 RVA: 0x0013569E File Offset: 0x0013469E
		public HashAlgorithmTag HashAlgorithm
		{
			get
			{
				return this.sigPck.HashAlgorithm;
			}
		}

		// Token: 0x0600319B RID: 12699 RVA: 0x001356AC File Offset: 0x001346AC
		public void InitVerify(PgpPublicKey pubKey)
		{
			this.lastb = 0;
			if (this.sig == null)
			{
				this.GetSig();
			}
			try
			{
				this.sig.Init(false, pubKey.GetKey());
			}
			catch (InvalidKeyException exception)
			{
				throw new PgpException("invalid key.", exception);
			}
		}

		// Token: 0x0600319C RID: 12700 RVA: 0x00135700 File Offset: 0x00134700
		public void Update(byte b)
		{
			if (this.signatureType == 1)
			{
				this.doCanonicalUpdateByte(b);
				return;
			}
			this.sig.Update(b);
		}

		// Token: 0x0600319D RID: 12701 RVA: 0x0013571F File Offset: 0x0013471F
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

		// Token: 0x0600319E RID: 12702 RVA: 0x00135758 File Offset: 0x00134758
		private void doUpdateCRLF()
		{
			this.sig.Update(13);
			this.sig.Update(10);
		}

		// Token: 0x0600319F RID: 12703 RVA: 0x00135774 File Offset: 0x00134774
		public void Update(params byte[] bytes)
		{
			this.Update(bytes, 0, bytes.Length);
		}

		// Token: 0x060031A0 RID: 12704 RVA: 0x00135784 File Offset: 0x00134784
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

		// Token: 0x060031A1 RID: 12705 RVA: 0x001357C4 File Offset: 0x001347C4
		public bool Verify()
		{
			byte[] signatureTrailer = this.GetSignatureTrailer();
			this.sig.BlockUpdate(signatureTrailer, 0, signatureTrailer.Length);
			return this.sig.VerifySignature(this.GetSignature());
		}

		// Token: 0x060031A2 RID: 12706 RVA: 0x001357FC File Offset: 0x001347FC
		private void UpdateWithIdData(int header, byte[] idBytes)
		{
			this.Update(new byte[]
			{
				(byte)header,
				(byte)(idBytes.Length >> 24),
				(byte)(idBytes.Length >> 16),
				(byte)(idBytes.Length >> 8),
				(byte)idBytes.Length
			});
			this.Update(idBytes);
		}

		// Token: 0x060031A3 RID: 12707 RVA: 0x00135848 File Offset: 0x00134848
		private void UpdateWithPublicKey(PgpPublicKey key)
		{
			byte[] encodedPublicKey = this.GetEncodedPublicKey(key);
			this.Update(new byte[]
			{
				153,
				(byte)(encodedPublicKey.Length >> 8),
				(byte)encodedPublicKey.Length
			});
			this.Update(encodedPublicKey);
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x0013588C File Offset: 0x0013488C
		public bool VerifyCertification(PgpUserAttributeSubpacketVector userAttributes, PgpPublicKey key)
		{
			this.UpdateWithPublicKey(key);
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				foreach (UserAttributeSubpacket userAttributeSubpacket in userAttributes.ToSubpacketArray())
				{
					userAttributeSubpacket.Encode(memoryStream);
				}
				this.UpdateWithIdData(209, memoryStream.ToArray());
			}
			catch (IOException exception)
			{
				throw new PgpException("cannot encode subpacket array", exception);
			}
			this.Update(this.sigPck.GetSignatureTrailer());
			return this.sig.VerifySignature(this.GetSignature());
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x00135920 File Offset: 0x00134920
		public bool VerifyCertification(string id, PgpPublicKey key)
		{
			this.UpdateWithPublicKey(key);
			this.UpdateWithIdData(180, Strings.ToByteArray(id));
			this.Update(this.sigPck.GetSignatureTrailer());
			return this.sig.VerifySignature(this.GetSignature());
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x0013595C File Offset: 0x0013495C
		public bool VerifyCertification(PgpPublicKey masterKey, PgpPublicKey pubKey)
		{
			this.UpdateWithPublicKey(masterKey);
			this.UpdateWithPublicKey(pubKey);
			this.Update(this.sigPck.GetSignatureTrailer());
			return this.sig.VerifySignature(this.GetSignature());
		}

		// Token: 0x060031A7 RID: 12711 RVA: 0x00135990 File Offset: 0x00134990
		public bool VerifyCertification(PgpPublicKey pubKey)
		{
			if (this.SignatureType != 32 && this.SignatureType != 40)
			{
				throw new InvalidOperationException("signature is not a key signature");
			}
			this.UpdateWithPublicKey(pubKey);
			this.Update(this.sigPck.GetSignatureTrailer());
			return this.sig.VerifySignature(this.GetSignature());
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x060031A8 RID: 12712 RVA: 0x001359E5 File Offset: 0x001349E5
		public int SignatureType
		{
			get
			{
				return this.sigPck.SignatureType;
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x060031A9 RID: 12713 RVA: 0x001359F2 File Offset: 0x001349F2
		public long KeyId
		{
			get
			{
				return this.sigPck.KeyId;
			}
		}

		// Token: 0x060031AA RID: 12714 RVA: 0x001359FF File Offset: 0x001349FF
		[Obsolete("Use 'CreationTime' property instead")]
		public DateTime GetCreationTime()
		{
			return this.CreationTime;
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x060031AB RID: 12715 RVA: 0x00135A07 File Offset: 0x00134A07
		public DateTime CreationTime
		{
			get
			{
				return DateTimeUtilities.UnixMsToDateTime(this.sigPck.CreationTime);
			}
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x00135A19 File Offset: 0x00134A19
		public byte[] GetSignatureTrailer()
		{
			return this.sigPck.GetSignatureTrailer();
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x060031AD RID: 12717 RVA: 0x00135A26 File Offset: 0x00134A26
		public bool HasSubpackets
		{
			get
			{
				return this.sigPck.GetHashedSubPackets() != null || this.sigPck.GetUnhashedSubPackets() != null;
			}
		}

		// Token: 0x060031AE RID: 12718 RVA: 0x00135A48 File Offset: 0x00134A48
		public PgpSignatureSubpacketVector GetHashedSubPackets()
		{
			return this.createSubpacketVector(this.sigPck.GetHashedSubPackets());
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x00135A5B File Offset: 0x00134A5B
		public PgpSignatureSubpacketVector GetUnhashedSubPackets()
		{
			return this.createSubpacketVector(this.sigPck.GetUnhashedSubPackets());
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x00135A6E File Offset: 0x00134A6E
		private PgpSignatureSubpacketVector createSubpacketVector(SignatureSubpacket[] pcks)
		{
			if (pcks != null)
			{
				return new PgpSignatureSubpacketVector(pcks);
			}
			return null;
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x00135A7C File Offset: 0x00134A7C
		public byte[] GetSignature()
		{
			MPInteger[] signature = this.sigPck.GetSignature();
			if (signature != null)
			{
				if (signature.Length == 1)
				{
					return signature[0].Value.ToByteArrayUnsigned();
				}
				try
				{
					return new DerSequence(new Asn1Encodable[]
					{
						new DerInteger(signature[0].Value),
						new DerInteger(signature[1].Value)
					}).GetEncoded();
				}
				catch (IOException exception)
				{
					throw new PgpException("exception encoding DSA sig.", exception);
				}
			}
			return this.sigPck.GetSignatureBytes();
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x00135B10 File Offset: 0x00134B10
		public byte[] GetEncoded()
		{
			MemoryStream memoryStream = new MemoryStream();
			this.Encode(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x00135B30 File Offset: 0x00134B30
		public void Encode(Stream outStream)
		{
			BcpgOutputStream bcpgOutputStream = BcpgOutputStream.Wrap(outStream);
			bcpgOutputStream.WritePacket(this.sigPck);
			if (this.trustPck != null)
			{
				bcpgOutputStream.WritePacket(this.trustPck);
			}
		}

		// Token: 0x060031B4 RID: 12724 RVA: 0x00135B64 File Offset: 0x00134B64
		private byte[] GetEncodedPublicKey(PgpPublicKey pubKey)
		{
			byte[] encodedContents;
			try
			{
				encodedContents = pubKey.publicPk.GetEncodedContents();
			}
			catch (IOException exception)
			{
				throw new PgpException("exception preparing key.", exception);
			}
			return encodedContents;
		}

		// Token: 0x04002221 RID: 8737
		public const int BinaryDocument = 0;

		// Token: 0x04002222 RID: 8738
		public const int CanonicalTextDocument = 1;

		// Token: 0x04002223 RID: 8739
		public const int StandAlone = 2;

		// Token: 0x04002224 RID: 8740
		public const int DefaultCertification = 16;

		// Token: 0x04002225 RID: 8741
		public const int NoCertification = 17;

		// Token: 0x04002226 RID: 8742
		public const int CasualCertification = 18;

		// Token: 0x04002227 RID: 8743
		public const int PositiveCertification = 19;

		// Token: 0x04002228 RID: 8744
		public const int SubkeyBinding = 24;

		// Token: 0x04002229 RID: 8745
		public const int PrimaryKeyBinding = 25;

		// Token: 0x0400222A RID: 8746
		public const int DirectKey = 31;

		// Token: 0x0400222B RID: 8747
		public const int KeyRevocation = 32;

		// Token: 0x0400222C RID: 8748
		public const int SubkeyRevocation = 40;

		// Token: 0x0400222D RID: 8749
		public const int CertificationRevocation = 48;

		// Token: 0x0400222E RID: 8750
		public const int Timestamp = 64;

		// Token: 0x0400222F RID: 8751
		private readonly SignaturePacket sigPck;

		// Token: 0x04002230 RID: 8752
		private readonly int signatureType;

		// Token: 0x04002231 RID: 8753
		private readonly TrustPacket trustPck;

		// Token: 0x04002232 RID: 8754
		private ISigner sig;

		// Token: 0x04002233 RID: 8755
		private byte lastb;
	}
}
