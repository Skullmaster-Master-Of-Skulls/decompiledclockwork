using System;
using TechnoPro.Common.Reports.Public.Entities.OperationContexts;

namespace TechnoPro.Common.Reports.Public
{
	// Token: 0x02000003 RID: 3
	public interface IBaseOperationContextRO<T> where T : OperationContextRO
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		// (set) Token: 0x06000002 RID: 2
		T OpContext { get; set; }
	}
}
