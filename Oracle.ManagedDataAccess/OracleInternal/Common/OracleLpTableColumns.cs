using System;
using System.Collections.Generic;

namespace OracleInternal.Common
{
	// Token: 0x020000BC RID: 188
	public class OracleLpTableColumns
	{
		// Token: 0x0600073D RID: 1853 RVA: 0x000434A0 File Offset: 0x000416A0
		public OracleLpTableColumns(OracleLpTable table, IEnumerable<OracleLpColumn> columns)
		{
			this.m_table = table;
			this.m_columns = columns;
		}

		// Token: 0x040009C2 RID: 2498
		public OracleLpTable m_table;

		// Token: 0x040009C3 RID: 2499
		public IEnumerable<OracleLpColumn> m_columns;
	}
}
