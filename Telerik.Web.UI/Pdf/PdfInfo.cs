using System;

namespace Telerik.Pdf
{
	// Token: 0x0200165E RID: 5726
	public class PdfInfo : PdfDictionary
	{
		// Token: 0x0600DDDC RID: 56796 RVA: 0x0030766B File Offset: 0x0030586B
		public PdfInfo(PdfObjectId objectId) : base(objectId)
		{
		}

		// Token: 0x170043E5 RID: 17381
		// (get) Token: 0x0600DDDD RID: 56797 RVA: 0x00307674 File Offset: 0x00305874
		// (set) Token: 0x0600DDDE RID: 56798 RVA: 0x00307686 File Offset: 0x00305886
		public PdfString Title
		{
			get
			{
				return (PdfString)base[PdfName.Names.Title];
			}
			set
			{
				base[PdfName.Names.Title] = value;
			}
		}

		// Token: 0x170043E6 RID: 17382
		// (get) Token: 0x0600DDDF RID: 56799 RVA: 0x00307694 File Offset: 0x00305894
		// (set) Token: 0x0600DDE0 RID: 56800 RVA: 0x003076A6 File Offset: 0x003058A6
		public PdfString Author
		{
			get
			{
				return (PdfString)base[PdfName.Names.Author];
			}
			set
			{
				base[PdfName.Names.Author] = value;
			}
		}

		// Token: 0x170043E7 RID: 17383
		// (get) Token: 0x0600DDE1 RID: 56801 RVA: 0x003076B4 File Offset: 0x003058B4
		// (set) Token: 0x0600DDE2 RID: 56802 RVA: 0x003076C6 File Offset: 0x003058C6
		public PdfString Subject
		{
			get
			{
				return (PdfString)base[PdfName.Names.Subject];
			}
			set
			{
				base[PdfName.Names.Subject] = value;
			}
		}

		// Token: 0x170043E8 RID: 17384
		// (get) Token: 0x0600DDE3 RID: 56803 RVA: 0x003076D4 File Offset: 0x003058D4
		// (set) Token: 0x0600DDE4 RID: 56804 RVA: 0x003076E6 File Offset: 0x003058E6
		public PdfString Keywords
		{
			get
			{
				return (PdfString)base[PdfName.Names.Keywords];
			}
			set
			{
				base[PdfName.Names.Keywords] = value;
			}
		}

		// Token: 0x170043E9 RID: 17385
		// (get) Token: 0x0600DDE5 RID: 56805 RVA: 0x003076F4 File Offset: 0x003058F4
		// (set) Token: 0x0600DDE6 RID: 56806 RVA: 0x00307706 File Offset: 0x00305906
		public PdfString Creator
		{
			get
			{
				return (PdfString)base[PdfName.Names.Creator];
			}
			set
			{
				base[PdfName.Names.Creator] = value;
			}
		}

		// Token: 0x170043EA RID: 17386
		// (get) Token: 0x0600DDE7 RID: 56807 RVA: 0x00307714 File Offset: 0x00305914
		// (set) Token: 0x0600DDE8 RID: 56808 RVA: 0x00307726 File Offset: 0x00305926
		public PdfString Producer
		{
			get
			{
				return (PdfString)base[PdfName.Names.Producer];
			}
			set
			{
				base[PdfName.Names.Producer] = value;
			}
		}

		// Token: 0x170043EB RID: 17387
		// (get) Token: 0x0600DDE9 RID: 56809 RVA: 0x00307734 File Offset: 0x00305934
		// (set) Token: 0x0600DDEA RID: 56810 RVA: 0x00307746 File Offset: 0x00305946
		public PdfString CreationDate
		{
			get
			{
				return (PdfString)base[PdfName.Names.CreationDate];
			}
			set
			{
				base[PdfName.Names.CreationDate] = value;
			}
		}

		// Token: 0x170043EC RID: 17388
		// (get) Token: 0x0600DDEB RID: 56811 RVA: 0x00307754 File Offset: 0x00305954
		// (set) Token: 0x0600DDEC RID: 56812 RVA: 0x00307766 File Offset: 0x00305966
		public PdfString ModDate
		{
			get
			{
				return (PdfString)base[PdfName.Names.ModDate];
			}
			set
			{
				base[PdfName.Names.ModDate] = value;
			}
		}
	}
}
