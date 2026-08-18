using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x0200044F RID: 1103
	internal sealed class ParsedAttributeCollection : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06003473 RID: 13427 RVA: 0x000E36F0 File Offset: 0x000E26F0
		internal ParsedAttributeCollection()
		{
			this._filterTable = new ListDictionary(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06003474 RID: 13428 RVA: 0x000E3708 File Offset: 0x000E2708
		private IDictionary AllFiltersDictionary
		{
			get
			{
				if (this._allFiltersDictionary == null)
				{
					this._allFiltersDictionary = new ListDictionary(StringComparer.OrdinalIgnoreCase);
					foreach (object obj in this._filterTable.Values)
					{
						FilteredAttributeDictionary filteredAttributeDictionary = (FilteredAttributeDictionary)obj;
						foreach (object obj2 in ((IEnumerable)filteredAttributeDictionary))
						{
							DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
							this._allFiltersDictionary[Util.CreateFilteredName(filteredAttributeDictionary.Filter, dictionaryEntry.Key.ToString())] = dictionaryEntry.Value;
						}
					}
				}
				return this._allFiltersDictionary;
			}
		}

		// Token: 0x06003475 RID: 13429 RVA: 0x000E37EC File Offset: 0x000E27EC
		public void AddFilteredAttribute(string filter, string name, string value)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (filter == null)
			{
				filter = string.Empty;
			}
			if (this._allFiltersDictionary != null)
			{
				this._allFiltersDictionary.Add(Util.CreateFilteredName(filter, name), value);
			}
			FilteredAttributeDictionary filteredAttributeDictionary = (FilteredAttributeDictionary)this._filterTable[filter];
			if (filteredAttributeDictionary == null)
			{
				filteredAttributeDictionary = new FilteredAttributeDictionary(this, filter);
				this._filterTable[filter] = filteredAttributeDictionary;
			}
			filteredAttributeDictionary.Data.Add(name, value);
		}

		// Token: 0x06003476 RID: 13430 RVA: 0x000E3878 File Offset: 0x000E2878
		public void ClearFilter(string filter)
		{
			if (filter == null)
			{
				filter = string.Empty;
			}
			if (this._allFiltersDictionary != null)
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this._allFiltersDictionary.Keys)
				{
					string text = (string)obj;
					string text2;
					string s = Util.ParsePropertyDeviceFilter(text, out text2);
					if (StringUtil.EqualsIgnoreCase(s, filter))
					{
						arrayList.Add(text);
					}
				}
				foreach (object obj2 in arrayList)
				{
					string key = (string)obj2;
					this._allFiltersDictionary.Remove(key);
				}
			}
			this._filterTable.Remove(filter);
		}

		// Token: 0x06003477 RID: 13431 RVA: 0x000E3968 File Offset: 0x000E2968
		public ICollection GetFilteredAttributeDictionaries()
		{
			return this._filterTable.Values;
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x000E3978 File Offset: 0x000E2978
		public void RemoveFilteredAttribute(string filter, string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("name");
			}
			if (filter == null)
			{
				filter = string.Empty;
			}
			if (this._allFiltersDictionary != null)
			{
				this._allFiltersDictionary.Remove(Util.CreateFilteredName(filter, name));
			}
			FilteredAttributeDictionary filteredAttributeDictionary = (FilteredAttributeDictionary)this._filterTable[filter];
			if (filteredAttributeDictionary != null)
			{
				filteredAttributeDictionary.Data.Remove(name);
			}
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x000E39E0 File Offset: 0x000E29E0
		public void ReplaceFilteredAttribute(string filter, string name, string value)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("name");
			}
			if (filter == null)
			{
				filter = string.Empty;
			}
			if (this._allFiltersDictionary != null)
			{
				this._allFiltersDictionary[Util.CreateFilteredName(filter, name)] = value;
			}
			FilteredAttributeDictionary filteredAttributeDictionary = (FilteredAttributeDictionary)this._filterTable[filter];
			if (filteredAttributeDictionary == null)
			{
				filteredAttributeDictionary = new FilteredAttributeDictionary(this, filter);
				this._filterTable[filter] = filteredAttributeDictionary;
			}
			filteredAttributeDictionary.Data[name] = value;
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x0600347A RID: 13434 RVA: 0x000E3A5C File Offset: 0x000E2A5C
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x0600347B RID: 13435 RVA: 0x000E3A5F File Offset: 0x000E2A5F
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BB6 RID: 2998
		object IDictionary.this[object key]
		{
			get
			{
				return this.AllFiltersDictionary[key];
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				string name;
				string filter = Util.ParsePropertyDeviceFilter(key.ToString(), out name);
				this.ReplaceFilteredAttribute(filter, name, value.ToString());
			}
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x0600347E RID: 13438 RVA: 0x000E3AA7 File Offset: 0x000E2AA7
		ICollection IDictionary.Keys
		{
			get
			{
				return this.AllFiltersDictionary.Keys;
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x0600347F RID: 13439 RVA: 0x000E3AB4 File Offset: 0x000E2AB4
		ICollection IDictionary.Values
		{
			get
			{
				return this.AllFiltersDictionary.Values;
			}
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x000E3AC4 File Offset: 0x000E2AC4
		void IDictionary.Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (value == null)
			{
				value = string.Empty;
			}
			string name;
			string filter = Util.ParsePropertyDeviceFilter(key.ToString(), out name);
			this.AddFilteredAttribute(filter, name, value.ToString());
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x000E3B05 File Offset: 0x000E2B05
		bool IDictionary.Contains(object key)
		{
			return this.AllFiltersDictionary.Contains(key);
		}

		// Token: 0x06003482 RID: 13442 RVA: 0x000E3B13 File Offset: 0x000E2B13
		void IDictionary.Clear()
		{
			this.AllFiltersDictionary.Clear();
			this._filterTable.Clear();
		}

		// Token: 0x06003483 RID: 13443 RVA: 0x000E3B2B File Offset: 0x000E2B2B
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return this.AllFiltersDictionary.GetEnumerator();
		}

		// Token: 0x06003484 RID: 13444 RVA: 0x000E3B38 File Offset: 0x000E2B38
		void IDictionary.Remove(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			string name;
			string filter = Util.ParsePropertyDeviceFilter(key.ToString(), out name);
			this.RemoveFilteredAttribute(filter, name);
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06003485 RID: 13445 RVA: 0x000E3B69 File Offset: 0x000E2B69
		int ICollection.Count
		{
			get
			{
				return this.AllFiltersDictionary.Count;
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06003486 RID: 13446 RVA: 0x000E3B76 File Offset: 0x000E2B76
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.AllFiltersDictionary.IsSynchronized;
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06003487 RID: 13447 RVA: 0x000E3B83 File Offset: 0x000E2B83
		object ICollection.SyncRoot
		{
			get
			{
				return this.AllFiltersDictionary.SyncRoot;
			}
		}

		// Token: 0x06003488 RID: 13448 RVA: 0x000E3B90 File Offset: 0x000E2B90
		void ICollection.CopyTo(Array array, int index)
		{
			this.AllFiltersDictionary.CopyTo(array, index);
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x000E3B9F File Offset: 0x000E2B9F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.AllFiltersDictionary.GetEnumerator();
		}

		// Token: 0x040024C0 RID: 9408
		private IDictionary _filterTable;

		// Token: 0x040024C1 RID: 9409
		private IDictionary _allFiltersDictionary;
	}
}
