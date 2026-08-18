using System;
using TechnoPro.Common.DAO.Vets;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Impl.Vets
{
	// Token: 0x02000020 RID: 32
	public class VetsChangeInBenefitDAO : IVetsChangeInBenefitDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000CE RID: 206 RVA: 0x00006477 File Offset: 0x00004677
		public VetsChangeInBenefitDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00006489 File Offset: 0x00004689
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00006491 File Offset: 0x00004691
		public OperationContext OpContext { get; set; }
	}
}
