using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200008E RID: 142
	[ServiceContract(Name = "StudentFileService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IStudentFile : IService
	{
		// Token: 0x060003DE RID: 990
		[OperationContract(Name = "LoadStudentFileDescriptions")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadStudentFileDescriptionsResp LoadStudentFileDescriptions(LoadStudentFileDescriptionsReq Request);

		// Token: 0x060003DF RID: 991
		[OperationContract(Name = "LoadFileFromDynamicFileDescription")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadFileFromDynamicFileDescriptionResp LoadFileFromDynamicFileDescription(LoadFileFromDynamicFileDescriptionReq Request);

		// Token: 0x060003E0 RID: 992
		[OperationContract(Name = "UploadStudentFile")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UploadStudentFileResp UploadStudentFile(UploadStudentFileReq Request);
	}
}
