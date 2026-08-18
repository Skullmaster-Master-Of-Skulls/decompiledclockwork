using System;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000319 RID: 793
	internal class MonthCalendarDesigner : ControlDesigner
	{
		// Token: 0x06001F3E RID: 7998 RVA: 0x00093E53 File Offset: 0x00092053
		public MonthCalendarDesigner()
		{
			base.AutoResizeHandles = true;
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001F3F RID: 7999 RVA: 0x000BBBBC File Offset: 0x000B9DBC
		public override SelectionRules SelectionRules
		{
			get
			{
				SelectionRules selectionRules = base.SelectionRules;
				if (this.Control.Parent == null || (this.Control.Parent != null && !this.Control.Parent.IsMirrored))
				{
					selectionRules &= ~(SelectionRules.TopSizeable | SelectionRules.LeftSizeable);
				}
				else
				{
					selectionRules &= ~(SelectionRules.TopSizeable | SelectionRules.RightSizeable);
				}
				return selectionRules;
			}
		}
	}
}
