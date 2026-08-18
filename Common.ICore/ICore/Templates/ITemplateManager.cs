using System;
using System.Collections.Generic;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.Templates;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.ICore.Templates
{
	// Token: 0x02000022 RID: 34
	public interface ITemplateManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000DA RID: 218
		Template LoadTemplate(int TemplateId, bool LoadDocumentOrEmail);

		// Token: 0x060000DB RID: 219
		int CreateNewTemplate(Template Template);

		// Token: 0x060000DC RID: 220
		void ReplaceTemplateFile(int TemplateId, BinaryFile File);

		// Token: 0x060000DD RID: 221
		void ReplaceTemplateEmail(int TemplateId, TPMailMessage EmailTemplate);

		// Token: 0x060000DE RID: 222
		void ReplaceTemplateEmailBehindDocument(int TemplateId, TPMailMessage EmailTemplate);

		// Token: 0x060000DF RID: 223
		void DeleteTemplate(int TemplateId);

		// Token: 0x060000E0 RID: 224
		TemplateCollection LoadTemplates(string TemplateGroupId, bool LoadDocumentsOrEmails);

		// Token: 0x060000E1 RID: 225
		TemplateCollection LoadAllTemplates(bool LoadDocumentsOrEmails);

		// Token: 0x060000E2 RID: 226
		Forest<TemplateOrGroup> LoadAllTemplatesAsForest(bool LoadDocumentsOrEmails);

		// Token: 0x060000E3 RID: 227
		TemplateGroup LoadTemplateGroupById(string TemplateGroupId);

		// Token: 0x060000E4 RID: 228
		void CreateTemplateGroup(TemplateGroup Group);

		// Token: 0x060000E5 RID: 229
		void DeleteTemplateGroup(string TemplateGroupId);

		// Token: 0x060000E6 RID: 230
		IList<TemplateGroup> LoadAllTemplateGroups();

		// Token: 0x060000E7 RID: 231
		void UpdateTemplate(Template Template);
	}
}
