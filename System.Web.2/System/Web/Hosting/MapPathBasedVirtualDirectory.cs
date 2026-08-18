using System;
using System.Collections;

namespace System.Web.Hosting
{
	// Token: 0x020007D1 RID: 2001
	internal class MapPathBasedVirtualDirectory : VirtualDirectory
	{
		// Token: 0x06006017 RID: 24599 RVA: 0x0014C08D File Offset: 0x0014A28D
		public MapPathBasedVirtualDirectory(string virtualPath) : base(virtualPath)
		{
		}

		// Token: 0x17001B7E RID: 7038
		// (get) Token: 0x06006018 RID: 24600 RVA: 0x0014C096 File Offset: 0x0014A296
		public override IEnumerable Directories
		{
			get
			{
				return new MapPathBasedVirtualPathCollection(System.Web.VirtualPath.CreateNonRelative(base.VirtualPath), RequestedEntryType.Directories);
			}
		}

		// Token: 0x17001B7F RID: 7039
		// (get) Token: 0x06006019 RID: 24601 RVA: 0x0014C0A9 File Offset: 0x0014A2A9
		public override IEnumerable Files
		{
			get
			{
				return new MapPathBasedVirtualPathCollection(System.Web.VirtualPath.CreateNonRelative(base.VirtualPath), RequestedEntryType.Files);
			}
		}

		// Token: 0x17001B80 RID: 7040
		// (get) Token: 0x0600601A RID: 24602 RVA: 0x0014C0BC File Offset: 0x0014A2BC
		public override IEnumerable Children
		{
			get
			{
				return new MapPathBasedVirtualPathCollection(System.Web.VirtualPath.CreateNonRelative(base.VirtualPath), RequestedEntryType.All);
			}
		}
	}
}
