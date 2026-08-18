using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace System.Collections.Specialized
{
	// Token: 0x020003B0 RID: 944
	[__DynamicallyInvokable]
	[Serializable]
	public class NameValueCollection : NameObjectCollectionBase
	{
		// Token: 0x0600235F RID: 9055 RVA: 0x000A7B1D File Offset: 0x000A5D1D
		public NameValueCollection()
		{
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x000A7B25 File Offset: 0x000A5D25
		public NameValueCollection(NameValueCollection col) : base((col != null) ? col.Comparer : null)
		{
			this.Add(col);
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x000A7B40 File Offset: 0x000A5D40
		[Obsolete("Please use NameValueCollection(IEqualityComparer) instead.")]
		public NameValueCollection(IHashCodeProvider hashProvider, IComparer comparer) : base(hashProvider, comparer)
		{
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x000A7B4A File Offset: 0x000A5D4A
		public NameValueCollection(int capacity) : base(capacity)
		{
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x000A7B53 File Offset: 0x000A5D53
		public NameValueCollection(IEqualityComparer equalityComparer) : base(equalityComparer)
		{
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x000A7B5C File Offset: 0x000A5D5C
		public NameValueCollection(int capacity, IEqualityComparer equalityComparer) : base(capacity, equalityComparer)
		{
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x000A7B66 File Offset: 0x000A5D66
		public NameValueCollection(int capacity, NameValueCollection col) : base(capacity, (col != null) ? col.Comparer : null)
		{
			if (col == null)
			{
				throw new ArgumentNullException("col");
			}
			base.Comparer = col.Comparer;
			this.Add(col);
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x000A7B9C File Offset: 0x000A5D9C
		[Obsolete("Please use NameValueCollection(Int32, IEqualityComparer) instead.")]
		public NameValueCollection(int capacity, IHashCodeProvider hashProvider, IComparer comparer) : base(capacity, hashProvider, comparer)
		{
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x000A7BA7 File Offset: 0x000A5DA7
		internal NameValueCollection(DBNull dummy) : base(dummy)
		{
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x000A7BB0 File Offset: 0x000A5DB0
		protected NameValueCollection(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x000A7BBA File Offset: 0x000A5DBA
		protected void InvalidateCachedArrays()
		{
			this._all = null;
			this._allKeys = null;
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x000A7BCC File Offset: 0x000A5DCC
		private static string GetAsOneString(ArrayList list)
		{
			int num = (list != null) ? list.Count : 0;
			if (num == 1)
			{
				return (string)list[0];
			}
			if (num > 1)
			{
				StringBuilder stringBuilder = new StringBuilder((string)list[0]);
				for (int i = 1; i < num; i++)
				{
					stringBuilder.Append(',');
					stringBuilder.Append((string)list[i]);
				}
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x000A7C40 File Offset: 0x000A5E40
		private static string[] GetAsStringArray(ArrayList list)
		{
			int num = (list != null) ? list.Count : 0;
			if (num == 0)
			{
				return null;
			}
			string[] array = new string[num];
			list.CopyTo(0, array, 0, num);
			return array;
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x000A7C74 File Offset: 0x000A5E74
		public void Add(NameValueCollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c");
			}
			this.InvalidateCachedArrays();
			int count = c.Count;
			for (int i = 0; i < count; i++)
			{
				string key = c.GetKey(i);
				string[] values = c.GetValues(i);
				if (values != null)
				{
					for (int j = 0; j < values.Length; j++)
					{
						this.Add(key, values[j]);
					}
				}
				else
				{
					this.Add(key, null);
				}
			}
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x000A7CE2 File Offset: 0x000A5EE2
		public virtual void Clear()
		{
			if (base.IsReadOnly)
			{
				throw new NotSupportedException(SR.GetString("CollectionReadOnly"));
			}
			this.InvalidateCachedArrays();
			base.BaseClear();
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x000A7D08 File Offset: 0x000A5F08
		public void CopyTo(Array dest, int index)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			if (dest.Rank != 1)
			{
				throw new ArgumentException(SR.GetString("Arg_MultiRank"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("IndexOutOfRange", new object[]
				{
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (dest.Length - index < this.Count)
			{
				throw new ArgumentException(SR.GetString("Arg_InsufficientSpace"));
			}
			int count = this.Count;
			if (this._all == null)
			{
				string[] array = new string[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = this.Get(i);
					dest.SetValue(array[i], i + index);
				}
				this._all = array;
				return;
			}
			for (int j = 0; j < count; j++)
			{
				dest.SetValue(this._all[j], j + index);
			}
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x000A7DE7 File Offset: 0x000A5FE7
		public bool HasKeys()
		{
			return this.InternalHasKeys();
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x000A7DEF File Offset: 0x000A5FEF
		internal virtual bool InternalHasKeys()
		{
			return base.BaseHasKeys();
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x000A7DF8 File Offset: 0x000A5FF8
		public virtual void Add(string name, string value)
		{
			if (base.IsReadOnly)
			{
				throw new NotSupportedException(SR.GetString("CollectionReadOnly"));
			}
			this.InvalidateCachedArrays();
			ArrayList arrayList = (ArrayList)base.BaseGet(name);
			if (arrayList == null)
			{
				arrayList = new ArrayList(1);
				if (value != null)
				{
					arrayList.Add(value);
				}
				base.BaseAdd(name, arrayList);
				return;
			}
			if (value != null)
			{
				arrayList.Add(value);
			}
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x000A7E5C File Offset: 0x000A605C
		public virtual string Get(string name)
		{
			ArrayList list = (ArrayList)base.BaseGet(name);
			return NameValueCollection.GetAsOneString(list);
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x000A7E7C File Offset: 0x000A607C
		public virtual string[] GetValues(string name)
		{
			ArrayList list = (ArrayList)base.BaseGet(name);
			return NameValueCollection.GetAsStringArray(list);
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x000A7E9C File Offset: 0x000A609C
		public virtual void Set(string name, string value)
		{
			if (base.IsReadOnly)
			{
				throw new NotSupportedException(SR.GetString("CollectionReadOnly"));
			}
			this.InvalidateCachedArrays();
			base.BaseSet(name, new ArrayList(1)
			{
				value
			});
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x000A7EDE File Offset: 0x000A60DE
		public virtual void Remove(string name)
		{
			this.InvalidateCachedArrays();
			base.BaseRemove(name);
		}

		// Token: 0x170008FA RID: 2298
		public string this[string name]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Get(name);
			}
			[__DynamicallyInvokable]
			set
			{
				this.Set(name, value);
			}
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x000A7F00 File Offset: 0x000A6100
		public virtual string Get(int index)
		{
			ArrayList list = (ArrayList)base.BaseGet(index);
			return NameValueCollection.GetAsOneString(list);
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x000A7F20 File Offset: 0x000A6120
		public virtual string[] GetValues(int index)
		{
			ArrayList list = (ArrayList)base.BaseGet(index);
			return NameValueCollection.GetAsStringArray(list);
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x000A7F40 File Offset: 0x000A6140
		public virtual string GetKey(int index)
		{
			return base.BaseGetKey(index);
		}

		// Token: 0x170008FB RID: 2299
		public string this[int index]
		{
			get
			{
				return this.Get(index);
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x0600237C RID: 9084 RVA: 0x000A7F52 File Offset: 0x000A6152
		public virtual string[] AllKeys
		{
			get
			{
				if (this._allKeys == null)
				{
					this._allKeys = base.BaseGetAllKeys();
				}
				return this._allKeys;
			}
		}

		// Token: 0x04001FDD RID: 8157
		private string[] _all;

		// Token: 0x04001FDE RID: 8158
		private string[] _allKeys;
	}
}
