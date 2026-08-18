using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Data.Mapping.ViewGeneration.Validation;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Mapping
{
	// Token: 0x0200023F RID: 575
	internal class StorageEntityContainerMapping : Map
	{
		// Token: 0x0600243B RID: 9275 RVA: 0x00083350 File Offset: 0x00081550
		internal StorageEntityContainerMapping(EntityContainer entityContainer, EntityContainer storageEntityContainer, StorageMappingItemCollection storageMappingItemCollection, bool validate, bool generateUpdateViews)
		{
			this.m_entityContainer = entityContainer;
			this.m_storageEntityContainer = storageEntityContainer;
			this.m_storageMappingItemCollection = storageMappingItemCollection;
			this.m_memoizedCellGroupEvaluator = new Memoizer<InputForComputingCellGroups, OutputFromComputeCellGroups>(new Func<InputForComputingCellGroups, OutputFromComputeCellGroups>(this.ComputeCellGroups), default(InputForComputingCellGroups));
			this.identity = entityContainer.Identity;
			this.m_validate = validate;
			this.m_generateUpdateViews = generateUpdateViews;
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x0600243C RID: 9276 RVA: 0x000833E4 File Offset: 0x000815E4
		public StorageMappingItemCollection StorageMappingItemCollection
		{
			get
			{
				return this.m_storageMappingItemCollection;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x0600243D RID: 9277 RVA: 0x000827C0 File Offset: 0x000809C0
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.MetadataItem;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x0600243E RID: 9278 RVA: 0x000833EC File Offset: 0x000815EC
		internal override MetadataItem EdmItem
		{
			get
			{
				return this.m_entityContainer;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x0600243F RID: 9279 RVA: 0x000833F4 File Offset: 0x000815F4
		internal override string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06002440 RID: 9280 RVA: 0x000833FC File Offset: 0x000815FC
		internal bool IsEmpty
		{
			get
			{
				return this.m_entitySetMappings.Count == 0 && this.m_associationSetMappings.Count == 0;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06002441 RID: 9281 RVA: 0x0008341B File Offset: 0x0008161B
		internal bool HasViews
		{
			get
			{
				if (!this.HasMappingFragments())
				{
					return this.AllSetMaps.Any((StorageSetMapping setMap) => setMap.QueryView != null);
				}
				return true;
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06002442 RID: 9282 RVA: 0x00083451 File Offset: 0x00081651
		// (set) Token: 0x06002443 RID: 9283 RVA: 0x00083459 File Offset: 0x00081659
		internal string SourceLocation
		{
			get
			{
				return this.m_sourceLocation;
			}
			set
			{
				this.m_sourceLocation = value;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06002444 RID: 9284 RVA: 0x000833EC File Offset: 0x000815EC
		internal EntityContainer EdmEntityContainer
		{
			get
			{
				return this.m_entityContainer;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06002445 RID: 9285 RVA: 0x00083462 File Offset: 0x00081662
		internal EntityContainer StorageEntityContainer
		{
			get
			{
				return this.m_storageEntityContainer;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06002446 RID: 9286 RVA: 0x0008346A File Offset: 0x0008166A
		internal ReadOnlyCollection<StorageSetMapping> EntitySetMaps
		{
			get
			{
				return new List<StorageSetMapping>(this.m_entitySetMappings.Values).AsReadOnly();
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06002447 RID: 9287 RVA: 0x00083481 File Offset: 0x00081681
		internal ReadOnlyCollection<StorageSetMapping> RelationshipSetMaps
		{
			get
			{
				return new List<StorageSetMapping>(this.m_associationSetMappings.Values).AsReadOnly();
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06002448 RID: 9288 RVA: 0x00083498 File Offset: 0x00081698
		internal IEnumerable<StorageSetMapping> AllSetMaps
		{
			get
			{
				return this.m_entitySetMappings.Values.Concat(this.m_associationSetMappings.Values);
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06002449 RID: 9289 RVA: 0x000834B5 File Offset: 0x000816B5
		// (set) Token: 0x0600244A RID: 9290 RVA: 0x000834BD File Offset: 0x000816BD
		internal int StartLineNumber
		{
			get
			{
				return this.m_startLineNumber;
			}
			set
			{
				this.m_startLineNumber = value;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x0600244B RID: 9291 RVA: 0x000834C6 File Offset: 0x000816C6
		// (set) Token: 0x0600244C RID: 9292 RVA: 0x000834CE File Offset: 0x000816CE
		internal int StartLinePosition
		{
			get
			{
				return this.m_startLinePosition;
			}
			set
			{
				this.m_startLinePosition = value;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x0600244D RID: 9293 RVA: 0x000834D7 File Offset: 0x000816D7
		internal bool Validate
		{
			get
			{
				return this.m_validate;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x0600244E RID: 9294 RVA: 0x000834DF File Offset: 0x000816DF
		internal bool GenerateUpdateViews
		{
			get
			{
				return this.m_generateUpdateViews;
			}
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x000834E8 File Offset: 0x000816E8
		internal StorageSetMapping GetEntitySetMapping(string entitySetName)
		{
			EntityUtil.CheckArgumentNull<string>(entitySetName, "entitySetName");
			StorageSetMapping result = null;
			this.m_entitySetMappings.TryGetValue(entitySetName, out result);
			return result;
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x00083514 File Offset: 0x00081714
		internal StorageSetMapping GetRelationshipSetMapping(string relationshipSetName)
		{
			EntityUtil.CheckArgumentNull<string>(relationshipSetName, "relationshipSetName");
			StorageSetMapping result = null;
			this.m_associationSetMappings.TryGetValue(relationshipSetName, out result);
			return result;
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x00083540 File Offset: 0x00081740
		internal IEnumerable<StorageAssociationSetMapping> GetRelationshipSetMappingsFor(EntitySetBase edmEntitySet, EntitySetBase storeEntitySet)
		{
			IEnumerable<StorageAssociationSetMapping> source = from StorageAssociationSetMapping w in this.m_associationSetMappings.Values
			where w.StoreEntitySet != null && w.StoreEntitySet == storeEntitySet
			select w;
			Func<AssociationSetEnd, bool> <>9__2;
			return source.Where(delegate(StorageAssociationSetMapping associationSetMap)
			{
				IEnumerable<AssociationSetEnd> associationSetEnds = (associationSetMap.Set as AssociationSet).AssociationSetEnds;
				Func<AssociationSetEnd, bool> predicate;
				if ((predicate = <>9__2) == null)
				{
					predicate = (<>9__2 = ((AssociationSetEnd associationSetEnd) => associationSetEnd.EntitySet == edmEntitySet));
				}
				return associationSetEnds.Any(predicate);
			});
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x00083598 File Offset: 0x00081798
		internal StorageSetMapping GetSetMapping(string setName)
		{
			StorageSetMapping storageSetMapping = this.GetEntitySetMapping(setName);
			if (storageSetMapping == null)
			{
				storageSetMapping = this.GetRelationshipSetMapping(setName);
			}
			return storageSetMapping;
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x000835B9 File Offset: 0x000817B9
		internal void AddEntitySetMapping(StorageSetMapping setMapping)
		{
			if (!this.m_entitySetMappings.ContainsKey(setMapping.Set.Name))
			{
				this.m_entitySetMappings.Add(setMapping.Set.Name, setMapping);
			}
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x000835EA File Offset: 0x000817EA
		internal void AddAssociationSetMapping(StorageSetMapping setMapping)
		{
			this.m_associationSetMappings.Add(setMapping.Set.Name, setMapping);
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x00083603 File Offset: 0x00081803
		internal bool ContainsAssociationSetMapping(AssociationSet associationSet)
		{
			return this.m_associationSetMappings.ContainsKey(associationSet.Name);
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x00083618 File Offset: 0x00081818
		internal bool HasQueryViewForSetMap(string setName)
		{
			StorageSetMapping setMapping = this.GetSetMapping(setName);
			return setMapping != null && setMapping.QueryView != null;
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x0008363C File Offset: 0x0008183C
		internal bool HasMappingFragments()
		{
			foreach (StorageSetMapping storageSetMapping in this.AllSetMaps)
			{
				foreach (StorageTypeMapping storageTypeMapping in storageSetMapping.TypeMappings)
				{
					if (storageTypeMapping.MappingFragments.Count > 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x000836D0 File Offset: 0x000818D0
		internal static string GetPrettyPrintString(ref int index)
		{
			string text = "";
			text = text.PadLeft(index, ' ');
			Console.WriteLine(text + "|");
			Console.WriteLine(text + "|");
			index++;
			text = text.PadLeft(index, ' ');
			Console.Write(text + "-");
			index++;
			text = text.PadLeft(index, ' ');
			Console.Write("-");
			index++;
			return text.PadLeft(index, ' ');
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x0008375C File Offset: 0x0008195C
		internal void Print(int index)
		{
			string value = "";
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(value);
			stringBuilder.Append("EntityContainerMapping");
			stringBuilder.Append("   ");
			stringBuilder.Append("Name:");
			stringBuilder.Append(this.m_entityContainer.Name);
			stringBuilder.Append("   ");
			Console.WriteLine(stringBuilder.ToString());
			foreach (StorageSetMapping storageSetMapping in this.m_entitySetMappings.Values)
			{
				storageSetMapping.Print(index + 5);
			}
			foreach (StorageSetMapping storageSetMapping2 in this.m_associationSetMappings.Values)
			{
				storageSetMapping2.Print(index + 5);
			}
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x00083864 File Offset: 0x00081A64
		internal void AddFunctionImportMapping(EdmFunction functionImport, FunctionImportMapping mapping)
		{
			this.m_functionImportMappings.Add(functionImport, mapping);
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x00083873 File Offset: 0x00081A73
		internal bool TryGetFunctionImportMapping(EdmFunction functionImport, out FunctionImportMapping mapping)
		{
			return this.m_functionImportMappings.TryGetValue(functionImport, out mapping);
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x00083882 File Offset: 0x00081A82
		internal OutputFromComputeCellGroups GetCellgroups(InputForComputingCellGroups args)
		{
			return this.m_memoizedCellGroupEvaluator.Evaluate(args);
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x00083890 File Offset: 0x00081A90
		private OutputFromComputeCellGroups ComputeCellGroups(InputForComputingCellGroups args)
		{
			OutputFromComputeCellGroups outputFromComputeCellGroups = default(OutputFromComputeCellGroups);
			outputFromComputeCellGroups.Success = true;
			CellCreator cellCreator = new CellCreator(args.ContainerMapping);
			outputFromComputeCellGroups.Cells = cellCreator.GenerateCells(args.Config);
			outputFromComputeCellGroups.Identifiers = cellCreator.Identifiers;
			if (outputFromComputeCellGroups.Cells.Count <= 0)
			{
				outputFromComputeCellGroups.Success = false;
				return outputFromComputeCellGroups;
			}
			outputFromComputeCellGroups.ForeignKeyConstraints = ForeignConstraint.GetForeignConstraints(args.ContainerMapping.StorageEntityContainer);
			CellPartitioner cellPartitioner = new CellPartitioner(outputFromComputeCellGroups.Cells, outputFromComputeCellGroups.ForeignKeyConstraints);
			List<Set<Cell>> source = cellPartitioner.GroupRelatedCells();
			outputFromComputeCellGroups.CellGroups = (from setOfcells in source
			select new Set<Cell>(from cell in setOfcells
			select new Cell(cell))).ToList<Set<Cell>>();
			return outputFromComputeCellGroups;
		}

		// Token: 0x0400100C RID: 4108
		private string identity;

		// Token: 0x0400100D RID: 4109
		private bool m_validate;

		// Token: 0x0400100E RID: 4110
		private bool m_generateUpdateViews;

		// Token: 0x0400100F RID: 4111
		private EntityContainer m_entityContainer;

		// Token: 0x04001010 RID: 4112
		private EntityContainer m_storageEntityContainer;

		// Token: 0x04001011 RID: 4113
		private Dictionary<string, StorageSetMapping> m_entitySetMappings = new Dictionary<string, StorageSetMapping>(StringComparer.Ordinal);

		// Token: 0x04001012 RID: 4114
		private Dictionary<string, StorageSetMapping> m_associationSetMappings = new Dictionary<string, StorageSetMapping>(StringComparer.Ordinal);

		// Token: 0x04001013 RID: 4115
		private Dictionary<EdmFunction, FunctionImportMapping> m_functionImportMappings = new Dictionary<EdmFunction, FunctionImportMapping>();

		// Token: 0x04001014 RID: 4116
		private string m_sourceLocation;

		// Token: 0x04001015 RID: 4117
		private int m_startLineNumber;

		// Token: 0x04001016 RID: 4118
		private int m_startLinePosition;

		// Token: 0x04001017 RID: 4119
		private readonly StorageMappingItemCollection m_storageMappingItemCollection;

		// Token: 0x04001018 RID: 4120
		private readonly Memoizer<InputForComputingCellGroups, OutputFromComputeCellGroups> m_memoizedCellGroupEvaluator;
	}
}
