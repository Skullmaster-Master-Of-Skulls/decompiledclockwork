using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200001F RID: 31
	[ServiceContract(Name = "TestProctorService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ITestProctor : IService
	{
		// Token: 0x0600011E RID: 286
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllProctorsResp LoadAllProctors(LoadAllProctorsReq Request);

		// Token: 0x0600011F RID: 287
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllReadersResp LoadAllReaders(LoadAllReadersReq Request);

		// Token: 0x06000120 RID: 288
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllScribesResp LoadAllScribes(LoadAllScribesReq Request);

		// Token: 0x06000121 RID: 289
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateProctorResp CreateProctor(CreateProctorReq Request);

		// Token: 0x06000122 RID: 290
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateReaderResp CreateReader(CreateReaderReq Request);

		// Token: 0x06000123 RID: 291
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateScribeResp CreateScribe(CreateScribeReq Request);

		// Token: 0x06000124 RID: 292
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteProctor(DeleteProctorReq Request);

		// Token: 0x06000125 RID: 293
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateProctor(UpdateProctorReq Request);

		// Token: 0x06000126 RID: 294
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadProctorByIdResp LoadProctorById(LoadProctorByIdReq Request);
	}
}
