using System;
using System.Collections.ObjectModel;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000148 RID: 328
	internal sealed class TrueReadOnlyCollection<T> : ReadOnlyCollection<T>
	{
		// Token: 0x06000AA0 RID: 2720 RVA: 0x00026460 File Offset: 0x00024660
		internal TrueReadOnlyCollection(T[] list) : base(list)
		{
		}
	}
}
