using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.IO
{
	// Token: 0x020005A9 RID: 1449
	[ComVisible(true)]
	public static class Directory
	{
		// Token: 0x06003534 RID: 13620 RVA: 0x000B0798 File Offset: 0x000AF798
		public static DirectoryInfo GetParent(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_PathEmpty"), "path");
			}
			string fullPathInternal = Path.GetFullPathInternal(path);
			string directoryName = Path.GetDirectoryName(fullPathInternal);
			if (directoryName == null)
			{
				return null;
			}
			return new DirectoryInfo(directoryName);
		}

		// Token: 0x06003535 RID: 13621 RVA: 0x000B07E9 File Offset: 0x000AF7E9
		public static DirectoryInfo CreateDirectory(string path)
		{
			return Directory.CreateDirectory(path, null);
		}

		// Token: 0x06003536 RID: 13622 RVA: 0x000B07F4 File Offset: 0x000AF7F4
		public static DirectoryInfo CreateDirectory(string path, DirectorySecurity directorySecurity)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_PathEmpty"));
			}
			string fullPathInternal = Path.GetFullPathInternal(path);
			string demandDir = Directory.GetDemandDir(fullPathInternal, true);
			new FileIOPermission(FileIOPermissionAccess.Read, new string[]
			{
				demandDir
			}, false, false).Demand();
			Directory.InternalCreateDirectory(fullPathInternal, path, directorySecurity);
			return new DirectoryInfo(fullPathInternal, false);
		}

		// Token: 0x06003537 RID: 13623 RVA: 0x000B0860 File Offset: 0x000AF860
		internal static string GetDemandDir(string fullPath, bool thisDirOnly)
		{
			string result;
			if (thisDirOnly)
			{
				if (fullPath.EndsWith(Path.DirectorySeparatorChar) || fullPath.EndsWith(Path.AltDirectorySeparatorChar))
				{
					result = fullPath + '.';
				}
				else
				{
					result = fullPath + Path.DirectorySeparatorChar + '.';
				}
			}
			else if (!fullPath.EndsWith(Path.DirectorySeparatorChar) && !fullPath.EndsWith(Path.AltDirectorySeparatorChar))
			{
				result = fullPath + Path.DirectorySeparatorChar;
			}
			else
			{
				result = fullPath;
			}
			return result;
		}

		// Token: 0x06003538 RID: 13624 RVA: 0x000B08E4 File Offset: 0x000AF8E4
		internal unsafe static void InternalCreateDirectory(string fullPath, string path, DirectorySecurity dirSecurity)
		{
			int num = fullPath.Length;
			if (num >= 2 && Path.IsDirectorySeparator(fullPath[num - 1]))
			{
				num--;
			}
			int rootLength = Path.GetRootLength(fullPath);
			if (num == 2 && Path.IsDirectorySeparator(fullPath[1]))
			{
				throw new IOException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("IO.IO_CannotCreateDirectory"), new object[]
				{
					path
				}));
			}
			List<string> list = new List<string>();
			bool flag = false;
			if (num > rootLength)
			{
				for (int i = num - 1; i >= rootLength; i--)
				{
					string text = fullPath.Substring(0, i + 1);
					if (!Directory.InternalExists(text))
					{
						list.Add(text);
					}
					else
					{
						flag = true;
					}
					while (i > rootLength && fullPath[i] != Path.DirectorySeparatorChar && fullPath[i] != Path.AltDirectorySeparatorChar)
					{
						i--;
					}
				}
			}
			int count = list.Count;
			if (list.Count != 0)
			{
				string[] array = new string[list.Count];
				list.CopyTo(array, 0);
				for (int j = 0; j < array.Length; j++)
				{
					string[] array2;
					IntPtr intPtr;
					(array2 = array)[(int)(intPtr = (IntPtr)j)] = array2[(int)intPtr] + "\\.";
				}
				AccessControlActions control = (dirSecurity == null) ? AccessControlActions.None : AccessControlActions.Change;
				new FileIOPermission(FileIOPermissionAccess.Write, control, array, false, false).Demand();
			}
			Win32Native.SECURITY_ATTRIBUTES security_ATTRIBUTES = null;
			if (dirSecurity != null)
			{
				security_ATTRIBUTES = new Win32Native.SECURITY_ATTRIBUTES();
				security_ATTRIBUTES.nLength = Marshal.SizeOf(security_ATTRIBUTES);
				byte[] securityDescriptorBinaryForm = dirSecurity.GetSecurityDescriptorBinaryForm();
				byte* ptr = stackalloc byte[1 * securityDescriptorBinaryForm.Length];
				Buffer.memcpy(securityDescriptorBinaryForm, 0, ptr, 0, securityDescriptorBinaryForm.Length);
				security_ATTRIBUTES.pSecurityDescriptor = ptr;
			}
			bool flag2 = true;
			int num2 = 0;
			string maybeFullPath = path;
			while (list.Count > 0)
			{
				string text2 = list[list.Count - 1];
				list.RemoveAt(list.Count - 1);
				if (text2.Length > 248)
				{
					throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
				}
				flag2 = Win32Native.CreateDirectory(text2, security_ATTRIBUTES);
				if (!flag2 && num2 == 0)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error != 183)
					{
						num2 = lastWin32Error;
					}
					else if (File.InternalExists(text2))
					{
						num2 = lastWin32Error;
						try
						{
							new FileIOPermission(FileIOPermissionAccess.PathDiscovery, Directory.GetDemandDir(text2, true)).Demand();
							maybeFullPath = text2;
						}
						catch (SecurityException)
						{
						}
					}
				}
			}
			if (count == 0 && !flag)
			{
				string path2 = Directory.InternalGetDirectoryRoot(fullPath);
				if (!Directory.InternalExists(path2))
				{
					__Error.WinIOError(3, Directory.InternalGetDirectoryRoot(path));
				}
				return;
			}
			if (!flag2 && num2 != 0)
			{
				__Error.WinIOError(num2, maybeFullPath);
			}
		}

		// Token: 0x06003539 RID: 13625 RVA: 0x000B0B60 File Offset: 0x000AFB60
		public static bool Exists(string path)
		{
			try
			{
				if (path == null)
				{
					return false;
				}
				if (path.Length == 0)
				{
					return false;
				}
				string fullPathInternal = Path.GetFullPathInternal(path);
				string demandDir = Directory.GetDemandDir(fullPathInternal, true);
				new FileIOPermission(FileIOPermissionAccess.Read, new string[]
				{
					demandDir
				}, false, false).Demand();
				return Directory.InternalExists(fullPathInternal);
			}
			catch (ArgumentException)
			{
			}
			catch (NotSupportedException)
			{
			}
			catch (SecurityException)
			{
			}
			catch (IOException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			return false;
		}

		// Token: 0x0600353A RID: 13626 RVA: 0x000B0C04 File Offset: 0x000AFC04
		internal static bool InternalExists(string path)
		{
			Win32Native.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Win32Native.WIN32_FILE_ATTRIBUTE_DATA);
			return File.FillAttributeInfo(path, ref win32_FILE_ATTRIBUTE_DATA, false, true) == 0 && win32_FILE_ATTRIBUTE_DATA.fileAttributes != -1 && (win32_FILE_ATTRIBUTE_DATA.fileAttributes & 16) != 0;
		}

		// Token: 0x0600353B RID: 13627 RVA: 0x000B0C43 File Offset: 0x000AFC43
		public static void SetCreationTime(string path, DateTime creationTime)
		{
			Directory.SetCreationTimeUtc(path, creationTime.ToUniversalTime());
		}

		// Token: 0x0600353C RID: 13628 RVA: 0x000B0C54 File Offset: 0x000AFC54
		public unsafe static void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
		{
			if ((Environment.OSInfo & Environment.OSName.WinNT) == Environment.OSName.WinNT)
			{
				using (SafeFileHandle safeFileHandle = Directory.OpenHandle(path))
				{
					Win32Native.FILE_TIME file_TIME = new Win32Native.FILE_TIME(creationTimeUtc.ToFileTimeUtc());
					if (!Win32Native.SetFileTime(safeFileHandle, &file_TIME, null, null))
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						__Error.WinIOError(lastWin32Error, path);
					}
				}
			}
		}

		// Token: 0x0600353D RID: 13629 RVA: 0x000B0CC8 File Offset: 0x000AFCC8
		public static DateTime GetCreationTime(string path)
		{
			return File.GetCreationTime(path);
		}

		// Token: 0x0600353E RID: 13630 RVA: 0x000B0CD0 File Offset: 0x000AFCD0
		public static DateTime GetCreationTimeUtc(string path)
		{
			return File.GetCreationTimeUtc(path);
		}

		// Token: 0x0600353F RID: 13631 RVA: 0x000B0CD8 File Offset: 0x000AFCD8
		public static void SetLastWriteTime(string path, DateTime lastWriteTime)
		{
			Directory.SetLastWriteTimeUtc(path, lastWriteTime.ToUniversalTime());
		}

		// Token: 0x06003540 RID: 13632 RVA: 0x000B0CE8 File Offset: 0x000AFCE8
		public unsafe static void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
		{
			if ((Environment.OSInfo & Environment.OSName.WinNT) == Environment.OSName.WinNT)
			{
				using (SafeFileHandle safeFileHandle = Directory.OpenHandle(path))
				{
					Win32Native.FILE_TIME file_TIME = new Win32Native.FILE_TIME(lastWriteTimeUtc.ToFileTimeUtc());
					if (!Win32Native.SetFileTime(safeFileHandle, null, null, &file_TIME))
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						__Error.WinIOError(lastWin32Error, path);
					}
				}
			}
		}

		// Token: 0x06003541 RID: 13633 RVA: 0x000B0D5C File Offset: 0x000AFD5C
		public static DateTime GetLastWriteTime(string path)
		{
			return File.GetLastWriteTime(path);
		}

		// Token: 0x06003542 RID: 13634 RVA: 0x000B0D64 File Offset: 0x000AFD64
		public static DateTime GetLastWriteTimeUtc(string path)
		{
			return File.GetLastWriteTimeUtc(path);
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x000B0D6C File Offset: 0x000AFD6C
		public static void SetLastAccessTime(string path, DateTime lastAccessTime)
		{
			Directory.SetLastAccessTimeUtc(path, lastAccessTime.ToUniversalTime());
		}

		// Token: 0x06003544 RID: 13636 RVA: 0x000B0D7C File Offset: 0x000AFD7C
		public unsafe static void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
		{
			if ((Environment.OSInfo & Environment.OSName.WinNT) == Environment.OSName.WinNT)
			{
				using (SafeFileHandle safeFileHandle = Directory.OpenHandle(path))
				{
					Win32Native.FILE_TIME file_TIME = new Win32Native.FILE_TIME(lastAccessTimeUtc.ToFileTimeUtc());
					if (!Win32Native.SetFileTime(safeFileHandle, null, &file_TIME, null))
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						__Error.WinIOError(lastWin32Error, path);
					}
				}
			}
		}

		// Token: 0x06003545 RID: 13637 RVA: 0x000B0DF0 File Offset: 0x000AFDF0
		public static DateTime GetLastAccessTime(string path)
		{
			return File.GetLastAccessTime(path);
		}

		// Token: 0x06003546 RID: 13638 RVA: 0x000B0DF8 File Offset: 0x000AFDF8
		public static DateTime GetLastAccessTimeUtc(string path)
		{
			return File.GetLastAccessTimeUtc(path);
		}

		// Token: 0x06003547 RID: 13639 RVA: 0x000B0E00 File Offset: 0x000AFE00
		public static DirectorySecurity GetAccessControl(string path)
		{
			return new DirectorySecurity(path, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		// Token: 0x06003548 RID: 13640 RVA: 0x000B0E0A File Offset: 0x000AFE0A
		public static DirectorySecurity GetAccessControl(string path, AccessControlSections includeSections)
		{
			return new DirectorySecurity(path, includeSections);
		}

		// Token: 0x06003549 RID: 13641 RVA: 0x000B0E14 File Offset: 0x000AFE14
		public static void SetAccessControl(string path, DirectorySecurity directorySecurity)
		{
			if (directorySecurity == null)
			{
				throw new ArgumentNullException("directorySecurity");
			}
			string fullPathInternal = Path.GetFullPathInternal(path);
			directorySecurity.Persist(fullPathInternal);
		}

		// Token: 0x0600354A RID: 13642 RVA: 0x000B0E3D File Offset: 0x000AFE3D
		public static string[] GetFiles(string path)
		{
			return Directory.GetFiles(path, "*");
		}

		// Token: 0x0600354B RID: 13643 RVA: 0x000B0E4A File Offset: 0x000AFE4A
		public static string[] GetFiles(string path, string searchPattern)
		{
			return Directory.GetFiles(path, searchPattern, SearchOption.TopDirectoryOnly);
		}

		// Token: 0x0600354C RID: 13644 RVA: 0x000B0E54 File Offset: 0x000AFE54
		public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			return Directory.InternalGetFileDirectoryNames(path, path, searchPattern, true, false, searchOption);
		}

		// Token: 0x0600354D RID: 13645 RVA: 0x000B0E7D File Offset: 0x000AFE7D
		public static string[] GetDirectories(string path)
		{
			return Directory.GetDirectories(path, "*");
		}

		// Token: 0x0600354E RID: 13646 RVA: 0x000B0E8A File Offset: 0x000AFE8A
		public static string[] GetDirectories(string path, string searchPattern)
		{
			return Directory.GetDirectories(path, searchPattern, SearchOption.TopDirectoryOnly);
		}

		// Token: 0x0600354F RID: 13647 RVA: 0x000B0E94 File Offset: 0x000AFE94
		public static string[] GetDirectories(string path, string searchPattern, SearchOption searchOption)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			return Directory.InternalGetFileDirectoryNames(path, path, searchPattern, false, true, searchOption);
		}

		// Token: 0x06003550 RID: 13648 RVA: 0x000B0EBD File Offset: 0x000AFEBD
		public static string[] GetFileSystemEntries(string path)
		{
			return Directory.GetFileSystemEntries(path, "*");
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x000B0ECA File Offset: 0x000AFECA
		public static string[] GetFileSystemEntries(string path, string searchPattern)
		{
			return Directory.GetFileSystemEntries(path, searchPattern, SearchOption.TopDirectoryOnly);
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x000B0ED4 File Offset: 0x000AFED4
		private static string[] GetFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			return Directory.InternalGetFileDirectoryNames(path, path, searchPattern, true, true, searchOption);
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x000B0F00 File Offset: 0x000AFF00
		internal static string[] InternalGetFileDirectoryNames(string path, string userPathOriginal, string searchPattern, bool includeFiles, bool includeDirs, SearchOption searchOption)
		{
			int num = 0;
			if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
			{
				throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
			}
			searchPattern = searchPattern.TrimEnd(new char[0]);
			if (searchPattern.Length == 0)
			{
				return new string[0];
			}
			Path.CheckSearchPattern(searchPattern);
			string text = Path.GetFullPathInternal(path);
			string[] array = new string[]
			{
				Directory.GetDemandDir(text, true)
			};
			new FileIOPermission(FileIOPermissionAccess.PathDiscovery, array, false, false).Demand();
			string text2 = userPathOriginal;
			string directoryName = Path.GetDirectoryName(searchPattern);
			if (directoryName != null && directoryName.Length != 0)
			{
				array = new string[]
				{
					Directory.GetDemandDir(Path.InternalCombine(text, directoryName), true)
				};
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, array, false, false).Demand();
				text2 = Path.Combine(text2, directoryName);
			}
			string text3 = Path.InternalCombine(text, searchPattern);
			char c = text3[text3.Length - 1];
			if (c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar || c == Path.VolumeSeparatorChar)
			{
				text3 += '*';
			}
			text = Path.GetDirectoryName(text3);
			bool flag = false;
			bool flag2 = false;
			c = text[text.Length - 1];
			flag = (c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar);
			string text4;
			if (flag)
			{
				text4 = text3.Substring(text.Length);
			}
			else
			{
				text4 = text3.Substring(text.Length + 1);
			}
			Win32Native.WIN32_FIND_DATA win32_FIND_DATA = new Win32Native.WIN32_FIND_DATA();
			SafeFindHandle safeFindHandle = null;
			Directory.SearchData searchData = new Directory.SearchData(text, text2, searchOption);
			List<Directory.SearchData> list = new List<Directory.SearchData>();
			list.Add(searchData);
			List<string> list2 = new List<string>();
			int num2 = 0;
			string[] array2 = new string[10];
			int errorMode = Win32Native.SetErrorMode(1);
			try
			{
				while (list.Count > 0)
				{
					searchData = list[list.Count - 1];
					list.RemoveAt(list.Count - 1);
					c = searchData.fullPath[searchData.fullPath.Length - 1];
					flag = (c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar);
					if (searchData.userPath.Length > 0)
					{
						c = searchData.userPath[searchData.userPath.Length - 1];
						flag2 = (c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar);
					}
					if (searchData.searchOption != SearchOption.TopDirectoryOnly)
					{
						try
						{
							string fileName;
							if (flag)
							{
								fileName = searchData.fullPath + "*";
							}
							else
							{
								fileName = searchData.fullPath + Path.DirectorySeparatorChar + "*";
							}
							safeFindHandle = Win32Native.FindFirstFile(fileName, win32_FIND_DATA);
							if (safeFindHandle.IsInvalid)
							{
								num = Marshal.GetLastWin32Error();
								if (num == 2)
								{
									continue;
								}
								__Error.WinIOError(num, searchData.fullPath);
							}
							do
							{
								if ((win32_FIND_DATA.dwFileAttributes & 16) != 0 && !win32_FIND_DATA.cFileName.Equals(".") && !win32_FIND_DATA.cFileName.Equals(".."))
								{
									Directory.SearchData searchData2 = new Directory.SearchData();
									StringBuilder stringBuilder = new StringBuilder(searchData.fullPath);
									if (!flag)
									{
										stringBuilder.Append(Path.DirectorySeparatorChar);
									}
									stringBuilder.Append(win32_FIND_DATA.cFileName);
									searchData2.fullPath = stringBuilder.ToString();
									stringBuilder.Length = 0;
									stringBuilder.Append(searchData.userPath);
									if (!flag2)
									{
										stringBuilder.Append(Path.DirectorySeparatorChar);
									}
									stringBuilder.Append(win32_FIND_DATA.cFileName);
									searchData2.userPath = stringBuilder.ToString();
									searchData2.searchOption = searchData.searchOption;
									list.Add(searchData2);
								}
							}
							while (Win32Native.FindNextFile(safeFindHandle, win32_FIND_DATA));
						}
						finally
						{
							if (safeFindHandle != null)
							{
								safeFindHandle.Dispose();
							}
						}
					}
					try
					{
						string fileName;
						if (flag)
						{
							fileName = searchData.fullPath + text4;
						}
						else
						{
							fileName = searchData.fullPath + Path.DirectorySeparatorChar + text4;
						}
						safeFindHandle = Win32Native.FindFirstFile(fileName, win32_FIND_DATA);
						if (safeFindHandle.IsInvalid)
						{
							num = Marshal.GetLastWin32Error();
							if (num == 2)
							{
								continue;
							}
							__Error.WinIOError(num, searchData.fullPath);
						}
						int num3 = 0;
						do
						{
							bool flag3 = false;
							if (includeFiles)
							{
								flag3 = (0 == (win32_FIND_DATA.dwFileAttributes & 16));
							}
							if (includeDirs && (win32_FIND_DATA.dwFileAttributes & 16) != 0 && !win32_FIND_DATA.cFileName.Equals(".") && !win32_FIND_DATA.cFileName.Equals(".."))
							{
								flag3 = true;
							}
							if (flag3)
							{
								num3++;
								if (num2 == array2.Length)
								{
									string[] array3 = new string[array2.Length * 2];
									Array.Copy(array2, 0, array3, 0, num2);
									array2 = array3;
								}
								array2[num2++] = Path.InternalCombine(searchData.userPath, win32_FIND_DATA.cFileName);
							}
						}
						while (Win32Native.FindNextFile(safeFindHandle, win32_FIND_DATA));
						num = Marshal.GetLastWin32Error();
						if (num3 > 0)
						{
							list2.Add(Directory.GetDemandDir(searchData.fullPath, true));
						}
					}
					finally
					{
						if (safeFindHandle != null)
						{
							safeFindHandle.Dispose();
						}
					}
				}
			}
			finally
			{
				Win32Native.SetErrorMode(errorMode);
			}
			if (num != 0 && num != 18 && num != 2)
			{
				__Error.WinIOError(num, searchData.fullPath);
			}
			if (list2.Count > 0)
			{
				array = new string[list2.Count];
				list2.CopyTo(array, 0);
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, array, false, false).Demand();
			}
			if (num2 == array2.Length)
			{
				return array2;
			}
			string[] array4 = new string[num2];
			Array.Copy(array2, 0, array4, 0, num2);
			return array4;
		}

		// Token: 0x06003554 RID: 13652 RVA: 0x000B14C0 File Offset: 0x000B04C0
		public static string[] GetLogicalDrives()
		{
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			int logicalDrives = Win32Native.GetLogicalDrives();
			if (logicalDrives == 0)
			{
				__Error.WinIOError();
			}
			uint num = (uint)logicalDrives;
			int num2 = 0;
			while (num != 0U)
			{
				if ((num & 1U) != 0U)
				{
					num2++;
				}
				num >>= 1;
			}
			string[] array = new string[num2];
			char[] array2 = new char[]
			{
				'A',
				':',
				'\\'
			};
			num = (uint)logicalDrives;
			num2 = 0;
			while (num != 0U)
			{
				if ((num & 1U) != 0U)
				{
					array[num2++] = new string(array2);
				}
				num >>= 1;
				char[] array3 = array2;
				int num3 = 0;
				array3[num3] += '\u0001';
			}
			return array;
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x000B1550 File Offset: 0x000B0550
		public static string GetDirectoryRoot(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			string fullPathInternal = Path.GetFullPathInternal(path);
			string text = fullPathInternal.Substring(0, Path.GetRootLength(fullPathInternal));
			string demandDir = Directory.GetDemandDir(text, true);
			new FileIOPermission(FileIOPermissionAccess.PathDiscovery, new string[]
			{
				demandDir
			}, false, false).Demand();
			return text;
		}

		// Token: 0x06003556 RID: 13654 RVA: 0x000B15A2 File Offset: 0x000B05A2
		internal static string InternalGetDirectoryRoot(string path)
		{
			if (path == null)
			{
				return null;
			}
			return path.Substring(0, Path.GetRootLength(path));
		}

		// Token: 0x06003557 RID: 13655 RVA: 0x000B15B8 File Offset: 0x000B05B8
		public static string GetCurrentDirectory()
		{
			StringBuilder stringBuilder = new StringBuilder(261);
			if (Win32Native.GetCurrentDirectory(stringBuilder.Capacity, stringBuilder) == 0)
			{
				__Error.WinIOError();
			}
			string text = stringBuilder.ToString();
			if (text.IndexOf('~') >= 0)
			{
				int longPathName = Win32Native.GetLongPathName(text, stringBuilder, stringBuilder.Capacity);
				if (longPathName == 0 || longPathName >= 260)
				{
					int num = Marshal.GetLastWin32Error();
					if (longPathName >= 260)
					{
						num = 206;
					}
					if (num != 2 && num != 3 && num != 1 && num != 5)
					{
						__Error.WinIOError(num, string.Empty);
					}
				}
				text = stringBuilder.ToString();
			}
			string demandDir = Directory.GetDemandDir(text, true);
			new FileIOPermission(FileIOPermissionAccess.PathDiscovery, new string[]
			{
				demandDir
			}, false, false).Demand();
			return text;
		}

		// Token: 0x06003558 RID: 13656 RVA: 0x000B166C File Offset: 0x000B066C
		public static void SetCurrentDirectory(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("value");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_PathEmpty"));
			}
			if (path.Length >= 260)
			{
				throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
			}
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			string fullPathInternal = Path.GetFullPathInternal(path);
			if (Environment.IsWin9X() && !Directory.InternalExists(Path.GetPathRoot(fullPathInternal)))
			{
				throw new DirectoryNotFoundException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("IO.PathNotFound_Path"), new object[]
				{
					path
				}));
			}
			if (!Win32Native.SetCurrentDirectory(fullPathInternal))
			{
				int num = Marshal.GetLastWin32Error();
				if (num == 2)
				{
					num = 3;
				}
				__Error.WinIOError(num, fullPathInternal);
			}
		}

		// Token: 0x06003559 RID: 13657 RVA: 0x000B1724 File Offset: 0x000B0724
		public static void Move(string sourceDirName, string destDirName)
		{
			if (sourceDirName == null)
			{
				throw new ArgumentNullException("sourceDirName");
			}
			if (sourceDirName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "sourceDirName");
			}
			if (destDirName == null)
			{
				throw new ArgumentNullException("destDirName");
			}
			if (destDirName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "destDirName");
			}
			string fullPathInternal = Path.GetFullPathInternal(sourceDirName);
			string demandDir = Directory.GetDemandDir(fullPathInternal, false);
			if (demandDir.Length >= 249)
			{
				throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
			}
			string fullPathInternal2 = Path.GetFullPathInternal(destDirName);
			string demandDir2 = Directory.GetDemandDir(fullPathInternal2, false);
			if (demandDir2.Length >= 249)
			{
				throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
			}
			new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, new string[]
			{
				demandDir
			}, false, false).Demand();
			new FileIOPermission(FileIOPermissionAccess.Write, new string[]
			{
				demandDir2
			}, false, false).Demand();
			if (CultureInfo.InvariantCulture.CompareInfo.Compare(demandDir, demandDir2, CompareOptions.IgnoreCase) == 0)
			{
				throw new IOException(Environment.GetResourceString("IO.IO_SourceDestMustBeDifferent"));
			}
			string pathRoot = Path.GetPathRoot(demandDir);
			string pathRoot2 = Path.GetPathRoot(demandDir2);
			if (CultureInfo.InvariantCulture.CompareInfo.Compare(pathRoot, pathRoot2, CompareOptions.IgnoreCase) != 0)
			{
				throw new IOException(Environment.GetResourceString("IO.IO_SourceDestMustHaveSameRoot"));
			}
			if (Environment.IsWin9X() && !Directory.InternalExists(Path.GetPathRoot(fullPathInternal2)))
			{
				throw new DirectoryNotFoundException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("IO.PathNotFound_Path"), new object[]
				{
					destDirName
				}));
			}
			if (!Win32Native.MoveFile(sourceDirName, destDirName))
			{
				int num = Marshal.GetLastWin32Error();
				if (num == 2)
				{
					num = 3;
					__Error.WinIOError(num, fullPathInternal);
				}
				if (num == 5)
				{
					throw new IOException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("UnauthorizedAccess_IODenied_Path"), new object[]
					{
						sourceDirName
					}), Win32Native.MakeHRFromErrorCode(num));
				}
				__Error.WinIOError(num, string.Empty);
			}
		}

		// Token: 0x0600355A RID: 13658 RVA: 0x000B1910 File Offset: 0x000B0910
		public static void Delete(string path)
		{
			string fullPathInternal = Path.GetFullPathInternal(path);
			Directory.Delete(fullPathInternal, path, false);
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x000B192C File Offset: 0x000B092C
		public static void Delete(string path, bool recursive)
		{
			string fullPathInternal = Path.GetFullPathInternal(path);
			Directory.Delete(fullPathInternal, path, recursive);
		}

		// Token: 0x0600355C RID: 13660 RVA: 0x000B1948 File Offset: 0x000B0948
		internal static void Delete(string fullPath, string userPath, bool recursive)
		{
			string demandDir = Directory.GetDemandDir(fullPath, !recursive);
			new FileIOPermission(FileIOPermissionAccess.Write, new string[]
			{
				demandDir
			}, false, false).Demand();
			Win32Native.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Win32Native.WIN32_FILE_ATTRIBUTE_DATA);
			int num = File.FillAttributeInfo(fullPath, ref win32_FILE_ATTRIBUTE_DATA, false, true);
			if (num != 0)
			{
				if (num == 2)
				{
					num = 3;
				}
				__Error.WinIOError(num, fullPath);
			}
			if ((win32_FILE_ATTRIBUTE_DATA.fileAttributes & 1024) != 0)
			{
				recursive = false;
			}
			Directory.DeleteHelper(fullPath, userPath, recursive);
		}

		// Token: 0x0600355D RID: 13661 RVA: 0x000B19B8 File Offset: 0x000B09B8
		private static void DeleteHelper(string fullPath, string userPath, bool recursive)
		{
			Exception ex = null;
			if (recursive)
			{
				Win32Native.WIN32_FIND_DATA win32_FIND_DATA = new Win32Native.WIN32_FIND_DATA();
				int num;
				using (SafeFindHandle safeFindHandle = Win32Native.FindFirstFile(fullPath + Path.DirectorySeparatorChar + "*", win32_FIND_DATA))
				{
					if (safeFindHandle.IsInvalid)
					{
						num = Marshal.GetLastWin32Error();
						__Error.WinIOError(num, fullPath);
					}
					for (;;)
					{
						bool flag = 0 != (win32_FIND_DATA.dwFileAttributes & 16);
						if (!flag)
						{
							goto IL_15A;
						}
						if (!win32_FIND_DATA.cFileName.Equals(".") && !win32_FIND_DATA.cFileName.Equals(".."))
						{
							bool flag2 = 0 == (win32_FIND_DATA.dwFileAttributes & 1024);
							if (flag2)
							{
								string fullPath2 = Path.InternalCombine(fullPath, win32_FIND_DATA.cFileName);
								string userPath2 = Path.InternalCombine(userPath, win32_FIND_DATA.cFileName);
								try
								{
									Directory.DeleteHelper(fullPath2, userPath2, recursive);
									goto IL_191;
								}
								catch (Exception ex2)
								{
									if (ex == null)
									{
										ex = ex2;
									}
									goto IL_191;
								}
							}
							if (win32_FIND_DATA.dwReserved0 == -1610612733)
							{
								string mountPoint = Path.InternalCombine(fullPath, win32_FIND_DATA.cFileName + Path.DirectorySeparatorChar);
								if (!Win32Native.DeleteVolumeMountPoint(mountPoint))
								{
									num = Marshal.GetLastWin32Error();
									try
									{
										__Error.WinIOError(num, win32_FIND_DATA.cFileName);
									}
									catch (Exception ex3)
									{
										if (ex == null)
										{
											ex = ex3;
										}
									}
								}
							}
							string path = Path.InternalCombine(fullPath, win32_FIND_DATA.cFileName);
							if (!Win32Native.RemoveDirectory(path))
							{
								num = Marshal.GetLastWin32Error();
								try
								{
									__Error.WinIOError(num, win32_FIND_DATA.cFileName);
									goto IL_191;
								}
								catch (Exception ex4)
								{
									if (ex == null)
									{
										ex = ex4;
									}
									goto IL_191;
								}
								goto IL_15A;
							}
						}
						IL_191:
						if (!Win32Native.FindNextFile(safeFindHandle, win32_FIND_DATA))
						{
							break;
						}
						continue;
						IL_15A:
						string path2 = Path.InternalCombine(fullPath, win32_FIND_DATA.cFileName);
						if (!Win32Native.DeleteFile(path2))
						{
							num = Marshal.GetLastWin32Error();
							try
							{
								__Error.WinIOError(num, win32_FIND_DATA.cFileName);
							}
							catch (Exception ex5)
							{
								if (ex == null)
								{
									ex = ex5;
								}
							}
							goto IL_191;
						}
						goto IL_191;
					}
					num = Marshal.GetLastWin32Error();
				}
				if (ex != null)
				{
					throw ex;
				}
				if (num != 0 && num != 18)
				{
					__Error.WinIOError(num, userPath);
				}
			}
			if (!Win32Native.RemoveDirectory(fullPath))
			{
				int num = Marshal.GetLastWin32Error();
				if (num == 2)
				{
					num = 3;
				}
				if (num == 5)
				{
					throw new IOException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("UnauthorizedAccess_IODenied_Path"), new object[]
					{
						userPath
					}));
				}
				__Error.WinIOError(num, fullPath);
			}
		}

		// Token: 0x0600355E RID: 13662 RVA: 0x000B1C54 File Offset: 0x000B0C54
		private static SafeFileHandle OpenHandle(string path)
		{
			string fullPathInternal = Path.GetFullPathInternal(path);
			string pathRoot = Path.GetPathRoot(fullPathInternal);
			if (pathRoot == fullPathInternal && pathRoot[1] == Path.VolumeSeparatorChar)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_PathIsVolume"));
			}
			new FileIOPermission(FileIOPermissionAccess.Write, new string[]
			{
				Directory.GetDemandDir(fullPathInternal, true)
			}, false, false).Demand();
			SafeFileHandle safeFileHandle = Win32Native.SafeCreateFile(fullPathInternal, 1073741824, FileShare.Write | FileShare.Delete, null, FileMode.Open, 33554432, Win32Native.NULL);
			if (safeFileHandle.IsInvalid)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				__Error.WinIOError(lastWin32Error, fullPathInternal);
			}
			return safeFileHandle;
		}

		// Token: 0x04001C11 RID: 7185
		private const int FILE_ATTRIBUTE_DIRECTORY = 16;

		// Token: 0x04001C12 RID: 7186
		private const int GENERIC_WRITE = 1073741824;

		// Token: 0x04001C13 RID: 7187
		private const int FILE_SHARE_WRITE = 2;

		// Token: 0x04001C14 RID: 7188
		private const int FILE_SHARE_DELETE = 4;

		// Token: 0x04001C15 RID: 7189
		private const int OPEN_EXISTING = 3;

		// Token: 0x04001C16 RID: 7190
		private const int FILE_FLAG_BACKUP_SEMANTICS = 33554432;

		// Token: 0x020005AA RID: 1450
		private sealed class SearchData
		{
			// Token: 0x0600355F RID: 13663 RVA: 0x000B1CE7 File Offset: 0x000B0CE7
			public SearchData()
			{
			}

			// Token: 0x06003560 RID: 13664 RVA: 0x000B1CEF File Offset: 0x000B0CEF
			public SearchData(string fullPath, string userPath, SearchOption searchOption)
			{
				this.fullPath = fullPath;
				this.userPath = userPath;
				this.searchOption = searchOption;
			}

			// Token: 0x04001C17 RID: 7191
			public string fullPath;

			// Token: 0x04001C18 RID: 7192
			public string userPath;

			// Token: 0x04001C19 RID: 7193
			public SearchOption searchOption;
		}
	}
}
