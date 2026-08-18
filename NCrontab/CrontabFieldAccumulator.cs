using System;

namespace NCrontab
{
	// Token: 0x02000005 RID: 5
	// (Invoke) Token: 0x06000022 RID: 34
	internal delegate T CrontabFieldAccumulator<T>(int start, int end, int interval, T successs, Func<ExceptionProvider, T> onError);
}
