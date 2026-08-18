using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000E6 RID: 230
	public class MailMergingDocReusableClientProxy : WCFTokenBasedReusableClientProxy<IMailMergingDoc>, IMailMergingDoc, IService
	{
		// Token: 0x060008E5 RID: 2277 RVA: 0x0001700E File Offset: 0x0001520E
		public MailMergingDocReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00017019 File Offset: 0x00015219
		public MailMergingDocReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00017028 File Offset: 0x00015228
		public MailMergeDocFromDocumentResp MailMergeFromDocument(MailMergeDocFromDocumentReq Request)
		{
			return this.WrapServiceMethod<MailMergeDocFromDocumentResp>(() => this.Proxy.MailMergeFromDocument(Request));
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00017060 File Offset: 0x00015260
		public MailMergeDocFromTemplateResp MailMergeFromTemplate(MailMergeDocFromTemplateReq Request)
		{
			return this.WrapServiceMethod<MailMergeDocFromTemplateResp>(() => this.Proxy.MailMergeFromTemplate(Request));
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00017098 File Offset: 0x00015298
		public MailMergeAccommodationLetterResp MailMergeAccommodationLetter(MailMergeAccommodationLetterReq Request)
		{
			return this.WrapServiceMethod<MailMergeAccommodationLetterResp>(() => this.Proxy.MailMergeAccommodationLetter(Request));
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x000170D0 File Offset: 0x000152D0
		public MailMergeAccommodationEmailsWithLettersAsAttachmentsResp MailMergeAccommodationEmailsWithLettersAsAttachments(MailMergeAccommodationEmailsWithLettersAsAttachmentsReq Request)
		{
			return this.WrapServiceMethod<MailMergeAccommodationEmailsWithLettersAsAttachmentsResp>(() => this.Proxy.MailMergeAccommodationEmailsWithLettersAsAttachments(Request));
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00017108 File Offset: 0x00015308
		public MailMergeAccommodationSingleEmailWithLetterAsAttachmentResp MailMergeAccommodationSingleEmailWithLetterAsAttachment(MailMergeAccommodationSingleEmailWithLetterAsAttachmentReq Request)
		{
			return this.WrapServiceMethod<MailMergeAccommodationSingleEmailWithLetterAsAttachmentResp>(() => this.Proxy.MailMergeAccommodationSingleEmailWithLetterAsAttachment(Request));
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00017140 File Offset: 0x00015340
		public MailMergeAccommodationSingleLetterResp MailMergeAccommodationSingleLetter(MailMergeAccommodationSingleLetterReq Request)
		{
			return this.WrapServiceMethod<MailMergeAccommodationSingleLetterResp>(() => this.Proxy.MailMergeAccommodationSingleLetter(Request));
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00017178 File Offset: 0x00015378
		public MailMergeExamSheetsResp MailMergeExamSheets(MailMergeExamSheetsReq Request)
		{
			return this.WrapServiceMethod<MailMergeExamSheetsResp>(() => this.Proxy.MailMergeExamSheets(Request));
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x000171B0 File Offset: 0x000153B0
		public MailMergeMailingLabelsResp MailMergeMailingLabels(MailMergeMailingLabelsReq Request)
		{
			return this.WrapServiceMethod<MailMergeMailingLabelsResp>(() => this.Proxy.MailMergeMailingLabels(Request));
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x000171E8 File Offset: 0x000153E8
		public MailMergeMultipleItemsToOneDocumentResp MailMergeMultipleItemsToOneDocument(MailMergeMultipleItemsToOneDocumentReq Request)
		{
			return this.WrapServiceMethod<MailMergeMultipleItemsToOneDocumentResp>(() => this.Proxy.MailMergeMultipleItemsToOneDocument(Request));
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00017220 File Offset: 0x00015420
		public AutoMailMergeAccommodationLetterResp AutoMailMergeAccommodationLetter(AutoMailMergeAccommodationLetterReq Request)
		{
			return this.WrapServiceMethod<AutoMailMergeAccommodationLetterResp>(() => this.Proxy.AutoMailMergeAccommodationLetter(Request));
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00017258 File Offset: 0x00015458
		public MailMergeAndStoreSignatureButtonFileInDocumentsResp MailMergeAndStoreSignatureButtonFileInDocuments(MailMergeAndStoreSignatureButtonFileInDocumentsReq Request)
		{
			return this.WrapServiceMethod<MailMergeAndStoreSignatureButtonFileInDocumentsResp>(() => this.Proxy.MailMergeAndStoreSignatureButtonFileInDocuments(Request));
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00017290 File Offset: 0x00015490
		public GenerateAccommodationLetterForExternalLogicRulesUserResp GenerateAccommodationLetterForExternalLogicRulesUser(GenerateAccommodationLetterForExternalLogicRulesUserReq Request)
		{
			return this.WrapServiceMethod<GenerateAccommodationLetterForExternalLogicRulesUserResp>(() => this.Proxy.GenerateAccommodationLetterForExternalLogicRulesUser(Request));
		}
	}
}
