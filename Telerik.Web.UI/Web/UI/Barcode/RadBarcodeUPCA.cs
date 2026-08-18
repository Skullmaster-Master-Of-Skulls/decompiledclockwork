using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Web.UI;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x02000A04 RID: 2564
	internal class RadBarcodeUPCA : MultiSectionBarcodeBase
	{
		// Token: 0x17001FE5 RID: 8165
		// (get) Token: 0x06006142 RID: 24898 RVA: 0x0016E027 File Offset: 0x0016C227
		// (set) Token: 0x06006143 RID: 24899 RVA: 0x0016E02F File Offset: 0x0016C22F
		public string LeadingText { get; set; }

		// Token: 0x17001FE6 RID: 8166
		// (get) Token: 0x06006144 RID: 24900 RVA: 0x0016E038 File Offset: 0x0016C238
		// (set) Token: 0x06006145 RID: 24901 RVA: 0x0016E040 File Offset: 0x0016C240
		public string TrailingText { get; set; }

		// Token: 0x06006146 RID: 24902 RVA: 0x0016E04C File Offset: 0x0016C24C
		public override void RenderContentsRectangles(HtmlTextWriter writer)
		{
			UPCA upca = new UPCA();
			string text = "";
			try
			{
				text = upca.GetEncoding(this.Text);
			}
			catch (Exception)
			{
			}
			int num = 7;
			if (!base.RenderChecksum)
			{
				text = text.Substring(0, text.Length - num);
			}
			base.LeftText = upca.LeftTextboxText;
			base.RightText = (base.ShowChecksum ? upca.RightTextboxText : upca.RightTextboxText.Substring(0, upca.RightTextboxText.Length - 1));
			string text2 = text.Substring(0, text.Length / 2);
			string text3 = text.Substring(text.Length / 2);
			float num2 = (float)(EAN13.Prefix.Length + EAN13.Suffix.Length + EAN13.Center.Length + text2.Length + text3.Length);
			float num3 = 0f;
			List<RectangleF> geometry = upca.GenerateGeometry(EAN13.Prefix);
			List<RectangleF> geometry2 = upca.GenerateGeometry(EAN13.Suffix);
			List<RectangleF> geometry3 = upca.GenerateGeometry(EAN13.Center);
			List<RectangleF> geometry4 = upca.GenerateGeometry(text2);
			List<RectangleF> geometry5 = upca.GenerateGeometry(text3);
			float widthPercent = base.ShowText ? base.ShortLinesLengthPercentage : 100f;
			float start = num3;
			num3 += (float)EAN13.Prefix.Length / num2;
			this.RenderRectangles(geometry, writer, start, num3, 1f, widthPercent);
			start = num3;
			num3 += (float)text2.Length / num2;
			this.RenderRectangles(geometry4, writer, start, num3, base.ShortLinesLengthPercentage / 100f, widthPercent);
			start = num3;
			num3 += (float)EAN13.Center.Length / num2;
			this.RenderRectangles(geometry3, writer, start, num3, 1f, widthPercent);
			start = num3;
			num3 += (float)text3.Length / num2;
			this.RenderRectangles(geometry5, writer, start, num3, base.ShortLinesLengthPercentage / 100f, widthPercent);
			start = num3;
			num3 += (float)EAN13.Suffix.Length / num2;
			this.RenderRectangles(geometry2, writer, start, num3, 1f, widthPercent);
			this.LeadingText = upca.LeadingTextboxText;
			base.LeftText = upca.LeftTextboxText;
			base.RightText = upca.RightTextboxText;
			this.TrailingText = upca.EndTextboxText;
			if (base.ShowText)
			{
				writer.Write(string.Format("<text x=\"30%\" y=\"" + base.VerticalTextPositionPercentage + "%\" text-anchor=\"middle\" >{0}</text>", base.LeftText));
				writer.Write(string.Format("<text x=\"70%\" y=\"" + base.VerticalTextPositionPercentage + "%\" text-anchor=\"middle\" >{0}</text>", base.RightText));
				writer.Write(string.Format("<text x=\"0%\" y=\"" + base.VerticalTextPositionPercentage + "%\" text-anchor=\"start\" >{0}</text>", this.LeadingText));
				if (base.ShowChecksum)
				{
					writer.Write(string.Format("<text x=\"100%\" y=\"" + base.VerticalTextPositionPercentage + "%\" text-anchor=\"end\" >{0}</text>", this.TrailingText));
				}
			}
		}

		// Token: 0x06006147 RID: 24903 RVA: 0x0016E35C File Offset: 0x0016C55C
		private void RenderRectangles(List<RectangleF> geometry, HtmlTextWriter writer, float start, float end, float height, float widthPercent)
		{
			foreach (RectangleF rectangleF in geometry)
			{
				writer.Write(string.Format(CultureInfo.InvariantCulture, "<rect x='{0}%' y='{1}%' width='{2}%' height='{3}%' style='fill:rgb(0,0,0)'></rect>", new object[]
				{
					rectangleF.X * (end - start) * widthPercent + start * widthPercent + (100f - widthPercent) / 2f,
					100f * rectangleF.Y,
					widthPercent * (end - start) * rectangleF.Width,
					100f * rectangleF.Height * height
				}));
			}
		}
	}
}
