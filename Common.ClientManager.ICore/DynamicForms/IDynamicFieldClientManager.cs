using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.DynamicForms
{
	// Token: 0x0200005F RID: 95
	public interface IDynamicFieldClientManager : IWebService
	{
		// Token: 0x060002D6 RID: 726
		Forest<DynamicFieldDTO> LoadFieldsAsTree(DynamicFormDTO Form, out List<DynamicFieldDTO> Fields);

		// Token: 0x060002D7 RID: 727
		DynamicFieldDTO LoadFieldByControlId(int ControlId);

		// Token: 0x060002D8 RID: 728
		IList<DynamicFieldDTO> LoadFieldsByControlIds(List<int> ControlIds);

		// Token: 0x060002D9 RID: 729
		IList<DynamicFieldDTO> LoadFieldsByForm(DynamicFormDTO Form);

		// Token: 0x060002DA RID: 730
		IList<DynamicFieldDTO> LoadFieldsByFormId(int screenNum, bool ignoreCache = true);

		// Token: 0x060002DB RID: 731
		IList<DynamicFieldDTO> LoadFieldsByForm(DynamicFormDTO Form, bool IgnoreCache);

		// Token: 0x060002DC RID: 732
		DynamicFieldDTO LoadFieldByName(string Name);

		// Token: 0x060002DD RID: 733
		int CreateField(DynamicFieldDTO Field);

		// Token: 0x060002DE RID: 734
		IList<DynamicListItemDTO> LoadListItems(int LookupGroupId);

		// Token: 0x060002DF RID: 735
		IList<DynamicFormOrGroupOrFieldDTO> LoadFormsWithControls2(bool ExcludeNonDataHoldingControls, params int[] ScreenNumsToExclude);

		// Token: 0x060002E0 RID: 736
		bool IsListItemSavedSomewhere(int LookupListId);

		// Token: 0x060002E1 RID: 737
		IList<DynamicListGroupDTO> LoadAllLookupLists();

		// Token: 0x060002E2 RID: 738
		IList<string> GetFieldPossibleValues(int ControlId);

		// Token: 0x060002E3 RID: 739
		int CreateList(DynamicListGroupDTO group, IList<DynamicListItemDTO> items);

		// Token: 0x060002E4 RID: 740
		IList<int> CreateFields(int ScreenNum, IList<DynamicFieldDTO> fields);

		// Token: 0x060002E5 RID: 741
		IList<int> LoadControlIdsOnForms(bool ignoreCache, params int[] screenNums);

		// Token: 0x060002E6 RID: 742
		DynamicFieldDTO GetEmailField();

		// Token: 0x060002E7 RID: 743
		IList<PersonBaseDTO> LoadStaffFromStaffDropList(DynamicFieldDTO staffDropListField);
	}
}
