using System;
using System.IO;
using System.util.zlib;

namespace iTextSharp.text.pdf
{
	// Token: 0x020000CD RID: 205
	public class PdfEFStream : PdfStream
	{
		// Token: 0x06000720 RID: 1824 RVA: 0x00025AEE File Offset: 0x00024AEE
		public PdfEFStream(Stream inp, PdfWriter writer) : base(inp, writer)
		{
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00025AF8 File Offset: 0x00024AF8
		public PdfEFStream(byte[] fileStore) : base(fileStore)
		{
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00025B04 File Offset: 0x00024B04
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
						if (!pdfArray.IsEmpty() && PdfName.CRYPT.Equals(pdfArray[0]))
						{
							pdfEncryption = null;
						}
					}
				}
			}
			if (pdfEncryption != null && pdfEncryption.IsEmbeddedFilesOnly())
			{
				PdfArray pdfArray2 = new PdfArray();
				PdfArray pdfArray3 = new PdfArray();
				PdfDictionary pdfDictionary = new PdfDictionary();
				pdfDictionary.Put(PdfName.NAME, PdfName.STDCF);
				pdfArray2.Add(PdfName.CRYPT);
				pdfArray3.Add(pdfDictionary);
				if (this.compressed)
				{
					pdfArray2.Add(PdfName.FLATEDECODE);
					pdfArray3.Add(new PdfNull());
				}
				base.Put(PdfName.FILTER, pdfArray2);
				base.Put(PdfName.DECODEPARMS, pdfArray3);
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
				if (pdfEncryption != null)
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
			else if (pdfEncryption == null)
			{
				if (this.streamBytes != null)
				{
					this.streamBytes.WriteTo(os);
				}
				else
				{
					os.Write(this.bytes, 0, this.bytes.Length);
				}
			}
			else
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
			os.Write(PdfStream.ENDSTREAM, 0, PdfStream.ENDSTREAM.Length);
		}
	}
}
