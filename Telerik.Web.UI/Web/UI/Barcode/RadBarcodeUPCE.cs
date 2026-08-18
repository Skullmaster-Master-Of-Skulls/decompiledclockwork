using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Web.UI;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x02000A01 RID: 2561
	internal class RadBarcodeUPCE : MultiSectionBarcodeBase
	{
		// Token: 0x17001FE2 RID: 8162
		// (get) Token: 0x06006136 RID: 24886 RVA: 0x0016DA53 File Offset: 0x0016BC53
		// (set) Token: 0x06006137 RID: 24887 RVA: 0x0016DA5B File Offset: 0x0016BC5B
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Description("Gets or sets the LeadingText")]
		[Browsable(false)]
		public string LeadingText { get; set; }

		// Token: 0x17001FE3 RID: 8163
		// (get) Token: 0x06006138 RID: 24888 RVA: 0x0016DA64 File Offset: 0x0016BC64
		// (set) Token: 0x06006139 RID: 24889 RVA: 0x0016DA6C File Offset: 0x0016BC6C
		[Browsable(false)]
		[Description("Gets or sets the TrailingText")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string TrailingText { get; set; }

		// Token: 0x0600613A RID: 24890 RVA: 0x0016DA78 File Offset: 0x0016BC78
		public override void RenderContentsRectangles(HtmlTextWriter writer)
		{
			UPCE upce = new UPCE();
			string text = "";
			try
			{
				text = upce.GetEncoding(this.Text);
			}
			catch (Exception)
			{
			}
			int num = 7;
			if (!base.RenderChecksum)
			{
				text = text.Substring(0, text.Length - num);
			}
			float num2 = (float)(UPCE.Prefix.Length + UPCE.Suffix.Length + text.Length);
			float num3 = 0f;
			List<RectangleF> geometry = upce.GenerateGeometry(UPCE.Prefix);
			List<RectangleF> geometry2 = upce.GenerateGeometry(UPCE.Suffix);
			List<RectangleF> geometry3 = upce.GenerateGeometry(text);
			float widthPercent = base.ShowText ? base.ShortLinesLengthPercentage : 100f;
			float start = num3;
			num3 += (float)EAN13.Prefix.Length / num2;
			this.RenderRectangles(geometry, writer, start, num3, 1f, widthPercent);
			start = num3;
			num3 += (float)text.Length / num2;
			this.RenderRectangles(geometry3, writer, start, num3, base.ShortLinesLengthPercentage / 100f, widthPercent);
			start = num3;
			num3 += (float)UPCE.Suffix.Length / num2;
			this.RenderRectangles(geometry2, writer, start, num3, 1f, widthPercent);
			if (base.ShowText)
			{
				this.LeadingText = upce.LeadingTextboxText;
				string leftTextboxText = upce.LeftTextboxText;
				this.TrailingText = (base.ShowChecksum ? upce.EndTextboxText : upce.EndTextboxText.Substring(0, upce.EndTextboxText.Length - 1));
				writer.Write(string.Format("<text x=\"50%\" y=\"" + base.VerticalTextPositionPercentage + "%\" text-anchor=\"middle\" >{0}</text>", leftTextboxText));
				writer.Write(string.Format("<text x=\"0%\" y=\"" + base.VerticalTextPositionPercentage + "%\" text-anchor=\"start\" >{0}</text>", this.LeadingText));
				writer.Write(string.Format("<text x=\"100%\" y=\"" + base.VerticalTextPositionPercentage + "%\" text-anchor=\"end\" >{0}</text>", this.TrailingText));
			}
		}

		// Token: 0x0600613B RID: 24891 RVA: 0x0016DC80 File Offset: 0x0016BE80
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
