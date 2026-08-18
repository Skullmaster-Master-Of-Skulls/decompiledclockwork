using System;
using System.Runtime.InteropServices;
using System.ServiceModel;

namespace System.Collections.Generic
{
	// Token: 0x02000021 RID: 33
	[ComVisible(false)]
	public abstract class SynchronizedKeyedCollection<K, T> : SynchronizedCollection<T>
	{
		// Token: 0x06000123 RID: 291 RVA: 0x00007CFD File Offset: 0x00005EFD
		protected SynchronizedKeyedCollection()
		{
			this.comparer = EqualityComparer<K>.Default;
			this.threshold = int.MaxValue;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00007D1B File Offset: 0x00005F1B
		protected SynchronizedKeyedCollection(object syncRoot) : base(syncRoot)
		{
			this.comparer = EqualityComparer<K>.Default;
			this.threshold = int.MaxValue;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00007D3A File Offset: 0x00005F3A
		protected SynchronizedKeyedCollection(object syncRoot, IEqualityComparer<K> comparer) : base(syncRoot)
		{
			if (comparer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("comparer"));
			}
			this.comparer = comparer;
			this.threshold = int.MaxValue;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00007D70 File Offset: 0x00005F70
		protected SynchronizedKeyedCollection(object syncRoot, IEqualityComparer<K> comparer, int dictionaryCreationThreshold) : base(syncRoot)
		{
			if (comparer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("comparer"));
			}
			if (dictionaryCreationThreshold < -1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("dictionaryCreationThreshold", dictionaryCreationThreshold, SR.GetString("ValueMustBeInRange", new object[]
				{
					-1,
					int.MaxValue
				})));
			}
			if (dictionaryCreationThreshold == -1)
			{
				this.threshold = int.MaxValue;
			}
			else
			{
				this.threshold = dictionaryCreationThreshold;
			}
			this.comparer = comparer;
		}

		// Token: 0x1700002C RID: 44
		public T this[K key]
		{
			get
			{
				if (key == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("key"));
				}
				object syncRoot = base.SyncRoot;
				T result;
				lock (syncRoot)
				{
					if (this.dictionary == null)
					{
						for (int i = 0; i < base.Items.Count; i++)
						{
							T t = base.Items[i];
							if (this.comparer.Equals(key, this.GetKeyForItem(t)))
							{
								return t;
							}
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new KeyNotFoundException());
					}
					result = this.dictionary[key];
				}
				return result;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00007EBC File Offset: 0x000060BC
		protected IDictionary<K, T> Dictionary
		{
			get
			{
				return this.dictionary;
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00007EC4 File Offset: 0x000060C4
		private void AddKey(K key, T item)
		{
			if (this.dictionary != null)
			{
				this.dictionary.Add(key, item);
				return;
			}
			if (this.keyCount == this.threshold)
			{
				this.CreateDictionary();
				this.dictionary.Add(key, item);
				return;
			}
			if (this.Contains(key))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("CannotAddTwoItemsWithTheSameKeyToSynchronizedKeyedCollection0")));
			}
			this.keyCount++;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00007F3C File Offset: 0x0000613C
		protected void ChangeItemKey(T item, K newKey)
		{
			if (!this.ContainsItem(item))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("ItemDoesNotExistInSynchronizedKeyedCollection0")));
			}
			K keyForItem = this.GetKeyForItem(item);
			if (!this.comparer.Equals(newKey, keyForItem))
			{
				if (newKey != null)
				{
					this.AddKey(newKey, item);
				}
				if (keyForItem != null)
				{
					this.RemoveKey(keyForItem);
				}
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00007FA2 File Offset: 0x000061A2
		protected override void ClearItems()
		{
			base.ClearItems();
			if (this.dictionary != null)
			{
				this.dictionary.Clear();
			}
			this.keyCount = 0;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00007FC4 File Offset: 0x000061C4
		public bool Contains(K key)
		{
			if (key == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("key"));
			}
			object syncRoot = base.SyncRoot;
			bool result;
			lock (syncRoot)
			{
				if (this.dictionary != null)
				{
					result = this.dictionary.ContainsKey(key);
				}
				else
				{
					if (key != null)
					{
						for (int i = 0; i < base.Items.Count; i++)
						{
							T item = base.Items[i];
							if (this.comparer.Equals(key, this.GetKeyForItem(item)))
							{
								return true;
							}
						}
					}
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000807C File Offset: 0x0000627C
		private bool ContainsItem(T item)
		{
			K keyForItem;
			if (this.dictionary == null || (keyForItem = this.GetKeyForItem(item)) == null)
			{
				return base.Items.Contains(item);
			}
			T y;
			return this.dictionary.TryGetValue(keyForItem, out y) && EqualityComparer<T>.Default.Equals(item, y);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000080CC File Offset: 0x000062CC
		private void CreateDictionary()
		{
			this.dictionary = new Dictionary<K, T>(this.comparer);
			foreach (T t in base.Items)
			{
				K keyForItem = this.GetKeyForItem(t);
				if (keyForItem != null)
				{
					this.dictionary.Add(keyForItem, t);
				}
			}
		}

		// Token: 0x0600012F RID: 303
		protected abstract K GetKeyForItem(T item);

		// Token: 0x06000130 RID: 304 RVA: 0x00008148 File Offset: 0x00006348
		protected override void InsertItem(int index, T item)
		{
			K keyForItem = this.GetKeyForItem(item);
			if (keyForItem != null)
			{
				this.AddKey(keyForItem, item);
			}
			base.InsertItem(index, item);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00008178 File Offset: 0x00006378
		public bool Remove(K key)
		{
			if (key == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("key"));
			}
			object syncRoot = base.SyncRoot;
			bool result;
			lock (syncRoot)
			{
				if (this.dictionary != null)
				{
					if (this.dictionary.ContainsKey(key))
					{
						result = base.Remove(this.dictionary[key]);
					}
					else
					{
						result = false;
					}
				}
				else
				{
					for (int i = 0; i < base.Items.Count; i++)
					{
						if (this.comparer.Equals(key, this.GetKeyForItem(base.Items[i])))
						{
							this.RemoveItem(i);
							return true;
						}
					}
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00008244 File Offset: 0x00006444
		protected override void RemoveItem(int index)
		{
			K keyForItem = this.GetKeyForItem(base.Items[index]);
			if (keyForItem != null)
			{
				this.RemoveKey(keyForItem);
			}
			base.RemoveItem(index);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000827A File Offset: 0x0000647A
		private void RemoveKey(K key)
		{
			if (key == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("key");
			}
			if (this.dictionary != null)
			{
				this.dictionary.Remove(key);
				return;
			}
			this.keyCount--;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000082B8 File Offset: 0x000064B8
		protected override void SetItem(int index, T item)
		{
			K keyForItem = this.GetKeyForItem(item);
			K keyForItem2 = this.GetKeyForItem(base.Items[index]);
			if (this.comparer.Equals(keyForItem, keyForItem2))
			{
				if (keyForItem != null && this.dictionary != null)
				{
					this.dictionary[keyForItem] = item;
				}
			}
			else
			{
				if (keyForItem != null)
				{
					this.AddKey(keyForItem, item);
				}
				if (keyForItem2 != null)
				{
					this.RemoveKey(keyForItem2);
				}
			}
			base.SetItem(index, item);
		}

		// Token: 0x0400017E RID: 382
		private const int defaultThreshold = 0;

		// Token: 0x0400017F RID: 383
		private IEqualityComparer<K> comparer;

		// Token: 0x04000180 RID: 384
		private Dictionary<K, T> dictionary;

		// Token: 0x04000181 RID: 385
		private int keyCount;

		// Token: 0x04000182 RID: 386
		private int threshold;
	}
}
