using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Client.Services.Proxies;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Exceptions;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.People
{
	// Token: 0x0200002E RID: 46
	public class PeopleClientManager : IPeopleClientManager, IWebService, IPeopleAsync, IPeople, IService, IDisposable
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00007D6D File Offset: 0x00005F6D
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00007D75 File Offset: 0x00005F75
		private IPeopleAsync PeopleAsyncProxy { get; set; }

		// Token: 0x0600017B RID: 379 RVA: 0x00007D80 File Offset: 0x00005F80
		public AddPersonResp AddPersonToCoreGroup(AddPersonReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<AddPersonReq>(Request);
			IPeople clientInstance = ClientServiceFactory.GetClientInstance<IPeople>();
			return clientInstance.AddPersonToCoreGroup(Request);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007DAC File Offset: 0x00005FAC
		public CreateUserResp CreateUser(CreateUserReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<CreateUserReq>(Request);
			IPeople clientInstance = ClientServiceFactory.GetClientInstance<IPeople>();
			return clientInstance.CreateUser(Request);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00007DD8 File Offset: 0x00005FD8
		public StudentNameResp GetStudentName(StudentNameReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<StudentNameReq>(Request);
			IPeople clientInstance = ClientServiceFactory.GetClientInstance<IPeople>();
			return clientInstance.GetStudentName(Request);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007E04 File Offset: 0x00006004
		public IsStudentsAccommodationsExpiredResp IsStudentsAccommodationsExpired(IsStudentsAccommodationsExpiredReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<IsStudentsAccommodationsExpiredReq>(Request);
			IPeople clientInstance = ClientServiceFactory.GetClientInstance<IPeople>();
			return clientInstance.IsStudentsAccommodationsExpired(Request);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00007E30 File Offset: 0x00006030
		public LoadAllRoomGroupsResp LoadAllRoomGroups(LoadAllRoomGroupsReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<LoadAllRoomGroupsReq>(Request);
			IPeople clientInstance = ClientServiceFactory.GetClientInstance<IPeople>();
			return clientInstance.LoadAllRoomGroups(Request);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00007E5C File Offset: 0x0000605C
		public LoadGroupMembersResp LoadMultipleGroupMembers(LoadMultipleGroupMembersReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<LoadMultipleGroupMembersReq>(Request);
			IPeople clientInstance = ClientServiceFactory.GetClientInstance<IPeople>();
			return clientInstance.LoadMultipleGroupMembers(Request);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007E88 File Offset: 0x00006088
		public LoadGroupMembersResp LoadGroupMembers(LoadGroupMembersReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<LoadGroupMembersReq>(Request);
			IPeople clientInstance = ClientServiceFactory.GetClientInstance<IPeople>();
			return clientInstance.LoadGroupMembers(Request);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00007EB4 File Offset: 0x000060B4
		public LoadGroupsResp LoadGroups(LoadGroupsReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<LoadGroupsReq>(Request);
			IPeople clientInstance = ClientServiceFactory.GetClientInstance<IPeople>();
			return clientInstance.LoadGroups(Request);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00007EE0 File Offset: 0x000060E0
		public LoadPersonResp LoadPerson(LoadPersonReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<LoadPersonReq>(Request);
			IPeople clientInstance = ClientServiceFactory.GetClientInstance<IPeople>();
			return clientInstance.LoadPerson(Request);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00007F0C File Offset: 0x0000610C
		public LoadPersonByStudentNumberResp LoadPersonByStudentNumber(LoadPersonByStudentNumberReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<LoadPersonByStudentNumberReq>(Request);
			IPeople clientInstance = ClientServiceFactory.GetClientInstance<IPeople>();
			return clientInstance.LoadPersonByStudentNumber(Request);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00007F38 File Offset: 0x00006138
		public LoadResourcesResp LoadResources(LoadResourcesReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<LoadResourcesReq>(Request);
			IPeople clientInstance = ClientServiceFactory.GetClientInstance<IPeople>();
			return clientInstance.LoadResources(Request);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007F64 File Offset: 0x00006164
		public LoadRoomsResp LoadRooms(LoadRoomsReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<LoadRoomsReq>(Request);
			IPeople clientInstance = ClientServiceFactory.GetClientInstance<IPeople>();
			return clientInstance.LoadRooms(Request);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007F90 File Offset: 0x00006190
		public LoadStaffResp LoadStaff(LoadStaffReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<LoadStaffReq>(Request);
			IPeople clientInstance = ClientServiceFactory.GetClientInstance<IPeople>();
			return clientInstance.LoadStaff(Request);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007FBC File Offset: 0x000061BC
		public LoadStudentsResp LoadStudents(LoadStudentsReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<LoadStudentsReq>(Request);
			IPeople clientInstance = ClientServiceFactory.GetClientInstance<IPeople>();
			return clientInstance.LoadStudents(Request);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00007FE8 File Offset: 0x000061E8
		public FindStudentBySearchStringResp FindStudentBySearchString(FindStudentBySearchStringReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<FindStudentBySearchStringReq>(Request);
			IPeople clientInstance = ClientServiceFactory.GetClientInstance<IPeople>();
			return clientInstance.FindStudentBySearchString(Request);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00008014 File Offset: 0x00006214
		public FindUserGroupObjectBySearchStringResp FindUserGroupObjectBySearchString(FindUserGroupObjectBySearchStringReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<FindUserGroupObjectBySearchStringReq>(Request);
			IPeople clientInstance = ClientServiceFactory.GetClientInstance<IPeople>();
			return clientInstance.FindUserGroupObjectBySearchString(Request);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00008040 File Offset: 0x00006240
		public FindUserGroupObjectBySearchStringResp FindUserGroupObjectBySearchString(string searchString, int startIndex, int maxResultsCount, params eUserGroupObjectType[] userGroupObjectTypes)
		{
			FindUserGroupObjectBySearchStringReq findUserGroupObjectBySearchStringReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<FindUserGroupObjectBySearchStringReq>();
			findUserGroupObjectBySearchStringReq.SearchString = searchString;
			findUserGroupObjectBySearchStringReq.MaxResultsCount = maxResultsCount;
			findUserGroupObjectBySearchStringReq.ObjectTypesToExclude = userGroupObjectTypes;
			findUserGroupObjectBySearchStringReq.StartIndex = startIndex;
			return this.FindUserGroupObjectBySearchString(findUserGroupObjectBySearchStringReq);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00008088 File Offset: 0x00006288
		public CreateGroupResp CreateGroup(CreateGroupReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<CreateGroupReq>(Request);
			IPeople clientInstance = ClientServiceFactory.GetClientInstance<IPeople>();
			return clientInstance.CreateGroup(Request);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000080B4 File Offset: 0x000062B4
		public void DeleteGroup(DeleteGroupReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<DeleteGroupReq>(Request);
			IPeople clientInstance = ClientServiceFactory.GetClientInstance<IPeople>();
			clientInstance.DeleteGroup(Request);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000080DC File Offset: 0x000062DC
		public void DeleteUser(DeleteUserReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<DeleteUserReq>(Request);
			IPeople clientInstance = ClientServiceFactory.GetClientInstance<IPeople>();
			clientInstance.DeleteUser(Request);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00008104 File Offset: 0x00006304
		public void UpdateGroup(UpdateGroupReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<UpdateGroupReq>(Request);
			IPeople clientInstance = ClientServiceFactory.GetClientInstance<IPeople>();
			clientInstance.UpdateGroup(Request);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000812C File Offset: 0x0000632C
		public void UpdateUser(UpdateUserReq request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<UpdateUserReq>(request);
			IPeople clientInstance = ClientServiceFactory.GetClientInstance<IPeople>();
			clientInstance.UpdateUser(request);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00008154 File Offset: 0x00006354
		public UnDeleteUserResp UnDeleteUser(UnDeleteUserReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<UnDeleteUserReq>(Request);
			IPeople clientInstance = ClientServiceFactory.GetClientInstance<IPeople>();
			return clientInstance.UnDeleteUser(Request);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00008180 File Offset: 0x00006380
		public LoadPersonWithExtendedInfoResp LoadPersonWithExtendedInfo(LoadPersonWithExtendedInfoReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<LoadPersonWithExtendedInfoReq>(Request);
			IPeople clientInstance = ClientServiceFactory.GetClientInstance<IPeople>();
			return clientInstance.LoadPersonWithExtendedInfo(Request);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000081AC File Offset: 0x000063AC
		public LoadPersonsByIdsResp LoadPersonsByIds(LoadPersonsByIdsReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<LoadPersonsByIdsReq>(Request);
			IPeople clientInstance = ClientServiceFactory.GetClientInstance<IPeople>();
			return clientInstance.LoadPersonsByIds(Request);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000081D8 File Offset: 0x000063D8
		public GetTempStudentNumberResp GetTempStudentNumber(GetTempStudentNumberReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<GetTempStudentNumberReq>(Request);
			IPeople clientInstance = ClientServiceFactory.GetClientInstance<IPeople>();
			return clientInstance.GetTempStudentNumber(Request);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00008204 File Offset: 0x00006404
		public LoadDeletedAccountsResp LoadDeletedAccounts(LoadDeletedAccountsReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<LoadDeletedAccountsReq>(Request);
			IPeople clientInstance = ClientServiceFactory.GetClientInstance<IPeople>();
			return clientInstance.LoadDeletedAccounts(Request);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00008230 File Offset: 0x00006430
		public IAsyncResult BeginFindUserGroupObjectBySearchString(FindUserGroupObjectBySearchStringReq req, AsyncCallback callback, object asyncState)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<FindUserGroupObjectBySearchStringReq>(req);
			this.PeopleAsyncProxy = ClientServiceFactory.GetAsyncClientInstance<IPeopleAsync>();
			bool flag = this.PeopleAsyncProxy == null;
			if (flag)
			{
				throw new ClockWorkServerNotConnectedException("Asynchronous call to FindUserGroupObjectBySearchString needs ClockWork Server connection");
			}
			return this.PeopleAsyncProxy.BeginFindUserGroupObjectBySearchString(req, callback, asyncState);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00008280 File Offset: 0x00006480
		public FindUserGroupObjectBySearchStringResp EndFindUserGroupObjectBySearchString(IAsyncResult result)
		{
			bool flag = this.PeopleAsyncProxy == null;
			if (flag)
			{
				throw new ClockWorkServerNotConnectedException("Asynchronous call to FindUserGroupObjectBySearchString needs ClockWork Server connection");
			}
			FindUserGroupObjectBySearchStringResp result2 = this.PeopleAsyncProxy.EndFindUserGroupObjectBySearchString(result);
			this.PeopleAsyncProxy = null;
			return result2;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000082C0 File Offset: 0x000064C0
		public int CreateUser(PersonBaseDTO User, List<int> GroupIds)
		{
			CreateUserReq createUserReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateUserReq>();
			createUserReq.User = User;
			createUserReq.GroupIds = GroupIds;
			return this.CreateUser(createUserReq).PersonId;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000082FC File Offset: 0x000064FC
		public PersonBaseDTO LoadPerson(int PersonId)
		{
			LoadPersonReq loadPersonReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadPersonReq>();
			loadPersonReq.PersonId = PersonId;
			return this.LoadPerson(loadPersonReq).Person;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00008330 File Offset: 0x00006530
		public PersonBaseDTO LoadPersonByStudentNumber(string Student_No, bool checkIfWhoamiIsAllowToSeeThisStudent = false)
		{
			LoadPersonByStudentNumberReq loadPersonByStudentNumberReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadPersonByStudentNumberReq>();
			loadPersonByStudentNumberReq.Student_no = Student_No;
			loadPersonByStudentNumberReq.CheckIfWhoAmIIsAllowedToSeeThisStudent = checkIfWhoamiIsAllowToSeeThisStudent;
			return this.LoadPersonByStudentNumber(loadPersonByStudentNumberReq).Person;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000836C File Offset: 0x0000656C
		public bool IsStudentsAccommodationsExpired(int PersonId)
		{
			IsStudentsAccommodationsExpiredReq isStudentsAccommodationsExpiredReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<IsStudentsAccommodationsExpiredReq>();
			isStudentsAccommodationsExpiredReq.PersonId = PersonId;
			return this.IsStudentsAccommodationsExpired(isStudentsAccommodationsExpiredReq).IsExpired;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000083A0 File Offset: 0x000065A0
		public IList<PersonBaseDTO> LoadGroupMembers(params int[] GroupIds)
		{
			LoadMultipleGroupMembersReq loadMultipleGroupMembersReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadMultipleGroupMembersReq>();
			loadMultipleGroupMembersReq.GroupIds = GroupIds;
			return this.LoadMultipleGroupMembers(loadMultipleGroupMembersReq).GroupMembers;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000083D4 File Offset: 0x000065D4
		public IList<GroupDTO> LoadGroups()
		{
			LoadGroupsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadGroupsReq>();
			return this.LoadGroups(request).Groups;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00008400 File Offset: 0x00006600
		public PersonBaseDTO LoadPersonById(int PersonId)
		{
			LoadPersonReq loadPersonReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadPersonReq>();
			loadPersonReq.PersonId = PersonId;
			return this.LoadPerson(loadPersonReq).Person;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00008434 File Offset: 0x00006634
		public IList<PersonBaseDTO> LoadStaff()
		{
			LoadStaffReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadStaffReq>();
			return this.LoadStaff(request).People;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00008460 File Offset: 0x00006660
		public IList<PersonBaseDTO> FindStudentBySearchString(string SearchString)
		{
			FindStudentBySearchStringReq findStudentBySearchStringReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<FindStudentBySearchStringReq>();
			findStudentBySearchStringReq.SearchString = SearchString;
			return this.FindStudentBySearchString(findStudentBySearchStringReq).Students;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00008494 File Offset: 0x00006694
		public int CreateGroup(GroupDTO Group)
		{
			CreateGroupReq createGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateGroupReq>();
			createGroupReq.Group = Group;
			return this.CreateGroup(createGroupReq).GroupId;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000084C8 File Offset: 0x000066C8
		public void DeleteGroup(int GroupId)
		{
			DeleteGroupReq deleteGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteGroupReq>();
			deleteGroupReq.GroupId = GroupId;
			this.DeleteGroup(deleteGroupReq);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000084F4 File Offset: 0x000066F4
		public void DeleteUser(int PersonId, bool JustDeactivate)
		{
			DeleteUserReq deleteUserReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteUserReq>();
			deleteUserReq.PersonId = PersonId;
			deleteUserReq.JustDeactivate = JustDeactivate;
			this.DeleteUser(deleteUserReq);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00008528 File Offset: 0x00006728
		public void UpdateGroup(GroupDTO Group)
		{
			UpdateGroupReq updateGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateGroupReq>();
			updateGroupReq.Group = Group;
			this.UpdateGroup(updateGroupReq);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00008554 File Offset: 0x00006754
		public void UpdateUser(PersonBaseDTO User)
		{
			UpdateUserReq updateUserReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateUserReq>();
			updateUserReq.User = User;
			this.UpdateUser(updateUserReq);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000857D File Offset: 0x0000677D
		public void Close()
		{
			this.Dispose();
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00008588 File Offset: 0x00006788
		~PeopleClientManager()
		{
			this.Dispose(false);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000085BC File Offset: 0x000067BC
		protected virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				if (disposing)
				{
				}
				bool flag2 = this.PeopleAsyncProxy != null;
				if (flag2)
				{
					this.PeopleAsyncProxy.Close();
				}
				this.PeopleAsyncProxy = null;
				this.disposed = true;
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00008608 File Offset: 0x00006808
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400000D RID: 13
		private bool disposed = false;
	}
}
