using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration
{
	// Token: 0x0200042C RID: 1068
	internal sealed class JoinCqlBlock : CqlBlock
	{
		// Token: 0x0600274A RID: 10058 RVA: 0x000BE6F9 File Offset: 0x000BC8F9
		internal JoinCqlBlock(CellTreeOpType opType, SlotInfo[] slotInfos, List<CqlBlock> children, List<JoinCqlBlock.OnClause> onClauses, CqlIdentifiers identifiers, int blockAliasNum) : base(slotInfos, children, BoolExpression.True, identifiers, blockAliasNum)
		{
			this.m_opType = opType;
			this.m_onClauses = onClauses;
		}

		// Token: 0x0600274B RID: 10059 RVA: 0x000BE71C File Offset: 0x000BC91C
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

		// Token: 0x0600274C RID: 10060 RVA: 0x000BE82C File Offset: 0x000BCA2C
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

		// Token: 0x04000EC1 RID: 3777
		private readonly CellTreeOpType m_opType;

		// Token: 0x04000EC2 RID: 3778
		private readonly List<JoinCqlBlock.OnClause> m_onClauses;

		// Token: 0x0200042D RID: 1069
		internal sealed class OnClause : InternalBase
		{
			// Token: 0x0600274E RID: 10062 RVA: 0x000BE942 File Offset: 0x000BCB42
			internal OnClause()
			{
				this.m_singleClauses = new List<JoinCqlBlock.OnClause.SingleClause>();
			}

			// Token: 0x0600274F RID: 10063 RVA: 0x000BE958 File Offset: 0x000BCB58
			internal void Add(QualifiedSlot leftSlot, MemberPath leftSlotOutputMember, QualifiedSlot rightSlot, MemberPath rightSlotOutputMember)
			{
				JoinCqlBlock.OnClause.SingleClause item = new JoinCqlBlock.OnClause.SingleClause(leftSlot, leftSlotOutputMember, rightSlot, rightSlotOutputMember);
				this.m_singleClauses.Add(item);
			}

			// Token: 0x06002750 RID: 10064 RVA: 0x000BE97C File Offset: 0x000BCB7C
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

			// Token: 0x06002751 RID: 10065 RVA: 0x000BE9E4 File Offset: 0x000BCBE4
			internal DbExpression AsCqt(DbExpression leftRow, DbExpression rightRow)
			{
				DbExpression dbExpression = this.m_singleClauses[0].AsCqt(leftRow, rightRow);
				for (int i = 1; i < this.m_singleClauses.Count; i++)
				{
					dbExpression = dbExpression.And(this.m_singleClauses[i].AsCqt(leftRow, rightRow));
				}
				return dbExpression;
			}

			// Token: 0x06002752 RID: 10066 RVA: 0x000BEA36 File Offset: 0x000BCC36
			internal override void ToCompactString(StringBuilder builder)
			{
				builder.Append("ON ");
				StringUtil.ToSeparatedString(builder, this.m_singleClauses, " AND ");
			}

			// Token: 0x04000EC3 RID: 3779
			private readonly List<JoinCqlBlock.OnClause.SingleClause> m_singleClauses;

			// Token: 0x0200042E RID: 1070
			private sealed class SingleClause : InternalBase
			{
				// Token: 0x06002753 RID: 10067 RVA: 0x000BEA55 File Offset: 0x000BCC55
				internal SingleClause(QualifiedSlot leftSlot, MemberPath leftSlotOutputMember, QualifiedSlot rightSlot, MemberPath rightSlotOutputMember)
				{
					this.m_leftSlot = leftSlot;
					this.m_leftSlotOutputMember = leftSlotOutputMember;
					this.m_rightSlot = rightSlot;
					this.m_rightSlotOutputMember = rightSlotOutputMember;
				}

				// Token: 0x06002754 RID: 10068 RVA: 0x000BEA7A File Offset: 0x000BCC7A
				internal StringBuilder AsEsql(StringBuilder builder)
				{
					builder.Append(this.m_leftSlot.GetQualifiedCqlName(this.m_leftSlotOutputMember)).Append(" = ").Append(this.m_rightSlot.GetQualifiedCqlName(this.m_rightSlotOutputMember));
					return builder;
				}

				// Token: 0x06002755 RID: 10069 RVA: 0x000BEAB5 File Offset: 0x000BCCB5
				internal DbExpression AsCqt(DbExpression leftRow, DbExpression rightRow)
				{
					return this.m_leftSlot.AsCqt(leftRow, this.m_leftSlotOutputMember).Equal(this.m_rightSlot.AsCqt(rightRow, this.m_rightSlotOutputMember));
				}

				// Token: 0x06002756 RID: 10070 RVA: 0x000BEAE0 File Offset: 0x000BCCE0
				internal override void ToCompactString(StringBuilder builder)
				{
					this.m_leftSlot.ToCompactString(builder);
					builder.Append(" = ");
					this.m_rightSlot.ToCompactString(builder);
				}

				// Token: 0x04000EC4 RID: 3780
				private readonly QualifiedSlot m_leftSlot;

				// Token: 0x04000EC5 RID: 3781
				private readonly MemberPath m_leftSlotOutputMember;

				// Token: 0x04000EC6 RID: 3782
				private readonly QualifiedSlot m_rightSlot;

				// Token: 0x04000EC7 RID: 3783
				private readonly MemberPath m_rightSlotOutputMember;
			}
		}
	}
}
