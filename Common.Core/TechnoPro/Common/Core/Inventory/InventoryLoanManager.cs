using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnoPro.Common.Core.AppointmentsPointOfContact;
using TechnoPro.Common.Core.Mappers.Inventory;
using TechnoPro.Common.DAO.Impl.Inventory;
using TechnoPro.Common.DAO.Inventory;
using TechnoPro.Common.ICore.AppointmentsPointOfContact;
using TechnoPro.Common.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsPointOfContact;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.Core.Inventory
{
	// Token: 0x020000E5 RID: 229
	public class InventoryLoanManager : IInventoryLoanManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x00039E99 File Offset: 0x00038099
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x00039EA1 File Offset: 0x000380A1
		public IInventoryLoanDAO InventoryLoanDAO { get; set; }

		// Token: 0x060008C0 RID: 2240 RVA: 0x00039EAA File Offset: 0x000380AA
		public InventoryLoanManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.InventoryLoanDAO = new InventoryLoanDAO(opContext);
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x00039EC9 File Offset: 0x000380C9
		// (set) Token: 0x060008C2 RID: 2242 RVA: 0x00039ED1 File Offset: 0x000380D1
		public OperationContext OpContext { get; set; }

		// Token: 0x060008C3 RID: 2243 RVA: 0x00039EDC File Offset: 0x000380DC
		public IList<InventoryLoan> GetActiveLoans()
		{
			return this.InventoryLoanDAO.GetActiveLoans();
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00039EFC File Offset: 0x000380FC
		public InventoryLoan GetActiveLoanById(int loanID)
		{
			return this.InventoryLoanDAO.GetActiveLoanById(loanID);
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00039F1C File Offset: 0x0003811C
		public InventoryLoan GetActiveLoanByProduct(Guid productUniqueID)
		{
			return this.InventoryLoanDAO.GetActiveLoanByProduct(productUniqueID);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00039F3C File Offset: 0x0003813C
		public InventoryLoan GetActiveLoanByProduct(int productId)
		{
			return this.InventoryLoanDAO.GetActiveLoanByProduct(productId);
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00039F5C File Offset: 0x0003815C
		public IList<InventoryLoan> GetActiveLoansByPersonLoanedTo(int personId)
		{
			return this.InventoryLoanDAO.GetActiveLoansByPersonLoanedTo(personId);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00039F7C File Offset: 0x0003817C
		public IList<InventoryLoan> GetActiveLoansByPersonLoanedTo(int personId, DateTime startDate, DateTime endDate)
		{
			return this.InventoryLoanDAO.GetActiveLoansByPersonLoanedTo(personId, startDate, endDate);
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00039F9C File Offset: 0x0003819C
		public IList<InventoryLoan> GetActiveLoansByDueDateInLessThan(TimeSpan dueDateIn)
		{
			return this.InventoryLoanDAO.GetActiveLoansByDueDateInLessThan(dueDateIn);
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x00039FBC File Offset: 0x000381BC
		public IList<InventoryLoan> GetOverDueDateActiveLoans()
		{
			return this.InventoryLoanDAO.GetOverDueDateActiveLoans();
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00039FDC File Offset: 0x000381DC
		public int MakeLoan(InventoryLoanGroup loan, params Guid[] loanedProductUniqueIds)
		{
			int loanGroupId = this.InventoryLoanDAO.MakeLoan(loan, loanedProductUniqueIds);
			bool flag = loanGroupId > 0;
			if (flag)
			{
				IInventoryProductDAO productDAO = new InventoryProductDAO(this.OpContext);
				List<InventoryProductSnapshot> snapshotList = (from uniqueId in loanedProductUniqueIds
				select productDAO.GetProductSnapshotByLoanGroup(uniqueId, loanGroupId, eInventoryProductSnapshotReason.Product_Loaned) into snapshot
				where snapshot != null
				select snapshot).ToList<InventoryProductSnapshot>();
				bool flag2 = snapshotList.Count > 0;
				if (flag2)
				{
					Task.Run(delegate()
					{
						PointOfContact pointOfContact = snapshotList.ToPointOfContact();
						bool flag3 = pointOfContact == null;
						if (!flag3)
						{
							IPointOfContactManager pointOfContactManager = new PointOfContactManager(this.OpContext);
							pointOfContactManager.CreatePointOfContact(true, pointOfContact);
						}
					});
				}
			}
			return loanGroupId;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0003A0B0 File Offset: 0x000382B0
		public int UpdateLoan(InventoryLoan loan)
		{
			return this.InventoryLoanDAO.UpdateLoan(loan);
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0003A0CE File Offset: 0x000382CE
		public void UpdateLoanGroup(InventoryLoanGroup loanGroup)
		{
			this.InventoryLoanDAO.UpdateLoanGroup(loanGroup);
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0003A0E0 File Offset: 0x000382E0
		public IList<InventoryArchivedLoan> GetReturnedLoans()
		{
			return this.InventoryLoanDAO.GetReturnedLoans();
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0003A100 File Offset: 0x00038300
		public void ReturnLoan(InventoryReturnedLoan returnedLoan)
		{
			this.InventoryLoanDAO.ReturnLoan(returnedLoan);
			IInventoryProductDAO inventoryProductDAO = new InventoryProductDAO(this.OpContext);
			InventoryProductSnapshot snapshot = inventoryProductDAO.GetProductSnapshot(returnedLoan.LoanedProduct.UniqueId, returnedLoan.LoanId, eInventoryProductSnapshotReason.Returned_Loan);
			bool flag = snapshot != null;
			if (flag)
			{
				Task.Run(delegate()
				{
					PointOfContact pointOfContact = snapshot.ToPointOfContact();
					bool flag2 = pointOfContact == null;
					if (!flag2)
					{
						IPointOfContactManager pointOfContactManager = new PointOfContactManager(this.OpContext);
						pointOfContactManager.CreatePointOfContact(true, pointOfContact);
					}
				});
			}
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0003A174 File Offset: 0x00038374
		public void ReturnLoan(params InventoryReturnedLoan[] returnedLoans)
		{
			bool flag = returnedLoans != null;
			if (flag)
			{
				foreach (InventoryReturnedLoan returnedLoan in returnedLoans)
				{
					this.ReturnLoan(returnedLoan);
				}
			}
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0003A1AC File Offset: 0x000383AC
		public InventoryArchivedLoan GetReturnedLoanById(int loanID)
		{
			return this.InventoryLoanDAO.GetReturnedLoanById(loanID);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0003A1CC File Offset: 0x000383CC
		public IList<InventoryArchivedLoan> GetReturnedLoansByProduct(Guid productUniqueID)
		{
			return this.InventoryLoanDAO.GetReturnedLoansByProduct(productUniqueID);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0003A1EC File Offset: 0x000383EC
		public IList<InventoryArchivedLoan> GetReturnedLoansByProduct(Guid productUniqueID, DateTime startDate, DateTime endDate)
		{
			return this.InventoryLoanDAO.GetReturnedLoansByProduct(productUniqueID, startDate, endDate);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0003A20C File Offset: 0x0003840C
		public IList<InventoryArchivedLoan> GetReturnedLoansByProduct(int productId, DateTime startDate, DateTime endDate)
		{
			return this.InventoryLoanDAO.GetReturnedLoansByProduct(productId, startDate, endDate);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0003A22C File Offset: 0x0003842C
		public IList<InventoryArchivedLoan> GetReturnedLoansByPersonLoanedTo(int personId)
		{
			return this.InventoryLoanDAO.GetReturnedLoansByPersonLoanedTo(personId);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0003A24C File Offset: 0x0003844C
		public IList<InventoryArchivedLoan> GetReturnedLoansByPersonLoanedTo(int personId, DateTime startDate, DateTime endDate)
		{
			return this.InventoryLoanDAO.GetReturnedLoansByPersonLoanedTo(personId, startDate, endDate);
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0003A26C File Offset: 0x0003846C
		public IList<InventoryLoan> GetLoansByLoanGroupId(int loanGroupId)
		{
			return this.InventoryLoanDAO.GetLoansByLoanGroupId(loanGroupId);
		}
	}
}
