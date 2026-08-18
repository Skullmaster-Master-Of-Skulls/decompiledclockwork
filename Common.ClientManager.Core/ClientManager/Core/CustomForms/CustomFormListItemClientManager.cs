using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.ListItem;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.CustomForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.CustomForms
{
	// Token: 0x02000072 RID: 114
	public class CustomFormListItemClientManager : ICustomFormListItemClientManager, IWebService
	{
		// Token: 0x06000421 RID: 1057 RVA: 0x00012854 File Offset: 0x00010A54
		[DebuggerStepThrough]
		public Task<IList<CustomListItemDTO>> LoadListItemsByGroupIdAsync(Guid customListGroupId)
		{
			CustomFormListItemClientManager.<LoadListItemsByGroupIdAsync>d__0 <LoadListItemsByGroupIdAsync>d__ = new CustomFormListItemClientManager.<LoadListItemsByGroupIdAsync>d__0();
			<LoadListItemsByGroupIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<CustomListItemDTO>>.Create();
			<LoadListItemsByGroupIdAsync>d__.<>4__this = this;
			<LoadListItemsByGroupIdAsync>d__.customListGroupId = customListGroupId;
			<LoadListItemsByGroupIdAsync>d__.<>1__state = -1;
			<LoadListItemsByGroupIdAsync>d__.<>t__builder.Start<CustomFormListItemClientManager.<LoadListItemsByGroupIdAsync>d__0>(ref <LoadListItemsByGroupIdAsync>d__);
			return <LoadListItemsByGroupIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000128A0 File Offset: 0x00010AA0
		public IList<CustomListItemDTO> LoadListItemsByGroupId(Guid customListGroupId)
		{
			LoadListItemsByGroupIdReq loadListItemsByGroupIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadListItemsByGroupIdReq>();
			loadListItemsByGroupIdReq.CustomListGroupId = customListGroupId;
			return ClientServiceFactory.GetClientInstance<ICustomFormListItem>().LoadListItemsByGroupId(loadListItemsByGroupIdReq).ListItems;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000128D8 File Offset: 0x00010AD8
		[DebuggerStepThrough]
		public Task<CustomListItemDTO> LoadListItemByListItemIdAsync(Guid listItemId)
		{
			CustomFormListItemClientManager.<LoadListItemByListItemIdAsync>d__2 <LoadListItemByListItemIdAsync>d__ = new CustomFormListItemClientManager.<LoadListItemByListItemIdAsync>d__2();
			<LoadListItemByListItemIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CustomListItemDTO>.Create();
			<LoadListItemByListItemIdAsync>d__.<>4__this = this;
			<LoadListItemByListItemIdAsync>d__.listItemId = listItemId;
			<LoadListItemByListItemIdAsync>d__.<>1__state = -1;
			<LoadListItemByListItemIdAsync>d__.<>t__builder.Start<CustomFormListItemClientManager.<LoadListItemByListItemIdAsync>d__2>(ref <LoadListItemByListItemIdAsync>d__);
			return <LoadListItemByListItemIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00012924 File Offset: 0x00010B24
		public CustomListItemDTO LoadListItemByListItemId(Guid listItemId)
		{
			LoadListItemByListItemIdReq loadListItemByListItemIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadListItemByListItemIdReq>();
			loadListItemByListItemIdReq.ListItemId = listItemId;
			return ClientServiceFactory.GetClientInstance<ICustomFormListItem>().LoadListItemByListItemId(loadListItemByListItemIdReq).ListItem;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001295C File Offset: 0x00010B5C
		[DebuggerStepThrough]
		public Task<Guid> CreateListGroupAsync(CustomListItemGroupDTO group)
		{
			CustomFormListItemClientManager.<CreateListGroupAsync>d__4 <CreateListGroupAsync>d__ = new CustomFormListItemClientManager.<CreateListGroupAsync>d__4();
			<CreateListGroupAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Guid>.Create();
			<CreateListGroupAsync>d__.<>4__this = this;
			<CreateListGroupAsync>d__.group = group;
			<CreateListGroupAsync>d__.<>1__state = -1;
			<CreateListGroupAsync>d__.<>t__builder.Start<CustomFormListItemClientManager.<CreateListGroupAsync>d__4>(ref <CreateListGroupAsync>d__);
			return <CreateListGroupAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000129A8 File Offset: 0x00010BA8
		[DebuggerStepThrough]
		public Task<Guid> CreateListItemAsync(Guid customListItemGroupId, CustomListItemDTO item)
		{
			CustomFormListItemClientManager.<CreateListItemAsync>d__5 <CreateListItemAsync>d__ = new CustomFormListItemClientManager.<CreateListItemAsync>d__5();
			<CreateListItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Guid>.Create();
			<CreateListItemAsync>d__.<>4__this = this;
			<CreateListItemAsync>d__.customListItemGroupId = customListItemGroupId;
			<CreateListItemAsync>d__.item = item;
			<CreateListItemAsync>d__.<>1__state = -1;
			<CreateListItemAsync>d__.<>t__builder.Start<CustomFormListItemClientManager.<CreateListItemAsync>d__5>(ref <CreateListItemAsync>d__);
			return <CreateListItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x000129FC File Offset: 0x00010BFC
		[DebuggerStepThrough]
		public Task UpdateListItemAsync(CustomListItemDTO item)
		{
			CustomFormListItemClientManager.<UpdateListItemAsync>d__6 <UpdateListItemAsync>d__ = new CustomFormListItemClientManager.<UpdateListItemAsync>d__6();
			<UpdateListItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateListItemAsync>d__.<>4__this = this;
			<UpdateListItemAsync>d__.item = item;
			<UpdateListItemAsync>d__.<>1__state = -1;
			<UpdateListItemAsync>d__.<>t__builder.Start<CustomFormListItemClientManager.<UpdateListItemAsync>d__6>(ref <UpdateListItemAsync>d__);
			return <UpdateListItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00012A48 File Offset: 0x00010C48
		[DebuggerStepThrough]
		public Task UpdateListItemGroupAsync(CustomListItemGroupDTO group)
		{
			CustomFormListItemClientManager.<UpdateListItemGroupAsync>d__7 <UpdateListItemGroupAsync>d__ = new CustomFormListItemClientManager.<UpdateListItemGroupAsync>d__7();
			<UpdateListItemGroupAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateListItemGroupAsync>d__.<>4__this = this;
			<UpdateListItemGroupAsync>d__.group = group;
			<UpdateListItemGroupAsync>d__.<>1__state = -1;
			<UpdateListItemGroupAsync>d__.<>t__builder.Start<CustomFormListItemClientManager.<UpdateListItemGroupAsync>d__7>(ref <UpdateListItemGroupAsync>d__);
			return <UpdateListItemGroupAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00012A94 File Offset: 0x00010C94
		[DebuggerStepThrough]
		public Task EnableOrDisableListItemAsync(Guid CustomListItemId, bool enable)
		{
			CustomFormListItemClientManager.<EnableOrDisableListItemAsync>d__8 <EnableOrDisableListItemAsync>d__ = new CustomFormListItemClientManager.<EnableOrDisableListItemAsync>d__8();
			<EnableOrDisableListItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<EnableOrDisableListItemAsync>d__.<>4__this = this;
			<EnableOrDisableListItemAsync>d__.CustomListItemId = CustomListItemId;
			<EnableOrDisableListItemAsync>d__.enable = enable;
			<EnableOrDisableListItemAsync>d__.<>1__state = -1;
			<EnableOrDisableListItemAsync>d__.<>t__builder.Start<CustomFormListItemClientManager.<EnableOrDisableListItemAsync>d__8>(ref <EnableOrDisableListItemAsync>d__);
			return <EnableOrDisableListItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00012AE8 File Offset: 0x00010CE8
		[DebuggerStepThrough]
		public Task EnableOrDisableListItemGroupAsync(Guid customListItemGroupId, bool enable)
		{
			CustomFormListItemClientManager.<EnableOrDisableListItemGroupAsync>d__9 <EnableOrDisableListItemGroupAsync>d__ = new CustomFormListItemClientManager.<EnableOrDisableListItemGroupAsync>d__9();
			<EnableOrDisableListItemGroupAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<EnableOrDisableListItemGroupAsync>d__.<>4__this = this;
			<EnableOrDisableListItemGroupAsync>d__.customListItemGroupId = customListItemGroupId;
			<EnableOrDisableListItemGroupAsync>d__.enable = enable;
			<EnableOrDisableListItemGroupAsync>d__.<>1__state = -1;
			<EnableOrDisableListItemGroupAsync>d__.<>t__builder.Start<CustomFormListItemClientManager.<EnableOrDisableListItemGroupAsync>d__9>(ref <EnableOrDisableListItemGroupAsync>d__);
			return <EnableOrDisableListItemGroupAsync>d__.<>t__builder.Task;
		}
	}
}
