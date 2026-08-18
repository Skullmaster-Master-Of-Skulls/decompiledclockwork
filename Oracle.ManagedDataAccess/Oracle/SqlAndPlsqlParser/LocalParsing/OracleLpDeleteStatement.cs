using System;
using OracleInternal.Common;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002B0 RID: 688
	public sealed class OracleLpDeleteStatement : OracleLpStatement
	{
		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060019BF RID: 6591 RVA: 0x001098E0 File Offset: 0x00107AE0
		public override OracleLpStatementType StatementType
		{
			get
			{
				return OracleLpStatementType.Delete;
			}
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x001098E4 File Offset: 0x00107AE4
		internal OracleLpDeleteStatement(OracleLpTextFragment text, IOracleMetadata odpContext) : base(text, odpContext)
		{
		}
	}
}
