using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace Telerik.Web.UI.HtmlChart.PlotArea.Series.SeriesItems
{
	// Token: 0x020003C7 RID: 967
	public abstract class SeriesItemBase : StateManager
	{
		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06002364 RID: 9060 RVA: 0x00076497 File Offset: 0x00074697
		// (set) Token: 0x06002365 RID: 9061 RVA: 0x000764BC File Offset: 0x000746BC
		[DefaultValue(typeof(Color), "")]
		[TypeConverter(typeof(ColorConverter))]
		public Color BackgroundColor
		{
			get
			{
				return (Color)(base.ViewState["BackgroundColor"] ?? Color.Empty);
			}
			set
			{
				base.ViewState["BackgroundColor"] = value;
			}
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x000764D4 File Offset: 0x000746D4
		protected internal virtual string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.BackgroundColor != Color.Empty)
			{
				stringBuilder.AppendFormat("color:{0},", HtmlChartHelper.SerializeColor(this.BackgroundColor));
			}
			return stringBuilder.ToString();
		}
	}
}
