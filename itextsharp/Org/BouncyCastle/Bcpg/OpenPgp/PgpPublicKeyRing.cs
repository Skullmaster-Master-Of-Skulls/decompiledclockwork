using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x020004B7 RID: 1207
	public class PgpPublicKeyRing : PgpKeyRing
	{
		// Token: 0x060028DE RID: 10462 RVA: 0x000F84CF File Offset: 0x000F74CF
		public PgpPublicKeyRing(byte[] encoding) : this(new MemoryStream(encoding, false))
		{
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x000F84DE File Offset: 0x000F74DE
		internal PgpPublicKeyRing(ArrayList pubKeys)
		{
			this.keys = pubKeys;
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x000F84F0 File Offset: 0x000F74F0
		public PgpPublicKeyRing(Stream inputStream)
		{
			this.keys = new ArrayList();
			BcpgInputStream bcpgInputStream = BcpgInputStream.Wrap(inputStream);
			PacketTag packetTag = bcpgInputStream.NextPacketTag();
			if (packetTag != PacketTag.PublicKey && packetTag != PacketTag.PublicSubkey)
			{
				string str = "public key ring doesn't start with public key tag: tag 0x";
				int num = (int)packetTag;
				throw new IOException(str + num.ToString("X"));
			}
			PublicKeyPacket publicPk = (PublicKeyPacket)bcpgInputStream.ReadPacket();
			TrustPacket trustPk = PgpKeyRing.ReadOptionalTrustPacket(bcpgInputStream);
			ArrayList keySigs = PgpKeyRing.ReadSignaturesAndTrust(bcpgInputStream);
			ArrayList ids;
			ArrayList idTrusts;
			ArrayList idSigs;
			PgpKeyRing.ReadUserIDs(bcpgInputStream, out ids, out idTrusts, out idSigs);
			this.keys.Add(new PgpPublicKey(publicPk, trustPk, keySigs, ids, idTrusts, idSigs));
			while (bcpgInputStream.NextPacketTag() == PacketTag.PublicSubkey)
			{
				PublicKeyPacket publicPk2 = (PublicKeyPacket)bcpgInputStream.ReadPacket();
				TrustPacket trustPk2 = PgpKeyRing.ReadOptionalTrustPacket(bcpgInputStream);
				ArrayList sigs = PgpKeyRing.ReadSignaturesAndTrust(bcpgInputStream);
				this.keys.Add(new PgpPublicKey(publicPk2, trustPk2, sigs));
			}
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x000F85C6 File Offset: 0x000F75C6
		public PgpPublicKey GetPublicKey()
		{
			return (PgpPublicKey)this.keys[0];
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x000F85DC File Offset: 0x000F75DC
		public PgpPublicKey GetPublicKey(long keyId)
		{
			foreach (object obj in this.keys)
			{
				PgpPublicKey pgpPublicKey = (PgpPublicKey)obj;
				if (keyId == pgpPublicKey.KeyId)
				{
					return pgpPublicKey;
				}
			}
			return null;
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x000F8640 File Offset: 0x000F7640
		public IEnumerable GetPublicKeys()
		{
			return new EnumerableProxy(this.keys);
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x000F8650 File Offset: 0x000F7650
		public byte[] GetEncoded()
		{
			MemoryStream memoryStream = new MemoryStream();
			this.Encode(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x000F8670 File Offset: 0x000F7670
		public void Encode(Stream outStr)
		{
			if (outStr == null)
			{
				throw new ArgumentNullException("outStr");
			}
			foreach (object obj in this.keys)
			{
				PgpPublicKey pgpPublicKey = (PgpPublicKey)obj;
				pgpPublicKey.Encode(outStr);
			}
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x000F86D8 File Offset: 0x000F76D8
		public static PgpPublicKeyRing InsertPublicKey(PgpPublicKeyRing pubRing, PgpPublicKey pubKey)
		{
			ArrayList arrayList = new ArrayList(pubRing.keys);
			bool flag = false;
			bool flag2 = false;
			for (int num = 0; num != arrayList.Count; num++)
			{
				PgpPublicKey pgpPublicKey = (PgpPublicKey)arrayList[num];
				if (pgpPublicKey.KeyId == pubKey.KeyId)
				{
					flag = true;
					arrayList[num] = pubKey;
				}
				if (pgpPublicKey.IsMasterKey)
				{
					flag2 = true;
				}
			}
			if (!flag)
			{
				if (pubKey.IsMasterKey)
				{
					if (flag2)
					{
						throw new ArgumentException("cannot add a master key to a ring that already has one");
					}
					arrayList.Insert(0, pubKey);
				}
				else
				{
					arrayList.Add(pubKey);
				}
			}
			return new PgpPublicKeyRing(arrayList);
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x000F876C File Offset: 0x000F776C
		public static PgpPublicKeyRing RemovePublicKey(PgpPublicKeyRing pubRing, PgpPublicKey pubKey)
		{
			ArrayList arrayList = new ArrayList(pubRing.keys);
			bool flag = false;
			for (int i = 0; i < arrayList.Count; i++)
			{
				PgpPublicKey pgpPublicKey = (PgpPublicKey)arrayList[i];
				if (pgpPublicKey.KeyId == pubKey.KeyId)
				{
					flag = true;
					arrayList.RemoveAt(i);
				}
			}
			if (!flag)
			{
				return null;
			}
			return new PgpPublicKeyRing(arrayList);
		}

		// Token: 0x04001CC5 RID: 7365
		private readonly ArrayList keys;
	}
}
