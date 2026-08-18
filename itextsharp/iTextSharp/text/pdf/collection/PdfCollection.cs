using System;

namespace iTextSharp.text.pdf.collection
{
	// Token: 0x020005F4 RID: 1524
	public class PdfCollection : PdfDictionary
	{
		// Token: 0x060033DB RID: 13275 RVA: 0x00140D74 File Offset: 0x0013FD74
		public PdfCollection(int type) : base(PdfName.COLLECTION)
		{
			switch (type)
			{
			case 1:
				base.Put(PdfName.VIEW, PdfName.T);
				return;
			case 2:
				base.Put(PdfName.VIEW, PdfName.H);
				return;
			case 3:
				base.Put(PdfName.VIEW, PdfName.C);
				return;
			default:
				base.Put(PdfName.VIEW, PdfName.D);
				return;
			}
		}

		// Token: 0x170008FD RID: 2301
		// (set) Token: 0x060033DC RID: 13276 RVA: 0x00140DE7 File Offset: 0x0013FDE7
		public string InitialDocument
		{
			set
			{
				base.Put(PdfName.D, new PdfString(value, null));
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x060033DE RID: 13278 RVA: 0x00140E09 File Offset: 0x0013FE09
		// (set) Token: 0x060033DD RID: 13277 RVA: 0x00140DFB File Offset: 0x0013FDFB
		public PdfCollectionSchema Schema
		{
			get
			{
				return (PdfCollectionSchema)base.Get(PdfName.SCHEMA);
			}
			set
			{
				base.Put(PdfName.SCHEMA, value);
			}
		}

		// Token: 0x170008FF RID: 2303
		// (set) Token: 0x060033DF RID: 13279 RVA: 0x00140E1B File Offset: 0x0013FE1B
		public PdfCollectionSort Sort
		{
			set
			{
				base.Put(PdfName.SORT, value);
			}
		}

		// Token: 0x040022F4 RID: 8948
		public const int DETAILS = 0;

		// Token: 0x040022F5 RID: 8949
		public const int TILE = 1;

		// Token: 0x040022F6 RID: 8950
		public const int HIDDEN = 2;

		// Token: 0x040022F7 RID: 8951
		public const int CUSTOM = 3;
	}
}
