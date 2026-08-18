using System;
using System.Collections.Generic;
using System.Web.Hosting;

namespace System.Web.Optimization
{
	// Token: 0x02000036 RID: 54
	internal sealed class VirtualFileComparer : IEqualityComparer<VirtualFile>, IComparer<VirtualFile>
	{
		// Token: 0x06000182 RID: 386 RVA: 0x00005F4F File Offset: 0x0000414F
		private VirtualFileComparer()
		{
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005F57 File Offset: 0x00004157
		public bool Equals(VirtualFile x, VirtualFile y)
		{
			if (x == null)
			{
				throw new ArgumentNullException("x");
			}
			if (y == null)
			{
				throw new ArgumentNullException("y");
			}
			return string.Equals(x.VirtualPath, y.VirtualPath, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00005F87 File Offset: 0x00004187
		public int GetHashCode(VirtualFile obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			return obj.VirtualPath.GetHashCode();
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00005FA2 File Offset: 0x000041A2
		public int Compare(VirtualFile x, VirtualFile y)
		{
			if (x == null)
			{
				throw new ArgumentNullException("x");
			}
			if (y == null)
			{
				throw new ArgumentNullException("y");
			}
			return string.Compare(x.VirtualPath, y.VirtualPath, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0400007E RID: 126
		internal static readonly VirtualFileComparer Instance = new VirtualFileComparer();
	}
}
