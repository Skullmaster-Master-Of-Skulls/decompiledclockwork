using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000B5 RID: 181
	public class InventoryLoanReusableClientProxy : WCFTokenBasedReusableClientProxy<IInventoryLoan>, IInventoryLoan, IService
	{
		// Token: 0x0600071F RID: 1823 RVA: 0x00012F5A File Offset: 0x0001115A
		public InventoryLoanReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00012F65 File Offset: 0x00011165
		public InventoryLoanReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00012F74 File Offset: 0x00011174
		public GetActiveLoansResp GetActiveLoans(GetActiveLoansReq request)
		{
			return this.WrapServiceMethod<GetActiveLoansResp>(() => this.Proxy.GetActiveLoans(request));
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00012FAC File Offset: 0x000111AC
		public GetActiveLoanByIdResp GetActiveLoanById(GetActiveLoanByIdReq request)
		{
			return this.WrapServiceMethod<GetActiveLoanByIdResp>(() => this.Proxy.GetActiveLoanById(request));
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00012FE4 File Offset: 0x000111E4
		public GetActiveLoanByProductResp GetActiveLoanByProduct(GetActiveLoanByProductReq request)
		{
			return this.WrapServiceMethod<GetActiveLoanByProductResp>(() => this.Proxy.GetActiveLoanByProduct(request));
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0001301C File Offset: 0x0001121C
		public GetActiveLoansByPersonLoanedToResp GetActiveLoansByPersonLoanedTo(GetActiveLoansByPersonLoanedToReq request)
		{
			return this.WrapServiceMethod<GetActiveLoansByPersonLoanedToResp>(() => this.Proxy.GetActiveLoansByPersonLoanedTo(request));
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00013054 File Offset: 0x00011254
		public GetActiveLoansByPersonLoanedToInDateRangeResp GetActiveLoansByPersonLoanedToInDateRange(GetActiveLoansByPersonLoanedToInDateRangeReq request)
		{
			return this.WrapServiceMethod<GetActiveLoansByPersonLoanedToInDateRangeResp>(() => this.Proxy.GetActiveLoansByPersonLoanedToInDateRange(request));
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001308C File Offset: 0x0001128C
		public GetActiveLoansByDueDateInLessThanResp GetActiveLoansByDueDateInLessThan(GetActiveLoansByDueDateInLessThanReq request)
		{
			return this.WrapServiceMethod<GetActiveLoansByDueDateInLessThanResp>(() => this.Proxy.GetActiveLoansByDueDateInLessThan(request));
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x000130C4 File Offset: 0x000112C4
		public GetOverDueDateActiveLoansResp GetOverDueDateActiveLoans(GetOverDueDateActiveLoansReq request)
		{
			return this.WrapServiceMethod<GetOverDueDateActiveLoansResp>(() => this.Proxy.GetOverDueDateActiveLoans(request));
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x000130FC File Offset: 0x000112FC
		public MakeLoanResp MakeLoan(MakeLoanReq request)
		{
			return this.WrapServiceMethod<MakeLoanResp>(() => this.Proxy.MakeLoan(request));
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00013134 File Offset: 0x00011334
		public UpdateLoanResp UpdateLoan(UpdateLoanReq request)
		{
			return this.WrapServiceMethod<UpdateLoanResp>(() => this.Proxy.UpdateLoan(request));
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001316C File Offset: 0x0001136C
		public UpdateLoanGroupResp UpdateLoanGroup(UpdateLoanGroupReq request)
		{
			return this.WrapServiceMethod<UpdateLoanGroupResp>(() => this.Proxy.UpdateLoanGroup(request));
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x000131A4 File Offset: 0x000113A4
		public ReturnLoanResp ReturnLoan(ReturnLoanReq request)
		{
			return this.WrapServiceMethod<ReturnLoanResp>(() => this.Proxy.ReturnLoan(request));
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x000131DC File Offset: 0x000113DC
		public ReturnLoansResp ReturnLoans(ReturnLoansReq request)
		{
			return this.WrapServiceMethod<ReturnLoansResp>(() => this.Proxy.ReturnLoans(request));
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00013214 File Offset: 0x00011414
		public GetReturnedLoansResp GetReturnedLoans(GetReturnedLoansReq request)
		{
			return this.WrapServiceMethod<GetReturnedLoansResp>(() => this.Proxy.GetReturnedLoans(request));
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001324C File Offset: 0x0001144C
		public GetReturnedLoanByIdResp GetReturnedLoanById(GetReturnedLoanByIdReq request)
		{
			return this.WrapServiceMethod<GetReturnedLoanByIdResp>(() => this.Proxy.GetReturnedLoanById(request));
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00013284 File Offset: 0x00011484
		public GetReturnedLoansByProductResp GetReturnedLoansByProduct(GetReturnedLoansByProductReq request)
		{
			return this.WrapServiceMethod<GetReturnedLoansByProductResp>(() => this.Proxy.GetReturnedLoansByProduct(request));
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x000132BC File Offset: 0x000114BC
		public GetReturnedLoansByProductInDateRangeResp GetReturnedLoansByProductInDateRange(GetReturnedLoansByProductInDateRangeReq request)
		{
			return this.WrapServiceMethod<GetReturnedLoansByProductInDateRangeResp>(() => this.Proxy.GetReturnedLoansByProductInDateRange(request));
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x000132F4 File Offset: 0x000114F4
		public GetReturnedLoansByPersonLoanedToResp GetReturnedLoansByPersonLoanedTo(GetReturnedLoansByPersonLoanedToReq request)
		{
			return this.WrapServiceMethod<GetReturnedLoansByPersonLoanedToResp>(() => this.Proxy.GetReturnedLoansByPersonLoanedTo(request));
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001332C File Offset: 0x0001152C
		public GetReturnedLoansByPersonLoanedToInDateRangeResp GetReturnedLoansByPersonLoanedToInDateRange(GetReturnedLoansByPersonLoanedToInDateRangeReq request)
		{
			return this.WrapServiceMethod<GetReturnedLoansByPersonLoanedToInDateRangeResp>(() => this.Proxy.GetReturnedLoansByPersonLoanedToInDateRange(request));
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00013364 File Offset: 0x00011564
		public GetLoansByLoanGroupIdResp GetLoansByLoanGroupId(GetLoansByLoanGroupIdReq request)
		{
			return this.WrapServiceMethod<GetLoansByLoanGroupIdResp>(() => this.Proxy.GetLoansByLoanGroupId(request));
		}
	}
}
