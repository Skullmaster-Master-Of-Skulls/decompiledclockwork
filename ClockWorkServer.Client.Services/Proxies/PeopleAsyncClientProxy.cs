using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000109 RID: 265
	public class PeopleAsyncClientProxy : WCFTokenBasedAsyncClientProxy<IPeopleAsync>, IPeopleAsync, IPeople, IService
	{
		// Token: 0x06000A4A RID: 2634 RVA: 0x0001A3F0 File Offset: 0x000185F0
		public PeopleAsyncClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0001A3FB File Offset: 0x000185FB
		public PeopleAsyncClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0001A408 File Offset: 0x00018608
		public IAsyncResult BeginFindUserGroupObjectBySearchString(FindUserGroupObjectBySearchStringReq req, AsyncCallback callback, object asyncState)
		{
			return this.WrapServiceMethod<IAsyncResult>(() => this.Proxy.BeginFindUserGroupObjectBySearchString(req, callback, asyncState));
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0001A450 File Offset: 0x00018650
		public FindUserGroupObjectBySearchStringResp EndFindUserGroupObjectBySearchString(IAsyncResult result)
		{
			return this.WrapServiceMethod<FindUserGroupObjectBySearchStringResp>(() => this.Proxy.EndFindUserGroupObjectBySearchString(result));
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0001A488 File Offset: 0x00018688
		public LoadGroupMembersResp LoadGroupMembers(LoadGroupMembersReq Req)
		{
			return this.WrapServiceMethod<LoadGroupMembersResp>(() => this.Proxy.LoadGroupMembers(Req));
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0001A4C0 File Offset: 0x000186C0
		public LoadGroupMembersResp LoadMultipleGroupMembers(LoadMultipleGroupMembersReq Req)
		{
			return this.WrapServiceMethod<LoadGroupMembersResp>(() => this.Proxy.LoadMultipleGroupMembers(Req));
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0001A4F8 File Offset: 0x000186F8
		public AddPersonResp AddPersonToCoreGroup(AddPersonReq Req)
		{
			return this.WrapServiceMethod<AddPersonResp>(() => this.Proxy.AddPersonToCoreGroup(Req));
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0001A530 File Offset: 0x00018730
		public StudentNameResp GetStudentName(StudentNameReq Req)
		{
			return this.WrapServiceMethod<StudentNameResp>(() => this.Proxy.GetStudentName(Req));
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0001A568 File Offset: 0x00018768
		public LoadGroupsResp LoadGroups(LoadGroupsReq Req)
		{
			return this.WrapServiceMethod<LoadGroupsResp>(() => this.Proxy.LoadGroups(Req));
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0001A5A0 File Offset: 0x000187A0
		public IsStudentsAccommodationsExpiredResp IsStudentsAccommodationsExpired(IsStudentsAccommodationsExpiredReq Req)
		{
			return this.WrapServiceMethod<IsStudentsAccommodationsExpiredResp>(() => this.Proxy.IsStudentsAccommodationsExpired(Req));
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0001A5D8 File Offset: 0x000187D8
		public LoadPersonResp LoadPerson(LoadPersonReq Req)
		{
			return this.WrapServiceMethod<LoadPersonResp>(() => this.Proxy.LoadPerson(Req));
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0001A610 File Offset: 0x00018810
		public CreateUserResp CreateUser(CreateUserReq Request)
		{
			return this.WrapServiceMethod<CreateUserResp>(() => this.Proxy.CreateUser(Request));
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0001A648 File Offset: 0x00018848
		public LoadAllRoomGroupsResp LoadAllRoomGroups(LoadAllRoomGroupsReq Request)
		{
			return this.WrapServiceMethod<LoadAllRoomGroupsResp>(() => this.Proxy.LoadAllRoomGroups(Request));
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0001A680 File Offset: 0x00018880
		public LoadPersonByStudentNumberResp LoadPersonByStudentNumber(LoadPersonByStudentNumberReq request)
		{
			return this.WrapServiceMethod<LoadPersonByStudentNumberResp>(() => this.Proxy.LoadPersonByStudentNumber(request));
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0001A6B8 File Offset: 0x000188B8
		public LoadResourcesResp LoadResources(LoadResourcesReq Request)
		{
			return this.WrapServiceMethod<LoadResourcesResp>(() => this.Proxy.LoadResources(Request));
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0001A6F0 File Offset: 0x000188F0
		public LoadRoomsResp LoadRooms(LoadRoomsReq Request)
		{
			return this.WrapServiceMethod<LoadRoomsResp>(() => this.Proxy.LoadRooms(Request));
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0001A728 File Offset: 0x00018928
		public LoadStaffResp LoadStaff(LoadStaffReq Request)
		{
			return this.WrapServiceMethod<LoadStaffResp>(() => this.Proxy.LoadStaff(Request));
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0001A760 File Offset: 0x00018960
		public LoadStudentsResp LoadStudents(LoadStudentsReq Request)
		{
			return this.WrapServiceMethod<LoadStudentsResp>(() => this.Proxy.LoadStudents(Request));
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0001A798 File Offset: 0x00018998
		public FindStudentBySearchStringResp FindStudentBySearchString(FindStudentBySearchStringReq Request)
		{
			return this.WrapServiceMethod<FindStudentBySearchStringResp>(() => this.Proxy.FindStudentBySearchString(Request));
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0001A7D0 File Offset: 0x000189D0
		public FindUserGroupObjectBySearchStringResp FindUserGroupObjectBySearchString(FindUserGroupObjectBySearchStringReq Request)
		{
			return this.WrapServiceMethod<FindUserGroupObjectBySearchStringResp>(() => this.Proxy.FindUserGroupObjectBySearchString(Request));
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0001A808 File Offset: 0x00018A08
		public CreateGroupResp CreateGroup(CreateGroupReq Request)
		{
			return this.WrapServiceMethod<CreateGroupResp>(() => this.Proxy.CreateGroup(Request));
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0001A840 File Offset: 0x00018A40
		public void DeleteGroup(DeleteGroupReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteGroup(Request);
			});
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0001A878 File Offset: 0x00018A78
		public void DeleteUser(DeleteUserReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteUser(Request);
			});
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0001A8B0 File Offset: 0x00018AB0
		public void UpdateGroup(UpdateGroupReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateGroup(Request);
			});
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0001A8E8 File Offset: 0x00018AE8
		public void UpdateUser(UpdateUserReq request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateUser(request);
			});
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0001A920 File Offset: 0x00018B20
		public UnDeleteUserResp UnDeleteUser(UnDeleteUserReq Request)
		{
			return this.WrapServiceMethod<UnDeleteUserResp>(() => this.Proxy.UnDeleteUser(Request));
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0001A958 File Offset: 0x00018B58
		public LoadPersonWithExtendedInfoResp LoadPersonWithExtendedInfo(LoadPersonWithExtendedInfoReq Request)
		{
			return this.WrapServiceMethod<LoadPersonWithExtendedInfoResp>(() => this.Proxy.LoadPersonWithExtendedInfo(Request));
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0001A990 File Offset: 0x00018B90
		public LoadPersonsByIdsResp LoadPersonsByIds(LoadPersonsByIdsReq Request)
		{
			return this.WrapServiceMethod<LoadPersonsByIdsResp>(() => this.Proxy.LoadPersonsByIds(Request));
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0001A9C8 File Offset: 0x00018BC8
		public GetTempStudentNumberResp GetTempStudentNumber(GetTempStudentNumberReq Request)
		{
			return this.WrapServiceMethod<GetTempStudentNumberResp>(() => this.Proxy.GetTempStudentNumber(Request));
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0001AA00 File Offset: 0x00018C00
		public LoadDeletedAccountsResp LoadDeletedAccounts(LoadDeletedAccountsReq Request)
		{
			return this.WrapServiceMethod<LoadDeletedAccountsResp>(() => this.Proxy.LoadDeletedAccounts(Request));
		}
	}
}
