using System;

namespace System.Linq
{
	// Token: 0x0200015F RID: 351
	internal sealed class SystemCore_EnumerableDebugViewEmptyException : Exception
	{
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0002D313 File Offset: 0x0002B513
		public string Empty
		{
			get
			{
				return Strings.EmptyEnumerable;
			}
		}
	}
}
