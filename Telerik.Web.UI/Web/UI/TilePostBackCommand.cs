using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200090E RID: 2318
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class TilePostBackCommand
	{
		// Token: 0x17001CFA RID: 7418
		// (get) Token: 0x06005796 RID: 22422 RVA: 0x0010BA69 File Offset: 0x00109C69
		// (set) Token: 0x06005797 RID: 22423 RVA: 0x0010BA71 File Offset: 0x00109C71
		public TileCommand Type { get; set; }

		// Token: 0x17001CFB RID: 7419
		// (get) Token: 0x06005798 RID: 22424 RVA: 0x0010BA7A File Offset: 0x00109C7A
		// (set) Token: 0x06005799 RID: 22425 RVA: 0x0010BA82 File Offset: 0x00109C82
		public int TileIndex { get; set; }

		// Token: 0x17001CFC RID: 7420
		// (get) Token: 0x0600579A RID: 22426 RVA: 0x0010BA8B File Offset: 0x00109C8B
		// (set) Token: 0x0600579B RID: 22427 RVA: 0x0010BA93 File Offset: 0x00109C93
		public bool OldSelectedValue { get; set; }
	}
}
