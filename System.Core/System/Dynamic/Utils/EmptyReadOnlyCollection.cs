using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace System.Dynamic.Utils
{
	// Token: 0x020000D5 RID: 213
	internal static class EmptyReadOnlyCollection<T>
	{
		// Token: 0x040005C5 RID: 1477
		internal static ReadOnlyCollection<T> Instance = new TrueReadOnlyCollection<T>(new T[0]);
	}
}
