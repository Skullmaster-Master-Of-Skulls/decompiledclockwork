using System;
using System.Collections;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020006F5 RID: 1781
	internal class AggregateDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06003F84 RID: 16260 RVA: 0x000D89AF File Offset: 0x000D79AF
		public AggregateDictionary(ICollection dictionaries)
		{
			this._dictionaries = dictionaries;
		}

		// Token: 0x17000ABC RID: 2748
		public virtual object this[object key]
		{
			get
			{
				foreach (object obj in this._dictionaries)
				{
					IDictionary dictionary = (IDictionary)obj;
					if (dictionary.Contains(key))
					{
						return dictionary[key];
					}
				}
				return null;
			}
			set
			{
				foreach (object obj in this._dictionaries)
				{
					IDictionary dictionary = (IDictionary)obj;
					if (dictionary.Contains(key))
					{
						dictionary[key] = value;
					}
				}
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06003F87 RID: 16263 RVA: 0x000D8A8C File Offset: 0x000D7A8C
		public virtual ICollection Keys
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this._dictionaries)
				{
					IDictionary dictionary = (IDictionary)obj;
					ICollection keys = dictionary.Keys;
					if (keys != null)
					{
						foreach (object value in keys)
						{
							arrayList.Add(value);
						}
					}
				}
				return arrayList;
			}
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06003F88 RID: 16264 RVA: 0x000D8B3C File Offset: 0x000D7B3C
		public virtual ICollection Values
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this._dictionaries)
				{
					IDictionary dictionary = (IDictionary)obj;
					ICollection values = dictionary.Values;
					if (values != null)
					{
						foreach (object value in values)
						{
							arrayList.Add(value);
						}
					}
				}
				return arrayList;
			}
		}

		// Token: 0x06003F89 RID: 16265 RVA: 0x000D8BEC File Offset: 0x000D7BEC
		public virtual bool Contains(object key)
		{
			foreach (object obj in this._dictionaries)
			{
				IDictionary dictionary = (IDictionary)obj;
				if (dictionary.Contains(key))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06003F8A RID: 16266 RVA: 0x000D8C50 File Offset: 0x000D7C50
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06003F8B RID: 16267 RVA: 0x000D8C53 File Offset: 0x000D7C53
		public virtual bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003F8C RID: 16268 RVA: 0x000D8C56 File Offset: 0x000D7C56
		public virtual void Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003F8D RID: 16269 RVA: 0x000D8C5D File Offset: 0x000D7C5D
		public virtual void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003F8E RID: 16270 RVA: 0x000D8C64 File Offset: 0x000D7C64
		public virtual void Remove(object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003F8F RID: 16271 RVA: 0x000D8C6B File Offset: 0x000D7C6B
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new DictionaryEnumeratorByKeys(this);
		}

		// Token: 0x06003F90 RID: 16272 RVA: 0x000D8C73 File Offset: 0x000D7C73
		public virtual void CopyTo(Array array, int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06003F91 RID: 16273 RVA: 0x000D8C7C File Offset: 0x000D7C7C
		public virtual int Count
		{
			get
			{
				int num = 0;
				foreach (object obj in this._dictionaries)
				{
					IDictionary dictionary = (IDictionary)obj;
					num += dictionary.Count;
				}
				return num;
			}
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06003F92 RID: 16274 RVA: 0x000D8CDC File Offset: 0x000D7CDC
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06003F93 RID: 16275 RVA: 0x000D8CDF File Offset: 0x000D7CDF
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003F94 RID: 16276 RVA: 0x000D8CE2 File Offset: 0x000D7CE2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new DictionaryEnumeratorByKeys(this);
		}

		// Token: 0x04002022 RID: 8226
		private ICollection _dictionaries;
	}
}
