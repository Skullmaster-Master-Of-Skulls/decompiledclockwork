using System;
using System.Collections.Generic;
using System.IO;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.MailMerging
{
	// Token: 0x02000037 RID: 55
	public interface IMailMergingDocClientManager : IWebService
	{
		// Token: 0x0600017F RID: 383
		BinaryFileDTO MailMergeFromDocument(MailMergeContextWithCustomDictionaryDTO ContextWithDictionary, eFileFormatDTO OutputFileFormat, BinaryFileDTO WordFile);

		// Token: 0x06000180 RID: 384
		BinaryFileDTO MailMergeFromTemplate(MailMergeContextWithCustomDictionaryDTO ContextWithDictionary, eFileFormatDTO OutputFileFormat, int TemplateId);

		// Token: 0x06000181 RID: 385
		BinaryFileDTO MailMergeAccommodationLetter(IList<int> LuCourseIds, MailMergeContextWithCustomDictionaryDTO ContextWithDictionary, eFileFormatDTO OutputFileFormat, int TemplateId);

		// Token: 0x06000182 RID: 386
		BinaryFileDTO MailMergeAccommodationSingleLetter(IList<int> LuCourseIds, MailMergeContextWithCustomDictionaryDTO ContextWithDictionary, eFileFormatDTO OutputFileFormat, int TemplateId);

		// Token: 0x06000183 RID: 387
		IDictionary<int, TPMailMessageDTO> MailMergeAccommodationEmailsWithLettersAsAttachments(IList<int> LuCourseIds, MailMergeContextWithCustomDictionaryDTO ContextWithDictionary, eFileFormatDTO OutputFileFormat, int TemplateId);

		// Token: 0x06000184 RID: 388
		TPMailMessageDTO MailMergeAccommodationSingleEmailWithLetterAsAttachment(IList<int> LuCourseIds, MailMergeContextWithCustomDictionaryDTO ContextWithDictionary, eFileFormatDTO OutputFileFormat, int TemplateId);

		// Token: 0x06000185 RID: 389
		BinaryFileDTO MailMergeExamSheets(IList<MailMergeContextWithCustomDictionaryDTO> MailMergeContextsWithDictionaries, eFileFormatDTO OutputFileFormat, int TemplateId);

		// Token: 0x06000186 RID: 390
		BinaryFileDTO MailMergeMailingLabels(IList<MailMergeContextWithCustomDictionaryDTO> MailMergeContextsWithDictionaries, eFileFormatDTO OutputFileFormat, int TemplateId);

		// Token: 0x06000187 RID: 391
		BinaryFileDTO MailMergeMultipleItemsToOneDocument(IList<MailMergeContextWithCustomDictionaryDTO> MailMergeContextsWithDictionaries, eFileFormatDTO OutputFileFormat, int TemplateId);

		// Token: 0x06000188 RID: 392
		BinaryFileDTO AutoMailMergeAccommodationLetter(AccommodationLetterGenerateContextDTO GenerateContext);

		// Token: 0x06000189 RID: 393
		int MailMergeAndStoreSignatureButtonFileInDocuments(int StudentPersonId, MailMergeCustomDictionaryDTO CustomArgs, int TemplateId, eFileFormatDTO OutputFormat, int OverrideFileListCid, string Title, IDictionary<int, string> ModifiedPerStudentFileLists, int[] FileListCidsOnLocalForm, out int[] FileListCidsFileWasStoredIn);

		// Token: 0x0600018A RID: 394
		BinaryFileDTO GenerateAccommodationLetterForExternalLogicRulesUser(int studentPersonId, int luCourseId);

		// Token: 0x0600018B RID: 395
		string GenerateAllAccommodationLettersForExternalLogicRulesUser(Stream zipStream, int studentPersonId);
	}
}
