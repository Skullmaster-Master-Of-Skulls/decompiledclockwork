using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x020016FE RID: 5886
	[ParseChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	[DefaultProperty("Items")]
	public class ExtendedLabel : ChartBaseLabel
	{
		// Token: 0x170045C7 RID: 17863
		// (get) Token: 0x0600E4B5 RID: 58549 RVA: 0x0032C5C4 File Offset: 0x0032A7C4
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public StyleExtendedLabel Appearance
		{
			get
			{
				return (StyleExtendedLabel)this.appearance;
			}
		}

		// Token: 0x170045C8 RID: 17864
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual LabelItem this[int itemIndex]
		{
			get
			{
				return this.extendedLabelItems[itemIndex];
			}
			set
			{
				this.extendedLabelItems[itemIndex] = value;
			}
		}

		// Token: 0x170045C9 RID: 17865
		// (get) Token: 0x0600E4B8 RID: 58552 RVA: 0x0032C5EE File Offset: 0x0032A7EE
		[Category("Items")]
		[Editor(typeof(LabelsCollectionEditor), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Items collection.")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ChartLabelsCollection Items
		{
			get
			{
				return this.extendedLabelItems;
			}
		}

		// Token: 0x0600E4B9 RID: 58553 RVA: 0x0032C5F6 File Offset: 0x0032A7F6
		public ExtendedLabel() : this(null, null, new StyleExtendedLabel(), new TextBlockLegend(), null)
		{
		}

		// Token: 0x0600E4BA RID: 58554 RVA: 0x0032C60B File Offset: 0x0032A80B
		public ExtendedLabel(object parent) : this(parent, null, new StyleExtendedLabel(), new TextBlockLegend(), null)
		{
		}

		// Token: 0x0600E4BB RID: 58555 RVA: 0x0032C620 File Offset: 0x0032A820
		public ExtendedLabel(string text) : this(null, null, new StyleExtendedLabel(), new TextBlockLegend(), text)
		{
		}

		// Token: 0x0600E4BC RID: 58556 RVA: 0x0032C635 File Offset: 0x0032A835
		public ExtendedLabel(StyleExtendedLabel appearance) : this(null, null, appearance, new TextBlockLegend(), null)
		{
		}

		// Token: 0x0600E4BD RID: 58557 RVA: 0x0032C646 File Offset: 0x0032A846
		public ExtendedLabel(StyleExtendedLabel appearance, object parent) : this(parent, null, appearance, new TextBlockLegend(), null)
		{
		}

		// Token: 0x0600E4BE RID: 58558 RVA: 0x0032C657 File Offset: 0x0032A857
		public ExtendedLabel(StyleExtendedLabel appearance, string text) : this(null, null, appearance, new TextBlockLegend(), text)
		{
		}

		// Token: 0x0600E4BF RID: 58559 RVA: 0x0032C668 File Offset: 0x0032A868
		public ExtendedLabel(TextBlock textBlock) : this(null, null, new StyleExtendedLabel(), textBlock, null)
		{
		}

		// Token: 0x0600E4C0 RID: 58560 RVA: 0x0032C679 File Offset: 0x0032A879
		public ExtendedLabel(object parent, IContainer container, TextBlock textBlock) : this(parent, container, new StyleExtendedLabel(), textBlock, null)
		{
		}

		// Token: 0x0600E4C1 RID: 58561 RVA: 0x0032C68C File Offset: 0x0032A88C
		public ExtendedLabel(object parent, IContainer container, StyleExtendedLabel appearance, TextBlock textBlock, string text) : base(parent, container, textBlock, appearance)
		{
			if (textBlock != null && text != null)
			{
				textBlock.Text = text;
			}
			this.extendedLabelItems = new ChartLabelsCollection();
			this.extendedLabelItems.Parent = this;
			this.extendedLabelItems.CollectionChange = new CollectionChange(this.ReCreateOrderList);
			this.Appearance.styleContainerObject = this;
		}

		// Token: 0x0600E4C2 RID: 58562 RVA: 0x0032C6F0 File Offset: 0x0032A8F0
		internal SizeF GetMaxAvailableContentSize()
		{
			if (!this.Appearance.Dimensions.AutoSize)
			{
				return new SizeF(this.Appearance.Dimensions.Width.PixelValue, this.Appearance.Dimensions.Height.PixelValue);
			}
			float num = 0f;
			float num2 = 0f;
			AlignedPositions alignedPosition = this.Appearance.Position.AlignedPosition;
			if (alignedPosition <= AlignedPositions.Center)
			{
				switch (alignedPosition)
				{
				case AlignedPositions.None:
					num = this.Appearance.Dimensions.Margins.Top.PixelValue + this.Appearance.Dimensions.Margins.Bottom.PixelValue;
					num2 = this.Appearance.Dimensions.Margins.Left.PixelValue + this.Appearance.Dimensions.Margins.Right.PixelValue;
					break;
				case AlignedPositions.TopLeft:
					num = this.Appearance.Dimensions.Margins.Top.PixelValue;
					num2 = this.Appearance.Dimensions.Margins.Left.PixelValue;
					break;
				case AlignedPositions.Top:
					num = this.Appearance.Dimensions.Margins.Top.PixelValue;
					break;
				case (AlignedPositions)3:
					break;
				case AlignedPositions.TopRight:
					num = this.Appearance.Dimensions.Margins.Top.PixelValue;
					num2 = this.Appearance.Dimensions.Margins.Right.PixelValue;
					break;
				default:
					if (alignedPosition != AlignedPositions.Left)
					{
						if (alignedPosition != AlignedPositions.Center)
						{
						}
					}
					else
					{
						num2 = this.Appearance.Dimensions.Margins.Left.PixelValue;
					}
					break;
				}
			}
			else if (alignedPosition <= AlignedPositions.BottomLeft)
			{
				if (alignedPosition != AlignedPositions.Right)
				{
					if (alignedPosition == AlignedPositions.BottomLeft)
					{
						num = this.Appearance.Dimensions.Margins.Bottom.PixelValue;
						num2 = this.Appearance.Dimensions.Margins.Left.PixelValue;
					}
				}
				else
				{
					num2 = this.Appearance.Dimensions.Margins.Right.PixelValue;
				}
			}
			else if (alignedPosition != AlignedPositions.Bottom)
			{
				if (alignedPosition == AlignedPositions.BottomRight)
				{
					num = this.Appearance.Dimensions.Margins.Bottom.PixelValue;
					num2 = this.Appearance.Dimensions.Margins.Right.PixelValue;
				}
			}
			else
			{
				num = this.Appearance.Dimensions.Margins.Bottom.PixelValue;
			}
			Dimensions dimensions = Style.GetStyleProperty(base.Container, StyleProperties.Dimensions) as Dimensions;
			if (dimensions != null)
			{
				return new SizeF(dimensions.Width.PixelValue - num2 - this.Appearance.Dimensions.Paddings.Left.PixelValue - this.Appearance.Dimensions.Paddings.Right.PixelValue, dimensions.Height.PixelValue - num - this.Appearance.Dimensions.Paddings.Top.PixelValue - this.Appearance.Dimensions.Paddings.Bottom.PixelValue);
			}
			return Size.Empty;
		}

		// Token: 0x0600E4C3 RID: 58563 RVA: 0x0032CA4B File Offset: 0x0032AC4B
		internal override bool IsVisible()
		{
			return base.IsVisible() || this.extendedLabelItems.IsVisible();
		}

		// Token: 0x0600E4C4 RID: 58564 RVA: 0x0032CA64 File Offset: 0x0032AC64
		internal override SizeF Measure(RenderEngine renderEngine)
		{
			SizeF sizeF = base.Measure(renderEngine);
			float num = sizeF.Height;
			float num2 = sizeF.Width;
			Overflow overflow = this.Appearance.Overflow;
			int count = this.Items.Count;
			num2 -= this.Appearance.Dimensions.Paddings.Left.PixelValue;
			num -= this.Appearance.Dimensions.Paddings.Top.PixelValue;
			List<SizeF> list = new List<SizeF>();
			LabelItem labelItem = new LabelItem();
			for (int i = 0; i < count; i++)
			{
				labelItem = this.Items[i];
				SizeF sizeF2 = labelItem.Measure(renderEngine);
				list.Add(new SizeF((float)Math.Round((double)sizeF2.Width) + labelItem.Appearance.Dimensions.Margins.Left.PixelValue + labelItem.Appearance.Dimensions.Margins.Right.PixelValue, (float)Math.Round((double)sizeF2.Height) + labelItem.Appearance.Dimensions.Margins.Top.PixelValue + labelItem.Appearance.Dimensions.Margins.Bottom.PixelValue));
				labelItem.Appearance.Dimensions.Width = new Unit((float)Math.Round((double)sizeF2.Width), UnitType.Pixel);
				labelItem.Appearance.Dimensions.Height = new Unit((float)Math.Round((double)sizeF2.Height), UnitType.Pixel);
			}
			SizeF maxAvailableContentSize = this.GetMaxAvailableContentSize();
			float num3;
			if (this.TextBlock.Appearance.Position.IsLeft)
			{
				num3 = num2;
			}
			else
			{
				num3 = this.Appearance.Dimensions.Paddings.Left.PixelValue;
			}
			float num4 = num2;
			float num5;
			if (this.TextBlock.Appearance.Position.IsTop)
			{
				num5 = num;
			}
			else
			{
				num5 = this.Appearance.Dimensions.Paddings.Top.PixelValue;
			}
			float num6 = num;
			List<SizeF> list2 = new List<SizeF>();
			int num7 = 0;
			int num8 = count * count;
			int num9 = 0;
			SizeF maxSize;
			while (num9 < count && num7 < num8)
			{
				labelItem = this.Items[num9];
				list2.Add(list[num9]);
				switch (overflow)
				{
				case Overflow.Auto:
				case Overflow.Row:
				{
					labelItem.Appearance.Position.X = num3;
					labelItem.Appearance.Position.Y = num5;
					if (list[num9].Width > maxAvailableContentSize.Width)
					{
						list[num9] = new SizeF(maxAvailableContentSize.Width - num2 - 1f, list[num9].Height);
					}
					float num10 = num3 + list[num9].Width;
					if (num10 > maxAvailableContentSize.Width)
					{
						list2.RemoveAt(list2.Count - 1);
						maxSize = RenderEngine.GetMaxSize(list2);
						if (this.TextBlock.Appearance.Position.IsLeft)
						{
							num3 = num2;
						}
						else
						{
							num3 = this.Appearance.Dimensions.Paddings.Left.PixelValue;
						}
						float num11 = num5 + maxSize.Height;
						if (num11 > maxAvailableContentSize.Height - maxSize.Height)
						{
							for (int j = num9; j < count; j++)
							{
								this.Items[j].Visible = false;
							}
						}
						else
						{
							num5 += maxSize.Height;
							list2.Clear();
							num9--;
						}
					}
					else
					{
						num3 = num10;
						if (num3 > num4)
						{
							num4 = num3;
						}
					}
					break;
				}
				case Overflow.Column:
				{
					labelItem.Appearance.Position.X = num3;
					labelItem.Appearance.Position.Y = num5;
					float num12 = num5 + list[num9].Height;
					if (num12 > maxAvailableContentSize.Height)
					{
						list2.RemoveAt(list2.Count - 1);
						maxSize = RenderEngine.GetMaxSize(list2);
						if (this.TextBlock.Appearance.Position.IsTop)
						{
							num5 = num;
						}
						else
						{
							num5 = this.Appearance.Dimensions.Paddings.Top.PixelValue;
						}
						float num13 = num3 + maxSize.Width;
						if (num13 > maxAvailableContentSize.Width - maxSize.Width)
						{
							for (int k = num9; k < count; k++)
							{
								this.Items[k].Visible = false;
							}
						}
						else
						{
							num3 = num13;
							list2.Clear();
							num9--;
						}
					}
					else
					{
						num5 = num12;
						if (num5 > num6)
						{
							num6 = num5;
						}
					}
					break;
				}
				}
				num9++;
				num7++;
			}
			maxSize = RenderEngine.GetMaxSize(list2);
			num4 -= labelItem.Appearance.Dimensions.Margins.Right.PixelValue + labelItem.Appearance.Dimensions.Margins.Left.PixelValue;
			num3 -= labelItem.Appearance.Dimensions.Margins.Right.PixelValue + labelItem.Appearance.Dimensions.Margins.Left.PixelValue;
			num5 -= labelItem.Appearance.Dimensions.Margins.Bottom.PixelValue + labelItem.Appearance.Dimensions.Margins.Top.PixelValue;
			num6 -= labelItem.Appearance.Dimensions.Margins.Bottom.PixelValue + labelItem.Appearance.Dimensions.Margins.Top.PixelValue;
			switch (overflow)
			{
			case Overflow.Auto:
			case Overflow.Row:
				num2 = (float)Math.Round((double)num4) + this.Appearance.Dimensions.Paddings.Right.PixelValue;
				num5 += (float)Math.Round((double)maxSize.Height);
				num = (float)Math.Round((double)num5) + this.Appearance.Dimensions.Paddings.Bottom.PixelValue;
				break;
			case Overflow.Column:
				num3 += (float)Math.Round((double)maxSize.Width);
				num2 = Math.Max(num2, (float)Math.Round((double)num3) + this.Appearance.Dimensions.Paddings.Right.PixelValue);
				num = (float)Math.Round((double)num6) + this.Appearance.Dimensions.Paddings.Bottom.PixelValue;
				break;
			}
			return new SizeF(num2, num);
		}

		// Token: 0x0600E4C5 RID: 58565 RVA: 0x0032D13C File Offset: 0x0032B33C
		internal void ReCreateOrderList()
		{
			base.OrderList.Clear();
			base.OrderList.Add(this.chartBaseLabelMarker);
			base.OrderList.Add(this.chartBaseLabelTextBlock);
			for (int i = 0; i < this.Items.Count; i++)
			{
				LabelItem item = this.Items[i];
				base.OrderList.Add(item);
			}
		}

		// Token: 0x0600E4C6 RID: 58566 RVA: 0x0032D1A5 File Offset: 0x0032B3A5
		public void Clear()
		{
			this.extendedLabelItems.Clear();
		}

		// Token: 0x0600E4C7 RID: 58567 RVA: 0x0032D1B4 File Offset: 0x0032B3B4
		public void AddLabel(LabelItem Label, params LabelItem[] chartLabels)
		{
			Label.Parent = this;
			this.extendedLabelItems.Add(Label);
			foreach (LabelItem item in chartLabels)
			{
				this.extendedLabelItems.Add(item);
			}
		}

		// Token: 0x0600E4C8 RID: 58568 RVA: 0x0032D1F4 File Offset: 0x0032B3F4
		public void AddLabel(ChartLabelsCollection chartLabels)
		{
			foreach (LabelItem labelItem in chartLabels)
			{
				labelItem.Parent = this;
				this.extendedLabelItems.Add(labelItem);
			}
		}

		// Token: 0x0600E4C9 RID: 58569 RVA: 0x0032D248 File Offset: 0x0032B448
		public void AddLabel(LabelItem[] chartLabels)
		{
			foreach (LabelItem labelItem in chartLabels)
			{
				labelItem.Parent = this;
				this.extendedLabelItems.Add(labelItem);
			}
		}

		// Token: 0x0600E4CA RID: 58570 RVA: 0x0032D27C File Offset: 0x0032B47C
		public void AddLabel(List<LabelItem> labels)
		{
			foreach (LabelItem labelItem in labels)
			{
				labelItem.Parent = this;
				this.extendedLabelItems.Add(labelItem);
			}
		}

		// Token: 0x0600E4CB RID: 58571 RVA: 0x0032D2D8 File Offset: 0x0032B4D8
		public LabelItem GetLabel(int index)
		{
			return this.extendedLabelItems[index];
		}

		// Token: 0x0600E4CC RID: 58572 RVA: 0x0032D2E6 File Offset: 0x0032B4E6
		public void RemoveAllLabels()
		{
			this.extendedLabelItems.Clear();
		}

		// Token: 0x0600E4CD RID: 58573 RVA: 0x0032D2F4 File Offset: 0x0032B4F4
		public void RemoveLabel(LabelItem Label, params LabelItem[] chartLabels)
		{
			this.extendedLabelItems.Remove(Label);
			foreach (LabelItem item in chartLabels)
			{
				this.extendedLabelItems.Remove(item);
			}
		}

		// Token: 0x0600E4CE RID: 58574 RVA: 0x0032D330 File Offset: 0x0032B530
		public void RemoveLabel(int index, params int[] indexes)
		{
			this.extendedLabelItems.RemoveAt(index);
			foreach (int index2 in indexes)
			{
				this.extendedLabelItems.RemoveAt(index2);
			}
		}

		// Token: 0x0600E4CF RID: 58575 RVA: 0x0032D369 File Offset: 0x0032B569
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.extendedLabelItems).TrackViewState();
		}

		// Token: 0x0600E4D0 RID: 58576 RVA: 0x0032D37C File Offset: 0x0032B57C
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.extendedLabelItems).LoadViewState(array[1]);
			}
		}

		// Token: 0x0600E4D1 RID: 58577 RVA: 0x0032D3AC File Offset: 0x0032B5AC
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.extendedLabelItems).SaveViewState()
			}.ToArray();
		}

		// Token: 0x0600E4D2 RID: 58578 RVA: 0x0032D3E4 File Offset: 0x0032B5E4
		protected override void Dispose(bool disposing)
		{
			if (this.extendedLabelItems != null)
			{
				this.extendedLabelItems = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x040041F9 RID: 16889
		protected ChartLabelsCollection extendedLabelItems;
	}
}
