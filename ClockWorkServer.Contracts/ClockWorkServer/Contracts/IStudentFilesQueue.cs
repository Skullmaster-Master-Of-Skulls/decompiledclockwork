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
	// Token: 0x0200008F RID: 143
	[ServiceContract(Name = "StudentFilesQueueService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IStudentFilesQueue : IService
	{
		// Token: 0x060003E1 RID: 993
		[OperationContract(Name = "LoadStudentFilesQueueFileItemsByStudent")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadStudentFilesQueueFileItemsByStudentResp LoadStudentFilesQueueFileItemsByStudent(LoadStudentFilesQueueFileItemsByStudentReq Request);

		// Token: 0x060003E2 RID: 994
		[OperationContract(Name = "LoadStudentFilesQueueItems")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadStudentFilesQueueItemsResp LoadStudentFilesQueueItems(LoadStudentFilesQueueItemsReq Request);

		// Token: 0x060003E3 RID: 995
		[OperationContract(Name = "UpdateStudentFilesQueueStudentItem")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateStudentFilesQueueStudentItemResp UpdateStudentFilesQueueStudentItem(UpdateStudentFilesQueueStudentItemReq Request);
	}
}
