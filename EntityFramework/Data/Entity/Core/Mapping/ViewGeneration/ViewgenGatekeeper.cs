using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Mapping.ViewGeneration.Validation;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration
{
	// Token: 0x020004A2 RID: 1186
	internal abstract class ViewgenGatekeeper : InternalBase
	{
		// Token: 0x06002BC2 RID: 11202 RVA: 0x000D5634 File Offset: 0x000D3834
		internal static ViewGenResults GenerateViewsFromMapping(EntityContainerMapping containerMapping, ConfigViewGenerator config)
		{
			CellCreator cellCreator = new CellCreator(containerMapping);
			List<Cell> cells = cellCreator.GenerateCells();
			CqlIdentifiers identifiers = cellCreator.Identifiers;
			return ViewgenGatekeeper.GenerateViewsFromCells(cells, config, identifiers, containerMapping);
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x000D5694 File Offset: 0x000D3894
		internal static ViewGenResults GenerateTypeSpecificQueryView(EntityContainerMapping containerMapping, ConfigViewGenerator config, EntitySetBase entity, EntityTypeBase type, bool includeSubtypes, out bool success)
		{
			if (config.IsNormalTracing)
			{
				Helpers.StringTraceLine("");
				Helpers.StringTraceLine(string.Concat(new string[]
				{
					"<<<<<<<< Generating Query View for Entity [",
					entity.Name,
					"] OfType",
					includeSubtypes ? "" : "Only",
					"(",
					type.Name,
					") >>>>>>>"
				}));
			}
			if (containerMapping.GetEntitySetMapping(entity.Name).QueryView != null)
			{
				success = false;
				return null;
			}
			InputForComputingCellGroups args = new InputForComputingCellGroups(containerMapping, config);
			OutputFromComputeCellGroups cellgroups = containerMapping.GetCellgroups(args);
			success = cellgroups.Success;
			if (!success)
			{
				return null;
			}
			List<ForeignConstraint> foreignKeyConstraints = cellgroups.ForeignKeyConstraints;
			List<Set<Cell>> list = (from setOfcells in cellgroups.CellGroups
			select new Set<Cell>(from cell in setOfcells
			select new Cell(cell))).ToList<Set<Cell>>();
			List<Cell> cells = cellgroups.Cells;
			CqlIdentifiers identifiers = cellgroups.Identifiers;
			ViewGenResults viewGenResults = new ViewGenResults();
			ErrorLog errorLog = ViewgenGatekeeper.EnsureAllCSpaceContainerSetsAreMapped(cells, containerMapping);
			if (errorLog.Count > 0)
			{
				viewGenResults.AddErrors(errorLog);
				Helpers.StringTraceLine(viewGenResults.ErrorsToString());
				success = true;
				return viewGenResults;
			}
			foreach (Set<Cell> set in list)
			{
				if (ViewgenGatekeeper.DoesCellGroupContainEntitySet(set, entity))
				{
					ViewGenerator viewGenerator = null;
					ErrorLog errorLog2 = new ErrorLog();
					try
					{
						viewGenerator = new ViewGenerator(set, config, foreignKeyConstraints, containerMapping);
					}
					catch (InternalMappingException ex)
					{
						errorLog2 = ex.ErrorLog;
					}
					if (errorLog2.Count > 0)
					{
						break;
					}
					ViewGenMode mode = includeSubtypes ? ViewGenMode.OfTypeViews : ViewGenMode.OfTypeOnlyViews;
					errorLog2 = viewGenerator.GenerateQueryViewForSingleExtent(viewGenResults.Views, identifiers, entity, type, mode);
					if (errorLog2.Count != 0)
					{
						viewGenResults.AddErrors(errorLog2);
					}
				}
			}
			success = true;
			return viewGenResults;
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x000D5888 File Offset: 0x000D3A88
		private static ViewGenResults GenerateViewsFromCells(List<Cell> cells, ConfigViewGenerator config, CqlIdentifiers identifiers, EntityContainerMapping containerMapping)
		{
			EntityContainer storageEntityContainer = containerMapping.StorageEntityContainer;
			ViewGenResults viewGenResults = new ViewGenResults();
			ErrorLog errorLog = ViewgenGatekeeper.EnsureAllCSpaceContainerSetsAreMapped(cells, containerMapping);
			if (errorLog.Count > 0)
			{
				viewGenResults.AddErrors(errorLog);
				Helpers.StringTraceLine(viewGenResults.ErrorsToString());
				return viewGenResults;
			}
			List<ForeignConstraint> foreignConstraints = ForeignConstraint.GetForeignConstraints(storageEntityContainer);
			CellPartitioner cellPartitioner = new CellPartitioner(cells, foreignConstraints);
			List<Set<Cell>> list = cellPartitioner.GroupRelatedCells();
			foreach (Set<Cell> cellGroup in list)
			{
				ViewGenerator viewGenerator = null;
				ErrorLog errorLog2 = new ErrorLog();
				try
				{
					viewGenerator = new ViewGenerator(cellGroup, config, foreignConstraints, containerMapping);
				}
				catch (InternalMappingException ex)
				{
					errorLog2 = ex.ErrorLog;
				}
				if (errorLog2.Count == 0)
				{
					errorLog2 = viewGenerator.GenerateAllBidirectionalViews(viewGenResults.Views, identifiers);
				}
				if (errorLog2.Count != 0)
				{
					viewGenResults.AddErrors(errorLog2);
				}
			}
			return viewGenResults;
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x000D597C File Offset: 0x000D3B7C
		private static ErrorLog EnsureAllCSpaceContainerSetsAreMapped(IEnumerable<Cell> cells, EntityContainerMapping containerMapping)
		{
			Set<EntitySetBase> set = new Set<EntitySetBase>();
			EntityContainer entityContainer = null;
			foreach (Cell cell in cells)
			{
				set.Add(cell.CQuery.Extent);
				string sourceLocation = cell.CellLabel.SourceLocation;
				entityContainer = cell.CQuery.Extent.EntityContainer;
			}
			List<EntitySetBase> list = new List<EntitySetBase>();
			foreach (EntitySetBase entitySetBase in entityContainer.BaseEntitySets)
			{
				if (!set.Contains(entitySetBase) && !containerMapping.HasQueryViewForSetMap(entitySetBase.Name))
				{
					AssociationSet associationSet = entitySetBase as AssociationSet;
					if (associationSet == null || !associationSet.ElementType.IsForeignKey)
					{
						list.Add(entitySetBase);
					}
				}
			}
			ErrorLog errorLog = new ErrorLog();
			if (list.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = true;
				foreach (EntitySetBase entitySetBase2 in list)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append(entitySetBase2.Name);
				}
				string message = Strings.ViewGen_Missing_Set_Mapping(stringBuilder);
				int num = -1;
				foreach (Cell cell2 in cells)
				{
					if (num == -1 || cell2.CellLabel.StartLineNumber < num)
					{
						num = cell2.CellLabel.StartLineNumber;
					}
				}
				EdmSchemaError error = new EdmSchemaError(message, 3027, EdmSchemaErrorSeverity.Error, containerMapping.SourceLocation, containerMapping.StartLineNumber, containerMapping.StartLinePosition, null);
				ErrorLog.Record record = new ErrorLog.Record(error);
				errorLog.AddEntry(record);
			}
			return errorLog;
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x000D5B8C File Offset: 0x000D3D8C
		private static bool DoesCellGroupContainEntitySet(Set<Cell> group, EntitySetBase entity)
		{
			foreach (Cell cell in group)
			{
				if (cell.GetLeftQuery(ViewTarget.QueryView).Extent.Equals(entity))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x000D5BF0 File Offset: 0x000D3DF0
		internal override void ToCompactString(StringBuilder builder)
		{
		}
	}
}
