using System;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x0200003F RID: 63
	public interface IRole<out TKey>
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000126 RID: 294
		TKey Id { get; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000127 RID: 295
		// (set) Token: 0x06000128 RID: 296
		string Name { get; set; }
	}
}
