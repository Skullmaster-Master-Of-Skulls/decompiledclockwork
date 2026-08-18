using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004DD RID: 1245
	public class EntityContainer : GlobalItem
	{
		// Token: 0x06002E0B RID: 11787 RVA: 0x000DD894 File Offset: 0x000DBA94
		internal EntityContainer()
		{
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x000DD8A8 File Offset: 0x000DBAA8
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public EntityContainer(string name, DataSpace dataSpace)
		{
			Check.NotEmpty(name, "name");
			this._name = name;
			this.DataSpace = dataSpace;
			this._baseEntitySets = new ReadOnlyMetadataCollection<EntitySetBase>(new EntitySetBaseCollection(this));
			this._functionImports = new ReadOnlyMetadataCollection<EdmFunction>(new MetadataCollection<EdmFunction>());
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06002E0D RID: 11789 RVA: 0x000DD901 File Offset: 0x000DBB01
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EntityContainer;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06002E0E RID: 11790 RVA: 0x000DD905 File Offset: 0x000DBB05
		internal override string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06002E0F RID: 11791 RVA: 0x000DD90D File Offset: 0x000DBB0D
		// (set) Token: 0x06002E10 RID: 11792 RVA: 0x000DD915 File Offset: 0x000DBB15
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
				this._name = value;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06002E11 RID: 11793 RVA: 0x000DD930 File Offset: 0x000DBB30
		[MetadataProperty(BuiltInTypeKind.EntitySetBase, true)]
		public ReadOnlyMetadataCollection<EntitySetBase> BaseEntitySets
		{
			get
			{
				return this._baseEntitySets;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06002E12 RID: 11794 RVA: 0x000DD938 File Offset: 0x000DBB38
		public ReadOnlyMetadataCollection<AssociationSet> AssociationSets
		{
			get
			{
				ReadOnlyMetadataCollection<AssociationSet> associationSetsCache = this._associationSetsCache;
				if (associationSetsCache == null)
				{
					lock (this._baseEntitySetsLock)
					{
						if (this._associationSetsCache == null)
						{
							this._baseEntitySets.SourceAccessed += this.ResetAssociationSetsCache;
							this._associationSetsCache = new FilteredReadOnlyMetadataCollection<AssociationSet, EntitySetBase>(this._baseEntitySets, new Predicate<EntitySetBase>(Helper.IsAssociationSet));
						}
						associationSetsCache = this._associationSetsCache;
					}
				}
				return associationSetsCache;
			}
		}

		// Token: 0x06002E13 RID: 11795 RVA: 0x000DD9C0 File Offset: 0x000DBBC0
		private void ResetAssociationSetsCache(object sender, EventArgs e)
		{
			if (this._associationSetsCache != null)
			{
				lock (this._baseEntitySetsLock)
				{
					if (this._associationSetsCache != null)
					{
						this._associationSetsCache = null;
						this._baseEntitySets.SourceAccessed -= this.ResetAssociationSetsCache;
					}
				}
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06002E14 RID: 11796 RVA: 0x000DDA28 File Offset: 0x000DBC28
		public ReadOnlyMetadataCollection<EntitySet> EntitySets
		{
			get
			{
				ReadOnlyMetadataCollection<EntitySet> entitySetsCache = this._entitySetsCache;
				if (entitySetsCache == null)
				{
					lock (this._baseEntitySetsLock)
					{
						if (this._entitySetsCache == null)
						{
							this._baseEntitySets.SourceAccessed += this.ResetEntitySetsCache;
							this._entitySetsCache = new FilteredReadOnlyMetadataCollection<EntitySet, EntitySetBase>(this._baseEntitySets, new Predicate<EntitySetBase>(Helper.IsEntitySet));
						}
						entitySetsCache = this._entitySetsCache;
					}
				}
				return entitySetsCache;
			}
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x000DDAB0 File Offset: 0x000DBCB0
		private void ResetEntitySetsCache(object sender, EventArgs e)
		{
			if (this._entitySetsCache != null)
			{
				lock (this._baseEntitySetsLock)
				{
					if (this._entitySetsCache != null)
					{
						this._entitySetsCache = null;
						this._baseEntitySets.SourceAccessed -= this.ResetEntitySetsCache;
					}
				}
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06002E16 RID: 11798 RVA: 0x000DDB18 File Offset: 0x000DBD18
		[MetadataProperty(BuiltInTypeKind.EdmFunction, true)]
		public ReadOnlyMetadataCollection<EdmFunction> FunctionImports
		{
			get
			{
				return this._functionImports;
			}
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x000DDB20 File Offset: 0x000DBD20
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				this.BaseEntitySets.Source.SetReadOnly();
				this.FunctionImports.Source.SetReadOnly();
			}
		}

		// Token: 0x06002E18 RID: 11800 RVA: 0x000DDB54 File Offset: 0x000DBD54
		public EntitySet GetEntitySetByName(string name, bool ignoreCase)
		{
			EntitySet entitySet = this.BaseEntitySets.GetValue(name, ignoreCase) as EntitySet;
			if (entitySet != null)
			{
				return entitySet;
			}
			throw new ArgumentException(Strings.InvalidEntitySetName(name));
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x000DDB84 File Offset: 0x000DBD84
		public bool TryGetEntitySetByName(string name, bool ignoreCase, out EntitySet entitySet)
		{
			Check.NotNull<string>(name, "name");
			EntitySetBase entitySetBase = null;
			entitySet = null;
			if (this.BaseEntitySets.TryGetValue(name, ignoreCase, out entitySetBase) && Helper.IsEntitySet(entitySetBase))
			{
				entitySet = (EntitySet)entitySetBase;
				return true;
			}
			return false;
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x000DDBC8 File Offset: 0x000DBDC8
		public RelationshipSet GetRelationshipSetByName(string name, bool ignoreCase)
		{
			RelationshipSet result;
			if (!this.TryGetRelationshipSetByName(name, ignoreCase, out result))
			{
				throw new ArgumentException(Strings.InvalidRelationshipSetName(name));
			}
			return result;
		}

		// Token: 0x06002E1B RID: 11803 RVA: 0x000DDBF0 File Offset: 0x000DBDF0
		public bool TryGetRelationshipSetByName(string name, bool ignoreCase, out RelationshipSet relationshipSet)
		{
			Check.NotNull<string>(name, "name");
			EntitySetBase entitySetBase = null;
			relationshipSet = null;
			if (this.BaseEntitySets.TryGetValue(name, ignoreCase, out entitySetBase) && Helper.IsRelationshipSet(entitySetBase))
			{
				relationshipSet = (RelationshipSet)entitySetBase;
				return true;
			}
			return false;
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x000DDC32 File Offset: 0x000DBE32
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06002E1D RID: 11805 RVA: 0x000DDC3A File Offset: 0x000DBE3A
		public void AddEntitySetBase(EntitySetBase entitySetBase)
		{
			Check.NotNull<EntitySetBase>(entitySetBase, "entitySetBase");
			Util.ThrowIfReadOnly(this);
			this._baseEntitySets.Source.Add(entitySetBase);
			entitySetBase.ChangeEntityContainerWithoutCollectionFixup(this);
		}

		// Token: 0x06002E1E RID: 11806 RVA: 0x000DDC66 File Offset: 0x000DBE66
		public void RemoveEntitySetBase(EntitySetBase entitySetBase)
		{
			Check.NotNull<EntitySetBase>(entitySetBase, "entitySetBase");
			Util.ThrowIfReadOnly(this);
			this._baseEntitySets.Source.Remove(entitySetBase);
			entitySetBase.ChangeEntityContainerWithoutCollectionFixup(null);
		}

		// Token: 0x06002E1F RID: 11807 RVA: 0x000DDC93 File Offset: 0x000DBE93
		public void AddFunctionImport(EdmFunction function)
		{
			Check.NotNull<EdmFunction>(function, "function");
			Util.ThrowIfReadOnly(this);
			if (!function.IsFunctionImport)
			{
				throw new ArgumentException(Strings.OnlyFunctionImportsCanBeAddedToEntityContainer(function.Name));
			}
			this._functionImports.Source.Add(function);
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x000DDCD4 File Offset: 0x000DBED4
		public static EntityContainer Create(string name, DataSpace dataSpace, IEnumerable<EntitySetBase> entitySets, IEnumerable<EdmFunction> functionImports, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			EntityContainer entityContainer = new EntityContainer(name, dataSpace);
			if (entitySets != null)
			{
				foreach (EntitySetBase entitySetBase in entitySets)
				{
					entityContainer.AddEntitySetBase(entitySetBase);
				}
			}
			if (functionImports != null)
			{
				foreach (EdmFunction edmFunction in functionImports)
				{
					if (!edmFunction.IsFunctionImport)
					{
						throw new ArgumentException(Strings.OnlyFunctionImportsCanBeAddedToEntityContainer(edmFunction.Name));
					}
					entityContainer.AddFunctionImport(edmFunction);
				}
			}
			if (metadataProperties != null)
			{
				entityContainer.AddMetadataProperties(metadataProperties.ToList<MetadataProperty>());
			}
			entityContainer.SetReadOnly();
			return entityContainer;
		}

		// Token: 0x06002E21 RID: 11809 RVA: 0x000DDDA4 File Offset: 0x000DBFA4
		internal virtual void NotifyItemIdentityChanged(EntitySetBase item, string initialIdentity)
		{
			this._baseEntitySets.Source.HandleIdentityChange(item, initialIdentity);
		}

		// Token: 0x04001198 RID: 4504
		private string _name;

		// Token: 0x04001199 RID: 4505
		private readonly ReadOnlyMetadataCollection<EntitySetBase> _baseEntitySets;

		// Token: 0x0400119A RID: 4506
		private readonly ReadOnlyMetadataCollection<EdmFunction> _functionImports;

		// Token: 0x0400119B RID: 4507
		private readonly object _baseEntitySetsLock = new object();

		// Token: 0x0400119C RID: 4508
		private ReadOnlyMetadataCollection<AssociationSet> _associationSetsCache;

		// Token: 0x0400119D RID: 4509
		private ReadOnlyMetadataCollection<EntitySet> _entitySetsCache;
	}
}
