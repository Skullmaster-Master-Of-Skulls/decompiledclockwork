using System;
using System.Collections;

namespace log4net.Util
{
	// Token: 0x020000F8 RID: 248
	[Serializable]
	public sealed class EmptyDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x060006F7 RID: 1783 RVA: 0x00016211 File Offset: 0x00014411
		private EmptyDictionary()
		{
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x00016219 File Offset: 0x00014419
		public static EmptyDictionary Instance
		{
			get
			{
				return EmptyDictionary.s_instance;
			}
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00016220 File Offset: 0x00014420
		public void CopyTo(Array array, int index)
		{
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x00016222 File Offset: 0x00014422
		public bool IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x00016225 File Offset: 0x00014425
		public int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x00016228 File Offset: 0x00014428
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001622B File Offset: 0x0001442B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return NullEnumerator.Instance;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00016232 File Offset: 0x00014432
		public void Add(object key, object value)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00016239 File Offset: 0x00014439
		public void Clear()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00016240 File Offset: 0x00014440
		public bool Contains(object key)
		{
			return false;
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00016243 File Offset: 0x00014443
		public IDictionaryEnumerator GetEnumerator()
		{
			return NullDictionaryEnumerator.Instance;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001624A File Offset: 0x0001444A
		public void Remove(object key)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x00016251 File Offset: 0x00014451
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x00016254 File Offset: 0x00014454
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x00016257 File Offset: 0x00014457
		public ICollection Keys
		{
			get
			{
				return EmptyCollection.Instance;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x0001625E File Offset: 0x0001445E
		public ICollection Values
		{
			get
			{
				return EmptyCollection.Instance;
			}
		}

		// Token: 0x17000179 RID: 377
		public object this[object key]
		{
			get
			{
				return null;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x040002AC RID: 684
		private static readonly EmptyDictionary s_instance = new EmptyDictionary();
	}
}
