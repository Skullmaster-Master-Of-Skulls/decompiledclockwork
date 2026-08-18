using System;

namespace Telerik.Pdf
{
	// Token: 0x02001677 RID: 5751
	public class PdfXObject : PdfStream
	{
		// Token: 0x0600DE60 RID: 56928 RVA: 0x0030943B File Offset: 0x0030763B
		public PdfXObject(byte[] objectData, PdfName name, PdfObjectId objectId) : base(objectId)
		{
			this.objectData = objectData;
			this.name = name;
			base.m_dictionary[PdfName.Names.Type] = PdfName.Names.XObject;
		}

		// Token: 0x17004406 RID: 17414
		// (get) Token: 0x0600DE61 RID: 56929 RVA: 0x00309467 File Offset: 0x00307667
		// (set) Token: 0x0600DE62 RID: 56930 RVA: 0x0030947E File Offset: 0x0030767E
		public PdfName SubType
		{
			get
			{
				return (PdfName)base.m_dictionary[PdfName.Names.Subtype];
			}
			set
			{
				base.m_dictionary[PdfName.Names.Subtype] = value;
			}
		}

		// Token: 0x17004407 RID: 17415
		// (get) Token: 0x0600DE63 RID: 56931 RVA: 0x00309491 File Offset: 0x00307691
		public PdfName Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17004408 RID: 17416
		// (get) Token: 0x0600DE64 RID: 56932 RVA: 0x00309499 File Offset: 0x00307699
		public PdfDictionary Dictionary
		{
			get
			{
				return base.m_dictionary;
			}
		}

		// Token: 0x0600DE65 RID: 56933 RVA: 0x003094A1 File Offset: 0x003076A1
		protected internal override void Write(PdfWriter writer)
		{
			base.data = this.objectData;
			base.Write(writer);
		}

		// Token: 0x04003FF4 RID: 16372
		private byte[] objectData;

		// Token: 0x04003FF5 RID: 16373
		private PdfName name;
	}
}
