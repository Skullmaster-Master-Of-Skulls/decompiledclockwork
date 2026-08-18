using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x0200018D RID: 397
	[ClientCssResource("Slider")]
	[Designer(typeof(SliderDesigner))]
	[ToolboxBitmap(typeof(Accessor), "Slider.bmp")]
	[ClientScriptResource("Sys.Extended.UI.SliderBehavior", "Slider")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[RequiredScript(typeof(DragDropScripts))]
	[RequiredScript(typeof(AnimationScripts))]
	[RequiredScript(typeof(TimerScript))]
	[TargetControlType(typeof(TextBox))]
	public class SliderExtender : ExtenderControlBase
	{
		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x0001C789 File Offset: 0x0001A989
		// (set) Token: 0x06000B1D RID: 2845 RVA: 0x0001C79F File Offset: 0x0001A99F
		[DefaultValue(0)]
		[ExtenderControlProperty]
		[ClientPropertyName("minimum")]
		public double Minimum
		{
			get
			{
				return base.GetPropertyValue<double>("Minimum", 0.0);
			}
			set
			{
				base.SetPropertyValue<double>("Minimum", value);
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x0001C7AD File Offset: 0x0001A9AD
		// (set) Token: 0x06000B1F RID: 2847 RVA: 0x0001C7C3 File Offset: 0x0001A9C3
		[ExtenderControlProperty]
		[ClientPropertyName("maximum")]
		[DefaultValue(100)]
		public double Maximum
		{
			get
			{
				return base.GetPropertyValue<double>("Maximum", 100.0);
			}
			set
			{
				base.SetPropertyValue<double>("Maximum", value);
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x0001C7D1 File Offset: 0x0001A9D1
		// (set) Token: 0x06000B21 RID: 2849 RVA: 0x0001C7E3 File Offset: 0x0001A9E3
		[ClientPropertyName("railCssClass")]
		[ExtenderControlProperty]
		[DefaultValue("")]
		public string RailCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("RailCssClass", "");
			}
			set
			{
				base.SetPropertyValue<string>("RailCssClass", value);
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x0001C7F1 File Offset: 0x0001A9F1
		// (set) Token: 0x06000B23 RID: 2851 RVA: 0x0001C803 File Offset: 0x0001AA03
		[DefaultValue("")]
		[ExtenderControlProperty]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[ClientPropertyName("handleImageUrl")]
		public string HandleImageUrl
		{
			get
			{
				return base.GetPropertyValue<string>("HandleImageUrl", "");
			}
			set
			{
				base.SetPropertyValue<string>("HandleImageUrl", value);
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x0001C811 File Offset: 0x0001AA11
		// (set) Token: 0x06000B25 RID: 2853 RVA: 0x0001C823 File Offset: 0x0001AA23
		[ExtenderControlProperty]
		[DefaultValue("")]
		[ClientPropertyName("handleCssClass")]
		public string HandleCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("HandleCssClass", "");
			}
			set
			{
				base.SetPropertyValue<string>("HandleCssClass", value);
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x0001C831 File Offset: 0x0001AA31
		// (set) Token: 0x06000B27 RID: 2855 RVA: 0x0001C83F File Offset: 0x0001AA3F
		[DefaultValue(false)]
		[ExtenderControlProperty]
		[ClientPropertyName("enableHandleAnimation")]
		public bool EnableHandleAnimation
		{
			get
			{
				return base.GetPropertyValue<bool>("EnableHandleAnimation", false);
			}
			set
			{
				base.SetPropertyValue<bool>("EnableHandleAnimation", value);
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x0001C84D File Offset: 0x0001AA4D
		// (set) Token: 0x06000B29 RID: 2857 RVA: 0x0001C85B File Offset: 0x0001AA5B
		[ClientPropertyName("steps")]
		[ExtenderControlProperty]
		[DefaultValue(0)]
		public int Steps
		{
			get
			{
				return base.GetPropertyValue<int>("Steps", 0);
			}
			set
			{
				base.SetPropertyValue<int>("Steps", value);
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0001C869 File Offset: 0x0001AA69
		// (set) Token: 0x06000B2B RID: 2859 RVA: 0x0001C877 File Offset: 0x0001AA77
		[ExtenderControlProperty]
		[ClientPropertyName("orientation")]
		[DefaultValue(SliderOrientation.Horizontal)]
		public SliderOrientation Orientation
		{
			get
			{
				return base.GetPropertyValue<SliderOrientation>("Orientation", SliderOrientation.Horizontal);
			}
			set
			{
				base.SetPropertyValue<SliderOrientation>("Orientation", value);
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x0001C885 File Offset: 0x0001AA85
		// (set) Token: 0x06000B2D RID: 2861 RVA: 0x0001C893 File Offset: 0x0001AA93
		[ClientPropertyName("decimals")]
		[ExtenderControlProperty]
		[DefaultValue(0)]
		public int Decimals
		{
			get
			{
				return base.GetPropertyValue<int>("Decimals", 0);
			}
			set
			{
				base.SetPropertyValue<int>("Decimals", value);
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x0001C8A1 File Offset: 0x0001AAA1
		// (set) Token: 0x06000B2F RID: 2863 RVA: 0x0001C8B3 File Offset: 0x0001AAB3
		[ExtenderControlProperty]
		[ClientPropertyName("boundControlID")]
		[IDReferenceProperty(typeof(WebControl))]
		[DefaultValue("")]
		public string BoundControlID
		{
			get
			{
				return base.GetPropertyValue<string>("BoundControlID", "");
			}
			set
			{
				base.SetPropertyValue<string>("BoundControlID", value);
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x0001C8C1 File Offset: 0x0001AAC1
		// (set) Token: 0x06000B31 RID: 2865 RVA: 0x0001C8D3 File Offset: 0x0001AAD3
		[DefaultValue(150)]
		[ExtenderControlProperty]
		[ClientPropertyName("length")]
		public int Length
		{
			get
			{
				return base.GetPropertyValue<int>("Length", 150);
			}
			set
			{
				base.SetPropertyValue<int>("Length", value);
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x0001C8E1 File Offset: 0x0001AAE1
		// (set) Token: 0x06000B33 RID: 2867 RVA: 0x0001C8EF File Offset: 0x0001AAEF
		[ExtenderControlProperty]
		[ClientPropertyName("raiseChangeOnlyOnMouseUp")]
		[DefaultValue(true)]
		public bool RaiseChangeOnlyOnMouseUp
		{
			get
			{
				return base.GetPropertyValue<bool>("RaiseChangeOnlyOnMouseUp", true);
			}
			set
			{
				base.SetPropertyValue<bool>("RaiseChangeOnlyOnMouseUp", value);
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x0001C8FD File Offset: 0x0001AAFD
		// (set) Token: 0x06000B35 RID: 2869 RVA: 0x0001C90F File Offset: 0x0001AB0F
		[ClientPropertyName("tooltipText")]
		[ExtenderControlProperty]
		[DefaultValue("")]
		public string TooltipText
		{
			get
			{
				return base.GetPropertyValue<string>("TooltipText", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("TooltipText", value);
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x0001C91D File Offset: 0x0001AB1D
		// (set) Token: 0x06000B37 RID: 2871 RVA: 0x0001C92B File Offset: 0x0001AB2B
		[Description("Determines if the slider will respond to arrow keys when it has focus.")]
		[ExtenderControlProperty]
		[DefaultValue(true)]
		[ClientPropertyName("enableKeyboard")]
		public bool EnableKeyboard
		{
			get
			{
				return base.GetPropertyValue<bool>("EnableKeyboard", true);
			}
			set
			{
				base.SetPropertyValue<bool>("EnableKeyboard", value);
			}
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0001C939 File Offset: 0x0001AB39
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			ToolkitResourceManager.RegisterImagePaths(this._imageNames, this);
		}

		// Token: 0x0400042C RID: 1068
		private readonly string[] _imageNames = new string[]
		{
			"Slider.Handle-Horizontal.gif",
			"Slider.Handle-Vertical.gif",
			"Slider.Rail-Horizontal.gif",
			"Slider.Rail-Vertical.gif"
		};
	}
}
