using System;
using System.Collections.Generic;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200003E RID: 62
	public class DynamicFieldServiceManager : IDynamicField, IService
	{
		// Token: 0x06000268 RID: 616 RVA: 0x0000C15C File Offset: 0x0000A35C
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000C170 File Offset: 0x0000A370
		public LoadListItemsResp LoadListItems(LoadListItemsReq Request)
		{
			IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(Request.GetOperationContext());
			List<DynamicListItem> list = dynamicFieldManager.LoadListItems(Request.LookupGroupId);
			LoadListItemsResp loadListItemsResp = new LoadListItemsResp();
			loadListItemsResp.Items = list.ConvertAll<DynamicListItemDTO>((DynamicListItem f) => f.ToDTO());
			return loadListItemsResp;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000C1CC File Offset: 0x0000A3CC
		public LoadFieldsByFormResp LoadFieldsByForm(LoadFieldsByFormReq Request)
		{
			IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(Request.GetOperationContext());
			List<DynamicField> list = dynamicFieldManager.LoadFields(Request.Form.ToDomainObject(), Request.IgnoreCache);
			LoadFieldsByFormResp loadFieldsByFormResp = new LoadFieldsByFormResp();
			loadFieldsByFormResp.Fields = list.ConvertAll<DynamicFieldDTO>((DynamicField f) => f.ToDTO());
			return loadFieldsByFormResp;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000C234 File Offset: 0x0000A434
		public LoadFieldsByFormIdResp LoadFieldsByFormId(LoadFieldsByFormIdReq Request)
		{
			IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(Request.GetOperationContext());
			List<DynamicField> list = dynamicFieldManager.LoadFields(Request.ScreenNum, Request.IgnoreCache);
			LoadFieldsByFormIdResp loadFieldsByFormIdResp = new LoadFieldsByFormIdResp();
			loadFieldsByFormIdResp.Fields = list.ConvertAll<DynamicFieldDTO>((DynamicField f) => f.ToDTO());
			return loadFieldsByFormIdResp;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000C298 File Offset: 0x0000A498
		public LoadFieldsByControlIdsResp LoadFieldsByControlIds(LoadFieldsByControlIdsReq Request)
		{
			IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(Request.GetOperationContext());
			List<DynamicField> list = dynamicFieldManager.LoadFieldsByControlIds(Request.ControlIds);
			LoadFieldsByControlIdsResp loadFieldsByControlIdsResp = new LoadFieldsByControlIdsResp();
			loadFieldsByControlIdsResp.Fields = list.ConvertAll<DynamicFieldDTO>((DynamicField f) => f.ToDTO());
			return loadFieldsByControlIdsResp;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000C2F4 File Offset: 0x0000A4F4
		public LoadFieldByNameResp LoadFieldByName(LoadFieldByNameReq Request)
		{
			IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(Request.GetOperationContext());
			DynamicField dynamicField = dynamicFieldManager.LoadFieldByName(Request.Name);
			return new LoadFieldByNameResp
			{
				Field = dynamicField.ToDTO()
			};
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000C334 File Offset: 0x0000A534
		public CreateFieldResp CreateField(CreateFieldReq Request)
		{
			IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(Request.GetOperationContext());
			int controlId = dynamicFieldManager.CreateField(Request.Field.ToDomainObject());
			return new CreateFieldResp
			{
				ControlId = controlId
			};
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000C374 File Offset: 0x0000A574
		public LoadFieldsAsTreeResp LoadFieldsAsTree(LoadFieldsAsTreeReq Request)
		{
			IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(Request.GetOperationContext());
			List<DynamicField> list;
			Forest<DynamicField> item = dynamicFieldManager.LoadFieldsAsTree(Request.Form.ToDomainObject(), out list);
			LoadFieldsAsTreeResp loadFieldsAsTreeResp = new LoadFieldsAsTreeResp();
			List<DynamicFieldDTO> fields;
			if (list == null)
			{
				fields = null;
			}
			else
			{
				fields = list.ConvertAll<DynamicFieldDTO>((DynamicField f) => f.ToDTO());
			}
			loadFieldsAsTreeResp.Fields = fields;
			loadFieldsAsTreeResp.Tree = item.ToDTO();
			return loadFieldsAsTreeResp;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000C3EC File Offset: 0x0000A5EC
		public LoadFormsWithControls2Resp LoadFormsWithControls2(LoadFormsWithControls2Req Request)
		{
			CWLogger.Logger.Trace("LoadFormsWithControls:Start");
			IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(Request.GetOperationContext());
			IDynamicFieldManager dynamicFieldManager2 = dynamicFieldManager;
			bool excludeNonDataHoldingControls = Request.ExcludeNonDataHoldingControls;
			IList<int> screenNumsToExclude = Request.ScreenNumsToExclude;
			IList<DynamicFormOrGroupOrField> list = dynamicFieldManager2.LoadFormsWithControls(excludeNonDataHoldingControls, (screenNumsToExclude != null) ? screenNumsToExclude.ToArray<int>() : null);
			LoadFormsWithControls2Resp loadFormsWithControls2Resp = new LoadFormsWithControls2Resp();
			IList<DynamicFormOrGroupOrFieldDTO> formsWithControls;
			if (list == null)
			{
				formsWithControls = null;
			}
			else
			{
				formsWithControls = list.ToList<DynamicFormOrGroupOrField>().ConvertAll<DynamicFormOrGroupOrFieldDTO>((DynamicFormOrGroupOrField g) => g.ToDTO());
			}
			loadFormsWithControls2Resp.FormsWithControls = formsWithControls;
			return loadFormsWithControls2Resp;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000C478 File Offset: 0x0000A678
		public GetEmailFieldResp GetEmailField(GetEmailFieldReq Request)
		{
			IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(Request.GetOperationContext());
			DynamicField emailField = dynamicFieldManager.GetEmailField();
			return new GetEmailFieldResp
			{
				EmailField = ((emailField != null) ? emailField.ToDTO() : null)
			};
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000C4B8 File Offset: 0x0000A6B8
		public IsListItemSavedSomewhereResp IsListItemSavedSomewhere(IsListItemSavedSomewhereReq Request)
		{
			IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(Request.GetOperationContext());
			return new IsListItemSavedSomewhereResp
			{
				DataExistsWithThisLookupListId = dynamicFieldManager.IsListItemSavedSomewhere(Request.LookupListId)
			};
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000C4F0 File Offset: 0x0000A6F0
		public LoadAllLookupListsResp LoadAllLookupLists(LoadAllLookupListsReq Request)
		{
			IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(Request.GetOperationContext());
			IList<DynamicListGroup> list = dynamicFieldManager.LoadAllLookupLists();
			LoadAllLookupListsResp loadAllLookupListsResp = new LoadAllLookupListsResp();
			IList<DynamicListGroupDTO> lookupGroups;
			if (list == null)
			{
				lookupGroups = null;
			}
			else
			{
				lookupGroups = (from g in list
				select g.ToDTO()).ToList<DynamicListGroupDTO>();
			}
			loadAllLookupListsResp.LookupGroups = lookupGroups;
			return loadAllLookupListsResp;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000C554 File Offset: 0x0000A754
		public GetFieldPossibleValuesResp GetFieldPossibleValues(GetFieldPossibleValuesReq Request)
		{
			IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(Request.GetOperationContext());
			IList<string> fieldPossibleValues = dynamicFieldManager.GetFieldPossibleValues(Request.ControlId);
			return new GetFieldPossibleValuesResp
			{
				Values = fieldPossibleValues
			};
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000C58C File Offset: 0x0000A78C
		public CreateListResp CreateList(CreateListReq Request)
		{
			IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(Request.GetOperationContext());
			IDynamicFieldManager dynamicFieldManager2 = dynamicFieldManager;
			DynamicListGroupDTO group = Request.Group;
			DynamicListGroup listGroup = (group != null) ? group.ToDomainObject() : null;
			IList<DynamicListItem> listItems;
			if (Request.ListItems != null)
			{
				listItems = (from g in Request.ListItems
				select g.ToDomainObject()).ToList<DynamicListItem>();
			}
			else
			{
				listItems = null;
			}
			int lookupGroupId = dynamicFieldManager2.CreateList(listGroup, listItems);
			return new CreateListResp
			{
				LookupGroupId = lookupGroupId
			};
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000C60C File Offset: 0x0000A80C
		public CreateFieldsResp CreateFields(CreateFieldsReq Request)
		{
			IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(Request.GetOperationContext());
			IDynamicFieldManager dynamicFieldManager2 = dynamicFieldManager;
			int screenNum = Request.ScreenNum;
			IList<DynamicFieldDTO> fields = Request.Fields;
			IList<DynamicField> fields2;
			if (fields == null)
			{
				fields2 = null;
			}
			else
			{
				fields2 = (from g in fields
				select g.ToDomainObject()).ToList<DynamicField>();
			}
			IList<int> controlIds = dynamicFieldManager2.CreateFields(screenNum, fields2);
			return new CreateFieldsResp
			{
				ControlIds = controlIds
			};
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000C67C File Offset: 0x0000A87C
		public LoadControlIdsOnFormsResp LoadControlIdsOnForms(LoadControlIdsOnFormsReq Request)
		{
			IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(Request.GetOperationContext());
			IList<int> controlIds = dynamicFieldManager.LoadControlIdsOnForms(Request.IgnoreCache, Request.ScreenNums.ToArray<int>());
			return new LoadControlIdsOnFormsResp
			{
				ControlIds = controlIds
			};
		}
	}
}
