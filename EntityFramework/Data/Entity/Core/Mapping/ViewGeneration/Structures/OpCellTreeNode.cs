using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Resources;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000483 RID: 1155
	internal class OpCellTreeNode : CellTreeNode
	{
		// Token: 0x06002ABE RID: 10942 RVA: 0x000CE45B File Offset: 0x000CC65B
		internal OpCellTreeNode(ViewgenContext context, CellTreeOpType opType) : base(context)
		{
			this.m_opType = opType;
			this.m_attrs = new Set<MemberPath>(MemberPath.EqualityComparer);
			this.m_children = new List<CellTreeNode>();
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x000CE486 File Offset: 0x000CC686
		internal OpCellTreeNode(ViewgenContext context, CellTreeOpType opType, params CellTreeNode[] children) : this(context, opType, (IEnumerable<CellTreeNode>)children)
		{
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x000CE498 File Offset: 0x000CC698
		internal OpCellTreeNode(ViewgenContext context, CellTreeOpType opType, IEnumerable<CellTreeNode> children) : this(context, opType)
		{
			foreach (CellTreeNode child in children)
			{
				this.Add(child);
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06002AC1 RID: 10945 RVA: 0x000CE4E8 File Offset: 0x000CC6E8
		internal override CellTreeOpType OpType
		{
			get
			{
				return this.m_opType;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06002AC2 RID: 10946 RVA: 0x000CE4F0 File Offset: 0x000CC6F0
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

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06002AC3 RID: 10947 RVA: 0x000CE51E File Offset: 0x000CC71E
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

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06002AC4 RID: 10948 RVA: 0x000CE54C File Offset: 0x000CC74C
		internal override MemberDomainMap RightDomainMap
		{
			get
			{
				return this.m_children[0].RightDomainMap;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06002AC5 RID: 10949 RVA: 0x000CE55F File Offset: 0x000CC75F
		internal override Set<MemberPath> Attributes
		{
			get
			{
				return this.m_attrs;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06002AC6 RID: 10950 RVA: 0x000CE567 File Offset: 0x000CC767
		internal override List<CellTreeNode> Children
		{
			get
			{
				return this.m_children;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06002AC7 RID: 10951 RVA: 0x000CE56F File Offset: 0x000CC76F
		internal override int NumProjectedSlots
		{
			get
			{
				return this.m_children[0].NumProjectedSlots;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06002AC8 RID: 10952 RVA: 0x000CE582 File Offset: 0x000CC782
		internal override int NumBoolSlots
		{
			get
			{
				return this.m_children[0].NumBoolSlots;
			}
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x000CE595 File Offset: 0x000CC795
		internal override TOutput Accept<TInput, TOutput>(CellTreeNode.SimpleCellTreeVisitor<TInput, TOutput> visitor, TInput param)
		{
			return visitor.VisitOpNode(this, param);
		}

		// Token: 0x06002ACA RID: 10954 RVA: 0x000CE5A0 File Offset: 0x000CC7A0
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

		// Token: 0x06002ACB RID: 10955 RVA: 0x000CE607 File Offset: 0x000CC807
		internal void Add(CellTreeNode child)
		{
			this.Insert(this.m_children.Count, child);
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x000CE61B File Offset: 0x000CC81B
		internal void AddFirst(CellTreeNode child)
		{
			this.Insert(0, child);
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x000CE625 File Offset: 0x000CC825
		private void Insert(int index, CellTreeNode child)
		{
			this.m_attrs.Unite(child.Attributes);
			this.m_children.Insert(index, child);
			this.m_leftFragmentQuery = null;
			this.m_rightFragmentQuery = null;
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x000CE654 File Offset: 0x000CC854
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

		// Token: 0x06002ACF RID: 10959 RVA: 0x000CE688 File Offset: 0x000CC888
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

		// Token: 0x06002AD0 RID: 10960 RVA: 0x000CE6E4 File Offset: 0x000CC8E4
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
							MemberPath outputMember = cqlBlock.MemberPath(j);
							array[j] = new SlotInfo(true, true, new ConstantProjectedSlot(Constant.Null), outputMember);
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
							array2[num2] = new SlotInfo(true, true, new ConstantProjectedSlot(Constant.Null), item.OutputMember);
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
			return new UnionCqlBlock(array3, list, identifiers, ++blockAliasNum);
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x000CEA28 File Offset: 0x000CCC28
		private static void AndWith(bool[] boolArray, bool[] another)
		{
			for (int i = 0; i < boolArray.Length; i++)
			{
				boolArray[i] &= another[i];
			}
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x000CEA5C File Offset: 0x000CCC5C
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
						errorLog.AddEntry(new ErrorLog.Record(ViewGenErrorCode.NoJoinKeyOrFKProvidedInMapping, Strings.Viewgen_NoJoinKeyOrFK, base.ViewgenContext.AllWrappersForExtent, string.Empty));
						ExceptionHelpers.ThrowMappingException(errorLog, base.ViewgenContext.Config);
					}
					QualifiedSlot leftSlot = list[0].QualifySlotWithBlockAlias(num3);
					QualifiedSlot rightSlot = cqlBlock2.QualifySlotWithBlockAlias(num3);
					MemberPath outputMember = array[num3].OutputMember;
					onClause.Add(leftSlot, outputMember, rightSlot, outputMember);
				}
				list3.Add(onClause);
			}
			return new JoinCqlBlock(this.OpType, array, list, list3, identifiers, ++blockAliasNum);
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x000CECDC File Offset: 0x000CCEDC
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
				slotValue = new ConstantProjectedSlot(Domain.GetDefaultValueForMemberPath(memberPath2, base.GetLeaves(), base.ViewgenContext.Config));
			}
			bool enforceNotNull = base.IsBoolSlot(slotNum) && ((opType == CellTreeOpType.LOJ && num > 0) || opType == CellTreeOpType.FOJ);
			return new SlotInfo(true, true, slotValue, memberPath2, enforceNotNull);
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x000CEE3C File Offset: 0x000CD03C
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

		// Token: 0x06002AD5 RID: 10965 RVA: 0x000CEEAC File Offset: 0x000CD0AC
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

		// Token: 0x06002AD6 RID: 10966 RVA: 0x000CEF48 File Offset: 0x000CD148
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

		// Token: 0x06002AD7 RID: 10967 RVA: 0x000CF010 File Offset: 0x000CD210
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

		// Token: 0x06002AD8 RID: 10968 RVA: 0x000CF054 File Offset: 0x000CD254
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

		// Token: 0x04000FB5 RID: 4021
		private readonly Set<MemberPath> m_attrs;

		// Token: 0x04000FB6 RID: 4022
		private readonly List<CellTreeNode> m_children;

		// Token: 0x04000FB7 RID: 4023
		private readonly CellTreeOpType m_opType;

		// Token: 0x04000FB8 RID: 4024
		private FragmentQuery m_leftFragmentQuery;

		// Token: 0x04000FB9 RID: 4025
		private FragmentQuery m_rightFragmentQuery;
	}
}
