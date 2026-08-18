using System;
using OracleInternal.Common;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002D9 RID: 729
	public sealed class OracleLpMergeStatement : OracleLpStatement
	{
		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001A8F RID: 6799 RVA: 0x0010B04C File Offset: 0x0010924C
		public override OracleLpStatementType StatementType
		{
			get
			{
				return OracleLpStatementType.Merge;
			}
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x0010B050 File Offset: 0x00109250
		internal OracleLpMergeStatement(OracleLpTextFragment text, IOracleMetadata odpContext) : base(text, odpContext)
		{
		}
	}
}
