using System;
using System.IO;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x0200019F RID: 415
	public class BcpgInputStream : BaseInputStream
	{
		// Token: 0x06000FFD RID: 4093 RVA: 0x0005C7D2 File Offset: 0x0005B7D2
		internal static BcpgInputStream Wrap(Stream inStr)
		{
			if (inStr is BcpgInputStream)
			{
				return (BcpgInputStream)inStr;
			}
			return new BcpgInputStream(inStr);
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x0005C7E9 File Offset: 0x0005B7E9
		private BcpgInputStream(Stream inputStream)
		{
			this.m_in = inputStream;
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x0005C7F8 File Offset: 0x0005B7F8
		public override int ReadByte()
		{
			if (this.next)
			{
				this.next = false;
				return this.nextB;
			}
			return this.m_in.ReadByte();
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0005C81C File Offset: 0x0005B81C
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (!this.next)
			{
				return this.m_in.Read(buffer, offset, count);
			}
			if (this.nextB < 0)
			{
				return 0;
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			buffer[offset] = (byte)this.nextB;
			this.next = false;
			return 1;
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0005C86B File Offset: 0x0005B86B
		public byte[] ReadAll()
		{
			return Streams.ReadAll(this);
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x0005C873 File Offset: 0x0005B873
		public void ReadFully(byte[] buffer, int off, int len)
		{
			if (Streams.ReadFully(this, buffer, off, len) < len)
			{
				throw new EndOfStreamException();
			}
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0005C887 File Offset: 0x0005B887
		public void ReadFully(byte[] buffer)
		{
			this.ReadFully(buffer, 0, buffer.Length);
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0005C894 File Offset: 0x0005B894
		public PacketTag NextPacketTag()
		{
			if (!this.next)
			{
				try
				{
					this.nextB = this.m_in.ReadByte();
				}
				catch (EndOfStreamException)
				{
					this.nextB = -1;
				}
				this.next = true;
			}
			if (this.nextB < 0)
			{
				return (PacketTag)this.nextB;
			}
			if ((this.nextB & 64) != 0)
			{
				return (PacketTag)(this.nextB & 63);
			}
			return (PacketTag)((this.nextB & 63) >> 2);
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0005C910 File Offset: 0x0005B910
		public Packet ReadPacket()
		{
			int num = this.ReadByte();
			if (num < 0)
			{
				return null;
			}
			if ((num & 128) == 0)
			{
				throw new IOException("invalid header encountered");
			}
			bool flag = (num & 64) != 0;
			int num2 = 0;
			bool flag2 = false;
			PacketTag packetTag;
			if (flag)
			{
				packetTag = (PacketTag)(num & 63);
				int num3 = this.ReadByte();
				if (num3 < 192)
				{
					num2 = num3;
				}
				else if (num3 <= 223)
				{
					int num4 = this.m_in.ReadByte();
					num2 = (num3 - 192 << 8) + num4 + 192;
				}
				else if (num3 == 255)
				{
					num2 = (this.m_in.ReadByte() << 24 | this.m_in.ReadByte() << 16 | this.m_in.ReadByte() << 8 | this.m_in.ReadByte());
				}
				else
				{
					flag2 = true;
					num2 = 1 << num3;
				}
			}
			else
			{
				int num5 = num & 3;
				packetTag = (PacketTag)((num & 63) >> 2);
				switch (num5)
				{
				case 0:
					num2 = this.ReadByte();
					break;
				case 1:
					num2 = (this.ReadByte() << 8 | this.ReadByte());
					break;
				case 2:
					num2 = (this.ReadByte() << 24 | this.ReadByte() << 16 | this.ReadByte() << 8 | this.ReadByte());
					break;
				case 3:
					flag2 = true;
					break;
				default:
					throw new IOException("unknown length type encountered");
				}
			}
			BcpgInputStream bcpgIn;
			if (num2 == 0 && flag2)
			{
				bcpgIn = this;
			}
			else
			{
				BcpgInputStream.PartialInputStream inputStream = new BcpgInputStream.PartialInputStream(this, flag2, num2);
				bcpgIn = new BcpgInputStream(inputStream);
			}
			PacketTag packetTag2 = packetTag;
			switch (packetTag2)
			{
			case PacketTag.Reserved:
				return new InputStreamPacket(bcpgIn);
			case PacketTag.PublicKeyEncryptedSession:
				return new PublicKeyEncSessionPacket(bcpgIn);
			case PacketTag.Signature:
				return new SignaturePacket(bcpgIn);
			case PacketTag.SymmetricKeyEncryptedSessionKey:
				return new SymmetricKeyEncSessionPacket(bcpgIn);
			case PacketTag.OnePassSignature:
				return new OnePassSignaturePacket(bcpgIn);
			case PacketTag.SecretKey:
				return new SecretKeyPacket(bcpgIn);
			case PacketTag.PublicKey:
				return new PublicKeyPacket(bcpgIn);
			case PacketTag.SecretSubkey:
				return new SecretSubkeyPacket(bcpgIn);
			case PacketTag.CompressedData:
				return new CompressedDataPacket(bcpgIn);
			case PacketTag.SymmetricKeyEncrypted:
				return new SymmetricEncDataPacket(bcpgIn);
			case PacketTag.Marker:
				return new MarkerPacket(bcpgIn);
			case PacketTag.LiteralData:
				return new LiteralDataPacket(bcpgIn);
			case PacketTag.Trust:
				return new TrustPacket(bcpgIn);
			case PacketTag.UserId:
				return new UserIdPacket(bcpgIn);
			case PacketTag.PublicSubkey:
				return new PublicSubkeyPacket(bcpgIn);
			case (PacketTag)15:
			case (PacketTag)16:
				break;
			case PacketTag.UserAttribute:
				return new UserAttributePacket(bcpgIn);
			case PacketTag.SymmetricEncryptedIntegrityProtected:
				return new SymmetricEncIntegrityPacket(bcpgIn);
			case PacketTag.ModificationDetectionCode:
				return new ModDetectionCodePacket(bcpgIn);
			default:
				switch (packetTag2)
				{
				case PacketTag.Experimental1:
				case PacketTag.Experimental2:
				case PacketTag.Experimental3:
				case PacketTag.Experimental4:
					return new ExperimentalPacket(packetTag, bcpgIn);
				}
				break;
			}
			throw new IOException("unknown packet type encountered: " + packetTag);
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0005CBBA File Offset: 0x0005BBBA
		public override void Close()
		{
			this.m_in.Close();
			base.Close();
		}

		// Token: 0x04000B92 RID: 2962
		private Stream m_in;

		// Token: 0x04000B93 RID: 2963
		private bool next;

		// Token: 0x04000B94 RID: 2964
		private int nextB;

		// Token: 0x020001A0 RID: 416
		private class PartialInputStream : BaseInputStream
		{
			// Token: 0x06001007 RID: 4103 RVA: 0x0005CBCD File Offset: 0x0005BBCD
			internal PartialInputStream(BcpgInputStream bcpgIn, bool partial, int dataLength)
			{
				this.m_in = bcpgIn;
				this.partial = partial;
				this.dataLength = dataLength;
			}

			// Token: 0x06001008 RID: 4104 RVA: 0x0005CBEC File Offset: 0x0005BBEC
			public override int ReadByte()
			{
				while (this.dataLength == 0)
				{
					if (!this.partial || this.ReadPartialDataLength() < 0)
					{
						return -1;
					}
				}
				int num = this.m_in.ReadByte();
				if (num < 0)
				{
					throw new EndOfStreamException("Premature end of stream in PartialInputStream");
				}
				this.dataLength--;
				return num;
			}

			// Token: 0x06001009 RID: 4105 RVA: 0x0005CC40 File Offset: 0x0005BC40
			public override int Read(byte[] buffer, int offset, int count)
			{
				while (this.dataLength == 0)
				{
					if (!this.partial || this.ReadPartialDataLength() < 0)
					{
						return 0;
					}
				}
				int count2 = (this.dataLength > count || this.dataLength < 0) ? count : this.dataLength;
				int num = this.m_in.Read(buffer, offset, count2);
				if (num < 1)
				{
					throw new EndOfStreamException("Premature end of stream in PartialInputStream");
				}
				this.dataLength -= num;
				return num;
			}

			// Token: 0x0600100A RID: 4106 RVA: 0x0005CCB4 File Offset: 0x0005BCB4
			private int ReadPartialDataLength()
			{
				int num = this.m_in.ReadByte();
				if (num < 0)
				{
					return -1;
				}
				this.partial = false;
				if (num < 192)
				{
					this.dataLength = num;
				}
				else if (num <= 223)
				{
					this.dataLength = (num - 192 << 8) + this.m_in.ReadByte() + 192;
				}
				else if (num == 255)
				{
					this.dataLength = (this.m_in.ReadByte() << 24 | this.m_in.ReadByte() << 16 | this.m_in.ReadByte() << 8 | this.m_in.ReadByte());
				}
				else
				{
					this.partial = true;
					this.dataLength = 1 << num;
				}
				return 0;
			}

			// Token: 0x04000B95 RID: 2965
			private BcpgInputStream m_in;

			// Token: 0x04000B96 RID: 2966
			private bool partial;

			// Token: 0x04000B97 RID: 2967
			private int dataLength;
		}
	}
}
