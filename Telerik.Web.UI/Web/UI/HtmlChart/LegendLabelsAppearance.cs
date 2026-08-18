using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart.PlotArea;

namespace Telerik.Web.UI.HtmlChart
{
	// Token: 0x02000B83 RID: 2947
	public class LegendLabelsAppearance : AppearanceBase
	{
		// Token: 0x06006F5E RID: 28510 RVA: 0x001A03AE File Offset: 0x0019E5AE
		public LegendLabelsAppearance(string key, StateBag OwnerStateBag) : base("lla" + key, OwnerStateBag)
		{
		}

		// Token: 0x1700247D RID: 9341
		// (get) Token: 0x06006F5F RID: 28511 RVA: 0x001A03C2 File Offset: 0x0019E5C2
		[Category("Appearance")]
		[DefaultValue("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Text visual settings")]
		public TextStyle TextStyle
		{
			get
			{
				if (this._textStyle == null)
				{
					this._textStyle = new TextStyle(HtmlChartConstants.DEFAULT_LEGEND_FONT_SIZE, "Arial,Helvetica,sans-serif");
				}
				return this._textStyle;
			}
		}

		// Token: 0x1700247E RID: 9342
		// (get) Token: 0x06006F60 RID: 28512 RVA: 0x001A03E7 File Offset: 0x0019E5E7
		// (set) Token: 0x06006F61 RID: 28513 RVA: 0x001A0407 File Offset: 0x0019E607
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(true)]
		[Bindable(true)]
		[DefaultValue("")]
		public string ClientTemplate
		{
			get
			{
				return ((string)base.ViewState["ClientTemplate"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["ClientTemplate"] = value;
			}
		}

		// Token: 0x06006F62 RID: 28514 RVA: 0x001A041C File Offset: 0x0019E61C
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.Serialize());
			string text = this.TextStyle.SerializeFontAndColor();
			if (text != string.Empty || !string.IsNullOrEmpty(this.ClientTemplate))
			{
				stringBuilder.Append("labels: {");
				if (text != string.Empty)
				{
					stringBuilder.Append(text);
				}
				if (stringBuilder.Length - 1 >= 0 && stringBuilder[stringBuilder.Length - 1] == ',')
				{
					stringBuilder.Remove(stringBuilder.Length - 1, 1);
				}
				if (!string.IsNullOrEmpty(this.ClientTemplate))
				{
					stringBuilder.AppendFormat(",template: '{0}'", HtmlChartHelper.GetTemplateWithoutNewLinesAndTabs(this.ClientTemplate));
				}
				stringBuilder.Append("}");
			}
			string text2 = this.TextStyle.SerializeMarginAndPadding();
			if (text2 != string.Empty)
			{
				stringBuilder.Append(", ");
				stringBuilder.Append(text2);
				if (stringBuilder.Length - 1 >= 0 && stringBuilder[stringBuilder.Length - 1] == ',')
				{
					stringBuilder.Remove(stringBuilder.Length - 1, 1);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001E07 RID: 7687
		private TextStyle _textStyle;
	}
}
