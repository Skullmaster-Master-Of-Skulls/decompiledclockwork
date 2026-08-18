using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Description
{
	// Token: 0x020003E7 RID: 999
	[ServiceContract(ConfigurationName = "IMetadataExchange", Name = "IMetadataExchange", Namespace = "http://schemas.microsoft.com/2006/04/mex")]
	public interface IMetadataExchange
	{
		// Token: 0x060025AE RID: 9646
		[OperationContract(Action = "http://schemas.xmlsoap.org/ws/2004/09/transfer/Get", ReplyAction = "http://schemas.xmlsoap.org/ws/2004/09/transfer/GetResponse")]
		Message Get(Message request);

		// Token: 0x060025AF RID: 9647
		[OperationContract(Action = "http://schemas.xmlsoap.org/ws/2004/09/transfer/Get", ReplyAction = "http://schemas.xmlsoap.org/ws/2004/09/transfer/GetResponse", AsyncPattern = true)]
		IAsyncResult BeginGet(Message request, AsyncCallback callback, object state);

		// Token: 0x060025B0 RID: 9648
		Message EndGet(IAsyncResult result);
	}
}
