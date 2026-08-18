using System;

namespace TechnoPro.Common.DynamicCompiler
{
	// Token: 0x02000008 RID: 8
	public class CustomCompileMessage
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002E14 File Offset: 0x00001014
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002E1C File Offset: 0x0000101C
		public int LineNumber { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002E25 File Offset: 0x00001025
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002E2D File Offset: 0x0000102D
		public int ColumnNumber { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002E36 File Offset: 0x00001036
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002E3E File Offset: 0x0000103E
		public string Title { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002E47 File Offset: 0x00001047
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002E4F File Offset: 0x0000104F
		public string Filename { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002E58 File Offset: 0x00001058
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002E60 File Offset: 0x00001060
		public eCustomCompileMessageType MessageType { get; set; }
	}
}
