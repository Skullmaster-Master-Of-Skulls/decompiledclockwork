using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x02000405 RID: 1029
	internal class ToolStripSplitStackLayout : LayoutEngine
	{
		// Token: 0x06004722 RID: 18210 RVA: 0x0012A24C File Offset: 0x0012844C
		internal ToolStripSplitStackLayout(ToolStrip owner)
		{
			this.toolStrip = owner;
		}

		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x06004723 RID: 18211 RVA: 0x0012A266 File Offset: 0x00128466
		// (set) Token: 0x06004724 RID: 18212 RVA: 0x0012A26E File Offset: 0x0012846E
		protected int BackwardsWalkingIndex
		{
			get
			{
				return this.backwardsWalkingIndex;
			}
			set
			{
				this.backwardsWalkingIndex = value;
			}
		}

		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x06004725 RID: 18213 RVA: 0x0012A277 File Offset: 0x00128477
		// (set) Token: 0x06004726 RID: 18214 RVA: 0x0012A27F File Offset: 0x0012847F
		protected int ForwardsWalkingIndex
		{
			get
			{
				return this.forwardsWalkingIndex;
			}
			set
			{
				this.forwardsWalkingIndex = value;
			}
		}

		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x06004727 RID: 18215 RVA: 0x0012A288 File Offset: 0x00128488
		private Size OverflowButtonSize
		{
			get
			{
				ToolStrip toolStrip = this.ToolStrip;
				if (!toolStrip.CanOverflow)
				{
					return Size.Empty;
				}
				Size sz = toolStrip.OverflowButton.AutoSize ? toolStrip.OverflowButton.GetPreferredSize(this.displayRectangle.Size) : toolStrip.OverflowButton.Size;
				return sz + toolStrip.OverflowButton.Margin.Size;
			}
		}

		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x06004728 RID: 18216 RVA: 0x0012A2F4 File Offset: 0x001284F4
		// (set) Token: 0x06004729 RID: 18217 RVA: 0x0012A2FC File Offset: 0x001284FC
		private int OverflowSpace
		{
			get
			{
				return this.overflowSpace;
			}
			set
			{
				this.overflowSpace = value;
			}
		}

		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x0600472A RID: 18218 RVA: 0x0012A305 File Offset: 0x00128505
		// (set) Token: 0x0600472B RID: 18219 RVA: 0x0012A30D File Offset: 0x0012850D
		private bool OverflowRequired
		{
			get
			{
				return this.overflowRequired;
			}
			set
			{
				this.overflowRequired = value;
			}
		}

		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x0600472C RID: 18220 RVA: 0x0012A316 File Offset: 0x00128516
		public ToolStrip ToolStrip
		{
			get
			{
				return this.toolStrip;
			}
		}

		// Token: 0x0600472D RID: 18221 RVA: 0x0012A320 File Offset: 0x00128520
		private void CalculatePlacementsHorizontal()
		{
			this.ResetItemPlacements();
			ToolStrip toolStrip = this.ToolStrip;
			int num = 0;
			if (this.ToolStrip.CanOverflow)
			{
				this.ForwardsWalkingIndex = 0;
				while (this.ForwardsWalkingIndex < toolStrip.Items.Count)
				{
					ToolStripItem toolStripItem = toolStrip.Items[this.ForwardsWalkingIndex];
					if (((IArrangedElement)toolStripItem).ParticipatesInLayout)
					{
						if (toolStripItem.Overflow == ToolStripItemOverflow.Always)
						{
							this.OverflowRequired = true;
						}
						if (toolStripItem.Overflow != ToolStripItemOverflow.Always && toolStripItem.Placement == ToolStripItemPlacement.None)
						{
							num += (toolStripItem.AutoSize ? toolStripItem.GetPreferredSize(this.displayRectangle.Size) : toolStripItem.Size).Width + toolStripItem.Margin.Horizontal;
							int num2 = this.OverflowRequired ? this.OverflowButtonSize.Width : 0;
							if (num > this.displayRectangle.Width - num2)
							{
								int num3 = this.SendNextItemToOverflow(num + num2 - this.displayRectangle.Width, true);
								num -= num3;
							}
						}
					}
					int num4 = this.ForwardsWalkingIndex;
					this.ForwardsWalkingIndex = num4 + 1;
				}
			}
			this.PlaceItems();
		}

		// Token: 0x0600472E RID: 18222 RVA: 0x0012A454 File Offset: 0x00128654
		private void CalculatePlacementsVertical()
		{
			this.ResetItemPlacements();
			ToolStrip toolStrip = this.ToolStrip;
			int num = 0;
			if (this.ToolStrip.CanOverflow)
			{
				this.ForwardsWalkingIndex = 0;
				while (this.ForwardsWalkingIndex < this.ToolStrip.Items.Count)
				{
					ToolStripItem toolStripItem = toolStrip.Items[this.ForwardsWalkingIndex];
					if (((IArrangedElement)toolStripItem).ParticipatesInLayout)
					{
						if (toolStripItem.Overflow == ToolStripItemOverflow.Always)
						{
							this.OverflowRequired = true;
						}
						if (toolStripItem.Overflow != ToolStripItemOverflow.Always && toolStripItem.Placement == ToolStripItemPlacement.None)
						{
							Size size = toolStripItem.AutoSize ? toolStripItem.GetPreferredSize(this.displayRectangle.Size) : toolStripItem.Size;
							int num2 = this.OverflowRequired ? this.OverflowButtonSize.Height : 0;
							num += size.Height + toolStripItem.Margin.Vertical;
							if (num > this.displayRectangle.Height - num2)
							{
								int num3 = this.SendNextItemToOverflow(num - this.displayRectangle.Height, false);
								num -= num3;
							}
						}
					}
					int num4 = this.ForwardsWalkingIndex;
					this.ForwardsWalkingIndex = num4 + 1;
				}
			}
			this.PlaceItems();
		}

		// Token: 0x0600472F RID: 18223 RVA: 0x0012A588 File Offset: 0x00128788
		internal override Size GetPreferredSize(IArrangedElement container, Size proposedConstraints)
		{
			if (!(container is ToolStrip))
			{
				throw new NotSupportedException(SR.GetString("ToolStripSplitStackLayoutContainerMustBeAToolStrip"));
			}
			if (this.toolStrip.LayoutStyle == ToolStripLayoutStyle.HorizontalStackWithOverflow)
			{
				return ToolStrip.GetPreferredSizeHorizontal(container, proposedConstraints);
			}
			return ToolStrip.GetPreferredSizeVertical(container, proposedConstraints);
		}

		// Token: 0x06004730 RID: 18224 RVA: 0x0012A5BF File Offset: 0x001287BF
		private void InvalidateLayout()
		{
			this.forwardsWalkingIndex = 0;
			this.backwardsWalkingIndex = -1;
			this.overflowSpace = 0;
			this.overflowRequired = false;
			this.displayRectangle = Rectangle.Empty;
		}

		// Token: 0x06004731 RID: 18225 RVA: 0x0012A5E8 File Offset: 0x001287E8
		internal override bool LayoutCore(IArrangedElement container, LayoutEventArgs layoutEventArgs)
		{
			if (!(container is ToolStrip))
			{
				throw new NotSupportedException(SR.GetString("ToolStripSplitStackLayoutContainerMustBeAToolStrip"));
			}
			this.InvalidateLayout();
			this.displayRectangle = this.toolStrip.DisplayRectangle;
			this.noMansLand = this.displayRectangle.Location;
			this.noMansLand.X = this.noMansLand.X + (this.toolStrip.ClientSize.Width + 1);
			this.noMansLand.Y = this.noMansLand.Y + (this.toolStrip.ClientSize.Height + 1);
			if (this.toolStrip.LayoutStyle == ToolStripLayoutStyle.HorizontalStackWithOverflow)
			{
				this.LayoutHorizontal();
			}
			else
			{
				this.LayoutVertical();
			}
			return CommonProperties.GetAutoSize(container);
		}

		// Token: 0x06004732 RID: 18226 RVA: 0x0012A6A8 File Offset: 0x001288A8
		private bool LayoutHorizontal()
		{
			ToolStrip toolStrip = this.ToolStrip;
			Rectangle clientRectangle = toolStrip.ClientRectangle;
			int num = this.displayRectangle.Right;
			int num2 = this.displayRectangle.Left;
			bool result = false;
			Size itemSize = Size.Empty;
			Rectangle rectangle = Rectangle.Empty;
			Rectangle rectangle2 = Rectangle.Empty;
			this.CalculatePlacementsHorizontal();
			bool flag = toolStrip.CanOverflow && (this.OverflowRequired || this.OverflowSpace >= this.OverflowButtonSize.Width);
			toolStrip.OverflowButton.Visible = flag;
			if (flag)
			{
				if (toolStrip.RightToLeft == RightToLeft.No)
				{
					num = clientRectangle.Right;
				}
				else
				{
					num2 = clientRectangle.Left;
				}
			}
			int i = -1;
			while (i < toolStrip.Items.Count)
			{
				ToolStripItem toolStripItem;
				if (i == -1)
				{
					if (flag)
					{
						toolStripItem = toolStrip.OverflowButton;
						toolStripItem.SetPlacement(ToolStripItemPlacement.Main);
						itemSize = this.OverflowButtonSize;
						goto IL_11F;
					}
					toolStripItem = toolStrip.OverflowButton;
					toolStripItem.SetPlacement(ToolStripItemPlacement.None);
				}
				else
				{
					toolStripItem = toolStrip.Items[i];
					if (((IArrangedElement)toolStripItem).ParticipatesInLayout)
					{
						itemSize = (toolStripItem.AutoSize ? toolStripItem.GetPreferredSize(Size.Empty) : toolStripItem.Size);
						goto IL_11F;
					}
				}
				IL_356:
				i++;
				continue;
				IL_11F:
				if (!flag && toolStripItem.Overflow == ToolStripItemOverflow.AsNeeded && toolStripItem.Placement == ToolStripItemPlacement.Overflow)
				{
					toolStripItem.SetPlacement(ToolStripItemPlacement.Main);
				}
				if (toolStripItem != null && toolStripItem.Placement == ToolStripItemPlacement.Main)
				{
					int num3 = this.displayRectangle.Left;
					int num4 = this.displayRectangle.Top;
					Padding margin = toolStripItem.Margin;
					if ((toolStripItem.Alignment == ToolStripItemAlignment.Right && toolStrip.RightToLeft == RightToLeft.No) || (toolStripItem.Alignment == ToolStripItemAlignment.Left && toolStrip.RightToLeft == RightToLeft.Yes))
					{
						num3 = num - (margin.Right + itemSize.Width);
						num4 += margin.Top;
						num = num3 - margin.Left;
						rectangle2 = ((rectangle2 == Rectangle.Empty) ? new Rectangle(num3, num4, itemSize.Width, itemSize.Height) : Rectangle.Union(rectangle2, new Rectangle(num3, num4, itemSize.Width, itemSize.Height)));
					}
					else
					{
						num3 = num2 + margin.Left;
						num4 += margin.Top;
						num2 = num3 + itemSize.Width + margin.Right;
						rectangle = ((rectangle == Rectangle.Empty) ? new Rectangle(num3, num4, itemSize.Width, itemSize.Height) : Rectangle.Union(rectangle, new Rectangle(num3, num4, itemSize.Width, itemSize.Height)));
					}
					toolStripItem.ParentInternal = this.ToolStrip;
					Point itemLocation = new Point(num3, num4);
					if (!clientRectangle.Contains(num3, num4))
					{
						toolStripItem.SetPlacement(ToolStripItemPlacement.None);
					}
					else if (rectangle2.Width > 0 && rectangle.Width > 0 && rectangle2.IntersectsWith(rectangle))
					{
						itemLocation = this.noMansLand;
						toolStripItem.SetPlacement(ToolStripItemPlacement.None);
					}
					if (toolStripItem.AutoSize)
					{
						itemSize.Height = Math.Max(this.displayRectangle.Height - margin.Vertical, 0);
					}
					else
					{
						itemLocation.Y = LayoutUtils.VAlign(toolStripItem.Size, this.displayRectangle, AnchorStyles.None).Y;
					}
					this.SetItemLocation(toolStripItem, itemLocation, itemSize);
					goto IL_356;
				}
				toolStripItem.ParentInternal = ((toolStripItem.Placement == ToolStripItemPlacement.Overflow) ? toolStrip.OverflowButton.DropDown : null);
				goto IL_356;
			}
			return result;
		}

		// Token: 0x06004733 RID: 18227 RVA: 0x0012AA28 File Offset: 0x00128C28
		private bool LayoutVertical()
		{
			ToolStrip toolStrip = this.ToolStrip;
			Rectangle clientRectangle = toolStrip.ClientRectangle;
			int num = this.displayRectangle.Bottom;
			int num2 = this.displayRectangle.Top;
			bool result = false;
			Size itemSize = Size.Empty;
			Rectangle rectangle = Rectangle.Empty;
			Rectangle rectangle2 = Rectangle.Empty;
			Size size = this.displayRectangle.Size;
			DockStyle dock = toolStrip.Dock;
			if (toolStrip.AutoSize && ((!toolStrip.IsInToolStripPanel && dock == DockStyle.Left) || dock == DockStyle.Right))
			{
				size = ToolStrip.GetPreferredSizeVertical(toolStrip, Size.Empty) - toolStrip.Padding.Size;
			}
			this.CalculatePlacementsVertical();
			bool flag = toolStrip.CanOverflow && (this.OverflowRequired || this.OverflowSpace >= this.OverflowButtonSize.Height);
			toolStrip.OverflowButton.Visible = flag;
			int i = -1;
			while (i < this.ToolStrip.Items.Count)
			{
				ToolStripItem toolStripItem;
				if (i == -1)
				{
					if (flag)
					{
						toolStripItem = toolStrip.OverflowButton;
						toolStripItem.SetPlacement(ToolStripItemPlacement.Main);
						itemSize = this.OverflowButtonSize;
						goto IL_153;
					}
					toolStripItem = toolStrip.OverflowButton;
					toolStripItem.SetPlacement(ToolStripItemPlacement.None);
				}
				else
				{
					toolStripItem = toolStrip.Items[i];
					if (((IArrangedElement)toolStripItem).ParticipatesInLayout)
					{
						itemSize = (toolStripItem.AutoSize ? toolStripItem.GetPreferredSize(Size.Empty) : toolStripItem.Size);
						goto IL_153;
					}
				}
				IL_366:
				i++;
				continue;
				IL_153:
				if (!flag && toolStripItem.Overflow == ToolStripItemOverflow.AsNeeded && toolStripItem.Placement == ToolStripItemPlacement.Overflow)
				{
					toolStripItem.SetPlacement(ToolStripItemPlacement.Main);
				}
				if (toolStripItem != null && toolStripItem.Placement == ToolStripItemPlacement.Main)
				{
					Padding margin = toolStripItem.Margin;
					int x = this.displayRectangle.Left + margin.Left;
					int num3 = this.displayRectangle.Top;
					ToolStripItemAlignment alignment = toolStripItem.Alignment;
					if (alignment != ToolStripItemAlignment.Left && alignment == ToolStripItemAlignment.Right)
					{
						num3 = num - (margin.Bottom + itemSize.Height);
						num = num3 - margin.Top;
						rectangle2 = ((rectangle2 == Rectangle.Empty) ? new Rectangle(x, num3, itemSize.Width, itemSize.Height) : Rectangle.Union(rectangle2, new Rectangle(x, num3, itemSize.Width, itemSize.Height)));
					}
					else
					{
						num3 = num2 + margin.Top;
						num2 = num3 + itemSize.Height + margin.Bottom;
						rectangle = ((rectangle == Rectangle.Empty) ? new Rectangle(x, num3, itemSize.Width, itemSize.Height) : Rectangle.Union(rectangle, new Rectangle(x, num3, itemSize.Width, itemSize.Height)));
					}
					toolStripItem.ParentInternal = this.ToolStrip;
					Point itemLocation = new Point(x, num3);
					if (!clientRectangle.Contains(x, num3))
					{
						toolStripItem.SetPlacement(ToolStripItemPlacement.None);
					}
					else if (rectangle2.Width > 0 && rectangle.Width > 0 && rectangle2.IntersectsWith(rectangle))
					{
						itemLocation = this.noMansLand;
						toolStripItem.SetPlacement(ToolStripItemPlacement.None);
					}
					if (toolStripItem.AutoSize)
					{
						itemSize.Width = Math.Max(size.Width - margin.Horizontal - 1, 0);
					}
					else
					{
						itemLocation.X = LayoutUtils.HAlign(toolStripItem.Size, this.displayRectangle, AnchorStyles.None).X;
					}
					this.SetItemLocation(toolStripItem, itemLocation, itemSize);
					goto IL_366;
				}
				toolStripItem.ParentInternal = ((toolStripItem.Placement == ToolStripItemPlacement.Overflow) ? toolStrip.OverflowButton.DropDown : null);
				goto IL_366;
			}
			return result;
		}

		// Token: 0x06004734 RID: 18228 RVA: 0x0012ADBC File Offset: 0x00128FBC
		private void SetItemLocation(ToolStripItem item, Point itemLocation, Size itemSize)
		{
			if (item.Placement == ToolStripItemPlacement.Main && !(item is ToolStripOverflowButton))
			{
				bool flag = this.ToolStrip.LayoutStyle == ToolStripLayoutStyle.HorizontalStackWithOverflow;
				Rectangle rectangle = this.displayRectangle;
				Rectangle rectangle2 = new Rectangle(itemLocation, itemSize);
				if (flag)
				{
					if (rectangle2.Right > this.displayRectangle.Right || rectangle2.Left < this.displayRectangle.Left)
					{
						itemLocation = this.noMansLand;
						item.SetPlacement(ToolStripItemPlacement.None);
					}
				}
				else if (rectangle2.Bottom > this.displayRectangle.Bottom || rectangle2.Top < this.displayRectangle.Top)
				{
					itemLocation = this.noMansLand;
					item.SetPlacement(ToolStripItemPlacement.None);
				}
			}
			item.SetBounds(new Rectangle(itemLocation, itemSize));
		}

		// Token: 0x06004735 RID: 18229 RVA: 0x0012AE80 File Offset: 0x00129080
		private void PlaceItems()
		{
			ToolStrip toolStrip = this.ToolStrip;
			for (int i = 0; i < toolStrip.Items.Count; i++)
			{
				ToolStripItem toolStripItem = toolStrip.Items[i];
				if (toolStripItem.Placement == ToolStripItemPlacement.None)
				{
					if (toolStripItem.Overflow != ToolStripItemOverflow.Always)
					{
						toolStripItem.SetPlacement(ToolStripItemPlacement.Main);
					}
					else
					{
						toolStripItem.SetPlacement(ToolStripItemPlacement.Overflow);
					}
				}
			}
		}

		// Token: 0x06004736 RID: 18230 RVA: 0x0012AEDC File Offset: 0x001290DC
		private void ResetItemPlacements()
		{
			ToolStrip toolStrip = this.ToolStrip;
			for (int i = 0; i < toolStrip.Items.Count; i++)
			{
				if (toolStrip.Items[i].Placement == ToolStripItemPlacement.Overflow)
				{
					toolStrip.Items[i].ParentInternal = null;
				}
				toolStrip.Items[i].SetPlacement(ToolStripItemPlacement.None);
			}
		}

		// Token: 0x06004737 RID: 18231 RVA: 0x0012AF40 File Offset: 0x00129140
		private int SendNextItemToOverflow(int spaceNeeded, bool horizontal)
		{
			int num = 0;
			int num2 = this.BackwardsWalkingIndex;
			this.BackwardsWalkingIndex = ((num2 == -1) ? (this.ToolStrip.Items.Count - 1) : (num2 - 1));
			while (this.BackwardsWalkingIndex >= 0)
			{
				ToolStripItem toolStripItem = this.ToolStrip.Items[this.BackwardsWalkingIndex];
				if (((IArrangedElement)toolStripItem).ParticipatesInLayout)
				{
					Padding margin = toolStripItem.Margin;
					if (toolStripItem.Overflow == ToolStripItemOverflow.AsNeeded && toolStripItem.Placement != ToolStripItemPlacement.Overflow)
					{
						Size size = toolStripItem.AutoSize ? toolStripItem.GetPreferredSize(this.displayRectangle.Size) : toolStripItem.Size;
						if (this.BackwardsWalkingIndex <= this.ForwardsWalkingIndex)
						{
							num += (horizontal ? (size.Width + margin.Horizontal) : (size.Height + margin.Vertical));
						}
						toolStripItem.SetPlacement(ToolStripItemPlacement.Overflow);
						if (!this.OverflowRequired)
						{
							spaceNeeded += (horizontal ? this.OverflowButtonSize.Width : this.OverflowButtonSize.Height);
							this.OverflowRequired = true;
						}
						this.OverflowSpace += (horizontal ? (size.Width + margin.Horizontal) : (size.Height + margin.Vertical));
					}
					if (num > spaceNeeded)
					{
						break;
					}
				}
				int num3 = this.BackwardsWalkingIndex;
				this.BackwardsWalkingIndex = num3 - 1;
			}
			return num;
		}

		// Token: 0x040026D1 RID: 9937
		private int backwardsWalkingIndex;

		// Token: 0x040026D2 RID: 9938
		private int forwardsWalkingIndex;

		// Token: 0x040026D3 RID: 9939
		private ToolStrip toolStrip;

		// Token: 0x040026D4 RID: 9940
		private int overflowSpace;

		// Token: 0x040026D5 RID: 9941
		private bool overflowRequired;

		// Token: 0x040026D6 RID: 9942
		private Point noMansLand;

		// Token: 0x040026D7 RID: 9943
		private Rectangle displayRectangle = Rectangle.Empty;

		// Token: 0x040026D8 RID: 9944
		internal static readonly TraceSwitch DebugLayoutTraceSwitch;
	}
}
