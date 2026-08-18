using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;

namespace System.Web.Optimization
{
	// Token: 0x02000002 RID: 2
	public class BundleFile
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public BundleFile(string includedVirtualPath, VirtualFile file, IList<IItemTransform> transforms)
		{
			this.VirtualFile = file;
			if (transforms != null)
			{
				this._transforms.AddRange(transforms);
			}
			this.IncludedVirtualPath = includedVirtualPath;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002100 File Offset: 0x00000300
		public BundleFile(string includedVirtualPath, VirtualFile file) : this(includedVirtualPath, file, null)
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000210B File Offset: 0x0000030B
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002113 File Offset: 0x00000313
		public VirtualFile VirtualFile
		{
			get
			{
				return this._virtualFile;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._virtualFile = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000212A File Offset: 0x0000032A
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002132 File Offset: 0x00000332
		public string IncludedVirtualPath { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000213B File Offset: 0x0000033B
		public IList<IItemTransform> Transforms
		{
			get
			{
				return this._transforms;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002144 File Offset: 0x00000344
		public string ApplyTransforms()
		{
			string text;
			using (StreamReader streamReader = new StreamReader(this.VirtualFile.Open()))
			{
				text = streamReader.ReadToEnd();
			}
			if (this.Transforms != null && this.Transforms.Count > 0)
			{
				foreach (IItemTransform itemTransform in this.Transforms)
				{
					text = itemTransform.Process(this.IncludedVirtualPath, text);
				}
			}
			return text;
		}

		// Token: 0x04000001 RID: 1
		private List<IItemTransform> _transforms = new List<IItemTransform>();

		// Token: 0x04000002 RID: 2
		private VirtualFile _virtualFile;
	}
}
