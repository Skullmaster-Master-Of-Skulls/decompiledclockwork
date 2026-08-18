using System;
using System.Collections;

namespace System.Configuration
{
	// Token: 0x02000034 RID: 52
	public class ConfigurationPropertyCollection : ICollection, IEnumerable
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0001144F File Offset: 0x0000F64F
		public int Count
		{
			get
			{
				return this._items.Count;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600027D RID: 637 RVA: 0x00008751 File Offset: 0x00006951
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0001145C File Offset: 0x0000F65C
		public object SyncRoot
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600027F RID: 639 RVA: 0x00011464 File Offset: 0x0000F664
		internal ConfigurationProperty DefaultCollectionProperty
		{
			get
			{
				return this[ConfigurationProperty.DefaultCollectionPropertyName];
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00011471 File Offset: 0x0000F671
		void ICollection.CopyTo(Array array, int index)
		{
			this._items.CopyTo(array, index);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000DD40 File Offset: 0x0000BF40
		public void CopyTo(ConfigurationProperty[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00011480 File Offset: 0x0000F680
		public IEnumerator GetEnumerator()
		{
			return this._items.GetEnumerator();
		}

		// Token: 0x170000B1 RID: 177
		public ConfigurationProperty this[string name]
		{
			get
			{
				for (int i = 0; i < this._items.Count; i++)
				{
					ConfigurationProperty configurationProperty = (ConfigurationProperty)this._items[i];
					if (configurationProperty.Name == name)
					{
						return (ConfigurationProperty)this._items[i];
					}
				}
				return null;
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000114E8 File Offset: 0x0000F6E8
		public bool Contains(string name)
		{
			for (int i = 0; i < this._items.Count; i++)
			{
				ConfigurationProperty configurationProperty = (ConfigurationProperty)this._items[i];
				if (configurationProperty.Name == name)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0001152E File Offset: 0x0000F72E
		public void Add(ConfigurationProperty property)
		{
			if (!this.Contains(property.Name))
			{
				this._items.Add(property);
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0001154C File Offset: 0x0000F74C
		public bool Remove(string name)
		{
			for (int i = 0; i < this._items.Count; i++)
			{
				ConfigurationProperty configurationProperty = (ConfigurationProperty)this._items[i];
				if (configurationProperty.Name == name)
				{
					this._items.RemoveAt(i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0001159E File Offset: 0x0000F79E
		public void Clear()
		{
			this._items.Clear();
		}

		// Token: 0x040001F8 RID: 504
		private ArrayList _items = new ArrayList();
	}
}
