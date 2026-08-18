using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.ClientManager.Core.People;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.DynamicForms
{
	// Token: 0x02000066 RID: 102
	public class DynamicFieldClientManager : IDynamicFieldClientManager, IWebService
	{
		// Token: 0x060003B4 RID: 948 RVA: 0x00010DE4 File Offset: 0x0000EFE4
		public Forest<DynamicFieldDTO> LoadFieldsAsTree(DynamicFormDTO Form, out List<DynamicFieldDTO> Fields)
		{
			LoadFieldsAsTreeReq loadFieldsAsTreeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadFieldsAsTreeReq>();
			loadFieldsAsTreeReq.Form = Form;
			LoadFieldsAsTreeResp loadFieldsAsTreeResp = ClientServiceFactory.GetClientInstance<IDynamicField>().LoadFieldsAsTree(loadFieldsAsTreeReq);
			Fields = loadFieldsAsTreeResp.Fields;
			return loadFieldsAsTreeResp.Tree;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00010E24 File Offset: 0x0000F024
		public DynamicFieldDTO LoadFieldByControlId(int ControlId)
		{
			IList<DynamicFieldDTO> list = this.LoadFieldsByControlIds(new List<int>
			{
				ControlId
			});
			return (list == null || list.Count < 1) ? null : list[0];
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00010E60 File Offset: 0x0000F060
		public IList<DynamicFieldDTO> LoadFieldsByControlIds(List<int> ControlIds)
		{
			LoadFieldsByControlIdsReq loadFieldsByControlIdsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadFieldsByControlIdsReq>();
			loadFieldsByControlIdsReq.ControlIds = ControlIds;
			return ClientServiceFactory.GetClientInstance<IDynamicField>().LoadFieldsByControlIds(loadFieldsByControlIdsReq).Fields;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00010E98 File Offset: 0x0000F098
		public IList<DynamicFieldDTO> LoadFieldsByForm(DynamicFormDTO Form)
		{
			return this.LoadFieldsByForm(Form, false);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00010EB4 File Offset: 0x0000F0B4
		public IList<DynamicFieldDTO> LoadFieldsByFormId(int screenNum, bool ignoreCache = true)
		{
			LoadFieldsByFormIdReq loadFieldsByFormIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadFieldsByFormIdReq>();
			loadFieldsByFormIdReq.ScreenNum = screenNum;
			loadFieldsByFormIdReq.IgnoreCache = ignoreCache;
			return ClientServiceFactory.GetClientInstance<IDynamicField>().LoadFieldsByFormId(loadFieldsByFormIdReq).Fields;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00010EF4 File Offset: 0x0000F0F4
		public IList<DynamicFieldDTO> LoadFieldsByForm(DynamicFormDTO Form, bool IgnoreCache)
		{
			LoadFieldsByFormReq loadFieldsByFormReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadFieldsByFormReq>();
			loadFieldsByFormReq.Form = Form;
			loadFieldsByFormReq.IgnoreCache = IgnoreCache;
			return ClientServiceFactory.GetClientInstance<IDynamicField>().LoadFieldsByForm(loadFieldsByFormReq).Fields;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00010F34 File Offset: 0x0000F134
		public DynamicFieldDTO LoadFieldByName(string Name)
		{
			LoadFieldByNameReq loadFieldByNameReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadFieldByNameReq>();
			loadFieldByNameReq.Name = Name;
			return ClientServiceFactory.GetClientInstance<IDynamicField>().LoadFieldByName(loadFieldByNameReq).Field;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00010F6C File Offset: 0x0000F16C
		public int CreateField(DynamicFieldDTO Field)
		{
			CreateFieldReq createFieldReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateFieldReq>();
			createFieldReq.Field = Field;
			return ClientServiceFactory.GetClientInstance<IDynamicField>().CreateField(createFieldReq).ControlId;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00010FA4 File Offset: 0x0000F1A4
		public IList<DynamicListItemDTO> LoadListItems(int LookupGroupId)
		{
			LoadListItemsReq loadListItemsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadListItemsReq>();
			loadListItemsReq.LookupGroupId = LookupGroupId;
			return ClientServiceFactory.GetClientInstance<IDynamicField>().LoadListItems(loadListItemsReq).Items;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00010FDC File Offset: 0x0000F1DC
		public IList<DynamicFormOrGroupOrFieldDTO> LoadFormsWithControls2(bool ExcludeNonDataHoldingControls, params int[] ScreenNumsToExclude)
		{
			LoadFormsWithControls2Req loadFormsWithControls2Req = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadFormsWithControls2Req>();
			loadFormsWithControls2Req.ExcludeNonDataHoldingControls = ExcludeNonDataHoldingControls;
			loadFormsWithControls2Req.ScreenNumsToExclude = ScreenNumsToExclude;
			return ClientServiceFactory.GetClientInstance<IDynamicField>().LoadFormsWithControls2(loadFormsWithControls2Req).FormsWithControls;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0001101C File Offset: 0x0000F21C
		public bool IsListItemSavedSomewhere(int LookupListId)
		{
			IsListItemSavedSomewhereReq isListItemSavedSomewhereReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<IsListItemSavedSomewhereReq>();
			isListItemSavedSomewhereReq.LookupListId = LookupListId;
			return ClientServiceFactory.GetClientInstance<IDynamicField>().IsListItemSavedSomewhere(isListItemSavedSomewhereReq).DataExistsWithThisLookupListId;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00011054 File Offset: 0x0000F254
		public IList<DynamicListGroupDTO> LoadAllLookupLists()
		{
			LoadAllLookupListsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllLookupListsReq>();
			return ClientServiceFactory.GetClientInstance<IDynamicField>().LoadAllLookupLists(request).LookupGroups;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00011084 File Offset: 0x0000F284
		public IList<string> GetFieldPossibleValues(int ControlId)
		{
			GetFieldPossibleValuesReq getFieldPossibleValuesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetFieldPossibleValuesReq>();
			getFieldPossibleValuesReq.ControlId = ControlId;
			return ClientServiceFactory.GetClientInstance<IDynamicField>().GetFieldPossibleValues(getFieldPossibleValuesReq).Values;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x000110BC File Offset: 0x0000F2BC
		public int CreateList(DynamicListGroupDTO group, IList<DynamicListItemDTO> items)
		{
			CreateListReq createListReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateListReq>();
			createListReq.Group = group;
			createListReq.ListItems = items;
			return ClientServiceFactory.GetClientInstance<IDynamicField>().CreateList(createListReq).LookupGroupId;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000110FC File Offset: 0x0000F2FC
		public IList<int> CreateFields(int ScreenNum, IList<DynamicFieldDTO> fields)
		{
			CreateFieldsReq createFieldsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateFieldsReq>();
			createFieldsReq.ScreenNum = ScreenNum;
			createFieldsReq.Fields = fields;
			return ClientServiceFactory.GetClientInstance<IDynamicField>().CreateFields(createFieldsReq).ControlIds;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0001113C File Offset: 0x0000F33C
		public IList<int> LoadControlIdsOnForms(bool ignoreCache, params int[] screenNums)
		{
			LoadControlIdsOnFormsReq loadControlIdsOnFormsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadControlIdsOnFormsReq>();
			loadControlIdsOnFormsReq.IgnoreCache = ignoreCache;
			loadControlIdsOnFormsReq.ScreenNums = screenNums;
			return ClientServiceFactory.GetClientInstance<IDynamicField>().LoadControlIdsOnForms(loadControlIdsOnFormsReq).ControlIds;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0001117C File Offset: 0x0000F37C
		public DynamicFieldDTO GetEmailField()
		{
			string key = "emailField";
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			DynamicFieldDTO dynamicFieldDTO = (DynamicFieldDTO)clientCache[key];
			bool flag = dynamicFieldDTO == null;
			if (flag)
			{
				GetEmailFieldReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetEmailFieldReq>();
				dynamicFieldDTO = ClientServiceFactory.GetClientInstance<IDynamicField>().GetEmailField(request).EmailField;
				bool flag2 = dynamicFieldDTO != null;
				if (flag2)
				{
					clientCache.Insert(key, dynamicFieldDTO, TimeSpan.FromMinutes(60.0));
				}
			}
			return dynamicFieldDTO;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x000111F4 File Offset: 0x0000F3F4
		public IList<PersonBaseDTO> LoadStaffFromStaffDropList(DynamicFieldDTO staffDropListField)
		{
			bool flag = ((staffDropListField != null) ? staffDropListField.ControlCode : eControlCode.Unknown) != eControlCode.StaffComboBox;
			if (flag)
			{
				throw new Exception("Control is not a staff drop list: " + ((staffDropListField != null) ? staffDropListField.ControlCode.ToString() : null));
			}
			int num = (staffDropListField != null) ? staffDropListField.Setting1 : 0;
			bool flag2 = num < 1;
			if (flag2)
			{
				num = 2;
			}
			IPersonBaseClientManager personBaseClientManager = new PersonBaseClientManager();
			List<PersonBaseDTO> list = personBaseClientManager.LoadGroupMembers(num).ToList<PersonBaseDTO>();
			list.Sort(delegate(PersonBaseDTO g1, PersonBaseDTO g2)
			{
				int num2 = (g1.LastName ?? "").CompareTo(g2.LastName ?? "");
				bool flag3 = num2 != 0;
				int result;
				if (flag3)
				{
					result = num2;
				}
				else
				{
					num2 = (g1.FirstName ?? "").CompareTo(g2.LastName ?? "");
					result = num2;
				}
				return result;
			});
			return list;
		}
	}
}
