using System;

namespace System.Runtime.InteropServices.TCEAdapterGen
{
	// Token: 0x020008F6 RID: 2294
	internal static class NameSpaceExtractor
	{
		// Token: 0x0600532A RID: 21290 RVA: 0x0012CC14 File Offset: 0x0012BC14
		public static string ExtractNameSpace(string FullyQualifiedTypeName)
		{
			int num = FullyQualifiedTypeName.LastIndexOf(NameSpaceExtractor.NameSpaceSeperator);
			if (num == -1)
			{
				return "";
			}
			return FullyQualifiedTypeName.Substring(0, num);
		}

		// Token: 0x04002B0E RID: 11022
		private static char NameSpaceSeperator = '.';
	}
}
