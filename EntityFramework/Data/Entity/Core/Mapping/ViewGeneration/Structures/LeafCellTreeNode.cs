using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000474 RID: 1140
	internal class LeafCellTreeNode : CellTreeNode
	{
		// Token: 0x060029FE RID: 10750 RVA: 0x000CAAF6 File Offset: 0x000C8CF6
		internal LeafCellTreeNode(ViewgenContext context, LeftCellWrapper cellWrapper) : base(context)
		{
			this.m_cellWrapper = cellWrapper;
			this.m_rightFragmentQuery = FragmentQuery.Create(cellWrapper.OriginalCellNumberString, cellWrapper.CreateRoleBoolean(), cellWrapper.RightCellQuery);
		}

		// Token: 0x060029FF RID: 10751 RVA: 0x000CAB23 File Offset: 0x000C8D23
		internal LeafCellTreeNode(ViewgenContext context, LeftCellWrapper cellWrapper, FragmentQuery rightFragmentQuery) : base(context)
		{
			this.m_cellWrapper = cellWrapper;
			this.m_rightFragmentQuery = rightFragmentQuery;
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06002A00 RID: 10752 RVA: 0x000CAB3A File Offset: 0x000C8D3A
		internal LeftCellWrapper LeftCellWrapper
		{
			get
			{
				return this.m_cellWrapper;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06002A01 RID: 10753 RVA: 0x000CAB42 File Offset: 0x000C8D42
		internal override MemberDomainMap RightDomainMap
		{
			get
			{
				return this.m_cellWrapper.RightDomainMap;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06002A02 RID: 10754 RVA: 0x000CAB4F File Offset: 0x000C8D4F
		internal override FragmentQuery LeftFragmentQuery
		{
			get
			{
				return this.m_cellWrapper.FragmentQuery;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06002A03 RID: 10755 RVA: 0x000CAB5C File Offset: 0x000C8D5C
		internal override FragmentQuery RightFragmentQuery
		{
			get
			{
				return this.m_rightFragmentQuery;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06002A04 RID: 10756 RVA: 0x000CAB64 File Offset: 0x000C8D64
		internal override Set<MemberPath> Attributes
		{
			get
			{
				return this.m_cellWrapper.Attributes;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06002A05 RID: 10757 RVA: 0x000CAB71 File Offset: 0x000C8D71
		internal override List<CellTreeNode> Children
		{
			get
			{
				return new List<CellTreeNode>();
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06002A06 RID: 10758 RVA: 0x000CAB78 File Offset: 0x000C8D78
		internal override CellTreeOpType OpType
		{
			get
			{
				return CellTreeOpType.Leaf;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06002A07 RID: 10759 RVA: 0x000CAB7B File Offset: 0x000C8D7B
		internal override int NumProjectedSlots
		{
			get
			{
				return this.LeftCellWrapper.RightCellQuery.NumProjectedSlots;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06002A08 RID: 10760 RVA: 0x000CAB8D File Offset: 0x000C8D8D
		internal override int NumBoolSlots
		{
			get
			{
				return this.LeftCellWrapper.RightCellQuery.NumBoolVars;
			}
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x000CAB9F File Offset: 0x000C8D9F
		internal override TOutput Accept<TInput, TOutput>(CellTreeNode.CellTreeVisitor<TInput, TOutput> visitor, TInput param)
		{
			return visitor.VisitLeaf(this, param);
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x000CABA9 File Offset: 0x000C8DA9
		internal override TOutput Accept<TInput, TOutput>(CellTreeNode.SimpleCellTreeVisitor<TInput, TOutput> visitor, TInput param)
		{
			return visitor.VisitLeaf(this, param);
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x000CABB4 File Offset: 0x000C8DB4
		internal override bool IsProjectedSlot(int slot)
		{
			CellQuery rightCellQuery = this.LeftCellWrapper.RightCellQuery;
			if (base.IsBoolSlot(slot))
			{
				return rightCellQuery.GetBoolVar(base.SlotToBoolIndex(slot)) != null;
			}
			return rightCellQuery.ProjectedSlotAt(slot) != null;
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x000CABF8 File Offset: 0x000C8DF8
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
					ConstantProjectedSlot constantProjectedSlot = new ConstantProjectedSlot(Domain.GetDefaultValueForMemberPath(memberPath, base.GetLeaves(), base.ViewgenContext.Config));
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
				IEnumerable<AssociationSetMapping> relationshipSetMappingsFor = base.ViewgenContext.EntityContainerMapping.GetRelationshipSetMappingsFor(this.m_cellWrapper.LeftExtent, rightCellQuery.Extent);
				List<SlotInfo> second = new List<SlotInfo>();
				foreach (AssociationSetMapping colocatedAssociationSetMap in relationshipSetMappingsFor)
				{
					WithRelationship item;
					if (LeafCellTreeNode.TryGetWithRelationship(colocatedAssociationSetMap, this.m_cellWrapper.LeftExtent, rightCellQuery.SourceExtentMemberPath, ref second, out item))
					{
						withRelationships.Add(item);
						source = array.Concat(second);
					}
				}
			}
			return new ExtentCqlBlock(rightCellQuery.Extent, rightCellQuery.SelectDistinctFlag, source.ToArray<SlotInfo>(), rightCellQuery.WhereClause, identifiers, ++blockAliasNum);
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x000CAE18 File Offset: 0x000C9018
		private static bool TryGetWithRelationship(AssociationSetMapping colocatedAssociationSetMap, EntitySetBase thisExtent, MemberPath sRootNode, ref List<SlotInfo> foreignKeySlots, out WithRelationship withRelationship)
		{
			withRelationship = null;
			EndPropertyMapping foreignKeyEndMapFromAssocitionMap = LeafCellTreeNode.GetForeignKeyEndMapFromAssocitionMap(colocatedAssociationSetMap);
			if (foreignKeyEndMapFromAssocitionMap == null || foreignKeyEndMapFromAssocitionMap.AssociationEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many)
			{
				return false;
			}
			AssociationEndMember associationEnd = foreignKeyEndMapFromAssocitionMap.AssociationEnd;
			AssociationEndMember otherAssociationEnd = MetadataHelper.GetOtherAssociationEnd(associationEnd);
			EntityType entityType = (EntityType)((RefType)associationEnd.TypeUsage.EdmType).ElementType;
			EntityType entityType2 = (EntityType)((RefType)otherAssociationEnd.TypeUsage.EdmType).ElementType;
			AssociationSet associationSet = (AssociationSet)colocatedAssociationSetMap.Set;
			MemberPath prefix = new MemberPath(associationSet, associationEnd);
			IEnumerable<ScalarPropertyMapping> source = foreignKeyEndMapFromAssocitionMap.PropertyMappings.Cast<ScalarPropertyMapping>();
			List<MemberPath> list = new List<MemberPath>();
			using (ReadOnlyMetadataCollection<EdmMember>.Enumerator enumerator = entityType.KeyMembers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EdmProperty edmProperty = (EdmProperty)enumerator.Current;
					IEnumerable<ScalarPropertyMapping> source2 = from propMap in source
					where propMap.Property.Equals(edmProperty)
					select propMap;
					ScalarPropertyMapping scalarPropertyMapping = source2.First<ScalarPropertyMapping>();
					MemberProjectedSlot slotValue = new MemberProjectedSlot(new MemberPath(sRootNode, scalarPropertyMapping.Column));
					MemberPath memberPath = new MemberPath(prefix, edmProperty);
					list.Add(memberPath);
					foreignKeySlots.Add(new SlotInfo(true, true, slotValue, memberPath));
				}
			}
			if (thisExtent.ElementType.IsAssignableFrom(entityType2))
			{
				withRelationship = new WithRelationship(associationSet, otherAssociationEnd, entityType2, associationEnd, entityType, list);
				return true;
			}
			return false;
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x000CAFB4 File Offset: 0x000C91B4
		private static EndPropertyMapping GetForeignKeyEndMapFromAssocitionMap(AssociationSetMapping colocatedAssociationSetMap)
		{
			MappingFragment mappingFragment = colocatedAssociationSetMap.TypeMappings.First<TypeMapping>().MappingFragments.First<MappingFragment>();
			EntitySet storeEntitySet = colocatedAssociationSetMap.StoreEntitySet;
			IEnumerable<EdmMember> keyMembers = storeEntitySet.ElementType.KeyMembers;
			using (IEnumerator<PropertyMapping> enumerator = mappingFragment.PropertyMappings.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EndPropertyMapping endMap = (EndPropertyMapping)enumerator.Current;
					IEnumerable<EdmMember> storeProperties = endMap.StoreProperties;
					if (storeProperties.SequenceEqual(keyMembers, EqualityComparer<EdmMember>.Default))
					{
						IEnumerable<EndPropertyMapping> source = from eMap in mappingFragment.PropertyMappings.OfType<EndPropertyMapping>()
						where !eMap.Equals(endMap)
						select eMap;
						return source.First<EndPropertyMapping>();
					}
				}
			}
			return null;
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x000CB090 File Offset: 0x000C9290
		internal override void ToCompactString(StringBuilder stringBuilder)
		{
			this.m_cellWrapper.ToCompactString(stringBuilder);
		}

		// Token: 0x04000F8E RID: 3982
		internal static readonly IEqualityComparer<LeafCellTreeNode> EqualityComparer = new LeafCellTreeNode.LeafCellTreeNodeComparer();

		// Token: 0x04000F8F RID: 3983
		private readonly LeftCellWrapper m_cellWrapper;

		// Token: 0x04000F90 RID: 3984
		private readonly FragmentQuery m_rightFragmentQuery;

		// Token: 0x02000475 RID: 1141
		private class LeafCellTreeNodeComparer : IEqualityComparer<LeafCellTreeNode>
		{
			// Token: 0x06002A11 RID: 10769 RVA: 0x000CB0AA File Offset: 0x000C92AA
			public bool Equals(LeafCellTreeNode left, LeafCellTreeNode right)
			{
				return object.ReferenceEquals(left, right) || (left != null && right != null && left.m_cellWrapper.Equals(right.m_cellWrapper));
			}

			// Token: 0x06002A12 RID: 10770 RVA: 0x000CB0D0 File Offset: 0x000C92D0
			public int GetHashCode(LeafCellTreeNode node)
			{
				return node.m_cellWrapper.GetHashCode();
			}
		}
	}
}
