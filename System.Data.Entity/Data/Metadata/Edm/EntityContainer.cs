using System;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001CD RID: 461
	public sealed class EntityContainer : GlobalItem
	{
		// Token: 0x06001F80 RID: 8064 RVA: 0x0006E6C8 File Offset: 0x0006C8C8
		internal EntityContainer(string name, DataSpace dataSpace)
		{
			EntityUtil.CheckStringArgument(name, "name");
			this._name = name;
			base.DataSpace = dataSpace;
			this._baseEntitySets = new ReadOnlyMetadataCollection<EntitySetBase>(new EntitySetBaseCollection(this));
			this._functionImports = new ReadOnlyMetadataCollection<EdmFunction>(new MetadataCollection<EdmFunction>());
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001F81 RID: 8065 RVA: 0x0006E715 File Offset: 0x0006C915
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EntityContainer;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001F82 RID: 8066 RVA: 0x0006E719 File Offset: 0x0006C919
		internal override string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001F83 RID: 8067 RVA: 0x0006E721 File Offset: 0x0006C921
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001F84 RID: 8068 RVA: 0x0006E729 File Offset: 0x0006C929
		[MetadataProperty(BuiltInTypeKind.EntitySetBase, true)]
		public ReadOnlyMetadataCollection<EntitySetBase> BaseEntitySets
		{
			get
			{
				return this._baseEntitySets;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001F85 RID: 8069 RVA: 0x0006E731 File Offset: 0x0006C931
		[MetadataProperty(BuiltInTypeKind.EdmFunction, true)]
		public ReadOnlyMetadataCollection<EdmFunction> FunctionImports
		{
			get
			{
				return this._functionImports;
			}
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x0006E739 File Offset: 0x0006C939
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				this.BaseEntitySets.Source.SetReadOnly();
				this.FunctionImports.Source.SetReadOnly();
			}
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x0006E76C File Offset: 0x0006C96C
		public EntitySet GetEntitySetByName(string name, bool ignoreCase)
		{
			EntitySet entitySet = this.BaseEntitySets.GetValue(name, ignoreCase) as EntitySet;
			if (entitySet != null)
			{
				return entitySet;
			}
			throw EntityUtil.InvalidEntitySetName(name);
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x0006E798 File Offset: 0x0006C998
		public bool TryGetEntitySetByName(string name, bool ignoreCase, out EntitySet entitySet)
		{
			EntityUtil.CheckArgumentNull<string>(name, "name");
			EntitySetBase entitySetBase = null;
			entitySet = null;
			if (this.BaseEntitySets.TryGetValue(name, ignoreCase, out entitySetBase) && Helper.IsEntitySet(entitySetBase))
			{
				entitySet = (EntitySet)entitySetBase;
				return true;
			}
			return false;
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x0006E7DC File Offset: 0x0006C9DC
		public RelationshipSet GetRelationshipSetByName(string name, bool ignoreCase)
		{
			RelationshipSet result;
			if (!this.TryGetRelationshipSetByName(name, ignoreCase, out result))
			{
				throw EntityUtil.InvalidRelationshipSetName(name);
			}
			return result;
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x0006E800 File Offset: 0x0006CA00
		public bool TryGetRelationshipSetByName(string name, bool ignoreCase, out RelationshipSet relationshipSet)
		{
			EntityUtil.CheckArgumentNull<string>(name, "name");
			EntitySetBase entitySetBase = null;
			relationshipSet = null;
			if (this.BaseEntitySets.TryGetValue(name, ignoreCase, out entitySetBase) && Helper.IsRelationshipSet(entitySetBase))
			{
				relationshipSet = (RelationshipSet)entitySetBase;
				return true;
			}
			return false;
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x0006E719 File Offset: 0x0006C919
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x0006E842 File Offset: 0x0006CA42
		internal void AddEntitySetBase(EntitySetBase entitySetBase)
		{
			this._baseEntitySets.Source.Add(entitySetBase);
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x0006E855 File Offset: 0x0006CA55
		internal void AddFunctionImport(EdmFunction function)
		{
			this._functionImports.Source.Add(function);
		}

		// Token: 0x04000DEF RID: 3567
		private readonly string _name;

		// Token: 0x04000DF0 RID: 3568
		private readonly ReadOnlyMetadataCollection<EntitySetBase> _baseEntitySets;

		// Token: 0x04000DF1 RID: 3569
		private readonly ReadOnlyMetadataCollection<EdmFunction> _functionImports;
	}
}
