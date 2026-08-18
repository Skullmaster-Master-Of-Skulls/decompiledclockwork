using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Data.Metadata.Edm;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.CqlGeneration
{
	// Token: 0x02000277 RID: 631
	internal sealed class ExtentCqlBlock : CqlBlock
	{
		// Token: 0x06002651 RID: 9809 RVA: 0x00092150 File Offset: 0x00090350
		internal ExtentCqlBlock(EntitySetBase extent, CellQuery.SelectDistinct selectDistinct, SlotInfo[] slots, BoolExpression whereClause, CqlIdentifiers identifiers, int blockAliasNum) : base(slots, ExtentCqlBlock.EmptyChildren, whereClause, identifiers, blockAliasNum)
		{
			this.m_extent = extent;
			this.m_nodeTableAlias = identifiers.GetBlockAlias();
			this.m_selectDistinct = selectDistinct;
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x00092180 File Offset: 0x00090380
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

		// Token: 0x06002653 RID: 9811 RVA: 0x00092248 File Offset: 0x00090448
		internal override DbExpression AsCqt(bool isTopLevel)
		{
			DbExpression dbExpression = this.m_extent.Scan();
			if (!BoolExpression.EqualityComparer.Equals(base.WhereClause, BoolExpression.True))
			{
				dbExpression = from row in dbExpression
				where this.WhereClause.AsCqt(row)
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

		// Token: 0x040011C1 RID: 4545
		private readonly EntitySetBase m_extent;

		// Token: 0x040011C2 RID: 4546
		private readonly string m_nodeTableAlias;

		// Token: 0x040011C3 RID: 4547
		private readonly CellQuery.SelectDistinct m_selectDistinct;

		// Token: 0x040011C4 RID: 4548
		private static readonly List<CqlBlock> EmptyChildren = new List<CqlBlock>();
	}
}
