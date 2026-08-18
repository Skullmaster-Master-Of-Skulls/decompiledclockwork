using System;

namespace System.Configuration
{
	// Token: 0x02000022 RID: 34
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class ConfigurationCollectionAttribute : Attribute
	{
		// Token: 0x06000141 RID: 321 RVA: 0x0000989C File Offset: 0x00007A9C
		public ConfigurationCollectionAttribute(Type itemType)
		{
			if (itemType == null)
			{
				throw new ArgumentNullException("itemType");
			}
			this._itemType = itemType;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000098C6 File Offset: 0x00007AC6
		public Type ItemType
		{
			get
			{
				return this._itemType;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000098CE File Offset: 0x00007ACE
		// (set) Token: 0x06000144 RID: 324 RVA: 0x000098E4 File Offset: 0x00007AE4
		public string AddItemName
		{
			get
			{
				if (this._addItemName == null)
				{
					return "add";
				}
				return this._addItemName;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = null;
				}
				this._addItemName = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000098F8 File Offset: 0x00007AF8
		// (set) Token: 0x06000146 RID: 326 RVA: 0x0000990E File Offset: 0x00007B0E
		public string RemoveItemName
		{
			get
			{
				if (this._removeItemName == null)
				{
					return "remove";
				}
				return this._removeItemName;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = null;
				}
				this._removeItemName = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00009922 File Offset: 0x00007B22
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00009938 File Offset: 0x00007B38
		public string ClearItemsName
		{
			get
			{
				if (this._clearItemsName == null)
				{
					return "clear";
				}
				return this._clearItemsName;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = null;
				}
				this._clearItemsName = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0000994C File Offset: 0x00007B4C
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00009954 File Offset: 0x00007B54
		public ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return this._collectionType;
			}
			set
			{
				this._collectionType = value;
			}
		}

		// Token: 0x0400018C RID: 396
		private string _addItemName;

		// Token: 0x0400018D RID: 397
		private string _removeItemName;

		// Token: 0x0400018E RID: 398
		private string _clearItemsName;

		// Token: 0x0400018F RID: 399
		private Type _itemType;

		// Token: 0x04000190 RID: 400
		private ConfigurationElementCollectionType _collectionType = ConfigurationElementCollectionType.AddRemoveClearMap;
	}
}
