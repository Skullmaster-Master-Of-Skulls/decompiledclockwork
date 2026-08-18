using System;

namespace WebGrease
{
	// Token: 0x020000FF RID: 255
	public interface ICachableWebGreaseSection
	{
		// Token: 0x06001061 RID: 4193
		bool Execute(Func<ICacheSection, bool> cachableSectionAction);

		// Token: 0x06001062 RID: 4194
		ICachableWebGreaseSection RestoreFromCacheAction(Func<ICacheSection, bool> action);

		// Token: 0x06001063 RID: 4195
		ICachableWebGreaseSection WhenSkipped(Action<ICacheSection> action);
	}
}
