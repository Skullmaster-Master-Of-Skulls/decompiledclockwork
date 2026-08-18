using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.HtmlChart.Appearance;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x020003E4 RID: 996
	public class MarkersAppearance : MarkersAppearanceBase
	{
		// Token: 0x06002472 RID: 9330 RVA: 0x00078F9A File Offset: 0x0007719A
		public MarkersAppearance(string prefix, StateBag OwnerStateBag) : base("ma" + prefix, OwnerStateBag)
		{
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06002473 RID: 9331 RVA: 0x00078FAE File Offset: 0x000771AE
		// (set) Token: 0x06002474 RID: 9332 RVA: 0x00078FD3 File Offset: 0x000771D3
		[DefaultValue(typeof(Color), "")]
		[TypeConverter(typeof(ColorConverter))]
		public Color BorderColor
		{
			get
			{
				return (Color)(base.ViewState["BorderColor"] ?? Color.Empty);
			}
			set
			{
				base.ViewState["BorderColor"] = value;
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06002475 RID: 9333 RVA: 0x00078FEB File Offset: 0x000771EB
		// (set) Token: 0x06002476 RID: 9334 RVA: 0x00079010 File Offset: 0x00077210
		[DefaultValue(typeof(Unit), "")]
		[TypeConverter(typeof(UnitConverter))]
		public Unit BorderWidth
		{
			get
			{
				return (Unit)(base.ViewState["BorderWidth"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["BorderWidth"] = value;
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06002477 RID: 9335 RVA: 0x00079028 File Offset: 0x00077228
		// (set) Token: 0x06002478 RID: 9336 RVA: 0x00079048 File Offset: 0x00077248
		[DefaultValue("")]
		public string Visual
		{
			get
			{
				return (string)(base.ViewState["Visual"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Visual"] = value;
			}
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x0007905C File Offset: 0x0007725C
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder("markers: {");
			if (this.Visible == false || this.Visible == null)
			{
				stringBuilder.Append("visible: false");
			}
			else
			{
				stringBuilder.Append(base.Serialize());
				if (this.MarkersType != MarkersType.Circle)
				{
					stringBuilder.Append(", type: '").Append(this.MarkersType.ToString().ToLower()).Append("'");
				}
				if (base.BackgroundColor != Color.Empty)
				{
					stringBuilder.Append(", background: '").Append(HtmlChartHelper.ColorToHex(base.BackgroundColor)).Append("'");
				}
				if (base.Size != null)
				{
					stringBuilder.Append(", size: ").Append(HtmlChartHelper.ToStringInvariant(base.Size));
				}
				this.SerializeBorder(stringBuilder);
				if (this.Visual != string.Empty)
				{
					stringBuilder.AppendFormat(",visual:{0}", this.Visual);
				}
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x00079198 File Offset: 0x00077398
		private void SerializeBorder(StringBuilder sb)
		{
			if (this.BorderColor != Color.Empty || this.BorderWidth != Unit.Empty)
			{
				sb.Append(", border: { ");
				if (this.BorderColor != Color.Empty)
				{
					sb.Append("color: '").Append(HtmlChartHelper.ColorToHex(this.BorderColor)).Append("',");
				}
				if (this.BorderWidth != Unit.Empty)
				{
					sb.AppendFormat("width: {0}", this.BorderWidth.Value);
				}
				HtmlChartHelper.RemoveEndingComma(sb);
				sb.Append("}");
			}
		}
	}
}
