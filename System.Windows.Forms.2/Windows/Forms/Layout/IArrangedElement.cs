using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms.Layout
{
	// Token: 0x020004CC RID: 1228
	internal interface IArrangedElement : IComponent, IDisposable
	{
		// Token: 0x1700138B RID: 5003
		// (get) Token: 0x0600509A RID: 20634
		Rectangle Bounds { get; }

		// Token: 0x0600509B RID: 20635
		void SetBounds(Rectangle bounds, BoundsSpecified specified);

		// Token: 0x0600509C RID: 20636
		Size GetPreferredSize(Size proposedSize);

		// Token: 0x1700138C RID: 5004
		// (get) Token: 0x0600509D RID: 20637
		Rectangle DisplayRectangle { get; }

		// Token: 0x1700138D RID: 5005
		// (get) Token: 0x0600509E RID: 20638
		bool ParticipatesInLayout { get; }

		// Token: 0x1700138E RID: 5006
		// (get) Token: 0x0600509F RID: 20639
		PropertyStore Properties { get; }

		// Token: 0x060050A0 RID: 20640
		void PerformLayout(IArrangedElement affectedElement, string propertyName);

		// Token: 0x1700138F RID: 5007
		// (get) Token: 0x060050A1 RID: 20641
		IArrangedElement Container { get; }

		// Token: 0x17001390 RID: 5008
		// (get) Token: 0x060050A2 RID: 20642
		ArrangedElementCollection Children { get; }
	}
}
