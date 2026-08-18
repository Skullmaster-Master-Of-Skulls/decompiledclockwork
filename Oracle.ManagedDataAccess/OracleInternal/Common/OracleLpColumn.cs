using System;

namespace OracleInternal.Common
{
	// Token: 0x020000BB RID: 187
	public class OracleLpColumn
	{
		// Token: 0x0600073C RID: 1852 RVA: 0x00043490 File Offset: 0x00041690
		public OracleLpColumn(string columnName)
		{
			this.m_columnName = columnName;
		}

		// Token: 0x040009BE RID: 2494
		public string m_columnName;

		// Token: 0x040009BF RID: 2495
		public string m_tableName;

		// Token: 0x040009C0 RID: 2496
		public string m_schemaName;

		// Token: 0x040009C1 RID: 2497
		public bool m_isHidden;
	}
}
