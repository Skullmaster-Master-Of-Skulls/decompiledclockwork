using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002CA RID: 714
	internal class DateTimePickerDesigner : ControlDesigner
	{
		// Token: 0x06001C41 RID: 7233 RVA: 0x00093E53 File Offset: 0x00092053
		public DateTimePickerDesigner()
		{
			base.AutoResizeHandles = true;
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001C42 RID: 7234 RVA: 0x000AA3D8 File Offset: 0x000A85D8
		public override SelectionRules SelectionRules
		{
			get
			{
				SelectionRules selectionRules = base.SelectionRules;
				return selectionRules & ~(SelectionRules.TopSizeable | SelectionRules.BottomSizeable);
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001C43 RID: 7235 RVA: 0x000AA3F4 File Offset: 0x000A85F4
		public override IList SnapLines
		{
			get
			{
				ArrayList arrayList = base.SnapLines as ArrayList;
				int num = DesignerUtils.GetTextBaseline(this.Control, ContentAlignment.MiddleLeft);
				num += 2;
				arrayList.Add(new SnapLine(SnapLineType.Baseline, num, SnapLinePriority.Medium));
				return arrayList;
			}
		}
	}
}
