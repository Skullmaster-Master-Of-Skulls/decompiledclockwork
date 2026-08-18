using System;

namespace WCFExtrasPlus.Wsdl.Documentation
{
	// Token: 0x0200001D RID: 29
	internal class ImportOptions
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004BAC File Offset: 0x00002DAC
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00004BB4 File Offset: 0x00002DB4
		public XmlCommentFormat Format { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00004BBD File Offset: 0x00002DBD
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00004BC5 File Offset: 0x00002DC5
		public bool WrapLongLines { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004BCE File Offset: 0x00002DCE
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00004BD6 File Offset: 0x00002DD6
		public bool Documentable { get; set; }

		// Token: 0x0400002B RID: 43
		internal bool Initialized;
	}
}
