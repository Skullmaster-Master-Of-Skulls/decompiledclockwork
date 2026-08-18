using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000568 RID: 1384
	public interface ICallContextInitializer
	{
		// Token: 0x060035E9 RID: 13801
		object BeforeInvoke(InstanceContext instanceContext, IClientChannel channel, Message message);

		// Token: 0x060035EA RID: 13802
		void AfterInvoke(object correlationState);
	}
}
