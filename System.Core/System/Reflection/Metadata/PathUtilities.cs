using System;
using System.IO;

namespace System.Reflection.Metadata
{
	// Token: 0x02000051 RID: 81
	internal static class PathUtilities
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00005CA8 File Offset: 0x00003EA8
		private static string PlatformSpecificDirectorySeparator
		{
			get
			{
				if (PathUtilities.s_platformSpecificDirectorySeparator == null)
				{
					PathUtilities.s_platformSpecificDirectorySeparator = ((Array.IndexOf<char>(Path.GetInvalidFileNameChars(), '*') >= 0) ? '\\' : '/').ToString();
				}
				return PathUtilities.s_platformSpecificDirectorySeparator;
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00005CE4 File Offset: 0x00003EE4
		internal static int IndexOfFileName(string path)
		{
			if (path == null)
			{
				return -1;
			}
			for (int i = path.Length - 1; i >= 0; i--)
			{
				char c = path[i];
				if (c == '\\' || c == '/' || c == ':')
				{
					return i + 1;
				}
			}
			return 0;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00005D28 File Offset: 0x00003F28
		internal static string GetFileName(string path, bool includeExtension = true)
		{
			int num = PathUtilities.IndexOfFileName(path);
			if (num > 0)
			{
				return path.Substring(num);
			}
			return path;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00005D4C File Offset: 0x00003F4C
		internal static string CombinePathWithRelativePath(string root, string relativePath)
		{
			if (root.Length == 0)
			{
				return relativePath;
			}
			char c = root[root.Length - 1];
			if (c == '\\' || c == '/' || c == ':')
			{
				return root + relativePath;
			}
			return root + PathUtilities.PlatformSpecificDirectorySeparator + relativePath;
		}

		// Token: 0x040002F5 RID: 757
		private const char DirectorySeparatorChar = '\\';

		// Token: 0x040002F6 RID: 758
		private const char AltDirectorySeparatorChar = '/';

		// Token: 0x040002F7 RID: 759
		private const char VolumeSeparatorChar = ':';

		// Token: 0x040002F8 RID: 760
		private static string s_platformSpecificDirectorySeparator;
	}
}
