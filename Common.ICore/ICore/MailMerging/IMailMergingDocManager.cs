using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms.Accommodations;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.MailMergeEntities.DocumentForPrint;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.ICore.MailMerging
{
	// Token: 0x02000068 RID: 104
	public interface IMailMergingDocManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060002CD RID: 717
		MailMergeCodesWithTemplate ExtractUniqueCodes(BinaryFile WordFile, IDictionary<string, string> fieldMappings = null);

		// Token: 0x060002CE RID: 718
		MailMergeCodesWithTemplate ExtractUniqueCodes(int TemplateId);

		// Token: 0x060002CF RID: 719
		BinaryFile OutputFile(MailMergeCodesWithTemplate Codes, eFileFormat FileFormat);

		// Token: 0x060002D0 RID: 720
		BinaryFile OutputFile(BinaryFile Template, List<List<MailMergeCode>> MultipleCodes, eFileFormat OutputFileFormat);

		// Token: 0x060002D1 RID: 721
		BinaryFile MailMerge(MailMergeContextWithCustomDictionary ContextWithDictionary, eFileFormat OutputFileFormat, BinaryFile WordFile);

		// Token: 0x060002D2 RID: 722
		BinaryFile MailMerge(MailMergeContextWithCustomDictionary ContextWithDictionary, eFileFormat OutputFileFormat, int TemplateId);

		// Token: 0x060002D3 RID: 723
		BinaryFile MailMergeAccommodationLetter(IList<int> LuCourseIds, MailMergeContextWithCustomDictionary ContextWithDictionary, eFileFormat OutputFileFormat, int TemplateId);

		// Token: 0x060002D4 RID: 724
		BinaryFile MailMergeAccommodationSingleLetter(IList<int> LuCourseIds, MailMergeContextWithCustomDictionary ContextWithDictionary, eFileFormat OutputFileFormat, int TemplateId);

		// Token: 0x060002D5 RID: 725
		IDictionary<int, TPMailMessage> MailMergeAccommodationEmailsWithLettersAsAttachments(IList<int> LuCourseIds, MailMergeContextWithCustomDictionary ContextWithDictionary, eFileFormat OutputFileFormat, int TemplateId);

		// Token: 0x060002D6 RID: 726
		TPMailMessage MailMergeAccommodationSingleEmailWithLetterAsAttachment(IList<int> LuCourseIds, MailMergeContextWithCustomDictionary ContextWithDictionary, eFileFormat OutputFileFormat, int TemplateId);

		// Token: 0x060002D7 RID: 727
		BinaryFile MailMergeExamSheets(IList<MailMergeContextWithCustomDictionary> MailMergeContextsWithDictionaries, eFileFormat OutputFileFormat, int TemplateId);

		// Token: 0x060002D8 RID: 728
		BinaryFile MailMergeMailingLabels(IList<MailMergeContextWithCustomDictionary> MailMergeContextsWithDictionaries, eFileFormat OutputFileFormat, int TemplateId);

		// Token: 0x060002D9 RID: 729
		BinaryFile GenerateDocumentFromPrintCodes(IList<DocumentPrintItem> PrintItems, string FileName, eFileFormat OutputFormat);

		// Token: 0x060002DA RID: 730
		BinaryFile MailMergeMultipleItemsToOneDocument(IList<MailMergeContextWithCustomDictionary> MailMergeContextsWithCustomDictionaries, eFileFormat OutputFileFormat, int TemplateId);

		// Token: 0x060002DB RID: 731
		BinaryFile AutoMailMergeAccommodationLetter(AccommodationLetterGenerateContext GenerateContext);

		// Token: 0x060002DC RID: 732
		int MailMergeAndStoreSignatureButtonFileInDocuments(int StudentPersonId, MailMergeCustomDictionary CustomArgs, int TemplateId, eFileFormat OutputFormat, int OverrideFileListCid, string Title, IDictionary<int, string> ModifiedPerStudentFileLists, int[] FileListCidsOnLocalForm, out int[] FileListCidsFileWasStoredIn);
	}
}
