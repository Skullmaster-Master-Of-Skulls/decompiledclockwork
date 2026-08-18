using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Runtime.CompilerServices;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004DF RID: 1247
	internal class MetadataCollection<T> : IList<T>, ICollection<T>, IEnumerable<!0>, IEnumerable where T : MetadataItem
	{
		// Token: 0x06002E2C RID: 11820 RVA: 0x000DDFDA File Offset: 0x000DC1DA
		internal MetadataCollection()
		{
			this._metadataList = new List<T>();
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x000DDFF0 File Offset: 0x000DC1F0
		internal MetadataCollection(IEnumerable<T> items)
		{
			this._metadataList = new List<T>();
			if (items != null)
			{
				foreach (T t in items)
				{
					if (t == null)
					{
						throw new ArgumentException(Strings.ADP_CollectionParameterElementIsNull("items"));
					}
					this.AddInternal(t);
				}
			}
		}

		// Token: 0x06002E2E RID: 11822 RVA: 0x000DE064 File Offset: 0x000DC264
		private MetadataCollection(List<T> items)
		{
			this._metadataList = items;
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x000DE073 File Offset: 0x000DC273
		internal static MetadataCollection<T> Wrap(List<T> items)
		{
			return new MetadataCollection<T>(items);
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06002E30 RID: 11824 RVA: 0x000DE07B File Offset: 0x000DC27B
		public virtual int Count
		{
			get
			{
				return this._metadataList.Count;
			}
		}

		// Token: 0x170006BC RID: 1724
		public virtual T this[int index]
		{
			get
			{
				return this._metadataList[index];
			}
			set
			{
				this.ThrowIfReadOnly();
				T t = this._metadataList[index];
				string identity = t.Identity;
				this._metadataList[index] = value;
				this.HandleIdentityChange(value, identity, false);
			}
		}

		// Token: 0x06002E33 RID: 11827 RVA: 0x000DE0DC File Offset: 0x000DC2DC
		internal void HandleIdentityChange(T item, string initialIdentity)
		{
			this.HandleIdentityChange(item, initialIdentity, true);
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x000DE0E8 File Offset: 0x000DC2E8
		private void HandleIdentityChange(T item, string initialIdentity, bool validate)
		{
			T t;
			if (this._caseSensitiveDictionary != null && (!validate || (this._caseSensitiveDictionary.TryGetValue(initialIdentity, out t) && object.ReferenceEquals(t, item))))
			{
				this.RemoveFromCaseSensitiveDictionary(initialIdentity);
				string identity = item.Identity;
				if (this._caseSensitiveDictionary.ContainsKey(identity))
				{
					this._caseSensitiveDictionary = null;
				}
				else
				{
					this._caseSensitiveDictionary.Add(identity, item);
				}
			}
			this._caseInsensitiveDictionary = null;
		}

		// Token: 0x170006BD RID: 1725
		public virtual T this[string identity]
		{
			get
			{
				return this.GetValue(identity, false);
			}
			set
			{
				throw new InvalidOperationException(Strings.OperationOnReadOnlyCollection);
			}
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x000DE188 File Offset: 0x000DC388
		public virtual T GetValue(string identity, bool ignoreCase)
		{
			T result;
			if (!this.TryGetValue(identity, ignoreCase, out result))
			{
				throw new ArgumentException(Strings.ItemInvalidIdentity(identity), "identity");
			}
			return result;
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x000DE1B3 File Offset: 0x000DC3B3
		public virtual bool TryGetValue(string identity, bool ignoreCase, out T item)
		{
			if (!ignoreCase)
			{
				return this.FindCaseSensitive(identity, out item);
			}
			return this.FindCaseInsensitive(identity, out item, false);
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x000DE1CA File Offset: 0x000DC3CA
		public virtual void Add(T item)
		{
			this.ThrowIfReadOnly();
			this.AddInternal(item);
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x000DE1DC File Offset: 0x000DC3DC
		private void AddInternal(T item)
		{
			string identity = item.Identity;
			if (this.ContainsIdentityCaseSensitive(identity))
			{
				throw new ArgumentException(Strings.ItemDuplicateIdentity(identity), "item");
			}
			this._metadataList.Add(item);
			if (this._caseSensitiveDictionary != null)
			{
				this._caseSensitiveDictionary.Add(identity, item);
			}
			this._caseInsensitiveDictionary = null;
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x000DE240 File Offset: 0x000DC440
		internal void AddRange(List<T> items)
		{
			Check.NotNull<List<T>>(items, "items");
			foreach (T t in items)
			{
				if (t == null)
				{
					throw new ArgumentException(Strings.ADP_CollectionParameterElementIsNull("items"));
				}
				this.AddInternal(t);
			}
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x000DE2B4 File Offset: 0x000DC4B4
		internal bool Remove(T item)
		{
			this.ThrowIfReadOnly();
			if (!this._metadataList.Remove(item))
			{
				return false;
			}
			if (this._caseSensitiveDictionary != null)
			{
				this.RemoveFromCaseSensitiveDictionary(item.Identity);
			}
			this._caseInsensitiveDictionary = null;
			return true;
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06002E3D RID: 11837 RVA: 0x000DE2F3 File Offset: 0x000DC4F3
		public virtual ReadOnlyCollection<T> AsReadOnly
		{
			get
			{
				return new ReadOnlyCollection<T>(this._metadataList);
			}
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x000DE300 File Offset: 0x000DC500
		public virtual ReadOnlyMetadataCollection<T> AsReadOnlyMetadataCollection()
		{
			return new ReadOnlyMetadataCollection<T>(this);
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06002E3F RID: 11839 RVA: 0x000DE308 File Offset: 0x000DC508
		public bool IsReadOnly
		{
			get
			{
				return this._readOnly;
			}
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x000DE310 File Offset: 0x000DC510
		internal void ResetReadOnly()
		{
			this._readOnly = false;
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x000DE31C File Offset: 0x000DC51C
		public MetadataCollection<T> SetReadOnly()
		{
			for (int i = 0; i < this._metadataList.Count; i++)
			{
				T t = this._metadataList[i];
				t.SetReadOnly();
			}
			this._readOnly = true;
			this._metadataList.TrimExcess();
			if (this._metadataList.Count <= 8)
			{
				this._caseSensitiveDictionary = null;
				this._caseInsensitiveDictionary = null;
			}
			return this;
		}

		// Token: 0x06002E42 RID: 11842 RVA: 0x000DE38C File Offset: 0x000DC58C
		void IList<!0>.Insert(int index, T item)
		{
			throw new InvalidOperationException(Strings.OperationOnReadOnlyCollection);
		}

		// Token: 0x06002E43 RID: 11843 RVA: 0x000DE398 File Offset: 0x000DC598
		bool ICollection<!0>.Remove(T item)
		{
			throw new InvalidOperationException(Strings.OperationOnReadOnlyCollection);
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x000DE3A4 File Offset: 0x000DC5A4
		void IList<!0>.RemoveAt(int index)
		{
			throw new InvalidOperationException(Strings.OperationOnReadOnlyCollection);
		}

		// Token: 0x06002E45 RID: 11845 RVA: 0x000DE3B0 File Offset: 0x000DC5B0
		void ICollection<!0>.Clear()
		{
			throw new InvalidOperationException(Strings.OperationOnReadOnlyCollection);
		}

		// Token: 0x06002E46 RID: 11846 RVA: 0x000DE3BC File Offset: 0x000DC5BC
		public bool Contains(T item)
		{
			T t;
			return this.TryGetValue(item.Identity, false, out t) && object.ReferenceEquals(t, item);
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x000DE3F4 File Offset: 0x000DC5F4
		public virtual bool ContainsIdentity(string identity)
		{
			return this.ContainsIdentityCaseSensitive(identity);
		}

		// Token: 0x06002E48 RID: 11848 RVA: 0x000DE3FD File Offset: 0x000DC5FD
		public virtual int IndexOf(T item)
		{
			return this._metadataList.IndexOf(item);
		}

		// Token: 0x06002E49 RID: 11849 RVA: 0x000DE40B File Offset: 0x000DC60B
		public virtual void CopyTo(T[] array, int arrayIndex)
		{
			this._metadataList.CopyTo(array, arrayIndex);
		}

		// Token: 0x06002E4A RID: 11850 RVA: 0x000DE41A File Offset: 0x000DC61A
		public ReadOnlyMetadataCollection<T>.Enumerator GetEnumerator()
		{
			return new ReadOnlyMetadataCollection<T>.Enumerator(this);
		}

		// Token: 0x06002E4B RID: 11851 RVA: 0x000DE422 File Offset: 0x000DC622
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002E4C RID: 11852 RVA: 0x000DE42F File Offset: 0x000DC62F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x000DE43C File Offset: 0x000DC63C
		internal void InvalidateCache()
		{
			this._caseSensitiveDictionary = null;
			this._caseInsensitiveDictionary = null;
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06002E4E RID: 11854 RVA: 0x000DE450 File Offset: 0x000DC650
		internal bool HasCaseSensitiveDictionary
		{
			get
			{
				return this._caseSensitiveDictionary != null;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06002E4F RID: 11855 RVA: 0x000DE460 File Offset: 0x000DC660
		internal bool HasCaseInsensitiveDictionary
		{
			get
			{
				return this._caseInsensitiveDictionary != null;
			}
		}

		// Token: 0x06002E50 RID: 11856 RVA: 0x000DE470 File Offset: 0x000DC670
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Dictionary<string, T> GetCaseSensitiveDictionary()
		{
			if (this._caseSensitiveDictionary == null && this._metadataList.Count > 8)
			{
				this._caseSensitiveDictionary = this.CreateCaseSensitiveDictionary();
			}
			return this._caseSensitiveDictionary;
		}

		// Token: 0x06002E51 RID: 11857 RVA: 0x000DE4A0 File Offset: 0x000DC6A0
		private Dictionary<string, T> CreateCaseSensitiveDictionary()
		{
			Dictionary<string, T> dictionary = new Dictionary<string, T>(this._metadataList.Count, StringComparer.Ordinal);
			for (int i = 0; i < this._metadataList.Count; i++)
			{
				T value = this._metadataList[i];
				dictionary.Add(value.Identity, value);
			}
			return dictionary;
		}

		// Token: 0x06002E52 RID: 11858 RVA: 0x000DE4FB File Offset: 0x000DC6FB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Dictionary<string, int> GetCaseInsensitiveDictionary()
		{
			if (this._caseInsensitiveDictionary == null && this._metadataList.Count > 8)
			{
				this._caseInsensitiveDictionary = this.CreateCaseInsensitiveDictionary();
			}
			return this._caseInsensitiveDictionary;
		}

		// Token: 0x06002E53 RID: 11859 RVA: 0x000DE52C File Offset: 0x000DC72C
		private Dictionary<string, int> CreateCaseInsensitiveDictionary()
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>(this._metadataList.Count, StringComparer.OrdinalIgnoreCase);
			Dictionary<string, int> dictionary2 = dictionary;
			T t = this._metadataList[0];
			dictionary2.Add(t.Identity, 0);
			Dictionary<string, int> dictionary3 = dictionary;
			for (int i = 1; i < this._metadataList.Count; i++)
			{
				T t2 = this._metadataList[i];
				string identity = t2.Identity;
				int num;
				if (!dictionary3.TryGetValue(identity, out num))
				{
					dictionary3[identity] = i;
				}
				else if (num >= 0)
				{
					dictionary3[identity] = -1;
				}
			}
			return dictionary3;
		}

		// Token: 0x06002E54 RID: 11860 RVA: 0x000DE5CC File Offset: 0x000DC7CC
		private bool ContainsIdentityCaseSensitive(string identity)
		{
			Dictionary<string, T> caseSensitiveDictionary = this.GetCaseSensitiveDictionary();
			if (caseSensitiveDictionary != null)
			{
				return caseSensitiveDictionary.ContainsKey(identity);
			}
			return this.ListContainsIdentityCaseSensitive(identity);
		}

		// Token: 0x06002E55 RID: 11861 RVA: 0x000DE5F4 File Offset: 0x000DC7F4
		private bool ListContainsIdentityCaseSensitive(string identity)
		{
			for (int i = 0; i < this._metadataList.Count; i++)
			{
				T t = this._metadataList[i];
				if (t.Identity.Equals(identity, StringComparison.Ordinal))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002E56 RID: 11862 RVA: 0x000DE640 File Offset: 0x000DC840
		private bool FindCaseSensitive(string identity, out T item)
		{
			Dictionary<string, T> caseSensitiveDictionary = this.GetCaseSensitiveDictionary();
			if (caseSensitiveDictionary != null)
			{
				return caseSensitiveDictionary.TryGetValue(identity, out item);
			}
			return this.ListFindCaseSensitive(identity, out item);
		}

		// Token: 0x06002E57 RID: 11863 RVA: 0x000DE670 File Offset: 0x000DC870
		private bool ListFindCaseSensitive(string identity, out T item)
		{
			for (int i = 0; i < this._metadataList.Count; i++)
			{
				T t = this._metadataList[i];
				if (t.Identity.Equals(identity, StringComparison.Ordinal))
				{
					item = t;
					return true;
				}
			}
			item = default(T);
			return false;
		}

		// Token: 0x06002E58 RID: 11864 RVA: 0x000DE6C8 File Offset: 0x000DC8C8
		private bool FindCaseInsensitive(string identity, out T item, bool throwOnMultipleMatches)
		{
			Dictionary<string, int> caseInsensitiveDictionary = this.GetCaseInsensitiveDictionary();
			if (caseInsensitiveDictionary != null)
			{
				int num;
				if (caseInsensitiveDictionary.TryGetValue(identity, out num))
				{
					if (num >= 0)
					{
						item = this._metadataList[num];
						return true;
					}
					if (throwOnMultipleMatches)
					{
						throw new InvalidOperationException(Strings.MoreThanOneItemMatchesIdentity(identity));
					}
				}
				item = default(T);
				return false;
			}
			return this.ListFindCaseInsensitive(identity, out item, throwOnMultipleMatches);
		}

		// Token: 0x06002E59 RID: 11865 RVA: 0x000DE724 File Offset: 0x000DC924
		private bool ListFindCaseInsensitive(string identity, out T item, bool throwOnMultipleMatches)
		{
			bool flag = false;
			item = default(T);
			for (int i = 0; i < this._metadataList.Count; i++)
			{
				T t = this._metadataList[i];
				if (t.Identity.Equals(identity, StringComparison.OrdinalIgnoreCase))
				{
					if (flag)
					{
						if (throwOnMultipleMatches)
						{
							throw new InvalidOperationException(Strings.MoreThanOneItemMatchesIdentity(identity));
						}
						item = default(T);
						return false;
					}
					else
					{
						flag = true;
						item = t;
					}
				}
			}
			return flag;
		}

		// Token: 0x06002E5A RID: 11866 RVA: 0x000DE798 File Offset: 0x000DC998
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void RemoveFromCaseSensitiveDictionary(string identity)
		{
			this._caseSensitiveDictionary.Remove(identity);
		}

		// Token: 0x06002E5B RID: 11867 RVA: 0x000DE7A9 File Offset: 0x000DC9A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ThrowIfReadOnly()
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(Strings.OperationOnReadOnlyCollection);
			}
		}

		// Token: 0x040011A2 RID: 4514
		internal const int UseDictionaryCrossover = 8;

		// Token: 0x040011A3 RID: 4515
		private bool _readOnly;

		// Token: 0x040011A4 RID: 4516
		private List<T> _metadataList;

		// Token: 0x040011A5 RID: 4517
		private volatile Dictionary<string, T> _caseSensitiveDictionary;

		// Token: 0x040011A6 RID: 4518
		private volatile Dictionary<string, int> _caseInsensitiveDictionary;
	}
}
