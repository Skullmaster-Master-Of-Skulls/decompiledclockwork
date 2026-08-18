using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017E0 RID: 6112
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	[ParseChildren(true)]
	public class StyleMarker : LayoutStyle
	{
		// Token: 0x170047F3 RID: 18419
		// (get) Token: 0x0600EDBE RID: 60862 RVA: 0x00362F93 File Offset: 0x00361193
		// (set) Token: 0x0600EDBF RID: 60863 RVA: 0x00362FB4 File Offset: 0x003611B4
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public override bool Visible
		{
			get
			{
				return (bool)(base.ViewState["Visible"] ?? false);
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x170047F4 RID: 18420
		// (get) Token: 0x0600EDC0 RID: 60864 RVA: 0x00362FBD File Offset: 0x003611BD
		// (set) Token: 0x0600EDC1 RID: 60865 RVA: 0x00362FC5 File Offset: 0x003611C5
		[TypeConverter(typeof(CornersConverter))]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Corners), "Rectangle, Rectangle, Rectangle, Rectangle, 3")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[SkinnableProperty]
		public virtual Corners Corners
		{
			get
			{
				return this.styleMarkerCorners;
			}
			set
			{
				this.styleMarkerCorners = value;
			}
		}

		// Token: 0x170047F5 RID: 18421
		// (get) Token: 0x0600EDC2 RID: 60866 RVA: 0x00362FCE File Offset: 0x003611CE
		[SkinnableProperty]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(true)]
		public virtual FillStyle FillStyle
		{
			get
			{
				return this.styleMarkerFillStyle;
			}
		}

		// Token: 0x170047F6 RID: 18422
		// (get) Token: 0x0600EDC3 RID: 60867 RVA: 0x00362FD6 File Offset: 0x003611D6
		// (set) Token: 0x0600EDC4 RID: 60868 RVA: 0x00362FFB File Offset: 0x003611FB
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[DefaultValue(0f)]
		public virtual float RotationAngle
		{
			get
			{
				return (float)(base.ViewState["RotationAngle"] ?? 0f);
			}
			set
			{
				base.ViewState["RotationAngle"] = value;
			}
		}

		// Token: 0x170047F7 RID: 18423
		// (get) Token: 0x0600EDC5 RID: 60869 RVA: 0x00363013 File Offset: 0x00361213
		// (set) Token: 0x0600EDC6 RID: 60870 RVA: 0x00363033 File Offset: 0x00361233
		[Editor(typeof(FiguresEditor), typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[DefaultValue("Rectangle")]
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

		// Token: 0x170047F8 RID: 18424
		internal override object this[StyleProperties name]
		{
			get
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
					return this.styleMarkerFillStyle;
				case StyleProperties.Corners:
					return this.styleMarkerCorners;
				default:
					switch (name)
					{
					case StyleProperties.RotationAngle:
						return this.RotationAngle;
					case StyleProperties.Position:
						return this.position;
					default:
						return base[name];
					}
					break;
				}
			}
		}

		// Token: 0x0600EDC8 RID: 60872 RVA: 0x003630B9 File Offset: 0x003612B9
		public StyleMarker(object containerObject) : base(containerObject)
		{
		}

		// Token: 0x0600EDC9 RID: 60873 RVA: 0x003630C2 File Offset: 0x003612C2
		public StyleMarker() : this(null)
		{
		}

		// Token: 0x0600EDCA RID: 60874 RVA: 0x003630CB File Offset: 0x003612CB
		public StyleMarker(string figureType) : this(figureType, new DimensionsMarker(), null)
		{
		}

		// Token: 0x0600EDCB RID: 60875 RVA: 0x003630DA File Offset: 0x003612DA
		public StyleMarker(string figureType, int pointSize) : this(figureType, new DimensionsMarker((float)pointSize, (float)pointSize), null)
		{
		}

		// Token: 0x0600EDCC RID: 60876 RVA: 0x003630F0 File Offset: 0x003612F0
		public StyleMarker(string figureType, Dimensions dimensions, FillStyle fillStyle) : this(dimensions, figureType, fillStyle, new PositionLeft(), 0f, null, null, null, true)
		{
		}

		// Token: 0x0600EDCD RID: 60877 RVA: 0x00363114 File Offset: 0x00361314
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public StyleMarker(Dimensions dimensions, string figure, FillStyle fillStyle, Position position, float rotationAngle, Corners corners, StyleBorder border, ShadowStyle shadowStyle, bool visible) : base(border, visible, shadowStyle, position, dimensions)
		{
			this.styleMarkerFillStyle = (fillStyle ?? new FillStyle());
			this.styleMarkerCorners = (corners ?? new Corners());
			if (rotationAngle != 0f)
			{
				this.RotationAngle = rotationAngle;
			}
			if (!string.IsNullOrEmpty(figure))
			{
				this.Figure = figure;
			}
		}

		// Token: 0x0600EDCE RID: 60878 RVA: 0x00363174 File Offset: 0x00361374
		internal override void Reset()
		{
			base.Reset();
			this.Visible = false;
			this.Figure = "Rectangle";
			this.RotationAngle = 0f;
			this.dimensions = new DimensionsMarker();
			this.styleMarkerFillStyle = new FillStyle();
			this.position = new PositionLeft();
			this.styleMarkerCorners = new Corners();
		}

		// Token: 0x0600EDCF RID: 60879 RVA: 0x003631D0 File Offset: 0x003613D0
		public override bool Equals(object obj)
		{
			StyleMarker styleMarker = obj as StyleMarker;
			if (styleMarker != null)
			{
				return styleMarker.Border.Equals(this.styleBorder) && styleMarker.Corners.Equals(this.styleMarkerCorners) && styleMarker.Dimensions.Equals(this.dimensions) && styleMarker.Figure.Equals(this.Figure) && styleMarker.FillStyle.Equals(this.styleMarkerFillStyle) && styleMarker.Position.Equals(this.position) && styleMarker.RotationAngle.Equals(this.RotationAngle) && styleMarker.Visible.Equals(this.Visible);
			}
			return base.Equals(obj);
		}

		// Token: 0x0600EDD0 RID: 60880 RVA: 0x00363294 File Offset: 0x00361494
		public override int GetHashCode()
		{
			return this.styleBorder.GetHashCode() ^ this.styleMarkerCorners.GetHashCode() ^ this.dimensions.GetHashCode() ^ this.Figure.GetHashCode() ^ this.styleMarkerFillStyle.GetHashCode() ^ this.position.GetHashCode() ^ this.RotationAngle.GetHashCode() ^ this.styleShadow.GetHashCode() ^ base.Visible.GetHashCode();
		}

		// Token: 0x0600EDD1 RID: 60881 RVA: 0x00363314 File Offset: 0x00361514
		public override object Clone()
		{
			StyleMarker styleMarker = (StyleMarker)base.MemberwiseClone();
			styleMarker.ViewState = base.CloneState();
			styleMarker.styleMarkerCorners.CopyFrom(this.styleMarkerCorners);
			styleMarker.styleMarkerFillStyle = (FillStyle)this.styleMarkerFillStyle.Clone();
			styleMarker.position = (Position)this.position.Clone();
			styleMarker.dimensions = (Dimensions)this.dimensions.Clone();
			styleMarker.styleBorder = (StyleBorder)this.styleBorder.Clone();
			styleMarker.styleShadow = (ShadowStyle)this.styleShadow.Clone();
			styleMarker.styleContainerObject = null;
			return styleMarker;
		}

		// Token: 0x0600EDD2 RID: 60882 RVA: 0x003633C0 File Offset: 0x003615C0
		protected override void Dispose(bool disposing)
		{
			if (this.styleMarkerFillStyle != null)
			{
				this.styleMarkerFillStyle.Dispose();
				this.styleMarkerFillStyle = null;
			}
			if (this.styleMarkerCorners != null)
			{
				this.styleMarkerCorners.Dispose();
				this.styleMarkerCorners = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600EDD3 RID: 60883 RVA: 0x003633FD File Offset: 0x003615FD
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.styleMarkerCorners).TrackViewState();
			((IChartingStateManager)this.styleMarkerFillStyle).TrackViewState();
			((IChartingStateManager)this.position).TrackViewState();
			((IChartingStateManager)this.dimensions).TrackViewState();
		}

		// Token: 0x0600EDD4 RID: 60884 RVA: 0x00363434 File Offset: 0x00361634
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.styleMarkerCorners).LoadViewState(array[1]);
				((IChartingStateManager)this.styleMarkerFillStyle).LoadViewState(array[2]);
				((IChartingStateManager)this.position).LoadViewState(array[3]);
				((IChartingStateManager)this.dimensions).LoadViewState(array[4]);
			}
		}

		// Token: 0x0600EDD5 RID: 60885 RVA: 0x0036348C File Offset: 0x0036168C
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.styleMarkerCorners).SaveViewState(),
				((IChartingStateManager)this.styleMarkerFillStyle).SaveViewState(),
				((IChartingStateManager)this.position).SaveViewState(),
				((IChartingStateManager)this.dimensions).SaveViewState()
			}.ToArray();
		}

		// Token: 0x040044BD RID: 17597
		internal Corners styleMarkerCorners;

		// Token: 0x040044BE RID: 17598
		internal FillStyle styleMarkerFillStyle;
	}
}
