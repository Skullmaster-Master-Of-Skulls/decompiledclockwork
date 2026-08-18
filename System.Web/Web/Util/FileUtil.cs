using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.Util
{
	// Token: 0x02000768 RID: 1896
	internal class FileUtil
	{
		// Token: 0x06005C0D RID: 23565 RVA: 0x001716B7 File Offset: 0x001706B7
		private FileUtil()
		{
		}

		// Token: 0x06005C0E RID: 23566 RVA: 0x001716C0 File Offset: 0x001706C0
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

		// Token: 0x06005C0F RID: 23567 RVA: 0x001716EC File Offset: 0x001706EC
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

		// Token: 0x06005C10 RID: 23568 RVA: 0x00171724 File Offset: 0x00170724
		internal static string TruncatePathIfNeeded(string path, int reservedLength)
		{
			int num = FileUtil._maxPathLength - reservedLength;
			if (path.Length > num)
			{
				path = path.Substring(0, num - 13) + path.GetHashCode().ToString(CultureInfo.InvariantCulture);
			}
			return path;
		}

		// Token: 0x06005C11 RID: 23569 RVA: 0x00171768 File Offset: 0x00170768
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

		// Token: 0x06005C12 RID: 23570 RVA: 0x00171792 File Offset: 0x00170792
		internal static void CheckSuspiciousPhysicalPath(string physicalPath)
		{
			if (FileUtil.IsSuspiciousPhysicalPath(physicalPath))
			{
				throw new HttpException(404, string.Empty);
			}
		}

		// Token: 0x06005C13 RID: 23571 RVA: 0x001717AC File Offset: 0x001707AC
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
			if (physicalPath.IndexOf('/') >= 0 || physicalPath.IndexOf("\\..", StringComparison.Ordinal) >= 0)
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

		// Token: 0x06005C14 RID: 23572 RVA: 0x0017181C File Offset: 0x0017081C
		[FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.PathDiscovery)]
		private static bool IsSuspiciousPhysicalPath(string physicalPath, out bool pathTooLong)
		{
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
			catch (ArgumentException)
			{
				result = true;
				pathTooLong = true;
			}
			return result;
		}

		// Token: 0x06005C15 RID: 23573 RVA: 0x0017187C File Offset: 0x0017087C
		private static bool HasInvalidLastChar(string physicalPath)
		{
			if (string.IsNullOrEmpty(physicalPath))
			{
				return false;
			}
			char c = physicalPath[physicalPath.Length - 1];
			return c == ' ' || c == '.';
		}

		// Token: 0x06005C16 RID: 23574 RVA: 0x001718B0 File Offset: 0x001708B0
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

		// Token: 0x06005C17 RID: 23575 RVA: 0x001718F0 File Offset: 0x001708F0
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

		// Token: 0x06005C18 RID: 23576 RVA: 0x00171934 File Offset: 0x00170934
		internal static bool IsValidDirectoryName(string name)
		{
			return !string.IsNullOrEmpty(name) && name.IndexOfAny(FileUtil._invalidFileNameChars, 0) == -1 && !name.Equals(".") && !name.Equals("..");
		}

		// Token: 0x06005C19 RID: 23577 RVA: 0x00171970 File Offset: 0x00170970
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

		// Token: 0x06005C1A RID: 23578 RVA: 0x00171A08 File Offset: 0x00170A08
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

		// Token: 0x0400313E RID: 12606
		private static int _maxPathLength = 259;

		// Token: 0x0400313F RID: 12607
		private static char[] _invalidFileNameChars = Path.GetInvalidFileNameChars();
	}
}
