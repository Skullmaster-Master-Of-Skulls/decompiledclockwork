using System;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf.collection
{
	// Token: 0x020004EF RID: 1263
	public class PdfCollectionField : PdfDictionary
	{
		// Token: 0x06002B33 RID: 11059 RVA: 0x00105D48 File Offset: 0x00104D48
		public PdfCollectionField(string name, int type) : base(PdfName.COLLECTIONFIELD)
		{
			base.Put(PdfName.N, new PdfString(name, "UnicodeBig"));
			this.fieldType = type;
			switch (type)
			{
			case 1:
				base.Put(PdfName.SUBTYPE, PdfName.D);
				return;
			case 2:
				base.Put(PdfName.SUBTYPE, PdfName.N);
				return;
			case 3:
				base.Put(PdfName.SUBTYPE, PdfName.F);
				return;
			case 4:
				base.Put(PdfName.SUBTYPE, PdfName.DESC);
				return;
			case 5:
				base.Put(PdfName.SUBTYPE, PdfName.MODDATE);
				return;
			case 6:
				base.Put(PdfName.SUBTYPE, PdfName.CREATIONDATE);
				return;
			case 7:
				base.Put(PdfName.SUBTYPE, PdfName.SIZE);
				return;
			default:
				base.Put(PdfName.SUBTYPE, PdfName.S);
				return;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (set) Token: 0x06002B34 RID: 11060 RVA: 0x00105E2A File Offset: 0x00104E2A
		public int Order
		{
			set
			{
				base.Put(PdfName.O, new PdfNumber(value));
			}
		}

		// Token: 0x17000779 RID: 1913
		// (set) Token: 0x06002B35 RID: 11061 RVA: 0x00105E3D File Offset: 0x00104E3D
		public bool Visible
		{
			set
			{
				base.Put(PdfName.V, new PdfBoolean(value));
			}
		}

		// Token: 0x1700077A RID: 1914
		// (set) Token: 0x06002B36 RID: 11062 RVA: 0x00105E50 File Offset: 0x00104E50
		public bool Editable
		{
			set
			{
				base.Put(PdfName.E, new PdfBoolean(value));
			}
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x00105E64 File Offset: 0x00104E64
		public bool IsCollectionItem()
		{
			switch (this.fieldType)
			{
			case 0:
			case 1:
			case 2:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06002B38 RID: 11064 RVA: 0x00105E90 File Offset: 0x00104E90
		public PdfObject GetValue(string v)
		{
			switch (this.fieldType)
			{
			case 0:
				return new PdfString(v, "UnicodeBig");
			case 1:
				return new PdfDate(PdfDate.Decode(v));
			case 2:
				return new PdfNumber(v);
			default:
				throw new InvalidOperationException(MessageLocalization.GetComposedMessage("1.is.not.an.acceptable.value.for.the.field.2", v, base.Get(PdfName.N).ToString()));
			}
		}

		// Token: 0x04001DD4 RID: 7636
		public const int TEXT = 0;

		// Token: 0x04001DD5 RID: 7637
		public const int DATE = 1;

		// Token: 0x04001DD6 RID: 7638
		public new const int NUMBER = 2;

		// Token: 0x04001DD7 RID: 7639
		public const int FILENAME = 3;

		// Token: 0x04001DD8 RID: 7640
		public const int DESC = 4;

		// Token: 0x04001DD9 RID: 7641
		public const int MODDATE = 5;

		// Token: 0x04001DDA RID: 7642
		public const int CREATIONDATE = 6;

		// Token: 0x04001DDB RID: 7643
		public const int SIZE = 7;

		// Token: 0x04001DDC RID: 7644
		protected internal int fieldType;
	}
}
