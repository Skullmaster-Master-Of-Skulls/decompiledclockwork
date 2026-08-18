using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x020016FC RID: 5884
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[DefaultProperty("TextBlock")]
	public class ChartBaseLabel : LayoutElement, IContainer, IActiveRegion, ICloneable
	{
		// Token: 0x170045BE RID: 17854
		// (get) Token: 0x0600E487 RID: 58503 RVA: 0x0032B707 File Offset: 0x00329907
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Elements")]
		[Description("TextBlock")]
		[Browsable(true)]
		public virtual TextBlock TextBlock
		{
			get
			{
				return this.chartBaseLabelTextBlock;
			}
		}

		// Token: 0x170045BF RID: 17855
		// (get) Token: 0x0600E488 RID: 58504 RVA: 0x0032B70F File Offset: 0x0032990F
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Elements")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Marker")]
		[Browsable(true)]
		public ChartMarker Marker
		{
			get
			{
				return this.chartBaseLabelMarker;
			}
		}

		// Token: 0x170045C0 RID: 17856
		// (get) Token: 0x0600E489 RID: 58505 RVA: 0x0032B717 File Offset: 0x00329917
		// (set) Token: 0x0600E48A RID: 58506 RVA: 0x0032B71F File Offset: 0x0032991F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[NotifyParentProperty(true)]
		[Browsable(false)]
		public object Parent
		{
			get
			{
				return this.chartBaseLabelParent;
			}
			set
			{
				this.chartBaseLabelParent = value;
			}
		}

		// Token: 0x170045C1 RID: 17857
		// (get) Token: 0x0600E48B RID: 58507 RVA: 0x0032B728 File Offset: 0x00329928
		// (set) Token: 0x0600E48C RID: 58508 RVA: 0x0032B749 File Offset: 0x00329949
		[PersistenceMode(PersistenceMode.Attribute)]
		[NotifyParentProperty(true)]
		[DefaultValue(PlacementDirection.Vertical)]
		internal virtual PlacementDirection PlacementDirection
		{
			get
			{
				return (PlacementDirection)(base.ViewState["PlacementDirection"] ?? PlacementDirection.Vertical);
			}
			set
			{
				base.ViewState["PlacementDirection"] = value;
			}
		}

		// Token: 0x170045C2 RID: 17858
		// (get) Token: 0x0600E48D RID: 58509 RVA: 0x0032B761 File Offset: 0x00329961
		// (set) Token: 0x0600E48E RID: 58510 RVA: 0x0032B769 File Offset: 0x00329969
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		public ActiveRegion ActiveRegion
		{
			get
			{
				return this.chartBaseLabelActiveRegion;
			}
			set
			{
				this.chartBaseLabelActiveRegion = value;
			}
		}

		// Token: 0x170045C3 RID: 17859
		// (get) Token: 0x0600E48F RID: 58511 RVA: 0x0032B772 File Offset: 0x00329972
		// (set) Token: 0x0600E490 RID: 58512 RVA: 0x0032B77F File Offset: 0x0032997F
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(true)]
		[PersistenceMode(PersistenceMode.Attribute)]
		public virtual bool Visible
		{
			get
			{
				return this.appearance.Visible;
			}
			set
			{
				this.appearance.Visible = value;
			}
		}

		// Token: 0x0600E491 RID: 58513 RVA: 0x0032B78D File Offset: 0x0032998D
		public ChartBaseLabel() : this(null, null, null)
		{
		}

		// Token: 0x0600E492 RID: 58514 RVA: 0x0032B798 File Offset: 0x00329998
		public ChartBaseLabel(IContainer container) : this(null, container, null)
		{
		}

		// Token: 0x0600E493 RID: 58515 RVA: 0x0032B7A3 File Offset: 0x003299A3
		public ChartBaseLabel(object parent, IContainer container) : this(parent, container, null)
		{
		}

		// Token: 0x0600E494 RID: 58516 RVA: 0x0032B7AE File Offset: 0x003299AE
		public ChartBaseLabel(object parent) : this(parent, null, null)
		{
		}

		// Token: 0x0600E495 RID: 58517 RVA: 0x0032B7B9 File Offset: 0x003299B9
		public ChartBaseLabel(string text) : this(null, null, null)
		{
			this.chartBaseLabelTextBlock.Text = text;
		}

		// Token: 0x0600E496 RID: 58518 RVA: 0x0032B7D0 File Offset: 0x003299D0
		public ChartBaseLabel(TextBlock textBlock) : this(null, null, textBlock)
		{
		}

		// Token: 0x0600E497 RID: 58519 RVA: 0x0032B7DB File Offset: 0x003299DB
		public ChartBaseLabel(object parent, IContainer container, TextBlock textBlock) : this(parent, container, textBlock, null)
		{
		}

		// Token: 0x0600E498 RID: 58520 RVA: 0x0032B7E8 File Offset: 0x003299E8
		public ChartBaseLabel(object parent, IContainer container, TextBlock textBlock, LayoutStyle appearance) : base(appearance, container)
		{
			this.chartBaseLabelTextBlock = (textBlock ?? new TextBlock());
			this.chartBaseLabelTextBlock.Parent = this;
			this.chartBaseLabelTextBlock.Container = this;
			this.chartBaseLabelParent = parent;
			this.chartBaseLabelActiveRegion = new ActiveRegion(this);
			this.chartBaseLabelOrderList = new List<IOrdering>();
			this.chartBaseLabelMarker = new ChartMarker(this, this);
			this.Add(this.chartBaseLabelMarker);
			this.Add(this.chartBaseLabelTextBlock);
		}

		// Token: 0x0600E499 RID: 58521 RVA: 0x0032B869 File Offset: 0x00329A69
		internal virtual bool IsVisible()
		{
			return this.chartBaseLabelMarker.Visible || (this.chartBaseLabelTextBlock.Visible && !string.IsNullOrEmpty(this.chartBaseLabelTextBlock.Text));
		}

		// Token: 0x0600E49A RID: 58522 RVA: 0x0032B89C File Offset: 0x00329A9C
		internal virtual SizeF Measure(RenderEngine renderEngine)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			bool isVisible = this.TextBlock.IsVisible;
			bool visible = this.Marker.Visible;
			bool auto = this.Marker.Appearance.Position.Auto;
			bool auto2 = this.TextBlock.Appearance.Position.Auto;
			float num5 = this.TextBlock.Appearance.Dimensions.Margins.Top.PixelValue;
			float num6 = this.Marker.Appearance.Dimensions.Margins.Top.PixelValue;
			float num7 = this.TextBlock.Appearance.Dimensions.Margins.Left.PixelValue;
			float num8 = this.Marker.Appearance.Dimensions.Margins.Left.PixelValue;
			float pixelValue = this.TextBlock.Appearance.Dimensions.Margins.Bottom.PixelValue;
			float pixelValue2 = this.Marker.Appearance.Dimensions.Margins.Bottom.PixelValue;
			if (visible)
			{
				num3 = this.Marker.Appearance.Dimensions.Width.PixelValue;
				num4 = this.Marker.Appearance.Dimensions.Height.PixelValue;
			}
			else
			{
				num6 = 0f;
				num8 = 0f;
			}
			if (isVisible)
			{
				SizeF sizeF = this.TextBlock.Measure(renderEngine);
				num = sizeF.Width;
				num2 = sizeF.Height;
				if (this.TextBlock.Appearance.Dimensions.AutoSize)
				{
					this.TextBlock.Appearance.Dimensions.SetDimensions(num, num2);
				}
			}
			else
			{
				num5 = 0f;
				num7 = 0f;
			}
			Dimensions dimensions = (Dimensions)Style.GetStyleProperty(this, StyleProperties.Dimensions);
			float num9 = dimensions.Paddings.Left.PixelValue;
			float num10 = dimensions.Paddings.Top.PixelValue;
			this.TextBlock.Appearance.Position.requireCalculation = false;
			this.Marker.Appearance.Position.requireCalculation = false;
			switch ((LabelItemsCompositionTypes)Style.GetStyleProperty(this, StyleProperties.CompositionType))
			{
			case LabelItemsCompositionTypes.ColumnImageText:
				if (auto && visible)
				{
					this.Marker.Appearance.Position.X = num8;
					this.Marker.Appearance.Position.Y = num6 + num10;
				}
				num10 += num4 + num6 + pixelValue2;
				if (auto2 && isVisible)
				{
					this.TextBlock.Appearance.Position.X = num7;
					this.TextBlock.Appearance.Position.Y = num5 + num10;
				}
				num9 = Math.Max(num, num3);
				num10 += num2 + num5 + pixelValue;
				goto IL_757;
			case LabelItemsCompositionTypes.ColumnTextImage:
				if (auto2 && isVisible)
				{
					this.TextBlock.Appearance.Position.X = num7;
					this.TextBlock.Appearance.Position.Y = num5 + num10;
				}
				num10 += num2 + num5 + pixelValue;
				if (auto && visible)
				{
					this.Marker.Appearance.Position.X = num8;
					this.Marker.Appearance.Position.Y = num6 + num10;
				}
				num9 = Math.Max(num, num3);
				num10 += num4 + num6 + pixelValue2;
				goto IL_757;
			case LabelItemsCompositionTypes.RowImageText:
				if (auto && visible)
				{
					this.Marker.Appearance.Position.X = num8 + num9;
					this.Marker.Appearance.Position.Y = num6 + num10;
				}
				if (visible)
				{
					num9 += num3 + num8 + this.Marker.Appearance.Dimensions.Margins.Right.PixelValue;
					num4 += num6 + this.Marker.Appearance.Dimensions.Margins.Bottom.PixelValue;
				}
				if (auto2 && isVisible)
				{
					this.TextBlock.Appearance.Position.X = num7 + num9;
					this.TextBlock.Appearance.Position.Y = num5 + num10;
				}
				if (isVisible)
				{
					num2 += num5 + this.TextBlock.Appearance.Dimensions.Margins.Bottom.PixelValue;
				}
				num10 += Math.Max(num2, num4);
				num9 += num + num7 + this.TextBlock.Appearance.Dimensions.Margins.Right.PixelValue;
				goto IL_757;
			case LabelItemsCompositionTypes.RowTextImage:
				if (auto2 && isVisible)
				{
					this.TextBlock.Appearance.Position.X = num7 + num9;
					this.TextBlock.Appearance.Position.Y = num5 + num10;
				}
				num9 += num + num7 + this.TextBlock.Appearance.Dimensions.Margins.Right.PixelValue;
				if (auto && visible)
				{
					this.Marker.Appearance.Position.X = num8 + num9;
					this.Marker.Appearance.Position.Y = num6 + num10;
				}
				if (visible)
				{
					num9 += num3 + num8 + this.Marker.Appearance.Dimensions.Margins.Right.PixelValue;
					num4 += num6 + this.Marker.Appearance.Dimensions.Margins.Bottom.PixelValue;
				}
				if (isVisible)
				{
					num2 += num5 + this.TextBlock.Appearance.Dimensions.Margins.Bottom.PixelValue;
				}
				num10 += Math.Max(num2, num4);
				goto IL_757;
			}
			if (auto && visible)
			{
				this.Marker.Appearance.Position.X = num8 + num9;
				this.Marker.Appearance.Position.Y = num6 + num10;
			}
			if (visible)
			{
				num9 += num3 + num8 + this.Marker.Appearance.Dimensions.Margins.Right.PixelValue;
				num4 += num6 + this.Marker.Appearance.Dimensions.Margins.Bottom.PixelValue;
			}
			if (auto2 && isVisible)
			{
				this.TextBlock.Appearance.Position.X = num7 + num9;
				this.TextBlock.Appearance.Position.Y = num5 + num10;
			}
			if (isVisible)
			{
				num2 += num5 + this.TextBlock.Appearance.Dimensions.Margins.Bottom.PixelValue;
				this.Marker.Appearance.Position.Y += Math.Max((num2 - num4) / 2f, 0f);
			}
			num10 += Math.Max(num2, num4);
			num9 += num + num7 + this.TextBlock.Appearance.Dimensions.Margins.Right.PixelValue;
			this.TextBlock.Appearance.Position.requireCalculation = true;
			this.Marker.Appearance.Position.requireCalculation = true;
			IL_757:
			num9 += dimensions.Paddings.Right.PixelValue;
			num10 += dimensions.Paddings.Bottom.PixelValue;
			return new SizeF(num9, num10);
		}

		// Token: 0x0600E49B RID: 58523 RVA: 0x0032C030 File Offset: 0x0032A230
		internal override void CalculatePosition(RenderEngine renderEngine)
		{
			if (this is ExtendedLabel && !this.Visible)
			{
				return;
			}
			if (this.appearance.Dimensions.AutoSize)
			{
				if (this.TextBlock.textBlockWrapContext == null)
				{
					this.TextBlock.textBlockWrapContext = new WrapContext(renderEngine.chart.Appearance.Dimensions.Width.PixelValue - renderEngine.chart.Appearance.Dimensions.Paddings.Left.PixelValue - renderEngine.chart.Appearance.Dimensions.Paddings.Right.PixelValue - this.appearance.Dimensions.Margins.Left.PixelValue - this.appearance.Dimensions.Margins.Right.PixelValue - renderEngine.chart.Appearance.Border.Width * 2f, renderEngine.chart.Appearance.Dimensions.Height.PixelValue - renderEngine.chart.Appearance.Dimensions.Paddings.Top.PixelValue - renderEngine.chart.Appearance.Dimensions.Paddings.Bottom.PixelValue - this.appearance.Dimensions.Margins.Top.PixelValue - this.appearance.Dimensions.Margins.Bottom.PixelValue - renderEngine.chart.Appearance.Border.Width * 2f, WrapType.FixedWidth);
				}
				SizeF sizeF = this.Measure(renderEngine);
				this.appearance.Dimensions.SetDimensions(sizeF.Width, sizeF.Height);
			}
			else
			{
				this.Measure(renderEngine);
			}
			this.chartBaseLabelTextBlock.CalculatePosition(renderEngine);
			this.chartBaseLabelMarker.CalculatePosition(renderEngine);
			base.CalculatePosition(renderEngine);
		}

		// Token: 0x170045C4 RID: 17860
		// (get) Token: 0x0600E49C RID: 58524 RVA: 0x0032C22B File Offset: 0x0032A42B
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public List<IOrdering> OrderList
		{
			get
			{
				return this.chartBaseLabelOrderList;
			}
		}

		// Token: 0x170045C5 RID: 17861
		// (get) Token: 0x0600E49D RID: 58525 RVA: 0x0032C234 File Offset: 0x0032A434
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int NextPosition
		{
			get
			{
				IOrdering item = null;
				foreach (IOrdering ordering in this.chartBaseLabelOrderList)
				{
					item = ordering;
				}
				return this.chartBaseLabelOrderList.IndexOf(item) + 1;
			}
		}

		// Token: 0x0600E49E RID: 58526 RVA: 0x0032C294 File Offset: 0x0032A494
		public int GetOrder(IOrdering element)
		{
			return this.chartBaseLabelOrderList.IndexOf(element);
		}

		// Token: 0x0600E49F RID: 58527 RVA: 0x0032C2A2 File Offset: 0x0032A4A2
		public void Add(IOrdering element)
		{
			element.Container = this;
			this.chartBaseLabelOrderList.Add(element);
		}

		// Token: 0x0600E4A0 RID: 58528 RVA: 0x0032C2B7 File Offset: 0x0032A4B7
		public void Insert(int order, IOrdering element)
		{
			element.Container = this;
			this.chartBaseLabelOrderList.Insert(order, element);
		}

		// Token: 0x0600E4A1 RID: 58529 RVA: 0x0032C2CD File Offset: 0x0032A4CD
		public void Remove(IOrdering element)
		{
			this.chartBaseLabelOrderList.Remove(element);
		}

		// Token: 0x0600E4A2 RID: 58530 RVA: 0x0032C2DC File Offset: 0x0032A4DC
		public void RemoveAt(int index)
		{
			this.chartBaseLabelOrderList.RemoveAt(index);
		}

		// Token: 0x0600E4A3 RID: 58531 RVA: 0x0032C2EC File Offset: 0x0032A4EC
		public void ReIndex()
		{
			List<IOrdering> list = new List<IOrdering>();
			int num = 0;
			foreach (IOrdering ordering in this.chartBaseLabelOrderList)
			{
				if (ordering != null)
				{
					list.Insert(num++, ordering);
				}
			}
			this.chartBaseLabelOrderList = list;
		}

		// Token: 0x0600E4A4 RID: 58532 RVA: 0x0032C358 File Offset: 0x0032A558
		protected override void Dispose(bool disposing)
		{
			if (this.chartBaseLabelActiveRegion != null)
			{
				this.chartBaseLabelActiveRegion.Dispose();
				this.chartBaseLabelActiveRegion = null;
			}
			if (this.chartBaseLabelMarker != null)
			{
				this.chartBaseLabelMarker.Dispose();
				this.chartBaseLabelMarker = null;
			}
			if (this.chartBaseLabelOrderList != null)
			{
				this.chartBaseLabelOrderList = null;
			}
			if (this.chartBaseLabelTextBlock != null)
			{
				this.chartBaseLabelTextBlock.Dispose();
				this.chartBaseLabelTextBlock = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600E4A5 RID: 58533 RVA: 0x0032C3C9 File Offset: 0x0032A5C9
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.chartBaseLabelTextBlock).TrackViewState();
			((IChartingStateManager)this.chartBaseLabelMarker).TrackViewState();
			((IChartingStateManager)this.chartBaseLabelActiveRegion).TrackViewState();
		}

		// Token: 0x0600E4A6 RID: 58534 RVA: 0x0032C3F4 File Offset: 0x0032A5F4
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.chartBaseLabelTextBlock).LoadViewState(array[1]);
				((IChartingStateManager)this.chartBaseLabelMarker).LoadViewState(array[2]);
				((IChartingStateManager)this.chartBaseLabelActiveRegion).LoadViewState(array[3]);
			}
		}

		// Token: 0x0600E4A7 RID: 58535 RVA: 0x0032C440 File Offset: 0x0032A640
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.chartBaseLabelTextBlock).SaveViewState(),
				((IChartingStateManager)this.chartBaseLabelMarker).SaveViewState(),
				((IChartingStateManager)this.chartBaseLabelActiveRegion).SaveViewState()
			}.ToArray();
		}

		// Token: 0x0600E4A8 RID: 58536 RVA: 0x0032C49C File Offset: 0x0032A69C
		public virtual object Clone()
		{
			ChartBaseLabel chartBaseLabel = (ChartBaseLabel)base.MemberwiseClone();
			chartBaseLabel.ViewState = base.CloneState();
			chartBaseLabel.chartBaseLabelTextBlock = new TextBlock();
			chartBaseLabel.TextBlock.CopyFrom(this.TextBlock);
			chartBaseLabel.chartBaseLabelMarker = new ChartMarker();
			chartBaseLabel.Marker.CopyFrom(this.Marker);
			chartBaseLabel.Parent = this.Parent;
			return chartBaseLabel;
		}

		// Token: 0x040041F4 RID: 16884
		internal TextBlock chartBaseLabelTextBlock;

		// Token: 0x040041F5 RID: 16885
		internal ChartMarker chartBaseLabelMarker;

		// Token: 0x040041F6 RID: 16886
		protected object chartBaseLabelParent;

		// Token: 0x040041F7 RID: 16887
		protected List<IOrdering> chartBaseLabelOrderList;

		// Token: 0x040041F8 RID: 16888
		protected ActiveRegion chartBaseLabelActiveRegion;
	}
}
