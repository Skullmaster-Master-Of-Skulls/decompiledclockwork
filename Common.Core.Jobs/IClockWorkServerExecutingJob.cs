using System;
using TechnoPro.Common.Public.Entities.ClockWorkServerJob;
using TechnoPro.Common.Public.Entities.InstanceInfo;

namespace TechnoPro.Common.Core.Jobs
{
	// Token: 0x02000003 RID: 3
	public interface IClockWorkServerExecutingJob : IDisposable
	{
		// Token: 0x0600001A RID: 26
		void Init(ServerInstanceInfo serverInstance, string parameters);

		// Token: 0x0600001B RID: 27
		ClockWorkServerJobRunningResult Run();
	}
}
