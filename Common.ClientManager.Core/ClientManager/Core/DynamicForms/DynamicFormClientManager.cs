using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.DynamicForms
{
	// Token: 0x02000067 RID: 103
	public class DynamicFormClientManager : IDynamicFormsClientManager, IWebService
	{
		// Token: 0x060003C7 RID: 967 RVA: 0x000112A0 File Offset: 0x0000F4A0
		public DynamicFormDTO LoadDynamicFormById(int ScreenNum)
		{
			LoadDynamicFormByIdReq loadDynamicFormByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadDynamicFormByIdReq>();
			loadDynamicFormByIdReq.ScreenNum = ScreenNum;
			return ClientServiceFactory.GetClientInstance<IDynamicForm>().LoadDynamicFormById(loadDynamicFormByIdReq).DynamicForm;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000112D8 File Offset: 0x0000F4D8
		public IList<DynamicFormDTO> FindFormByTitleSubstringMatch(string SubstringToMatch, bool SearchPrimaryTitle, bool SearchSecondaryTitle)
		{
			FindFormByTitleSubstringMatchReq findFormByTitleSubstringMatchReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<FindFormByTitleSubstringMatchReq>();
			findFormByTitleSubstringMatchReq.SubstringToMatch = SubstringToMatch;
			findFormByTitleSubstringMatchReq.SearchPrimaryTitle = SearchPrimaryTitle;
			findFormByTitleSubstringMatchReq.SearchSecondaryTitle = SearchSecondaryTitle;
			return ClientServiceFactory.GetClientInstance<IDynamicForm>().FindFormByTitleSubstringMatch(findFormByTitleSubstringMatchReq).MatchingForms;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00011320 File Offset: 0x0000F520
		public Forest<DynamicFormOrGroupOrFormTypeDTO> LoadAllForms()
		{
			LoadAllFormsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllFormsReq>();
			return ClientServiceFactory.GetClientInstance<IDynamicForm>().LoadAllForms(request).Forms;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00011350 File Offset: 0x0000F550
		public IList<DynamicFormDTO> LoadActiveFormsByFormType(params eDynamicFormType[] FormTypes)
		{
			LoadActiveFormsByFormTypeReq loadActiveFormsByFormTypeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadActiveFormsByFormTypeReq>();
			LoadActiveFormsByFormTypeReq loadActiveFormsByFormTypeReq2 = loadActiveFormsByFormTypeReq;
			eDynamicFormTypeDTO[] formTypes;
			if (FormTypes == null)
			{
				formTypes = null;
			}
			else
			{
				formTypes = (from g in FormTypes
				select (eDynamicFormTypeDTO)g).ToArray<eDynamicFormTypeDTO>();
			}
			loadActiveFormsByFormTypeReq2.FormTypes = formTypes;
			return ClientServiceFactory.GetClientInstance<IDynamicForm>().LoadActiveFormsByFormType(loadActiveFormsByFormTypeReq).Forms;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000113B4 File Offset: 0x0000F5B4
		public IList<BinaryFileDTO> ExportFormsToXml(params int[] ScreenNum)
		{
			ExportFormsToXmlReq exportFormsToXmlReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ExportFormsToXmlReq>();
			exportFormsToXmlReq.ScreenNums = ScreenNum;
			return ClientServiceFactory.GetClientInstance<IDynamicForm>().ExportFormsToXml(exportFormsToXmlReq).XmlFiles;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000113EC File Offset: 0x0000F5EC
		public void ImportFormFromXml(string xml, int ScreenNumToImportControlsInto)
		{
			ImportFormFromXmlReq importFormFromXmlReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ImportFormFromXmlReq>();
			importFormFromXmlReq.Xml = xml;
			importFormFromXmlReq.ScreenNumToImportControlsInto = ScreenNumToImportControlsInto;
			ClientServiceFactory.GetClientInstance<IDynamicForm>().ImportFormFromXml(importFormFromXmlReq);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00011424 File Offset: 0x0000F624
		public IList<DynamicFormWithExtendedInfoDTO> LoadFormsWithExtendedInfoByScreenNums(params int[] ScreenNums)
		{
			LoadFormsWithExtendedInfoByScreenNumsReq loadFormsWithExtendedInfoByScreenNumsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadFormsWithExtendedInfoByScreenNumsReq>();
			loadFormsWithExtendedInfoByScreenNumsReq.ScreenNums = ScreenNums;
			return ClientServiceFactory.GetClientInstance<IDynamicForm>().LoadFormsWithExtendedInfoByScreenNums(loadFormsWithExtendedInfoByScreenNumsReq).Forms;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0001145C File Offset: 0x0000F65C
		public int CreateForm(DynamicFormWithExtendedInfoDTO Form)
		{
			CreateFormReq createFormReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateFormReq>();
			createFormReq.Form = Form;
			return ClientServiceFactory.GetClientInstance<IDynamicForm>().CreateForm(createFormReq).ScreenNum;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00011494 File Offset: 0x0000F694
		public void UpdateForm(DynamicFormWithExtendedInfoDTO Form)
		{
			UpdateFormReq updateFormReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateFormReq>();
			updateFormReq.Form = Form;
			ClientServiceFactory.GetClientInstance<IDynamicForm>().UpdateForm(updateFormReq);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x000114C4 File Offset: 0x0000F6C4
		public bool DeleteForm(int ScreenNum)
		{
			DeleteFormReq deleteFormReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteFormReq>();
			deleteFormReq.ScreenNum = ScreenNum;
			return ClientServiceFactory.GetClientInstance<IDynamicForm>().DeleteForm(deleteFormReq).Worked;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x000114FC File Offset: 0x0000F6FC
		public IList<int> FindScreensAControlExistsOn(int ControlId)
		{
			FindScreensAControlExistsOnReq findScreensAControlExistsOnReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<FindScreensAControlExistsOnReq>();
			findScreensAControlExistsOnReq.ControlId = ControlId;
			return ClientServiceFactory.GetClientInstance<IDynamicForm>().FindScreensAControlExistsOn(findScreensAControlExistsOnReq).FormNums;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00011534 File Offset: 0x0000F734
		public IList<DynamicFormDTO> LoadFormsByScreenNums(params int[] ScreenNums)
		{
			bool flag = ScreenNums == null || ScreenNums.Length < 1;
			IList<DynamicFormDTO> result;
			if (flag)
			{
				result = new List<DynamicFormDTO>();
			}
			else
			{
				LoadDynamicFormsByIdsReq loadDynamicFormsByIdsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadDynamicFormsByIdsReq>();
				loadDynamicFormsByIdsReq.ScreenNums = ScreenNums;
				result = ClientServiceFactory.GetClientInstance<IDynamicForm>().LoadDynamicFormsByIds(loadDynamicFormsByIdsReq).Forms;
			}
			return result;
		}
	}
}
