using System;
using TechnoPro.Common.DAO.DynamicCompile;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Impl.DynamicCompile
{
	// Token: 0x020000F1 RID: 241
	public class DynamicCompileDAO : IDynamicCompileDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060006E4 RID: 1764 RVA: 0x0000ED1A File Offset: 0x0000CF1A
		public DynamicCompileDAO()
		{
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00048386 File Offset: 0x00046586
		public DynamicCompileDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x00048398 File Offset: 0x00046598
		// (set) Token: 0x060006E7 RID: 1767 RVA: 0x000483A0 File Offset: 0x000465A0
		public OperationContext OpContext { get; set; }
	}
}
