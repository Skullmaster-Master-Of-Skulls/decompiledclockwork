using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration
{
	// Token: 0x02000428 RID: 1064
	internal sealed class CaseCqlBlock : CqlBlock
	{
		// Token: 0x06002735 RID: 10037 RVA: 0x000BE190 File Offset: 0x000BC390
		internal CaseCqlBlock(SlotInfo[] slots, int caseSlot, CqlBlock child, BoolExpression whereClause, CqlIdentifiers identifiers, int blockAliasNum) : base(slots, new List<CqlBlock>(new CqlBlock[]
		{
			child
		}), whereClause, identifiers, blockAliasNum)
		{
			this.m_caseSlotInfo = slots[caseSlot];
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x000BE1C4 File Offset: 0x000BC3C4
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

		// Token: 0x06002737 RID: 10039 RVA: 0x000BE2D4 File Offset: 0x000BC4D4
		internal override DbExpression AsCqt(bool isTopLevel)
		{
			CqlBlock cqlBlock = base.Children[0];
			DbExpression source = cqlBlock.AsCqt(false);
			if (!BoolExpression.EqualityComparer.Equals(base.WhereClause, BoolExpression.True))
			{
				source = from row in source
				where base.WhereClause.AsCqt(row)
				select row;
			}
			return from row in source
			select this.GenerateProjectionCqt(row, isTopLevel);
		}

		// Token: 0x04000EBA RID: 3770
		private readonly SlotInfo m_caseSlotInfo;
	}
}
