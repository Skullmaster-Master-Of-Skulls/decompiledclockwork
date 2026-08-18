using System;
using System.ServiceModel;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.TempFiles;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.ClockWorkServer.Contracts.Faults.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000093 RID: 147
	[ServiceContract(Name = "TempFileService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ITempFile : IService
	{
		// Token: 0x060003FC RID: 1020
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteOldTempFiles(DeleteOldTempFilesReq Request);

		// Token: 0x060003FD RID: 1021
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AddNewTempFileResp AddNewTempFile(AddNewTempFileReq Request);

		// Token: 0x060003FE RID: 1022
		[OperationContract(Name = "AddNewTempFileAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<AddNewTempFileResp> AddNewTempFileAsync(AddNewTempFileReq Request);

		// Token: 0x060003FF RID: 1023
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DownloadTempFileResp DownloadTempFile(DownloadTempFileReq Request);

		// Token: 0x06000400 RID: 1024
		[OperationContract(Name = "DownloadTempFileAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<DownloadTempFileResp> DownloadTempFileAsync(DownloadTempFileReq Request);

		// Token: 0x06000401 RID: 1025
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteTempFiles(DeleteTempFilesReq Request);

		// Token: 0x06000402 RID: 1026
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteTempFile(DeleteTempFileReq Request);

		// Token: 0x06000403 RID: 1027
		[OperationContract(Name = "DeleteTempFileAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task DeleteTempFileAsync(DeleteTempFileReq Request);

		// Token: 0x06000404 RID: 1028
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(ReportGenericFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CopyTempFilesToInstructorExamUploadAndDeleteTempFileResp CopyTempFilesToInstructorExamUploadAndDeleteTempFile(CopyTempFilesToInstructorExamUploadAndDeleteTempFileReq Request);
	}
}
