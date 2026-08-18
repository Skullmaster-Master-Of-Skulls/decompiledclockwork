using System;
using System.IO;
using System.util.zlib;

namespace iTextSharp.text.pdf
{
	// Token: 0x020005CE RID: 1486
	public class PRStream : PdfStream
	{
		// Token: 0x06003328 RID: 13096 RVA: 0x0013E358 File Offset: 0x0013D358
		public PRStream(PRStream stream, PdfDictionary newDic)
		{
			this.reader = stream.reader;
			this.offset = stream.offset;
			this.length = stream.Length;
			this.compressed = stream.compressed;
			this.compressionLevel = stream.compressionLevel;
			this.streamBytes = stream.streamBytes;
			this.bytes = stream.bytes;
			this.objNum = stream.objNum;
			this.objGen = stream.objGen;
			if (newDic != null)
			{
				base.Merge(newDic);
				return;
			}
			base.Merge(stream);
		}

		// Token: 0x06003329 RID: 13097 RVA: 0x0013E3E9 File Offset: 0x0013D3E9
		public PRStream(PRStream stream, PdfDictionary newDic, PdfReader reader) : this(stream, newDic)
		{
			this.reader = reader;
		}

		// Token: 0x0600332A RID: 13098 RVA: 0x0013E3FA File Offset: 0x0013D3FA
		public PRStream(PdfReader reader, int offset)
		{
			this.reader = reader;
			this.offset = offset;
		}

		// Token: 0x0600332B RID: 13099 RVA: 0x0013E410 File Offset: 0x0013D410
		public PRStream(PdfReader reader, byte[] conts) : this(reader, conts, -1)
		{
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x0013E41C File Offset: 0x0013D41C
		public PRStream(PdfReader reader, byte[] conts, int compressionLevel)
		{
			this.reader = reader;
			this.offset = -1;
			if (Document.Compress)
			{
				MemoryStream memoryStream = new MemoryStream();
				ZDeflaterOutputStream zdeflaterOutputStream = new ZDeflaterOutputStream(memoryStream, compressionLevel);
				zdeflaterOutputStream.Write(conts, 0, conts.Length);
				zdeflaterOutputStream.Close();
				this.bytes = memoryStream.ToArray();
				base.Put(PdfName.FILTER, PdfName.FLATEDECODE);
			}
			else
			{
				this.bytes = conts;
			}
			this.Length = this.bytes.Length;
		}

		// Token: 0x0600332D RID: 13101 RVA: 0x0013E496 File Offset: 0x0013D496
		public void SetData(byte[] data, bool compress)
		{
			this.SetData(data, compress, -1);
		}

		// Token: 0x0600332E RID: 13102 RVA: 0x0013E4A4 File Offset: 0x0013D4A4
		public void SetData(byte[] data, bool compress, int compressionLevel)
		{
			base.Remove(PdfName.FILTER);
			this.offset = -1;
			if (Document.Compress && compress)
			{
				MemoryStream memoryStream = new MemoryStream();
				ZDeflaterOutputStream zdeflaterOutputStream = new ZDeflaterOutputStream(memoryStream, compressionLevel);
				zdeflaterOutputStream.Write(data, 0, data.Length);
				zdeflaterOutputStream.Close();
				this.bytes = memoryStream.ToArray();
				this.compressionLevel = compressionLevel;
				base.Put(PdfName.FILTER, PdfName.FLATEDECODE);
			}
			else
			{
				this.bytes = data;
			}
			this.Length = this.bytes.Length;
		}

		// Token: 0x0600332F RID: 13103 RVA: 0x0013E526 File Offset: 0x0013D526
		public void SetData(byte[] data)
		{
			this.SetData(data, true);
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06003331 RID: 13105 RVA: 0x0013E54F File Offset: 0x0013D54F
		// (set) Token: 0x06003330 RID: 13104 RVA: 0x0013E530 File Offset: 0x0013D530
		public new int Length
		{
			get
			{
				return this.length;
			}
			set
			{
				this.length = value;
				base.Put(PdfName.LENGTH, new PdfNumber(this.length));
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06003332 RID: 13106 RVA: 0x0013E557 File Offset: 0x0013D557
		public int Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06003333 RID: 13107 RVA: 0x0013E55F File Offset: 0x0013D55F
		public PdfReader Reader
		{
			get
			{
				return this.reader;
			}
		}

		// Token: 0x06003334 RID: 13108 RVA: 0x0013E567 File Offset: 0x0013D567
		public new byte[] GetBytes()
		{
			return this.bytes;
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06003335 RID: 13109 RVA: 0x0013E56F File Offset: 0x0013D56F
		// (set) Token: 0x06003336 RID: 13110 RVA: 0x0013E577 File Offset: 0x0013D577
		public int ObjNum
		{
			get
			{
				return this.objNum;
			}
			set
			{
				this.objNum = value;
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06003337 RID: 13111 RVA: 0x0013E580 File Offset: 0x0013D580
		// (set) Token: 0x06003338 RID: 13112 RVA: 0x0013E588 File Offset: 0x0013D588
		public int ObjGen
		{
			get
			{
				return this.objGen;
			}
			set
			{
				this.objGen = value;
			}
		}

		// Token: 0x06003339 RID: 13113 RVA: 0x0013E594 File Offset: 0x0013D594
		public override void ToPdf(PdfWriter writer, Stream os)
		{
			byte[] array = PdfReader.GetStreamBytesRaw(this);
			PdfEncryption pdfEncryption = null;
			if (writer != null)
			{
				pdfEncryption = writer.Encryption;
			}
			PdfObject value = base.Get(PdfName.LENGTH);
			int num = array.Length;
			if (pdfEncryption != null)
			{
				num = pdfEncryption.CalculateStreamSize(num);
			}
			base.Put(PdfName.LENGTH, new PdfNumber(num));
			this.SuperToPdf(writer, os);
			base.Put(PdfName.LENGTH, value);
			os.Write(PdfStream.STARTSTREAM, 0, PdfStream.STARTSTREAM.Length);
			if (this.length > 0)
			{
				if (pdfEncryption != null && !pdfEncryption.IsEmbeddedFilesOnly())
				{
					array = pdfEncryption.EncryptByteArray(array);
				}
				os.Write(array, 0, array.Length);
			}
			os.Write(PdfStream.ENDSTREAM, 0, PdfStream.ENDSTREAM.Length);
		}

		// Token: 0x040022C3 RID: 8899
		protected PdfReader reader;

		// Token: 0x040022C4 RID: 8900
		protected int offset;

		// Token: 0x040022C5 RID: 8901
		protected int length;

		// Token: 0x040022C6 RID: 8902
		protected int objNum;

		// Token: 0x040022C7 RID: 8903
		protected int objGen;
	}
}
