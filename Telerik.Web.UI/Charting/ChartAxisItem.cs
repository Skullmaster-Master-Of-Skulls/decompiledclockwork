using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x0200172F RID: 5935
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
	public class ChartAxisItem : ChartLabel
	{
		// Token: 0x1700464A RID: 17994
		// (get) Token: 0x0600E6EB RID: 59115 RVA: 0x0033A3E4 File Offset: 0x003385E4
		// (set) Token: 0x0600E6EC RID: 59116 RVA: 0x0033A3F1 File Offset: 0x003385F1
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public override bool Visible
		{
			get
			{
				return this.Appearance.Visible;
			}
			set
			{
				this.Appearance.Visible = value;
			}
		}

		// Token: 0x1700464B RID: 17995
		// (get) Token: 0x0600E6ED RID: 59117 RVA: 0x0033A3FF File Offset: 0x003385FF
		// (set) Token: 0x0600E6EE RID: 59118 RVA: 0x0033A425 File Offset: 0x00338625
		[Description("Specifies the numeric value of the axis item.")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(decimal), "0")]
		[Category("General")]
		[Bindable(true)]
		public decimal Value
		{
			get
			{
				return (decimal)(base.ViewState["Value"] ?? 0m);
			}
			set
			{
				base.ViewState["Value"] = value;
			}
		}

		// Token: 0x0600E6EF RID: 59119 RVA: 0x0033A43D File Offset: 0x0033863D
		public ChartAxisItem(IContainer container) : this(null, Color.Empty, true, container)
		{
		}

		// Token: 0x0600E6F0 RID: 59120 RVA: 0x0033A44D File Offset: 0x0033864D
		public ChartAxisItem() : this(null, Color.Empty, true, null)
		{
		}

		// Token: 0x0600E6F1 RID: 59121 RVA: 0x0033A45D File Offset: 0x0033865D
		public ChartAxisItem(string labelText) : this(labelText, Color.Empty)
		{
		}

		// Token: 0x0600E6F2 RID: 59122 RVA: 0x0033A46B File Offset: 0x0033866B
		public ChartAxisItem(string labelText, Color color) : this(labelText, color, true)
		{
		}

		// Token: 0x0600E6F3 RID: 59123 RVA: 0x0033A476 File Offset: 0x00338676
		public ChartAxisItem(string label, Color color, bool visible) : this(label, color, visible, null)
		{
		}

		// Token: 0x0600E6F4 RID: 59124 RVA: 0x0033A484 File Offset: 0x00338684
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public ChartAxisItem(string labelText, Color color, bool visible, IContainer container) : base(null, container, new StyleLabel(), new TextBlockAxisItem(), string.Empty)
		{
			RenderedObject chartBaseLabelTextBlock = this.chartBaseLabelTextBlock;
			this.chartBaseLabelTextBlock.Parent = this;
			chartBaseLabelTextBlock.Container = this;
			if (color != Color.Empty)
			{
				this.chartBaseLabelTextBlock.Appearance.TextProperties.Color = color;
			}
			if (!string.IsNullOrEmpty(labelText))
			{
				this.chartBaseLabelTextBlock.Text = labelText;
			}
			this.Appearance.Visible = visible;
		}

		// Token: 0x0600E6F5 RID: 59125 RVA: 0x0033A506 File Offset: 0x00338706
		internal RectangleF GetBound()
		{
			return Style.GetRealBounds(this.Appearance.Dimensions, new float?(((ChartAxisItemsCollection)base.Parent).GetItemRotationAngle(this)));
		}

		// Token: 0x0600E6F6 RID: 59126 RVA: 0x0033A530 File Offset: 0x00338730
		internal float GetHeight()
		{
			return this.GetBound().Height;
		}

		// Token: 0x0600E6F7 RID: 59127 RVA: 0x0033A54C File Offset: 0x0033874C
		internal float GetHeight(bool withTopMargin, bool withBottomMargin)
		{
			float num = this.GetHeight();
			if (withTopMargin)
			{
				num += this.Appearance.Dimensions.Margins.Top.PixelValue;
			}
			if (withBottomMargin)
			{
				num += this.Appearance.Dimensions.Margins.Bottom.PixelValue;
			}
			return num;
		}

		// Token: 0x0600E6F8 RID: 59128 RVA: 0x0033A5A4 File Offset: 0x003387A4
		internal float GetWidth()
		{
			return this.GetBound().Width;
		}

		// Token: 0x0600E6F9 RID: 59129 RVA: 0x0033A5C0 File Offset: 0x003387C0
		internal float GetWidth(bool withLeftMargin, bool withRigthMargin)
		{
			float num = this.GetWidth();
			if (withLeftMargin)
			{
				num += this.Appearance.Dimensions.Margins.Left.PixelValue;
			}
			if (withRigthMargin)
			{
				num += this.Appearance.Dimensions.Margins.Right.PixelValue;
			}
			return num;
		}

		// Token: 0x0600E6FA RID: 59130 RVA: 0x0033A618 File Offset: 0x00338818
		internal void CorrectTextBlockAlignedPosition(bool reason)
		{
			if (reason)
			{
				if (this.TextBlock.Appearance.Position.IsLeft)
				{
					this.TextBlock.Appearance.Position.AlignedPosition = AlignedPositions.Left;
					return;
				}
				if (this.TextBlock.Appearance.Position.IsRight || this.TextBlock.Appearance.Position.IsNone)
				{
					this.TextBlock.Appearance.Position.AlignedPosition = AlignedPositions.Right;
				}
			}
		}

		// Token: 0x0600E6FB RID: 59131 RVA: 0x0033A69C File Offset: 0x0033889C
		internal SizeF Measure(RenderEngine renderEngine, ChartAxisItem emptyItem)
		{
			ChartAxis chartAxis = null;
			ChartAxisItemsCollection chartAxisItemsCollection = this.chartBaseLabelParent as ChartAxisItemsCollection;
			if (chartAxisItemsCollection != null)
			{
				chartAxis = chartAxisItemsCollection.Parent;
			}
			if (chartAxis != null)
			{
				if (this.TextBlock.Appearance.MaxLength == emptyItem.TextBlock.Appearance.MaxLength)
				{
					this.TextBlock.Appearance.MaxLength = chartAxis.Appearance.TextAppearance.MaxLength;
				}
				if (this.TextBlock.Appearance.TextProperties.Font.Equals(emptyItem.TextBlock.Appearance.TextProperties.Font))
				{
					this.TextBlock.Appearance.TextProperties.Font = chartAxis.Appearance.TextAppearance.TextProperties.Font;
				}
				if (this.TextBlock.Appearance.Dimensions.EqualsWithoutMarginsPaddings(emptyItem.TextBlock.Appearance.Dimensions))
				{
					this.TextBlock.Appearance.dimensions.SetDimensions(chartAxis.Appearance.TextAppearance.Dimensions.Width, chartAxis.Appearance.TextAppearance.Dimensions.Height);
				}
				if (this.TextBlock.Appearance.Dimensions.Margins.Equals(emptyItem.TextBlock.Appearance.Dimensions.Margins))
				{
					this.TextBlock.Appearance.Dimensions.Margins.CopyFrom(chartAxis.Appearance.TextAppearance.Dimensions.Margins);
				}
				if (this.TextBlock.Appearance.Dimensions.Paddings.Equals(emptyItem.TextBlock.Appearance.Dimensions.Paddings))
				{
					this.TextBlock.Appearance.Dimensions.Paddings.CopyFrom(chartAxis.Appearance.TextAppearance.Dimensions.Paddings);
				}
				if (this.Appearance.Dimensions.EqualsWithoutMarginsPaddings(emptyItem.Appearance.Dimensions))
				{
					this.Appearance.dimensions.SetDimensions(chartAxis.Appearance.LabelAppearance.Dimensions.Width, chartAxis.Appearance.LabelAppearance.Dimensions.Height);
				}
				if (this.Appearance.Dimensions.Margins.Equals(emptyItem.Appearance.Dimensions.Margins))
				{
					this.Appearance.Dimensions.Margins.CopyFrom(chartAxis.Appearance.LabelAppearance.Dimensions.Margins);
				}
				if (this.Appearance.Dimensions.Paddings.Equals(emptyItem.Appearance.Dimensions.Paddings))
				{
					this.Appearance.Dimensions.Paddings.CopyFrom(chartAxis.Appearance.LabelAppearance.Dimensions.Paddings);
				}
			}
			return base.Measure(renderEngine);
		}

		// Token: 0x04004264 RID: 16996
		internal ChartAxisItemType chartAxisItemType;
	}
}
