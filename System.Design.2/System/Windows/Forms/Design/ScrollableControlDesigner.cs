using System;
using System.Design;
using System.Drawing;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000329 RID: 809
	public class ScrollableControlDesigner : ParentControlDesigner
	{
		// Token: 0x06001FE5 RID: 8165 RVA: 0x000C12C4 File Offset: 0x000BF4C4
		protected override bool GetHitTest(Point pt)
		{
			if (base.GetHitTest(pt))
			{
				return true;
			}
			ScrollableControl scrollableControl = (ScrollableControl)this.Control;
			if (scrollableControl.IsHandleCreated && scrollableControl.AutoScroll)
			{
				int num = (int)NativeMethods.SendMessage(scrollableControl.Handle, 132, (IntPtr)0, (IntPtr)NativeMethods.Util.MAKELPARAM(pt.X, pt.Y));
				if (num == 7 || num == 6)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x000C1338 File Offset: 0x000BF538
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			int msg = m.Msg;
			if (msg - 276 <= 1)
			{
				if (this.selManager == null)
				{
					this.selManager = (this.GetService(typeof(SelectionManager)) as SelectionManager);
				}
				if (this.selManager != null)
				{
					this.selManager.Refresh();
				}
				this.Control.Invalidate();
				this.Control.Update();
			}
		}

		// Token: 0x0400189B RID: 6299
		private SelectionManager selManager;
	}
}
