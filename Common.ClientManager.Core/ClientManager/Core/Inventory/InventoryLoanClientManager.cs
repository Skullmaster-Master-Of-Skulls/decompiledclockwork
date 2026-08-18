using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Inventory
{
	// Token: 0x02000054 RID: 84
	public class InventoryLoanClientManager : IInventoryLoanClientManager, IWebService
	{
		// Token: 0x060002D8 RID: 728 RVA: 0x0000CA40 File Offset: 0x0000AC40
		public IList<InventoryLoanDTO> GetActiveLoans()
		{
			GetActiveLoansReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetActiveLoansReq>();
			return ClientServiceFactory.GetClientInstance<IInventoryLoan>().GetActiveLoans(request).ActiveLoans;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000CA70 File Offset: 0x0000AC70
		public InventoryLoanDTO GetActiveLoanById(int loanID)
		{
			GetActiveLoanByIdReq getActiveLoanByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetActiveLoanByIdReq>();
			getActiveLoanByIdReq.LoanId = loanID;
			return ClientServiceFactory.GetClientInstance<IInventoryLoan>().GetActiveLoanById(getActiveLoanByIdReq).Loan;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000CAA8 File Offset: 0x0000ACA8
		public InventoryLoanDTO GetActiveLoanByProduct(Guid productUniqueID)
		{
			GetActiveLoanByProductReq getActiveLoanByProductReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetActiveLoanByProductReq>();
			getActiveLoanByProductReq.ProductUniqueId = productUniqueID.ToString();
			return ClientServiceFactory.GetClientInstance<IInventoryLoan>().GetActiveLoanByProduct(getActiveLoanByProductReq).Loan;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000CAEC File Offset: 0x0000ACEC
		public InventoryLoanDTO GetActiveLoanByProduct(int productId)
		{
			GetActiveLoanByProductReq getActiveLoanByProductReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetActiveLoanByProductReq>();
			getActiveLoanByProductReq.AlternateProductId = productId;
			return ClientServiceFactory.GetClientInstance<IInventoryLoan>().GetActiveLoanByProduct(getActiveLoanByProductReq).Loan;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000CB24 File Offset: 0x0000AD24
		public IList<InventoryLoanDTO> GetActiveLoansByPersonLoanedTo(int personId)
		{
			GetActiveLoansByPersonLoanedToReq getActiveLoansByPersonLoanedToReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetActiveLoansByPersonLoanedToReq>();
			getActiveLoansByPersonLoanedToReq.PersonLoanToId = personId;
			return ClientServiceFactory.GetClientInstance<IInventoryLoan>().GetActiveLoansByPersonLoanedTo(getActiveLoansByPersonLoanedToReq).Loans;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000CB5C File Offset: 0x0000AD5C
		public IList<InventoryLoanDTO> GetActiveLoansByPersonLoanedTo(int personId, DateTime startDate, DateTime endDate)
		{
			GetActiveLoansByPersonLoanedToInDateRangeReq getActiveLoansByPersonLoanedToInDateRangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetActiveLoansByPersonLoanedToInDateRangeReq>();
			getActiveLoansByPersonLoanedToInDateRangeReq.PersonLoanToId = personId;
			getActiveLoansByPersonLoanedToInDateRangeReq.StartDate = startDate;
			getActiveLoansByPersonLoanedToInDateRangeReq.EndDate = endDate;
			return ClientServiceFactory.GetClientInstance<IInventoryLoan>().GetActiveLoansByPersonLoanedToInDateRange(getActiveLoansByPersonLoanedToInDateRangeReq).Loans;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000CBA4 File Offset: 0x0000ADA4
		public IList<InventoryLoanDTO> GetActiveLoansByDueDateInLessThan(TimeSpan dueDateIn)
		{
			GetActiveLoansByDueDateInLessThanReq getActiveLoansByDueDateInLessThanReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetActiveLoansByDueDateInLessThanReq>();
			getActiveLoansByDueDateInLessThanReq.DueDateIn = dueDateIn;
			return ClientServiceFactory.GetClientInstance<IInventoryLoan>().GetActiveLoansByDueDateInLessThan(getActiveLoansByDueDateInLessThanReq).Loans;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000CBDC File Offset: 0x0000ADDC
		public IList<InventoryLoanDTO> GetOverDueDateActiveLoans()
		{
			GetOverDueDateActiveLoansReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetOverDueDateActiveLoansReq>();
			return ClientServiceFactory.GetClientInstance<IInventoryLoan>().GetOverDueDateActiveLoans(request).Loans;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000CC0C File Offset: 0x0000AE0C
		public int MakeLoan(InventoryLoanGroupDTO loan, params Guid[] loanedProductUniqueIds)
		{
			MakeLoanReq makeLoanReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MakeLoanReq>();
			makeLoanReq.Loan = loan;
			makeLoanReq.LoanedProductUniqueIds = (from p in loanedProductUniqueIds
			select p.ToString()).ToList<string>();
			return ClientServiceFactory.GetClientInstance<IInventoryLoan>().MakeLoan(makeLoanReq).LoanId;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000CC74 File Offset: 0x0000AE74
		public int UpdateLoan(InventoryLoanDTO loan)
		{
			UpdateLoanReq updateLoanReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateLoanReq>();
			updateLoanReq.Loan = loan;
			return ClientServiceFactory.GetClientInstance<IInventoryLoan>().UpdateLoan(updateLoanReq).LoanGroupId;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000CCAC File Offset: 0x0000AEAC
		public void UpdateLoanGroup(InventoryLoanGroupDTO loanGroup)
		{
			UpdateLoanGroupReq updateLoanGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateLoanGroupReq>();
			updateLoanGroupReq.LoanGroup = loanGroup;
			ClientServiceFactory.GetClientInstance<IInventoryLoan>().UpdateLoanGroup(updateLoanGroupReq);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000CCDC File Offset: 0x0000AEDC
		public IList<InventoryArchivedLoanDTO> GetReturnedLoans()
		{
			GetReturnedLoansReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetReturnedLoansReq>();
			return ClientServiceFactory.GetClientInstance<IInventoryLoan>().GetReturnedLoans(request).ReturnerdLoans;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000CD0C File Offset: 0x0000AF0C
		public void ReturnLoan(InventoryReturnedLoanDTO returnedLoan)
		{
			ReturnLoanReq returnLoanReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ReturnLoanReq>();
			returnLoanReq.ReturnedLoan = returnedLoan;
			ClientServiceFactory.GetClientInstance<IInventoryLoan>().ReturnLoan(returnLoanReq);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000CD3C File Offset: 0x0000AF3C
		public void ReturnLoan(IList<InventoryReturnedLoanDTO> returnedLoan)
		{
			ReturnLoansReq returnLoansReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ReturnLoansReq>();
			returnLoansReq.ReturnedLoans = returnedLoan;
			ClientServiceFactory.GetClientInstance<IInventoryLoan>().ReturnLoans(returnLoansReq);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000CD6C File Offset: 0x0000AF6C
		public InventoryArchivedLoanDTO GetReturnedLoanById(int loanID)
		{
			GetReturnedLoanByIdReq getReturnedLoanByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetReturnedLoanByIdReq>();
			getReturnedLoanByIdReq.LoanId = loanID;
			return ClientServiceFactory.GetClientInstance<IInventoryLoan>().GetReturnedLoanById(getReturnedLoanByIdReq).ReturnedLoan;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000CDA4 File Offset: 0x0000AFA4
		public IList<InventoryArchivedLoanDTO> GetReturnedLoansByProduct(Guid productUniqueID)
		{
			GetReturnedLoansByProductReq getReturnedLoansByProductReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetReturnedLoansByProductReq>();
			getReturnedLoansByProductReq.ProductUniqueId = productUniqueID.ToString();
			return ClientServiceFactory.GetClientInstance<IInventoryLoan>().GetReturnedLoansByProduct(getReturnedLoansByProductReq).ReturnedLoans;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000CDE8 File Offset: 0x0000AFE8
		public IList<InventoryArchivedLoanDTO> GetReturnedLoansByProduct(Guid productUniqueID, DateTime startDate, DateTime endDate)
		{
			GetReturnedLoansByProductInDateRangeReq getReturnedLoansByProductInDateRangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetReturnedLoansByProductInDateRangeReq>();
			getReturnedLoansByProductInDateRangeReq.ProductUniqueId = productUniqueID.ToString();
			getReturnedLoansByProductInDateRangeReq.StartDate = startDate;
			getReturnedLoansByProductInDateRangeReq.EndDate = endDate;
			return ClientServiceFactory.GetClientInstance<IInventoryLoan>().GetReturnedLoansByProductInDateRange(getReturnedLoansByProductInDateRangeReq).ReturnedLoans;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000CE3C File Offset: 0x0000B03C
		public IList<InventoryArchivedLoanDTO> GetReturnedLoansByProduct(int productId, DateTime startDate, DateTime endDate)
		{
			GetReturnedLoansByProductInDateRangeReq getReturnedLoansByProductInDateRangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetReturnedLoansByProductInDateRangeReq>();
			getReturnedLoansByProductInDateRangeReq.AlternateProductId = productId;
			getReturnedLoansByProductInDateRangeReq.StartDate = startDate;
			getReturnedLoansByProductInDateRangeReq.EndDate = endDate;
			return ClientServiceFactory.GetClientInstance<IInventoryLoan>().GetReturnedLoansByProductInDateRange(getReturnedLoansByProductInDateRangeReq).ReturnedLoans;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000CE84 File Offset: 0x0000B084
		public IList<InventoryArchivedLoanDTO> GetReturnedLoansByPersonLoanedTo(int personId)
		{
			GetReturnedLoansByPersonLoanedToReq getReturnedLoansByPersonLoanedToReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetReturnedLoansByPersonLoanedToReq>();
			getReturnedLoansByPersonLoanedToReq.PersonLoanedToId = personId;
			return ClientServiceFactory.GetClientInstance<IInventoryLoan>().GetReturnedLoansByPersonLoanedTo(getReturnedLoansByPersonLoanedToReq).ReturnedLoans;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000CEBC File Offset: 0x0000B0BC
		public IList<InventoryArchivedLoanDTO> GetReturnedLoansByPersonLoanedTo(int personId, DateTime startDate, DateTime endDate)
		{
			GetReturnedLoansByPersonLoanedToInDateRangeReq getReturnedLoansByPersonLoanedToInDateRangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetReturnedLoansByPersonLoanedToInDateRangeReq>();
			getReturnedLoansByPersonLoanedToInDateRangeReq.PersonLoanedToId = personId;
			getReturnedLoansByPersonLoanedToInDateRangeReq.StartDate = startDate;
			getReturnedLoansByPersonLoanedToInDateRangeReq.EndDate = endDate;
			return ClientServiceFactory.GetClientInstance<IInventoryLoan>().GetReturnedLoansByPersonLoanedToInDateRange(getReturnedLoansByPersonLoanedToInDateRangeReq).ReturnedLoans;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000CF04 File Offset: 0x0000B104
		public IList<InventoryLoanDTO> GetLoansByLoanGroupId(int loanGroupId)
		{
			GetLoansByLoanGroupIdReq getLoansByLoanGroupIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetLoansByLoanGroupIdReq>();
			getLoansByLoanGroupIdReq.LoanGroupId = loanGroupId;
			return ClientServiceFactory.GetClientInstance<IInventoryLoan>().GetLoansByLoanGroupId(getLoansByLoanGroupIdReq).Loans;
		}
	}
}
