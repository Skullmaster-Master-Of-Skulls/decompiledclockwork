using System;

namespace System.Collections.Immutable
{
	// Token: 0x02000016 RID: 22
	internal interface IStrongEnumerator<T>
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000A1 RID: 161
		T Current { get; }

		// Token: 0x060000A2 RID: 162
		bool MoveNext();
	}
}
