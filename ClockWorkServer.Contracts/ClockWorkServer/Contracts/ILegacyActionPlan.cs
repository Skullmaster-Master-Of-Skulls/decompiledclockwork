using System;
using System.ServiceModel;
using TechnoPro.Common.Public;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200005E RID: 94
	[ServiceContract(Name = "LegacyActionPlanService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ILegacyActionPlan : IService
	{
	}
}
