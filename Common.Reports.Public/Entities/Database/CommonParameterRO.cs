using System;
using System.Data;

namespace TechnoPro.Common.Reports.Public.Entities.Database
{
	// Token: 0x02000007 RID: 7
	public class CommonParameterRO
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000020B8 File Offset: 0x000002B8
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000020C0 File Offset: 0x000002C0
		public string Name { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000020C9 File Offset: 0x000002C9
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000020D1 File Offset: 0x000002D1
		public object Value { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000020DA File Offset: 0x000002DA
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000020E2 File Offset: 0x000002E2
		public DbType? DbType { get; set; }
	}
}
