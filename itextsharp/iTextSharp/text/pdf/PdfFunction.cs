using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x020004E2 RID: 1250
	public class PdfFunction
	{
		// Token: 0x06002AC8 RID: 10952 RVA: 0x00103DBB File Offset: 0x00102DBB
		protected PdfFunction(PdfWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06002AC9 RID: 10953 RVA: 0x00103DCA File Offset: 0x00102DCA
		internal PdfIndirectReference Reference
		{
			get
			{
				if (this.reference == null)
				{
					this.reference = this.writer.AddToBody(this.dictionary).IndirectReference;
				}
				return this.reference;
			}
		}

		// Token: 0x06002ACA RID: 10954 RVA: 0x00103DF8 File Offset: 0x00102DF8
		public static PdfFunction Type0(PdfWriter writer, float[] domain, float[] range, int[] size, int bitsPerSample, int order, float[] encode, float[] decode, byte[] stream)
		{
			PdfFunction pdfFunction = new PdfFunction(writer);
			pdfFunction.dictionary = new PdfStream(stream);
			((PdfStream)pdfFunction.dictionary).FlateCompress(writer.CompressionLevel);
			pdfFunction.dictionary.Put(PdfName.FUNCTIONTYPE, new PdfNumber(0));
			pdfFunction.dictionary.Put(PdfName.DOMAIN, new PdfArray(domain));
			pdfFunction.dictionary.Put(PdfName.RANGE, new PdfArray(range));
			pdfFunction.dictionary.Put(PdfName.SIZE, new PdfArray(size));
			pdfFunction.dictionary.Put(PdfName.BITSPERSAMPLE, new PdfNumber(bitsPerSample));
			if (order != 1)
			{
				pdfFunction.dictionary.Put(PdfName.ORDER, new PdfNumber(order));
			}
			if (encode != null)
			{
				pdfFunction.dictionary.Put(PdfName.ENCODE, new PdfArray(encode));
			}
			if (decode != null)
			{
				pdfFunction.dictionary.Put(PdfName.DECODE, new PdfArray(decode));
			}
			return pdfFunction;
		}

		// Token: 0x06002ACB RID: 10955 RVA: 0x00103EF4 File Offset: 0x00102EF4
		public static PdfFunction Type2(PdfWriter writer, float[] domain, float[] range, float[] c0, float[] c1, float n)
		{
			PdfFunction pdfFunction = new PdfFunction(writer);
			pdfFunction.dictionary = new PdfDictionary();
			pdfFunction.dictionary.Put(PdfName.FUNCTIONTYPE, new PdfNumber(2));
			pdfFunction.dictionary.Put(PdfName.DOMAIN, new PdfArray(domain));
			if (range != null)
			{
				pdfFunction.dictionary.Put(PdfName.RANGE, new PdfArray(range));
			}
			if (c0 != null)
			{
				pdfFunction.dictionary.Put(PdfName.C0, new PdfArray(c0));
			}
			if (c1 != null)
			{
				pdfFunction.dictionary.Put(PdfName.C1, new PdfArray(c1));
			}
			pdfFunction.dictionary.Put(PdfName.N, new PdfNumber(n));
			return pdfFunction;
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x00103FA4 File Offset: 0x00102FA4
		public static PdfFunction Type3(PdfWriter writer, float[] domain, float[] range, PdfFunction[] functions, float[] bounds, float[] encode)
		{
			PdfFunction pdfFunction = new PdfFunction(writer);
			pdfFunction.dictionary = new PdfDictionary();
			pdfFunction.dictionary.Put(PdfName.FUNCTIONTYPE, new PdfNumber(3));
			pdfFunction.dictionary.Put(PdfName.DOMAIN, new PdfArray(domain));
			if (range != null)
			{
				pdfFunction.dictionary.Put(PdfName.RANGE, new PdfArray(range));
			}
			PdfArray pdfArray = new PdfArray();
			for (int i = 0; i < functions.Length; i++)
			{
				pdfArray.Add(functions[i].Reference);
			}
			pdfFunction.dictionary.Put(PdfName.FUNCTIONS, pdfArray);
			pdfFunction.dictionary.Put(PdfName.BOUNDS, new PdfArray(bounds));
			pdfFunction.dictionary.Put(PdfName.ENCODE, new PdfArray(encode));
			return pdfFunction;
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x0010406C File Offset: 0x0010306C
		public static PdfFunction Type4(PdfWriter writer, float[] domain, float[] range, string postscript)
		{
			byte[] array = new byte[postscript.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (byte)postscript[i];
			}
			PdfFunction pdfFunction = new PdfFunction(writer);
			pdfFunction.dictionary = new PdfStream(array);
			((PdfStream)pdfFunction.dictionary).FlateCompress(writer.CompressionLevel);
			pdfFunction.dictionary.Put(PdfName.FUNCTIONTYPE, new PdfNumber(4));
			pdfFunction.dictionary.Put(PdfName.DOMAIN, new PdfArray(domain));
			pdfFunction.dictionary.Put(PdfName.RANGE, new PdfArray(range));
			return pdfFunction;
		}

		// Token: 0x04001D9E RID: 7582
		protected PdfWriter writer;

		// Token: 0x04001D9F RID: 7583
		protected PdfIndirectReference reference;

		// Token: 0x04001DA0 RID: 7584
		protected PdfDictionary dictionary;
	}
}
