using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200074E RID: 1870
	internal abstract class LayeredChannelFactory<TChannel> : ChannelFactoryBase<TChannel>
	{
		// Token: 0x0600476D RID: 18285 RVA: 0x001094A0 File Offset: 0x001076A0
		public LayeredChannelFactory(IDefaultCommunicationTimeouts timeouts, IChannelFactory innerChannelFactory) : base(timeouts)
		{
			this.innerChannelFactory = innerChannelFactory;
		}

		// Token: 0x17001219 RID: 4633
		// (get) Token: 0x0600476E RID: 18286 RVA: 0x001094B0 File Offset: 0x001076B0
		protected IChannelFactory InnerChannelFactory
		{
			get
			{
				return this.innerChannelFactory;
			}
		}

		// Token: 0x0600476F RID: 18287 RVA: 0x001094B8 File Offset: 0x001076B8
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(IChannelFactory<TChannel>))
			{
				return (T)((object)this);
			}
			T property = base.GetProperty<T>();
			if (property != null)
			{
				return property;
			}
			return this.innerChannelFactory.GetProperty<T>();
		}

		// Token: 0x06004770 RID: 18288 RVA: 0x00109503 File Offset: 0x00107703
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerChannelFactory.BeginOpen(timeout, callback, state);
		}

		// Token: 0x06004771 RID: 18289 RVA: 0x00109513 File Offset: 0x00107713
		protected override void OnEndOpen(IAsyncResult result)
		{
			this.innerChannelFactory.EndOpen(result);
		}

		// Token: 0x06004772 RID: 18290 RVA: 0x00109524 File Offset: 0x00107724
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChainedCloseAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginClose), new ChainedEndHandler(base.OnEndClose), new ICommunicationObject[]
			{
				this.innerChannelFactory
			});
		}

		// Token: 0x06004773 RID: 18291 RVA: 0x00109560 File Offset: 0x00107760
		protected override void OnEndClose(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x06004774 RID: 18292 RVA: 0x00109568 File Offset: 0x00107768
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.OnClose(timeoutHelper.RemainingTime());
			this.innerChannelFactory.Close(timeoutHelper.RemainingTime());
		}

		// Token: 0x06004775 RID: 18293 RVA: 0x0010959C File Offset: 0x0010779C
		protected override void OnOpen(TimeSpan timeout)
		{
			this.innerChannelFactory.Open(timeout);
		}

		// Token: 0x06004776 RID: 18294 RVA: 0x001095AA File Offset: 0x001077AA
		protected override void OnAbort()
		{
			base.OnAbort();
			this.innerChannelFactory.Abort();
		}

		// Token: 0x04002DAD RID: 11693
		private IChannelFactory innerChannelFactory;
	}
}
