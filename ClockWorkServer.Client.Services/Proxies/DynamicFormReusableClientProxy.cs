using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200008C RID: 140
	public class DynamicFormReusableClientProxy : WCFTokenBasedReusableClientProxy<IDynamicForm>, IDynamicForm, IService
	{
		// Token: 0x060005ED RID: 1517 RVA: 0x000104F2 File Offset: 0x0000E6F2
		public DynamicFormReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x000104FD File Offset: 0x0000E6FD
		public DynamicFormReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001050C File Offset: 0x0000E70C
		public LoadDynamicFormByIdResp LoadDynamicFormById(LoadDynamicFormByIdReq Request)
		{
			return this.WrapServiceMethod<LoadDynamicFormByIdResp>(() => this.Proxy.LoadDynamicFormById(Request));
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00010544 File Offset: 0x0000E744
		public FindFormByTitleSubstringMatchResp FindFormByTitleSubstringMatch(FindFormByTitleSubstringMatchReq Request)
		{
			return this.WrapServiceMethod<FindFormByTitleSubstringMatchResp>(() => this.Proxy.FindFormByTitleSubstringMatch(Request));
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001057C File Offset: 0x0000E77C
		public LoadAllFormsResp LoadAllForms(LoadAllFormsReq Request)
		{
			return this.WrapServiceMethod<LoadAllFormsResp>(() => this.Proxy.LoadAllForms(Request));
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x000105B4 File Offset: 0x0000E7B4
		public LoadActiveFormsByFormTypeResp LoadActiveFormsByFormType(LoadActiveFormsByFormTypeReq request)
		{
			return this.WrapServiceMethod<LoadActiveFormsByFormTypeResp>(() => this.Proxy.LoadActiveFormsByFormType(request));
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x000105EC File Offset: 0x0000E7EC
		public ExportFormsToXmlResp ExportFormsToXml(ExportFormsToXmlReq Request)
		{
			return this.WrapServiceMethod<ExportFormsToXmlResp>(() => this.Proxy.ExportFormsToXml(Request));
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00010624 File Offset: 0x0000E824
		public ImportFormFromXmlResp ImportFormFromXml(ImportFormFromXmlReq Request)
		{
			return this.WrapServiceMethod<ImportFormFromXmlResp>(() => this.Proxy.ImportFormFromXml(Request));
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001065C File Offset: 0x0000E85C
		public LoadDynamicFormsByIdsResp LoadDynamicFormsByIds(LoadDynamicFormsByIdsReq Request)
		{
			return this.WrapServiceMethod<LoadDynamicFormsByIdsResp>(() => this.Proxy.LoadDynamicFormsByIds(Request));
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00010694 File Offset: 0x0000E894
		public LoadFormsWithExtendedInfoByScreenNumsResp LoadFormsWithExtendedInfoByScreenNums(LoadFormsWithExtendedInfoByScreenNumsReq Request)
		{
			return this.WrapServiceMethod<LoadFormsWithExtendedInfoByScreenNumsResp>(() => this.Proxy.LoadFormsWithExtendedInfoByScreenNums(Request));
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x000106CC File Offset: 0x0000E8CC
		public CreateFormResp CreateForm(CreateFormReq Request)
		{
			return this.WrapServiceMethod<CreateFormResp>(() => this.Proxy.CreateForm(Request));
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00010704 File Offset: 0x0000E904
		public DeleteFormResp DeleteForm(DeleteFormReq Request)
		{
			return this.WrapServiceMethod<DeleteFormResp>(() => this.Proxy.DeleteForm(Request));
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001073C File Offset: 0x0000E93C
		public UpdateFormResp UpdateForm(UpdateFormReq Request)
		{
			return this.WrapServiceMethod<UpdateFormResp>(() => this.Proxy.UpdateForm(Request));
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00010774 File Offset: 0x0000E974
		public FindScreensAControlExistsOnResp FindScreensAControlExistsOn(FindScreensAControlExistsOnReq Request)
		{
			return this.WrapServiceMethod<FindScreensAControlExistsOnResp>(() => this.Proxy.FindScreensAControlExistsOn(Request));
		}
	}
}
