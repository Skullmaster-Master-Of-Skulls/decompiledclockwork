using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System.Web.Util
{
	// Token: 0x0200022A RID: 554
	internal class VersionInfo
	{
		// Token: 0x06001A74 RID: 6772 RVA: 0x000030B5 File Offset: 0x000012B5
		private VersionInfo()
		{
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x00053204 File Offset: 0x00051404
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static string GetFileVersion(string filename)
		{
			string result;
			try
			{
				FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(filename);
				result = string.Format(CultureInfo.InvariantCulture, "{0}.{1}.{2}.{3}", new object[]
				{
					versionInfo.FileMajorPart,
					versionInfo.FileMinorPart,
					versionInfo.FileBuildPart,
					versionInfo.FilePrivatePart
				});
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x00053284 File Offset: 0x00051484
		internal static string GetLoadedModuleFileName(string module)
		{
			IntPtr moduleHandle = UnsafeNativeMethods.GetModuleHandle(module);
			if (moduleHandle == IntPtr.Zero)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(256);
			if (UnsafeNativeMethods.GetModuleFileName(moduleHandle, stringBuilder, 256) == 0)
			{
				return null;
			}
			string text = stringBuilder.ToString();
			if (StringUtil.StringStartsWith(text, "\\\\?\\"))
			{
				text = text.Substring(4);
			}
			return text;
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x000532E0 File Offset: 0x000514E0
		internal static string GetLoadedModuleVersion(string module)
		{
			string loadedModuleFileName = VersionInfo.GetLoadedModuleFileName(module);
			if (loadedModuleFileName == null)
			{
				return null;
			}
			return VersionInfo.GetFileVersion(loadedModuleFileName);
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06001A78 RID: 6776 RVA: 0x000532FF File Offset: 0x000514FF
		internal static string SystemWebVersion
		{
			get
			{
				return "4.0.30319.42000";
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06001A79 RID: 6777 RVA: 0x00053308 File Offset: 0x00051508
		internal static string EngineVersion
		{
			get
			{
				if (VersionInfo._engineVersion == null)
				{
					object @lock = VersionInfo._lock;
					lock (@lock)
					{
						if (VersionInfo._engineVersion == null)
						{
							VersionInfo._engineVersion = VersionInfo.GetLoadedModuleVersion("webengine4.dll");
						}
					}
				}
				return VersionInfo._engineVersion;
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001A7A RID: 6778 RVA: 0x00053364 File Offset: 0x00051564
		internal static string ClrVersion
		{
			get
			{
				if (VersionInfo._mscoreeVersion == null)
				{
					object @lock = VersionInfo._lock;
					lock (@lock)
					{
						if (VersionInfo._mscoreeVersion == null)
						{
							VersionInfo._mscoreeVersion = RuntimeEnvironment.GetSystemVersion().Substring(1);
						}
					}
				}
				return VersionInfo._mscoreeVersion;
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001A7B RID: 6779 RVA: 0x000533C0 File Offset: 0x000515C0
		internal static string ExeName
		{
			get
			{
				if (VersionInfo._exeName == null)
				{
					object @lock = VersionInfo._lock;
					lock (@lock)
					{
						if (VersionInfo._exeName == null)
						{
							string text = VersionInfo.GetLoadedModuleFileName(null);
							if (text == null)
							{
								text = string.Empty;
							}
							int num = text.LastIndexOf('\\');
							if (num >= 0)
							{
								text = text.Substring(num + 1);
							}
							num = text.LastIndexOf('.');
							if (num >= 0)
							{
								text = text.Substring(0, num);
							}
							VersionInfo._exeName = text.ToLower(CultureInfo.InvariantCulture);
						}
					}
				}
				return VersionInfo._exeName;
			}
		}

		// Token: 0x04001823 RID: 6179
		private static string _engineVersion;

		// Token: 0x04001824 RID: 6180
		private static string _mscoreeVersion;

		// Token: 0x04001825 RID: 6181
		private static string _exeName;

		// Token: 0x04001826 RID: 6182
		private static object _lock = new object();
	}
}
