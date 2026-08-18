using System;
using System.IO;

namespace System.Configuration
{
	// Token: 0x0200009F RID: 159
	internal static class UrlPath
	{
		// Token: 0x06000643 RID: 1603 RVA: 0x0001D7D0 File Offset: 0x0001B9D0
		internal static string GetDirectoryOrRootName(string path)
		{
			string text = Path.GetDirectoryName(path);
			if (text == null)
			{
				text = Path.GetPathRoot(path);
			}
			return text;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001D7F0 File Offset: 0x0001B9F0
		internal static bool IsEqualOrSubdirectory(string dir, string subdir)
		{
			if (string.IsNullOrEmpty(dir))
			{
				return true;
			}
			if (string.IsNullOrEmpty(subdir))
			{
				return false;
			}
			int num = dir.Length;
			if (dir[num - 1] == '\\')
			{
				num--;
			}
			int num2 = subdir.Length;
			if (subdir[num2 - 1] == '\\')
			{
				num2--;
			}
			return num2 >= num && string.Compare(dir, 0, subdir, 0, num, StringComparison.OrdinalIgnoreCase) == 0 && (num2 <= num || subdir[num] == '\\');
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0001D868 File Offset: 0x0001BA68
		internal static bool IsEqualOrSubpath(string path, string subpath)
		{
			return UrlPath.IsEqualOrSubpathImpl(path, subpath, false);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001D872 File Offset: 0x0001BA72
		internal static bool IsSubpath(string path, string subpath)
		{
			return UrlPath.IsEqualOrSubpathImpl(path, subpath, true);
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001D87C File Offset: 0x0001BA7C
		private static bool IsEqualOrSubpathImpl(string path, string subpath, bool excludeEqual)
		{
			if (string.IsNullOrEmpty(path))
			{
				return true;
			}
			if (string.IsNullOrEmpty(subpath))
			{
				return false;
			}
			int num = path.Length;
			if (path[num - 1] == '/')
			{
				num--;
			}
			int num2 = subpath.Length;
			if (subpath[num2 - 1] == '/')
			{
				num2--;
			}
			return num2 >= num && (!excludeEqual || num2 != num) && string.Compare(path, 0, subpath, 0, num, StringComparison.OrdinalIgnoreCase) == 0 && (num2 <= num || subpath[num] == '/');
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001D8FD File Offset: 0x0001BAFD
		private static bool IsDirectorySeparatorChar(char ch)
		{
			return ch == '\\' || ch == '/';
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001D90B File Offset: 0x0001BB0B
		private static bool IsAbsoluteLocalPhysicalPath(string path)
		{
			return path != null && path.Length >= 3 && path[1] == ':' && UrlPath.IsDirectorySeparatorChar(path[2]);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001D934 File Offset: 0x0001BB34
		private static bool IsAbsoluteUNCPhysicalPath(string path)
		{
			return path != null && path.Length >= 3 && UrlPath.IsDirectorySeparatorChar(path[0]) && UrlPath.IsDirectorySeparatorChar(path[1]);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001D960 File Offset: 0x0001BB60
		internal static string ConvertFileNameToUrl(string fileName)
		{
			string str;
			if (UrlPath.IsAbsoluteLocalPhysicalPath(fileName))
			{
				str = "file:///";
			}
			else
			{
				if (!UrlPath.IsAbsoluteUNCPhysicalPath(fileName))
				{
					throw ExceptionUtil.ParameterInvalid("filename");
				}
				str = "file:";
			}
			return str + fileName.Replace('\\', '/');
		}

		// Token: 0x04000367 RID: 871
		private const string FILE_URL_LOCAL = "file:///";

		// Token: 0x04000368 RID: 872
		private const string FILE_URL_UNC = "file:";
	}
}
