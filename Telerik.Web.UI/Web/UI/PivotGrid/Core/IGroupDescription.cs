using System;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x020006F1 RID: 1777
	public interface IGroupDescription : IDescriptionBase, INamed
	{
		// Token: 0x1700149E RID: 5278
		// (get) Token: 0x06003F33 RID: 16179
		SortOrder SortOrder { get; }

		// Token: 0x1700149F RID: 5279
		// (get) Token: 0x06003F34 RID: 16180
		GroupComparer GroupComparer { get; }
	}
}
