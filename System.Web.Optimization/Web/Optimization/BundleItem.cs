using System;
using System.Collections.Generic;

namespace System.Web.Optimization
{
	// Token: 0x02000022 RID: 34
	internal class BundleItem
	{
		// Token: 0x0600011B RID: 283 RVA: 0x00004966 File Offset: 0x00002B66
		public BundleItem(string virtualPath) : this(virtualPath, null)
		{
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004970 File Offset: 0x00002B70
		public BundleItem(string virtualPath, IEnumerable<IItemTransform> transforms)
		{
			this.VirtualPath = virtualPath;
			if (transforms != null)
			{
				this.Transforms.AddRange(transforms);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00004999 File Offset: 0x00002B99
		// (set) Token: 0x0600011E RID: 286 RVA: 0x000049A1 File Offset: 0x00002BA1
		public string VirtualPath { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600011F RID: 287 RVA: 0x000049AA File Offset: 0x00002BAA
		public List<IItemTransform> Transforms
		{
			get
			{
				return this._transforms;
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000049B2 File Offset: 0x00002BB2
		public virtual void AddFiles(List<BundleFile> files, BundleContext context)
		{
			files.Add(new BundleFile(this.VirtualPath, context.VirtualPathProvider.GetFile(this.VirtualPath), this.Transforms));
		}

		// Token: 0x0400005A RID: 90
		private List<IItemTransform> _transforms = new List<IItemTransform>();
	}
}
