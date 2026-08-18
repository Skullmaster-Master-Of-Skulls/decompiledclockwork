using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Web.UI;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x02000A06 RID: 2566
	internal class RadBarcodeEAN13 : MultiSectionBarcodeBase
	{
		// Token: 0x06006150 RID: 24912 RVA: 0x0016E650 File Offset: 0x0016C850
		public override void RenderContentsRectangles(HtmlTextWriter writer)
		{
			EAN13 ean = new EAN13();
			string text = "";
			try
			{
				text = ean.GetEncoding(this.Text);
			}
			catch (Exception)
			{
			}
			int num = 7;
			if (!base.RenderChecksum)
			{
				text = text.Substring(0, text.Length - num);
			}
			string text2 = text.Substring(0, text.Length / 2);
			string text3 = text.Substring(text.Length / 2);
			float num2 = (float)(EAN13.Prefix.Length + EAN13.Suffix.Length + EAN13.Center.Length + text2.Length + text3.Length);
			float num3 = 0f;
			float widthPercent = base.ShowText ? base.ShortLinesLengthPercentage : 100f;
			List<RectangleF> geometry = ean.GenerateGeometry(EAN13.Prefix);
			List<RectangleF> geometry2 = ean.GenerateGeometry(EAN13.Suffix);
			List<RectangleF> geometry3 = ean.GenerateGeometry(EAN13.Center);
			List<RectangleF> geometry4 = ean.GenerateGeometry(text2);
			List<RectangleF> geometry5 = ean.GenerateGeometry(text3);
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
			string leadingTextboxText = ean.LeadingTextboxText;
			base.LeftText = ean.LeftTextboxText;
			base.RightText = (base.ShowChecksum ? ean.RightTextboxText : ean.RightTextboxText.Substring(0, ean.RightTextboxText.Length - 1));
			if (base.ShowText)
			{
				writer.Write(string.Format("<text x=\"30%\" y=\"" + base.VerticalTextPositionPercentage + "%\" text-anchor=\"middle\" >{0}</text>", base.LeftText));
				writer.Write(string.Format("<text x=\"70%\" y=\"" + base.VerticalTextPositionPercentage + "%\" text-anchor=\"middle\" >{0}</text>", base.RightText));
				writer.Write(string.Format("<text x=\"0%\" y=\"" + base.VerticalTextPositionPercentage + "%\" text-anchor=\"start\" >{0}</text>", leadingTextboxText));
				writer.Write(string.Format("<text x=\"100%\" y=\"" + base.VerticalTextPositionPercentage + "%\" text-anchor=\"end\" >{0}</text>", ">"));
			}
		}

		// Token: 0x06006151 RID: 24913 RVA: 0x0016E92C File Offset: 0x0016CB2C
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
