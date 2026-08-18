using System;
using OracleInternal.Common;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002AE RID: 686
	public sealed class OracleLpCreateStatement : OracleLpStatement
	{
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x060019B4 RID: 6580 RVA: 0x001097DC File Offset: 0x001079DC
		public override OracleLpStatementType StatementType
		{
			get
			{
				return OracleLpStatementType.Create;
			}
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x001097E0 File Offset: 0x001079E0
		internal OracleLpCreateStatement(OracleLpTextFragment text, IOracleMetadata odpContext) : base(text, odpContext)
		{
		}
	}
}
