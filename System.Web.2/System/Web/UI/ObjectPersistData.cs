using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Web.UI
{
	// Token: 0x020002CA RID: 714
	public class ObjectPersistData
	{
		// Token: 0x0600201E RID: 8222 RVA: 0x00066458 File Offset: 0x00064658
		public ObjectPersistData(ControlBuilder builder, IDictionary builtObjects)
		{
			this._objectType = builder.ControlType;
			this._localize = builder.Localize;
			this._resourceKey = builder.GetResourceKey();
			this._builtObjects = builtObjects;
			if (typeof(ICollection).IsAssignableFrom(this._objectType))
			{
				this._isCollection = true;
			}
			this._collectionItems = new ArrayList();
			this._propertyTableByFilter = new HybridDictionary(true);
			this._propertyTableByProperty = new HybridDictionary(true);
			this._allPropertyEntries = new ArrayList();
			this._eventEntries = new ArrayList();
			foreach (object obj in builder.SimplePropertyEntries)
			{
				PropertyEntry entry = (PropertyEntry)obj;
				this.AddPropertyEntry(entry);
			}
			foreach (object obj2 in builder.ComplexPropertyEntries)
			{
				PropertyEntry entry2 = (PropertyEntry)obj2;
				this.AddPropertyEntry(entry2);
			}
			foreach (object obj3 in builder.TemplatePropertyEntries)
			{
				PropertyEntry entry3 = (PropertyEntry)obj3;
				this.AddPropertyEntry(entry3);
			}
			foreach (object obj4 in builder.BoundPropertyEntries)
			{
				PropertyEntry entry4 = (PropertyEntry)obj4;
				this.AddPropertyEntry(entry4);
			}
			foreach (object obj5 in builder.EventEntries)
			{
				EventEntry entry5 = (EventEntry)obj5;
				this.AddEventEntry(entry5);
			}
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x0600201F RID: 8223 RVA: 0x00066670 File Offset: 0x00064870
		public ICollection AllPropertyEntries
		{
			get
			{
				return this._allPropertyEntries;
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06002020 RID: 8224 RVA: 0x00066678 File Offset: 0x00064878
		public IDictionary BuiltObjects
		{
			get
			{
				return this._builtObjects;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06002021 RID: 8225 RVA: 0x00066680 File Offset: 0x00064880
		public ICollection CollectionItems
		{
			get
			{
				return this._collectionItems;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06002022 RID: 8226 RVA: 0x00066688 File Offset: 0x00064888
		public ICollection EventEntries
		{
			get
			{
				return this._eventEntries;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06002023 RID: 8227 RVA: 0x00066690 File Offset: 0x00064890
		public bool IsCollection
		{
			get
			{
				return this._isCollection;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06002024 RID: 8228 RVA: 0x00066698 File Offset: 0x00064898
		public bool Localize
		{
			get
			{
				return this._localize;
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06002025 RID: 8229 RVA: 0x000666A0 File Offset: 0x000648A0
		public Type ObjectType
		{
			get
			{
				return this._objectType;
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06002026 RID: 8230 RVA: 0x000666A8 File Offset: 0x000648A8
		public string ResourceKey
		{
			get
			{
				return this._resourceKey;
			}
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x000666B0 File Offset: 0x000648B0
		private void AddPropertyEntry(PropertyEntry entry)
		{
			if (this._isCollection && entry is ComplexPropertyEntry && ((ComplexPropertyEntry)entry).IsCollectionItem)
			{
				this._collectionItems.Add(entry);
			}
			else
			{
				IDictionary dictionary = (IDictionary)this._propertyTableByFilter[entry.Filter];
				if (dictionary == null)
				{
					dictionary = new HybridDictionary(true);
					this._propertyTableByFilter[entry.Filter] = dictionary;
				}
				dictionary[entry.Name] = entry;
				ArrayList arrayList = (ArrayList)this._propertyTableByProperty[entry.Name];
				if (arrayList == null)
				{
					arrayList = new ArrayList();
					this._propertyTableByProperty[entry.Name] = arrayList;
				}
				arrayList.Add(entry);
			}
			this._allPropertyEntries.Add(entry);
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x00066770 File Offset: 0x00064970
		private void AddEventEntry(EventEntry entry)
		{
			this._eventEntries.Add(entry);
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x00066780 File Offset: 0x00064980
		public void AddToObjectControlBuilderTable(IDictionary table)
		{
			if (this._builtObjects != null)
			{
				foreach (object obj in this._builtObjects)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					table[dictionaryEntry.Key] = dictionaryEntry.Value;
				}
			}
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x000667F0 File Offset: 0x000649F0
		public PropertyEntry GetFilteredProperty(string filter, string name)
		{
			IDictionary filteredProperties = this.GetFilteredProperties(filter);
			if (filteredProperties != null)
			{
				return (PropertyEntry)filteredProperties[name];
			}
			return null;
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x00066816 File Offset: 0x00064A16
		public IDictionary GetFilteredProperties(string filter)
		{
			return (IDictionary)this._propertyTableByFilter[filter];
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x0006682C File Offset: 0x00064A2C
		public ICollection GetPropertyAllFilters(string name)
		{
			ICollection collection = (ICollection)this._propertyTableByProperty[name];
			if (collection == null)
			{
				return new ArrayList();
			}
			return collection;
		}

		// Token: 0x04001AD2 RID: 6866
		private Type _objectType;

		// Token: 0x04001AD3 RID: 6867
		private bool _isCollection;

		// Token: 0x04001AD4 RID: 6868
		private ArrayList _collectionItems;

		// Token: 0x04001AD5 RID: 6869
		private bool _localize;

		// Token: 0x04001AD6 RID: 6870
		private string _resourceKey;

		// Token: 0x04001AD7 RID: 6871
		private IDictionary _propertyTableByFilter;

		// Token: 0x04001AD8 RID: 6872
		private IDictionary _propertyTableByProperty;

		// Token: 0x04001AD9 RID: 6873
		private ArrayList _allPropertyEntries;

		// Token: 0x04001ADA RID: 6874
		private ArrayList _eventEntries;

		// Token: 0x04001ADB RID: 6875
		private IDictionary _builtObjects;
	}
}
