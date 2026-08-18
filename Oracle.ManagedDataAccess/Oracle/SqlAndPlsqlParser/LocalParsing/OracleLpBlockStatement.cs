using System;
using OracleInternal.Common;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020001CC RID: 460
	internal sealed class OracleLpBlockStatement : OracleLpStatement
	{
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x000C3E6C File Offset: 0x000C206C
		public override OracleLpStatementType StatementType
		{
			get
			{
				return OracleLpStatementType.BlockStatement;
			}
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x000C3E70 File Offset: 0x000C2070
		internal OracleLpBlockStatement(OracleLpTextFragment text, IOracleMetadata odpContext) : base(text, odpContext)
		{
		}
	}
}
