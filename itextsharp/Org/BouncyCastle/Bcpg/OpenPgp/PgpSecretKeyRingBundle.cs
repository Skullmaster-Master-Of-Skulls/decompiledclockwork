using System;
using System.Collections;
using System.Globalization;
using System.IO;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000340 RID: 832
	public class PgpSecretKeyRingBundle
	{
		// Token: 0x06001E13 RID: 7699 RVA: 0x000B4B84 File Offset: 0x000B3B84
		private PgpSecretKeyRingBundle(IDictionary secretRings, ArrayList order)
		{
			this.secretRings = secretRings;
			this.order = order;
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x000B4B9A File Offset: 0x000B3B9A
		public PgpSecretKeyRingBundle(byte[] encoding) : this(new MemoryStream(encoding, false))
		{
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x000B4BA9 File Offset: 0x000B3BA9
		public PgpSecretKeyRingBundle(Stream inputStream) : this(new PgpObjectFactory(inputStream).AllPgpObjects())
		{
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x000B4BBC File Offset: 0x000B3BBC
		public PgpSecretKeyRingBundle(IEnumerable e)
		{
			this.secretRings = new Hashtable();
			this.order = new ArrayList();
			foreach (object obj in e)
			{
				PgpSecretKeyRing pgpSecretKeyRing = obj as PgpSecretKeyRing;
				if (pgpSecretKeyRing == null)
				{
					throw new PgpException(obj.GetType().FullName + " found where PgpSecretKeyRing expected");
				}
				long keyId = pgpSecretKeyRing.GetPublicKey().KeyId;
				this.secretRings.Add(keyId, pgpSecretKeyRing);
				this.order.Add(keyId);
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001E17 RID: 7703 RVA: 0x000B4C78 File Offset: 0x000B3C78
		[Obsolete("Use 'Count' property instead")]
		public int Size
		{
			get
			{
				return this.order.Count;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001E18 RID: 7704 RVA: 0x000B4C85 File Offset: 0x000B3C85
		public int Count
		{
			get
			{
				return this.order.Count;
			}
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x000B4C92 File Offset: 0x000B3C92
		public IEnumerable GetKeyRings()
		{
			return new EnumerableProxy(this.secretRings.Values);
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x000B4CA4 File Offset: 0x000B3CA4
		public IEnumerable GetKeyRings(string userId)
		{
			return this.GetKeyRings(userId, false, false);
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x000B4CAF File Offset: 0x000B3CAF
		public IEnumerable GetKeyRings(string userId, bool matchPartial)
		{
			return this.GetKeyRings(userId, matchPartial, false);
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x000B4CBC File Offset: 0x000B3CBC
		public IEnumerable GetKeyRings(string userId, bool matchPartial, bool ignoreCase)
		{
			IList list = new ArrayList();
			if (ignoreCase)
			{
				userId = userId.ToLower(CultureInfo.InvariantCulture);
			}
			foreach (object obj in this.GetKeyRings())
			{
				PgpSecretKeyRing pgpSecretKeyRing = (PgpSecretKeyRing)obj;
				foreach (object obj2 in pgpSecretKeyRing.GetSecretKey().UserIds)
				{
					string text = (string)obj2;
					string text2 = text;
					if (ignoreCase)
					{
						text2 = text2.ToLower(CultureInfo.InvariantCulture);
					}
					if (matchPartial)
					{
						if (text2.IndexOf(userId) > -1)
						{
							list.Add(pgpSecretKeyRing);
						}
					}
					else if (text2.Equals(userId))
					{
						list.Add(pgpSecretKeyRing);
					}
				}
			}
			return new EnumerableProxy(list);
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x000B4DC0 File Offset: 0x000B3DC0
		public PgpSecretKey GetSecretKey(long keyId)
		{
			foreach (object obj in this.GetKeyRings())
			{
				PgpSecretKeyRing pgpSecretKeyRing = (PgpSecretKeyRing)obj;
				PgpSecretKey secretKey = pgpSecretKeyRing.GetSecretKey(keyId);
				if (secretKey != null)
				{
					return secretKey;
				}
			}
			return null;
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x000B4E28 File Offset: 0x000B3E28
		public PgpSecretKeyRing GetSecretKeyRing(long keyId)
		{
			if (this.secretRings.Contains(keyId))
			{
				return (PgpSecretKeyRing)this.secretRings[keyId];
			}
			foreach (object obj in this.GetKeyRings())
			{
				PgpSecretKeyRing pgpSecretKeyRing = (PgpSecretKeyRing)obj;
				PgpSecretKey secretKey = pgpSecretKeyRing.GetSecretKey(keyId);
				if (secretKey != null)
				{
					return pgpSecretKeyRing;
				}
			}
			return null;
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x000B4EC0 File Offset: 0x000B3EC0
		public bool Contains(long keyID)
		{
			return this.GetSecretKey(keyID) != null;
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x000B4ED0 File Offset: 0x000B3ED0
		public byte[] GetEncoded()
		{
			MemoryStream memoryStream = new MemoryStream();
			this.Encode(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x000B4EF0 File Offset: 0x000B3EF0
		public void Encode(Stream outStr)
		{
			BcpgOutputStream outStr2 = BcpgOutputStream.Wrap(outStr);
			foreach (object obj in this.order)
			{
				long num = (long)obj;
				PgpSecretKeyRing pgpSecretKeyRing = (PgpSecretKeyRing)this.secretRings[num];
				pgpSecretKeyRing.Encode(outStr2);
			}
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x000B4F6C File Offset: 0x000B3F6C
		public static PgpSecretKeyRingBundle AddSecretKeyRing(PgpSecretKeyRingBundle bundle, PgpSecretKeyRing secretKeyRing)
		{
			long keyId = secretKeyRing.GetPublicKey().KeyId;
			if (bundle.secretRings.Contains(keyId))
			{
				throw new ArgumentException("Collection already contains a key with a keyId for the passed in ring.");
			}
			IDictionary dictionary = new Hashtable(bundle.secretRings);
			ArrayList arrayList = new ArrayList(bundle.order);
			dictionary[keyId] = secretKeyRing;
			arrayList.Add(keyId);
			return new PgpSecretKeyRingBundle(dictionary, arrayList);
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x000B4FDC File Offset: 0x000B3FDC
		public static PgpSecretKeyRingBundle RemoveSecretKeyRing(PgpSecretKeyRingBundle bundle, PgpSecretKeyRing secretKeyRing)
		{
			long keyId = secretKeyRing.GetPublicKey().KeyId;
			if (!bundle.secretRings.Contains(keyId))
			{
				throw new ArgumentException("Collection does not contain a key with a keyId for the passed in ring.");
			}
			IDictionary dictionary = new Hashtable(bundle.secretRings);
			ArrayList arrayList = new ArrayList(bundle.order);
			dictionary.Remove(keyId);
			arrayList.Remove(keyId);
			return new PgpSecretKeyRingBundle(dictionary, arrayList);
		}

		// Token: 0x040014F3 RID: 5363
		private readonly IDictionary secretRings;

		// Token: 0x040014F4 RID: 5364
		private readonly ArrayList order;
	}
}
