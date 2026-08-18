using System;
using System.Collections;

namespace System.Web.Hosting
{
	// Token: 0x020007EE RID: 2030
	public abstract class VirtualDirectory : VirtualFileBase
	{
		// Token: 0x060060E4 RID: 24804 RVA: 0x0014E367 File Offset: 0x0014C567
		protected VirtualDirectory(string virtualPath)
		{
			this._virtualPath = System.Web.VirtualPath.CreateTrailingSlash(virtualPath);
		}

		// Token: 0x17001B91 RID: 7057
		// (get) Token: 0x060060E5 RID: 24805 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool IsDirectory
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001B92 RID: 7058
		// (get) Token: 0x060060E6 RID: 24806
		public abstract IEnumerable Directories { get; }

		// Token: 0x17001B93 RID: 7059
		// (get) Token: 0x060060E7 RID: 24807
		public abstract IEnumerable Files { get; }

		// Token: 0x17001B94 RID: 7060
		// (get) Token: 0x060060E8 RID: 24808
		public abstract IEnumerable Children { get; }
	}
}
