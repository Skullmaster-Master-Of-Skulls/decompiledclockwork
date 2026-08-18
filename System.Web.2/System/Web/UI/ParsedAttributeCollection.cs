using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x020002E5 RID: 741
	internal sealed class ParsedAttributeCollection : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x0600226F RID: 8815 RVA: 0x0007067B File Offset: 0x0006E87B
		internal ParsedAttributeCollection()
		{
			this._filterTable = new ListDictionary(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06002270 RID: 8816 RVA: 0x00070694 File Offset: 0x0006E894
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

		// Token: 0x06002271 RID: 8817 RVA: 0x00070778 File Offset: 0x0006E978
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

		// Token: 0x06002272 RID: 8818 RVA: 0x00070804 File Offset: 0x0006EA04
		public void AddAttributeValuePositionInformation(string name, int line, int column)
		{
			Pair value = new Pair(line, column);
			this.AttributeValuePositionsDictionary[name] = value;
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06002273 RID: 8819 RVA: 0x00070830 File Offset: 0x0006EA30
		public IDictionary<string, Pair> AttributeValuePositionsDictionary
		{
			get
			{
				if (this._attributeValuePositionInfo == null)
				{
					this._attributeValuePositionInfo = new Dictionary<string, Pair>(StringComparer.OrdinalIgnoreCase);
				}
				return this._attributeValuePositionInfo;
			}
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x00070850 File Offset: 0x0006EA50
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

		// Token: 0x06002275 RID: 8821 RVA: 0x00070940 File Offset: 0x0006EB40
		public ICollection GetFilteredAttributeDictionaries()
		{
			return this._filterTable.Values;
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x00070950 File Offset: 0x0006EB50
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

		// Token: 0x06002277 RID: 8823 RVA: 0x000709B8 File Offset: 0x0006EBB8
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

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06002278 RID: 8824 RVA: 0x00007722 File Offset: 0x00005922
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06002279 RID: 8825 RVA: 0x00007722 File Offset: 0x00005922
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009AC RID: 2476
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

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x0600227C RID: 8828 RVA: 0x00070A7B File Offset: 0x0006EC7B
		ICollection IDictionary.Keys
		{
			get
			{
				return this.AllFiltersDictionary.Keys;
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x0600227D RID: 8829 RVA: 0x00070A88 File Offset: 0x0006EC88
		ICollection IDictionary.Values
		{
			get
			{
				return this.AllFiltersDictionary.Values;
			}
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x00070A98 File Offset: 0x0006EC98
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

		// Token: 0x0600227F RID: 8831 RVA: 0x00070AD9 File Offset: 0x0006ECD9
		bool IDictionary.Contains(object key)
		{
			return this.AllFiltersDictionary.Contains(key);
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x00070AE7 File Offset: 0x0006ECE7
		void IDictionary.Clear()
		{
			this.AllFiltersDictionary.Clear();
			this._filterTable.Clear();
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x00070AFF File Offset: 0x0006ECFF
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return this.AllFiltersDictionary.GetEnumerator();
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x00070B0C File Offset: 0x0006ED0C
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

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06002283 RID: 8835 RVA: 0x00070B3D File Offset: 0x0006ED3D
		int ICollection.Count
		{
			get
			{
				return this.AllFiltersDictionary.Count;
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06002284 RID: 8836 RVA: 0x00070B4A File Offset: 0x0006ED4A
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.AllFiltersDictionary.IsSynchronized;
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06002285 RID: 8837 RVA: 0x00070B57 File Offset: 0x0006ED57
		object ICollection.SyncRoot
		{
			get
			{
				return this.AllFiltersDictionary.SyncRoot;
			}
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x00070B64 File Offset: 0x0006ED64
		void ICollection.CopyTo(Array array, int index)
		{
			this.AllFiltersDictionary.CopyTo(array, index);
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x00070AFF File Offset: 0x0006ECFF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.AllFiltersDictionary.GetEnumerator();
		}

		// Token: 0x04001C43 RID: 7235
		private IDictionary _filterTable;

		// Token: 0x04001C44 RID: 7236
		private IDictionary _allFiltersDictionary;

		// Token: 0x04001C45 RID: 7237
		private IDictionary<string, Pair> _attributeValuePositionInfo;
	}
}
