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
	// Token: 0x02000069 RID: 105
	[ServiceContract(Name = "MailMergingEmailService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IMailMergingEmail : IService
	{
		// Token: 0x06000314 RID: 788
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MailMergeEmailFromTemplateXmlResp MailMergeFromTemplateXml(MailMergeEmailFromTemplateXmlReq Request);

		// Token: 0x06000315 RID: 789
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MailMergeEmailFromTemplateResp MailMergeFromTemplate(MailMergeEmailFromTemplateReq Request);

		// Token: 0x06000316 RID: 790
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MailMergeEmailFromTemplateInWebSettingsResp MailMergeFromTemplateInWebSettings(MailMergeEmailFromTemplateInWebSettingsReq Request);

		// Token: 0x06000317 RID: 791
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MailMergeAccommodationLetterCoursesEmailResp MailMergeAccommodationLetterCoursesEmail(MailMergeAccommodationLetterCoursesEmailReq Request);

		// Token: 0x06000318 RID: 792
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MailMergeAccommodationSingleLetterEmailResp MailMergeAccommodationSingleLetterEmail(MailMergeAccommodationSingleLetterEmailReq Request);

		// Token: 0x06000319 RID: 793
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MailMergeMultipleEmailsFromTemplateXmlResp MailMergeMultipleEmailsFromTemplateXml(MailMergeMultipleEmailsFromTemplateXmlReq Request);

		// Token: 0x0600031A RID: 794
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MailMergeMultipleEmailsFromTemplateIdResp MailMergeMultipleEmailsFromTemplateId(MailMergeMultipleEmailsFromTemplateIdReq Request);

		// Token: 0x0600031B RID: 795
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MailMergeMultipleEmailsFromTemplateInWebSettingsResp MailMergeMultipleEmailsFromTemplateInWebSettings(MailMergeMultipleEmailsFromTemplateInWebSettingsReq Request);
	}
}
