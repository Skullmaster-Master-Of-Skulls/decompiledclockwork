using System;
using System.IO;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000213 RID: 531
	public class PdfIndirectObject
	{
		// Token: 0x060014B3 RID: 5299 RVA: 0x000754AF File Offset: 0x000744AF
		internal PdfIndirectObject(int number, PdfObject objecti, PdfWriter writer) : this(number, 0, objecti, writer)
		{
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x000754BB File Offset: 0x000744BB
		internal PdfIndirectObject(PdfIndirectReference refi, PdfObject objecti, PdfWriter writer) : this(refi.Number, refi.Generation, objecti, writer)
		{
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x000754D4 File Offset: 0x000744D4
		internal PdfIndirectObject(int number, int generation, PdfObject objecti, PdfWriter writer)
		{
			this.writer = writer;
			this.number = number;
			this.generation = generation;
			this.objecti = objecti;
			PdfEncryption pdfEncryption = null;
			if (writer != null)
			{
				pdfEncryption = writer.Encryption;
			}
			if (pdfEncryption != null)
			{
				pdfEncryption.SetHashKey(number, generation);
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x0007551D File Offset: 0x0007451D
		public PdfIndirectReference IndirectReference
		{
			get
			{
				return new PdfIndirectReference(this.objecti.Type, this.number, this.generation);
			}
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x0007553C File Offset: 0x0007453C
		internal void WriteTo(Stream os)
		{
			byte[] isobytes = DocWriter.GetISOBytes(this.number.ToString());
			os.Write(isobytes, 0, isobytes.Length);
			os.WriteByte(32);
			isobytes = DocWriter.GetISOBytes(this.generation.ToString());
			os.Write(isobytes, 0, isobytes.Length);
			os.Write(PdfIndirectObject.STARTOBJ, 0, PdfIndirectObject.STARTOBJ.Length);
			this.objecti.ToPdf(this.writer, os);
			os.Write(PdfIndirectObject.ENDOBJ, 0, PdfIndirectObject.ENDOBJ.Length);
		}

		// Token: 0x04000E1A RID: 3610
		protected int number;

		// Token: 0x04000E1B RID: 3611
		protected int generation;

		// Token: 0x04000E1C RID: 3612
		internal static byte[] STARTOBJ = DocWriter.GetISOBytes(" obj\n");

		// Token: 0x04000E1D RID: 3613
		internal static byte[] ENDOBJ = DocWriter.GetISOBytes("\nendobj\n");

		// Token: 0x04000E1E RID: 3614
		internal static int SIZEOBJ = PdfIndirectObject.STARTOBJ.Length + PdfIndirectObject.ENDOBJ.Length;

		// Token: 0x04000E1F RID: 3615
		internal PdfObject objecti;

		// Token: 0x04000E20 RID: 3616
		internal PdfWriter writer;
	}
}
