using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000050 RID: 80
	internal class ServiceDiscoveryInstanceContextProvider : IInstanceContextProvider, IInstanceProvider
	{
		// Token: 0x060003E3 RID: 995 RVA: 0x0000C67C File Offset: 0x0000A87C
		internal ServiceDiscoveryInstanceContextProvider(DiscoveryService discoveryService)
		{
			this.discoveryService = discoveryService;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00006B84 File Offset: 0x00004D84
		InstanceContext IInstanceContextProvider.GetExistingInstanceContext(Message message, IContextChannel channel)
		{
			return null;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x000030E1 File Offset: 0x000012E1
		void IInstanceContextProvider.InitializeInstanceContext(InstanceContext instanceContext, Message message, IContextChannel channel)
		{
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000C68B File Offset: 0x0000A88B
		bool IInstanceContextProvider.IsIdle(InstanceContext instanceContext)
		{
			return true;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000030E1 File Offset: 0x000012E1
		void IInstanceContextProvider.NotifyIdle(InstanceContextIdleCallback callback, InstanceContext instanceContext)
		{
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000C68E File Offset: 0x0000A88E
		object IInstanceProvider.GetInstance(InstanceContext instanceContext, Message message)
		{
			return this.discoveryService;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000C68E File Offset: 0x0000A88E
		object IInstanceProvider.GetInstance(InstanceContext instanceContext)
		{
			return this.discoveryService;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x000030E1 File Offset: 0x000012E1
		void IInstanceProvider.ReleaseInstance(InstanceContext instanceContext, object instance)
		{
		}

		// Token: 0x04000101 RID: 257
		private DiscoveryService discoveryService;
	}
}
