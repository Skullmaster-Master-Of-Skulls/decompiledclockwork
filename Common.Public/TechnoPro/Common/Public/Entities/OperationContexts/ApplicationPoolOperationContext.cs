using System;

namespace TechnoPro.Common.Public.Entities.OperationContexts
{
	// Token: 0x0200026C RID: 620
	public class ApplicationPoolOperationContext : OperationContext
	{
		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x00018F60 File Offset: 0x00017160
		// (set) Token: 0x060012B4 RID: 4788 RVA: 0x00018F68 File Offset: 0x00017168
		public string ApplicationPoolName { get; set; }

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x00018F71 File Offset: 0x00017171
		// (set) Token: 0x060012B6 RID: 4790 RVA: 0x00018F79 File Offset: 0x00017179
		public string ManageRuntimeVersion { get; set; }
	}
}
