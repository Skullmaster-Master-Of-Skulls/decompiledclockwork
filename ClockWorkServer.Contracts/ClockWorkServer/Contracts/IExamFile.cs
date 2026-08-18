using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000018 RID: 24
	[ServiceContract(Name = "ExamFileService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IExamFile : IService
	{
		// Token: 0x060000F1 RID: 241
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadExamFilesByExamResp LoadExamFilesByExam(LoadExamFilesByExamReq Request);

		// Token: 0x060000F2 RID: 242
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadExamFileByIdResp LoadExamFileById(LoadExamFileByIdReq Request);

		// Token: 0x060000F3 RID: 243
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateExamFileResp CreateExamFile(CreateExamFileReq Request);

		// Token: 0x060000F4 RID: 244
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteExamFileResp DeleteExamFile(DeleteExamFileReq Request);

		// Token: 0x060000F5 RID: 245
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadExamFilesByExamCheckProfAltContactPermissionsResp LoadExamFilesByExamCheckProfAltContactPermissions(LoadExamFilesByExamCheckProfAltContactPermissionsReq Request);

		// Token: 0x060000F6 RID: 246
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadExamFileByIdCheckProfAltContactPermissionsResp LoadExamFileByIdCheckProfAltContactPermissions(LoadExamFileByIdCheckProfAltContactPermissionsReq Request);
	}
}
