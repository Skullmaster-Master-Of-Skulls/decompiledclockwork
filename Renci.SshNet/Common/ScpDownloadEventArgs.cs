using System;

namespace Renci.SshNet.Common
{
	// Token: 0x020000FA RID: 250
	public class ScpDownloadEventArgs : EventArgs
	{
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x00024BF6 File Offset: 0x00022DF6
		// (set) Token: 0x06000ACF RID: 2767 RVA: 0x00024BFE File Offset: 0x00022DFE
		public string Filename { get; private set; }

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x00024C07 File Offset: 0x00022E07
		// (set) Token: 0x06000AD1 RID: 2769 RVA: 0x00024C0F File Offset: 0x00022E0F
		public long Size { get; private set; }

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x00024C18 File Offset: 0x00022E18
		// (set) Token: 0x06000AD3 RID: 2771 RVA: 0x00024C20 File Offset: 0x00022E20
		public long Downloaded { get; private set; }

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00024C29 File Offset: 0x00022E29
		public ScpDownloadEventArgs(string filename, long size, long downloaded)
		{
			this.Filename = filename;
			this.Size = size;
			this.Downloaded = downloaded;
		}
	}
}
