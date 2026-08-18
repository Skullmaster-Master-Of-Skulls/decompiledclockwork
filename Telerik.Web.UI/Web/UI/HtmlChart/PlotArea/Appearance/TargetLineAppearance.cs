using System;
using System.Web.Script.Serialization;
using Telerik.Web.UI.HtmlChart.JavaScriptConverters;
using Telerik.Web.UI.HtmlChart.JavaScriptConverters.Bullet;

namespace Telerik.Web.UI.HtmlChart.PlotArea.Appearance
{
	// Token: 0x020003C5 RID: 965
	public class TargetLineAppearance : StateManager, IJsConvertable, IDefaultCheck
	{
		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x0600235A RID: 9050 RVA: 0x000763CC File Offset: 0x000745CC
		// (set) Token: 0x0600235B RID: 9051 RVA: 0x000763ED File Offset: 0x000745ED
		public int? Width
		{
			get
			{
				return base.GetViewStateValue<int?>("Width", null);
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x00076408 File Offset: 0x00074608
		public void RegisterJSConverters(JavaScriptSerializer serializer)
		{
			serializer.RegisterConverters(new TargetLineConverter[]
			{
				new TargetLineConverter()
			});
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x0600235D RID: 9053 RVA: 0x0007642C File Offset: 0x0007462C
		public bool IsDefault
		{
			get
			{
				return this.Width == null;
			}
		}
	}
}
