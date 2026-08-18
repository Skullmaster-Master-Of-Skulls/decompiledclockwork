using System;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x020004E1 RID: 1249
	public class PdfImportedPage : PdfTemplate
	{
		// Token: 0x06002ABA RID: 10938 RVA: 0x00103CC4 File Offset: 0x00102CC4
		internal PdfImportedPage(PdfReaderInstance readerInstance, PdfWriter writer, int pageNumber)
		{
			this.readerInstance = readerInstance;
			this.pageNumber = pageNumber;
			this.writer = writer;
			this.bBox = readerInstance.Reader.GetPageSize(pageNumber);
			base.SetMatrix(1f, 0f, 0f, 1f, -this.bBox.Left, -this.bBox.Bottom);
			this.type = 2;
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06002ABB RID: 10939 RVA: 0x00103D37 File Offset: 0x00102D37
		public PdfImportedPage FromReader
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06002ABC RID: 10940 RVA: 0x00103D3A File Offset: 0x00102D3A
		public int PageNumber
		{
			get
			{
				return this.pageNumber;
			}
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x00103D42 File Offset: 0x00102D42
		public override void AddImage(Image image, float a, float b, float c, float d, float e, float f)
		{
			this.ThrowError();
		}

		// Token: 0x06002ABE RID: 10942 RVA: 0x00103D4A File Offset: 0x00102D4A
		public override void AddTemplate(PdfTemplate template, float a, float b, float c, float d, float e, float f)
		{
			this.ThrowError();
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06002ABF RID: 10943 RVA: 0x00103D52 File Offset: 0x00102D52
		public override PdfContentByte Duplicate
		{
			get
			{
				this.ThrowError();
				return null;
			}
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x00103D5B File Offset: 0x00102D5B
		internal override PdfStream GetFormXObject(int compressionLevel)
		{
			return this.readerInstance.GetFormXObject(this.pageNumber, compressionLevel);
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x00103D6F File Offset: 0x00102D6F
		public override void SetColorFill(PdfSpotColor sp, float tint)
		{
			this.ThrowError();
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x00103D77 File Offset: 0x00102D77
		public override void SetColorStroke(PdfSpotColor sp, float tint)
		{
			this.ThrowError();
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06002AC3 RID: 10947 RVA: 0x00103D7F File Offset: 0x00102D7F
		internal override PdfObject Resources
		{
			get
			{
				return this.readerInstance.GetResources(this.pageNumber);
			}
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x00103D92 File Offset: 0x00102D92
		public override void SetFontAndSize(BaseFont bf, float size)
		{
			this.ThrowError();
		}

		// Token: 0x17000770 RID: 1904
		// (set) Token: 0x06002AC5 RID: 10949 RVA: 0x00103D9A File Offset: 0x00102D9A
		public override PdfTransparencyGroup Group
		{
			set
			{
				this.ThrowError();
			}
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x00103DA2 File Offset: 0x00102DA2
		internal void ThrowError()
		{
			throw new Exception(MessageLocalization.GetComposedMessage("content.can.not.be.added.to.a.pdfimportedpage"));
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06002AC7 RID: 10951 RVA: 0x00103DB3 File Offset: 0x00102DB3
		internal PdfReaderInstance PdfReaderInstance
		{
			get
			{
				return this.readerInstance;
			}
		}

		// Token: 0x04001D9C RID: 7580
		internal PdfReaderInstance readerInstance;

		// Token: 0x04001D9D RID: 7581
		internal int pageNumber;
	}
}
