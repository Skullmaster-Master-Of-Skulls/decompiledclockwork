using System;

namespace TechnoPro.Common.Public.Entities.ClockWorkServerJob
{
	// Token: 0x02000459 RID: 1113
	public interface IClockWorkServerJobSchedule
	{
		// Token: 0x060021E0 RID: 8672
		bool IsValidRunningDate(DateTime datetime);

		// Token: 0x060021E1 RID: 8673
		string SaveToXml();

		// Token: 0x060021E2 RID: 8674
		string ToCron(TimeSpan startTime);
	}
}
