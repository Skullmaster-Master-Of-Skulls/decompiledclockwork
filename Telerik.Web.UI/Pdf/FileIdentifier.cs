using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Telerik.Pdf
{
	// Token: 0x020015FF RID: 5631
	public class FileIdentifier : PdfObject
	{
		// Token: 0x0600DB87 RID: 56199 RVA: 0x003004E0 File Offset: 0x002FE6E0
		public FileIdentifier()
		{
			string s = Guid.NewGuid().ToString("N");
			this.createdPart = Encoding.ASCII.GetBytes(s);
			this.modifiedPart = (byte[])this.createdPart.Clone();
		}

		// Token: 0x0600DB88 RID: 56200 RVA: 0x0030052D File Offset: 0x002FE72D
		public FileIdentifier(byte[] createdPart)
		{
			this.createdPart = (byte[])createdPart.Clone();
			this.modifiedPart = (byte[])createdPart.Clone();
		}

		// Token: 0x17004333 RID: 17203
		// (get) Token: 0x0600DB89 RID: 56201 RVA: 0x00300557 File Offset: 0x002FE757
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		public byte[] CreatedPart
		{
			get
			{
				return this.createdPart;
			}
		}

		// Token: 0x17004334 RID: 17204
		// (get) Token: 0x0600DB8A RID: 56202 RVA: 0x0030055F File Offset: 0x002FE75F
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		public byte[] ModifiedPart
		{
			get
			{
				return this.modifiedPart;
			}
		}

		// Token: 0x0600DB8B RID: 56203 RVA: 0x00300567 File Offset: 0x002FE767
		protected internal override void Write(PdfWriter writer)
		{
			writer.WriteKeyword(Keyword.ArrayBegin);
			writer.Write(PdfString.ToPdfHexadecimal(new byte[0], this.CreatedPart));
			writer.Write(PdfString.ToPdfHexadecimal(new byte[0], this.ModifiedPart));
			writer.WriteKeyword(Keyword.ArrayEnd);
		}

		// Token: 0x04003D64 RID: 15716
		private byte[] createdPart;

		// Token: 0x04003D65 RID: 15717
		private byte[] modifiedPart;
	}
}
