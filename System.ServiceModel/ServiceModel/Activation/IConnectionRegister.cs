using System;
using System.Net;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Activation
{
	// Token: 0x020005C6 RID: 1478
	[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IConnectionDuplicator))]
	internal interface IConnectionRegister
	{
		// Token: 0x06003998 RID: 14744
		[OperationContract(IsOneWay = false, IsInitiating = true)]
		ListenerExceptionStatus Register(Version version, int pid, BaseUriWithWildcard path, int queueId, Guid token, string eventName);

		// Token: 0x06003999 RID: 14745
		[OperationContract]
		bool ValidateUriRoute(Uri uri, IPAddress address, int port);

		// Token: 0x0600399A RID: 14746
		[OperationContract]
		void Unregister();
	}
}
