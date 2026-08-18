using System;

namespace System.Collections.Immutable
{
	// Token: 0x0200000C RID: 12
	internal interface IBinaryTree<out T> : IBinaryTree
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000056 RID: 86
		T Value { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000057 RID: 87
		IBinaryTree<T> Left { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000058 RID: 88
		IBinaryTree<T> Right { get; }
	}
}
