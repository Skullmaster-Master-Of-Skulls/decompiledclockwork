using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000560 RID: 1376
	internal interface IClientFaultFormatter
	{
		// Token: 0x06003599 RID: 13721
		FaultException Deserialize(MessageFault messageFault, string action);
	}
}
