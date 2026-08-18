using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000076 RID: 118
	public static class Sequence
	{
		// Token: 0x060001F6 RID: 502 RVA: 0x0000725D File Offset: 0x0000545D
		public static T[] Collect<T>(params T[] arguments)
		{
			return arguments;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00007388 File Offset: 0x00005588
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "No other way to do this")]
		public static IEnumerable<Pair<TFirstSequenceElement, TSecondSequenceElement>> Zip<TFirstSequenceElement, TSecondSequenceElement>(IEnumerable<TFirstSequenceElement> sequence1, IEnumerable<TSecondSequenceElement> sequence2)
		{
			IEnumerator<TFirstSequenceElement> @enum = sequence1.GetEnumerator();
			IEnumerator<TSecondSequenceElement> enum2 = sequence2.GetEnumerator();
			while (@enum.MoveNext() && enum2.MoveNext())
			{
				yield return new Pair<TFirstSequenceElement, TSecondSequenceElement>(@enum.Current, enum2.Current);
			}
			yield break;
		}
	}
}
