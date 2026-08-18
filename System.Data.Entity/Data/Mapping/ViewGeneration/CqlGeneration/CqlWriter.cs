using System;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration.Utils;
using System.Data.Metadata.Edm;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Data.Mapping.ViewGeneration.CqlGeneration
{
	// Token: 0x02000276 RID: 630
	internal static class CqlWriter
	{
		// Token: 0x0600264C RID: 9804 RVA: 0x0009208C File Offset: 0x0009028C
		internal static string GetQualifiedName(string blockName, string field)
		{
			return StringUtil.FormatInvariant("{0}.{1}", new object[]
			{
				blockName,
				field
			});
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x000920B3 File Offset: 0x000902B3
		internal static void AppendEscapedTypeName(StringBuilder builder, EdmType type)
		{
			CqlWriter.AppendEscapedName(builder, CqlWriter.GetQualifiedName(type.NamespaceName, type.Name));
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x000920CC File Offset: 0x000902CC
		internal static void AppendEscapedQualifiedName(StringBuilder builder, string name1, string name2)
		{
			CqlWriter.AppendEscapedName(builder, name1);
			builder.Append('.');
			CqlWriter.AppendEscapedName(builder, name2);
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x000920E8 File Offset: 0x000902E8
		internal static void AppendEscapedName(StringBuilder builder, string name)
		{
			if (CqlWriter.s_wordIdentifierRegex.IsMatch(name) && !ExternalCalls.IsReservedKeyword(name))
			{
				builder.Append(name);
				return;
			}
			string value = name.Replace("]", "]]");
			builder.Append('[').Append(value).Append(']');
		}

		// Token: 0x040011C0 RID: 4544
		private static readonly Regex s_wordIdentifierRegex = new Regex("^[_A-Za-z]\\w*$", RegexOptions.Compiled | RegexOptions.ECMAScript);
	}
}
