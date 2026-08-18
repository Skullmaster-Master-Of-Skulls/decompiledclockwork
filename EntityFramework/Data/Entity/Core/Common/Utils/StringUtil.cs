using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x02000332 RID: 818
	internal static class StringUtil
	{
		// Token: 0x06001C58 RID: 7256 RVA: 0x0008B114 File Offset: 0x00089314
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

		// Token: 0x06001C59 RID: 7257 RVA: 0x0008B1A4 File Offset: 0x000893A4
		internal static string ToCommaSeparatedString(IEnumerable list)
		{
			return StringUtil.ToSeparatedString(list, ", ", string.Empty);
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x0008B1B8 File Offset: 0x000893B8
		internal static string ToSeparatedString(IEnumerable list, string separator, string nullValue)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringUtil.ToSeparatedString(stringBuilder, list, separator, nullValue);
			return stringBuilder.ToString();
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x0008B1DA File Offset: 0x000893DA
		internal static string ToCommaSeparatedStringSorted(IEnumerable list)
		{
			return StringUtil.ToSeparatedStringSorted(list, ", ", string.Empty);
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x0008B1EC File Offset: 0x000893EC
		internal static string ToSeparatedStringSorted(IEnumerable list, string separator, string nullValue)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringUtil.ToSeparatedStringPrivate(stringBuilder, list, separator, nullValue, true);
			return stringBuilder.ToString();
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x0008B210 File Offset: 0x00089410
		internal static string MembersToCommaSeparatedString(IEnumerable members)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			StringUtil.ToCommaSeparatedString(stringBuilder, members);
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x0008B248 File Offset: 0x00089448
		internal static void ToCommaSeparatedString(StringBuilder builder, IEnumerable list)
		{
			StringUtil.ToSeparatedStringPrivate(builder, list, ", ", string.Empty, false);
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x0008B25C File Offset: 0x0008945C
		internal static void ToCommaSeparatedStringSorted(StringBuilder builder, IEnumerable list)
		{
			StringUtil.ToSeparatedStringPrivate(builder, list, ", ", string.Empty, true);
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x0008B270 File Offset: 0x00089470
		internal static void ToSeparatedString(StringBuilder builder, IEnumerable list, string separator)
		{
			StringUtil.ToSeparatedStringPrivate(builder, list, separator, string.Empty, false);
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x0008B280 File Offset: 0x00089480
		internal static void ToSeparatedStringSorted(StringBuilder builder, IEnumerable list, string separator)
		{
			StringUtil.ToSeparatedStringPrivate(builder, list, separator, string.Empty, true);
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x0008B290 File Offset: 0x00089490
		internal static void ToSeparatedString(StringBuilder stringBuilder, IEnumerable list, string separator, string nullValue)
		{
			StringUtil.ToSeparatedStringPrivate(stringBuilder, list, separator, nullValue, false);
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x0008B29C File Offset: 0x0008949C
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

		// Token: 0x06001C64 RID: 7268 RVA: 0x0008B37C File Offset: 0x0008957C
		internal static string FormatInvariant(string format, params object[] args)
		{
			return string.Format(CultureInfo.InvariantCulture, format, args);
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x0008B38A File Offset: 0x0008958A
		internal static StringBuilder FormatStringBuilder(StringBuilder builder, string format, params object[] args)
		{
			builder.AppendFormat(CultureInfo.InvariantCulture, format, args);
			return builder;
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x0008B39C File Offset: 0x0008959C
		internal static StringBuilder IndentNewLine(StringBuilder builder, int indent)
		{
			builder.AppendLine();
			for (int i = 0; i < indent; i++)
			{
				builder.Append("    ");
			}
			return builder;
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x0008B3CC File Offset: 0x000895CC
		internal static string FormatIndex(string arrayVarName, int index)
		{
			StringBuilder stringBuilder = new StringBuilder(arrayVarName.Length + 10 + 2);
			return stringBuilder.Append(arrayVarName).Append('[').Append(index).Append(']').ToString();
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x0008B40C File Offset: 0x0008960C
		private static string InvariantConvertToString<T>(T value)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
			{
				value
			});
		}

		// Token: 0x040009CC RID: 2508
		private const string s_defaultDelimiter = ", ";

		// Token: 0x02000333 RID: 819
		// (Invoke) Token: 0x06001C6A RID: 7274
		internal delegate string ToStringConverter<T>(T value);
	}
}
