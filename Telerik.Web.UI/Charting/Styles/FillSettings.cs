using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Web.UI;
using System.Web.UI.Design;
using System.Windows.Forms.Design;
using Telerik.Charting.Styles.Skins;

namespace Telerik.Charting.Styles
{
	// Token: 0x02001788 RID: 6024
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class FillSettings : StateManagedObject, ICloneable
	{
		// Token: 0x17004723 RID: 18211
		// (get) Token: 0x0600EADE RID: 60126 RVA: 0x003584D4 File Offset: 0x003566D4
		// (set) Token: 0x0600EADF RID: 60127 RVA: 0x003584F5 File Offset: 0x003566F5
		[SkinnableProperty]
		[Browsable(true)]
		[DefaultValue(typeof(GradientFillStyle), "Horizontal")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[NotifyParentProperty(true)]
		public virtual GradientFillStyle GradientMode
		{
			get
			{
				return (GradientFillStyle)(base.ViewState["GradientMode"] ?? GradientFillStyle.Horizontal);
			}
			set
			{
				base.ViewState["GradientMode"] = value;
			}
		}

		// Token: 0x17004724 RID: 18212
		// (get) Token: 0x0600EAE0 RID: 60128 RVA: 0x0035850D File Offset: 0x0035670D
		// (set) Token: 0x0600EAE1 RID: 60129 RVA: 0x00358532 File Offset: 0x00356732
		[PersistenceMode(PersistenceMode.Attribute)]
		[SkinnableProperty]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue(0f)]
		public float GradientAngle
		{
			get
			{
				return (float)(base.ViewState["GradientAngle"] ?? 0f);
			}
			set
			{
				base.ViewState["GradientAngle"] = value;
			}
		}

		// Token: 0x17004725 RID: 18213
		// (get) Token: 0x0600EAE2 RID: 60130 RVA: 0x0035854A File Offset: 0x0035674A
		[Browsable(true)]
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ColorBlend ComplexGradient
		{
			get
			{
				return this.fillSettingsComplexGradient;
			}
		}

		// Token: 0x17004726 RID: 18214
		// (get) Token: 0x0600EAE3 RID: 60131 RVA: 0x00358552 File Offset: 0x00356752
		// (set) Token: 0x0600EAE4 RID: 60132 RVA: 0x00358573 File Offset: 0x00356773
		[SkinnableProperty]
		[DefaultValue(typeof(HatchStyle), "BackwardDiagonal")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		public HatchStyle HatchStyle
		{
			get
			{
				return (HatchStyle)(base.ViewState["HatchStyle"] ?? HatchStyle.BackwardDiagonal);
			}
			set
			{
				base.ViewState["HatchStyle"] = value;
			}
		}

		// Token: 0x17004727 RID: 18215
		// (get) Token: 0x0600EAE5 RID: 60133 RVA: 0x0035858B File Offset: 0x0035678B
		// (set) Token: 0x0600EAE6 RID: 60134 RVA: 0x003585AC File Offset: 0x003567AC
		[SkinnableProperty]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(ImageDrawMode), "Stretch")]
		public ImageDrawMode ImageDrawMode
		{
			get
			{
				return (ImageDrawMode)(base.ViewState["ImageDrawMode"] ?? ImageDrawMode.Stretch);
			}
			set
			{
				base.ViewState["ImageDrawMode"] = value;
			}
		}

		// Token: 0x17004728 RID: 18216
		// (get) Token: 0x0600EAE7 RID: 60135 RVA: 0x003585C4 File Offset: 0x003567C4
		// (set) Token: 0x0600EAE8 RID: 60136 RVA: 0x003585E4 File Offset: 0x003567E4
		[DefaultValue("")]
		[Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
		[PersistenceMode(PersistenceMode.Attribute)]
		[SkinnableProperty]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
		public string BackgroundImage
		{
			get
			{
				return (string)(base.ViewState["BackgroundImage"] ?? string.Empty);
			}
			set
			{
				base.ViewState["BackgroundImage"] = value;
			}
		}

		// Token: 0x17004729 RID: 18217
		// (get) Token: 0x0600EAE9 RID: 60137 RVA: 0x003585F7 File Offset: 0x003567F7
		// (set) Token: 0x0600EAEA RID: 60138 RVA: 0x00358618 File Offset: 0x00356818
		[SkinnableProperty]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(ImageAlignModes), "Center")]
		[PersistenceMode(PersistenceMode.Attribute)]
		public ImageAlignModes ImageAlign
		{
			get
			{
				return (ImageAlignModes)(base.ViewState["ImageAlign"] ?? ImageAlignModes.Center);
			}
			set
			{
				base.ViewState["ImageAlign"] = value;
			}
		}

		// Token: 0x1700472A RID: 18218
		// (get) Token: 0x0600EAEB RID: 60139 RVA: 0x00358630 File Offset: 0x00356830
		// (set) Token: 0x0600EAEC RID: 60140 RVA: 0x00358651 File Offset: 0x00356851
		[SkinnableProperty]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(ImageTileModes), "Flip")]
		public ImageTileModes ImageFlip
		{
			get
			{
				return (ImageTileModes)(base.ViewState["ImageFlip"] ?? ImageTileModes.Flip);
			}
			set
			{
				base.ViewState["ImageFlip"] = value;
			}
		}

		// Token: 0x1700472B RID: 18219
		internal virtual object this[StyleProperties name]
		{
			get
			{
				switch (name)
				{
				case StyleProperties.GradientMode:
					return this.GradientMode;
				case StyleProperties.GradientAngle:
					return this.GradientAngle;
				case StyleProperties.ComplexGradient:
					return this.fillSettingsComplexGradient;
				case StyleProperties.HatchStyle:
					return this.HatchStyle;
				case StyleProperties.ImageDrawMode:
					return this.ImageDrawMode;
				case StyleProperties.BackGroundImage:
					return this.BackgroundImage;
				case StyleProperties.ImageAlign:
					return this.ImageAlign;
				case StyleProperties.ImageFlip:
					return this.ImageFlip;
				default:
					return null;
				}
			}
		}

		// Token: 0x0600EAEE RID: 60142 RVA: 0x003586FD File Offset: 0x003568FD
		public FillSettings(object containerObject) : this()
		{
			this.fillSettingsContainerObject = containerObject;
		}

		// Token: 0x0600EAEF RID: 60143 RVA: 0x0035870C File Offset: 0x0035690C
		public FillSettings()
		{
			this.fillSettingsComplexGradient = new ColorBlend();
		}

		// Token: 0x0600EAF0 RID: 60144 RVA: 0x0035871F File Offset: 0x0035691F
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public FillSettings(GradientFillStyle lgMode, float lgAngle, ColorBlend blend) : this()
		{
			this.GradientMode = lgMode;
			this.GradientAngle = lgAngle;
			this.fillSettingsComplexGradient = blend;
		}

		// Token: 0x0600EAF1 RID: 60145 RVA: 0x0035873C File Offset: 0x0035693C
		public FillSettings(HatchStyle style) : this()
		{
			this.HatchStyle = style;
		}

		// Token: 0x0600EAF2 RID: 60146 RVA: 0x0035874B File Offset: 0x0035694B
		public FillSettings(ImageDrawMode idMode, string imageURL, ImageAlignModes aligneMode, ImageTileModes flip) : this()
		{
			this.ImageDrawMode = idMode;
			this.BackgroundImage = imageURL;
			this.ImageAlign = aligneMode;
			this.ImageFlip = flip;
		}

		// Token: 0x0600EAF3 RID: 60147 RVA: 0x00358770 File Offset: 0x00356970
		internal virtual void Reset()
		{
			this.fillSettingsComplexGradient = new ColorBlend();
			this.BackgroundImage = string.Empty;
			this.ImageFlip = ImageTileModes.Flip;
			this.ImageAlign = ImageAlignModes.Center;
			this.ImageDrawMode = ImageDrawMode.Stretch;
			this.HatchStyle = HatchStyle.BackwardDiagonal;
			this.GradientAngle = 0f;
			this.GradientMode = GradientFillStyle.Horizontal;
		}

		// Token: 0x0600EAF4 RID: 60148 RVA: 0x003587C4 File Offset: 0x003569C4
		internal Image GetImage(Chart chart)
		{
			if (chart == null)
			{
				return null;
			}
			if (this.BackgroundImage.StartsWith("{") && this.BackgroundImage.EndsWith("}"))
			{
				char[] trimChars = new char[]
				{
					'{',
					'}'
				};
				return Images.GetImageFromResource(this.BackgroundImage.Trim(trimChars), chart.Skin);
			}
			return new Bitmap(chart.MapPath(this.BackgroundImage));
		}

		// Token: 0x0600EAF5 RID: 60149 RVA: 0x00358838 File Offset: 0x00356A38
		public object Clone()
		{
			FillSettings fillSettings = (FillSettings)base.MemberwiseClone();
			fillSettings.ViewState = base.CloneState();
			fillSettings.fillSettingsComplexGradient = new ColorBlend();
			fillSettings.ComplexGradient.LoadFrom(this.ComplexGradient);
			fillSettings.fillSettingsContainerObject = null;
			return fillSettings;
		}

		// Token: 0x0600EAF6 RID: 60150 RVA: 0x00358884 File Offset: 0x00356A84
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			FillSettings fillSettings = obj as FillSettings;
			if (fillSettings != null)
			{
				return fillSettings.GradientAngle == this.GradientAngle && fillSettings.GradientMode.Equals(this.GradientMode) && fillSettings.HatchStyle.Equals(this.HatchStyle) && fillSettings.ImageAlign.Equals(this.ImageAlign) && fillSettings.ImageDrawMode.Equals(this.ImageDrawMode) && fillSettings.ImageFlip.Equals(this.ImageFlip) && fillSettings.fillSettingsComplexGradient.Equals(this.fillSettingsComplexGradient) && fillSettings.BackgroundImage.Equals(this.BackgroundImage);
			}
			return base.Equals(obj);
		}

		// Token: 0x0600EAF7 RID: 60151 RVA: 0x00358978 File Offset: 0x00356B78
		public override int GetHashCode()
		{
			return this.BackgroundImage.GetHashCode() ^ this.fillSettingsComplexGradient.GetHashCode() ^ this.GradientAngle.GetHashCode() ^ this.GradientMode.GetHashCode() ^ this.HatchStyle.GetHashCode() ^ this.ImageAlign.GetHashCode() ^ this.ImageDrawMode.GetHashCode() ^ this.ImageFlip.GetHashCode();
		}

		// Token: 0x0600EAF8 RID: 60152 RVA: 0x00358A00 File Offset: 0x00356C00
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.fillSettingsComplexGradient).TrackViewState();
		}

		// Token: 0x0600EAF9 RID: 60153 RVA: 0x00358A14 File Offset: 0x00356C14
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.fillSettingsComplexGradient).LoadViewState(array[1]);
			}
		}

		// Token: 0x0600EAFA RID: 60154 RVA: 0x00358A44 File Offset: 0x00356C44
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.fillSettingsComplexGradient).SaveViewState()
			}.ToArray();
		}

		// Token: 0x040043EC RID: 17388
		protected ColorBlend fillSettingsComplexGradient;

		// Token: 0x040043ED RID: 17389
		internal object fillSettingsContainerObject;
	}
}
