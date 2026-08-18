using System;

namespace OracleInternal.Common
{
	// Token: 0x020000BA RID: 186
	public class OracleLpTable
	{
		// Token: 0x0600073B RID: 1851 RVA: 0x00043438 File Offset: 0x00041638
		public OracleLpTable(string schemaName, string tableName, string dbLinkName = null)
		{
			if (schemaName == null)
			{
				this.m_schemaName = string.Empty;
			}
			else
			{
				this.m_schemaName = schemaName;
			}
			if (tableName == null)
			{
				this.m_tableName = string.Empty;
			}
			else
			{
				this.m_tableName = tableName;
			}
			if (dbLinkName == null)
			{
				this.m_dbLinkName = string.Empty;
				return;
			}
			this.m_dbLinkName = dbLinkName;
		}

		// Token: 0x040009BB RID: 2491
		public string m_schemaName;

		// Token: 0x040009BC RID: 2492
		public string m_tableName;

		// Token: 0x040009BD RID: 2493
		public string m_dbLinkName;
	}
}
