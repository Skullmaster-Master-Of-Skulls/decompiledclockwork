using System;
using System.Web.Razor.Text;

namespace System.Web.Razor
{
	// Token: 0x02000004 RID: 4
	public class DocumentParseCompleteEventArgs : EventArgs
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000022FA File Offset: 0x000004FA
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002302 File Offset: 0x00000502
		public bool TreeStructureChanged { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000230B File Offset: 0x0000050B
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002313 File Offset: 0x00000513
		public GeneratorResults GeneratorResults { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000231C File Offset: 0x0000051C
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002324 File Offset: 0x00000524
		public TextChange SourceChange { get; set; }
	}
}
