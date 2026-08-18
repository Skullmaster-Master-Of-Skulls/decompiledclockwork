using System;

namespace Renci.SshNet.Common
{
	// Token: 0x020000FC RID: 252
	public class ScpUploadEventArgs : EventArgs
	{
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x00024C46 File Offset: 0x00022E46
		// (set) Token: 0x06000ADA RID: 2778 RVA: 0x00024C4E File Offset: 0x00022E4E
		public string Filename { get; private set; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x00024C57 File Offset: 0x00022E57
		// (set) Token: 0x06000ADC RID: 2780 RVA: 0x00024C5F File Offset: 0x00022E5F
		public long Size { get; private set; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x00024C68 File Offset: 0x00022E68
		// (set) Token: 0x06000ADE RID: 2782 RVA: 0x00024C70 File Offset: 0x00022E70
		public long Uploaded { get; private set; }

		// Token: 0x06000ADF RID: 2783 RVA: 0x00024C79 File Offset: 0x00022E79
		public ScpUploadEventArgs(string filename, long size, long uploaded)
		{
			this.Filename = filename;
			this.Size = size;
			this.Uploaded = uploaded;
		}
	}
}
