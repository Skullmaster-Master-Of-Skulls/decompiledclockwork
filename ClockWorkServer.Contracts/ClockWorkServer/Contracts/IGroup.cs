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
	// Token: 0x02000072 RID: 114
	[ServiceContract(Name = "GroupService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IGroup : IService
	{
		// Token: 0x0600035F RID: 863
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadGroupByTitleResp LoadGroupByTitle(LoadGroupByTitleReq Request);

		// Token: 0x06000360 RID: 864
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateGroupByTitleResp CreateGroupByTitle(CreateGroupByTitleReq Request);

		// Token: 0x06000361 RID: 865
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadGroupByIdResp LoadGroupById(LoadGroupByIdReq Request);

		// Token: 0x06000362 RID: 866
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllowedGroupsResp LoadAllowedGroups(LoadAllowedGroupsReq Request);

		// Token: 0x06000363 RID: 867
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllGroupContainersResp LoadAllGroupContainers(LoadAllGroupContainersReq Request);

		// Token: 0x06000364 RID: 868
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllGroupForEditsResp LoadAllGroupForEdits(LoadAllGroupForEditsReq Request);
	}
}
