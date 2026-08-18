using System;
using System.ServiceModel;
using TechnoPro.Common.Public;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000033 RID: 51
	[ServiceContract(Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IClockWorkServerConnection : IService
	{
		// Token: 0x060001A4 RID: 420
		[OperationContract]
		int CheckConnection();
	}
}
