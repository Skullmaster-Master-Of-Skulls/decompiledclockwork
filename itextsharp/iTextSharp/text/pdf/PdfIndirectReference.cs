using System;
using System.Text;

namespace iTextSharp.text.pdf
{
	// Token: 0x020001C4 RID: 452
	public class PdfIndirectReference : PdfObject
	{
		// Token: 0x060010FC RID: 4348 RVA: 0x000600D4 File Offset: 0x0005F0D4
		protected PdfIndirectReference() : base(0)
		{
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x000600DD File Offset: 0x0005F0DD
		internal PdfIndirectReference(int type, int number, int generation) : base(0, new StringBuilder().Append(number).Append(' ').Append(generation).Append(" R").ToString())
		{
			this.number = number;
			this.generation = generation;
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x0006011B File Offset: 0x0005F11B
		internal PdfIndirectReference(int type, int number) : this(type, number, 0)
		{
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x00060126 File Offset: 0x0005F126
		public int Number
		{
			get
			{
				return this.number;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06001100 RID: 4352 RVA: 0x0006012E File Offset: 0x0005F12E
		public int Generation
		{
			get
			{
				return this.generation;
			}
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00060136 File Offset: 0x0005F136
		public override string ToString()
		{
			return new StringBuilder().Append(this.number).Append(' ').Append(this.generation).Append(" R").ToString();
		}

		// Token: 0x04000C52 RID: 3154
		protected int number;

		// Token: 0x04000C53 RID: 3155
		protected int generation;
	}
}
