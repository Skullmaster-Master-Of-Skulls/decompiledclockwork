using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkAudit;
using TechnoPro.Common.Core.ClockWorkAudit;
using TechnoPro.Common.Core.Mappers.ClockWorkAudit;
using TechnoPro.Common.ICore.ClockWorkAudit;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.ClockWorkAudit;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200002B RID: 43
	public class ClockWorkAuditServiceManager : IClockWorkAudit, IService
	{
		// Token: 0x060001CB RID: 459 RVA: 0x00009198 File Offset: 0x00007398
		public ExecuteAuditResp ExecuteAudit(ExecuteAuditReq Request)
		{
			IClockWorkAuditManager clockWorkAuditManager = new ClockWorkAuditManager(Request.GetOperationContext());
			AuditResult auditResult = clockWorkAuditManager.ExecuteAudit(Request.AuditType);
			return new ExecuteAuditResp
			{
				Result = ((auditResult == null) ? null : auditResult.ToDTO())
			};
		}
	}
}
