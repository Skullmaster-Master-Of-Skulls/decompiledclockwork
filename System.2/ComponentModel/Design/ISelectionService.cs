using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	// Token: 0x020005F6 RID: 1526
	[ComVisible(true)]
	public interface ISelectionService
	{
		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x0600384F RID: 14415
		object PrimarySelection { get; }

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x06003850 RID: 14416
		int SelectionCount { get; }

		// Token: 0x14000065 RID: 101
		// (add) Token: 0x06003851 RID: 14417
		// (remove) Token: 0x06003852 RID: 14418
		event EventHandler SelectionChanged;

		// Token: 0x14000066 RID: 102
		// (add) Token: 0x06003853 RID: 14419
		// (remove) Token: 0x06003854 RID: 14420
		event EventHandler SelectionChanging;

		// Token: 0x06003855 RID: 14421
		bool GetComponentSelected(object component);

		// Token: 0x06003856 RID: 14422
		ICollection GetSelectedComponents();

		// Token: 0x06003857 RID: 14423
		void SetSelectedComponents(ICollection components);

		// Token: 0x06003858 RID: 14424
		void SetSelectedComponents(ICollection components, SelectionTypes selectionType);
	}
}
