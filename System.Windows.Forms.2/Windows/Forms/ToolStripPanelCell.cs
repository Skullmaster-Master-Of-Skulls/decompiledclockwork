using System;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x020003EF RID: 1007
	internal class ToolStripPanelCell : ArrangedElement
	{
		// Token: 0x06004531 RID: 17713 RVA: 0x00122BF1 File Offset: 0x00120DF1
		public ToolStripPanelCell(Control control) : this(null, control)
		{
		}

		// Token: 0x06004532 RID: 17714 RVA: 0x00122BFC File Offset: 0x00120DFC
		public ToolStripPanelCell(ToolStripPanelRow parent, Control control)
		{
			this.ToolStripPanelRow = parent;
			this._wrappedToolStrip = (control as ToolStrip);
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (this._wrappedToolStrip == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("TypedControlCollectionShouldBeOfType", new object[]
				{
					typeof(ToolStrip).Name
				}), new object[0]), control.GetType().Name);
			}
			CommonProperties.SetAutoSize(this, true);
			this._wrappedToolStrip.LocationChanging += this.OnToolStripLocationChanging;
			this._wrappedToolStrip.VisibleChanged += this.OnToolStripVisibleChanged;
		}

		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x06004533 RID: 17715 RVA: 0x00122CC6 File Offset: 0x00120EC6
		// (set) Token: 0x06004534 RID: 17716 RVA: 0x00122CCE File Offset: 0x00120ECE
		public Rectangle CachedBounds
		{
			get
			{
				return this.cachedBounds;
			}
			set
			{
				this.cachedBounds = value;
			}
		}

		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x06004535 RID: 17717 RVA: 0x00122CD7 File Offset: 0x00120ED7
		public Control Control
		{
			get
			{
				return this._wrappedToolStrip;
			}
		}

		// Token: 0x170010FC RID: 4348
		// (get) Token: 0x06004536 RID: 17718 RVA: 0x00122CDF File Offset: 0x00120EDF
		public bool ControlInDesignMode
		{
			get
			{
				return this._wrappedToolStrip != null && this._wrappedToolStrip.IsInDesignMode;
			}
		}

		// Token: 0x170010FD RID: 4349
		// (get) Token: 0x06004537 RID: 17719 RVA: 0x00122CD7 File Offset: 0x00120ED7
		public IArrangedElement InnerElement
		{
			get
			{
				return this._wrappedToolStrip;
			}
		}

		// Token: 0x170010FE RID: 4350
		// (get) Token: 0x06004538 RID: 17720 RVA: 0x00122CD7 File Offset: 0x00120ED7
		public ISupportToolStripPanel DraggedControl
		{
			get
			{
				return this._wrappedToolStrip;
			}
		}

		// Token: 0x170010FF RID: 4351
		// (get) Token: 0x06004539 RID: 17721 RVA: 0x00122CF6 File Offset: 0x00120EF6
		// (set) Token: 0x0600453A RID: 17722 RVA: 0x00122CFE File Offset: 0x00120EFE
		public ToolStripPanelRow ToolStripPanelRow
		{
			get
			{
				return this.parent;
			}
			set
			{
				if (this.parent != value)
				{
					if (this.parent != null)
					{
						((IList)this.parent.Cells).Remove(this);
					}
					this.parent = value;
					base.Margin = Padding.Empty;
				}
			}
		}

		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x0600453B RID: 17723 RVA: 0x00122D34 File Offset: 0x00120F34
		// (set) Token: 0x0600453C RID: 17724 RVA: 0x00122D63 File Offset: 0x00120F63
		public override bool Visible
		{
			get
			{
				return this.Control != null && this.Control.ParentInternal == this.ToolStripPanelRow.ToolStripPanel && this.InnerElement.ParticipatesInLayout;
			}
			set
			{
				this.Control.Visible = value;
			}
		}

		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x0600453D RID: 17725 RVA: 0x00122D71 File Offset: 0x00120F71
		public Size MaximumSize
		{
			get
			{
				return this.maxSize;
			}
		}

		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x0600453E RID: 17726 RVA: 0x0002F8B5 File Offset: 0x0002DAB5
		public override LayoutEngine LayoutEngine
		{
			get
			{
				return DefaultLayout.Instance;
			}
		}

		// Token: 0x0600453F RID: 17727 RVA: 0x00122CF6 File Offset: 0x00120EF6
		protected override IArrangedElement GetContainer()
		{
			return this.parent;
		}

		// Token: 0x06004540 RID: 17728 RVA: 0x00122D79 File Offset: 0x00120F79
		public int Grow(int growBy)
		{
			if (this.ToolStripPanelRow.Orientation == Orientation.Vertical)
			{
				return this.GrowVertical(growBy);
			}
			return this.GrowHorizontal(growBy);
		}

		// Token: 0x06004541 RID: 17729 RVA: 0x00122D98 File Offset: 0x00120F98
		private int GrowVertical(int growBy)
		{
			if (this.MaximumSize.Height >= this.Control.PreferredSize.Height)
			{
				return 0;
			}
			if (this.MaximumSize.Height + growBy >= this.Control.PreferredSize.Height)
			{
				int result = this.Control.PreferredSize.Height - this.MaximumSize.Height;
				this.maxSize = LayoutUtils.MaxSize;
				return result;
			}
			if (this.MaximumSize.Height + growBy < this.Control.PreferredSize.Height)
			{
				this.maxSize.Height = this.maxSize.Height + growBy;
				return growBy;
			}
			return 0;
		}

		// Token: 0x06004542 RID: 17730 RVA: 0x00122E5C File Offset: 0x0012105C
		private int GrowHorizontal(int growBy)
		{
			if (this.MaximumSize.Width >= this.Control.PreferredSize.Width)
			{
				return 0;
			}
			if (this.MaximumSize.Width + growBy >= this.Control.PreferredSize.Width)
			{
				int result = this.Control.PreferredSize.Width - this.MaximumSize.Width;
				this.maxSize = LayoutUtils.MaxSize;
				return result;
			}
			if (this.MaximumSize.Width + growBy < this.Control.PreferredSize.Width)
			{
				this.maxSize.Width = this.maxSize.Width + growBy;
				return growBy;
			}
			return 0;
		}

		// Token: 0x06004543 RID: 17731 RVA: 0x00122F20 File Offset: 0x00121120
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (this._wrappedToolStrip != null)
					{
						this._wrappedToolStrip.LocationChanging -= this.OnToolStripLocationChanging;
						this._wrappedToolStrip.VisibleChanged -= this.OnToolStripVisibleChanged;
					}
					this._wrappedToolStrip = null;
					if (this.parent != null)
					{
						((IList)this.parent.Cells).Remove(this);
					}
					this.parent = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06004544 RID: 17732 RVA: 0x00122FA8 File Offset: 0x001211A8
		protected override ArrangedElementCollection GetChildren()
		{
			return ArrangedElementCollection.Empty;
		}

		// Token: 0x06004545 RID: 17733 RVA: 0x00122FB0 File Offset: 0x001211B0
		public override Size GetPreferredSize(Size constrainingSize)
		{
			ISupportToolStripPanel draggedControl = this.DraggedControl;
			Size result = Size.Empty;
			if (draggedControl.Stretch)
			{
				if (this.ToolStripPanelRow.Orientation == Orientation.Horizontal)
				{
					constrainingSize.Width = this.ToolStripPanelRow.Bounds.Width;
					result = this._wrappedToolStrip.GetPreferredSize(constrainingSize);
					result.Width = constrainingSize.Width;
				}
				else
				{
					constrainingSize.Height = this.ToolStripPanelRow.Bounds.Height;
					result = this._wrappedToolStrip.GetPreferredSize(constrainingSize);
					result.Height = constrainingSize.Height;
				}
			}
			else
			{
				result = ((!this._wrappedToolStrip.AutoSize) ? this._wrappedToolStrip.Size : this._wrappedToolStrip.GetPreferredSize(constrainingSize));
			}
			return result;
		}

		// Token: 0x06004546 RID: 17734 RVA: 0x00123078 File Offset: 0x00121278
		protected override void SetBoundsCore(Rectangle bounds, BoundsSpecified specified)
		{
			this.currentlySizing = true;
			this.CachedBounds = bounds;
			try
			{
				if (this.DraggedControl.IsCurrentlyDragging)
				{
					if (this.ToolStripPanelRow.Cells[this.ToolStripPanelRow.Cells.Count - 1] == this)
					{
						Rectangle displayRectangle = this.ToolStripPanelRow.DisplayRectangle;
						if (this.ToolStripPanelRow.Orientation == Orientation.Horizontal)
						{
							int num = bounds.Right - displayRectangle.Right;
							if (num > 0 && bounds.Width > num)
							{
								bounds.Width -= num;
							}
						}
						else
						{
							int num2 = bounds.Bottom - displayRectangle.Bottom;
							if (num2 > 0 && bounds.Height > num2)
							{
								bounds.Height -= num2;
							}
						}
					}
					base.SetBoundsCore(bounds, specified);
					this.InnerElement.SetBounds(bounds, specified);
				}
				else if (!this.ToolStripPanelRow.CachedBoundsMode)
				{
					base.SetBoundsCore(bounds, specified);
					this.InnerElement.SetBounds(bounds, specified);
				}
			}
			finally
			{
				this.currentlySizing = false;
			}
		}

		// Token: 0x06004547 RID: 17735 RVA: 0x00123190 File Offset: 0x00121390
		public int Shrink(int shrinkBy)
		{
			if (this.ToolStripPanelRow.Orientation == Orientation.Vertical)
			{
				return this.ShrinkVertical(shrinkBy);
			}
			return this.ShrinkHorizontal(shrinkBy);
		}

		// Token: 0x06004548 RID: 17736 RVA: 0x00011A20 File Offset: 0x0000FC20
		private int ShrinkHorizontal(int shrinkBy)
		{
			return 0;
		}

		// Token: 0x06004549 RID: 17737 RVA: 0x00011A20 File Offset: 0x0000FC20
		private int ShrinkVertical(int shrinkBy)
		{
			return 0;
		}

		// Token: 0x0600454A RID: 17738 RVA: 0x001231B0 File Offset: 0x001213B0
		private void OnToolStripLocationChanging(object sender, ToolStripLocationCancelEventArgs e)
		{
			if (this.ToolStripPanelRow == null)
			{
				return;
			}
			if (!this.currentlySizing && !this.currentlyDragging)
			{
				try
				{
					this.currentlyDragging = true;
					Point newLocation = e.NewLocation;
					if (this.ToolStripPanelRow != null && this.ToolStripPanelRow.Bounds == Rectangle.Empty)
					{
						this.ToolStripPanelRow.ToolStripPanel.PerformUpdate(true);
					}
					if (this._wrappedToolStrip != null)
					{
						this.ToolStripPanelRow.ToolStripPanel.Join(this._wrappedToolStrip, newLocation);
					}
				}
				finally
				{
					this.currentlyDragging = false;
					e.Cancel = true;
				}
			}
		}

		// Token: 0x0600454B RID: 17739 RVA: 0x00123254 File Offset: 0x00121454
		private void OnToolStripVisibleChanged(object sender, EventArgs e)
		{
			if (this._wrappedToolStrip != null && !this._wrappedToolStrip.IsInDesignMode && !this._wrappedToolStrip.IsCurrentlyDragging && !this._wrappedToolStrip.IsDisposed && !this._wrappedToolStrip.Disposing)
			{
				if (!this.Control.Visible)
				{
					this.restoreOnVisibleChanged = (this.ToolStripPanelRow != null && ((IList)this.ToolStripPanelRow.Cells).Contains(this));
					return;
				}
				if (this.restoreOnVisibleChanged)
				{
					try
					{
						if (this.ToolStripPanelRow != null && ((IList)this.ToolStripPanelRow.Cells).Contains(this))
						{
							this.ToolStripPanelRow.ToolStripPanel.Join(this._wrappedToolStrip, this._wrappedToolStrip.Location);
						}
					}
					finally
					{
						this.restoreOnVisibleChanged = false;
					}
				}
			}
		}

		// Token: 0x04002649 RID: 9801
		private ToolStrip _wrappedToolStrip;

		// Token: 0x0400264A RID: 9802
		private ToolStripPanelRow parent;

		// Token: 0x0400264B RID: 9803
		private Size maxSize = LayoutUtils.MaxSize;

		// Token: 0x0400264C RID: 9804
		private bool currentlySizing;

		// Token: 0x0400264D RID: 9805
		private bool currentlyDragging;

		// Token: 0x0400264E RID: 9806
		private bool restoreOnVisibleChanged;

		// Token: 0x0400264F RID: 9807
		private Rectangle cachedBounds = Rectangle.Empty;
	}
}
