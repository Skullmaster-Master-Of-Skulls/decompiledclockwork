using System;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x02000285 RID: 645
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[Serializable]
	public sealed class ApplicationInfo
	{
		// Token: 0x0600211F RID: 8479 RVA: 0x00091487 File Offset: 0x00090487
		internal ApplicationInfo(string id, VirtualPath virtualPath, string physicalPath)
		{
			this._id = id;
			this._virtualPath = virtualPath;
			this._physicalPath = physicalPath;
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06002120 RID: 8480 RVA: 0x000914A4 File Offset: 0x000904A4
		public string ID
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06002121 RID: 8481 RVA: 0x000914AC File Offset: 0x000904AC
		public string VirtualPath
		{
			get
			{
				return this._virtualPath.VirtualPathString;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06002122 RID: 8482 RVA: 0x000914B9 File Offset: 0x000904B9
		public string PhysicalPath
		{
			get
			{
				return this._physicalPath;
			}
		}

		// Token: 0x04001AF0 RID: 6896
		private string _id;

		// Token: 0x04001AF1 RID: 6897
		private VirtualPath _virtualPath;

		// Token: 0x04001AF2 RID: 6898
		private string _physicalPath;
	}
}
