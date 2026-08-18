using System;

namespace System.Collections.Generic
{
	// Token: 0x02000004 RID: 4
	internal static class Empty<T>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000255F File Offset: 0x0000075F
		public static T[] Array
		{
			get
			{
				return Empty<T>._emptyArray;
			}
		}

		// Token: 0x04000004 RID: 4
		private static readonly T[] _emptyArray = new T[0];
	}
}
