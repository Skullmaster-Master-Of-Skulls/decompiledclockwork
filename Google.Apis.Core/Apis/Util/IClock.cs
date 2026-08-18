using System;

namespace Google.Apis.Util
{
	// Token: 0x02000007 RID: 7
	public interface IClock
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000018 RID: 24
		[Obsolete("System local time is almost always inappropriate to use. If you really need this, call UtcNow and then call ToLocalTime on the result")]
		DateTime Now { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000019 RID: 25
		DateTime UtcNow { get; }
	}
}
