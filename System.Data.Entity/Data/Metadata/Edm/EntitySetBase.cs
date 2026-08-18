using System;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001CF RID: 463
	public abstract class EntitySetBase : MetadataItem
	{
		// Token: 0x06001F96 RID: 8086 RVA: 0x0006EA38 File Offset: 0x0006CC38
		internal EntitySetBase(string name, string schema, string table, string definingQuery, EntityTypeBase entityType)
		{
			EntityUtil.GenericCheckArgumentNull<EntityTypeBase>(entityType, "entityType");
			EntityUtil.CheckStringArgument(name, "name");
			this._name = name;
			this._schema = schema;
			this._table = table;
			this._definingQuery = definingQuery;
			this.ElementType = entityType;
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001F97 RID: 8087 RVA: 0x0003C2A0 File Offset: 0x0003A4A0
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EntitySetBase;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001F98 RID: 8088 RVA: 0x0006EA88 File Offset: 0x0006CC88
		internal override string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001F99 RID: 8089 RVA: 0x0006EA90 File Offset: 0x0006CC90
		// (set) Token: 0x06001F9A RID: 8090 RVA: 0x0006EA98 File Offset: 0x0006CC98
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		internal string DefiningQuery
		{
			get
			{
				return this._definingQuery;
			}
			set
			{
				this._definingQuery = value;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001F9B RID: 8091 RVA: 0x0006EAA1 File Offset: 0x0006CCA1
		// (set) Token: 0x06001F9C RID: 8092 RVA: 0x0006EAA9 File Offset: 0x0006CCA9
		internal string CachedProviderSql
		{
			get
			{
				return this._cachedProviderSql;
			}
			set
			{
				this._cachedProviderSql = value;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001F9D RID: 8093 RVA: 0x0006EAB2 File Offset: 0x0006CCB2
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001F9E RID: 8094 RVA: 0x0006EABA File Offset: 0x0006CCBA
		public EntityContainer EntityContainer
		{
			get
			{
				return this._entityContainer;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001F9F RID: 8095 RVA: 0x0006EAC2 File Offset: 0x0006CCC2
		// (set) Token: 0x06001FA0 RID: 8096 RVA: 0x0006EACA File Offset: 0x0006CCCA
		[MetadataProperty(BuiltInTypeKind.EntityTypeBase, false)]
		public EntityTypeBase ElementType
		{
			get
			{
				return this._elementType;
			}
			internal set
			{
				EntityUtil.GenericCheckArgumentNull<EntityTypeBase>(value, "value");
				Util.ThrowIfReadOnly(this);
				this._elementType = value;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001FA1 RID: 8097 RVA: 0x0006EAE5 File Offset: 0x0006CCE5
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		internal string Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001FA2 RID: 8098 RVA: 0x0006EAED File Offset: 0x0006CCED
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		internal string Schema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x0006EA88 File Offset: 0x0006CC88
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x0006EAF8 File Offset: 0x0006CCF8
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

		// Token: 0x06001FA5 RID: 8101 RVA: 0x0006EB23 File Offset: 0x0006CD23
		internal void ChangeEntityContainerWithoutCollectionFixup(EntityContainer newEntityContainer)
		{
			this._entityContainer = newEntityContainer;
		}

		// Token: 0x04000DF6 RID: 3574
		private EntityContainer _entityContainer;

		// Token: 0x04000DF7 RID: 3575
		private string _name;

		// Token: 0x04000DF8 RID: 3576
		private EntityTypeBase _elementType;

		// Token: 0x04000DF9 RID: 3577
		private string _table;

		// Token: 0x04000DFA RID: 3578
		private string _schema;

		// Token: 0x04000DFB RID: 3579
		private string _definingQuery;

		// Token: 0x04000DFC RID: 3580
		private string _cachedProviderSql;
	}
}
