using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001F6 RID: 502
	internal static class CancellableEnumerable
	{
		// Token: 0x0600100C RID: 4108 RVA: 0x00038A8E File Offset: 0x00036C8E
		internal static IEnumerable<TElement> Wrap<TElement>(IEnumerable<TElement> source, CancellationToken token)
		{
			int count = 0;
			foreach (TElement telement in source)
			{
				int num = count;
				count = num + 1;
				if ((num & 63) == 0)
				{
					CancellationState.ThrowIfCanceled(token);
				}
				yield return telement;
			}
			IEnumerator<TElement> enumerator = null;
			yield break;
			yield break;
		}
	}
}
