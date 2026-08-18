using System;
using System.IO;
using System.Runtime.Remoting;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020007A2 RID: 1954
	public sealed class ApplicationHost
	{
		// Token: 0x06005CC1 RID: 23745 RVA: 0x000030B5 File Offset: 0x000012B5
		private ApplicationHost()
		{
		}

		// Token: 0x06005CC2 RID: 23746 RVA: 0x00140B0C File Offset: 0x0013ED0C
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static object CreateApplicationHost(Type hostType, string virtualDir, string physicalDir)
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				throw new PlatformNotSupportedException(SR.GetString("RequiresNT"));
			}
			if (!StringUtil.StringEndsWith(physicalDir, Path.DirectorySeparatorChar))
			{
				physicalDir += Path.DirectorySeparatorChar.ToString();
			}
			ApplicationManager applicationManager = ApplicationManager.GetApplicationManager();
			string appId = StringUtil.GetNonRandomizedHashCode(virtualDir + physicalDir, false).ToString("x");
			ObjectHandle objectHandle = applicationManager.CreateInstanceInNewWorkerAppDomain(hostType, appId, VirtualPath.CreateNonRelative(virtualDir), physicalDir);
			return objectHandle.Unwrap();
		}
	}
}
