using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Mapping.ViewGeneration.Validation;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003D8 RID: 984
	public class EntityContainerMapping : MappingBase
	{
		// Token: 0x060023DE RID: 9182 RVA: 0x000A5EA8 File Offset: 0x000A40A8
		public EntityContainerMapping(EntityContainer conceptualEntityContainer, EntityContainer storeEntityContainer, StorageMappingItemCollection mappingItemCollection, bool generateUpdateViews) : this(conceptualEntityContainer, storeEntityContainer, mappingItemCollection, true, generateUpdateViews)
		{
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x000A5EB8 File Offset: 0x000A40B8
		internal EntityContainerMapping(EntityContainer entityContainer, EntityContainer storageEntityContainer, StorageMappingItemCollection storageMappingItemCollection, bool validate, bool generateUpdateViews)
		{
			this.m_entitySetMappings = new Dictionary<string, EntitySetBaseMapping>(StringComparer.Ordinal);
			this.m_associationSetMappings = new Dictionary<string, EntitySetBaseMapping>(StringComparer.Ordinal);
			this.m_functionImportMappings = new Dictionary<EdmFunction, FunctionImportMapping>();
			base..ctor(MetadataItem.MetadataFlags.CSSpace);
			Check.NotNull<EntityContainer>(entityContainer, "entityContainer");
			this.m_entityContainer = entityContainer;
			this.m_storageEntityContainer = storageEntityContainer;
			this.m_storageMappingItemCollection = storageMappingItemCollection;
			this.m_memoizedCellGroupEvaluator = new Memoizer<InputForComputingCellGroups, OutputFromComputeCellGroups>(new Func<InputForComputingCellGroups, OutputFromComputeCellGroups>(this.ComputeCellGroups), default(InputForComputingCellGroups));
			this.identity = entityContainer.Identity;
			this.m_validate = validate;
			this.m_generateUpdateViews = generateUpdateViews;
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x000A5F59 File Offset: 0x000A4159
		internal EntityContainerMapping(EntityContainer entityContainer) : this(entityContainer, null, null, false, false)
		{
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x000A5F66 File Offset: 0x000A4166
		internal EntityContainerMapping()
		{
			this.m_entitySetMappings = new Dictionary<string, EntitySetBaseMapping>(StringComparer.Ordinal);
			this.m_associationSetMappings = new Dictionary<string, EntitySetBaseMapping>(StringComparer.Ordinal);
			this.m_functionImportMappings = new Dictionary<EdmFunction, FunctionImportMapping>();
			base..ctor();
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060023E2 RID: 9186 RVA: 0x000A5F99 File Offset: 0x000A4199
		public StorageMappingItemCollection MappingItemCollection
		{
			get
			{
				return this.m_storageMappingItemCollection;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060023E3 RID: 9187 RVA: 0x000A5FA1 File Offset: 0x000A41A1
		internal StorageMappingItemCollection StorageMappingItemCollection
		{
			get
			{
				return this.MappingItemCollection;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060023E4 RID: 9188 RVA: 0x000A5FA9 File Offset: 0x000A41A9
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.MetadataItem;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060023E5 RID: 9189 RVA: 0x000A5FAD File Offset: 0x000A41AD
		internal override MetadataItem EdmItem
		{
			get
			{
				return this.m_entityContainer;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060023E6 RID: 9190 RVA: 0x000A5FB5 File Offset: 0x000A41B5
		internal override string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060023E7 RID: 9191 RVA: 0x000A5FBD File Offset: 0x000A41BD
		internal bool IsEmpty
		{
			get
			{
				return this.m_entitySetMappings.Count == 0 && this.m_associationSetMappings.Count == 0;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x060023E8 RID: 9192 RVA: 0x000A5FEA File Offset: 0x000A41EA
		internal bool HasViews
		{
			get
			{
				if (!this.HasMappingFragments())
				{
					return this.AllSetMaps.Any((EntitySetBaseMapping setMap) => setMap.QueryView != null);
				}
				return true;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x060023E9 RID: 9193 RVA: 0x000A601E File Offset: 0x000A421E
		// (set) Token: 0x060023EA RID: 9194 RVA: 0x000A6026 File Offset: 0x000A4226
		internal string SourceLocation { get; set; }

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060023EB RID: 9195 RVA: 0x000A602F File Offset: 0x000A422F
		public EntityContainer ConceptualEntityContainer
		{
			get
			{
				return this.m_entityContainer;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060023EC RID: 9196 RVA: 0x000A6037 File Offset: 0x000A4237
		internal EntityContainer EdmEntityContainer
		{
			get
			{
				return this.ConceptualEntityContainer;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060023ED RID: 9197 RVA: 0x000A603F File Offset: 0x000A423F
		public EntityContainer StoreEntityContainer
		{
			get
			{
				return this.m_storageEntityContainer;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060023EE RID: 9198 RVA: 0x000A6047 File Offset: 0x000A4247
		internal EntityContainer StorageEntityContainer
		{
			get
			{
				return this.StoreEntityContainer;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060023EF RID: 9199 RVA: 0x000A604F File Offset: 0x000A424F
		internal ReadOnlyCollection<EntitySetBaseMapping> EntitySetMaps
		{
			get
			{
				return new ReadOnlyCollection<EntitySetBaseMapping>(new List<EntitySetBaseMapping>(this.m_entitySetMappings.Values));
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060023F0 RID: 9200 RVA: 0x000A6066 File Offset: 0x000A4266
		public virtual IEnumerable<EntitySetMapping> EntitySetMappings
		{
			get
			{
				return this.EntitySetMaps.OfType<EntitySetMapping>();
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060023F1 RID: 9201 RVA: 0x000A6073 File Offset: 0x000A4273
		public virtual IEnumerable<AssociationSetMapping> AssociationSetMappings
		{
			get
			{
				return this.RelationshipSetMaps.OfType<AssociationSetMapping>();
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060023F2 RID: 9202 RVA: 0x000A6080 File Offset: 0x000A4280
		public IEnumerable<FunctionImportMapping> FunctionImportMappings
		{
			get
			{
				return this.m_functionImportMappings.Values;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060023F3 RID: 9203 RVA: 0x000A608D File Offset: 0x000A428D
		internal ReadOnlyCollection<EntitySetBaseMapping> RelationshipSetMaps
		{
			get
			{
				return new ReadOnlyCollection<EntitySetBaseMapping>(new List<EntitySetBaseMapping>(this.m_associationSetMappings.Values));
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060023F4 RID: 9204 RVA: 0x000A60A4 File Offset: 0x000A42A4
		internal IEnumerable<EntitySetBaseMapping> AllSetMaps
		{
			get
			{
				return this.m_entitySetMappings.Values.Concat(this.m_associationSetMappings.Values);
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060023F5 RID: 9205 RVA: 0x000A60C1 File Offset: 0x000A42C1
		// (set) Token: 0x060023F6 RID: 9206 RVA: 0x000A60C9 File Offset: 0x000A42C9
		internal int StartLineNumber { get; set; }

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060023F7 RID: 9207 RVA: 0x000A60D2 File Offset: 0x000A42D2
		// (set) Token: 0x060023F8 RID: 9208 RVA: 0x000A60DA File Offset: 0x000A42DA
		internal int StartLinePosition { get; set; }

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060023F9 RID: 9209 RVA: 0x000A60E3 File Offset: 0x000A42E3
		internal bool Validate
		{
			get
			{
				return this.m_validate;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060023FA RID: 9210 RVA: 0x000A60EB File Offset: 0x000A42EB
		public bool GenerateUpdateViews
		{
			get
			{
				return this.m_generateUpdateViews;
			}
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x000A60F4 File Offset: 0x000A42F4
		internal EntitySetBaseMapping GetEntitySetMapping(string setName)
		{
			EntitySetBaseMapping result = null;
			this.m_entitySetMappings.TryGetValue(setName, out result);
			return result;
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x000A6114 File Offset: 0x000A4314
		internal EntitySetBaseMapping GetAssociationSetMapping(string setName)
		{
			EntitySetBaseMapping result = null;
			this.m_associationSetMappings.TryGetValue(setName, out result);
			return result;
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x000A6188 File Offset: 0x000A4388
		internal IEnumerable<AssociationSetMapping> GetRelationshipSetMappingsFor(EntitySetBase edmEntitySet, EntitySetBase storeEntitySet)
		{
			IEnumerable<AssociationSetMapping> source = from AssociationSetMapping w in this.m_associationSetMappings.Values
			where w.StoreEntitySet != null && w.StoreEntitySet == storeEntitySet
			select w;
			return from associationSetMap in source
			where (associationSetMap.Set as AssociationSet).AssociationSetEnds.Any((AssociationSetEnd associationSetEnd) => associationSetEnd.EntitySet == edmEntitySet)
			select associationSetMap;
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x000A61E0 File Offset: 0x000A43E0
		internal EntitySetBaseMapping GetSetMapping(string setName)
		{
			EntitySetBaseMapping entitySetBaseMapping = this.GetEntitySetMapping(setName);
			if (entitySetBaseMapping == null)
			{
				entitySetBaseMapping = this.GetAssociationSetMapping(setName);
			}
			return entitySetBaseMapping;
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x000A6204 File Offset: 0x000A4404
		public void AddSetMapping(EntitySetMapping setMapping)
		{
			Check.NotNull<EntitySetMapping>(setMapping, "setMapping");
			Util.ThrowIfReadOnly(this);
			if (!this.m_entitySetMappings.ContainsKey(setMapping.Set.Name))
			{
				this.m_entitySetMappings.Add(setMapping.Set.Name, setMapping);
			}
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x000A6252 File Offset: 0x000A4452
		public void RemoveSetMapping(EntitySetMapping setMapping)
		{
			Check.NotNull<EntitySetMapping>(setMapping, "setMapping");
			Util.ThrowIfReadOnly(this);
			this.m_entitySetMappings.Remove(setMapping.Set.Name);
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x000A6280 File Offset: 0x000A4480
		public void AddSetMapping(AssociationSetMapping setMapping)
		{
			Check.NotNull<AssociationSetMapping>(setMapping, "setMapping");
			Util.ThrowIfReadOnly(this);
			if (!this.m_associationSetMappings.ContainsKey(setMapping.Set.Name))
			{
				this.m_associationSetMappings.Add(setMapping.Set.Name, setMapping);
			}
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x000A62CE File Offset: 0x000A44CE
		public void RemoveSetMapping(AssociationSetMapping setMapping)
		{
			Check.NotNull<AssociationSetMapping>(setMapping, "setMapping");
			Util.ThrowIfReadOnly(this);
			this.m_associationSetMappings.Remove(setMapping.Set.Name);
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x000A62F9 File Offset: 0x000A44F9
		internal bool ContainsAssociationSetMapping(AssociationSet associationSet)
		{
			return this.m_associationSetMappings.ContainsKey(associationSet.Name);
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x000A630C File Offset: 0x000A450C
		public void AddFunctionImportMapping(FunctionImportMapping functionImportMapping)
		{
			Check.NotNull<FunctionImportMapping>(functionImportMapping, "functionImportMapping");
			Util.ThrowIfReadOnly(this);
			this.m_functionImportMappings.Add(functionImportMapping.FunctionImport, functionImportMapping);
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x000A6332 File Offset: 0x000A4532
		public void RemoveFunctionImportMapping(FunctionImportMapping functionImportMapping)
		{
			Check.NotNull<FunctionImportMapping>(functionImportMapping, "functionImportMapping");
			Util.ThrowIfReadOnly(this);
			this.m_functionImportMappings.Remove(functionImportMapping.FunctionImport);
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x000A6358 File Offset: 0x000A4558
		internal override void SetReadOnly()
		{
			MappingItem.SetReadOnly(this.m_entitySetMappings.Values);
			MappingItem.SetReadOnly(this.m_associationSetMappings.Values);
			MappingItem.SetReadOnly(this.m_functionImportMappings.Values);
			base.SetReadOnly();
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x000A6390 File Offset: 0x000A4590
		internal bool HasQueryViewForSetMap(string setName)
		{
			EntitySetBaseMapping setMapping = this.GetSetMapping(setName);
			return setMapping != null && setMapping.QueryView != null;
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x000A63B8 File Offset: 0x000A45B8
		internal bool HasMappingFragments()
		{
			foreach (EntitySetBaseMapping entitySetBaseMapping in this.AllSetMaps)
			{
				foreach (TypeMapping typeMapping in entitySetBaseMapping.TypeMappings)
				{
					if (typeMapping.MappingFragments.Count > 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x000A6450 File Offset: 0x000A4650
		internal virtual bool TryGetFunctionImportMapping(EdmFunction functionImport, out FunctionImportMapping mapping)
		{
			return this.m_functionImportMappings.TryGetValue(functionImport, out mapping);
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x000A645F File Offset: 0x000A465F
		internal OutputFromComputeCellGroups GetCellgroups(InputForComputingCellGroups args)
		{
			return this.m_memoizedCellGroupEvaluator.Evaluate(args);
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x000A64A0 File Offset: 0x000A46A0
		private OutputFromComputeCellGroups ComputeCellGroups(InputForComputingCellGroups args)
		{
			OutputFromComputeCellGroups result = default(OutputFromComputeCellGroups);
			result.Success = true;
			CellCreator cellCreator = new CellCreator(args.ContainerMapping);
			result.Cells = cellCreator.GenerateCells();
			result.Identifiers = cellCreator.Identifiers;
			if (result.Cells.Count <= 0)
			{
				result.Success = false;
				return result;
			}
			result.ForeignKeyConstraints = ForeignConstraint.GetForeignConstraints(args.ContainerMapping.StorageEntityContainer);
			CellPartitioner cellPartitioner = new CellPartitioner(result.Cells, result.ForeignKeyConstraints);
			List<Set<Cell>> source = cellPartitioner.GroupRelatedCells();
			result.CellGroups = (from setOfcells in source
			select new Set<Cell>(from cell in setOfcells
			select new Cell(cell))).ToList<Set<Cell>>();
			return result;
		}

		// Token: 0x04000C96 RID: 3222
		private readonly string identity;

		// Token: 0x04000C97 RID: 3223
		private readonly bool m_validate;

		// Token: 0x04000C98 RID: 3224
		private readonly bool m_generateUpdateViews;

		// Token: 0x04000C99 RID: 3225
		private readonly EntityContainer m_entityContainer;

		// Token: 0x04000C9A RID: 3226
		private readonly EntityContainer m_storageEntityContainer;

		// Token: 0x04000C9B RID: 3227
		private readonly Dictionary<string, EntitySetBaseMapping> m_entitySetMappings;

		// Token: 0x04000C9C RID: 3228
		private readonly Dictionary<string, EntitySetBaseMapping> m_associationSetMappings;

		// Token: 0x04000C9D RID: 3229
		private readonly Dictionary<EdmFunction, FunctionImportMapping> m_functionImportMappings;

		// Token: 0x04000C9E RID: 3230
		private readonly StorageMappingItemCollection m_storageMappingItemCollection;

		// Token: 0x04000C9F RID: 3231
		private readonly Memoizer<InputForComputingCellGroups, OutputFromComputeCellGroups> m_memoizedCellGroupEvaluator;
	}
}
