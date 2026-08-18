using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x0200178A RID: 6026
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class FillStyle : StateManagedObject, ICloneable
	{
		// Token: 0x1700472D RID: 18221
		// (get) Token: 0x0600EAFF RID: 60159 RVA: 0x00358ABD File Offset: 0x00356CBD
		// (set) Token: 0x0600EB00 RID: 60160 RVA: 0x00358AE2 File Offset: 0x00356CE2
		[PersistenceMode(PersistenceMode.Attribute)]
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(ColorConverter))]
		[Description("Gets or sets the main color")]
		[DefaultValue(typeof(Color), "")]
		public virtual Color MainColor
		{
			get
			{
				return (Color)(base.ViewState["MainColor"] ?? Color.Empty);
			}
			set
			{
				base.ViewState["MainColor"] = value;
			}
		}

		// Token: 0x1700472E RID: 18222
		// (get) Token: 0x0600EB01 RID: 60161 RVA: 0x00358AFA File Offset: 0x00356CFA
		// (set) Token: 0x0600EB02 RID: 60162 RVA: 0x00358B1F File Offset: 0x00356D1F
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.Attribute)]
		[SkinnableProperty]
		[TypeConverter(typeof(ColorConverter))]
		[Description("Gets or sets the secondary color")]
		[DefaultValue(typeof(Color), "")]
		public virtual Color SecondColor
		{
			get
			{
				return (Color)(base.ViewState["SecondColor"] ?? Color.Empty);
			}
			set
			{
				base.ViewState["SecondColor"] = value;
			}
		}

		// Token: 0x1700472F RID: 18223
		// (get) Token: 0x0600EB03 RID: 60163 RVA: 0x00358B37 File Offset: 0x00356D37
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SkinnableProperty]
		[Browsable(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual FillSettings FillSettings
		{
			get
			{
				return this.fillStyleFillSettings;
			}
		}

		// Token: 0x17004730 RID: 18224
		// (get) Token: 0x0600EB04 RID: 60164 RVA: 0x00358B40 File Offset: 0x00356D40
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public byte MainColorOpacity
		{
			get
			{
				return this.MainColor.A;
			}
		}

		// Token: 0x17004731 RID: 18225
		// (get) Token: 0x0600EB05 RID: 60165 RVA: 0x00358B5C File Offset: 0x00356D5C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public byte SecondColorOpacity
		{
			get
			{
				return this.SecondColor.A;
			}
		}

		// Token: 0x17004732 RID: 18226
		// (get) Token: 0x0600EB06 RID: 60166 RVA: 0x00358B77 File Offset: 0x00356D77
		// (set) Token: 0x0600EB07 RID: 60167 RVA: 0x00358B98 File Offset: 0x00356D98
		[SkinnableProperty]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Description("Specifies whether gamma correction should be used")]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public bool GammaCorrection
		{
			get
			{
				return (bool)(base.ViewState["GammaCorrection"] ?? true);
			}
			set
			{
				base.ViewState["GammaCorrection"] = value;
			}
		}

		// Token: 0x17004733 RID: 18227
		// (get) Token: 0x0600EB08 RID: 60168 RVA: 0x00358BB0 File Offset: 0x00356DB0
		// (set) Token: 0x0600EB09 RID: 60169 RVA: 0x00358BD1 File Offset: 0x00356DD1
		[Description("Specifies which of fill styles (Hatch, Solid, Image, Gradient) should be used")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[DefaultValue(typeof(FillType), "Gradient")]
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		public virtual FillType FillType
		{
			get
			{
				return (FillType)(base.ViewState["FillType"] ?? FillType.Gradient);
			}
			set
			{
				base.ViewState["FillType"] = value;
			}
		}

		// Token: 0x17004734 RID: 18228
		internal virtual object this[StyleProperties name]
		{
			get
			{
				switch (name)
				{
				case StyleProperties.MainColor:
					return this.MainColor;
				case StyleProperties.SecondColor:
					return this.SecondColor;
				case StyleProperties.FillSettings:
					return this.fillStyleFillSettings;
				default:
					switch (name)
					{
					case StyleProperties.GammaCorrection:
						return this.GammaCorrection;
					case StyleProperties.FillType:
						return this.FillType;
					default:
						return null;
					}
					break;
				}
			}
		}

		// Token: 0x0600EB0B RID: 60171 RVA: 0x00358C5A File Offset: 0x00356E5A
		public FillStyle()
		{
			this.fillStyleFillSettings = new FillSettings();
		}

		// Token: 0x0600EB0C RID: 60172 RVA: 0x00358C6D File Offset: 0x00356E6D
		public FillStyle(object container) : this()
		{
			this.fillStyleContainerObject = container;
			this.fillStyleFillSettings = new FillSettings(this.fillStyleContainerObject);
		}

		// Token: 0x0600EB0D RID: 60173 RVA: 0x00358C8D File Offset: 0x00356E8D
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public FillStyle(Color mainColor) : this()
		{
			this.MainColor = mainColor;
		}

		// Token: 0x0600EB0E RID: 60174 RVA: 0x00358C9C File Offset: 0x00356E9C
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public FillStyle(Color mainColor, Color secondColor) : this(mainColor)
		{
			this.SecondColor = secondColor;
		}

		// Token: 0x0600EB0F RID: 60175 RVA: 0x00358CAC File Offset: 0x00356EAC
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public FillStyle(Color mainColor, FillType fillType) : this(mainColor)
		{
			this.FillType = fillType;
		}

		// Token: 0x0600EB10 RID: 60176 RVA: 0x00358CBC File Offset: 0x00356EBC
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public FillStyle(Color mainColor, Color secondColor, FillType fillType) : this(mainColor, secondColor)
		{
			this.FillType = fillType;
		}

		// Token: 0x0600EB11 RID: 60177 RVA: 0x00358CCD File Offset: 0x00356ECD
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public FillStyle(Color mainColor, Color secondColor, FillSettings fillSettings, bool gammaCorrection, FillType fillType) : this()
		{
			this.MainColor = mainColor;
			this.SecondColor = secondColor;
			this.fillStyleFillSettings = fillSettings;
			this.GammaCorrection = gammaCorrection;
			this.FillType = fillType;
		}

		// Token: 0x0600EB12 RID: 60178 RVA: 0x00358CFA File Offset: 0x00356EFA
		internal virtual void Reset()
		{
			this.FillType = FillType.Gradient;
			this.MainColor = Color.Empty;
			this.SecondColor = Color.Empty;
			this.GammaCorrection = true;
			this.fillStyleFillSettings = new FillSettings();
		}

		// Token: 0x0600EB13 RID: 60179 RVA: 0x00358D2C File Offset: 0x00356F2C
		public virtual object Clone()
		{
			FillStyle fillStyle = (FillStyle)base.MemberwiseClone();
			fillStyle.ViewState = base.CloneState();
			fillStyle.fillStyleFillSettings = (FillSettings)this.fillStyleFillSettings.Clone();
			fillStyle.fillStyleContainerObject = null;
			return fillStyle;
		}

		// Token: 0x0600EB14 RID: 60180 RVA: 0x00358D70 File Offset: 0x00356F70
		public override bool Equals(object obj)
		{
			FillStyle fillStyle = obj as FillStyle;
			if (fillStyle != null)
			{
				return this.MainColor.Equals(fillStyle.MainColor) && this.SecondColor.Equals(fillStyle.SecondColor) && this.fillStyleFillSettings.Equals(fillStyle.fillStyleFillSettings) && this.FillType.Equals(fillStyle.FillType) && this.GammaCorrection == fillStyle.GammaCorrection;
			}
			return base.Equals(obj);
		}

		// Token: 0x0600EB15 RID: 60181 RVA: 0x00358E14 File Offset: 0x00357014
		public override int GetHashCode()
		{
			return this.MainColor.GetHashCode() ^ this.SecondColor.GetHashCode() ^ this.fillStyleFillSettings.GetHashCode() ^ this.FillType.GetHashCode() ^ this.GammaCorrection.GetHashCode();
		}

		// Token: 0x0600EB16 RID: 60182 RVA: 0x00358E76 File Offset: 0x00357076
		protected override void Dispose(bool disposing)
		{
			if (this.fillStyleFillSettings != null)
			{
				this.fillStyleFillSettings.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600EB17 RID: 60183 RVA: 0x00358E92 File Offset: 0x00357092
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.fillStyleFillSettings).TrackViewState();
		}

		// Token: 0x0600EB18 RID: 60184 RVA: 0x00358EA8 File Offset: 0x003570A8
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.fillStyleFillSettings).LoadViewState(array[1]);
			}
		}

		// Token: 0x0600EB19 RID: 60185 RVA: 0x00358ED8 File Offset: 0x003570D8
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.fillStyleFillSettings).SaveViewState()
			}.ToArray();
		}

		// Token: 0x040043EE RID: 17390
		internal FillSettings fillStyleFillSettings;

		// Token: 0x040043EF RID: 17391
		internal object fillStyleContainerObject;
	}
}
