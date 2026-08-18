using System;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x020003E6 RID: 998
	public class RangeSeriesLabelsAppearance : BarColumnSeriesLabelsAppearance
	{
		// Token: 0x0600247F RID: 9343 RVA: 0x000792B3 File Offset: 0x000774B3
		public RangeSeriesLabelsAppearance(string prefix, StateBag OwnerStateBag) : base("rsla" + prefix, OwnerStateBag)
		{
			this.ownerStateBag = OwnerStateBag;
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06002480 RID: 9344 RVA: 0x000792CE File Offset: 0x000774CE
		// (set) Token: 0x06002481 RID: 9345 RVA: 0x000792E8 File Offset: 0x000774E8
		[DefaultValue(null)]
		public override bool? Visible
		{
			get
			{
				return (bool?)base.ViewState["Visible"];
			}
			set
			{
				if (value == true)
				{
					this.FromLabelsAppearance.Visible = value;
					this.ToLabelsAppearance.Visible = value;
				}
				base.ViewState["Visible"] = value;
			}
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x06002482 RID: 9346 RVA: 0x0007933A File Offset: 0x0007753A
		[DefaultValue("LabelsAppearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Appearance")]
		[Description("Series labels visual settings")]
		public BarColumnSeriesLabelsAppearance FromLabelsAppearance
		{
			get
			{
				if (this._fromLabelsAppearance == null)
				{
					this._fromLabelsAppearance = new BarColumnSeriesLabelsAppearance("rslafbcla", this.ownerStateBag);
				}
				return this._fromLabelsAppearance;
			}
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06002483 RID: 9347 RVA: 0x00079360 File Offset: 0x00077560
		[Category("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Series labels visual settings")]
		[DefaultValue("LabelsAppearance")]
		public BarColumnSeriesLabelsAppearance ToLabelsAppearance
		{
			get
			{
				if (this._toLabelsAppearance == null)
				{
					this._toLabelsAppearance = new BarColumnSeriesLabelsAppearance("rslatbcla", this.ownerStateBag);
				}
				return this._toLabelsAppearance;
			}
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x00079388 File Offset: 0x00077588
		internal override string Serialize()
		{
			Regex regex = new Regex("labels\\s*:");
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(",");
			stringBuilder.Append(regex.Replace(this.FromLabelsAppearance.Serialize(), "from: ", 1));
			stringBuilder.Append(",");
			stringBuilder.Append(regex.Replace(this.ToLabelsAppearance.Serialize(), "to: ", 1));
			StringBuilder stringBuilder2 = new StringBuilder(base.Serialize());
			stringBuilder2.Insert(stringBuilder2.Length - 1, stringBuilder.ToString());
			return stringBuilder2.ToString();
		}

		// Token: 0x0400095E RID: 2398
		private StateBag ownerStateBag;

		// Token: 0x0400095F RID: 2399
		private BarColumnSeriesLabelsAppearance _fromLabelsAppearance;

		// Token: 0x04000960 RID: 2400
		private BarColumnSeriesLabelsAppearance _toLabelsAppearance;
	}
}
