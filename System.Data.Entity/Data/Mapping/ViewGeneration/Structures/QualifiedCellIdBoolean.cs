using System;
using System.Data.Common.CommandTrees;
using System.Data.Mapping.ViewGeneration.CqlGeneration;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x020002B8 RID: 696
	internal sealed class QualifiedCellIdBoolean : CellIdBoolean
	{
		// Token: 0x0600297E RID: 10622 RVA: 0x000A1289 File Offset: 0x0009F489
		internal QualifiedCellIdBoolean(CqlBlock block, CqlIdentifiers identifiers, int originalCellNum) : base(identifiers, originalCellNum)
		{
			this.m_block = block;
		}

		// Token: 0x0600297F RID: 10623 RVA: 0x000A129A File Offset: 0x0009F49A
		internal override StringBuilder AsEsql(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			return base.AsEsql(builder, this.m_block.CqlAlias, skipIsNotNull);
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x000A12AF File Offset: 0x0009F4AF
		internal override DbExpression AsCqt(DbExpression row, bool skipIsNotNull)
		{
			return base.AsCqt(this.m_block.GetInput(row), skipIsNotNull);
		}

		// Token: 0x04001283 RID: 4739
		private readonly CqlBlock m_block;
	}
}
