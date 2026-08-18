using System;
using TechnoPro.Common.Public.Entities.Timers;

namespace TechnoPro.Common.ICore.Timers
{
	// Token: 0x02000021 RID: 33
	public interface ITimerManager : IDisposable
	{
		// Token: 0x060000D8 RID: 216
		void AddTimer(ClockWorkServerTimer timer);

		// Token: 0x060000D9 RID: 217
		void RemoveTimer(string timerName);
	}
}
