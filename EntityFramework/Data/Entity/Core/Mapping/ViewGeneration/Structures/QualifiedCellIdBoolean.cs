using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000484 RID: 1156
	internal sealed class QualifiedCellIdBoolean : CellIdBoolean
	{
		// Token: 0x06002AD9 RID: 10969 RVA: 0x000CF0D5 File Offset: 0x000CD2D5
		internal QualifiedCellIdBoolean(CqlBlock block, CqlIdentifiers identifiers, int originalCellNum) : base(identifiers, originalCellNum)
		{
			this.m_block = block;
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x000CF0E6 File Offset: 0x000CD2E6
		internal override StringBuilder AsEsql(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			return base.AsEsql(builder, this.m_block.CqlAlias, skipIsNotNull);
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x000CF0FB File Offset: 0x000CD2FB
		internal override DbExpression AsCqt(DbExpression row, bool skipIsNotNull)
		{
			return base.AsCqt(this.m_block.GetInput(row), skipIsNotNull);
		}

		// Token: 0x04000FBA RID: 4026
		private readonly CqlBlock m_block;
	}
}
