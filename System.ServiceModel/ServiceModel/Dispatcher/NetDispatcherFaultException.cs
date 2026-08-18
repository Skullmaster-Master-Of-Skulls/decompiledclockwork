using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000547 RID: 1351
	internal class NetDispatcherFaultException : FaultException
	{
		// Token: 0x06003367 RID: 13159 RVA: 0x000C6A79 File Offset: 0x000C4C79
		public NetDispatcherFaultException(string reason, FaultCode code, Exception innerException) : base(reason, code, "http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/dispatcher/fault", innerException)
		{
		}

		// Token: 0x06003368 RID: 13160 RVA: 0x000C6A89 File Offset: 0x000C4C89
		public NetDispatcherFaultException(FaultReason reason, FaultCode code, Exception innerException) : base(reason, code, "http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/dispatcher/fault", innerException)
		{
		}
	}
}
