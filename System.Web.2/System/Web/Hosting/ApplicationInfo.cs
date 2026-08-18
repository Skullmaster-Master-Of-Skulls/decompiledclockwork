using System;

namespace System.Web.Hosting
{
	// Token: 0x020007A3 RID: 1955
	[Serializable]
	public sealed class ApplicationInfo
	{
		// Token: 0x06005CC3 RID: 23747 RVA: 0x00140B90 File Offset: 0x0013ED90
		internal ApplicationInfo(string id, VirtualPath virtualPath, string physicalPath)
		{
			this._id = id;
			this._virtualPath = virtualPath;
			this._physicalPath = physicalPath;
		}

		// Token: 0x17001B14 RID: 6932
		// (get) Token: 0x06005CC4 RID: 23748 RVA: 0x00140BAD File Offset: 0x0013EDAD
		public string ID
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x17001B15 RID: 6933
		// (get) Token: 0x06005CC5 RID: 23749 RVA: 0x00140BB5 File Offset: 0x0013EDB5
		public string VirtualPath
		{
			get
			{
				return this._virtualPath.VirtualPathString;
			}
		}

		// Token: 0x17001B16 RID: 6934
		// (get) Token: 0x06005CC6 RID: 23750 RVA: 0x00140BC2 File Offset: 0x0013EDC2
		public string PhysicalPath
		{
			get
			{
				return this._physicalPath;
			}
		}

		// Token: 0x040030D4 RID: 12500
		private string _id;

		// Token: 0x040030D5 RID: 12501
		private VirtualPath _virtualPath;

		// Token: 0x040030D6 RID: 12502
		private string _physicalPath;
	}
}
