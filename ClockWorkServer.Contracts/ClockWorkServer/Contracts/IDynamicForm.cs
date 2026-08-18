using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000046 RID: 70
	[ServiceContract(Name = "DynamicFormService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IDynamicForm : IService
	{
		// Token: 0x06000233 RID: 563
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadDynamicFormByIdResp LoadDynamicFormById(LoadDynamicFormByIdReq Request);

		// Token: 0x06000234 RID: 564
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		FindFormByTitleSubstringMatchResp FindFormByTitleSubstringMatch(FindFormByTitleSubstringMatchReq Request);

		// Token: 0x06000235 RID: 565
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllFormsResp LoadAllForms(LoadAllFormsReq Request);

		// Token: 0x06000236 RID: 566
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadActiveFormsByFormTypeResp LoadActiveFormsByFormType(LoadActiveFormsByFormTypeReq request);

		// Token: 0x06000237 RID: 567
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadDynamicFormsByIdsResp LoadDynamicFormsByIds(LoadDynamicFormsByIdsReq Request);

		// Token: 0x06000238 RID: 568
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ExportFormsToXmlResp ExportFormsToXml(ExportFormsToXmlReq Request);

		// Token: 0x06000239 RID: 569
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ImportFormFromXmlResp ImportFormFromXml(ImportFormFromXmlReq Request);

		// Token: 0x0600023A RID: 570
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadFormsWithExtendedInfoByScreenNumsResp LoadFormsWithExtendedInfoByScreenNums(LoadFormsWithExtendedInfoByScreenNumsReq Request);

		// Token: 0x0600023B RID: 571
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateFormResp CreateForm(CreateFormReq Request);

		// Token: 0x0600023C RID: 572
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateFormResp UpdateForm(UpdateFormReq Request);

		// Token: 0x0600023D RID: 573
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteFormResp DeleteForm(DeleteFormReq Request);

		// Token: 0x0600023E RID: 574
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		FindScreensAControlExistsOnResp FindScreensAControlExistsOn(FindScreensAControlExistsOnReq Request);
	}
}
