using System;
using System.ServiceModel;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000CA RID: 202
	[ServiceContract(Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IConnectivity
	{
		// Token: 0x06000592 RID: 1426
		[OperationContract]
		int CheckConnectivity();
	}
}
