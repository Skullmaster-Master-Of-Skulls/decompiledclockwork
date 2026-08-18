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
	// Token: 0x0200006F RID: 111
	[ServiceContract(Name = "AdminGroupService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IAdminGroup : IService
	{
		// Token: 0x0600034F RID: 847
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllGroupsAndContainersResp LoadAllGroupsAndContainers(LoadAllGroupsAndContainersReq Request);

		// Token: 0x06000350 RID: 848
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AdminCreateGroupResp AdminCreateGroup(AdminCreateGroupReq Request);

		// Token: 0x06000351 RID: 849
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AdminUpdateGroupResp AdminUpdateGroup(AdminUpdateGroupReq request);

		// Token: 0x06000352 RID: 850
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AdminDeleteGroupResp AdminDeleteGroup(AdminDeleteGroupReq Request);

		// Token: 0x06000353 RID: 851
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateGroupOrderResp UpdateGroupOrder(UpdateGroupOrderReq Request);

		// Token: 0x06000354 RID: 852
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateGroupContainerTitleResp UpdateGroupContainerTitle(UpdateGroupContainerTitleReq Request);

		// Token: 0x06000355 RID: 853
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateGroupOrdersResp UpdateGroupOrders(UpdateGroupOrdersReq Request);

		// Token: 0x06000356 RID: 854
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AddMembersToGroupResp AddMembersToGroup(AddMembersToGroupReq Request);

		// Token: 0x06000357 RID: 855
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		RemoveMembersFromGroupResp RemoveMembersFromGroup(RemoveMembersFromGroupReq Request);

		// Token: 0x06000358 RID: 856
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateGroupsOrdersResp UpdateGroupsOrders(UpdateGroupsOrdersReq Request);

		// Token: 0x06000359 RID: 857
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadGroupMemberCountResp LoadGroupMemberCount(LoadGroupMemberCountReq Request);
	}
}
