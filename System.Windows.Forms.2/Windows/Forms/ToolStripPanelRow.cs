using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x020003F4 RID: 1012
	[ToolboxItem(false)]
	public class ToolStripPanelRow : Component, IArrangedElement, IComponent, IDisposable
	{
		// Token: 0x0600455E RID: 17758 RVA: 0x001233A6 File Offset: 0x001215A6
		public ToolStripPanelRow(ToolStripPanel parent) : this(parent, true)
		{
		}

		// Token: 0x0600455F RID: 17759 RVA: 0x001233B0 File Offset: 0x001215B0
		internal ToolStripPanelRow(ToolStripPanel parent, bool visible)
		{
			if (DpiHelper.EnableToolStripHighDpiImprovements)
			{
				this.minAllowedWidth = DpiHelper.LogicalToDeviceUnitsX(50);
			}
			this.parent = parent;
			this.state[ToolStripPanelRow.stateVisible] = visible;
			this.state[ToolStripPanelRow.stateDisposing | ToolStripPanelRow.stateLocked | ToolStripPanelRow.stateInitialized] = false;
			using (new LayoutTransaction(parent, this, null))
			{
				this.Margin = this.DefaultMargin;
				CommonProperties.SetAutoSize(this, true);
			}
		}

		// Token: 0x17001109 RID: 4361
		// (get) Token: 0x06004560 RID: 17760 RVA: 0x00123464 File Offset: 0x00121664
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		// Token: 0x1700110A RID: 4362
		// (get) Token: 0x06004561 RID: 17761 RVA: 0x0012346C File Offset: 0x0012166C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlControlsDescr")]
		public Control[] Controls
		{
			get
			{
				Control[] array = new Control[this.ControlsInternal.Count];
				this.ControlsInternal.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x1700110B RID: 4363
		// (get) Token: 0x06004562 RID: 17762 RVA: 0x00123498 File Offset: 0x00121698
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlControlsDescr")]
		internal ToolStripPanelRow.ToolStripPanelRowControlCollection ControlsInternal
		{
			get
			{
				ToolStripPanelRow.ToolStripPanelRowControlCollection toolStripPanelRowControlCollection = (ToolStripPanelRow.ToolStripPanelRowControlCollection)this.Properties.GetObject(ToolStripPanelRow.PropControlsCollection);
				if (toolStripPanelRowControlCollection == null)
				{
					toolStripPanelRowControlCollection = this.CreateControlsInstance();
					this.Properties.SetObject(ToolStripPanelRow.PropControlsCollection, toolStripPanelRowControlCollection);
				}
				return toolStripPanelRowControlCollection;
			}
		}

		// Token: 0x1700110C RID: 4364
		// (get) Token: 0x06004563 RID: 17763 RVA: 0x001234D7 File Offset: 0x001216D7
		internal ArrangedElementCollection Cells
		{
			get
			{
				return this.ControlsInternal.Cells;
			}
		}

		// Token: 0x1700110D RID: 4365
		// (get) Token: 0x06004564 RID: 17764 RVA: 0x001234E4 File Offset: 0x001216E4
		// (set) Token: 0x06004565 RID: 17765 RVA: 0x001234F6 File Offset: 0x001216F6
		internal bool CachedBoundsMode
		{
			get
			{
				return this.state[ToolStripPanelRow.stateCachedBoundsMode];
			}
			set
			{
				this.state[ToolStripPanelRow.stateCachedBoundsMode] = value;
			}
		}

		// Token: 0x1700110E RID: 4366
		// (get) Token: 0x06004566 RID: 17766 RVA: 0x00123509 File Offset: 0x00121709
		private ToolStripPanelRow.ToolStripPanelRowManager RowManager
		{
			get
			{
				if (this.rowManager == null)
				{
					this.rowManager = ((this.Orientation == Orientation.Horizontal) ? new ToolStripPanelRow.HorizontalRowManager(this) : new ToolStripPanelRow.VerticalRowManager(this));
					this.Initialized = true;
				}
				return this.rowManager;
			}
		}

		// Token: 0x1700110F RID: 4367
		// (get) Token: 0x06004567 RID: 17767 RVA: 0x0012353C File Offset: 0x0012173C
		protected virtual Padding DefaultMargin
		{
			get
			{
				ToolStripPanelCell nextVisibleCell = this.RowManager.GetNextVisibleCell(0, true);
				if (nextVisibleCell != null && nextVisibleCell.DraggedControl != null && nextVisibleCell.DraggedControl.Stretch)
				{
					Padding rowMargin = this.ToolStripPanel.RowMargin;
					if (this.Orientation == Orientation.Horizontal)
					{
						rowMargin.Left = 0;
						rowMargin.Right = 0;
					}
					else
					{
						rowMargin.Top = 0;
						rowMargin.Bottom = 0;
					}
					return rowMargin;
				}
				return this.ToolStripPanel.RowMargin;
			}
		}

		// Token: 0x17001110 RID: 4368
		// (get) Token: 0x06004568 RID: 17768 RVA: 0x00019BFD File Offset: 0x00017DFD
		protected virtual Padding DefaultPadding
		{
			get
			{
				return Padding.Empty;
			}
		}

		// Token: 0x17001111 RID: 4369
		// (get) Token: 0x06004569 RID: 17769 RVA: 0x001235B2 File Offset: 0x001217B2
		public Rectangle DisplayRectangle
		{
			get
			{
				return this.RowManager.DisplayRectangle;
			}
		}

		// Token: 0x17001112 RID: 4370
		// (get) Token: 0x0600456A RID: 17770 RVA: 0x000AFBF0 File Offset: 0x000ADDF0
		public LayoutEngine LayoutEngine
		{
			get
			{
				return FlowLayout.Instance;
			}
		}

		// Token: 0x17001113 RID: 4371
		// (get) Token: 0x0600456B RID: 17771 RVA: 0x001235BF File Offset: 0x001217BF
		internal bool Locked
		{
			get
			{
				return this.state[ToolStripPanelRow.stateLocked];
			}
		}

		// Token: 0x17001114 RID: 4372
		// (get) Token: 0x0600456C RID: 17772 RVA: 0x001235D1 File Offset: 0x001217D1
		// (set) Token: 0x0600456D RID: 17773 RVA: 0x001235E3 File Offset: 0x001217E3
		private bool Initialized
		{
			get
			{
				return this.state[ToolStripPanelRow.stateInitialized];
			}
			set
			{
				this.state[ToolStripPanelRow.stateInitialized] = value;
			}
		}

		// Token: 0x17001115 RID: 4373
		// (get) Token: 0x0600456E RID: 17774 RVA: 0x00019C19 File Offset: 0x00017E19
		// (set) Token: 0x0600456F RID: 17775 RVA: 0x001235F6 File Offset: 0x001217F6
		public Padding Margin
		{
			get
			{
				return CommonProperties.GetMargin(this);
			}
			set
			{
				if (this.Margin != value)
				{
					CommonProperties.SetMargin(this, value);
				}
			}
		}

		// Token: 0x17001116 RID: 4374
		// (get) Token: 0x06004570 RID: 17776 RVA: 0x0012360D File Offset: 0x0012180D
		// (set) Token: 0x06004571 RID: 17777 RVA: 0x0012361B File Offset: 0x0012181B
		public virtual Padding Padding
		{
			get
			{
				return CommonProperties.GetPadding(this, this.DefaultPadding);
			}
			set
			{
				if (this.Padding != value)
				{
					CommonProperties.SetPadding(this, value);
				}
			}
		}

		// Token: 0x17001117 RID: 4375
		// (get) Token: 0x06004572 RID: 17778 RVA: 0x00123632 File Offset: 0x00121832
		internal Control ParentInternal
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17001118 RID: 4376
		// (get) Token: 0x06004573 RID: 17779 RVA: 0x0012363A File Offset: 0x0012183A
		internal PropertyStore Properties
		{
			get
			{
				return this.propertyStore;
			}
		}

		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x06004574 RID: 17780 RVA: 0x00123632 File Offset: 0x00121832
		public ToolStripPanel ToolStripPanel
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x06004575 RID: 17781 RVA: 0x00123642 File Offset: 0x00121842
		internal bool Visible
		{
			get
			{
				return this.state[ToolStripPanelRow.stateVisible];
			}
		}

		// Token: 0x1700111B RID: 4379
		// (get) Token: 0x06004576 RID: 17782 RVA: 0x00123654 File Offset: 0x00121854
		public Orientation Orientation
		{
			get
			{
				return this.ToolStripPanel.Orientation;
			}
		}

		// Token: 0x06004577 RID: 17783 RVA: 0x00123661 File Offset: 0x00121861
		public bool CanMove(ToolStrip toolStripToDrag)
		{
			return !this.ToolStripPanel.Locked && !this.Locked && this.RowManager.CanMove(toolStripToDrag);
		}

		// Token: 0x06004578 RID: 17784 RVA: 0x00123686 File Offset: 0x00121886
		private ToolStripPanelRow.ToolStripPanelRowControlCollection CreateControlsInstance()
		{
			return new ToolStripPanelRow.ToolStripPanelRowControlCollection(this);
		}

		// Token: 0x06004579 RID: 17785 RVA: 0x00123690 File Offset: 0x00121890
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.state[ToolStripPanelRow.stateDisposing] = true;
					this.ControlsInternal.Clear();
				}
			}
			finally
			{
				this.state[ToolStripPanelRow.stateDisposing] = false;
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600457A RID: 17786 RVA: 0x001236E8 File Offset: 0x001218E8
		protected internal virtual void OnControlAdded(Control control, int index)
		{
			ISupportToolStripPanel supportToolStripPanel = control as ISupportToolStripPanel;
			if (supportToolStripPanel != null)
			{
				supportToolStripPanel.ToolStripPanelRow = this;
			}
			this.RowManager.OnControlAdded(control, index);
		}

		// Token: 0x0600457B RID: 17787 RVA: 0x00123713 File Offset: 0x00121913
		protected internal virtual void OnOrientationChanged()
		{
			this.rowManager = null;
		}

		// Token: 0x0600457C RID: 17788 RVA: 0x0012371C File Offset: 0x0012191C
		protected void OnBoundsChanged(Rectangle oldBounds, Rectangle newBounds)
		{
			((IArrangedElement)this).PerformLayout(this, PropertyNames.Size);
			this.RowManager.OnBoundsChanged(oldBounds, newBounds);
		}

		// Token: 0x0600457D RID: 17789 RVA: 0x00123738 File Offset: 0x00121938
		protected internal virtual void OnControlRemoved(Control control, int index)
		{
			if (!this.state[ToolStripPanelRow.stateDisposing])
			{
				this.SuspendLayout();
				this.RowManager.OnControlRemoved(control, index);
				ISupportToolStripPanel supportToolStripPanel = control as ISupportToolStripPanel;
				if (supportToolStripPanel != null && supportToolStripPanel.ToolStripPanelRow == this)
				{
					supportToolStripPanel.ToolStripPanelRow = null;
				}
				this.ResumeLayout(true);
				if (this.ControlsInternal.Count <= 0)
				{
					this.ToolStripPanel.RowsInternal.Remove(this);
					base.Dispose();
				}
			}
		}

		// Token: 0x0600457E RID: 17790 RVA: 0x001237B0 File Offset: 0x001219B0
		internal Size GetMinimumSize(ToolStrip toolStrip)
		{
			if (toolStrip.MinimumSize == Size.Empty)
			{
				return new Size(this.minAllowedWidth, this.minAllowedWidth);
			}
			return toolStrip.MinimumSize;
		}

		// Token: 0x0600457F RID: 17791 RVA: 0x001237DC File Offset: 0x001219DC
		private void ApplyCachedBounds()
		{
			for (int i = 0; i < this.Cells.Count; i++)
			{
				IArrangedElement arrangedElement = this.Cells[i];
				if (arrangedElement.ParticipatesInLayout)
				{
					ToolStripPanelCell toolStripPanelCell = arrangedElement as ToolStripPanelCell;
					arrangedElement.SetBounds(toolStripPanelCell.CachedBounds, BoundsSpecified.None);
				}
			}
		}

		// Token: 0x06004580 RID: 17792 RVA: 0x00123828 File Offset: 0x00121A28
		protected virtual void OnLayout(LayoutEventArgs e)
		{
			if (this.Initialized && !this.state[ToolStripPanelRow.stateInLayout])
			{
				this.state[ToolStripPanelRow.stateInLayout] = true;
				try
				{
					this.Margin = this.DefaultMargin;
					this.CachedBoundsMode = true;
					try
					{
						bool flag = this.LayoutEngine.Layout(this, e);
					}
					finally
					{
						this.CachedBoundsMode = false;
					}
					if (this.RowManager.GetNextVisibleCell(this.Cells.Count - 1, false) == null)
					{
						this.ApplyCachedBounds();
					}
					else if (this.Orientation == Orientation.Horizontal)
					{
						this.OnLayoutHorizontalPostFix();
					}
					else
					{
						this.OnLayoutVerticalPostFix();
					}
				}
				finally
				{
					this.state[ToolStripPanelRow.stateInLayout] = false;
				}
			}
		}

		// Token: 0x06004581 RID: 17793 RVA: 0x001238FC File Offset: 0x00121AFC
		private void OnLayoutHorizontalPostFix()
		{
			ToolStripPanelCell nextVisibleCell = this.RowManager.GetNextVisibleCell(this.Cells.Count - 1, false);
			if (nextVisibleCell == null)
			{
				this.ApplyCachedBounds();
				return;
			}
			int num = nextVisibleCell.CachedBounds.Right - this.RowManager.DisplayRectangle.Right;
			if (num <= 0)
			{
				this.ApplyCachedBounds();
				return;
			}
			int[] array = new int[this.Cells.Count];
			for (int i = 0; i < this.Cells.Count; i++)
			{
				ToolStripPanelCell toolStripPanelCell = this.Cells[i] as ToolStripPanelCell;
				array[i] = toolStripPanelCell.Margin.Left;
			}
			num -= this.RowManager.FreeSpaceFromRow(num);
			for (int j = 0; j < this.Cells.Count; j++)
			{
				ToolStripPanelCell toolStripPanelCell2 = this.Cells[j] as ToolStripPanelCell;
				Rectangle cachedBounds = toolStripPanelCell2.CachedBounds;
				cachedBounds.X -= Math.Max(0, array[j] - toolStripPanelCell2.Margin.Left);
				toolStripPanelCell2.CachedBounds = cachedBounds;
			}
			if (num <= 0)
			{
				this.ApplyCachedBounds();
				return;
			}
			int[] array2 = null;
			for (int k = this.Cells.Count - 1; k >= 0; k--)
			{
				ToolStripPanelCell toolStripPanelCell3 = this.Cells[k] as ToolStripPanelCell;
				if (toolStripPanelCell3.Visible)
				{
					Size minimumSize = this.GetMinimumSize(toolStripPanelCell3.Control as ToolStrip);
					Rectangle cachedBounds2 = toolStripPanelCell3.CachedBounds;
					if (cachedBounds2.Width > minimumSize.Width)
					{
						num -= cachedBounds2.Width - minimumSize.Width;
						cachedBounds2.Width = ((num < 0) ? (minimumSize.Width + -num) : minimumSize.Width);
						for (int l = k + 1; l < this.Cells.Count; l++)
						{
							if (array2 == null)
							{
								array2 = new int[this.Cells.Count];
							}
							array2[l] += Math.Max(0, toolStripPanelCell3.CachedBounds.Width - cachedBounds2.Width);
						}
						toolStripPanelCell3.CachedBounds = cachedBounds2;
					}
				}
				if (num <= 0)
				{
					break;
				}
			}
			if (array2 != null)
			{
				for (int m = 0; m < this.Cells.Count; m++)
				{
					ToolStripPanelCell toolStripPanelCell4 = this.Cells[m] as ToolStripPanelCell;
					Rectangle cachedBounds3 = toolStripPanelCell4.CachedBounds;
					cachedBounds3.X -= array2[m];
					toolStripPanelCell4.CachedBounds = cachedBounds3;
				}
			}
			this.ApplyCachedBounds();
		}

		// Token: 0x06004582 RID: 17794 RVA: 0x00123B9C File Offset: 0x00121D9C
		private void OnLayoutVerticalPostFix()
		{
			ToolStripPanelCell nextVisibleCell = this.RowManager.GetNextVisibleCell(this.Cells.Count - 1, false);
			int num = nextVisibleCell.CachedBounds.Bottom - this.RowManager.DisplayRectangle.Bottom;
			if (num <= 0)
			{
				this.ApplyCachedBounds();
				return;
			}
			int[] array = new int[this.Cells.Count];
			for (int i = 0; i < this.Cells.Count; i++)
			{
				ToolStripPanelCell toolStripPanelCell = this.Cells[i] as ToolStripPanelCell;
				array[i] = toolStripPanelCell.Margin.Top;
			}
			num -= this.RowManager.FreeSpaceFromRow(num);
			for (int j = 0; j < this.Cells.Count; j++)
			{
				ToolStripPanelCell toolStripPanelCell2 = this.Cells[j] as ToolStripPanelCell;
				Rectangle cachedBounds = toolStripPanelCell2.CachedBounds;
				cachedBounds.X = Math.Max(0, cachedBounds.X - array[j] - toolStripPanelCell2.Margin.Top);
				toolStripPanelCell2.CachedBounds = cachedBounds;
			}
			if (num <= 0)
			{
				this.ApplyCachedBounds();
				return;
			}
			int[] array2 = null;
			for (int k = this.Cells.Count - 1; k >= 0; k--)
			{
				ToolStripPanelCell toolStripPanelCell3 = this.Cells[k] as ToolStripPanelCell;
				if (toolStripPanelCell3.Visible)
				{
					Size minimumSize = this.GetMinimumSize(toolStripPanelCell3.Control as ToolStrip);
					Rectangle cachedBounds2 = toolStripPanelCell3.CachedBounds;
					if (cachedBounds2.Height > minimumSize.Height)
					{
						num -= cachedBounds2.Height - minimumSize.Height;
						cachedBounds2.Height = ((num < 0) ? (minimumSize.Height + -num) : minimumSize.Height);
						for (int l = k + 1; l < this.Cells.Count; l++)
						{
							if (array2 == null)
							{
								array2 = new int[this.Cells.Count];
							}
							array2[l] += Math.Max(0, toolStripPanelCell3.CachedBounds.Height - cachedBounds2.Height);
						}
						toolStripPanelCell3.CachedBounds = cachedBounds2;
					}
				}
				if (num <= 0)
				{
					break;
				}
			}
			if (array2 != null)
			{
				for (int m = 0; m < this.Cells.Count; m++)
				{
					ToolStripPanelCell toolStripPanelCell4 = this.Cells[m] as ToolStripPanelCell;
					Rectangle cachedBounds3 = toolStripPanelCell4.CachedBounds;
					cachedBounds3.Y -= array2[m];
					toolStripPanelCell4.CachedBounds = cachedBounds3;
				}
			}
			this.ApplyCachedBounds();
		}

		// Token: 0x06004583 RID: 17795 RVA: 0x00123E34 File Offset: 0x00122034
		private void SetBounds(Rectangle bounds)
		{
			if (bounds != this.bounds)
			{
				Rectangle oldBounds = this.bounds;
				this.bounds = bounds;
				this.OnBoundsChanged(oldBounds, bounds);
			}
		}

		// Token: 0x06004584 RID: 17796 RVA: 0x00123E65 File Offset: 0x00122065
		private void SuspendLayout()
		{
			this.suspendCount++;
		}

		// Token: 0x06004585 RID: 17797 RVA: 0x00123E75 File Offset: 0x00122075
		private void ResumeLayout(bool performLayout)
		{
			this.suspendCount--;
			if (performLayout)
			{
				((IArrangedElement)this).PerformLayout(this, null);
			}
		}

		// Token: 0x1700111C RID: 4380
		// (get) Token: 0x06004586 RID: 17798 RVA: 0x00123E90 File Offset: 0x00122090
		ArrangedElementCollection IArrangedElement.Children
		{
			get
			{
				return this.Cells;
			}
		}

		// Token: 0x1700111D RID: 4381
		// (get) Token: 0x06004587 RID: 17799 RVA: 0x00123E98 File Offset: 0x00122098
		IArrangedElement IArrangedElement.Container
		{
			get
			{
				return this.ToolStripPanel;
			}
		}

		// Token: 0x1700111E RID: 4382
		// (get) Token: 0x06004588 RID: 17800 RVA: 0x00123EA0 File Offset: 0x001220A0
		Rectangle IArrangedElement.DisplayRectangle
		{
			get
			{
				return this.Bounds;
			}
		}

		// Token: 0x1700111F RID: 4383
		// (get) Token: 0x06004589 RID: 17801 RVA: 0x00123EB5 File Offset: 0x001220B5
		bool IArrangedElement.ParticipatesInLayout
		{
			get
			{
				return this.Visible;
			}
		}

		// Token: 0x17001120 RID: 4384
		// (get) Token: 0x0600458A RID: 17802 RVA: 0x00123EBD File Offset: 0x001220BD
		PropertyStore IArrangedElement.Properties
		{
			get
			{
				return this.Properties;
			}
		}

		// Token: 0x0600458B RID: 17803 RVA: 0x00123EC8 File Offset: 0x001220C8
		Size IArrangedElement.GetPreferredSize(Size constrainingSize)
		{
			Size result = this.LayoutEngine.GetPreferredSize(this, constrainingSize - this.Padding.Size) + this.Padding.Size;
			if (this.Orientation == Orientation.Horizontal && this.ParentInternal != null)
			{
				result.Width = this.DisplayRectangle.Width;
			}
			else
			{
				result.Height = this.DisplayRectangle.Height;
			}
			return result;
		}

		// Token: 0x0600458C RID: 17804 RVA: 0x00123F46 File Offset: 0x00122146
		void IArrangedElement.SetBounds(Rectangle bounds, BoundsSpecified specified)
		{
			this.SetBounds(bounds);
		}

		// Token: 0x0600458D RID: 17805 RVA: 0x00123F4F File Offset: 0x0012214F
		void IArrangedElement.PerformLayout(IArrangedElement container, string propertyName)
		{
			if (this.suspendCount <= 0)
			{
				this.OnLayout(new LayoutEventArgs(container, propertyName));
			}
		}

		// Token: 0x17001121 RID: 4385
		// (get) Token: 0x0600458E RID: 17806 RVA: 0x00123F67 File Offset: 0x00122167
		internal Rectangle DragBounds
		{
			get
			{
				return this.RowManager.DragBounds;
			}
		}

		// Token: 0x0600458F RID: 17807 RVA: 0x00123F74 File Offset: 0x00122174
		internal void MoveControl(ToolStrip movingControl, Point startClientLocation, Point endClientLocation)
		{
			this.RowManager.MoveControl(movingControl, startClientLocation, endClientLocation);
		}

		// Token: 0x06004590 RID: 17808 RVA: 0x00123F84 File Offset: 0x00122184
		internal void JoinRow(ToolStrip toolStripToDrag, Point locationToDrag)
		{
			this.RowManager.JoinRow(toolStripToDrag, locationToDrag);
		}

		// Token: 0x06004591 RID: 17809 RVA: 0x00123F93 File Offset: 0x00122193
		internal void LeaveRow(ToolStrip toolStripToDrag)
		{
			this.RowManager.LeaveRow(toolStripToDrag);
			if (this.ControlsInternal.Count == 0)
			{
				this.ToolStripPanel.RowsInternal.Remove(this);
				base.Dispose();
			}
		}

		// Token: 0x06004592 RID: 17810 RVA: 0x000072B6 File Offset: 0x000054B6
		[Conditional("DEBUG")]
		private void PrintPlacements(int index)
		{
		}

		// Token: 0x04002656 RID: 9814
		private Rectangle bounds = Rectangle.Empty;

		// Token: 0x04002657 RID: 9815
		private ToolStripPanel parent;

		// Token: 0x04002658 RID: 9816
		private BitVector32 state;

		// Token: 0x04002659 RID: 9817
		private PropertyStore propertyStore = new PropertyStore();

		// Token: 0x0400265A RID: 9818
		private int suspendCount;

		// Token: 0x0400265B RID: 9819
		private ToolStripPanelRow.ToolStripPanelRowManager rowManager;

		// Token: 0x0400265C RID: 9820
		private const int MINALLOWEDWIDTH = 50;

		// Token: 0x0400265D RID: 9821
		private int minAllowedWidth = 50;

		// Token: 0x0400265E RID: 9822
		private static readonly int stateVisible = BitVector32.CreateMask();

		// Token: 0x0400265F RID: 9823
		private static readonly int stateDisposing = BitVector32.CreateMask(ToolStripPanelRow.stateVisible);

		// Token: 0x04002660 RID: 9824
		private static readonly int stateLocked = BitVector32.CreateMask(ToolStripPanelRow.stateDisposing);

		// Token: 0x04002661 RID: 9825
		private static readonly int stateInitialized = BitVector32.CreateMask(ToolStripPanelRow.stateLocked);

		// Token: 0x04002662 RID: 9826
		private static readonly int stateCachedBoundsMode = BitVector32.CreateMask(ToolStripPanelRow.stateInitialized);

		// Token: 0x04002663 RID: 9827
		private static readonly int stateInLayout = BitVector32.CreateMask(ToolStripPanelRow.stateCachedBoundsMode);

		// Token: 0x04002664 RID: 9828
		private static readonly int PropControlsCollection = PropertyStore.CreateKey();

		// Token: 0x04002665 RID: 9829
		internal static TraceSwitch ToolStripPanelRowCreationDebug;

		// Token: 0x04002666 RID: 9830
		internal static readonly TraceSwitch ToolStripPanelMouseDebug;

		// Token: 0x02000812 RID: 2066
		private abstract class ToolStripPanelRowManager
		{
			// Token: 0x06006F76 RID: 28534 RVA: 0x00198B2C File Offset: 0x00196D2C
			public ToolStripPanelRowManager(ToolStripPanelRow owner)
			{
				this.owner = owner;
			}

			// Token: 0x06006F77 RID: 28535 RVA: 0x00198B3C File Offset: 0x00196D3C
			public virtual bool CanMove(ToolStrip toolStripToDrag)
			{
				if (toolStripToDrag != null && ((ISupportToolStripPanel)toolStripToDrag).Stretch)
				{
					return false;
				}
				foreach (object obj in this.Row.ControlsInternal)
				{
					Control control = (Control)obj;
					ISupportToolStripPanel supportToolStripPanel = control as ISupportToolStripPanel;
					if (supportToolStripPanel != null && supportToolStripPanel.Stretch)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x17001859 RID: 6233
			// (get) Token: 0x06006F78 RID: 28536 RVA: 0x00054335 File Offset: 0x00052535
			public virtual Rectangle DragBounds
			{
				get
				{
					return Rectangle.Empty;
				}
			}

			// Token: 0x1700185A RID: 6234
			// (get) Token: 0x06006F79 RID: 28537 RVA: 0x00054335 File Offset: 0x00052535
			public virtual Rectangle DisplayRectangle
			{
				get
				{
					return Rectangle.Empty;
				}
			}

			// Token: 0x1700185B RID: 6235
			// (get) Token: 0x06006F7A RID: 28538 RVA: 0x00198BC0 File Offset: 0x00196DC0
			public ToolStripPanel ToolStripPanel
			{
				get
				{
					return this.owner.ToolStripPanel;
				}
			}

			// Token: 0x1700185C RID: 6236
			// (get) Token: 0x06006F7B RID: 28539 RVA: 0x00198BCD File Offset: 0x00196DCD
			public ToolStripPanelRow Row
			{
				get
				{
					return this.owner;
				}
			}

			// Token: 0x1700185D RID: 6237
			// (get) Token: 0x06006F7C RID: 28540 RVA: 0x00198BD5 File Offset: 0x00196DD5
			public FlowLayoutSettings FlowLayoutSettings
			{
				get
				{
					if (this.flowLayoutSettings == null)
					{
						this.flowLayoutSettings = new FlowLayoutSettings(this.owner);
					}
					return this.flowLayoutSettings;
				}
			}

			// Token: 0x06006F7D RID: 28541 RVA: 0x00011A20 File Offset: 0x0000FC20
			protected internal virtual int FreeSpaceFromRow(int spaceToFree)
			{
				return 0;
			}

			// Token: 0x06006F7E RID: 28542 RVA: 0x00198BF8 File Offset: 0x00196DF8
			protected virtual int Grow(int index, int growBy)
			{
				int result = 0;
				if (index >= 0 && index < this.Row.ControlsInternal.Count - 1)
				{
					ToolStripPanelCell toolStripPanelCell = (ToolStripPanelCell)this.Row.Cells[index];
					if (toolStripPanelCell.Visible)
					{
						result = toolStripPanelCell.Grow(growBy);
					}
				}
				return result;
			}

			// Token: 0x06006F7F RID: 28543 RVA: 0x00198C48 File Offset: 0x00196E48
			public ToolStripPanelCell GetNextVisibleCell(int index, bool forward)
			{
				if (forward)
				{
					for (int i = index; i < this.Row.Cells.Count; i++)
					{
						ToolStripPanelCell toolStripPanelCell = this.Row.Cells[i] as ToolStripPanelCell;
						if ((toolStripPanelCell.Visible || (this.owner.parent.Visible && toolStripPanelCell.ControlInDesignMode)) && toolStripPanelCell.ToolStripPanelRow == this.owner)
						{
							return toolStripPanelCell;
						}
					}
				}
				else
				{
					for (int j = index; j >= 0; j--)
					{
						ToolStripPanelCell toolStripPanelCell2 = this.Row.Cells[j] as ToolStripPanelCell;
						if ((toolStripPanelCell2.Visible || (this.owner.parent.Visible && toolStripPanelCell2.ControlInDesignMode)) && toolStripPanelCell2.ToolStripPanelRow == this.owner)
						{
							return toolStripPanelCell2;
						}
					}
				}
				return null;
			}

			// Token: 0x06006F80 RID: 28544 RVA: 0x00198D14 File Offset: 0x00196F14
			protected virtual int GrowControlsAfter(int index, int growBy)
			{
				if (growBy < 0)
				{
					return 0;
				}
				int num = growBy;
				for (int i = index + 1; i < this.Row.ControlsInternal.Count; i++)
				{
					int num2 = this.Grow(i, num);
					if (num2 >= 0)
					{
						num -= num2;
						if (num <= 0)
						{
							return growBy;
						}
					}
				}
				return growBy - num;
			}

			// Token: 0x06006F81 RID: 28545 RVA: 0x00198D60 File Offset: 0x00196F60
			protected virtual int GrowControlsBefore(int index, int growBy)
			{
				if (growBy < 0)
				{
					return 0;
				}
				int num = growBy;
				for (int i = index - 1; i >= 0; i--)
				{
					num -= this.Grow(i, num);
					if (num <= 0)
					{
						return growBy;
					}
				}
				return growBy - num;
			}

			// Token: 0x06006F82 RID: 28546 RVA: 0x000072B6 File Offset: 0x000054B6
			public virtual void MoveControl(ToolStrip movingControl, Point startClientLocation, Point endClientLocation)
			{
			}

			// Token: 0x06006F83 RID: 28547 RVA: 0x000072B6 File Offset: 0x000054B6
			public virtual void LeaveRow(ToolStrip toolStripToDrag)
			{
			}

			// Token: 0x06006F84 RID: 28548 RVA: 0x000072B6 File Offset: 0x000054B6
			public virtual void JoinRow(ToolStrip toolStripToDrag, Point locationToDrag)
			{
			}

			// Token: 0x06006F85 RID: 28549 RVA: 0x000072B6 File Offset: 0x000054B6
			protected internal virtual void OnControlAdded(Control c, int index)
			{
			}

			// Token: 0x06006F86 RID: 28550 RVA: 0x000072B6 File Offset: 0x000054B6
			protected internal virtual void OnControlRemoved(Control c, int index)
			{
			}

			// Token: 0x06006F87 RID: 28551 RVA: 0x000072B6 File Offset: 0x000054B6
			protected internal virtual void OnBoundsChanged(Rectangle oldBounds, Rectangle newBounds)
			{
			}

			// Token: 0x04004323 RID: 17187
			private FlowLayoutSettings flowLayoutSettings;

			// Token: 0x04004324 RID: 17188
			private ToolStripPanelRow owner;
		}

		// Token: 0x02000813 RID: 2067
		private class HorizontalRowManager : ToolStripPanelRow.ToolStripPanelRowManager
		{
			// Token: 0x06006F88 RID: 28552 RVA: 0x00198D97 File Offset: 0x00196F97
			public HorizontalRowManager(ToolStripPanelRow owner) : base(owner)
			{
				owner.SuspendLayout();
				base.FlowLayoutSettings.WrapContents = false;
				base.FlowLayoutSettings.FlowDirection = FlowDirection.LeftToRight;
				owner.ResumeLayout(false);
			}

			// Token: 0x1700185E RID: 6238
			// (get) Token: 0x06006F89 RID: 28553 RVA: 0x00198DC8 File Offset: 0x00196FC8
			public override Rectangle DisplayRectangle
			{
				get
				{
					Rectangle displayRectangle = ((IArrangedElement)base.Row).DisplayRectangle;
					if (base.ToolStripPanel != null)
					{
						Rectangle displayRectangle2 = base.ToolStripPanel.DisplayRectangle;
						if ((!base.ToolStripPanel.Visible || LayoutUtils.IsZeroWidthOrHeight(displayRectangle2)) && base.ToolStripPanel.ParentInternal != null)
						{
							displayRectangle.Width = base.ToolStripPanel.ParentInternal.DisplayRectangle.Width - (base.ToolStripPanel.Margin.Horizontal + base.ToolStripPanel.Padding.Horizontal) - base.Row.Margin.Horizontal;
						}
						else
						{
							displayRectangle.Width = displayRectangle2.Width - base.Row.Margin.Horizontal;
						}
					}
					return displayRectangle;
				}
			}

			// Token: 0x1700185F RID: 6239
			// (get) Token: 0x06006F8A RID: 28554 RVA: 0x00198E9C File Offset: 0x0019709C
			public override Rectangle DragBounds
			{
				get
				{
					Rectangle bounds = base.Row.Bounds;
					int num = base.ToolStripPanel.RowsInternal.IndexOf(base.Row);
					if (num > 0)
					{
						Rectangle bounds2 = base.ToolStripPanel.RowsInternal[num - 1].Bounds;
						int num2 = bounds2.Y + bounds2.Height - (bounds2.Height >> 2);
						bounds.Height += bounds.Y - num2;
						bounds.Y = num2;
					}
					if (num < base.ToolStripPanel.RowsInternal.Count - 1)
					{
						Rectangle bounds3 = base.ToolStripPanel.RowsInternal[num + 1].Bounds;
						bounds.Height += (bounds3.Height >> 2) + base.Row.Margin.Bottom + base.ToolStripPanel.RowsInternal[num + 1].Margin.Top;
					}
					bounds.Width += base.Row.Margin.Horizontal + base.ToolStripPanel.Padding.Horizontal + 5;
					bounds.X -= base.Row.Margin.Left + base.ToolStripPanel.Padding.Left + 4;
					return bounds;
				}
			}

			// Token: 0x06006F8B RID: 28555 RVA: 0x00199014 File Offset: 0x00197214
			public override bool CanMove(ToolStrip toolStripToDrag)
			{
				if (base.CanMove(toolStripToDrag))
				{
					Size sz = Size.Empty;
					for (int i = 0; i < base.Row.ControlsInternal.Count; i++)
					{
						sz += base.Row.GetMinimumSize(base.Row.ControlsInternal[i] as ToolStrip);
					}
					return (sz + base.Row.GetMinimumSize(toolStripToDrag)).Width < this.DisplayRectangle.Width;
				}
				return false;
			}

			// Token: 0x06006F8C RID: 28556 RVA: 0x001990A0 File Offset: 0x001972A0
			protected internal override int FreeSpaceFromRow(int spaceToFree)
			{
				int num = spaceToFree;
				if (spaceToFree > 0)
				{
					ToolStripPanelCell nextVisibleCell = base.GetNextVisibleCell(base.Row.Cells.Count - 1, false);
					if (nextVisibleCell == null)
					{
						return 0;
					}
					Padding margin = nextVisibleCell.Margin;
					if (margin.Left >= spaceToFree)
					{
						margin.Left -= spaceToFree;
						margin.Right = 0;
						spaceToFree = 0;
					}
					else
					{
						spaceToFree -= nextVisibleCell.Margin.Left;
						margin.Left = 0;
						margin.Right = 0;
					}
					nextVisibleCell.Margin = margin;
					spaceToFree -= this.MoveLeft(base.Row.Cells.Count - 1, spaceToFree);
					if (spaceToFree > 0)
					{
						spaceToFree -= nextVisibleCell.Shrink(spaceToFree);
					}
				}
				return num - Math.Max(0, spaceToFree);
			}

			// Token: 0x06006F8D RID: 28557 RVA: 0x00199160 File Offset: 0x00197360
			public override void MoveControl(ToolStrip movingControl, Point clientStartLocation, Point clientEndLocation)
			{
				if (base.Row.Locked)
				{
					return;
				}
				if (!this.DragBounds.Contains(clientEndLocation))
				{
					base.MoveControl(movingControl, clientStartLocation, clientEndLocation);
					return;
				}
				int index = base.Row.ControlsInternal.IndexOf(movingControl);
				int num = clientEndLocation.X - clientStartLocation.X;
				if (num < 0)
				{
					this.MoveLeft(index, num * -1);
					return;
				}
				this.MoveRight(index, num);
			}

			// Token: 0x06006F8E RID: 28558 RVA: 0x001991D4 File Offset: 0x001973D4
			private int MoveLeft(int index, int spaceToFree)
			{
				int num = 0;
				base.Row.SuspendLayout();
				try
				{
					if (spaceToFree == 0 || index < 0)
					{
						return 0;
					}
					for (int i = index; i >= 0; i--)
					{
						ToolStripPanelCell toolStripPanelCell = (ToolStripPanelCell)base.Row.Cells[i];
						if (toolStripPanelCell.Visible || toolStripPanelCell.ControlInDesignMode)
						{
							int num2 = spaceToFree - num;
							Padding margin = toolStripPanelCell.Margin;
							if (margin.Horizontal >= num2)
							{
								num += num2;
								margin.Left -= num2;
								margin.Right = 0;
								toolStripPanelCell.Margin = margin;
							}
							else
							{
								num += toolStripPanelCell.Margin.Horizontal;
								margin.Left = 0;
								margin.Right = 0;
								toolStripPanelCell.Margin = margin;
							}
							if (num >= spaceToFree)
							{
								if (index + 1 < base.Row.Cells.Count)
								{
									toolStripPanelCell = base.GetNextVisibleCell(index + 1, true);
									if (toolStripPanelCell != null)
									{
										margin = toolStripPanelCell.Margin;
										margin.Left += spaceToFree;
										toolStripPanelCell.Margin = margin;
									}
								}
								return spaceToFree;
							}
						}
					}
				}
				finally
				{
					base.Row.ResumeLayout(true);
				}
				return num;
			}

			// Token: 0x06006F8F RID: 28559 RVA: 0x00199310 File Offset: 0x00197510
			private int MoveRight(int index, int spaceToFree)
			{
				int num = 0;
				base.Row.SuspendLayout();
				try
				{
					if (spaceToFree == 0 || index < 0 || index >= base.Row.ControlsInternal.Count)
					{
						return 0;
					}
					int i = index + 1;
					while (i < base.Row.Cells.Count)
					{
						ToolStripPanelCell toolStripPanelCell = (ToolStripPanelCell)base.Row.Cells[i];
						if (toolStripPanelCell.Visible || toolStripPanelCell.ControlInDesignMode)
						{
							int num2 = spaceToFree - num;
							Padding margin = toolStripPanelCell.Margin;
							if (margin.Horizontal >= num2)
							{
								num += num2;
								margin.Left -= num2;
								margin.Right = 0;
								toolStripPanelCell.Margin = margin;
								break;
							}
							num += toolStripPanelCell.Margin.Horizontal;
							margin.Left = 0;
							margin.Right = 0;
							toolStripPanelCell.Margin = margin;
							break;
						}
						else
						{
							i++;
						}
					}
					if (base.Row.Cells.Count > 0 && spaceToFree > num)
					{
						ToolStripPanelCell nextVisibleCell = base.GetNextVisibleCell(base.Row.Cells.Count - 1, false);
						if (nextVisibleCell != null)
						{
							num += this.DisplayRectangle.Right - nextVisibleCell.Bounds.Right;
						}
						else
						{
							num += this.DisplayRectangle.Width;
						}
					}
					if (spaceToFree <= num)
					{
						ToolStripPanelCell toolStripPanelCell = base.GetNextVisibleCell(index, true);
						if (toolStripPanelCell == null)
						{
							toolStripPanelCell = (base.Row.Cells[index] as ToolStripPanelCell);
						}
						if (toolStripPanelCell != null)
						{
							Padding margin = toolStripPanelCell.Margin;
							margin.Left += spaceToFree;
							toolStripPanelCell.Margin = margin;
						}
						return spaceToFree;
					}
					for (int j = index + 1; j < base.Row.Cells.Count; j++)
					{
						ToolStripPanelCell toolStripPanelCell = (ToolStripPanelCell)base.Row.Cells[j];
						if (toolStripPanelCell.Visible || toolStripPanelCell.ControlInDesignMode)
						{
							int shrinkBy = spaceToFree - num;
							num += toolStripPanelCell.Shrink(shrinkBy);
							if (spaceToFree >= num)
							{
								base.Row.ResumeLayout(true);
								return spaceToFree;
							}
						}
					}
					if (base.Row.Cells.Count == 1)
					{
						ToolStripPanelCell toolStripPanelCell = base.GetNextVisibleCell(index, true);
						if (toolStripPanelCell != null)
						{
							Padding margin = toolStripPanelCell.Margin;
							margin.Left += num;
							toolStripPanelCell.Margin = margin;
						}
					}
				}
				finally
				{
					base.Row.ResumeLayout(true);
				}
				return num;
			}

			// Token: 0x06006F90 RID: 28560 RVA: 0x00199594 File Offset: 0x00197794
			public override void LeaveRow(ToolStrip toolStripToDrag)
			{
				base.Row.SuspendLayout();
				int num = base.Row.ControlsInternal.IndexOf(toolStripToDrag);
				if (num >= 0)
				{
					if (num < base.Row.ControlsInternal.Count - 1)
					{
						ToolStripPanelCell toolStripPanelCell = (ToolStripPanelCell)base.Row.Cells[num];
						if (toolStripPanelCell.Visible)
						{
							int num2 = toolStripPanelCell.Margin.Horizontal + toolStripPanelCell.Bounds.Width;
							ToolStripPanelCell nextVisibleCell = base.GetNextVisibleCell(num + 1, true);
							if (nextVisibleCell != null)
							{
								Padding margin = nextVisibleCell.Margin;
								margin.Left += num2;
								nextVisibleCell.Margin = margin;
							}
						}
					}
					((IList)base.Row.Cells).RemoveAt(num);
				}
				base.Row.ResumeLayout(true);
			}

			// Token: 0x06006F91 RID: 28561 RVA: 0x000072B6 File Offset: 0x000054B6
			protected internal override void OnControlAdded(Control control, int index)
			{
			}

			// Token: 0x06006F92 RID: 28562 RVA: 0x000072B6 File Offset: 0x000054B6
			protected internal override void OnControlRemoved(Control control, int index)
			{
			}

			// Token: 0x06006F93 RID: 28563 RVA: 0x00199664 File Offset: 0x00197864
			public override void JoinRow(ToolStrip toolStripToDrag, Point locationToDrag)
			{
				if (!base.Row.ControlsInternal.Contains(toolStripToDrag))
				{
					base.Row.SuspendLayout();
					try
					{
						if (base.Row.ControlsInternal.Count > 0)
						{
							int i;
							for (i = 0; i < base.Row.Cells.Count; i++)
							{
								ToolStripPanelCell toolStripPanelCell = base.Row.Cells[i] as ToolStripPanelCell;
								if ((toolStripPanelCell.Visible || toolStripPanelCell.ControlInDesignMode) && (base.Row.Cells[i].Bounds.Contains(locationToDrag) || base.Row.Cells[i].Bounds.X >= locationToDrag.X))
								{
									break;
								}
							}
							Control control = base.Row.ControlsInternal[i];
							if (i < base.Row.ControlsInternal.Count)
							{
								base.Row.ControlsInternal.Insert(i, toolStripToDrag);
							}
							else
							{
								base.Row.ControlsInternal.Add(toolStripToDrag);
							}
							int num = toolStripToDrag.AutoSize ? toolStripToDrag.PreferredSize.Width : toolStripToDrag.Width;
							int num2 = num;
							if (i == 0)
							{
								num2 += locationToDrag.X;
							}
							int num3 = 0;
							if (i < base.Row.ControlsInternal.Count - 1)
							{
								ToolStripPanelCell toolStripPanelCell2 = (ToolStripPanelCell)base.Row.Cells[i + 1];
								Padding margin = toolStripPanelCell2.Margin;
								if (margin.Left > num2)
								{
									margin.Left -= num2;
									toolStripPanelCell2.Margin = margin;
									num3 = num2;
								}
								else
								{
									num3 = this.MoveRight(i + 1, num2 - num3);
									if (num3 > 0)
									{
										margin = toolStripPanelCell2.Margin;
										margin.Left = Math.Max(0, margin.Left - num3);
										toolStripPanelCell2.Margin = margin;
									}
								}
							}
							else
							{
								ToolStripPanelCell nextVisibleCell = base.GetNextVisibleCell(base.Row.Cells.Count - 2, false);
								ToolStripPanelCell nextVisibleCell2 = base.GetNextVisibleCell(base.Row.Cells.Count - 1, false);
								if (nextVisibleCell != null && nextVisibleCell2 != null)
								{
									Padding margin2 = nextVisibleCell2.Margin;
									margin2.Left = Math.Max(0, locationToDrag.X - nextVisibleCell.Bounds.Right);
									nextVisibleCell2.Margin = margin2;
									num3 = num2;
								}
							}
							if (num3 < num2 && i > 0)
							{
								num3 = this.MoveLeft(i - 1, num2 - num3);
							}
							if (i == 0 && num3 - num > 0)
							{
								ToolStripPanelCell toolStripPanelCell3 = base.Row.Cells[i] as ToolStripPanelCell;
								Padding margin3 = toolStripPanelCell3.Margin;
								margin3.Left = num3 - num;
								toolStripPanelCell3.Margin = margin3;
							}
						}
						else
						{
							base.Row.ControlsInternal.Add(toolStripToDrag);
							if (base.Row.Cells.Count > 0 || toolStripToDrag.IsInDesignMode)
							{
								ToolStripPanelCell toolStripPanelCell4 = base.GetNextVisibleCell(base.Row.Cells.Count - 1, false);
								if (toolStripPanelCell4 == null && toolStripToDrag.IsInDesignMode)
								{
									toolStripPanelCell4 = (ToolStripPanelCell)base.Row.Cells[base.Row.Cells.Count - 1];
								}
								if (toolStripPanelCell4 != null)
								{
									Padding margin4 = toolStripPanelCell4.Margin;
									margin4.Left = Math.Max(0, locationToDrag.X - base.Row.Margin.Left);
									toolStripPanelCell4.Margin = margin4;
								}
							}
						}
					}
					finally
					{
						base.Row.ResumeLayout(true);
					}
				}
			}

			// Token: 0x06006F94 RID: 28564 RVA: 0x00199A24 File Offset: 0x00197C24
			protected internal override void OnBoundsChanged(Rectangle oldBounds, Rectangle newBounds)
			{
				base.OnBoundsChanged(oldBounds, newBounds);
			}

			// Token: 0x04004325 RID: 17189
			private const int DRAG_BOUNDS_INFLATE = 4;
		}

		// Token: 0x02000814 RID: 2068
		private class VerticalRowManager : ToolStripPanelRow.ToolStripPanelRowManager
		{
			// Token: 0x06006F95 RID: 28565 RVA: 0x00199A2E File Offset: 0x00197C2E
			public VerticalRowManager(ToolStripPanelRow owner) : base(owner)
			{
				owner.SuspendLayout();
				base.FlowLayoutSettings.WrapContents = false;
				base.FlowLayoutSettings.FlowDirection = FlowDirection.TopDown;
				owner.ResumeLayout(false);
			}

			// Token: 0x17001860 RID: 6240
			// (get) Token: 0x06006F96 RID: 28566 RVA: 0x00199A5C File Offset: 0x00197C5C
			public override Rectangle DisplayRectangle
			{
				get
				{
					Rectangle displayRectangle = ((IArrangedElement)base.Row).DisplayRectangle;
					if (base.ToolStripPanel != null)
					{
						Rectangle displayRectangle2 = base.ToolStripPanel.DisplayRectangle;
						if ((!base.ToolStripPanel.Visible || LayoutUtils.IsZeroWidthOrHeight(displayRectangle2)) && base.ToolStripPanel.ParentInternal != null)
						{
							displayRectangle.Height = base.ToolStripPanel.ParentInternal.DisplayRectangle.Height - (base.ToolStripPanel.Margin.Vertical + base.ToolStripPanel.Padding.Vertical) - base.Row.Margin.Vertical;
						}
						else
						{
							displayRectangle.Height = displayRectangle2.Height - base.Row.Margin.Vertical;
						}
					}
					return displayRectangle;
				}
			}

			// Token: 0x17001861 RID: 6241
			// (get) Token: 0x06006F97 RID: 28567 RVA: 0x00199B30 File Offset: 0x00197D30
			public override Rectangle DragBounds
			{
				get
				{
					Rectangle bounds = base.Row.Bounds;
					int num = base.ToolStripPanel.RowsInternal.IndexOf(base.Row);
					if (num > 0)
					{
						Rectangle bounds2 = base.ToolStripPanel.RowsInternal[num - 1].Bounds;
						int num2 = bounds2.X + bounds2.Width - (bounds2.Width >> 2);
						bounds.Width += bounds.X - num2;
						bounds.X = num2;
					}
					if (num < base.ToolStripPanel.RowsInternal.Count - 1)
					{
						Rectangle bounds3 = base.ToolStripPanel.RowsInternal[num + 1].Bounds;
						bounds.Width += (bounds3.Width >> 2) + base.Row.Margin.Right + base.ToolStripPanel.RowsInternal[num + 1].Margin.Left;
					}
					bounds.Height += base.Row.Margin.Vertical + base.ToolStripPanel.Padding.Vertical + 5;
					bounds.Y -= base.Row.Margin.Top + base.ToolStripPanel.Padding.Top + 4;
					return bounds;
				}
			}

			// Token: 0x06006F98 RID: 28568 RVA: 0x00199CA8 File Offset: 0x00197EA8
			public override bool CanMove(ToolStrip toolStripToDrag)
			{
				if (base.CanMove(toolStripToDrag))
				{
					Size sz = Size.Empty;
					for (int i = 0; i < base.Row.ControlsInternal.Count; i++)
					{
						sz += base.Row.GetMinimumSize(base.Row.ControlsInternal[i] as ToolStrip);
					}
					return (sz + base.Row.GetMinimumSize(toolStripToDrag)).Height < this.DisplayRectangle.Height;
				}
				return false;
			}

			// Token: 0x06006F99 RID: 28569 RVA: 0x00199D34 File Offset: 0x00197F34
			protected internal override int FreeSpaceFromRow(int spaceToFree)
			{
				int num = spaceToFree;
				if (spaceToFree > 0)
				{
					ToolStripPanelCell nextVisibleCell = base.GetNextVisibleCell(base.Row.Cells.Count - 1, false);
					if (nextVisibleCell == null)
					{
						return 0;
					}
					Padding margin = nextVisibleCell.Margin;
					if (margin.Top >= spaceToFree)
					{
						margin.Top -= spaceToFree;
						margin.Bottom = 0;
						spaceToFree = 0;
					}
					else
					{
						spaceToFree -= nextVisibleCell.Margin.Top;
						margin.Top = 0;
						margin.Bottom = 0;
					}
					nextVisibleCell.Margin = margin;
					spaceToFree -= this.MoveUp(base.Row.Cells.Count - 1, spaceToFree);
					if (spaceToFree > 0)
					{
						spaceToFree -= nextVisibleCell.Shrink(spaceToFree);
					}
				}
				return num - Math.Max(0, spaceToFree);
			}

			// Token: 0x06006F9A RID: 28570 RVA: 0x00199DF4 File Offset: 0x00197FF4
			public override void MoveControl(ToolStrip movingControl, Point clientStartLocation, Point clientEndLocation)
			{
				if (base.Row.Locked)
				{
					return;
				}
				if (!this.DragBounds.Contains(clientEndLocation))
				{
					base.MoveControl(movingControl, clientStartLocation, clientEndLocation);
					return;
				}
				int index = base.Row.ControlsInternal.IndexOf(movingControl);
				int num = clientEndLocation.Y - clientStartLocation.Y;
				if (num < 0)
				{
					this.MoveUp(index, num * -1);
					return;
				}
				this.MoveDown(index, num);
			}

			// Token: 0x06006F9B RID: 28571 RVA: 0x00199E68 File Offset: 0x00198068
			private int MoveUp(int index, int spaceToFree)
			{
				int num = 0;
				base.Row.SuspendLayout();
				try
				{
					if (spaceToFree == 0 || index < 0)
					{
						return 0;
					}
					for (int i = index; i >= 0; i--)
					{
						ToolStripPanelCell toolStripPanelCell = (ToolStripPanelCell)base.Row.Cells[i];
						if (toolStripPanelCell.Visible || toolStripPanelCell.ControlInDesignMode)
						{
							int num2 = spaceToFree - num;
							Padding margin = toolStripPanelCell.Margin;
							if (margin.Vertical >= num2)
							{
								num += num2;
								margin.Top -= num2;
								margin.Bottom = 0;
								toolStripPanelCell.Margin = margin;
							}
							else
							{
								num += toolStripPanelCell.Margin.Vertical;
								margin.Top = 0;
								margin.Bottom = 0;
								toolStripPanelCell.Margin = margin;
							}
							if (num >= spaceToFree)
							{
								if (index + 1 < base.Row.Cells.Count)
								{
									toolStripPanelCell = base.GetNextVisibleCell(index + 1, true);
									if (toolStripPanelCell != null)
									{
										margin = toolStripPanelCell.Margin;
										margin.Top += spaceToFree;
										toolStripPanelCell.Margin = margin;
									}
								}
								return spaceToFree;
							}
						}
					}
				}
				finally
				{
					base.Row.ResumeLayout(true);
				}
				return num;
			}

			// Token: 0x06006F9C RID: 28572 RVA: 0x00199FA4 File Offset: 0x001981A4
			private int MoveDown(int index, int spaceToFree)
			{
				int num = 0;
				base.Row.SuspendLayout();
				try
				{
					if (spaceToFree == 0 || index < 0 || index >= base.Row.ControlsInternal.Count)
					{
						return 0;
					}
					int i = index + 1;
					while (i < base.Row.Cells.Count)
					{
						ToolStripPanelCell toolStripPanelCell = (ToolStripPanelCell)base.Row.Cells[i];
						if (toolStripPanelCell.Visible || toolStripPanelCell.ControlInDesignMode)
						{
							int num2 = spaceToFree - num;
							Padding margin = toolStripPanelCell.Margin;
							if (margin.Vertical >= num2)
							{
								num += num2;
								margin.Top -= num2;
								margin.Bottom = 0;
								toolStripPanelCell.Margin = margin;
								break;
							}
							num += toolStripPanelCell.Margin.Vertical;
							margin.Top = 0;
							margin.Bottom = 0;
							toolStripPanelCell.Margin = margin;
							break;
						}
						else
						{
							i++;
						}
					}
					if (base.Row.Cells.Count > 0 && spaceToFree > num)
					{
						ToolStripPanelCell nextVisibleCell = base.GetNextVisibleCell(base.Row.Cells.Count - 1, false);
						if (nextVisibleCell != null)
						{
							num += this.DisplayRectangle.Bottom - nextVisibleCell.Bounds.Bottom;
						}
						else
						{
							num += this.DisplayRectangle.Height;
						}
					}
					if (spaceToFree <= num)
					{
						ToolStripPanelCell toolStripPanelCell = (ToolStripPanelCell)base.Row.Cells[index];
						Padding margin = toolStripPanelCell.Margin;
						margin.Top += spaceToFree;
						toolStripPanelCell.Margin = margin;
						return spaceToFree;
					}
					for (int j = index + 1; j < base.Row.Cells.Count; j++)
					{
						ToolStripPanelCell toolStripPanelCell = (ToolStripPanelCell)base.Row.Cells[j];
						if (toolStripPanelCell.Visible || toolStripPanelCell.ControlInDesignMode)
						{
							int shrinkBy = spaceToFree - num;
							num += toolStripPanelCell.Shrink(shrinkBy);
							if (spaceToFree >= num)
							{
								base.Row.ResumeLayout(true);
								return spaceToFree;
							}
						}
					}
					if (base.Row.Cells.Count == 1)
					{
						ToolStripPanelCell toolStripPanelCell = base.GetNextVisibleCell(index, true);
						if (toolStripPanelCell != null)
						{
							Padding margin = toolStripPanelCell.Margin;
							margin.Top += num;
							toolStripPanelCell.Margin = margin;
						}
					}
				}
				finally
				{
					base.Row.ResumeLayout(true);
				}
				return spaceToFree - num;
			}

			// Token: 0x06006F9D RID: 28573 RVA: 0x0019A224 File Offset: 0x00198424
			protected internal override void OnBoundsChanged(Rectangle oldBounds, Rectangle newBounds)
			{
				base.OnBoundsChanged(oldBounds, newBounds);
				if (base.Row.Cells.Count > 0)
				{
					ToolStripPanelCell nextVisibleCell = base.GetNextVisibleCell(base.Row.Cells.Count - 1, false);
					int num = (nextVisibleCell != null) ? (nextVisibleCell.Bounds.Bottom - newBounds.Height) : 0;
					if (num > 0)
					{
						ToolStripPanelCell nextVisibleCell2 = base.GetNextVisibleCell(base.Row.Cells.Count - 1, false);
						Padding margin = nextVisibleCell2.Margin;
						if (margin.Top >= num)
						{
							margin.Top -= num;
							margin.Bottom = 0;
							nextVisibleCell2.Margin = margin;
							num = 0;
						}
						else
						{
							num -= nextVisibleCell2.Margin.Top;
							margin.Top = 0;
							margin.Bottom = 0;
							nextVisibleCell2.Margin = margin;
						}
						num -= nextVisibleCell2.Shrink(num);
						this.MoveUp(base.Row.Cells.Count - 1, num);
					}
				}
			}

			// Token: 0x06006F9E RID: 28574 RVA: 0x000072B6 File Offset: 0x000054B6
			protected internal override void OnControlRemoved(Control c, int index)
			{
			}

			// Token: 0x06006F9F RID: 28575 RVA: 0x000072B6 File Offset: 0x000054B6
			protected internal override void OnControlAdded(Control control, int index)
			{
			}

			// Token: 0x06006FA0 RID: 28576 RVA: 0x0019A32C File Offset: 0x0019852C
			public override void JoinRow(ToolStrip toolStripToDrag, Point locationToDrag)
			{
				if (!base.Row.ControlsInternal.Contains(toolStripToDrag))
				{
					base.Row.SuspendLayout();
					try
					{
						if (base.Row.ControlsInternal.Count > 0)
						{
							int i;
							for (i = 0; i < base.Row.Cells.Count; i++)
							{
								ToolStripPanelCell toolStripPanelCell = base.Row.Cells[i] as ToolStripPanelCell;
								if ((toolStripPanelCell.Visible || toolStripPanelCell.ControlInDesignMode) && (toolStripPanelCell.Bounds.Contains(locationToDrag) || toolStripPanelCell.Bounds.Y >= locationToDrag.Y))
								{
									break;
								}
							}
							Control control = base.Row.ControlsInternal[i];
							if (i < base.Row.ControlsInternal.Count)
							{
								base.Row.ControlsInternal.Insert(i, toolStripToDrag);
							}
							else
							{
								base.Row.ControlsInternal.Add(toolStripToDrag);
							}
							int num = toolStripToDrag.AutoSize ? toolStripToDrag.PreferredSize.Height : toolStripToDrag.Height;
							int num2 = num;
							if (i == 0)
							{
								num2 += locationToDrag.Y;
							}
							int num3 = 0;
							if (i < base.Row.ControlsInternal.Count - 1)
							{
								ToolStripPanelCell nextVisibleCell = base.GetNextVisibleCell(i + 1, true);
								if (nextVisibleCell != null)
								{
									Padding margin = nextVisibleCell.Margin;
									if (margin.Top > num2)
									{
										margin.Top -= num2;
										nextVisibleCell.Margin = margin;
										num3 = num2;
									}
									else
									{
										num3 = this.MoveDown(i + 1, num2 - num3);
										if (num3 > 0)
										{
											margin = nextVisibleCell.Margin;
											margin.Top -= num3;
											nextVisibleCell.Margin = margin;
										}
									}
								}
							}
							else
							{
								ToolStripPanelCell nextVisibleCell2 = base.GetNextVisibleCell(base.Row.Cells.Count - 2, false);
								ToolStripPanelCell nextVisibleCell3 = base.GetNextVisibleCell(base.Row.Cells.Count - 1, false);
								if (nextVisibleCell2 != null && nextVisibleCell3 != null)
								{
									Padding margin2 = nextVisibleCell3.Margin;
									margin2.Top = Math.Max(0, locationToDrag.Y - nextVisibleCell2.Bounds.Bottom);
									nextVisibleCell3.Margin = margin2;
									num3 = num2;
								}
							}
							if (num3 < num2 && i > 0)
							{
								num3 = this.MoveUp(i - 1, num2 - num3);
							}
							if (i == 0 && num3 - num > 0)
							{
								ToolStripPanelCell toolStripPanelCell2 = base.Row.Cells[i] as ToolStripPanelCell;
								Padding margin3 = toolStripPanelCell2.Margin;
								margin3.Top = num3 - num;
								toolStripPanelCell2.Margin = margin3;
							}
						}
						else
						{
							base.Row.ControlsInternal.Add(toolStripToDrag);
							if (base.Row.Cells.Count > 0)
							{
								ToolStripPanelCell nextVisibleCell4 = base.GetNextVisibleCell(base.Row.Cells.Count - 1, false);
								if (nextVisibleCell4 != null)
								{
									Padding margin4 = nextVisibleCell4.Margin;
									margin4.Top = Math.Max(0, locationToDrag.Y - base.Row.Margin.Top);
									nextVisibleCell4.Margin = margin4;
								}
							}
						}
					}
					finally
					{
						base.Row.ResumeLayout(true);
					}
				}
			}

			// Token: 0x06006FA1 RID: 28577 RVA: 0x0019A67C File Offset: 0x0019887C
			public override void LeaveRow(ToolStrip toolStripToDrag)
			{
				base.Row.SuspendLayout();
				int num = base.Row.ControlsInternal.IndexOf(toolStripToDrag);
				if (num >= 0)
				{
					if (num < base.Row.ControlsInternal.Count - 1)
					{
						ToolStripPanelCell toolStripPanelCell = (ToolStripPanelCell)base.Row.Cells[num];
						if (toolStripPanelCell.Visible)
						{
							int num2 = toolStripPanelCell.Margin.Vertical + toolStripPanelCell.Bounds.Height;
							ToolStripPanelCell nextVisibleCell = base.GetNextVisibleCell(num + 1, true);
							if (nextVisibleCell != null)
							{
								Padding margin = nextVisibleCell.Margin;
								margin.Top += num2;
								nextVisibleCell.Margin = margin;
							}
						}
					}
					((IList)base.Row.Cells).RemoveAt(num);
				}
				base.Row.ResumeLayout(true);
			}

			// Token: 0x04004326 RID: 17190
			private const int DRAG_BOUNDS_INFLATE = 4;
		}

		// Token: 0x02000815 RID: 2069
		internal class ToolStripPanelRowControlCollection : ArrangedElementCollection, IList, ICollection, IEnumerable
		{
			// Token: 0x06006FA2 RID: 28578 RVA: 0x0019A74B File Offset: 0x0019894B
			public ToolStripPanelRowControlCollection(ToolStripPanelRow owner)
			{
				this.owner = owner;
			}

			// Token: 0x06006FA3 RID: 28579 RVA: 0x0019A75A File Offset: 0x0019895A
			public ToolStripPanelRowControlCollection(ToolStripPanelRow owner, Control[] value)
			{
				this.owner = owner;
				this.AddRange(value);
			}

			// Token: 0x17001862 RID: 6242
			public virtual Control this[int index]
			{
				get
				{
					return this.GetControl(index);
				}
			}

			// Token: 0x17001863 RID: 6243
			// (get) Token: 0x06006FA5 RID: 28581 RVA: 0x0019A779 File Offset: 0x00198979
			public ArrangedElementCollection Cells
			{
				get
				{
					if (this.cellCollection == null)
					{
						this.cellCollection = new ArrangedElementCollection(base.InnerList);
					}
					return this.cellCollection;
				}
			}

			// Token: 0x17001864 RID: 6244
			// (get) Token: 0x06006FA6 RID: 28582 RVA: 0x0019A79A File Offset: 0x0019899A
			public ToolStripPanel ToolStripPanel
			{
				get
				{
					return this.owner.ToolStripPanel;
				}
			}

			// Token: 0x06006FA7 RID: 28583 RVA: 0x0019A7A8 File Offset: 0x001989A8
			[EditorBrowsable(EditorBrowsableState.Never)]
			public int Add(Control value)
			{
				ISupportToolStripPanel supportToolStripPanel = value as ISupportToolStripPanel;
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (supportToolStripPanel == null)
				{
					throw new NotSupportedException(SR.GetString("TypedControlCollectionShouldBeOfType", new object[]
					{
						typeof(ToolStrip).Name
					}));
				}
				int num = base.InnerList.Add(supportToolStripPanel.ToolStripPanelCell);
				this.OnAdd(supportToolStripPanel, num);
				return num;
			}

			// Token: 0x06006FA8 RID: 28584 RVA: 0x0019A810 File Offset: 0x00198A10
			[EditorBrowsable(EditorBrowsableState.Never)]
			public void AddRange(Control[] value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				ToolStripPanel toolStripPanel = this.ToolStripPanel;
				if (toolStripPanel != null)
				{
					toolStripPanel.SuspendLayout();
				}
				try
				{
					for (int i = 0; i < value.Length; i++)
					{
						this.Add(value[i]);
					}
				}
				finally
				{
					if (toolStripPanel != null)
					{
						toolStripPanel.ResumeLayout();
					}
				}
			}

			// Token: 0x06006FA9 RID: 28585 RVA: 0x0019A870 File Offset: 0x00198A70
			public bool Contains(Control value)
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (this.GetControl(i) == value)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06006FAA RID: 28586 RVA: 0x0019A89C File Offset: 0x00198A9C
			public virtual void Clear()
			{
				if (this.owner != null)
				{
					this.ToolStripPanel.SuspendLayout();
				}
				try
				{
					while (this.Count != 0)
					{
						this.RemoveAt(this.Count - 1);
					}
				}
				finally
				{
					if (this.owner != null)
					{
						this.ToolStripPanel.ResumeLayout();
					}
				}
			}

			// Token: 0x06006FAB RID: 28587 RVA: 0x0019A8FC File Offset: 0x00198AFC
			public override IEnumerator GetEnumerator()
			{
				return new ToolStripPanelRow.ToolStripPanelRowControlCollection.ToolStripPanelCellToControlEnumerator(base.InnerList);
			}

			// Token: 0x06006FAC RID: 28588 RVA: 0x0019A90C File Offset: 0x00198B0C
			private Control GetControl(int index)
			{
				Control result = null;
				if (index < this.Count && index >= 0)
				{
					ToolStripPanelCell toolStripPanelCell = (ToolStripPanelCell)base.InnerList[index];
					result = ((toolStripPanelCell != null) ? toolStripPanelCell.Control : null);
				}
				return result;
			}

			// Token: 0x06006FAD RID: 28589 RVA: 0x0019A94C File Offset: 0x00198B4C
			private int IndexOfControl(Control c)
			{
				for (int i = 0; i < this.Count; i++)
				{
					ToolStripPanelCell toolStripPanelCell = (ToolStripPanelCell)base.InnerList[i];
					if (toolStripPanelCell.Control == c)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06006FAE RID: 28590 RVA: 0x0019A988 File Offset: 0x00198B88
			void IList.Clear()
			{
				this.Clear();
			}

			// Token: 0x17001865 RID: 6245
			// (get) Token: 0x06006FAF RID: 28591 RVA: 0x0011CD5C File Offset: 0x0011AF5C
			bool IList.IsFixedSize
			{
				get
				{
					return base.InnerList.IsFixedSize;
				}
			}

			// Token: 0x06006FB0 RID: 28592 RVA: 0x0011CAE8 File Offset: 0x0011ACE8
			bool IList.Contains(object value)
			{
				return base.InnerList.Contains(value);
			}

			// Token: 0x17001866 RID: 6246
			// (get) Token: 0x06006FB1 RID: 28593 RVA: 0x0014D7A3 File Offset: 0x0014B9A3
			bool IList.IsReadOnly
			{
				get
				{
					return base.InnerList.IsReadOnly;
				}
			}

			// Token: 0x06006FB2 RID: 28594 RVA: 0x0019A990 File Offset: 0x00198B90
			void IList.RemoveAt(int index)
			{
				this.RemoveAt(index);
			}

			// Token: 0x06006FB3 RID: 28595 RVA: 0x0019A999 File Offset: 0x00198B99
			void IList.Remove(object value)
			{
				this.Remove(value as Control);
			}

			// Token: 0x06006FB4 RID: 28596 RVA: 0x0019A9A7 File Offset: 0x00198BA7
			int IList.Add(object value)
			{
				return this.Add(value as Control);
			}

			// Token: 0x06006FB5 RID: 28597 RVA: 0x0019A9B5 File Offset: 0x00198BB5
			int IList.IndexOf(object value)
			{
				return this.IndexOf(value as Control);
			}

			// Token: 0x06006FB6 RID: 28598 RVA: 0x0019A9C3 File Offset: 0x00198BC3
			void IList.Insert(int index, object value)
			{
				this.Insert(index, value as Control);
			}

			// Token: 0x06006FB7 RID: 28599 RVA: 0x0019A9D4 File Offset: 0x00198BD4
			public int IndexOf(Control value)
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (this.GetControl(i) == value)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06006FB8 RID: 28600 RVA: 0x0019AA00 File Offset: 0x00198C00
			[EditorBrowsable(EditorBrowsableState.Never)]
			public void Insert(int index, Control value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				ISupportToolStripPanel supportToolStripPanel = value as ISupportToolStripPanel;
				if (supportToolStripPanel == null)
				{
					throw new NotSupportedException(SR.GetString("TypedControlCollectionShouldBeOfType", new object[]
					{
						typeof(ToolStrip).Name
					}));
				}
				base.InnerList.Insert(index, supportToolStripPanel.ToolStripPanelCell);
				this.OnAdd(supportToolStripPanel, index);
			}

			// Token: 0x06006FB9 RID: 28601 RVA: 0x0019AA68 File Offset: 0x00198C68
			private void OnAfterRemove(Control control, int index)
			{
				if (this.owner != null)
				{
					using (new LayoutTransaction(this.ToolStripPanel, control, PropertyNames.Parent))
					{
						this.owner.ToolStripPanel.Controls.Remove(control);
						this.owner.OnControlRemoved(control, index);
					}
				}
			}

			// Token: 0x06006FBA RID: 28602 RVA: 0x0019AAD0 File Offset: 0x00198CD0
			private void OnAdd(ISupportToolStripPanel controlToBeDragged, int index)
			{
				if (this.owner != null)
				{
					LayoutTransaction layoutTransaction = null;
					if (this.ToolStripPanel != null && this.ToolStripPanel.ParentInternal != null)
					{
						layoutTransaction = new LayoutTransaction(this.ToolStripPanel, this.ToolStripPanel.ParentInternal, PropertyNames.Parent);
					}
					try
					{
						if (controlToBeDragged != null)
						{
							controlToBeDragged.ToolStripPanelRow = this.owner;
							Control control = controlToBeDragged as Control;
							if (control != null)
							{
								control.ParentInternal = this.owner.ToolStripPanel;
								this.owner.OnControlAdded(control, index);
							}
						}
					}
					finally
					{
						if (layoutTransaction != null)
						{
							layoutTransaction.Dispose();
						}
					}
				}
			}

			// Token: 0x06006FBB RID: 28603 RVA: 0x0019AB6C File Offset: 0x00198D6C
			[EditorBrowsable(EditorBrowsableState.Never)]
			public void Remove(Control value)
			{
				int index = this.IndexOfControl(value);
				this.RemoveAt(index);
			}

			// Token: 0x06006FBC RID: 28604 RVA: 0x0019AB88 File Offset: 0x00198D88
			[EditorBrowsable(EditorBrowsableState.Never)]
			public void RemoveAt(int index)
			{
				if (index >= 0 && index < this.Count)
				{
					Control control = this.GetControl(index);
					ToolStripPanelCell toolStripPanelCell = base.InnerList[index] as ToolStripPanelCell;
					base.InnerList.RemoveAt(index);
					this.OnAfterRemove(control, index);
				}
			}

			// Token: 0x06006FBD RID: 28605 RVA: 0x0019ABD0 File Offset: 0x00198DD0
			[EditorBrowsable(EditorBrowsableState.Never)]
			public void CopyTo(Control[] array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (index >= array.Length || base.InnerList.Count > array.Length - index)
				{
					throw new ArgumentException(SR.GetString("ToolStripPanelRowControlCollectionIncorrectIndexLength"));
				}
				for (int i = 0; i < base.InnerList.Count; i++)
				{
					array[index++] = this.GetControl(i);
				}
			}

			// Token: 0x04004327 RID: 17191
			private ToolStripPanelRow owner;

			// Token: 0x04004328 RID: 17192
			private ArrangedElementCollection cellCollection;

			// Token: 0x020008CF RID: 2255
			private class ToolStripPanelCellToControlEnumerator : IEnumerator, ICloneable
			{
				// Token: 0x06007325 RID: 29477 RVA: 0x001A55FC File Offset: 0x001A37FC
				internal ToolStripPanelCellToControlEnumerator(ArrayList list)
				{
					this.arrayListEnumerator = ((IEnumerable)list).GetEnumerator();
				}

				// Token: 0x1700193E RID: 6462
				// (get) Token: 0x06007326 RID: 29478 RVA: 0x001A5610 File Offset: 0x001A3810
				public virtual object Current
				{
					get
					{
						ToolStripPanelCell toolStripPanelCell = this.arrayListEnumerator.Current as ToolStripPanelCell;
						if (toolStripPanelCell != null)
						{
							return toolStripPanelCell.Control;
						}
						return null;
					}
				}

				// Token: 0x06007327 RID: 29479 RVA: 0x001A5639 File Offset: 0x001A3839
				public object Clone()
				{
					return base.MemberwiseClone();
				}

				// Token: 0x06007328 RID: 29480 RVA: 0x001A5641 File Offset: 0x001A3841
				public virtual bool MoveNext()
				{
					return this.arrayListEnumerator.MoveNext();
				}

				// Token: 0x06007329 RID: 29481 RVA: 0x001A564E File Offset: 0x001A384E
				public virtual void Reset()
				{
					this.arrayListEnumerator.Reset();
				}

				// Token: 0x0400455D RID: 17757
				private IEnumerator arrayListEnumerator;
			}
		}
	}
}
