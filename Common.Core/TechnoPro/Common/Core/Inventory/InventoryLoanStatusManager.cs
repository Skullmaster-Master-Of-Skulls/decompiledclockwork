using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.Impl.Inventory;
using TechnoPro.Common.DAO.Inventory;
using TechnoPro.Common.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.Core.Inventory
{
	// Token: 0x020000E6 RID: 230
	public class InventoryLoanStatusManager : IInventoryLoanStatusManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x0003A28A File Offset: 0x0003848A
		// (set) Token: 0x060008D9 RID: 2265 RVA: 0x0003A292 File Offset: 0x00038492
		private IInventoryLoanStatusDAO LoanStatusDAO { get; set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x0003A29B File Offset: 0x0003849B
		// (set) Token: 0x060008DB RID: 2267 RVA: 0x0003A2A3 File Offset: 0x000384A3
		public OperationContext OpContext { get; set; }

		// Token: 0x060008DC RID: 2268 RVA: 0x0003A2AC File Offset: 0x000384AC
		public InventoryLoanStatusManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.LoanStatusDAO = new InventoryLoanStatusDAO(opContext);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0003A2CC File Offset: 0x000384CC
		public int CreateLoanStatus(InventoryLoanStatus loanStatus)
		{
			return this.LoanStatusDAO.CreateLoanStatus(loanStatus);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0003A2EA File Offset: 0x000384EA
		public void UpdateLoanStatus(InventoryLoanStatus loanStatus)
		{
			this.LoanStatusDAO.UpdateLoanStatus(loanStatus);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0003A2FC File Offset: 0x000384FC
		public InventoryLoanStatus GetLoanStatusById(int lStatusId)
		{
			return this.LoanStatusDAO.GetLoanStatusById(lStatusId);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0003A31C File Offset: 0x0003851C
		public IList<InventoryLoanStatus> GetLoanStatusList()
		{
			return this.LoanStatusDAO.GetLoanStatusList();
		}
	}
}
