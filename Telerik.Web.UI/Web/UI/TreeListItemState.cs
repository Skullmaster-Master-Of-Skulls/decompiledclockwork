using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x0200128C RID: 4748
	[Serializable]
	public class TreeListItemState
	{
		// Token: 0x17003FFD RID: 16381
		// (get) Token: 0x0600C624 RID: 50724 RVA: 0x002C38E8 File Offset: 0x002C1AE8
		// (set) Token: 0x0600C625 RID: 50725 RVA: 0x002C38F0 File Offset: 0x002C1AF0
		public bool HasChildItems { get; set; }

		// Token: 0x17003FFE RID: 16382
		// (get) Token: 0x0600C626 RID: 50726 RVA: 0x002C38F9 File Offset: 0x002C1AF9
		// (set) Token: 0x0600C627 RID: 50727 RVA: 0x002C3901 File Offset: 0x002C1B01
		public List<TreeListSiblingState> Siblings { get; set; }

		// Token: 0x17003FFF RID: 16383
		// (get) Token: 0x0600C628 RID: 50728 RVA: 0x002C390A File Offset: 0x002C1B0A
		// (set) Token: 0x0600C629 RID: 50729 RVA: 0x002C3912 File Offset: 0x002C1B12
		public TreeListHierarchyIndex ParentHierarchyIndex { get; set; }
	}
}
