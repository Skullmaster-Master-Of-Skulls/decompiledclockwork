using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.Razor.Utils
{
	// Token: 0x02000093 RID: 147
	internal static class EnumeratorExtensions
	{
		// Token: 0x0600062A RID: 1578 RVA: 0x00017867 File Offset: 0x00015A67
		public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> source)
		{
			return source.SelectMany((IEnumerable<T> e) => e);
		}
	}
}
