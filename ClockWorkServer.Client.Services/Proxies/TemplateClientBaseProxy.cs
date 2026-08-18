using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Templates;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000152 RID: 338
	internal class TemplateClientBaseProxy : ClientBase<ITemplate>, ITemplate, IService
	{
		// Token: 0x06000CF4 RID: 3316 RVA: 0x00020439 File Offset: 0x0001E639
		public TemplateClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00020444 File Offset: 0x0001E644
		public TemplateClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00020450 File Offset: 0x0001E650
		public CreateNewTemplateResp CreateNewTemplate(CreateNewTemplateReq Request)
		{
			return base.Channel.CreateNewTemplate(Request);
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0002046E File Offset: 0x0001E66E
		public void CreateTemplateGroup(CreateTemplateGroupReq Request)
		{
			base.Channel.CreateTemplateGroup(Request);
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0002047E File Offset: 0x0001E67E
		public void DeleteTemplate(DeleteTemplateReq Request)
		{
			base.Channel.DeleteTemplate(Request);
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0002048E File Offset: 0x0001E68E
		public void DeleteTemplateGroup(DeleteTemplateGroupReq Request)
		{
			base.Channel.DeleteTemplateGroup(Request);
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x000204A0 File Offset: 0x0001E6A0
		public LoadAllTemplatesResp LoadAllTemplates(LoadAllTemplatesReq Request)
		{
			return base.Channel.LoadAllTemplates(Request);
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x000204C0 File Offset: 0x0001E6C0
		public LoadAllTemplatesAsForestResp LoadAllTemplatesAsForest(LoadAllTemplatesAsForestReq Request)
		{
			return base.Channel.LoadAllTemplatesAsForest(Request);
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x000204E0 File Offset: 0x0001E6E0
		public LoadTemplateResp LoadTemplate(LoadTemplateReq Request)
		{
			return base.Channel.LoadTemplate(Request);
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00020500 File Offset: 0x0001E700
		public LoadTemplateGroupByIdResp LoadTemplateGroupById(LoadTemplateGroupByIdReq Request)
		{
			return base.Channel.LoadTemplateGroupById(Request);
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00020520 File Offset: 0x0001E720
		public LoadTemplatesResp LoadTemplates(LoadTemplatesReq Request)
		{
			return base.Channel.LoadTemplates(Request);
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0002053E File Offset: 0x0001E73E
		public void ReplaceTemplateEmail(ReplaceTemplateEmailReq Request)
		{
			base.Channel.ReplaceTemplateEmail(Request);
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0002054E File Offset: 0x0001E74E
		public void ReplaceTemplateEmailBehindDocument(ReplaceTemplateEmailBehindDocumentReq Request)
		{
			base.Channel.ReplaceTemplateEmailBehindDocument(Request);
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0002055E File Offset: 0x0001E75E
		public void ReplaceTemplateFile(ReplaceTemplateFileReq Request)
		{
			base.Channel.ReplaceTemplateFile(Request);
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00020570 File Offset: 0x0001E770
		public LoadAllTemplateGroupsResp LoadAllTemplateGroups(LoadAllTemplateGroupsReq Request)
		{
			return base.Channel.LoadAllTemplateGroups(Request);
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0002058E File Offset: 0x0001E78E
		public void UpdateTemplate(UpdateTemplateReq Request)
		{
			base.Channel.UpdateTemplate(Request);
		}
	}
}
