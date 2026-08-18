using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004C3 RID: 1219
	public abstract class EntitySetBase : MetadataItem, INamedDataModelItem
	{
		// Token: 0x06002CE5 RID: 11493 RVA: 0x000DA819 File Offset: 0x000D8A19
		internal EntitySetBase()
		{
		}

		// Token: 0x06002CE6 RID: 11494 RVA: 0x000DA824 File Offset: 0x000D8A24
		internal EntitySetBase(string name, string schema, string table, string definingQuery, EntityTypeBase entityType)
		{
			Check.NotNull<EntityTypeBase>(entityType, "entityType");
			Check.NotEmpty(name, "name");
			this._name = name;
			this._schema = schema;
			this._table = table;
			this._definingQuery = definingQuery;
			this.ElementType = entityType;
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06002CE7 RID: 11495 RVA: 0x000DA875 File Offset: 0x000D8A75
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EntitySetBase;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06002CE8 RID: 11496 RVA: 0x000DA878 File Offset: 0x000D8A78
		string INamedDataModelItem.Identity
		{
			get
			{
				return this.Identity;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06002CE9 RID: 11497 RVA: 0x000DA880 File Offset: 0x000D8A80
		internal override string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06002CEA RID: 11498 RVA: 0x000DA888 File Offset: 0x000D8A88
		// (set) Token: 0x06002CEB RID: 11499 RVA: 0x000DA890 File Offset: 0x000D8A90
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string DefiningQuery
		{
			get
			{
				return this._definingQuery;
			}
			internal set
			{
				Check.NotEmpty(value, "value");
				Util.ThrowIfReadOnly(this);
				this._definingQuery = value;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06002CEC RID: 11500 RVA: 0x000DA8AB File Offset: 0x000D8AAB
		// (set) Token: 0x06002CED RID: 11501 RVA: 0x000DA8B4 File Offset: 0x000D8AB4
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public virtual string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				Check.NotEmpty(value, "value");
				Util.ThrowIfReadOnly(this);
				if (!string.Equals(this._name, value, StringComparison.Ordinal))
				{
					string identity = this.Identity;
					this._name = value;
					if (this._entityContainer != null)
					{
						this._entityContainer.NotifyItemIdentityChanged(this, identity);
					}
				}
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06002CEE RID: 11502 RVA: 0x000DA905 File Offset: 0x000D8B05
		public virtual EntityContainer EntityContainer
		{
			get
			{
				return this._entityContainer;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06002CEF RID: 11503 RVA: 0x000DA90D File Offset: 0x000D8B0D
		// (set) Token: 0x06002CF0 RID: 11504 RVA: 0x000DA915 File Offset: 0x000D8B15
		[MetadataProperty(BuiltInTypeKind.EntityTypeBase, false)]
		public EntityTypeBase ElementType
		{
			get
			{
				return this._elementType;
			}
			internal set
			{
				Check.NotNull<EntityTypeBase>(value, "value");
				Util.ThrowIfReadOnly(this);
				this._elementType = value;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06002CF1 RID: 11505 RVA: 0x000DA930 File Offset: 0x000D8B30
		// (set) Token: 0x06002CF2 RID: 11506 RVA: 0x000DA938 File Offset: 0x000D8B38
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Table
		{
			get
			{
				return this._table;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				this._table = value;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06002CF3 RID: 11507 RVA: 0x000DA947 File Offset: 0x000D8B47
		// (set) Token: 0x06002CF4 RID: 11508 RVA: 0x000DA94F File Offset: 0x000D8B4F
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Schema
		{
			get
			{
				return this._schema;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				this._schema = value;
			}
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x000DA95E File Offset: 0x000D8B5E
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x000DA968 File Offset: 0x000D8B68
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				EntityTypeBase elementType = this.ElementType;
				if (elementType != null)
				{
					elementType.SetReadOnly();
				}
			}
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x000DA993 File Offset: 0x000D8B93
		internal void ChangeEntityContainerWithoutCollectionFixup(EntityContainer newEntityContainer)
		{
			this._entityContainer = newEntityContainer;
		}

		// Token: 0x04001086 RID: 4230
		private EntityContainer _entityContainer;

		// Token: 0x04001087 RID: 4231
		private string _name;

		// Token: 0x04001088 RID: 4232
		private EntityTypeBase _elementType;

		// Token: 0x04001089 RID: 4233
		private string _table;

		// Token: 0x0400108A RID: 4234
		private string _schema;

		// Token: 0x0400108B RID: 4235
		private string _definingQuery;
	}
}
