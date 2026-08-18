using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Web.UI
{
	// Token: 0x020003F6 RID: 1014
	internal sealed class FilteredAttributeDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06003214 RID: 12820 RVA: 0x000DC2C2 File Offset: 0x000DB2C2
		internal FilteredAttributeDictionary(ParsedAttributeCollection owner, string filter)
		{
			this._filter = filter;
			this._owner = owner;
			this._data = new ListDictionary(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06003215 RID: 12821 RVA: 0x000DC2E8 File Offset: 0x000DB2E8
		internal IDictionary Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06003216 RID: 12822 RVA: 0x000DC2F0 File Offset: 0x000DB2F0
		public string Filter
		{
			get
			{
				return this._filter;
			}
		}

		// Token: 0x17000B04 RID: 2820
		public string this[string key]
		{
			get
			{
				return (string)this._data[key];
			}
			set
			{
				this._owner.ReplaceFilteredAttribute(this._filter, key, value);
			}
		}

		// Token: 0x06003219 RID: 12825 RVA: 0x000DC320 File Offset: 0x000DB320
		public void Add(string key, string value)
		{
			this._owner.AddFilteredAttribute(this._filter, key, value);
		}

		// Token: 0x0600321A RID: 12826 RVA: 0x000DC335 File Offset: 0x000DB335
		public void Clear()
		{
			this._owner.ClearFilter(this._filter);
		}

		// Token: 0x0600321B RID: 12827 RVA: 0x000DC348 File Offset: 0x000DB348
		public bool Contains(string key)
		{
			return this._data.Contains(key);
		}

		// Token: 0x0600321C RID: 12828 RVA: 0x000DC356 File Offset: 0x000DB356
		public void Remove(string key)
		{
			this._owner.RemoveFilteredAttribute(this._filter, key);
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x0600321D RID: 12829 RVA: 0x000DC36A File Offset: 0x000DB36A
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x0600321E RID: 12830 RVA: 0x000DC36D File Offset: 0x000DB36D
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B07 RID: 2823
		object IDictionary.this[object key]
		{
			get
			{
				if (!(key is string))
				{
					throw new ArgumentException(SR.GetString("FilteredAttributeDictionary_ArgumentMustBeString"), "key");
				}
				return this[key.ToString()];
			}
			set
			{
				if (!(key is string))
				{
					throw new ArgumentException(SR.GetString("FilteredAttributeDictionary_ArgumentMustBeString"), "key");
				}
				if (!(value is string))
				{
					throw new ArgumentException(SR.GetString("FilteredAttributeDictionary_ArgumentMustBeString"), "value");
				}
				this[key.ToString()] = value.ToString();
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06003221 RID: 12833 RVA: 0x000DC3F5 File Offset: 0x000DB3F5
		ICollection IDictionary.Keys
		{
			get
			{
				return this._data.Keys;
			}
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06003222 RID: 12834 RVA: 0x000DC402 File Offset: 0x000DB402
		ICollection IDictionary.Values
		{
			get
			{
				return this._data.Values;
			}
		}

		// Token: 0x06003223 RID: 12835 RVA: 0x000DC410 File Offset: 0x000DB410
		void IDictionary.Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (!(key is string))
			{
				throw new ArgumentException(SR.GetString("FilteredAttributeDictionary_ArgumentMustBeString"), "key");
			}
			if (!(value is string))
			{
				throw new ArgumentException(SR.GetString("FilteredAttributeDictionary_ArgumentMustBeString"), "value");
			}
			if (value == null)
			{
				value = string.Empty;
			}
			this.Add(key.ToString(), value.ToString());
		}

		// Token: 0x06003224 RID: 12836 RVA: 0x000DC481 File Offset: 0x000DB481
		bool IDictionary.Contains(object key)
		{
			if (!(key is string))
			{
				throw new ArgumentException(SR.GetString("FilteredAttributeDictionary_ArgumentMustBeString"), "key");
			}
			return this.Contains(key.ToString());
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x000DC4AC File Offset: 0x000DB4AC
		void IDictionary.Clear()
		{
			this.Clear();
		}

		// Token: 0x06003226 RID: 12838 RVA: 0x000DC4B4 File Offset: 0x000DB4B4
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return this._data.GetEnumerator();
		}

		// Token: 0x06003227 RID: 12839 RVA: 0x000DC4C1 File Offset: 0x000DB4C1
		void IDictionary.Remove(object key)
		{
			this.Remove(key.ToString());
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06003228 RID: 12840 RVA: 0x000DC4CF File Offset: 0x000DB4CF
		int ICollection.Count
		{
			get
			{
				return this._data.Count;
			}
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06003229 RID: 12841 RVA: 0x000DC4DC File Offset: 0x000DB4DC
		bool ICollection.IsSynchronized
		{
			get
			{
				return this._data.IsSynchronized;
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x0600322A RID: 12842 RVA: 0x000DC4E9 File Offset: 0x000DB4E9
		object ICollection.SyncRoot
		{
			get
			{
				return this._data.SyncRoot;
			}
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x000DC4F6 File Offset: 0x000DB4F6
		void ICollection.CopyTo(Array array, int index)
		{
			this._data.CopyTo(array, index);
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x000DC505 File Offset: 0x000DB505
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._data.GetEnumerator();
		}

		// Token: 0x040022F5 RID: 8949
		private string _filter;

		// Token: 0x040022F6 RID: 8950
		private IDictionary _data;

		// Token: 0x040022F7 RID: 8951
		private ParsedAttributeCollection _owner;
	}
}
