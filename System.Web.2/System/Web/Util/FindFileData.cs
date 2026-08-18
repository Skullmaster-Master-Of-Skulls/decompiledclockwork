using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Web.Util
{
	// Token: 0x02000201 RID: 513
	internal sealed class FindFileData
	{
		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001932 RID: 6450 RVA: 0x0004E174 File Offset: 0x0004C374
		internal string FileNameLong
		{
			get
			{
				return this._fileNameLong;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06001933 RID: 6451 RVA: 0x0004E17C File Offset: 0x0004C37C
		internal string FileNameShort
		{
			get
			{
				return this._fileNameShort;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06001934 RID: 6452 RVA: 0x0004E184 File Offset: 0x0004C384
		internal FileAttributesData FileAttributesData
		{
			get
			{
				return this._fileAttributesData;
			}
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x0004E18C File Offset: 0x0004C38C
		internal static int FindFile(string path, out FindFileData data)
		{
			data = null;
			path = FileUtil.RemoveTrailingDirectoryBackSlash(path);
			UnsafeNativeMethods.WIN32_FIND_DATA win32_FIND_DATA;
			IntPtr intPtr = UnsafeNativeMethods.FindFirstFile(path, out win32_FIND_DATA);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (intPtr == UnsafeNativeMethods.INVALID_HANDLE_VALUE)
			{
				return HttpException.HResultFromLastError(lastWin32Error);
			}
			UnsafeNativeMethods.FindClose(intPtr);
			data = new FindFileData(ref win32_FIND_DATA);
			return 0;
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x0004E1D8 File Offset: 0x0004C3D8
		internal static int FindFile(string fullPath, string rootDirectoryPath, out FindFileData data)
		{
			int num = FindFileData.FindFile(fullPath, out data);
			if (num != 0 || string.IsNullOrEmpty(rootDirectoryPath))
			{
				return num;
			}
			rootDirectoryPath = FileUtil.RemoveTrailingDirectoryBackSlash(rootDirectoryPath);
			string text = string.Empty;
			string text2 = string.Empty;
			string directoryName = Path.GetDirectoryName(fullPath);
			while (directoryName != null && directoryName.Length > rootDirectoryPath.Length + 1 && directoryName.IndexOf(rootDirectoryPath, StringComparison.OrdinalIgnoreCase) == 0)
			{
				UnsafeNativeMethods.WIN32_FIND_DATA win32_FIND_DATA;
				IntPtr intPtr = UnsafeNativeMethods.FindFirstFile(directoryName, out win32_FIND_DATA);
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (intPtr == UnsafeNativeMethods.INVALID_HANDLE_VALUE)
				{
					return HttpException.HResultFromLastError(lastWin32Error);
				}
				UnsafeNativeMethods.FindClose(intPtr);
				text = win32_FIND_DATA.cFileName + Path.DirectorySeparatorChar.ToString() + text;
				if (!string.IsNullOrEmpty(win32_FIND_DATA.cAlternateFileName))
				{
					text2 = win32_FIND_DATA.cAlternateFileName + Path.DirectorySeparatorChar.ToString() + text2;
				}
				else
				{
					text2 = win32_FIND_DATA.cFileName + Path.DirectorySeparatorChar.ToString() + text2;
				}
				directoryName = Path.GetDirectoryName(directoryName);
			}
			if (!string.IsNullOrEmpty(text))
			{
				data.PrependRelativePath(text, text2);
			}
			return num;
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x0004E2E8 File Offset: 0x0004C4E8
		internal FindFileData(ref UnsafeNativeMethods.WIN32_FIND_DATA wfd)
		{
			this._fileAttributesData = new FileAttributesData(ref wfd);
			this._fileNameLong = wfd.cFileName;
			if (wfd.cAlternateFileName != null && wfd.cAlternateFileName.Length > 0 && !StringUtil.EqualsIgnoreCase(wfd.cFileName, wfd.cAlternateFileName))
			{
				this._fileNameShort = wfd.cAlternateFileName;
			}
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x0004E348 File Offset: 0x0004C548
		private void PrependRelativePath(string relativePathLong, string relativePathShort)
		{
			this._fileNameLong = relativePathLong + this._fileNameLong;
			string str = string.IsNullOrEmpty(this._fileNameShort) ? this._fileNameLong : this._fileNameShort;
			this._fileNameShort = relativePathShort + str;
			if (StringUtil.EqualsIgnoreCase(this._fileNameShort, this._fileNameLong))
			{
				this._fileNameShort = null;
			}
		}

		// Token: 0x040017B3 RID: 6067
		private FileAttributesData _fileAttributesData;

		// Token: 0x040017B4 RID: 6068
		private string _fileNameLong;

		// Token: 0x040017B5 RID: 6069
		private string _fileNameShort;
	}
}
