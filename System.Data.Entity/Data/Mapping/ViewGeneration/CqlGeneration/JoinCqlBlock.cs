using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.CqlGeneration
{
	// Token: 0x02000278 RID: 632
	internal sealed class JoinCqlBlock : CqlBlock
	{
		// Token: 0x06002655 RID: 9813 RVA: 0x000922CE File Offset: 0x000904CE
		internal JoinCqlBlock(CellTreeOpType opType, SlotInfo[] slotInfos, List<CqlBlock> children, List<JoinCqlBlock.OnClause> onClauses, CqlIdentifiers identifiers, int blockAliasNum) : base(slotInfos, children, BoolExpression.True, identifiers, blockAliasNum)
		{
			this.m_opType = opType;
			this.m_onClauses = onClauses;
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x000922F0 File Offset: 0x000904F0
		internal override StringBuilder AsEsql(StringBuilder builder, bool isTopLevel, int indentLevel)
		{
			StringUtil.IndentNewLine(builder, indentLevel);
			builder.Append("SELECT ");
			base.GenerateProjectionEsql(builder, null, false, indentLevel, isTopLevel);
			StringUtil.IndentNewLine(builder, indentLevel);
			builder.Append("FROM ");
			int num = 0;
			foreach (CqlBlock cqlBlock in base.Children)
			{
				if (num > 0)
				{
					StringUtil.IndentNewLine(builder, indentLevel + 1);
					builder.Append(OpCellTreeNode.OpToEsql(this.m_opType));
				}
				builder.Append(" (");
				cqlBlock.AsEsql(builder, false, indentLevel + 1);
				builder.Append(") AS ").Append(cqlBlock.CqlAlias);
				if (num > 0)
				{
					StringUtil.IndentNewLine(builder, indentLevel + 1);
					builder.Append("ON ");
					this.m_onClauses[num - 1].AsEsql(builder);
				}
				num++;
			}
			return builder;
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x000923F4 File Offset: 0x000905F4
		internal override DbExpression AsCqt(bool isTopLevel)
		{
			CqlBlock cqlBlock = base.Children[0];
			DbExpression dbExpression = cqlBlock.AsCqt(false);
			List<string> list = new List<string>();
			for (int i = 1; i < base.Children.Count; i++)
			{
				CqlBlock cqlBlock2 = base.Children[i];
				DbExpression right = cqlBlock2.AsCqt(false);
				Func<DbExpression, DbExpression, DbExpression> joinCondition = new Func<DbExpression, DbExpression, DbExpression>(this.m_onClauses[i - 1].AsCqt);
				DbJoinExpression dbJoinExpression;
				switch (this.m_opType)
				{
				case CellTreeOpType.FOJ:
					dbJoinExpression = dbExpression.FullOuterJoin(right, joinCondition);
					break;
				case CellTreeOpType.LOJ:
					dbJoinExpression = dbExpression.LeftOuterJoin(right, joinCondition);
					break;
				case CellTreeOpType.IJ:
					dbJoinExpression = dbExpression.InnerJoin(right, joinCondition);
					break;
				default:
					return null;
				}
				if (i == 1)
				{
					cqlBlock.SetJoinTreeContext(list, dbJoinExpression.Left.VariableName);
				}
				else
				{
					list.Add(dbJoinExpression.Left.VariableName);
				}
				cqlBlock2.SetJoinTreeContext(list, dbJoinExpression.Right.VariableName);
				dbExpression = dbJoinExpression;
			}
			return from row in dbExpression
			select base.GenerateProjectionCqt(row, false);
		}

		// Token: 0x040011C5 RID: 4549
		private readonly CellTreeOpType m_opType;

		// Token: 0x040011C6 RID: 4550
		private readonly List<JoinCqlBlock.OnClause> m_onClauses;

		// Token: 0x020005A4 RID: 1444
		internal sealed class OnClause : InternalBase
		{
			// Token: 0x06004052 RID: 16466 RVA: 0x000EC9E3 File Offset: 0x000EABE3
			internal OnClause()
			{
				this.m_singleClauses = new List<JoinCqlBlock.OnClause.SingleClause>();
			}

			// Token: 0x06004053 RID: 16467 RVA: 0x000EC9F8 File Offset: 0x000EABF8
			internal void Add(QualifiedSlot leftSlot, MemberPath leftSlotOutputMember, QualifiedSlot rightSlot, MemberPath rightSlotOutputMember)
			{
				JoinCqlBlock.OnClause.SingleClause item = new JoinCqlBlock.OnClause.SingleClause(leftSlot, leftSlotOutputMember, rightSlot, rightSlotOutputMember);
				this.m_singleClauses.Add(item);
			}

			// Token: 0x06004054 RID: 16468 RVA: 0x000ECA1C File Offset: 0x000EAC1C
			internal StringBuilder AsEsql(StringBuilder builder)
			{
				bool flag = true;
				foreach (JoinCqlBlock.OnClause.SingleClause singleClause in this.m_singleClauses)
				{
					if (!flag)
					{
						builder.Append(" AND ");
					}
					singleClause.AsEsql(builder);
					flag = false;
				}
				return builder;
			}

			// Token: 0x06004055 RID: 16469 RVA: 0x000ECA84 File Offset: 0x000EAC84
			internal DbExpression AsCqt(DbExpression leftRow, DbExpression rightRow)
			{
				DbExpression dbExpression = this.m_singleClauses[0].AsCqt(leftRow, rightRow);
				for (int i = 1; i < this.m_singleClauses.Count; i++)
				{
					dbExpression = dbExpression.And(this.m_singleClauses[i].AsCqt(leftRow, rightRow));
				}
				return dbExpression;
			}

			// Token: 0x06004056 RID: 16470 RVA: 0x000ECAD6 File Offset: 0x000EACD6
			internal override void ToCompactString(StringBuilder builder)
			{
				builder.Append("ON ");
				StringUtil.ToSeparatedString(builder, this.m_singleClauses, " AND ");
			}

			// Token: 0x04001CDF RID: 7391
			private readonly List<JoinCqlBlock.OnClause.SingleClause> m_singleClauses;

			// Token: 0x0200076C RID: 1900
			private sealed class SingleClause : InternalBase
			{
				// Token: 0x06004851 RID: 18513 RVA: 0x00104DF3 File Offset: 0x00102FF3
				internal SingleClause(QualifiedSlot leftSlot, MemberPath leftSlotOutputMember, QualifiedSlot rightSlot, MemberPath rightSlotOutputMember)
				{
					this.m_leftSlot = leftSlot;
					this.m_leftSlotOutputMember = leftSlotOutputMember;
					this.m_rightSlot = rightSlot;
					this.m_rightSlotOutputMember = rightSlotOutputMember;
				}

				// Token: 0x06004852 RID: 18514 RVA: 0x00104E18 File Offset: 0x00103018
				internal StringBuilder AsEsql(StringBuilder builder)
				{
					builder.Append(this.m_leftSlot.GetQualifiedCqlName(this.m_leftSlotOutputMember)).Append(" = ").Append(this.m_rightSlot.GetQualifiedCqlName(this.m_rightSlotOutputMember));
					return builder;
				}

				// Token: 0x06004853 RID: 18515 RVA: 0x00104E53 File Offset: 0x00103053
				internal DbExpression AsCqt(DbExpression leftRow, DbExpression rightRow)
				{
					return this.m_leftSlot.AsCqt(leftRow, this.m_leftSlotOutputMember).Equal(this.m_rightSlot.AsCqt(rightRow, this.m_rightSlotOutputMember));
				}

				// Token: 0x06004854 RID: 18516 RVA: 0x00104E7E File Offset: 0x0010307E
				internal override void ToCompactString(StringBuilder builder)
				{
					this.m_leftSlot.ToCompactString(builder);
					builder.Append(" = ");
					this.m_rightSlot.ToCompactString(builder);
				}

				// Token: 0x04002137 RID: 8503
				private readonly QualifiedSlot m_leftSlot;

				// Token: 0x04002138 RID: 8504
				private readonly MemberPath m_leftSlotOutputMember;

				// Token: 0x04002139 RID: 8505
				private readonly QualifiedSlot m_rightSlot;

				// Token: 0x0400213A RID: 8506
				private readonly MemberPath m_rightSlotOutputMember;
			}
		}
	}
}
