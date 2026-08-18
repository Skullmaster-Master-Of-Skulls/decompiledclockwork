using System;
using OracleInternal.Common;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020001D3 RID: 467
	public sealed class OracleLpExplainPlanStatement : OracleLpStatement
	{
		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x000C3EFC File Offset: 0x000C20FC
		public override OracleLpStatementType StatementType
		{
			get
			{
				return OracleLpStatementType.ExplainPlan;
			}
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x000C3F00 File Offset: 0x000C2100
		internal OracleLpExplainPlanStatement(OracleLpTextFragment text, IOracleMetadata odpContext) : base(text, odpContext)
		{
		}
	}
}
