using System;

namespace System.Collections.Immutable
{
	// Token: 0x0200000B RID: 11
	internal interface IBinaryTree
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000051 RID: 81
		int Height { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000052 RID: 82
		bool IsEmpty { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000053 RID: 83
		int Count { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000054 RID: 84
		IBinaryTree Left { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000055 RID: 85
		IBinaryTree Right { get; }
	}
}
