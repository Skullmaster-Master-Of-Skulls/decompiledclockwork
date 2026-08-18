using System;

namespace Telerik.Pdf.Gdi.Font
{
	// Token: 0x02001616 RID: 5654
	internal abstract class TableNames
	{
		// Token: 0x0600DC2C RID: 56364 RVA: 0x00302048 File Offset: 0x00300248
		public static int ToUint(string tableName)
		{
			if (tableName == null)
			{
				throw new ArgumentNullException("tableName", "tableName cannot be null.");
			}
			if (tableName.Length != 4)
			{
				throw new ArgumentException("tableName must be 4 characters in length.");
			}
			return (int)((byte)tableName[3]) << 24 | (int)((byte)tableName[2]) << 16 | (int)((byte)tableName[1]) << 8 | (int)((byte)tableName[0]);
		}

		// Token: 0x04003D97 RID: 15767
		public const string Ttcf = "ttcf";

		// Token: 0x04003D98 RID: 15768
		public const string Cmap = "cmap";

		// Token: 0x04003D99 RID: 15769
		public const string Head = "head";

		// Token: 0x04003D9A RID: 15770
		public const string Hhea = "hhea";

		// Token: 0x04003D9B RID: 15771
		public const string Hmtx = "hmtx";

		// Token: 0x04003D9C RID: 15772
		public const string Maxp = "maxp";

		// Token: 0x04003D9D RID: 15773
		public const string Name = "name";

		// Token: 0x04003D9E RID: 15774
		public const string Os2 = "OS/2";

		// Token: 0x04003D9F RID: 15775
		public const string Post = "post";

		// Token: 0x04003DA0 RID: 15776
		public const string Cvt = "cvt ";

		// Token: 0x04003DA1 RID: 15777
		public const string Fpgm = "fpgm";

		// Token: 0x04003DA2 RID: 15778
		public const string Glyf = "glyf";

		// Token: 0x04003DA3 RID: 15779
		public const string Loca = "loca";

		// Token: 0x04003DA4 RID: 15780
		public const string Prep = "prep";

		// Token: 0x04003DA5 RID: 15781
		public const string CFF = "CFF ";

		// Token: 0x04003DA6 RID: 15782
		public const string VORG = "VORG";

		// Token: 0x04003DA7 RID: 15783
		public const string BASE = "BASE";

		// Token: 0x04003DA8 RID: 15784
		public const string GDEF = "GDEF";

		// Token: 0x04003DA9 RID: 15785
		public const string GPOS = "GPOS";

		// Token: 0x04003DAA RID: 15786
		public const string GSUB = "GSUB";

		// Token: 0x04003DAB RID: 15787
		public const string JSTF = "JSTF";

		// Token: 0x04003DAC RID: 15788
		public const string DSIG = "DSIG";

		// Token: 0x04003DAD RID: 15789
		public const string Gasp = "gasp";

		// Token: 0x04003DAE RID: 15790
		public const string Hdmx = "hdmx";

		// Token: 0x04003DAF RID: 15791
		public const string Kern = "kern";

		// Token: 0x04003DB0 RID: 15792
		public const string LTSH = "LTSH";

		// Token: 0x04003DB1 RID: 15793
		public const string PCLT = "PCLT";

		// Token: 0x04003DB2 RID: 15794
		public const string VDMX = "VDMX";

		// Token: 0x04003DB3 RID: 15795
		public const string Vhea = "vhea";

		// Token: 0x04003DB4 RID: 15796
		public const string Vmtx = "vmtx";
	}
}
