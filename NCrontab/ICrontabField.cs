using System;

namespace NCrontab
{
	// Token: 0x0200000A RID: 10
	public interface ICrontabField
	{
		// Token: 0x06000048 RID: 72
		int GetFirst();

		// Token: 0x06000049 RID: 73
		int Next(int start);

		// Token: 0x0600004A RID: 74
		bool Contains(int value);
	}
}
