using System;

namespace TechnoPro.Common.Public.Entities.FTP
{
	// Token: 0x02000332 RID: 818
	public class FtpFileInfo
	{
		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x060019A0 RID: 6560 RVA: 0x0001E07E File Offset: 0x0001C27E
		// (set) Token: 0x060019A1 RID: 6561 RVA: 0x0001E086 File Offset: 0x0001C286
		public string Filename { get; set; }

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x060019A2 RID: 6562 RVA: 0x0001E08F File Offset: 0x0001C28F
		// (set) Token: 0x060019A3 RID: 6563 RVA: 0x0001E097 File Offset: 0x0001C297
		public int SizeinBytes { get; set; }

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x060019A4 RID: 6564 RVA: 0x0001E0A0 File Offset: 0x0001C2A0
		// (set) Token: 0x060019A5 RID: 6565 RVA: 0x0001E0A8 File Offset: 0x0001C2A8
		public bool IsDirectory { get; set; }

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x060019A6 RID: 6566 RVA: 0x0001E0B1 File Offset: 0x0001C2B1
		// (set) Token: 0x060019A7 RID: 6567 RVA: 0x0001E0B9 File Offset: 0x0001C2B9
		public DateTime LastModifiedTime { get; set; }

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x060019A8 RID: 6568 RVA: 0x0001E0C2 File Offset: 0x0001C2C2
		// (set) Token: 0x060019A9 RID: 6569 RVA: 0x0001E0CA File Offset: 0x0001C2CA
		public string Folder { get; set; }
	}
}
