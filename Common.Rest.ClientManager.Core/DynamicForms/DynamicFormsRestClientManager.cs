using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.DynamicForms
{
	// Token: 0x02000056 RID: 86
	public class DynamicFormsRestClientManager : BearerTokenRestProxy<IDynamicFormsClientManager>, IDynamicFormsClientManager, IWebService
	{
		// Token: 0x06000353 RID: 851 RVA: 0x0000A6B8 File Offset: 0x000088B8
		public DynamicFormsRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000A6C2 File Offset: 0x000088C2
		public DynamicFormsRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000A6CD File Offset: 0x000088CD
		public DynamicFormDTO LoadDynamicFormById(int ScreenNum)
		{
			return base.Get<DynamicFormDTO>(string.Format("dynamicform/screennum/{0}", ScreenNum), true);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000A6E6 File Offset: 0x000088E6
		public IList<DynamicFormDTO> FindFormByTitleSubstringMatch(string SubstringToMatch, bool SearchPrimaryTitle, bool SearchSecondaryTitle)
		{
			return base.GetMany<DynamicFormDTO>(string.Format("dynamicform/find?titlesubstringtomatch={0}&searchprimarytitle={1}&searchsecondarytitle={2}", SubstringToMatch, SearchPrimaryTitle, SearchSecondaryTitle), true);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000A706 File Offset: 0x00008906
		public Forest<DynamicFormOrGroupOrFormTypeDTO> LoadAllForms()
		{
			return base.Get<LoadAllFormsResp>("dynamicform", true).Forms;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000A719 File Offset: 0x00008919
		public IList<DynamicFormDTO> LoadActiveFormsByFormType(params eDynamicFormType[] FormTypes)
		{
			return base.GetMany<DynamicFormDTO>(string.Format("dynamicform/active/formtypes/{0}", FormTypes.CommaSeparatedValuesWithoutSpace<eDynamicFormType>()), true);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000A732 File Offset: 0x00008932
		public IList<BinaryFileDTO> ExportFormsToXml(params int[] ScreenNum)
		{
			return base.GetMany<BinaryFileDTO>(string.Format("dynamicform/exporttoxml/screennums/{0}", ScreenNum.CommaSeparatedValuesWithoutSpace<int>()), true);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000A74C File Offset: 0x0000894C
		public void ImportFormFromXml(string xml, int ScreenNumToImportControlsInto)
		{
			ImportFormFromXmlReq importFormFromXmlReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ImportFormFromXmlReq>();
			importFormFromXmlReq.Xml = xml;
			importFormFromXmlReq.ScreenNumToImportControlsInto = ScreenNumToImportControlsInto;
			base.Post<ImportFormFromXmlReq>(importFormFromXmlReq, "dynamicform/importfromxml");
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000A77E File Offset: 0x0000897E
		public IList<DynamicFormWithExtendedInfoDTO> LoadFormsWithExtendedInfoByScreenNums(params int[] ScreenNums)
		{
			return base.GetMany<DynamicFormWithExtendedInfoDTO>(string.Format("dynamicform/withextendedinfo/screennums/{0}", ScreenNums), true);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000A792 File Offset: 0x00008992
		public IList<DynamicFormDTO> LoadFormsByScreenNums(params int[] ScreenNums)
		{
			return base.GetMany<DynamicFormDTO>(string.Format("dynamicform/screennums/{0}", ScreenNums.CommaSeparatedValuesWithoutSpace<int>()), true);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000A7AB File Offset: 0x000089AB
		public int CreateForm(DynamicFormWithExtendedInfoDTO Form)
		{
			return base.Post<DynamicFormWithExtendedInfoDTO, int>(Form, "dynamicform");
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000A7B9 File Offset: 0x000089B9
		public void UpdateForm(DynamicFormWithExtendedInfoDTO Form)
		{
			base.Put<DynamicFormWithExtendedInfoDTO>(Form, "dynamicform");
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000A7C8 File Offset: 0x000089C8
		public bool DeleteForm(int ScreenNum)
		{
			DeleteFormReq deleteFormReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteFormReq>();
			deleteFormReq.ScreenNum = ScreenNum;
			return base.Post<DeleteFormReq, bool>(deleteFormReq, "dynamicform/deleteform");
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000A7F3 File Offset: 0x000089F3
		public IList<int> FindScreensAControlExistsOn(int ControlId)
		{
			return base.GetMany<int>(string.Format("dynamicform/findscreensacontrolexistson/controlid/{0}", ControlId), true);
		}
	}
}
