using System;

namespace System.Web
{
	// Token: 0x020000DF RID: 223
	public interface IPartitionResolver
	{
		// Token: 0x06000E2D RID: 3629
		void Initialize();

		// Token: 0x06000E2E RID: 3630
		string ResolvePartition(object key);
	}
}
