using System;
using System.Collections.Generic;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf.events
{
	// Token: 0x02000531 RID: 1329
	public class FieldPositioningEvents : PdfPageEventHelper, IPdfPCellEvent
	{
		// Token: 0x06002D94 RID: 11668 RVA: 0x0011659D File Offset: 0x0011559D
		public FieldPositioningEvents()
		{
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x001165B0 File Offset: 0x001155B0
		public void AddField(string text, PdfFormField field)
		{
			this.genericChunkFields[text] = field;
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x001165BF File Offset: 0x001155BF
		public FieldPositioningEvents(PdfWriter writer, PdfFormField field)
		{
			this.cellField = field;
			this.fieldWriter = writer;
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x001165E0 File Offset: 0x001155E0
		public FieldPositioningEvents(PdfFormField parent, PdfFormField field)
		{
			this.cellField = field;
			this.parent = parent;
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x00116604 File Offset: 0x00115604
		public FieldPositioningEvents(PdfWriter writer, string text)
		{
			this.fieldWriter = writer;
			this.cellField = new TextField(writer, new Rectangle(0f, 0f), text)
			{
				FontSize = 14f
			}.GetTextField();
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x00116658 File Offset: 0x00115658
		public FieldPositioningEvents(PdfWriter writer, PdfFormField parent, string text)
		{
			this.parent = parent;
			this.cellField = new TextField(writer, new Rectangle(0f, 0f), text)
			{
				FontSize = 14f
			}.GetTextField();
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06002D9B RID: 11675 RVA: 0x001166B4 File Offset: 0x001156B4
		// (set) Token: 0x06002D9A RID: 11674 RVA: 0x001166AB File Offset: 0x001156AB
		public float Padding
		{
			get
			{
				return this.padding;
			}
			set
			{
				this.padding = value;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06002D9D RID: 11677 RVA: 0x001166C5 File Offset: 0x001156C5
		// (set) Token: 0x06002D9C RID: 11676 RVA: 0x001166BC File Offset: 0x001156BC
		public PdfFormField Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x06002D9E RID: 11678 RVA: 0x001166D0 File Offset: 0x001156D0
		public override void OnGenericTag(PdfWriter writer, Document document, Rectangle rect, string text)
		{
			rect.Bottom -= 3f;
			PdfFormField textField;
			this.genericChunkFields.TryGetValue(text, out textField);
			if (textField == null)
			{
				textField = new TextField(writer, new Rectangle(rect.GetLeft(this.padding), rect.GetBottom(this.padding), rect.GetRight(this.padding), rect.GetTop(this.padding)), text)
				{
					FontSize = 14f
				}.GetTextField();
			}
			else
			{
				textField.Put(PdfName.RECT, new PdfRectangle(rect.GetLeft(this.padding), rect.GetBottom(this.padding), rect.GetRight(this.padding), rect.GetTop(this.padding)));
			}
			if (this.parent == null)
			{
				writer.AddAnnotation(textField);
				return;
			}
			this.parent.AddKid(textField);
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x001167B0 File Offset: 0x001157B0
		public void CellLayout(PdfPCell cell, Rectangle rect, PdfContentByte[] canvases)
		{
			if (this.cellField == null || (this.fieldWriter == null && this.parent == null))
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("you.have.used.the.wrong.constructor.for.this.fieldpositioningevents.class"));
			}
			this.cellField.Put(PdfName.RECT, new PdfRectangle(rect.GetLeft(this.padding), rect.GetBottom(this.padding), rect.GetRight(this.padding), rect.GetTop(this.padding)));
			if (this.parent == null)
			{
				this.fieldWriter.AddAnnotation(this.cellField);
				return;
			}
			this.parent.AddKid(this.cellField);
		}

		// Token: 0x04001F69 RID: 8041
		protected Dictionary<string, PdfFormField> genericChunkFields = new Dictionary<string, PdfFormField>();

		// Token: 0x04001F6A RID: 8042
		protected PdfFormField cellField;

		// Token: 0x04001F6B RID: 8043
		protected PdfWriter fieldWriter;

		// Token: 0x04001F6C RID: 8044
		protected PdfFormField parent;

		// Token: 0x04001F6D RID: 8045
		public float padding;
	}
}
