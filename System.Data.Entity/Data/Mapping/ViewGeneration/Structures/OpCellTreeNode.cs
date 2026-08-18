using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Mapping.ViewGeneration.QueryRewriting;
using System.Data.Mapping.ViewGeneration.Utils;
using System.Linq;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x020002B6 RID: 694
	internal class OpCellTreeNode : CellTreeNode
	{
		// Token: 0x06002956 RID: 10582 RVA: 0x000A0516 File Offset: 0x0009E716
		internal OpCellTreeNode(ViewgenContext context, CellTreeOpType opType) : base(context)
		{
			this.m_opType = opType;
			this.m_attrs = new Set<MemberPath>(MemberPath.EqualityComparer);
			this.m_children = new List<CellTreeNode>();
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x000A0541 File Offset: 0x0009E741
		internal OpCellTreeNode(ViewgenContext context, CellTreeOpType opType, params CellTreeNode[] children) : this(context, opType, children)
		{
		}

		// Token: 0x06002958 RID: 10584 RVA: 0x000A054C File Offset: 0x0009E74C
		internal OpCellTreeNode(ViewgenContext context, CellTreeOpType opType, IEnumerable<CellTreeNode> children) : this(context, opType)
		{
			foreach (CellTreeNode child in children)
			{
				this.Add(child);
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06002959 RID: 10585 RVA: 0x000A059C File Offset: 0x0009E79C
		internal override CellTreeOpType OpType
		{
			get
			{
				return this.m_opType;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x0600295A RID: 10586 RVA: 0x000A05A4 File Offset: 0x0009E7A4
		internal override FragmentQuery LeftFragmentQuery
		{
			get
			{
				if (this.m_leftFragmentQuery == null)
				{
					this.m_leftFragmentQuery = OpCellTreeNode.GenerateFragmentQuery(this.Children, true, base.ViewgenContext, this.OpType);
				}
				return this.m_leftFragmentQuery;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x0600295B RID: 10587 RVA: 0x000A05D2 File Offset: 0x0009E7D2
		internal override FragmentQuery RightFragmentQuery
		{
			get
			{
				if (this.m_rightFragmentQuery == null)
				{
					this.m_rightFragmentQuery = OpCellTreeNode.GenerateFragmentQuery(this.Children, false, base.ViewgenContext, this.OpType);
				}
				return this.m_rightFragmentQuery;
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x0600295C RID: 10588 RVA: 0x000A0600 File Offset: 0x0009E800
		internal override MemberDomainMap RightDomainMap
		{
			get
			{
				return this.m_children[0].RightDomainMap;
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x0600295D RID: 10589 RVA: 0x000A0613 File Offset: 0x0009E813
		internal override Set<MemberPath> Attributes
		{
			get
			{
				return this.m_attrs;
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x0600295E RID: 10590 RVA: 0x000A061B File Offset: 0x0009E81B
		internal override List<CellTreeNode> Children
		{
			get
			{
				return this.m_children;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x0600295F RID: 10591 RVA: 0x000A0623 File Offset: 0x0009E823
		internal override int NumProjectedSlots
		{
			get
			{
				return this.m_children[0].NumProjectedSlots;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06002960 RID: 10592 RVA: 0x000A0636 File Offset: 0x0009E836
		internal override int NumBoolSlots
		{
			get
			{
				return this.m_children[0].NumBoolSlots;
			}
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x000A0649 File Offset: 0x0009E849
		internal override TOutput Accept<TInput, TOutput>(CellTreeNode.SimpleCellTreeVisitor<TInput, TOutput> visitor, TInput param)
		{
			return visitor.VisitOpNode(this, param);
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x000A0654 File Offset: 0x0009E854
		internal override TOutput Accept<TInput, TOutput>(CellTreeNode.CellTreeVisitor<TInput, TOutput> visitor, TInput param)
		{
			switch (this.OpType)
			{
			case CellTreeOpType.Union:
				return visitor.VisitUnion(this, param);
			case CellTreeOpType.FOJ:
				return visitor.VisitFullOuterJoin(this, param);
			case CellTreeOpType.LOJ:
				return visitor.VisitLeftOuterJoin(this, param);
			case CellTreeOpType.IJ:
				return visitor.VisitInnerJoin(this, param);
			case CellTreeOpType.LASJ:
				return visitor.VisitLeftAntiSemiJoin(this, param);
			default:
				return visitor.VisitInnerJoin(this, param);
			}
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x000A06BB File Offset: 0x0009E8BB
		internal void Add(CellTreeNode child)
		{
			this.Insert(this.m_children.Count, child);
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x000A06CF File Offset: 0x0009E8CF
		internal void AddFirst(CellTreeNode child)
		{
			this.Insert(0, child);
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x000A06D9 File Offset: 0x0009E8D9
		private void Insert(int index, CellTreeNode child)
		{
			this.m_attrs.Unite(child.Attributes);
			this.m_children.Insert(index, child);
			this.m_leftFragmentQuery = null;
			this.m_rightFragmentQuery = null;
		}

		// Token: 0x06002966 RID: 10598 RVA: 0x000A0708 File Offset: 0x0009E908
		internal override CqlBlock ToCqlBlock(bool[] requiredSlots, CqlIdentifiers identifiers, ref int blockAliasNum, ref List<WithRelationship> withRelationships)
		{
			CqlBlock result;
			if (this.OpType == CellTreeOpType.Union)
			{
				result = this.UnionToCqlBlock(requiredSlots, identifiers, ref blockAliasNum, ref withRelationships);
			}
			else
			{
				result = this.JoinToCqlBlock(requiredSlots, identifiers, ref blockAliasNum, ref withRelationships);
			}
			return result;
		}

		// Token: 0x06002967 RID: 10599 RVA: 0x000A073C File Offset: 0x0009E93C
		internal override bool IsProjectedSlot(int slot)
		{
			foreach (CellTreeNode cellTreeNode in this.Children)
			{
				if (cellTreeNode.IsProjectedSlot(slot))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002968 RID: 10600 RVA: 0x000A0798 File Offset: 0x0009E998
		private CqlBlock UnionToCqlBlock(bool[] requiredSlots, CqlIdentifiers identifiers, ref int blockAliasNum, ref List<WithRelationship> withRelationships)
		{
			List<CqlBlock> list = new List<CqlBlock>();
			List<Tuple<CqlBlock, SlotInfo>> list2 = new List<Tuple<CqlBlock, SlotInfo>>();
			int num = requiredSlots.Length;
			foreach (CellTreeNode cellTreeNode in this.Children)
			{
				bool[] projectedSlots = cellTreeNode.GetProjectedSlots();
				OpCellTreeNode.AndWith(projectedSlots, requiredSlots);
				CqlBlock cqlBlock = cellTreeNode.ToCqlBlock(projectedSlots, identifiers, ref blockAliasNum, ref withRelationships);
				for (int i = projectedSlots.Length; i < cqlBlock.Slots.Count; i++)
				{
					list2.Add(Tuple.Create<CqlBlock, SlotInfo>(cqlBlock, cqlBlock.Slots[i]));
				}
				SlotInfo[] array = new SlotInfo[cqlBlock.Slots.Count];
				for (int j = 0; j < num; j++)
				{
					if (requiredSlots[j] && !projectedSlots[j])
					{
						if (base.IsBoolSlot(j))
						{
							array[j] = new SlotInfo(true, true, new BooleanProjectedSlot(BoolExpression.False, identifiers, base.SlotToBoolIndex(j)), null);
						}
						else
						{
							MemberPath memberPath = cqlBlock.MemberPath(j);
							array[j] = new SlotInfo(true, true, new ConstantProjectedSlot(Constant.Null, memberPath), memberPath);
						}
					}
					else
					{
						array[j] = cqlBlock.Slots[j];
					}
				}
				cqlBlock.Slots = new ReadOnlyCollection<SlotInfo>(array);
				list.Add(cqlBlock);
			}
			if (list2.Count != 0)
			{
				foreach (CqlBlock cqlBlock2 in list)
				{
					SlotInfo[] array2 = new SlotInfo[num + list2.Count];
					cqlBlock2.Slots.CopyTo(array2, 0);
					int num2 = num;
					foreach (Tuple<CqlBlock, SlotInfo> tuple in list2)
					{
						SlotInfo item = tuple.Item2;
						if (tuple.Item1.Equals(cqlBlock2))
						{
							array2[num2] = new SlotInfo(true, true, item.SlotValue, item.OutputMember);
						}
						else
						{
							array2[num2] = new SlotInfo(true, true, new ConstantProjectedSlot(Constant.Null, item.OutputMember), item.OutputMember);
						}
						num2++;
					}
					cqlBlock2.Slots = new ReadOnlyCollection<SlotInfo>(array2);
				}
			}
			SlotInfo[] array3 = new SlotInfo[num + list2.Count];
			CqlBlock cqlBlock3 = list[0];
			for (int k = 0; k < num; k++)
			{
				SlotInfo slotInfo = cqlBlock3.Slots[k];
				bool flag = requiredSlots[k];
				array3[k] = new SlotInfo(flag, flag, slotInfo.SlotValue, slotInfo.OutputMember);
			}
			for (int l = num; l < num + list2.Count; l++)
			{
				SlotInfo slotInfo2 = cqlBlock3.Slots[l];
				array3[l] = new SlotInfo(true, true, slotInfo2.SlotValue, slotInfo2.OutputMember);
			}
			SlotInfo[] slotInfos = array3;
			List<CqlBlock> children = list;
			int num3 = blockAliasNum + 1;
			blockAliasNum = num3;
			return new UnionCqlBlock(slotInfos, children, identifiers, num3);
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x000A0AE4 File Offset: 0x0009ECE4
		private static void AndWith(bool[] boolArray, bool[] another)
		{
			for (int i = 0; i < boolArray.Length; i++)
			{
				boolArray[i] &= another[i];
			}
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x000A0B10 File Offset: 0x0009ED10
		private CqlBlock JoinToCqlBlock(bool[] requiredSlots, CqlIdentifiers identifiers, ref int blockAliasNum, ref List<WithRelationship> withRelationships)
		{
			int num = requiredSlots.Length;
			List<CqlBlock> list = new List<CqlBlock>();
			List<Tuple<QualifiedSlot, MemberPath>> list2 = new List<Tuple<QualifiedSlot, MemberPath>>();
			foreach (CellTreeNode cellTreeNode in this.Children)
			{
				bool[] projectedSlots = cellTreeNode.GetProjectedSlots();
				OpCellTreeNode.AndWith(projectedSlots, requiredSlots);
				CqlBlock cqlBlock = cellTreeNode.ToCqlBlock(projectedSlots, identifiers, ref blockAliasNum, ref withRelationships);
				list.Add(cqlBlock);
				for (int i = projectedSlots.Length; i < cqlBlock.Slots.Count; i++)
				{
					list2.Add(Tuple.Create<QualifiedSlot, MemberPath>(cqlBlock.QualifySlotWithBlockAlias(i), cqlBlock.MemberPath(i)));
				}
			}
			SlotInfo[] array = new SlotInfo[num + list2.Count];
			for (int j = 0; j < num; j++)
			{
				SlotInfo joinSlotInfo = this.GetJoinSlotInfo(this.OpType, requiredSlots[j], list, j, identifiers);
				array[j] = joinSlotInfo;
			}
			int num2 = 0;
			int k = num;
			while (k < num + list2.Count)
			{
				array[k] = new SlotInfo(true, true, list2[num2].Item1, list2[num2].Item2);
				k++;
				num2++;
			}
			List<JoinCqlBlock.OnClause> list3 = new List<JoinCqlBlock.OnClause>();
			for (int l = 1; l < list.Count; l++)
			{
				CqlBlock cqlBlock2 = list[l];
				JoinCqlBlock.OnClause onClause = new JoinCqlBlock.OnClause();
				foreach (int num3 in base.KeySlots)
				{
					if (!base.ViewgenContext.Config.IsValidationEnabled && (!cqlBlock2.IsProjected(num3) || !list[0].IsProjected(num3)))
					{
						ErrorLog errorLog = new ErrorLog();
						errorLog.AddEntry(new ErrorLog.Record(true, ViewGenErrorCode.NoJoinKeyOrFKProvidedInMapping, Strings.Viewgen_NoJoinKeyOrFK, base.ViewgenContext.AllWrappersForExtent, string.Empty));
						ExceptionHelpers.ThrowMappingException(errorLog, base.ViewgenContext.Config);
					}
					QualifiedSlot leftSlot = list[0].QualifySlotWithBlockAlias(num3);
					QualifiedSlot rightSlot = cqlBlock2.QualifySlotWithBlockAlias(num3);
					MemberPath outputMember = array[num3].OutputMember;
					onClause.Add(leftSlot, outputMember, rightSlot, outputMember);
				}
				list3.Add(onClause);
			}
			CellTreeOpType opType = this.OpType;
			SlotInfo[] slotInfos = array;
			List<CqlBlock> children = list;
			List<JoinCqlBlock.OnClause> onClauses = list3;
			int num4 = blockAliasNum + 1;
			blockAliasNum = num4;
			return new JoinCqlBlock(opType, slotInfos, children, onClauses, identifiers, num4);
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x000A0D90 File Offset: 0x0009EF90
		private SlotInfo GetJoinSlotInfo(CellTreeOpType opType, bool isRequiredSlot, List<CqlBlock> children, int slotNum, CqlIdentifiers identifiers)
		{
			if (!isRequiredSlot)
			{
				return new SlotInfo(false, false, null, base.GetMemberPath(slotNum));
			}
			int num = -1;
			CaseStatement caseStatement = null;
			for (int i = 0; i < children.Count; i++)
			{
				CqlBlock cqlBlock = children[i];
				if (cqlBlock.IsProjected(slotNum))
				{
					if (base.IsKeySlot(slotNum))
					{
						num = i;
						break;
					}
					if (opType == CellTreeOpType.IJ)
					{
						num = OpCellTreeNode.GetInnerJoinChildForSlot(children, slotNum);
						break;
					}
					if (num != -1)
					{
						if (caseStatement == null)
						{
							MemberPath memberPath = base.GetMemberPath(slotNum);
							caseStatement = new CaseStatement(memberPath);
							this.AddCaseForOuterJoins(caseStatement, children[num], slotNum, identifiers);
						}
						this.AddCaseForOuterJoins(caseStatement, cqlBlock, slotNum, identifiers);
					}
					num = i;
				}
			}
			MemberPath memberPath2 = base.GetMemberPath(slotNum);
			ProjectedSlot slotValue;
			if (caseStatement != null && (caseStatement.Clauses.Count > 0 || caseStatement.ElseValue != null))
			{
				caseStatement.Simplify();
				slotValue = new CaseStatementProjectedSlot(caseStatement, null);
			}
			else if (num >= 0)
			{
				slotValue = children[num].QualifySlotWithBlockAlias(slotNum);
			}
			else if (base.IsBoolSlot(slotNum))
			{
				slotValue = new BooleanProjectedSlot(BoolExpression.False, identifiers, base.SlotToBoolIndex(slotNum));
			}
			else
			{
				slotValue = new ConstantProjectedSlot(Domain.GetDefaultValueForMemberPath(memberPath2, base.GetLeaves(), base.ViewgenContext.Config), memberPath2);
			}
			bool enforceNotNull = base.IsBoolSlot(slotNum) && ((opType == CellTreeOpType.LOJ && num > 0) || opType == CellTreeOpType.FOJ);
			return new SlotInfo(true, true, slotValue, memberPath2, enforceNotNull);
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x000A0EF4 File Offset: 0x0009F0F4
		private static int GetInnerJoinChildForSlot(List<CqlBlock> children, int slotNum)
		{
			int num = -1;
			for (int i = 0; i < children.Count; i++)
			{
				CqlBlock cqlBlock = children[i];
				if (cqlBlock.IsProjected(slotNum))
				{
					ProjectedSlot projectedSlot = cqlBlock.SlotValue(slotNum);
					ConstantProjectedSlot constantProjectedSlot = projectedSlot as ConstantProjectedSlot;
					MemberProjectedSlot memberProjectedSlot = projectedSlot as MemberProjectedSlot;
					if (memberProjectedSlot != null)
					{
						num = i;
					}
					else if (constantProjectedSlot != null && constantProjectedSlot.CellConstant.IsNull())
					{
						if (num == -1)
						{
							num = i;
						}
					}
					else
					{
						num = i;
					}
				}
			}
			return num;
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x000A0F64 File Offset: 0x0009F164
		private void AddCaseForOuterJoins(CaseStatement caseForOuterJoins, CqlBlock child, int slotNum, CqlIdentifiers identifiers)
		{
			ProjectedSlot projectedSlot = child.SlotValue(slotNum);
			ConstantProjectedSlot constantProjectedSlot = projectedSlot as ConstantProjectedSlot;
			if (constantProjectedSlot != null && constantProjectedSlot.CellConstant.IsNull())
			{
				return;
			}
			BoolExpression boolExpression = BoolExpression.False;
			for (int i = 0; i < this.NumBoolSlots; i++)
			{
				int slotNum2 = base.BoolIndexToSlot(i);
				if (child.IsProjected(slotNum2))
				{
					QualifiedCellIdBoolean literal = new QualifiedCellIdBoolean(child, identifiers, i);
					boolExpression = BoolExpression.CreateOr(new BoolExpression[]
					{
						boolExpression,
						BoolExpression.CreateLiteral(literal, this.RightDomainMap)
					});
				}
			}
			QualifiedSlot value = child.QualifySlotWithBlockAlias(slotNum);
			caseForOuterJoins.AddWhenThen(boolExpression, value);
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x000A0FFC File Offset: 0x0009F1FC
		private static FragmentQuery GenerateFragmentQuery(IEnumerable<CellTreeNode> children, bool isLeft, ViewgenContext context, CellTreeOpType OpType)
		{
			FragmentQuery fragmentQuery = isLeft ? children.First<CellTreeNode>().LeftFragmentQuery : children.First<CellTreeNode>().RightFragmentQuery;
			FragmentQueryProcessor fragmentQueryProcessor = isLeft ? context.LeftFragmentQP : context.RightFragmentQP;
			foreach (CellTreeNode cellTreeNode in children.Skip(1))
			{
				FragmentQuery arg = isLeft ? cellTreeNode.LeftFragmentQuery : cellTreeNode.RightFragmentQuery;
				switch (OpType)
				{
				case CellTreeOpType.LOJ:
					break;
				case CellTreeOpType.IJ:
					fragmentQuery = fragmentQueryProcessor.Intersect(fragmentQuery, arg);
					break;
				case CellTreeOpType.LASJ:
					fragmentQuery = fragmentQueryProcessor.Difference(fragmentQuery, arg);
					break;
				default:
					fragmentQuery = fragmentQueryProcessor.Union(fragmentQuery, arg);
					break;
				}
			}
			return fragmentQuery;
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x000A10C0 File Offset: 0x0009F2C0
		internal static string OpToEsql(CellTreeOpType opType)
		{
			switch (opType)
			{
			case CellTreeOpType.Union:
				return "UNION ALL";
			case CellTreeOpType.FOJ:
				return "FULL OUTER JOIN";
			case CellTreeOpType.LOJ:
				return "LEFT OUTER JOIN";
			case CellTreeOpType.IJ:
				return "INNER JOIN";
			default:
				return null;
			}
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x000A10F8 File Offset: 0x0009F2F8
		internal override void ToCompactString(StringBuilder stringBuilder)
		{
			stringBuilder.Append("(");
			for (int i = 0; i < this.m_children.Count; i++)
			{
				CellTreeNode cellTreeNode = this.m_children[i];
				cellTreeNode.ToCompactString(stringBuilder);
				if (i != this.m_children.Count - 1)
				{
					StringUtil.FormatStringBuilder(stringBuilder, " {0} ", new object[]
					{
						this.OpType
					});
				}
			}
			stringBuilder.Append(")");
		}

		// Token: 0x0400127D RID: 4733
		private Set<MemberPath> m_attrs;

		// Token: 0x0400127E RID: 4734
		private List<CellTreeNode> m_children;

		// Token: 0x0400127F RID: 4735
		private CellTreeOpType m_opType;

		// Token: 0x04001280 RID: 4736
		private FragmentQuery m_leftFragmentQuery;

		// Token: 0x04001281 RID: 4737
		private FragmentQuery m_rightFragmentQuery;
	}
}
