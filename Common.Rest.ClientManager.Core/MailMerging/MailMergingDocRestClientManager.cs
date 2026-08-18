using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.MailMerging;
using TechnoPro.Common.ClientManager.ICore.StudentAccommodationRequests;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.MailMerging
{
	// Token: 0x0200002F RID: 47
	public class MailMergingDocRestClientManager : BearerTokenRestProxy<IMailMergingDocClientManager>, IMailMergingDocClientManager, IWebService
	{
		// Token: 0x060001AC RID: 428 RVA: 0x00005F41 File Offset: 0x00004141
		public MailMergingDocRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00005F4B File Offset: 0x0000414B
		public MailMergingDocRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00005F58 File Offset: 0x00004158
		public BinaryFileDTO MailMergeFromDocument(MailMergeContextWithCustomDictionaryDTO ContextWithDictionary, eFileFormatDTO OutputFileFormat, BinaryFileDTO WordFile)
		{
			MailMergeDocFromDocumentReq mailMergeDocFromDocumentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeDocFromDocumentReq>();
			mailMergeDocFromDocumentReq.ContextWithCustomDictionary = ContextWithDictionary;
			mailMergeDocFromDocumentReq.OutputFileFormat = OutputFileFormat;
			mailMergeDocFromDocumentReq.BinaryFile = WordFile;
			BaseReportMessageReq baseReportMessageReq = mailMergeDocFromDocumentReq;
			ApplicationContext applicationContext = mailMergeDocFromDocumentReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return base.Post<MailMergeDocFromDocumentReq, BinaryFileDTO>(mailMergeDocFromDocumentReq, "mailmergingdoc/fromdocument");
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00005FAC File Offset: 0x000041AC
		public BinaryFileDTO MailMergeFromTemplate(MailMergeContextWithCustomDictionaryDTO ContextWithDictionary, eFileFormatDTO OutputFileFormat, int TemplateId)
		{
			MailMergeDocFromTemplateReq mailMergeDocFromTemplateReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeDocFromTemplateReq>();
			mailMergeDocFromTemplateReq.ContextWithCustomDictionary = ContextWithDictionary;
			mailMergeDocFromTemplateReq.OutputFileFormat = OutputFileFormat;
			mailMergeDocFromTemplateReq.TemplateId = TemplateId;
			BaseReportMessageReq baseReportMessageReq = mailMergeDocFromTemplateReq;
			ApplicationContext applicationContext = mailMergeDocFromTemplateReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return base.Post<MailMergeDocFromTemplateReq, BinaryFileDTO>(mailMergeDocFromTemplateReq, "mailmergingdoc/fromtemplate");
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00006000 File Offset: 0x00004200
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
			return base.Post<MailMergeAccommodationLetterReq, BinaryFileDTO>(mailMergeAccommodationLetterReq, "mailmergingdoc/accommodationletter");
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000605C File Offset: 0x0000425C
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
			return base.Post<MailMergeAccommodationSingleLetterReq, BinaryFileDTO>(mailMergeAccommodationSingleLetterReq, "mailmergingdoc/accommodationsingleletter");
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000060B8 File Offset: 0x000042B8
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
			return base.Post<MailMergeAccommodationEmailsWithLettersAsAttachmentsReq, MailMergeAccommodationEmailsWithLettersAsAttachmentsResp>(mailMergeAccommodationEmailsWithLettersAsAttachmentsReq, "mailmergingdoc/accommodationemailswithlettersasattachments").Emails;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00006118 File Offset: 0x00004318
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
			return base.Post<MailMergeAccommodationSingleEmailWithLetterAsAttachmentReq, TPMailMessageDTO>(mailMergeAccommodationSingleEmailWithLetterAsAttachmentReq, "mailmergingdoc/accommodationsingleemailwithletterasattachment");
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00006174 File Offset: 0x00004374
		public BinaryFileDTO MailMergeExamSheets(IList<MailMergeContextWithCustomDictionaryDTO> MailMergeContextsWithDictionaries, eFileFormatDTO OutputFileFormat, int TemplateId)
		{
			MailMergeExamSheetsReq mailMergeExamSheetsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeExamSheetsReq>();
			mailMergeExamSheetsReq.MailMergeContextsWithDictionaries = MailMergeContextsWithDictionaries;
			mailMergeExamSheetsReq.OutputFileFormat = OutputFileFormat;
			mailMergeExamSheetsReq.TemplateId = TemplateId;
			BaseReportMessageReq baseReportMessageReq = mailMergeExamSheetsReq;
			ApplicationContext applicationContext = mailMergeExamSheetsReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return base.Post<MailMergeExamSheetsReq, BinaryFileDTO>(mailMergeExamSheetsReq, "mailmergingdoc/examsheets");
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000061C8 File Offset: 0x000043C8
		public BinaryFileDTO MailMergeMailingLabels(IList<MailMergeContextWithCustomDictionaryDTO> MailMergeContextsWithDictionaries, eFileFormatDTO OutputFileFormat, int TemplateId)
		{
			MailMergeMailingLabelsReq mailMergeMailingLabelsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeMailingLabelsReq>();
			mailMergeMailingLabelsReq.MailMergeContextsWithDictionaries = MailMergeContextsWithDictionaries;
			mailMergeMailingLabelsReq.OutputFileFormat = OutputFileFormat;
			mailMergeMailingLabelsReq.TemplateId = TemplateId;
			BaseReportMessageReq baseReportMessageReq = mailMergeMailingLabelsReq;
			ApplicationContext applicationContext = mailMergeMailingLabelsReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return base.Post<MailMergeMailingLabelsReq, BinaryFileDTO>(mailMergeMailingLabelsReq, "mailmergingdoc/mailinglabels");
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000621C File Offset: 0x0000441C
		public BinaryFileDTO MailMergeMultipleItemsToOneDocument(IList<MailMergeContextWithCustomDictionaryDTO> MailMergeContextsWithDictionaries, eFileFormatDTO OutputFileFormat, int TemplateId)
		{
			MailMergeMultipleItemsToOneDocumentReq mailMergeMultipleItemsToOneDocumentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeMultipleItemsToOneDocumentReq>();
			mailMergeMultipleItemsToOneDocumentReq.MailMergeContextsWithDictionaries = MailMergeContextsWithDictionaries;
			mailMergeMultipleItemsToOneDocumentReq.OutputFileFormat = OutputFileFormat;
			mailMergeMultipleItemsToOneDocumentReq.TemplateId = TemplateId;
			BaseReportMessageReq baseReportMessageReq = mailMergeMultipleItemsToOneDocumentReq;
			ApplicationContext applicationContext = mailMergeMultipleItemsToOneDocumentReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return base.Post<MailMergeMultipleItemsToOneDocumentReq, BinaryFileDTO>(mailMergeMultipleItemsToOneDocumentReq, "mailmergingdoc/multipleitemstoonedocument");
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00006270 File Offset: 0x00004470
		public BinaryFileDTO AutoMailMergeAccommodationLetter(AccommodationLetterGenerateContextDTO GenerateContext)
		{
			AutoMailMergeAccommodationLetterReq autoMailMergeAccommodationLetterReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AutoMailMergeAccommodationLetterReq>();
			autoMailMergeAccommodationLetterReq.Context = GenerateContext;
			return base.Post<AutoMailMergeAccommodationLetterReq, BinaryFileDTO>(autoMailMergeAccommodationLetterReq, "mailmergingdoc/automailmergeaccommodationletter");
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000629C File Offset: 0x0000449C
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
			MailMergeAndStoreSignatureButtonFileInDocumentsResp mailMergeAndStoreSignatureButtonFileInDocumentsResp = base.Post<MailMergeAndStoreSignatureButtonFileInDocumentsReq, MailMergeAndStoreSignatureButtonFileInDocumentsResp>(mailMergeAndStoreSignatureButtonFileInDocumentsReq, "mailmergingdoc/mailmergeandstoresignaturebuttonfileindocument");
			FileListCidsFileWasStoredIn = mailMergeAndStoreSignatureButtonFileInDocumentsResp.FileListCidsFileWasStoredIn;
			return mailMergeAndStoreSignatureButtonFileInDocumentsResp.FileId;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00006328 File Offset: 0x00004528
		public BinaryFileDTO GenerateAccommodationLetterForExternalLogicRulesUser(int studentPersonId, int luCourseId)
		{
			GenerateAccommodationLetterForExternalLogicRulesUserReq generateAccommodationLetterForExternalLogicRulesUserReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GenerateAccommodationLetterForExternalLogicRulesUserReq>();
			generateAccommodationLetterForExternalLogicRulesUserReq.StudentPersonId = studentPersonId;
			generateAccommodationLetterForExternalLogicRulesUserReq.LuCourseId = luCourseId;
			return base.Post<GenerateAccommodationLetterForExternalLogicRulesUserReq, BinaryFileDTO>(generateAccommodationLetterForExternalLogicRulesUserReq, "mailmergingdoc/generateaccommodationletterforexternallogicrulesuser");
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000635C File Offset: 0x0000455C
		public string GenerateAllAccommodationLettersForExternalLogicRulesUser(Stream outputZipStream, int studentPersonId)
		{
			AllowedStudentCourseRegistrationsForCustomEmailLogicDTO coursesAllowedBySelfRegCustomLogicRulesToViewLoaFor = ObjectFactory.Resolve<ISelfRegClientManager>().GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaFor(studentPersonId);
			using (PositionWrapperStream positionWrapperStream = new PositionWrapperStream(outputZipStream))
			{
				using (ZipArchive zipArchive = new ZipArchive(positionWrapperStream, ZipArchiveMode.Create, true))
				{
					foreach (CourseRegistrationDTO courseRegistrationDTO in coursesAllowedBySelfRegCustomLogicRulesToViewLoaFor.CourseRegistrations)
					{
						BinaryFileDTO binaryFileDTO = this.GenerateAccommodationLetterForExternalLogicRulesUser(studentPersonId, courseRegistrationDTO.Course.LuCourseId);
						using (MemoryStream memoryStream = new MemoryStream(binaryFileDTO.ByteArray))
						{
							using (Stream stream = zipArchive.CreateEntry(binaryFileDTO.FileName).Open())
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
