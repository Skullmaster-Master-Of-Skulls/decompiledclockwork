using System;
using System.Drawing;
using System.IO;
using iTextSharp.text.pdf.codec;

namespace iTextSharp.text.pdf.parser
{
	// Token: 0x020000E1 RID: 225
	public class PdfImageObject
	{
		// Token: 0x06000844 RID: 2116 RVA: 0x0002B30B File Offset: 0x0002A30B
		public string GetFileType()
		{
			return this.fileType;
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0002B314 File Offset: 0x0002A314
		public PdfImageObject(PRStream stream)
		{
			this.dictionary = stream;
			try
			{
				this.streamBytes = PdfReader.GetStreamBytes(stream);
				this.decoded = true;
			}
			catch
			{
				try
				{
					this.streamBytes = PdfReader.GetStreamBytesRaw(stream);
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0002B37C File Offset: 0x0002A37C
		public PdfObject Get(PdfName key)
		{
			return this.dictionary.Get(key);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0002B38A File Offset: 0x0002A38A
		public PdfDictionary GetDictionary()
		{
			return this.dictionary;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0002B392 File Offset: 0x0002A392
		public byte[] GetStreamBytes()
		{
			return this.streamBytes;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0002B39C File Offset: 0x0002A39C
		private void FindColorspace(PdfObject colorspace, bool allowIndexed)
		{
			if (PdfName.DEVICEGRAY.Equals(colorspace))
			{
				this.stride = (this.width * this.bpc + 7) / 8;
				this.pngColorType = 0;
				return;
			}
			if (PdfName.DEVICERGB.Equals(colorspace))
			{
				if (this.bpc == 8 || this.bpc == 16)
				{
					this.stride = (this.width * this.bpc * 3 + 7) / 8;
					this.pngColorType = 2;
					return;
				}
			}
			else if (colorspace is PdfArray)
			{
				PdfArray pdfArray = (PdfArray)colorspace;
				PdfObject directObject = pdfArray.GetDirectObject(0);
				if (PdfName.CALGRAY.Equals(directObject))
				{
					this.stride = (this.width * this.bpc + 7) / 8;
					this.pngColorType = 0;
					return;
				}
				if (PdfName.CALRGB.Equals(directObject))
				{
					if (this.bpc == 8 || this.bpc == 16)
					{
						this.stride = (this.width * this.bpc * 3 + 7) / 8;
						this.pngColorType = 2;
						return;
					}
				}
				else if (PdfName.ICCBASED.Equals(directObject))
				{
					PRStream prstream = (PRStream)pdfArray.GetDirectObject(1);
					int intValue = prstream.GetAsNumber(PdfName.N).IntValue;
					if (intValue == 1)
					{
						this.stride = (this.width * this.bpc + 7) / 8;
						this.pngColorType = 0;
						this.icc = PdfReader.GetStreamBytes(prstream);
						return;
					}
					if (intValue == 3)
					{
						this.stride = (this.width * this.bpc * 3 + 7) / 8;
						this.pngColorType = 2;
						this.icc = PdfReader.GetStreamBytes(prstream);
						return;
					}
				}
				else if (allowIndexed && PdfName.INDEXED.Equals(directObject))
				{
					this.FindColorspace(pdfArray.GetDirectObject(1), false);
					if (this.pngColorType == 2)
					{
						PdfObject directObject2 = pdfArray.GetDirectObject(3);
						if (directObject2 is PdfString)
						{
							this.palette = ((PdfString)directObject2).GetBytes();
						}
						else if (directObject2 is PRStream)
						{
							this.palette = PdfReader.GetStreamBytes((PRStream)directObject2);
						}
						this.stride = (this.width * this.bpc + 7) / 8;
						this.pngColorType = 3;
					}
				}
			}
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0002B5C0 File Offset: 0x0002A5C0
		public byte[] GetImageAsBytes()
		{
			if (this.streamBytes == null)
			{
				return null;
			}
			if (!this.decoded)
			{
				PdfName asName = this.dictionary.GetAsName(PdfName.FILTER);
				if (PdfName.DCTDECODE.Equals(asName))
				{
					this.fileType = "jpg";
					return this.streamBytes;
				}
				if (PdfName.JPXDECODE.Equals(asName))
				{
					this.fileType = "jp2";
					return this.streamBytes;
				}
				return null;
			}
			else
			{
				this.pngColorType = -1;
				this.width = this.dictionary.GetAsNumber(PdfName.WIDTH).IntValue;
				this.height = this.dictionary.GetAsNumber(PdfName.HEIGHT).IntValue;
				this.bpc = this.dictionary.GetAsNumber(PdfName.BITSPERCOMPONENT).IntValue;
				this.pngBitDepth = this.bpc;
				PdfObject directObject = this.dictionary.GetDirectObject(PdfName.COLORSPACE);
				this.palette = null;
				this.icc = null;
				this.stride = 0;
				this.FindColorspace(directObject, true);
				if (this.pngColorType < 0)
				{
					return null;
				}
				MemoryStream memoryStream = new MemoryStream();
				PngWriter pngWriter = new PngWriter(memoryStream);
				pngWriter.WriteHeader(this.width, this.height, this.pngBitDepth, this.pngColorType);
				if (this.icc != null)
				{
					pngWriter.WriteIccProfile(this.icc);
				}
				if (this.palette != null)
				{
					pngWriter.WritePalette(this.palette);
				}
				pngWriter.WriteData(this.streamBytes, this.stride);
				pngWriter.WriteEnd();
				this.fileType = "png";
				return memoryStream.ToArray();
			}
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0002B74C File Offset: 0x0002A74C
		public Image GetDrawingImage()
		{
			byte[] imageAsBytes = this.GetImageAsBytes();
			if (imageAsBytes == null)
			{
				return null;
			}
			return Image.FromStream(new MemoryStream(imageAsBytes));
		}

		// Token: 0x040006DA RID: 1754
		public const string TYPE_PNG = "png";

		// Token: 0x040006DB RID: 1755
		public const string TYPE_JPG = "jpg";

		// Token: 0x040006DC RID: 1756
		public const string TYPE_JP2 = "jp2";

		// Token: 0x040006DD RID: 1757
		protected PdfDictionary dictionary;

		// Token: 0x040006DE RID: 1758
		protected byte[] streamBytes;

		// Token: 0x040006DF RID: 1759
		private int pngColorType = -1;

		// Token: 0x040006E0 RID: 1760
		private int pngBitDepth;

		// Token: 0x040006E1 RID: 1761
		private int width;

		// Token: 0x040006E2 RID: 1762
		private int height;

		// Token: 0x040006E3 RID: 1763
		private int bpc;

		// Token: 0x040006E4 RID: 1764
		private byte[] palette;

		// Token: 0x040006E5 RID: 1765
		private byte[] icc;

		// Token: 0x040006E6 RID: 1766
		private int stride;

		// Token: 0x040006E7 RID: 1767
		private bool decoded;

		// Token: 0x040006E8 RID: 1768
		protected string fileType;
	}
}
