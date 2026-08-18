using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000476 RID: 1142
	internal class LeftCellWrapper : InternalBase
	{
		// Token: 0x06002A14 RID: 10772 RVA: 0x000CB0E8 File Offset: 0x000C92E8
		internal LeftCellWrapper(ViewTarget viewTarget, Set<MemberPath> attrs, FragmentQuery fragmentQuery, CellQuery leftCellQuery, CellQuery rightCellQuery, MemberMaps memberMaps, IEnumerable<Cell> inputCells)
		{
			this.m_leftFragmentQuery = fragmentQuery;
			this.m_rightCellQuery = rightCellQuery;
			this.m_leftCellQuery = leftCellQuery;
			this.m_attributes = attrs;
			this.m_viewTarget = viewTarget;
			this.m_memberMaps = memberMaps;
			this.m_mergedCells = new HashSet<Cell>(inputCells);
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x000CB138 File Offset: 0x000C9338
		internal LeftCellWrapper(ViewTarget viewTarget, Set<MemberPath> attrs, FragmentQuery fragmentQuery, CellQuery leftCellQuery, CellQuery rightCellQuery, MemberMaps memberMaps, Cell inputCell) : this(viewTarget, attrs, fragmentQuery, leftCellQuery, rightCellQuery, memberMaps, Enumerable.Repeat<Cell>(inputCell, 1))
		{
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06002A16 RID: 10774 RVA: 0x000CB15C File Offset: 0x000C935C
		internal FragmentQuery FragmentQuery
		{
			get
			{
				return this.m_leftFragmentQuery;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06002A17 RID: 10775 RVA: 0x000CB164 File Offset: 0x000C9364
		internal Set<MemberPath> Attributes
		{
			get
			{
				return this.m_attributes;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06002A18 RID: 10776 RVA: 0x000CB174 File Offset: 0x000C9374
		internal string OriginalCellNumberString
		{
			get
			{
				return StringUtil.ToSeparatedString(from cell in this.m_mergedCells
				select cell.CellNumberAsString, "+", "");
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06002A19 RID: 10777 RVA: 0x000CB1AD File Offset: 0x000C93AD
		internal MemberDomainMap RightDomainMap
		{
			get
			{
				return this.m_memberMaps.RightDomainMap;
			}
		}

		// Token: 0x06002A1A RID: 10778 RVA: 0x000CB1BA File Offset: 0x000C93BA
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[Conditional("DEBUG")]
		internal void AssertHasUniqueCell()
		{
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06002A1B RID: 10779 RVA: 0x000CB1BC File Offset: 0x000C93BC
		internal IEnumerable<Cell> Cells
		{
			get
			{
				return this.m_mergedCells;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06002A1C RID: 10780 RVA: 0x000CB1C4 File Offset: 0x000C93C4
		internal Cell OnlyInputCell
		{
			get
			{
				return this.m_mergedCells.First<Cell>();
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06002A1D RID: 10781 RVA: 0x000CB1D1 File Offset: 0x000C93D1
		internal CellQuery RightCellQuery
		{
			get
			{
				return this.m_rightCellQuery;
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06002A1E RID: 10782 RVA: 0x000CB1D9 File Offset: 0x000C93D9
		internal CellQuery LeftCellQuery
		{
			get
			{
				return this.m_leftCellQuery;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06002A1F RID: 10783 RVA: 0x000CB1E1 File Offset: 0x000C93E1
		internal EntitySetBase LeftExtent
		{
			get
			{
				return this.m_mergedCells.First<Cell>().GetLeftQuery(this.m_viewTarget).Extent;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06002A20 RID: 10784 RVA: 0x000CB200 File Offset: 0x000C9400
		internal EntitySetBase RightExtent
		{
			get
			{
				return this.m_rightCellQuery.Extent;
			}
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x000CB424 File Offset: 0x000C9624
		internal static IEnumerable<Cell> GetInputCellsForWrappers(IEnumerable<LeftCellWrapper> wrappers)
		{
			foreach (LeftCellWrapper wrapper in wrappers)
			{
				foreach (Cell cell in wrapper.m_mergedCells)
				{
					yield return cell;
				}
			}
			yield break;
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x000CB444 File Offset: 0x000C9644
		internal RoleBoolean CreateRoleBoolean()
		{
			if (this.RightExtent is AssociationSet)
			{
				Set<AssociationEndMember> endsForTablePrimaryKey = this.GetEndsForTablePrimaryKey();
				if (endsForTablePrimaryKey.Count == 1)
				{
					AssociationSetEnd end = ((AssociationSet)this.RightExtent).AssociationSetEnds[endsForTablePrimaryKey.First<AssociationEndMember>().Name];
					return new RoleBoolean(end);
				}
			}
			return new RoleBoolean(this.RightExtent);
		}

		// Token: 0x06002A23 RID: 10787 RVA: 0x000CB4A4 File Offset: 0x000C96A4
		internal static string GetExtentListAsUserString(IEnumerable<LeftCellWrapper> wrappers)
		{
			Set<EntitySetBase> set = new Set<EntitySetBase>(EqualityComparer<EntitySetBase>.Default);
			foreach (LeftCellWrapper leftCellWrapper in wrappers)
			{
				set.Add(leftCellWrapper.RightExtent);
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (EntitySetBase entitySetBase in set)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				flag = false;
				stringBuilder.Append(entitySetBase.Name);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x000CB568 File Offset: 0x000C9768
		internal override void ToFullString(StringBuilder builder)
		{
			builder.Append("P[");
			StringUtil.ToSeparatedString(builder, this.m_attributes, ",");
			builder.Append("] = ");
			this.m_rightCellQuery.ToFullString(builder);
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x000CB59F File Offset: 0x000C979F
		internal override void ToCompactString(StringBuilder stringBuilder)
		{
			stringBuilder.Append(this.OriginalCellNumberString);
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x000CB5B0 File Offset: 0x000C97B0
		internal static void WrappersToStringBuilder(StringBuilder builder, List<LeftCellWrapper> wrappers, string header)
		{
			builder.AppendLine().Append(header).AppendLine();
			LeftCellWrapper[] array = wrappers.ToArray();
			Array.Sort<LeftCellWrapper>(array, LeftCellWrapper.OriginalCellIdComparer);
			foreach (LeftCellWrapper leftCellWrapper in array)
			{
				leftCellWrapper.ToCompactString(builder);
				builder.Append(" = ");
				leftCellWrapper.ToFullString(builder);
				builder.AppendLine();
			}
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x000CB618 File Offset: 0x000C9818
		private Set<AssociationEndMember> GetEndsForTablePrimaryKey()
		{
			CellQuery rightCellQuery = this.RightCellQuery;
			Set<AssociationEndMember> set = new Set<AssociationEndMember>(EqualityComparer<AssociationEndMember>.Default);
			foreach (int slotNum in this.m_memberMaps.ProjectedSlotMap.KeySlots)
			{
				MemberProjectedSlot memberProjectedSlot = (MemberProjectedSlot)rightCellQuery.ProjectedSlotAt(slotNum);
				MemberPath memberPath = memberProjectedSlot.MemberPath;
				AssociationEndMember element = (AssociationEndMember)memberPath.RootEdmMember;
				set.Add(element);
			}
			return set;
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x000CB6AC File Offset: 0x000C98AC
		internal MemberProjectedSlot GetLeftSideMappedSlotForRightSideMember(MemberPath member)
		{
			int projectedPosition = this.RightCellQuery.GetProjectedPosition(new MemberProjectedSlot(member));
			if (projectedPosition == -1)
			{
				return null;
			}
			ProjectedSlot projectedSlot = this.LeftCellQuery.ProjectedSlotAt(projectedPosition);
			if (projectedSlot == null || projectedSlot is ConstantProjectedSlot)
			{
				return null;
			}
			return projectedSlot as MemberProjectedSlot;
		}

		// Token: 0x06002A29 RID: 10793 RVA: 0x000CB6F4 File Offset: 0x000C98F4
		internal MemberProjectedSlot GetRightSideMappedSlotForLeftSideMember(MemberPath member)
		{
			int projectedPosition = this.LeftCellQuery.GetProjectedPosition(new MemberProjectedSlot(member));
			if (projectedPosition == -1)
			{
				return null;
			}
			ProjectedSlot projectedSlot = this.RightCellQuery.ProjectedSlotAt(projectedPosition);
			if (projectedSlot == null || projectedSlot is ConstantProjectedSlot)
			{
				return null;
			}
			return projectedSlot as MemberProjectedSlot;
		}

		// Token: 0x06002A2A RID: 10794 RVA: 0x000CB739 File Offset: 0x000C9939
		internal MemberProjectedSlot GetCSideMappedSlotForSMember(MemberPath member)
		{
			if (this.m_viewTarget == ViewTarget.QueryView)
			{
				return this.GetLeftSideMappedSlotForRightSideMember(member);
			}
			return this.GetRightSideMappedSlotForLeftSideMember(member);
		}

		// Token: 0x04000F91 RID: 3985
		internal static readonly IEqualityComparer<LeftCellWrapper> BoolEqualityComparer = new LeftCellWrapper.BoolWrapperComparer();

		// Token: 0x04000F92 RID: 3986
		private readonly Set<MemberPath> m_attributes;

		// Token: 0x04000F93 RID: 3987
		private readonly MemberMaps m_memberMaps;

		// Token: 0x04000F94 RID: 3988
		private readonly CellQuery m_leftCellQuery;

		// Token: 0x04000F95 RID: 3989
		private readonly CellQuery m_rightCellQuery;

		// Token: 0x04000F96 RID: 3990
		private readonly HashSet<Cell> m_mergedCells;

		// Token: 0x04000F97 RID: 3991
		private readonly ViewTarget m_viewTarget;

		// Token: 0x04000F98 RID: 3992
		private readonly FragmentQuery m_leftFragmentQuery;

		// Token: 0x04000F99 RID: 3993
		internal static readonly IComparer<LeftCellWrapper> Comparer = new LeftCellWrapper.LeftCellWrapperComparer();

		// Token: 0x04000F9A RID: 3994
		internal static readonly IComparer<LeftCellWrapper> OriginalCellIdComparer = new LeftCellWrapper.CellIdComparer();

		// Token: 0x02000477 RID: 1143
		private class BoolWrapperComparer : IEqualityComparer<LeftCellWrapper>
		{
			// Token: 0x06002A2D RID: 10797 RVA: 0x000CB774 File Offset: 0x000C9974
			public bool Equals(LeftCellWrapper left, LeftCellWrapper right)
			{
				if (object.ReferenceEquals(left, right))
				{
					return true;
				}
				if (left == null || right == null)
				{
					return false;
				}
				bool flag = BoolExpression.EqualityComparer.Equals(left.RightCellQuery.WhereClause, right.RightCellQuery.WhereClause);
				return left.RightExtent.Equals(right.RightExtent) && flag;
			}

			// Token: 0x06002A2E RID: 10798 RVA: 0x000CB7CB File Offset: 0x000C99CB
			public int GetHashCode(LeftCellWrapper wrapper)
			{
				return BoolExpression.EqualityComparer.GetHashCode(wrapper.RightCellQuery.WhereClause) ^ wrapper.RightExtent.GetHashCode();
			}
		}

		// Token: 0x02000478 RID: 1144
		private class LeftCellWrapperComparer : IComparer<LeftCellWrapper>
		{
			// Token: 0x06002A30 RID: 10800 RVA: 0x000CB7F8 File Offset: 0x000C99F8
			public int Compare(LeftCellWrapper x, LeftCellWrapper y)
			{
				if (x.FragmentQuery.Attributes.Count > y.FragmentQuery.Attributes.Count)
				{
					return -1;
				}
				if (x.FragmentQuery.Attributes.Count < y.FragmentQuery.Attributes.Count)
				{
					return 1;
				}
				return string.CompareOrdinal(x.OriginalCellNumberString, y.OriginalCellNumberString);
			}
		}

		// Token: 0x02000479 RID: 1145
		internal class CellIdComparer : IComparer<LeftCellWrapper>
		{
			// Token: 0x06002A32 RID: 10802 RVA: 0x000CB866 File Offset: 0x000C9A66
			public int Compare(LeftCellWrapper x, LeftCellWrapper y)
			{
				return StringComparer.Ordinal.Compare(x.OriginalCellNumberString, y.OriginalCellNumberString);
			}
		}
	}
}
