using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200036A RID: 874
	internal class UpDownBaseDesigner : ControlDesigner
	{
		// Token: 0x060023DE RID: 9182 RVA: 0x00093E53 File Offset: 0x00092053
		public UpDownBaseDesigner()
		{
			base.AutoResizeHandles = true;
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x060023DF RID: 9183 RVA: 0x000E010C File Offset: 0x000DE30C
		public override SelectionRules SelectionRules
		{
			get
			{
				SelectionRules selectionRules = base.SelectionRules;
				return selectionRules & ~(SelectionRules.TopSizeable | SelectionRules.BottomSizeable);
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x060023E0 RID: 9184 RVA: 0x000E0128 File Offset: 0x000DE328
		public override IList SnapLines
		{
			get
			{
				ArrayList arrayList = base.SnapLines as ArrayList;
				int num = DesignerUtils.GetTextBaseline(this.Control, ContentAlignment.TopLeft);
				BorderStyle borderStyle = BorderStyle.Fixed3D;
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["BorderStyle"];
				if (propertyDescriptor != null)
				{
					borderStyle = (BorderStyle)propertyDescriptor.GetValue(base.Component);
				}
				if (borderStyle == BorderStyle.None)
				{
					num--;
				}
				else
				{
					num += 2;
				}
				arrayList.Add(new SnapLine(SnapLineType.Baseline, num, SnapLinePriority.Medium));
				return arrayList;
			}
		}
	}
}
