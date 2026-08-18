using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.Templates;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Templates
{
	// Token: 0x0200000C RID: 12
	public interface ITemplateClientManager : IWebService
	{
		// Token: 0x0600003F RID: 63
		TemplateDTO LoadTemplate(int TemplateId, bool LoadDocumentOrEmail);

		// Token: 0x06000040 RID: 64
		int CreateNewTemplate(TemplateDTO Template);

		// Token: 0x06000041 RID: 65
		void ReplaceTemplateFile(int TemplateId, BinaryFileDTO File);

		// Token: 0x06000042 RID: 66
		void ReplaceTemplateEmail(int TemplateId, TPMailMessageDTO EmailTemplate);

		// Token: 0x06000043 RID: 67
		void ReplaceTemplateEmailBehindDocument(int TemplateId, TPMailMessageDTO EmailTemplate);

		// Token: 0x06000044 RID: 68
		void DeleteTemplate(int TemplateId);

		// Token: 0x06000045 RID: 69
		TemplateCollectionDTO LoadTemplates(string TemplateGroupId, bool LoadDocumentsOrEmails);

		// Token: 0x06000046 RID: 70
		TemplateCollectionDTO LoadAllTemplates(bool LoadDocumentsOrEmails);

		// Token: 0x06000047 RID: 71
		Forest<TemplateOrGroupDTO> LoadAllTemplatesAsForest(bool LoadDocumentsOrEmails);

		// Token: 0x06000048 RID: 72
		TemplateGroupDTO LoadTemplateGroupById(string TemplateGroupId);

		// Token: 0x06000049 RID: 73
		void CreateTemplateGroup(TemplateGroupDTO Group);

		// Token: 0x0600004A RID: 74
		void DeleteTemplateGroup(string TemplateGroupId);

		// Token: 0x0600004B RID: 75
		IList<TemplateGroupDTO> LoadAllTemplateGroups();

		// Token: 0x0600004C RID: 76
		void UpdateTemplate(TemplateDTO Template);
	}
}
