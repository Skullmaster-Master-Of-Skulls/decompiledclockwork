using System;
using System.Collections;

namespace Telerik.Pdf
{
	// Token: 0x02001669 RID: 5737
	public class PdfOutline : PdfObject
	{
		// Token: 0x0600DE0D RID: 56845 RVA: 0x00308724 File Offset: 0x00306924
		public PdfOutline(PdfObjectId objectId, string title, PdfObjectReference action) : base(objectId)
		{
			this.subentries = new ArrayList();
			this.count = 0;
			this.parent = null;
			this.prev = null;
			this.next = null;
			this.first = null;
			this.last = null;
			this.title = title;
			this.actionRef = action;
		}

		// Token: 0x0600DE0E RID: 56846 RVA: 0x0030877B File Offset: 0x0030697B
		public void SetTitle(string title)
		{
			this.title = title;
		}

		// Token: 0x0600DE0F RID: 56847 RVA: 0x00308784 File Offset: 0x00306984
		public void AddOutline(PdfOutline outline)
		{
			if (this.subentries.Count > 0)
			{
				outline.prev = (PdfOutline)this.subentries[this.subentries.Count - 1];
				outline.prev.next = outline;
			}
			else
			{
				this.first = outline;
			}
			this.subentries.Add(outline);
			outline.parent = this;
			this.IncrementCount();
			this.last = outline;
		}

		// Token: 0x0600DE10 RID: 56848 RVA: 0x003087F8 File Offset: 0x003069F8
		private void IncrementCount()
		{
			this.count++;
			if (this.parent != null)
			{
				this.parent.IncrementCount();
			}
		}

		// Token: 0x0600DE11 RID: 56849 RVA: 0x0030881C File Offset: 0x00306A1C
		protected internal override void Write(PdfWriter writer)
		{
			PdfDictionary pdfDictionary = new PdfDictionary();
			if (this.parent == null)
			{
				if (this.first != null && this.last != null)
				{
					pdfDictionary.Add(PdfName.Names.First, this.first.GetReference());
					pdfDictionary.Add(PdfName.Names.Last, this.last.GetReference());
				}
			}
			else
			{
				pdfDictionary.Add(PdfName.Names.Title, new PdfString(this.title));
				pdfDictionary.Add(PdfName.Names.Parent, this.parent.GetReference());
				if (this.first != null && this.last != null)
				{
					pdfDictionary.Add(PdfName.Names.First, this.first.GetReference());
					pdfDictionary.Add(PdfName.Names.Last, this.last.GetReference());
				}
				if (this.prev != null)
				{
					pdfDictionary.Add(PdfName.Names.Prev, this.prev.GetReference());
				}
				if (this.next != null)
				{
					pdfDictionary.Add(PdfName.Names.Next, this.next.GetReference());
				}
				if (this.count > 0)
				{
					pdfDictionary.Add(PdfName.Names.Count, new PdfNumeric(this.count));
				}
				if (this.actionRef != null)
				{
					pdfDictionary.Add(PdfName.Names.A, this.actionRef);
				}
			}
			writer.Write(pdfDictionary);
		}

		// Token: 0x04003FCB RID: 16331
		private ArrayList subentries;

		// Token: 0x04003FCC RID: 16332
		private PdfOutline parent;

		// Token: 0x04003FCD RID: 16333
		private PdfOutline prev;

		// Token: 0x04003FCE RID: 16334
		private PdfOutline next;

		// Token: 0x04003FCF RID: 16335
		private PdfOutline first;

		// Token: 0x04003FD0 RID: 16336
		private PdfOutline last;

		// Token: 0x04003FD1 RID: 16337
		private int count;

		// Token: 0x04003FD2 RID: 16338
		private string title;

		// Token: 0x04003FD3 RID: 16339
		private PdfObjectReference actionRef;
	}
}
