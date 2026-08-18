using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x020003DA RID: 986
	public class AxisTitleAppearance : AppearanceBase
	{
		// Token: 0x06002427 RID: 9255 RVA: 0x00078368 File Offset: 0x00076568
		public AxisTitleAppearance(string prefix, StateBag OwnerStateBag) : base("ata" + prefix, OwnerStateBag)
		{
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06002428 RID: 9256 RVA: 0x0007837C File Offset: 0x0007657C
		// (set) Token: 0x06002429 RID: 9257 RVA: 0x0007839D File Offset: 0x0007659D
		[DefaultValue(AxisTitlePosition.Center)]
		public AxisTitlePosition Position
		{
			get
			{
				return (AxisTitlePosition)(base.ViewState["Position"] ?? AxisTitlePosition.Center);
			}
			set
			{
				base.ViewState["Position"] = value;
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x0600242A RID: 9258 RVA: 0x000783B5 File Offset: 0x000765B5
		// (set) Token: 0x0600242B RID: 9259 RVA: 0x000783D5 File Offset: 0x000765D5
		[DefaultValue("")]
		public string Text
		{
			get
			{
				return (string)(base.ViewState["Text"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x0600242C RID: 9260 RVA: 0x000783E8 File Offset: 0x000765E8
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Appearance")]
		[Description("Text visual settings")]
		[DefaultValue("Appearance")]
		public TextStyle TextStyle
		{
			get
			{
				if (this._textStyle == null)
				{
					this._textStyle = new TextStyle(HtmlChartConstants.DEFAULT_AXIS_TITLE_FONT_SIZE, "Arial,Helvetica,sans-serif");
				}
				return this._textStyle;
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x0600242D RID: 9261 RVA: 0x0007840D File Offset: 0x0007660D
		// (set) Token: 0x0600242E RID: 9262 RVA: 0x0007842D File Offset: 0x0007662D
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

		// Token: 0x0600242F RID: 9263 RVA: 0x00078440 File Offset: 0x00076640
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder("title: {");
			stringBuilder.Append(base.Serialize());
			string text = this.TextStyle.Serialize();
			if (text != string.Empty)
			{
				stringBuilder.Append(",").Append(text);
			}
			if (stringBuilder.Length - 1 >= 0 && stringBuilder[stringBuilder.Length - 1] == ',')
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			if (this.Text != string.Empty)
			{
				stringBuilder.Append(", text: '").Append(this.Text).Append("'");
			}
			if (this.Position != AxisTitlePosition.Center)
			{
				stringBuilder.Append(", position: '").Append(this.Position.ToString().ToLower()).Append("'");
			}
			if (this.Visual != string.Empty)
			{
				stringBuilder.AppendFormat(",visual:{0}", this.Visual);
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x0400095C RID: 2396
		private TextStyle _textStyle;
	}
}
