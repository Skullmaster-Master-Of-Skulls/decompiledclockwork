using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.MailMergeEntities.Output;

namespace TechnoPro.Common.ClientManager.ICore.MailMerging
{
	// Token: 0x02000036 RID: 54
	public interface IMailMergingClientManager : IWebService
	{
		// Token: 0x0600017A RID: 378
		IList<string> MailMergeText(MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary, string Template, eMailMergeDocumentOutputFormat OutputFormat);

		// Token: 0x0600017B RID: 379
		string GetMailMergeCodeDefinitionsForDisplay();

		// Token: 0x0600017C RID: 380
		IList<MailMergeCodeDTO> LookupCodeValues(MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary, IList<string> CodesNoTags);

		// Token: 0x0600017D RID: 381
		IList<string> TestAllMailMergeCodes(MailMergeContextDTO StartingContext, string TemplateHeaderText, IList<string> CustomMailMergeCodes);

		// Token: 0x0600017E RID: 382
		IList<string> TestAllMailMergeCodes(string StartingContextString, string TemplateHeaderText, IList<string> CustomMailMergeCodes);
	}
}
