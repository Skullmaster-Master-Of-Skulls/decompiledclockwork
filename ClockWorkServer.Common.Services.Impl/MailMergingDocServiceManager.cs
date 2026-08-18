using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.Core.LookupCourses;
using TechnoPro.Common.Core.Mappers.DynamicForms.Accommodations;
using TechnoPro.Common.Core.Mappers.Files;
using TechnoPro.Common.Core.Mappers.MailMergeEntities;
using TechnoPro.Common.Core.Mappers.TPMailMan;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.Core.SpireDoc;
using TechnoPro.Common.Core.StudentAccommodationRequests;
using TechnoPro.Common.ICore.LookupCourses;
using TechnoPro.Common.ICore.MailMerging;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.StudentAccommodationRequests;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests.SelfRegEmail;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000068 RID: 104
	public class MailMergingDocServiceManager : IMailMergingDoc, IService
	{
		// Token: 0x060003D2 RID: 978 RVA: 0x00011AEC File Offset: 0x0000FCEC
		public GenerateAccommodationLetterForExternalLogicRulesUserResp GenerateAccommodationLetterForExternalLogicRulesUser(GenerateAccommodationLetterForExternalLogicRulesUserReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			bool flag = Request.StudentPersonId < 1 || Request.LuCourseId < 1 || operationContext.WhoAmI < 1;
			GenerateAccommodationLetterForExternalLogicRulesUserResp result;
			if (flag)
			{
				result = null;
			}
			else
			{
				ISelfRegManager selfRegManager = new SelfRegManager(operationContext);
				SelfRegEmailLogicRule selfRegEmailLogicRule = selfRegManager.FindLogicRuleThatApplies(Request.StudentPersonId, Request.LuCourseId);
				bool flag2 = selfRegEmailLogicRule == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					LoaExternalAccessLogItem logItem = new LoaExternalAccessLogItem
					{
						StaffPersonId = Request.WhoAmI,
						StudentPersonId = Request.StudentPersonId,
						LuCourseId = Request.LuCourseId
					};
					ISelfRegTrackingManager selfRegTrackingManager = new SelfRegTrackingManager
					{
						OpContext = operationContext
					};
					Task.Run(() => selfRegTrackingManager.LogExternalStaffLoaAccessAsync(logItem));
					IMailMergingDocManager mailMergingDocManager = new MailMergingDocManager(operationContext);
					BinaryFile binaryFile = mailMergingDocManager.MailMergeAccommodationLetter(new int[]
					{
						Request.LuCourseId
					}.ToList<int>(), new MailMergeContextWithCustomDictionary
					{
						Context = new MailMergeContext
						{
							PersonId = Request.StudentPersonId,
							LuCourseId = Request.LuCourseId,
							WhoAmId = operationContext.WhoAmI
						},
						CustomDictionary = new MailMergeCustomDictionary
						{
							Args = new Dictionary<string, string>()
						}
					}, eFileFormat.PDF, selfRegEmailLogicRule.LetterTemplateId);
					string text = (binaryFile != null) ? binaryFile.FileName : null;
					bool flag3 = !string.IsNullOrEmpty(text);
					if (flag3)
					{
						IPeopleManager peopleManager = new PeopleManager(operationContext);
						PersonBase personBase = peopleManager.LoadPerson(Request.StudentPersonId);
						ILookupCourseManager lookupCourseManager = new LookupCourseManager(operationContext);
						IList<LookupCourseBase> list = lookupCourseManager.LoadCourseBasesByIds(new int[]
						{
							Request.LuCourseId
						});
						LookupCourseBase lookupCourseBase = (list != null) ? list.FirstOrDefault<LookupCourseBase>() : null;
						BinaryFile binaryFile2 = binaryFile;
						string[] array = new string[8];
						array[0] = Path.GetFileNameWithoutExtension(text);
						array[1] = "_";
						array[2] = (((personBase != null) ? personBase.Student_no : null) ?? "");
						array[3] = "_";
						int num = 4;
						string text2;
						if (lookupCourseBase == null)
						{
							text2 = null;
						}
						else
						{
							LookupSubject subject = lookupCourseBase.Subject;
							text2 = ((subject != null) ? subject.SubjectDescription : null);
						}
						array[num] = (text2 ?? "");
						array[5] = "_";
						array[6] = (((lookupCourseBase != null) ? lookupCourseBase.Course : null) ?? "");
						array[7] = Path.GetExtension(text);
						binaryFile2.FileName = string.Concat(array);
					}
					result = new GenerateAccommodationLetterForExternalLogicRulesUserResp
					{
						AccommodationLetter = ((binaryFile != null) ? binaryFile.ToDTO() : null)
					};
				}
			}
			return result;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00011D54 File Offset: 0x0000FF54
		public MailMergeDocFromTemplateResp MailMergeFromTemplate(MailMergeDocFromTemplateReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			operationContext.WhoAmI = (Request.ContextWithCustomDictionary.Context.WhoAmId = Math.Max(Request.WhoAmI, Request.ContextWithCustomDictionary.Context.WhoAmId));
			IMailMergingDocManager mailMergingDocManager = new MailMergingDocManager(operationContext);
			BinaryFile binaryFile = mailMergingDocManager.MailMerge(Request.ContextWithCustomDictionary.ToDomainObject(), (eFileFormat)Request.OutputFileFormat, Request.TemplateId);
			return new MailMergeDocFromTemplateResp
			{
				Document = binaryFile.ToDTO()
			};
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00011DDC File Offset: 0x0000FFDC
		public MailMergeDocFromDocumentResp MailMergeFromDocument(MailMergeDocFromDocumentReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			operationContext.WhoAmI = (Request.ContextWithCustomDictionary.Context.WhoAmId = Math.Max(Request.WhoAmI, Request.ContextWithCustomDictionary.Context.WhoAmId));
			IMailMergingDocManager mailMergingDocManager = new MailMergingDocManager(operationContext);
			BinaryFile binaryFile = mailMergingDocManager.MailMerge(Request.ContextWithCustomDictionary.ToDomainObject(), (eFileFormat)Request.OutputFileFormat, Request.BinaryFile.ToDomainObject());
			return new MailMergeDocFromDocumentResp
			{
				Document = binaryFile.ToDTO()
			};
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00011E6C File Offset: 0x0001006C
		public MailMergeAccommodationLetterResp MailMergeAccommodationLetter(MailMergeAccommodationLetterReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			operationContext.WhoAmI = (Request.ContextWithCustomDictionary.Context.WhoAmId = Math.Max(Request.WhoAmI, Request.ContextWithCustomDictionary.Context.WhoAmId));
			IMailMergingDocManager mailMergingDocManager = new MailMergingDocManager(operationContext);
			BinaryFile binaryFile = mailMergingDocManager.MailMergeAccommodationLetter(Request.LuCourseIds, Request.ContextWithCustomDictionary.ToDomainObject(), (eFileFormat)Request.OutputFileFormat, Request.TemplateId);
			return new MailMergeAccommodationLetterResp
			{
				Document = binaryFile.ToDTO()
			};
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00011EFC File Offset: 0x000100FC
		public MailMergeAccommodationSingleLetterResp MailMergeAccommodationSingleLetter(MailMergeAccommodationSingleLetterReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			operationContext.WhoAmI = (Request.ContextWithCustomDictionary.Context.WhoAmId = Math.Max(Request.WhoAmI, Request.ContextWithCustomDictionary.Context.WhoAmId));
			IMailMergingDocManager mailMergingDocManager = new MailMergingDocManager(operationContext);
			BinaryFile binaryFile = mailMergingDocManager.MailMergeAccommodationSingleLetter(Request.LuCourseIds, Request.ContextWithCustomDictionary.ToDomainObject(), (eFileFormat)Request.OutputFileFormat, Request.TemplateId);
			return new MailMergeAccommodationSingleLetterResp
			{
				Document = binaryFile.ToDTO()
			};
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00011F8C File Offset: 0x0001018C
		public MailMergeAccommodationSingleEmailWithLetterAsAttachmentResp MailMergeAccommodationSingleEmailWithLetterAsAttachment(MailMergeAccommodationSingleEmailWithLetterAsAttachmentReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			operationContext.WhoAmI = (Request.ContextWithCustomDictionary.Context.WhoAmId = Math.Max(Request.WhoAmI, Request.ContextWithCustomDictionary.Context.WhoAmId));
			IMailMergingDocManager mailMergingDocManager = new MailMergingDocManager(operationContext);
			TPMailMessage tPMailMessage = mailMergingDocManager.MailMergeAccommodationSingleEmailWithLetterAsAttachment(Request.LuCourseIds, Request.ContextWithCustomDictionary.ToDomainObject(), (eFileFormat)Request.OutputFileFormat, Request.TemplateId);
			return new MailMergeAccommodationSingleEmailWithLetterAsAttachmentResp
			{
				Email = tPMailMessage.ToDTO()
			};
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0001201C File Offset: 0x0001021C
		public MailMergeAccommodationEmailsWithLettersAsAttachmentsResp MailMergeAccommodationEmailsWithLettersAsAttachments(MailMergeAccommodationEmailsWithLettersAsAttachmentsReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			operationContext.WhoAmI = (Request.ContextWithCustomDictionary.Context.WhoAmId = Math.Max(Request.WhoAmI, Request.ContextWithCustomDictionary.Context.WhoAmId));
			IMailMergingDocManager mailMergingDocManager = new MailMergingDocManager(operationContext);
			IDictionary<int, TPMailMessage> source = mailMergingDocManager.MailMergeAccommodationEmailsWithLettersAsAttachments(Request.LuCourseIds, Request.ContextWithCustomDictionary.ToDomainObject(), (eFileFormat)Request.OutputFileFormat, Request.TemplateId);
			Dictionary<int, TPMailMessageDTO> emails = source.ToDictionary((KeyValuePair<int, TPMailMessage> email) => email.Key, (KeyValuePair<int, TPMailMessage> email) => email.Value.ToDTO());
			return new MailMergeAccommodationEmailsWithLettersAsAttachmentsResp
			{
				Emails = emails
			};
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x000120EC File Offset: 0x000102EC
		public MailMergeExamSheetsResp MailMergeExamSheets(MailMergeExamSheetsReq Request)
		{
			IMailMergingDocManager mailMergingDocManager = new MailMergingDocManager(Request.GetOperationContext());
			List<MailMergeContextWithCustomDictionary> list = Request.MailMergeContextsWithDictionaries.ToList<MailMergeContextWithCustomDictionaryDTO>().ConvertAll<MailMergeContextWithCustomDictionary>((MailMergeContextWithCustomDictionaryDTO f) => f.ToDomainObject());
			foreach (MailMergeContextWithCustomDictionary mailMergeContextWithCustomDictionary in list)
			{
				mailMergeContextWithCustomDictionary.Context.WhoAmId = Request.WhoAmI;
			}
			BinaryFile binaryFile = mailMergingDocManager.MailMergeExamSheets(list, (eFileFormat)Request.OutputFileFormat, Request.TemplateId);
			return new MailMergeExamSheetsResp
			{
				Document = binaryFile.ToDTO()
			};
		}

		// Token: 0x060003DA RID: 986 RVA: 0x000121B4 File Offset: 0x000103B4
		public MailMergeMailingLabelsResp MailMergeMailingLabels(MailMergeMailingLabelsReq Request)
		{
			IMailMergingDocManager mailMergingDocManager = new MailMergingDocManager(Request.GetOperationContext());
			List<MailMergeContextWithCustomDictionary> list = Request.MailMergeContextsWithDictionaries.ToList<MailMergeContextWithCustomDictionaryDTO>().ConvertAll<MailMergeContextWithCustomDictionary>((MailMergeContextWithCustomDictionaryDTO f) => f.ToDomainObject());
			foreach (MailMergeContextWithCustomDictionary mailMergeContextWithCustomDictionary in list)
			{
				mailMergeContextWithCustomDictionary.Context.WhoAmId = Request.WhoAmI;
			}
			BinaryFile binaryFile = mailMergingDocManager.MailMergeMailingLabels(list, (eFileFormat)Request.OutputFileFormat, Request.TemplateId);
			return new MailMergeMailingLabelsResp
			{
				Document = binaryFile.ToDTO()
			};
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0001227C File Offset: 0x0001047C
		public MailMergeMultipleItemsToOneDocumentResp MailMergeMultipleItemsToOneDocument(MailMergeMultipleItemsToOneDocumentReq Request)
		{
			IMailMergingDocManager mailMergingDocManager = new MailMergingDocManager(Request.GetOperationContext());
			List<MailMergeContextWithCustomDictionary> list = Request.MailMergeContextsWithDictionaries.ToList<MailMergeContextWithCustomDictionaryDTO>().ConvertAll<MailMergeContextWithCustomDictionary>((MailMergeContextWithCustomDictionaryDTO f) => f.ToDomainObject());
			foreach (MailMergeContextWithCustomDictionary mailMergeContextWithCustomDictionary in list)
			{
				mailMergeContextWithCustomDictionary.Context.WhoAmId = Request.WhoAmI;
			}
			BinaryFile binaryFile = mailMergingDocManager.MailMergeMultipleItemsToOneDocument(list, (eFileFormat)Request.OutputFileFormat, Request.TemplateId);
			return new MailMergeMultipleItemsToOneDocumentResp
			{
				Document = binaryFile.ToDTO()
			};
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00012344 File Offset: 0x00010544
		public AutoMailMergeAccommodationLetterResp AutoMailMergeAccommodationLetter(AutoMailMergeAccommodationLetterReq Request)
		{
			IMailMergingDocManager mailMergingDocManager = new MailMergingDocManager(Request.GetOperationContext());
			BinaryFile binaryFile = mailMergingDocManager.AutoMailMergeAccommodationLetter(Request.Context.ToDomainObject());
			return new AutoMailMergeAccommodationLetterResp
			{
				Document = binaryFile.ToDTO()
			};
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00012388 File Offset: 0x00010588
		public MailMergeAndStoreSignatureButtonFileInDocumentsResp MailMergeAndStoreSignatureButtonFileInDocuments(MailMergeAndStoreSignatureButtonFileInDocumentsReq Request)
		{
			IMailMergingDocManager mailMergingDocManager = new MailMergingDocManager(Request.GetOperationContext());
			IMailMergingDocManager mailMergingDocManager2 = mailMergingDocManager;
			int studentPersonId = Request.StudentPersonId;
			MailMergeCustomDictionaryDTO customArgs = Request.CustomArgs;
			int[] fileListCidsFileWasStoredIn;
			int fileId = mailMergingDocManager2.MailMergeAndStoreSignatureButtonFileInDocuments(studentPersonId, (customArgs != null) ? customArgs.ToDomainObject() : null, Request.TemplateId, (eFileFormat)Request.OutputFormat, Request.OverrideFileListCid, Request.Title, Request.ModifiedPerStudentFileLists, Request.FileListCidsOnLocalForm, out fileListCidsFileWasStoredIn);
			return new MailMergeAndStoreSignatureButtonFileInDocumentsResp
			{
				FileId = fileId,
				FileListCidsFileWasStoredIn = fileListCidsFileWasStoredIn
			};
		}
	}
}
