using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200003E RID: 62
	[ServiceContract(Name = "DataSyncCourseService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IDataSyncCourse : IService
	{
		// Token: 0x060001F0 RID: 496
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ParseExternalCourseRowPartsResp ParseExternalCourseRowParts(ParseExternalCourseRowPartsReq Request);

		// Token: 0x060001F1 RID: 497
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DataSyncCoursesResp DataSyncCourses(DataSyncCoursesReq Request);

		// Token: 0x060001F2 RID: 498
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DataSyncLookupCoursesByTableResp DataSyncLookupCoursesByTable(DataSyncLookupCoursesByTableReq request);

		// Token: 0x060001F3 RID: 499
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DataSyncLookupCoursesResp DataSyncLookupCourses(DataSyncLookupCoursesReq request);
	}
}
