using System;
using System.Collections;
using System.Globalization;
using System.IO;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000341 RID: 833
	public class PgpPublicKeyRingBundle
	{
		// Token: 0x06001E24 RID: 7716 RVA: 0x000B504A File Offset: 0x000B404A
		private PgpPublicKeyRingBundle(IDictionary pubRings, ArrayList order)
		{
			this.pubRings = pubRings;
			this.order = order;
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x000B5060 File Offset: 0x000B4060
		public PgpPublicKeyRingBundle(byte[] encoding) : this(new MemoryStream(encoding, false))
		{
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x000B506F File Offset: 0x000B406F
		public PgpPublicKeyRingBundle(Stream inputStream) : this(new PgpObjectFactory(inputStream).AllPgpObjects())
		{
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x000B5084 File Offset: 0x000B4084
		public PgpPublicKeyRingBundle(IEnumerable e)
		{
			this.pubRings = new Hashtable();
			this.order = new ArrayList();
			foreach (object obj in e)
			{
				PgpPublicKeyRing pgpPublicKeyRing = obj as PgpPublicKeyRing;
				if (pgpPublicKeyRing == null)
				{
					throw new PgpException(obj.GetType().FullName + " found where PgpPublicKeyRing expected");
				}
				long keyId = pgpPublicKeyRing.GetPublicKey().KeyId;
				this.pubRings.Add(keyId, pgpPublicKeyRing);
				this.order.Add(keyId);
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001E28 RID: 7720 RVA: 0x000B5140 File Offset: 0x000B4140
		[Obsolete("Use 'Count' property instead")]
		public int Size
		{
			get
			{
				return this.order.Count;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001E29 RID: 7721 RVA: 0x000B514D File Offset: 0x000B414D
		public int Count
		{
			get
			{
				return this.order.Count;
			}
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x000B515A File Offset: 0x000B415A
		public IEnumerable GetKeyRings()
		{
			return new EnumerableProxy(this.pubRings.Values);
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x000B516C File Offset: 0x000B416C
		public IEnumerable GetKeyRings(string userId)
		{
			return this.GetKeyRings(userId, false, false);
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x000B5177 File Offset: 0x000B4177
		public IEnumerable GetKeyRings(string userId, bool matchPartial)
		{
			return this.GetKeyRings(userId, matchPartial, false);
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x000B5184 File Offset: 0x000B4184
		public IEnumerable GetKeyRings(string userId, bool matchPartial, bool ignoreCase)
		{
			IList list = new ArrayList();
			if (ignoreCase)
			{
				userId = userId.ToLower(CultureInfo.InvariantCulture);
			}
			foreach (object obj in this.GetKeyRings())
			{
				PgpPublicKeyRing pgpPublicKeyRing = (PgpPublicKeyRing)obj;
				foreach (object obj2 in pgpPublicKeyRing.GetPublicKey().GetUserIds())
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
							list.Add(pgpPublicKeyRing);
						}
					}
					else if (text2.Equals(userId))
					{
						list.Add(pgpPublicKeyRing);
					}
				}
			}
			return new EnumerableProxy(list);
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x000B5288 File Offset: 0x000B4288
		public PgpPublicKey GetPublicKey(long keyId)
		{
			foreach (object obj in this.GetKeyRings())
			{
				PgpPublicKeyRing pgpPublicKeyRing = (PgpPublicKeyRing)obj;
				PgpPublicKey publicKey = pgpPublicKeyRing.GetPublicKey(keyId);
				if (publicKey != null)
				{
					return publicKey;
				}
			}
			return null;
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x000B52F0 File Offset: 0x000B42F0
		public PgpPublicKeyRing GetPublicKeyRing(long keyId)
		{
			if (this.pubRings.Contains(keyId))
			{
				return (PgpPublicKeyRing)this.pubRings[keyId];
			}
			foreach (object obj in this.GetKeyRings())
			{
				PgpPublicKeyRing pgpPublicKeyRing = (PgpPublicKeyRing)obj;
				PgpPublicKey publicKey = pgpPublicKeyRing.GetPublicKey(keyId);
				if (publicKey != null)
				{
					return pgpPublicKeyRing;
				}
			}
			return null;
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x000B5384 File Offset: 0x000B4384
		public bool Contains(long keyID)
		{
			return this.GetPublicKey(keyID) != null;
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x000B5394 File Offset: 0x000B4394
		public byte[] GetEncoded()
		{
			MemoryStream memoryStream = new MemoryStream();
			this.Encode(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x000B53B4 File Offset: 0x000B43B4
		public void Encode(Stream outStr)
		{
			BcpgOutputStream outStr2 = BcpgOutputStream.Wrap(outStr);
			foreach (object obj in this.order)
			{
				long num = (long)obj;
				PgpPublicKeyRing pgpPublicKeyRing = (PgpPublicKeyRing)this.pubRings[num];
				pgpPublicKeyRing.Encode(outStr2);
			}
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x000B5430 File Offset: 0x000B4430
		public static PgpPublicKeyRingBundle AddPublicKeyRing(PgpPublicKeyRingBundle bundle, PgpPublicKeyRing publicKeyRing)
		{
			long keyId = publicKeyRing.GetPublicKey().KeyId;
			if (bundle.pubRings.Contains(keyId))
			{
				throw new ArgumentException("Bundle already contains a key with a keyId for the passed in ring.");
			}
			IDictionary dictionary = new Hashtable(bundle.pubRings);
			ArrayList arrayList = new ArrayList(bundle.order);
			dictionary[keyId] = publicKeyRing;
			arrayList.Add(keyId);
			return new PgpPublicKeyRingBundle(dictionary, arrayList);
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x000B54A0 File Offset: 0x000B44A0
		public static PgpPublicKeyRingBundle RemovePublicKeyRing(PgpPublicKeyRingBundle bundle, PgpPublicKeyRing publicKeyRing)
		{
			long keyId = publicKeyRing.GetPublicKey().KeyId;
			if (!bundle.pubRings.Contains(keyId))
			{
				throw new ArgumentException("Bundle does not contain a key with a keyId for the passed in ring.");
			}
			IDictionary dictionary = new Hashtable(bundle.pubRings);
			ArrayList arrayList = new ArrayList(bundle.order);
			dictionary.Remove(keyId);
			arrayList.Remove(keyId);
			return new PgpPublicKeyRingBundle(dictionary, arrayList);
		}

		// Token: 0x040014F5 RID: 5365
		private readonly IDictionary pubRings;

		// Token: 0x040014F6 RID: 5366
		private readonly ArrayList order;
	}
}
