using System;

namespace WebGrease
{
	// Token: 0x0200010C RID: 268
	public interface ITimeMeasure
	{
		// Token: 0x060010E5 RID: 4325
		TimeMeasureResult[] GetResults();

		// Token: 0x060010E6 RID: 4326
		void End(bool isGroup, params string[] idParts);

		// Token: 0x060010E7 RID: 4327
		void Start(bool isGroup, params string[] idParts);

		// Token: 0x060010E8 RID: 4328
		void WriteResults(string filePathWithoutExtension, string title, DateTimeOffset utcStart);
	}
}
