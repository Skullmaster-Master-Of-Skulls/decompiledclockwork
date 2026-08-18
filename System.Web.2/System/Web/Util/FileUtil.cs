using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.Util
{
	// Token: 0x02000200 RID: 512
	internal class FileUtil
	{
		// Token: 0x06001921 RID: 6433 RVA: 0x000030B5 File Offset: 0x000012B5
		private FileUtil()
		{
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x0004DCB4 File Offset: 0x0004BEB4
		[FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.Read)]
		internal static bool FileExists(string filename)
		{
			bool result = false;
			try
			{
				result = File.Exists(filename);
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x0004DCE0 File Offset: 0x0004BEE0
		internal static string GetFirstExistingDirectory(string appRoot, string fileName)
		{
			if (FileUtil.IsBeneathAppRoot(appRoot, fileName))
			{
				string text = appRoot;
				for (;;)
				{
					int num = fileName.IndexOf(Path.DirectorySeparatorChar, text.Length + 1);
					if (num <= -1)
					{
						break;
					}
					string text2 = fileName.Substring(0, num);
					if (!FileUtil.DirectoryExists(text2, false))
					{
						break;
					}
					text = text2;
				}
				return text;
			}
			return null;
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x0004DD29 File Offset: 0x0004BF29
		internal static bool IsBeneathAppRoot(string appRoot, string filePath)
		{
			return filePath.Length > appRoot.Length + 1 && filePath.IndexOf(appRoot, StringComparison.OrdinalIgnoreCase) > -1 && filePath[appRoot.Length] == Path.DirectorySeparatorChar;
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x0004DD5C File Offset: 0x0004BF5C
		internal static string RemoveTrailingDirectoryBackSlash(string path)
		{
			if (path == null)
			{
				return null;
			}
			int length = path.Length;
			if (length > 3 && path[length - 1] == '\\')
			{
				path = path.Substring(0, length - 1);
			}
			return path;
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x0004DD94 File Offset: 0x0004BF94
		internal static string TruncatePathIfNeeded(string path, int reservedLength)
		{
			int num = FileUtil._maxPathLength - reservedLength;
			if (path.Length > num)
			{
				path = path.Substring(0, num - 13) + path.GetHashCode().ToString(CultureInfo.InvariantCulture);
			}
			return path;
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x0004DDD8 File Offset: 0x0004BFD8
		internal static string FixUpPhysicalDirectory(string dir)
		{
			if (dir == null)
			{
				return null;
			}
			dir = Path.GetFullPath(dir);
			if (!StringUtil.StringEndsWith(dir, "\\"))
			{
				dir += "\\";
			}
			return dir;
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x0004DE02 File Offset: 0x0004C002
		internal static void CheckSuspiciousPhysicalPath(string physicalPath)
		{
			if (FileUtil.IsSuspiciousPhysicalPath(physicalPath))
			{
				throw new HttpException(404, string.Empty);
			}
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x0004DE1C File Offset: 0x0004C01C
		internal static bool IsSuspiciousPhysicalPath(string physicalPath)
		{
			bool flag;
			if (!FileUtil.IsSuspiciousPhysicalPath(physicalPath, out flag))
			{
				return false;
			}
			if (!flag)
			{
				return true;
			}
			if (physicalPath.IndexOf('/') >= 0)
			{
				return true;
			}
			string text = "\\..";
			int num = physicalPath.IndexOf(text, StringComparison.Ordinal);
			if (num >= 0 && (physicalPath.Length == num + text.Length || physicalPath[num + text.Length] == '\\'))
			{
				return true;
			}
			for (int i = physicalPath.LastIndexOf('\\'); i >= 0; i = physicalPath.LastIndexOf('\\', i - 1))
			{
				string physicalPath2 = physicalPath.Substring(0, i);
				if (!FileUtil.IsSuspiciousPhysicalPath(physicalPath2, out flag))
				{
					return false;
				}
				if (!flag)
				{
					return true;
				}
			}
			return true;
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x0004DEB8 File Offset: 0x0004C0B8
		[FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.PathDiscovery)]
		internal static bool IsSuspiciousPhysicalPath(string physicalPath, out bool pathTooLong)
		{
			if (physicalPath != null && (physicalPath.Length > FileUtil._maxPathLength || physicalPath.IndexOfAny(FileUtil.s_invalidPathChars) != -1 || (physicalPath.Length > 0 && physicalPath[0] == ':') || (physicalPath.Length > 2 && physicalPath.IndexOf(':', 2) > 0)))
			{
				pathTooLong = true;
				return true;
			}
			bool result;
			try
			{
				result = (!string.IsNullOrEmpty(physicalPath) && string.Compare(physicalPath, Path.GetFullPath(physicalPath), StringComparison.OrdinalIgnoreCase) != 0);
				pathTooLong = false;
			}
			catch (PathTooLongException)
			{
				result = true;
				pathTooLong = true;
			}
			catch (NotSupportedException)
			{
				result = true;
				pathTooLong = true;
			}
			catch (ArgumentException)
			{
				result = true;
				pathTooLong = true;
			}
			return result;
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x0004DF74 File Offset: 0x0004C174
		private static bool HasInvalidLastChar(string physicalPath)
		{
			if (string.IsNullOrEmpty(physicalPath))
			{
				return false;
			}
			char c = physicalPath[physicalPath.Length - 1];
			return c == ' ' || c == '.';
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x0004DFA8 File Offset: 0x0004C1A8
		internal static bool DirectoryExists(string dirname)
		{
			bool result = false;
			dirname = FileUtil.RemoveTrailingDirectoryBackSlash(dirname);
			if (FileUtil.HasInvalidLastChar(dirname))
			{
				return false;
			}
			try
			{
				result = Directory.Exists(dirname);
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x0004DFE8 File Offset: 0x0004C1E8
		internal static bool DirectoryAccessible(string dirname)
		{
			bool result = false;
			dirname = FileUtil.RemoveTrailingDirectoryBackSlash(dirname);
			if (FileUtil.HasInvalidLastChar(dirname))
			{
				return false;
			}
			try
			{
				result = new DirectoryInfo(dirname).Exists;
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x0004E02C File Offset: 0x0004C22C
		internal static bool IsValidDirectoryName(string name)
		{
			return !string.IsNullOrEmpty(name) && name.IndexOfAny(FileUtil._invalidFileNameChars, 0) == -1 && !name.Equals(".") && !name.Equals("..");
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x0004E068 File Offset: 0x0004C268
		internal static void PhysicalPathStatus(string physicalPath, bool directoryExistsOnError, bool fileExistsOnError, out bool exists, out bool isDirectory)
		{
			exists = false;
			isDirectory = true;
			if (string.IsNullOrEmpty(physicalPath))
			{
				return;
			}
			using (new ApplicationImpersonationContext())
			{
				UnsafeNativeMethods.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA;
				bool fileAttributesEx = UnsafeNativeMethods.GetFileAttributesEx(physicalPath, 0, out win32_FILE_ATTRIBUTE_DATA);
				if (fileAttributesEx)
				{
					exists = true;
					isDirectory = ((win32_FILE_ATTRIBUTE_DATA.fileAttributes & 16) == 16);
					if (isDirectory && FileUtil.HasInvalidLastChar(physicalPath))
					{
						exists = false;
					}
				}
				else if (directoryExistsOnError || fileExistsOnError)
				{
					int hrforLastWin32Error = Marshal.GetHRForLastWin32Error();
					if (hrforLastWin32Error != -2147024894 && hrforLastWin32Error != -2147024893)
					{
						exists = true;
						isDirectory = directoryExistsOnError;
					}
				}
			}
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x0004E0FC File Offset: 0x0004C2FC
		internal static bool DirectoryExists(string filename, bool trueOnError)
		{
			filename = FileUtil.RemoveTrailingDirectoryBackSlash(filename);
			if (FileUtil.HasInvalidLastChar(filename))
			{
				return false;
			}
			UnsafeNativeMethods.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA;
			bool fileAttributesEx = UnsafeNativeMethods.GetFileAttributesEx(filename, 0, out win32_FILE_ATTRIBUTE_DATA);
			if (fileAttributesEx)
			{
				return (win32_FILE_ATTRIBUTE_DATA.fileAttributes & 16) == 16;
			}
			if (!trueOnError)
			{
				return false;
			}
			int hrforLastWin32Error = Marshal.GetHRForLastWin32Error();
			return hrforLastWin32Error != -2147024894 && hrforLastWin32Error != -2147024893;
		}

		// Token: 0x040017B0 RID: 6064
		private static int _maxPathLength = 259;

		// Token: 0x040017B1 RID: 6065
		private static readonly char[] s_invalidPathChars = Path.GetInvalidPathChars();

		// Token: 0x040017B2 RID: 6066
		private static char[] _invalidFileNameChars = Path.GetInvalidFileNameChars();
	}
}
