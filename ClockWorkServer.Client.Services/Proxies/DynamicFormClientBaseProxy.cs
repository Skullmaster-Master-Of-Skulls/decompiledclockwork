using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200008D RID: 141
	internal class DynamicFormClientBaseProxy : ClientBase<IDynamicForm>, IDynamicForm, IService
	{
		// Token: 0x060005FB RID: 1531 RVA: 0x000107AC File Offset: 0x0000E9AC
		public DynamicFormClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x000107B7 File Offset: 0x0000E9B7
		public DynamicFormClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x000107C4 File Offset: 0x0000E9C4
		public LoadDynamicFormByIdResp LoadDynamicFormById(LoadDynamicFormByIdReq Request)
		{
			return base.Channel.LoadDynamicFormById(Request);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x000107E4 File Offset: 0x0000E9E4
		public FindFormByTitleSubstringMatchResp FindFormByTitleSubstringMatch(FindFormByTitleSubstringMatchReq Request)
		{
			return base.Channel.FindFormByTitleSubstringMatch(Request);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00010804 File Offset: 0x0000EA04
		public LoadAllFormsResp LoadAllForms(LoadAllFormsReq Request)
		{
			return base.Channel.LoadAllForms(Request);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00010824 File Offset: 0x0000EA24
		public LoadActiveFormsByFormTypeResp LoadActiveFormsByFormType(LoadActiveFormsByFormTypeReq request)
		{
			return base.Channel.LoadActiveFormsByFormType(request);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00010844 File Offset: 0x0000EA44
		public ExportFormsToXmlResp ExportFormsToXml(ExportFormsToXmlReq Request)
		{
			return base.Channel.ExportFormsToXml(Request);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00010864 File Offset: 0x0000EA64
		public ImportFormFromXmlResp ImportFormFromXml(ImportFormFromXmlReq Request)
		{
			return base.Channel.ImportFormFromXml(Request);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00010884 File Offset: 0x0000EA84
		public LoadDynamicFormsByIdsResp LoadDynamicFormsByIds(LoadDynamicFormsByIdsReq Request)
		{
			return base.Channel.LoadDynamicFormsByIds(Request);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000108A4 File Offset: 0x0000EAA4
		public LoadFormsWithExtendedInfoByScreenNumsResp LoadFormsWithExtendedInfoByScreenNums(LoadFormsWithExtendedInfoByScreenNumsReq Request)
		{
			return base.Channel.LoadFormsWithExtendedInfoByScreenNums(Request);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x000108C4 File Offset: 0x0000EAC4
		public CreateFormResp CreateForm(CreateFormReq Request)
		{
			return base.Channel.CreateForm(Request);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x000108E4 File Offset: 0x0000EAE4
		public DeleteFormResp DeleteForm(DeleteFormReq Request)
		{
			return base.Channel.DeleteForm(Request);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00010904 File Offset: 0x0000EB04
		public UpdateFormResp UpdateForm(UpdateFormReq Request)
		{
			return base.Channel.UpdateForm(Request);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00010924 File Offset: 0x0000EB24
		public FindScreensAControlExistsOnResp FindScreensAControlExistsOn(FindScreensAControlExistsOnReq Request)
		{
			return base.Channel.FindScreensAControlExistsOn(Request);
		}
	}
}
