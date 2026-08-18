using System;

namespace Renci.SshNet.Common
{
	// Token: 0x02000100 RID: 256
	public class ShellDataEventArgs : EventArgs
	{
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x00024DA0 File Offset: 0x00022FA0
		// (set) Token: 0x06000AEE RID: 2798 RVA: 0x00024DA8 File Offset: 0x00022FA8
		public byte[] Data { get; private set; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x00024DB1 File Offset: 0x00022FB1
		// (set) Token: 0x06000AF0 RID: 2800 RVA: 0x00024DB9 File Offset: 0x00022FB9
		public string Line { get; private set; }

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00024DC2 File Offset: 0x00022FC2
		public ShellDataEventArgs(byte[] data)
		{
			this.Data = data;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00024DD1 File Offset: 0x00022FD1
		public ShellDataEventArgs(string line)
		{
			this.Line = line;
		}
	}
}
