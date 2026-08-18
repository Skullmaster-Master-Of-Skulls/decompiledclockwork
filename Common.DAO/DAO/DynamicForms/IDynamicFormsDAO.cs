using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.DAO.DynamicForms
{
	// Token: 0x02000086 RID: 134
	public interface IDynamicFormsDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600037D RID: 893
		IList<DynamicForm> LoadDynamicFormsByIds(params int[] ScreenNums);

		// Token: 0x0600037E RID: 894
		Task<IList<DynamicForm>> LoadDynamicFormsByIdsAsync(params int[] ScreenNums);

		// Token: 0x0600037F RID: 895
		IList<DynamicForm> LoadActiveFormsByFormType(eDynamicFormType FormType);

		// Token: 0x06000380 RID: 896
		IList<DynamicFormWithExtendedInfo> LoadActiveFormsWithExtendedInfo();

		// Token: 0x06000381 RID: 897
		IList<DynamicForm> GetScreensAStudentHasDataOn(int PersonId);

		// Token: 0x06000382 RID: 898
		IList<DynamicForm> FindFormByTitleSubstringMatch(string SubstringToMatch, bool SearchPrimaryTitle, bool SearchSecondaryTitle);

		// Token: 0x06000383 RID: 899
		IList<DynamicForm> LoadAllForms();

		// Token: 0x06000384 RID: 900
		string ConvertDynamicFormDefinitionToXml(DynamicForm form);

		// Token: 0x06000385 RID: 901
		void ImportFormFromXml(string xml, int ScreenNumToImportControlsInto);

		// Token: 0x06000386 RID: 902
		IList<DynamicFormWithExtendedInfo> LoadFormsWithExtendedInfoByScreenNums(params int[] ScreenNums);

		// Token: 0x06000387 RID: 903
		int CreateForm(DynamicFormWithExtendedInfo Form);

		// Token: 0x06000388 RID: 904
		void UpdateForm(DynamicFormWithExtendedInfo Form);

		// Token: 0x06000389 RID: 905
		bool DeleteForm(int ScreenNum);

		// Token: 0x0600038A RID: 906
		int DoesFormExist(string UniqueId);

		// Token: 0x0600038B RID: 907
		IDictionary<int, string> LoadScreenUniqueIdsByScreenNums(params int[] ScreenNums);

		// Token: 0x0600038C RID: 908
		IList<int> FindScreensAControlExistsOn(int ControlId);

		// Token: 0x0600038D RID: 909
		Task<IList<int>> FindScreensAControlExistsOnAsync(int ControlId);

		// Token: 0x0600038E RID: 910
		IList<int> LoadControlIdsForScreenInOrder(int ScreenNum, bool RemoveNonDataHoldingControls);

		// Token: 0x0600038F RID: 911
		IDictionary<int, IList<int>> FindScreensControlIdsExistOn(IList<int> ControlIds, out IList<DynamicForm> Screens);
	}
}
