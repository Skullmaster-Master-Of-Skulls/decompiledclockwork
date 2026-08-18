using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.Common.ICore.CustomForms
{
	// Token: 0x020000AD RID: 173
	public interface ICustomFormListItemManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000518 RID: 1304
		Task<CustomListItem> LoadListItemByListItemIdAsync(Guid listItemId);

		// Token: 0x06000519 RID: 1305
		CustomListItem LoadListItemByListItemId(Guid listItemId);

		// Token: 0x0600051A RID: 1306
		Task<IList<CustomListItem>> LoadListItemsByGroupIdAsync(Guid customListItemGroupId);

		// Token: 0x0600051B RID: 1307
		IList<CustomListItem> LoadListItemsByGroupId(Guid customListItemGroupId);

		// Token: 0x0600051C RID: 1308
		Task<Guid> CreateListGroupAsync(CustomListItemGroup group);

		// Token: 0x0600051D RID: 1309
		Task<Guid> CreateListItemAsync(Guid customListItemGroupId, CustomListItem item);

		// Token: 0x0600051E RID: 1310
		Task UpdateListItemAsync(CustomListItem item);

		// Token: 0x0600051F RID: 1311
		Task UpdateListItemGroupAsync(CustomListItemGroup group);

		// Token: 0x06000520 RID: 1312
		Task EnableOrDisableListItemAsync(Guid CustomListItemId, bool enable);

		// Token: 0x06000521 RID: 1313
		Task EnableOrDisableListItemGroupAsync(Guid customListItemGroupId, bool enable);
	}
}
