using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000A0 RID: 160
	[ServiceContract(Name = "PeopleService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IPeople : IService
	{
		// Token: 0x0600047D RID: 1149
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		FindStudentBySearchStringResp FindStudentBySearchString(FindStudentBySearchStringReq Request);

		// Token: 0x0600047E RID: 1150
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadPersonByStudentNumberResp LoadPersonByStudentNumber(LoadPersonByStudentNumberReq request);

		// Token: 0x0600047F RID: 1151
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AddPersonResp AddPersonToCoreGroup(AddPersonReq request);

		// Token: 0x06000480 RID: 1152
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		StudentNameResp GetStudentName(StudentNameReq request);

		// Token: 0x06000481 RID: 1153
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadGroupMembersResp LoadGroupMembers(LoadGroupMembersReq request);

		// Token: 0x06000482 RID: 1154
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadGroupMembersResp LoadMultipleGroupMembers(LoadMultipleGroupMembersReq request);

		// Token: 0x06000483 RID: 1155
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadGroupsResp LoadGroups(LoadGroupsReq request);

		// Token: 0x06000484 RID: 1156
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		IsStudentsAccommodationsExpiredResp IsStudentsAccommodationsExpired(IsStudentsAccommodationsExpiredReq request);

		// Token: 0x06000485 RID: 1157
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadPersonResp LoadPerson(LoadPersonReq request);

		// Token: 0x06000486 RID: 1158
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateUserResp CreateUser(CreateUserReq Request);

		// Token: 0x06000487 RID: 1159
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllRoomGroupsResp LoadAllRoomGroups(LoadAllRoomGroupsReq Request);

		// Token: 0x06000488 RID: 1160
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadStaffResp LoadStaff(LoadStaffReq Request);

		// Token: 0x06000489 RID: 1161
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadStudentsResp LoadStudents(LoadStudentsReq Request);

		// Token: 0x0600048A RID: 1162
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadRoomsResp LoadRooms(LoadRoomsReq Request);

		// Token: 0x0600048B RID: 1163
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadResourcesResp LoadResources(LoadResourcesReq Request);

		// Token: 0x0600048C RID: 1164
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		FindUserGroupObjectBySearchStringResp FindUserGroupObjectBySearchString(FindUserGroupObjectBySearchStringReq Request);

		// Token: 0x0600048D RID: 1165
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateUser(UpdateUserReq request);

		// Token: 0x0600048E RID: 1166
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateGroupResp CreateGroup(CreateGroupReq Request);

		// Token: 0x0600048F RID: 1167
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateGroup(UpdateGroupReq Request);

		// Token: 0x06000490 RID: 1168
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteGroup(DeleteGroupReq Request);

		// Token: 0x06000491 RID: 1169
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteUser(DeleteUserReq Request);

		// Token: 0x06000492 RID: 1170
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UnDeleteUserResp UnDeleteUser(UnDeleteUserReq Request);

		// Token: 0x06000493 RID: 1171
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadPersonWithExtendedInfoResp LoadPersonWithExtendedInfo(LoadPersonWithExtendedInfoReq Request);

		// Token: 0x06000494 RID: 1172
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadPersonsByIdsResp LoadPersonsByIds(LoadPersonsByIdsReq Request);

		// Token: 0x06000495 RID: 1173
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetTempStudentNumberResp GetTempStudentNumber(GetTempStudentNumberReq Request);

		// Token: 0x06000496 RID: 1174
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadDeletedAccountsResp LoadDeletedAccounts(LoadDeletedAccountsReq Request);
	}
}
