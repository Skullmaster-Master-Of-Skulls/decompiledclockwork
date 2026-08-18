using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x02000199 RID: 409
	[ToolboxBitmap(typeof(Accessor), "ToggleButton.bmp")]
	[TargetControlType(typeof(ICheckBoxControl))]
	[Designer(typeof(ToggleButtonExtenderDesigner))]
	[ClientScriptResource("Sys.Extended.UI.ToggleButtonBehavior", "ToggleButton")]
	public class ToggleButtonExtender : ExtenderControlBase
	{
		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x0001EA70 File Offset: 0x0001CC70
		// (set) Token: 0x06000BBF RID: 3007 RVA: 0x0001EA7E File Offset: 0x0001CC7E
		[RequiredProperty]
		[DefaultValue(0)]
		[ClientPropertyName("imageWidth")]
		[ExtenderControlProperty]
		public int ImageWidth
		{
			get
			{
				return base.GetPropertyValue<int>("ImageWidth", 0);
			}
			set
			{
				base.SetPropertyValue<int>("ImageWidth", value);
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0001EA8C File Offset: 0x0001CC8C
		// (set) Token: 0x06000BC1 RID: 3009 RVA: 0x0001EA9A File Offset: 0x0001CC9A
		[DefaultValue(0)]
		[ExtenderControlProperty]
		[RequiredProperty]
		[ClientPropertyName("imageHeight")]
		public int ImageHeight
		{
			get
			{
				return base.GetPropertyValue<int>("ImageHeight", 0);
			}
			set
			{
				base.SetPropertyValue<int>("ImageHeight", value);
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x0001EAA8 File Offset: 0x0001CCA8
		// (set) Token: 0x06000BC3 RID: 3011 RVA: 0x0001EABA File Offset: 0x0001CCBA
		[ExtenderControlProperty]
		[ClientPropertyName("uncheckedImageUrl")]
		[RequiredProperty]
		[DefaultValue("")]
		[UrlProperty]
		public string UncheckedImageUrl
		{
			get
			{
				return base.GetPropertyValue<string>("UncheckedImageUrl", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("UncheckedImageUrl", value);
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x0001EAC8 File Offset: 0x0001CCC8
		// (set) Token: 0x06000BC5 RID: 3013 RVA: 0x0001EADA File Offset: 0x0001CCDA
		[ClientPropertyName("checkedImageUrl")]
		[ExtenderControlProperty]
		[RequiredProperty]
		[DefaultValue("")]
		[UrlProperty]
		public string CheckedImageUrl
		{
			get
			{
				return base.GetPropertyValue<string>("CheckedImageUrl", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("CheckedImageUrl", value);
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x0001EAE8 File Offset: 0x0001CCE8
		// (set) Token: 0x06000BC7 RID: 3015 RVA: 0x0001EAFA File Offset: 0x0001CCFA
		[ExtenderControlProperty]
		[DefaultValue("")]
		[UrlProperty]
		[ClientPropertyName("disabledUncheckedImageUrl")]
		public string DisabledUncheckedImageUrl
		{
			get
			{
				return base.GetPropertyValue<string>("DisabledUncheckedImageUrl", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("DisabledUncheckedImageUrl", value);
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x0001EB08 File Offset: 0x0001CD08
		// (set) Token: 0x06000BC9 RID: 3017 RVA: 0x0001EB1A File Offset: 0x0001CD1A
		[ExtenderControlProperty]
		[ClientPropertyName("disabledCheckedImageUrl")]
		[DefaultValue("")]
		[UrlProperty]
		public string DisabledCheckedImageUrl
		{
			get
			{
				return base.GetPropertyValue<string>("DisabledCheckedImageUrl", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("DisabledCheckedImageUrl", value);
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x0001EB28 File Offset: 0x0001CD28
		// (set) Token: 0x06000BCB RID: 3019 RVA: 0x0001EB3A File Offset: 0x0001CD3A
		[DefaultValue("")]
		[ExtenderControlProperty]
		[ClientPropertyName("checkedImageOverUrl")]
		[UrlProperty]
		public string CheckedImageOverUrl
		{
			get
			{
				return base.GetPropertyValue<string>("CheckedImageOverUrl", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("CheckedImageOverUrl", value);
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x0001EB48 File Offset: 0x0001CD48
		// (set) Token: 0x06000BCD RID: 3021 RVA: 0x0001EB5A File Offset: 0x0001CD5A
		[ExtenderControlProperty]
		[ClientPropertyName("uncheckedImageOverUrl")]
		[DefaultValue("")]
		[UrlProperty]
		public string UncheckedImageOverUrl
		{
			get
			{
				return base.GetPropertyValue<string>("UncheckedImageOverUrl", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("UncheckedImageOverUrl", value);
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x0001EB68 File Offset: 0x0001CD68
		// (set) Token: 0x06000BCF RID: 3023 RVA: 0x0001EB7A File Offset: 0x0001CD7A
		[ExtenderControlProperty]
		[ClientPropertyName("uncheckedImageAlternateText")]
		[DefaultValue("")]
		public string UncheckedImageAlternateText
		{
			get
			{
				return base.GetPropertyValue<string>("UncheckedImageAlternateText", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("UncheckedImageAlternateText", value);
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0001EB88 File Offset: 0x0001CD88
		// (set) Token: 0x06000BD1 RID: 3025 RVA: 0x0001EB9A File Offset: 0x0001CD9A
		[ExtenderControlProperty]
		[ClientPropertyName("checkedImageAlternateText")]
		[DefaultValue("")]
		public string CheckedImageAlternateText
		{
			get
			{
				return base.GetPropertyValue<string>("CheckedImageAlternateText", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("CheckedImageAlternateText", value);
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0001EBA8 File Offset: 0x0001CDA8
		// (set) Token: 0x06000BD3 RID: 3027 RVA: 0x0001EBBA File Offset: 0x0001CDBA
		[ClientPropertyName("checkedImageOverAlternateText")]
		[ExtenderControlProperty]
		[DefaultValue("")]
		public string CheckedImageOverAlternateText
		{
			get
			{
				return base.GetPropertyValue<string>("CheckedImageOverAlternateText", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("CheckedImageOverAlternateText", value);
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x0001EBC8 File Offset: 0x0001CDC8
		// (set) Token: 0x06000BD5 RID: 3029 RVA: 0x0001EBDA File Offset: 0x0001CDDA
		[ExtenderControlProperty]
		[ClientPropertyName("uncheckedImageOverAlternateText")]
		[DefaultValue("")]
		public string UncheckedImageOverAlternateText
		{
			get
			{
				return base.GetPropertyValue<string>("UncheckedImageOverAlternateText", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("UncheckedImageOverAlternateText", value);
			}
		}

		// Token: 0x04000451 RID: 1105
		private const string stringImageWidth = "ImageWidth";

		// Token: 0x04000452 RID: 1106
		private const string stringImageHeight = "ImageHeight";

		// Token: 0x04000453 RID: 1107
		private const string stringUncheckedImageUrl = "UncheckedImageUrl";

		// Token: 0x04000454 RID: 1108
		private const string stringCheckedImageUrl = "CheckedImageUrl";

		// Token: 0x04000455 RID: 1109
		private const string stringDisabledUncheckedImageUrl = "DisabledUncheckedImageUrl";

		// Token: 0x04000456 RID: 1110
		private const string stringDisabledCheckedImageUrl = "DisabledCheckedImageUrl";

		// Token: 0x04000457 RID: 1111
		private const string stringCheckedImageOverUrl = "CheckedImageOverUrl";

		// Token: 0x04000458 RID: 1112
		private const string stringUncheckedImageOverUrl = "UncheckedImageOverUrl";

		// Token: 0x04000459 RID: 1113
		private const string stringUncheckedImageAlternateText = "UncheckedImageAlternateText";

		// Token: 0x0400045A RID: 1114
		private const string stringCheckedImageAlternateText = "CheckedImageAlternateText";

		// Token: 0x0400045B RID: 1115
		private const string stringCheckedImageOverAlternateText = "CheckedImageOverAlternateText";

		// Token: 0x0400045C RID: 1116
		private const string stringUncheckedImageOverAlternateText = "UncheckedImageOverAlternateText";
	}
}
