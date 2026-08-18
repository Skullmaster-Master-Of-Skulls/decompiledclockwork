using System;
using OracleInternal.Common;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020001D2 RID: 466
	public sealed class OracleLpExecuteStatement : OracleLpStatement
	{
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x000C3EEC File Offset: 0x000C20EC
		public override OracleLpStatementType StatementType
		{
			get
			{
				return OracleLpStatementType.Execute;
			}
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x000C3EF0 File Offset: 0x000C20F0
		internal OracleLpExecuteStatement(OracleLpTextFragment text, IOracleMetadata odpContext) : base(text, odpContext)
		{
		}
	}
}
