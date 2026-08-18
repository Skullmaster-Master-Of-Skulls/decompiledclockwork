using System;
using System.Data;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000150 RID: 336
	internal class DeriveParamInfo
	{
		// Token: 0x06000D4F RID: 3407 RVA: 0x0008A578 File Offset: 0x00089578
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
				this.m_typeName = new string[this.m_allocCount];
			}
		}

		// Token: 0x04000A99 RID: 2713
		public int m_paramCount;

		// Token: 0x04000A9A RID: 2714
		public int m_allocCount;

		// Token: 0x04000A9B RID: 2715
		public int[] m_arrayBindSize;

		// Token: 0x04000A9C RID: 2716
		public OracleCollectionType[] m_oraCollType;

		// Token: 0x04000A9D RID: 2717
		public ParameterDirection[] m_direction;

		// Token: 0x04000A9E RID: 2718
		public OracleDbType[] m_oraDbType;

		// Token: 0x04000A9F RID: 2719
		public string[] m_paramName;

		// Token: 0x04000AA0 RID: 2720
		public int[] m_size;

		// Token: 0x04000AA1 RID: 2721
		public string[] m_typeName;

		// Token: 0x04000AA2 RID: 2722
		public static Pooler m_pooler = new Pooler(10, 50);
	}
}
