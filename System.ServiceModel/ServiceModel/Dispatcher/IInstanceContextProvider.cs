using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000580 RID: 1408
	public interface IInstanceContextProvider
	{
		// Token: 0x0600365F RID: 13919
		InstanceContext GetExistingInstanceContext(Message message, IContextChannel channel);

		// Token: 0x06003660 RID: 13920
		void InitializeInstanceContext(InstanceContext instanceContext, Message message, IContextChannel channel);

		// Token: 0x06003661 RID: 13921
		bool IsIdle(InstanceContext instanceContext);

		// Token: 0x06003662 RID: 13922
		void NotifyIdle(InstanceContextIdleCallback callback, InstanceContext instanceContext);
	}
}
