using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000068 RID: 104
	[ServiceContract(Name = "MailMergingDocService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IMailMergingDoc : IService
	{
		// Token: 0x06000308 RID: 776
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MailMergeDocFromTemplateResp MailMergeFromTemplate(MailMergeDocFromTemplateReq Request);

		// Token: 0x06000309 RID: 777
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MailMergeDocFromDocumentResp MailMergeFromDocument(MailMergeDocFromDocumentReq Request);

		// Token: 0x0600030A RID: 778
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MailMergeAccommodationLetterResp MailMergeAccommodationLetter(MailMergeAccommodationLetterReq Request);

		// Token: 0x0600030B RID: 779
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MailMergeAccommodationSingleLetterResp MailMergeAccommodationSingleLetter(MailMergeAccommodationSingleLetterReq Request);

		// Token: 0x0600030C RID: 780
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MailMergeAccommodationSingleEmailWithLetterAsAttachmentResp MailMergeAccommodationSingleEmailWithLetterAsAttachment(MailMergeAccommodationSingleEmailWithLetterAsAttachmentReq Request);

		// Token: 0x0600030D RID: 781
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MailMergeAccommodationEmailsWithLettersAsAttachmentsResp MailMergeAccommodationEmailsWithLettersAsAttachments(MailMergeAccommodationEmailsWithLettersAsAttachmentsReq Request);

		// Token: 0x0600030E RID: 782
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MailMergeExamSheetsResp MailMergeExamSheets(MailMergeExamSheetsReq Request);

		// Token: 0x0600030F RID: 783
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MailMergeMailingLabelsResp MailMergeMailingLabels(MailMergeMailingLabelsReq Request);

		// Token: 0x06000310 RID: 784
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MailMergeMultipleItemsToOneDocumentResp MailMergeMultipleItemsToOneDocument(MailMergeMultipleItemsToOneDocumentReq Request);

		// Token: 0x06000311 RID: 785
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AutoMailMergeAccommodationLetterResp AutoMailMergeAccommodationLetter(AutoMailMergeAccommodationLetterReq Request);

		// Token: 0x06000312 RID: 786
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MailMergeAndStoreSignatureButtonFileInDocumentsResp MailMergeAndStoreSignatureButtonFileInDocuments(MailMergeAndStoreSignatureButtonFileInDocumentsReq Request);

		// Token: 0x06000313 RID: 787
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GenerateAccommodationLetterForExternalLogicRulesUserResp GenerateAccommodationLetterForExternalLogicRulesUser(GenerateAccommodationLetterForExternalLogicRulesUserReq Request);
	}
}
