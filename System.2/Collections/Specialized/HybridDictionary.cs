using System;

namespace System.Collections.Specialized
{
	// Token: 0x020003AA RID: 938
	[Serializable]
	public class HybridDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06002304 RID: 8964 RVA: 0x000A66DC File Offset: 0x000A48DC
		public HybridDictionary()
		{
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x000A66E4 File Offset: 0x000A48E4
		public HybridDictionary(int initialSize) : this(initialSize, false)
		{
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x000A66EE File Offset: 0x000A48EE
		public HybridDictionary(bool caseInsensitive)
		{
			this.caseInsensitive = caseInsensitive;
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x000A66FD File Offset: 0x000A48FD
		public HybridDictionary(int initialSize, bool caseInsensitive)
		{
			this.caseInsensitive = caseInsensitive;
			if (initialSize >= 6)
			{
				if (caseInsensitive)
				{
					this.hashtable = new Hashtable(initialSize, StringComparer.OrdinalIgnoreCase);
					return;
				}
				this.hashtable = new Hashtable(initialSize);
			}
		}

		// Token: 0x170008DE RID: 2270
		public object this[object key]
		{
			get
			{
				ListDictionary listDictionary = this.list;
				if (this.hashtable != null)
				{
					return this.hashtable[key];
				}
				if (listDictionary != null)
				{
					return listDictionary[key];
				}
				if (key == null)
				{
					throw new ArgumentNullException("key", SR.GetString("ArgumentNull_Key"));
				}
				return null;
			}
			set
			{
				if (this.hashtable != null)
				{
					this.hashtable[key] = value;
					return;
				}
				if (this.list == null)
				{
					this.list = new ListDictionary(this.caseInsensitive ? StringComparer.OrdinalIgnoreCase : null);
					this.list[key] = value;
					return;
				}
				if (this.list.Count >= 8)
				{
					this.ChangeOver();
					this.hashtable[key] = value;
					return;
				}
				this.list[key] = value;
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x0600230A RID: 8970 RVA: 0x000A6807 File Offset: 0x000A4A07
		private ListDictionary List
		{
			get
			{
				if (this.list == null)
				{
					this.list = new ListDictionary(this.caseInsensitive ? StringComparer.OrdinalIgnoreCase : null);
				}
				return this.list;
			}
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x000A6834 File Offset: 0x000A4A34
		private void ChangeOver()
		{
			IDictionaryEnumerator enumerator = this.list.GetEnumerator();
			Hashtable hashtable;
			if (this.caseInsensitive)
			{
				hashtable = new Hashtable(13, StringComparer.OrdinalIgnoreCase);
			}
			else
			{
				hashtable = new Hashtable(13);
			}
			while (enumerator.MoveNext())
			{
				hashtable.Add(enumerator.Key, enumerator.Value);
			}
			this.hashtable = hashtable;
			this.list = null;
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x0600230C RID: 8972 RVA: 0x000A6898 File Offset: 0x000A4A98
		public int Count
		{
			get
			{
				ListDictionary listDictionary = this.list;
				if (this.hashtable != null)
				{
					return this.hashtable.Count;
				}
				if (listDictionary != null)
				{
					return listDictionary.Count;
				}
				return 0;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x0600230D RID: 8973 RVA: 0x000A68CB File Offset: 0x000A4ACB
		public ICollection Keys
		{
			get
			{
				if (this.hashtable != null)
				{
					return this.hashtable.Keys;
				}
				return this.List.Keys;
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x0600230E RID: 8974 RVA: 0x000A68EC File Offset: 0x000A4AEC
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x0600230F RID: 8975 RVA: 0x000A68EF File Offset: 0x000A4AEF
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06002310 RID: 8976 RVA: 0x000A68F2 File Offset: 0x000A4AF2
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06002311 RID: 8977 RVA: 0x000A68F5 File Offset: 0x000A4AF5
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06002312 RID: 8978 RVA: 0x000A68F8 File Offset: 0x000A4AF8
		public ICollection Values
		{
			get
			{
				if (this.hashtable != null)
				{
					return this.hashtable.Values;
				}
				return this.List.Values;
			}
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x000A691C File Offset: 0x000A4B1C
		public void Add(object key, object value)
		{
			if (this.hashtable != null)
			{
				this.hashtable.Add(key, value);
				return;
			}
			if (this.list == null)
			{
				this.list = new ListDictionary(this.caseInsensitive ? StringComparer.OrdinalIgnoreCase : null);
				this.list.Add(key, value);
				return;
			}
			if (this.list.Count + 1 >= 9)
			{
				this.ChangeOver();
				this.hashtable.Add(key, value);
				return;
			}
			this.list.Add(key, value);
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x000A69A4 File Offset: 0x000A4BA4
		public void Clear()
		{
			if (this.hashtable != null)
			{
				Hashtable hashtable = this.hashtable;
				this.hashtable = null;
				hashtable.Clear();
			}
			if (this.list != null)
			{
				ListDictionary listDictionary = this.list;
				this.list = null;
				listDictionary.Clear();
			}
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x000A69EC File Offset: 0x000A4BEC
		public bool Contains(object key)
		{
			ListDictionary listDictionary = this.list;
			if (this.hashtable != null)
			{
				return this.hashtable.Contains(key);
			}
			if (listDictionary != null)
			{
				return listDictionary.Contains(key);
			}
			if (key == null)
			{
				throw new ArgumentNullException("key", SR.GetString("ArgumentNull_Key"));
			}
			return false;
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x000A6A39 File Offset: 0x000A4C39
		public void CopyTo(Array array, int index)
		{
			if (this.hashtable != null)
			{
				this.hashtable.CopyTo(array, index);
				return;
			}
			this.List.CopyTo(array, index);
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x000A6A60 File Offset: 0x000A4C60
		public IDictionaryEnumerator GetEnumerator()
		{
			if (this.hashtable != null)
			{
				return this.hashtable.GetEnumerator();
			}
			if (this.list == null)
			{
				this.list = new ListDictionary(this.caseInsensitive ? StringComparer.OrdinalIgnoreCase : null);
			}
			return this.list.GetEnumerator();
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x000A6AB0 File Offset: 0x000A4CB0
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (this.hashtable != null)
			{
				return this.hashtable.GetEnumerator();
			}
			if (this.list == null)
			{
				this.list = new ListDictionary(this.caseInsensitive ? StringComparer.OrdinalIgnoreCase : null);
			}
			return this.list.GetEnumerator();
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x000A6B00 File Offset: 0x000A4D00
		public void Remove(object key)
		{
			if (this.hashtable != null)
			{
				this.hashtable.Remove(key);
				return;
			}
			if (this.list != null)
			{
				this.list.Remove(key);
				return;
			}
			if (key == null)
			{
				throw new ArgumentNullException("key", SR.GetString("ArgumentNull_Key"));
			}
		}

		// Token: 0x04001FBC RID: 8124
		private const int CutoverPoint = 9;

		// Token: 0x04001FBD RID: 8125
		private const int InitialHashtableSize = 13;

		// Token: 0x04001FBE RID: 8126
		private const int FixedSizeCutoverPoint = 6;

		// Token: 0x04001FBF RID: 8127
		private ListDictionary list;

		// Token: 0x04001FC0 RID: 8128
		private Hashtable hashtable;

		// Token: 0x04001FC1 RID: 8129
		private bool caseInsensitive;
	}
}
