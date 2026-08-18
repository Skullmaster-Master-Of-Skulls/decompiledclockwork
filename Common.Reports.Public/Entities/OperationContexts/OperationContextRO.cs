using System;

namespace TechnoPro.Common.Reports.Public.Entities.OperationContexts
{
	// Token: 0x02000006 RID: 6
	public class OperationContextRO
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000206A File Offset: 0x0000026A
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002072 File Offset: 0x00000272
		public virtual int WhoAmI { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000207B File Offset: 0x0000027B
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002083 File Offset: 0x00000283
		public string TenantId { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002094 File Offset: 0x00000294
		public ApplicationContextRO AppContext { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000209D File Offset: 0x0000029D
		public static OperationContextRO Empty
		{
			get
			{
				return OperationContextRO._empty;
			}
		}

		// Token: 0x04000028 RID: 40
		protected static OperationContextRO _empty = new OperationContextRO
		{
			WhoAmI = 0
		};
	}
}
