using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000B6 RID: 182
	internal class InventoryLoanClientBaseProxy : ClientBase<IInventoryLoan>, IInventoryLoan, IService
	{
		// Token: 0x06000734 RID: 1844 RVA: 0x0001339C File Offset: 0x0001159C
		public InventoryLoanClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x000133A7 File Offset: 0x000115A7
		public InventoryLoanClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x000133B4 File Offset: 0x000115B4
		public GetActiveLoansResp GetActiveLoans(GetActiveLoansReq request)
		{
			return base.Channel.GetActiveLoans(request);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x000133D4 File Offset: 0x000115D4
		public GetActiveLoanByIdResp GetActiveLoanById(GetActiveLoanByIdReq request)
		{
			return base.Channel.GetActiveLoanById(request);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x000133F4 File Offset: 0x000115F4
		public GetActiveLoanByProductResp GetActiveLoanByProduct(GetActiveLoanByProductReq request)
		{
			return base.Channel.GetActiveLoanByProduct(request);
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00013414 File Offset: 0x00011614
		public GetActiveLoansByPersonLoanedToResp GetActiveLoansByPersonLoanedTo(GetActiveLoansByPersonLoanedToReq request)
		{
			return base.Channel.GetActiveLoansByPersonLoanedTo(request);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00013434 File Offset: 0x00011634
		public GetActiveLoansByPersonLoanedToInDateRangeResp GetActiveLoansByPersonLoanedToInDateRange(GetActiveLoansByPersonLoanedToInDateRangeReq request)
		{
			return base.Channel.GetActiveLoansByPersonLoanedToInDateRange(request);
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00013454 File Offset: 0x00011654
		public GetActiveLoansByDueDateInLessThanResp GetActiveLoansByDueDateInLessThan(GetActiveLoansByDueDateInLessThanReq request)
		{
			return base.Channel.GetActiveLoansByDueDateInLessThan(request);
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00013474 File Offset: 0x00011674
		public GetOverDueDateActiveLoansResp GetOverDueDateActiveLoans(GetOverDueDateActiveLoansReq request)
		{
			return base.Channel.GetOverDueDateActiveLoans(request);
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00013494 File Offset: 0x00011694
		public MakeLoanResp MakeLoan(MakeLoanReq request)
		{
			return base.Channel.MakeLoan(request);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x000134B4 File Offset: 0x000116B4
		public UpdateLoanResp UpdateLoan(UpdateLoanReq request)
		{
			return base.Channel.UpdateLoan(request);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x000134D4 File Offset: 0x000116D4
		public UpdateLoanGroupResp UpdateLoanGroup(UpdateLoanGroupReq request)
		{
			return base.Channel.UpdateLoanGroup(request);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x000134F4 File Offset: 0x000116F4
		public ReturnLoanResp ReturnLoan(ReturnLoanReq request)
		{
			return base.Channel.ReturnLoan(request);
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00013514 File Offset: 0x00011714
		public ReturnLoansResp ReturnLoans(ReturnLoansReq request)
		{
			return base.Channel.ReturnLoans(request);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00013534 File Offset: 0x00011734
		public GetReturnedLoansResp GetReturnedLoans(GetReturnedLoansReq request)
		{
			return base.Channel.GetReturnedLoans(request);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00013554 File Offset: 0x00011754
		public GetReturnedLoanByIdResp GetReturnedLoanById(GetReturnedLoanByIdReq request)
		{
			return base.Channel.GetReturnedLoanById(request);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00013574 File Offset: 0x00011774
		public GetReturnedLoansByProductResp GetReturnedLoansByProduct(GetReturnedLoansByProductReq request)
		{
			return base.Channel.GetReturnedLoansByProduct(request);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00013594 File Offset: 0x00011794
		public GetReturnedLoansByProductInDateRangeResp GetReturnedLoansByProductInDateRange(GetReturnedLoansByProductInDateRangeReq request)
		{
			return base.Channel.GetReturnedLoansByProductInDateRange(request);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x000135B4 File Offset: 0x000117B4
		public GetReturnedLoansByPersonLoanedToResp GetReturnedLoansByPersonLoanedTo(GetReturnedLoansByPersonLoanedToReq request)
		{
			return base.Channel.GetReturnedLoansByPersonLoanedTo(request);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x000135D4 File Offset: 0x000117D4
		public GetReturnedLoansByPersonLoanedToInDateRangeResp GetReturnedLoansByPersonLoanedToInDateRange(GetReturnedLoansByPersonLoanedToInDateRangeReq request)
		{
			return base.Channel.GetReturnedLoansByPersonLoanedToInDateRange(request);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x000135F4 File Offset: 0x000117F4
		public GetLoansByLoanGroupIdResp GetLoansByLoanGroupId(GetLoansByLoanGroupIdReq request)
		{
			return base.Channel.GetLoansByLoanGroupId(request);
		}
	}
}
