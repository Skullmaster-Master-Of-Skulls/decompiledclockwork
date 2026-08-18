using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace Telerik.Web.UI
{
	// Token: 0x02000B09 RID: 2825
	internal static class JavaScriptString
	{
		// Token: 0x060069C8 RID: 27080 RVA: 0x0018D6D8 File Offset: 0x0018B8D8
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Callable implementation of System.Web.Script.Serialization.JavaScriptString.QuoteString")]
		internal static string QuoteString(string value)
		{
			StringBuilder stringBuilder = null;
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			int startIndex = 0;
			int num = 0;
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				if (c == '\r' || c == '\t' || c == '"' || c == '\'' || c == '<' || c == '>' || c == '\\' || c == '\n' || c == '\b' || c == '\f' || c < ' ')
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(value.Length + 5);
					}
					if (num > 0)
					{
						stringBuilder.Append(value, startIndex, num);
					}
					startIndex = i + 1;
					num = 0;
				}
				char c2 = c;
				if (c2 <= '"')
				{
					switch (c2)
					{
					case '\b':
						stringBuilder.Append("\\b");
						break;
					case '\t':
						stringBuilder.Append("\\t");
						break;
					case '\n':
						stringBuilder.Append("\\n");
						break;
					case '\v':
						goto IL_153;
					case '\f':
						stringBuilder.Append("\\f");
						break;
					case '\r':
						stringBuilder.Append("\\r");
						break;
					default:
						if (c2 != '"')
						{
							goto IL_153;
						}
						stringBuilder.Append("\\\"");
						break;
					}
				}
				else
				{
					if (c2 != '\'')
					{
						switch (c2)
						{
						case '<':
						case '>':
							break;
						case '=':
							goto IL_153;
						default:
							if (c2 != '\\')
							{
								goto IL_153;
							}
							stringBuilder.Append("\\\\");
							goto IL_167;
						}
					}
					JavaScriptString.AppendCharAsUnicode(stringBuilder, c);
				}
				IL_167:
				i++;
				continue;
				IL_153:
				if (c < ' ')
				{
					JavaScriptString.AppendCharAsUnicode(stringBuilder, c);
					goto IL_167;
				}
				num++;
				goto IL_167;
			}
			if (stringBuilder == null)
			{
				return value;
			}
			if (num > 0)
			{
				stringBuilder.Append(value, startIndex, num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060069C9 RID: 27081 RVA: 0x0018D878 File Offset: 0x0018BA78
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Callable implementation of System.Web.Script.Serialization.JavaScriptString.AppendCharAsUnicode")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "1#c", Justification = "Callable implementation of System.Web.Script.Serialization.JavaScriptString.AppendCharAsUnicode")]
		internal static void AppendCharAsUnicode(StringBuilder builder, char c)
		{
			builder.Append("\\u");
			builder.AppendFormat(CultureInfo.InvariantCulture, "{0:x4}", new object[]
			{
				(int)c
			});
		}
	}
}
