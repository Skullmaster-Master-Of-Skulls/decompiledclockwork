using System;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020007E8 RID: 2024
	internal class SimpleApplicationHost : MarshalByRefObject, IApplicationHost
	{
		// Token: 0x0600608F RID: 24719 RVA: 0x0014DB9C File Offset: 0x0014BD9C
		internal SimpleApplicationHost(VirtualPath virtualPath, string physicalPath)
		{
			if (string.IsNullOrEmpty(physicalPath))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("physicalPath");
			}
			if (FileUtil.IsSuspiciousPhysicalPath(physicalPath))
			{
				throw ExceptionUtil.ParameterInvalid(physicalPath);
			}
			this._appVirtualPath = virtualPath;
			this._appPhysicalPath = (StringUtil.StringEndsWith(physicalPath, "\\") ? physicalPath : (physicalPath + "\\"));
		}

		// Token: 0x06006090 RID: 24720 RVA: 0x0000298D File Offset: 0x00000B8D
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x06006091 RID: 24721 RVA: 0x0014DBF9 File Offset: 0x0014BDF9
		public string GetVirtualPath()
		{
			return this._appVirtualPath.VirtualPathString;
		}

		// Token: 0x06006092 RID: 24722 RVA: 0x0014DC06 File Offset: 0x0014BE06
		string IApplicationHost.GetPhysicalPath()
		{
			return this._appPhysicalPath;
		}

		// Token: 0x06006093 RID: 24723 RVA: 0x0014DC0E File Offset: 0x0014BE0E
		IConfigMapPathFactory IApplicationHost.GetConfigMapPathFactory()
		{
			return new SimpleConfigMapPathFactory();
		}

		// Token: 0x06006094 RID: 24724 RVA: 0x0002E5BA File Offset: 0x0002C7BA
		IntPtr IApplicationHost.GetConfigToken()
		{
			return IntPtr.Zero;
		}

		// Token: 0x06006095 RID: 24725 RVA: 0x0014DC15 File Offset: 0x0014BE15
		string IApplicationHost.GetSiteName()
		{
			return WebConfigurationHost.DefaultSiteName;
		}

		// Token: 0x06006096 RID: 24726 RVA: 0x0014DC1C File Offset: 0x0014BE1C
		string IApplicationHost.GetSiteID()
		{
			return "1";
		}

		// Token: 0x06006097 RID: 24727 RVA: 0x00006164 File Offset: 0x00004364
		public void MessageReceived()
		{
		}

		// Token: 0x0400325F RID: 12895
		private VirtualPath _appVirtualPath;

		// Token: 0x04003260 RID: 12896
		private string _appPhysicalPath;
	}
}
