using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Inventory
{
	// Token: 0x02000058 RID: 88
	public class InventoryProductStatusClientManager : IInventoryProductStatusClientManager, IWebService
	{
		// Token: 0x06000313 RID: 787 RVA: 0x0000D7EC File Offset: 0x0000B9EC
		public int CreateProductStatus(InventoryProductStatusDTO productStatus)
		{
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			CreateProductStatusReq createProductStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateProductStatusReq>();
			createProductStatusReq.ProductStatus = productStatus;
			int productStatusId = ClientServiceFactory.GetClientInstance<IInventoryProductStatus>().CreateProductStatus(createProductStatusReq).ProductStatusId;
			bool flag = productStatusId > 0;
			if (flag)
			{
				clientCache.Remove("cProductStatusList");
			}
			return productStatusId;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000D840 File Offset: 0x0000BA40
		public void UpdateProductStatus(InventoryProductStatusDTO productStatus)
		{
			UpdateProductStatusReq updateProductStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateProductStatusReq>();
			updateProductStatusReq.ProductStatus = productStatus;
			ClientServiceFactory.GetClientInstance<IInventoryProductStatus>().UpdateProductStatus(updateProductStatusReq);
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			clientCache.Remove("cProductStatusList");
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000D880 File Offset: 0x0000BA80
		public InventoryProductStatusDTO GetProductStatusById(int pStatusId)
		{
			GetProductStatusByIdReq getProductStatusByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetProductStatusByIdReq>();
			getProductStatusByIdReq.ProductStatusId = pStatusId;
			return ClientServiceFactory.GetClientInstance<IInventoryProductStatus>().GetProductStatusById(getProductStatusByIdReq).ProductStatus;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000D8B8 File Offset: 0x0000BAB8
		public IList<InventoryProductStatusDTO> GetProductStatusList()
		{
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			IList<InventoryProductStatusDTO> productStatusList = clientCache.ProductStatusList;
			bool flag = productStatusList != null;
			IList<InventoryProductStatusDTO> result;
			if (flag)
			{
				result = productStatusList;
			}
			else
			{
				GetProductStatusListReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetProductStatusListReq>();
				result = ClientServiceFactory.GetClientInstance<IInventoryProductStatus>().GetProductStatusList(request).ProductStatusList;
			}
			return result;
		}
	}
}
