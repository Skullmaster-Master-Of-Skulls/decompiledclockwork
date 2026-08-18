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
	// Token: 0x02000153 RID: 339
	[ClientScriptResource("Sys.Extended.UI.NumericUpDownBehavior", "NumericUpDown")]
	[Designer(typeof(NumericUpDownExtenderDesigner))]
	[ClientCssResource("NumericUpDown")]
	[TargetControlType(typeof(TextBox))]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ToolboxBitmap(typeof(Accessor), "NumericUpDown.bmp")]
	public class NumericUpDownExtender : ExtenderControlBase
	{
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x000178DB File Offset: 0x00015ADB
		// (set) Token: 0x060008D2 RID: 2258 RVA: 0x000178ED File Offset: 0x00015AED
		[ExtenderControlProperty]
		[ClientPropertyName("targetButtonUpID")]
		[IDReferenceProperty(typeof(Control))]
		public string TargetButtonUpID
		{
			get
			{
				return base.GetPropertyValue<string>("TargetButtonUpID", "");
			}
			set
			{
				base.SetPropertyValue<string>("TargetButtonUpID", value);
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x000178FB File Offset: 0x00015AFB
		// (set) Token: 0x060008D4 RID: 2260 RVA: 0x0001790D File Offset: 0x00015B0D
		[IDReferenceProperty(typeof(Control))]
		[ClientPropertyName("targetButtonDownID")]
		[ExtenderControlProperty]
		public string TargetButtonDownID
		{
			get
			{
				return base.GetPropertyValue<string>("TargetButtonDownID", "");
			}
			set
			{
				base.SetPropertyValue<string>("TargetButtonDownID", value);
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x0001791B File Offset: 0x00015B1B
		// (set) Token: 0x060008D6 RID: 2262 RVA: 0x0001792D File Offset: 0x00015B2D
		[ClientPropertyName("serviceUpPath")]
		[Editor("System.Web.UI.Design.UrlEditor", typeof(UITypeEditor))]
		[UrlProperty]
		[TypeConverter(typeof(ServicePathConverter))]
		[ExtenderControlProperty]
		public string ServiceUpPath
		{
			get
			{
				return base.GetPropertyValue<string>("ServiceUpPath", "");
			}
			set
			{
				base.SetPropertyValue<string>("ServiceUpPath", value);
			}
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0001793B File Offset: 0x00015B3B
		private bool ShouldSerializeServiceUpPath()
		{
			return !string.IsNullOrEmpty(this.ServiceUpMethod);
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x0001794B File Offset: 0x00015B4B
		// (set) Token: 0x060008D9 RID: 2265 RVA: 0x0001795D File Offset: 0x00015B5D
		[ExtenderControlProperty]
		[ClientPropertyName("serviceUpMethod")]
		public string ServiceUpMethod
		{
			get
			{
				return base.GetPropertyValue<string>("ServiceUpMethod", "");
			}
			set
			{
				base.SetPropertyValue<string>("ServiceUpMethod", value);
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x0001796B File Offset: 0x00015B6B
		// (set) Token: 0x060008DB RID: 2267 RVA: 0x0001797D File Offset: 0x00015B7D
		[UrlProperty]
		[Editor("System.Web.UI.Design.UrlEditor", typeof(UITypeEditor))]
		[TypeConverter(typeof(ServicePathConverter))]
		[ClientPropertyName("serviceDownPath")]
		[ExtenderControlProperty]
		public string ServiceDownPath
		{
			get
			{
				return base.GetPropertyValue<string>("ServiceDownPath", "");
			}
			set
			{
				base.SetPropertyValue<string>("ServiceDownPath", value);
			}
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0001798B File Offset: 0x00015B8B
		private bool ShouldSerializeServieDownPath()
		{
			return !string.IsNullOrEmpty(this.ServiceDownMethod);
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x0001799B File Offset: 0x00015B9B
		// (set) Token: 0x060008DE RID: 2270 RVA: 0x000179AD File Offset: 0x00015BAD
		[ExtenderControlProperty]
		[ClientPropertyName("serviceDownMethod")]
		public string ServiceDownMethod
		{
			get
			{
				return base.GetPropertyValue<string>("ServiceDownMethod", "");
			}
			set
			{
				base.SetPropertyValue<string>("ServiceDownMethod", value);
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x000179BB File Offset: 0x00015BBB
		// (set) Token: 0x060008E0 RID: 2272 RVA: 0x000179D1 File Offset: 0x00015BD1
		[DefaultValue(1.0)]
		[ClientPropertyName("step")]
		[ExtenderControlProperty]
		public double Step
		{
			get
			{
				return base.GetPropertyValue<double>("Step", 1.0);
			}
			set
			{
				base.SetPropertyValue<double>("Step", value);
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x000179DF File Offset: 0x00015BDF
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x000179F5 File Offset: 0x00015BF5
		[ClientPropertyName("minimum")]
		[ExtenderControlProperty]
		public double Minimum
		{
			get
			{
				return base.GetPropertyValue<double>("Minimum", double.MinValue);
			}
			set
			{
				base.SetPropertyValue<double>("Minimum", value);
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x00017A03 File Offset: 0x00015C03
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x00017A19 File Offset: 0x00015C19
		[ExtenderControlProperty]
		[ClientPropertyName("maximum")]
		public double Maximum
		{
			get
			{
				return base.GetPropertyValue<double>("Maximum", double.MaxValue);
			}
			set
			{
				base.SetPropertyValue<double>("Maximum", value);
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x00017A27 File Offset: 0x00015C27
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x00017A39 File Offset: 0x00015C39
		[Editor("System.ComponentModel.Design.MultilineStringEditor", typeof(UITypeEditor))]
		[ExtenderControlProperty]
		[ClientPropertyName("refValues")]
		public string RefValues
		{
			get
			{
				return base.GetPropertyValue<string>("RefValues", "");
			}
			set
			{
				base.SetPropertyValue<string>("RefValues", value);
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x00017A47 File Offset: 0x00015C47
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x00017A55 File Offset: 0x00015C55
		[ExtenderControlProperty]
		[ClientPropertyName("width")]
		[RequiredProperty]
		public int Width
		{
			get
			{
				return base.GetPropertyValue<int>("Width", 0);
			}
			set
			{
				base.SetPropertyValue<int>("Width", value);
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x00017A63 File Offset: 0x00015C63
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x00017A75 File Offset: 0x00015C75
		[ExtenderControlProperty]
		[ClientPropertyName("tag")]
		public string Tag
		{
			get
			{
				return base.GetPropertyValue<string>("Tag", "");
			}
			set
			{
				base.SetPropertyValue<string>("Tag", value);
			}
		}
	}
}
