using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.Templates;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.DAO.Templates
{
	// Token: 0x02000022 RID: 34
	public interface ITemplateDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000074 RID: 116
		Template LoadTemplate(int TemplateId, bool LoadDocumentOrEmail);

		// Token: 0x06000075 RID: 117
		int CreateNewTemplate(Template Template);

		// Token: 0x06000076 RID: 118
		void ReplaceTemplateFile(int TemplateId, BinaryFile File);

		// Token: 0x06000077 RID: 119
		void ReplaceTemplateEmail(int TemplateId, TPMailMessage EmailTemplate);

		// Token: 0x06000078 RID: 120
		void ReplaceTemplateEmailBehindDocument(int TemplateId, TPMailMessage EmailTemplate);

		// Token: 0x06000079 RID: 121
		void DeleteTemplate(int TemplateId);

		// Token: 0x0600007A RID: 122
		TemplateCollection LoadTemplates(string TemplateGroupId, bool LoadDocumentsOrEmails);

		// Token: 0x0600007B RID: 123
		TemplateCollection LoadAllTemplates(bool LoadDocumentsOrEmails);

		// Token: 0x0600007C RID: 124
		IList<TemplateGroup> LoadAllTemplateGroups();

		// Token: 0x0600007D RID: 125
		string CreateTemplateGroup(TemplateGroup Group);

		// Token: 0x0600007E RID: 126
		void DeleteTemplateGroup(string TemplateGroupId);

		// Token: 0x0600007F RID: 127
		void UpdateTemplateTitleAndGroup(int TemplateId, string TemplateGroupId, string TemplateTitle, IDictionary<string, string> fieldMappings);
	}
}
