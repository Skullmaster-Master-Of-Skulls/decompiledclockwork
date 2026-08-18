using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Web.UI;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x02000A09 RID: 2569
	internal class RadBarcodeMSI : RadBarcodeBase
	{
		// Token: 0x06006183 RID: 24963 RVA: 0x0016FD0D File Offset: 0x0016DF0D
		public RadBarcodeMSI(CheckMSI checksumType)
		{
			this.ChecksumType = checksumType;
		}

		// Token: 0x06006184 RID: 24964 RVA: 0x0016FD1C File Offset: 0x0016DF1C
		public override void RenderContentsRectangles(HtmlTextWriter writer)
		{
			CodeMSI codeMSI = new CodeMSI
			{
				CalculateCheckSum = base.RenderChecksum,
				Algorithm = this.ChecksumType
			};
			string barCodeEncodedText = "";
			try
			{
				barCodeEncodedText = codeMSI.GetEncoding(this.Text);
			}
			catch (Exception)
			{
			}
			List<RectangleF> list = codeMSI.GenerateGeometry(barCodeEncodedText);
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
				string text = this.Text + (base.ShowChecksum ? codeMSI.CheckSum : string.Empty);
				writer.Write(string.Format(CultureInfo.InvariantCulture, "<text x=\"50%\" y=\"" + base.VerticalTextPositionPercentage + "%\" text-anchor=\"middle\" >{0}</text>", new object[]
				{
					text
				}));
			}
		}

		// Token: 0x040017C9 RID: 6089
		public readonly CheckMSI ChecksumType;
	}
}
