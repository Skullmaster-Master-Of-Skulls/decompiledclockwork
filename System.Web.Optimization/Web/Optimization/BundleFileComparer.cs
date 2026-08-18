using System;
using System.Collections.Generic;

namespace System.Web.Optimization
{
	// Token: 0x02000003 RID: 3
	internal sealed class BundleFileComparer : IEqualityComparer<BundleFile>, IComparer<BundleFile>
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000021E0 File Offset: 0x000003E0
		private BundleFileComparer()
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021E8 File Offset: 0x000003E8
		public bool Equals(BundleFile x, BundleFile y)
		{
			if (x == null)
			{
				throw new ArgumentNullException("x");
			}
			if (y == null)
			{
				throw new ArgumentNullException("y");
			}
			return string.Equals(x.VirtualFile.VirtualPath, y.VirtualFile.VirtualPath, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002222 File Offset: 0x00000422
		public int GetHashCode(BundleFile obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			return obj.VirtualFile.VirtualPath.GetHashCode();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002242 File Offset: 0x00000442
		public int Compare(BundleFile x, BundleFile y)
		{
			if (x == null)
			{
				throw new ArgumentNullException("x");
			}
			if (y == null)
			{
				throw new ArgumentNullException("y");
			}
			return string.Compare(x.VirtualFile.VirtualPath, y.VirtualFile.VirtualPath, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x04000004 RID: 4
		internal static readonly BundleFileComparer Instance = new BundleFileComparer();
	}
}
