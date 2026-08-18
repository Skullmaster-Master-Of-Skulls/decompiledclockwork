using System;
using System.Collections.Generic;
using System.Data;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.MailMergeEntities.Output;

namespace TechnoPro.Common.ICore.MailMerging
{
	// Token: 0x02000069 RID: 105
	public interface IMailMergingManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060002DD RID: 733
		IList<MailMergeCode> LookupCodeValues(MailMergeContextWithCustomDictionary ContextWithCustomDictionary, IList<MailMergeCode> Codes);

		// Token: 0x060002DE RID: 734
		IList<string> OutputText(List<MailMergeCode> Codes, string Template, eMailMergeDocumentOutputFormat outputFormat);

		// Token: 0x060002DF RID: 735
		IList<MailMergeCode> ExtractCodes(string Template);

		// Token: 0x060002E0 RID: 736
		IList<string> MailMerge(MailMergeContextWithCustomDictionary ContextWithCustomDictionary, string text, eMailMergeDocumentOutputFormat outputFormat);

		// Token: 0x060002E1 RID: 737
		string GetMailMergeCodeDefinitionsForDisplay();

		// Token: 0x060002E2 RID: 738
		IList<string> TestAllMailMergeCodes(MailMergeContext StartingContext, string TemplateHeaderText, IList<string> CustomMailMergeCodes, out IList<MailMergeCode> CodesWithValues);

		// Token: 0x060002E3 RID: 739
		IList<string> MailMergeAndReturnCodesWithValues(MailMergeContextWithCustomDictionary ContextWithCustomDictionary, string text, eMailMergeDocumentOutputFormat outputFormat, out IList<MailMergeCode> CodesWithValues);

		// Token: 0x060002E4 RID: 740
		IList<MailMergeContextWithCustomDictionary> ExtractMailMergeContextFromTable(DataTable t);

		// Token: 0x060002E5 RID: 741
		IList<string> TestAllMailMergeCodes(string StartingContext, string TemplateHeaderText, IList<string> CustomMailMergeCodes, out IList<MailMergeCode> CodesWithValues);
	}
}
