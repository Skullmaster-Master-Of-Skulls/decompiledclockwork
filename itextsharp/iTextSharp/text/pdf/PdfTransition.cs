using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200039D RID: 925
	public class PdfTransition
	{
		// Token: 0x06002004 RID: 8196 RVA: 0x000BEF48 File Offset: 0x000BDF48
		public PdfTransition() : this(6)
		{
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x000BEF51 File Offset: 0x000BDF51
		public PdfTransition(int type) : this(type, 1)
		{
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x000BEF5B File Offset: 0x000BDF5B
		public PdfTransition(int type, int duration)
		{
			this.duration = duration;
			this.type = type;
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06002007 RID: 8199 RVA: 0x000BEF71 File Offset: 0x000BDF71
		public int Duration
		{
			get
			{
				return this.duration;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06002008 RID: 8200 RVA: 0x000BEF79 File Offset: 0x000BDF79
		public int Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06002009 RID: 8201 RVA: 0x000BEF84 File Offset: 0x000BDF84
		public PdfDictionary TransitionDictionary
		{
			get
			{
				PdfDictionary pdfDictionary = new PdfDictionary(PdfName.TRANS);
				switch (this.type)
				{
				case 1:
					pdfDictionary.Put(PdfName.S, PdfName.SPLIT);
					pdfDictionary.Put(PdfName.D, new PdfNumber(this.duration));
					pdfDictionary.Put(PdfName.DM, PdfName.V);
					pdfDictionary.Put(PdfName.M, PdfName.O);
					break;
				case 2:
					pdfDictionary.Put(PdfName.S, PdfName.SPLIT);
					pdfDictionary.Put(PdfName.D, new PdfNumber(this.duration));
					pdfDictionary.Put(PdfName.DM, PdfName.H);
					pdfDictionary.Put(PdfName.M, PdfName.O);
					break;
				case 3:
					pdfDictionary.Put(PdfName.S, PdfName.SPLIT);
					pdfDictionary.Put(PdfName.D, new PdfNumber(this.duration));
					pdfDictionary.Put(PdfName.DM, PdfName.V);
					pdfDictionary.Put(PdfName.M, PdfName.I);
					break;
				case 4:
					pdfDictionary.Put(PdfName.S, PdfName.SPLIT);
					pdfDictionary.Put(PdfName.D, new PdfNumber(this.duration));
					pdfDictionary.Put(PdfName.DM, PdfName.H);
					pdfDictionary.Put(PdfName.M, PdfName.I);
					break;
				case 5:
					pdfDictionary.Put(PdfName.S, PdfName.BLINDS);
					pdfDictionary.Put(PdfName.D, new PdfNumber(this.duration));
					pdfDictionary.Put(PdfName.DM, PdfName.V);
					break;
				case 6:
					pdfDictionary.Put(PdfName.S, PdfName.BLINDS);
					pdfDictionary.Put(PdfName.D, new PdfNumber(this.duration));
					pdfDictionary.Put(PdfName.DM, PdfName.H);
					break;
				case 7:
					pdfDictionary.Put(PdfName.S, PdfName.BOX);
					pdfDictionary.Put(PdfName.D, new PdfNumber(this.duration));
					pdfDictionary.Put(PdfName.M, PdfName.I);
					break;
				case 8:
					pdfDictionary.Put(PdfName.S, PdfName.BOX);
					pdfDictionary.Put(PdfName.D, new PdfNumber(this.duration));
					pdfDictionary.Put(PdfName.M, PdfName.O);
					break;
				case 9:
					pdfDictionary.Put(PdfName.S, PdfName.WIPE);
					pdfDictionary.Put(PdfName.D, new PdfNumber(this.duration));
					pdfDictionary.Put(PdfName.DI, new PdfNumber(0));
					break;
				case 10:
					pdfDictionary.Put(PdfName.S, PdfName.WIPE);
					pdfDictionary.Put(PdfName.D, new PdfNumber(this.duration));
					pdfDictionary.Put(PdfName.DI, new PdfNumber(180));
					break;
				case 11:
					pdfDictionary.Put(PdfName.S, PdfName.WIPE);
					pdfDictionary.Put(PdfName.D, new PdfNumber(this.duration));
					pdfDictionary.Put(PdfName.DI, new PdfNumber(90));
					break;
				case 12:
					pdfDictionary.Put(PdfName.S, PdfName.WIPE);
					pdfDictionary.Put(PdfName.D, new PdfNumber(this.duration));
					pdfDictionary.Put(PdfName.DI, new PdfNumber(270));
					break;
				case 13:
					pdfDictionary.Put(PdfName.S, PdfName.DISSOLVE);
					pdfDictionary.Put(PdfName.D, new PdfNumber(this.duration));
					break;
				case 14:
					pdfDictionary.Put(PdfName.S, PdfName.GLITTER);
					pdfDictionary.Put(PdfName.D, new PdfNumber(this.duration));
					pdfDictionary.Put(PdfName.DI, new PdfNumber(0));
					break;
				case 15:
					pdfDictionary.Put(PdfName.S, PdfName.GLITTER);
					pdfDictionary.Put(PdfName.D, new PdfNumber(this.duration));
					pdfDictionary.Put(PdfName.DI, new PdfNumber(270));
					break;
				case 16:
					pdfDictionary.Put(PdfName.S, PdfName.GLITTER);
					pdfDictionary.Put(PdfName.D, new PdfNumber(this.duration));
					pdfDictionary.Put(PdfName.DI, new PdfNumber(315));
					break;
				}
				return pdfDictionary;
			}
		}

		// Token: 0x04001607 RID: 5639
		public const int SPLITVOUT = 1;

		// Token: 0x04001608 RID: 5640
		public const int SPLITHOUT = 2;

		// Token: 0x04001609 RID: 5641
		public const int SPLITVIN = 3;

		// Token: 0x0400160A RID: 5642
		public const int SPLITHIN = 4;

		// Token: 0x0400160B RID: 5643
		public const int BLINDV = 5;

		// Token: 0x0400160C RID: 5644
		public const int BLINDH = 6;

		// Token: 0x0400160D RID: 5645
		public const int INBOX = 7;

		// Token: 0x0400160E RID: 5646
		public const int OUTBOX = 8;

		// Token: 0x0400160F RID: 5647
		public const int LRWIPE = 9;

		// Token: 0x04001610 RID: 5648
		public const int RLWIPE = 10;

		// Token: 0x04001611 RID: 5649
		public const int BTWIPE = 11;

		// Token: 0x04001612 RID: 5650
		public const int TBWIPE = 12;

		// Token: 0x04001613 RID: 5651
		public const int DISSOLVE = 13;

		// Token: 0x04001614 RID: 5652
		public const int LRGLITTER = 14;

		// Token: 0x04001615 RID: 5653
		public const int TBGLITTER = 15;

		// Token: 0x04001616 RID: 5654
		public const int DGLITTER = 16;

		// Token: 0x04001617 RID: 5655
		protected int duration;

		// Token: 0x04001618 RID: 5656
		protected int type;
	}
}
