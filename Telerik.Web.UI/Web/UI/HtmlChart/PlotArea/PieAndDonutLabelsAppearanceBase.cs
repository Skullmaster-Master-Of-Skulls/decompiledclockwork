using System;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x020003DF RID: 991
	public abstract class PieAndDonutLabelsAppearanceBase : SeriesLabelsAppearanceBase
	{
		// Token: 0x06002450 RID: 9296 RVA: 0x00078B28 File Offset: 0x00076D28
		public PieAndDonutLabelsAppearanceBase(string prefix, StateBag OwnerStateBag) : base("pdla" + prefix, OwnerStateBag)
		{
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06002451 RID: 9297 RVA: 0x00078B3C File Offset: 0x00076D3C
		// (set) Token: 0x06002452 RID: 9298 RVA: 0x00078B5D File Offset: 0x00076D5D
		public virtual PieAndDonutLabelsPosition Position
		{
			get
			{
				return (PieAndDonutLabelsPosition)(base.ViewState["Position"] ?? PieAndDonutLabelsPosition.InsideEnd);
			}
			set
			{
				base.ViewState["Position"] = value;
			}
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x00078B78 File Offset: 0x00076D78
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder(base.Serialize());
			if (this.Visible == true)
			{
				this.SerializeLabelsProperties(stringBuilder);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002454 RID: 9300
		protected abstract void SerializeLabelsProperties(StringBuilder sb);

		// Token: 0x06002455 RID: 9301 RVA: 0x00078BBA File Offset: 0x00076DBA
		protected void SerializeLabelsPosition(StringBuilder sb)
		{
			HtmlChartHelper.RemoveEndingComma(sb);
			sb.Insert(sb.Length - 1, ",position: '" + HtmlChartHelper.StringToLowerCamelCase(this.Position.ToString()) + "'");
		}
	}
}
