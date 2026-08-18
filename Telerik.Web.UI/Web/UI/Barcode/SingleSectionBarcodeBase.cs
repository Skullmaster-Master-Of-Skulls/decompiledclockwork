using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Web.UI;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x02000A02 RID: 2562
	internal abstract class SingleSectionBarcodeBase : RadBarcodeBase
	{
		// Token: 0x17001FE4 RID: 8164
		// (get) Token: 0x0600613D RID: 24893 RVA: 0x0016DD64 File Offset: 0x0016BF64
		// (set) Token: 0x0600613E RID: 24894 RVA: 0x0016DD6C File Offset: 0x0016BF6C
		public Symbology1D Code { get; set; }

		// Token: 0x0600613F RID: 24895 RVA: 0x0016DD78 File Offset: 0x0016BF78
		public override void RenderContentsRectangles(HtmlTextWriter writer)
		{
			this.Code.CalculateCheckSum = base.RenderChecksum;
			string barCodeEncodedText = "";
			try
			{
				barCodeEncodedText = this.Code.GetEncoding(this.Text);
			}
			catch (Exception)
			{
			}
			List<RectangleF> list = this.Code.GenerateGeometry(barCodeEncodedText);
			foreach (RectangleF rectangleF in list)
			{
				float num = base.ShowText ? base.ShortLinesLengthPercentage : 100f;
				writer.Write(string.Format(CultureInfo.InvariantCulture, "<rect x='{0}%' y='{1}%' width='{2}%' height='{3}%' style='fill:rgb(0,0,0)'></rect>", new object[]
				{
					100f * rectangleF.X,
					num * rectangleF.Y,
					100f * rectangleF.Width,
					num * rectangleF.Height
				}));
			}
			if (base.ShowText)
			{
				string arg = this.Text + (base.ShowChecksum ? this.Code.CheckSum : string.Empty);
				writer.Write(string.Format("<text x=\"50%\" y=\"" + base.VerticalTextPositionPercentage + "%\" text-anchor=\"middle\" >{0}</text>", arg));
			}
		}
	}
}
