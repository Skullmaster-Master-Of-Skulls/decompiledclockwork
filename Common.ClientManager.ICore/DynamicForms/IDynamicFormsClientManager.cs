using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.ClientManager.ICore.DynamicForms
{
	// Token: 0x02000060 RID: 96
	public interface IDynamicFormsClientManager : IWebService
	{
		// Token: 0x060002E8 RID: 744
		DynamicFormDTO LoadDynamicFormById(int ScreenNum);

		// Token: 0x060002E9 RID: 745
		IList<DynamicFormDTO> FindFormByTitleSubstringMatch(string SubstringToMatch, bool SearchPrimaryTitle, bool SearchSecondaryTitle);

		// Token: 0x060002EA RID: 746
		Forest<DynamicFormOrGroupOrFormTypeDTO> LoadAllForms();

		// Token: 0x060002EB RID: 747
		IList<DynamicFormDTO> LoadActiveFormsByFormType(params eDynamicFormType[] FormTypes);

		// Token: 0x060002EC RID: 748
		IList<BinaryFileDTO> ExportFormsToXml(params int[] ScreenNum);

		// Token: 0x060002ED RID: 749
		void ImportFormFromXml(string xml, int ScreenNumToImportControlsInto);

		// Token: 0x060002EE RID: 750
		IList<DynamicFormWithExtendedInfoDTO> LoadFormsWithExtendedInfoByScreenNums(params int[] ScreenNums);

		// Token: 0x060002EF RID: 751
		IList<DynamicFormDTO> LoadFormsByScreenNums(params int[] ScreenNums);

		// Token: 0x060002F0 RID: 752
		int CreateForm(DynamicFormWithExtendedInfoDTO Form);

		// Token: 0x060002F1 RID: 753
		void UpdateForm(DynamicFormWithExtendedInfoDTO Form);

		// Token: 0x060002F2 RID: 754
		bool DeleteForm(int ScreenNum);

		// Token: 0x060002F3 RID: 755
		IList<int> FindScreensAControlExistsOn(int ControlId);
	}
}
