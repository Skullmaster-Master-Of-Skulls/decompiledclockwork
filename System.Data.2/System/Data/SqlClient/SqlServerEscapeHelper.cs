using System;
using System.Data.Common;
using System.Text;

namespace System.Data.SqlClient
{
	// Token: 0x02000204 RID: 516
	internal static class SqlServerEscapeHelper
	{
		// Token: 0x060020F2 RID: 8434 RVA: 0x000DE388 File Offset: 0x000DD788
		internal static string EscapeIdentifier(string name)
		{
			return "[" + name.Replace("]", "]]") + "]";
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x000DE3B4 File Offset: 0x000DD7B4
		internal static void EscapeIdentifier(StringBuilder builder, string name)
		{
			builder.Append("[");
			builder.Append(name.Replace("]", "]]"));
			builder.Append("]");
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x000DE3F0 File Offset: 0x000DD7F0
		internal static string EscapeStringAsLiteral(string input)
		{
			return input.Replace("'", "''");
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x000DE410 File Offset: 0x000DD810
		internal static string MakeStringLiteral(string input)
		{
			if (ADP.IsEmpty(input))
			{
				return "''";
			}
			return "'" + SqlServerEscapeHelper.EscapeStringAsLiteral(input) + "'";
		}
	}
}
