using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x020002CF RID: 719
	public class PdfDestination : PdfArray
	{
		// Token: 0x06001AE2 RID: 6882 RVA: 0x0009EB89 File Offset: 0x0009DB89
		public PdfDestination(int type)
		{
			if (type == 5)
			{
				this.Add(PdfName.FITB);
				return;
			}
			this.Add(PdfName.FIT);
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x0009EBB0 File Offset: 0x0009DBB0
		public PdfDestination(int type, float parameter) : base(new PdfNumber(parameter))
		{
			switch (type)
			{
			case 3:
				this.AddFirst(PdfName.FITV);
				return;
			default:
				this.AddFirst(PdfName.FITH);
				return;
			case 6:
				this.AddFirst(PdfName.FITBH);
				return;
			case 7:
				this.AddFirst(PdfName.FITBV);
				return;
			}
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x0009EC18 File Offset: 0x0009DC18
		public PdfDestination(int type, float left, float top, float zoom) : base(PdfName.XYZ)
		{
			if (left < 0f)
			{
				this.Add(PdfNull.PDFNULL);
			}
			else
			{
				this.Add(new PdfNumber(left));
			}
			if (top < 0f)
			{
				this.Add(PdfNull.PDFNULL);
			}
			else
			{
				this.Add(new PdfNumber(top));
			}
			this.Add(new PdfNumber(zoom));
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x0009EC84 File Offset: 0x0009DC84
		public PdfDestination(int type, float left, float bottom, float right, float top) : base(PdfName.FITR)
		{
			this.Add(new PdfNumber(left));
			this.Add(new PdfNumber(bottom));
			this.Add(new PdfNumber(right));
			this.Add(new PdfNumber(top));
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x0009ECD4 File Offset: 0x0009DCD4
		public PdfDestination(string dest)
		{
			string[] array = dest.Trim().Split(null);
			if (array.Length > 0)
			{
				this.Add(new PdfName(array[0]));
			}
			for (int i = 1; i < array.Length; i++)
			{
				if (array[i].Length != 0)
				{
					this.Add(new PdfNumber(array[i]));
				}
			}
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x0009ED30 File Offset: 0x0009DD30
		public bool HasPage()
		{
			return this.status;
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x0009ED38 File Offset: 0x0009DD38
		public bool AddPage(PdfIndirectReference page)
		{
			if (!this.status)
			{
				this.AddFirst(page);
				this.status = true;
				return true;
			}
			return false;
		}

		// Token: 0x040011E9 RID: 4585
		public const int XYZ = 0;

		// Token: 0x040011EA RID: 4586
		public const int FIT = 1;

		// Token: 0x040011EB RID: 4587
		public const int FITH = 2;

		// Token: 0x040011EC RID: 4588
		public const int FITV = 3;

		// Token: 0x040011ED RID: 4589
		public const int FITR = 4;

		// Token: 0x040011EE RID: 4590
		public const int FITB = 5;

		// Token: 0x040011EF RID: 4591
		public const int FITBH = 6;

		// Token: 0x040011F0 RID: 4592
		public const int FITBV = 7;

		// Token: 0x040011F1 RID: 4593
		private bool status;
	}
}
