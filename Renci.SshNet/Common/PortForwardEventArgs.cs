using System;

namespace Renci.SshNet.Common
{
	// Token: 0x020000F9 RID: 249
	public class PortForwardEventArgs : EventArgs
	{
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x00024BA5 File Offset: 0x00022DA5
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x00024BAD File Offset: 0x00022DAD
		public string OriginatorHost { get; private set; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x00024BB6 File Offset: 0x00022DB6
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x00024BBE File Offset: 0x00022DBE
		public uint OriginatorPort { get; private set; }

		// Token: 0x06000ACD RID: 2765 RVA: 0x00024BC7 File Offset: 0x00022DC7
		internal PortForwardEventArgs(string host, uint port)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			port.ValidatePort("port");
			this.OriginatorHost = host;
			this.OriginatorPort = port;
		}
	}
}
