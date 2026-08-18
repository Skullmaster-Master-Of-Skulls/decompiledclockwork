using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using Telerik.Web.UI.HtmlChart.Enums;
using Telerik.Web.UI.HtmlChart.JavaScriptConverters;

namespace Telerik.Web.UI.HtmlChart.Appearance
{
	// Token: 0x0200004F RID: 79
	public class SeriesBorderAppearance : DashedBorderAppearance, IJsConvertable, IDefaultCheck
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00006957 File Offset: 0x00004B57
		// (set) Token: 0x0600026D RID: 621 RVA: 0x00006980 File Offset: 0x00004B80
		[DefaultValue(1.0)]
		public double Opacity
		{
			get
			{
				return (double)(base.ViewState["Opacity"] ?? 1.0);
			}
			set
			{
				double num = 0.0;
				if (value > 0.0)
				{
					num = Math.Min(value, 1.0);
				}
				base.ViewState["Opacity"] = num;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600026E RID: 622 RVA: 0x000069CC File Offset: 0x00004BCC
		public override bool IsDefault
		{
			get
			{
				return base.Color == Color.Empty && base.DashType == DashType.Solid && base.Width == Unit.Empty && this.Opacity == 1.0;
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00006A1C File Offset: 0x00004C1C
		internal override string Serialize()
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			this.RegisterJSConverters(javaScriptSerializer);
			return javaScriptSerializer.Serialize(this);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00006A40 File Offset: 0x00004C40
		public override void RegisterJSConverters(JavaScriptSerializer serializer)
		{
			serializer.RegisterConverters(new SeriesBorderConverter[]
			{
				new SeriesBorderConverter()
			});
		}
	}
}
