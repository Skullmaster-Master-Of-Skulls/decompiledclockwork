using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000561 RID: 1377
	internal interface IDispatchFaultFormatter
	{
		// Token: 0x0600359A RID: 13722
		MessageFault Serialize(FaultException faultException, out string action);
	}
}
