using System;
using System.IO;
using System.Security;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000571 RID: 1393
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal static class IntSecurity
	{
		// Token: 0x060033D9 RID: 13273 RVA: 0x000E4308 File Offset: 0x000E2508
		public static string UnsafeGetFullPath(string fileName)
		{
			string result = fileName;
			new FileIOPermission(PermissionState.None)
			{
				AllFiles = FileIOPermissionAccess.PathDiscovery
			}.Assert();
			try
			{
				result = Path.GetFullPath(fileName);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return result;
		}

		// Token: 0x040029C7 RID: 10695
		public static readonly CodeAccessPermission UnmanagedCode = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);

		// Token: 0x040029C8 RID: 10696
		public static readonly CodeAccessPermission FullReflection = new ReflectionPermission(PermissionState.Unrestricted);
	}
}
