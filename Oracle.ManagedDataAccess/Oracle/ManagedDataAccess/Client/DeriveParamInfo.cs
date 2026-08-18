using System;
using System.Data;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x02000054 RID: 84
	internal class DeriveParamInfo
	{
		// Token: 0x060003BD RID: 957 RVA: 0x0001DD74 File Offset: 0x0001BF74
		public DeriveParamInfo(int allocCount)
		{
			this.m_allocCount = allocCount;
			this.m_paramCount = allocCount;
			if (this.m_allocCount > 0)
			{
				this.m_arrayBindSize = new int[this.m_allocCount];
				this.m_oraCollType = new OracleCollectionType[this.m_allocCount];
				this.m_direction = new ParameterDirection[this.m_allocCount];
				this.m_oraDbType = new OracleDbType[this.m_allocCount];
				this.m_paramName = new string[this.m_allocCount];
				this.m_size = new int[this.m_allocCount];
			}
		}

		// Token: 0x04000579 RID: 1401
		public int m_paramCount;

		// Token: 0x0400057A RID: 1402
		public int m_allocCount;

		// Token: 0x0400057B RID: 1403
		public int[] m_arrayBindSize;

		// Token: 0x0400057C RID: 1404
		public OracleCollectionType[] m_oraCollType;

		// Token: 0x0400057D RID: 1405
		public ParameterDirection[] m_direction;

		// Token: 0x0400057E RID: 1406
		public OracleDbType[] m_oraDbType;

		// Token: 0x0400057F RID: 1407
		public string[] m_paramName;

		// Token: 0x04000580 RID: 1408
		public int[] m_size;

		// Token: 0x04000581 RID: 1409
		internal long m_lastUsedCount;
	}
}
