using System;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Public
{
	// Token: 0x020000BF RID: 191
	public interface IBaseOperationContext<T> where T : OperationContext
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060004DE RID: 1246
		// (set) Token: 0x060004DF RID: 1247
		T OpContext { get; set; }
	}
}
