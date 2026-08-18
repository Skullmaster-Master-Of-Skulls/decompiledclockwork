using System;
using System.Collections;
using System.IO;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000235 RID: 565
	public class PgpObjectFactory
	{
		// Token: 0x06001616 RID: 5654 RVA: 0x00081818 File Offset: 0x00080818
		public PgpObjectFactory(Stream inputStream)
		{
			this.bcpgIn = BcpgInputStream.Wrap(inputStream);
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x0008182C File Offset: 0x0008082C
		public PgpObjectFactory(byte[] bytes) : this(new MemoryStream(bytes, false))
		{
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x0008183C File Offset: 0x0008083C
		public PgpObject NextPgpObject()
		{
			PacketTag packetTag = this.bcpgIn.NextPacketTag();
			if (packetTag == (PacketTag)(-1))
			{
				return null;
			}
			PacketTag packetTag2 = packetTag;
			switch (packetTag2)
			{
			case PacketTag.PublicKeyEncryptedSession:
			case PacketTag.SymmetricKeyEncryptedSessionKey:
				return new PgpEncryptedDataList(this.bcpgIn);
			case PacketTag.Signature:
			{
				ArrayList arrayList = new ArrayList();
				while (this.bcpgIn.NextPacketTag() == PacketTag.Signature)
				{
					try
					{
						arrayList.Add(new PgpSignature(this.bcpgIn));
					}
					catch (PgpException arg)
					{
						throw new IOException("can't create signature object: " + arg);
					}
				}
				return new PgpSignatureList((PgpSignature[])arrayList.ToArray(typeof(PgpSignature)));
			}
			case PacketTag.OnePassSignature:
			{
				ArrayList arrayList2 = new ArrayList();
				while (this.bcpgIn.NextPacketTag() == PacketTag.OnePassSignature)
				{
					try
					{
						arrayList2.Add(new PgpOnePassSignature(this.bcpgIn));
					}
					catch (PgpException arg2)
					{
						throw new IOException("can't create one pass signature object: " + arg2);
					}
				}
				return new PgpOnePassSignatureList((PgpOnePassSignature[])arrayList2.ToArray(typeof(PgpOnePassSignature)));
			}
			case PacketTag.SecretKey:
				try
				{
					return new PgpSecretKeyRing(this.bcpgIn);
				}
				catch (PgpException arg3)
				{
					throw new IOException("can't create secret key object: " + arg3);
				}
				break;
			case PacketTag.PublicKey:
				break;
			case PacketTag.SecretSubkey:
			case PacketTag.SymmetricKeyEncrypted:
				goto IL_188;
			case PacketTag.CompressedData:
				return new PgpCompressedData(this.bcpgIn);
			case PacketTag.Marker:
				return new PgpMarker(this.bcpgIn);
			case PacketTag.LiteralData:
				return new PgpLiteralData(this.bcpgIn);
			default:
				switch (packetTag2)
				{
				case PacketTag.Experimental1:
				case PacketTag.Experimental2:
				case PacketTag.Experimental3:
				case PacketTag.Experimental4:
					return new PgpExperimental(this.bcpgIn);
				default:
					goto IL_188;
				}
				break;
			}
			return new PgpPublicKeyRing(this.bcpgIn);
			IL_188:
			throw new IOException("unknown object in stream " + this.bcpgIn.NextPacketTag());
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x00081A1C File Offset: 0x00080A1C
		[Obsolete("Use NextPgpObject() instead")]
		public object NextObject()
		{
			return this.NextPgpObject();
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x00081A24 File Offset: 0x00080A24
		public IList AllPgpObjects()
		{
			ArrayList arrayList = new ArrayList();
			PgpObject value;
			while ((value = this.NextPgpObject()) != null)
			{
				arrayList.Add(value);
			}
			return arrayList;
		}

		// Token: 0x04000F3F RID: 3903
		private readonly BcpgInputStream bcpgIn;
	}
}
