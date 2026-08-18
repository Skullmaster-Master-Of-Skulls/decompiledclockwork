using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration
{
	// Token: 0x02000430 RID: 1072
	internal sealed class UnionCqlBlock : CqlBlock
	{
		// Token: 0x06002763 RID: 10083 RVA: 0x000BEC5A File Offset: 0x000BCE5A
		internal UnionCqlBlock(SlotInfo[] slotInfos, List<CqlBlock> children, CqlIdentifiers identifiers, int blockAliasNum) : base(slotInfos, children, BoolExpression.True, identifiers, blockAliasNum)
		{
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x000BEC6C File Offset: 0x000BCE6C
		internal override StringBuilder AsEsql(StringBuilder builder, bool isTopLevel, int indentLevel)
		{
			bool flag = true;
			foreach (CqlBlock cqlBlock in base.Children)
			{
				if (!flag)
				{
					StringUtil.IndentNewLine(builder, indentLevel + 1);
					builder.Append(OpCellTreeNode.OpToEsql(CellTreeOpType.Union));
				}
				flag = false;
				builder.Append(" (");
				cqlBlock.AsEsql(builder, isTopLevel, indentLevel + 1);
				builder.Append(')');
			}
			return builder;
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x000BECF4 File Offset: 0x000BCEF4
		internal override DbExpression AsCqt(bool isTopLevel)
		{
			DbExpression dbExpression = base.Children[0].AsCqt(isTopLevel);
			for (int i = 1; i < base.Children.Count; i++)
			{
				dbExpression = dbExpression.UnionAll(base.Children[i].AsCqt(isTopLevel));
			}
			return dbExpression;
		}
	}
}
