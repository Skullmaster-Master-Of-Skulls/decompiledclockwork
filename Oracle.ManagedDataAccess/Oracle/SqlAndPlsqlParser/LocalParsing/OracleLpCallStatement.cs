using System;
using OracleInternal.Common;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020001CD RID: 461
	public sealed class OracleLpCallStatement : OracleLpStatement
	{
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x000C3E7C File Offset: 0x000C207C
		public override OracleLpStatementType StatementType
		{
			get
			{
				return OracleLpStatementType.Call;
			}
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x000C3E80 File Offset: 0x000C2080
		internal OracleLpCallStatement(OracleLpTextFragment text, IOracleMetadata odpContext) : base(text, odpContext)
		{
		}
	}
}
