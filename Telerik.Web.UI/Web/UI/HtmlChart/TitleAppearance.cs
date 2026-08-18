using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart.PlotArea;

namespace Telerik.Web.UI.HtmlChart
{
	// Token: 0x02000B82 RID: 2946
	public class TitleAppearance : AppearanceBase
	{
		// Token: 0x06006F57 RID: 28503 RVA: 0x001A0213 File Offset: 0x0019E413
		public TitleAppearance(StateBag OwnerStateBag) : base("ta", OwnerStateBag)
		{
		}

		// Token: 0x1700247A RID: 9338
		// (get) Token: 0x06006F58 RID: 28504 RVA: 0x001A0221 File Offset: 0x0019E421
		// (set) Token: 0x06006F59 RID: 28505 RVA: 0x001A0242 File Offset: 0x0019E442
		[DefaultValue(ChartTitleAlign.Center)]
		public ChartTitleAlign Align
		{
			get
			{
				return (ChartTitleAlign)(base.ViewState["Align"] ?? ChartTitleAlign.Center);
			}
			set
			{
				base.ViewState["Align"] = value;
			}
		}

		// Token: 0x1700247B RID: 9339
		// (get) Token: 0x06006F5A RID: 28506 RVA: 0x001A025A File Offset: 0x0019E45A
		// (set) Token: 0x06006F5B RID: 28507 RVA: 0x001A027B File Offset: 0x0019E47B
		[DefaultValue(ChartTitlePosition.Top)]
		public ChartTitlePosition Position
		{
			get
			{
				return (ChartTitlePosition)(base.ViewState["Position"] ?? ChartTitlePosition.Top);
			}
			set
			{
				base.ViewState["Position"] = value;
			}
		}

		// Token: 0x1700247C RID: 9340
		// (get) Token: 0x06006F5C RID: 28508 RVA: 0x001A0293 File Offset: 0x0019E493
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
					this._textStyle = new TextStyle(HtmlChartConstants.DEFAULT_TITLE_FONT_SIZE, "Arial,Helvetica,sans-serif");
				}
				return this._textStyle;
			}
		}

		// Token: 0x06006F5D RID: 28509 RVA: 0x001A02B8 File Offset: 0x0019E4B8
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.Serialize());
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			string text = this.TextStyle.Serialize();
			if (text != string.Empty)
			{
				stringBuilder.Append(",").Append(text);
			}
			if (stringBuilder.Length - 1 >= 0 && stringBuilder[stringBuilder.Length - 1] == ',')
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			if (this.Align != ChartTitleAlign.Center)
			{
				stringBuilder.Append(", align: '").Append(this.Align.ToString().ToLower()).Append("'");
			}
			if (this.Position != ChartTitlePosition.Top)
			{
				stringBuilder.Append(", position: '").Append(this.Position.ToString().ToLower()).Append("'");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001E06 RID: 7686
		private TextStyle _textStyle;
	}
}
