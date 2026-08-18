using System;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020006D5 RID: 1749
	internal class DispatchChannelSinkProvider : IServerChannelSinkProvider
	{
		// Token: 0x06003F06 RID: 16134 RVA: 0x000D7FD2 File Offset: 0x000D6FD2
		internal DispatchChannelSinkProvider()
		{
		}

		// Token: 0x06003F07 RID: 16135 RVA: 0x000D7FDA File Offset: 0x000D6FDA
		public void GetChannelData(IChannelDataStore channelData)
		{
		}

		// Token: 0x06003F08 RID: 16136 RVA: 0x000D7FDC File Offset: 0x000D6FDC
		public IServerChannelSink CreateSink(IChannelReceiver channel)
		{
			return new DispatchChannelSink();
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06003F09 RID: 16137 RVA: 0x000D7FE3 File Offset: 0x000D6FE3
		// (set) Token: 0x06003F0A RID: 16138 RVA: 0x000D7FE6 File Offset: 0x000D6FE6
		public IServerChannelSinkProvider Next
		{
			get
			{
				return null;
			}
			set
			{
				throw new NotSupportedException();
			}
		}
	}
}
