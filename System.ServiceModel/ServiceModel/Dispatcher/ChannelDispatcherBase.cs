using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200056A RID: 1386
	public abstract class ChannelDispatcherBase : CommunicationObject
	{
		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x060035FF RID: 13823
		public abstract ServiceHostBase Host { get; }

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06003600 RID: 13824
		public abstract IChannelListener Listener { get; }

		// Token: 0x06003601 RID: 13825 RVA: 0x000D1879 File Offset: 0x000CFA79
		internal void AttachInternal(ServiceHostBase host)
		{
			this.Attach(host);
		}

		// Token: 0x06003602 RID: 13826 RVA: 0x000D1882 File Offset: 0x000CFA82
		protected virtual void Attach(ServiceHostBase host)
		{
		}

		// Token: 0x06003603 RID: 13827 RVA: 0x000D1884 File Offset: 0x000CFA84
		internal void DetachInternal(ServiceHostBase host)
		{
			this.Detach(host);
		}

		// Token: 0x06003604 RID: 13828 RVA: 0x000D188D File Offset: 0x000CFA8D
		protected virtual void Detach(ServiceHostBase host)
		{
		}

		// Token: 0x06003605 RID: 13829 RVA: 0x000D188F File Offset: 0x000CFA8F
		public virtual void CloseInput()
		{
		}

		// Token: 0x06003606 RID: 13830 RVA: 0x000D1891 File Offset: 0x000CFA91
		internal virtual void CloseInput(TimeSpan timeout)
		{
			this.CloseInput();
		}
	}
}
