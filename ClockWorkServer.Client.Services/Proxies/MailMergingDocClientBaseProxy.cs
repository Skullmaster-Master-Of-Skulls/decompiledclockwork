using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000E7 RID: 231
	internal class MailMergingDocClientBaseProxy : ClientBase<IMailMergingDoc>, IMailMergingDoc, IService
	{
		// Token: 0x060008F3 RID: 2291 RVA: 0x000172C8 File Offset: 0x000154C8
		public MailMergingDocClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x000172D3 File Offset: 0x000154D3
		public MailMergingDocClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x000172E0 File Offset: 0x000154E0
		public MailMergeDocFromDocumentResp MailMergeFromDocument(MailMergeDocFromDocumentReq Request)
		{
			return base.Channel.MailMergeFromDocument(Request);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00017300 File Offset: 0x00015500
		public MailMergeDocFromTemplateResp MailMergeFromTemplate(MailMergeDocFromTemplateReq Request)
		{
			return base.Channel.MailMergeFromTemplate(Request);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00017320 File Offset: 0x00015520
		public MailMergeAccommodationLetterResp MailMergeAccommodationLetter(MailMergeAccommodationLetterReq Request)
		{
			return base.Channel.MailMergeAccommodationLetter(Request);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00017340 File Offset: 0x00015540
		public MailMergeAccommodationEmailsWithLettersAsAttachmentsResp MailMergeAccommodationEmailsWithLettersAsAttachments(MailMergeAccommodationEmailsWithLettersAsAttachmentsReq Request)
		{
			return base.Channel.MailMergeAccommodationEmailsWithLettersAsAttachments(Request);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x00017360 File Offset: 0x00015560
		public MailMergeAccommodationSingleEmailWithLetterAsAttachmentResp MailMergeAccommodationSingleEmailWithLetterAsAttachment(MailMergeAccommodationSingleEmailWithLetterAsAttachmentReq Request)
		{
			return base.Channel.MailMergeAccommodationSingleEmailWithLetterAsAttachment(Request);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00017380 File Offset: 0x00015580
		public MailMergeAccommodationSingleLetterResp MailMergeAccommodationSingleLetter(MailMergeAccommodationSingleLetterReq Request)
		{
			return base.Channel.MailMergeAccommodationSingleLetter(Request);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x000173A0 File Offset: 0x000155A0
		public MailMergeExamSheetsResp MailMergeExamSheets(MailMergeExamSheetsReq Request)
		{
			return base.Channel.MailMergeExamSheets(Request);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x000173C0 File Offset: 0x000155C0
		public MailMergeMailingLabelsResp MailMergeMailingLabels(MailMergeMailingLabelsReq Request)
		{
			return base.Channel.MailMergeMailingLabels(Request);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x000173E0 File Offset: 0x000155E0
		public MailMergeMultipleItemsToOneDocumentResp MailMergeMultipleItemsToOneDocument(MailMergeMultipleItemsToOneDocumentReq Request)
		{
			return base.Channel.MailMergeMultipleItemsToOneDocument(Request);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00017400 File Offset: 0x00015600
		public AutoMailMergeAccommodationLetterResp AutoMailMergeAccommodationLetter(AutoMailMergeAccommodationLetterReq Request)
		{
			return base.Channel.AutoMailMergeAccommodationLetter(Request);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00017420 File Offset: 0x00015620
		public MailMergeAndStoreSignatureButtonFileInDocumentsResp MailMergeAndStoreSignatureButtonFileInDocuments(MailMergeAndStoreSignatureButtonFileInDocumentsReq Request)
		{
			return base.Channel.MailMergeAndStoreSignatureButtonFileInDocuments(Request);
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00017440 File Offset: 0x00015640
		public GenerateAccommodationLetterForExternalLogicRulesUserResp GenerateAccommodationLetterForExternalLogicRulesUser(GenerateAccommodationLetterForExternalLogicRulesUserReq Request)
		{
			return base.Channel.GenerateAccommodationLetterForExternalLogicRulesUser(Request);
		}
	}
}
