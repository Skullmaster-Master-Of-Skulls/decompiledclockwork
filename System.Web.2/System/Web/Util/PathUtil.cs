using System;
using System.IO;
using System.Security.Permissions;

namespace System.Web.Util
{
	// Token: 0x020001DA RID: 474
	internal static class PathUtil
	{
		// Token: 0x0600179C RID: 6044 RVA: 0x0004A12A File Offset: 0x0004832A
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private static string GetSystem32Path()
		{
			return Environment.GetFolderPath(Environment.SpecialFolder.System);
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x0004A133 File Offset: 0x00048333
		internal static string GetSystemDllFullPath(string filename)
		{
			return Path.Combine(PathUtil._system32Path, filename);
		}

		// Token: 0x0400171E RID: 5918
		private static string _system32Path = PathUtil.GetSystem32Path();
	}
}
