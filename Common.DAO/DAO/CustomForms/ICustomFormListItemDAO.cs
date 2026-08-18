using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.Common.DAO.CustomForms
{
	// Token: 0x02000095 RID: 149
	public interface ICustomFormListItemDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003D4 RID: 980
		Task<CustomListItem> LoadListItemByListItemIdAsync(Guid listItemId);

		// Token: 0x060003D5 RID: 981
		CustomListItem LoadListItemByListItemId(Guid listItemId);

		// Token: 0x060003D6 RID: 982
		Task<IList<CustomListItem>> LoadListItemsByGroupIdAsync(Guid customListItemGroupId);

		// Token: 0x060003D7 RID: 983
		IList<CustomListItem> LoadListItemsByGroupId(Guid customListItemGroupId);

		// Token: 0x060003D8 RID: 984
		Task<Guid> CreateListGroupAsync(CustomListItemGroup group);

		// Token: 0x060003D9 RID: 985
		Task<Guid> CreateListItemAsync(Guid customListItemGroupId, CustomListItem item);

		// Token: 0x060003DA RID: 986
		Task UpdateListItemAsync(CustomListItem item);

		// Token: 0x060003DB RID: 987
		Task UpdateListItemGroupAsync(CustomListItemGroup group);

		// Token: 0x060003DC RID: 988
		Task EnableOrDisableListItemAsync(Guid CustomListItemId, bool enable);

		// Token: 0x060003DD RID: 989
		Task EnableOrDisableListItemGroupAsync(Guid customListItemGroupId, bool enable);
	}
}
