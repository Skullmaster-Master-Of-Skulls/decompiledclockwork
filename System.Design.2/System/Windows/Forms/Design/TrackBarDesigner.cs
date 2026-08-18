using System;
using System.ComponentModel;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000363 RID: 867
	internal class TrackBarDesigner : ControlDesigner
	{
		// Token: 0x060023C1 RID: 9153 RVA: 0x00093E53 File Offset: 0x00092053
		public TrackBarDesigner()
		{
			base.AutoResizeHandles = true;
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x060023C2 RID: 9154 RVA: 0x000DFCF4 File Offset: 0x000DDEF4
		public override SelectionRules SelectionRules
		{
			get
			{
				SelectionRules selectionRules = base.SelectionRules;
				object component = base.Component;
				selectionRules |= SelectionRules.AllSizeable;
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)["AutoSize"];
				if (propertyDescriptor != null)
				{
					bool flag = (bool)propertyDescriptor.GetValue(component);
					PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(component)["Orientation"];
					Orientation orientation = Orientation.Horizontal;
					if (propertyDescriptor2 != null)
					{
						orientation = (Orientation)propertyDescriptor2.GetValue(component);
					}
					if (flag)
					{
						if (orientation == Orientation.Horizontal)
						{
							selectionRules &= ~(SelectionRules.TopSizeable | SelectionRules.BottomSizeable);
						}
						else if (orientation == Orientation.Vertical)
						{
							selectionRules &= ~(SelectionRules.LeftSizeable | SelectionRules.RightSizeable);
						}
					}
				}
				return selectionRules;
			}
		}
	}
}
