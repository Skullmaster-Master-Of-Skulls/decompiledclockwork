using System;
using System.Globalization;
using System.Web.Razor.Text;

namespace System.Web.Razor.Generator
{
	// Token: 0x02000011 RID: 17
	internal static class CodeWriterExtensions
	{
		// Token: 0x06000082 RID: 130 RVA: 0x000033C0 File Offset: 0x000015C0
		public static void WriteLocationTaggedString(this CodeWriter writer, LocationTagged<string> value)
		{
			writer.WriteStartMethodInvoke("Tuple.Create");
			writer.WriteStringLiteral(value.Value);
			writer.WriteParameterSeparator();
			writer.WriteSnippet(value.Location.AbsoluteIndex.ToString(CultureInfo.CurrentCulture));
			writer.WriteEndMethodInvoke();
		}
	}
}
