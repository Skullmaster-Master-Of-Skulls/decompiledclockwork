using System;
using System.Collections.Generic;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000275 RID: 629
	public class PdfStructureTreeRoot : PdfDictionary
	{
		// Token: 0x060017BE RID: 6078 RVA: 0x0008795B File Offset: 0x0008695B
		internal PdfStructureTreeRoot(PdfWriter writer) : base(PdfName.STRUCTTREEROOT)
		{
			this.writer = writer;
			this.reference = writer.PdfIndirectReference;
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x00087988 File Offset: 0x00086988
		public void MapRole(PdfName used, PdfName standard)
		{
			PdfDictionary pdfDictionary = (PdfDictionary)base.Get(PdfName.ROLEMAP);
			if (pdfDictionary == null)
			{
				pdfDictionary = new PdfDictionary();
				base.Put(PdfName.ROLEMAP, pdfDictionary);
			}
			pdfDictionary.Put(used, standard);
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060017C0 RID: 6080 RVA: 0x000879C3 File Offset: 0x000869C3
		public PdfWriter Writer
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060017C1 RID: 6081 RVA: 0x000879CB File Offset: 0x000869CB
		public PdfIndirectReference Reference
		{
			get
			{
				return this.reference;
			}
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x000879D4 File Offset: 0x000869D4
		internal void SetPageMark(int page, PdfIndirectReference struc)
		{
			PdfArray pdfArray;
			if (!this.parentTree.ContainsKey(page))
			{
				pdfArray = new PdfArray();
				this.parentTree[page] = pdfArray;
			}
			else
			{
				pdfArray = (PdfArray)this.parentTree[page];
			}
			pdfArray.Add(struc);
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x00087A20 File Offset: 0x00086A20
		private void NodeProcess(PdfDictionary struc, PdfIndirectReference reference)
		{
			PdfObject pdfObject = struc.Get(PdfName.K);
			if (pdfObject != null && pdfObject.IsArray() && !((PdfArray)pdfObject).ArrayList[0].IsNumber())
			{
				PdfArray pdfArray = (PdfArray)pdfObject;
				List<PdfObject> arrayList = pdfArray.ArrayList;
				for (int i = 0; i < arrayList.Count; i++)
				{
					PdfStructureElement pdfStructureElement = (PdfStructureElement)arrayList[i];
					arrayList[i] = pdfStructureElement.Reference;
					this.NodeProcess(pdfStructureElement, pdfStructureElement.Reference);
				}
			}
			if (reference != null)
			{
				this.writer.AddToBody(struc, reference);
			}
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x00087AB8 File Offset: 0x00086AB8
		internal void BuildTree()
		{
			Dictionary<int, PdfIndirectReference> dictionary = new Dictionary<int, PdfIndirectReference>();
			foreach (int key in this.parentTree.Keys)
			{
				PdfArray objecta = (PdfArray)this.parentTree[key];
				dictionary[key] = this.writer.AddToBody(objecta).IndirectReference;
			}
			PdfDictionary pdfDictionary = PdfNumberTree.WriteTree<PdfIndirectReference>(dictionary, this.writer);
			if (pdfDictionary != null)
			{
				base.Put(PdfName.PARENTTREE, this.writer.AddToBody(pdfDictionary).IndirectReference);
			}
			this.NodeProcess(this, this.reference);
		}

		// Token: 0x0400101F RID: 4127
		private Dictionary<int, PdfObject> parentTree = new Dictionary<int, PdfObject>();

		// Token: 0x04001020 RID: 4128
		private PdfIndirectReference reference;

		// Token: 0x04001021 RID: 4129
		private PdfWriter writer;
	}
}
