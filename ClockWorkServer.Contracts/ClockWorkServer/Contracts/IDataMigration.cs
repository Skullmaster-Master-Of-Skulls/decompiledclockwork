using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200003C RID: 60
	[ServiceContract(Name = "DataMigrationService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IDataMigration : IService
	{
		// Token: 0x060001DF RID: 479
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateStudentsResp CreateStudents(CreateStudentsReq Request);

		// Token: 0x060001E0 RID: 480
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MigrateStudentDataResp MigrateStudentData(MigrateStudentDataReq Request);

		// Token: 0x060001E1 RID: 481
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MigrateStudentPerDateDataResp MigrateStudentPerDateData(MigrateStudentPerDateDataReq Request);

		// Token: 0x060001E2 RID: 482
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MigrateAppointmentsResp MigrateAppointments(MigrateAppointmentsReq Request);

		// Token: 0x060001E3 RID: 483
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MigrateAccommodationsResp MigrateAccommodations(MigrateAccommodationsReq Request);
	}
}
