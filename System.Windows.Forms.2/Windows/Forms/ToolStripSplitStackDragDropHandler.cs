using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x020003B3 RID: 947
	internal sealed class ToolStripSplitStackDragDropHandler : IDropTarget, ISupportOleDropSource
	{
		// Token: 0x06003ED9 RID: 16089 RVA: 0x00110942 File Offset: 0x0010EB42
		public ToolStripSplitStackDragDropHandler(ToolStrip owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this.owner = owner;
		}

		// Token: 0x06003EDA RID: 16090 RVA: 0x00110960 File Offset: 0x0010EB60
		public void OnDragEnter(DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(ToolStripItem)))
			{
				e.Effect = DragDropEffects.Move;
				this.ShowItemDropPoint(this.owner.PointToClient(new Point(e.X, e.Y)));
			}
		}

		// Token: 0x06003EDB RID: 16091 RVA: 0x001109AE File Offset: 0x0010EBAE
		public void OnDragLeave(EventArgs e)
		{
			this.owner.ClearInsertionMark();
		}

		// Token: 0x06003EDC RID: 16092 RVA: 0x001109BC File Offset: 0x0010EBBC
		public void OnDragDrop(DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(ToolStripItem)))
			{
				ToolStripItem droppedItem = (ToolStripItem)e.Data.GetData(typeof(ToolStripItem));
				this.OnDropItem(droppedItem, this.owner.PointToClient(new Point(e.X, e.Y)));
			}
		}

		// Token: 0x06003EDD RID: 16093 RVA: 0x00110A20 File Offset: 0x0010EC20
		public void OnDragOver(DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(ToolStripItem)))
			{
				if (this.ShowItemDropPoint(this.owner.PointToClient(new Point(e.X, e.Y))))
				{
					e.Effect = DragDropEffects.Move;
					return;
				}
				if (this.owner != null)
				{
					this.owner.ClearInsertionMark();
				}
				e.Effect = DragDropEffects.None;
			}
		}

		// Token: 0x06003EDE RID: 16094 RVA: 0x000072B6 File Offset: 0x000054B6
		public void OnGiveFeedback(GiveFeedbackEventArgs e)
		{
		}

		// Token: 0x06003EDF RID: 16095 RVA: 0x000072B6 File Offset: 0x000054B6
		public void OnQueryContinueDrag(QueryContinueDragEventArgs e)
		{
		}

		// Token: 0x06003EE0 RID: 16096 RVA: 0x00110A8C File Offset: 0x0010EC8C
		private void OnDropItem(ToolStripItem droppedItem, Point ownerClientAreaRelativeDropPoint)
		{
			Point empty = Point.Empty;
			int itemInsertionIndex = this.GetItemInsertionIndex(ownerClientAreaRelativeDropPoint);
			if (itemInsertionIndex < 0)
			{
				if (itemInsertionIndex == -1 && this.owner.Items.Count == 0)
				{
					this.owner.Items.Add(droppedItem);
					this.owner.ClearInsertionMark();
				}
				return;
			}
			ToolStripItem toolStripItem = this.owner.Items[itemInsertionIndex];
			if (toolStripItem == droppedItem)
			{
				this.owner.ClearInsertionMark();
				return;
			}
			ToolStripSplitStackDragDropHandler.RelativeLocation relativeLocation = this.ComparePositions(toolStripItem.Bounds, ownerClientAreaRelativeDropPoint);
			droppedItem.Alignment = toolStripItem.Alignment;
			int num = Math.Max(0, itemInsertionIndex);
			if (relativeLocation == ToolStripSplitStackDragDropHandler.RelativeLocation.Above)
			{
				num = ((toolStripItem.Alignment == ToolStripItemAlignment.Left) ? num : (num + 1));
			}
			else if (relativeLocation == ToolStripSplitStackDragDropHandler.RelativeLocation.Below)
			{
				num = ((toolStripItem.Alignment == ToolStripItemAlignment.Left) ? num : (num - 1));
			}
			else if ((toolStripItem.Alignment == ToolStripItemAlignment.Left && relativeLocation == ToolStripSplitStackDragDropHandler.RelativeLocation.Left) || (toolStripItem.Alignment == ToolStripItemAlignment.Right && relativeLocation == ToolStripSplitStackDragDropHandler.RelativeLocation.Right))
			{
				num = Math.Max(0, (this.owner.RightToLeft == RightToLeft.Yes) ? (num + 1) : num);
			}
			else
			{
				num = Math.Max(0, (this.owner.RightToLeft == RightToLeft.No) ? (num + 1) : num);
			}
			if (this.owner.Items.IndexOf(droppedItem) < num)
			{
				num--;
			}
			this.owner.Items.MoveItem(Math.Max(0, num), droppedItem);
			this.owner.ClearInsertionMark();
		}

		// Token: 0x06003EE1 RID: 16097 RVA: 0x00110BEC File Offset: 0x0010EDEC
		private bool ShowItemDropPoint(Point ownerClientAreaRelativeDropPoint)
		{
			int itemInsertionIndex = this.GetItemInsertionIndex(ownerClientAreaRelativeDropPoint);
			if (itemInsertionIndex >= 0)
			{
				ToolStripItem toolStripItem = this.owner.Items[itemInsertionIndex];
				ToolStripSplitStackDragDropHandler.RelativeLocation relativeLocation = this.ComparePositions(toolStripItem.Bounds, ownerClientAreaRelativeDropPoint);
				Rectangle empty = Rectangle.Empty;
				switch (relativeLocation)
				{
				case ToolStripSplitStackDragDropHandler.RelativeLocation.Above:
					empty = new Rectangle(this.owner.Margin.Left, toolStripItem.Bounds.Top, this.owner.Width - this.owner.Margin.Horizontal - 1, ToolStrip.insertionBeamWidth);
					break;
				case ToolStripSplitStackDragDropHandler.RelativeLocation.Below:
					empty = new Rectangle(this.owner.Margin.Left, toolStripItem.Bounds.Bottom, this.owner.Width - this.owner.Margin.Horizontal - 1, ToolStrip.insertionBeamWidth);
					break;
				case ToolStripSplitStackDragDropHandler.RelativeLocation.Right:
					empty = new Rectangle(toolStripItem.Bounds.Right, this.owner.Margin.Top, ToolStrip.insertionBeamWidth, this.owner.Height - this.owner.Margin.Vertical - 1);
					break;
				case ToolStripSplitStackDragDropHandler.RelativeLocation.Left:
					empty = new Rectangle(toolStripItem.Bounds.Left, this.owner.Margin.Top, ToolStrip.insertionBeamWidth, this.owner.Height - this.owner.Margin.Vertical - 1);
					break;
				}
				this.owner.PaintInsertionMark(empty);
				return true;
			}
			if (this.owner.Items.Count == 0)
			{
				Rectangle displayRectangle = this.owner.DisplayRectangle;
				displayRectangle.Width = ToolStrip.insertionBeamWidth;
				this.owner.PaintInsertionMark(displayRectangle);
				return true;
			}
			return false;
		}

		// Token: 0x06003EE2 RID: 16098 RVA: 0x00110DE4 File Offset: 0x0010EFE4
		private int GetItemInsertionIndex(Point ownerClientAreaRelativeDropPoint)
		{
			for (int i = 0; i < this.owner.DisplayedItems.Count; i++)
			{
				Rectangle bounds = this.owner.DisplayedItems[i].Bounds;
				bounds.Inflate(this.owner.DisplayedItems[i].Margin.Size);
				if (bounds.Contains(ownerClientAreaRelativeDropPoint))
				{
					return this.owner.Items.IndexOf(this.owner.DisplayedItems[i]);
				}
			}
			if (this.owner.DisplayedItems.Count > 0)
			{
				int j = 0;
				while (j < this.owner.DisplayedItems.Count)
				{
					if (this.owner.DisplayedItems[j].Alignment == ToolStripItemAlignment.Right)
					{
						if (j > 0)
						{
							return this.owner.Items.IndexOf(this.owner.DisplayedItems[j - 1]);
						}
						return this.owner.Items.IndexOf(this.owner.DisplayedItems[j]);
					}
					else
					{
						j++;
					}
				}
				return this.owner.Items.IndexOf(this.owner.DisplayedItems[this.owner.DisplayedItems.Count - 1]);
			}
			return -1;
		}

		// Token: 0x06003EE3 RID: 16099 RVA: 0x00110F3C File Offset: 0x0010F13C
		private ToolStripSplitStackDragDropHandler.RelativeLocation ComparePositions(Rectangle orig, Point check)
		{
			if (this.owner.Orientation == Orientation.Horizontal)
			{
				int num = orig.Width / 2;
				if (orig.Left + num >= check.X)
				{
					return ToolStripSplitStackDragDropHandler.RelativeLocation.Left;
				}
				if (orig.Right - num <= check.X)
				{
					return ToolStripSplitStackDragDropHandler.RelativeLocation.Right;
				}
			}
			if (this.owner.Orientation == Orientation.Vertical)
			{
				int num2 = orig.Height / 2;
				return (check.Y <= orig.Top + num2) ? ToolStripSplitStackDragDropHandler.RelativeLocation.Above : ToolStripSplitStackDragDropHandler.RelativeLocation.Below;
			}
			return ToolStripSplitStackDragDropHandler.RelativeLocation.Left;
		}

		// Token: 0x04002498 RID: 9368
		private ToolStrip owner;

		// Token: 0x020007FB RID: 2043
		private enum RelativeLocation
		{
			// Token: 0x040042EF RID: 17135
			Above,
			// Token: 0x040042F0 RID: 17136
			Below,
			// Token: 0x040042F1 RID: 17137
			Right,
			// Token: 0x040042F2 RID: 17138
			Left
		}
	}
}
