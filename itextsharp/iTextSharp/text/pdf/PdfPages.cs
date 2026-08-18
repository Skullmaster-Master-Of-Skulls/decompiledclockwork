using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x020000C7 RID: 199
	public class PdfPages
	{
		// Token: 0x060006F9 RID: 1785 RVA: 0x000229F3 File Offset: 0x000219F3
		internal PdfPages(PdfWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00022A20 File Offset: 0x00021A20
		internal void AddPage(PdfDictionary page)
		{
			if (this.pages.Count % this.leafSize == 0)
			{
				this.parents.Add(this.writer.PdfIndirectReference);
			}
			PdfIndirectReference value = this.parents[this.parents.Count - 1];
			page.Put(PdfName.PARENT, value);
			PdfIndirectReference currentPage = this.writer.CurrentPage;
			this.writer.AddToBody(page, currentPage);
			this.pages.Add(currentPage);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00022AA4 File Offset: 0x00021AA4
		internal PdfIndirectReference AddPageRef(PdfIndirectReference pageRef)
		{
			if (this.pages.Count % this.leafSize == 0)
			{
				this.parents.Add(this.writer.PdfIndirectReference);
			}
			this.pages.Add(pageRef);
			return this.parents[this.parents.Count - 1];
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00022B00 File Offset: 0x00021B00
		internal PdfIndirectReference WritePageTree()
		{
			if (this.pages.Count == 0)
			{
				throw new IOException(MessageLocalization.GetComposedMessage("the.document.has.no.pages"));
			}
			int num = 1;
			List<PdfIndirectReference> list = this.parents;
			List<PdfIndirectReference> list2 = this.pages;
			List<PdfIndirectReference> list3 = new List<PdfIndirectReference>();
			for (;;)
			{
				num *= this.leafSize;
				int num2 = this.leafSize;
				int num3 = list2.Count % this.leafSize;
				if (num3 == 0)
				{
					num3 = this.leafSize;
				}
				for (int i = 0; i < list.Count; i++)
				{
					int num4 = num;
					int count;
					if (i == list.Count - 1)
					{
						count = num3;
						num4 = this.pages.Count % num;
						if (num4 == 0)
						{
							num4 = num;
						}
					}
					else
					{
						count = num2;
					}
					PdfDictionary pdfDictionary = new PdfDictionary(PdfName.PAGES);
					pdfDictionary.Put(PdfName.COUNT, new PdfNumber(num4));
					PdfArray pdfArray = new PdfArray();
					List<PdfObject> arrayList = pdfArray.ArrayList;
					foreach (PdfObject item in list2.GetRange(i * num2, count))
					{
						arrayList.Add(item);
					}
					pdfDictionary.Put(PdfName.KIDS, pdfArray);
					if (list.Count > 1)
					{
						if (i % this.leafSize == 0)
						{
							list3.Add(this.writer.PdfIndirectReference);
						}
						pdfDictionary.Put(PdfName.PARENT, list3[i / this.leafSize]);
					}
					else
					{
						pdfDictionary.Put(PdfName.ITXT, new PdfString(Document.Release));
					}
					this.writer.AddToBody(pdfDictionary, list[i]);
				}
				if (list.Count == 1)
				{
					break;
				}
				list2 = list;
				list = list3;
				list3 = new List<PdfIndirectReference>();
			}
			this.topParent = list[0];
			return this.topParent;
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x00022CE0 File Offset: 0x00021CE0
		internal PdfIndirectReference TopParent
		{
			get
			{
				return this.topParent;
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00022CE8 File Offset: 0x00021CE8
		internal void SetLinearMode(PdfIndirectReference topParent)
		{
			if (this.parents.Count > 1)
			{
				throw new Exception(MessageLocalization.GetComposedMessage("linear.page.mode.can.only.be.called.with.a.single.parent"));
			}
			if (topParent != null)
			{
				this.topParent = topParent;
				this.parents.Clear();
				this.parents.Add(topParent);
			}
			this.leafSize = 10000000;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00022D3F File Offset: 0x00021D3F
		internal void AddPage(PdfIndirectReference page)
		{
			this.pages.Add(page);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00022D50 File Offset: 0x00021D50
		internal int ReorderPages(int[] order)
		{
			if (order == null)
			{
				return this.pages.Count;
			}
			if (this.parents.Count > 1)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("page.reordering.requires.a.single.parent.in.the.page.tree.call.pdfwriter.setlinearmode.after.open"));
			}
			if (order.Length != this.pages.Count)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("page.reordering.requires.an.array.with.the.same.size.as.the.number.of.pages"));
			}
			int count = this.pages.Count;
			bool[] array = new bool[count];
			for (int i = 0; i < count; i++)
			{
				int num = order[i];
				if (num < 1 || num > count)
				{
					throw new DocumentException(MessageLocalization.GetComposedMessage("page.reordering.requires.pages.between.1.and.1.found.2", count, num));
				}
				if (array[num - 1])
				{
					throw new DocumentException(MessageLocalization.GetComposedMessage("page.reordering.requires.no.page.repetition.page.1.is.repeated", num));
				}
				array[num - 1] = true;
			}
			PdfIndirectReference[] array2 = this.pages.ToArray();
			for (int j = 0; j < count; j++)
			{
				this.pages[j] = array2[order[j] - 1];
			}
			return count;
		}

		// Token: 0x04000378 RID: 888
		private List<PdfIndirectReference> pages = new List<PdfIndirectReference>();

		// Token: 0x04000379 RID: 889
		private List<PdfIndirectReference> parents = new List<PdfIndirectReference>();

		// Token: 0x0400037A RID: 890
		private int leafSize = 10;

		// Token: 0x0400037B RID: 891
		private PdfWriter writer;

		// Token: 0x0400037C RID: 892
		private PdfIndirectReference topParent;
	}
}
