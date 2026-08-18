using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart.PlotArea;

namespace Telerik.Web.UI.HtmlChart.Appearance
{
	// Token: 0x020004CD RID: 1229
	public class FunnelSeriesLabelsAppearance : SeriesLabelsAppearanceBase
	{
		// Token: 0x06002C89 RID: 11401 RVA: 0x00092514 File Offset: 0x00090714
		public FunnelSeriesLabelsAppearance(string prefix, StateBag OwnerStateBag) : base("fla" + prefix, OwnerStateBag)
		{
		}

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x06002C8A RID: 11402 RVA: 0x00092528 File Offset: 0x00090728
		// (set) Token: 0x06002C8B RID: 11403 RVA: 0x00092549 File Offset: 0x00090749
		[DefaultValue(PieAndDonutLabelsPosition.Center)]
		public FunnelLabelsPosition Position
		{
			get
			{
				return (FunnelLabelsPosition)(base.ViewState["Position"] ?? FunnelLabelsPosition.Center);
			}
			set
			{
				base.ViewState["Position"] = value;
			}
		}

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x06002C8C RID: 11404 RVA: 0x00092561 File Offset: 0x00090761
		// (set) Token: 0x06002C8D RID: 11405 RVA: 0x00092582 File Offset: 0x00090782
		[DefaultValue(FunnelLabelsAlignment.Center)]
		public FunnelLabelsAlignment Align
		{
			get
			{
				return (FunnelLabelsAlignment)(base.ViewState["Align"] ?? FunnelLabelsAlignment.Center);
			}
			set
			{
				base.ViewState["Align"] = value;
			}
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x0009259C File Offset: 0x0009079C
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder(base.Serialize());
			if (this.Visible == true)
			{
				this.SerializeLabelsProperties(stringBuilder);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x000925DE File Offset: 0x000907DE
		private void SerializeLabelsProperties(StringBuilder sb)
		{
			this.SerializeLabelsPosition(sb);
			this.SerializeLabelsAlignment(sb);
		}

		// Token: 0x06002C90 RID: 11408 RVA: 0x000925F0 File Offset: 0x000907F0
		private void SerializeLabelsPosition(StringBuilder sb)
		{
			if (this.Position != FunnelLabelsPosition.Center)
			{
				HtmlChartHelper.RemoveEndingComma(sb);
				sb.Insert(sb.Length - 1, ",position:'" + HtmlChartHelper.StringToLowerCamelCase(this.Position.ToString()) + "'");
			}
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x00092640 File Offset: 0x00090840
		private void SerializeLabelsAlignment(StringBuilder sb)
		{
			if (this.Align != FunnelLabelsAlignment.Center)
			{
				HtmlChartHelper.RemoveEndingComma(sb);
				sb.Insert(sb.Length - 1, ",align:'" + HtmlChartHelper.StringToLowerCamelCase(this.Align.ToString()) + "'");
			}
		}
	}
}
