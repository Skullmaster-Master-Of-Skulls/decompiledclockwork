using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x020003D9 RID: 985
	public class AxisLabelsAppearance : LabelsAppearanceBase
	{
		// Token: 0x0600241C RID: 9244 RVA: 0x00078159 File Offset: 0x00076359
		public AxisLabelsAppearance(string prefix, StateBag OwnerStateBag) : base("ala" + prefix, OwnerStateBag)
		{
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x0600241D RID: 9245 RVA: 0x0007816D File Offset: 0x0007636D
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public DateFormatter DateFormats
		{
			get
			{
				if (this._dateFormats == null)
				{
					this._dateFormats = new DateFormatter();
				}
				return this._dateFormats;
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x0600241E RID: 9246 RVA: 0x00078188 File Offset: 0x00076388
		// (set) Token: 0x0600241F RID: 9247 RVA: 0x000781A9 File Offset: 0x000763A9
		[DefaultValue(false)]
		public bool Mirror
		{
			get
			{
				return (bool)(base.ViewState["Mirror"] ?? false);
			}
			set
			{
				base.ViewState["Mirror"] = value;
			}
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06002420 RID: 9248 RVA: 0x000781C1 File Offset: 0x000763C1
		// (set) Token: 0x06002421 RID: 9249 RVA: 0x000781E2 File Offset: 0x000763E2
		[DefaultValue(AxisLabelPosition.OnAxis)]
		public AxisLabelPosition Position
		{
			get
			{
				return (AxisLabelPosition)(base.ViewState["AxisLabelPosition"] ?? AxisLabelPosition.OnAxis);
			}
			set
			{
				base.ViewState["AxisLabelPosition"] = value;
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06002422 RID: 9250 RVA: 0x000781FA File Offset: 0x000763FA
		// (set) Token: 0x06002423 RID: 9251 RVA: 0x00078211 File Offset: 0x00076411
		public int? Step
		{
			get
			{
				return (int?)base.ViewState["Step"];
			}
			set
			{
				base.ViewState["Step"] = value;
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06002424 RID: 9252 RVA: 0x00078229 File Offset: 0x00076429
		// (set) Token: 0x06002425 RID: 9253 RVA: 0x00078240 File Offset: 0x00076440
		public int? Skip
		{
			get
			{
				return (int?)base.ViewState["Skip"];
			}
			set
			{
				base.ViewState["Skip"] = value;
			}
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x00078258 File Offset: 0x00076458
		internal override void SerializeDataFormats(StringBuilder sb)
		{
			if (base.DataFormatString != string.Empty)
			{
				sb.Append(", format: '").Append(base.DataFormatString).Append("'");
			}
			else if (!this.DateFormats.IsDefault)
			{
				sb.Append(", dateFormats: ").Append(this.DateFormats.Serialize());
			}
			HtmlChartHelper.RemoveEndingComma(sb);
			HtmlChartHelper.AddComma(sb);
			sb.AppendFormat("{0}:{1}", "mirror", HtmlChartHelper.SerializeBoolean(this.Mirror));
			if (this.Position != AxisLabelPosition.OnAxis)
			{
				sb.AppendFormat(",{0}: '{1}'", "position", HtmlChartHelper.StringToLowerCamelCase(this.Position.ToString()));
			}
			if (this.Step != null)
			{
				sb.AppendFormat(",step: {0}", this.Step);
			}
			if (this.Skip != null)
			{
				sb.AppendFormat(",skip: {0}", this.Skip);
			}
		}

		// Token: 0x0400095B RID: 2395
		private DateFormatter _dateFormats;
	}
}
