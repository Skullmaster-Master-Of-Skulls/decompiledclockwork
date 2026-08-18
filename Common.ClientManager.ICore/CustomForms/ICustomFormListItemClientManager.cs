using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.CustomForms
{
	// Token: 0x0200006B RID: 107
	public interface ICustomFormListItemClientManager : IWebService
	{
		// Token: 0x06000321 RID: 801
		Task<CustomListItemDTO> LoadListItemByListItemIdAsync(Guid listItemId);

		// Token: 0x06000322 RID: 802
		CustomListItemDTO LoadListItemByListItemId(Guid listItemId);

		// Token: 0x06000323 RID: 803
		Task<IList<CustomListItemDTO>> LoadListItemsByGroupIdAsync(Guid customListItemGroupId);

		// Token: 0x06000324 RID: 804
		IList<CustomListItemDTO> LoadListItemsByGroupId(Guid customListItemGroupId);

		// Token: 0x06000325 RID: 805
		Task<Guid> CreateListGroupAsync(CustomListItemGroupDTO group);

		// Token: 0x06000326 RID: 806
		Task<Guid> CreateListItemAsync(Guid customListItemGroupId, CustomListItemDTO item);

		// Token: 0x06000327 RID: 807
		Task UpdateListItemAsync(CustomListItemDTO item);

		// Token: 0x06000328 RID: 808
		Task UpdateListItemGroupAsync(CustomListItemGroupDTO group);

		// Token: 0x06000329 RID: 809
		Task EnableOrDisableListItemAsync(Guid CustomListItemId, bool enable);

		// Token: 0x0600032A RID: 810
		Task EnableOrDisableListItemGroupAsync(Guid customListItemGroupId, bool enable);
	}
}
