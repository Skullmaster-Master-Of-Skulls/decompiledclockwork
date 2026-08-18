using System;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.CqlGeneration
{
	// Token: 0x02000273 RID: 627
	internal sealed class BooleanProjectedSlot : ProjectedSlot
	{
		// Token: 0x06002633 RID: 9779 RVA: 0x00091B4A File Offset: 0x0008FD4A
		internal BooleanProjectedSlot(BoolExpression expr, CqlIdentifiers identifiers, int originalCellNum)
		{
			this.m_expr = expr;
			this.m_originalCell = new CellIdBoolean(identifiers, originalCellNum);
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x00091B66 File Offset: 0x0008FD66
		internal override string GetCqlFieldAlias(MemberPath outputMember)
		{
			return this.m_originalCell.SlotName;
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x00091B74 File Offset: 0x0008FD74
		internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias, int indentLevel)
		{
			if (this.m_expr.IsTrue || this.m_expr.IsFalse)
			{
				this.m_expr.AsEsql(builder, blockAlias);
			}
			else
			{
				builder.Append("CASE WHEN ");
				this.m_expr.AsEsql(builder, blockAlias);
				builder.Append(" THEN True ELSE False END");
			}
			return builder;
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x00091BD4 File Offset: 0x0008FDD4
		internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
		{
			if (this.m_expr.IsTrue || this.m_expr.IsFalse)
			{
				return this.m_expr.AsCqt(row);
			}
			return DbExpressionBuilder.Case(new DbExpression[]
			{
				this.m_expr.AsCqt(row)
			}, new DbExpression[]
			{
				DbExpressionBuilder.True
			}, DbExpressionBuilder.False);
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x00091C35 File Offset: 0x0008FE35
		internal override void ToCompactString(StringBuilder builder)
		{
			StringUtil.FormatStringBuilder(builder, "<{0}, ", new object[]
			{
				this.m_originalCell.SlotName
			});
			this.m_expr.ToCompactString(builder);
			builder.Append('>');
		}

		// Token: 0x040011B8 RID: 4536
		private readonly BoolExpression m_expr;

		// Token: 0x040011B9 RID: 4537
		private readonly CellIdBoolean m_originalCell;
	}
}
