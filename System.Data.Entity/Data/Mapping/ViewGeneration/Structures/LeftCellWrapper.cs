using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration.QueryRewriting;
using System.Data.Metadata.Edm;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x020002AD RID: 685
	internal class LeftCellWrapper : InternalBase
	{
		// Token: 0x060028B3 RID: 10419 RVA: 0x0009D894 File Offset: 0x0009BA94
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

		// Token: 0x060028B4 RID: 10420 RVA: 0x0009D8E4 File Offset: 0x0009BAE4
		internal LeftCellWrapper(ViewTarget viewTarget, Set<MemberPath> attrs, FragmentQuery fragmentQuery, CellQuery leftCellQuery, CellQuery rightCellQuery, MemberMaps memberMaps, Cell inputCell) : this(viewTarget, attrs, fragmentQuery, leftCellQuery, rightCellQuery, memberMaps, Enumerable.Repeat<Cell>(inputCell, 1))
		{
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x060028B5 RID: 10421 RVA: 0x0009D908 File Offset: 0x0009BB08
		internal FragmentQuery FragmentQuery
		{
			get
			{
				return this.m_leftFragmentQuery;
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x060028B6 RID: 10422 RVA: 0x0009D910 File Offset: 0x0009BB10
		internal Set<MemberPath> Attributes
		{
			get
			{
				return this.m_attributes;
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x060028B7 RID: 10423 RVA: 0x0009D918 File Offset: 0x0009BB18
		internal string OriginalCellNumberString
		{
			get
			{
				return StringUtil.ToSeparatedString(from cell in this.m_mergedCells
				select cell.CellNumberAsString, "+", "");
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x060028B8 RID: 10424 RVA: 0x0009D953 File Offset: 0x0009BB53
		internal MemberDomainMap RightDomainMap
		{
			get
			{
				return this.m_memberMaps.RightDomainMap;
			}
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x000089D0 File Offset: 0x00006BD0
		[Conditional("DEBUG")]
		internal void AssertHasUniqueCell()
		{
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x060028BA RID: 10426 RVA: 0x0009D960 File Offset: 0x0009BB60
		internal IEnumerable<Cell> Cells
		{
			get
			{
				return this.m_mergedCells;
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x060028BB RID: 10427 RVA: 0x0009D968 File Offset: 0x0009BB68
		internal Cell OnlyInputCell
		{
			get
			{
				return this.m_mergedCells.First<Cell>();
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x060028BC RID: 10428 RVA: 0x0009D975 File Offset: 0x0009BB75
		internal CellQuery RightCellQuery
		{
			get
			{
				return this.m_rightCellQuery;
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x060028BD RID: 10429 RVA: 0x0009D97D File Offset: 0x0009BB7D
		internal CellQuery LeftCellQuery
		{
			get
			{
				return this.m_leftCellQuery;
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x060028BE RID: 10430 RVA: 0x0009D985 File Offset: 0x0009BB85
		internal EntitySetBase LeftExtent
		{
			get
			{
				return this.m_mergedCells.First<Cell>().GetLeftQuery(this.m_viewTarget).Extent;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x060028BF RID: 10431 RVA: 0x0009D9A4 File Offset: 0x0009BBA4
		internal EntitySetBase RightExtent
		{
			get
			{
				return this.m_rightCellQuery.Extent;
			}
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x0009D9BE File Offset: 0x0009BBBE
		internal static IEnumerable<Cell> GetInputCellsForWrappers(IEnumerable<LeftCellWrapper> wrappers)
		{
			foreach (LeftCellWrapper leftCellWrapper in wrappers)
			{
				foreach (Cell cell in leftCellWrapper.m_mergedCells)
				{
					yield return cell;
				}
				HashSet<Cell>.Enumerator enumerator2 = default(HashSet<Cell>.Enumerator);
			}
			IEnumerator<LeftCellWrapper> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x0009D9D0 File Offset: 0x0009BBD0
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

		// Token: 0x060028C2 RID: 10434 RVA: 0x0009DA30 File Offset: 0x0009BC30
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

		// Token: 0x060028C3 RID: 10435 RVA: 0x0009DAF0 File Offset: 0x0009BCF0
		internal override void ToFullString(StringBuilder builder)
		{
			builder.Append("P[");
			StringUtil.ToSeparatedString(builder, this.m_attributes, ",");
			builder.Append("] = ");
			this.m_rightCellQuery.ToFullString(builder);
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x0009DB27 File Offset: 0x0009BD27
		internal override void ToCompactString(StringBuilder stringBuilder)
		{
			stringBuilder.Append(this.OriginalCellNumberString);
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x0009DB38 File Offset: 0x0009BD38
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

		// Token: 0x060028C6 RID: 10438 RVA: 0x0009DBA0 File Offset: 0x0009BDA0
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

		// Token: 0x060028C7 RID: 10439 RVA: 0x0009DC30 File Offset: 0x0009BE30
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

		// Token: 0x060028C8 RID: 10440 RVA: 0x0009DC78 File Offset: 0x0009BE78
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

		// Token: 0x060028C9 RID: 10441 RVA: 0x0009DCBD File Offset: 0x0009BEBD
		internal MemberProjectedSlot GetCSideMappedSlotForSMember(MemberPath member)
		{
			if (this.m_viewTarget == ViewTarget.QueryView)
			{
				return this.GetLeftSideMappedSlotForRightSideMember(member);
			}
			return this.GetRightSideMappedSlotForLeftSideMember(member);
		}

		// Token: 0x04001262 RID: 4706
		internal static readonly IEqualityComparer<LeftCellWrapper> BoolEqualityComparer = new LeftCellWrapper.BoolWrapperComparer();

		// Token: 0x04001263 RID: 4707
		private Set<MemberPath> m_attributes;

		// Token: 0x04001264 RID: 4708
		private MemberMaps m_memberMaps;

		// Token: 0x04001265 RID: 4709
		private CellQuery m_leftCellQuery;

		// Token: 0x04001266 RID: 4710
		private CellQuery m_rightCellQuery;

		// Token: 0x04001267 RID: 4711
		private HashSet<Cell> m_mergedCells;

		// Token: 0x04001268 RID: 4712
		private ViewTarget m_viewTarget;

		// Token: 0x04001269 RID: 4713
		private FragmentQuery m_leftFragmentQuery;

		// Token: 0x0400126A RID: 4714
		internal static readonly IComparer<LeftCellWrapper> Comparer = new LeftCellWrapper.LeftCellWrapperComparer();

		// Token: 0x0400126B RID: 4715
		internal static readonly IComparer<LeftCellWrapper> OriginalCellIdComparer = new LeftCellWrapper.CellIdComparer();

		// Token: 0x020005F1 RID: 1521
		private class BoolWrapperComparer : IEqualityComparer<LeftCellWrapper>
		{
			// Token: 0x060041F7 RID: 16887 RVA: 0x000EFEB4 File Offset: 0x000EE0B4
			public bool Equals(LeftCellWrapper left, LeftCellWrapper right)
			{
				if (left == right)
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

			// Token: 0x060041F8 RID: 16888 RVA: 0x000EFF03 File Offset: 0x000EE103
			public int GetHashCode(LeftCellWrapper wrapper)
			{
				return BoolExpression.EqualityComparer.GetHashCode(wrapper.RightCellQuery.WhereClause) ^ wrapper.RightExtent.GetHashCode();
			}
		}

		// Token: 0x020005F2 RID: 1522
		private class LeftCellWrapperComparer : IComparer<LeftCellWrapper>
		{
			// Token: 0x060041FA RID: 16890 RVA: 0x000EFF28 File Offset: 0x000EE128
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

		// Token: 0x020005F3 RID: 1523
		internal class CellIdComparer : IComparer<LeftCellWrapper>
		{
			// Token: 0x060041FC RID: 16892 RVA: 0x000EFF8E File Offset: 0x000EE18E
			public int Compare(LeftCellWrapper x, LeftCellWrapper y)
			{
				return StringComparer.Ordinal.Compare(x.OriginalCellNumberString, y.OriginalCellNumberString);
			}
		}
	}
}
