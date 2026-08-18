using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart.TextStyles;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x020004F3 RID: 1267
	public class PlotAreaAppearance : AppearanceBase
	{
		// Token: 0x06002D35 RID: 11573 RVA: 0x000948E7 File Offset: 0x00092AE7
		public PlotAreaAppearance(StateBag OwnerStateBag) : base("pa", OwnerStateBag)
		{
		}

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x06002D36 RID: 11574 RVA: 0x000948F5 File Offset: 0x00092AF5
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public PlotAreaFillStyle FillStyle
		{
			get
			{
				if (this._fillStyle == null)
				{
					this._fillStyle = new PlotAreaFillStyle("pafs", base.OwnerViewState);
				}
				return this._fillStyle;
			}
		}

		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x06002D37 RID: 11575 RVA: 0x0009491B File Offset: 0x00092B1B
		[Description("Text visual settings")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Appearance")]
		[DefaultValue("Appearance")]
		public PlotAreaTextStyle TextStyle
		{
			get
			{
				if (this._textStyle == null)
				{
					this._textStyle = new PlotAreaTextStyle();
				}
				return this._textStyle;
			}
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x00094938 File Offset: 0x00092B38
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			this.AppendSerializedStyle(stringBuilder, this.FillStyle.Serialize());
			this.AppendSerializedStyle(stringBuilder, this.TextStyle.Serialize());
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x0009499B File Offset: 0x00092B9B
		private void AppendSerializedStyle(StringBuilder sb, string serializedStyle)
		{
			if (!string.IsNullOrEmpty(serializedStyle))
			{
				sb.AppendFormat("{0},", serializedStyle);
			}
		}

		// Token: 0x04000C31 RID: 3121
		private PlotAreaFillStyle _fillStyle;

		// Token: 0x04000C32 RID: 3122
		private PlotAreaTextStyle _textStyle;
	}
}
