using System;
using System.Drawing;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x020003C6 RID: 966
	internal class ToolStripGrip : ToolStripButton
	{
		// Token: 0x06004181 RID: 16769 RVA: 0x0011870C File Offset: 0x0011690C
		internal ToolStripGrip()
		{
			if (DpiHelper.EnableToolStripHighDpiImprovements)
			{
				this.scaledDefaultPadding = DpiHelper.LogicalToDeviceUnits(ToolStripGrip.defaultPadding, 0);
				this.scaledGripThickness = DpiHelper.LogicalToDeviceUnitsX(ToolStripGrip.gripThicknessDefault);
				this.scaledGripThicknessVisualStylesEnabled = DpiHelper.LogicalToDeviceUnitsX(ToolStripGrip.gripThicknessVisualStylesEnabled);
			}
			this.gripThickness = (ToolStripManager.VisualStylesEnabled ? this.scaledGripThicknessVisualStylesEnabled : this.scaledGripThickness);
			base.SupportsItemClick = false;
		}

		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x06004182 RID: 16770 RVA: 0x001187B0 File Offset: 0x001169B0
		protected internal override Padding DefaultMargin
		{
			get
			{
				return this.scaledDefaultPadding;
			}
		}

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x06004183 RID: 16771 RVA: 0x00011A20 File Offset: 0x0000FC20
		public override bool CanSelect
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x06004184 RID: 16772 RVA: 0x001187B8 File Offset: 0x001169B8
		internal int GripThickness
		{
			get
			{
				return this.gripThickness;
			}
		}

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x06004185 RID: 16773 RVA: 0x001187C0 File Offset: 0x001169C0
		// (set) Token: 0x06004186 RID: 16774 RVA: 0x001187D4 File Offset: 0x001169D4
		internal bool MovingToolStrip
		{
			get
			{
				return this.ToolStripPanelRow != null && this.movingToolStrip;
			}
			set
			{
				if (this.movingToolStrip != value && base.ParentInternal != null)
				{
					if (value && base.ParentInternal.ToolStripPanelRow == null)
					{
						return;
					}
					this.movingToolStrip = value;
					this.lastEndLocation = ToolStrip.InvalidMouseEnter;
					if (this.movingToolStrip)
					{
						((ISupportToolStripPanel)base.ParentInternal).BeginDrag();
						return;
					}
					((ISupportToolStripPanel)base.ParentInternal).EndDrag();
				}
			}
		}

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x06004187 RID: 16775 RVA: 0x00118834 File Offset: 0x00116A34
		private ToolStripPanelRow ToolStripPanelRow
		{
			get
			{
				if (base.ParentInternal != null)
				{
					return ((ISupportToolStripPanel)base.ParentInternal).ToolStripPanelRow;
				}
				return null;
			}
		}

		// Token: 0x06004188 RID: 16776 RVA: 0x0011884B File Offset: 0x00116A4B
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new ToolStripGrip.ToolStripGripAccessibleObject(this);
		}

		// Token: 0x06004189 RID: 16777 RVA: 0x00118854 File Offset: 0x00116A54
		public override Size GetPreferredSize(Size constrainingSize)
		{
			Size empty = Size.Empty;
			if (base.ParentInternal != null)
			{
				if (base.ParentInternal.LayoutStyle == ToolStripLayoutStyle.VerticalStackWithOverflow)
				{
					empty = new Size(base.ParentInternal.Width, this.gripThickness);
				}
				else
				{
					empty = new Size(this.gripThickness, base.ParentInternal.Height);
				}
			}
			if (empty.Width > constrainingSize.Width)
			{
				empty.Width = constrainingSize.Width;
			}
			if (empty.Height > constrainingSize.Height)
			{
				empty.Height = constrainingSize.Height;
			}
			return empty;
		}

		// Token: 0x0600418A RID: 16778 RVA: 0x001188EC File Offset: 0x00116AEC
		private bool LeftMouseButtonIsDown()
		{
			return Control.MouseButtons == MouseButtons.Left && Control.ModifierKeys == Keys.None;
		}

		// Token: 0x0600418B RID: 16779 RVA: 0x00118904 File Offset: 0x00116B04
		protected override void OnPaint(PaintEventArgs e)
		{
			if (base.ParentInternal != null)
			{
				base.ParentInternal.OnPaintGrip(e);
			}
		}

		// Token: 0x0600418C RID: 16780 RVA: 0x0011891A File Offset: 0x00116B1A
		protected override void OnMouseDown(MouseEventArgs mea)
		{
			this.startLocation = base.TranslatePoint(new Point(mea.X, mea.Y), ToolStripPointType.ToolStripItemCoords, ToolStripPointType.ScreenCoords);
			base.OnMouseDown(mea);
		}

		// Token: 0x0600418D RID: 16781 RVA: 0x00118944 File Offset: 0x00116B44
		protected override void OnMouseMove(MouseEventArgs mea)
		{
			bool flag = this.LeftMouseButtonIsDown();
			if (!this.MovingToolStrip && flag)
			{
				Point point = base.TranslatePoint(mea.Location, ToolStripPointType.ToolStripItemCoords, ToolStripPointType.ScreenCoords);
				int num = point.X - this.startLocation.X;
				num = ((num < 0) ? (num * -1) : num);
				if (ToolStripGrip.DragSize == LayoutUtils.MaxSize)
				{
					ToolStripGrip.DragSize = SystemInformation.DragSize;
				}
				if (num >= ToolStripGrip.DragSize.Width)
				{
					this.MovingToolStrip = true;
				}
				else
				{
					int num2 = point.Y - this.startLocation.Y;
					num2 = ((num2 < 0) ? (num2 * -1) : num2);
					if (num2 >= ToolStripGrip.DragSize.Height)
					{
						this.MovingToolStrip = true;
					}
				}
			}
			if (this.MovingToolStrip)
			{
				if (flag)
				{
					Point point2 = base.TranslatePoint(new Point(mea.X, mea.Y), ToolStripPointType.ToolStripItemCoords, ToolStripPointType.ScreenCoords);
					if (point2 != this.lastEndLocation)
					{
						this.ToolStripPanelRow.ToolStripPanel.MoveControl(base.ParentInternal, point2);
						this.lastEndLocation = point2;
					}
					this.startLocation = point2;
				}
				else
				{
					this.MovingToolStrip = false;
				}
			}
			base.OnMouseMove(mea);
		}

		// Token: 0x0600418E RID: 16782 RVA: 0x00118A68 File Offset: 0x00116C68
		protected override void OnMouseEnter(EventArgs e)
		{
			if (base.ParentInternal != null && this.ToolStripPanelRow != null && !base.ParentInternal.IsInDesignMode)
			{
				this.oldCursor = base.ParentInternal.Cursor;
				ToolStripGrip.SetCursor(base.ParentInternal, Cursors.SizeAll);
			}
			else
			{
				this.oldCursor = null;
			}
			base.OnMouseEnter(e);
		}

		// Token: 0x0600418F RID: 16783 RVA: 0x00118AC4 File Offset: 0x00116CC4
		protected override void OnMouseLeave(EventArgs e)
		{
			if (this.oldCursor != null && !base.ParentInternal.IsInDesignMode)
			{
				ToolStripGrip.SetCursor(base.ParentInternal, this.oldCursor);
			}
			if (!this.MovingToolStrip && this.LeftMouseButtonIsDown())
			{
				this.MovingToolStrip = true;
			}
			base.OnMouseLeave(e);
		}

		// Token: 0x06004190 RID: 16784 RVA: 0x00118B1C File Offset: 0x00116D1C
		protected override void OnMouseUp(MouseEventArgs mea)
		{
			if (this.MovingToolStrip)
			{
				Point screenLocation = base.TranslatePoint(new Point(mea.X, mea.Y), ToolStripPointType.ToolStripItemCoords, ToolStripPointType.ScreenCoords);
				this.ToolStripPanelRow.ToolStripPanel.MoveControl(base.ParentInternal, screenLocation);
			}
			if (!base.ParentInternal.IsInDesignMode)
			{
				ToolStripGrip.SetCursor(base.ParentInternal, this.oldCursor);
			}
			ToolStripPanel.ClearDragFeedback();
			this.MovingToolStrip = false;
			base.OnMouseUp(mea);
		}

		// Token: 0x06004191 RID: 16785 RVA: 0x00118B94 File Offset: 0x00116D94
		internal override void ToolStrip_RescaleConstants(int oldDpi, int newDpi)
		{
			base.RescaleConstantsInternal(newDpi);
			this.scaledDefaultPadding = DpiHelper.LogicalToDeviceUnits(ToolStripGrip.defaultPadding, newDpi);
			this.scaledGripThickness = DpiHelper.LogicalToDeviceUnits(ToolStripGrip.gripThicknessDefault, newDpi);
			this.scaledGripThicknessVisualStylesEnabled = DpiHelper.LogicalToDeviceUnits(ToolStripGrip.gripThicknessVisualStylesEnabled, newDpi);
			base.Margin = this.DefaultMargin;
			this.gripThickness = (ToolStripManager.VisualStylesEnabled ? this.scaledGripThicknessVisualStylesEnabled : this.scaledGripThickness);
			this.OnFontChanged(EventArgs.Empty);
		}

		// Token: 0x06004192 RID: 16786 RVA: 0x00118C0D File Offset: 0x00116E0D
		private static void SetCursor(Control control, Cursor cursor)
		{
			IntSecurity.ModifyCursor.Assert();
			control.Cursor = cursor;
		}

		// Token: 0x0400251E RID: 9502
		private Cursor oldCursor;

		// Token: 0x0400251F RID: 9503
		private int gripThickness;

		// Token: 0x04002520 RID: 9504
		private Point startLocation = Point.Empty;

		// Token: 0x04002521 RID: 9505
		private bool movingToolStrip;

		// Token: 0x04002522 RID: 9506
		private Point lastEndLocation = ToolStrip.InvalidMouseEnter;

		// Token: 0x04002523 RID: 9507
		private static Size DragSize = LayoutUtils.MaxSize;

		// Token: 0x04002524 RID: 9508
		private static readonly Padding defaultPadding = new Padding(2);

		// Token: 0x04002525 RID: 9509
		private static readonly int gripThicknessDefault = 3;

		// Token: 0x04002526 RID: 9510
		private static readonly int gripThicknessVisualStylesEnabled = 5;

		// Token: 0x04002527 RID: 9511
		private Padding scaledDefaultPadding = ToolStripGrip.defaultPadding;

		// Token: 0x04002528 RID: 9512
		private int scaledGripThickness = ToolStripGrip.gripThicknessDefault;

		// Token: 0x04002529 RID: 9513
		private int scaledGripThicknessVisualStylesEnabled = ToolStripGrip.gripThicknessVisualStylesEnabled;

		// Token: 0x02000804 RID: 2052
		internal class ToolStripGripAccessibleObject : ToolStripButton.ToolStripButtonAccessibleObject
		{
			// Token: 0x06006EF1 RID: 28401 RVA: 0x00196D49 File Offset: 0x00194F49
			public ToolStripGripAccessibleObject(ToolStripGrip owner) : base(owner)
			{
			}

			// Token: 0x1700183A RID: 6202
			// (get) Token: 0x06006EF2 RID: 28402 RVA: 0x00196D54 File Offset: 0x00194F54
			// (set) Token: 0x06006EF3 RID: 28403 RVA: 0x00196DA3 File Offset: 0x00194FA3
			public override string Name
			{
				get
				{
					if (base.IsOwnerItemCleared())
					{
						return string.Empty;
					}
					string accessibleName = base.Owner.AccessibleName;
					if (accessibleName != null)
					{
						return accessibleName;
					}
					if (string.IsNullOrEmpty(this.stockName))
					{
						this.stockName = SR.GetString("ToolStripGripAccessibleName");
					}
					return this.stockName;
				}
				set
				{
					base.Name = value;
				}
			}

			// Token: 0x1700183B RID: 6203
			// (get) Token: 0x06006EF4 RID: 28404 RVA: 0x00196DAC File Offset: 0x00194FAC
			public override AccessibleRole Role
			{
				get
				{
					if (base.IsOwnerItemCleared())
					{
						return AccessibleRole.Grip;
					}
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return AccessibleRole.Grip;
				}
			}

			// Token: 0x06006EF5 RID: 28405 RVA: 0x00196DD6 File Offset: 0x00194FD6
			internal override object GetPropertyValue(int propertyID)
			{
				if (AccessibilityImprovements.Level3)
				{
					if (propertyID == 30003)
					{
						return 50027;
					}
					if (propertyID == 30022)
					{
						return false;
					}
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x04004302 RID: 17154
			private string stockName;
		}
	}
}
