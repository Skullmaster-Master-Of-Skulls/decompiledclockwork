using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web.UI.WebControls;
using Telerik.Web.UI.HtmlChart.TextStyles;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x02000B9C RID: 2972
	public class TextStyle : TextStyleBase
	{
		// Token: 0x06007037 RID: 28727 RVA: 0x001A33D1 File Offset: 0x001A15D1
		internal TextStyle()
		{
		}

		// Token: 0x06007038 RID: 28728 RVA: 0x001A33D9 File Offset: 0x001A15D9
		public TextStyle(Unit defaultFontSize, string defaultFontFamily)
		{
			this._defaultFontSize = defaultFontSize;
			this._defaultFontFamily = defaultFontFamily;
		}

		// Token: 0x170024B5 RID: 9397
		// (get) Token: 0x06007039 RID: 28729 RVA: 0x001A33EF File Offset: 0x001A15EF
		// (set) Token: 0x0600703A RID: 28730 RVA: 0x001A3410 File Offset: 0x001A1610
		[DefaultValue(false)]
		public bool Bold
		{
			get
			{
				return (bool)(base.ViewState["Bold"] ?? false);
			}
			set
			{
				base.ViewState["Bold"] = value;
			}
		}

		// Token: 0x170024B6 RID: 9398
		// (get) Token: 0x0600703B RID: 28731 RVA: 0x001A3428 File Offset: 0x001A1628
		// (set) Token: 0x0600703C RID: 28732 RVA: 0x001A3449 File Offset: 0x001A1649
		[DefaultValue(false)]
		public bool Italic
		{
			get
			{
				return (bool)(base.ViewState["Italic"] ?? false);
			}
			set
			{
				base.ViewState["Italic"] = value;
			}
		}

		// Token: 0x170024B7 RID: 9399
		// (get) Token: 0x0600703D RID: 28733 RVA: 0x001A3461 File Offset: 0x001A1661
		// (set) Token: 0x0600703E RID: 28734 RVA: 0x001A3486 File Offset: 0x001A1686
		[DefaultValue(typeof(Color), "")]
		[TypeConverter(typeof(ColorConverter))]
		public Color Color
		{
			get
			{
				return (Color)(base.ViewState["Color"] ?? Color.Empty);
			}
			set
			{
				base.ViewState["Color"] = value;
			}
		}

		// Token: 0x170024B8 RID: 9400
		// (get) Token: 0x0600703F RID: 28735 RVA: 0x001A349E File Offset: 0x001A169E
		// (set) Token: 0x06007040 RID: 28736 RVA: 0x001A34C4 File Offset: 0x001A16C4
		[DefaultValue(typeof(Unit), "12px")]
		public Unit FontSize
		{
			get
			{
				return (Unit)(base.ViewState["FontSize"] ?? this._defaultFontSize);
			}
			set
			{
				base.ViewState["FontSize"] = value;
			}
		}

		// Token: 0x170024B9 RID: 9401
		// (get) Token: 0x06007041 RID: 28737 RVA: 0x001A34DC File Offset: 0x001A16DC
		// (set) Token: 0x06007042 RID: 28738 RVA: 0x001A34FD File Offset: 0x001A16FD
		[DefaultValue("Arial,Helvetica,sans-serif")]
		public string FontFamily
		{
			get
			{
				return (string)(base.ViewState["FontFamily"] ?? this._defaultFontFamily);
			}
			set
			{
				base.ViewState["FontFamily"] = value;
			}
		}

		// Token: 0x170024BA RID: 9402
		// (get) Token: 0x06007043 RID: 28739 RVA: 0x001A3510 File Offset: 0x001A1710
		// (set) Token: 0x06007044 RID: 28740 RVA: 0x001A3530 File Offset: 0x001A1730
		[DefaultValue("")]
		public string Padding
		{
			get
			{
				return (string)(base.ViewState["Padding"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Padding"] = value;
			}
		}

		// Token: 0x06007045 RID: 28741 RVA: 0x001A3544 File Offset: 0x001A1744
		protected internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string serializedFont = this.GetSerializedFont();
			stringBuilder.Append(serializedFont);
			string serializedColor = this.GetSerializedColor();
			stringBuilder.Append(serializedColor);
			stringBuilder.Append(base.Serialize());
			string serializedPadding = this.GetSerializedPadding();
			stringBuilder.Append(serializedPadding);
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06007046 RID: 28742 RVA: 0x001A35A0 File Offset: 0x001A17A0
		internal string GetSerializedFont()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.Bold)
			{
				stringBuilder.Append("bold ");
			}
			if (this.Italic)
			{
				stringBuilder.Append("italic ");
			}
			string text = this.FontSize.ToString();
			if (text.Contains("px") || text.Contains("pt") || text.Contains("em"))
			{
				stringBuilder.Append(text).Append(" ");
			}
			else
			{
				stringBuilder.Append(this.FontSize.Value).Append("px ");
			}
			stringBuilder.Append(this.FontFamily);
			stringBuilder.Insert(0, "font: '").Append("',");
			return stringBuilder.ToString();
		}

		// Token: 0x06007047 RID: 28743 RVA: 0x001A3674 File Offset: 0x001A1874
		internal string GetSerializedColor()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.Color != Color.Empty)
			{
				stringBuilder.Append("color: '").Append(HtmlChartHelper.ColorToHex(this.Color)).Append("',");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06007048 RID: 28744 RVA: 0x001A36C8 File Offset: 0x001A18C8
		internal string SerializeFontAndColor()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.GetSerializedFont());
			stringBuilder.Append(this.GetSerializedColor());
			return stringBuilder.ToString();
		}

		// Token: 0x06007049 RID: 28745 RVA: 0x001A36FC File Offset: 0x001A18FC
		internal string GetSerializedPadding()
		{
			StringBuilder stringBuilder = new StringBuilder();
			base.SerializeSpacing(stringBuilder, this.Padding, "padding");
			return stringBuilder.ToString();
		}

		// Token: 0x0600704A RID: 28746 RVA: 0x001A3728 File Offset: 0x001A1928
		internal string SerializeMarginAndPadding()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.GetSerializedMargin());
			stringBuilder.Append(this.GetSerializedPadding());
			return stringBuilder.ToString();
		}

		// Token: 0x04001E20 RID: 7712
		private Unit _defaultFontSize;

		// Token: 0x04001E21 RID: 7713
		private string _defaultFontFamily;
	}
}
