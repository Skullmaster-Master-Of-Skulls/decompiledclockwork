using System;
using System.IO;
using System.util.zlib;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200004E RID: 78
	public class PdfStream : PdfDictionary
	{
		// Token: 0x06000229 RID: 553 RVA: 0x0000AC3E File Offset: 0x00009C3E
		public PdfStream(byte[] bytes)
		{
			this.type = 7;
			this.bytes = bytes;
			this.rawLength = bytes.Length;
			base.Put(PdfName.LENGTH, new PdfNumber(bytes.Length));
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000AC78 File Offset: 0x00009C78
		public PdfStream(Stream inputStream, PdfWriter writer)
		{
			this.type = 7;
			this.inputStream = inputStream;
			this.writer = writer;
			this.iref = writer.PdfIndirectReference;
			base.Put(PdfName.LENGTH, this.iref);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000ACC4 File Offset: 0x00009CC4
		protected PdfStream()
		{
			this.type = 7;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000ACDC File Offset: 0x00009CDC
		public void WriteLength()
		{
			if (this.inputStream == null)
			{
				throw new PdfException(MessageLocalization.GetComposedMessage("writelength.can.only.be.called.in.a.contructed.pdfstream.inputstream.pdfwriter"));
			}
			if (this.inputStreamLength == -1)
			{
				throw new PdfException(MessageLocalization.GetComposedMessage("writelength.can.only.be.called.after.output.of.the.stream.body"));
			}
			this.writer.AddToBody(new PdfNumber(this.inputStreamLength), this.iref, false);
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000AD38 File Offset: 0x00009D38
		public int RawLength
		{
			get
			{
				return this.rawLength;
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000AD40 File Offset: 0x00009D40
		public void FlateCompress()
		{
			this.FlateCompress(-1);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000AD4C File Offset: 0x00009D4C
		public void FlateCompress(int compressionLevel)
		{
			if (!Document.Compress)
			{
				return;
			}
			if (this.compressed)
			{
				return;
			}
			this.compressionLevel = compressionLevel;
			if (this.inputStream != null)
			{
				this.compressed = true;
				return;
			}
			PdfObject pdfObject = PdfReader.GetPdfObject(base.Get(PdfName.FILTER));
			if (pdfObject != null)
			{
				if (pdfObject.IsName())
				{
					if (PdfName.FLATEDECODE.Equals(pdfObject))
					{
						return;
					}
				}
				else
				{
					if (!pdfObject.IsArray())
					{
						throw new PdfException(MessageLocalization.GetComposedMessage("stream.could.not.be.compressed.filter.is.not.a.name.or.array"));
					}
					if (((PdfArray)pdfObject).Contains(PdfName.FLATEDECODE))
					{
						return;
					}
				}
			}
			MemoryStream outp = new MemoryStream();
			ZDeflaterOutputStream zdeflaterOutputStream = new ZDeflaterOutputStream(outp, compressionLevel);
			if (this.streamBytes != null)
			{
				this.streamBytes.WriteTo(zdeflaterOutputStream);
			}
			else
			{
				zdeflaterOutputStream.Write(this.bytes, 0, this.bytes.Length);
			}
			zdeflaterOutputStream.Finish();
			this.streamBytes = outp;
			this.bytes = null;
			base.Put(PdfName.LENGTH, new PdfNumber((float)this.streamBytes.Length));
			if (pdfObject == null)
			{
				base.Put(PdfName.FILTER, PdfName.FLATEDECODE);
			}
			else
			{
				PdfArray pdfArray = new PdfArray(pdfObject);
				pdfArray.Add(PdfName.FLATEDECODE);
				base.Put(PdfName.FILTER, pdfArray);
			}
			this.compressed = true;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000AE7A File Offset: 0x00009E7A
		protected virtual void SuperToPdf(PdfWriter writer, Stream os)
		{
			base.ToPdf(writer, os);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000AE84 File Offset: 0x00009E84
		public override void ToPdf(PdfWriter writer, Stream os)
		{
			if (this.inputStream != null && this.compressed)
			{
				base.Put(PdfName.FILTER, PdfName.FLATEDECODE);
			}
			PdfEncryption pdfEncryption = null;
			if (writer != null)
			{
				pdfEncryption = writer.Encryption;
			}
			if (pdfEncryption != null)
			{
				PdfObject pdfObject = base.Get(PdfName.FILTER);
				if (pdfObject != null)
				{
					if (PdfName.CRYPT.Equals(pdfObject))
					{
						pdfEncryption = null;
					}
					else if (pdfObject.IsArray())
					{
						PdfArray pdfArray = (PdfArray)pdfObject;
						if (pdfArray.Size > 0 && PdfName.CRYPT.Equals(pdfArray[0]))
						{
							pdfEncryption = null;
						}
					}
				}
			}
			PdfObject pdfObject2 = base.Get(PdfName.LENGTH);
			if (pdfEncryption != null && pdfObject2 != null && pdfObject2.IsNumber())
			{
				int intValue = ((PdfNumber)pdfObject2).IntValue;
				base.Put(PdfName.LENGTH, new PdfNumber(pdfEncryption.CalculateStreamSize(intValue)));
				this.SuperToPdf(writer, os);
				base.Put(PdfName.LENGTH, pdfObject2);
			}
			else
			{
				this.SuperToPdf(writer, os);
			}
			os.Write(PdfStream.STARTSTREAM, 0, PdfStream.STARTSTREAM.Length);
			if (this.inputStream != null)
			{
				this.rawLength = 0;
				ZDeflaterOutputStream zdeflaterOutputStream = null;
				OutputStreamCounter outputStreamCounter = new OutputStreamCounter(os);
				OutputStreamEncryption outputStreamEncryption = null;
				Stream stream = outputStreamCounter;
				if (pdfEncryption != null && !pdfEncryption.IsEmbeddedFilesOnly())
				{
					outputStreamEncryption = (stream = pdfEncryption.GetEncryptionStream(stream));
				}
				if (this.compressed)
				{
					zdeflaterOutputStream = (stream = new ZDeflaterOutputStream(stream, this.compressionLevel));
				}
				byte[] array = new byte[4192];
				for (;;)
				{
					int num = this.inputStream.Read(array, 0, array.Length);
					if (num <= 0)
					{
						break;
					}
					stream.Write(array, 0, num);
					this.rawLength += num;
				}
				if (zdeflaterOutputStream != null)
				{
					zdeflaterOutputStream.Finish();
				}
				if (outputStreamEncryption != null)
				{
					outputStreamEncryption.Finish();
				}
				this.inputStreamLength = outputStreamCounter.Counter;
			}
			else if (pdfEncryption != null && !pdfEncryption.IsEmbeddedFilesOnly())
			{
				byte[] array2;
				if (this.streamBytes != null)
				{
					array2 = pdfEncryption.EncryptByteArray(this.streamBytes.ToArray());
				}
				else
				{
					array2 = pdfEncryption.EncryptByteArray(this.bytes);
				}
				os.Write(array2, 0, array2.Length);
			}
			else if (this.streamBytes != null)
			{
				this.streamBytes.WriteTo(os);
			}
			else
			{
				os.Write(this.bytes, 0, this.bytes.Length);
			}
			os.Write(PdfStream.ENDSTREAM, 0, PdfStream.ENDSTREAM.Length);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000B0BD File Offset: 0x0000A0BD
		public void WriteContent(Stream os)
		{
			if (this.streamBytes != null)
			{
				this.streamBytes.WriteTo(os);
				return;
			}
			if (this.bytes != null)
			{
				os.Write(this.bytes, 0, this.bytes.Length);
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000B0F1 File Offset: 0x0000A0F1
		public override string ToString()
		{
			if (base.Get(PdfName.TYPE) == null)
			{
				return "Stream";
			}
			return "Stream of type: " + base.Get(PdfName.TYPE);
		}

		// Token: 0x040000F2 RID: 242
		public const int DEFAULT_COMPRESSION = -1;

		// Token: 0x040000F3 RID: 243
		public const int NO_COMPRESSION = 0;

		// Token: 0x040000F4 RID: 244
		public const int BEST_SPEED = 1;

		// Token: 0x040000F5 RID: 245
		public const int BEST_COMPRESSION = 9;

		// Token: 0x040000F6 RID: 246
		protected bool compressed;

		// Token: 0x040000F7 RID: 247
		protected int compressionLevel;

		// Token: 0x040000F8 RID: 248
		protected MemoryStream streamBytes;

		// Token: 0x040000F9 RID: 249
		protected Stream inputStream;

		// Token: 0x040000FA RID: 250
		protected PdfIndirectReference iref;

		// Token: 0x040000FB RID: 251
		protected int inputStreamLength = -1;

		// Token: 0x040000FC RID: 252
		protected PdfWriter writer;

		// Token: 0x040000FD RID: 253
		protected int rawLength;

		// Token: 0x040000FE RID: 254
		internal static byte[] STARTSTREAM = DocWriter.GetISOBytes("stream\n");

		// Token: 0x040000FF RID: 255
		internal static byte[] ENDSTREAM = DocWriter.GetISOBytes("\nendstream");

		// Token: 0x04000100 RID: 256
		internal static int SIZESTREAM = PdfStream.STARTSTREAM.Length + PdfStream.ENDSTREAM.Length;
	}
}
