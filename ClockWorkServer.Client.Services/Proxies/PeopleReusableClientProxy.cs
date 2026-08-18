using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000108 RID: 264
	public class PeopleReusableClientProxy : WCFTokenBasedReusableClientProxy<IPeople>, IPeople, IService
	{
		// Token: 0x06000A2E RID: 2606 RVA: 0x00019E28 File Offset: 0x00018028
		public PeopleReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00019E33 File Offset: 0x00018033
		public PeopleReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00019E40 File Offset: 0x00018040
		public LoadGroupMembersResp LoadGroupMembers(LoadGroupMembersReq Req)
		{
			return this.WrapServiceMethod<LoadGroupMembersResp>(() => this.Proxy.LoadGroupMembers(Req));
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00019E78 File Offset: 0x00018078
		public LoadGroupMembersResp LoadMultipleGroupMembers(LoadMultipleGroupMembersReq Req)
		{
			return this.WrapServiceMethod<LoadGroupMembersResp>(() => this.Proxy.LoadMultipleGroupMembers(Req));
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00019EB0 File Offset: 0x000180B0
		public AddPersonResp AddPersonToCoreGroup(AddPersonReq Req)
		{
			return this.WrapServiceMethod<AddPersonResp>(() => this.Proxy.AddPersonToCoreGroup(Req));
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00019EE8 File Offset: 0x000180E8
		public StudentNameResp GetStudentName(StudentNameReq Req)
		{
			return this.WrapServiceMethod<StudentNameResp>(() => this.Proxy.GetStudentName(Req));
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00019F20 File Offset: 0x00018120
		public LoadGroupsResp LoadGroups(LoadGroupsReq Req)
		{
			return this.WrapServiceMethod<LoadGroupsResp>(() => this.Proxy.LoadGroups(Req));
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00019F58 File Offset: 0x00018158
		public IsStudentsAccommodationsExpiredResp IsStudentsAccommodationsExpired(IsStudentsAccommodationsExpiredReq Req)
		{
			return this.WrapServiceMethod<IsStudentsAccommodationsExpiredResp>(() => this.Proxy.IsStudentsAccommodationsExpired(Req));
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00019F90 File Offset: 0x00018190
		public LoadPersonResp LoadPerson(LoadPersonReq Req)
		{
			return this.WrapServiceMethod<LoadPersonResp>(() => this.Proxy.LoadPerson(Req));
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00019FC8 File Offset: 0x000181C8
		public CreateUserResp CreateUser(CreateUserReq Request)
		{
			return this.WrapServiceMethod<CreateUserResp>(() => this.Proxy.CreateUser(Request));
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0001A000 File Offset: 0x00018200
		public LoadAllRoomGroupsResp LoadAllRoomGroups(LoadAllRoomGroupsReq Request)
		{
			return this.WrapServiceMethod<LoadAllRoomGroupsResp>(() => this.Proxy.LoadAllRoomGroups(Request));
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0001A038 File Offset: 0x00018238
		public LoadPersonByStudentNumberResp LoadPersonByStudentNumber(LoadPersonByStudentNumberReq request)
		{
			return this.WrapServiceMethod<LoadPersonByStudentNumberResp>(() => this.Proxy.LoadPersonByStudentNumber(request));
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0001A070 File Offset: 0x00018270
		public LoadResourcesResp LoadResources(LoadResourcesReq Request)
		{
			return this.WrapServiceMethod<LoadResourcesResp>(() => this.Proxy.LoadResources(Request));
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0001A0A8 File Offset: 0x000182A8
		public LoadRoomsResp LoadRooms(LoadRoomsReq Request)
		{
			return this.WrapServiceMethod<LoadRoomsResp>(() => this.Proxy.LoadRooms(Request));
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0001A0E0 File Offset: 0x000182E0
		public LoadStaffResp LoadStaff(LoadStaffReq Request)
		{
			return this.WrapServiceMethod<LoadStaffResp>(() => this.Proxy.LoadStaff(Request));
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0001A118 File Offset: 0x00018318
		public LoadStudentsResp LoadStudents(LoadStudentsReq Request)
		{
			return this.WrapServiceMethod<LoadStudentsResp>(() => this.Proxy.LoadStudents(Request));
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0001A150 File Offset: 0x00018350
		public FindStudentBySearchStringResp FindStudentBySearchString(FindStudentBySearchStringReq Request)
		{
			return this.WrapServiceMethod<FindStudentBySearchStringResp>(() => this.Proxy.FindStudentBySearchString(Request));
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0001A188 File Offset: 0x00018388
		public FindUserGroupObjectBySearchStringResp FindUserGroupObjectBySearchString(FindUserGroupObjectBySearchStringReq Request)
		{
			return this.WrapServiceMethod<FindUserGroupObjectBySearchStringResp>(() => this.Proxy.FindUserGroupObjectBySearchString(Request));
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0001A1C0 File Offset: 0x000183C0
		public CreateGroupResp CreateGroup(CreateGroupReq Request)
		{
			return this.WrapServiceMethod<CreateGroupResp>(() => this.Proxy.CreateGroup(Request));
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0001A1F8 File Offset: 0x000183F8
		public void DeleteGroup(DeleteGroupReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteGroup(Request);
			});
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0001A230 File Offset: 0x00018430
		public void DeleteUser(DeleteUserReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteUser(Request);
			});
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0001A268 File Offset: 0x00018468
		public void UpdateGroup(UpdateGroupReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateGroup(Request);
			});
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0001A2A0 File Offset: 0x000184A0
		public void UpdateUser(UpdateUserReq request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateUser(request);
			});
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0001A2D8 File Offset: 0x000184D8
		public UnDeleteUserResp UnDeleteUser(UnDeleteUserReq Request)
		{
			return this.WrapServiceMethod<UnDeleteUserResp>(() => this.Proxy.UnDeleteUser(Request));
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0001A310 File Offset: 0x00018510
		public LoadPersonWithExtendedInfoResp LoadPersonWithExtendedInfo(LoadPersonWithExtendedInfoReq Request)
		{
			return this.WrapServiceMethod<LoadPersonWithExtendedInfoResp>(() => this.Proxy.LoadPersonWithExtendedInfo(Request));
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0001A348 File Offset: 0x00018548
		public LoadPersonsByIdsResp LoadPersonsByIds(LoadPersonsByIdsReq Request)
		{
			return this.WrapServiceMethod<LoadPersonsByIdsResp>(() => this.Proxy.LoadPersonsByIds(Request));
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0001A380 File Offset: 0x00018580
		public GetTempStudentNumberResp GetTempStudentNumber(GetTempStudentNumberReq Request)
		{
			return this.WrapServiceMethod<GetTempStudentNumberResp>(() => this.Proxy.GetTempStudentNumber(Request));
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0001A3B8 File Offset: 0x000185B8
		public LoadDeletedAccountsResp LoadDeletedAccounts(LoadDeletedAccountsReq Request)
		{
			return this.WrapServiceMethod<LoadDeletedAccountsResp>(() => this.Proxy.LoadDeletedAccounts(Request));
		}
	}
}
