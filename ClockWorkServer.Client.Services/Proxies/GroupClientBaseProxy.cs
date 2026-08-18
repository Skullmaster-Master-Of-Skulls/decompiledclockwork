using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000104 RID: 260
	internal class GroupClientBaseProxy : ClientBase<IGroup>, IGroup, IService
	{
		// Token: 0x06000A1D RID: 2589 RVA: 0x00019CCC File Offset: 0x00017ECC
		public GroupClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x00019CD7 File Offset: 0x00017ED7
		public GroupClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x00019CE4 File Offset: 0x00017EE4
		public CreateGroupByTitleResp CreateGroupByTitle(CreateGroupByTitleReq Request)
		{
			return base.Channel.CreateGroupByTitle(Request);
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00019D04 File Offset: 0x00017F04
		public LoadGroupByTitleResp LoadGroupByTitle(LoadGroupByTitleReq Request)
		{
			return base.Channel.LoadGroupByTitle(Request);
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00019D24 File Offset: 0x00017F24
		public LoadGroupByIdResp LoadGroupById(LoadGroupByIdReq Request)
		{
			return base.Channel.LoadGroupById(Request);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00019D44 File Offset: 0x00017F44
		public LoadAllowedGroupsResp LoadAllowedGroups(LoadAllowedGroupsReq Request)
		{
			return base.Channel.LoadAllowedGroups(Request);
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00019D64 File Offset: 0x00017F64
		public LoadAllGroupContainersResp LoadAllGroupContainers(LoadAllGroupContainersReq Request)
		{
			return base.Channel.LoadAllGroupContainers(Request);
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00019D84 File Offset: 0x00017F84
		public LoadAllGroupForEditsResp LoadAllGroupForEdits(LoadAllGroupForEditsReq Request)
		{
			return base.Channel.LoadAllGroupForEdits(Request);
		}
	}
}
