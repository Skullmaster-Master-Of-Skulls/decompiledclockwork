using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000738 RID: 1848
	[__DynamicallyInvokable]
	public abstract class ChannelFactoryBase : ChannelManagerBase, IChannelFactory, ICommunicationObject
	{
		// Token: 0x0600463F RID: 17983 RVA: 0x001066C7 File Offset: 0x001048C7
		[__DynamicallyInvokable]
		protected ChannelFactoryBase()
		{
		}

		// Token: 0x06004640 RID: 17984 RVA: 0x001066FB File Offset: 0x001048FB
		[__DynamicallyInvokable]
		protected ChannelFactoryBase(IDefaultCommunicationTimeouts timeouts)
		{
			this.InitializeTimeouts(timeouts);
		}

		// Token: 0x170011EB RID: 4587
		// (get) Token: 0x06004641 RID: 17985 RVA: 0x00106736 File Offset: 0x00104936
		[__DynamicallyInvokable]
		protected override TimeSpan DefaultCloseTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				return this.closeTimeout;
			}
		}

		// Token: 0x170011EC RID: 4588
		// (get) Token: 0x06004642 RID: 17986 RVA: 0x0010673E File Offset: 0x0010493E
		[__DynamicallyInvokable]
		protected override TimeSpan DefaultOpenTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				return this.openTimeout;
			}
		}

		// Token: 0x170011ED RID: 4589
		// (get) Token: 0x06004643 RID: 17987 RVA: 0x00106746 File Offset: 0x00104946
		[__DynamicallyInvokable]
		protected override TimeSpan DefaultReceiveTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				return this.receiveTimeout;
			}
		}

		// Token: 0x170011EE RID: 4590
		// (get) Token: 0x06004644 RID: 17988 RVA: 0x0010674E File Offset: 0x0010494E
		[__DynamicallyInvokable]
		protected override TimeSpan DefaultSendTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				return this.sendTimeout;
			}
		}

		// Token: 0x06004645 RID: 17989 RVA: 0x00106758 File Offset: 0x00104958
		[__DynamicallyInvokable]
		public virtual T GetProperty<T>() where T : class
		{
			if (typeof(T) == typeof(IChannelFactory))
			{
				return (T)((object)this);
			}
			return default(T);
		}

		// Token: 0x06004646 RID: 17990 RVA: 0x00106790 File Offset: 0x00104990
		[__DynamicallyInvokable]
		protected override void OnAbort()
		{
		}

		// Token: 0x06004647 RID: 17991 RVA: 0x00106792 File Offset: 0x00104992
		[__DynamicallyInvokable]
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06004648 RID: 17992 RVA: 0x0010679B File Offset: 0x0010499B
		[__DynamicallyInvokable]
		protected override void OnClose(TimeSpan timeout)
		{
		}

		// Token: 0x06004649 RID: 17993 RVA: 0x0010679D File Offset: 0x0010499D
		[__DynamicallyInvokable]
		protected override void OnEndClose(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x0600464A RID: 17994 RVA: 0x001067A5 File Offset: 0x001049A5
		private void InitializeTimeouts(IDefaultCommunicationTimeouts timeouts)
		{
			if (timeouts != null)
			{
				this.closeTimeout = timeouts.CloseTimeout;
				this.openTimeout = timeouts.OpenTimeout;
				this.receiveTimeout = timeouts.ReceiveTimeout;
				this.sendTimeout = timeouts.SendTimeout;
			}
		}

		// Token: 0x04002D81 RID: 11649
		private TimeSpan closeTimeout = ServiceDefaults.CloseTimeout;

		// Token: 0x04002D82 RID: 11650
		private TimeSpan openTimeout = ServiceDefaults.OpenTimeout;

		// Token: 0x04002D83 RID: 11651
		private TimeSpan receiveTimeout = ServiceDefaults.ReceiveTimeout;

		// Token: 0x04002D84 RID: 11652
		private TimeSpan sendTimeout = ServiceDefaults.SendTimeout;
	}
}
