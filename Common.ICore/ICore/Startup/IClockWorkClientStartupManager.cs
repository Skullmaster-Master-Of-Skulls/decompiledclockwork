using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Startup;

namespace TechnoPro.Common.ICore.Startup
{
	// Token: 0x02000034 RID: 52
	public interface IClockWorkClientStartupManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000160 RID: 352
		ClockWorkClientStartup GetClockWorkClientStartup(int PersonId);
	}
}
