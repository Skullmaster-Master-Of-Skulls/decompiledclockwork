using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200034A RID: 842
	internal class ToolBarDesigner : ControlDesigner
	{
		// Token: 0x0600214C RID: 8524 RVA: 0x00093E53 File Offset: 0x00092053
		public ToolBarDesigner()
		{
			base.AutoResizeHandles = true;
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x0600214D RID: 8525 RVA: 0x000CB584 File Offset: 0x000C9784
		public override ICollection AssociatedComponents
		{
			get
			{
				ToolBar toolBar = this.Control as ToolBar;
				if (toolBar != null)
				{
					return toolBar.Buttons;
				}
				return base.AssociatedComponents;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x0600214E RID: 8526 RVA: 0x000CB5B0 File Offset: 0x000C97B0
		public override SelectionRules SelectionRules
		{
			get
			{
				SelectionRules selectionRules = base.SelectionRules;
				object component = base.Component;
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)["Dock"];
				PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(component)["AutoSize"];
				if (propertyDescriptor != null && propertyDescriptor2 != null)
				{
					DockStyle dockStyle = (DockStyle)propertyDescriptor.GetValue(component);
					bool flag = (bool)propertyDescriptor2.GetValue(component);
					if (flag)
					{
						selectionRules &= ~(SelectionRules.TopSizeable | SelectionRules.BottomSizeable);
						if (dockStyle != DockStyle.None)
						{
							selectionRules &= ~(SelectionRules.TopSizeable | SelectionRules.BottomSizeable | SelectionRules.LeftSizeable | SelectionRules.RightSizeable);
						}
					}
				}
				return selectionRules;
			}
		}
	}
}
