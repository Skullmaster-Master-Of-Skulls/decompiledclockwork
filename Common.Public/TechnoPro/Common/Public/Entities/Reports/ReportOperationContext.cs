using System;

namespace TechnoPro.Common.Public.Entities.Reports
{
	// Token: 0x02000218 RID: 536
	[Obsolete("Use OperationContext - ReportOperationContext included only for legacy custom reports as a wrapper around OperationContext")]
	public class ReportOperationContext : OperationContext
	{
		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001058 RID: 4184 RVA: 0x000176FC File Offset: 0x000158FC
		// (set) Token: 0x06001059 RID: 4185 RVA: 0x00017704 File Offset: 0x00015904
		public string BinPath { get; set; }
	}
}
