using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration.QueryRewriting;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Data.Mapping.ViewGeneration.Validation;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Mapping.ViewGeneration
{
	// Token: 0x0200026C RID: 620
	internal class ViewGenerator : InternalBase
	{
		// Token: 0x06002609 RID: 9737 RVA: 0x0009085C File Offset: 0x0008EA5C
		internal ViewGenerator(Set<Cell> cellGroup, ConfigViewGenerator config, List<ForeignConstraint> foreignKeyConstraints, StorageEntityContainerMapping entityContainerMapping)
		{
			this.m_cellGroup = cellGroup;
			this.m_config = config;
			this.m_queryRewriterCache = new Dictionary<EntitySetBase, QueryRewriter>();
			this.m_foreignKeyConstraints = foreignKeyConstraints;
			this.m_entityContainerMapping = entityContainerMapping;
			Dictionary<EntityType, Set<EntityType>> inheritanceGraph = MetadataHelper.BuildUndirectedGraphOfTypes(entityContainerMapping.StorageMappingItemCollection.EdmItemCollection);
			this.SetConfiguration(entityContainerMapping);
			this.m_queryDomainMap = new MemberDomainMap(ViewTarget.QueryView, this.m_config.IsValidationEnabled, cellGroup, entityContainerMapping.StorageMappingItemCollection.EdmItemCollection, this.m_config, inheritanceGraph);
			this.m_updateDomainMap = new MemberDomainMap(ViewTarget.UpdateView, this.m_config.IsValidationEnabled, cellGroup, entityContainerMapping.StorageMappingItemCollection.EdmItemCollection, this.m_config, inheritanceGraph);
			MemberDomainMap.PropagateUpdateDomainToQueryDomain(cellGroup, this.m_queryDomainMap, this.m_updateDomainMap);
			ViewGenerator.UpdateWhereClauseForEachCell(cellGroup, this.m_queryDomainMap, this.m_updateDomainMap, this.m_config);
			MemberDomainMap openDomain = this.m_queryDomainMap.GetOpenDomain();
			MemberDomainMap openDomain2 = this.m_updateDomainMap.GetOpenDomain();
			foreach (Cell cell in cellGroup)
			{
				cell.CQuery.WhereClause.FixDomainMap(openDomain);
				cell.SQuery.WhereClause.FixDomainMap(openDomain2);
				cell.CQuery.WhereClause.ExpensiveSimplify();
				cell.SQuery.WhereClause.ExpensiveSimplify();
				cell.CQuery.WhereClause.FixDomainMap(this.m_queryDomainMap);
				cell.SQuery.WhereClause.FixDomainMap(this.m_updateDomainMap);
			}
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x000909FC File Offset: 0x0008EBFC
		private void SetConfiguration(StorageEntityContainerMapping entityContainerMapping)
		{
			this.m_config.IsValidationEnabled = entityContainerMapping.Validate;
			this.m_config.GenerateUpdateViews = entityContainerMapping.GenerateUpdateViews;
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x00090A20 File Offset: 0x0008EC20
		internal ErrorLog GenerateAllBidirectionalViews(KeyToListMap<EntitySetBase, GeneratedView> views, CqlIdentifiers identifiers)
		{
			if (this.m_config.IsNormalTracing)
			{
				StringBuilder stringBuilder = new StringBuilder();
				Cell.CellsToBuilder(stringBuilder, this.m_cellGroup);
				Helpers.StringTraceLine(stringBuilder.ToString());
			}
			this.m_config.SetTimeForFinishedActivity(PerfType.CellCreation);
			CellGroupValidator cellGroupValidator = new CellGroupValidator(this.m_cellGroup, this.m_config);
			ErrorLog errorLog = cellGroupValidator.Validate();
			if (errorLog.Count > 0)
			{
				errorLog.PrintTrace();
				return errorLog;
			}
			this.m_config.SetTimeForFinishedActivity(PerfType.KeyConstraint);
			if (this.m_config.GenerateUpdateViews)
			{
				errorLog = this.GenerateDirectionalViews(ViewTarget.UpdateView, identifiers, views);
				if (errorLog.Count > 0)
				{
					return errorLog;
				}
			}
			if (this.m_config.IsValidationEnabled)
			{
				this.CheckForeignKeyConstraints(errorLog);
			}
			this.m_config.SetTimeForFinishedActivity(PerfType.ForeignConstraint);
			if (errorLog.Count > 0)
			{
				errorLog.PrintTrace();
				return errorLog;
			}
			this.m_updateDomainMap.ExpandDomainsToIncludeAllPossibleValues();
			return this.GenerateDirectionalViews(ViewTarget.QueryView, identifiers, views);
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x00090B04 File Offset: 0x0008ED04
		internal ErrorLog GenerateQueryViewForSingleExtent(KeyToListMap<EntitySetBase, GeneratedView> views, CqlIdentifiers identifiers, EntitySetBase entity, EntityTypeBase type, ViewGenMode mode)
		{
			if (this.m_config.IsNormalTracing)
			{
				StringBuilder stringBuilder = new StringBuilder();
				Cell.CellsToBuilder(stringBuilder, this.m_cellGroup);
				Helpers.StringTraceLine(stringBuilder.ToString());
			}
			CellGroupValidator cellGroupValidator = new CellGroupValidator(this.m_cellGroup, this.m_config);
			ErrorLog errorLog = cellGroupValidator.Validate();
			if (errorLog.Count > 0)
			{
				errorLog.PrintTrace();
				return errorLog;
			}
			if (this.m_config.IsValidationEnabled)
			{
				this.CheckForeignKeyConstraints(errorLog);
			}
			if (errorLog.Count > 0)
			{
				errorLog.PrintTrace();
				return errorLog;
			}
			this.m_updateDomainMap.ExpandDomainsToIncludeAllPossibleValues();
			foreach (Cell cell in this.m_cellGroup)
			{
				cell.SQuery.WhereClause.FixDomainMap(this.m_updateDomainMap);
			}
			return this.GenerateQueryViewForExtentAndType(this.m_entityContainerMapping, identifiers, views, entity, type, mode);
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x00090C04 File Offset: 0x0008EE04
		private static void UpdateWhereClauseForEachCell(IEnumerable<Cell> extentCells, MemberDomainMap queryDomainMap, MemberDomainMap updateDomainMap, ConfigViewGenerator config)
		{
			foreach (Cell cell in extentCells)
			{
				cell.CQuery.UpdateWhereClause(queryDomainMap);
				cell.SQuery.UpdateWhereClause(updateDomainMap);
			}
			queryDomainMap.ReduceEnumerableDomainToEnumeratedValues(ViewTarget.QueryView, config);
			updateDomainMap.ReduceEnumerableDomainToEnumeratedValues(ViewTarget.UpdateView, config);
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x00090C70 File Offset: 0x0008EE70
		private ErrorLog GenerateQueryViewForExtentAndType(StorageEntityContainerMapping entityContainerMapping, CqlIdentifiers identifiers, KeyToListMap<EntitySetBase, GeneratedView> views, EntitySetBase entity, EntityTypeBase type, ViewGenMode mode)
		{
			ErrorLog errorLog = new ErrorLog();
			if (this.m_config.IsViewTracing)
			{
				Helpers.StringTraceLine(string.Empty);
				Helpers.StringTraceLine(string.Empty);
				Helpers.FormatTraceLine("================= Generating {0} Query View for: {1} ===========================", new object[]
				{
					(mode == ViewGenMode.OfTypeViews) ? "OfType" : "OfTypeOnly",
					entity.Name
				});
				Helpers.StringTraceLine(string.Empty);
				Helpers.StringTraceLine(string.Empty);
			}
			try
			{
				ViewgenContext context = this.CreateViewgenContext(entity, ViewTarget.QueryView, identifiers);
				QueryRewriter queryRewriter = this.GenerateViewsForExtentAndType(type, context, identifiers, views, mode);
			}
			catch (InternalMappingException ex)
			{
				errorLog.Merge(ex.ErrorLog);
			}
			return errorLog;
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x00090D24 File Offset: 0x0008EF24
		private ErrorLog GenerateDirectionalViews(ViewTarget viewTarget, CqlIdentifiers identifiers, KeyToListMap<EntitySetBase, GeneratedView> views)
		{
			bool flag = viewTarget == ViewTarget.QueryView;
			KeyToListMap<EntitySetBase, Cell> keyToListMap = ViewGenerator.GroupCellsByExtent(this.m_cellGroup, viewTarget);
			ErrorLog errorLog = new ErrorLog();
			foreach (EntitySetBase entitySetBase in keyToListMap.Keys)
			{
				if (this.m_config.IsViewTracing)
				{
					Helpers.StringTraceLine(string.Empty);
					Helpers.StringTraceLine(string.Empty);
					Helpers.FormatTraceLine("================= Generating {0} View for: {1} ===========================", new object[]
					{
						flag ? "Query" : "Update",
						entitySetBase.Name
					});
					Helpers.StringTraceLine(string.Empty);
					Helpers.StringTraceLine(string.Empty);
				}
				try
				{
					QueryRewriter queryRewriter = this.GenerateDirectionalViewsForExtent(viewTarget, entitySetBase, identifiers, views);
					if (viewTarget == ViewTarget.UpdateView && this.m_config.IsValidationEnabled)
					{
						if (this.m_config.IsViewTracing)
						{
							Helpers.StringTraceLine(string.Empty);
							Helpers.StringTraceLine(string.Empty);
							Helpers.FormatTraceLine("----------------- Validation for generated update view for: {0} -----------------", new object[]
							{
								entitySetBase.Name
							});
							Helpers.StringTraceLine(string.Empty);
							Helpers.StringTraceLine(string.Empty);
						}
						RewritingValidator rewritingValidator = new RewritingValidator(queryRewriter.ViewgenContext, queryRewriter.BasicView);
						rewritingValidator.Validate();
					}
				}
				catch (InternalMappingException ex)
				{
					errorLog.Merge(ex.ErrorLog);
				}
			}
			return errorLog;
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x00090EAC File Offset: 0x0008F0AC
		private QueryRewriter GenerateDirectionalViewsForExtent(ViewTarget viewTarget, EntitySetBase extent, CqlIdentifiers identifiers, KeyToListMap<EntitySetBase, GeneratedView> views)
		{
			ViewgenContext context = this.CreateViewgenContext(extent, viewTarget, identifiers);
			QueryRewriter queryRewriter = null;
			if (this.m_config.GenerateViewsForEachType)
			{
				using (IEnumerator<EdmType> enumerator = MetadataHelper.GetTypeAndSubtypesOf(extent.ElementType, this.m_entityContainerMapping.StorageMappingItemCollection.EdmItemCollection, false).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						EdmType edmType = enumerator.Current;
						if (this.m_config.IsViewTracing && !edmType.Equals(extent.ElementType))
						{
							Helpers.FormatTraceLine("CQL View for {0} and type {1}", new object[]
							{
								extent.Name,
								edmType.Name
							});
						}
						queryRewriter = this.GenerateViewsForExtentAndType(edmType, context, identifiers, views, ViewGenMode.OfTypeViews);
					}
					goto IL_B7;
				}
			}
			queryRewriter = this.GenerateViewsForExtentAndType(extent.ElementType, context, identifiers, views, ViewGenMode.OfTypeViews);
			IL_B7:
			if (viewTarget == ViewTarget.QueryView)
			{
				this.m_config.SetTimeForFinishedActivity(PerfType.QueryViews);
			}
			else
			{
				this.m_config.SetTimeForFinishedActivity(PerfType.UpdateViews);
			}
			this.m_queryRewriterCache[extent] = queryRewriter;
			return queryRewriter;
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x00090FAC File Offset: 0x0008F1AC
		private ViewgenContext CreateViewgenContext(EntitySetBase extent, ViewTarget viewTarget, CqlIdentifiers identifiers)
		{
			QueryRewriter queryRewriter;
			if (!this.m_queryRewriterCache.TryGetValue(extent, out queryRewriter))
			{
				IEnumerable<Cell> extentCells = from c in this.m_cellGroup
				where c.GetLeftQuery(viewTarget).Extent == extent
				select c;
				return new ViewgenContext(viewTarget, extent, extentCells, identifiers, this.m_config, this.m_queryDomainMap, this.m_updateDomainMap, this.m_entityContainerMapping);
			}
			return queryRewriter.ViewgenContext;
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x0009102C File Offset: 0x0008F22C
		private QueryRewriter GenerateViewsForExtentAndType(EdmType generatedType, ViewgenContext context, CqlIdentifiers identifiers, KeyToListMap<EntitySetBase, GeneratedView> views, ViewGenMode mode)
		{
			QueryRewriter queryRewriter = new QueryRewriter(generatedType, context, mode);
			queryRewriter.GenerateViewComponents();
			CellTreeNode basicView = queryRewriter.BasicView;
			if (this.m_config.IsNormalTracing)
			{
				Helpers.StringTrace("Basic View: ");
				Helpers.StringTraceLine(basicView.ToString());
			}
			CellTreeNode cellTreeNode = this.GenerateSimplifiedView(basicView, queryRewriter.UsedCells);
			if (this.m_config.IsNormalTracing)
			{
				Helpers.StringTraceLine(string.Empty);
				Helpers.StringTrace("Simplified View: ");
				Helpers.StringTraceLine(cellTreeNode.ToString());
			}
			CqlGenerator cqlGenerator = new CqlGenerator(cellTreeNode, queryRewriter.CaseStatements, identifiers, context.MemberMaps.ProjectedSlotMap, queryRewriter.UsedCells.Count, queryRewriter.TopLevelWhereClause, this.m_entityContainerMapping.StorageMappingItemCollection);
			string eSQL;
			DbQueryCommandTree commandTree;
			if (this.m_config.GenerateEsql)
			{
				eSQL = cqlGenerator.GenerateEsql();
				commandTree = null;
			}
			else
			{
				eSQL = null;
				commandTree = cqlGenerator.GenerateCqt();
			}
			GeneratedView value = GeneratedView.CreateGeneratedView(context.Extent, generatedType, commandTree, eSQL, this.m_entityContainerMapping.StorageMappingItemCollection, this.m_config);
			views.Add(context.Extent, value);
			return queryRewriter;
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x0009113C File Offset: 0x0008F33C
		private CellTreeNode GenerateSimplifiedView(CellTreeNode basicView, List<LeftCellWrapper> usedCells)
		{
			int count = usedCells.Count;
			for (int i = 0; i < count; i++)
			{
				usedCells[i].RightCellQuery.InitializeBoolExpressions(count, i);
			}
			return CellTreeSimplifier.MergeNodes(basicView);
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x00091178 File Offset: 0x0008F378
		private void CheckForeignKeyConstraints(ErrorLog errorLog)
		{
			foreach (ForeignConstraint foreignConstraint in this.m_foreignKeyConstraints)
			{
				QueryRewriter childRewriter = null;
				QueryRewriter parentRewriter = null;
				this.m_queryRewriterCache.TryGetValue(foreignConstraint.ChildTable, out childRewriter);
				this.m_queryRewriterCache.TryGetValue(foreignConstraint.ParentTable, out parentRewriter);
				foreignConstraint.CheckConstraint(this.m_cellGroup, childRewriter, parentRewriter, errorLog, this.m_config);
			}
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x00091208 File Offset: 0x0008F408
		private static KeyToListMap<EntitySetBase, Cell> GroupCellsByExtent(IEnumerable<Cell> cells, ViewTarget viewTarget)
		{
			KeyToListMap<EntitySetBase, Cell> keyToListMap = new KeyToListMap<EntitySetBase, Cell>(EqualityComparer<EntitySetBase>.Default);
			foreach (Cell cell in cells)
			{
				CellQuery leftQuery = cell.GetLeftQuery(viewTarget);
				keyToListMap.Add(leftQuery.Extent, cell);
			}
			return keyToListMap;
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x0009126C File Offset: 0x0008F46C
		internal override void ToCompactString(StringBuilder builder)
		{
			Cell.CellsToBuilder(builder, this.m_cellGroup);
		}

		// Token: 0x0400118B RID: 4491
		private Set<Cell> m_cellGroup;

		// Token: 0x0400118C RID: 4492
		private ConfigViewGenerator m_config;

		// Token: 0x0400118D RID: 4493
		private MemberDomainMap m_queryDomainMap;

		// Token: 0x0400118E RID: 4494
		private MemberDomainMap m_updateDomainMap;

		// Token: 0x0400118F RID: 4495
		private Dictionary<EntitySetBase, QueryRewriter> m_queryRewriterCache;

		// Token: 0x04001190 RID: 4496
		private List<ForeignConstraint> m_foreignKeyConstraints;

		// Token: 0x04001191 RID: 4497
		private StorageEntityContainerMapping m_entityContainerMapping;
	}
}
