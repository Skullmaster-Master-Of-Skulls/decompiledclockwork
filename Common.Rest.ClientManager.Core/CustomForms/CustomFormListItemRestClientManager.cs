using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.ListItem;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.CustomForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.CustomForms
{
	// Token: 0x0200005F RID: 95
	public class CustomFormListItemRestClientManager : BearerTokenRestProxy<ICustomFormListItemClientManager>, ICustomFormListItemClientManager, IWebService
	{
		// Token: 0x06000394 RID: 916 RVA: 0x0000AE89 File Offset: 0x00009089
		public CustomFormListItemRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000AE93 File Offset: 0x00009093
		public CustomFormListItemRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000AEA0 File Offset: 0x000090A0
		public async Task<CustomListItemDTO> LoadListItemByListItemIdAsync(Guid listItemId)
		{
			return await this.GetAsync<CustomListItemDTO>(string.Format("customformlistitem/listitem/id/{0}", listItemId), true).ConfigureAwait(false);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000AEED File Offset: 0x000090ED
		public CustomListItemDTO LoadListItemByListItemId(Guid listItemId)
		{
			return base.Get<CustomListItemDTO>(string.Format("customformlistitem/listitem/id/{0}", listItemId), true);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000AF08 File Offset: 0x00009108
		public async Task<IList<CustomListItemDTO>> LoadListItemsByGroupIdAsync(Guid customListItemGroupId)
		{
			return await this.GetManyAsync<CustomListItemDTO>(string.Format("customformlistitem/listitems/groupid/{0}", customListItemGroupId), true).ConfigureAwait(false);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000AF55 File Offset: 0x00009155
		public IList<CustomListItemDTO> LoadListItemsByGroupId(Guid customListItemGroupId)
		{
			return base.GetMany<CustomListItemDTO>(string.Format("customformlistitem/listitems/groupid/{0}", customListItemGroupId), true);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000AF70 File Offset: 0x00009170
		public async Task<Guid> CreateListGroupAsync(CustomListItemGroupDTO group)
		{
			return await this.PostAsync<CustomListItemGroupDTO, Guid>(group, "customformlistitem/customlistgroup").ConfigureAwait(false);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000AFC0 File Offset: 0x000091C0
		public async Task<Guid> CreateListItemAsync(Guid customListItemGroupId, CustomListItemDTO item)
		{
			CreateCustomListItemReq createCustomListItemReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateCustomListItemReq>();
			createCustomListItemReq.GroupId = customListItemGroupId;
			createCustomListItemReq.Item = item;
			return await this.PostAsync<CreateCustomListItemReq, Guid>(createCustomListItemReq, "customformlistitem").ConfigureAwait(false);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000B018 File Offset: 0x00009218
		public async Task UpdateListItemAsync(CustomListItemDTO item)
		{
			await this.PutAsync<CustomListItemDTO>(item, "customformlistitem").ConfigureAwait(false);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000B068 File Offset: 0x00009268
		public async Task UpdateListItemGroupAsync(CustomListItemGroupDTO group)
		{
			await this.PutAsync<CustomListItemGroupDTO>(group, "customformlistitem/customlistitemgroup").ConfigureAwait(false);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000B0B8 File Offset: 0x000092B8
		public async Task EnableOrDisableListItemAsync(Guid CustomListItemId, bool enable)
		{
			EnableOrDisableCustomListItemReq enableOrDisableCustomListItemReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<EnableOrDisableCustomListItemReq>();
			enableOrDisableCustomListItemReq.ItemId = CustomListItemId;
			enableOrDisableCustomListItemReq.Enable = enable;
			await this.PostAsync<EnableOrDisableCustomListItemReq>(enableOrDisableCustomListItemReq, "customformlistitem/enableordisablecustomlistitem").ConfigureAwait(false);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000B110 File Offset: 0x00009310
		public async Task EnableOrDisableListItemGroupAsync(Guid customListItemGroupId, bool enable)
		{
			EnableOrDisableCustomListItemGroupReq enableOrDisableCustomListItemGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<EnableOrDisableCustomListItemGroupReq>();
			enableOrDisableCustomListItemGroupReq.GroupId = customListItemGroupId;
			enableOrDisableCustomListItemGroupReq.Enable = enable;
			await this.PostAsync<EnableOrDisableCustomListItemGroupReq>(enableOrDisableCustomListItemGroupReq, "customformlistitem/enableordisablecustomlistitemgroup");
		}
	}
}
