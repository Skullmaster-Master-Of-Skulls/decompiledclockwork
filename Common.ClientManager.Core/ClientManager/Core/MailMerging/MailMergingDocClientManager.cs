using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.ClientManager.Core.StudentAccommodationRequests;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.MailMerging;
using TechnoPro.Common.ClientManager.ICore.StudentAccommodationRequests;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.MailMerging
{
	// Token: 0x0200003B RID: 59
	public class MailMergingDocClientManager : IMailMergingDocClientManager, IWebService
	{
		// Token: 0x06000216 RID: 534 RVA: 0x00009E44 File Offset: 0x00008044
		public BinaryFileDTO MailMergeFromDocument(MailMergeContextWithCustomDictionaryDTO ContextWithDictionary, eFileFormatDTO OutputFileFormat, BinaryFileDTO WordFile)
		{
			MailMergeDocFromDocumentReq mailMergeDocFromDocumentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeDocFromDocumentReq>();
			mailMergeDocFromDocumentReq.ContextWithCustomDictionary = ContextWithDictionary;
			mailMergeDocFromDocumentReq.OutputFileFormat = OutputFileFormat;
			mailMergeDocFromDocumentReq.BinaryFile = WordFile;
			BaseReportMessageReq baseReportMessageReq = mailMergeDocFromDocumentReq;
			ApplicationContext applicationContext = mailMergeDocFromDocumentReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IMailMergingDoc>().MailMergeFromDocument(mailMergeDocFromDocumentReq).Document;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00009EA4 File Offset: 0x000080A4
		public BinaryFileDTO MailMergeFromTemplate(MailMergeContextWithCustomDictionaryDTO ContextWithDictionary, eFileFormatDTO OutputFileFormat, int TemplateId)
		{
			MailMergeDocFromTemplateReq mailMergeDocFromTemplateReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeDocFromTemplateReq>();
			mailMergeDocFromTemplateReq.ContextWithCustomDictionary = ContextWithDictionary;
			mailMergeDocFromTemplateReq.OutputFileFormat = OutputFileFormat;
			mailMergeDocFromTemplateReq.TemplateId = TemplateId;
			BaseReportMessageReq baseReportMessageReq = mailMergeDocFromTemplateReq;
			ApplicationContext applicationContext = mailMergeDocFromTemplateReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IMailMergingDoc>().MailMergeFromTemplate(mailMergeDocFromTemplateReq).Document;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00009F04 File Offset: 0x00008104
		public BinaryFileDTO MailMergeAccommodationLetter(IList<int> LuCourseIds, MailMergeContextWithCustomDictionaryDTO ContextWithDictionary, eFileFormatDTO OutputFileFormat, int TemplateId)
		{
			MailMergeAccommodationLetterReq mailMergeAccommodationLetterReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeAccommodationLetterReq>();
			mailMergeAccommodationLetterReq.ContextWithCustomDictionary = ContextWithDictionary;
			mailMergeAccommodationLetterReq.OutputFileFormat = OutputFileFormat;
			mailMergeAccommodationLetterReq.TemplateId = TemplateId;
			mailMergeAccommodationLetterReq.LuCourseIds = LuCourseIds;
			BaseReportMessageReq baseReportMessageReq = mailMergeAccommodationLetterReq;
			ApplicationContext applicationContext = mailMergeAccommodationLetterReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IMailMergingDoc>().MailMergeAccommodationLetter(mailMergeAccommodationLetterReq).Document;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00009F6C File Offset: 0x0000816C
		public BinaryFileDTO MailMergeAccommodationSingleLetter(IList<int> LuCourseIds, MailMergeContextWithCustomDictionaryDTO ContextWithDictionary, eFileFormatDTO OutputFileFormat, int TemplateId)
		{
			MailMergeAccommodationSingleLetterReq mailMergeAccommodationSingleLetterReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeAccommodationSingleLetterReq>();
			mailMergeAccommodationSingleLetterReq.ContextWithCustomDictionary = ContextWithDictionary;
			mailMergeAccommodationSingleLetterReq.OutputFileFormat = OutputFileFormat;
			mailMergeAccommodationSingleLetterReq.TemplateId = TemplateId;
			mailMergeAccommodationSingleLetterReq.LuCourseIds = LuCourseIds;
			BaseReportMessageReq baseReportMessageReq = mailMergeAccommodationSingleLetterReq;
			ApplicationContext applicationContext = mailMergeAccommodationSingleLetterReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IMailMergingDoc>().MailMergeAccommodationSingleLetter(mailMergeAccommodationSingleLetterReq).Document;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00009FD4 File Offset: 0x000081D4
		public IDictionary<int, TPMailMessageDTO> MailMergeAccommodationEmailsWithLettersAsAttachments(IList<int> LuCourseIds, MailMergeContextWithCustomDictionaryDTO ContextWithDictionary, eFileFormatDTO OutputFileFormat, int TemplateId)
		{
			MailMergeAccommodationEmailsWithLettersAsAttachmentsReq mailMergeAccommodationEmailsWithLettersAsAttachmentsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeAccommodationEmailsWithLettersAsAttachmentsReq>();
			mailMergeAccommodationEmailsWithLettersAsAttachmentsReq.ContextWithCustomDictionary = ContextWithDictionary;
			mailMergeAccommodationEmailsWithLettersAsAttachmentsReq.OutputFileFormat = OutputFileFormat;
			mailMergeAccommodationEmailsWithLettersAsAttachmentsReq.TemplateId = TemplateId;
			mailMergeAccommodationEmailsWithLettersAsAttachmentsReq.LuCourseIds = LuCourseIds;
			BaseReportMessageReq baseReportMessageReq = mailMergeAccommodationEmailsWithLettersAsAttachmentsReq;
			ApplicationContext applicationContext = mailMergeAccommodationEmailsWithLettersAsAttachmentsReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IMailMergingDoc>().MailMergeAccommodationEmailsWithLettersAsAttachments(mailMergeAccommodationEmailsWithLettersAsAttachmentsReq).Emails;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000A03C File Offset: 0x0000823C
		public TPMailMessageDTO MailMergeAccommodationSingleEmailWithLetterAsAttachment(IList<int> LuCourseIds, MailMergeContextWithCustomDictionaryDTO ContextWithDictionary, eFileFormatDTO OutputFileFormat, int TemplateId)
		{
			MailMergeAccommodationSingleEmailWithLetterAsAttachmentReq mailMergeAccommodationSingleEmailWithLetterAsAttachmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeAccommodationSingleEmailWithLetterAsAttachmentReq>();
			mailMergeAccommodationSingleEmailWithLetterAsAttachmentReq.ContextWithCustomDictionary = ContextWithDictionary;
			mailMergeAccommodationSingleEmailWithLetterAsAttachmentReq.OutputFileFormat = OutputFileFormat;
			mailMergeAccommodationSingleEmailWithLetterAsAttachmentReq.TemplateId = TemplateId;
			mailMergeAccommodationSingleEmailWithLetterAsAttachmentReq.LuCourseIds = LuCourseIds;
			BaseReportMessageReq baseReportMessageReq = mailMergeAccommodationSingleEmailWithLetterAsAttachmentReq;
			ApplicationContext applicationContext = mailMergeAccommodationSingleEmailWithLetterAsAttachmentReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IMailMergingDoc>().MailMergeAccommodationSingleEmailWithLetterAsAttachment(mailMergeAccommodationSingleEmailWithLetterAsAttachmentReq).Email;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000A0A4 File Offset: 0x000082A4
		public BinaryFileDTO MailMergeExamSheets(IList<MailMergeContextWithCustomDictionaryDTO> MailMergeContextsWithDictionaries, eFileFormatDTO OutputFileFormat, int TemplateId)
		{
			MailMergeExamSheetsReq mailMergeExamSheetsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeExamSheetsReq>();
			mailMergeExamSheetsReq.MailMergeContextsWithDictionaries = MailMergeContextsWithDictionaries;
			mailMergeExamSheetsReq.OutputFileFormat = OutputFileFormat;
			mailMergeExamSheetsReq.TemplateId = TemplateId;
			BaseReportMessageReq baseReportMessageReq = mailMergeExamSheetsReq;
			ApplicationContext applicationContext = mailMergeExamSheetsReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IMailMergingDoc>().MailMergeExamSheets(mailMergeExamSheetsReq).Document;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000A104 File Offset: 0x00008304
		public BinaryFileDTO MailMergeMailingLabels(IList<MailMergeContextWithCustomDictionaryDTO> MailMergeContextsWithDictionaries, eFileFormatDTO OutputFileFormat, int TemplateId)
		{
			MailMergeMailingLabelsReq mailMergeMailingLabelsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeMailingLabelsReq>();
			mailMergeMailingLabelsReq.MailMergeContextsWithDictionaries = MailMergeContextsWithDictionaries;
			mailMergeMailingLabelsReq.OutputFileFormat = OutputFileFormat;
			mailMergeMailingLabelsReq.TemplateId = TemplateId;
			BaseReportMessageReq baseReportMessageReq = mailMergeMailingLabelsReq;
			ApplicationContext applicationContext = mailMergeMailingLabelsReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IMailMergingDoc>().MailMergeMailingLabels(mailMergeMailingLabelsReq).Document;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000A164 File Offset: 0x00008364
		public BinaryFileDTO MailMergeMultipleItemsToOneDocument(IList<MailMergeContextWithCustomDictionaryDTO> MailMergeContextsWithDictionaries, eFileFormatDTO OutputFileFormat, int TemplateId)
		{
			MailMergeMultipleItemsToOneDocumentReq mailMergeMultipleItemsToOneDocumentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeMultipleItemsToOneDocumentReq>();
			mailMergeMultipleItemsToOneDocumentReq.MailMergeContextsWithDictionaries = MailMergeContextsWithDictionaries;
			mailMergeMultipleItemsToOneDocumentReq.OutputFileFormat = OutputFileFormat;
			mailMergeMultipleItemsToOneDocumentReq.TemplateId = TemplateId;
			BaseReportMessageReq baseReportMessageReq = mailMergeMultipleItemsToOneDocumentReq;
			ApplicationContext applicationContext = mailMergeMultipleItemsToOneDocumentReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IMailMergingDoc>().MailMergeMultipleItemsToOneDocument(mailMergeMultipleItemsToOneDocumentReq).Document;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000A1C4 File Offset: 0x000083C4
		public BinaryFileDTO AutoMailMergeAccommodationLetter(AccommodationLetterGenerateContextDTO GenerateContext)
		{
			AutoMailMergeAccommodationLetterReq autoMailMergeAccommodationLetterReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AutoMailMergeAccommodationLetterReq>();
			autoMailMergeAccommodationLetterReq.Context = GenerateContext;
			return ClientServiceFactory.GetClientInstance<IMailMergingDoc>().AutoMailMergeAccommodationLetter(autoMailMergeAccommodationLetterReq).Document;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000A1FC File Offset: 0x000083FC
		public int MailMergeAndStoreSignatureButtonFileInDocuments(int StudentPersonId, MailMergeCustomDictionaryDTO CustomArgs, int TemplateId, eFileFormatDTO OutputFormat, int OverrideFileListCid, string Title, IDictionary<int, string> ModifiedPerStudentFileLists, int[] FileListCidsOnLocalForm, out int[] FileListCidsFileWasStoredIn)
		{
			MailMergeAndStoreSignatureButtonFileInDocumentsReq mailMergeAndStoreSignatureButtonFileInDocumentsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeAndStoreSignatureButtonFileInDocumentsReq>();
			mailMergeAndStoreSignatureButtonFileInDocumentsReq.StudentPersonId = StudentPersonId;
			mailMergeAndStoreSignatureButtonFileInDocumentsReq.CustomArgs = CustomArgs;
			mailMergeAndStoreSignatureButtonFileInDocumentsReq.TemplateId = TemplateId;
			mailMergeAndStoreSignatureButtonFileInDocumentsReq.OutputFormat = OutputFormat;
			mailMergeAndStoreSignatureButtonFileInDocumentsReq.OverrideFileListCid = OverrideFileListCid;
			mailMergeAndStoreSignatureButtonFileInDocumentsReq.Title = Title;
			mailMergeAndStoreSignatureButtonFileInDocumentsReq.ModifiedPerStudentFileLists = ModifiedPerStudentFileLists;
			mailMergeAndStoreSignatureButtonFileInDocumentsReq.FileListCidsOnLocalForm = FileListCidsOnLocalForm;
			BaseReportMessageReq baseReportMessageReq = mailMergeAndStoreSignatureButtonFileInDocumentsReq;
			ApplicationContext applicationContext = mailMergeAndStoreSignatureButtonFileInDocumentsReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			MailMergeAndStoreSignatureButtonFileInDocumentsResp mailMergeAndStoreSignatureButtonFileInDocumentsResp = ClientServiceFactory.GetClientInstance<IMailMergingDoc>().MailMergeAndStoreSignatureButtonFileInDocuments(mailMergeAndStoreSignatureButtonFileInDocumentsReq);
			FileListCidsFileWasStoredIn = mailMergeAndStoreSignatureButtonFileInDocumentsResp.FileListCidsFileWasStoredIn;
			return mailMergeAndStoreSignatureButtonFileInDocumentsResp.FileId;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000A294 File Offset: 0x00008494
		public BinaryFileDTO GenerateAccommodationLetterForExternalLogicRulesUser(int studentPersonId, int luCourseId)
		{
			GenerateAccommodationLetterForExternalLogicRulesUserReq generateAccommodationLetterForExternalLogicRulesUserReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GenerateAccommodationLetterForExternalLogicRulesUserReq>();
			generateAccommodationLetterForExternalLogicRulesUserReq.StudentPersonId = studentPersonId;
			generateAccommodationLetterForExternalLogicRulesUserReq.LuCourseId = luCourseId;
			GenerateAccommodationLetterForExternalLogicRulesUserResp generateAccommodationLetterForExternalLogicRulesUserResp = ClientServiceFactory.GetClientInstance<IMailMergingDoc>().GenerateAccommodationLetterForExternalLogicRulesUser(generateAccommodationLetterForExternalLogicRulesUserReq);
			return (generateAccommodationLetterForExternalLogicRulesUserResp != null) ? generateAccommodationLetterForExternalLogicRulesUserResp.AccommodationLetter : null;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000A2D8 File Offset: 0x000084D8
		public string GenerateAllAccommodationLettersForExternalLogicRulesUser(Stream outputZipStream, int studentPersonId)
		{
			ISelfRegClientManager selfRegClientManager = new SelfRegClientManager();
			AllowedStudentCourseRegistrationsForCustomEmailLogicDTO coursesAllowedBySelfRegCustomLogicRulesToViewLoaFor = selfRegClientManager.GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaFor(studentPersonId);
			using (PositionWrapperStream positionWrapperStream = new PositionWrapperStream(outputZipStream))
			{
				using (ZipArchive zipArchive = new ZipArchive(positionWrapperStream, ZipArchiveMode.Create, true))
				{
					foreach (CourseRegistrationDTO courseRegistrationDTO in coursesAllowedBySelfRegCustomLogicRulesToViewLoaFor.CourseRegistrations)
					{
						BinaryFileDTO binaryFileDTO = this.GenerateAccommodationLetterForExternalLogicRulesUser(studentPersonId, courseRegistrationDTO.Course.LuCourseId);
						using (MemoryStream memoryStream = new MemoryStream(binaryFileDTO.ByteArray))
						{
							ZipArchiveEntry zipArchiveEntry = zipArchive.CreateEntry(binaryFileDTO.FileName);
							using (Stream stream = zipArchiveEntry.Open())
							{
								memoryStream.WriteTo(stream);
							}
						}
					}
				}
			}
			string str = "acc_";
			string text;
			if (coursesAllowedBySelfRegCustomLogicRulesToViewLoaFor == null)
			{
				text = null;
			}
			else
			{
				PersonBaseDTO student = coursesAllowedBySelfRegCustomLogicRulesToViewLoaFor.Student;
				text = ((student != null) ? student.Student_no : null);
			}
			return str + (text ?? "") + ".zip";
		}
	}
}
