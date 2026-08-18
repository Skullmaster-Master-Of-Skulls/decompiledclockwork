using System;
using System.Collections;

namespace TechnoPro.Common.Security.Saml
{
	// Token: 0x0200001D RID: 29
	public static class EnumeratorAdapter
	{
		// Token: 0x060000FF RID: 255 RVA: 0x0000599C File Offset: 0x00003B9C
		public static T GetFirstValue<T>(this IEnumerator enumerator)
		{
			enumerator.MoveNext();
			return (T)((object)enumerator.Current);
		}
	}
}
