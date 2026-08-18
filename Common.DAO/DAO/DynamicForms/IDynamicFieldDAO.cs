using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.DAO.DynamicForms
{
	// Token: 0x02000082 RID: 130
	public interface IDynamicFieldDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600034D RID: 845
		List<DynamicField> LoadFields(int screenNum);

		// Token: 0x0600034E RID: 846
		List<DynamicField> LoadFieldsByControlIds(List<int> ControlIds);

		// Token: 0x0600034F RID: 847
		Task<List<DynamicField>> LoadFieldsByControlIdsAsync(List<int> ControlIds);

		// Token: 0x06000350 RID: 848
		DynamicField SearchForField(string ControlCaption, int ScreenNum);

		// Token: 0x06000351 RID: 849
		int CreateField(DynamicField Field);

		// Token: 0x06000352 RID: 850
		DynamicField LoadFieldByName(string Name);

		// Token: 0x06000353 RID: 851
		DynamicField LoadFieldByUniqueId(Guid uniqueId);

		// Token: 0x06000354 RID: 852
		List<DynamicListItem> LoadListItems(int LookupGroupId);

		// Token: 0x06000355 RID: 853
		Task<List<DynamicListItem>> LoadListItemsAsync(int LookupGroupId);

		// Token: 0x06000356 RID: 854
		void UpdateFieldName(int ControlId, string NewName);

		// Token: 0x06000357 RID: 855
		IList<DynamicFormOrGroupOrField> LoadFormsWithGroupsAndFields(bool ExcludeNonDataHoldingControls, params int[] ScreenNumsToExclude);

		// Token: 0x06000358 RID: 856
		IDictionary<int, ExtendedAccommodationInfo> LoadAccommodationShortCodes(params int[] ControlIds);

		// Token: 0x06000359 RID: 857
		int CreateFieldOnForm(DynamicFieldOnForm FieldOnForm);

		// Token: 0x0600035A RID: 858
		DynamicField GetFirstFieldOnFirstPerAppointmentForm(int AppTypeId, eControlCode FieldType);

		// Token: 0x0600035B RID: 859
		bool IsListItemSavedSomewhere(int LookupListId);

		// Token: 0x0600035C RID: 860
		IList<DynamicListGroup> LoadAllLookupLists();

		// Token: 0x0600035D RID: 861
		int CreateList(DynamicListGroup listGroup, IList<DynamicListItem> listItems);

		// Token: 0x0600035E RID: 862
		IDictionary<int, IList<int>> LoadControlIdsByForms(params int[] screenNums);

		// Token: 0x0600035F RID: 863
		Task<IDictionary<int, IList<int>>> LoadControlIdsByFormsAsync(params int[] screenNums);
	}
}
