using System;
using System.Runtime.InteropServices;
using System.ServiceModel;

namespace System.Collections.Generic
{
	// Token: 0x02000022 RID: 34
	[ComVisible(false)]
	public class SynchronizedReadOnlyCollection<T> : IList<T>, ICollection<!0>, IEnumerable<!0>, IEnumerable, IList, ICollection
	{
		// Token: 0x06000135 RID: 309 RVA: 0x00008335 File Offset: 0x00006535
		public SynchronizedReadOnlyCollection()
		{
			this.items = new List<T>();
			this.sync = new object();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00008353 File Offset: 0x00006553
		public SynchronizedReadOnlyCollection(object syncRoot)
		{
			if (syncRoot == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("syncRoot"));
			}
			this.items = new List<T>();
			this.sync = syncRoot;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00008388 File Offset: 0x00006588
		public SynchronizedReadOnlyCollection(object syncRoot, IEnumerable<T> list)
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

		// Token: 0x06000138 RID: 312 RVA: 0x000083E0 File Offset: 0x000065E0
		public SynchronizedReadOnlyCollection(object syncRoot, params T[] list)
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

		// Token: 0x06000139 RID: 313 RVA: 0x00008458 File Offset: 0x00006658
		internal SynchronizedReadOnlyCollection(object syncRoot, List<T> list, bool makeCopy)
		{
			if (syncRoot == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("syncRoot"));
			}
			if (list == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("list"));
			}
			if (makeCopy)
			{
				this.items = new List<T>(list);
			}
			else
			{
				this.items = list;
			}
			this.sync = syncRoot;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600013A RID: 314 RVA: 0x000084BC File Offset: 0x000066BC
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

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00008504 File Offset: 0x00006704
		protected IList<T> Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x17000030 RID: 48
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
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00008554 File Offset: 0x00006754
		public bool Contains(T value)
		{
			object obj = this.sync;
			bool result;
			lock (obj)
			{
				result = this.items.Contains(value);
			}
			return result;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000859C File Offset: 0x0000679C
		public void CopyTo(T[] array, int index)
		{
			object obj = this.sync;
			lock (obj)
			{
				this.items.CopyTo(array, index);
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000085E4 File Offset: 0x000067E4
		public IEnumerator<T> GetEnumerator()
		{
			object obj = this.sync;
			IEnumerator<T> enumerator;
			lock (obj)
			{
				enumerator = this.items.GetEnumerator();
			}
			return enumerator;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000862C File Offset: 0x0000682C
		public int IndexOf(T value)
		{
			object obj = this.sync;
			int result;
			lock (obj)
			{
				result = this.items.IndexOf(value);
			}
			return result;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00008674 File Offset: 0x00006874
		private void ThrowReadOnly()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SFxCollectionReadOnly")));
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000142 RID: 322 RVA: 0x0000868F File Offset: 0x0000688F
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000032 RID: 50
		T IList<!0>.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this.ThrowReadOnly();
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000086A3 File Offset: 0x000068A3
		void ICollection<!0>.Add(T value)
		{
			this.ThrowReadOnly();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000086AB File Offset: 0x000068AB
		void ICollection<!0>.Clear()
		{
			this.ThrowReadOnly();
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000086B3 File Offset: 0x000068B3
		bool ICollection<!0>.Remove(T value)
		{
			this.ThrowReadOnly();
			return false;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000086BC File Offset: 0x000068BC
		void IList<!0>.Insert(int index, T value)
		{
			this.ThrowReadOnly();
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000086C4 File Offset: 0x000068C4
		void IList<!0>.RemoveAt(int index)
		{
			this.ThrowReadOnly();
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000086CC File Offset: 0x000068CC
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000086CF File Offset: 0x000068CF
		object ICollection.SyncRoot
		{
			get
			{
				return this.sync;
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000086D8 File Offset: 0x000068D8
		void ICollection.CopyTo(Array array, int index)
		{
			ICollection collection = this.items as ICollection;
			if (collection == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SFxCopyToRequiresICollection")));
			}
			object obj = this.sync;
			lock (obj)
			{
				collection.CopyTo(array, index);
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00008744 File Offset: 0x00006944
		IEnumerator IEnumerable.GetEnumerator()
		{
			object obj = this.sync;
			IEnumerator result;
			lock (obj)
			{
				IEnumerable enumerable = this.items;
				if (enumerable != null)
				{
					result = enumerable.GetEnumerator();
				}
				else
				{
					result = new SynchronizedReadOnlyCollection<T>.EnumeratorAdapter(this.items);
				}
			}
			return result;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000087A0 File Offset: 0x000069A0
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000087A3 File Offset: 0x000069A3
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000037 RID: 55
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this.ThrowReadOnly();
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000087BC File Offset: 0x000069BC
		int IList.Add(object value)
		{
			this.ThrowReadOnly();
			return 0;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000087C5 File Offset: 0x000069C5
		void IList.Clear()
		{
			this.ThrowReadOnly();
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000087CD File Offset: 0x000069CD
		bool IList.Contains(object value)
		{
			SynchronizedReadOnlyCollection<T>.VerifyValueType(value);
			return this.Contains((T)((object)value));
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000087E1 File Offset: 0x000069E1
		int IList.IndexOf(object value)
		{
			SynchronizedReadOnlyCollection<T>.VerifyValueType(value);
			return this.IndexOf((T)((object)value));
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000087F5 File Offset: 0x000069F5
		void IList.Insert(int index, object value)
		{
			this.ThrowReadOnly();
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000087FD File Offset: 0x000069FD
		void IList.Remove(object value)
		{
			this.ThrowReadOnly();
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00008805 File Offset: 0x00006A05
		void IList.RemoveAt(int index)
		{
			this.ThrowReadOnly();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00008810 File Offset: 0x00006A10
		private static void VerifyValueType(object value)
		{
			if (value is T || (value == null && !typeof(T).IsValueType))
			{
				return;
			}
			Type type = (value == null) ? typeof(object) : value.GetType();
			string @string = SR.GetString("SFxCollectionWrongType2", new object[]
			{
				type.ToString(),
				typeof(T).ToString()
			});
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(@string));
		}

		// Token: 0x04000183 RID: 387
		private IList<T> items;

		// Token: 0x04000184 RID: 388
		private object sync;

		// Token: 0x02000AC9 RID: 2761
		private sealed class EnumeratorAdapter : IEnumerator, IDisposable
		{
			// Token: 0x06006E37 RID: 28215 RVA: 0x0019B987 File Offset: 0x00199B87
			public EnumeratorAdapter(IList<T> list)
			{
				this.list = list;
				this.e = list.GetEnumerator();
			}

			// Token: 0x170019B2 RID: 6578
			// (get) Token: 0x06006E38 RID: 28216 RVA: 0x0019B9A2 File Offset: 0x00199BA2
			public object Current
			{
				get
				{
					return this.e.Current;
				}
			}

			// Token: 0x06006E39 RID: 28217 RVA: 0x0019B9B4 File Offset: 0x00199BB4
			public bool MoveNext()
			{
				return this.e.MoveNext();
			}

			// Token: 0x06006E3A RID: 28218 RVA: 0x0019B9C1 File Offset: 0x00199BC1
			public void Dispose()
			{
				this.e.Dispose();
			}

			// Token: 0x06006E3B RID: 28219 RVA: 0x0019B9CE File Offset: 0x00199BCE
			public void Reset()
			{
				this.e = this.list.GetEnumerator();
			}

			// Token: 0x04003F02 RID: 16130
			private IList<T> list;

			// Token: 0x04003F03 RID: 16131
			private IEnumerator<T> e;
		}
	}
}
