using System;
using System.Runtime.InteropServices;
using System.ServiceModel;

namespace System.Collections.Generic
{
	// Token: 0x02000020 RID: 32
	[ComVisible(false)]
	public class SynchronizedCollection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00007531 File Offset: 0x00005731
		public SynchronizedCollection()
		{
			this.items = new List<T>();
			this.sync = new object();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000754F File Offset: 0x0000574F
		public SynchronizedCollection(object syncRoot)
		{
			if (syncRoot == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("syncRoot"));
			}
			this.items = new List<T>();
			this.sync = syncRoot;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00007584 File Offset: 0x00005784
		public SynchronizedCollection(object syncRoot, IEnumerable<T> list)
		{
			if (syncRoot == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("syncRoot"));
			}
			if (list == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("list"));
			}
			this.items = new List<T>(list);
			this.sync = syncRoot;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000075DC File Offset: 0x000057DC
		public SynchronizedCollection(object syncRoot, params T[] list)
		{
			if (syncRoot == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("syncRoot"));
			}
			if (list == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("list"));
			}
			this.items = new List<T>(list.Length);
			for (int i = 0; i < list.Length; i++)
			{
				this.items.Add(list[i]);
			}
			this.sync = syncRoot;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00007654 File Offset: 0x00005854
		public int Count
		{
			get
			{
				object obj = this.sync;
				int count;
				lock (obj)
				{
					count = this.items.Count;
				}
				return count;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000102 RID: 258 RVA: 0x0000769C File Offset: 0x0000589C
		protected List<T> Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000076A4 File Offset: 0x000058A4
		public object SyncRoot
		{
			get
			{
				return this.sync;
			}
		}

		// Token: 0x17000025 RID: 37
		public T this[int index]
		{
			get
			{
				object obj = this.sync;
				T result;
				lock (obj)
				{
					result = this.items[index];
				}
				return result;
			}
			set
			{
				object obj = this.sync;
				lock (obj)
				{
					if (index < 0 || index >= this.items.Count)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("index", index, SR.GetString("ValueMustBeInRange", new object[]
						{
							0,
							this.items.Count - 1
						})));
					}
					this.SetItem(index, value);
				}
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00007794 File Offset: 0x00005994
		public void Add(T item)
		{
			object obj = this.sync;
			lock (obj)
			{
				int count = this.items.Count;
				this.InsertItem(count, item);
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000077E4 File Offset: 0x000059E4
		public void Clear()
		{
			object obj = this.sync;
			lock (obj)
			{
				this.ClearItems();
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00007824 File Offset: 0x00005A24
		public void CopyTo(T[] array, int index)
		{
			object obj = this.sync;
			lock (obj)
			{
				this.items.CopyTo(array, index);
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000786C File Offset: 0x00005A6C
		public bool Contains(T item)
		{
			object obj = this.sync;
			bool result;
			lock (obj)
			{
				result = this.items.Contains(item);
			}
			return result;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000078B4 File Offset: 0x00005AB4
		public IEnumerator<T> GetEnumerator()
		{
			object obj = this.sync;
			IEnumerator<T> result;
			lock (obj)
			{
				result = this.items.GetEnumerator();
			}
			return result;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00007900 File Offset: 0x00005B00
		public int IndexOf(T item)
		{
			object obj = this.sync;
			int result;
			lock (obj)
			{
				result = this.InternalIndexOf(item);
			}
			return result;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00007944 File Offset: 0x00005B44
		public void Insert(int index, T item)
		{
			object obj = this.sync;
			lock (obj)
			{
				if (index < 0 || index > this.items.Count)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("index", index, SR.GetString("ValueMustBeInRange", new object[]
					{
						0,
						this.items.Count
					})));
				}
				this.InsertItem(index, item);
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000079E0 File Offset: 0x00005BE0
		private int InternalIndexOf(T item)
		{
			int count = this.items.Count;
			for (int i = 0; i < count; i++)
			{
				if (object.Equals(this.items[i], item))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00007A28 File Offset: 0x00005C28
		public bool Remove(T item)
		{
			object obj = this.sync;
			bool result;
			lock (obj)
			{
				int num = this.InternalIndexOf(item);
				if (num < 0)
				{
					result = false;
				}
				else
				{
					this.RemoveItem(num);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00007A7C File Offset: 0x00005C7C
		public void RemoveAt(int index)
		{
			object obj = this.sync;
			lock (obj)
			{
				if (index < 0 || index >= this.items.Count)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("index", index, SR.GetString("ValueMustBeInRange", new object[]
					{
						0,
						this.items.Count - 1
					})));
				}
				this.RemoveItem(index);
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00007B18 File Offset: 0x00005D18
		protected virtual void ClearItems()
		{
			this.items.Clear();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00007B25 File Offset: 0x00005D25
		protected virtual void InsertItem(int index, T item)
		{
			this.items.Insert(index, item);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00007B34 File Offset: 0x00005D34
		protected virtual void RemoveItem(int index)
		{
			this.items.RemoveAt(index);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00007B42 File Offset: 0x00005D42
		protected virtual void SetItem(int index, T item)
		{
			this.items[index] = item;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00007B51 File Offset: 0x00005D51
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00007B54 File Offset: 0x00005D54
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.items).GetEnumerator();
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00007B61 File Offset: 0x00005D61
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00007B64 File Offset: 0x00005D64
		object ICollection.SyncRoot
		{
			get
			{
				return this.sync;
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00007B6C File Offset: 0x00005D6C
		void ICollection.CopyTo(Array array, int index)
		{
			object obj = this.sync;
			lock (obj)
			{
				((ICollection)this.items).CopyTo(array, index);
			}
		}

		// Token: 0x17000029 RID: 41
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				SynchronizedCollection<T>.VerifyValueType(value);
				this[index] = (T)((object)value);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00007BD7 File Offset: 0x00005DD7
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00007BDA File Offset: 0x00005DDA
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00007BE0 File Offset: 0x00005DE0
		int IList.Add(object value)
		{
			SynchronizedCollection<T>.VerifyValueType(value);
			object obj = this.sync;
			int result;
			lock (obj)
			{
				this.Add((T)((object)value));
				result = this.Count - 1;
			}
			return result;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00007C38 File Offset: 0x00005E38
		bool IList.Contains(object value)
		{
			SynchronizedCollection<T>.VerifyValueType(value);
			return this.Contains((T)((object)value));
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00007C4C File Offset: 0x00005E4C
		int IList.IndexOf(object value)
		{
			SynchronizedCollection<T>.VerifyValueType(value);
			return this.IndexOf((T)((object)value));
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00007C60 File Offset: 0x00005E60
		void IList.Insert(int index, object value)
		{
			SynchronizedCollection<T>.VerifyValueType(value);
			this.Insert(index, (T)((object)value));
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00007C75 File Offset: 0x00005E75
		void IList.Remove(object value)
		{
			SynchronizedCollection<T>.VerifyValueType(value);
			this.Remove((T)((object)value));
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00007C8C File Offset: 0x00005E8C
		private static void VerifyValueType(object value)
		{
			if (value == null)
			{
				if (typeof(T).IsValueType)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SynchronizedCollectionWrongTypeNull")));
				}
			}
			else if (!(value is T))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SynchronizedCollectionWrongType1", new object[]
				{
					value.GetType().FullName
				})));
			}
		}

		// Token: 0x0400017C RID: 380
		private List<T> items;

		// Token: 0x0400017D RID: 381
		private object sync;
	}
}
