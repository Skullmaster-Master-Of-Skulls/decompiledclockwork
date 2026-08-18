using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkAudit;

namespace TechnoPro.Common.ICore.ClockWorkAudit
{
	// Token: 0x020000B8 RID: 184
	public interface IClockWorkAuditManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000576 RID: 1398
		AuditResult ExecuteAudit(eClockWorkAuditType AuditType);

		// Token: 0x06000577 RID: 1399
		IList<AuditResult> ExecuteFullAudit();
	}
}
