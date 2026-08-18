using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Inventory
{
	// Token: 0x02000044 RID: 68
	public class InventoryLoanRestClientManager : BearerTokenRestProxy<IInventoryLoanClientManager>, IInventoryLoanClientManager, IWebService
	{
		// Token: 0x06000267 RID: 615 RVA: 0x0000781A File Offset: 0x00005A1A
		public InventoryLoanRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00007824 File Offset: 0x00005A24
		public InventoryLoanRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000782F File Offset: 0x00005A2F
		public IList<InventoryLoanDTO> GetActiveLoans()
		{
			return base.GetMany<InventoryLoanDTO>("inventoryloan/active", true);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000783D File Offset: 0x00005A3D
		public InventoryLoanDTO GetActiveLoanById(int loanID)
		{
			return base.Get<InventoryLoanDTO>(string.Format("inventoryloan/active/loanid/{0}", loanID), true);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00007856 File Offset: 0x00005A56
		public InventoryLoanDTO GetActiveLoanByProduct(Guid productUniqueID)
		{
			return base.Get<InventoryLoanDTO>(string.Format("inventoryloan/active/productuniqueid/{0}", productUniqueID), true);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000786F File Offset: 0x00005A6F
		public InventoryLoanDTO GetActiveLoanByProduct(int productId)
		{
			return base.Get<InventoryLoanDTO>(string.Format("inventoryloan/active/productid/{0}", productId), true);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00007888 File Offset: 0x00005A88
		public IList<InventoryLoanDTO> GetActiveLoansByPersonLoanedTo(int personId)
		{
			return base.GetMany<InventoryLoanDTO>(string.Format("inventoryloan/active/personloanto/{0}", personId), true);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000078A1 File Offset: 0x00005AA1
		public IList<InventoryLoanDTO> GetActiveLoansByPersonLoanedTo(int personId, DateTime startDate, DateTime endDate)
		{
			return base.GetMany<InventoryLoanDTO>(string.Format("inventoryloan/active/personloanto/{0}/range/{1}/{2}", personId, startDate, endDate), true);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000078C6 File Offset: 0x00005AC6
		public IList<InventoryLoanDTO> GetActiveLoansByDueDateInLessThan(TimeSpan dueDateIn)
		{
			return base.GetMany<InventoryLoanDTO>(string.Format("inventoryloan/active/duedateinlessthan/days/{0}", (int)dueDateIn.TotalDays), true);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x000078E6 File Offset: 0x00005AE6
		public IList<InventoryLoanDTO> GetOverDueDateActiveLoans()
		{
			return base.GetMany<InventoryLoanDTO>("inventoryloan/active/overdue", true);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000078F4 File Offset: 0x00005AF4
		public int MakeLoan(InventoryLoanGroupDTO loan, params Guid[] loanedProductUniqueIds)
		{
			MakeLoanReq makeLoanReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MakeLoanReq>();
			makeLoanReq.Loan = loan;
			makeLoanReq.LoanedProductUniqueIds = (from p in loanedProductUniqueIds
			select p.ToString()).ToList<string>();
			return base.Post<MakeLoanReq, int>(makeLoanReq, "inventoryloan");
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000794F File Offset: 0x00005B4F
		public int UpdateLoan(InventoryLoanDTO loan)
		{
			return base.Post<InventoryLoanDTO, int>(loan, "inventoryloan/updateloan");
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000795D File Offset: 0x00005B5D
		public void UpdateLoanGroup(InventoryLoanGroupDTO loanGroup)
		{
			base.Put<InventoryLoanGroupDTO>(loanGroup, "inventoryloan/loangroup");
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000796B File Offset: 0x00005B6B
		public IList<InventoryLoanDTO> GetLoansByLoanGroupId(int loanGroupId)
		{
			return base.GetMany<InventoryLoanDTO>(string.Format("inventoryloan/loangroupid/{0}", loanGroupId), true);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00007984 File Offset: 0x00005B84
		public void ReturnLoan(InventoryReturnedLoanDTO returnedLoan)
		{
			base.Post<InventoryReturnedLoanDTO>(returnedLoan, "inventoryloan/return");
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00007994 File Offset: 0x00005B94
		public void ReturnLoan(IList<InventoryReturnedLoanDTO> returnedLoan)
		{
			ReturnLoansReq returnLoansReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ReturnLoansReq>();
			returnLoansReq.ReturnedLoans = returnedLoan;
			base.Post<ReturnLoansReq>(returnLoansReq, "inventoryloan/return/list");
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000079BF File Offset: 0x00005BBF
		public InventoryArchivedLoanDTO GetReturnedLoanById(int loanID)
		{
			return base.Get<InventoryArchivedLoanDTO>(string.Format("inventoryloan/returned/loanid/{0}", loanID), true);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000079D8 File Offset: 0x00005BD8
		public IList<InventoryArchivedLoanDTO> GetReturnedLoans()
		{
			return base.GetMany<InventoryArchivedLoanDTO>("inventoryloan/returned", true);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000079E6 File Offset: 0x00005BE6
		public IList<InventoryArchivedLoanDTO> GetReturnedLoansByProduct(Guid productUniqueID)
		{
			return base.GetMany<InventoryArchivedLoanDTO>(string.Format("inventoryloan/returned/productid/{0}", productUniqueID), true);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000079FF File Offset: 0x00005BFF
		public IList<InventoryArchivedLoanDTO> GetReturnedLoansByProduct(Guid productUniqueID, DateTime startDate, DateTime endDate)
		{
			return base.GetMany<InventoryArchivedLoanDTO>(string.Format("inventoryloan/returned/productuniqueid/{0}/range/{1}/{2}", productUniqueID, startDate, endDate), true);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00007A24 File Offset: 0x00005C24
		public IList<InventoryArchivedLoanDTO> GetReturnedLoansByProduct(int productId, DateTime startDate, DateTime endDate)
		{
			return base.GetMany<InventoryArchivedLoanDTO>(string.Format("inventoryloan/returned/productid/{0}/range/{1}/{2}", productId, startDate, endDate), true);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00007A49 File Offset: 0x00005C49
		public IList<InventoryArchivedLoanDTO> GetReturnedLoansByPersonLoanedTo(int personId)
		{
			return base.GetMany<InventoryArchivedLoanDTO>(string.Format("inventoryloan/returned/loanedto/{0}", personId), true);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00007A62 File Offset: 0x00005C62
		public IList<InventoryArchivedLoanDTO> GetReturnedLoansByPersonLoanedTo(int personId, DateTime startDate, DateTime endDate)
		{
			return base.GetMany<InventoryArchivedLoanDTO>(string.Format("inventoryloan/returned/loanedto/{0}/range/{1}/{2}", personId, startDate, endDate), true);
		}
	}
}
