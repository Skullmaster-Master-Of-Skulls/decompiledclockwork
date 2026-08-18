using System;
using System.IO;
using System.Web.Compilation;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020007D0 RID: 2000
	internal class MapPathBasedVirtualFile : VirtualFile
	{
		// Token: 0x06006011 RID: 24593 RVA: 0x0014BFE9 File Offset: 0x0014A1E9
		internal MapPathBasedVirtualFile(string virtualPath) : base(virtualPath)
		{
		}

		// Token: 0x06006012 RID: 24594 RVA: 0x0014BFF2 File Offset: 0x0014A1F2
		internal MapPathBasedVirtualFile(string virtualPath, string physicalPath, FindFileData ffd) : base(virtualPath)
		{
			this._physicalPath = physicalPath;
			this._ffd = ffd;
		}

		// Token: 0x06006013 RID: 24595 RVA: 0x0014C009 File Offset: 0x0014A209
		private void EnsureFileInfoObtained()
		{
			if (this._physicalPath == null)
			{
				this._physicalPath = HostingEnvironment.MapPathInternal(base.VirtualPath);
				FindFileData.FindFile(this._physicalPath, out this._ffd);
			}
		}

		// Token: 0x17001B7C RID: 7036
		// (get) Token: 0x06006014 RID: 24596 RVA: 0x0014C036 File Offset: 0x0014A236
		public override string Name
		{
			get
			{
				this.EnsureFileInfoObtained();
				if (this._ffd == null)
				{
					return base.Name;
				}
				return this._ffd.FileNameLong;
			}
		}

		// Token: 0x06006015 RID: 24597 RVA: 0x0014C058 File Offset: 0x0014A258
		public override Stream Open()
		{
			this.EnsureFileInfoObtained();
			TimeStampChecker.AddFile(base.VirtualPath, this._physicalPath);
			return new FileStream(this._physicalPath, FileMode.Open, FileAccess.Read, FileShare.Read);
		}

		// Token: 0x17001B7D RID: 7037
		// (get) Token: 0x06006016 RID: 24598 RVA: 0x0014C07F File Offset: 0x0014A27F
		internal string PhysicalPath
		{
			get
			{
				this.EnsureFileInfoObtained();
				return this._physicalPath;
			}
		}

		// Token: 0x04003237 RID: 12855
		private string _physicalPath;

		// Token: 0x04003238 RID: 12856
		private FindFileData _ffd;
	}
}
