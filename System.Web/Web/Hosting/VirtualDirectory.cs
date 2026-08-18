using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x020002AD RID: 685
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class VirtualDirectory : VirtualFileBase
	{
		// Token: 0x060023E2 RID: 9186 RVA: 0x0009A096 File Offset: 0x00099096
		protected VirtualDirectory(string virtualPath)
		{
			this._virtualPath = System.Web.VirtualPath.CreateTrailingSlash(virtualPath);
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x060023E3 RID: 9187 RVA: 0x0009A0AA File Offset: 0x000990AA
		public override bool IsDirectory
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x060023E4 RID: 9188
		public abstract IEnumerable Directories { get; }

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x060023E5 RID: 9189
		public abstract IEnumerable Files { get; }

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x060023E6 RID: 9190
		public abstract IEnumerable Children { get; }
	}
}
