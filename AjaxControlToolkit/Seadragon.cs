using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Web.UI;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x02000183 RID: 387
	[ToolboxBitmap(typeof(Accessor), "Seadragon.bmp")]
	[ClientScriptResource("Sys.Extended.UI.Seadragon.Viewer", "Seadragon")]
	[ToolboxData("<{0}:Seadragon runat=server></{0}:Seadragon>")]
	public class Seadragon : ScriptControlBase
	{
		// Token: 0x06000ABA RID: 2746 RVA: 0x0001BC54 File Offset: 0x00019E54
		public Seadragon() : base(HtmlTextWriterTag.Unknown)
		{
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x0001BCFE File Offset: 0x00019EFE
		// (set) Token: 0x06000ABC RID: 2748 RVA: 0x0001BD10 File Offset: 0x00019F10
		[DefaultValue(1.5f)]
		[ClientPropertyName("animationTime")]
		[ExtenderControlProperty]
		public float AnimationTime
		{
			get
			{
				return this.GetPropertyValue<float>("AnimationTime", 1.5f);
			}
			set
			{
				this.SetPropertyValue<float>("AnimationTime", value);
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0001BD1E File Offset: 0x00019F1E
		// (set) Token: 0x06000ABE RID: 2750 RVA: 0x0001BD2C File Offset: 0x00019F2C
		[DefaultValue(true)]
		[ClientPropertyName("showNavigationControl")]
		[ExtenderControlProperty]
		public bool ShowNavigationControl
		{
			get
			{
				return this.GetPropertyValue<bool>("ShowNavigationControl", true);
			}
			set
			{
				this.SetPropertyValue<bool>("ShowNavigationControl", value);
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x0001BD3A File Offset: 0x00019F3A
		// (set) Token: 0x06000AC0 RID: 2752 RVA: 0x0001BD4C File Offset: 0x00019F4C
		[ExtenderControlProperty]
		[DefaultValue(0.5f)]
		[ClientPropertyName("blendTime")]
		public float BlendTime
		{
			get
			{
				return this.GetPropertyValue<float>("BlendTime", 0.5f);
			}
			set
			{
				this.SetPropertyValue<float>("BlendTime", value);
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x0001BD5A File Offset: 0x00019F5A
		// (set) Token: 0x06000AC2 RID: 2754 RVA: 0x0001BD68 File Offset: 0x00019F68
		[ClientPropertyName("alwaysBlend")]
		[DefaultValue(false)]
		[ExtenderControlProperty]
		public bool AlwaysBlend
		{
			get
			{
				return this.GetPropertyValue<bool>("AlwaysBlend", false);
			}
			set
			{
				this.SetPropertyValue<bool>("AlwaysBlend", value);
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x0001BD76 File Offset: 0x00019F76
		// (set) Token: 0x06000AC4 RID: 2756 RVA: 0x0001BD84 File Offset: 0x00019F84
		[ClientPropertyName("autoHideControls")]
		[ExtenderControlProperty]
		[DefaultValue(true)]
		public bool AutoHideControls
		{
			get
			{
				return this.GetPropertyValue<bool>("AutoHideControls", true);
			}
			set
			{
				this.SetPropertyValue<bool>("AutoHideControls", value);
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x0001BD92 File Offset: 0x00019F92
		// (set) Token: 0x06000AC6 RID: 2758 RVA: 0x0001BDA0 File Offset: 0x00019FA0
		[ClientPropertyName("immediateRender")]
		[ExtenderControlProperty]
		[DefaultValue(true)]
		public bool ImmediateRender
		{
			get
			{
				return this.GetPropertyValue<bool>("ImmediateRender", true);
			}
			set
			{
				this.SetPropertyValue<bool>("ImmediateRender", value);
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0001BDAE File Offset: 0x00019FAE
		// (set) Token: 0x06000AC8 RID: 2760 RVA: 0x0001BDBC File Offset: 0x00019FBC
		[ExtenderControlProperty]
		[ClientPropertyName("wrapHorizontal")]
		[DefaultValue(false)]
		public bool WrapHorizontal
		{
			get
			{
				return this.GetPropertyValue<bool>("WrapHorizontal", false);
			}
			set
			{
				this.SetPropertyValue<bool>("WrapHorizontal", value);
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x0001BDCA File Offset: 0x00019FCA
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x0001BDD8 File Offset: 0x00019FD8
		[ClientPropertyName("wrapVertical")]
		[ExtenderControlProperty]
		[DefaultValue(false)]
		public bool WrapVertical
		{
			get
			{
				return this.GetPropertyValue<bool>("WrapVertical", false);
			}
			set
			{
				this.SetPropertyValue<bool>("WrapVertical", value);
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0001BDE6 File Offset: 0x00019FE6
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x0001BDF8 File Offset: 0x00019FF8
		[DefaultValue(0.8f)]
		[ClientPropertyName("minZoomDimension")]
		[ExtenderControlProperty]
		public float MinZoomDimension
		{
			get
			{
				return this.GetPropertyValue<float>("MinZoomDimension", 0.8f);
			}
			set
			{
				this.SetPropertyValue<float>("MinZoomDimension", value);
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x0001BE06 File Offset: 0x0001A006
		// (set) Token: 0x06000ACE RID: 2766 RVA: 0x0001BE18 File Offset: 0x0001A018
		[DefaultValue(2f)]
		[ClientPropertyName("maxZoomPixelRatio")]
		[ExtenderControlProperty]
		public float MaxZoomPixelRatio
		{
			get
			{
				return this.GetPropertyValue<float>("MaxZoomPixelRatio", 2f);
			}
			set
			{
				this.SetPropertyValue<float>("MaxZoomPixelRatio", value);
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x0001BE26 File Offset: 0x0001A026
		// (set) Token: 0x06000AD0 RID: 2768 RVA: 0x0001BE38 File Offset: 0x0001A038
		[ExtenderControlProperty]
		[DefaultValue(0.5f)]
		[ClientPropertyName("visibilityRatio")]
		public float VisibilityRatio
		{
			get
			{
				return this.GetPropertyValue<float>("VisibilityRatio", 0.5f);
			}
			set
			{
				this.SetPropertyValue<float>("VisibilityRatio", value);
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x0001BE46 File Offset: 0x0001A046
		// (set) Token: 0x06000AD2 RID: 2770 RVA: 0x0001BE58 File Offset: 0x0001A058
		[ExtenderControlProperty]
		[ClientPropertyName("springStiffness")]
		[DefaultValue(5f)]
		public float SpringStiffness
		{
			get
			{
				return this.GetPropertyValue<float>("SpringStiffness", 5f);
			}
			set
			{
				this.SetPropertyValue<float>("SpringStiffness", value);
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0001BE66 File Offset: 0x0001A066
		// (set) Token: 0x06000AD4 RID: 2772 RVA: 0x0001BE74 File Offset: 0x0001A074
		[ClientPropertyName("imageLoaderLimit")]
		[DefaultValue(2)]
		[ExtenderControlProperty]
		public int ImageLoaderLimit
		{
			get
			{
				return this.GetPropertyValue<int>("SpringStiffness", 2);
			}
			set
			{
				this.SetPropertyValue<int>("SpringStiffness", value);
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x0001BE82 File Offset: 0x0001A082
		// (set) Token: 0x06000AD6 RID: 2774 RVA: 0x0001BE94 File Offset: 0x0001A094
		[DefaultValue(200)]
		[ClientPropertyName("clickTimeThreshold")]
		[ExtenderControlProperty]
		public int ClickTimeThreshold
		{
			get
			{
				return this.GetPropertyValue<int>("ClickTimeThreshold", 200);
			}
			set
			{
				this.SetPropertyValue<int>("ClickTimeThreshold", value);
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x0001BEA2 File Offset: 0x0001A0A2
		// (set) Token: 0x06000AD8 RID: 2776 RVA: 0x0001BEB0 File Offset: 0x0001A0B0
		[ClientPropertyName("clickDistThreshold")]
		[DefaultValue(2)]
		[ExtenderControlProperty]
		public int clickDistThreshold
		{
			get
			{
				return this.GetPropertyValue<int>("clickDistThreshold", 2);
			}
			set
			{
				this.SetPropertyValue<int>("clickDistThreshold", value);
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x0001BEBE File Offset: 0x0001A0BE
		// (set) Token: 0x06000ADA RID: 2778 RVA: 0x0001BED0 File Offset: 0x0001A0D0
		[ExtenderControlProperty]
		[DefaultValue(2f)]
		[ClientPropertyName("zoomPerClick")]
		public float ZoomPerClick
		{
			get
			{
				return this.GetPropertyValue<float>("ZoomPerClick", 2f);
			}
			set
			{
				this.SetPropertyValue<float>("ZoomPerClick", value);
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x0001BEDE File Offset: 0x0001A0DE
		// (set) Token: 0x06000ADC RID: 2780 RVA: 0x0001BEF0 File Offset: 0x0001A0F0
		[ClientPropertyName("zoomPerSecond")]
		[ExtenderControlProperty]
		[DefaultValue(2f)]
		public float ZoomPerSecond
		{
			get
			{
				return this.GetPropertyValue<float>("ZoomPerSecond", 2f);
			}
			set
			{
				this.SetPropertyValue<float>("ZoomPerSecond", value);
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x0001BEFE File Offset: 0x0001A0FE
		// (set) Token: 0x06000ADE RID: 2782 RVA: 0x0001BF0D File Offset: 0x0001A10D
		[ExtenderControlProperty]
		[DefaultValue(100)]
		[ClientPropertyName("maxImageCacheCount")]
		public int MaxImageCacheCount
		{
			get
			{
				return this.GetPropertyValue<int>("maxImageCacheCount", 100);
			}
			set
			{
				this.SetPropertyValue<int>("maxImageCacheCount", value);
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x0001BF1B File Offset: 0x0001A11B
		// (set) Token: 0x06000AE0 RID: 2784 RVA: 0x0001BF2D File Offset: 0x0001A12D
		[ExtenderControlProperty]
		[DefaultValue(0.5f)]
		[ClientPropertyName("minPixelRatio")]
		public float MinPixelRatio
		{
			get
			{
				return this.GetPropertyValue<float>("MinPixelRatio", 0.5f);
			}
			set
			{
				this.SetPropertyValue<float>("MinPixelRatio", value);
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x0001BF3B File Offset: 0x0001A13B
		// (set) Token: 0x06000AE2 RID: 2786 RVA: 0x0001BF4D File Offset: 0x0001A14D
		[DefaultValue("")]
		[ClientPropertyName("open")]
		[ExtenderControlEvent]
		public string OnClientOpen
		{
			get
			{
				return this.GetPropertyValue<string>("OnClientOpen", string.Empty);
			}
			set
			{
				this.SetPropertyValue<string>("OnClientOpen", value);
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x0001BF5B File Offset: 0x0001A15B
		// (set) Token: 0x06000AE4 RID: 2788 RVA: 0x0001BF6D File Offset: 0x0001A16D
		[ClientPropertyName("error")]
		[DefaultValue("")]
		[ExtenderControlEvent]
		public string OnClientError
		{
			get
			{
				return this.GetPropertyValue<string>("OnClientError", string.Empty);
			}
			set
			{
				this.SetPropertyValue<string>("OnClientError", value);
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x0001BF7B File Offset: 0x0001A17B
		// (set) Token: 0x06000AE6 RID: 2790 RVA: 0x0001BF8D File Offset: 0x0001A18D
		[ClientPropertyName("ignore")]
		[DefaultValue("")]
		[ExtenderControlEvent]
		public string OnClientIgnore
		{
			get
			{
				return this.GetPropertyValue<string>("OnClientIgnore", string.Empty);
			}
			set
			{
				this.SetPropertyValue<string>("OnClientIgnore", value);
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x0001BF9B File Offset: 0x0001A19B
		// (set) Token: 0x06000AE8 RID: 2792 RVA: 0x0001BFAD File Offset: 0x0001A1AD
		[ExtenderControlEvent]
		[ClientPropertyName("resize")]
		[DefaultValue("")]
		public string OnClientResize
		{
			get
			{
				return this.GetPropertyValue<string>("OnClientResize", string.Empty);
			}
			set
			{
				this.SetPropertyValue<string>("OnClientResize", value);
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x0001BFBB File Offset: 0x0001A1BB
		// (set) Token: 0x06000AEA RID: 2794 RVA: 0x0001BFCD File Offset: 0x0001A1CD
		[ClientPropertyName("animationstart")]
		[ExtenderControlEvent]
		[DefaultValue("")]
		public string OnClientAnimationStart
		{
			get
			{
				return this.GetPropertyValue<string>("OnClientAnimationStart", string.Empty);
			}
			set
			{
				this.SetPropertyValue<string>("OnClientAnimationStart", value);
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x0001BFDB File Offset: 0x0001A1DB
		// (set) Token: 0x06000AEC RID: 2796 RVA: 0x0001BFED File Offset: 0x0001A1ED
		[ClientPropertyName("animationend")]
		[DefaultValue("")]
		[ExtenderControlEvent]
		public string OnClientAnimationEnd
		{
			get
			{
				return this.GetPropertyValue<string>("OnClientAnimationEnd", string.Empty);
			}
			set
			{
				this.SetPropertyValue<string>("OnClientAnimationEnd", value);
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x0001BFFB File Offset: 0x0001A1FB
		// (set) Token: 0x06000AEE RID: 2798 RVA: 0x0001C00D File Offset: 0x0001A20D
		[ClientPropertyName("animation")]
		[ExtenderControlEvent]
		[DefaultValue("")]
		public string OnClientAnimation
		{
			get
			{
				return this.GetPropertyValue<string>("OnClientAnimation", string.Empty);
			}
			set
			{
				this.SetPropertyValue<string>("OnClientAnimation", value);
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x0001C01B File Offset: 0x0001A21B
		// (set) Token: 0x06000AF0 RID: 2800 RVA: 0x0001C029 File Offset: 0x0001A229
		[DefaultValue(true)]
		[ExtenderControlProperty]
		[ClientPropertyName("mouseNavEnabled")]
		public bool MouseNavEnabled
		{
			get
			{
				return this.GetPropertyValue<bool>("MouseNavEnabled", true);
			}
			set
			{
				this.SetPropertyValue<bool>("MouseNavEnabled", value);
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x0001C037 File Offset: 0x0001A237
		// (set) Token: 0x06000AF2 RID: 2802 RVA: 0x0001C049 File Offset: 0x0001A249
		[Editor(typeof(SeadragonUrlEditor), typeof(UITypeEditor))]
		public string SourceUrl
		{
			get
			{
				return this.GetPropertyValue<string>("SourceUrl", string.Empty);
			}
			set
			{
				this.SetPropertyValue<string>("SourceUrl", value);
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x0001C057 File Offset: 0x0001A257
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public List<SeadragonControl> ControlsCollection
		{
			get
			{
				if (this._controls == null)
				{
					this._controls = new List<SeadragonControl>();
				}
				return this._controls;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x0001C072 File Offset: 0x0001A272
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0001C07A File Offset: 0x0001A27A
		protected override ControlCollection CreateControlCollection()
		{
			return base.CreateControlCollection();
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x0001C082 File Offset: 0x0001A282
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor(typeof(OverlayCollectionEditor), typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		public List<SeadragonOverlay> OverlaysCollection
		{
			get
			{
				if (this._overlays == null)
				{
					this._overlays = new List<SeadragonOverlay>();
				}
				return this._overlays;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x0001C09D File Offset: 0x0001A29D
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0001C44C File Offset: 0x0001A64C
		protected override void CreateChildControls()
		{
			this._controlsDescriptor = new ArrayList();
			this._overlaysDescriptor = new ArrayList();
			foreach (SeadragonControl seadragonControl in this.ControlsCollection)
			{
				this.Controls.Add(seadragonControl);
				this._controlsDescriptor.Add(new
				{
					id = seadragonControl.ClientID,
					anchor = seadragonControl.Anchor
				});
			}
			foreach (SeadragonOverlay seadragonOverlay in this.OverlaysCollection)
			{
				this.Controls.Add(seadragonOverlay);
				if (seadragonOverlay is SeadragonFixedOverlay)
				{
					SeadragonFixedOverlay seadragonFixedOverlay = seadragonOverlay as SeadragonFixedOverlay;
					this._overlaysDescriptor.Add(new
					{
						id = seadragonFixedOverlay.ClientID,
						point = seadragonFixedOverlay.Point,
						placement = seadragonFixedOverlay.Placement
					});
				}
				else
				{
					SeadragonScalableOverlay seadragonScalableOverlay = seadragonOverlay as SeadragonScalableOverlay;
					this._overlaysDescriptor.Add(new
					{
						id = seadragonScalableOverlay.ClientID,
						rect = seadragonScalableOverlay.Rect
					});
				}
			}
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0001C578 File Offset: 0x0001A778
		protected override void DescribeComponent(ScriptComponentDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddProperty("controls", this._controlsDescriptor);
			descriptor.AddProperty("overlays", this._overlaysDescriptor);
			descriptor.AddProperty("xmlPath", base.ResolveClientUrl(this.SourceUrl));
			descriptor.AddProperty("prefixUrl", this.Page.Request.ApplicationPath);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0001C5E0 File Offset: 0x0001A7E0
		protected V GetPropertyValue<V>(string propertyName, V nullValue)
		{
			if (this.ViewState[propertyName] == null)
			{
				return nullValue;
			}
			return (V)((object)this.ViewState[propertyName]);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0001C603 File Offset: 0x0001A803
		protected void SetPropertyValue<V>(string propertyName, V value)
		{
			this.ViewState[propertyName] = value;
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0001C617 File Offset: 0x0001A817
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			ToolkitResourceManager.RegisterImagePaths(this._imageNames, this);
		}

		// Token: 0x04000414 RID: 1044
		private List<SeadragonControl> _controls;

		// Token: 0x04000415 RID: 1045
		private List<SeadragonOverlay> _overlays;

		// Token: 0x04000416 RID: 1046
		private ArrayList _controlsDescriptor;

		// Token: 0x04000417 RID: 1047
		private ArrayList _overlaysDescriptor;

		// Token: 0x04000418 RID: 1048
		private readonly string[] _imageNames = new string[]
		{
			"Seadragon.Fullscreen-Grouphover.png",
			"Seadragon.Fullscreen-Hover.png",
			"Seadragon.Fullscreen-Pressed.png",
			"Seadragon.Fullscreen-Rest.png",
			"Seadragon.Home-Grouphover.png",
			"Seadragon.Home-Hover.png",
			"Seadragon.Home-Pressed.png",
			"Seadragon.Home-Rest.png",
			"Seadragon.ZoomIn-Grouphover.png",
			"Seadragon.ZoomIn-Hover.png",
			"Seadragon.ZoomIn-Pressed.png",
			"Seadragon.ZoomIn-Rest.png",
			"Seadragon.ZoomOut-Grouphover.png",
			"Seadragon.ZoomOut-Hover.png",
			"Seadragon.ZoomOut-Pressed.png",
			"Seadragon.ZoomOut-Rest.png"
		};
	}
}
