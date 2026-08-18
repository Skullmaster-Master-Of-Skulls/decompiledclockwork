using System;
using System.IO;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000C4 RID: 196
	public static class SourceMapFactory
	{
		// Token: 0x06000D65 RID: 3429 RVA: 0x000408F4 File Offset: 0x0003EAF4
		public static ISourceMap Create(TextWriter writer, string implementationName)
		{
			ISourceMap result = null;
			if (string.Compare(implementationName, V3SourceMap.ImplementationName, StringComparison.OrdinalIgnoreCase) == 0)
			{
				result = new V3SourceMap(writer);
			}
			else if (string.Compare(implementationName, ScriptSharpSourceMap.ImplementationName, StringComparison.OrdinalIgnoreCase) == 0)
			{
				result = new ScriptSharpSourceMap(writer);
			}
			return result;
		}
	}
}
