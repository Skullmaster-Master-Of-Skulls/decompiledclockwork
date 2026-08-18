using System;

namespace System.Collections.Immutable
{
	// Token: 0x02000015 RID: 21
	internal interface IStrongEnumerable<out T, TEnumerator> where TEnumerator : struct, IStrongEnumerator<T>
	{
		// Token: 0x060000A0 RID: 160
		TEnumerator GetEnumerator();
	}
}
