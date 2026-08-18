using System;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration
{
	// Token: 0x0200042A RID: 1066
	internal static class CqlWriter
	{
		// Token: 0x06002740 RID: 10048 RVA: 0x000BE484 File Offset: 0x000BC684
		internal static string GetQualifiedName(string blockName, string field)
		{
			return StringUtil.FormatInvariant("{0}.{1}", new object[]
			{
				blockName,
				field
			});
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x000BE4AD File Offset: 0x000BC6AD
		internal static void AppendEscapedTypeName(StringBuilder builder, EdmType type)
		{
			CqlWriter.AppendEscapedName(builder, CqlWriter.GetQualifiedName(type.NamespaceName, type.Name));
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x000BE4C6 File Offset: 0x000BC6C6
		internal static void AppendEscapedQualifiedName(StringBuilder builder, string name1, string name2)
		{
			CqlWriter.AppendEscapedName(builder, name1);
			builder.Append('.');
			CqlWriter.AppendEscapedName(builder, name2);
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x000BE4E0 File Offset: 0x000BC6E0
		internal static void AppendEscapedName(StringBuilder builder, string name)
		{
			if (CqlWriter._wordIdentifierRegex.IsMatch(name) && !ExternalCalls.IsReservedKeyword(name))
			{
				builder.Append(name);
				return;
			}
			string value = name.Replace("]", "]]");
			builder.Append('[').Append(value).Append(']');
		}

		// Token: 0x04000EBC RID: 3772
		private static readonly Regex _wordIdentifierRegex = new Regex("^[_A-Za-z]\\w*$", RegexOptions.Compiled | RegexOptions.ECMAScript);
	}
}
