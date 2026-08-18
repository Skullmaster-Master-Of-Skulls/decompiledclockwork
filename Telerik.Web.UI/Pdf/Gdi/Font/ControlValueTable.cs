using System;

namespace Telerik.Pdf.Gdi.Font
{
	// Token: 0x02001619 RID: 5657
	internal class ControlValueTable : FontTable
	{
		// Token: 0x0600DC38 RID: 56376 RVA: 0x00302146 File Offset: 0x00300346
		public ControlValueTable(DirectoryEntry entry) : base("cvt ", entry)
		{
		}

		// Token: 0x17004363 RID: 17251
		// (get) Token: 0x0600DC39 RID: 56377 RVA: 0x00302154 File Offset: 0x00300354
		public int Count
		{
			get
			{
				return this.values.Length;
			}
		}

		// Token: 0x0600DC3A RID: 56378 RVA: 0x00302160 File Offset: 0x00300360
		protected internal override void Read(FontFileReader reader)
		{
			this.values = new short[base.Entry.Length / 2];
			for (int i = 0; i < this.values.Length; i++)
			{
				this.values[i] = reader.Stream.ReadFWord();
			}
		}

		// Token: 0x0600DC3B RID: 56379 RVA: 0x003021AC File Offset: 0x003003AC
		protected internal override void Write(FontFileWriter writer)
		{
			foreach (short value in this.values)
			{
				writer.Stream.WriteFWord((int)value);
			}
		}

		// Token: 0x04003DB7 RID: 15799
		private short[] values;
	}
}
