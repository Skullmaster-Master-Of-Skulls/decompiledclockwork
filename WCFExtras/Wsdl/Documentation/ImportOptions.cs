using System;

namespace WCFExtras.Wsdl.Documentation
{
	// Token: 0x0200001A RID: 26
	internal class ImportOptions
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00004CB4 File Offset: 0x00002EB4
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00004CCB File Offset: 0x00002ECB
		public XmlCommentFormat Format { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00004CD4 File Offset: 0x00002ED4
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00004CEB File Offset: 0x00002EEB
		public bool WrapLongLines { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00004CF4 File Offset: 0x00002EF4
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00004D0B File Offset: 0x00002F0B
		public bool Documentable { get; set; }

		// Token: 0x04000021 RID: 33
		internal bool Initialized = false;
	}
}
