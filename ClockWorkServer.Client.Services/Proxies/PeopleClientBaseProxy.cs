using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200010A RID: 266
	internal class PeopleClientBaseProxy : ClientBase<IPeople>, IPeople, IService
	{
		// Token: 0x06000A68 RID: 2664 RVA: 0x0001AA38 File Offset: 0x00018C38
		public PeopleClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0001AA43 File Offset: 0x00018C43
		public PeopleClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0001AA50 File Offset: 0x00018C50
		public CreateUserResp CreateUser(CreateUserReq Request)
		{
			return base.Channel.CreateUser(Request);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0001AA70 File Offset: 0x00018C70
		public LoadGroupMembersResp LoadGroupMembers(LoadGroupMembersReq Req)
		{
			return base.Channel.LoadGroupMembers(Req);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0001AA90 File Offset: 0x00018C90
		public LoadGroupMembersResp LoadMultipleGroupMembers(LoadMultipleGroupMembersReq Req)
		{
			return base.Channel.LoadMultipleGroupMembers(Req);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0001AAB0 File Offset: 0x00018CB0
		public AddPersonResp AddPersonToCoreGroup(AddPersonReq Req)
		{
			return base.Channel.AddPersonToCoreGroup(Req);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0001AAD0 File Offset: 0x00018CD0
		public StudentNameResp GetStudentName(StudentNameReq Req)
		{
			return base.Channel.GetStudentName(Req);
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0001AAF0 File Offset: 0x00018CF0
		public LoadGroupsResp LoadGroups(LoadGroupsReq Req)
		{
			return base.Channel.LoadGroups(Req);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0001AB10 File Offset: 0x00018D10
		public IsStudentsAccommodationsExpiredResp IsStudentsAccommodationsExpired(IsStudentsAccommodationsExpiredReq Req)
		{
			return base.Channel.IsStudentsAccommodationsExpired(Req);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0001AB30 File Offset: 0x00018D30
		public LoadPersonResp LoadPerson(LoadPersonReq Req)
		{
			return base.Channel.LoadPerson(Req);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0001AB50 File Offset: 0x00018D50
		public LoadAllRoomGroupsResp LoadAllRoomGroups(LoadAllRoomGroupsReq Request)
		{
			return base.Channel.LoadAllRoomGroups(Request);
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0001AB70 File Offset: 0x00018D70
		public LoadPersonByStudentNumberResp LoadPersonByStudentNumber(LoadPersonByStudentNumberReq request)
		{
			return base.Channel.LoadPersonByStudentNumber(request);
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0001AB90 File Offset: 0x00018D90
		public LoadResourcesResp LoadResources(LoadResourcesReq Request)
		{
			return base.Channel.LoadResources(Request);
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0001ABB0 File Offset: 0x00018DB0
		public LoadRoomsResp LoadRooms(LoadRoomsReq Request)
		{
			return base.Channel.LoadRooms(Request);
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0001ABD0 File Offset: 0x00018DD0
		public LoadStaffResp LoadStaff(LoadStaffReq Request)
		{
			return base.Channel.LoadStaff(Request);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0001ABF0 File Offset: 0x00018DF0
		public LoadStudentsResp LoadStudents(LoadStudentsReq Request)
		{
			return base.Channel.LoadStudents(Request);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0001AC10 File Offset: 0x00018E10
		public FindStudentBySearchStringResp FindStudentBySearchString(FindStudentBySearchStringReq Request)
		{
			return base.Channel.FindStudentBySearchString(Request);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0001AC30 File Offset: 0x00018E30
		public FindUserGroupObjectBySearchStringResp FindUserGroupObjectBySearchString(FindUserGroupObjectBySearchStringReq Request)
		{
			return base.Channel.FindUserGroupObjectBySearchString(Request);
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0001AC50 File Offset: 0x00018E50
		public CreateGroupResp CreateGroup(CreateGroupReq Request)
		{
			return base.Channel.CreateGroup(Request);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0001AC6E File Offset: 0x00018E6E
		public void DeleteGroup(DeleteGroupReq Request)
		{
			base.Channel.DeleteGroup(Request);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0001AC7E File Offset: 0x00018E7E
		public void DeleteUser(DeleteUserReq Request)
		{
			base.Channel.DeleteUser(Request);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0001AC90 File Offset: 0x00018E90
		public UnDeleteUserResp UnDeleteUser(UnDeleteUserReq Request)
		{
			return base.Channel.UnDeleteUser(Request);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0001ACAE File Offset: 0x00018EAE
		public void UpdateGroup(UpdateGroupReq Request)
		{
			base.Channel.UpdateGroup(Request);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0001ACBE File Offset: 0x00018EBE
		public void UpdateUser(UpdateUserReq request)
		{
			base.Channel.UpdateUser(request);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0001ACD0 File Offset: 0x00018ED0
		public LoadPersonWithExtendedInfoResp LoadPersonWithExtendedInfo(LoadPersonWithExtendedInfoReq Request)
		{
			return base.Channel.LoadPersonWithExtendedInfo(Request);
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0001ACF0 File Offset: 0x00018EF0
		public LoadPersonsByIdsResp LoadPersonsByIds(LoadPersonsByIdsReq Request)
		{
			return base.Channel.LoadPersonsByIds(Request);
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0001AD10 File Offset: 0x00018F10
		public GetTempStudentNumberResp GetTempStudentNumber(GetTempStudentNumberReq Request)
		{
			return base.Channel.GetTempStudentNumber(Request);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0001AD30 File Offset: 0x00018F30
		public LoadDeletedAccountsResp LoadDeletedAccounts(LoadDeletedAccountsReq Request)
		{
			return base.Channel.LoadDeletedAccounts(Request);
		}
	}
}
