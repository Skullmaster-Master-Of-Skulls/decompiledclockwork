using System;
using System.Collections.Generic;
using System.Data;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.MailMergeEntities.DataTableMailMerging;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.ICore.MailMerging
{
	// Token: 0x02000067 RID: 103
	public interface IMailMergingEmailManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060002BF RID: 703
		MailMergeCodesWithTemplate ExtractUniqueCodes(string TemplateXml);

		// Token: 0x060002C0 RID: 704
		MailMergeCodesWithTemplate ExtractUniqueCodes(int TemplateId);

		// Token: 0x060002C1 RID: 705
		MailMergeCodesWithTemplate ExtractUniqueCodes(Setting WebSettingEmailXmlTemplate);

		// Token: 0x060002C2 RID: 706
		TPMailMessage OutputFile(MailMergeCodesWithTemplate EmailCodes);

		// Token: 0x060002C3 RID: 707
		TPMailMessage OutputFile(MailMergeCodesWithTemplate EmailCodes, bool isPlainText);

		// Token: 0x060002C4 RID: 708
		TPMailMessage MailMerge(MailMergeContextWithCustomDictionary ContextWithCustomDictionary, string TemplateXml);

		// Token: 0x060002C5 RID: 709
		TPMailMessage MailMerge(MailMergeContextWithCustomDictionary ContextWithCustomDictionary, int TemplateId);

		// Token: 0x060002C6 RID: 710
		TPMailMessage MailMerge(MailMergeContextWithCustomDictionary ContextWithCustomDictionary, Setting WebSettingEmailXmlTemplate);

		// Token: 0x060002C7 RID: 711
		IDictionary<int, TPMailMessage> MailMergeAccommodationLetterCoursesEmail(IList<int> LuCourseIds, MailMergeContextWithCustomDictionary ContextWithCustomDictionary, int TemplateId);

		// Token: 0x060002C8 RID: 712
		TPMailMessage MailMergeAccommodationSingleLetterEmail(IList<int> LuCourseIds, MailMergeContextWithCustomDictionary ContextWithCustomDictionary, int TemplateId);

		// Token: 0x060002C9 RID: 713
		IDictionary<MailMergeContext, TPMailMessage> MailMerge(IList<MailMergeContextWithCustomDictionary> ContextsWithCustomDictionaries, string TemplateXml);

		// Token: 0x060002CA RID: 714
		IDictionary<MailMergeContext, TPMailMessage> MailMerge(IList<MailMergeContextWithCustomDictionary> ContextsWithCustomDictionaries, int TemplateId);

		// Token: 0x060002CB RID: 715
		IDictionary<MailMergeContext, TPMailMessage> MailMerge(IList<MailMergeContextWithCustomDictionary> ContextsWithCustomDictionaries, Setting WebSettingExmailXmlTemplate);

		// Token: 0x060002CC RID: 716
		IList<MailMergedEmailWithOriginalRowAndDictionary> MailMergeAndReturnOriginalDataRows(DataTable t, string TemplateXml);
	}
}
