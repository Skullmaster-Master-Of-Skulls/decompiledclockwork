using System;

namespace Telerik.Pdf
{
	// Token: 0x02001653 RID: 5715
	public class PdfFileTrailer : PdfDictionary
	{
		// Token: 0x170043CE RID: 17358
		// (get) Token: 0x0600DD96 RID: 56726 RVA: 0x00306CB2 File Offset: 0x00304EB2
		// (set) Token: 0x0600DD97 RID: 56727 RVA: 0x00306CBA File Offset: 0x00304EBA
		public long XRefOffset
		{
			get
			{
				return this.xrefOffset;
			}
			set
			{
				this.xrefOffset = value;
			}
		}

		// Token: 0x170043CF RID: 17359
		// (get) Token: 0x0600DD98 RID: 56728 RVA: 0x00306CC3 File Offset: 0x00304EC3
		// (set) Token: 0x0600DD99 RID: 56729 RVA: 0x00306CD5 File Offset: 0x00304ED5
		public PdfNumeric Size
		{
			get
			{
				return (PdfNumeric)base[PdfName.Names.Size];
			}
			set
			{
				base[PdfName.Names.Size] = value;
			}
		}

		// Token: 0x170043D0 RID: 17360
		// (get) Token: 0x0600DD9A RID: 56730 RVA: 0x00306CE3 File Offset: 0x00304EE3
		// (set) Token: 0x0600DD9B RID: 56731 RVA: 0x00306CF5 File Offset: 0x00304EF5
		public PdfNumeric Prev
		{
			get
			{
				return (PdfNumeric)base[PdfName.Names.Prev];
			}
			set
			{
				base[PdfName.Names.Prev] = value;
			}
		}

		// Token: 0x170043D1 RID: 17361
		// (get) Token: 0x0600DD9C RID: 56732 RVA: 0x00306D03 File Offset: 0x00304F03
		// (set) Token: 0x0600DD9D RID: 56733 RVA: 0x00306D15 File Offset: 0x00304F15
		public PdfObject Root
		{
			get
			{
				return (PdfDictionary)base[PdfName.Names.Root];
			}
			set
			{
				base[PdfName.Names.Root] = value;
			}
		}

		// Token: 0x170043D2 RID: 17362
		// (get) Token: 0x0600DD9E RID: 56734 RVA: 0x00306D23 File Offset: 0x00304F23
		// (set) Token: 0x0600DD9F RID: 56735 RVA: 0x00306D35 File Offset: 0x00304F35
		public PdfObject Encrypt
		{
			get
			{
				return (PdfDictionary)base[PdfName.Names.Encrypt];
			}
			set
			{
				base[PdfName.Names.Encrypt] = value;
			}
		}

		// Token: 0x170043D3 RID: 17363
		// (get) Token: 0x0600DDA0 RID: 56736 RVA: 0x00306D43 File Offset: 0x00304F43
		// (set) Token: 0x0600DDA1 RID: 56737 RVA: 0x00306D55 File Offset: 0x00304F55
		public PdfObject Info
		{
			get
			{
				return (PdfDictionary)base[PdfName.Names.Info];
			}
			set
			{
				base[PdfName.Names.Info] = value;
			}
		}

		// Token: 0x170043D4 RID: 17364
		// (get) Token: 0x0600DDA2 RID: 56738 RVA: 0x00306D63 File Offset: 0x00304F63
		// (set) Token: 0x0600DDA3 RID: 56739 RVA: 0x00306D75 File Offset: 0x00304F75
		public PdfObject Id
		{
			get
			{
				return (PdfArray)base[PdfName.Names.Id];
			}
			set
			{
				base[PdfName.Names.Id] = value;
			}
		}

		// Token: 0x0600DDA4 RID: 56740 RVA: 0x00306D83 File Offset: 0x00304F83
		protected internal override void Write(PdfWriter writer)
		{
			writer.WriteKeywordLine(Keyword.Trailer);
			base.Write(writer);
			writer.WriteLine();
			writer.WriteKeywordLine(Keyword.StartXRef);
			writer.WriteLine(this.xrefOffset);
			writer.WriteKeyword(Keyword.Eof);
		}

		// Token: 0x04003F0B RID: 16139
		private long xrefOffset;
	}
}
