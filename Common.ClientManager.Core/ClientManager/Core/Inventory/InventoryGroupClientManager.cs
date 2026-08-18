using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Inventory
{
	// Token: 0x02000053 RID: 83
	public class InventoryGroupClientManager : IInventoryGroupClientManager, IWebService
	{
		// Token: 0x060002D2 RID: 722 RVA: 0x0000C938 File Offset: 0x0000AB38
		public int CreateProductGroup(InventoryGroupDTO pGroup)
		{
			CreateProductGroupReq createProductGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateProductGroupReq>();
			createProductGroupReq.Group = pGroup;
			return ClientServiceFactory.GetClientInstance<IInventoryGroup>().CreateProductGroup(createProductGroupReq).GroupId;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000C970 File Offset: 0x0000AB70
		public void UpdateProductGroup(InventoryGroupDTO pGroup)
		{
			UpdateProductGroupReq updateProductGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateProductGroupReq>();
			updateProductGroupReq.Group = pGroup;
			ClientServiceFactory.GetClientInstance<IInventoryGroup>().UpdateProductGroup(updateProductGroupReq);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000C9A0 File Offset: 0x0000ABA0
		public bool DeleteEmptyProductGroup(int pGroupId)
		{
			DeleteEmptyProductGroupReq deleteEmptyProductGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteEmptyProductGroupReq>();
			deleteEmptyProductGroupReq.GroupId = pGroupId;
			return ClientServiceFactory.GetClientInstance<IInventoryGroup>().DeleteEmptyProductGroup(deleteEmptyProductGroupReq).WasDeleted;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000C9D8 File Offset: 0x0000ABD8
		public InventoryGroupDTO GetGroupById(int pGroupId)
		{
			GetGroupByIdReq getGroupByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetGroupByIdReq>();
			getGroupByIdReq.GroupId = pGroupId;
			return ClientServiceFactory.GetClientInstance<IInventoryGroup>().GetGroupById(getGroupByIdReq).Group;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000CA10 File Offset: 0x0000AC10
		public IList<InventoryGroupDTO> GetGroups()
		{
			GetGroupsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetGroupsReq>();
			return ClientServiceFactory.GetClientInstance<IInventoryGroup>().GetGroups(request).Groups;
		}
	}
}
