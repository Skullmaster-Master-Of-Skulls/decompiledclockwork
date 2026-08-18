using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.Core.Mappers.Files;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.Files;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200003F RID: 63
	public class DynamicFormServiceManager : IDynamicForm, IService
	{
		// Token: 0x06000279 RID: 633 RVA: 0x0000C6C0 File Offset: 0x0000A8C0
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000C6D4 File Offset: 0x0000A8D4
		public LoadDynamicFormByIdResp LoadDynamicFormById(LoadDynamicFormByIdReq Request)
		{
			IDynamicFormManager dynamicFormManager = new DynamicFormManager(Request.GetOperationContext());
			DynamicForm dynamicForm = dynamicFormManager.LoadDynamicFormById(Request.ScreenNum);
			return new LoadDynamicFormByIdResp
			{
				DynamicForm = dynamicForm.ToDTO()
			};
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000C714 File Offset: 0x0000A914
		public FindFormByTitleSubstringMatchResp FindFormByTitleSubstringMatch(FindFormByTitleSubstringMatchReq Request)
		{
			IDynamicFormManager dynamicFormManager = new DynamicFormManager(Request.GetOperationContext());
			IList<DynamicForm> list = dynamicFormManager.FindFormByTitleSubstringMatch(Request.SubstringToMatch, Request.SearchPrimaryTitle, Request.SearchSecondaryTitle);
			FindFormByTitleSubstringMatchResp findFormByTitleSubstringMatchResp = new FindFormByTitleSubstringMatchResp();
			IList<DynamicFormDTO> matchingForms;
			if (list != null)
			{
				matchingForms = list.ToList<DynamicForm>().ConvertAll<DynamicFormDTO>((DynamicForm f) => f.ToDTO());
			}
			else
			{
				matchingForms = null;
			}
			findFormByTitleSubstringMatchResp.MatchingForms = matchingForms;
			return findFormByTitleSubstringMatchResp;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000C788 File Offset: 0x0000A988
		public LoadAllFormsResp LoadAllForms(LoadAllFormsReq Request)
		{
			IDynamicFormManager dynamicFormManager = new DynamicFormManager(Request.GetOperationContext());
			Forest<DynamicFormOrGroupOrFormType> forest = dynamicFormManager.LoadAllForms();
			return new LoadAllFormsResp
			{
				Forms = ((forest == null) ? null : forest.ToDTO())
			};
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000C7C8 File Offset: 0x0000A9C8
		public LoadActiveFormsByFormTypeResp LoadActiveFormsByFormType(LoadActiveFormsByFormTypeReq request)
		{
			IDynamicFormManager dynamicFormManager = new DynamicFormManager(request.GetOperationContext());
			IDynamicFormManager dynamicFormManager2 = dynamicFormManager;
			eDynamicFormTypeDTO[] formTypes = request.FormTypes;
			eDynamicFormType[] formTypes2;
			if (formTypes == null)
			{
				formTypes2 = null;
			}
			else
			{
				formTypes2 = (from g in formTypes
				select (eDynamicFormType)g).ToArray<eDynamicFormType>();
			}
			IList<DynamicForm> list = dynamicFormManager2.LoadActiveFormsByFormType(formTypes2);
			LoadActiveFormsByFormTypeResp loadActiveFormsByFormTypeResp = new LoadActiveFormsByFormTypeResp();
			IList<DynamicFormDTO> forms;
			if (list == null)
			{
				forms = null;
			}
			else
			{
				forms = list.ToList<DynamicForm>().ConvertAll<DynamicFormDTO>((DynamicForm f) => f.ToDTO());
			}
			loadActiveFormsByFormTypeResp.Forms = forms;
			return loadActiveFormsByFormTypeResp;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000C860 File Offset: 0x0000AA60
		public LoadDynamicFormsByIdsResp LoadDynamicFormsByIds(LoadDynamicFormsByIdsReq Request)
		{
			IDynamicFormManager dynamicFormManager = new DynamicFormManager(Request.GetOperationContext());
			IList<DynamicForm> list = dynamicFormManager.LoadDynamicFormsByIds(Request.ScreenNums.ToArray<int>());
			LoadDynamicFormsByIdsResp loadDynamicFormsByIdsResp = new LoadDynamicFormsByIdsResp();
			IList<DynamicFormDTO> forms;
			if (list == null)
			{
				forms = null;
			}
			else
			{
				forms = list.ToList<DynamicForm>().ConvertAll<DynamicFormDTO>((DynamicForm f) => f.ToDTO());
			}
			loadDynamicFormsByIdsResp.Forms = forms;
			return loadDynamicFormsByIdsResp;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000C8CC File Offset: 0x0000AACC
		public ExportFormsToXmlResp ExportFormsToXml(ExportFormsToXmlReq Request)
		{
			IDynamicFormManager dynamicFormManager = new DynamicFormManager(Request.GetOperationContext());
			IList<BinaryFile> list = dynamicFormManager.ExportFormsToXml(Request.ScreenNums.ToArray<int>());
			ExportFormsToXmlResp exportFormsToXmlResp = new ExportFormsToXmlResp();
			IList<BinaryFileDTO> xmlFiles;
			if (list == null)
			{
				xmlFiles = null;
			}
			else
			{
				xmlFiles = list.ToList<BinaryFile>().ConvertAll<BinaryFileDTO>((BinaryFile f) => f.ToDTO());
			}
			exportFormsToXmlResp.XmlFiles = xmlFiles;
			return exportFormsToXmlResp;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000C938 File Offset: 0x0000AB38
		public ImportFormFromXmlResp ImportFormFromXml(ImportFormFromXmlReq Request)
		{
			IDynamicFormManager dynamicFormManager = new DynamicFormManager(Request.GetOperationContext());
			dynamicFormManager.ImportFormFromXml(Request.Xml, Request.ScreenNumToImportControlsInto);
			return new ImportFormFromXmlResp();
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000C970 File Offset: 0x0000AB70
		public LoadFormsWithExtendedInfoByScreenNumsResp LoadFormsWithExtendedInfoByScreenNums(LoadFormsWithExtendedInfoByScreenNumsReq Request)
		{
			IDynamicFormManager dynamicFormManager = new DynamicFormManager(Request.GetOperationContext());
			IList<DynamicFormWithExtendedInfo> list = dynamicFormManager.LoadFormsWithExtendedInfoByScreenNums(Request.ScreenNums.ToArray<int>());
			LoadFormsWithExtendedInfoByScreenNumsResp loadFormsWithExtendedInfoByScreenNumsResp = new LoadFormsWithExtendedInfoByScreenNumsResp();
			IList<DynamicFormWithExtendedInfoDTO> forms;
			if (list == null)
			{
				forms = null;
			}
			else
			{
				forms = list.ToList<DynamicFormWithExtendedInfo>().ConvertAll<DynamicFormWithExtendedInfoDTO>((DynamicFormWithExtendedInfo f) => f.ToDTO());
			}
			loadFormsWithExtendedInfoByScreenNumsResp.Forms = forms;
			return loadFormsWithExtendedInfoByScreenNumsResp;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000C9DC File Offset: 0x0000ABDC
		public CreateFormResp CreateForm(CreateFormReq Request)
		{
			IDynamicFormManager dynamicFormManager = new DynamicFormManager(Request.GetOperationContext());
			int screenNum = dynamicFormManager.CreateForm(Request.Form.ToDomainObject());
			return new CreateFormResp
			{
				ScreenNum = screenNum
			};
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000CA1C File Offset: 0x0000AC1C
		public UpdateFormResp UpdateForm(UpdateFormReq Request)
		{
			IDynamicFormManager dynamicFormManager = new DynamicFormManager(Request.GetOperationContext());
			dynamicFormManager.UpdateForm(Request.Form.ToDomainObject());
			return new UpdateFormResp();
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000CA54 File Offset: 0x0000AC54
		public DeleteFormResp DeleteForm(DeleteFormReq Request)
		{
			IDynamicFormManager dynamicFormManager = new DynamicFormManager(Request.GetOperationContext());
			bool worked = dynamicFormManager.DeleteForm(Request.ScreenNum);
			return new DeleteFormResp
			{
				Worked = worked
			};
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000CA8C File Offset: 0x0000AC8C
		public FindScreensAControlExistsOnResp FindScreensAControlExistsOn(FindScreensAControlExistsOnReq Request)
		{
			IDynamicFormManager dynamicFormManager = new DynamicFormManager(Request.GetOperationContext());
			IList<int> list = dynamicFormManager.FindScreensAControlExistsOn(Request.ControlId);
			return new FindScreensAControlExistsOnResp
			{
				FormNums = ((list != null) ? list.ToList<int>() : null)
			};
		}
	}
}
