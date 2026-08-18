using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200056F RID: 1391
	public interface IInstanceProvider
	{
		// Token: 0x0600360E RID: 13838
		object GetInstance(InstanceContext instanceContext);

		// Token: 0x0600360F RID: 13839
		object GetInstance(InstanceContext instanceContext, Message message);

		// Token: 0x06003610 RID: 13840
		void ReleaseInstance(InstanceContext instanceContext, object instance);
	}
}
