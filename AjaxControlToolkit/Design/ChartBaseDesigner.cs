using System;
using System.Text;
using System.Web.UI.Design;

namespace AjaxControlToolkit.Design
{
	// Token: 0x0200004B RID: 75
	public class ChartBaseDesigner : ControlDesigner
	{
		// Token: 0x0600028E RID: 654 RVA: 0x00008CAC File Offset: 0x00006EAC
		public override string GetDesignTimeHtml(DesignerRegionCollection regions)
		{
			ChartBase chartBase = (ChartBase)base.Component;
			StringBuilder stringBuilder = new StringBuilder(1024);
			stringBuilder.AppendFormat("<div style=\"width: {0}px; height:{1}px;border-style: solid; border-width: 1px;\">", chartBase.ChartWidth, chartBase.ChartHeight);
			return stringBuilder.ToString();
		}
	}
}
