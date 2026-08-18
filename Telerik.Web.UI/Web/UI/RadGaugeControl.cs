using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Gauge;

namespace Telerik.Web.UI
{
	// Token: 0x02000B60 RID: 2912
	[RequiredCss("Telerik.Web.UI.Skins.HTML5UI.dataviz.css", RenderMode.Lightweight, typeof(RadGaugeControl<PointerBase, ScaleBase>))]
	[ClientScriptResource("Telerik.Web.UI.RadGaugeControl", "Telerik.Web.UI.Gauge.Scripts.RadGaugeControl.js")]
	[RequiredCss("Telerik.Web.UI.Skins.HTML5UI.dataviz.css", RenderMode.Classic, typeof(RadGaugeControl<PointerBase, ScaleBase>))]
	[RequiredScript(typeof(Html5DataVizThemes))]
	[EmbeddedSkin("Gauge")]
	[RequiredScript(typeof(Html5DataVizGauge))]
	public abstract class RadGaugeControl<TOne, TTwo> : RadWebControl where TOne : PointerBase where TTwo : ScaleBase
	{
		// Token: 0x17002406 RID: 9222
		// (get) Token: 0x06006DED RID: 28141
		public abstract TOne Pointer { get; }

		// Token: 0x17002407 RID: 9223
		// (get) Token: 0x06006DEE RID: 28142
		public abstract TTwo Scale { get; }

		// Token: 0x06006DEF RID: 28143 RVA: 0x001985A0 File Offset: 0x001967A0
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddProperty("skin", base.RuntimeSkin);
			if (this.RenderAs != GaugeRenderingEngine.Auto)
			{
				descriptor.AddProperty("renderAs", this.RenderAs.ToString().ToLower());
			}
		}

		// Token: 0x06006DF0 RID: 28144 RVA: 0x001985F0 File Offset: 0x001967F0
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			this.Pointer.LoadViewState(array[1]);
			this.Scale.LoadViewState(array[2]);
			((IStateManager)this.Appearance).LoadViewState(array[3]);
		}

		// Token: 0x06006DF1 RID: 28145 RVA: 0x00198644 File Offset: 0x00196844
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				this.Pointer.SaveViewState(),
				this.Scale.SaveViewState(),
				((IStateManager)this.Appearance).SaveViewState()
			};
		}

		// Token: 0x06006DF2 RID: 28146 RVA: 0x00198698 File Offset: 0x00196898
		protected override void TrackViewState()
		{
			base.TrackViewState();
			this.Pointer.TrackViewState();
			this.Scale.TrackViewState();
			((IStateManager)this.Appearance).TrackViewState();
		}

		// Token: 0x17002408 RID: 9224
		// (get) Token: 0x06006DF3 RID: 28147 RVA: 0x001986CB File Offset: 0x001968CB
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17002409 RID: 9225
		// (get) Token: 0x06006DF4 RID: 28148 RVA: 0x001986CF File Offset: 0x001968CF
		protected override string CssClassFormatString
		{
			get
			{
				return "RadGauge";
			}
		}

		// Token: 0x1700240A RID: 9226
		// (get) Token: 0x06006DF5 RID: 28149 RVA: 0x001986D6 File Offset: 0x001968D6
		// (set) Token: 0x06006DF6 RID: 28150 RVA: 0x001986D9 File Offset: 0x001968D9
		[DefaultValue(false)]
		public override bool EnableEmbeddedSkins
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x1700240B RID: 9227
		// (get) Token: 0x06006DF7 RID: 28151 RVA: 0x001986DB File Offset: 0x001968DB
		// (set) Token: 0x06006DF8 RID: 28152 RVA: 0x001986DE File Offset: 0x001968DE
		[DefaultValue(false)]
		public override bool EnableEmbeddedBaseStylesheet
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x1700240C RID: 9228
		// (get) Token: 0x06006DF9 RID: 28153 RVA: 0x001986E0 File Offset: 0x001968E0
		// (set) Token: 0x06006DFA RID: 28154 RVA: 0x001986E8 File Offset: 0x001968E8
		[ClientControlProperty]
		[ClientPropertyName("height")]
		public override Unit Height
		{
			get
			{
				return base.Height;
			}
			set
			{
				base.Height = value;
			}
		}

		// Token: 0x1700240D RID: 9229
		// (get) Token: 0x06006DFB RID: 28155 RVA: 0x001986F1 File Offset: 0x001968F1
		// (set) Token: 0x06006DFC RID: 28156 RVA: 0x001986F9 File Offset: 0x001968F9
		[ClientControlProperty]
		[ClientPropertyName("width")]
		public override Unit Width
		{
			get
			{
				return base.Width;
			}
			set
			{
				base.Width = value;
			}
		}

		// Token: 0x1700240E RID: 9230
		// (get) Token: 0x06006DFD RID: 28157 RVA: 0x00198702 File Offset: 0x00196902
		// (set) Token: 0x06006DFE RID: 28158 RVA: 0x00198723 File Offset: 0x00196923
		[ClientControlProperty]
		[Description("Gets or sets a bool value indicating whether transition animations should be played.")]
		[DefaultValue(true)]
		[Category("Behavior")]
		public bool Transitions
		{
			get
			{
				return (bool)(this.ViewState["Transitions"] ?? true);
			}
			set
			{
				this.ViewState["Transitions"] = value;
			}
		}

		// Token: 0x1700240F RID: 9231
		// (get) Token: 0x06006DFF RID: 28159 RVA: 0x0019873B File Offset: 0x0019693B
		[Browsable(true)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Behavior")]
		[Description("Defines the appearance settings of the Gauge.")]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Appearance Appearance
		{
			get
			{
				if (this._appearance == null)
				{
					this._appearance = new Appearance();
				}
				return this._appearance;
			}
		}

		// Token: 0x17002410 RID: 9232
		// (get) Token: 0x06006E00 RID: 28160 RVA: 0x00198756 File Offset: 0x00196956
		// (set) Token: 0x06006E01 RID: 28161 RVA: 0x00198777 File Offset: 0x00196977
		[Description("Gets or sets the rendering engine.")]
		[Category("Behavior")]
		[DefaultValue(GaugeRenderingEngine.Auto)]
		public GaugeRenderingEngine RenderAs
		{
			get
			{
				return (GaugeRenderingEngine)(this.ViewState["RenderAs"] ?? GaugeRenderingEngine.Auto);
			}
			set
			{
				this.ViewState["RenderAs"] = value;
			}
		}

		// Token: 0x06006E02 RID: 28162 RVA: 0x00198790 File Offset: 0x00196990
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<string>(descriptor, "height", this.Height.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeProperty<bool>(descriptor, "transitions", this.Transitions, true);
			base.DescribeProperty<string>(descriptor, "width", this.Width.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06006E03 RID: 28163 RVA: 0x001987FF File Offset: 0x001969FF
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x04001DBE RID: 7614
		private Appearance _appearance;
	}
}
