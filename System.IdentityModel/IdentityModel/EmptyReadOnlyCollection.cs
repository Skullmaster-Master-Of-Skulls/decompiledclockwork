using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.IdentityModel
{
	// Token: 0x02000079 RID: 121
	internal static class EmptyReadOnlyCollection<T>
	{
		// Token: 0x04000395 RID: 917
		public static ReadOnlyCollection<T> Instance = new ReadOnlyCollection<T>(new List<T>());
	}
}
