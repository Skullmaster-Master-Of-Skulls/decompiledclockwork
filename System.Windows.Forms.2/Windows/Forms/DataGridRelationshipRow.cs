using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x02000187 RID: 391
	internal class DataGridRelationshipRow : DataGridRow
	{
		// Token: 0x06001710 RID: 5904 RVA: 0x0004EFEC File Offset: 0x0004D1EC
		public DataGridRelationshipRow(DataGrid dataGrid, DataGridTableStyle dgTable, int rowNumber) : base(dataGrid, dgTable, rowNumber)
		{
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x00052F44 File Offset: 0x00051144
		protected internal override int MinimumRowHeight(GridColumnStylesCollection cols)
		{
			return base.MinimumRowHeight(cols) + (this.expanded ? this.GetRelationshipRect().Height : 0);
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x00052F74 File Offset: 0x00051174
		protected internal override int MinimumRowHeight(DataGridTableStyle dgTable)
		{
			return base.MinimumRowHeight(dgTable) + (this.expanded ? this.GetRelationshipRect().Height : 0);
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001713 RID: 5907 RVA: 0x00052FA2 File Offset: 0x000511A2
		// (set) Token: 0x06001714 RID: 5908 RVA: 0x00052FAA File Offset: 0x000511AA
		public virtual bool Expanded
		{
			get
			{
				return this.expanded;
			}
			set
			{
				if (this.expanded == value)
				{
					return;
				}
				if (this.expanded)
				{
					this.Collapse();
					return;
				}
				this.Expand();
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x00052FCB File Offset: 0x000511CB
		// (set) Token: 0x06001716 RID: 5910 RVA: 0x00052FD8 File Offset: 0x000511D8
		private int FocusedRelation
		{
			get
			{
				return this.dgTable.FocusedRelation;
			}
			set
			{
				this.dgTable.FocusedRelation = value;
			}
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x00052FE6 File Offset: 0x000511E6
		private void Collapse()
		{
			if (this.expanded)
			{
				this.expanded = false;
				this.FocusedRelation = -1;
				base.DataGrid.OnRowHeightChanged(this);
			}
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x0005300A File Offset: 0x0005120A
		protected override AccessibleObject CreateAccessibleObject()
		{
			return new DataGridRelationshipRow.DataGridRelationshipRowAccessibleObject(this);
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x00053014 File Offset: 0x00051214
		private void Expand()
		{
			if (!this.expanded && base.DataGrid != null && this.dgTable != null && this.dgTable.RelationsList.Count > 0)
			{
				this.expanded = true;
				this.FocusedRelation = -1;
				base.DataGrid.OnRowHeightChanged(this);
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x0600171A RID: 5914 RVA: 0x00053068 File Offset: 0x00051268
		// (set) Token: 0x0600171B RID: 5915 RVA: 0x00053098 File Offset: 0x00051298
		public override int Height
		{
			get
			{
				int height = base.Height;
				if (this.expanded)
				{
					return height + this.GetRelationshipRect().Height;
				}
				return height;
			}
			set
			{
				if (this.expanded)
				{
					base.Height = value - this.GetRelationshipRect().Height;
					return;
				}
				base.Height = value;
			}
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x000530CC File Offset: 0x000512CC
		public override Rectangle GetCellBounds(int col)
		{
			Rectangle cellBounds = base.GetCellBounds(col);
			cellBounds.Height = base.Height - 1;
			return cellBounds;
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x000530F4 File Offset: 0x000512F4
		private Rectangle GetOutlineRect(int xOrigin, int yOrigin)
		{
			Rectangle result = new Rectangle(xOrigin + 2, yOrigin + 2, 9, 9);
			return result;
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x00053113 File Offset: 0x00051313
		public override Rectangle GetNonScrollableArea()
		{
			if (this.expanded)
			{
				return this.GetRelationshipRect();
			}
			return Rectangle.Empty;
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x0005312C File Offset: 0x0005132C
		private Rectangle GetRelationshipRect()
		{
			Rectangle relationshipRect = this.dgTable.RelationshipRect;
			relationshipRect.Y = base.Height - this.dgTable.BorderWidth;
			return relationshipRect;
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x00053160 File Offset: 0x00051360
		private Rectangle GetRelationshipRectWithMirroring()
		{
			Rectangle relationshipRect = this.GetRelationshipRect();
			bool flag = this.dgTable.IsDefault ? base.DataGrid.RowHeadersVisible : this.dgTable.RowHeadersVisible;
			if (flag)
			{
				int num = this.dgTable.IsDefault ? base.DataGrid.RowHeaderWidth : this.dgTable.RowHeaderWidth;
				relationshipRect.X += base.DataGrid.GetRowHeaderRect().X + num;
			}
			relationshipRect.X = this.MirrorRelationshipRectangle(relationshipRect, base.DataGrid.GetRowHeaderRect(), base.DataGrid.RightToLeft == RightToLeft.Yes);
			return relationshipRect;
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x00053210 File Offset: 0x00051410
		private bool PointOverPlusMinusGlyph(int x, int y, Rectangle rowHeaders, bool alignToRight)
		{
			if (this.dgTable == null || this.dgTable.DataGrid == null || !this.dgTable.DataGrid.AllowNavigation)
			{
				return false;
			}
			Rectangle rect = rowHeaders;
			if (!base.DataGrid.FlatMode)
			{
				rect.Inflate(-1, -1);
			}
			Rectangle outlineRect = this.GetOutlineRect(rect.Right - 14, 0);
			outlineRect.X = this.MirrorRectangle(outlineRect.X, outlineRect.Width, rect, alignToRight);
			return outlineRect.Contains(x, y);
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x00053298 File Offset: 0x00051498
		public override bool OnMouseDown(int x, int y, Rectangle rowHeaders, bool alignToRight)
		{
			bool flag = this.dgTable.IsDefault ? base.DataGrid.RowHeadersVisible : this.dgTable.RowHeadersVisible;
			if (flag && this.PointOverPlusMinusGlyph(x, y, rowHeaders, alignToRight))
			{
				if (this.dgTable.RelationsList.Count == 0)
				{
					return false;
				}
				if (this.expanded)
				{
					this.Collapse();
				}
				else
				{
					this.Expand();
				}
				base.DataGrid.OnNodeClick(EventArgs.Empty);
				return true;
			}
			else
			{
				if (!this.expanded)
				{
					return base.OnMouseDown(x, y, rowHeaders, alignToRight);
				}
				if (this.GetRelationshipRectWithMirroring().Contains(x, y))
				{
					int num = this.RelationFromY(y);
					if (num != -1)
					{
						this.FocusedRelation = -1;
						base.DataGrid.NavigateTo((string)this.dgTable.RelationsList[num], this, true);
					}
					return true;
				}
				return base.OnMouseDown(x, y, rowHeaders, alignToRight);
			}
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x00053380 File Offset: 0x00051580
		public override bool OnMouseMove(int x, int y, Rectangle rowHeaders, bool alignToRight)
		{
			if (!this.expanded)
			{
				return false;
			}
			if (this.GetRelationshipRectWithMirroring().Contains(x, y))
			{
				base.DataGrid.Cursor = Cursors.Hand;
				return true;
			}
			base.DataGrid.Cursor = Cursors.Default;
			return base.OnMouseMove(x, y, rowHeaders, alignToRight);
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x000533D8 File Offset: 0x000515D8
		public override void OnMouseLeft(Rectangle rowHeaders, bool alignToRight)
		{
			if (!this.expanded)
			{
				return;
			}
			Rectangle relationshipRect = this.GetRelationshipRect();
			relationshipRect.X += rowHeaders.X + this.dgTable.RowHeaderWidth;
			relationshipRect.X = this.MirrorRelationshipRectangle(relationshipRect, rowHeaders, alignToRight);
			if (this.FocusedRelation != -1)
			{
				this.InvalidateRowRect(relationshipRect);
				this.FocusedRelation = -1;
			}
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x0005343D File Offset: 0x0005163D
		public override void OnMouseLeft()
		{
			if (!this.expanded)
			{
				return;
			}
			if (this.FocusedRelation != -1)
			{
				this.InvalidateRow();
				this.FocusedRelation = -1;
			}
			base.OnMouseLeft();
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x00053464 File Offset: 0x00051664
		public override bool OnKeyPress(Keys keyData)
		{
			if ((keyData & Keys.Modifiers) == Keys.Shift && (keyData & Keys.KeyCode) != Keys.Tab)
			{
				return false;
			}
			Keys keys = keyData & Keys.KeyCode;
			if (keys <= Keys.Return)
			{
				if (keys == Keys.Tab)
				{
					return false;
				}
				if (keys == Keys.Return)
				{
					if (this.FocusedRelation != -1)
					{
						base.DataGrid.NavigateTo((string)this.dgTable.RelationsList[this.FocusedRelation], this, true);
						this.FocusedRelation = -1;
						return true;
					}
					return false;
				}
			}
			else if (keys != Keys.F5)
			{
				if (keys == Keys.NumLock)
				{
					return this.FocusedRelation == -1 && base.OnKeyPress(keyData);
				}
			}
			else
			{
				if (this.dgTable == null || this.dgTable.DataGrid == null || !this.dgTable.DataGrid.AllowNavigation)
				{
					return false;
				}
				if (this.expanded)
				{
					this.Collapse();
				}
				else
				{
					this.Expand();
				}
				this.FocusedRelation = -1;
				return true;
			}
			this.FocusedRelation = -1;
			return base.OnKeyPress(keyData);
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x00053564 File Offset: 0x00051764
		internal override void LoseChildFocus(Rectangle rowHeaders, bool alignToRight)
		{
			if (this.FocusedRelation == -1 || !this.expanded)
			{
				return;
			}
			this.FocusedRelation = -1;
			Rectangle relationshipRect = this.GetRelationshipRect();
			relationshipRect.X += rowHeaders.X + this.dgTable.RowHeaderWidth;
			relationshipRect.X = this.MirrorRelationshipRectangle(relationshipRect, rowHeaders, alignToRight);
			this.InvalidateRowRect(relationshipRect);
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x000535CC File Offset: 0x000517CC
		internal override bool ProcessTabKey(Keys keyData, Rectangle rowHeaders, bool alignToRight)
		{
			if (this.dgTable.RelationsList.Count == 0 || this.dgTable.DataGrid == null || !this.dgTable.DataGrid.AllowNavigation)
			{
				return false;
			}
			if (!this.expanded)
			{
				this.Expand();
			}
			if ((keyData & Keys.Shift) == Keys.Shift)
			{
				if (this.FocusedRelation == 0)
				{
					this.FocusedRelation = -1;
					return false;
				}
				Rectangle relationshipRect = this.GetRelationshipRect();
				relationshipRect.X += rowHeaders.X + this.dgTable.RowHeaderWidth;
				relationshipRect.X = this.MirrorRelationshipRectangle(relationshipRect, rowHeaders, alignToRight);
				this.InvalidateRowRect(relationshipRect);
				if (this.FocusedRelation == -1)
				{
					this.FocusedRelation = this.dgTable.RelationsList.Count - 1;
				}
				else
				{
					int focusedRelation = this.FocusedRelation;
					this.FocusedRelation = focusedRelation - 1;
				}
				return true;
			}
			else
			{
				if (this.FocusedRelation == this.dgTable.RelationsList.Count - 1)
				{
					this.FocusedRelation = -1;
					return false;
				}
				Rectangle relationshipRect2 = this.GetRelationshipRect();
				relationshipRect2.X += rowHeaders.X + this.dgTable.RowHeaderWidth;
				relationshipRect2.X = this.MirrorRelationshipRectangle(relationshipRect2, rowHeaders, alignToRight);
				this.InvalidateRowRect(relationshipRect2);
				int focusedRelation = this.FocusedRelation;
				this.FocusedRelation = focusedRelation + 1;
				return true;
			}
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x0004F02E File Offset: 0x0004D22E
		public override int Paint(Graphics g, Rectangle bounds, Rectangle trueRowBounds, int firstVisibleColumn, int numVisibleColumns)
		{
			return this.Paint(g, bounds, trueRowBounds, firstVisibleColumn, numVisibleColumns, false);
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x00053724 File Offset: 0x00051924
		public override int Paint(Graphics g, Rectangle bounds, Rectangle trueRowBounds, int firstVisibleColumn, int numVisibleColumns, bool alignToRight)
		{
			bool traceVerbose = CompModSwitches.DGRelationShpRowPaint.TraceVerbose;
			int borderWidth = this.dgTable.BorderWidth;
			Rectangle bounds2 = bounds;
			bounds2.Height = base.Height - borderWidth;
			int num = this.PaintData(g, bounds2, firstVisibleColumn, numVisibleColumns, alignToRight);
			int dataWidth = num + bounds.X - trueRowBounds.X;
			bounds2.Offset(0, borderWidth);
			if (borderWidth > 0)
			{
				this.PaintBottomBorder(g, bounds2, num, borderWidth, alignToRight);
			}
			if (this.expanded && this.dgTable.RelationsList.Count > 0)
			{
				Rectangle bounds3 = new Rectangle(trueRowBounds.X, bounds2.Bottom, trueRowBounds.Width, trueRowBounds.Height - bounds2.Height - 2 * borderWidth);
				this.PaintRelations(g, bounds3, trueRowBounds, dataWidth, firstVisibleColumn, numVisibleColumns, alignToRight);
				bounds3.Height++;
				if (borderWidth > 0)
				{
					this.PaintBottomBorder(g, bounds3, dataWidth, borderWidth, alignToRight);
				}
			}
			return num;
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x00053810 File Offset: 0x00051A10
		protected override void PaintCellContents(Graphics g, Rectangle cellBounds, DataGridColumnStyle column, Brush backBr, Brush foreBrush, bool alignToRight)
		{
			CurrencyManager listManager = base.DataGrid.ListManager;
			string text = string.Empty;
			Rectangle rectangle = cellBounds;
			object obj = base.DataGrid.ListManager[this.number];
			if (obj is IDataErrorInfo)
			{
				text = ((IDataErrorInfo)obj)[column.PropertyDescriptor.Name];
			}
			if (!string.IsNullOrEmpty(text))
			{
				Bitmap errorBitmap = base.GetErrorBitmap();
				Bitmap obj2 = errorBitmap;
				Rectangle iconBounds;
				lock (obj2)
				{
					iconBounds = base.PaintIcon(g, rectangle, true, alignToRight, errorBitmap, backBr);
				}
				if (alignToRight)
				{
					rectangle.Width -= iconBounds.Width + 3;
				}
				else
				{
					rectangle.X += iconBounds.Width + 3;
				}
				DataGridToolTip toolTipProvider = base.DataGrid.ToolTipProvider;
				string toolTipString = text;
				DataGrid dataGrid = base.DataGrid;
				int toolTipId = dataGrid.ToolTipId;
				dataGrid.ToolTipId = toolTipId + 1;
				toolTipProvider.AddToolTip(toolTipString, (IntPtr)toolTipId, iconBounds);
			}
			column.Paint(g, rectangle, listManager, base.RowNumber, backBr, foreBrush, alignToRight);
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x00053934 File Offset: 0x00051B34
		public override void PaintHeader(Graphics g, Rectangle bounds, bool alignToRight, bool isDirty)
		{
			DataGrid dataGrid = base.DataGrid;
			Rectangle rectangle = bounds;
			if (!dataGrid.FlatMode)
			{
				ControlPaint.DrawBorder3D(g, rectangle, Border3DStyle.RaisedInner);
				rectangle.Inflate(-1, -1);
			}
			if (this.dgTable.IsDefault)
			{
				this.PaintHeaderInside(g, rectangle, base.DataGrid.HeaderBackBrush, alignToRight, isDirty);
				return;
			}
			this.PaintHeaderInside(g, rectangle, this.dgTable.HeaderBackBrush, alignToRight, isDirty);
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x000539A0 File Offset: 0x00051BA0
		public void PaintHeaderInside(Graphics g, Rectangle bounds, Brush backBr, bool alignToRight, bool isDirty)
		{
			bool flag = this.dgTable.RelationsList.Count > 0 && this.dgTable.DataGrid.AllowNavigation;
			int x = this.MirrorRectangle(bounds.X, bounds.Width - (flag ? 14 : 0), bounds, alignToRight);
			Rectangle visualBounds = new Rectangle(x, bounds.Y, bounds.Width - (flag ? 14 : 0), bounds.Height);
			base.PaintHeader(g, visualBounds, alignToRight, isDirty);
			int x2 = this.MirrorRectangle(bounds.X + visualBounds.Width, 14, bounds, alignToRight);
			Rectangle bounds2 = new Rectangle(x2, bounds.Y, 14, bounds.Height);
			if (flag)
			{
				this.PaintPlusMinusGlyph(g, bounds2, backBr, alignToRight);
			}
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x00053A70 File Offset: 0x00051C70
		private void PaintRelations(Graphics g, Rectangle bounds, Rectangle trueRowBounds, int dataWidth, int firstCol, int nCols, bool alignToRight)
		{
			Rectangle relationshipRect = this.GetRelationshipRect();
			relationshipRect.X = (alignToRight ? (bounds.Right - relationshipRect.Width) : bounds.X);
			relationshipRect.Y = bounds.Y;
			int num = Math.Max(dataWidth, relationshipRect.Width);
			Region clip = g.Clip;
			g.ExcludeClip(relationshipRect);
			g.FillRectangle(base.GetBackBrush(), alignToRight ? (bounds.Right - dataWidth) : bounds.X, bounds.Y, dataWidth, bounds.Height);
			g.SetClip(bounds);
			relationshipRect.Height -= this.dgTable.BorderWidth;
			g.DrawRectangle(SystemPens.ControlText, relationshipRect.X, relationshipRect.Y, relationshipRect.Width - 1, relationshipRect.Height - 1);
			relationshipRect.Inflate(-1, -1);
			int num2 = this.PaintRelationText(g, relationshipRect, alignToRight);
			if (num2 < relationshipRect.Height)
			{
				g.FillRectangle(base.GetBackBrush(), relationshipRect.X, relationshipRect.Y + num2, relationshipRect.Width, relationshipRect.Height - num2);
			}
			g.Clip = clip;
			if (num < bounds.Width)
			{
				int gridLineWidth;
				if (this.dgTable.IsDefault)
				{
					gridLineWidth = base.DataGrid.GridLineWidth;
				}
				else
				{
					gridLineWidth = this.dgTable.GridLineWidth;
				}
				g.FillRectangle(base.DataGrid.BackgroundBrush, alignToRight ? bounds.X : (bounds.X + num), bounds.Y, bounds.Width - num - gridLineWidth + 1, bounds.Height);
				if (gridLineWidth > 0)
				{
					Brush gridLineBrush;
					if (this.dgTable.IsDefault)
					{
						gridLineBrush = base.DataGrid.GridLineBrush;
					}
					else
					{
						gridLineBrush = this.dgTable.GridLineBrush;
					}
					g.FillRectangle(gridLineBrush, alignToRight ? (bounds.Right - gridLineWidth - num) : (bounds.X + num - gridLineWidth), bounds.Y, gridLineWidth, bounds.Height);
				}
			}
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x00053C80 File Offset: 0x00051E80
		private int PaintRelationText(Graphics g, Rectangle bounds, bool alignToRight)
		{
			g.FillRectangle(base.GetBackBrush(), bounds.X, bounds.Y, bounds.Width, 1);
			int relationshipHeight = this.dgTable.RelationshipHeight;
			Rectangle rectangle = new Rectangle(bounds.X, bounds.Y + 1, bounds.Width, relationshipHeight);
			int num = 1;
			int num2 = 0;
			while (num2 < this.dgTable.RelationsList.Count && num <= bounds.Height)
			{
				Brush brush = this.dgTable.IsDefault ? base.DataGrid.LinkBrush : this.dgTable.LinkBrush;
				Font font = base.DataGrid.Font;
				Brush brush2 = this.dgTable.IsDefault ? base.DataGrid.LinkBrush : this.dgTable.LinkBrush;
				font = base.DataGrid.LinkFont;
				g.FillRectangle(base.GetBackBrush(), rectangle);
				StringFormat stringFormat = new StringFormat();
				if (alignToRight)
				{
					stringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
					stringFormat.Alignment = StringAlignment.Far;
				}
				g.DrawString((string)this.dgTable.RelationsList[num2], font, brush2, rectangle, stringFormat);
				if (num2 == this.FocusedRelation && this.number == base.DataGrid.CurrentCell.RowNumber)
				{
					rectangle.Width = this.dgTable.FocusedTextWidth;
					ControlPaint.DrawFocusRectangle(g, rectangle, ((SolidBrush)brush2).Color, ((SolidBrush)base.GetBackBrush()).Color);
					rectangle.Width = bounds.Width;
				}
				stringFormat.Dispose();
				rectangle.Y += relationshipHeight;
				num += rectangle.Height;
				num2++;
			}
			return num;
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x00053E54 File Offset: 0x00052054
		private void PaintPlusMinusGlyph(Graphics g, Rectangle bounds, Brush backBr, bool alignToRight)
		{
			bool traceVerbose = CompModSwitches.DGRelationShpRowPaint.TraceVerbose;
			Rectangle b = this.GetOutlineRect(bounds.X, bounds.Y);
			b = Rectangle.Intersect(bounds, b);
			if (b.IsEmpty)
			{
				return;
			}
			g.FillRectangle(backBr, bounds);
			bool traceVerbose2 = CompModSwitches.DGRelationShpRowPaint.TraceVerbose;
			Pen pen = this.dgTable.IsDefault ? base.DataGrid.HeaderForePen : this.dgTable.HeaderForePen;
			g.DrawRectangle(pen, b.X, b.Y, b.Width - 1, b.Height - 1);
			int num = 2;
			g.DrawLine(pen, b.X + num, b.Y + b.Width / 2, b.Right - num - 1, b.Y + b.Width / 2);
			if (!this.expanded)
			{
				g.DrawLine(pen, b.X + b.Height / 2, b.Y + num, b.X + b.Height / 2, b.Bottom - num - 1);
				return;
			}
			Point[] array = new Point[3];
			array[0] = new Point(b.X + b.Height / 2, b.Bottom);
			array[1] = new Point(array[0].X, bounds.Y + 2 * num + base.Height);
			array[2] = new Point(alignToRight ? bounds.X : bounds.Right, array[1].Y);
			g.DrawLines(pen, array);
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x00054004 File Offset: 0x00052204
		private int RelationFromY(int y)
		{
			int num = -1;
			int relationshipHeight = this.dgTable.RelationshipHeight;
			Rectangle relationshipRect = this.GetRelationshipRect();
			int num2 = base.Height - this.dgTable.BorderWidth + 1;
			while (num2 < relationshipRect.Bottom && num2 <= y)
			{
				num2 += relationshipHeight;
				num++;
			}
			if (num >= this.dgTable.RelationsList.Count)
			{
				return -1;
			}
			return num;
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x00054069 File Offset: 0x00052269
		private int MirrorRelationshipRectangle(Rectangle relRect, Rectangle rowHeader, bool alignToRight)
		{
			if (alignToRight)
			{
				return rowHeader.X - relRect.Width;
			}
			return relRect.X;
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x00054085 File Offset: 0x00052285
		private int MirrorRectangle(int x, int width, Rectangle rect, bool alignToRight)
		{
			if (alignToRight)
			{
				return rect.Right + rect.X - width - x;
			}
			return x;
		}

		// Token: 0x04000A77 RID: 2679
		private const bool defaultOpen = false;

		// Token: 0x04000A78 RID: 2680
		private const int expandoBoxWidth = 14;

		// Token: 0x04000A79 RID: 2681
		private const int indentWidth = 20;

		// Token: 0x04000A7A RID: 2682
		private const int triangleSize = 5;

		// Token: 0x04000A7B RID: 2683
		private bool expanded;

		// Token: 0x02000651 RID: 1617
		[ComVisible(true)]
		protected class DataGridRelationshipRowAccessibleObject : DataGridRow.DataGridRowAccessibleObject
		{
			// Token: 0x06006502 RID: 25858 RVA: 0x00177FE6 File Offset: 0x001761E6
			public DataGridRelationshipRowAccessibleObject(DataGridRow owner) : base(owner)
			{
			}

			// Token: 0x06006503 RID: 25859 RVA: 0x00177FF0 File Offset: 0x001761F0
			protected override void AddChildAccessibleObjects(IList children)
			{
				base.AddChildAccessibleObjects(children);
				DataGridRelationshipRow dataGridRelationshipRow = (DataGridRelationshipRow)base.Owner;
				if (dataGridRelationshipRow.dgTable.RelationsList != null)
				{
					for (int i = 0; i < dataGridRelationshipRow.dgTable.RelationsList.Count; i++)
					{
						children.Add(new DataGridRelationshipRow.DataGridRelationshipAccessibleObject(dataGridRelationshipRow, i));
					}
				}
			}

			// Token: 0x170015B1 RID: 5553
			// (get) Token: 0x06006504 RID: 25860 RVA: 0x00178046 File Offset: 0x00176246
			private DataGridRelationshipRow RelationshipRow
			{
				get
				{
					return (DataGridRelationshipRow)base.Owner;
				}
			}

			// Token: 0x170015B2 RID: 5554
			// (get) Token: 0x06006505 RID: 25861 RVA: 0x00178053 File Offset: 0x00176253
			public override string DefaultAction
			{
				get
				{
					if (this.RelationshipRow.dgTable.RelationsList.Count <= 0)
					{
						return null;
					}
					if (this.RelationshipRow.Expanded)
					{
						return SR.GetString("AccDGCollapse");
					}
					return SR.GetString("AccDGExpand");
				}
			}

			// Token: 0x170015B3 RID: 5555
			// (get) Token: 0x06006506 RID: 25862 RVA: 0x00178094 File Offset: 0x00176294
			public override AccessibleStates State
			{
				get
				{
					AccessibleStates accessibleStates = base.State;
					if (this.RelationshipRow.dgTable.RelationsList.Count > 0)
					{
						if (((DataGridRelationshipRow)base.Owner).Expanded)
						{
							accessibleStates |= AccessibleStates.Expanded;
						}
						else
						{
							accessibleStates |= AccessibleStates.Collapsed;
						}
					}
					return accessibleStates;
				}
			}

			// Token: 0x06006507 RID: 25863 RVA: 0x001780E5 File Offset: 0x001762E5
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				if (this.RelationshipRow.dgTable.RelationsList.Count > 0)
				{
					((DataGridRelationshipRow)base.Owner).Expanded = !((DataGridRelationshipRow)base.Owner).Expanded;
				}
			}

			// Token: 0x06006508 RID: 25864 RVA: 0x00178124 File Offset: 0x00176324
			public override AccessibleObject GetFocused()
			{
				DataGridRelationshipRow dataGridRelationshipRow = (DataGridRelationshipRow)base.Owner;
				int focusedRelation = dataGridRelationshipRow.dgTable.FocusedRelation;
				if (focusedRelation == -1)
				{
					return base.GetFocused();
				}
				return this.GetChild(this.GetChildCount() - dataGridRelationshipRow.dgTable.RelationsList.Count + focusedRelation);
			}
		}

		// Token: 0x02000652 RID: 1618
		[ComVisible(true)]
		protected class DataGridRelationshipAccessibleObject : AccessibleObject
		{
			// Token: 0x06006509 RID: 25865 RVA: 0x00178173 File Offset: 0x00176373
			public DataGridRelationshipAccessibleObject(DataGridRelationshipRow owner, int relationship)
			{
				this.owner = owner;
				this.relationship = relationship;
			}

			// Token: 0x170015B4 RID: 5556
			// (get) Token: 0x0600650A RID: 25866 RVA: 0x0017818C File Offset: 0x0017638C
			public override Rectangle Bounds
			{
				get
				{
					Rectangle rowBounds = this.DataGrid.GetRowBounds(this.owner);
					Rectangle r = this.owner.Expanded ? this.owner.GetRelationshipRectWithMirroring() : Rectangle.Empty;
					r.Y += this.owner.dgTable.RelationshipHeight * this.relationship;
					r.Height = (this.owner.Expanded ? this.owner.dgTable.RelationshipHeight : 0);
					if (!this.owner.Expanded)
					{
						r.X += rowBounds.X;
					}
					r.Y += rowBounds.Y;
					return this.owner.DataGrid.RectangleToScreen(r);
				}
			}

			// Token: 0x170015B5 RID: 5557
			// (get) Token: 0x0600650B RID: 25867 RVA: 0x0017825F File Offset: 0x0017645F
			public override string Name
			{
				get
				{
					return (string)this.owner.dgTable.RelationsList[this.relationship];
				}
			}

			// Token: 0x170015B6 RID: 5558
			// (get) Token: 0x0600650C RID: 25868 RVA: 0x00178281 File Offset: 0x00176481
			protected DataGridRelationshipRow Owner
			{
				get
				{
					return this.owner;
				}
			}

			// Token: 0x170015B7 RID: 5559
			// (get) Token: 0x0600650D RID: 25869 RVA: 0x00178289 File Offset: 0x00176489
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.owner.AccessibleObject;
				}
			}

			// Token: 0x170015B8 RID: 5560
			// (get) Token: 0x0600650E RID: 25870 RVA: 0x00178296 File Offset: 0x00176496
			protected DataGrid DataGrid
			{
				get
				{
					return this.owner.DataGrid;
				}
			}

			// Token: 0x170015B9 RID: 5561
			// (get) Token: 0x0600650F RID: 25871 RVA: 0x001782A3 File Offset: 0x001764A3
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.Link;
				}
			}

			// Token: 0x170015BA RID: 5562
			// (get) Token: 0x06006510 RID: 25872 RVA: 0x001782A8 File Offset: 0x001764A8
			public override AccessibleStates State
			{
				get
				{
					DataGridRow[] dataGridRows = this.DataGrid.DataGridRows;
					if (Array.IndexOf<DataGridRow>(dataGridRows, this.owner) == -1)
					{
						return AccessibleStates.Unavailable;
					}
					AccessibleStates accessibleStates = AccessibleStates.Focusable | AccessibleStates.Selectable | AccessibleStates.Linked;
					if (!this.owner.Expanded)
					{
						accessibleStates |= AccessibleStates.Invisible;
					}
					if (this.DataGrid.Focused && this.Owner.dgTable.FocusedRelation == this.relationship)
					{
						accessibleStates |= AccessibleStates.Focused;
					}
					return accessibleStates;
				}
			}

			// Token: 0x170015BB RID: 5563
			// (get) Token: 0x06006511 RID: 25873 RVA: 0x00178318 File Offset: 0x00176518
			// (set) Token: 0x06006512 RID: 25874 RVA: 0x000072B6 File Offset: 0x000054B6
			public override string Value
			{
				get
				{
					DataGridRow[] dataGridRows = this.DataGrid.DataGridRows;
					if (Array.IndexOf<DataGridRow>(dataGridRows, this.owner) == -1)
					{
						return null;
					}
					return (string)this.owner.dgTable.RelationsList[this.relationship];
				}
				set
				{
				}
			}

			// Token: 0x170015BC RID: 5564
			// (get) Token: 0x06006513 RID: 25875 RVA: 0x00178362 File Offset: 0x00176562
			public override string DefaultAction
			{
				get
				{
					return SR.GetString("AccDGNavigate");
				}
			}

			// Token: 0x06006514 RID: 25876 RVA: 0x00178370 File Offset: 0x00176570
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				this.Owner.Expanded = true;
				this.owner.FocusedRelation = -1;
				this.DataGrid.NavigateTo((string)this.owner.dgTable.RelationsList[this.relationship], this.owner, true);
				this.DataGrid.BeginInvoke(new MethodInvoker(this.ResetAccessibilityLayer));
			}

			// Token: 0x06006515 RID: 25877 RVA: 0x001783E0 File Offset: 0x001765E0
			private void ResetAccessibilityLayer()
			{
				((DataGrid.DataGridAccessibleObject)this.DataGrid.AccessibilityObject).NotifyClients(AccessibleEvents.Reorder, 0);
				((DataGrid.DataGridAccessibleObject)this.DataGrid.AccessibilityObject).NotifyClients(AccessibleEvents.Focus, this.DataGrid.CurrentCellAccIndex);
				((DataGrid.DataGridAccessibleObject)this.DataGrid.AccessibilityObject).NotifyClients(AccessibleEvents.Selection, this.DataGrid.CurrentCellAccIndex);
			}

			// Token: 0x06006516 RID: 25878 RVA: 0x00178454 File Offset: 0x00176654
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation navdir)
			{
				switch (navdir)
				{
				case AccessibleNavigation.Up:
				case AccessibleNavigation.Left:
				case AccessibleNavigation.Previous:
					if (this.relationship > 0)
					{
						return this.Parent.GetChild(this.Parent.GetChildCount() - this.owner.dgTable.RelationsList.Count + this.relationship - 1);
					}
					break;
				case AccessibleNavigation.Down:
				case AccessibleNavigation.Right:
				case AccessibleNavigation.Next:
					if (this.relationship + 1 < this.owner.dgTable.RelationsList.Count)
					{
						return this.Parent.GetChild(this.Parent.GetChildCount() - this.owner.dgTable.RelationsList.Count + this.relationship + 1);
					}
					break;
				}
				return null;
			}

			// Token: 0x06006517 RID: 25879 RVA: 0x0017851B File Offset: 0x0017671B
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void Select(AccessibleSelection flags)
			{
				if ((flags & AccessibleSelection.TakeFocus) == AccessibleSelection.TakeFocus)
				{
					this.DataGrid.Focus();
				}
				if ((flags & AccessibleSelection.TakeSelection) == AccessibleSelection.TakeSelection)
				{
					this.Owner.FocusedRelation = this.relationship;
				}
			}

			// Token: 0x040039E1 RID: 14817
			private DataGridRelationshipRow owner;

			// Token: 0x040039E2 RID: 14818
			private int relationship;
		}
	}
}
