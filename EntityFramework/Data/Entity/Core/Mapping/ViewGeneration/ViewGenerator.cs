using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Mapping.ViewGeneration.Validation;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration
{
	// Token: 0x020004A1 RID: 1185
	internal class ViewGenerator : InternalBase
	{
		// Token: 0x06002BB4 RID: 11188 RVA: 0x000D4BE8 File Offset: 0x000D2DE8
		internal ViewGenerator(Set<Cell> cellGroup, ConfigViewGenerator config, List<ForeignConstraint> foreignKeyConstraints, EntityContainerMapping entityContainerMapping)
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

		// Token: 0x06002BB5 RID: 11189 RVA: 0x000D4D80 File Offset: 0x000D2F80
		private void SetConfiguration(EntityContainerMapping entityContainerMapping)
		{
			this.m_config.IsValidationEnabled = entityContainerMapping.Validate;
			this.m_config.GenerateUpdateViews = entityContainerMapping.GenerateUpdateViews;
		}

		// Token: 0x06002BB6 RID: 11190 RVA: 0x000D4DA4 File Offset: 0x000D2FA4
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

		// Token: 0x06002BB7 RID: 11191 RVA: 0x000D4E88 File Offset: 0x000D3088
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
			return this.GenerateQueryViewForExtentAndType(identifiers, views, entity, type, mode);
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x000D4F80 File Offset: 0x000D3180
		private static void UpdateWhereClauseForEachCell(IEnumerable<Cell> extentCells, MemberDomainMap queryDomainMap, MemberDomainMap updateDomainMap, ConfigViewGenerator config)
		{
			foreach (Cell cell in extentCells)
			{
				cell.CQuery.UpdateWhereClause(queryDomainMap);
				cell.SQuery.UpdateWhereClause(updateDomainMap);
			}
			queryDomainMap.ReduceEnumerableDomainToEnumeratedValues(config);
			updateDomainMap.ReduceEnumerableDomainToEnumeratedValues(config);
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x000D4FE8 File Offset: 0x000D31E8
		private ErrorLog GenerateQueryViewForExtentAndType(CqlIdentifiers identifiers, KeyToListMap<EntitySetBase, GeneratedView> views, EntitySetBase entity, EntityTypeBase type, ViewGenMode mode)
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
				this.GenerateViewsForExtentAndType(type, context, identifiers, views, mode);
			}
			catch (InternalMappingException ex)
			{
				errorLog.Merge(ex.ErrorLog);
			}
			return errorLog;
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x000D509C File Offset: 0x000D329C
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

		// Token: 0x06002BBB RID: 11195 RVA: 0x000D5230 File Offset: 0x000D3430
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
					goto IL_BD;
				}
			}
			queryRewriter = this.GenerateViewsForExtentAndType(extent.ElementType, context, identifiers, views, ViewGenMode.OfTypeViews);
			IL_BD:
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

		// Token: 0x06002BBC RID: 11196 RVA: 0x000D535C File Offset: 0x000D355C
		private ViewgenContext CreateViewgenContext(EntitySetBase extent, ViewTarget viewTarget, CqlIdentifiers identifiers)
		{
			QueryRewriter queryRewriter;
			if (!this.m_queryRewriterCache.TryGetValue(extent, out queryRewriter))
			{
				List<Cell> extentCells = (from c in this.m_cellGroup
				where c.GetLeftQuery(viewTarget).Extent == extent
				select c).ToList<Cell>();
				return new ViewgenContext(viewTarget, extent, extentCells, identifiers, this.m_config, this.m_queryDomainMap, this.m_updateDomainMap, this.m_entityContainerMapping);
			}
			return queryRewriter.ViewgenContext;
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x000D53E8 File Offset: 0x000D35E8
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
			CellTreeNode cellTreeNode = ViewGenerator.GenerateSimplifiedView(basicView, queryRewriter.UsedCells);
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

		// Token: 0x06002BBE RID: 11198 RVA: 0x000D54F4 File Offset: 0x000D36F4
		private static CellTreeNode GenerateSimplifiedView(CellTreeNode basicView, List<LeftCellWrapper> usedCells)
		{
			int count = usedCells.Count;
			for (int i = 0; i < count; i++)
			{
				usedCells[i].RightCellQuery.InitializeBoolExpressions(count, i);
			}
			return CellTreeSimplifier.MergeNodes(basicView);
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x000D5530 File Offset: 0x000D3730
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

		// Token: 0x06002BC0 RID: 11200 RVA: 0x000D55C0 File Offset: 0x000D37C0
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

		// Token: 0x06002BC1 RID: 11201 RVA: 0x000D5624 File Offset: 0x000D3824
		internal override void ToCompactString(StringBuilder builder)
		{
			Cell.CellsToBuilder(builder, this.m_cellGroup);
		}

		// Token: 0x04001028 RID: 4136
		private readonly Set<Cell> m_cellGroup;

		// Token: 0x04001029 RID: 4137
		private readonly ConfigViewGenerator m_config;

		// Token: 0x0400102A RID: 4138
		private readonly MemberDomainMap m_queryDomainMap;

		// Token: 0x0400102B RID: 4139
		private readonly MemberDomainMap m_updateDomainMap;

		// Token: 0x0400102C RID: 4140
		private readonly Dictionary<EntitySetBase, QueryRewriter> m_queryRewriterCache;

		// Token: 0x0400102D RID: 4141
		private readonly List<ForeignConstraint> m_foreignKeyConstraints;

		// Token: 0x0400102E RID: 4142
		private readonly EntityContainerMapping m_entityContainerMapping;
	}
}
