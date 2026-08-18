using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x020003E0 RID: 992
	public class DonutSeriesLabelsAppearance : PieAndDonutLabelsAppearanceBase
	{
		// Token: 0x06002456 RID: 9302 RVA: 0x00078BF6 File Offset: 0x00076DF6
		public DonutSeriesLabelsAppearance(string prefix, StateBag OwnerStateBag) : base("dla" + prefix, OwnerStateBag)
		{
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06002457 RID: 9303 RVA: 0x00078C0A File Offset: 0x00076E0A
		// (set) Token: 0x06002458 RID: 9304 RVA: 0x00078C2B File Offset: 0x00076E2B
		[DefaultValue(PieAndDonutLabelsPosition.Center)]
		public override PieAndDonutLabelsPosition Position
		{
			get
			{
				return (PieAndDonutLabelsPosition)(base.ViewState["Position"] ?? PieAndDonutLabelsPosition.Center);
			}
			set
			{
				base.ViewState["Position"] = value;
			}
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x00078C43 File Offset: 0x00076E43
		protected override void SerializeLabelsProperties(StringBuilder sb)
		{
			if (this.Position != PieAndDonutLabelsPosition.Center)
			{
				base.SerializeLabelsPosition(sb);
			}
		}
	}
}
