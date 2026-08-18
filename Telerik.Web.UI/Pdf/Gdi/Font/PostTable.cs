using System;

namespace Telerik.Pdf.Gdi.Font
{
	// Token: 0x02001627 RID: 5671
	internal class PostTable : FontTable
	{
		// Token: 0x0600DC94 RID: 56468 RVA: 0x003034F4 File Offset: 0x003016F4
		public PostTable(DirectoryEntry entry) : base("post", entry)
		{
		}

		// Token: 0x1700438A RID: 17290
		// (get) Token: 0x0600DC95 RID: 56469 RVA: 0x00303502 File Offset: 0x00301702
		public bool IsFixedPitch
		{
			get
			{
				return this.fixedPitch == 1;
			}
		}

		// Token: 0x1700438B RID: 17291
		// (get) Token: 0x0600DC96 RID: 56470 RVA: 0x0030350D File Offset: 0x0030170D
		public float ItalicAngle
		{
			get
			{
				return this.italicAngle;
			}
		}

		// Token: 0x0600DC97 RID: 56471 RVA: 0x00303518 File Offset: 0x00301718
		protected internal override void Read(FontFileReader reader)
		{
			FontFileStream stream = reader.Stream;
			this.version = stream.ReadFixed();
			this.italicAngle = (float)stream.ReadFixed() / 65536f;
			this.underlinePosition = stream.ReadFWord();
			this.underlineThickness = stream.ReadFWord();
			this.fixedPitch = stream.ReadULong();
			this.minMemType42 = stream.ReadULong();
			this.maxMemType42 = stream.ReadULong();
			this.minMemType1 = stream.ReadULong();
			this.maxMemType1 = stream.ReadULong();
		}

		// Token: 0x0600DC98 RID: 56472 RVA: 0x0030359F File Offset: 0x0030179F
		protected internal override void Write(FontFileWriter writer)
		{
			throw new NotImplementedException("Write is not implemented.");
		}

		// Token: 0x04003E2F RID: 15919
		private int version;

		// Token: 0x04003E30 RID: 15920
		private float italicAngle;

		// Token: 0x04003E31 RID: 15921
		private short underlinePosition;

		// Token: 0x04003E32 RID: 15922
		private short underlineThickness;

		// Token: 0x04003E33 RID: 15923
		private int fixedPitch;

		// Token: 0x04003E34 RID: 15924
		private int minMemType42;

		// Token: 0x04003E35 RID: 15925
		private int maxMemType42;

		// Token: 0x04003E36 RID: 15926
		private int minMemType1;

		// Token: 0x04003E37 RID: 15927
		private int maxMemType1;
	}
}
