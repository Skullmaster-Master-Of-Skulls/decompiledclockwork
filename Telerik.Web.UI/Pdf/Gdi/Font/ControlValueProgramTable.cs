using System;

namespace Telerik.Pdf.Gdi.Font
{
	// Token: 0x02001618 RID: 5656
	internal class ControlValueProgramTable : FontTable
	{
		// Token: 0x0600DC35 RID: 56373 RVA: 0x003020E9 File Offset: 0x003002E9
		public ControlValueProgramTable(DirectoryEntry entry) : base("prep", entry)
		{
		}

		// Token: 0x0600DC36 RID: 56374 RVA: 0x003020F7 File Offset: 0x003002F7
		protected internal override void Read(FontFileReader reader)
		{
			this.instructions = new byte[base.Entry.Length];
			reader.Stream.Read(this.instructions, 0, this.instructions.Length);
		}

		// Token: 0x0600DC37 RID: 56375 RVA: 0x0030212A File Offset: 0x0030032A
		protected internal override void Write(FontFileWriter writer)
		{
			writer.Stream.Write(this.instructions, 0, this.instructions.Length);
		}

		// Token: 0x04003DB6 RID: 15798
		private byte[] instructions;
	}
}
