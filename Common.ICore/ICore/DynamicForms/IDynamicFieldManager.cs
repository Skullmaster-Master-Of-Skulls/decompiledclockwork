using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.ICore.DynamicForms
{
	// Token: 0x0200009A RID: 154
	public interface IDynamicFieldManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600046D RID: 1133
		Task<IList<int>> LoadControlIdsOnFormsAsync(bool ignoreCache, params int[] screenNums);

		// Token: 0x0600046E RID: 1134
		Task<List<DynamicField>> LoadFieldsByControlIdsAsync(List<int> ControlIds);

		// Token: 0x0600046F RID: 1135
		List<DynamicField> LoadFieldsByControlIds(List<int> ControlIds);

		// Token: 0x06000470 RID: 1136
		DynamicField LoadFieldByControlId(int ControlId);

		// Token: 0x06000471 RID: 1137
		Task<DynamicField> LoadFieldByControlIdAsync(int ControlId);

		// Token: 0x06000472 RID: 1138
		DynamicField LoadFieldByUniqueId(Guid uniqueId);

		// Token: 0x06000473 RID: 1139
		List<DynamicField> LoadFields(DynamicForm Form);

		// Token: 0x06000474 RID: 1140
		Forest<DynamicField> LoadFieldsAsTree(DynamicForm Form, out List<DynamicField> Fields);

		// Token: 0x06000475 RID: 1141
		int CreateField(DynamicField Field);

		// Token: 0x06000476 RID: 1142
		DynamicField LoadFieldByName(string Name);

		// Token: 0x06000477 RID: 1143
		DynamicField GetEmailField();

		// Token: 0x06000478 RID: 1144
		List<DynamicListItem> LoadListItems(int LookupGroupId);

		// Token: 0x06000479 RID: 1145
		Task<List<DynamicListItem>> LoadListItemsAsync(int LookupGroupId);

		// Token: 0x0600047A RID: 1146
		void UpdateFieldName(int ControlId, string NewName);

		// Token: 0x0600047B RID: 1147
		IList<DynamicFormOrGroupOrField> LoadFormsWithControls(bool ExcludeNonDataHoldingControls, params int[] ScreenNumsToExclude);

		// Token: 0x0600047C RID: 1148
		IList<DynamicFormMigrationInfo> LoadDynamicFormMigrationInfo(params int[] ScreenNums);

		// Token: 0x0600047D RID: 1149
		IDictionary<int, ExtendedAccommodationInfo> LoadAccommodationShortCodes(params int[] ControlIds);

		// Token: 0x0600047E RID: 1150
		IList<int> CreateFields(int ScreenNum, IList<DynamicField> Fields);

		// Token: 0x0600047F RID: 1151
		List<DynamicField> LoadFields(DynamicForm Form, bool IgnoreCache);

		// Token: 0x06000480 RID: 1152
		List<DynamicField> LoadFields(int screenNum, bool IgnoreCache);

		// Token: 0x06000481 RID: 1153
		IList<int> LoadControlIdsOnForms(bool ignoreCache, params int[] screenNums);

		// Token: 0x06000482 RID: 1154
		DynamicField GetFirstFieldOnFirstPerAppointmentForm(int AppTypeId, eControlCode FieldType);

		// Token: 0x06000483 RID: 1155
		bool IsListItemSavedSomewhere(int LookupListId);

		// Token: 0x06000484 RID: 1156
		IList<DynamicListGroup> LoadAllLookupLists();

		// Token: 0x06000485 RID: 1157
		IDictionary<string, Type> LoadListViewOrFileListColumns(int ControlId);

		// Token: 0x06000486 RID: 1158
		IList<string> GetFieldPossibleValues(int ControlId);

		// Token: 0x06000487 RID: 1159
		int CreateList(DynamicListGroup listGroup, IList<DynamicListItem> listItems);
	}
}
