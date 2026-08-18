using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.DAO.CustomForms;
using TechnoPro.Common.DAO.Impl.CustomForms;
using TechnoPro.Common.ICore.CustomForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.Common.Core.CustomForms
{
	// Token: 0x02000116 RID: 278
	public class CustomFormListItemManager : ICustomFormListItemManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000BAE RID: 2990 RVA: 0x0000672B File Offset: 0x0000492B
		public CustomFormListItemManager()
		{
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00052FEB File Offset: 0x000511EB
		public CustomFormListItemManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x00052FFD File Offset: 0x000511FD
		// (set) Token: 0x06000BB1 RID: 2993 RVA: 0x00053005 File Offset: 0x00051205
		public OperationContext OpContext { get; set; }

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00053010 File Offset: 0x00051210
		[DebuggerStepThrough]
		public Task<CustomListItem> LoadListItemByListItemIdAsync(Guid listItemId)
		{
			CustomFormListItemManager.<LoadListItemByListItemIdAsync>d__6 <LoadListItemByListItemIdAsync>d__ = new CustomFormListItemManager.<LoadListItemByListItemIdAsync>d__6();
			<LoadListItemByListItemIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CustomListItem>.Create();
			<LoadListItemByListItemIdAsync>d__.<>4__this = this;
			<LoadListItemByListItemIdAsync>d__.listItemId = listItemId;
			<LoadListItemByListItemIdAsync>d__.<>1__state = -1;
			<LoadListItemByListItemIdAsync>d__.<>t__builder.Start<CustomFormListItemManager.<LoadListItemByListItemIdAsync>d__6>(ref <LoadListItemByListItemIdAsync>d__);
			return <LoadListItemByListItemIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0005305C File Offset: 0x0005125C
		public CustomListItem LoadListItemByListItemId(Guid listItemId)
		{
			ICustomFormListItemDAO customFormListItemDAO = new CustomFormListItemDAO(this.OpContext);
			return customFormListItemDAO.LoadListItemByListItemId(listItemId);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00053084 File Offset: 0x00051284
		[DebuggerStepThrough]
		public Task<IList<CustomListItem>> LoadListItemsByGroupIdAsync(Guid customListItemGroupId)
		{
			CustomFormListItemManager.<LoadListItemsByGroupIdAsync>d__8 <LoadListItemsByGroupIdAsync>d__ = new CustomFormListItemManager.<LoadListItemsByGroupIdAsync>d__8();
			<LoadListItemsByGroupIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<CustomListItem>>.Create();
			<LoadListItemsByGroupIdAsync>d__.<>4__this = this;
			<LoadListItemsByGroupIdAsync>d__.customListItemGroupId = customListItemGroupId;
			<LoadListItemsByGroupIdAsync>d__.<>1__state = -1;
			<LoadListItemsByGroupIdAsync>d__.<>t__builder.Start<CustomFormListItemManager.<LoadListItemsByGroupIdAsync>d__8>(ref <LoadListItemsByGroupIdAsync>d__);
			return <LoadListItemsByGroupIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x000530D0 File Offset: 0x000512D0
		public IList<CustomListItem> LoadListItemsByGroupId(Guid customListItemGroupId)
		{
			ICustomFormListItemDAO customFormListItemDAO = new CustomFormListItemDAO(this.OpContext);
			return customFormListItemDAO.LoadListItemsByGroupId(customListItemGroupId);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x000530F8 File Offset: 0x000512F8
		[DebuggerStepThrough]
		public Task<Guid> CreateListGroupAsync(CustomListItemGroup group)
		{
			CustomFormListItemManager.<CreateListGroupAsync>d__10 <CreateListGroupAsync>d__ = new CustomFormListItemManager.<CreateListGroupAsync>d__10();
			<CreateListGroupAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Guid>.Create();
			<CreateListGroupAsync>d__.<>4__this = this;
			<CreateListGroupAsync>d__.group = group;
			<CreateListGroupAsync>d__.<>1__state = -1;
			<CreateListGroupAsync>d__.<>t__builder.Start<CustomFormListItemManager.<CreateListGroupAsync>d__10>(ref <CreateListGroupAsync>d__);
			return <CreateListGroupAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00053144 File Offset: 0x00051344
		[DebuggerStepThrough]
		public Task<Guid> CreateListItemAsync(Guid customListItemGroupId, CustomListItem item)
		{
			CustomFormListItemManager.<CreateListItemAsync>d__11 <CreateListItemAsync>d__ = new CustomFormListItemManager.<CreateListItemAsync>d__11();
			<CreateListItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Guid>.Create();
			<CreateListItemAsync>d__.<>4__this = this;
			<CreateListItemAsync>d__.customListItemGroupId = customListItemGroupId;
			<CreateListItemAsync>d__.item = item;
			<CreateListItemAsync>d__.<>1__state = -1;
			<CreateListItemAsync>d__.<>t__builder.Start<CustomFormListItemManager.<CreateListItemAsync>d__11>(ref <CreateListItemAsync>d__);
			return <CreateListItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00053198 File Offset: 0x00051398
		[DebuggerStepThrough]
		public Task UpdateListItemAsync(CustomListItem item)
		{
			CustomFormListItemManager.<UpdateListItemAsync>d__12 <UpdateListItemAsync>d__ = new CustomFormListItemManager.<UpdateListItemAsync>d__12();
			<UpdateListItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateListItemAsync>d__.<>4__this = this;
			<UpdateListItemAsync>d__.item = item;
			<UpdateListItemAsync>d__.<>1__state = -1;
			<UpdateListItemAsync>d__.<>t__builder.Start<CustomFormListItemManager.<UpdateListItemAsync>d__12>(ref <UpdateListItemAsync>d__);
			return <UpdateListItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x000531E4 File Offset: 0x000513E4
		[DebuggerStepThrough]
		public Task UpdateListItemGroupAsync(CustomListItemGroup group)
		{
			CustomFormListItemManager.<UpdateListItemGroupAsync>d__13 <UpdateListItemGroupAsync>d__ = new CustomFormListItemManager.<UpdateListItemGroupAsync>d__13();
			<UpdateListItemGroupAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateListItemGroupAsync>d__.<>4__this = this;
			<UpdateListItemGroupAsync>d__.group = group;
			<UpdateListItemGroupAsync>d__.<>1__state = -1;
			<UpdateListItemGroupAsync>d__.<>t__builder.Start<CustomFormListItemManager.<UpdateListItemGroupAsync>d__13>(ref <UpdateListItemGroupAsync>d__);
			return <UpdateListItemGroupAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00053230 File Offset: 0x00051430
		[DebuggerStepThrough]
		public Task EnableOrDisableListItemAsync(Guid CustomListItemId, bool enable)
		{
			CustomFormListItemManager.<EnableOrDisableListItemAsync>d__14 <EnableOrDisableListItemAsync>d__ = new CustomFormListItemManager.<EnableOrDisableListItemAsync>d__14();
			<EnableOrDisableListItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<EnableOrDisableListItemAsync>d__.<>4__this = this;
			<EnableOrDisableListItemAsync>d__.CustomListItemId = CustomListItemId;
			<EnableOrDisableListItemAsync>d__.enable = enable;
			<EnableOrDisableListItemAsync>d__.<>1__state = -1;
			<EnableOrDisableListItemAsync>d__.<>t__builder.Start<CustomFormListItemManager.<EnableOrDisableListItemAsync>d__14>(ref <EnableOrDisableListItemAsync>d__);
			return <EnableOrDisableListItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x00053284 File Offset: 0x00051484
		[DebuggerStepThrough]
		public Task EnableOrDisableListItemGroupAsync(Guid customListItemGroupId, bool enable)
		{
			CustomFormListItemManager.<EnableOrDisableListItemGroupAsync>d__15 <EnableOrDisableListItemGroupAsync>d__ = new CustomFormListItemManager.<EnableOrDisableListItemGroupAsync>d__15();
			<EnableOrDisableListItemGroupAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<EnableOrDisableListItemGroupAsync>d__.<>4__this = this;
			<EnableOrDisableListItemGroupAsync>d__.customListItemGroupId = customListItemGroupId;
			<EnableOrDisableListItemGroupAsync>d__.enable = enable;
			<EnableOrDisableListItemGroupAsync>d__.<>1__state = -1;
			<EnableOrDisableListItemGroupAsync>d__.<>t__builder.Start<CustomFormListItemManager.<EnableOrDisableListItemGroupAsync>d__15>(ref <EnableOrDisableListItemGroupAsync>d__);
			return <EnableOrDisableListItemGroupAsync>d__.<>t__builder.Task;
		}
	}
}
