using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000739 RID: 1849
	[__DynamicallyInvokable]
	public abstract class ChannelFactoryBase<TChannel> : ChannelFactoryBase, IChannelFactory<TChannel>, IChannelFactory, ICommunicationObject
	{
		// Token: 0x0600464B RID: 17995 RVA: 0x001067DA File Offset: 0x001049DA
		[__DynamicallyInvokable]
		protected ChannelFactoryBase() : this(null)
		{
		}

		// Token: 0x0600464C RID: 17996 RVA: 0x001067E3 File Offset: 0x001049E3
		[__DynamicallyInvokable]
		protected ChannelFactoryBase(IDefaultCommunicationTimeouts timeouts) : base(timeouts)
		{
			this.channels = new CommunicationObjectManager<IChannel>(base.ThisLock);
		}

		// Token: 0x0600464D RID: 17997 RVA: 0x001067FD File Offset: 0x001049FD
		[__DynamicallyInvokable]
		public TChannel CreateChannel(EndpointAddress address)
		{
			if (address == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("address");
			}
			return this.InternalCreateChannel(address, address.Uri);
		}

		// Token: 0x0600464E RID: 17998 RVA: 0x00106825 File Offset: 0x00104A25
		[__DynamicallyInvokable]
		public TChannel CreateChannel(EndpointAddress address, Uri via)
		{
			if (address == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("address");
			}
			if (via == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("via");
			}
			return this.InternalCreateChannel(address, via);
		}

		// Token: 0x0600464F RID: 17999 RVA: 0x00106864 File Offset: 0x00104A64
		private TChannel InternalCreateChannel(EndpointAddress address, Uri via)
		{
			this.ValidateCreateChannel();
			TChannel tchannel = this.OnCreateChannel(address, via);
			bool flag = false;
			try
			{
				this.channels.Add((IChannel)((object)tchannel));
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					((IChannel)((object)tchannel)).Abort();
				}
			}
			return tchannel;
		}

		// Token: 0x06004650 RID: 18000
		[__DynamicallyInvokable]
		protected abstract TChannel OnCreateChannel(EndpointAddress address, Uri via);

		// Token: 0x06004651 RID: 18001 RVA: 0x001068C4 File Offset: 0x00104AC4
		[__DynamicallyInvokable]
		protected void ValidateCreateChannel()
		{
			base.ThrowIfDisposed();
			if (base.State != CommunicationState.Opened)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ChannelFactoryCannotBeUsedToCreateChannels", new object[]
				{
					base.GetType().ToString()
				})));
			}
		}

		// Token: 0x06004652 RID: 18002 RVA: 0x00106904 File Offset: 0x00104B04
		[__DynamicallyInvokable]
		protected override void OnAbort()
		{
			IChannel[] array = this.channels.ToArray();
			foreach (IChannel channel in array)
			{
				channel.Abort();
			}
			this.channels.Abort();
		}

		// Token: 0x06004653 RID: 18003 RVA: 0x00106944 File Offset: 0x00104B44
		[__DynamicallyInvokable]
		protected override void OnClose(TimeSpan timeout)
		{
			IChannel[] array = this.channels.ToArray();
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			foreach (IChannel channel in array)
			{
				channel.Close(timeoutHelper.RemainingTime());
			}
			this.channels.Close(timeoutHelper.RemainingTime());
		}

		// Token: 0x06004654 RID: 18004 RVA: 0x0010699C File Offset: 0x00104B9C
		[__DynamicallyInvokable]
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			ChainedBeginHandler begin = new ChainedBeginHandler(this.channels.BeginClose);
			ChainedEndHandler end = new ChainedEndHandler(this.channels.EndClose);
			ICommunicationObject[] objs = this.channels.ToArray();
			return new ChainedCloseAsyncResult(timeout, callback, state, begin, end, objs);
		}

		// Token: 0x06004655 RID: 18005 RVA: 0x001069E2 File Offset: 0x00104BE2
		[__DynamicallyInvokable]
		protected override void OnEndClose(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x04002D85 RID: 11653
		private CommunicationObjectManager<IChannel> channels;
	}
}
