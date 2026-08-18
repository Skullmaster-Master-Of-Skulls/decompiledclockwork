using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000605 RID: 1541
	public class PgpSecretKeyRing : PgpKeyRing
	{
		// Token: 0x06003489 RID: 13449 RVA: 0x001473A7 File Offset: 0x001463A7
		internal PgpSecretKeyRing(IList keys) : this(keys, new ArrayList())
		{
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x001473B5 File Offset: 0x001463B5
		private PgpSecretKeyRing(IList keys, IList extraPubKeys)
		{
			this.keys = keys;
			this.extraPubKeys = extraPubKeys;
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x001473CB File Offset: 0x001463CB
		public PgpSecretKeyRing(byte[] encoding) : this(new MemoryStream(encoding))
		{
		}

		// Token: 0x0600348C RID: 13452 RVA: 0x001473DC File Offset: 0x001463DC
		public PgpSecretKeyRing(Stream inputStream)
		{
			this.keys = new ArrayList();
			this.extraPubKeys = new ArrayList();
			BcpgInputStream bcpgInputStream = BcpgInputStream.Wrap(inputStream);
			PacketTag packetTag = bcpgInputStream.NextPacketTag();
			if (packetTag != PacketTag.SecretKey && packetTag != PacketTag.SecretSubkey)
			{
				string str = "secret key ring doesn't start with secret key tag: tag 0x";
				int num = (int)packetTag;
				throw new IOException(str + num.ToString("X"));
			}
			SecretKeyPacket secretKeyPacket = (SecretKeyPacket)bcpgInputStream.ReadPacket();
			while (bcpgInputStream.NextPacketTag() == PacketTag.Experimental2)
			{
				bcpgInputStream.ReadPacket();
			}
			TrustPacket trustPk = PgpKeyRing.ReadOptionalTrustPacket(bcpgInputStream);
			ArrayList keySigs = PgpKeyRing.ReadSignaturesAndTrust(bcpgInputStream);
			ArrayList ids;
			ArrayList idTrusts;
			ArrayList idSigs;
			PgpKeyRing.ReadUserIDs(bcpgInputStream, out ids, out idTrusts, out idSigs);
			this.keys.Add(new PgpSecretKey(secretKeyPacket, new PgpPublicKey(secretKeyPacket.PublicKeyPacket, trustPk, keySigs, ids, idTrusts, idSigs)));
			while (bcpgInputStream.NextPacketTag() == PacketTag.SecretSubkey || bcpgInputStream.NextPacketTag() == PacketTag.PublicSubkey)
			{
				if (bcpgInputStream.NextPacketTag() == PacketTag.SecretSubkey)
				{
					SecretSubkeyPacket secretSubkeyPacket = (SecretSubkeyPacket)bcpgInputStream.ReadPacket();
					while (bcpgInputStream.NextPacketTag() == PacketTag.Experimental2)
					{
						bcpgInputStream.ReadPacket();
					}
					TrustPacket trustPk2 = PgpKeyRing.ReadOptionalTrustPacket(bcpgInputStream);
					ArrayList sigs = PgpKeyRing.ReadSignaturesAndTrust(bcpgInputStream);
					this.keys.Add(new PgpSecretKey(secretSubkeyPacket, new PgpPublicKey(secretSubkeyPacket.PublicKeyPacket, trustPk2, sigs)));
				}
				else
				{
					PublicSubkeyPacket publicPk = (PublicSubkeyPacket)bcpgInputStream.ReadPacket();
					TrustPacket trustPk3 = PgpKeyRing.ReadOptionalTrustPacket(bcpgInputStream);
					ArrayList sigs2 = PgpKeyRing.ReadSignaturesAndTrust(bcpgInputStream);
					this.extraPubKeys.Add(new PgpPublicKey(publicPk, trustPk3, sigs2));
				}
			}
		}

		// Token: 0x0600348D RID: 13453 RVA: 0x0014754A File Offset: 0x0014654A
		public PgpPublicKey GetPublicKey()
		{
			return ((PgpSecretKey)this.keys[0]).PublicKey;
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x00147562 File Offset: 0x00146562
		public PgpSecretKey GetSecretKey()
		{
			return (PgpSecretKey)this.keys[0];
		}

		// Token: 0x0600348F RID: 13455 RVA: 0x00147575 File Offset: 0x00146575
		public IEnumerable GetSecretKeys()
		{
			return new EnumerableProxy(this.keys);
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x00147584 File Offset: 0x00146584
		public PgpSecretKey GetSecretKey(long keyId)
		{
			foreach (object obj in this.keys)
			{
				PgpSecretKey pgpSecretKey = (PgpSecretKey)obj;
				if (keyId == pgpSecretKey.KeyId)
				{
					return pgpSecretKey;
				}
			}
			return null;
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x001475E8 File Offset: 0x001465E8
		public IEnumerable GetExtraPublicKeys()
		{
			return new EnumerableProxy(this.extraPubKeys);
		}

		// Token: 0x06003492 RID: 13458 RVA: 0x001475F8 File Offset: 0x001465F8
		public byte[] GetEncoded()
		{
			MemoryStream memoryStream = new MemoryStream();
			this.Encode(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x06003493 RID: 13459 RVA: 0x00147618 File Offset: 0x00146618
		public void Encode(Stream outStr)
		{
			if (outStr == null)
			{
				throw new ArgumentNullException("outStr");
			}
			foreach (object obj in this.keys)
			{
				PgpSecretKey pgpSecretKey = (PgpSecretKey)obj;
				pgpSecretKey.Encode(outStr);
			}
			foreach (object obj2 in this.extraPubKeys)
			{
				PgpPublicKey pgpPublicKey = (PgpPublicKey)obj2;
				pgpPublicKey.Encode(outStr);
			}
		}

		// Token: 0x06003494 RID: 13460 RVA: 0x001476D0 File Offset: 0x001466D0
		public static PgpSecretKeyRing ReplacePublicKeys(PgpSecretKeyRing secretRing, PgpPublicKeyRing publicRing)
		{
			IList list = new ArrayList(secretRing.keys.Count);
			foreach (object obj in secretRing.keys)
			{
				PgpSecretKey pgpSecretKey = (PgpSecretKey)obj;
				PgpPublicKey publicKey = null;
				try
				{
					publicKey = publicRing.GetPublicKey(pgpSecretKey.KeyId);
				}
				catch (PgpException ex)
				{
					throw new InvalidOperationException(ex.Message, ex);
				}
				list.Add(PgpSecretKey.ReplacePublicKey(pgpSecretKey, publicKey));
			}
			return new PgpSecretKeyRing(list);
		}

		// Token: 0x06003495 RID: 13461 RVA: 0x0014777C File Offset: 0x0014677C
		public static PgpSecretKeyRing CopyWithNewPassword(PgpSecretKeyRing ring, char[] oldPassPhrase, char[] newPassPhrase, SymmetricKeyAlgorithmTag newEncAlgorithm, SecureRandom rand)
		{
			IList list = new ArrayList(ring.keys.Count);
			foreach (object obj in ring.GetSecretKeys())
			{
				PgpSecretKey key = (PgpSecretKey)obj;
				list.Add(PgpSecretKey.CopyWithNewPassword(key, oldPassPhrase, newPassPhrase, newEncAlgorithm, rand));
			}
			return new PgpSecretKeyRing(list, ring.extraPubKeys);
		}

		// Token: 0x06003496 RID: 13462 RVA: 0x00147800 File Offset: 0x00146800
		public static PgpSecretKeyRing InsertSecretKey(PgpSecretKeyRing secRing, PgpSecretKey secKey)
		{
			ArrayList arrayList = new ArrayList(secRing.keys);
			bool flag = false;
			bool flag2 = false;
			for (int num = 0; num != arrayList.Count; num++)
			{
				PgpSecretKey pgpSecretKey = (PgpSecretKey)arrayList[num];
				if (pgpSecretKey.KeyId == secKey.KeyId)
				{
					flag = true;
					arrayList[num] = secKey;
				}
				if (pgpSecretKey.IsMasterKey)
				{
					flag2 = true;
				}
			}
			if (!flag)
			{
				if (secKey.IsMasterKey)
				{
					if (flag2)
					{
						throw new ArgumentException("cannot add a master key to a ring that already has one");
					}
					arrayList.Insert(0, secKey);
				}
				else
				{
					arrayList.Add(secKey);
				}
			}
			return new PgpSecretKeyRing(arrayList, secRing.extraPubKeys);
		}

		// Token: 0x06003497 RID: 13463 RVA: 0x00147898 File Offset: 0x00146898
		public static PgpSecretKeyRing RemoveSecretKey(PgpSecretKeyRing secRing, PgpSecretKey secKey)
		{
			ArrayList arrayList = new ArrayList(secRing.keys);
			bool flag = false;
			for (int i = 0; i < arrayList.Count; i++)
			{
				PgpSecretKey pgpSecretKey = (PgpSecretKey)arrayList[i];
				if (pgpSecretKey.KeyId == secKey.KeyId)
				{
					flag = true;
					arrayList.RemoveAt(i);
				}
			}
			if (!flag)
			{
				return null;
			}
			return new PgpSecretKeyRing(arrayList, secRing.extraPubKeys);
		}

		// Token: 0x04002354 RID: 9044
		private readonly IList keys;

		// Token: 0x04002355 RID: 9045
		private readonly IList extraPubKeys;
	}
}
