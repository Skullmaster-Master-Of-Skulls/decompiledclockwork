using System;
using System.Collections;

namespace System.Web.Hosting
{
	// Token: 0x020002AE RID: 686
	internal class MapPathBasedVirtualDirectory : VirtualDirectory
	{
		// Token: 0x060023E7 RID: 9191 RVA: 0x0009A0AD File Offset: 0x000990AD
		public MapPathBasedVirtualDirectory(string virtualPath) : base(virtualPath)
		{
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x060023E8 RID: 9192 RVA: 0x0009A0B6 File Offset: 0x000990B6
		public override IEnumerable Directories
		{
			get
			{
				return new MapPathBasedVirtualPathCollection(System.Web.VirtualPath.CreateNonRelative(base.VirtualPath), RequestedEntryType.Directories);
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x060023E9 RID: 9193 RVA: 0x0009A0C9 File Offset: 0x000990C9
		public override IEnumerable Files
		{
			get
			{
				return new MapPathBasedVirtualPathCollection(System.Web.VirtualPath.CreateNonRelative(base.VirtualPath), RequestedEntryType.Files);
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x060023EA RID: 9194 RVA: 0x0009A0DC File Offset: 0x000990DC
		public override IEnumerable Children
		{
			get
			{
				return new MapPathBasedVirtualPathCollection(System.Web.VirtualPath.CreateNonRelative(base.VirtualPath), RequestedEntryType.All);
			}
		}
	}
}
