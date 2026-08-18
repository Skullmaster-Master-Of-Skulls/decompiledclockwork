using System;
using System.IO;
using System.Web.Compilation;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020002AC RID: 684
	internal class MapPathBasedVirtualFile : VirtualFile
	{
		// Token: 0x060023DC RID: 9180 RVA: 0x00099FF2 File Offset: 0x00098FF2
		internal MapPathBasedVirtualFile(string virtualPath) : base(virtualPath)
		{
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x00099FFB File Offset: 0x00098FFB
		internal MapPathBasedVirtualFile(string virtualPath, string physicalPath, FindFileData ffd) : base(virtualPath)
		{
			this._physicalPath = physicalPath;
			this._ffd = ffd;
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x0009A012 File Offset: 0x00099012
		private void EnsureFileInfoObtained()
		{
			if (this._physicalPath == null)
			{
				this._physicalPath = HostingEnvironment.MapPathInternal(base.VirtualPath);
				FindFileData.FindFile(this._physicalPath, out this._ffd);
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x060023DF RID: 9183 RVA: 0x0009A03F File Offset: 0x0009903F
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

		// Token: 0x060023E0 RID: 9184 RVA: 0x0009A061 File Offset: 0x00099061
		public override Stream Open()
		{
			this.EnsureFileInfoObtained();
			TimeStampChecker.AddFile(base.VirtualPath, this._physicalPath);
			return new FileStream(this._physicalPath, FileMode.Open, FileAccess.Read, FileShare.Read);
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x060023E1 RID: 9185 RVA: 0x0009A088 File Offset: 0x00099088
		internal string PhysicalPath
		{
			get
			{
				this.EnsureFileInfoObtained();
				return this._physicalPath;
			}
		}

		// Token: 0x04001C1D RID: 7197
		private string _physicalPath;

		// Token: 0x04001C1E RID: 7198
		private FindFileData _ffd;
	}
}
