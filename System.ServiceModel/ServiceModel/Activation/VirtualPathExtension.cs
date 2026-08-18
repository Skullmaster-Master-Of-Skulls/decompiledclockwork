using System;

namespace System.ServiceModel.Activation
{
	// Token: 0x020005C3 RID: 1475
	public sealed class VirtualPathExtension : IExtension<ServiceHostBase>
	{
		// Token: 0x06003984 RID: 14724 RVA: 0x000DE6F2 File Offset: 0x000DC8F2
		internal VirtualPathExtension(string virtualPath, string applicationVirtualPath, string siteName)
		{
			this.VirtualPath = virtualPath;
			this.ApplicationVirtualPath = applicationVirtualPath;
			this.SiteName = siteName;
		}

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x06003985 RID: 14725 RVA: 0x000DE70F File Offset: 0x000DC90F
		// (set) Token: 0x06003986 RID: 14726 RVA: 0x000DE717 File Offset: 0x000DC917
		public string ApplicationVirtualPath { get; private set; }

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x06003987 RID: 14727 RVA: 0x000DE720 File Offset: 0x000DC920
		// (set) Token: 0x06003988 RID: 14728 RVA: 0x000DE728 File Offset: 0x000DC928
		public string SiteName { get; private set; }

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x06003989 RID: 14729 RVA: 0x000DE731 File Offset: 0x000DC931
		// (set) Token: 0x0600398A RID: 14730 RVA: 0x000DE739 File Offset: 0x000DC939
		public string VirtualPath { get; private set; }

		// Token: 0x0600398B RID: 14731 RVA: 0x000DE742 File Offset: 0x000DC942
		public void Attach(ServiceHostBase owner)
		{
		}

		// Token: 0x0600398C RID: 14732 RVA: 0x000DE744 File Offset: 0x000DC944
		public void Detach(ServiceHostBase owner)
		{
			throw FxTrace.Exception.AsError(new InvalidOperationException(SR.GetString("Hosting_VirtualPathExtenstionCanNotBeDetached")));
		}
	}
}
