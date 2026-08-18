using System;
using System.ServiceModel;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000044 RID: 68
	[ServiceContract(Name = "DynamicDataForReportsService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IDynamicDataForReports : IService
	{
		// Token: 0x0600021D RID: 541
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CrossReferenceDataIntoSingleTableResp CrossReferenceDataIntoSingleTable(CrossReferenceDataIntoSingleTableReq Request);

		// Token: 0x0600021E RID: 542
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CrossReferencePerStudentDataResp CrossReferencePerStudentData(CrossReferencePerStudentDataReq Request);

		// Token: 0x0600021F RID: 543
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CrossReferencePerAppointmentDataResp CrossReferencePerAppointmentData(CrossReferencePerAppointmentDataReq Request);

		// Token: 0x06000220 RID: 544
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CrossReferenceAccommodationDataTemplateOnlyResp CrossReferenceAccommodationDataTemplateOnly(CrossReferenceAccommodationDataTemplateOnlyReq Request);

		// Token: 0x06000221 RID: 545
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CrossReferenceAccommodationDataTemplateOrCourseSpecificResp CrossReferenceAccommodationDataTemplateOrCourseSpecific(CrossReferenceAccommodationDataTemplateOrCourseSpecificReq Request);

		// Token: 0x06000222 RID: 546
		[OperationContract(Name = "LoadStudentReportInfo")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadStudentReportInfoResp LoadStudentReportInfo(LoadStudentReportInfoReq Request);

		// Token: 0x06000223 RID: 547
		[OperationContract(Name = "LoadStudentReportInfoAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<LoadStudentReportInfoResp> LoadStudentReportInfoAsync(LoadStudentReportInfoReq Request);
	}
}
