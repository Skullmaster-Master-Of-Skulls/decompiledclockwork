using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Mapping.ViewGeneration.QueryRewriting;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Data.Mapping.ViewGeneration.Utils;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Mapping.ViewGeneration
{
	// Token: 0x02000262 RID: 610
	internal class ViewgenContext : InternalBase
	{
		// Token: 0x060025AF RID: 9647 RVA: 0x0008DF74 File Offset: 0x0008C174
		internal ViewgenContext(ViewTarget viewTarget, EntitySetBase extent, IEnumerable<Cell> extentCells, CqlIdentifiers identifiers, ConfigViewGenerator config, MemberDomainMap queryDomainMap, MemberDomainMap updateDomainMap, StorageEntityContainerMapping entityContainerMapping)
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
			FragmentQueryKB fragmentQueryKB = new FragmentQueryKB();
			fragmentQueryKB.CreateVariableConstraints(extent, domainMap, this.m_edmItemCollection);
			this.m_leftFragmentQP = new FragmentQueryProcessor(fragmentQueryKB);
			this.m_rewritingCache = new Dictionary<FragmentQuery, Tile<FragmentQuery>>(FragmentQuery.GetEqualityComparer(this.m_leftFragmentQP));
			if (!this.CreateLeftCellWrappers(extentCells, viewTarget))
			{
				return;
			}
			FragmentQueryKB fragmentQueryKB2 = new FragmentQueryKB();
			MemberDomainMap memberDomainMap = (viewTarget == ViewTarget.QueryView) ? updateDomainMap : queryDomainMap;
			foreach (LeftCellWrapper leftCellWrapper in this.m_cellWrappers)
			{
				EntitySetBase rightExtent = leftCellWrapper.RightExtent;
				fragmentQueryKB2.CreateVariableConstraints(rightExtent, memberDomainMap, this.m_edmItemCollection);
				fragmentQueryKB2.CreateAssociationConstraints(rightExtent, memberDomainMap, this.m_edmItemCollection);
			}
			if (this.m_viewTarget == ViewTarget.UpdateView)
			{
				this.CreateConstraintsForForeignKeyAssociationsAffectingThisWarapper(fragmentQueryKB2, memberDomainMap);
			}
			this.m_rightFragmentQP = new FragmentQueryProcessor(fragmentQueryKB2);
			if (this.m_viewTarget == ViewTarget.QueryView)
			{
				this.CheckConcurrencyControlTokens();
			}
			this.m_cellWrappers.Sort(LeftCellWrapper.Comparer);
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x0008E118 File Offset: 0x0008C318
		private void CreateConstraintsForForeignKeyAssociationsAffectingThisWarapper(FragmentQueryKB rightKB, MemberDomainMap rightDomainMap)
		{
			IEnumerable<EntityType> entityTypes = from it in (from it in this.m_cellWrappers
			select it.RightExtent).OfType<EntitySet>()
			select it.ElementType;
			IEnumerable<AssociationSet> source = from it in this.m_entityContainerMapping.EdmEntityContainer.BaseEntitySets.OfType<AssociationSet>()
			where it.ElementType.IsForeignKey
			select it;
			IEnumerable<AssociationType> oneToOneForeignKeyAssociationsForThisWrapper = from it in source
			select it.ElementType;
			oneToOneForeignKeyAssociationsForThisWrapper = from it in oneToOneForeignKeyAssociationsForThisWrapper
			where it.AssociationEndMembers.All((AssociationEndMember endMember) => endMember.RelationshipMultiplicity == RelationshipMultiplicity.One)
			select it;
			Func<AssociationEndMember, bool> <>9__8;
			oneToOneForeignKeyAssociationsForThisWrapper = oneToOneForeignKeyAssociationsForThisWrapper.Where(delegate(AssociationType it)
			{
				IEnumerable<AssociationEndMember> associationEndMembers = it.AssociationEndMembers;
				Func<AssociationEndMember, bool> predicate;
				if ((predicate = <>9__8) == null)
				{
					predicate = (<>9__8 = ((AssociationEndMember endMember) => entityTypes.Contains(endMember.GetEntityType())));
				}
				return associationEndMembers.All(predicate);
			});
			IEnumerable<AssociationSet> enumerable = from it in source
			where oneToOneForeignKeyAssociationsForThisWrapper.Contains(it.ElementType)
			select it;
			foreach (AssociationSet assocSet in enumerable)
			{
				rightKB.CreateEquivalenceConstraintForOneToOneForeignKeyAssociation(assocSet, rightDomainMap, this.m_edmItemCollection);
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x060025B1 RID: 9649 RVA: 0x0008E290 File Offset: 0x0008C490
		internal ViewTarget ViewTarget
		{
			get
			{
				return this.m_viewTarget;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x060025B2 RID: 9650 RVA: 0x0008E298 File Offset: 0x0008C498
		internal MemberMaps MemberMaps
		{
			get
			{
				return this.m_memberMaps;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x060025B3 RID: 9651 RVA: 0x0008E2A0 File Offset: 0x0008C4A0
		internal EntitySetBase Extent
		{
			get
			{
				return this.m_extent;
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x060025B4 RID: 9652 RVA: 0x0008E2A8 File Offset: 0x0008C4A8
		internal ConfigViewGenerator Config
		{
			get
			{
				return this.m_config;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x060025B5 RID: 9653 RVA: 0x0008E2B0 File Offset: 0x0008C4B0
		internal CqlIdentifiers CqlIdentifiers
		{
			get
			{
				return this.m_identifiers;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x060025B6 RID: 9654 RVA: 0x0008E2B8 File Offset: 0x0008C4B8
		internal EdmItemCollection EdmItemCollection
		{
			get
			{
				return this.m_edmItemCollection;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x060025B7 RID: 9655 RVA: 0x0008E2C0 File Offset: 0x0008C4C0
		internal FragmentQueryProcessor LeftFragmentQP
		{
			get
			{
				return this.m_leftFragmentQP;
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x060025B8 RID: 9656 RVA: 0x0008E2C8 File Offset: 0x0008C4C8
		internal FragmentQueryProcessor RightFragmentQP
		{
			get
			{
				return this.m_rightFragmentQP;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x060025B9 RID: 9657 RVA: 0x0008E2D0 File Offset: 0x0008C4D0
		internal List<LeftCellWrapper> AllWrappersForExtent
		{
			get
			{
				return this.m_cellWrappers;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x060025BA RID: 9658 RVA: 0x0008E2D8 File Offset: 0x0008C4D8
		internal StorageEntityContainerMapping EntityContainerMapping
		{
			get
			{
				return this.m_entityContainerMapping;
			}
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x0008E2E0 File Offset: 0x0008C4E0
		internal bool TryGetCachedRewriting(FragmentQuery query, out Tile<FragmentQuery> rewriting)
		{
			return this.m_rewritingCache.TryGetValue(query, out rewriting);
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x0008E2EF File Offset: 0x0008C4EF
		internal void SetCachedRewriting(FragmentQuery query, Tile<FragmentQuery> rewriting)
		{
			this.m_rewritingCache[query] = rewriting;
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x0008E300 File Offset: 0x0008C500
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
					ErrorLog.Record errorRecord = new ErrorLog.Record(true, ViewGenErrorCode.ConcurrencyDerivedClass, message, this.m_cellWrappers, string.Empty);
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
						ErrorLog.Record errorRecord2 = new ErrorLog.Record(true, ViewGenErrorCode.ConcurrencyTokenHasCondition, stringBuilder.ToString(), new LeftCellWrapper[]
						{
							leftCellWrapper
						}, string.Empty);
						ExceptionHelpers.ThrowMappingException(errorRecord2, this.m_config);
					}
				}
			}
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x0008E4D4 File Offset: 0x0008C6D4
		private bool CreateLeftCellWrappers(IEnumerable<Cell> extentCells, ViewTarget viewTarget)
		{
			List<Cell> list = new List<Cell>(extentCells);
			List<Cell> list2 = ViewgenContext.AlignFields(list, this.m_memberMaps.ProjectedSlotMap, viewTarget);
			this.m_cellWrappers = new List<LeftCellWrapper>();
			for (int i = 0; i < list2.Count; i++)
			{
				Cell cell = list2[i];
				CellQuery leftQuery = cell.GetLeftQuery(viewTarget);
				CellQuery rightQuery = cell.GetRightQuery(viewTarget);
				Set<MemberPath> nonNullSlots = leftQuery.GetNonNullSlots();
				BoolExpression fromVariable = BoolExpression.CreateLiteral(new CellIdBoolean(this.m_identifiers, list[i].CellNumber), this.m_memberMaps.LeftDomainMap);
				FragmentQuery fragmentQuery = FragmentQuery.Create(fromVariable, leftQuery);
				FragmentQuery fragmentQuery2 = FragmentQuery.Create(fromVariable, rightQuery);
				if (viewTarget == ViewTarget.UpdateView)
				{
					fragmentQuery = (this.m_leftFragmentQP.CreateDerivedViewBySelectingConstantAttributes(fragmentQuery) ?? fragmentQuery);
				}
				LeftCellWrapper item = new LeftCellWrapper(this.m_viewTarget, nonNullSlots, fragmentQuery, leftQuery, rightQuery, this.m_memberMaps, list[i]);
				this.m_cellWrappers.Add(item);
			}
			return true;
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x0008E5C8 File Offset: 0x0008C7C8
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

		// Token: 0x060025C0 RID: 9664 RVA: 0x0008E668 File Offset: 0x0008C868
		internal override void ToCompactString(StringBuilder builder)
		{
			LeftCellWrapper.WrappersToStringBuilder(builder, this.m_cellWrappers, "Left Celll Wrappers");
		}

		// Token: 0x04001148 RID: 4424
		private ConfigViewGenerator m_config;

		// Token: 0x04001149 RID: 4425
		private ViewTarget m_viewTarget;

		// Token: 0x0400114A RID: 4426
		private EntitySetBase m_extent;

		// Token: 0x0400114B RID: 4427
		private MemberMaps m_memberMaps;

		// Token: 0x0400114C RID: 4428
		private EdmItemCollection m_edmItemCollection;

		// Token: 0x0400114D RID: 4429
		private StorageEntityContainerMapping m_entityContainerMapping;

		// Token: 0x0400114E RID: 4430
		private List<LeftCellWrapper> m_cellWrappers;

		// Token: 0x0400114F RID: 4431
		private FragmentQueryProcessor m_leftFragmentQP;

		// Token: 0x04001150 RID: 4432
		private FragmentQueryProcessor m_rightFragmentQP;

		// Token: 0x04001151 RID: 4433
		private CqlIdentifiers m_identifiers;

		// Token: 0x04001152 RID: 4434
		private Dictionary<FragmentQuery, Tile<FragmentQuery>> m_rewritingCache;
	}
}
