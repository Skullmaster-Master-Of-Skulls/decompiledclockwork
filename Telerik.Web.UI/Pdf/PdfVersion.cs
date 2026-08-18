using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Telerik.Pdf
{
	// Token: 0x02001673 RID: 5747
	public class PdfVersion
	{
		// Token: 0x0600DE39 RID: 56889 RVA: 0x0030901B File Offset: 0x0030721B
		private PdfVersion(byte major, byte minor)
		{
			this.major = major;
			this.minor = minor;
		}

		// Token: 0x170043FD RID: 17405
		// (get) Token: 0x0600DE3A RID: 56890 RVA: 0x00309031 File Offset: 0x00307231
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		public byte[] Header
		{
			get
			{
				if (this.header == null)
				{
					this.header = Encoding.ASCII.GetBytes(string.Format("%PDF-{0}.{1}", this.major, this.minor));
				}
				return this.header;
			}
		}

		// Token: 0x170043FE RID: 17406
		// (get) Token: 0x0600DE3B RID: 56891 RVA: 0x00309071 File Offset: 0x00307271
		public byte Major
		{
			get
			{
				return this.major;
			}
		}

		// Token: 0x170043FF RID: 17407
		// (get) Token: 0x0600DE3C RID: 56892 RVA: 0x00309079 File Offset: 0x00307279
		public byte Minor
		{
			get
			{
				return this.minor;
			}
		}

		// Token: 0x04003FE0 RID: 16352
		public static readonly PdfVersion V14 = new PdfVersion(1, 4);

		// Token: 0x04003FE1 RID: 16353
		public static readonly PdfVersion V13 = new PdfVersion(1, 3);

		// Token: 0x04003FE2 RID: 16354
		public static readonly PdfVersion V12 = new PdfVersion(1, 2);

		// Token: 0x04003FE3 RID: 16355
		public static readonly PdfVersion V11 = new PdfVersion(1, 1);

		// Token: 0x04003FE4 RID: 16356
		public static readonly PdfVersion V10 = new PdfVersion(1, 0);

		// Token: 0x04003FE5 RID: 16357
		private byte major;

		// Token: 0x04003FE6 RID: 16358
		private byte minor;

		// Token: 0x04003FE7 RID: 16359
		private byte[] header;
	}
}
