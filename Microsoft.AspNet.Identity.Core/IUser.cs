using System;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000042 RID: 66
	public interface IUser<out TKey>
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600012E RID: 302
		TKey Id { get; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600012F RID: 303
		// (set) Token: 0x06000130 RID: 304
		string UserName { get; set; }
	}
}
