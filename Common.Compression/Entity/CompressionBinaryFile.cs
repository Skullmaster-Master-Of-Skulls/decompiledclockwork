using System;

namespace TechnoPro.Common.Compression.Entity
{
	// Token: 0x02000004 RID: 4
	public class CompressionBinaryFile
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000028BC File Offset: 0x00000ABC
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000028C4 File Offset: 0x00000AC4
		public string FileName { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000028CD File Offset: 0x00000ACD
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000028D5 File Offset: 0x00000AD5
		public byte[] FileBytes { get; set; }
	}
}
