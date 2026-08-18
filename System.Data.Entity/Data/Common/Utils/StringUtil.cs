using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.Data.Common.Utils
{
	// Token: 0x0200039A RID: 922
	internal static class StringUtil
	{
		// Token: 0x06003317 RID: 13079 RVA: 0x000C71A0 File Offset: 0x000C53A0
		internal static string BuildDelimitedList<T>(IEnumerable<T> values, StringUtil.ToStringConverter<T> converter, string delimiter)
		{
			if (values == null)
			{
				return string.Empty;
			}
			if (converter == null)
			{
				converter = new StringUtil.ToStringConverter<T>(StringUtil.InvariantConvertToString<T>);
			}
			if (delimiter == null)
			{
				delimiter = ", ";
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (T value in values)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(delimiter);
				}
				stringBuilder.Append(converter(value));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003318 RID: 13080 RVA: 0x000C7230 File Offset: 0x000C5430
		internal static string ToCommaSeparatedString(IEnumerable list)
		{
			return StringUtil.ToSeparatedString(list, ", ", string.Empty);
		}

		// Token: 0x06003319 RID: 13081 RVA: 0x000C7244 File Offset: 0x000C5444
		internal static string ToSeparatedString(IEnumerable list, string separator, string nullValue)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringUtil.ToSeparatedString(stringBuilder, list, separator, nullValue);
			return stringBuilder.ToString();
		}

		// Token: 0x0600331A RID: 13082 RVA: 0x000C7266 File Offset: 0x000C5466
		internal static string ToCommaSeparatedStringSorted(IEnumerable list)
		{
			return StringUtil.ToSeparatedStringSorted(list, ", ", string.Empty);
		}

		// Token: 0x0600331B RID: 13083 RVA: 0x000C7278 File Offset: 0x000C5478
		internal static string ToSeparatedStringSorted(IEnumerable list, string separator, string nullValue)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringUtil.ToSeparatedStringPrivate(stringBuilder, list, separator, nullValue, true);
			return stringBuilder.ToString();
		}

		// Token: 0x0600331C RID: 13084 RVA: 0x000C729C File Offset: 0x000C549C
		internal static string MembersToCommaSeparatedString(IEnumerable members)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			StringUtil.ToCommaSeparatedString(stringBuilder, members);
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x0600331D RID: 13085 RVA: 0x000C72D4 File Offset: 0x000C54D4
		internal static void ToCommaSeparatedString(StringBuilder builder, IEnumerable list)
		{
			StringUtil.ToSeparatedStringPrivate(builder, list, ", ", string.Empty, false);
		}

		// Token: 0x0600331E RID: 13086 RVA: 0x000C72E8 File Offset: 0x000C54E8
		internal static void ToCommaSeparatedStringSorted(StringBuilder builder, IEnumerable list)
		{
			StringUtil.ToSeparatedStringPrivate(builder, list, ", ", string.Empty, true);
		}

		// Token: 0x0600331F RID: 13087 RVA: 0x000C72FC File Offset: 0x000C54FC
		internal static void ToSeparatedString(StringBuilder builder, IEnumerable list, string separator)
		{
			StringUtil.ToSeparatedStringPrivate(builder, list, separator, string.Empty, false);
		}

		// Token: 0x06003320 RID: 13088 RVA: 0x000C730C File Offset: 0x000C550C
		internal static void ToSeparatedStringSorted(StringBuilder builder, IEnumerable list, string separator)
		{
			StringUtil.ToSeparatedStringPrivate(builder, list, separator, string.Empty, true);
		}

		// Token: 0x06003321 RID: 13089 RVA: 0x000C731C File Offset: 0x000C551C
		internal static void ToSeparatedString(StringBuilder stringBuilder, IEnumerable list, string separator, string nullValue)
		{
			StringUtil.ToSeparatedStringPrivate(stringBuilder, list, separator, nullValue, false);
		}

		// Token: 0x06003322 RID: 13090 RVA: 0x000C7328 File Offset: 0x000C5528
		private static void ToSeparatedStringPrivate(StringBuilder stringBuilder, IEnumerable list, string separator, string nullValue, bool toSort)
		{
			if (list == null)
			{
				return;
			}
			bool flag = true;
			List<string> list2 = new List<string>();
			foreach (object obj in list)
			{
				string item;
				if (obj == null)
				{
					item = nullValue;
				}
				else
				{
					item = StringUtil.FormatInvariant("{0}", new object[]
					{
						obj
					});
				}
				list2.Add(item);
			}
			if (toSort)
			{
				list2.Sort(StringComparer.Ordinal);
			}
			foreach (string value in list2)
			{
				if (!flag)
				{
					stringBuilder.Append(separator);
				}
				stringBuilder.Append(value);
				flag = false;
			}
		}

		// Token: 0x06003323 RID: 13091 RVA: 0x000C7404 File Offset: 0x000C5604
		internal static bool IsNullOrEmptyOrWhiteSpace(string value)
		{
			return StringUtil.IsNullOrEmptyOrWhiteSpace(value, 0);
		}

		// Token: 0x06003324 RID: 13092 RVA: 0x000C7410 File Offset: 0x000C5610
		internal static bool IsNullOrEmptyOrWhiteSpace(string value, int offset)
		{
			if (value != null)
			{
				for (int i = offset; i < value.Length; i++)
				{
					if (!char.IsWhiteSpace(value[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06003325 RID: 13093 RVA: 0x000C7444 File Offset: 0x000C5644
		internal static bool IsNullOrEmptyOrWhiteSpace(string value, int offset, int length)
		{
			if (value != null)
			{
				length = Math.Min(value.Length, length);
				for (int i = offset; i < length; i++)
				{
					if (!char.IsWhiteSpace(value[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06003326 RID: 13094 RVA: 0x000C747F File Offset: 0x000C567F
		internal static string FormatInvariant(string format, params object[] args)
		{
			return string.Format(CultureInfo.InvariantCulture, format, args);
		}

		// Token: 0x06003327 RID: 13095 RVA: 0x000C748D File Offset: 0x000C568D
		internal static StringBuilder FormatStringBuilder(StringBuilder builder, string format, params object[] args)
		{
			builder.AppendFormat(CultureInfo.InvariantCulture, format, args);
			return builder;
		}

		// Token: 0x06003328 RID: 13096 RVA: 0x000C74A0 File Offset: 0x000C56A0
		internal static StringBuilder IndentNewLine(StringBuilder builder, int indent)
		{
			builder.AppendLine();
			for (int i = 0; i < indent; i++)
			{
				builder.Append("    ");
			}
			return builder;
		}

		// Token: 0x06003329 RID: 13097 RVA: 0x000C74D0 File Offset: 0x000C56D0
		internal static string FormatIndex(string arrayVarName, int index)
		{
			StringBuilder stringBuilder = new StringBuilder(arrayVarName.Length + 10 + 2);
			return stringBuilder.Append(arrayVarName).Append('[').Append(index).Append(']').ToString();
		}

		// Token: 0x0600332A RID: 13098 RVA: 0x000C750E File Offset: 0x000C570E
		private static string InvariantConvertToString<T>(T value)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
			{
				value
			});
		}

		// Token: 0x0400166B RID: 5739
		private const string s_defaultDelimiter = ", ";

		// Token: 0x0200067D RID: 1661
		// (Invoke) Token: 0x060044D4 RID: 17620
		internal delegate string ToStringConverter<T>(T value);
	}
}
