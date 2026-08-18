using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.Files;

namespace TechnoPro.Common.ICore.DynamicForms
{
	// Token: 0x0200009D RID: 157
	public interface IDynamicFormManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600049C RID: 1180
		DynamicForm LoadDynamicFormById(int ScreenNum);

		// Token: 0x0600049D RID: 1181
		Task<DynamicForm> LoadDynamicFormByIdAsync(int ScreenNum);

		// Token: 0x0600049E RID: 1182
		IList<DynamicForm> LoadActiveFormsByFormType(params eDynamicFormType[] FormTypes);

		// Token: 0x0600049F RID: 1183
		IList<DynamicFormWithExtendedInfo> LoadActiveFormsWithExtendedInfo();

		// Token: 0x060004A0 RID: 1184
		IList<DynamicForm> GetScreensAStudentHasDataOn(int PersonId);

		// Token: 0x060004A1 RID: 1185
		IList<DynamicForm> FindFormByTitleSubstringMatch(string SubstringToMatch, bool SearchPrimaryTitle, bool SearchSecondaryTitle);

		// Token: 0x060004A2 RID: 1186
		Forest<DynamicFormOrGroupOrFormType> LoadAllForms();

		// Token: 0x060004A3 RID: 1187
		IList<DynamicForm> LoadDynamicFormsByIds(params int[] ScreenNums);

		// Token: 0x060004A4 RID: 1188
		IList<BinaryFile> ExportFormsToXml(params int[] ScreenNums);

		// Token: 0x060004A5 RID: 1189
		void ImportFormFromXml(string xml, int ScreenNumToImportControlsInto);

		// Token: 0x060004A6 RID: 1190
		IList<DynamicFormWithExtendedInfo> LoadFormsWithExtendedInfoByScreenNums(params int[] ScreenNums);

		// Token: 0x060004A7 RID: 1191
		int CreateForm(DynamicFormWithExtendedInfo Form);

		// Token: 0x060004A8 RID: 1192
		void UpdateForm(DynamicFormWithExtendedInfo Form);

		// Token: 0x060004A9 RID: 1193
		bool DeleteForm(int ScreenNum);

		// Token: 0x060004AA RID: 1194
		int DoesFormExist(string UniqueId);

		// Token: 0x060004AB RID: 1195
		string ExportFormsWithFieldsToXmlNew(bool IncludeFullXmlDeclaration = false, params int[] ScreenNums);

		// Token: 0x060004AC RID: 1196
		IList<DynamicFormWithFields> ImportFormsFromXmlNew(string xml, bool writeToDatabase = false);

		// Token: 0x060004AD RID: 1197
		IDictionary<int, string> LoadScreenUniqueIdsByScreenNums(params int[] ScreenNums);

		// Token: 0x060004AE RID: 1198
		IList<int> FindScreensAControlExistsOn(int ControlId);

		// Token: 0x060004AF RID: 1199
		Task<IList<int>> FindScreensAControlExistsOnAsync(int ControlId);

		// Token: 0x060004B0 RID: 1200
		IList<int> LoadControlIdsForScreenInOrder(int ScreenNum, bool RemoveNonDataHoldingControls);

		// Token: 0x060004B1 RID: 1201
		IDictionary<int, IList<int>> FindScreensControlIdsExistOn(IList<int> ControlIds, out IList<DynamicForm> Screens);
	}
}
