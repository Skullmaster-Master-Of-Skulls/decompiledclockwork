using System;
using System.ServiceModel;
using TechnoPro.Common.Public;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000080 RID: 128
	[ServiceContract(Name = "ServiceProviderMatchingService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IServiceProviderMatching : IService
	{
	}
}
