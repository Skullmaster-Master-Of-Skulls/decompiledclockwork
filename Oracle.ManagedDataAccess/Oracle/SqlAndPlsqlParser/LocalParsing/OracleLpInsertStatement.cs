using System;
using OracleInternal.Common;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002CE RID: 718
	public sealed class OracleLpInsertStatement : OracleLpStatement
	{
		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001A6C RID: 6764 RVA: 0x0010AD64 File Offset: 0x00108F64
		public override OracleLpStatementType StatementType
		{
			get
			{
				return OracleLpStatementType.Insert;
			}
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x0010AD68 File Offset: 0x00108F68
		internal OracleLpInsertStatement(OracleLpTextFragment text, IOracleMetadata odpContext) : base(text, odpContext)
		{
		}
	}
}
