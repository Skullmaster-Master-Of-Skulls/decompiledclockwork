using System;

namespace System.Net.Http
{
	// Token: 0x0200000C RID: 12
	internal static class CloneableExtensions
	{
		// Token: 0x06000049 RID: 73 RVA: 0x000030E0 File Offset: 0x000012E0
		internal static T Clone<T>(this T value) where T : ICloneable
		{
			return (T)((object)value.Clone());
		}
	}
}
