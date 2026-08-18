using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200056C RID: 1388
	public interface IDispatchOperationSelector
	{
		// Token: 0x06003609 RID: 13833
		string SelectOperation(ref Message message);
	}
}
