using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000073 RID: 115
	[ServiceContract(Name = "PeopleGroupService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IPeopleGroup : IService
	{
		// Token: 0x06000365 RID: 869
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadUsersByGroupTitleResp LoadUsersByGroupTitle(LoadUsersByGroupTitleReq Request);
	}
}
