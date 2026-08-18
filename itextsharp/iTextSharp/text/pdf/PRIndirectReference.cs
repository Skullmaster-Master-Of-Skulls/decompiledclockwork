using System;
using System.IO;
using System.Text;

namespace iTextSharp.text.pdf
{
	// Token: 0x020001C5 RID: 453
	public class PRIndirectReference : PdfIndirectReference
	{
		// Token: 0x06001102 RID: 4354 RVA: 0x00060169 File Offset: 0x0005F169
		internal PRIndirectReference(PdfReader reader, int number, int generation)
		{
			this.type = 10;
			this.number = number;
			this.generation = generation;
			this.reader = reader;
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x0006018E File Offset: 0x0005F18E
		internal PRIndirectReference(PdfReader reader, int number) : this(reader, number, 0)
		{
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x0006019C File Offset: 0x0005F19C
		public override void ToPdf(PdfWriter writer, Stream os)
		{
			int newObjectNumber = writer.GetNewObjectNumber(this.reader, this.number, this.generation);
			byte[] array = PdfEncodings.ConvertToBytes(new StringBuilder().Append(newObjectNumber).Append(" 0 R").ToString(), null);
			os.Write(array, 0, array.Length);
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06001105 RID: 4357 RVA: 0x000601EE File Offset: 0x0005F1EE
		public PdfReader Reader
		{
			get
			{
				return this.reader;
			}
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x000601F6 File Offset: 0x0005F1F6
		public void SetNumber(int number, int generation)
		{
			this.number = number;
			this.generation = generation;
		}

		// Token: 0x04000C54 RID: 3156
		protected PdfReader reader;
	}
}
