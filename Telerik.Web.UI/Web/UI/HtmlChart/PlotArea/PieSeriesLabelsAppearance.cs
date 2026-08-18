using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x020003E5 RID: 997
	public class PieSeriesLabelsAppearance : PieAndDonutLabelsAppearanceBase
	{
		// Token: 0x0600247B RID: 9339 RVA: 0x00079254 File Offset: 0x00077454
		public PieSeriesLabelsAppearance(string prefix, StateBag OwnerStateBag) : base("pla" + prefix, OwnerStateBag)
		{
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x0600247C RID: 9340 RVA: 0x00079268 File Offset: 0x00077468
		// (set) Token: 0x0600247D RID: 9341 RVA: 0x00079289 File Offset: 0x00077489
		[DefaultValue(PieAndDonutLabelsPosition.OutsideEnd)]
		public override PieAndDonutLabelsPosition Position
		{
			get
			{
				return (PieAndDonutLabelsPosition)(base.ViewState["Position"] ?? PieAndDonutLabelsPosition.OutsideEnd);
			}
			set
			{
				base.ViewState["Position"] = value;
			}
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x000792A1 File Offset: 0x000774A1
		protected override void SerializeLabelsProperties(StringBuilder sb)
		{
			if (this.Position != PieAndDonutLabelsPosition.OutsideEnd)
			{
				base.SerializeLabelsPosition(sb);
			}
		}
	}
}
