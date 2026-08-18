using System;
using System.IO;
using Telerik.Pdf.Security;

namespace Telerik.Pdf
{
	// Token: 0x02001651 RID: 5713
	public class PdfDocument
	{
		// Token: 0x0600DD87 RID: 56711 RVA: 0x00306B66 File Offset: 0x00304D66
		public PdfDocument(Stream stream) : this(new PdfWriter(stream))
		{
		}

		// Token: 0x0600DD88 RID: 56712 RVA: 0x00306B74 File Offset: 0x00304D74
		public PdfDocument(PdfWriter writer)
		{
			this.writer = writer;
			this.catalog = new PdfCatalog(this.NextObjectId());
			this.pages = new PdfPageTree(this.NextObjectId());
			this.catalog.Pages = this.pages;
		}

		// Token: 0x170043C7 RID: 17351
		// (get) Token: 0x0600DD89 RID: 56713 RVA: 0x00306BDE File Offset: 0x00304DDE
		// (set) Token: 0x0600DD8A RID: 56714 RVA: 0x00306BE6 File Offset: 0x00304DE6
		public PdfVersion Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
			}
		}

		// Token: 0x170043C8 RID: 17352
		// (get) Token: 0x0600DD8B RID: 56715 RVA: 0x00306BEF File Offset: 0x00304DEF
		// (set) Token: 0x0600DD8C RID: 56716 RVA: 0x00306BF7 File Offset: 0x00304DF7
		public FileIdentifier FileIdentifier
		{
			get
			{
				return this.fileId;
			}
			set
			{
				this.fileId = value;
			}
		}

		// Token: 0x170043C9 RID: 17353
		// (set) Token: 0x0600DD8D RID: 56717 RVA: 0x00306C00 File Offset: 0x00304E00
		public SecurityOptions SecurityOptions
		{
			set
			{
				this.writer.SecurityManager = new SecurityManager(value, this.fileId);
			}
		}

		// Token: 0x170043CA RID: 17354
		// (get) Token: 0x0600DD8E RID: 56718 RVA: 0x00306C19 File Offset: 0x00304E19
		public PdfCatalog Catalog
		{
			get
			{
				return this.catalog;
			}
		}

		// Token: 0x170043CB RID: 17355
		// (get) Token: 0x0600DD8F RID: 56719 RVA: 0x00306C21 File Offset: 0x00304E21
		public PdfPageTree Pages
		{
			get
			{
				return this.pages;
			}
		}

		// Token: 0x0600DD90 RID: 56720 RVA: 0x00306C2C File Offset: 0x00304E2C
		public PdfObjectId NextObjectId()
		{
			return new PdfObjectId(this.nextObjectNumber++, 0);
		}

		// Token: 0x170043CC RID: 17356
		// (get) Token: 0x0600DD91 RID: 56721 RVA: 0x00306C50 File Offset: 0x00304E50
		public int ObjectCount
		{
			get
			{
				return this.nextObjectNumber - 1;
			}
		}

		// Token: 0x170043CD RID: 17357
		// (get) Token: 0x0600DD92 RID: 56722 RVA: 0x00306C5A File Offset: 0x00304E5A
		public PdfWriter Writer
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x0600DD93 RID: 56723 RVA: 0x00306C62 File Offset: 0x00304E62
		public void WriteHeader()
		{
			this.writer.WriteHeader(this.version);
			this.writer.WriteBinaryComment();
		}

		// Token: 0x04003F05 RID: 16133
		private PdfWriter writer;

		// Token: 0x04003F06 RID: 16134
		private PdfVersion version = PdfVersion.V14;

		// Token: 0x04003F07 RID: 16135
		private FileIdentifier fileId = new FileIdentifier();

		// Token: 0x04003F08 RID: 16136
		private PdfCatalog catalog;

		// Token: 0x04003F09 RID: 16137
		private PdfPageTree pages;

		// Token: 0x04003F0A RID: 16138
		private int nextObjectNumber = 1;
	}
}
