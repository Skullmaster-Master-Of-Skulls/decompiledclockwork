using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017E8 RID: 6120
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
	public class StyleTextBlock : LayoutStyle
	{
		// Token: 0x17004824 RID: 18468
		// (get) Token: 0x0600EE45 RID: 60997 RVA: 0x00364844 File Offset: 0x00362A44
		// (set) Token: 0x0600EE46 RID: 60998 RVA: 0x00364869 File Offset: 0x00362A69
		[RefreshProperties(RefreshProperties.Repaint)]
		[Description("Max number of visible characters")]
		[NotifyParentProperty(true)]
		public int MaxLength
		{
			get
			{
				return (int)(base.ViewState["MaxLength"] ?? DefaultValues.DEFAULT_MAX_TEXT_LENGTH);
			}
			set
			{
				base.ViewState["MaxLength"] = value;
				if (this.MaxLengthChanged != null)
				{
					this.MaxLengthChanged(this, new EventArgs());
				}
			}
		}

		// Token: 0x140001C4 RID: 452
		// (add) Token: 0x0600EE47 RID: 60999 RVA: 0x0036489C File Offset: 0x00362A9C
		// (remove) Token: 0x0600EE48 RID: 61000 RVA: 0x003648D4 File Offset: 0x00362AD4
		internal event EventHandler MaxLengthChanged;

		// Token: 0x0600EE49 RID: 61001 RVA: 0x00364909 File Offset: 0x00362B09
		protected virtual bool ShouldSerializeMaxLength()
		{
			return this.MaxLength != DefaultValues.DEFAULT_MAX_TEXT_LENGTH;
		}

		// Token: 0x0600EE4A RID: 61002 RVA: 0x0036491B File Offset: 0x00362B1B
		protected virtual void ResetMaxLength()
		{
			this.MaxLength = DefaultValues.DEFAULT_MAX_TEXT_LENGTH;
		}

		// Token: 0x17004825 RID: 18469
		// (get) Token: 0x0600EE4B RID: 61003 RVA: 0x00364928 File Offset: 0x00362B28
		// (set) Token: 0x0600EE4C RID: 61004 RVA: 0x00364930 File Offset: 0x00362B30
		[DefaultValue(typeof(Corners), "Rectangle, Rectangle, Rectangle, Rectangle, 3")]
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[TypeConverter(typeof(CornersConverter))]
		public Corners Corners
		{
			get
			{
				return this.styleTextBlockCorners;
			}
			set
			{
				this.styleTextBlockCorners = value;
			}
		}

		// Token: 0x17004826 RID: 18470
		// (get) Token: 0x0600EE4D RID: 61005 RVA: 0x00364939 File Offset: 0x00362B39
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(true)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[SkinnableProperty]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public virtual FillStyle FillStyle
		{
			get
			{
				return this.styleTextBlockFillStyle;
			}
		}

		// Token: 0x17004827 RID: 18471
		// (get) Token: 0x0600EE4E RID: 61006 RVA: 0x00364941 File Offset: 0x00362B41
		[DefaultValue("Rectangle")]
		[Editor(typeof(FiguresEditor), typeof(UITypeEditor))]
		[Browsable(false)]
		public string Figure
		{
			get
			{
				return "Rectangle";
			}
		}

		// Token: 0x17004828 RID: 18472
		// (get) Token: 0x0600EE4F RID: 61007 RVA: 0x00364948 File Offset: 0x00362B48
		[SkinnableProperty]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual TextProperties TextProperties
		{
			get
			{
				return this.styleTextBlockTextProperties;
			}
		}

		// Token: 0x17004829 RID: 18473
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
					return this.styleTextBlockFillStyle;
				case StyleProperties.Corners:
					return this.styleTextBlockCorners;
				default:
					switch (name)
					{
					case StyleProperties.RotationAngle:
						return this.styleTextBlockRotationAngle;
					case StyleProperties.Position:
						return this.position;
					case StyleProperties.TextProperties:
						return this.styleTextBlockTextProperties;
					case StyleProperties.Overflow:
						return this.styleTextBlockOverflow;
					case StyleProperties.MaxLength:
						return this.MaxLength;
					}
					return base[name];
				}
			}
		}

		// Token: 0x1700482A RID: 18474
		// (get) Token: 0x0600EE51 RID: 61009 RVA: 0x003649F8 File Offset: 0x00362BF8
		// (set) Token: 0x0600EE52 RID: 61010 RVA: 0x00364A19 File Offset: 0x00362C19
		[DefaultValue(AutoTextWrap.Auto)]
		[NotifyParentProperty(true)]
		public virtual AutoTextWrap AutoTextWrap
		{
			get
			{
				return (AutoTextWrap)(base.ViewState["AutoTextWrap"] ?? AutoTextWrap.Auto);
			}
			set
			{
				base.ViewState["AutoTextWrap"] = value;
			}
		}

		// Token: 0x0600EE53 RID: 61011 RVA: 0x00364A31 File Offset: 0x00362C31
		public StyleTextBlock() : this(new FillStyle())
		{
		}

		// Token: 0x0600EE54 RID: 61012 RVA: 0x00364A3E File Offset: 0x00362C3E
		public StyleTextBlock(FillStyle fillStyle) : this(fillStyle, new Position())
		{
		}

		// Token: 0x0600EE55 RID: 61013 RVA: 0x00364A4C File Offset: 0x00362C4C
		public StyleTextBlock(FillStyle fillStyle, Position position) : this(fillStyle, position, null)
		{
		}

		// Token: 0x0600EE56 RID: 61014 RVA: 0x00364A57 File Offset: 0x00362C57
		public StyleTextBlock(TextProperties textProperties) : this(null, textProperties)
		{
		}

		// Token: 0x0600EE57 RID: 61015 RVA: 0x00364A61 File Offset: 0x00362C61
		public StyleTextBlock(FillStyle fillStyle, TextProperties textProperties) : this(fillStyle, null, textProperties)
		{
		}

		// Token: 0x0600EE58 RID: 61016 RVA: 0x00364A6C File Offset: 0x00362C6C
		public StyleTextBlock(FillStyle fillStyle, Position position, TextProperties textProperties) : this(fillStyle, position, textProperties, null)
		{
		}

		// Token: 0x0600EE59 RID: 61017 RVA: 0x00364A78 File Offset: 0x00362C78
		public StyleTextBlock(FillStyle fillStyle, Position position, TextProperties textProperties, Dimensions dimensions) : this(dimensions, fillStyle, position, 0f, textProperties, null, null, null, true)
		{
		}

		// Token: 0x0600EE5A RID: 61018 RVA: 0x00364A9C File Offset: 0x00362C9C
		public StyleTextBlock(Dimensions dimensions, FillStyle fillStyle, Position position, float rotationAngle, TextProperties textProperties, Corners corners, StyleBorder border, ShadowStyle shadowStyle, bool visible) : base(border, visible, shadowStyle, position, dimensions)
		{
			this.styleTextBlockFillStyle = (fillStyle ?? new FillStyle());
			this.styleTextBlockTextProperties = (textProperties ?? new TextProperties());
			this.styleTextBlockCorners = (corners ?? new Corners());
			this.styleTextBlockRotationAngle = rotationAngle;
		}

		// Token: 0x0600EE5B RID: 61019 RVA: 0x00364AF4 File Offset: 0x00362CF4
		internal void SetStringFormat()
		{
			this.styleTextBlockStringFormat = new StringFormat();
			AlignedPositions alignedPosition = this.position.AlignedPosition;
			if (alignedPosition <= AlignedPositions.Center)
			{
				switch (alignedPosition)
				{
				case AlignedPositions.None:
				case AlignedPositions.TopLeft:
				case (AlignedPositions)3:
					break;
				case AlignedPositions.Top:
					goto IL_76;
				case AlignedPositions.TopRight:
					goto IL_83;
				default:
					if (alignedPosition != AlignedPositions.Left)
					{
						if (alignedPosition == AlignedPositions.Center)
						{
							goto IL_76;
						}
					}
					break;
				}
			}
			else if (alignedPosition <= AlignedPositions.BottomLeft)
			{
				if (alignedPosition == AlignedPositions.Right)
				{
					goto IL_83;
				}
				if (alignedPosition != AlignedPositions.BottomLeft)
				{
				}
			}
			else
			{
				if (alignedPosition == AlignedPositions.Bottom)
				{
					goto IL_76;
				}
				if (alignedPosition == AlignedPositions.BottomRight)
				{
					goto IL_83;
				}
			}
			this.styleTextBlockStringFormat.Alignment = StringAlignment.Near;
			return;
			IL_76:
			this.styleTextBlockStringFormat.Alignment = StringAlignment.Center;
			return;
			IL_83:
			this.styleTextBlockStringFormat.Alignment = StringAlignment.Far;
		}

		// Token: 0x1700482B RID: 18475
		// (get) Token: 0x0600EE5C RID: 61020 RVA: 0x00364B90 File Offset: 0x00362D90
		internal StringFormat StringFormat
		{
			get
			{
				return this.styleTextBlockStringFormat;
			}
		}

		// Token: 0x0600EE5D RID: 61021 RVA: 0x00364B98 File Offset: 0x00362D98
		internal override void Reset()
		{
			base.Reset();
			this.MaxLength = DefaultValues.DEFAULT_MAX_TEXT_LENGTH;
			this.styleTextBlockRotationAngle = 0f;
			this.styleTextBlockFillStyle = new FillStyle();
			this.styleTextBlockTextProperties = new TextProperties();
			this.styleTextBlockCorners = new Corners();
			this.styleBorder = new StyleBorder();
		}

		// Token: 0x0600EE5E RID: 61022 RVA: 0x00364BF0 File Offset: 0x00362DF0
		public override object Clone()
		{
			StyleTextBlock styleTextBlock = (StyleTextBlock)base.MemberwiseClone();
			styleTextBlock.ViewState = base.CloneState();
			styleTextBlock.styleTextBlockCorners.CopyFrom(this.styleTextBlockCorners);
			styleTextBlock.dimensions = (Dimensions)this.dimensions.Clone();
			styleTextBlock.position = (Position)this.position.Clone();
			styleTextBlock.styleShadow = (ShadowStyle)this.styleShadow.Clone();
			styleTextBlock.styleTextBlockFillStyle = (FillStyle)this.styleTextBlockFillStyle.Clone();
			styleTextBlock.styleBorder = (StyleBorder)this.styleBorder.Clone();
			styleTextBlock.styleTextBlockTextProperties = (TextProperties)this.styleTextBlockTextProperties.Clone();
			styleTextBlock.styleContainerObject = null;
			return styleTextBlock;
		}

		// Token: 0x0600EE5F RID: 61023 RVA: 0x00364CB4 File Offset: 0x00362EB4
		protected override void Dispose(bool disposing)
		{
			if (this.styleTextBlockFillStyle != null)
			{
				this.styleTextBlockFillStyle.Dispose();
				this.styleTextBlockFillStyle = null;
			}
			if (this.styleTextBlockTextProperties != null)
			{
				this.styleTextBlockTextProperties.Dispose();
				this.styleTextBlockTextProperties = null;
			}
			if (this.styleTextBlockCorners != null)
			{
				this.styleTextBlockCorners.Dispose();
				this.styleTextBlockCorners = null;
			}
			if (this.styleTextBlockStringFormat != null)
			{
				this.styleTextBlockStringFormat.Dispose();
				this.styleTextBlockStringFormat = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600EE60 RID: 61024 RVA: 0x00364D30 File Offset: 0x00362F30
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.styleTextBlockCorners).TrackViewState();
			((IChartingStateManager)this.styleTextBlockFillStyle).TrackViewState();
			((IChartingStateManager)this.styleTextBlockTextProperties).TrackViewState();
		}

		// Token: 0x0600EE61 RID: 61025 RVA: 0x00364D5C File Offset: 0x00362F5C
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.styleTextBlockCorners).LoadViewState(array[1]);
				((IChartingStateManager)this.styleTextBlockFillStyle).LoadViewState(array[2]);
				((IChartingStateManager)this.styleTextBlockTextProperties).LoadViewState(array[3]);
			}
		}

		// Token: 0x0600EE62 RID: 61026 RVA: 0x00364DA8 File Offset: 0x00362FA8
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.styleTextBlockCorners).SaveViewState(),
				((IChartingStateManager)this.styleTextBlockFillStyle).SaveViewState(),
				((IChartingStateManager)this.styleTextBlockTextProperties).SaveViewState()
			}.ToArray();
		}

		// Token: 0x040044D1 RID: 17617
		protected Corners styleTextBlockCorners;

		// Token: 0x040044D2 RID: 17618
		internal FillStyle styleTextBlockFillStyle;

		// Token: 0x040044D3 RID: 17619
		protected float styleTextBlockRotationAngle;

		// Token: 0x040044D4 RID: 17620
		internal TextProperties styleTextBlockTextProperties;

		// Token: 0x040044D5 RID: 17621
		protected Overflow styleTextBlockOverflow;

		// Token: 0x040044D6 RID: 17622
		private StringFormat styleTextBlockStringFormat;
	}
}
