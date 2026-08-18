using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Templates;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000151 RID: 337
	public class TemplateReusableClientProxy : WCFTokenBasedReusableClientProxy<ITemplate>, ITemplate, IService
	{
		// Token: 0x06000CE4 RID: 3300 RVA: 0x00020112 File Offset: 0x0001E312
		public TemplateReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0002011D File Offset: 0x0001E31D
		public TemplateReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0002012C File Offset: 0x0001E32C
		public CreateNewTemplateResp CreateNewTemplate(CreateNewTemplateReq Request)
		{
			return this.WrapServiceMethod<CreateNewTemplateResp>(() => this.Proxy.CreateNewTemplate(Request));
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00020164 File Offset: 0x0001E364
		public void CreateTemplateGroup(CreateTemplateGroupReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.CreateTemplateGroup(Request);
			});
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0002019C File Offset: 0x0001E39C
		public void DeleteTemplate(DeleteTemplateReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteTemplate(Request);
			});
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x000201D4 File Offset: 0x0001E3D4
		public void DeleteTemplateGroup(DeleteTemplateGroupReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteTemplateGroup(Request);
			});
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0002020C File Offset: 0x0001E40C
		public LoadAllTemplatesResp LoadAllTemplates(LoadAllTemplatesReq Request)
		{
			return this.WrapServiceMethod<LoadAllTemplatesResp>(() => this.Proxy.LoadAllTemplates(Request));
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00020244 File Offset: 0x0001E444
		public LoadAllTemplatesAsForestResp LoadAllTemplatesAsForest(LoadAllTemplatesAsForestReq Request)
		{
			return this.WrapServiceMethod<LoadAllTemplatesAsForestResp>(() => this.Proxy.LoadAllTemplatesAsForest(Request));
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0002027C File Offset: 0x0001E47C
		public LoadTemplateResp LoadTemplate(LoadTemplateReq Request)
		{
			return this.WrapServiceMethod<LoadTemplateResp>(() => this.Proxy.LoadTemplate(Request));
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x000202B4 File Offset: 0x0001E4B4
		public LoadTemplateGroupByIdResp LoadTemplateGroupById(LoadTemplateGroupByIdReq Request)
		{
			return this.WrapServiceMethod<LoadTemplateGroupByIdResp>(() => this.Proxy.LoadTemplateGroupById(Request));
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x000202EC File Offset: 0x0001E4EC
		public LoadTemplatesResp LoadTemplates(LoadTemplatesReq Request)
		{
			return this.WrapServiceMethod<LoadTemplatesResp>(() => this.Proxy.LoadTemplates(Request));
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00020324 File Offset: 0x0001E524
		public void ReplaceTemplateEmail(ReplaceTemplateEmailReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.ReplaceTemplateEmail(Request);
			});
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0002035C File Offset: 0x0001E55C
		public void ReplaceTemplateEmailBehindDocument(ReplaceTemplateEmailBehindDocumentReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.ReplaceTemplateEmailBehindDocument(Request);
			});
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00020394 File Offset: 0x0001E594
		public void ReplaceTemplateFile(ReplaceTemplateFileReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.ReplaceTemplateFile(Request);
			});
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x000203CC File Offset: 0x0001E5CC
		public LoadAllTemplateGroupsResp LoadAllTemplateGroups(LoadAllTemplateGroupsReq Request)
		{
			return this.WrapServiceMethod<LoadAllTemplateGroupsResp>(() => this.Proxy.LoadAllTemplateGroups(Request));
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00020404 File Offset: 0x0001E604
		public void UpdateTemplate(UpdateTemplateReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateTemplate(Request);
			});
		}
	}
}
