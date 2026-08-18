using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Diagnostics;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003DB RID: 987
	public class EntitySetMapping : EntitySetBaseMapping
	{
		// Token: 0x06002417 RID: 9239 RVA: 0x000A6638 File Offset: 0x000A4838
		public EntitySetMapping(EntitySet entitySet, EntityContainerMapping containerMapping) : base(containerMapping)
		{
			Check.NotNull<EntitySet>(entitySet, "entitySet");
			this._entitySet = entitySet;
			this._entityTypeMappings = new List<EntityTypeMapping>();
			this._modificationFunctionMappings = new List<EntityTypeModificationFunctionMapping>();
			this._implicitlyMappedAssociationSetEnds = new Lazy<List<AssociationSetEnd>>(new Func<List<AssociationSetEnd>>(this.InitializeImplicitlyMappedAssociationSetEnds));
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06002418 RID: 9240 RVA: 0x000A668C File Offset: 0x000A488C
		public EntitySet EntitySet
		{
			get
			{
				return this._entitySet;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06002419 RID: 9241 RVA: 0x000A6694 File Offset: 0x000A4894
		internal override EntitySetBase Set
		{
			get
			{
				return this.EntitySet;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600241A RID: 9242 RVA: 0x000A669C File Offset: 0x000A489C
		public ReadOnlyCollection<EntityTypeMapping> EntityTypeMappings
		{
			get
			{
				return new ReadOnlyCollection<EntityTypeMapping>(this._entityTypeMappings);
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x0600241B RID: 9243 RVA: 0x000A66A9 File Offset: 0x000A48A9
		internal override IEnumerable<TypeMapping> TypeMappings
		{
			get
			{
				return this._entityTypeMappings;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x0600241C RID: 9244 RVA: 0x000A66B1 File Offset: 0x000A48B1
		public ReadOnlyCollection<EntityTypeModificationFunctionMapping> ModificationFunctionMappings
		{
			get
			{
				return new ReadOnlyCollection<EntityTypeModificationFunctionMapping>(this._modificationFunctionMappings);
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x0600241D RID: 9245 RVA: 0x000A66BE File Offset: 0x000A48BE
		internal IEnumerable<AssociationSetEnd> ImplicitlyMappedAssociationSetEnds
		{
			get
			{
				return this._implicitlyMappedAssociationSetEnds.Value;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x0600241E RID: 9246 RVA: 0x000A66CB File Offset: 0x000A48CB
		internal override bool HasNoContent
		{
			get
			{
				return this._modificationFunctionMappings.Count == 0 && base.HasNoContent;
			}
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x000A66E2 File Offset: 0x000A48E2
		public void AddTypeMapping(EntityTypeMapping typeMapping)
		{
			Check.NotNull<EntityTypeMapping>(typeMapping, "typeMapping");
			base.ThrowIfReadOnly();
			this._entityTypeMappings.Add(typeMapping);
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x000A6702 File Offset: 0x000A4902
		public void RemoveTypeMapping(EntityTypeMapping typeMapping)
		{
			Check.NotNull<EntityTypeMapping>(typeMapping, "typeMapping");
			base.ThrowIfReadOnly();
			this._entityTypeMappings.Remove(typeMapping);
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x000A6723 File Offset: 0x000A4923
		internal void ClearModificationFunctionMappings()
		{
			this._modificationFunctionMappings.Clear();
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x000A6730 File Offset: 0x000A4930
		public void AddModificationFunctionMapping(EntityTypeModificationFunctionMapping modificationFunctionMapping)
		{
			Check.NotNull<EntityTypeModificationFunctionMapping>(modificationFunctionMapping, "modificationFunctionMapping");
			base.ThrowIfReadOnly();
			this._modificationFunctionMappings.Add(modificationFunctionMapping);
			if (this._implicitlyMappedAssociationSetEnds.IsValueCreated)
			{
				this._implicitlyMappedAssociationSetEnds = new Lazy<List<AssociationSetEnd>>(new Func<List<AssociationSetEnd>>(this.InitializeImplicitlyMappedAssociationSetEnds));
			}
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x000A6780 File Offset: 0x000A4980
		public void RemoveModificationFunctionMapping(EntityTypeModificationFunctionMapping modificationFunctionMapping)
		{
			Check.NotNull<EntityTypeModificationFunctionMapping>(modificationFunctionMapping, "modificationFunctionMapping");
			base.ThrowIfReadOnly();
			this._modificationFunctionMappings.Remove(modificationFunctionMapping);
			if (this._implicitlyMappedAssociationSetEnds.IsValueCreated)
			{
				this._implicitlyMappedAssociationSetEnds = new Lazy<List<AssociationSetEnd>>(new Func<List<AssociationSetEnd>>(this.InitializeImplicitlyMappedAssociationSetEnds));
			}
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x000A67D0 File Offset: 0x000A49D0
		internal override void SetReadOnly()
		{
			this._entityTypeMappings.TrimExcess();
			this._modificationFunctionMappings.TrimExcess();
			if (this._implicitlyMappedAssociationSetEnds.IsValueCreated)
			{
				this._implicitlyMappedAssociationSetEnds.Value.TrimExcess();
			}
			MappingItem.SetReadOnly(this._entityTypeMappings);
			MappingItem.SetReadOnly(this._modificationFunctionMappings);
			base.SetReadOnly();
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x000A682C File Offset: 0x000A4A2C
		[Conditional("DEBUG")]
		private void AssertModificationFunctionMappingInvariants(EntityTypeModificationFunctionMapping modificationFunctionMapping)
		{
			foreach (EntityTypeModificationFunctionMapping entityTypeModificationFunctionMapping in this._modificationFunctionMappings)
			{
			}
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x000A6878 File Offset: 0x000A4A78
		private List<AssociationSetEnd> InitializeImplicitlyMappedAssociationSetEnds()
		{
			List<AssociationSetEnd> list = new List<AssociationSetEnd>();
			foreach (EntityTypeModificationFunctionMapping entityTypeModificationFunctionMapping in this._modificationFunctionMappings)
			{
				if (entityTypeModificationFunctionMapping.DeleteFunctionMapping != null)
				{
					list.AddRange(entityTypeModificationFunctionMapping.DeleteFunctionMapping.CollocatedAssociationSetEnds);
				}
				if (entityTypeModificationFunctionMapping.InsertFunctionMapping != null)
				{
					list.AddRange(entityTypeModificationFunctionMapping.InsertFunctionMapping.CollocatedAssociationSetEnds);
				}
				if (entityTypeModificationFunctionMapping.UpdateFunctionMapping != null)
				{
					list.AddRange(entityTypeModificationFunctionMapping.UpdateFunctionMapping.CollocatedAssociationSetEnds);
				}
			}
			if (base.IsReadOnly)
			{
				list.TrimExcess();
			}
			return list;
		}

		// Token: 0x04000CAD RID: 3245
		private readonly EntitySet _entitySet;

		// Token: 0x04000CAE RID: 3246
		private readonly List<EntityTypeMapping> _entityTypeMappings;

		// Token: 0x04000CAF RID: 3247
		private readonly List<EntityTypeModificationFunctionMapping> _modificationFunctionMappings;

		// Token: 0x04000CB0 RID: 3248
		private Lazy<List<AssociationSetEnd>> _implicitlyMappedAssociationSetEnds;
	}
}
