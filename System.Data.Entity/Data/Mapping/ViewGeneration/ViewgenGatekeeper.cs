using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Data.Mapping.ViewGeneration.Validation;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Mapping.ViewGeneration
{
	// Token: 0x0200026E RID: 622
	internal abstract class ViewgenGatekeeper : InternalBase
	{
		// Token: 0x0600261E RID: 9758 RVA: 0x0009130C File Offset: 0x0008F50C
		internal static ViewGenResults GenerateViewsFromMapping(StorageEntityContainerMapping containerMapping, ConfigViewGenerator config)
		{
			EntityUtil.CheckArgumentNull<StorageEntityContainerMapping>(containerMapping, "containerMapping");
			EntityUtil.CheckArgumentNull<ConfigViewGenerator>(config, "config");
			if (config.IsNormalTracing)
			{
				containerMapping.Print(0);
			}
			CellCreator cellCreator = new CellCreator(containerMapping);
			List<Cell> cells = cellCreator.GenerateCells(config);
			CqlIdentifiers identifiers = cellCreator.Identifiers;
			return ViewgenGatekeeper.GenerateViewsFromCells(cells, config, identifiers, containerMapping);
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x00091360 File Offset: 0x0008F560
		internal static ViewGenResults GenerateTypeSpecificQueryView(StorageEntityContainerMapping containerMapping, ConfigViewGenerator config, EntitySetBase entity, EntityTypeBase type, bool includeSubtypes, out bool success)
		{
			EntityUtil.CheckArgumentNull<StorageEntityContainerMapping>(containerMapping, "containerMapping");
			EntityUtil.CheckArgumentNull<ConfigViewGenerator>(config, "config");
			EntityUtil.CheckArgumentNull<EntitySetBase>(entity, "entity");
			EntityUtil.CheckArgumentNull<EntityTypeBase>(type, "type");
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
			ErrorLog errorLog = ViewgenGatekeeper.EnsureAllCSpaceContainerSetsAreMapped(cells, config, containerMapping);
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

		// Token: 0x06002620 RID: 9760 RVA: 0x0009157C File Offset: 0x0008F77C
		private static ViewGenResults GenerateViewsFromCells(List<Cell> cells, ConfigViewGenerator config, CqlIdentifiers identifiers, StorageEntityContainerMapping containerMapping)
		{
			EntityUtil.CheckArgumentNull<List<Cell>>(cells, "cells");
			EntityUtil.CheckArgumentNull<ConfigViewGenerator>(config, "config");
			EntityContainer storageEntityContainer = containerMapping.StorageEntityContainer;
			ViewGenResults viewGenResults = new ViewGenResults();
			ErrorLog errorLog = ViewgenGatekeeper.EnsureAllCSpaceContainerSetsAreMapped(cells, config, containerMapping);
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

		// Token: 0x06002621 RID: 9761 RVA: 0x00091688 File Offset: 0x0008F888
		private static ErrorLog EnsureAllCSpaceContainerSetsAreMapped(IEnumerable<Cell> cells, ConfigViewGenerator config, StorageEntityContainerMapping containerMapping)
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

		// Token: 0x06002622 RID: 9762 RVA: 0x000918A4 File Offset: 0x0008FAA4
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

		// Token: 0x06002623 RID: 9763 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal override void ToCompactString(StringBuilder builder)
		{
		}
	}
}
