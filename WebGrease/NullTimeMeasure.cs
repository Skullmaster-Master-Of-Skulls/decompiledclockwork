using System;

namespace WebGrease
{
	// Token: 0x0200010D RID: 269
	internal class NullTimeMeasure : ITimeMeasure
	{
		// Token: 0x060010E9 RID: 4329 RVA: 0x0004B3D3 File Offset: 0x000495D3
		public TimeMeasureResult[] GetResults()
		{
			return this.emptyResult;
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0004B3DB File Offset: 0x000495DB
		public void End(bool isGroup, params string[] idParts)
		{
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0004B3DD File Offset: 0x000495DD
		public void Start(bool isGroup, params string[] idParts)
		{
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0004B3DF File Offset: 0x000495DF
		public void WriteResults(string filePathWithoutExtension, string title, DateTimeOffset utcStart)
		{
		}

		// Token: 0x040006A3 RID: 1699
		private readonly TimeMeasureResult[] emptyResult = new TimeMeasureResult[0];
	}
}
