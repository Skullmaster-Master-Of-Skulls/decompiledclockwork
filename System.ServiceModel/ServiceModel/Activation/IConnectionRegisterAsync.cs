using System;
using System.Net;

namespace System.ServiceModel.Activation
{
	// Token: 0x020005C7 RID: 1479
	[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IConnectionDuplicator))]
	internal interface IConnectionRegisterAsync : IConnectionRegister
	{
		// Token: 0x0600399B RID: 14747
		[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IConnectionRegister/ValidateUriRoute", ReplyAction = "http://tempuri.org/IConnectionRegister/ValidateUriRouteResponse")]
		IAsyncResult BeginValidateUriRoute(Uri uri, IPAddress address, int port, AsyncCallback callback, object asyncState);

		// Token: 0x0600399C RID: 14748
		bool EndValidateUriRoute(IAsyncResult result);
	}
}
