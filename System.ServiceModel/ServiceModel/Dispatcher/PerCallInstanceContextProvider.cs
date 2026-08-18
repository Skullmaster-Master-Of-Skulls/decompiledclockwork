using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000591 RID: 1425
	internal class PerCallInstanceContextProvider : InstanceContextProviderBase
	{
		// Token: 0x06003712 RID: 14098 RVA: 0x000D4268 File Offset: 0x000D2468
		internal PerCallInstanceContextProvider(DispatchRuntime dispatchRuntime) : base(dispatchRuntime)
		{
		}

		// Token: 0x06003713 RID: 14099 RVA: 0x000D4271 File Offset: 0x000D2471
		public override InstanceContext GetExistingInstanceContext(Message message, IContextChannel channel)
		{
			return null;
		}

		// Token: 0x06003714 RID: 14100 RVA: 0x000D4274 File Offset: 0x000D2474
		public override void InitializeInstanceContext(InstanceContext instanceContext, Message message, IContextChannel channel)
		{
		}

		// Token: 0x06003715 RID: 14101 RVA: 0x000D4276 File Offset: 0x000D2476
		public override bool IsIdle(InstanceContext instanceContext)
		{
			return true;
		}

		// Token: 0x06003716 RID: 14102 RVA: 0x000D4279 File Offset: 0x000D2479
		public override void NotifyIdle(InstanceContextIdleCallback callback, InstanceContext instanceContext)
		{
		}
	}
}
