using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration
{
	// Token: 0x02000425 RID: 1061
	internal sealed class BooleanProjectedSlot : ProjectedSlot
	{
		// Token: 0x0600271B RID: 10011 RVA: 0x000BDD13 File Offset: 0x000BBF13
		internal BooleanProjectedSlot(BoolExpression expr, CqlIdentifiers identifiers, int originalCellNum)
		{
			this.m_expr = expr;
			this.m_originalCell = new CellIdBoolean(identifiers, originalCellNum);
		}

		// Token: 0x0600271C RID: 10012 RVA: 0x000BDD2F File Offset: 0x000BBF2F
		internal override string GetCqlFieldAlias(MemberPath outputMember)
		{
			return this.m_originalCell.SlotName;
		}

		// Token: 0x0600271D RID: 10013 RVA: 0x000BDD3C File Offset: 0x000BBF3C
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

		// Token: 0x0600271E RID: 10014 RVA: 0x000BDD9C File Offset: 0x000BBF9C
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

		// Token: 0x0600271F RID: 10015 RVA: 0x000BDE04 File Offset: 0x000BC004
		internal override void ToCompactString(StringBuilder builder)
		{
			StringUtil.FormatStringBuilder(builder, "<{0}, ", new object[]
			{
				this.m_originalCell.SlotName
			});
			this.m_expr.ToCompactString(builder);
			builder.Append('>');
		}

		// Token: 0x04000EAE RID: 3758
		private readonly BoolExpression m_expr;

		// Token: 0x04000EAF RID: 3759
		private readonly CellIdBoolean m_originalCell;
	}
}
