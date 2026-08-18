using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.Templates;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Templates;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Templates
{
	// Token: 0x0200000F RID: 15
	public class TemplateClientManager : ITemplateClientManager, IWebService
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00003C98 File Offset: 0x00001E98
		public TemplateDTO LoadTemplate(int TemplateId, bool LoadDocumentOrEmail)
		{
			LoadTemplateReq loadTemplateReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTemplateReq>();
			loadTemplateReq.TemplateId = TemplateId;
			loadTemplateReq.LoadDocumentOrEmail = LoadDocumentOrEmail;
			return ClientServiceFactory.GetClientInstance<ITemplate>().LoadTemplate(loadTemplateReq).Template;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003CD8 File Offset: 0x00001ED8
		public int CreateNewTemplate(TemplateDTO Template)
		{
			CreateNewTemplateReq createNewTemplateReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateNewTemplateReq>();
			createNewTemplateReq.Template = Template;
			return ClientServiceFactory.GetClientInstance<ITemplate>().CreateNewTemplate(createNewTemplateReq).TemplateId;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003D10 File Offset: 0x00001F10
		public void ReplaceTemplateFile(int TemplateId, BinaryFileDTO File)
		{
			ReplaceTemplateFileReq replaceTemplateFileReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ReplaceTemplateFileReq>();
			replaceTemplateFileReq.TemplateId = TemplateId;
			replaceTemplateFileReq.File = File;
			ClientServiceFactory.GetClientInstance<ITemplate>().ReplaceTemplateFile(replaceTemplateFileReq);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003D48 File Offset: 0x00001F48
		public void ReplaceTemplateEmail(int TemplateId, TPMailMessageDTO EmailTemplate)
		{
			ReplaceTemplateEmailReq replaceTemplateEmailReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ReplaceTemplateEmailReq>();
			replaceTemplateEmailReq.TemplateId = TemplateId;
			replaceTemplateEmailReq.EmailTemplate = EmailTemplate;
			ClientServiceFactory.GetClientInstance<ITemplate>().ReplaceTemplateEmail(replaceTemplateEmailReq);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003D80 File Offset: 0x00001F80
		public void ReplaceTemplateEmailBehindDocument(int TemplateId, TPMailMessageDTO EmailTemplate)
		{
			ReplaceTemplateEmailBehindDocumentReq replaceTemplateEmailBehindDocumentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ReplaceTemplateEmailBehindDocumentReq>();
			replaceTemplateEmailBehindDocumentReq.TemplateId = TemplateId;
			replaceTemplateEmailBehindDocumentReq.EmailTemplate = EmailTemplate;
			ClientServiceFactory.GetClientInstance<ITemplate>().ReplaceTemplateEmailBehindDocument(replaceTemplateEmailBehindDocumentReq);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003DB8 File Offset: 0x00001FB8
		public void DeleteTemplate(int TemplateId)
		{
			DeleteTemplateReq deleteTemplateReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteTemplateReq>();
			deleteTemplateReq.TemplateId = TemplateId;
			ClientServiceFactory.GetClientInstance<ITemplate>().DeleteTemplate(deleteTemplateReq);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003DE8 File Offset: 0x00001FE8
		public TemplateCollectionDTO LoadTemplates(string TemplateGroupId, bool LoadDocumentsOrEmails)
		{
			LoadTemplatesReq loadTemplatesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTemplatesReq>();
			loadTemplatesReq.TemplateGroupId = TemplateGroupId;
			loadTemplatesReq.LoadDocumentsOrEmails = LoadDocumentsOrEmails;
			return ClientServiceFactory.GetClientInstance<ITemplate>().LoadTemplates(loadTemplatesReq).TemplateCollection;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003E28 File Offset: 0x00002028
		public TemplateCollectionDTO LoadAllTemplates(bool LoadDocumentsOrEmails)
		{
			LoadAllTemplatesReq loadAllTemplatesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllTemplatesReq>();
			loadAllTemplatesReq.LoadDocumentsOrEmails = LoadDocumentsOrEmails;
			return ClientServiceFactory.GetClientInstance<ITemplate>().LoadAllTemplates(loadAllTemplatesReq).TemplateCollection;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003E60 File Offset: 0x00002060
		public Forest<TemplateOrGroupDTO> LoadAllTemplatesAsForest(bool LoadDocumentsOrEmails)
		{
			LoadAllTemplatesAsForestReq loadAllTemplatesAsForestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllTemplatesAsForestReq>();
			loadAllTemplatesAsForestReq.LoadDocumentsOrEmails = LoadDocumentsOrEmails;
			return ClientServiceFactory.GetClientInstance<ITemplate>().LoadAllTemplatesAsForest(loadAllTemplatesAsForestReq).Forest;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003E98 File Offset: 0x00002098
		public TemplateGroupDTO LoadTemplateGroupById(string TemplateGroupId)
		{
			LoadTemplateGroupByIdReq loadTemplateGroupByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTemplateGroupByIdReq>();
			loadTemplateGroupByIdReq.TemplateGroupId = TemplateGroupId;
			return ClientServiceFactory.GetClientInstance<ITemplate>().LoadTemplateGroupById(loadTemplateGroupByIdReq).TemplateGroup;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003ED0 File Offset: 0x000020D0
		public void CreateTemplateGroup(TemplateGroupDTO Group)
		{
			CreateTemplateGroupReq createTemplateGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateTemplateGroupReq>();
			createTemplateGroupReq.TemplateGroup = Group;
			ClientServiceFactory.GetClientInstance<ITemplate>().CreateTemplateGroup(createTemplateGroupReq);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003F00 File Offset: 0x00002100
		public void DeleteTemplateGroup(string TemplateGroupId)
		{
			DeleteTemplateGroupReq deleteTemplateGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteTemplateGroupReq>();
			deleteTemplateGroupReq.TemplateGroupId = TemplateGroupId;
			ClientServiceFactory.GetClientInstance<ITemplate>().DeleteTemplateGroup(deleteTemplateGroupReq);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003F30 File Offset: 0x00002130
		public IList<TemplateGroupDTO> LoadAllTemplateGroups()
		{
			LoadAllTemplateGroupsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllTemplateGroupsReq>();
			return ClientServiceFactory.GetClientInstance<ITemplate>().LoadAllTemplateGroups(request).TemplateGroups;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003F60 File Offset: 0x00002160
		public void UpdateTemplate(TemplateDTO Template)
		{
			UpdateTemplateReq updateTemplateReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateTemplateReq>();
			updateTemplateReq.Template = Template;
			ClientServiceFactory.GetClientInstance<ITemplate>().UpdateTemplate(updateTemplateReq);
		}
	}
}
