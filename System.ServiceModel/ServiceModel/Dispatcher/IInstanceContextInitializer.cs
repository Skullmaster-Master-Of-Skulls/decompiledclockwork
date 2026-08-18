using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000582 RID: 1410
	public interface IInstanceContextInitializer
	{
		// Token: 0x0600366E RID: 13934
		void Initialize(InstanceContext instanceContext, Message message);
	}
}
