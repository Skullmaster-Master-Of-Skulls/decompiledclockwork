using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000056 RID: 86
	[ServiceContract(Name = "InventoryLoanService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IInventoryLoan : IService
	{
		// Token: 0x0600028B RID: 651
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetActiveLoansResp GetActiveLoans(GetActiveLoansReq request);

		// Token: 0x0600028C RID: 652
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetActiveLoanByIdResp GetActiveLoanById(GetActiveLoanByIdReq request);

		// Token: 0x0600028D RID: 653
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetActiveLoanByProductResp GetActiveLoanByProduct(GetActiveLoanByProductReq request);

		// Token: 0x0600028E RID: 654
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetActiveLoansByPersonLoanedToResp GetActiveLoansByPersonLoanedTo(GetActiveLoansByPersonLoanedToReq request);

		// Token: 0x0600028F RID: 655
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetActiveLoansByPersonLoanedToInDateRangeResp GetActiveLoansByPersonLoanedToInDateRange(GetActiveLoansByPersonLoanedToInDateRangeReq request);

		// Token: 0x06000290 RID: 656
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetActiveLoansByDueDateInLessThanResp GetActiveLoansByDueDateInLessThan(GetActiveLoansByDueDateInLessThanReq request);

		// Token: 0x06000291 RID: 657
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetOverDueDateActiveLoansResp GetOverDueDateActiveLoans(GetOverDueDateActiveLoansReq request);

		// Token: 0x06000292 RID: 658
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		MakeLoanResp MakeLoan(MakeLoanReq request);

		// Token: 0x06000293 RID: 659
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateLoanResp UpdateLoan(UpdateLoanReq request);

		// Token: 0x06000294 RID: 660
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateLoanGroupResp UpdateLoanGroup(UpdateLoanGroupReq request);

		// Token: 0x06000295 RID: 661
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ReturnLoanResp ReturnLoan(ReturnLoanReq request);

		// Token: 0x06000296 RID: 662
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ReturnLoansResp ReturnLoans(ReturnLoansReq request);

		// Token: 0x06000297 RID: 663
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetReturnedLoansResp GetReturnedLoans(GetReturnedLoansReq request);

		// Token: 0x06000298 RID: 664
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetReturnedLoanByIdResp GetReturnedLoanById(GetReturnedLoanByIdReq request);

		// Token: 0x06000299 RID: 665
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetReturnedLoansByProductResp GetReturnedLoansByProduct(GetReturnedLoansByProductReq request);

		// Token: 0x0600029A RID: 666
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetReturnedLoansByProductInDateRangeResp GetReturnedLoansByProductInDateRange(GetReturnedLoansByProductInDateRangeReq request);

		// Token: 0x0600029B RID: 667
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetReturnedLoansByPersonLoanedToResp GetReturnedLoansByPersonLoanedTo(GetReturnedLoansByPersonLoanedToReq request);

		// Token: 0x0600029C RID: 668
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetReturnedLoansByPersonLoanedToInDateRangeResp GetReturnedLoansByPersonLoanedToInDateRange(GetReturnedLoansByPersonLoanedToInDateRangeReq request);

		// Token: 0x0600029D RID: 669
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetLoansByLoanGroupIdResp GetLoansByLoanGroupId(GetLoansByLoanGroupIdReq request);
	}
}
