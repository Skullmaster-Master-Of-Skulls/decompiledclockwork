using System;

namespace System.Web
{
	// Token: 0x02000007 RID: 7
	internal static class PathHelpers
	{
		// Token: 0x06000026 RID: 38 RVA: 0x0000266C File Offset: 0x0000086C
		public static bool EndsWithExtension(string path, string extension)
		{
			if (path.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
			{
				int length = extension.Length;
				int length2 = path.Length;
				return length2 > length && path[length2 - length - 1] == '.';
			}
			return false;
		}
	}
}
