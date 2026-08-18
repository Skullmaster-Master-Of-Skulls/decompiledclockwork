using System;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using Telerik.Web.UI.HtmlChart.Enums;
using Telerik.Web.UI.HtmlChart.JavaScriptConverters;

namespace Telerik.Web.UI.HtmlChart.Appearance
{
	// Token: 0x0200004E RID: 78
	public class DashedBorderAppearance : BorderAppearance, IJsConvertable, IDefaultCheck
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000267 RID: 615 RVA: 0x000068D7 File Offset: 0x00004AD7
		// (set) Token: 0x06000268 RID: 616 RVA: 0x000068E5 File Offset: 0x00004AE5
		public DashType DashType
		{
			get
			{
				return base.GetViewStateValue<DashType>("DashType", DashType.Solid);
			}
			set
			{
				base.ViewState["DashType"] = value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000269 RID: 617 RVA: 0x000068FD File Offset: 0x00004AFD
		public virtual bool IsDefault
		{
			get
			{
				return base.Color == Color.Empty && this.DashType == DashType.Solid && base.Width == Unit.Empty;
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000692C File Offset: 0x00004B2C
		public virtual void RegisterJSConverters(JavaScriptSerializer serializer)
		{
			serializer.RegisterConverters(new DashedBorderConverter[]
			{
				new DashedBorderConverter()
			});
		}
	}
}
