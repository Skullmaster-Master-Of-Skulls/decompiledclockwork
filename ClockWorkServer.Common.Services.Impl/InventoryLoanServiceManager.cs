using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Core.Inventory;
using TechnoPro.Common.Core.Mappers.Inventory;
using TechnoPro.Common.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000051 RID: 81
	public class InventoryLoanServiceManager : IInventoryLoan, IService
	{
		// Token: 0x06000301 RID: 769 RVA: 0x0000E938 File Offset: 0x0000CB38
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000E94C File Offset: 0x0000CB4C
		public GetActiveLoansResp GetActiveLoans(GetActiveLoansReq request)
		{
			IInventoryLoanManager inventoryLoanManager = new InventoryLoanManager(request.GetOperationContext());
			return new GetActiveLoansResp
			{
				ActiveLoans = inventoryLoanManager.GetActiveLoans().ToDTO()
			};
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000E984 File Offset: 0x0000CB84
		public GetActiveLoanByIdResp GetActiveLoanById(GetActiveLoanByIdReq request)
		{
			IInventoryLoanManager inventoryLoanManager = new InventoryLoanManager(request.GetOperationContext());
			return new GetActiveLoanByIdResp
			{
				Loan = inventoryLoanManager.GetActiveLoanById(request.LoanId).ToDTO()
			};
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000E9C0 File Offset: 0x0000CBC0
		public GetActiveLoanByProductResp GetActiveLoanByProduct(GetActiveLoanByProductReq request)
		{
			IInventoryLoanManager inventoryLoanManager = new InventoryLoanManager(request.GetOperationContext());
			InventoryLoan loan = string.IsNullOrEmpty(request.ProductUniqueId) ? inventoryLoanManager.GetActiveLoanByProduct(request.AlternateProductId) : inventoryLoanManager.GetActiveLoanByProduct(new Guid(request.ProductUniqueId));
			return new GetActiveLoanByProductResp
			{
				Loan = loan.ToDTO()
			};
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000EA20 File Offset: 0x0000CC20
		public GetActiveLoansByPersonLoanedToResp GetActiveLoansByPersonLoanedTo(GetActiveLoansByPersonLoanedToReq request)
		{
			IInventoryLoanManager inventoryLoanManager = new InventoryLoanManager(request.GetOperationContext());
			return new GetActiveLoansByPersonLoanedToResp
			{
				Loans = inventoryLoanManager.GetActiveLoansByPersonLoanedTo(request.PersonLoanToId).ToDTO()
			};
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000EA5C File Offset: 0x0000CC5C
		public GetActiveLoansByPersonLoanedToInDateRangeResp GetActiveLoansByPersonLoanedToInDateRange(GetActiveLoansByPersonLoanedToInDateRangeReq request)
		{
			IInventoryLoanManager inventoryLoanManager = new InventoryLoanManager(request.GetOperationContext());
			return new GetActiveLoansByPersonLoanedToInDateRangeResp
			{
				Loans = inventoryLoanManager.GetActiveLoansByPersonLoanedTo(request.PersonLoanToId, request.StartDate, request.EndDate).ToDTO()
			};
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000EAA4 File Offset: 0x0000CCA4
		public GetActiveLoansByDueDateInLessThanResp GetActiveLoansByDueDateInLessThan(GetActiveLoansByDueDateInLessThanReq request)
		{
			IInventoryLoanManager inventoryLoanManager = new InventoryLoanManager(request.GetOperationContext());
			return new GetActiveLoansByDueDateInLessThanResp
			{
				Loans = inventoryLoanManager.GetActiveLoansByDueDateInLessThan(request.DueDateIn).ToDTO()
			};
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000EAE0 File Offset: 0x0000CCE0
		public GetOverDueDateActiveLoansResp GetOverDueDateActiveLoans(GetOverDueDateActiveLoansReq request)
		{
			IInventoryLoanManager inventoryLoanManager = new InventoryLoanManager(request.GetOperationContext());
			return new GetOverDueDateActiveLoansResp
			{
				Loans = inventoryLoanManager.GetOverDueDateActiveLoans().ToDTO()
			};
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000EB18 File Offset: 0x0000CD18
		public MakeLoanResp MakeLoan(MakeLoanReq request)
		{
			IInventoryLoanManager inventoryLoanManager = new InventoryLoanManager(request.GetOperationContext());
			MakeLoanResp makeLoanResp = new MakeLoanResp();
			makeLoanResp.LoanId = inventoryLoanManager.MakeLoan(request.Loan.ToDomainObject(), (from pId in request.LoanedProductUniqueIds
			select new Guid(pId)).ToArray<Guid>());
			return makeLoanResp;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000EB84 File Offset: 0x0000CD84
		public UpdateLoanResp UpdateLoan(UpdateLoanReq request)
		{
			IInventoryLoanManager inventoryLoanManager = new InventoryLoanManager(request.GetOperationContext());
			return new UpdateLoanResp
			{
				LoanGroupId = inventoryLoanManager.UpdateLoan(request.Loan.ToDomainObject())
			};
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000EBC0 File Offset: 0x0000CDC0
		public UpdateLoanGroupResp UpdateLoanGroup(UpdateLoanGroupReq request)
		{
			IInventoryLoanManager inventoryLoanManager = new InventoryLoanManager(request.GetOperationContext());
			inventoryLoanManager.UpdateLoanGroup(request.LoanGroup.ToDomainObject());
			return new UpdateLoanGroupResp();
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000EBF8 File Offset: 0x0000CDF8
		public ReturnLoanResp ReturnLoan(ReturnLoanReq request)
		{
			IInventoryLoanManager inventoryLoanManager = new InventoryLoanManager(request.GetOperationContext());
			inventoryLoanManager.ReturnLoan(request.ReturnedLoan.ToDomainObject());
			return new ReturnLoanResp();
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000EC30 File Offset: 0x0000CE30
		public ReturnLoansResp ReturnLoans(ReturnLoansReq request)
		{
			IInventoryLoanManager inventoryLoanManager = new InventoryLoanManager(request.GetOperationContext());
			inventoryLoanManager.ReturnLoan(request.ReturnedLoans.ToDomainObject().ToArray<InventoryReturnedLoan>());
			return new ReturnLoansResp();
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		public GetReturnedLoansResp GetReturnedLoans(GetReturnedLoansReq request)
		{
			IInventoryLoanManager inventoryLoanManager = new InventoryLoanManager(request.GetOperationContext());
			return new GetReturnedLoansResp
			{
				ReturnerdLoans = inventoryLoanManager.GetReturnedLoans().ToDTO()
			};
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000ECA4 File Offset: 0x0000CEA4
		public GetReturnedLoanByIdResp GetReturnedLoanById(GetReturnedLoanByIdReq request)
		{
			IInventoryLoanManager inventoryLoanManager = new InventoryLoanManager(request.GetOperationContext());
			return new GetReturnedLoanByIdResp
			{
				ReturnedLoan = inventoryLoanManager.GetReturnedLoanById(request.LoanId).ToDTO()
			};
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000ECE0 File Offset: 0x0000CEE0
		public GetReturnedLoansByProductResp GetReturnedLoansByProduct(GetReturnedLoansByProductReq request)
		{
			IInventoryLoanManager inventoryLoanManager = new InventoryLoanManager(request.GetOperationContext());
			return new GetReturnedLoansByProductResp
			{
				ReturnedLoans = inventoryLoanManager.GetReturnedLoansByProduct(new Guid(request.ProductUniqueId)).ToDTO()
			};
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000ED20 File Offset: 0x0000CF20
		public GetReturnedLoansByProductInDateRangeResp GetReturnedLoansByProductInDateRange(GetReturnedLoansByProductInDateRangeReq request)
		{
			IInventoryLoanManager inventoryLoanManager = new InventoryLoanManager(request.GetOperationContext());
			IList<InventoryArchivedLoan> list = string.IsNullOrEmpty(request.ProductUniqueId) ? inventoryLoanManager.GetReturnedLoansByProduct(request.AlternateProductId, request.StartDate, request.EndDate) : inventoryLoanManager.GetReturnedLoansByProduct(new Guid(request.ProductUniqueId), request.StartDate, request.EndDate);
			return new GetReturnedLoansByProductInDateRangeResp
			{
				ReturnedLoans = list.ToDTO()
			};
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000ED98 File Offset: 0x0000CF98
		public GetReturnedLoansByPersonLoanedToResp GetReturnedLoansByPersonLoanedTo(GetReturnedLoansByPersonLoanedToReq request)
		{
			IInventoryLoanManager inventoryLoanManager = new InventoryLoanManager(request.GetOperationContext());
			return new GetReturnedLoansByPersonLoanedToResp
			{
				ReturnedLoans = inventoryLoanManager.GetReturnedLoansByPersonLoanedTo(request.PersonLoanedToId).ToDTO()
			};
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000EDD4 File Offset: 0x0000CFD4
		public GetReturnedLoansByPersonLoanedToInDateRangeResp GetReturnedLoansByPersonLoanedToInDateRange(GetReturnedLoansByPersonLoanedToInDateRangeReq request)
		{
			IInventoryLoanManager inventoryLoanManager = new InventoryLoanManager(request.GetOperationContext());
			return new GetReturnedLoansByPersonLoanedToInDateRangeResp
			{
				ReturnedLoans = inventoryLoanManager.GetReturnedLoansByPersonLoanedTo(request.PersonLoanedToId, request.StartDate, request.EndDate).ToDTO()
			};
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000EE1C File Offset: 0x0000D01C
		public GetLoansByLoanGroupIdResp GetLoansByLoanGroupId(GetLoansByLoanGroupIdReq request)
		{
			IInventoryLoanManager inventoryLoanManager = new InventoryLoanManager(request.GetOperationContext());
			return new GetLoansByLoanGroupIdResp
			{
				Loans = inventoryLoanManager.GetLoansByLoanGroupId(request.LoanGroupId).ToDTO()
			};
		}
	}
}
