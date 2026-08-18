using System;

namespace Telerik.Pdf
{
	// Token: 0x02001666 RID: 5734
	public sealed class PdfNumeric : PdfObject
	{
		// Token: 0x0600DE04 RID: 56836 RVA: 0x0030867C File Offset: 0x0030687C
		public PdfNumeric(decimal val)
		{
			this.val = val;
		}

		// Token: 0x0600DE05 RID: 56837 RVA: 0x0030868B File Offset: 0x0030688B
		public PdfNumeric(decimal val, PdfObjectId objectId) : base(objectId)
		{
			this.val = val;
		}

		// Token: 0x0600DE06 RID: 56838 RVA: 0x0030869B File Offset: 0x0030689B
		protected internal override void Write(PdfWriter writer)
		{
			writer.Write(this.val);
		}

		// Token: 0x04003FC7 RID: 16327
		private decimal val;
	}
}
