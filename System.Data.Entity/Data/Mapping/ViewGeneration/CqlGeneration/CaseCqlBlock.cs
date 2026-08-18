using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.CqlGeneration
{
	// Token: 0x02000274 RID: 628
	internal sealed class CaseCqlBlock : CqlBlock
	{
		// Token: 0x06002638 RID: 9784 RVA: 0x00091C6C File Offset: 0x0008FE6C
		internal CaseCqlBlock(SlotInfo[] slots, int caseSlot, CqlBlock child, BoolExpression whereClause, CqlIdentifiers identifiers, int blockAliasNum) : base(slots, new List<CqlBlock>(new CqlBlock[]
		{
			child
		}), whereClause, identifiers, blockAliasNum)
		{
			this.m_caseSlotInfo = slots[caseSlot];
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x00091C94 File Offset: 0x0008FE94
		internal override StringBuilder AsEsql(StringBuilder builder, bool isTopLevel, int indentLevel)
		{
			StringUtil.IndentNewLine(builder, indentLevel);
			builder.Append("SELECT ");
			if (isTopLevel)
			{
				builder.Append("VALUE ");
			}
			builder.Append("-- Constructing ").Append(this.m_caseSlotInfo.OutputMember.LeafName);
			CqlBlock cqlBlock = base.Children[0];
			base.GenerateProjectionEsql(builder, cqlBlock.CqlAlias, true, indentLevel, isTopLevel);
			builder.Append("FROM (");
			cqlBlock.AsEsql(builder, false, indentLevel + 1);
			StringUtil.IndentNewLine(builder, indentLevel);
			builder.Append(") AS ").Append(cqlBlock.CqlAlias);
			if (!BoolExpression.EqualityComparer.Equals(base.WhereClause, BoolExpression.True))
			{
				StringUtil.IndentNewLine(builder, indentLevel);
				builder.Append("WHERE ");
				base.WhereClause.AsEsql(builder, cqlBlock.CqlAlias);
			}
			return builder;
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x00091D78 File Offset: 0x0008FF78
		internal override DbExpression AsCqt(bool isTopLevel)
		{
			CqlBlock cqlBlock = base.Children[0];
			DbExpression source = cqlBlock.AsCqt(false);
			if (!BoolExpression.EqualityComparer.Equals(base.WhereClause, BoolExpression.True))
			{
				source = from row in source
				where this.WhereClause.AsCqt(row)
				select row;
			}
			return from row in source
			select this.GenerateProjectionCqt(row, isTopLevel);
		}

		// Token: 0x040011BA RID: 4538
		private readonly SlotInfo m_caseSlotInfo;
	}
}
