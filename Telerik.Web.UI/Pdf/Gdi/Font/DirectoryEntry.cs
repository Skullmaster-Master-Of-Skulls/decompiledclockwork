using System;
using System.Text;

namespace Telerik.Pdf.Gdi.Font
{
	// Token: 0x0200160C RID: 5644
	internal class DirectoryEntry
	{
		// Token: 0x0600DBBD RID: 56253 RVA: 0x00300AEB File Offset: 0x002FECEB
		public DirectoryEntry(string tagName)
		{
			this.tag = ((int)((byte)tagName[0]) << 24 | (int)((byte)tagName[1]) << 16 | (int)((byte)tagName[2]) << 8 | (int)((byte)tagName[3]));
			this.tagName = tagName;
		}

		// Token: 0x0600DBBE RID: 56254 RVA: 0x00300B2C File Offset: 0x002FED2C
		public DirectoryEntry(byte[] tag, int checkSum, int offset, int length)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag", "tag cannot be null");
			}
			if (tag.Length != 4)
			{
				throw new ArgumentException("tag array must be 4 bytes in size", "tag");
			}
			this.tag = ((int)tag[0] << 24 | (int)tag[1] << 16 | (int)tag[2] << 8 | (int)tag[3]);
			this.tagName = Encoding.ASCII.GetString(tag);
			this.checkSum = (long)checkSum;
			this.offset = offset;
			this.length = length;
		}

		// Token: 0x17004350 RID: 17232
		// (get) Token: 0x0600DBBF RID: 56255 RVA: 0x00300BAD File Offset: 0x002FEDAD
		public string TableName
		{
			get
			{
				return this.tagName;
			}
		}

		// Token: 0x17004351 RID: 17233
		// (get) Token: 0x0600DBC0 RID: 56256 RVA: 0x00300BB5 File Offset: 0x002FEDB5
		public int Tag
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x17004352 RID: 17234
		// (get) Token: 0x0600DBC1 RID: 56257 RVA: 0x00300BBD File Offset: 0x002FEDBD
		// (set) Token: 0x0600DBC2 RID: 56258 RVA: 0x00300BC5 File Offset: 0x002FEDC5
		public int Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.offset = value;
			}
		}

		// Token: 0x17004353 RID: 17235
		// (get) Token: 0x0600DBC3 RID: 56259 RVA: 0x00300BCE File Offset: 0x002FEDCE
		// (set) Token: 0x0600DBC4 RID: 56260 RVA: 0x00300BD6 File Offset: 0x002FEDD6
		public int Length
		{
			get
			{
				return this.length;
			}
			set
			{
				this.length = value;
			}
		}

		// Token: 0x17004354 RID: 17236
		// (get) Token: 0x0600DBC5 RID: 56261 RVA: 0x00300BDF File Offset: 0x002FEDDF
		// (set) Token: 0x0600DBC6 RID: 56262 RVA: 0x00300BE7 File Offset: 0x002FEDE7
		public long CheckSum
		{
			get
			{
				return this.checkSum;
			}
			set
			{
				this.checkSum = value;
			}
		}

		// Token: 0x0600DBC7 RID: 56263 RVA: 0x00300BF0 File Offset: 0x002FEDF0
		internal FontTable MakeTable(FontFileReader reader)
		{
			return FontTableFactory.Make(this.TableName, reader);
		}

		// Token: 0x04003D67 RID: 15719
		private int tag;

		// Token: 0x04003D68 RID: 15720
		private string tagName;

		// Token: 0x04003D69 RID: 15721
		private long checkSum;

		// Token: 0x04003D6A RID: 15722
		private int offset;

		// Token: 0x04003D6B RID: 15723
		private int length;
	}
}
