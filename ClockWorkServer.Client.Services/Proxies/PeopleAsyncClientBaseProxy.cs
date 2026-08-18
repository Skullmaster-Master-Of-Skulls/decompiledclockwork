using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200010B RID: 267
	internal class PeopleAsyncClientBaseProxy : ClientBase<IPeopleAsync>, IPeopleAsync, IPeople, IService
	{
		// Token: 0x06000A84 RID: 2692 RVA: 0x0001AD4E File Offset: 0x00018F4E
		public PeopleAsyncClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0001AD59 File Offset: 0x00018F59
		public PeopleAsyncClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0001AD68 File Offset: 0x00018F68
		public CreateUserResp CreateUser(CreateUserReq Request)
		{
			return base.Channel.CreateUser(Request);
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0001AD88 File Offset: 0x00018F88
		public LoadGroupMembersResp LoadGroupMembers(LoadGroupMembersReq Req)
		{
			return base.Channel.LoadGroupMembers(Req);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0001ADA8 File Offset: 0x00018FA8
		public LoadGroupMembersResp LoadMultipleGroupMembers(LoadMultipleGroupMembersReq Req)
		{
			return base.Channel.LoadMultipleGroupMembers(Req);
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0001ADC8 File Offset: 0x00018FC8
		public AddPersonResp AddPersonToCoreGroup(AddPersonReq Req)
		{
			return base.Channel.AddPersonToCoreGroup(Req);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0001ADE8 File Offset: 0x00018FE8
		public StudentNameResp GetStudentName(StudentNameReq Req)
		{
			return base.Channel.GetStudentName(Req);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0001AE08 File Offset: 0x00019008
		public LoadGroupsResp LoadGroups(LoadGroupsReq Req)
		{
			return base.Channel.LoadGroups(Req);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0001AE28 File Offset: 0x00019028
		public IsStudentsAccommodationsExpiredResp IsStudentsAccommodationsExpired(IsStudentsAccommodationsExpiredReq Req)
		{
			return base.Channel.IsStudentsAccommodationsExpired(Req);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0001AE48 File Offset: 0x00019048
		public LoadPersonResp LoadPerson(LoadPersonReq Req)
		{
			return base.Channel.LoadPerson(Req);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0001AE68 File Offset: 0x00019068
		public LoadAllRoomGroupsResp LoadAllRoomGroups(LoadAllRoomGroupsReq Request)
		{
			return base.Channel.LoadAllRoomGroups(Request);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0001AE88 File Offset: 0x00019088
		public LoadPersonByStudentNumberResp LoadPersonByStudentNumber(LoadPersonByStudentNumberReq request)
		{
			return base.Channel.LoadPersonByStudentNumber(request);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0001AEA8 File Offset: 0x000190A8
		public LoadResourcesResp LoadResources(LoadResourcesReq Request)
		{
			return base.Channel.LoadResources(Request);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0001AEC8 File Offset: 0x000190C8
		public LoadRoomsResp LoadRooms(LoadRoomsReq Request)
		{
			return base.Channel.LoadRooms(Request);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0001AEE8 File Offset: 0x000190E8
		public LoadStaffResp LoadStaff(LoadStaffReq Request)
		{
			return base.Channel.LoadStaff(Request);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0001AF08 File Offset: 0x00019108
		public LoadStudentsResp LoadStudents(LoadStudentsReq Request)
		{
			return base.Channel.LoadStudents(Request);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0001AF28 File Offset: 0x00019128
		public FindStudentBySearchStringResp FindStudentBySearchString(FindStudentBySearchStringReq Request)
		{
			return base.Channel.FindStudentBySearchString(Request);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0001AF48 File Offset: 0x00019148
		public FindUserGroupObjectBySearchStringResp FindUserGroupObjectBySearchString(FindUserGroupObjectBySearchStringReq Request)
		{
			return base.Channel.FindUserGroupObjectBySearchString(Request);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0001AF68 File Offset: 0x00019168
		public CreateGroupResp CreateGroup(CreateGroupReq Request)
		{
			return base.Channel.CreateGroup(Request);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0001AF86 File Offset: 0x00019186
		public void DeleteGroup(DeleteGroupReq Request)
		{
			base.Channel.DeleteGroup(Request);
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0001AF96 File Offset: 0x00019196
		public void DeleteUser(DeleteUserReq Request)
		{
			base.Channel.DeleteUser(Request);
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0001AFA8 File Offset: 0x000191A8
		public UnDeleteUserResp UnDeleteUser(UnDeleteUserReq Request)
		{
			return base.Channel.UnDeleteUser(Request);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0001AFC6 File Offset: 0x000191C6
		public void UpdateGroup(UpdateGroupReq Request)
		{
			base.Channel.UpdateGroup(Request);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0001AFD6 File Offset: 0x000191D6
		public void UpdateUser(UpdateUserReq request)
		{
			base.Channel.UpdateUser(request);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0001AFE8 File Offset: 0x000191E8
		public LoadPersonWithExtendedInfoResp LoadPersonWithExtendedInfo(LoadPersonWithExtendedInfoReq Request)
		{
			return base.Channel.LoadPersonWithExtendedInfo(Request);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0001B008 File Offset: 0x00019208
		public LoadPersonsByIdsResp LoadPersonsByIds(LoadPersonsByIdsReq Request)
		{
			return base.Channel.LoadPersonsByIds(Request);
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0001B028 File Offset: 0x00019228
		public IAsyncResult BeginFindUserGroupObjectBySearchString(FindUserGroupObjectBySearchStringReq req, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginFindUserGroupObjectBySearchString(req, callback, asyncState);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0001B048 File Offset: 0x00019248
		public FindUserGroupObjectBySearchStringResp EndFindUserGroupObjectBySearchString(IAsyncResult result)
		{
			return base.Channel.EndFindUserGroupObjectBySearchString(result);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0001B068 File Offset: 0x00019268
		public GetTempStudentNumberResp GetTempStudentNumber(GetTempStudentNumberReq Request)
		{
			return base.Channel.GetTempStudentNumber(Request);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0001B088 File Offset: 0x00019288
		public LoadDeletedAccountsResp LoadDeletedAccounts(LoadDeletedAccountsReq Request)
		{
			return base.Channel.LoadDeletedAccounts(Request);
		}
	}
}
