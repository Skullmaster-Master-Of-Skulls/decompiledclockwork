using System;
using System.Data;

namespace TechnoPro.Common.Public.Entities.UnivDataAccess
{
	// Token: 0x02000155 RID: 341
	public class CommonParameter
	{
		// Token: 0x06000825 RID: 2085 RVA: 0x0000D55A File Offset: 0x0000B75A
		public CommonParameter()
		{
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00011850 File Offset: 0x0000FA50
		public CommonParameter(string name, object val)
		{
			this.Name = name;
			this.Value = val;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0001186A File Offset: 0x0000FA6A
		public CommonParameter(string name, object val, DbType dbType)
		{
			this.Name = name;
			this.Value = val;
			this.DbType = new DbType?(dbType);
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x00011891 File Offset: 0x0000FA91
		// (set) Token: 0x06000829 RID: 2089 RVA: 0x00011899 File Offset: 0x0000FA99
		public string Name { get; set; }

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x000118A2 File Offset: 0x0000FAA2
		// (set) Token: 0x0600082B RID: 2091 RVA: 0x000118AA File Offset: 0x0000FAAA
		public object Value { get; set; }

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x000118B3 File Offset: 0x0000FAB3
		// (set) Token: 0x0600082D RID: 2093 RVA: 0x000118BB File Offset: 0x0000FABB
		public DbType? DbType { get; set; }
	}
}
