using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Mapping.ViewGeneration.QueryRewriting;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x020002AC RID: 684
	internal class LeafCellTreeNode : CellTreeNode
	{
		// Token: 0x060028A0 RID: 10400 RVA: 0x0009D308 File Offset: 0x0009B508
		internal LeafCellTreeNode(ViewgenContext context, LeftCellWrapper cellWrapper) : base(context)
		{
			this.m_cellWrapper = cellWrapper;
			this.m_leftFragmentQuery = cellWrapper.FragmentQuery;
			this.m_rightFragmentQuery = FragmentQuery.Create(cellWrapper.OriginalCellNumberString, cellWrapper.CreateRoleBoolean(), cellWrapper.RightCellQuery);
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x0009D341 File Offset: 0x0009B541
		internal LeafCellTreeNode(ViewgenContext context, LeftCellWrapper cellWrapper, FragmentQuery rightFragmentQuery) : base(context)
		{
			this.m_cellWrapper = cellWrapper;
			this.m_leftFragmentQuery = cellWrapper.FragmentQuery;
			this.m_rightFragmentQuery = rightFragmentQuery;
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x060028A2 RID: 10402 RVA: 0x0009D364 File Offset: 0x0009B564
		internal LeftCellWrapper LeftCellWrapper
		{
			get
			{
				return this.m_cellWrapper;
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x060028A3 RID: 10403 RVA: 0x0009D36C File Offset: 0x0009B56C
		internal override MemberDomainMap RightDomainMap
		{
			get
			{
				return this.m_cellWrapper.RightDomainMap;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x060028A4 RID: 10404 RVA: 0x0009D379 File Offset: 0x0009B579
		internal override FragmentQuery LeftFragmentQuery
		{
			get
			{
				return this.m_cellWrapper.FragmentQuery;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x060028A5 RID: 10405 RVA: 0x0009D386 File Offset: 0x0009B586
		internal override FragmentQuery RightFragmentQuery
		{
			get
			{
				return this.m_rightFragmentQuery;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x060028A6 RID: 10406 RVA: 0x0009D38E File Offset: 0x0009B58E
		internal override Set<MemberPath> Attributes
		{
			get
			{
				return this.m_cellWrapper.Attributes;
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x060028A7 RID: 10407 RVA: 0x0009D39B File Offset: 0x0009B59B
		internal override List<CellTreeNode> Children
		{
			get
			{
				return new List<CellTreeNode>();
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x060028A8 RID: 10408 RVA: 0x000173E2 File Offset: 0x000155E2
		internal override CellTreeOpType OpType
		{
			get
			{
				return CellTreeOpType.Leaf;
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x060028A9 RID: 10409 RVA: 0x0009D3A2 File Offset: 0x0009B5A2
		internal override int NumProjectedSlots
		{
			get
			{
				return this.LeftCellWrapper.RightCellQuery.NumProjectedSlots;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x060028AA RID: 10410 RVA: 0x0009D3B4 File Offset: 0x0009B5B4
		internal override int NumBoolSlots
		{
			get
			{
				return this.LeftCellWrapper.RightCellQuery.NumBoolVars;
			}
		}

		// Token: 0x060028AB RID: 10411 RVA: 0x0009D3C6 File Offset: 0x0009B5C6
		internal override TOutput Accept<TInput, TOutput>(CellTreeNode.CellTreeVisitor<TInput, TOutput> visitor, TInput param)
		{
			return visitor.VisitLeaf(this, param);
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x0009D3D0 File Offset: 0x0009B5D0
		internal override TOutput Accept<TInput, TOutput>(CellTreeNode.SimpleCellTreeVisitor<TInput, TOutput> visitor, TInput param)
		{
			return visitor.VisitLeaf(this, param);
		}

		// Token: 0x060028AD RID: 10413 RVA: 0x0009D3DC File Offset: 0x0009B5DC
		internal override bool IsProjectedSlot(int slot)
		{
			CellQuery rightCellQuery = this.LeftCellWrapper.RightCellQuery;
			if (base.IsBoolSlot(slot))
			{
				return rightCellQuery.GetBoolVar(base.SlotToBoolIndex(slot)) != null;
			}
			return rightCellQuery.ProjectedSlotAt(slot) != null;
		}

		// Token: 0x060028AE RID: 10414 RVA: 0x0009D41C File Offset: 0x0009B61C
		internal override CqlBlock ToCqlBlock(bool[] requiredSlots, CqlIdentifiers identifiers, ref int blockAliasNum, ref List<WithRelationship> withRelationships)
		{
			int num = requiredSlots.Length;
			CellQuery rightCellQuery = this.LeftCellWrapper.RightCellQuery;
			SlotInfo[] array = new SlotInfo[num];
			for (int i = 0; i < rightCellQuery.NumProjectedSlots; i++)
			{
				ProjectedSlot projectedSlot = rightCellQuery.ProjectedSlotAt(i);
				if (requiredSlots[i] && projectedSlot == null)
				{
					MemberPath memberPath = base.ProjectedSlotMap[i];
					ConstantProjectedSlot constantProjectedSlot = new ConstantProjectedSlot(Domain.GetDefaultValueForMemberPath(memberPath, base.GetLeaves(), base.ViewgenContext.Config), memberPath);
					rightCellQuery.FixMissingSlotAsDefaultConstant(i, constantProjectedSlot);
					projectedSlot = constantProjectedSlot;
				}
				SlotInfo slotInfo = new SlotInfo(requiredSlots[i], projectedSlot != null, projectedSlot, base.ProjectedSlotMap[i]);
				array[i] = slotInfo;
			}
			for (int j = 0; j < rightCellQuery.NumBoolVars; j++)
			{
				BoolExpression boolVar = rightCellQuery.GetBoolVar(j);
				BooleanProjectedSlot slotValue;
				if (boolVar != null)
				{
					slotValue = new BooleanProjectedSlot(boolVar, identifiers, j);
				}
				else
				{
					slotValue = new BooleanProjectedSlot(BoolExpression.False, identifiers, j);
				}
				int num2 = base.BoolIndexToSlot(j);
				SlotInfo slotInfo2 = new SlotInfo(requiredSlots[num2], boolVar != null, slotValue, null);
				array[num2] = slotInfo2;
			}
			IEnumerable<SlotInfo> source = array;
			if (rightCellQuery.Extent.EntityContainer.DataSpace == DataSpace.SSpace && this.m_cellWrapper.LeftExtent.BuiltInTypeKind == BuiltInTypeKind.EntitySet)
			{
				IEnumerable<StorageAssociationSetMapping> relationshipSetMappingsFor = base.ViewgenContext.EntityContainerMapping.GetRelationshipSetMappingsFor(this.m_cellWrapper.LeftExtent, rightCellQuery.Extent);
				List<SlotInfo> second = new List<SlotInfo>();
				foreach (StorageAssociationSetMapping colocatedAssociationSetMap in relationshipSetMappingsFor)
				{
					WithRelationship item;
					if (this.TryGetWithRelationship(colocatedAssociationSetMap, this.m_cellWrapper.LeftExtent, rightCellQuery.SourceExtentMemberPath, ref second, out item))
					{
						withRelationships.Add(item);
						source = array.Concat(second);
					}
				}
			}
			EntitySetBase extent = rightCellQuery.Extent;
			CellQuery.SelectDistinct selectDistinctFlag = rightCellQuery.SelectDistinctFlag;
			SlotInfo[] slots = source.ToArray<SlotInfo>();
			BoolExpression whereClause = rightCellQuery.WhereClause;
			int num3 = blockAliasNum + 1;
			blockAliasNum = num3;
			return new ExtentCqlBlock(extent, selectDistinctFlag, slots, whereClause, identifiers, num3);
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x0009D628 File Offset: 0x0009B828
		private bool TryGetWithRelationship(StorageAssociationSetMapping colocatedAssociationSetMap, EntitySetBase thisExtent, MemberPath sRootNode, ref List<SlotInfo> foreignKeySlots, out WithRelationship withRelationship)
		{
			withRelationship = null;
			StorageEndPropertyMapping foreignKeyEndMapFromAssocitionMap = this.GetForeignKeyEndMapFromAssocitionMap(colocatedAssociationSetMap, thisExtent);
			if (foreignKeyEndMapFromAssocitionMap == null || foreignKeyEndMapFromAssocitionMap.EndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many)
			{
				return false;
			}
			AssociationEndMember associationEndMember = (AssociationEndMember)foreignKeyEndMapFromAssocitionMap.EndMember;
			AssociationEndMember otherAssociationEnd = MetadataHelper.GetOtherAssociationEnd(associationEndMember);
			EntityType entityType = (EntityType)((RefType)associationEndMember.TypeUsage.EdmType).ElementType;
			EntityType entityType2 = (EntityType)((RefType)otherAssociationEnd.TypeUsage.EdmType).ElementType;
			AssociationSet associationSet = (AssociationSet)colocatedAssociationSetMap.Set;
			MemberPath prefix = new MemberPath(associationSet, associationEndMember);
			IEnumerable<StorageScalarPropertyMapping> source = foreignKeyEndMapFromAssocitionMap.Properties.Cast<StorageScalarPropertyMapping>();
			List<MemberPath> list = new List<MemberPath>();
			using (ReadOnlyMetadataCollection<EdmMember>.Enumerator enumerator = entityType.KeyMembers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EdmProperty edmProperty = (EdmProperty)enumerator.Current;
					IEnumerable<StorageScalarPropertyMapping> source2 = from propMap in source
					where propMap.EdmProperty.Equals(edmProperty)
					select propMap;
					StorageScalarPropertyMapping storageScalarPropertyMapping = source2.First<StorageScalarPropertyMapping>();
					MemberProjectedSlot slotValue = new MemberProjectedSlot(new MemberPath(sRootNode, storageScalarPropertyMapping.ColumnProperty));
					MemberPath memberPath = new MemberPath(prefix, edmProperty);
					list.Add(memberPath);
					foreignKeySlots.Add(new SlotInfo(true, true, slotValue, memberPath));
				}
			}
			if (thisExtent.ElementType.IsAssignableFrom(entityType2))
			{
				withRelationship = new WithRelationship(associationSet, otherAssociationEnd, entityType2, associationEndMember, entityType, list);
				return true;
			}
			return false;
		}

		// Token: 0x060028B0 RID: 10416 RVA: 0x0009D7A4 File Offset: 0x0009B9A4
		private StorageEndPropertyMapping GetForeignKeyEndMapFromAssocitionMap(StorageAssociationSetMapping colocatedAssociationSetMap, EntitySetBase thisExtent)
		{
			StorageMappingFragment storageMappingFragment = colocatedAssociationSetMap.TypeMappings.First<StorageTypeMapping>().MappingFragments.First<StorageMappingFragment>();
			EntitySet entitySet = (EntitySet)colocatedAssociationSetMap.StoreEntitySet;
			IEnumerable<EdmMember> keyMembers = entitySet.ElementType.KeyMembers;
			using (IEnumerator<StoragePropertyMapping> enumerator = storageMappingFragment.Properties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					StorageEndPropertyMapping endMap = (StorageEndPropertyMapping)enumerator.Current;
					IEnumerable<EdmMember> storeProperties = endMap.StoreProperties;
					if (storeProperties.SequenceEqual(keyMembers, EqualityComparer<EdmMember>.Default))
					{
						IEnumerable<StorageEndPropertyMapping> source = from eMap in storageMappingFragment.Properties.OfType<StorageEndPropertyMapping>()
						where !eMap.Equals(endMap)
						select eMap;
						return source.First<StorageEndPropertyMapping>();
					}
				}
			}
			return null;
		}

		// Token: 0x060028B1 RID: 10417 RVA: 0x0009D878 File Offset: 0x0009BA78
		internal override void ToCompactString(StringBuilder stringBuilder)
		{
			this.m_cellWrapper.ToCompactString(stringBuilder);
		}

		// Token: 0x0400125E RID: 4702
		internal static readonly IEqualityComparer<LeafCellTreeNode> EqualityComparer = new LeafCellTreeNode.LeafCellTreeNodeComparer();

		// Token: 0x0400125F RID: 4703
		private LeftCellWrapper m_cellWrapper;

		// Token: 0x04001260 RID: 4704
		private FragmentQuery m_leftFragmentQuery;

		// Token: 0x04001261 RID: 4705
		private FragmentQuery m_rightFragmentQuery;

		// Token: 0x020005EE RID: 1518
		private class LeafCellTreeNodeComparer : IEqualityComparer<LeafCellTreeNode>
		{
			// Token: 0x060041F0 RID: 16880 RVA: 0x000EFE5F File Offset: 0x000EE05F
			public bool Equals(LeafCellTreeNode left, LeafCellTreeNode right)
			{
				return left == right || (left != null && right != null && left.m_cellWrapper.Equals(right.m_cellWrapper));
			}

			// Token: 0x060041F1 RID: 16881 RVA: 0x000EFE80 File Offset: 0x000EE080
			public int GetHashCode(LeafCellTreeNode node)
			{
				return node.m_cellWrapper.GetHashCode();
			}
		}
	}
}
