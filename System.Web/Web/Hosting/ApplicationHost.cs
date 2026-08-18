using System;
using System.IO;
using System.Runtime.Remoting;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x02000284 RID: 644
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class ApplicationHost
	{
		// Token: 0x0600211D RID: 8477 RVA: 0x00091400 File Offset: 0x00090400
		private ApplicationHost()
		{
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x00091408 File Offset: 0x00090408
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static object CreateApplicationHost(Type hostType, string virtualDir, string physicalDir)
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				throw new PlatformNotSupportedException(SR.GetString("RequiresNT"));
			}
			if (!StringUtil.StringEndsWith(physicalDir, Path.DirectorySeparatorChar))
			{
				physicalDir += Path.DirectorySeparatorChar;
			}
			ApplicationManager applicationManager = ApplicationManager.GetApplicationManager();
			string appId = (virtualDir + physicalDir).GetHashCode().ToString("x");
			ObjectHandle objectHandle = applicationManager.CreateInstanceInNewWorkerAppDomain(hostType, appId, VirtualPath.CreateNonRelative(virtualDir), physicalDir);
			return objectHandle.Unwrap();
		}
	}
}
