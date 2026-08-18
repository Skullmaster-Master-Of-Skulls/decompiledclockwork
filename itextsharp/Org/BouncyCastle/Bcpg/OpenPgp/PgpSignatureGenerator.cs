using System;
using System.IO;
using Org.BouncyCastle.Bcpg.Sig;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x020004B6 RID: 1206
	public class PgpSignatureGenerator
	{
		// Token: 0x060028C7 RID: 10439 RVA: 0x000F7E94 File Offset: 0x000F6E94
		public PgpSignatureGenerator(PublicKeyAlgorithmTag keyAlgorithm, HashAlgorithmTag hashAlgorithm)
		{
			this.keyAlgorithm = keyAlgorithm;
			this.hashAlgorithm = hashAlgorithm;
			this.dig = DigestUtilities.GetDigest(PgpUtilities.GetDigestName(hashAlgorithm));
			this.sig = SignerUtilities.GetSigner(PgpUtilities.GetSignatureName(keyAlgorithm, hashAlgorithm));
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x000F7EEE File Offset: 0x000F6EEE
		public void InitSign(int sigType, PgpPrivateKey key)
		{
			this.InitSign(sigType, key, null);
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x000F7EFC File Offset: 0x000F6EFC
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

		// Token: 0x060028CA RID: 10442 RVA: 0x000F7F6C File Offset: 0x000F6F6C
		public void Update(byte b)
		{
			if (this.signatureType == 1)
			{
				this.doCanonicalUpdateByte(b);
				return;
			}
			this.doUpdateByte(b);
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x000F7F86 File Offset: 0x000F6F86
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

		// Token: 0x060028CC RID: 10444 RVA: 0x000F7FBA File Offset: 0x000F6FBA
		private void doUpdateCRLF()
		{
			this.doUpdateByte(13);
			this.doUpdateByte(10);
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x000F7FCC File Offset: 0x000F6FCC
		private void doUpdateByte(byte b)
		{
			this.sig.Update(b);
			this.dig.Update(b);
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x000F7FE6 File Offset: 0x000F6FE6
		public void Update(params byte[] b)
		{
			this.Update(b, 0, b.Length);
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x000F7FF4 File Offset: 0x000F6FF4
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

		// Token: 0x060028D0 RID: 10448 RVA: 0x000F8040 File Offset: 0x000F7040
		public void SetHashedSubpackets(PgpSignatureSubpacketVector hashedPackets)
		{
			this.hashed = ((hashedPackets == null) ? PgpSignatureGenerator.EmptySignatureSubpackets : hashedPackets.ToSubpacketArray());
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x000F8058 File Offset: 0x000F7058
		public void SetUnhashedSubpackets(PgpSignatureSubpacketVector unhashedPackets)
		{
			this.unhashed = ((unhashedPackets == null) ? PgpSignatureGenerator.EmptySignatureSubpackets : unhashedPackets.ToSubpacketArray());
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x000F8070 File Offset: 0x000F7070
		public PgpOnePassSignature GenerateOnePassVersion(bool isNested)
		{
			return new PgpOnePassSignature(new OnePassSignaturePacket(this.signatureType, this.hashAlgorithm, this.keyAlgorithm, this.privKey.KeyId, isNested));
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x000F809C File Offset: 0x000F709C
		public PgpSignature Generate()
		{
			SignatureSubpacket[] array = this.hashed;
			SignatureSubpacket[] array2 = this.unhashed;
			if (!this.packetPresent(this.hashed, SignatureSubpacketTag.CreationTime))
			{
				array = this.insertSubpacket(array, new SignatureCreationTime(false, DateTime.UtcNow));
			}
			if (!this.packetPresent(this.hashed, SignatureSubpacketTag.IssuerKeyId) && !this.packetPresent(this.unhashed, SignatureSubpacketTag.IssuerKeyId))
			{
				array2 = this.insertSubpacket(array2, new IssuerKeyId(false, this.privKey.KeyId));
			}
			int num = 4;
			byte[] array4;
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				for (int num2 = 0; num2 != array.Length; num2++)
				{
					array[num2].Encode(memoryStream);
				}
				byte[] array3 = memoryStream.ToArray();
				MemoryStream memoryStream2 = new MemoryStream(array3.Length + 6);
				memoryStream2.WriteByte((byte)num);
				memoryStream2.WriteByte((byte)this.signatureType);
				memoryStream2.WriteByte((byte)this.keyAlgorithm);
				memoryStream2.WriteByte((byte)this.hashAlgorithm);
				memoryStream2.WriteByte((byte)(array3.Length >> 8));
				memoryStream2.WriteByte((byte)array3.Length);
				memoryStream2.Write(array3, 0, array3.Length);
				array4 = memoryStream2.ToArray();
			}
			catch (IOException exception)
			{
				throw new PgpException("exception encoding hashed data.", exception);
			}
			this.sig.BlockUpdate(array4, 0, array4.Length);
			this.dig.BlockUpdate(array4, 0, array4.Length);
			array4 = new byte[]
			{
				(byte)num,
				byte.MaxValue,
				(byte)(array4.Length >> 24),
				(byte)(array4.Length >> 16),
				(byte)(array4.Length >> 8),
				(byte)array4.Length
			};
			this.sig.BlockUpdate(array4, 0, array4.Length);
			this.dig.BlockUpdate(array4, 0, array4.Length);
			byte[] encoding = this.sig.GenerateSignature();
			byte[] array5 = DigestUtilities.DoFinal(this.dig);
			byte[] fingerprint = new byte[]
			{
				array5[0],
				array5[1]
			};
			MPInteger[] signature = (this.keyAlgorithm == PublicKeyAlgorithmTag.RsaSign || this.keyAlgorithm == PublicKeyAlgorithmTag.RsaGeneral) ? PgpUtilities.RsaSigToMpi(encoding) : PgpUtilities.DsaSigToMpi(encoding);
			return new PgpSignature(new SignaturePacket(this.signatureType, this.privKey.KeyId, this.keyAlgorithm, this.hashAlgorithm, array, array2, fingerprint, signature));
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x000F82E4 File Offset: 0x000F72E4
		public PgpSignature GenerateCertification(string id, PgpPublicKey pubKey)
		{
			this.UpdateWithPublicKey(pubKey);
			this.UpdateWithIdData(180, Strings.ToByteArray(id));
			return this.Generate();
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x000F8304 File Offset: 0x000F7304
		public PgpSignature GenerateCertification(PgpUserAttributeSubpacketVector userAttributes, PgpPublicKey pubKey)
		{
			this.UpdateWithPublicKey(pubKey);
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
			return this.Generate();
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x000F837C File Offset: 0x000F737C
		public PgpSignature GenerateCertification(PgpPublicKey masterKey, PgpPublicKey pubKey)
		{
			this.UpdateWithPublicKey(masterKey);
			this.UpdateWithPublicKey(pubKey);
			return this.Generate();
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x000F8392 File Offset: 0x000F7392
		public PgpSignature GenerateCertification(PgpPublicKey pubKey)
		{
			this.UpdateWithPublicKey(pubKey);
			return this.Generate();
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x000F83A4 File Offset: 0x000F73A4
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

		// Token: 0x060028D9 RID: 10457 RVA: 0x000F83E0 File Offset: 0x000F73E0
		private bool packetPresent(SignatureSubpacket[] packets, SignatureSubpacketTag type)
		{
			for (int num = 0; num != packets.Length; num++)
			{
				if (packets[num].SubpacketType == type)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x000F840C File Offset: 0x000F740C
		private SignatureSubpacket[] insertSubpacket(SignatureSubpacket[] packets, SignatureSubpacket subpacket)
		{
			SignatureSubpacket[] array = new SignatureSubpacket[packets.Length + 1];
			array[0] = subpacket;
			packets.CopyTo(array, 1);
			return array;
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x000F8434 File Offset: 0x000F7434
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

		// Token: 0x060028DC RID: 10460 RVA: 0x000F8480 File Offset: 0x000F7480
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

		// Token: 0x04001CBB RID: 7355
		private static readonly SignatureSubpacket[] EmptySignatureSubpackets = new SignatureSubpacket[0];

		// Token: 0x04001CBC RID: 7356
		private PublicKeyAlgorithmTag keyAlgorithm;

		// Token: 0x04001CBD RID: 7357
		private HashAlgorithmTag hashAlgorithm;

		// Token: 0x04001CBE RID: 7358
		private PgpPrivateKey privKey;

		// Token: 0x04001CBF RID: 7359
		private ISigner sig;

		// Token: 0x04001CC0 RID: 7360
		private IDigest dig;

		// Token: 0x04001CC1 RID: 7361
		private int signatureType;

		// Token: 0x04001CC2 RID: 7362
		private byte lastb;

		// Token: 0x04001CC3 RID: 7363
		private SignatureSubpacket[] unhashed = PgpSignatureGenerator.EmptySignatureSubpackets;

		// Token: 0x04001CC4 RID: 7364
		private SignatureSubpacket[] hashed = PgpSignatureGenerator.EmptySignatureSubpackets;
	}
}
