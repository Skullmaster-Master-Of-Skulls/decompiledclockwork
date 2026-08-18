using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x020003DC RID: 988
	public class BarColumnSeriesLabelsAppearance : SeriesLabelsAppearanceBase
	{
		// Token: 0x06002434 RID: 9268 RVA: 0x00078635 File Offset: 0x00076835
		public BarColumnSeriesLabelsAppearance(string prefix, StateBag OwnerStateBag) : base("bcla" + prefix, OwnerStateBag)
		{
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06002435 RID: 9269 RVA: 0x00078649 File Offset: 0x00076849
		// (set) Token: 0x06002436 RID: 9270 RVA: 0x0007866A File Offset: 0x0007686A
		[DefaultValue(BarColumnLabelsPosition.OutsideEnd)]
		public BarColumnLabelsPosition Position
		{
			get
			{
				return (BarColumnLabelsPosition)(base.ViewState["Position"] ?? BarColumnLabelsPosition.OutsideEnd);
			}
			set
			{
				base.ViewState["Position"] = value;
			}
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x00078684 File Offset: 0x00076884
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder(base.Serialize());
			if (this.Visible == true && this.Position != BarColumnLabelsPosition.OutsideEnd)
			{
				stringBuilder.Insert(stringBuilder.Length - 1, ", position: '" + this.Position.ToString()[0].ToString().ToLower() + this.Position.ToString().Substring(1) + "'");
			}
			return stringBuilder.ToString();
		}
	}
}
