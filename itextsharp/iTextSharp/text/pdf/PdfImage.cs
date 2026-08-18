using System;
using System.IO;
using System.Net;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x020002CE RID: 718
	public class PdfImage : PdfStream
	{
		// Token: 0x06001ADD RID: 6877 RVA: 0x0009E3B0 File Offset: 0x0009D3B0
		public PdfImage(Image image, string name, PdfIndirectReference maskRef)
		{
			if (name == null)
			{
				this.GenerateImgResName(image);
			}
			else
			{
				this.name = new PdfName(name);
			}
			base.Put(PdfName.TYPE, PdfName.XOBJECT);
			base.Put(PdfName.SUBTYPE, PdfName.IMAGE);
			base.Put(PdfName.WIDTH, new PdfNumber(image.Width));
			base.Put(PdfName.HEIGHT, new PdfNumber(image.Height));
			if (image.Layer != null)
			{
				base.Put(PdfName.OC, image.Layer.Ref);
			}
			if (image.IsMask() && (image.Bpc == 1 || image.Bpc > 255))
			{
				base.Put(PdfName.IMAGEMASK, PdfBoolean.PDFTRUE);
			}
			if (maskRef != null)
			{
				if (image.Smask)
				{
					base.Put(PdfName.SMASK, maskRef);
				}
				else
				{
					base.Put(PdfName.MASK, maskRef);
				}
			}
			if (image.IsMask() && image.Inverted)
			{
				base.Put(PdfName.DECODE, new PdfLiteral("[1 0]"));
			}
			if (image.Interpolation)
			{
				base.Put(PdfName.INTERPOLATE, PdfBoolean.PDFTRUE);
			}
			Stream stream = null;
			try
			{
				if (image.IsImgRaw())
				{
					int colorspace = image.Colorspace;
					int[] transparency = image.Transparency;
					if (transparency != null && !image.IsMask() && maskRef == null)
					{
						string text = "[";
						for (int i = 0; i < transparency.Length; i++)
						{
							text = text + transparency[i] + " ";
						}
						text += "]";
						base.Put(PdfName.MASK, new PdfLiteral(text));
					}
					this.bytes = image.RawData;
					base.Put(PdfName.LENGTH, new PdfNumber(this.bytes.Length));
					int bpc = image.Bpc;
					if (bpc > 255)
					{
						if (!image.IsMask())
						{
							base.Put(PdfName.COLORSPACE, PdfName.DEVICEGRAY);
						}
						base.Put(PdfName.BITSPERCOMPONENT, new PdfNumber(1));
						base.Put(PdfName.FILTER, PdfName.CCITTFAXDECODE);
						int num = bpc - 257;
						PdfDictionary pdfDictionary = new PdfDictionary();
						if (num != 0)
						{
							pdfDictionary.Put(PdfName.K, new PdfNumber(num));
						}
						if ((colorspace & 1) != 0)
						{
							pdfDictionary.Put(PdfName.BLACKIS1, PdfBoolean.PDFTRUE);
						}
						if ((colorspace & 2) != 0)
						{
							pdfDictionary.Put(PdfName.ENCODEDBYTEALIGN, PdfBoolean.PDFTRUE);
						}
						if ((colorspace & 4) != 0)
						{
							pdfDictionary.Put(PdfName.ENDOFLINE, PdfBoolean.PDFTRUE);
						}
						if ((colorspace & 8) != 0)
						{
							pdfDictionary.Put(PdfName.ENDOFBLOCK, PdfBoolean.PDFFALSE);
						}
						pdfDictionary.Put(PdfName.COLUMNS, new PdfNumber(image.Width));
						pdfDictionary.Put(PdfName.ROWS, new PdfNumber(image.Height));
						base.Put(PdfName.DECODEPARMS, pdfDictionary);
					}
					else
					{
						switch (colorspace)
						{
						case 1:
							base.Put(PdfName.COLORSPACE, PdfName.DEVICEGRAY);
							if (image.Inverted)
							{
								base.Put(PdfName.DECODE, new PdfLiteral("[1 0]"));
								goto IL_365;
							}
							goto IL_365;
						case 3:
							base.Put(PdfName.COLORSPACE, PdfName.DEVICERGB);
							if (image.Inverted)
							{
								base.Put(PdfName.DECODE, new PdfLiteral("[1 0 1 0 1 0]"));
								goto IL_365;
							}
							goto IL_365;
						}
						base.Put(PdfName.COLORSPACE, PdfName.DEVICECMYK);
						if (image.Inverted)
						{
							base.Put(PdfName.DECODE, new PdfLiteral("[1 0 1 0 1 0 1 0]"));
						}
						IL_365:
						PdfDictionary additional = image.Additional;
						if (additional != null)
						{
							base.Merge(additional);
						}
						if (image.IsMask() && (image.Bpc == 1 || image.Bpc > 8))
						{
							base.Remove(PdfName.COLORSPACE);
						}
						base.Put(PdfName.BITSPERCOMPONENT, new PdfNumber(image.Bpc));
						if (image.Deflated)
						{
							base.Put(PdfName.FILTER, PdfName.FLATEDECODE);
						}
						else
						{
							base.FlateCompress(image.CompressionLevel);
						}
					}
				}
				else
				{
					string p;
					if (image.RawData == null)
					{
						stream = WebRequest.Create(image.Url).GetResponse().GetResponseStream();
						p = image.Url.ToString();
					}
					else
					{
						stream = new MemoryStream(image.RawData);
						p = "Byte array";
					}
					switch (image.Type)
					{
					case 32:
						base.Put(PdfName.FILTER, PdfName.DCTDECODE);
						switch (image.Colorspace)
						{
						case 1:
							base.Put(PdfName.COLORSPACE, PdfName.DEVICEGRAY);
							goto IL_4CA;
						case 3:
							base.Put(PdfName.COLORSPACE, PdfName.DEVICERGB);
							goto IL_4CA;
						}
						base.Put(PdfName.COLORSPACE, PdfName.DEVICECMYK);
						if (image.Inverted)
						{
							base.Put(PdfName.DECODE, new PdfLiteral("[1 0 1 0 1 0 1 0]"));
						}
						IL_4CA:
						base.Put(PdfName.BITSPERCOMPONENT, new PdfNumber(8));
						if (image.RawData != null)
						{
							this.bytes = image.RawData;
							base.Put(PdfName.LENGTH, new PdfNumber(this.bytes.Length));
							return;
						}
						this.streamBytes = new MemoryStream();
						PdfImage.TransferBytes(stream, this.streamBytes, -1);
						goto IL_684;
					case 33:
						base.Put(PdfName.FILTER, PdfName.JPXDECODE);
						if (image.Colorspace > 0)
						{
							switch (image.Colorspace)
							{
							case 1:
								base.Put(PdfName.COLORSPACE, PdfName.DEVICEGRAY);
								goto IL_595;
							case 3:
								base.Put(PdfName.COLORSPACE, PdfName.DEVICERGB);
								goto IL_595;
							}
							base.Put(PdfName.COLORSPACE, PdfName.DEVICECMYK);
							IL_595:
							base.Put(PdfName.BITSPERCOMPONENT, new PdfNumber(image.Bpc));
						}
						if (image.RawData != null)
						{
							this.bytes = image.RawData;
							base.Put(PdfName.LENGTH, new PdfNumber(this.bytes.Length));
							return;
						}
						this.streamBytes = new MemoryStream();
						PdfImage.TransferBytes(stream, this.streamBytes, -1);
						goto IL_684;
					case 36:
						base.Put(PdfName.FILTER, PdfName.JBIG2DECODE);
						base.Put(PdfName.COLORSPACE, PdfName.DEVICEGRAY);
						base.Put(PdfName.BITSPERCOMPONENT, new PdfNumber(1));
						if (image.RawData != null)
						{
							this.bytes = image.RawData;
							base.Put(PdfName.LENGTH, new PdfNumber(this.bytes.Length));
							return;
						}
						this.streamBytes = new MemoryStream();
						PdfImage.TransferBytes(stream, this.streamBytes, -1);
						goto IL_684;
					}
					throw new IOException(MessageLocalization.GetComposedMessage("1.is.an.unknown.image.format", p));
					IL_684:
					base.Put(PdfName.LENGTH, new PdfNumber((float)this.streamBytes.Length));
				}
			}
			finally
			{
				if (stream != null)
				{
					try
					{
						stream.Close();
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001ADE RID: 6878 RVA: 0x0009EAA4 File Offset: 0x0009DAA4
		public PdfName Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x0009EAAC File Offset: 0x0009DAAC
		internal static void TransferBytes(Stream inp, Stream outp, int len)
		{
			byte[] buffer = new byte[4096];
			if (len < 0)
			{
				len = 2147418112;
			}
			while (len != 0)
			{
				int num = inp.Read(buffer, 0, Math.Min(len, 4096));
				if (num <= 0)
				{
					return;
				}
				outp.Write(buffer, 0, num);
				len -= num;
			}
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x0009EAFC File Offset: 0x0009DAFC
		protected void ImportAll(PdfImage dup)
		{
			this.name = dup.name;
			this.compressed = dup.compressed;
			this.compressionLevel = dup.compressionLevel;
			this.streamBytes = dup.streamBytes;
			this.bytes = dup.bytes;
			this.hashMap = dup.hashMap;
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x0009EB54 File Offset: 0x0009DB54
		private void GenerateImgResName(Image img)
		{
			this.name = new PdfName("img" + img.MySerialId.ToString("X"));
		}

		// Token: 0x040011E7 RID: 4583
		internal const int TRANSFERSIZE = 4096;

		// Token: 0x040011E8 RID: 4584
		protected PdfName name;
	}
}
