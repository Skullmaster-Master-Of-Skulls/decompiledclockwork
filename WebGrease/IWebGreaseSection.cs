using System;
using WebGrease.Configuration;

namespace WebGrease
{
	// Token: 0x02000103 RID: 259
	public interface IWebGreaseSection
	{
		// Token: 0x0600106E RID: 4206
		void Execute(Action action);

		// Token: 0x0600106F RID: 4207
		T Execute<T>(Func<T> action);

		// Token: 0x06001070 RID: 4208
		ICachableWebGreaseSection MakeCachable(object varBySettings, bool isSkipable = false, bool infiniteWaitForLock = false);

		// Token: 0x06001071 RID: 4209
		ICachableWebGreaseSection MakeCachable(ContentItem varByContentItem, object varBySettings = null, bool isSkipable = false, bool infiniteWaitForLock = false);

		// Token: 0x06001072 RID: 4210
		ICachableWebGreaseSection MakeCachable(IFileSet varByFileSet, object varBySettings = null, bool isSkipable = false, bool infiniteWaitForLock = false);
	}
}
