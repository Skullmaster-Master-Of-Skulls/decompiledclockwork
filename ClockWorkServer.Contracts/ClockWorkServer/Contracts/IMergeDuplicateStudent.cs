using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.MergeDuplicates;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200006A RID: 106
	[ServiceContract(Name = "MergeDuplicateStudentService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IMergeDuplicateStudent : IService
	{
		// Token: 0x0600031C RID: 796
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		FindPotentialDuplicateStudentsResp FindPotentialDuplicateStudents(FindPotentialDuplicateStudentsReq Request);

		// Token: 0x0600031D RID: 797
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MergeDuplicateStudentsResp MergeDuplicateStudents(MergeDuplicateStudentsReq Request);

		// Token: 0x0600031E RID: 798
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadDuplicateStudentPreviewInfoResp LoadDuplicateStudentPreviewInfo(LoadDuplicateStudentPreviewInfoReq Request);
	}
}
