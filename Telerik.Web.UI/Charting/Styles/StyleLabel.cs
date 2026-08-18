using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017D4 RID: 6100
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class StyleLabel : LayoutStyle
	{
		// Token: 0x170047D8 RID: 18392
		// (get) Token: 0x0600ED53 RID: 60755 RVA: 0x00361F8F File Offset: 0x0036018F
		// (set) Token: 0x0600ED54 RID: 60756 RVA: 0x00361F97 File Offset: 0x00360197
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[DefaultValue(typeof(Corners), "Rectangle, Rectangle, Rectangle, Rectangle, 3")]
		[TypeConverter(typeof(CornersConverter))]
		public Corners Corners
		{
			get
			{
				return this.styleLabelCorners;
			}
			set
			{
				this.styleLabelCorners = value;
			}
		}

		// Token: 0x170047D9 RID: 18393
		// (get) Token: 0x0600ED55 RID: 60757 RVA: 0x00361FA0 File Offset: 0x003601A0
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual FillStyle FillStyle
		{
			get
			{
				return this.styleLabelFillStyle;
			}
		}

		// Token: 0x170047DA RID: 18394
		// (get) Token: 0x0600ED56 RID: 60758 RVA: 0x00361FA8 File Offset: 0x003601A8
		// (set) Token: 0x0600ED57 RID: 60759 RVA: 0x00361FC8 File Offset: 0x003601C8
		[Editor(typeof(FiguresEditor), typeof(UITypeEditor))]
		[SkinnableProperty]
		[DefaultValue("Rectangle")]
		[NotifyParentProperty(true)]
		public virtual string Figure
		{
			get
			{
				return (string)(base.ViewState["Figure"] ?? "Rectangle");
			}
			set
			{
				base.ViewState["Figure"] = value;
			}
		}

		// Token: 0x170047DB RID: 18395
		// (get) Token: 0x0600ED58 RID: 60760 RVA: 0x00361FDB File Offset: 0x003601DB
		// (set) Token: 0x0600ED59 RID: 60761 RVA: 0x00362000 File Offset: 0x00360200
		[SkinnableProperty]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue(0f)]
		public virtual float RotationAngle
		{
			get
			{
				return (float)(base.ViewState["RotationAngle"] ?? 0f);
			}
			set
			{
				if (base.ViewState["RotationAngle"] != null && value == 0f)
				{
					base.ViewState.Remove("RotationAngle");
					return;
				}
				if (value != 0f)
				{
					base.ViewState["RotationAngle"] = value;
				}
			}
		}

		// Token: 0x170047DC RID: 18396
		// (get) Token: 0x0600ED5A RID: 60762 RVA: 0x00362056 File Offset: 0x00360256
		// (set) Token: 0x0600ED5B RID: 60763 RVA: 0x00362078 File Offset: 0x00360278
		[SkinnableProperty]
		[DefaultValue(typeof(LabelItemsCompositionTypes), "None")]
		[NotifyParentProperty(true)]
		public virtual LabelItemsCompositionTypes CompositionType
		{
			get
			{
				return (LabelItemsCompositionTypes)(base.ViewState["CompositionType"] ?? LabelItemsCompositionTypes.None);
			}
			set
			{
				if (base.ViewState["CompositionType"] != null && value == LabelItemsCompositionTypes.None)
				{
					base.ViewState.Remove("CompositionType");
					return;
				}
				if (value != LabelItemsCompositionTypes.None)
				{
					base.ViewState["CompositionType"] = value;
				}
			}
		}

		// Token: 0x170047DD RID: 18397
		// (get) Token: 0x0600ED5C RID: 60764 RVA: 0x003620C6 File Offset: 0x003602C6
		// (set) Token: 0x0600ED5D RID: 60765 RVA: 0x003620E7 File Offset: 0x003602E7
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public override bool Visible
		{
			get
			{
				return (bool)(base.ViewState["Visible"] ?? true);
			}
			set
			{
				base.Visible = value;
				if (this.styleContainerObject == null)
				{
					this.styleLabelIsSet = true;
				}
			}
		}

		// Token: 0x170047DE RID: 18398
		// (get) Token: 0x0600ED5E RID: 60766 RVA: 0x003620FF File Offset: 0x003602FF
		[Browsable(false)]
		internal bool IsSet
		{
			get
			{
				return this.styleLabelIsSet;
			}
		}

		// Token: 0x170047DF RID: 18399
		internal override object this[StyleProperties name]
		{
			get
			{
				if (name <= StyleProperties.Corners)
				{
					if (name == StyleProperties.Dimensions)
					{
						return this.dimensions;
					}
					switch (name)
					{
					case StyleProperties.Figure:
						return this.Figure;
					case StyleProperties.FillStyle:
						return this.styleLabelFillStyle;
					case StyleProperties.Corners:
						return this.styleLabelCorners;
					}
				}
				else
				{
					switch (name)
					{
					case StyleProperties.RotationAngle:
						return this.RotationAngle;
					case StyleProperties.Position:
						return this.position;
					default:
						if (name == StyleProperties.CompositionType)
						{
							return this.CompositionType;
						}
						break;
					}
				}
				return base[name];
			}
		}

		// Token: 0x0600ED60 RID: 60768 RVA: 0x0036218F File Offset: 0x0036038F
		public StyleLabel(object containerObject) : base(containerObject)
		{
			this.styleContainerObject = containerObject;
			this.styleLabelFillStyle = new FillStyle(containerObject);
			this.styleLabelCorners = new Corners(containerObject);
		}

		// Token: 0x0600ED61 RID: 60769 RVA: 0x003621B7 File Offset: 0x003603B7
		public StyleLabel() : this(new FillStyle())
		{
		}

		// Token: 0x0600ED62 RID: 60770 RVA: 0x003621C4 File Offset: 0x003603C4
		public StyleLabel(FillStyle fillStyle) : this(fillStyle, null)
		{
		}

		// Token: 0x0600ED63 RID: 60771 RVA: 0x003621CE File Offset: 0x003603CE
		public StyleLabel(Position position) : this(null, position)
		{
		}

		// Token: 0x0600ED64 RID: 60772 RVA: 0x003621D8 File Offset: 0x003603D8
		public StyleLabel(FillStyle fillStyle, Position position) : this(fillStyle, position, null)
		{
		}

		// Token: 0x0600ED65 RID: 60773 RVA: 0x003621E3 File Offset: 0x003603E3
		public StyleLabel(FillStyle fillStyle, Position position, Dimensions dimensions) : this(fillStyle, null, position, dimensions)
		{
		}

		// Token: 0x0600ED66 RID: 60774 RVA: 0x003621EF File Offset: 0x003603EF
		public StyleLabel(FillStyle fillStyle, Corners corners, Position position, Dimensions dimensions) : base(position, dimensions)
		{
			this.styleLabelFillStyle = (fillStyle ?? new FillStyle());
			this.styleLabelCorners = (corners ?? new Corners());
		}

		// Token: 0x0600ED67 RID: 60775 RVA: 0x0036221C File Offset: 0x0036041C
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public StyleLabel(LabelItemsCompositionTypes compositionType, Dimensions dimensions, string figure, FillStyle fillStyle, Position position, float rotationAngle, Corners corners, StyleBorder border, ShadowStyle shadowStyle, bool visible) : base(border, visible, shadowStyle, position, dimensions)
		{
			if (figure != null)
			{
				this.Figure = figure;
			}
			this.CompositionType = compositionType;
			this.RotationAngle = rotationAngle;
			this.styleLabelFillStyle = (fillStyle ?? new FillStyle());
			this.styleLabelCorners = (corners ?? new Corners());
		}

		// Token: 0x0600ED68 RID: 60776 RVA: 0x00362274 File Offset: 0x00360474
		internal override void Reset()
		{
			base.Reset();
			this.dimensions = new Dimensions();
			this.styleLabelFillStyle = new FillStyle();
			this.position = new Position();
			this.styleLabelCorners = new Corners();
			this.CompositionType = LabelItemsCompositionTypes.RowImageText;
			this.Figure = "Rectangle";
			this.RotationAngle = 0f;
			this.Visible = true;
		}

		// Token: 0x0600ED69 RID: 60777 RVA: 0x003622D7 File Offset: 0x003604D7
		internal void SaveDimensions()
		{
			this.Dimensions.Copy = this.Dimensions;
		}

		// Token: 0x0600ED6A RID: 60778 RVA: 0x003622EA File Offset: 0x003604EA
		internal void RestoreDimensions()
		{
			this.Dimensions.SetDimensions(this.Dimensions.Copy);
		}

		// Token: 0x0600ED6B RID: 60779 RVA: 0x00362302 File Offset: 0x00360502
		internal void RestoreInitialValues()
		{
			this.Position.AlignedPosition = this.Position.Copy.AlignedPosition;
			this.Dimensions.Margins.CopyFrom(this.Dimensions.Copy.Margins);
		}

		// Token: 0x0600ED6C RID: 60780 RVA: 0x0036233F File Offset: 0x0036053F
		internal virtual void SetAutoLayoutDefaults()
		{
			this.SaveDimensions();
			this.Position.Copy = this.Position;
		}

		// Token: 0x0600ED6D RID: 60781 RVA: 0x00362358 File Offset: 0x00360558
		public override object Clone()
		{
			StyleLabel styleLabel = (StyleLabel)base.MemberwiseClone();
			styleLabel.ViewState = base.CloneState();
			styleLabel.styleBorder = (StyleBorder)this.styleBorder.Clone();
			styleLabel.styleShadow = (ShadowStyle)this.styleShadow.Clone();
			styleLabel.styleLabelCorners.CopyFrom(this.styleLabelCorners);
			styleLabel.dimensions = (Dimensions)this.dimensions.Clone();
			styleLabel.styleLabelFillStyle = (FillStyle)this.styleLabelFillStyle.Clone();
			styleLabel.position = (Position)this.position.Clone();
			styleLabel.styleContainerObject = null;
			return styleLabel;
		}

		// Token: 0x0600ED6E RID: 60782 RVA: 0x00362404 File Offset: 0x00360604
		protected override void Dispose(bool disposing)
		{
			if (this.styleLabelFillStyle != null)
			{
				this.styleLabelFillStyle.Dispose();
				this.styleLabelFillStyle = null;
			}
			if (this.styleLabelCorners != null)
			{
				this.styleLabelCorners.Dispose();
				this.styleLabelCorners = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600ED6F RID: 60783 RVA: 0x00362441 File Offset: 0x00360641
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.styleLabelCorners).TrackViewState();
			((IChartingStateManager)this.dimensions).TrackViewState();
			((IChartingStateManager)this.styleLabelFillStyle).TrackViewState();
			((IChartingStateManager)this.position).TrackViewState();
		}

		// Token: 0x0600ED70 RID: 60784 RVA: 0x00362478 File Offset: 0x00360678
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.styleLabelCorners).LoadViewState(array[1]);
				((IChartingStateManager)this.dimensions).LoadViewState(array[2]);
				((IChartingStateManager)this.styleLabelFillStyle).LoadViewState(array[3]);
				((IChartingStateManager)this.position).LoadViewState(array[4]);
			}
		}

		// Token: 0x0600ED71 RID: 60785 RVA: 0x003624D0 File Offset: 0x003606D0
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.styleLabelCorners).SaveViewState(),
				((IChartingStateManager)this.dimensions).SaveViewState(),
				((IChartingStateManager)this.styleLabelFillStyle).SaveViewState(),
				((IChartingStateManager)this.position).SaveViewState()
			}.ToArray();
		}

		// Token: 0x040044AD RID: 17581
		protected Corners styleLabelCorners;

		// Token: 0x040044AE RID: 17582
		internal FillStyle styleLabelFillStyle;

		// Token: 0x040044AF RID: 17583
		private bool styleLabelIsSet;
	}
}
