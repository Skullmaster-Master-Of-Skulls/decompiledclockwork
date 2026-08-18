using System;

namespace System.Windows.Forms
{
	// Token: 0x020003E8 RID: 1000
	internal class MenuTimer
	{
		// Token: 0x0600442E RID: 17454 RVA: 0x001209A0 File Offset: 0x0011EBA0
		public MenuTimer()
		{
			this.autoMenuExpandTimer.Tick += this.OnTick;
			this.slowShow = Math.Max(this.quickShow, SystemInformation.MenuShowDelay);
		}

		// Token: 0x170010A3 RID: 4259
		// (get) Token: 0x0600442F RID: 17455 RVA: 0x001209F2 File Offset: 0x0011EBF2
		// (set) Token: 0x06004430 RID: 17456 RVA: 0x001209FA File Offset: 0x0011EBFA
		private ToolStripMenuItem CurrentItem
		{
			get
			{
				return this.currentItem;
			}
			set
			{
				this.currentItem = value;
			}
		}

		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x06004431 RID: 17457 RVA: 0x00120A03 File Offset: 0x0011EC03
		// (set) Token: 0x06004432 RID: 17458 RVA: 0x00120A0B File Offset: 0x0011EC0B
		public bool InTransition
		{
			get
			{
				return this.inTransition;
			}
			set
			{
				this.inTransition = value;
			}
		}

		// Token: 0x06004433 RID: 17459 RVA: 0x00120A14 File Offset: 0x0011EC14
		public void Start(ToolStripMenuItem item)
		{
			if (this.InTransition)
			{
				return;
			}
			this.StartCore(item);
		}

		// Token: 0x06004434 RID: 17460 RVA: 0x00120A28 File Offset: 0x0011EC28
		private void StartCore(ToolStripMenuItem item)
		{
			if (item != this.CurrentItem)
			{
				this.Cancel(this.CurrentItem);
			}
			this.CurrentItem = item;
			if (item != null)
			{
				this.CurrentItem = item;
				this.autoMenuExpandTimer.Interval = (item.IsOnDropDown ? this.slowShow : this.quickShow);
				this.autoMenuExpandTimer.Enabled = true;
			}
		}

		// Token: 0x06004435 RID: 17461 RVA: 0x00120A88 File Offset: 0x0011EC88
		public void Transition(ToolStripMenuItem fromItem, ToolStripMenuItem toItem)
		{
			if (toItem == null && this.InTransition)
			{
				this.Cancel();
				this.EndTransition(true);
				return;
			}
			if (this.fromItem != fromItem)
			{
				this.fromItem = fromItem;
				this.CancelCore();
				this.StartCore(toItem);
			}
			this.CurrentItem = toItem;
			this.InTransition = true;
		}

		// Token: 0x06004436 RID: 17462 RVA: 0x00120AD9 File Offset: 0x0011ECD9
		public void Cancel()
		{
			if (this.InTransition)
			{
				return;
			}
			this.CancelCore();
		}

		// Token: 0x06004437 RID: 17463 RVA: 0x00120AEA File Offset: 0x0011ECEA
		public void Cancel(ToolStripMenuItem item)
		{
			if (this.InTransition)
			{
				return;
			}
			if (item == this.CurrentItem)
			{
				this.CancelCore();
			}
		}

		// Token: 0x06004438 RID: 17464 RVA: 0x00120B04 File Offset: 0x0011ED04
		private void CancelCore()
		{
			this.autoMenuExpandTimer.Enabled = false;
			this.CurrentItem = null;
		}

		// Token: 0x06004439 RID: 17465 RVA: 0x00120B1C File Offset: 0x0011ED1C
		private void EndTransition(bool forceClose)
		{
			ToolStripMenuItem toolStripMenuItem = this.fromItem;
			this.fromItem = null;
			if (this.InTransition)
			{
				this.InTransition = false;
				bool flag = forceClose || (this.CurrentItem != null && this.CurrentItem != toolStripMenuItem && this.CurrentItem.Selected);
				if (flag && toolStripMenuItem != null && toolStripMenuItem.HasDropDownItems)
				{
					toolStripMenuItem.HideDropDown();
				}
			}
		}

		// Token: 0x0600443A RID: 17466 RVA: 0x00120B80 File Offset: 0x0011ED80
		internal void HandleToolStripMouseLeave(ToolStrip toolStrip)
		{
			if (this.InTransition && toolStrip == this.fromItem.ParentInternal)
			{
				if (this.CurrentItem != null)
				{
					this.CurrentItem.Select();
					return;
				}
			}
			else if (toolStrip.IsDropDown && toolStrip.ActiveDropDowns.Count > 0)
			{
				ToolStripDropDown toolStripDropDown = toolStrip.ActiveDropDowns[0] as ToolStripDropDown;
				ToolStripMenuItem toolStripMenuItem = (toolStripDropDown == null) ? null : (toolStripDropDown.OwnerItem as ToolStripMenuItem);
				if (toolStripMenuItem != null && toolStripMenuItem.Pressed)
				{
					toolStripMenuItem.Select();
				}
			}
		}

		// Token: 0x0600443B RID: 17467 RVA: 0x00120C04 File Offset: 0x0011EE04
		private void OnTick(object sender, EventArgs e)
		{
			this.autoMenuExpandTimer.Enabled = false;
			if (this.CurrentItem == null)
			{
				return;
			}
			this.EndTransition(false);
			if (this.CurrentItem != null && !this.CurrentItem.IsDisposed && this.CurrentItem.Selected && this.CurrentItem.Enabled && ToolStripManager.ModalMenuFilter.InMenuMode)
			{
				this.CurrentItem.OnMenuAutoExpand();
			}
		}

		// Token: 0x0400261A RID: 9754
		private Timer autoMenuExpandTimer = new Timer();

		// Token: 0x0400261B RID: 9755
		private ToolStripMenuItem currentItem;

		// Token: 0x0400261C RID: 9756
		private ToolStripMenuItem fromItem;

		// Token: 0x0400261D RID: 9757
		private bool inTransition;

		// Token: 0x0400261E RID: 9758
		private int quickShow = 1;

		// Token: 0x0400261F RID: 9759
		private int slowShow;
	}
}
