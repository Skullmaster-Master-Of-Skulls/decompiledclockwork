using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Web.UI;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x02000A07 RID: 2567
	internal class RadBarcodeEAN8 : MultiSectionBarcodeBase
	{
		// Token: 0x06006153 RID: 24915 RVA: 0x0016EA10 File Offset: 0x0016CC10
		public override void RenderContentsRectangles(HtmlTextWriter writer)
		{
			EAN8 ean = new EAN8();
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
			base.LeftText = ean.LeftTextboxText;
			base.RightText = (base.ShowChecksum ? ean.RightTextboxText : ean.RightTextboxText.Substring(0, ean.RightTextboxText.Length - 1));
			string text2 = text.Substring(0, text.Length / 2);
			string text3 = text.Substring(text.Length / 2);
			float num2 = (float)(EAN8.Prefix.Length + EAN8.Suffix.Length + EAN8.Center.Length + text2.Length + text3.Length);
			float num3 = 0f;
			List<RectangleF> geometry = ean.GenerateGeometry(EAN8.Prefix);
			List<RectangleF> geometry2 = ean.GenerateGeometry(EAN8.Suffix);
			List<RectangleF> geometry3 = ean.GenerateGeometry(EAN8.Center);
			List<RectangleF> geometry4 = ean.GenerateGeometry(text2);
			List<RectangleF> geometry5 = ean.GenerateGeometry(text3);
			float start = num3;
			num3 += (float)EAN8.Prefix.Length / num2;
			this.RenderRectangles(geometry, writer, start, num3, 1f);
			start = num3;
			num3 += (float)text2.Length / num2;
			this.RenderRectangles(geometry4, writer, start, num3, base.ShortLinesLengthPercentage / 100f);
			start = num3;
			num3 += (float)EAN8.Center.Length / num2;
			this.RenderRectangles(geometry3, writer, start, num3, 1f);
			start = num3;
			num3 += (float)text3.Length / num2;
			this.RenderRectangles(geometry5, writer, start, num3, base.ShortLinesLengthPercentage / 100f);
			start = num3;
			num3 += (float)EAN8.Suffix.Length / num2;
			this.RenderRectangles(geometry2, writer, start, num3, 1f);
			if (base.ShowText)
			{
				writer.Write(string.Format("<text x=\"25%\" y=\"" + base.VerticalTextPositionPercentage + "%\" text-anchor=\"middle\" >{0}</text>", base.LeftText));
				writer.Write(string.Format("<text x=\"75%\" y=\"" + base.VerticalTextPositionPercentage + "%\" text-anchor=\"middle\" >{0}</text>", base.RightText));
			}
		}

		// Token: 0x06006154 RID: 24916 RVA: 0x0016EC6C File Offset: 0x0016CE6C
		private void RenderRectangles(List<RectangleF> geometry, HtmlTextWriter writer, float start, float end, float height)
		{
			foreach (RectangleF rectangleF in geometry)
			{
				writer.Write(string.Format(CultureInfo.InvariantCulture, "<rect x='{0}%' y='{1}%' width='{2}%' height='{3}%' style='fill:rgb(0,0,0)'></rect>", new object[]
				{
					rectangleF.X * (end - start) * 100f + start * 100f,
					100f * rectangleF.Y,
					100f * (end - start) * rectangleF.Width,
					100f * rectangleF.Height * height
				}));
			}
		}
	}
}
