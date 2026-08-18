using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000D6 RID: 214
	public static class AjaxMinExtensions
	{
		// Token: 0x06000E35 RID: 3637 RVA: 0x00042210 File Offset: 0x00040410
		public static string FormatInvariant(this string format, params object[] args)
		{
			string result;
			try
			{
				result = ((format == null) ? string.Empty : string.Format(CultureInfo.InvariantCulture, format, args));
			}
			catch (FormatException)
			{
				result = format;
			}
			return result;
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0004224C File Offset: 0x0004044C
		public static bool TryParseSingleInvariant(this string text, out float number)
		{
			bool result;
			try
			{
				number = Convert.ToSingle(text, CultureInfo.InvariantCulture);
				result = true;
			}
			catch (FormatException)
			{
				number = float.NaN;
				result = false;
			}
			catch (OverflowException)
			{
				number = float.NaN;
				result = false;
			}
			return result;
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x000422A0 File Offset: 0x000404A0
		public static bool TryParseIntInvariant(this string text, NumberStyles numberStyles, out int number)
		{
			number = 0;
			return text != null && int.TryParse(text, numberStyles, CultureInfo.InvariantCulture, out number);
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x000422B7 File Offset: 0x000404B7
		public static bool TryParseLongInvariant(this string text, NumberStyles numberStyles, out long number)
		{
			number = 0L;
			return text != null && long.TryParse(text, numberStyles, CultureInfo.InvariantCulture, out number);
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x000422CF File Offset: 0x000404CF
		public static bool IsNullOrWhiteSpace(this string text)
		{
			return string.IsNullOrWhiteSpace(text);
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x000422D7 File Offset: 0x000404D7
		public static string IfNullOrWhiteSpace(this string text, string defaultValue)
		{
			if (!string.IsNullOrWhiteSpace(text))
			{
				return text;
			}
			return defaultValue;
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x000422E4 File Offset: 0x000404E4
		public static string SubstringUpToFirst(this string text, char delimiter)
		{
			if (text == null)
			{
				return null;
			}
			int num = text.IndexOf(delimiter);
			if (num >= 0)
			{
				return text.Substring(0, num);
			}
			return text;
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0004230C File Offset: 0x0004050C
		public static string ToStringInvariant(this int number, string format)
		{
			if (format != null)
			{
				return number.ToString(format, CultureInfo.InvariantCulture);
			}
			return number.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0004232B File Offset: 0x0004052B
		public static string ToStringInvariant(this double number, string format)
		{
			if (format != null)
			{
				return number.ToString(format, CultureInfo.InvariantCulture);
			}
			return number.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0004234A File Offset: 0x0004054A
		public static string ToStringInvariant(this int number)
		{
			return number.ToStringInvariant(null);
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x00042374 File Offset: 0x00040574
		public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			HashSet<TKey> hash = new HashSet<TKey>();
			return from p in source
			where hash.Add(keySelector(p))
			select p;
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x000423C4 File Offset: 0x000405C4
		public static void ForEach<TObject>(this IEnumerable<TObject> collection, Action<TObject> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (collection != null)
			{
				foreach (TObject obj in collection)
				{
					obj.IfNotNull(delegate(TObject i)
					{
						action(i);
					});
				}
			}
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00042444 File Offset: 0x00040644
		public static TResult IfNotNull<TObject, TResult>(this TObject obj, Func<TObject, TResult> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (obj != null)
			{
				return action(obj);
			}
			return default(TResult);
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x00042478 File Offset: 0x00040678
		public static TResult IfNotNull<TObject, TResult>(this TObject obj, Func<TObject, TResult> action, TResult defaultValue)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (obj != null)
			{
				return action(obj);
			}
			return defaultValue;
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00042499 File Offset: 0x00040699
		public static void IfNotNull<TObject>(this TObject obj, Action<TObject> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (obj != null)
			{
				action(obj);
			}
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x000424B8 File Offset: 0x000406B8
		public static void CopyItemsTo<TSource>(this ICollection<TSource> fromSet, ICollection<TSource> toSet)
		{
			if (toSet == null)
			{
				throw new ArgumentNullException("toSet");
			}
			if (fromSet != null)
			{
				foreach (TSource item in fromSet)
				{
					toSet.Add(item);
				}
			}
		}
	}
}
