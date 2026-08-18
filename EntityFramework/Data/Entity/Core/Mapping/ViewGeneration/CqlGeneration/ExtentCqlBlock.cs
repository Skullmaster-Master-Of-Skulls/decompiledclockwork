using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration
{
	// Token: 0x0200042B RID: 1067
	internal sealed class ExtentCqlBlock : CqlBlock
	{
		// Token: 0x06002745 RID: 10053 RVA: 0x000BE548 File Offset: 0x000BC748
		internal ExtentCqlBlock(EntitySetBase extent, CellQuery.SelectDistinct selectDistinct, SlotInfo[] slots, BoolExpression whereClause, CqlIdentifiers identifiers, int blockAliasNum) : base(slots, ExtentCqlBlock._emptyChildren, whereClause, identifiers, blockAliasNum)
		{
			this.m_extent = extent;
			this.m_nodeTableAlias = identifiers.GetBlockAlias();
			this.m_selectDistinct = selectDistinct;
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x000BE578 File Offset: 0x000BC778
		internal override StringBuilder AsEsql(StringBuilder builder, bool isTopLevel, int indentLevel)
		{
			StringUtil.IndentNewLine(builder, indentLevel);
			builder.Append("SELECT ");
			if (this.m_selectDistinct == CellQuery.SelectDistinct.Yes)
			{
				builder.Append("DISTINCT ");
			}
			base.GenerateProjectionEsql(builder, this.m_nodeTableAlias, true, indentLevel, isTopLevel);
			builder.Append("FROM ");
			CqlWriter.AppendEscapedQualifiedName(builder, this.m_extent.EntityContainer.Name, this.m_extent.Name);
			builder.Append(" AS ").Append(this.m_nodeTableAlias);
			if (!BoolExpression.EqualityComparer.Equals(base.WhereClause, BoolExpression.True))
			{
				StringUtil.IndentNewLine(builder, indentLevel);
				builder.Append("WHERE ");
				base.WhereClause.AsEsql(builder, this.m_nodeTableAlias);
			}
			return builder;
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x000BE66C File Offset: 0x000BC86C
		internal override DbExpression AsCqt(bool isTopLevel)
		{
			DbExpression dbExpression = this.m_extent.Scan();
			if (!BoolExpression.EqualityComparer.Equals(base.WhereClause, BoolExpression.True))
			{
				dbExpression = from row in dbExpression
				where base.WhereClause.AsCqt(row)
				select row;
			}
			dbExpression = from row in dbExpression
			select this.GenerateProjectionCqt(row, isTopLevel);
			if (this.m_selectDistinct == CellQuery.SelectDistinct.Yes)
			{
				dbExpression = dbExpression.Distinct();
			}
			return dbExpression;
		}

		// Token: 0x04000EBD RID: 3773
		private readonly EntitySetBase m_extent;

		// Token: 0x04000EBE RID: 3774
		private readonly string m_nodeTableAlias;

		// Token: 0x04000EBF RID: 3775
		private readonly CellQuery.SelectDistinct m_selectDistinct;

		// Token: 0x04000EC0 RID: 3776
		private static readonly List<CqlBlock> _emptyChildren = new List<CqlBlock>();
	}
}
