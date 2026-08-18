using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000098 RID: 152
	[ServiceContract(Name = "UnivDataAccessService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IUnivDataAccess : IService
	{
		// Token: 0x06000436 RID: 1078
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DoesTableExistResp DoesTableExist(DoesTableExistReq Request);

		// Token: 0x06000437 RID: 1079
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DoesColumnExistResp DoesColumnExist(DoesColumnExistReq Request);

		// Token: 0x06000438 RID: 1080
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetSQLCommandParametersFilledInResp GetSQLCommandParametersFilledIn(GetSQLCommandParametersFilledInReq Request);

		// Token: 0x06000439 RID: 1081
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		FillReturnIdentityResp FillReturnIdentity(FillReturnIdentityReq Request);

		// Token: 0x0600043A RID: 1082
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		FillResp Fill(FillReq Request);

		// Token: 0x0600043B RID: 1083
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ExecuteScalarResp ExecuteScalar(ExecuteScalarReq Request);

		// Token: 0x0600043C RID: 1084
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ExecuteNonQueryResp ExecuteNonQuery(ExecuteNonQueryReq Request);
	}
}
