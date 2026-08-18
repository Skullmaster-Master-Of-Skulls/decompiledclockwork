using System;
using System.Collections.Generic;
using iTextSharp.text.pdf.codec;
using iTextSharp.text.pdf.qrcode;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000462 RID: 1122
	public class BarcodeQRCode
	{
		// Token: 0x0600262C RID: 9772 RVA: 0x000E65F0 File Offset: 0x000E55F0
		public BarcodeQRCode(string content, int width, int height, IDictionary<EncodeHintType, object> hints)
		{
			QRCodeWriter qrcodeWriter = new QRCodeWriter();
			this.bm = qrcodeWriter.Encode(content, width, height, hints);
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x000E661C File Offset: 0x000E561C
		private byte[] GetBitMatrix()
		{
			int width = this.bm.GetWidth();
			int height = this.bm.GetHeight();
			int num = (width + 7) / 8;
			byte[] array = new byte[num * height];
			sbyte[][] array2 = this.bm.GetArray();
			for (int i = 0; i < height; i++)
			{
				sbyte[] array3 = array2[i];
				for (int j = 0; j < width; j++)
				{
					if (array3[j] != 0)
					{
						int num2 = num * i + j / 8;
						byte[] array4 = array;
						int num3 = num2;
						array4[num3] |= (byte)(128 >> j % 8);
					}
				}
			}
			return array;
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x000E66BC File Offset: 0x000E56BC
		public Image GetImage()
		{
			byte[] bitMatrix = this.GetBitMatrix();
			byte[] data = CCITTG4Encoder.Compress(bitMatrix, this.bm.GetWidth(), this.bm.GetHeight());
			return Image.GetInstance(this.bm.GetWidth(), this.bm.GetHeight(), false, 256, 1, data, null);
		}

		// Token: 0x04001A7B RID: 6779
		private ByteMatrix bm;
	}
}
