using System;
using System.IO;

namespace System.Web.Hosting
{
	// Token: 0x020007ED RID: 2029
	public abstract class VirtualFile : VirtualFileBase
	{
		// Token: 0x060060E1 RID: 24801 RVA: 0x0014E353 File Offset: 0x0014C553
		protected VirtualFile(string virtualPath)
		{
			this._virtualPath = System.Web.VirtualPath.Create(virtualPath);
		}

		// Token: 0x17001B90 RID: 7056
		// (get) Token: 0x060060E2 RID: 24802 RVA: 0x00007722 File Offset: 0x00005922
		public override bool IsDirectory
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060060E3 RID: 24803
		public abstract Stream Open();
	}
}
