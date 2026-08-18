using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart.Appearance;

namespace Telerik.Web.UI.HtmlChart
{
	// Token: 0x020003EA RID: 1002
	public class HtmlChartLegend : ObjectWithState
	{
		// Token: 0x060024DF RID: 9439 RVA: 0x0007B059 File Offset: 0x00079259
		public HtmlChartLegend(StateBag OwnerStateBag) : base("chl", OwnerStateBag)
		{
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x060024E0 RID: 9440 RVA: 0x0007B067 File Offset: 0x00079267
		[Description("Chart legend visual settings")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Appearance")]
		[DefaultValue("Appearance")]
		public LegendAppearance Appearance
		{
			get
			{
				if (this._appearance == null)
				{
					this._appearance = new LegendAppearance(base.OwnerViewState);
				}
				return this._appearance;
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x060024E1 RID: 9441 RVA: 0x0007B088 File Offset: 0x00079288
		[Category("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue("Appearance")]
		[Description("Chart legend visual settings")]
		public LegendItem Item
		{
			get
			{
				if (this._item == null)
				{
					this._item = new LegendItem(base.OwnerViewState);
				}
				return this._item;
			}
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x060024E2 RID: 9442 RVA: 0x0007B0A9 File Offset: 0x000792A9
		// (set) Token: 0x060024E3 RID: 9443 RVA: 0x0007B0CA File Offset: 0x000792CA
		[DefaultValue(false)]
		public bool Reversed
		{
			get
			{
				return (bool)(base.ViewState["Reversed"] ?? false);
			}
			set
			{
				base.ViewState["Reversed"] = value;
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x060024E4 RID: 9444 RVA: 0x0007B0E2 File Offset: 0x000792E2
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Appearance")]
		public SeriesBorderAppearance BorderAppearance
		{
			get
			{
				if (this._borderAppearance == null)
				{
					this._borderAppearance = new SeriesBorderAppearance();
				}
				return this._borderAppearance;
			}
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x0007B100 File Offset: 0x00079300
		internal string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder("{");
			stringBuilder.Append(this.Appearance.Serialize());
			if (!this.BorderAppearance.IsDefault)
			{
				string value = this.BorderAppearance.Serialize();
				HtmlChartHelper.AddComma(stringBuilder);
				stringBuilder.Append("border:").Append(value);
			}
			stringBuilder.Append(this.Item.Serialize());
			if (this.Reversed)
			{
				stringBuilder.Append(", reverse: true");
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x04000971 RID: 2417
		private LegendAppearance _appearance;

		// Token: 0x04000972 RID: 2418
		private LegendItem _item;

		// Token: 0x04000973 RID: 2419
		private SeriesBorderAppearance _borderAppearance;
	}
}
