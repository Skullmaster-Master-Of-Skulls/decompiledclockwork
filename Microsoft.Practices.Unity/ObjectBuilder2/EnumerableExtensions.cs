using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000030 RID: 48
	public static class EnumerableExtensions
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x00003690 File Offset: 0x00001890
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public static void ForEach<TItem>(this IEnumerable<TItem> sequence, Action<TItem> action)
		{
			Guard.ArgumentNotNull(sequence, "sequence");
			foreach (TItem obj in sequence)
			{
				action(obj);
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003718 File Offset: 0x00001918
		public static string JoinStrings<TItem>(this IEnumerable<TItem> sequence, string separator, Func<TItem, string> converter)
		{
			StringBuilder stringBuilder = new StringBuilder();
			sequence.Aggregate(stringBuilder, delegate(StringBuilder builder, TItem item)
			{
				if (builder.Length > 0)
				{
					builder.Append(separator);
				}
				builder.Append(converter(item));
				return builder;
			});
			return stringBuilder.ToString();
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003768 File Offset: 0x00001968
		public static string JoinStrings<TItem>(this IEnumerable<TItem> sequence, string separator)
		{
			return sequence.JoinStrings(separator, (TItem item) => item.ToString());
		}
	}
}
