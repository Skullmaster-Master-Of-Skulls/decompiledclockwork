using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.ListItem;
using TechnoPro.Common.Core.CustomForms;
using TechnoPro.Common.Core.Mappers.CustomForms.Field;
using TechnoPro.Common.ICore.CustomForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000033 RID: 51
	public class CustomFormListItemServiceManager : ICustomFormListItem, IService
	{
		// Token: 0x06000206 RID: 518 RVA: 0x0000A068 File Offset: 0x00008268
		[DebuggerStepThrough]
		public Task<LoadListItemsByGroupIdResp> LoadListItemsByGroupIdAsync(LoadListItemsByGroupIdReq Request)
		{
			CustomFormListItemServiceManager.<LoadListItemsByGroupIdAsync>d__0 <LoadListItemsByGroupIdAsync>d__ = new CustomFormListItemServiceManager.<LoadListItemsByGroupIdAsync>d__0();
			<LoadListItemsByGroupIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadListItemsByGroupIdResp>.Create();
			<LoadListItemsByGroupIdAsync>d__.<>4__this = this;
			<LoadListItemsByGroupIdAsync>d__.Request = Request;
			<LoadListItemsByGroupIdAsync>d__.<>1__state = -1;
			<LoadListItemsByGroupIdAsync>d__.<>t__builder.Start<CustomFormListItemServiceManager.<LoadListItemsByGroupIdAsync>d__0>(ref <LoadListItemsByGroupIdAsync>d__);
			return <LoadListItemsByGroupIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000A0B4 File Offset: 0x000082B4
		public LoadListItemsByGroupIdResp LoadListItemsByGroupId(LoadListItemsByGroupIdReq Request)
		{
			ICustomFormListItemManager customFormListItemManager = new CustomFormListItemManager(Request.GetOperationContext());
			IList<CustomListItem> list = customFormListItemManager.LoadListItemsByGroupId(Request.CustomListGroupId);
			LoadListItemsByGroupIdResp loadListItemsByGroupIdResp = new LoadListItemsByGroupIdResp();
			List<CustomListItemDTO> listItems;
			if (list == null)
			{
				listItems = null;
			}
			else
			{
				listItems = (from g in list
				select g.ToDTO()).ToList<CustomListItemDTO>();
			}
			loadListItemsByGroupIdResp.ListItems = listItems;
			return loadListItemsByGroupIdResp;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000A11C File Offset: 0x0000831C
		[DebuggerStepThrough]
		public Task<LoadListItemByListItemIdResp> LoadListItemByListItemIdAsync(LoadListItemByListItemIdReq Request)
		{
			CustomFormListItemServiceManager.<LoadListItemByListItemIdAsync>d__2 <LoadListItemByListItemIdAsync>d__ = new CustomFormListItemServiceManager.<LoadListItemByListItemIdAsync>d__2();
			<LoadListItemByListItemIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadListItemByListItemIdResp>.Create();
			<LoadListItemByListItemIdAsync>d__.<>4__this = this;
			<LoadListItemByListItemIdAsync>d__.Request = Request;
			<LoadListItemByListItemIdAsync>d__.<>1__state = -1;
			<LoadListItemByListItemIdAsync>d__.<>t__builder.Start<CustomFormListItemServiceManager.<LoadListItemByListItemIdAsync>d__2>(ref <LoadListItemByListItemIdAsync>d__);
			return <LoadListItemByListItemIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000A168 File Offset: 0x00008368
		public LoadListItemByListItemIdResp LoadListItemByListItemId(LoadListItemByListItemIdReq Request)
		{
			ICustomFormListItemManager customFormListItemManager = new CustomFormListItemManager(Request.GetOperationContext());
			CustomListItem customListItem = customFormListItemManager.LoadListItemByListItemId(Request.ListItemId);
			return new LoadListItemByListItemIdResp
			{
				ListItem = ((customListItem != null) ? customListItem.ToDTO() : null)
			};
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000A1AC File Offset: 0x000083AC
		[DebuggerStepThrough]
		public Task<CreateCustomListGroupResp> CreateCustomListGroupAsync(CreateCustomListGroupReq Request)
		{
			CustomFormListItemServiceManager.<CreateCustomListGroupAsync>d__4 <CreateCustomListGroupAsync>d__ = new CustomFormListItemServiceManager.<CreateCustomListGroupAsync>d__4();
			<CreateCustomListGroupAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CreateCustomListGroupResp>.Create();
			<CreateCustomListGroupAsync>d__.<>4__this = this;
			<CreateCustomListGroupAsync>d__.Request = Request;
			<CreateCustomListGroupAsync>d__.<>1__state = -1;
			<CreateCustomListGroupAsync>d__.<>t__builder.Start<CustomFormListItemServiceManager.<CreateCustomListGroupAsync>d__4>(ref <CreateCustomListGroupAsync>d__);
			return <CreateCustomListGroupAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000A1F8 File Offset: 0x000083F8
		[DebuggerStepThrough]
		public Task<CreateCustomListItemResp> CreateCustomListItemAsync(CreateCustomListItemReq Request)
		{
			CustomFormListItemServiceManager.<CreateCustomListItemAsync>d__5 <CreateCustomListItemAsync>d__ = new CustomFormListItemServiceManager.<CreateCustomListItemAsync>d__5();
			<CreateCustomListItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CreateCustomListItemResp>.Create();
			<CreateCustomListItemAsync>d__.<>4__this = this;
			<CreateCustomListItemAsync>d__.Request = Request;
			<CreateCustomListItemAsync>d__.<>1__state = -1;
			<CreateCustomListItemAsync>d__.<>t__builder.Start<CustomFormListItemServiceManager.<CreateCustomListItemAsync>d__5>(ref <CreateCustomListItemAsync>d__);
			return <CreateCustomListItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000A244 File Offset: 0x00008444
		[DebuggerStepThrough]
		public Task<UpdateCustomListItemResp> UpdateCustomListItemAsync(UpdateCustomListItemReq Request)
		{
			CustomFormListItemServiceManager.<UpdateCustomListItemAsync>d__6 <UpdateCustomListItemAsync>d__ = new CustomFormListItemServiceManager.<UpdateCustomListItemAsync>d__6();
			<UpdateCustomListItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UpdateCustomListItemResp>.Create();
			<UpdateCustomListItemAsync>d__.<>4__this = this;
			<UpdateCustomListItemAsync>d__.Request = Request;
			<UpdateCustomListItemAsync>d__.<>1__state = -1;
			<UpdateCustomListItemAsync>d__.<>t__builder.Start<CustomFormListItemServiceManager.<UpdateCustomListItemAsync>d__6>(ref <UpdateCustomListItemAsync>d__);
			return <UpdateCustomListItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000A290 File Offset: 0x00008490
		[DebuggerStepThrough]
		public Task<UpdateCustomListItemGroupResp> UpdateCustomListItemGroupAsync(UpdateCustomListItemGroupReq Request)
		{
			CustomFormListItemServiceManager.<UpdateCustomListItemGroupAsync>d__7 <UpdateCustomListItemGroupAsync>d__ = new CustomFormListItemServiceManager.<UpdateCustomListItemGroupAsync>d__7();
			<UpdateCustomListItemGroupAsync>d__.<>t__builder = AsyncTaskMethodBuilder<UpdateCustomListItemGroupResp>.Create();
			<UpdateCustomListItemGroupAsync>d__.<>4__this = this;
			<UpdateCustomListItemGroupAsync>d__.Request = Request;
			<UpdateCustomListItemGroupAsync>d__.<>1__state = -1;
			<UpdateCustomListItemGroupAsync>d__.<>t__builder.Start<CustomFormListItemServiceManager.<UpdateCustomListItemGroupAsync>d__7>(ref <UpdateCustomListItemGroupAsync>d__);
			return <UpdateCustomListItemGroupAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000A2DC File Offset: 0x000084DC
		[DebuggerStepThrough]
		public Task<EnableOrDisableCustomListItemResp> EnableOrDisableCustomListItemAsync(EnableOrDisableCustomListItemReq Request)
		{
			CustomFormListItemServiceManager.<EnableOrDisableCustomListItemAsync>d__8 <EnableOrDisableCustomListItemAsync>d__ = new CustomFormListItemServiceManager.<EnableOrDisableCustomListItemAsync>d__8();
			<EnableOrDisableCustomListItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<EnableOrDisableCustomListItemResp>.Create();
			<EnableOrDisableCustomListItemAsync>d__.<>4__this = this;
			<EnableOrDisableCustomListItemAsync>d__.Request = Request;
			<EnableOrDisableCustomListItemAsync>d__.<>1__state = -1;
			<EnableOrDisableCustomListItemAsync>d__.<>t__builder.Start<CustomFormListItemServiceManager.<EnableOrDisableCustomListItemAsync>d__8>(ref <EnableOrDisableCustomListItemAsync>d__);
			return <EnableOrDisableCustomListItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000A328 File Offset: 0x00008528
		[DebuggerStepThrough]
		public Task<EnableOrDisableCustomListItemGroupResp> EnableOrDisableCustomListItemGroupAsync(EnableOrDisableCustomListItemGroupReq Request)
		{
			CustomFormListItemServiceManager.<EnableOrDisableCustomListItemGroupAsync>d__9 <EnableOrDisableCustomListItemGroupAsync>d__ = new CustomFormListItemServiceManager.<EnableOrDisableCustomListItemGroupAsync>d__9();
			<EnableOrDisableCustomListItemGroupAsync>d__.<>t__builder = AsyncTaskMethodBuilder<EnableOrDisableCustomListItemGroupResp>.Create();
			<EnableOrDisableCustomListItemGroupAsync>d__.<>4__this = this;
			<EnableOrDisableCustomListItemGroupAsync>d__.Request = Request;
			<EnableOrDisableCustomListItemGroupAsync>d__.<>1__state = -1;
			<EnableOrDisableCustomListItemGroupAsync>d__.<>t__builder.Start<CustomFormListItemServiceManager.<EnableOrDisableCustomListItemGroupAsync>d__9>(ref <EnableOrDisableCustomListItemGroupAsync>d__);
			return <EnableOrDisableCustomListItemGroupAsync>d__.<>t__builder.Task;
		}
	}
}
