using System;

namespace System.ServiceModel
{
	// Token: 0x020000E9 RID: 233
	internal sealed class ChannelFactoryRef<TChannel> where TChannel : class
	{
		// Token: 0x060004D0 RID: 1232 RVA: 0x00017674 File Offset: 0x00015874
		public ChannelFactoryRef(ChannelFactory<TChannel> channelFactory)
		{
			this.channelFactory = channelFactory;
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0001768A File Offset: 0x0001588A
		public void AddRef()
		{
			this.refCount++;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0001769A File Offset: 0x0001589A
		public bool Release()
		{
			this.refCount--;
			return this.refCount == 0;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000176B5 File Offset: 0x000158B5
		public void Close(TimeSpan timeout)
		{
			this.channelFactory.Close(timeout);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x000176C3 File Offset: 0x000158C3
		public void Abort()
		{
			this.channelFactory.Abort();
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x000176D0 File Offset: 0x000158D0
		public ChannelFactory<TChannel> ChannelFactory
		{
			get
			{
				return this.channelFactory;
			}
		}

		// Token: 0x04000A1E RID: 2590
		private ChannelFactory<TChannel> channelFactory;

		// Token: 0x04000A1F RID: 2591
		private int refCount = 1;
	}
}
