using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.Templates;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Templates;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Templates
{
	// Token: 0x0200000B RID: 11
	public class TemplateRestClientManager : BearerTokenRestProxy<ITemplateClientManager>, ITemplateClientManager, IWebService
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002E4B File Offset: 0x0000104B
		public TemplateRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E55 File Offset: 0x00001055
		public TemplateRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002E60 File Offset: 0x00001060
		public TemplateDTO LoadTemplate(int TemplateId, bool LoadDocumentOrEmail)
		{
			return base.Get<TemplateDTO>(string.Format("template/templateid/{0}?loaddocumentoremail={1}", TemplateId, LoadDocumentOrEmail), true);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002E7F File Offset: 0x0000107F
		public int CreateNewTemplate(TemplateDTO Template)
		{
			return base.Post<TemplateDTO, int>(Template, "template");
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002E90 File Offset: 0x00001090
		public void ReplaceTemplateFile(int TemplateId, BinaryFileDTO File)
		{
			ReplaceTemplateFileReq replaceTemplateFileReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ReplaceTemplateFileReq>();
			replaceTemplateFileReq.TemplateId = TemplateId;
			replaceTemplateFileReq.File = File;
			base.Post<ReplaceTemplateFileReq>(replaceTemplateFileReq, "template/replacetemplatefile");
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002EC4 File Offset: 0x000010C4
		public void ReplaceTemplateEmail(int TemplateId, TPMailMessageDTO EmailTemplate)
		{
			ReplaceTemplateEmailReq replaceTemplateEmailReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ReplaceTemplateEmailReq>();
			replaceTemplateEmailReq.TemplateId = TemplateId;
			replaceTemplateEmailReq.EmailTemplate = EmailTemplate;
			base.Post<ReplaceTemplateEmailReq>(replaceTemplateEmailReq, "template/replacetemplateemail");
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002EF8 File Offset: 0x000010F8
		public void ReplaceTemplateEmailBehindDocument(int TemplateId, TPMailMessageDTO EmailTemplate)
		{
			ReplaceTemplateEmailBehindDocumentReq replaceTemplateEmailBehindDocumentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ReplaceTemplateEmailBehindDocumentReq>();
			replaceTemplateEmailBehindDocumentReq.TemplateId = TemplateId;
			replaceTemplateEmailBehindDocumentReq.EmailTemplate = EmailTemplate;
			base.Post<ReplaceTemplateEmailBehindDocumentReq>(replaceTemplateEmailBehindDocumentReq, "template/replacetemplateemailbehinddoument");
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002F2A File Offset: 0x0000112A
		public void DeleteTemplate(int TemplateId)
		{
			base.Delete(string.Format("template/templateid/{0}", TemplateId));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002F42 File Offset: 0x00001142
		public TemplateCollectionDTO LoadTemplates(string TemplateGroupId, bool LoadDocumentsOrEmails)
		{
			return base.Get<TemplateCollectionDTO>(string.Format("template/templategroupid/{0}?loaddocumentsoremails={1}", TemplateGroupId, LoadDocumentsOrEmails), true);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002F5C File Offset: 0x0000115C
		public TemplateCollectionDTO LoadAllTemplates(bool LoadDocumentsOrEmails)
		{
			return base.Get<TemplateCollectionDTO>(string.Format("template?loaddocumentsoremails={0}", LoadDocumentsOrEmails), true);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002F75 File Offset: 0x00001175
		public Forest<TemplateOrGroupDTO> LoadAllTemplatesAsForest(bool LoadDocumentsOrEmails)
		{
			return base.Get<LoadAllTemplatesAsForestResp>(string.Format("template/asforest?loaddocumentsoremails={0}", LoadDocumentsOrEmails), true).Forest;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002F93 File Offset: 0x00001193
		public TemplateGroupDTO LoadTemplateGroupById(string TemplateGroupId)
		{
			return base.Get<TemplateGroupDTO>(string.Format("template/group/id/{0}", TemplateGroupId), true);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002FA7 File Offset: 0x000011A7
		public void CreateTemplateGroup(TemplateGroupDTO Group)
		{
			base.Post<TemplateGroupDTO>(Group, "template/group");
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002FB5 File Offset: 0x000011B5
		public void DeleteTemplateGroup(string TemplateGroupId)
		{
			base.Delete(string.Format("template/group/id/{0}", TemplateGroupId));
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002FC8 File Offset: 0x000011C8
		public IList<TemplateGroupDTO> LoadAllTemplateGroups()
		{
			return base.GetMany<TemplateGroupDTO>("template/groups", true);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002FD6 File Offset: 0x000011D6
		public void UpdateTemplate(TemplateDTO Template)
		{
			base.Put<TemplateDTO>(Template, "template");
		}
	}
}
