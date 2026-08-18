using System;

namespace Telerik.Web.Analytics
{
	// Token: 0x0200047B RID: 1147
	internal interface IFeatureTraceHandler : IDisposable
	{
		// Token: 0x060028F7 RID: 10487
		void End();

		// Token: 0x060028F8 RID: 10488
		void Cancel();

		// Token: 0x060028F9 RID: 10489
		void TraceValue(long value);

		// Token: 0x060028FA RID: 10490
		void TraceError(Exception exception);
	}
}
