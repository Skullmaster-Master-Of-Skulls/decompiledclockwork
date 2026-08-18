using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration
{
	// Token: 0x0200049F RID: 1183
	internal class ViewgenContext : InternalBase
	{
		// Token: 0x06002B9D RID: 11165 RVA: 0x000D4504 File Offset: 0x000D2704
		internal ViewgenContext(ViewTarget viewTarget, EntitySetBase extent, IList<Cell> extentCells, CqlIdentifiers identifiers, ConfigViewGenerator config, MemberDomainMap queryDomainMap, MemberDomainMap updateDomainMap, EntityContainerMapping entityContainerMapping)
		{
			foreach (Cell cell in extentCells)
			{
			}
			this.m_extent = extent;
			this.m_viewTarget = viewTarget;
			this.m_config = config;
			this.m_edmItemCollection = entityContainerMapping.StorageMappingItemCollection.EdmItemCollection;
			this.m_entityContainerMapping = entityContainerMapping;
			this.m_identifiers = identifiers;
			updateDomainMap = updateDomainMap.MakeCopy();
			MemberDomainMap domainMap = (viewTarget == ViewTarget.QueryView) ? queryDomainMap : updateDomainMap;
			this.m_memberMaps = new MemberMaps(viewTarget, MemberProjectionIndex.Create(extent, this.m_edmItemCollection), queryDomainMap, updateDomainMap);
			FragmentQueryKBChaseSupport fragmentQueryKBChaseSupport = new FragmentQueryKBChaseSupport();
			fragmentQueryKBChaseSupport.CreateVariableConstraints(extent, domainMap, this.m_edmItemCollection);
			this.m_leftFragmentQP = new FragmentQueryProcessor(fragmentQueryKBChaseSupport);
			this.m_rewritingCache = new Dictionary<FragmentQuery, Tile<FragmentQuery>>(FragmentQuery.GetEqualityComparer(this.m_leftFragmentQP));
			if (!this.CreateLeftCellWrappers(extentCells, viewTarget))
			{
				return;
			}
			FragmentQueryKBChaseSupport fragmentQueryKBChaseSupport2 = new FragmentQueryKBChaseSupport();
			MemberDomainMap memberDomainMap = (viewTarget == ViewTarget.QueryView) ? updateDomainMap : queryDomainMap;
			foreach (LeftCellWrapper leftCellWrapper in this.m_cellWrappers)
			{
				EntitySetBase rightExtent = leftCellWrapper.RightExtent;
				fragmentQueryKBChaseSupport2.CreateVariableConstraints(rightExtent, memberDomainMap, this.m_edmItemCollection);
				fragmentQueryKBChaseSupport2.CreateAssociationConstraints(rightExtent, memberDomainMap, this.m_edmItemCollection);
			}
			if (this.m_viewTarget == ViewTarget.UpdateView)
			{
				this.CreateConstraintsForForeignKeyAssociationsAffectingThisWrapper(fragmentQueryKBChaseSupport2, memberDomainMap);
			}
			this.m_rightFragmentQP = new FragmentQueryProcessor(fragmentQueryKBChaseSupport2);
			if (this.m_viewTarget == ViewTarget.QueryView)
			{
				this.CheckConcurrencyControlTokens();
			}
			this.m_cellWrappers.Sort(LeftCellWrapper.Comparer);
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x000D46B8 File Offset: 0x000D28B8
		private void CreateConstraintsForForeignKeyAssociationsAffectingThisWrapper(FragmentQueryKB rightKB, MemberDomainMap rightDomainMap)
		{
			IEnumerable<AssociationSet> enumerable = new ViewgenContext.OneToOneFkAssociationsForEntitiesFilter().Filter((from it in (from it in this.m_cellWrappers
			select it.RightExtent).OfType<EntitySet>()
			select it.ElementType).ToList<EntityType>(), this.m_entityContainerMapping.EdmEntityContainer.BaseEntitySets.OfType<AssociationSet>());
			foreach (AssociationSet assocSet in enumerable)
			{
				rightKB.CreateEquivalenceConstraintForOneToOneForeignKeyAssociation(assocSet, rightDomainMap);
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06002B9F RID: 11167 RVA: 0x000D4778 File Offset: 0x000D2978
		internal ViewTarget ViewTarget
		{
			get
			{
				return this.m_viewTarget;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06002BA0 RID: 11168 RVA: 0x000D4780 File Offset: 0x000D2980
		internal MemberMaps MemberMaps
		{
			get
			{
				return this.m_memberMaps;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06002BA1 RID: 11169 RVA: 0x000D4788 File Offset: 0x000D2988
		internal EntitySetBase Extent
		{
			get
			{
				return this.m_extent;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06002BA2 RID: 11170 RVA: 0x000D4790 File Offset: 0x000D2990
		internal ConfigViewGenerator Config
		{
			get
			{
				return this.m_config;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06002BA3 RID: 11171 RVA: 0x000D4798 File Offset: 0x000D2998
		internal CqlIdentifiers CqlIdentifiers
		{
			get
			{
				return this.m_identifiers;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06002BA4 RID: 11172 RVA: 0x000D47A0 File Offset: 0x000D29A0
		internal EdmItemCollection EdmItemCollection
		{
			get
			{
				return this.m_edmItemCollection;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06002BA5 RID: 11173 RVA: 0x000D47A8 File Offset: 0x000D29A8
		internal FragmentQueryProcessor LeftFragmentQP
		{
			get
			{
				return this.m_leftFragmentQP;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06002BA6 RID: 11174 RVA: 0x000D47B0 File Offset: 0x000D29B0
		internal FragmentQueryProcessor RightFragmentQP
		{
			get
			{
				return this.m_rightFragmentQP;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06002BA7 RID: 11175 RVA: 0x000D47B8 File Offset: 0x000D29B8
		internal List<LeftCellWrapper> AllWrappersForExtent
		{
			get
			{
				return this.m_cellWrappers;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06002BA8 RID: 11176 RVA: 0x000D47C0 File Offset: 0x000D29C0
		internal EntityContainerMapping EntityContainerMapping
		{
			get
			{
				return this.m_entityContainerMapping;
			}
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x000D47C8 File Offset: 0x000D29C8
		internal bool TryGetCachedRewriting(FragmentQuery query, out Tile<FragmentQuery> rewriting)
		{
			return this.m_rewritingCache.TryGetValue(query, out rewriting);
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x000D47D7 File Offset: 0x000D29D7
		internal void SetCachedRewriting(FragmentQuery query, Tile<FragmentQuery> rewriting)
		{
			this.m_rewritingCache[query] = rewriting;
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x000D47F4 File Offset: 0x000D29F4
		private void CheckConcurrencyControlTokens()
		{
			EntityTypeBase elementType = this.m_extent.ElementType;
			Set<EdmMember> concurrencyMembersForTypeHierarchy = MetadataHelper.GetConcurrencyMembersForTypeHierarchy(elementType, this.m_edmItemCollection);
			Set<MemberPath> set = new Set<MemberPath>(MemberPath.EqualityComparer);
			foreach (EdmMember edmMember in concurrencyMembersForTypeHierarchy)
			{
				if (!edmMember.DeclaringType.IsAssignableFrom(elementType))
				{
					string message = Strings.ViewGen_Concurrency_Derived_Class(edmMember.Name, edmMember.DeclaringType.Name, this.m_extent);
					ErrorLog.Record errorRecord = new ErrorLog.Record(ViewGenErrorCode.ConcurrencyDerivedClass, message, this.m_cellWrappers, string.Empty);
					ExceptionHelpers.ThrowMappingException(errorRecord, this.m_config);
				}
				set.Add(new MemberPath(this.m_extent, edmMember));
			}
			if (concurrencyMembersForTypeHierarchy.Count > 0)
			{
				foreach (LeftCellWrapper leftCellWrapper in this.m_cellWrappers)
				{
					Set<MemberPath> set2 = new Set<MemberPath>(from oneOf in leftCellWrapper.OnlyInputCell.CQuery.WhereClause.MemberRestrictions
					select oneOf.RestrictedMemberSlot.MemberPath, MemberPath.EqualityComparer);
					set2.Intersect(set);
					if (set2.Count > 0)
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.AppendLine(Strings.ViewGen_Concurrency_Invalid_Condition(MemberPath.PropertiesToUserString(set2, false), this.m_extent.Name));
						ErrorLog.Record errorRecord2 = new ErrorLog.Record(ViewGenErrorCode.ConcurrencyTokenHasCondition, stringBuilder.ToString(), new LeftCellWrapper[]
						{
							leftCellWrapper
						}, string.Empty);
						ExceptionHelpers.ThrowMappingException(errorRecord2, this.m_config);
					}
				}
			}
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x000D49C8 File Offset: 0x000D2BC8
		private bool CreateLeftCellWrappers(IList<Cell> extentCells, ViewTarget viewTarget)
		{
			List<Cell> list = ViewgenContext.AlignFields(extentCells, this.m_memberMaps.ProjectedSlotMap, viewTarget);
			this.m_cellWrappers = new List<LeftCellWrapper>();
			for (int i = 0; i < list.Count; i++)
			{
				Cell cell = list[i];
				CellQuery leftQuery = cell.GetLeftQuery(viewTarget);
				CellQuery rightQuery = cell.GetRightQuery(viewTarget);
				Set<MemberPath> nonNullSlots = leftQuery.GetNonNullSlots();
				BoolExpression fromVariable = BoolExpression.CreateLiteral(new CellIdBoolean(this.m_identifiers, extentCells[i].CellNumber), this.m_memberMaps.LeftDomainMap);
				FragmentQuery fragmentQuery = FragmentQuery.Create(fromVariable, leftQuery);
				if (viewTarget == ViewTarget.UpdateView)
				{
					fragmentQuery = (this.m_leftFragmentQP.CreateDerivedViewBySelectingConstantAttributes(fragmentQuery) ?? fragmentQuery);
				}
				LeftCellWrapper item = new LeftCellWrapper(this.m_viewTarget, nonNullSlots, fragmentQuery, leftQuery, rightQuery, this.m_memberMaps, extentCells[i]);
				this.m_cellWrappers.Add(item);
			}
			return true;
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x000D4AA8 File Offset: 0x000D2CA8
		private static List<Cell> AlignFields(IEnumerable<Cell> cells, MemberProjectionIndex projectedSlotMap, ViewTarget viewTarget)
		{
			List<Cell> list = new List<Cell>();
			foreach (Cell cell in cells)
			{
				CellQuery leftQuery = cell.GetLeftQuery(viewTarget);
				CellQuery rightQuery = cell.GetRightQuery(viewTarget);
				CellQuery cellQuery;
				CellQuery cellQuery2;
				leftQuery.CreateFieldAlignedCellQueries(rightQuery, projectedSlotMap, out cellQuery, out cellQuery2);
				Cell item = (viewTarget == ViewTarget.QueryView) ? Cell.CreateCS(cellQuery, cellQuery2, cell.CellLabel, cell.CellNumber) : Cell.CreateCS(cellQuery2, cellQuery, cell.CellLabel, cell.CellNumber);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x000D4B4C File Offset: 0x000D2D4C
		internal override void ToCompactString(StringBuilder builder)
		{
			LeftCellWrapper.WrappersToStringBuilder(builder, this.m_cellWrappers, "Left Celll Wrappers");
		}

		// Token: 0x0400101A RID: 4122
		private readonly ConfigViewGenerator m_config;

		// Token: 0x0400101B RID: 4123
		private readonly ViewTarget m_viewTarget;

		// Token: 0x0400101C RID: 4124
		private readonly EntitySetBase m_extent;

		// Token: 0x0400101D RID: 4125
		private readonly MemberMaps m_memberMaps;

		// Token: 0x0400101E RID: 4126
		private readonly EdmItemCollection m_edmItemCollection;

		// Token: 0x0400101F RID: 4127
		private readonly EntityContainerMapping m_entityContainerMapping;

		// Token: 0x04001020 RID: 4128
		private List<LeftCellWrapper> m_cellWrappers;

		// Token: 0x04001021 RID: 4129
		private readonly FragmentQueryProcessor m_leftFragmentQP;

		// Token: 0x04001022 RID: 4130
		private readonly FragmentQueryProcessor m_rightFragmentQP;

		// Token: 0x04001023 RID: 4131
		private readonly CqlIdentifiers m_identifiers;

		// Token: 0x04001024 RID: 4132
		private readonly Dictionary<FragmentQuery, Tile<FragmentQuery>> m_rewritingCache;

		// Token: 0x020004A0 RID: 1184
		internal class OneToOneFkAssociationsForEntitiesFilter
		{
			// Token: 0x06002BB2 RID: 11186 RVA: 0x000D4BB4 File Offset: 0x000D2DB4
			public virtual IEnumerable<AssociationSet> Filter(IList<EntityType> entityTypes, IEnumerable<AssociationSet> associationSets)
			{
				return from a in associationSets
				where a.ElementType.IsForeignKey && a.ElementType.AssociationEndMembers.All((AssociationEndMember aem) => aem.RelationshipMultiplicity == RelationshipMultiplicity.One && entityTypes.Contains(aem.GetEntityType()))
				select a;
			}
		}
	}
}
