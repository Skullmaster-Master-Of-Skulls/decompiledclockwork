using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;

namespace TechnoPro.Common.ICore.Startup
{
	// Token: 0x02000033 RID: 51
	public interface IClientStartupManager : IBaseOperationContext<ClockWorkServerOperationContext>
	{
		// Token: 0x0600015F RID: 351
		CertificateInfo GetClockWorkServerCertificate();
	}
}
