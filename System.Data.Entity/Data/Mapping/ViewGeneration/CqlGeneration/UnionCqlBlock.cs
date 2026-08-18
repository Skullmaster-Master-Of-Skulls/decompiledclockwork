using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.CqlGeneration
{
	// Token: 0x0200027A RID: 634
	internal sealed class UnionCqlBlock : CqlBlock
	{
		// Token: 0x06002665 RID: 9829 RVA: 0x0009266A File Offset: 0x0009086A
		internal UnionCqlBlock(SlotInfo[] slotInfos, List<CqlBlock> children, CqlIdentifiers identifiers, int blockAliasNum) : base(slotInfos, children, BoolExpression.True, identifiers, blockAliasNum)
		{
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x0009267C File Offset: 0x0009087C
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

		// Token: 0x06002667 RID: 9831 RVA: 0x00092704 File Offset: 0x00090904
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
