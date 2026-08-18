using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000592 RID: 1426
	internal class PerSessionInstanceContextProvider : InstanceContextProviderBase
	{
		// Token: 0x06003717 RID: 14103 RVA: 0x000D427B File Offset: 0x000D247B
		internal PerSessionInstanceContextProvider(DispatchRuntime dispatchRuntime) : base(dispatchRuntime)
		{
		}

		// Token: 0x06003718 RID: 14104 RVA: 0x000D4284 File Offset: 0x000D2484
		public override InstanceContext GetExistingInstanceContext(Message message, IContextChannel channel)
		{
			ServiceChannel serviceChannelFromProxy = base.GetServiceChannelFromProxy(channel);
			if (serviceChannelFromProxy == null)
			{
				return null;
			}
			return serviceChannelFromProxy.InstanceContext;
		}

		// Token: 0x06003719 RID: 14105 RVA: 0x000D42A4 File Offset: 0x000D24A4
		public override void InitializeInstanceContext(InstanceContext instanceContext, Message message, IContextChannel channel)
		{
			ServiceChannel serviceChannelFromProxy = base.GetServiceChannelFromProxy(channel);
			if (serviceChannelFromProxy != null && serviceChannelFromProxy.HasSession)
			{
				instanceContext.BindIncomingChannel(serviceChannelFromProxy);
			}
		}

		// Token: 0x0600371A RID: 14106 RVA: 0x000D42CB File Offset: 0x000D24CB
		public override bool IsIdle(InstanceContext instanceContext)
		{
			return true;
		}

		// Token: 0x0600371B RID: 14107 RVA: 0x000D42CE File Offset: 0x000D24CE
		public override void NotifyIdle(InstanceContextIdleCallback callback, InstanceContext instanceContext)
		{
		}
	}
}
