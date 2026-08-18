using System;
using System.Drawing;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x020002DB RID: 731
	public class BarcodeEANSUPP : Barcode
	{
		// Token: 0x06001B41 RID: 6977 RVA: 0x000A3A04 File Offset: 0x000A2A04
		public BarcodeEANSUPP(Barcode ean, Barcode supp)
		{
			this.n = 8f;
			this.ean = ean;
			this.supp = supp;
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001B42 RID: 6978 RVA: 0x000A3A28 File Offset: 0x000A2A28
		public override Rectangle BarcodeSize
		{
			get
			{
				Rectangle barcodeSize = this.ean.BarcodeSize;
				barcodeSize.Right = barcodeSize.Width + this.supp.BarcodeSize.Width + this.n;
				return barcodeSize;
			}
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x000A3A68 File Offset: 0x000A2A68
		public override Rectangle PlaceBarcode(PdfContentByte cb, BaseColor barColor, BaseColor textColor)
		{
			if (this.supp.Font != null)
			{
				this.supp.BarHeight = this.ean.BarHeight + this.supp.Baseline - this.supp.Font.GetFontDescriptor(2, this.supp.Size);
			}
			else
			{
				this.supp.BarHeight = this.ean.BarHeight;
			}
			Rectangle barcodeSize = this.ean.BarcodeSize;
			cb.SaveState();
			this.ean.PlaceBarcode(cb, barColor, textColor);
			cb.RestoreState();
			cb.SaveState();
			cb.ConcatCTM(1f, 0f, 0f, 1f, barcodeSize.Width + this.n, barcodeSize.Height - this.ean.BarHeight);
			this.supp.PlaceBarcode(cb, barColor, textColor);
			cb.RestoreState();
			return this.BarcodeSize;
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x000A3B5A File Offset: 0x000A2B5A
		public override Image CreateDrawingImage(Color foreground, Color background)
		{
			throw new InvalidOperationException(MessageLocalization.GetComposedMessage("the.two.barcodes.must.be.composed.externally"));
		}

		// Token: 0x040012AD RID: 4781
		protected Barcode ean;

		// Token: 0x040012AE RID: 4782
		protected Barcode supp;
	}
}
