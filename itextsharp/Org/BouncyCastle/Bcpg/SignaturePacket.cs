using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Bcpg.Sig;
using Org.BouncyCastle.Utilities.Date;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x0200061F RID: 1567
	public class SignaturePacket : ContainedPacket
	{
		// Token: 0x06003540 RID: 13632 RVA: 0x0014A91C File Offset: 0x0014991C
		internal SignaturePacket(BcpgInputStream bcpgIn)
		{
			this.version = bcpgIn.ReadByte();
			if (this.version == 3 || this.version == 2)
			{
				bcpgIn.ReadByte();
				this.signatureType = bcpgIn.ReadByte();
				this.creationTime = ((long)bcpgIn.ReadByte() << 24 | (long)bcpgIn.ReadByte() << 16 | (long)bcpgIn.ReadByte() << 8 | (long)((ulong)bcpgIn.ReadByte())) * 1000L;
				this.keyId |= (long)bcpgIn.ReadByte() << 56;
				this.keyId |= (long)bcpgIn.ReadByte() << 48;
				this.keyId |= (long)bcpgIn.ReadByte() << 40;
				this.keyId |= (long)bcpgIn.ReadByte() << 32;
				this.keyId |= (long)bcpgIn.ReadByte() << 24;
				this.keyId |= (long)bcpgIn.ReadByte() << 16;
				this.keyId |= (long)bcpgIn.ReadByte() << 8;
				this.keyId |= (long)((ulong)bcpgIn.ReadByte());
				this.keyAlgorithm = (PublicKeyAlgorithmTag)bcpgIn.ReadByte();
				this.hashAlgorithm = (HashAlgorithmTag)bcpgIn.ReadByte();
			}
			else
			{
				if (this.version != 4)
				{
					throw new Exception("unsupported version: " + this.version);
				}
				this.signatureType = bcpgIn.ReadByte();
				this.keyAlgorithm = (PublicKeyAlgorithmTag)bcpgIn.ReadByte();
				this.hashAlgorithm = (HashAlgorithmTag)bcpgIn.ReadByte();
				int num = bcpgIn.ReadByte() << 8 | bcpgIn.ReadByte();
				byte[] buffer = new byte[num];
				bcpgIn.ReadFully(buffer);
				SignatureSubpacketsParser signatureSubpacketsParser = new SignatureSubpacketsParser(new MemoryStream(buffer, false));
				ArrayList arrayList = new ArrayList();
				SignatureSubpacket value;
				while ((value = signatureSubpacketsParser.ReadPacket()) != null)
				{
					arrayList.Add(value);
				}
				this.hashedData = new SignatureSubpacket[arrayList.Count];
				for (int num2 = 0; num2 != this.hashedData.Length; num2++)
				{
					SignatureSubpacket signatureSubpacket = (SignatureSubpacket)arrayList[num2];
					if (signatureSubpacket is IssuerKeyId)
					{
						this.keyId = ((IssuerKeyId)signatureSubpacket).KeyId;
					}
					else if (signatureSubpacket is SignatureCreationTime)
					{
						this.creationTime = DateTimeUtilities.DateTimeToUnixMs(((SignatureCreationTime)signatureSubpacket).GetTime());
					}
					this.hashedData[num2] = signatureSubpacket;
				}
				int num3 = bcpgIn.ReadByte() << 8 | bcpgIn.ReadByte();
				byte[] buffer2 = new byte[num3];
				bcpgIn.ReadFully(buffer2);
				signatureSubpacketsParser = new SignatureSubpacketsParser(new MemoryStream(buffer2, false));
				arrayList.Clear();
				while ((value = signatureSubpacketsParser.ReadPacket()) != null)
				{
					arrayList.Add(value);
				}
				this.unhashedData = new SignatureSubpacket[arrayList.Count];
				for (int num4 = 0; num4 != this.unhashedData.Length; num4++)
				{
					SignatureSubpacket signatureSubpacket2 = (SignatureSubpacket)arrayList[num4];
					if (signatureSubpacket2 is IssuerKeyId)
					{
						this.keyId = ((IssuerKeyId)signatureSubpacket2).KeyId;
					}
					this.unhashedData[num4] = signatureSubpacket2;
				}
			}
			this.fingerprint = new byte[2];
			bcpgIn.ReadFully(this.fingerprint);
			PublicKeyAlgorithmTag publicKeyAlgorithmTag = this.keyAlgorithm;
			switch (publicKeyAlgorithmTag)
			{
			case PublicKeyAlgorithmTag.RsaGeneral:
			case PublicKeyAlgorithmTag.RsaSign:
			{
				MPInteger mpinteger = new MPInteger(bcpgIn);
				this.signature = new MPInteger[]
				{
					mpinteger
				};
				return;
			}
			case PublicKeyAlgorithmTag.RsaEncrypt:
				break;
			default:
				switch (publicKeyAlgorithmTag)
				{
				case PublicKeyAlgorithmTag.ElGamalEncrypt:
				case PublicKeyAlgorithmTag.ElGamalGeneral:
				{
					MPInteger mpinteger2 = new MPInteger(bcpgIn);
					MPInteger mpinteger3 = new MPInteger(bcpgIn);
					MPInteger mpinteger4 = new MPInteger(bcpgIn);
					this.signature = new MPInteger[]
					{
						mpinteger2,
						mpinteger3,
						mpinteger4
					};
					return;
				}
				case PublicKeyAlgorithmTag.Dsa:
				{
					MPInteger mpinteger5 = new MPInteger(bcpgIn);
					MPInteger mpinteger6 = new MPInteger(bcpgIn);
					this.signature = new MPInteger[]
					{
						mpinteger5,
						mpinteger6
					};
					return;
				}
				}
				break;
			}
			if (this.keyAlgorithm >= PublicKeyAlgorithmTag.Experimental_1 && this.keyAlgorithm <= PublicKeyAlgorithmTag.Experimental_11)
			{
				this.signature = null;
				MemoryStream memoryStream = new MemoryStream();
				int num5;
				while ((num5 = bcpgIn.ReadByte()) >= 0)
				{
					memoryStream.WriteByte((byte)num5);
				}
				this.signatureEncoding = memoryStream.ToArray();
				return;
			}
			throw new IOException("unknown signature key algorithm: " + this.keyAlgorithm);
		}

		// Token: 0x06003541 RID: 13633 RVA: 0x0014AD68 File Offset: 0x00149D68
		public SignaturePacket(int signatureType, long keyId, PublicKeyAlgorithmTag keyAlgorithm, HashAlgorithmTag hashAlgorithm, SignatureSubpacket[] hashedData, SignatureSubpacket[] unhashedData, byte[] fingerprint, MPInteger[] signature) : this(4, signatureType, keyId, keyAlgorithm, hashAlgorithm, hashedData, unhashedData, fingerprint, signature)
		{
		}

		// Token: 0x06003542 RID: 13634 RVA: 0x0014AD8C File Offset: 0x00149D8C
		public SignaturePacket(int version, int signatureType, long keyId, PublicKeyAlgorithmTag keyAlgorithm, HashAlgorithmTag hashAlgorithm, long creationTime, byte[] fingerprint, MPInteger[] signature) : this(version, signatureType, keyId, keyAlgorithm, hashAlgorithm, null, null, fingerprint, signature)
		{
			this.creationTime = creationTime;
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x0014ADB4 File Offset: 0x00149DB4
		public SignaturePacket(int version, int signatureType, long keyId, PublicKeyAlgorithmTag keyAlgorithm, HashAlgorithmTag hashAlgorithm, SignatureSubpacket[] hashedData, SignatureSubpacket[] unhashedData, byte[] fingerprint, MPInteger[] signature)
		{
			this.version = version;
			this.signatureType = signatureType;
			this.keyId = keyId;
			this.keyAlgorithm = keyAlgorithm;
			this.hashAlgorithm = hashAlgorithm;
			this.hashedData = hashedData;
			this.unhashedData = unhashedData;
			this.fingerprint = fingerprint;
			this.signature = signature;
			if (hashedData != null)
			{
				this.setCreationTime();
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06003544 RID: 13636 RVA: 0x0014AE16 File Offset: 0x00149E16
		public int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06003545 RID: 13637 RVA: 0x0014AE1E File Offset: 0x00149E1E
		public int SignatureType
		{
			get
			{
				return this.signatureType;
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06003546 RID: 13638 RVA: 0x0014AE26 File Offset: 0x00149E26
		public long KeyId
		{
			get
			{
				return this.keyId;
			}
		}

		// Token: 0x06003547 RID: 13639 RVA: 0x0014AE30 File Offset: 0x00149E30
		public byte[] GetSignatureTrailer()
		{
			byte[] array;
			if (this.version == 3)
			{
				array = new byte[5];
				long num = this.creationTime / 1000L;
				array[0] = (byte)this.signatureType;
				array[1] = (byte)(num >> 24);
				array[2] = (byte)(num >> 16);
				array[3] = (byte)(num >> 8);
				array[4] = (byte)num;
			}
			else
			{
				MemoryStream memoryStream = new MemoryStream();
				memoryStream.WriteByte((byte)this.Version);
				memoryStream.WriteByte((byte)this.SignatureType);
				memoryStream.WriteByte((byte)this.KeyAlgorithm);
				memoryStream.WriteByte((byte)this.HashAlgorithm);
				MemoryStream memoryStream2 = new MemoryStream();
				SignatureSubpacket[] hashedSubPackets = this.GetHashedSubPackets();
				for (int num2 = 0; num2 != hashedSubPackets.Length; num2++)
				{
					hashedSubPackets[num2].Encode(memoryStream2);
				}
				byte[] array2 = memoryStream2.ToArray();
				memoryStream.WriteByte((byte)(array2.Length >> 8));
				memoryStream.WriteByte((byte)array2.Length);
				memoryStream.Write(array2, 0, array2.Length);
				byte[] array3 = memoryStream.ToArray();
				memoryStream.WriteByte((byte)this.Version);
				memoryStream.WriteByte(byte.MaxValue);
				memoryStream.WriteByte((byte)(array3.Length >> 24));
				memoryStream.WriteByte((byte)(array3.Length >> 16));
				memoryStream.WriteByte((byte)(array3.Length >> 8));
				memoryStream.WriteByte((byte)array3.Length);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06003548 RID: 13640 RVA: 0x0014AF77 File Offset: 0x00149F77
		public PublicKeyAlgorithmTag KeyAlgorithm
		{
			get
			{
				return this.keyAlgorithm;
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06003549 RID: 13641 RVA: 0x0014AF7F File Offset: 0x00149F7F
		public HashAlgorithmTag HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x0600354A RID: 13642 RVA: 0x0014AF87 File Offset: 0x00149F87
		public MPInteger[] GetSignature()
		{
			return this.signature;
		}

		// Token: 0x0600354B RID: 13643 RVA: 0x0014AF90 File Offset: 0x00149F90
		public byte[] GetSignatureBytes()
		{
			if (this.signatureEncoding != null)
			{
				return (byte[])this.signatureEncoding.Clone();
			}
			MemoryStream memoryStream = new MemoryStream();
			BcpgOutputStream bcpgOutputStream = new BcpgOutputStream(memoryStream);
			foreach (MPInteger bcpgObject in this.signature)
			{
				try
				{
					bcpgOutputStream.WriteObject(bcpgObject);
				}
				catch (IOException arg)
				{
					throw new Exception("internal error: " + arg);
				}
			}
			return memoryStream.ToArray();
		}

		// Token: 0x0600354C RID: 13644 RVA: 0x0014B018 File Offset: 0x0014A018
		public SignatureSubpacket[] GetHashedSubPackets()
		{
			return this.hashedData;
		}

		// Token: 0x0600354D RID: 13645 RVA: 0x0014B020 File Offset: 0x0014A020
		public SignatureSubpacket[] GetUnhashedSubPackets()
		{
			return this.unhashedData;
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x0600354E RID: 13646 RVA: 0x0014B028 File Offset: 0x0014A028
		public long CreationTime
		{
			get
			{
				return this.creationTime;
			}
		}

		// Token: 0x0600354F RID: 13647 RVA: 0x0014B030 File Offset: 0x0014A030
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			MemoryStream memoryStream = new MemoryStream();
			BcpgOutputStream bcpgOutputStream = new BcpgOutputStream(memoryStream);
			bcpgOutputStream.WriteByte((byte)this.version);
			if (this.version == 3 || this.version == 2)
			{
				bcpgOutputStream.Write(new byte[]
				{
					5,
					(byte)this.signatureType
				});
				bcpgOutputStream.WriteInt((int)(this.creationTime / 1000L));
				bcpgOutputStream.WriteLong(this.keyId);
				bcpgOutputStream.Write(new byte[]
				{
					(byte)this.keyAlgorithm,
					(byte)this.hashAlgorithm
				});
			}
			else
			{
				if (this.version != 4)
				{
					throw new IOException("unknown version: " + this.version);
				}
				bcpgOutputStream.Write(new byte[]
				{
					(byte)this.signatureType,
					(byte)this.keyAlgorithm,
					(byte)this.hashAlgorithm
				});
				SignaturePacket.EncodeLengthAndData(bcpgOutputStream, SignaturePacket.GetEncodedSubpackets(this.hashedData));
				SignaturePacket.EncodeLengthAndData(bcpgOutputStream, SignaturePacket.GetEncodedSubpackets(this.unhashedData));
			}
			bcpgOutputStream.Write(this.fingerprint);
			if (this.signature != null)
			{
				bcpgOutputStream.WriteObjects(this.signature);
			}
			else
			{
				bcpgOutputStream.Write(this.signatureEncoding);
			}
			bcpgOut.WritePacket(PacketTag.Signature, memoryStream.ToArray(), true);
		}

		// Token: 0x06003550 RID: 13648 RVA: 0x0014B17E File Offset: 0x0014A17E
		private static void EncodeLengthAndData(BcpgOutputStream pOut, byte[] data)
		{
			pOut.WriteShort((short)data.Length);
			pOut.Write(data);
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x0014B194 File Offset: 0x0014A194
		private static byte[] GetEncodedSubpackets(SignatureSubpacket[] ps)
		{
			MemoryStream memoryStream = new MemoryStream();
			foreach (SignatureSubpacket signatureSubpacket in ps)
			{
				signatureSubpacket.Encode(memoryStream);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x0014B1C8 File Offset: 0x0014A1C8
		private void setCreationTime()
		{
			foreach (SignatureSubpacket signatureSubpacket in this.hashedData)
			{
				if (signatureSubpacket is SignatureCreationTime)
				{
					this.creationTime = DateTimeUtilities.DateTimeToUnixMs(((SignatureCreationTime)signatureSubpacket).GetTime());
					return;
				}
			}
		}

		// Token: 0x04002398 RID: 9112
		private int version;

		// Token: 0x04002399 RID: 9113
		private int signatureType;

		// Token: 0x0400239A RID: 9114
		private long creationTime;

		// Token: 0x0400239B RID: 9115
		private long keyId;

		// Token: 0x0400239C RID: 9116
		private PublicKeyAlgorithmTag keyAlgorithm;

		// Token: 0x0400239D RID: 9117
		private HashAlgorithmTag hashAlgorithm;

		// Token: 0x0400239E RID: 9118
		private MPInteger[] signature;

		// Token: 0x0400239F RID: 9119
		private byte[] fingerprint;

		// Token: 0x040023A0 RID: 9120
		private SignatureSubpacket[] hashedData;

		// Token: 0x040023A1 RID: 9121
		private SignatureSubpacket[] unhashedData;

		// Token: 0x040023A2 RID: 9122
		private byte[] signatureEncoding;
	}
}
