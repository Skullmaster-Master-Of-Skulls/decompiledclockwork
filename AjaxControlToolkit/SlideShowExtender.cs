using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x020001A5 RID: 421
	[RequiredScript(typeof(TimerScript))]
	[ClientCssResource("SlideShow")]
	[TargetControlType(typeof(System.Web.UI.WebControls.Image))]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[Designer(typeof(SlideShowExtenderDesigner))]
	[ToolboxBitmap(typeof(Accessor), "SlideShow.bmp")]
	[ClientScriptResource("Sys.Extended.UI.SlideShowBehavior", "SlideShow")]
	public class SlideShowExtender : ExtenderControlBase
	{
		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x0001FD9E File Offset: 0x0001DF9E
		// (set) Token: 0x06000C22 RID: 3106 RVA: 0x0001FDB0 File Offset: 0x0001DFB0
		[RequiredProperty]
		[ExtenderControlProperty]
		[ClientPropertyName("slideShowServiceMethod")]
		[DefaultValue("")]
		public string SlideShowServiceMethod
		{
			get
			{
				return base.GetPropertyValue<string>("SlideShowServiceMethod", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("SlideShowServiceMethod", value);
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x0001FDBE File Offset: 0x0001DFBE
		// (set) Token: 0x06000C24 RID: 3108 RVA: 0x0001FDD0 File Offset: 0x0001DFD0
		[UrlProperty]
		[ClientPropertyName("slideShowServicePath")]
		[ExtenderControlProperty]
		[TypeConverter(typeof(ServicePathConverter))]
		public string SlideShowServicePath
		{
			get
			{
				return base.GetPropertyValue<string>("SlideShowServicePath", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("SlideShowServicePath", value);
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0001FDDE File Offset: 0x0001DFDE
		// (set) Token: 0x06000C26 RID: 3110 RVA: 0x0001FDEC File Offset: 0x0001DFEC
		[ClientPropertyName("contextKey")]
		[DefaultValue(null)]
		[ExtenderControlProperty]
		public string ContextKey
		{
			get
			{
				return base.GetPropertyValue<string>("ContextKey", null);
			}
			set
			{
				base.SetPropertyValue<string>("ContextKey", value);
				this.UseContextKey = true;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x0001FE01 File Offset: 0x0001E001
		// (set) Token: 0x06000C28 RID: 3112 RVA: 0x0001FE0F File Offset: 0x0001E00F
		[DefaultValue(false)]
		[ExtenderControlProperty]
		[ClientPropertyName("useContextKey")]
		public bool UseContextKey
		{
			get
			{
				return base.GetPropertyValue<bool>("UseContextKey", false);
			}
			set
			{
				base.SetPropertyValue<bool>("UseContextKey", value);
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x0001FE1D File Offset: 0x0001E01D
		// (set) Token: 0x06000C2A RID: 3114 RVA: 0x0001FE2F File Offset: 0x0001E02F
		[DefaultValue("")]
		[ClientPropertyName("nextButtonID")]
		[ExtenderControlProperty]
		[IDReferenceProperty(typeof(WebControl))]
		public string NextButtonID
		{
			get
			{
				return base.GetPropertyValue<string>("NextButtonID", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("NextButtonID", value);
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x0001FE3D File Offset: 0x0001E03D
		// (set) Token: 0x06000C2C RID: 3116 RVA: 0x0001FE4F File Offset: 0x0001E04F
		[ExtenderControlProperty]
		[IDReferenceProperty(typeof(WebControl))]
		[ClientPropertyName("playButtonID")]
		[DefaultValue("")]
		public string PlayButtonID
		{
			get
			{
				return base.GetPropertyValue<string>("PlayButtonID", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("PlayButtonID", value);
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x0001FE5D File Offset: 0x0001E05D
		// (set) Token: 0x06000C2E RID: 3118 RVA: 0x0001FE6F File Offset: 0x0001E06F
		[ClientPropertyName("playButtonText")]
		[ExtenderControlProperty]
		[DefaultValue("")]
		public string PlayButtonText
		{
			get
			{
				return base.GetPropertyValue<string>("PlayButtonText", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("PlayButtonText", value);
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0001FE7D File Offset: 0x0001E07D
		// (set) Token: 0x06000C30 RID: 3120 RVA: 0x0001FE8F File Offset: 0x0001E08F
		[DefaultValue("")]
		[ClientPropertyName("stopButtonText")]
		[ExtenderControlProperty]
		public string StopButtonText
		{
			get
			{
				return base.GetPropertyValue<string>("StopButtonText", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("StopButtonText", value);
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0001FE9D File Offset: 0x0001E09D
		// (set) Token: 0x06000C32 RID: 3122 RVA: 0x0001FEAF File Offset: 0x0001E0AF
		[ClientPropertyName("playInterval")]
		[ExtenderControlProperty]
		[DefaultValue(3000)]
		public int PlayInterval
		{
			get
			{
				return base.GetPropertyValue<int>("PlayInterval", 3000);
			}
			set
			{
				base.SetPropertyValue<int>("PlayInterval", value);
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0001FEBD File Offset: 0x0001E0BD
		// (set) Token: 0x06000C34 RID: 3124 RVA: 0x0001FECF File Offset: 0x0001E0CF
		[IDReferenceProperty(typeof(WebControl))]
		[DefaultValue("")]
		[ClientPropertyName("imageTitleLabelID")]
		[ExtenderControlProperty]
		public string ImageTitleLabelID
		{
			get
			{
				return base.GetPropertyValue<string>("ImageTitleLabelID", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("ImageTitleLabelID", value);
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x0001FEDD File Offset: 0x0001E0DD
		// (set) Token: 0x06000C36 RID: 3126 RVA: 0x0001FEEF File Offset: 0x0001E0EF
		[ExtenderControlProperty]
		[IDReferenceProperty(typeof(WebControl))]
		[ClientPropertyName("imageDescriptionLabelID")]
		[DefaultValue("")]
		public string ImageDescriptionLabelID
		{
			get
			{
				return base.GetPropertyValue<string>("ImageDescriptionLabelID", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("ImageDescriptionLabelID", value);
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0001FEFD File Offset: 0x0001E0FD
		// (set) Token: 0x06000C38 RID: 3128 RVA: 0x0001FF0F File Offset: 0x0001E10F
		[ClientPropertyName("previousButtonID")]
		[ExtenderControlProperty]
		[DefaultValue("")]
		[IDReferenceProperty(typeof(WebControl))]
		public string PreviousButtonID
		{
			get
			{
				return base.GetPropertyValue<string>("PreviousButtonID", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("PreviousButtonID", value);
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0001FF1D File Offset: 0x0001E11D
		// (set) Token: 0x06000C3A RID: 3130 RVA: 0x0001FF2B File Offset: 0x0001E12B
		[DefaultValue(false)]
		[ExtenderControlProperty]
		[ClientPropertyName("loop")]
		public bool Loop
		{
			get
			{
				return base.GetPropertyValue<bool>("Loop", false);
			}
			set
			{
				base.SetPropertyValue<bool>("Loop", value);
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x0001FF39 File Offset: 0x0001E139
		// (set) Token: 0x06000C3C RID: 3132 RVA: 0x0001FF47 File Offset: 0x0001E147
		[DefaultValue(false)]
		[ExtenderControlProperty]
		[ClientPropertyName("autoPlay")]
		public bool AutoPlay
		{
			get
			{
				return base.GetPropertyValue<bool>("AutoPlay", false);
			}
			set
			{
				base.SetPropertyValue<bool>("AutoPlay", value);
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x0001FF55 File Offset: 0x0001E155
		// (set) Token: 0x06000C3E RID: 3134 RVA: 0x0001FF63 File Offset: 0x0001E163
		[ClientPropertyName("slideShowAnimationType")]
		[ExtenderControlProperty]
		[DefaultValue(SlideShowAnimationType.None)]
		public SlideShowAnimationType SlideShowAnimationType
		{
			get
			{
				return base.GetPropertyValue<SlideShowAnimationType>("SlideShowAnimationType", SlideShowAnimationType.None);
			}
			set
			{
				base.SetPropertyValue<SlideShowAnimationType>("SlideShowAnimationType", value);
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x0001FF71 File Offset: 0x0001E171
		// (set) Token: 0x06000C40 RID: 3136 RVA: 0x0001FF83 File Offset: 0x0001E183
		[Browsable(false)]
		[ClientPropertyName("imageWidth")]
		[DefaultValue(400)]
		[ExtenderControlProperty]
		public int ImageWidth
		{
			get
			{
				return base.GetPropertyValue<int>("ImageWidth", 400);
			}
			set
			{
				base.SetPropertyValue<int>("ImageWidth", value);
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x0001FF91 File Offset: 0x0001E191
		// (set) Token: 0x06000C42 RID: 3138 RVA: 0x0001FFA3 File Offset: 0x0001E1A3
		[Browsable(false)]
		[DefaultValue(300)]
		[ClientPropertyName("imageHeight")]
		[ExtenderControlProperty]
		public int ImageHeight
		{
			get
			{
				return base.GetPropertyValue<int>("ImageHeight", 300);
			}
			set
			{
				base.SetPropertyValue<int>("ImageHeight", value);
			}
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0001FFB4 File Offset: 0x0001E1B4
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			System.Web.UI.WebControls.Image image = (System.Web.UI.WebControls.Image)base.TargetControl;
			this.ImageHeight = (int)image.Height.Value;
			this.ImageWidth = (int)image.Width.Value;
		}
	}
}
