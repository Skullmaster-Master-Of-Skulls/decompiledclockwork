using System;
using System.Collections;

namespace Telerik.Pdf.Gdi.Font
{
	// Token: 0x02001622 RID: 5666
	internal class IndexToLocationTable : FontTable
	{
		// Token: 0x0600DC66 RID: 56422 RVA: 0x00302C08 File Offset: 0x00300E08
		public IndexToLocationTable(DirectoryEntry entry) : base("loca", entry)
		{
		}

		// Token: 0x0600DC67 RID: 56423 RVA: 0x00302C16 File Offset: 0x00300E16
		public IndexToLocationTable(DirectoryEntry entry, int numOffsets) : base("loca", entry)
		{
			this.offsets = new ArrayList(numOffsets);
		}

		// Token: 0x0600DC68 RID: 56424 RVA: 0x00302C30 File Offset: 0x00300E30
		protected internal override void Read(FontFileReader reader)
		{
			FontFileStream stream = reader.Stream;
			bool isShortFormat = reader.GetHeaderTable().IsShortFormat;
			int num = reader.GetMaximumProfileTable().GlyphCount + 1;
			this.offsets = new ArrayList(num);
			for (int i = 0; i < num; i++)
			{
				this.offsets.Insert(i, isShortFormat ? (stream.ReadUShort() << 1) : stream.ReadULong());
			}
		}

		// Token: 0x0600DC69 RID: 56425 RVA: 0x00302C9C File Offset: 0x00300E9C
		protected internal override void Write(FontFileWriter writer)
		{
			foreach (object obj in this.offsets)
			{
				int num = (int)obj;
				writer.Stream.WriteULong((long)num);
			}
		}

		// Token: 0x0600DC6A RID: 56426 RVA: 0x00302CFC File Offset: 0x00300EFC
		public void Clear()
		{
			this.offsets.Clear();
		}

		// Token: 0x0600DC6B RID: 56427 RVA: 0x00302D09 File Offset: 0x00300F09
		public void AddOffset(int offset)
		{
			this.offsets.Add(offset);
		}

		// Token: 0x17004371 RID: 17265
		// (get) Token: 0x0600DC6C RID: 56428 RVA: 0x00302D1D File Offset: 0x00300F1D
		public int Count
		{
			get
			{
				return this.offsets.Count;
			}
		}

		// Token: 0x17004372 RID: 17266
		public int this[int index]
		{
			get
			{
				return (int)this.offsets[index];
			}
			set
			{
				this.offsets.Insert(index, value);
			}
		}

		// Token: 0x04003DDF RID: 15839
		private IList offsets;
	}
}
