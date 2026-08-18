using System;
using OracleInternal.Common;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002FE RID: 766
	public sealed class OracleLpUpdateStatement : OracleLpStatement
	{
		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001B65 RID: 7013 RVA: 0x0010D508 File Offset: 0x0010B708
		public override OracleLpStatementType StatementType
		{
			get
			{
				return OracleLpStatementType.Update;
			}
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x0010D50C File Offset: 0x0010B70C
		internal OracleLpUpdateStatement(OracleLpTextFragment text, IOracleMetadata odpContext) : base(text, odpContext)
		{
		}
	}
}
