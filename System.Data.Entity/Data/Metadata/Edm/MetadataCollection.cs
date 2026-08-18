using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001E1 RID: 481
	internal class MetadataCollection<T> : IList<T>, ICollection<T>, IEnumerable<!0>, IEnumerable where T : MetadataItem
	{
		// Token: 0x0600205F RID: 8287 RVA: 0x00070BCD File Offset: 0x0006EDCD
		internal MetadataCollection() : this(null)
		{
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x00070BD8 File Offset: 0x0006EDD8
		internal MetadataCollection(IEnumerable<T> items)
		{
			this._collectionData = new MetadataCollection<T>.CollectionData();
			if (items != null)
			{
				foreach (T t in items)
				{
					if (t == null)
					{
						throw EntityUtil.CollectionParameterElementIsNull("items");
					}
					this.AddInternal(t);
				}
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06002061 RID: 8289 RVA: 0x00070C48 File Offset: 0x0006EE48
		public bool IsReadOnly
		{
			get
			{
				return this._readOnly;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06002062 RID: 8290 RVA: 0x00070C50 File Offset: 0x0006EE50
		public virtual ReadOnlyCollection<T> AsReadOnly
		{
			get
			{
				return this._collectionData.OrderedList.AsReadOnly();
			}
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x00070C62 File Offset: 0x0006EE62
		public virtual ReadOnlyMetadataCollection<T> AsReadOnlyMetadataCollection()
		{
			return new ReadOnlyMetadataCollection<T>(this);
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06002064 RID: 8292 RVA: 0x00070C6A File Offset: 0x0006EE6A
		public virtual int Count
		{
			get
			{
				return this._collectionData.OrderedList.Count;
			}
		}

		// Token: 0x1700068C RID: 1676
		public virtual T this[int index]
		{
			get
			{
				return this._collectionData.OrderedList[index];
			}
			set
			{
				throw EntityUtil.OperationOnReadOnlyCollection();
			}
		}

		// Token: 0x1700068D RID: 1677
		public virtual T this[string identity]
		{
			get
			{
				return this.GetValue(identity, false);
			}
			set
			{
				throw EntityUtil.OperationOnReadOnlyCollection();
			}
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x00070C9C File Offset: 0x0006EE9C
		public virtual T GetValue(string identity, bool ignoreCase)
		{
			T t = this.InternalTryGetValue(identity, ignoreCase);
			if (t == null)
			{
				throw EntityUtil.ItemInvalidIdentity(identity, "identity");
			}
			return t;
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x00070CC7 File Offset: 0x0006EEC7
		public virtual void Add(T item)
		{
			this.AddInternal(item);
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x00070CD0 File Offset: 0x0006EED0
		private static int AddToDictionary(MetadataCollection<T>.CollectionData collectionData, string identity, int index, bool updateIfFound)
		{
			int[] array = null;
			int exactIndex = index;
			MetadataCollection<T>.OrderedIndex orderedIndex;
			if (collectionData.IdentityDictionary.TryGetValue(identity, out orderedIndex))
			{
				if (MetadataCollection<T>.EqualIdentity(collectionData.OrderedList, orderedIndex.ExactIndex, identity))
				{
					if (updateIfFound)
					{
						return orderedIndex.ExactIndex;
					}
					throw EntityUtil.ItemDuplicateIdentity(identity, "item", null);
				}
				else
				{
					if (orderedIndex.InexactIndexes != null)
					{
						int i = 0;
						while (i < orderedIndex.InexactIndexes.Length)
						{
							if (MetadataCollection<T>.EqualIdentity(collectionData.OrderedList, orderedIndex.InexactIndexes[i], identity))
							{
								if (updateIfFound)
								{
									return orderedIndex.InexactIndexes[i];
								}
								throw EntityUtil.ItemDuplicateIdentity(identity, "item", null);
							}
							else
							{
								i++;
							}
						}
						array = new int[orderedIndex.InexactIndexes.Length + 1];
						orderedIndex.InexactIndexes.CopyTo(array, 0);
						array[array.Length - 1] = index;
					}
					else
					{
						array = new int[]
						{
							index
						};
					}
					exactIndex = orderedIndex.ExactIndex;
				}
			}
			collectionData.IdentityDictionary[identity] = new MetadataCollection<T>.OrderedIndex(exactIndex, array);
			return index;
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x00070DB6 File Offset: 0x0006EFB6
		private void AddInternal(T item)
		{
			this.ThrowIfReadOnly();
			MetadataCollection<T>.AddInternalHelper(item, this._collectionData, false);
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x00070DCC File Offset: 0x0006EFCC
		private static void AddInternalHelper(T item, MetadataCollection<T>.CollectionData collectionData, bool updateIfFound)
		{
			int count = collectionData.OrderedList.Count;
			int num;
			if (collectionData.IdentityDictionary != null)
			{
				num = MetadataCollection<T>.AddToDictionary(collectionData, item.Identity, count, updateIfFound);
			}
			else
			{
				num = MetadataCollection<T>.IndexOf(collectionData, item.Identity, false);
				if (0 <= num)
				{
					if (!updateIfFound)
					{
						throw EntityUtil.ItemDuplicateIdentity(item.Identity, "item", null);
					}
				}
				else if (25 <= count)
				{
					collectionData.IdentityDictionary = new Dictionary<string, MetadataCollection<T>.OrderedIndex>(collectionData.OrderedList.Count + 1, StringComparer.OrdinalIgnoreCase);
					for (int i = 0; i < collectionData.OrderedList.Count; i++)
					{
						MetadataCollection<T>.AddToDictionary(collectionData, collectionData.OrderedList[i].Identity, i, false);
					}
					MetadataCollection<T>.AddToDictionary(collectionData, item.Identity, count, false);
				}
			}
			if (0 <= num && num < count)
			{
				collectionData.OrderedList[num] = item;
				return;
			}
			collectionData.OrderedList.Add(item);
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x00070EC8 File Offset: 0x0006F0C8
		internal bool AtomicAddRange(List<T> items)
		{
			MetadataCollection<T>.CollectionData collectionData = this._collectionData;
			MetadataCollection<T>.CollectionData collectionData2 = new MetadataCollection<T>.CollectionData(collectionData, items.Count);
			foreach (T item in items)
			{
				MetadataCollection<T>.AddInternalHelper(item, collectionData2, false);
			}
			MetadataCollection<T>.CollectionData collectionData3 = Interlocked.CompareExchange<MetadataCollection<T>.CollectionData>(ref this._collectionData, collectionData2, collectionData);
			return collectionData3 == collectionData;
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x00070F44 File Offset: 0x0006F144
		private static bool EqualIdentity(List<T> orderedList, int index, string identity)
		{
			return orderedList[index].Identity == identity;
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x0006E219 File Offset: 0x0006C419
		void IList<!0>.Insert(int index, T item)
		{
			throw EntityUtil.OperationOnReadOnlyCollection();
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x0006E219 File Offset: 0x0006C419
		bool ICollection<!0>.Remove(T item)
		{
			throw EntityUtil.OperationOnReadOnlyCollection();
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x0006E219 File Offset: 0x0006C419
		void IList<!0>.RemoveAt(int index)
		{
			throw EntityUtil.OperationOnReadOnlyCollection();
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x0006E219 File Offset: 0x0006C419
		void ICollection<!0>.Clear()
		{
			throw EntityUtil.OperationOnReadOnlyCollection();
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x00070F5D File Offset: 0x0006F15D
		public bool Contains(T item)
		{
			return -1 != this.IndexOf(item);
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x00070F6C File Offset: 0x0006F16C
		public virtual bool ContainsIdentity(string identity)
		{
			EntityUtil.CheckStringArgument(identity, "identity");
			return 0 <= MetadataCollection<T>.IndexOf(this._collectionData, identity, false);
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x00070F8C File Offset: 0x0006F18C
		private static int IndexOf(MetadataCollection<T>.CollectionData collectionData, string identity, bool ignoreCase)
		{
			int num = -1;
			if (collectionData.IdentityDictionary != null)
			{
				MetadataCollection<T>.OrderedIndex orderedIndex;
				if (collectionData.IdentityDictionary.TryGetValue(identity, out orderedIndex))
				{
					if (ignoreCase)
					{
						num = orderedIndex.ExactIndex;
					}
					else if (MetadataCollection<T>.EqualIdentity(collectionData.OrderedList, orderedIndex.ExactIndex, identity))
					{
						return orderedIndex.ExactIndex;
					}
					if (orderedIndex.InexactIndexes != null)
					{
						if (ignoreCase)
						{
							throw EntityUtil.MoreThanOneItemMatchesIdentity(identity);
						}
						for (int i = 0; i < orderedIndex.InexactIndexes.Length; i++)
						{
							if (MetadataCollection<T>.EqualIdentity(collectionData.OrderedList, orderedIndex.InexactIndexes[i], identity))
							{
								return orderedIndex.InexactIndexes[i];
							}
						}
					}
				}
			}
			else if (ignoreCase)
			{
				for (int j = 0; j < collectionData.OrderedList.Count; j++)
				{
					if (string.Equals(collectionData.OrderedList[j].Identity, identity, StringComparison.OrdinalIgnoreCase))
					{
						if (0 <= num)
						{
							throw EntityUtil.MoreThanOneItemMatchesIdentity(identity);
						}
						num = j;
					}
				}
			}
			else
			{
				for (int k = 0; k < collectionData.OrderedList.Count; k++)
				{
					if (MetadataCollection<T>.EqualIdentity(collectionData.OrderedList, k, identity))
					{
						return k;
					}
				}
			}
			return num;
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x000710A0 File Offset: 0x0006F2A0
		public virtual int IndexOf(T item)
		{
			int num = MetadataCollection<T>.IndexOf(this._collectionData, item.Identity, false);
			if (num != -1 && this._collectionData.OrderedList[num] == item)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x000710EC File Offset: 0x0006F2EC
		public virtual void CopyTo(T[] array, int arrayIndex)
		{
			EntityUtil.GenericCheckArgumentNull<T[]>(array, "array");
			if (arrayIndex < 0)
			{
				throw EntityUtil.ArgumentOutOfRange("arrayIndex");
			}
			if (this._collectionData.OrderedList.Count > array.Length - arrayIndex)
			{
				throw EntityUtil.ArrayTooSmall("arrayIndex");
			}
			this._collectionData.OrderedList.CopyTo(array, arrayIndex);
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x00071148 File Offset: 0x0006F348
		public ReadOnlyMetadataCollection<T>.Enumerator GetEnumerator()
		{
			return new ReadOnlyMetadataCollection<T>.Enumerator(this);
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x00071150 File Offset: 0x0006F350
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x00071150 File Offset: 0x0006F350
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x00071160 File Offset: 0x0006F360
		public MetadataCollection<T> SetReadOnly()
		{
			for (int i = 0; i < this._collectionData.OrderedList.Count; i++)
			{
				this._collectionData.OrderedList[i].SetReadOnly();
			}
			this._collectionData.OrderedList.TrimExcess();
			this._readOnly = true;
			return this;
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x000711BB File Offset: 0x0006F3BB
		public virtual bool TryGetValue(string identity, bool ignoreCase, out T item)
		{
			item = this.InternalTryGetValue(identity, ignoreCase);
			return item != null;
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x000711DC File Offset: 0x0006F3DC
		private T InternalTryGetValue(string identity, bool ignoreCase)
		{
			int num = MetadataCollection<T>.IndexOf(this._collectionData, EntityUtil.GenericCheckArgumentNull<string>(identity, "identity"), ignoreCase);
			if (0 > num)
			{
				return default(T);
			}
			return this._collectionData.OrderedList[num];
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x00071220 File Offset: 0x0006F420
		internal void ThrowIfReadOnly()
		{
			if (this.IsReadOnly)
			{
				throw EntityUtil.OperationOnReadOnlyCollection();
			}
		}

		// Token: 0x04000E44 RID: 3652
		private MetadataCollection<T>.CollectionData _collectionData;

		// Token: 0x04000E45 RID: 3653
		private bool _readOnly;

		// Token: 0x04000E46 RID: 3654
		private const int UseSortedListCrossover = 25;

		// Token: 0x0200051A RID: 1306
		private struct OrderedIndex
		{
			// Token: 0x06003DE7 RID: 15847 RVA: 0x000E73DB File Offset: 0x000E55DB
			internal OrderedIndex(int exactIndex, int[] inexactIndexes)
			{
				this.ExactIndex = exactIndex;
				this.InexactIndexes = inexactIndexes;
			}

			// Token: 0x04001B2D RID: 6957
			internal readonly int ExactIndex;

			// Token: 0x04001B2E RID: 6958
			internal readonly int[] InexactIndexes;
		}

		// Token: 0x0200051B RID: 1307
		private class CollectionData
		{
			// Token: 0x06003DE8 RID: 15848 RVA: 0x000E73EB File Offset: 0x000E55EB
			internal CollectionData()
			{
				this.OrderedList = new List<T>();
			}

			// Token: 0x06003DE9 RID: 15849 RVA: 0x000E7400 File Offset: 0x000E5600
			internal CollectionData(MetadataCollection<T>.CollectionData original, int additionalCapacity)
			{
				this.OrderedList = new List<T>(original.OrderedList.Count + additionalCapacity);
				foreach (T item in original.OrderedList)
				{
					this.OrderedList.Add(item);
				}
				if (25 <= this.OrderedList.Capacity)
				{
					this.IdentityDictionary = new Dictionary<string, MetadataCollection<T>.OrderedIndex>(this.OrderedList.Capacity, StringComparer.OrdinalIgnoreCase);
					if (original.IdentityDictionary != null)
					{
						using (Dictionary<string, MetadataCollection<T>.OrderedIndex>.Enumerator enumerator2 = original.IdentityDictionary.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								KeyValuePair<string, MetadataCollection<T>.OrderedIndex> keyValuePair = enumerator2.Current;
								this.IdentityDictionary.Add(keyValuePair.Key, keyValuePair.Value);
							}
							return;
						}
					}
					for (int i = 0; i < this.OrderedList.Count; i++)
					{
						MetadataCollection<T>.AddToDictionary(this, this.OrderedList[i].Identity, i, false);
					}
				}
			}

			// Token: 0x04001B2F RID: 6959
			internal Dictionary<string, MetadataCollection<T>.OrderedIndex> IdentityDictionary;

			// Token: 0x04001B30 RID: 6960
			internal List<T> OrderedList;
		}
	}
}
