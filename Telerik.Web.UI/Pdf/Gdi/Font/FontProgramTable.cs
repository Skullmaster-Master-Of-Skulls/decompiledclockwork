using System;

namespace Telerik.Pdf.Gdi.Font
{
	// Token: 0x0200161A RID: 5658
	internal class FontProgramTable : FontTable
	{
		// Token: 0x0600DC3C RID: 56380 RVA: 0x003021DE File Offset: 0x003003DE
		public FontProgramTable(DirectoryEntry entry) : base("fpgm", entry)
		{
		}

		// Token: 0x17004364 RID: 17252
		// (get) Token: 0x0600DC3D RID: 56381 RVA: 0x003021EC File Offset: 0x003003EC
		public int Count
		{
			get
			{
				return this.instructions.Length;
			}
		}

		// Token: 0x0600DC3E RID: 56382 RVA: 0x003021F6 File Offset: 0x003003F6
		protected internal override void Read(FontFileReader reader)
		{
			this.instructions = new byte[base.Entry.Length];
			reader.Stream.Read(this.instructions, 0, this.instructions.Length);
		}

		// Token: 0x0600DC3F RID: 56383 RVA: 0x00302229 File Offset: 0x00300429
		protected internal override void Write(FontFileWriter writer)
		{
			writer.Stream.Write(this.instructions, 0, this.instructions.Length);
		}

		// Token: 0x04003DB8 RID: 15800
		private byte[] instructions;
	}
}
