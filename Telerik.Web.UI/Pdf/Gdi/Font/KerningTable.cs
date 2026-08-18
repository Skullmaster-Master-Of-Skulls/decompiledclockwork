using System;

namespace Telerik.Pdf.Gdi.Font
{
	// Token: 0x02001623 RID: 5667
	internal class KerningTable : FontTable
	{
		// Token: 0x0600DC6F RID: 56431 RVA: 0x00302D51 File Offset: 0x00300F51
		public KerningTable(DirectoryEntry entry) : base("kern", entry)
		{
		}

		// Token: 0x17004373 RID: 17267
		// (get) Token: 0x0600DC70 RID: 56432 RVA: 0x00302D5F File Offset: 0x00300F5F
		public bool HasKerningInfo
		{
			get
			{
				return this.hasKerningInfo;
			}
		}

		// Token: 0x17004374 RID: 17268
		// (get) Token: 0x0600DC71 RID: 56433 RVA: 0x00302D67 File Offset: 0x00300F67
		public KerningPairs KerningPairs
		{
			get
			{
				return this.pairs;
			}
		}

		// Token: 0x0600DC72 RID: 56434 RVA: 0x00302D70 File Offset: 0x00300F70
		protected internal override void Read(FontFileReader reader)
		{
			FontFileStream stream = reader.Stream;
			stream.Skip(2L);
			int num = stream.ReadUShort();
			for (int i = 0; i < num; i++)
			{
				stream.Skip(2L);
				int num2 = stream.ReadUShort();
				int num3 = stream.ReadUShort();
				if ((num3 & 1) == 1 && (num3 & 2) == 0 && num3 >> 8 == 0)
				{
					int num4 = stream.ReadUShort();
					this.hasKerningInfo = true;
					this.pairs = new KerningPairs(num4);
					stream.Skip(6L);
					for (int j = 0; j < num4; j++)
					{
						this.pairs.Add(stream.ReadUShort(), stream.ReadUShort(), (int)stream.ReadFWord());
					}
				}
				else
				{
					stream.Skip((long)(num2 - 6));
				}
			}
		}

		// Token: 0x0600DC73 RID: 56435 RVA: 0x00302E2D File Offset: 0x0030102D
		protected internal override void Write(FontFileWriter writer)
		{
			throw new InvalidOperationException("Write not supported.");
		}

		// Token: 0x04003DE0 RID: 15840
		private const int HoriztonalMask = 1;

		// Token: 0x04003DE1 RID: 15841
		private const int MinimumMask = 2;

		// Token: 0x04003DE2 RID: 15842
		private bool hasKerningInfo;

		// Token: 0x04003DE3 RID: 15843
		private KerningPairs pairs;
	}
}
