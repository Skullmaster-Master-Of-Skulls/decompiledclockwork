using System;

namespace OracleInternal.Common
{
	// Token: 0x020000B7 RID: 183
	internal class SQLLocalParsePrimaryKeyInfo
	{
		// Token: 0x0600072E RID: 1838 RVA: 0x00042598 File Offset: 0x00040798
		private SQLLocalParsePrimaryKeyInfo()
		{
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x000425A0 File Offset: 0x000407A0
		internal SQLLocalParsePrimaryKeyInfo(int count)
		{
			this.m_columnMetaInfo = new ColumnLocalParsePrimaryKeyInfo[count];
			for (int i = 0; i < count; i++)
			{
				this.m_columnMetaInfo[i] = new ColumnLocalParsePrimaryKeyInfo();
			}
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x000425D8 File Offset: 0x000407D8
		internal void CopyPrimaryKeyInfoFrom(SQLLocalParsePrimaryKeyInfo destInfo)
		{
			this.bPkPresent = destInfo.bPkPresent;
			this.bRowidPresent = destInfo.bRowidPresent;
			if (destInfo.m_columnMetaInfo != null)
			{
				int num = destInfo.m_columnMetaInfo.Length;
				if (this.m_columnMetaInfo == null || this.m_columnMetaInfo.Length != num)
				{
					this.m_columnMetaInfo = new ColumnLocalParsePrimaryKeyInfo[num];
					for (int i = 0; i < num; i++)
					{
						this.m_columnMetaInfo[i] = new ColumnLocalParsePrimaryKeyInfo();
					}
				}
				for (int j = 0; j < num; j++)
				{
					this.m_columnMetaInfo[j].CopyPrimaryKeyInfoFrom(destInfo.m_columnMetaInfo[j]);
				}
			}
		}

		// Token: 0x04000982 RID: 2434
		internal string m_tableName;

		// Token: 0x04000983 RID: 2435
		internal string m_schemaName;

		// Token: 0x04000984 RID: 2436
		internal bool bPkPresent;

		// Token: 0x04000985 RID: 2437
		internal bool bRowidPresent;

		// Token: 0x04000986 RID: 2438
		internal ColumnLocalParsePrimaryKeyInfo[] m_columnMetaInfo;

		// Token: 0x04000987 RID: 2439
		internal bool bPkFetched;

		// Token: 0x04000988 RID: 2440
		internal bool bStmtParsed;

		// Token: 0x04000989 RID: 2441
		internal long m_lastUsedCount;

		// Token: 0x0400098A RID: 2442
		internal bool bIsPooled;

		// Token: 0x0400098B RID: 2443
		internal static readonly SQLLocalParsePrimaryKeyInfo Null = new SQLLocalParsePrimaryKeyInfo();
	}
}
