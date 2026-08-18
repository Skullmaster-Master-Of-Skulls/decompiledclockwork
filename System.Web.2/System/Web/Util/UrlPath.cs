using System;
using System.Collections;
using System.IO;
using System.Text;

namespace System.Web.Util
{
	// Token: 0x02000229 RID: 553
	internal static class UrlPath
	{
		// Token: 0x06001A4F RID: 6735 RVA: 0x000526EC File Offset: 0x000508EC
		internal static bool IsRooted(string basepath)
		{
			return string.IsNullOrEmpty(basepath) || basepath[0] == '/' || basepath[0] == '\\';
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x00052710 File Offset: 0x00050910
		private static bool HasScheme(string virtualPath)
		{
			int num = virtualPath.IndexOf(':');
			if (num == -1)
			{
				return false;
			}
			int num2 = virtualPath.IndexOf('/');
			return num2 == -1 || num < num2;
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x0005273F File Offset: 0x0005093F
		internal static bool IsRelativeUrl(string virtualPath)
		{
			return !UrlPath.HasScheme(virtualPath) && !UrlPath.IsRooted(virtualPath);
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x00052754 File Offset: 0x00050954
		internal static bool IsAppRelativePath(string path)
		{
			if (path == null)
			{
				return false;
			}
			int length = path.Length;
			return length != 0 && path[0] == '~' && (length == 1 || path[1] == '\\' || path[1] == '/');
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x0005279D File Offset: 0x0005099D
		internal static bool IsValidVirtualPathWithoutProtocol(string path)
		{
			return path != null && !UrlPath.HasScheme(path);
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x000527B0 File Offset: 0x000509B0
		internal static string GetDirectory(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentException(SR.GetString("Empty_path_has_no_directory"));
			}
			if (path[0] != '/' && path[0] != '~')
			{
				throw new ArgumentException(SR.GetString("Path_must_be_rooted", new object[]
				{
					path
				}));
			}
			if (path.Length == 1)
			{
				return path;
			}
			int num = path.LastIndexOf('/');
			if (num < 0)
			{
				throw new ArgumentException(SR.GetString("Path_must_be_rooted", new object[]
				{
					path
				}));
			}
			return path.Substring(0, num + 1);
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x00052841 File Offset: 0x00050A41
		private static bool IsDirectorySeparatorChar(char ch)
		{
			return ch == '\\' || ch == '/';
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x0005284F File Offset: 0x00050A4F
		internal static bool IsAbsolutePhysicalPath(string path)
		{
			return path != null && path.Length >= 3 && ((path[1] == ':' && UrlPath.IsDirectorySeparatorChar(path[2])) || UrlPath.IsUncSharePath(path));
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x00052880 File Offset: 0x00050A80
		internal static bool IsUncSharePath(string path)
		{
			return path.Length > 2 && UrlPath.IsDirectorySeparatorChar(path[0]) && UrlPath.IsDirectorySeparatorChar(path[1]);
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x000528AC File Offset: 0x00050AAC
		internal static void CheckValidVirtualPath(string path)
		{
			if (UrlPath.IsAbsolutePhysicalPath(path))
			{
				throw new HttpException(SR.GetString("Physical_path_not_allowed", new object[]
				{
					path
				}));
			}
			int num = path.IndexOf('?');
			if (num >= 0)
			{
				path = path.Substring(0, num);
			}
			if (UrlPath.HasScheme(path))
			{
				throw new HttpException(SR.GetString("Invalid_vpath", new object[]
				{
					path
				}));
			}
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x00052914 File Offset: 0x00050B14
		private static string Combine(string appPath, string basepath, string relative)
		{
			if (string.IsNullOrEmpty(relative))
			{
				throw new ArgumentNullException("relative");
			}
			if (string.IsNullOrEmpty(basepath))
			{
				throw new ArgumentNullException("basepath");
			}
			if (basepath[0] == '~' && basepath.Length == 1)
			{
				basepath = "~/";
			}
			else
			{
				int num = basepath.LastIndexOf('/');
				if (num < basepath.Length - 1)
				{
					basepath = basepath.Substring(0, num + 1);
				}
			}
			UrlPath.CheckValidVirtualPath(relative);
			string path;
			if (UrlPath.IsRooted(relative))
			{
				path = relative;
			}
			else
			{
				if (relative.Length == 1 && relative[0] == '~')
				{
					return appPath;
				}
				if (UrlPath.IsAppRelativePath(relative))
				{
					if (appPath.Length > 1)
					{
						path = appPath + "/" + relative.Substring(2);
					}
					else
					{
						path = "/" + relative.Substring(2);
					}
				}
				else
				{
					path = UrlPath.SimpleCombine(basepath, relative);
				}
			}
			return UrlPath.Reduce(path);
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x000529F4 File Offset: 0x00050BF4
		internal static string Combine(string basepath, string relative)
		{
			return UrlPath.Combine(HttpRuntime.AppDomainAppVirtualPathString, basepath, relative);
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x00052A02 File Offset: 0x00050C02
		internal static string SimpleCombine(string basepath, string relative)
		{
			if (UrlPath.HasTrailingSlash(basepath))
			{
				return basepath + relative;
			}
			return basepath + "/" + relative;
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x00052A20 File Offset: 0x00050C20
		internal static string Reduce(string path)
		{
			string text = null;
			if (path != null)
			{
				int num = path.IndexOf('?');
				if (num >= 0)
				{
					text = path.Substring(num);
					path = path.Substring(0, num);
				}
			}
			path = UrlPath.FixVirtualPathSlashes(path);
			path = UrlPath.ReduceVirtualPath(path);
			if (text == null)
			{
				return path;
			}
			return path + text;
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x00052A70 File Offset: 0x00050C70
		internal static string ReduceVirtualPath(string path)
		{
			int length = path.Length;
			int num = 0;
			for (;;)
			{
				num = path.IndexOf('.', num);
				if (num < 0)
				{
					break;
				}
				if ((num == 0 || path[num - 1] == '/') && (num + 1 == length || path[num + 1] == '/' || (path[num + 1] == '.' && (num + 2 == length || path[num + 2] == '/'))))
				{
					goto IL_62;
				}
				num++;
			}
			return path;
			IL_62:
			ArrayList arrayList = new ArrayList();
			StringBuilder stringBuilder = new StringBuilder();
			num = 0;
			for (;;)
			{
				int num2 = num;
				num = path.IndexOf('/', num2 + 1);
				if (num < 0)
				{
					num = length;
				}
				if (num - num2 <= 3 && (num < 1 || path[num - 1] == '.') && (num2 + 1 >= length || path[num2 + 1] == '.'))
				{
					if (num - num2 == 3)
					{
						if (arrayList.Count == 0)
						{
							break;
						}
						if (arrayList.Count == 1 && UrlPath.IsAppRelativePath(path))
						{
							goto Block_14;
						}
						stringBuilder.Length = (int)arrayList[arrayList.Count - 1];
						arrayList.RemoveRange(arrayList.Count - 1, 1);
					}
				}
				else
				{
					arrayList.Add(stringBuilder.Length);
					stringBuilder.Append(path, num2, num - num2);
				}
				if (num == length)
				{
					goto Block_15;
				}
			}
			throw new HttpException(SR.GetString("Cannot_exit_up_top_directory"));
			Block_14:
			return UrlPath.ReduceVirtualPath(UrlPath.MakeVirtualPathAppAbsolute(path));
			Block_15:
			string text = stringBuilder.ToString();
			if (text.Length == 0)
			{
				if (length > 0 && path[0] == '/')
				{
					text = "/";
				}
				else
				{
					text = ".";
				}
			}
			return text;
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x00052BF4 File Offset: 0x00050DF4
		internal static string FixVirtualPathSlashes(string virtualPath)
		{
			virtualPath = virtualPath.Replace('\\', '/');
			for (;;)
			{
				string text = virtualPath.Replace("//", "/");
				if (text == virtualPath)
				{
					break;
				}
				virtualPath = text;
			}
			return virtualPath;
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x00052C28 File Offset: 0x00050E28
		internal static string MakeRelative(string from, string to)
		{
			from = UrlPath.MakeVirtualPathAppAbsolute(from);
			to = UrlPath.MakeVirtualPathAppAbsolute(to);
			if (!UrlPath.IsRooted(from))
			{
				throw new ArgumentException(SR.GetString("Path_must_be_rooted", new object[]
				{
					from
				}));
			}
			if (!UrlPath.IsRooted(to))
			{
				throw new ArgumentException(SR.GetString("Path_must_be_rooted", new object[]
				{
					to
				}));
			}
			string str = null;
			if (to != null)
			{
				int num = to.IndexOf('?');
				if (num >= 0)
				{
					str = to.Substring(num);
					to = to.Substring(0, num);
				}
			}
			Uri uri = new Uri("file://foo" + from);
			Uri uri2 = new Uri("file://foo" + to);
			string str2;
			if (uri.Equals(uri2))
			{
				int num2 = to.LastIndexOfAny(UrlPath.s_slashChars);
				if (num2 >= 0)
				{
					if (num2 == to.Length - 1)
					{
						str2 = "./";
					}
					else
					{
						str2 = to.Substring(num2 + 1);
					}
				}
				else
				{
					str2 = to;
				}
			}
			else
			{
				str2 = uri.MakeRelative(uri2);
			}
			return str2 + str + uri2.Fragment;
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x00052D28 File Offset: 0x00050F28
		internal static string GetDirectoryOrRootName(string path)
		{
			string text = Path.GetDirectoryName(path);
			if (text == null)
			{
				text = Path.GetPathRoot(path);
			}
			return text;
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00052D48 File Offset: 0x00050F48
		internal static string GetFileName(string virtualPath)
		{
			if (virtualPath != null)
			{
				int length = virtualPath.Length;
				int num = length;
				while (--num >= 0)
				{
					char c = virtualPath[num];
					if (c == '/')
					{
						return virtualPath.Substring(num + 1, length - num - 1);
					}
				}
			}
			return virtualPath;
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x00052D88 File Offset: 0x00050F88
		internal static string GetFileNameWithoutExtension(string virtualPath)
		{
			virtualPath = UrlPath.GetFileName(virtualPath);
			if (virtualPath == null)
			{
				return null;
			}
			int length;
			if ((length = virtualPath.LastIndexOf('.')) == -1)
			{
				return virtualPath;
			}
			return virtualPath.Substring(0, length);
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x00052DBC File Offset: 0x00050FBC
		internal static string GetExtension(string virtualPath)
		{
			if (virtualPath == null)
			{
				return null;
			}
			int length = virtualPath.Length;
			int num = length;
			while (--num >= 0)
			{
				char c = virtualPath[num];
				if (c == '.')
				{
					if (num != length - 1)
					{
						return virtualPath.Substring(num, length - num);
					}
					return string.Empty;
				}
				else if (c == '/')
				{
					break;
				}
			}
			return string.Empty;
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x00052E0F File Offset: 0x0005100F
		internal static bool HasTrailingSlash(string virtualPath)
		{
			return virtualPath[virtualPath.Length - 1] == '/';
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x00052E24 File Offset: 0x00051024
		internal static string AppendSlashToPathIfNeeded(string path)
		{
			if (path == null)
			{
				return null;
			}
			int length = path.Length;
			if (length == 0)
			{
				return path;
			}
			if (path[length - 1] != '/')
			{
				path += "/";
			}
			return path;
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x00052E60 File Offset: 0x00051060
		internal static string RemoveSlashFromPathIfNeeded(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return null;
			}
			int length = path.Length;
			if (length <= 1 || path[length - 1] != '/')
			{
				return path;
			}
			return path.Substring(0, length - 1);
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x00052E9C File Offset: 0x0005109C
		private static bool VirtualPathStartsWithVirtualPath(string virtualPath1, string virtualPath2)
		{
			if (virtualPath1 == null)
			{
				throw new ArgumentNullException("virtualPath1");
			}
			if (virtualPath2 == null)
			{
				throw new ArgumentNullException("virtualPath2");
			}
			if (!StringUtil.StringStartsWithIgnoreCase(virtualPath1, virtualPath2))
			{
				return false;
			}
			int length = virtualPath2.Length;
			return virtualPath1.Length == length || length == 1 || virtualPath2[length - 1] == '/' || virtualPath1[length] == '/';
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x00052F05 File Offset: 0x00051105
		internal static bool VirtualPathStartsWithAppPath(string virtualPath)
		{
			return UrlPath.VirtualPathStartsWithVirtualPath(virtualPath, HttpRuntime.AppDomainAppVirtualPathString);
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x00052F12 File Offset: 0x00051112
		internal static string MakeVirtualPathAppRelative(string virtualPath)
		{
			return UrlPath.MakeVirtualPathAppRelative(virtualPath, HttpRuntime.AppDomainAppVirtualPathString, false);
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x00052F20 File Offset: 0x00051120
		internal static string MakeVirtualPathAppRelativeOrNull(string virtualPath)
		{
			return UrlPath.MakeVirtualPathAppRelative(virtualPath, HttpRuntime.AppDomainAppVirtualPathString, true);
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x00052F30 File Offset: 0x00051130
		internal static string MakeVirtualPathAppRelative(string virtualPath, string applicationPath, bool nullIfNotInApp)
		{
			if (virtualPath == null)
			{
				throw new ArgumentNullException("virtualPath");
			}
			int length = applicationPath.Length;
			int length2 = virtualPath.Length;
			if (length2 == length - 1 && StringUtil.StringStartsWithIgnoreCase(applicationPath, virtualPath))
			{
				return "~/";
			}
			if (!UrlPath.VirtualPathStartsWithVirtualPath(virtualPath, applicationPath))
			{
				if (nullIfNotInApp)
				{
					return null;
				}
				return virtualPath;
			}
			else
			{
				if (length2 == length)
				{
					return "~/";
				}
				if (length == 1)
				{
					return "~" + virtualPath;
				}
				return "~" + virtualPath.Substring(length - 1);
			}
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x00052FAB File Offset: 0x000511AB
		internal static string MakeVirtualPathAppAbsolute(string virtualPath)
		{
			return UrlPath.MakeVirtualPathAppAbsolute(virtualPath, HttpRuntime.AppDomainAppVirtualPathString);
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x00052FB8 File Offset: 0x000511B8
		internal static string MakeVirtualPathAppAbsolute(string virtualPath, string applicationPath)
		{
			if (virtualPath.Length == 1 && virtualPath[0] == '~')
			{
				return applicationPath;
			}
			if (virtualPath.Length >= 2 && virtualPath[0] == '~' && (virtualPath[1] == '/' || virtualPath[1] == '\\'))
			{
				if (applicationPath.Length > 1)
				{
					return applicationPath + virtualPath.Substring(2);
				}
				return "/" + virtualPath.Substring(2);
			}
			else
			{
				if (!UrlPath.IsRooted(virtualPath))
				{
					throw new ArgumentOutOfRangeException("virtualPath");
				}
				return virtualPath;
			}
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x00053044 File Offset: 0x00051244
		internal static string MakeVirtualPathAppAbsoluteReduceAndCheck(string virtualPath)
		{
			if (virtualPath == null)
			{
				throw new ArgumentNullException("virtualPath");
			}
			string text = UrlPath.Reduce(UrlPath.MakeVirtualPathAppAbsolute(virtualPath));
			if (!UrlPath.VirtualPathStartsWithAppPath(text))
			{
				throw new ArgumentException(SR.GetString("Invalid_app_VirtualPath", new object[]
				{
					virtualPath
				}));
			}
			return text;
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x00053090 File Offset: 0x00051290
		internal static bool PathEndsWithExtraSlash(string path)
		{
			if (path == null)
			{
				return false;
			}
			int length = path.Length;
			return length != 0 && path[length - 1] == '\\' && (length != 3 || path[1] != ':');
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x000530D0 File Offset: 0x000512D0
		internal static bool PathIsDriveRoot(string path)
		{
			if (path != null)
			{
				int length = path.Length;
				if (length == 3 && path[1] == ':' && path[2] == '\\')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x00053104 File Offset: 0x00051304
		internal static bool IsEqualOrSubpath(string path, string subpath)
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
			return num2 >= num && StringUtil.EqualsIgnoreCase(path, 0, subpath, 0, num) && (num2 <= num || subpath[num] == '/');
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x0005317C File Offset: 0x0005137C
		internal static bool IsPathOnSameServer(string absUriOrLocalPath, Uri currentRequestUri)
		{
			Uri uri;
			if (!Uri.TryCreate(absUriOrLocalPath, UriKind.Absolute, out uri))
			{
				return AppSettings.AllowRelaxedRelativeUrl || ((UrlPath.IsRooted(absUriOrLocalPath) || UrlPath.IsRelativeUrl(absUriOrLocalPath)) && !absUriOrLocalPath.TrimStart(new char[]
				{
					' '
				}).StartsWith("//", StringComparison.Ordinal));
			}
			return uri.IsLoopback || string.Equals(currentRequestUri.Host, uri.Host, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0400181F RID: 6175
		internal const char appRelativeCharacter = '~';

		// Token: 0x04001820 RID: 6176
		internal const string appRelativeCharacterString = "~/";

		// Token: 0x04001821 RID: 6177
		private static char[] s_slashChars = new char[]
		{
			'\\',
			'/'
		};

		// Token: 0x04001822 RID: 6178
		private const string dummyProtocolAndServer = "file://foo";
	}
}
