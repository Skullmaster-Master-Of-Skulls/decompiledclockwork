using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Web.UI
{
	// Token: 0x0200028C RID: 652
	internal sealed class FilteredAttributeDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06001EAC RID: 7852 RVA: 0x000623AC File Offset: 0x000605AC
		internal FilteredAttributeDictionary(ParsedAttributeCollection owner, string filter)
		{
			this._filter = filter;
			this._owner = owner;
			this._data = new ListDictionary(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06001EAD RID: 7853 RVA: 0x000623D2 File Offset: 0x000605D2
		internal IDictionary Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06001EAE RID: 7854 RVA: 0x000623DA File Offset: 0x000605DA
		public string Filter
		{
			get
			{
				return this._filter;
			}
		}

		// Token: 0x1700089B RID: 2203
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

		// Token: 0x06001EB1 RID: 7857 RVA: 0x0006240A File Offset: 0x0006060A
		public void Add(string key, string value)
		{
			this._owner.AddFilteredAttribute(this._filter, key, value);
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x0006241F File Offset: 0x0006061F
		public void Clear()
		{
			this._owner.ClearFilter(this._filter);
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x00062432 File Offset: 0x00060632
		public bool Contains(string key)
		{
			return this._data.Contains(key);
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x00062440 File Offset: 0x00060640
		public void Remove(string key)
		{
			this._owner.RemoveFilteredAttribute(this._filter, key);
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06001EB5 RID: 7861 RVA: 0x00007722 File Offset: 0x00005922
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06001EB6 RID: 7862 RVA: 0x00007722 File Offset: 0x00005922
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700089E RID: 2206
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

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06001EB9 RID: 7865 RVA: 0x000624D9 File Offset: 0x000606D9
		ICollection IDictionary.Keys
		{
			get
			{
				return this._data.Keys;
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06001EBA RID: 7866 RVA: 0x000624E6 File Offset: 0x000606E6
		ICollection IDictionary.Values
		{
			get
			{
				return this._data.Values;
			}
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x000624F4 File Offset: 0x000606F4
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

		// Token: 0x06001EBC RID: 7868 RVA: 0x00062565 File Offset: 0x00060765
		bool IDictionary.Contains(object key)
		{
			if (!(key is string))
			{
				throw new ArgumentException(SR.GetString("FilteredAttributeDictionary_ArgumentMustBeString"), "key");
			}
			return this.Contains(key.ToString());
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x00062590 File Offset: 0x00060790
		void IDictionary.Clear()
		{
			this.Clear();
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x00062598 File Offset: 0x00060798
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return this._data.GetEnumerator();
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x000625A5 File Offset: 0x000607A5
		void IDictionary.Remove(object key)
		{
			this.Remove(key.ToString());
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06001EC0 RID: 7872 RVA: 0x000625B3 File Offset: 0x000607B3
		int ICollection.Count
		{
			get
			{
				return this._data.Count;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06001EC1 RID: 7873 RVA: 0x000625C0 File Offset: 0x000607C0
		bool ICollection.IsSynchronized
		{
			get
			{
				return this._data.IsSynchronized;
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06001EC2 RID: 7874 RVA: 0x000625CD File Offset: 0x000607CD
		object ICollection.SyncRoot
		{
			get
			{
				return this._data.SyncRoot;
			}
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x000625DA File Offset: 0x000607DA
		void ICollection.CopyTo(Array array, int index)
		{
			this._data.CopyTo(array, index);
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x00062598 File Offset: 0x00060798
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._data.GetEnumerator();
		}

		// Token: 0x040019A9 RID: 6569
		private string _filter;

		// Token: 0x040019AA RID: 6570
		private IDictionary _data;

		// Token: 0x040019AB RID: 6571
		private ParsedAttributeCollection _owner;
	}
}
