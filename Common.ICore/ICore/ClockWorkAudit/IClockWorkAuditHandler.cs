using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkAudit;

namespace TechnoPro.Common.ICore.ClockWorkAudit
{
	// Token: 0x020000B7 RID: 183
	public interface IClockWorkAuditHandler : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000575 RID: 1397
		AuditResult ExecuteAudit();
	}
}
