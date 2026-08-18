using System;
using System.Data;

namespace TechnoPro.Common.UI.ClientManager.UnivAccessThroughServer
{
	// Token: 0x02000002 RID: 2
	public class CommonParameter
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public CommonParameter()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public CommonParameter(string name, object val)
		{
			this.Name = name;
			this.Value = val;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000206E File Offset: 0x0000026E
		public CommonParameter(string name, object val, DbType dbType)
		{
			this.Name = name;
			this.Value = val;
			this.DbType = new DbType?(dbType);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002090 File Offset: 0x00000290
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002098 File Offset: 0x00000298
		public string Name { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020A1 File Offset: 0x000002A1
		// (set) Token: 0x06000007 RID: 7 RVA: 0x000020A9 File Offset: 0x000002A9
		public object Value { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020B2 File Offset: 0x000002B2
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000020BA File Offset: 0x000002BA
		public DbType? DbType { get; set; }
	}
}
