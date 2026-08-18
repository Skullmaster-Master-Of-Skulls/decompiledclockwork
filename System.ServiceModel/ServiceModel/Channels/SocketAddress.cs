using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A4E RID: 2638
	[Serializable]
	internal struct SocketAddress
	{
		// Token: 0x170018EE RID: 6382
		// (get) Token: 0x06006843 RID: 26691 RVA: 0x00184E48 File Offset: 0x00183048
		public IntPtr SockAddr
		{
			get
			{
				return this.sockAddr;
			}
		}

		// Token: 0x170018EF RID: 6383
		// (get) Token: 0x06006844 RID: 26692 RVA: 0x00184E50 File Offset: 0x00183050
		public int SockAddrLength
		{
			get
			{
				return this.sockAddrLength;
			}
		}

		// Token: 0x06006845 RID: 26693 RVA: 0x00184E58 File Offset: 0x00183058
		public void InitializeFromCriticalAllocHandleSocketAddress(CriticalAllocHandleSocketAddress sockAddr)
		{
			this.sockAddr = sockAddr;
			this.sockAddrLength = sockAddr.Size;
		}

		// Token: 0x04003BC5 RID: 15301
		private IntPtr sockAddr;

		// Token: 0x04003BC6 RID: 15302
		private int sockAddrLength;
	}
}
