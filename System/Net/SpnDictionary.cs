using System;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.Net
{
	// Token: 0x0200053C RID: 1340
	internal class SpnDictionary : StringDictionary
	{
		// Token: 0x060028ED RID: 10477 RVA: 0x000AA313 File Offset: 0x000A9313
		internal SpnDictionary()
		{
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x060028EE RID: 10478 RVA: 0x000AA32B File Offset: 0x000A932B
		public override int Count
		{
			get
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
				return this.m_SyncTable.Count;
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x060028EF RID: 10479 RVA: 0x000AA342 File Offset: 0x000A9342
		public override bool IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x000AA348 File Offset: 0x000A9348
		internal string InternalGet(string canonicalKey)
		{
			int num = 0;
			string text = null;
			lock (this.m_SyncTable.SyncRoot)
			{
				foreach (object obj in this.m_SyncTable.Keys)
				{
					string text2 = (string)obj;
					if (text2 != null && text2.Length > num && string.Compare(text2, 0, canonicalKey, 0, text2.Length, StringComparison.OrdinalIgnoreCase) == 0)
					{
						num = text2.Length;
						text = text2;
					}
				}
			}
			if (text == null)
			{
				return null;
			}
			return (string)this.m_SyncTable[text];
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x000AA414 File Offset: 0x000A9414
		internal void InternalSet(string canonicalKey, string spn)
		{
			this.m_SyncTable[canonicalKey] = spn;
		}

		// Token: 0x17000860 RID: 2144
		public override string this[string key]
		{
			get
			{
				key = SpnDictionary.GetCanonicalKey(key);
				return this.InternalGet(key);
			}
			set
			{
				key = SpnDictionary.GetCanonicalKey(key);
				this.InternalSet(key, value);
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x060028F4 RID: 10484 RVA: 0x000AA446 File Offset: 0x000A9446
		public override ICollection Keys
		{
			get
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
				return this.m_SyncTable.Keys;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x060028F5 RID: 10485 RVA: 0x000AA45D File Offset: 0x000A945D
		public override object SyncRoot
		{
			[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
			get
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
				return this.m_SyncTable;
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x060028F6 RID: 10486 RVA: 0x000AA46F File Offset: 0x000A946F
		public override ICollection Values
		{
			get
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
				return this.m_SyncTable.Values;
			}
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x000AA486 File Offset: 0x000A9486
		public override void Add(string key, string value)
		{
			key = SpnDictionary.GetCanonicalKey(key);
			this.m_SyncTable.Add(key, value);
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x000AA49D File Offset: 0x000A949D
		public override void Clear()
		{
			ExceptionHelper.WebPermissionUnrestricted.Demand();
			this.m_SyncTable.Clear();
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x000AA4B4 File Offset: 0x000A94B4
		public override bool ContainsKey(string key)
		{
			key = SpnDictionary.GetCanonicalKey(key);
			return this.m_SyncTable.ContainsKey(key);
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x000AA4CA File Offset: 0x000A94CA
		public override bool ContainsValue(string value)
		{
			ExceptionHelper.WebPermissionUnrestricted.Demand();
			return this.m_SyncTable.ContainsValue(value);
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x000AA4E2 File Offset: 0x000A94E2
		public override void CopyTo(Array array, int index)
		{
			ExceptionHelper.WebPermissionUnrestricted.Demand();
			this.m_SyncTable.CopyTo(array, index);
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x000AA4FB File Offset: 0x000A94FB
		public override IEnumerator GetEnumerator()
		{
			ExceptionHelper.WebPermissionUnrestricted.Demand();
			return this.m_SyncTable.GetEnumerator();
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x000AA512 File Offset: 0x000A9512
		public override void Remove(string key)
		{
			key = SpnDictionary.GetCanonicalKey(key);
			this.m_SyncTable.Remove(key);
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x000AA528 File Offset: 0x000A9528
		private static string GetCanonicalKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			try
			{
				Uri uri = new Uri(key);
				key = uri.GetParts(UriComponents.Scheme | UriComponents.Host | UriComponents.Port | UriComponents.Path, UriFormat.SafeUnescaped);
				new WebPermission(NetworkAccess.Connect, new Uri(key)).Demand();
			}
			catch (UriFormatException innerException)
			{
				throw new ArgumentException(SR.GetString("net_mustbeuri", new object[]
				{
					"key"
				}), "key", innerException);
			}
			return key;
		}

		// Token: 0x040027D5 RID: 10197
		private Hashtable m_SyncTable = Hashtable.Synchronized(new Hashtable());
	}
}
