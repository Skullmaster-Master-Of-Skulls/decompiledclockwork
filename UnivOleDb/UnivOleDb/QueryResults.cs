using System;
using System.Data;

namespace UnivOleDb
{
	// Token: 0x0200000C RID: 12
	[Serializable]
	public class QueryResults
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00005413 File Offset: 0x00004413
		public QueryResults(DataTable t)
		{
			this.table = t;
			this.exception = null;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000542B File Offset: 0x0000442B
		public QueryResults(Exception ex)
		{
			this.table = null;
			this.exception = ex;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00005444 File Offset: 0x00004444
		public DataTable Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000545C File Offset: 0x0000445C
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00005474 File Offset: 0x00004474
		public bool WasSuccessful
		{
			get
			{
				return this.table != null;
			}
		}

		// Token: 0x04000031 RID: 49
		private DataTable table;

		// Token: 0x04000032 RID: 50
		private Exception exception;
	}
}
